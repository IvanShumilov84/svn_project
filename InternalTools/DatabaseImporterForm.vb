

Imports System.Data
Imports System.Data.OleDb
Imports System.Xml
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports System.Text.RegularExpressions
Imports CEL.Library
Imports CEL.HMI
Imports System.Linq

Public Class DatabaseImporterForm


    Private CELProjectSource As CELDAS
    Private CELProjectDestination As CELDAS
    Private HMIProject As New KineticaRT.Studio.Application.Project
    Private Sub btnXmlSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnXmlSource.Click
        ' Set the file dialog to filter for graphics files.
        Me.OpenFileDialog1.Filter = "Kineticart Channel Collections (*.ksc;)|*.ksc;|" + "All files (*.*)|*.*"


        Me.OpenFileDialog1.Multiselect = False
        Me.OpenFileDialog1.Title = "Please Select a File"

        Me.OpenFileDialog1.InitialDirectory = "C:\My Projects\KineticaRT"

        Dim dr As DialogResult = Me.OpenFileDialog1.ShowDialog()
        If (dr = System.Windows.Forms.DialogResult.OK) Then
            ' Read the files
            Dim file As String
            For Each file In OpenFileDialog1.FileNames
                txtXmlSource.Text = file.ToString()
            Next file
        End If
    End Sub
    Private Sub btnSelectSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectSource.Click


        ' Set the file dialog to filter for graphics files.
        Me.OpenFileDialog1.Filter = "Database (*.mdb;)|*.mdb;|" + "All files (*.*)|*.*"


        Me.OpenFileDialog1.Multiselect = False
        Me.OpenFileDialog1.Title = "Please Select a File"

        Dim directoryName As String = String.Empty
        If (txtSource.Text <> String.Empty) Then
            directoryName = System.IO.Path.GetDirectoryName(txtSource.Text) '"C:\My Projects\ISaGRAF Enhanced\Workbench\Prj"
        End If
        Me.OpenFileDialog1.InitialDirectory = directoryName

        Dim dr As DialogResult = Me.OpenFileDialog1.ShowDialog()
        If (dr = System.Windows.Forms.DialogResult.OK) Then
            ' Read the files
            Dim file As String
            For Each file In OpenFileDialog1.FileNames
                txtSource.Text = file.ToString()

                'Me.CELProjectSource = New StudioDemonstration.CELDAS()
                Me.CELProjectSource = CELDAS.Instance
                Me.CELProjectSource.Project.Number = "9999"
                Me.CELProjectSource.TechnologyName = "CELDAS™ 200"
                Me.CELProjectSource.Project.Name = "CEL Project Source"

                Me.CELProjectSource.Database.Path = System.IO.Path.GetDirectoryName(file)
                Me.CELProjectSource.Database.DatabaseFileName = System.IO.Path.GetFileName(file)

                Me.CELProjectSource.Database.WorkGroupFileName = "Secured.mdw"
                Me.CELProjectSource.Database.Password = "CEL9994"
                Me.CELProjectSource.Database.UserId = "Administrator"


                ReadEngineList(CELProjectSource, 1)


            Next file
        End If

    End Sub
    Private Sub ReadEngineList(ByVal CELProject As CELDAS, ByVal SelectStringValue As Integer)

        Dim ConnectionString As String
        Dim SelectString As String


        Dim cn As OleDbConnection = Nothing

        Try
            cboEngineList.Items.Clear()
            'Connect to the TestSession Database
            ConnectionString = CELProject.Database.OleDbConnectionString
            If SelectStringValue = 1 Then
                'SelectString = "(SELECT DISTINCT Engine_Model FROM HMI_Alias_Name_Boolean) UNION ALL " & _
                '               "(SELECT DISTINCT Engine_Model FROM HMI_Alias_Name_Numeric) UNION ALL " & _
                '               "(SELECT DISTINCT Engine_Model FROM HMI_Boolean_Channels) UNION ALL " & _
                '               "(SELECT DISTINCT Engine_Model FROM HMI_Numeric_Channels) UNION ALL " & _
                '               "(SELECT DISTINCT Engine_Model FROM HMI_Calculated_Value_Boolean) UNION ALL " & _
                '               "(SELECT DISTINCT Engine_Model FROM HMI_Calculated_Value_Numeric)"
                SelectString = "(SELECT DISTINCT Engine_Model FROM HMI_Alias_Name_Boolean) UNION ALL " &
                             "(SELECT DISTINCT Engine_Model FROM HMI_Alias_Name_Numeric) UNION ALL " &
                             "(SELECT DISTINCT Engine_Model FROM HMI_Calculated_Value_Boolean) UNION ALL " &
                             "(SELECT DISTINCT Engine_Model FROM HMI_Calculated_Value_Numeric)"
            Else
                SelectString = "(SELECT DISTINCT CollectionName FROM HMI_Boolean_Channels) UNION ALL " &
                                "(SELECT DISTINCT CollectionName FROM HMI_Numeric_Channels)"
            End If

            Dim dic As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)
            If DatabaseHelper.OpenConnection(ConnectionString, cn) Then
                Dim cmd As OleDbCommand = New OleDbCommand(SelectString, cn)
                Dim reader As OleDbDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Try
                        If SelectStringValue = 1 Then
                            dic.Add(reader.Item("Engine_Model").ToString(), 1) 'Petite passe passe pour garder 1 objet unique
                            cboEngineList.Items.Add(reader.Item("Engine_Model").ToString())
                        Else
                            dic.Add(reader.Item("CollectionName").ToString(), 1) 'Petite passe passe pour garder 1 objet unique
                            cboEngineList.Items.Add(reader.Item("CollectionName").ToString())
                        End If

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
    Private Sub btnSelectDestination_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectDestination.Click

        ' Set the file dialog to filter for graphics files.
        Me.OpenFileDialog1.Filter = "Database (*.mdb;)|*.mdb;|" + "All files (*.*)|*.*"


        Me.OpenFileDialog1.Multiselect = False
        Me.OpenFileDialog1.Title = "Please Select a File"

        Dim directoryName As String = String.Empty
        If (txtDestination.Text <> String.Empty) Then
            directoryName = System.IO.Path.GetDirectoryName(txtDestination.Text) '"C:\My Projects\ISaGRAF Enhanced\Workbench\Prj"
        End If
        Me.OpenFileDialog1.InitialDirectory = directoryName

        Dim dr As DialogResult = Me.OpenFileDialog1.ShowDialog()
        If (dr = System.Windows.Forms.DialogResult.OK) Then
            ' Read the files
            Dim file As String
            For Each file In OpenFileDialog1.FileNames
                txtDestination.Text = file.ToString()

                'Me.CELProjectDestination = New StudioDemonstration.CELDAS()
                Me.CELProjectDestination = CELDAS.Instance()

                Me.CELProjectDestination.Project.Number = "12917"
                Me.CELProjectDestination.TechnologyName = "CELDAS™ 200"
                Me.CELProjectDestination.Project.Name = "CEL Project Destination"

                ' To get whole path
                Me.CELProjectDestination.Database.Path = System.IO.Path.GetDirectoryName(file) '"\My Projects\ISaGRAF Enhanced\Workbench\Prj\PrjMaster\"
                ' To get filename with extension
                Me.CELProjectDestination.Database.DatabaseFileName = System.IO.Path.GetFileName(file) '"TestSession.mdb"
                Me.CELProjectDestination.Database.WorkGroupFileName = "Secured.mdw"
                Me.CELProjectDestination.Database.Password = "CEL9994"
                Me.CELProjectDestination.Database.UserId = "Administrator"




            Next file
        End If

        If txtSource.Text = txtDestination.Text Then
            MessageBox.Show(" Source = Destination ")
            txtDestination.Text = ""
        End If

        If cboEngineList.Items.Count = 0 Then
            ReadEngineList(CELProjectDestination, 2)
        End If
    End Sub



    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim excel As Microsoft.Office.Interop.Excel.Application
        excel = New Microsoft.Office.Interop.Excel.Application
    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click

        Dim cnStringSource As String
        Dim cnStringDestination As String
        Dim strSelectSource As String = String.Empty
        'Dim strSelectDestination As String
        Dim cnSource As OleDbConnection = Nothing
        Dim cnDestination As ADODB.Connection = Nothing
        Dim rs As New ADODB.Recordset
        Dim loggerRecordSet As New ADODB.Recordset

        Dim projectId = "18942"

        Try

            cboEngineList.Items.Clear()
            'Connect to the TestSession Database
            'petit probleme du au singleton, on changer le path avant le call
            Me.CELProjectSource.Database.Path = System.IO.Path.GetDirectoryName(txtSource.Text)
            Me.CELProjectSource.Database.DatabaseFileName = System.IO.Path.GetFileName(txtSource.Text)
            cnStringSource = Me.CELProjectSource.Database.OleDbConnectionString
            'petit probleme du au singleton, on changer le path avant le call
            Me.CELProjectSource.Database.Path = System.IO.Path.GetDirectoryName(txtDestination.Text)
            Me.CELProjectSource.Database.DatabaseFileName = System.IO.Path.GetFileName(txtDestination.Text)
            cnStringDestination = Me.CELProjectDestination.Database.OleDbConnectionString

            Dim units As IList(Of DBUnit) = UnitsHelper.RetrieveEngineeringUnits(cnStringDestination)

            If DatabaseHelper.OpenConnection(cnStringSource, cnSource) Then
                If DatabaseHelper.OpenConnection(cnStringDestination, cnDestination) Then

                    DatabaseHelper.OpenRecordset(loggerRecordSet, "SELECT * FROM Log_Report_Detail", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic)

                    If chkAliasBoolean.Checked Then
                        If rdoDatabaseV1.Checked Then
                            If chkUseLike.Checked Then
                                strSelectSource = "SELECT * FROM HMI_Alias_Name_Boolean WHERE Engine_Model like '%" & cboEngineList.Text & "%'"
                            Else
                                strSelectSource = "SELECT * FROM HMI_Alias_Name_Boolean WHERE Engine_Model = '" & cboEngineList.Text & "'"
                            End If
                        ElseIf rdoDatabaseV2.Checked Then
                            strSelectSource = "SELECT * FROM HMI_Alias_Name_Boolean WHERE CollectionName = '" & cboEngineList.Text & "'"
                        End If


                        If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM HMI_Boolean_Channels", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                            Dim cmd As OleDbCommand = New OleDbCommand(strSelectSource, cnSource)
                            Dim reader As OleDbDataReader = cmd.ExecuteReader()

                            While (reader.Read())

                                Try

                                    rs.AddNew()
                                    If rdoDatabaseV1.Checked Then
                                        rs.Fields("CollectionName").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("Engine_Model"))
                                        rs.Fields("ChannelName").Value = reader.Item("Name")
                                    ElseIf rdoDatabaseV2.Checked Then
                                        rs.Fields("CollectionName").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("CollectionName"))
                                        rs.Fields("ChannelName").Value = reader.Item("ChannelName")
                                    End If


                                    rs.Fields("Description").Value = reader.Item("Description")
                                    rs.Fields("Source").Value = reader.Item("Source")
                                    rs.Fields("Destination").Value = reader.Item("Destination")
                                    rs.Fields("ToolTip").Value = reader.Item("ToolTip")
                                    Try
                                        rs.Fields("Note").Value = reader.Item("Note")
                                    Catch ex As Exception

                                    End Try
                                    rs.Fields("FalseText").Value = reader.Item("FalseText")
                                    rs.Fields("TrueText").Value = reader.Item("TrueText")
                                    rs.Fields("FalseMessageText").Value = reader.Item("FalseMessageText")
                                    rs.Fields("TrueMessageText").Value = reader.Item("TrueMessageText")
                                    rs.Fields("SoundMessage").Value = reader.Item("SoundMessage")

                                    rs.Update()

                                Catch ex As Exception

                                    Console.WriteLine(ex.ToString())

                                End Try

                            End While
                            reader.Close()
                            DatabaseHelper.CloseRecordset(rs)
                        Else
                            MessageBox.Show("Impossible d'ouvrir HMI_Alias_Name_Boolean")
                        End If
                    End If
                    '*********************************************************************************************
                    If chkCalculatedBoolean.Checked Then
                        If rdoDatabaseV1.Checked Then
                            If chkUseLike.Checked Then
                                strSelectSource = "SELECT * FROM HMI_Calculated_Value_Boolean WHERE Engine_Model like '%" & cboEngineList.Text & "%'"
                            Else
                                strSelectSource = "SELECT * FROM HMI_Calculated_Value_Boolean WHERE Engine_Model = '" & cboEngineList.Text & "'"
                            End If
                        ElseIf rdoDatabaseV2.Checked Then
                            strSelectSource = "SELECT * FROM HMI_Calculated_Value_Boolean WHERE CollectionName = '" & cboEngineList.Text & "'"
                        End If


                        If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM HMI_Boolean_Channels", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                            Dim cmd As OleDbCommand = New OleDbCommand(strSelectSource, cnSource)
                            Dim reader As OleDbDataReader = cmd.ExecuteReader()

                            While (reader.Read())

                                Try

                                    rs.AddNew()
                                    If rdoDatabaseV1.Checked Then
                                        rs.Fields("CollectionName").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("Engine_Model"))
                                        rs.Fields("ChannelName").Value = reader.Item("Name")
                                    ElseIf rdoDatabaseV2.Checked Then
                                        rs.Fields("CollectionName").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("CollectionName"))
                                        rs.Fields("ChannelName").Value = reader.Item("ChannelName")
                                    End If



                                    rs.Fields("Description").Value = reader.Item("Description")
                                    rs.Fields("Expression").Value = reader.Item("Text")
                                    rs.Fields("ToolTip").Value = reader.Item("ToolTip")
                                    Try
                                        rs.Fields("Note").Value = reader.Item("Note")
                                    Catch ex As Exception

                                    End Try
                                    rs.Fields("FalseText").Value = reader.Item("FalseText")
                                    rs.Fields("TrueText").Value = reader.Item("TrueText")
                                    rs.Fields("FalseMessageText").Value = reader.Item("FalseMessageText")
                                    rs.Fields("TrueMessageText").Value = reader.Item("TrueMessageText")
                                    rs.Fields("SoundMessage").Value = reader.Item("SoundMessage")
                                    rs.Fields("FormulavisibleForAdminOnly").Value = reader.Item("IsFormulaVisibleForAdminOnly")

                                    rs.Update()

                                Catch ex As Exception

                                    Console.WriteLine(ex.ToString())

                                End Try

                            End While
                            reader.Close()
                            DatabaseHelper.CloseRecordset(rs)
                        Else
                            MessageBox.Show("Impossible d'ouvrir HMI_Calculated_Value_Boolean")
                        End If
                    End If

                    '*********************************************************************************************
                    If ChkHMIBoolean.Checked Then
                        If rdoDatabaseV1.Checked Then
                            If chkUseLike.Checked Then
                                strSelectSource = "SELECT * FROM HMI_Boolean_Channels WHERE Engine_Model like '%" & cboEngineList.Text & "%'"
                            Else
                                strSelectSource = "SELECT * FROM HMI_Boolean_Channels WHERE Engine_Model = '" & cboEngineList.Text & "'"
                            End If
                        ElseIf rdoDatabaseV2.Checked Then
                            strSelectSource = "SELECT * FROM HMI_Boolean_Channels WHERE CollectionName = '" & cboEngineList.Text & "'"
                        End If

                        If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM HMI_Boolean_Channels", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                            Dim cmd As OleDbCommand = New OleDbCommand(strSelectSource, cnSource)
                            Dim reader As OleDbDataReader = cmd.ExecuteReader()

                            While (reader.Read())

                                Try

                                    rs.AddNew()

                                    If rdoDatabaseV1.Checked Then
                                        rs.Fields("CollectionName").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("Engine_Model"))
                                        rs.Fields("ChannelName").Value = reader.Item("Name")
                                    ElseIf rdoDatabaseV2.Checked Then
                                        rs.Fields("CollectionName").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("CollectionName"))
                                        rs.Fields("ChannelName").Value = reader.Item("ChannelName")
                                    End If


                                    rs.Fields("Description").Value = reader.Item("Description")
                                    rs.Fields("Source").Value = reader.Item("Source")
                                    rs.Fields("Destination").Value = reader.Item("Destination")
                                    rs.Fields("ResetLatch").Value = reader.Item("ResetLatch")
                                    rs.Fields("TempNote").Value = reader.Item("TempNote")
                                    rs.Fields("Expression").Value = reader.Item("Expression")
                                    rs.Fields("OPC_Group").Value = reader.Item("OPC_Group")
                                    rs.Fields("OPC_ItemID").Value = reader.Item("OPC_ItemID")
                                    rs.Fields("ToolTip").Value = reader.Item("ToolTip")
                                    Try
                                        rs.Fields("Note").Value = reader.Item("Note")
                                    Catch ex As Exception

                                    End Try
                                    rs.Fields("FalseText").Value = reader.Item("FalseText")
                                    rs.Fields("TrueText").Value = reader.Item("TrueText")
                                    rs.Fields("FalseMessageText").Value = reader.Item("FalseMessageText")
                                    rs.Fields("TrueMessageText").Value = reader.Item("TrueMessageText")
                                    rs.Fields("SoundMessage").Value = reader.Item("SoundMessage")
                                    rs.Fields("FormulavisibleForAdminOnly").Value = reader.Item("FormulavisibleForAdminOnly")
                                    rs.Fields("InitialValue").Value = reader.Item("InitialValue")
                                    rs.Fields("FinalValue").Value = reader.Item("FinalValue")
                                    rs.Fields("Unavailable").Value = reader.Item("Unavailable")
                                    rs.Fields("Persistent").Value = reader.Item("Persistent")
                                    rs.Update()

                                Catch ex As Exception
                                    Console.WriteLine(ex.ToString())
                                    MessageBox.Show("An error occured during the read/write operation of one of the fields in the database." & vbCr &
                                                    ex.ToString,
                                                    "Error",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Exclamation)
                                End Try

                            End While
                            reader.Close()
                            DatabaseHelper.CloseRecordset(rs)
                        Else
                            MessageBox.Show("Impossible d'ouvrir HMI_Boolean_Channels")
                        End If
                    End If

                    '*********************************************************************************************
                    If chkAliasNumeric.Checked Then
                        If rdoDatabaseV1.Checked Then
                            If chkUseLike.Checked Then
                                strSelectSource = "SELECT * FROM HMI_Alias_Name_Numeric WHERE Engine_Model like '%" & cboEngineList.Text & "%'"
                            Else
                                strSelectSource = "SELECT * FROM HMI_Alias_Name_Numeric WHERE Engine_Model = '" & cboEngineList.Text & "'"
                            End If
                        ElseIf rdoDatabaseV2.Checked Then
                            strSelectSource = "SELECT * FROM HMI_Alias_Name_Numeric WHERE CollectionName = '" & cboEngineList.Text & "'"
                        End If

                        If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM HMI_Numeric_Channels", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                            Dim cmd As OleDbCommand = New OleDbCommand(strSelectSource, cnSource)
                            Dim reader As OleDbDataReader = cmd.ExecuteReader()

                            While (reader.Read())

                                Try

                                    AddLogReportDetail(projectId, loggerRecordSet, reader)

                                    rs.AddNew()

                                    If rdoDatabaseV1.Checked Then
                                        rs.Fields("CollectionName").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("Engine_Model"))
                                        rs.Fields("ChannelName").Value = reader.Item("Name")
                                    ElseIf rdoDatabaseV2.Checked Then
                                        rs.Fields("CollectionName").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("CollectionName"))
                                        rs.Fields("ChannelName").Value = reader.Item("ChannelName")
                                    End If



                                    rs.Fields("Description").Value = reader.Item("Description")
                                    rs.Fields("Source").Value = reader.Item("Source")
                                    rs.Fields("Destination").Value = reader.Item("Destination")

                                    rs.Fields("ToolTip").Value = reader.Item("ToolTip")
                                    Try
                                        rs.Fields("Note").Value = reader.Item("Note")
                                    Catch ex As Exception

                                    End Try
                                    rs.Fields("DecimalDigits").Value = reader.Item("DecimalDigits")
                                    rs.Fields("Format").Value = reader.Item("Format")
                                    rs.Fields("Minimum").Value = reader.Item("Minimum")
                                    rs.Fields("Maximum").Value = reader.Item("Maximum")

                                    rs = ExtractAndSetUnitValue(rs, units, reader)

                                    rs.Fields("Mean_Enabled").Value = reader.Item("Mean_Enabled")
                                    rs.Fields("Mean_Period").Value = reader.Item("Mean_Period")

                                    rs.Fields("Threshold_HighHigh").Value = reader.Item("Threshold_HighHigh")
                                    rs.Fields("Threshold_HighHighSource").Value = reader.Item("Threshold_HighHighSource")
                                    rs.Fields("Threshold_HighHighColor").Value = reader.Item("Threshold_HighHighColor")
                                    rs.Fields("Threshold_HighHighMessageText").Value = reader.Item("Threshold_HighHighMessageText")

                                    rs.Fields("Threshold_High").Value = reader.Item("Threshold_High")
                                    rs.Fields("Threshold_HighSource").Value = reader.Item("Threshold_HighSource")
                                    rs.Fields("Threshold_HighColor").Value = reader.Item("Threshold_HighColor")
                                    rs.Fields("Threshold_HighMessageText").Value = reader.Item("Threshold_HighMessageText")

                                    rs.Fields("Threshold_Normal").Value = reader.Item("Threshold_Normal")
                                    rs.Fields("Threshold_NormalColor").Value = reader.Item("Threshold_NormalColor")


                                    rs.Fields("Threshold_Low").Value = reader.Item("Threshold_Low")
                                    rs.Fields("Threshold_LowSource").Value = reader.Item("Threshold_LowSource")
                                    rs.Fields("Threshold_LowColor").Value = reader.Item("Threshold_LowColor")
                                    rs.Fields("Threshold_LowMessageText").Value = reader.Item("Threshold_LowMessageText")

                                    rs.Fields("Threshold_LowLow").Value = reader.Item("Threshold_LowLow")
                                    rs.Fields("Threshold_LowLowSource").Value = reader.Item("Threshold_LowLowSource")
                                    rs.Fields("Threshold_LowLowColor").Value = reader.Item("Threshold_LowLowColor")
                                    rs.Fields("Threshold_LowLowMessageText").Value = reader.Item("Threshold_LowLowMessageText")

                                    rs.Fields("High_Trigger").Value = reader.Item("High_Trigger")
                                    rs.Fields("High_Trigger_Enabled").Value = reader.Item("High_Trigger_Enabled")

                                    rs.Fields("Low_Trigger").Value = reader.Item("Low_Trigger")
                                    rs.Fields("Low_Trigger_Enabled").Value = reader.Item("Low_Trigger_Enabled")

                                    rs.Fields("SpeechMessageText").Value = reader.Item("SpeechMessageText")

                                    rs.Update()

                                Catch ex As Exception

                                    Console.WriteLine(ex.ToString())

                                End Try

                            End While
                            reader.Close()
                            DatabaseHelper.CloseRecordset(rs)
                        Else
                            MessageBox.Show("Impossible d'ouvrir HMI_Alias_Name_Numeric")
                        End If
                    End If
                    '*********************************************************************************************
                    If chkCalCulatedNumeric.Checked Then
                        If rdoDatabaseV1.Checked Then
                            If chkUseLike.Checked Then
                                strSelectSource = "SELECT * FROM HMI_Calculated_Value_Numeric WHERE Engine_Model like '%" & cboEngineList.Text & "%'"
                            Else
                                strSelectSource = "SELECT * FROM HMI_Calculated_Value_Numeric WHERE Engine_Model = '" & cboEngineList.Text & "'"
                            End If
                        ElseIf rdoDatabaseV2.Checked Then
                            strSelectSource = "SELECT * FROM HMI_Calculated_Value_Numeric WHERE CollectionName = '" & cboEngineList.Text & "'"
                        End If

                        If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM HMI_Numeric_Channels", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                            Dim cmd As OleDbCommand = New OleDbCommand(strSelectSource, cnSource)
                            Dim reader As OleDbDataReader = cmd.ExecuteReader()

                            While (reader.Read())

                                Try

                                    AddLogReportDetail(projectId, loggerRecordSet, reader)

                                    rs.AddNew()
                                    If rdoDatabaseV1.Checked Then
                                        rs.Fields("CollectionName").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("Engine_Model"))
                                        rs.Fields("ChannelName").Value = reader.Item("Name")
                                    ElseIf rdoDatabaseV2.Checked Then
                                        rs.Fields("CollectionName").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("CollectionName"))
                                        rs.Fields("ChannelName").Value = reader.Item("ChannelName")
                                    End If


                                    rs.Fields("Description").Value = reader.Item("Description")
                                    rs.Fields("Expression").Value = reader.Item("Text")
                                    rs.Fields("ToolTip").Value = reader.Item("ToolTip")
                                    Try
                                        rs.Fields("Note").Value = reader.Item("Note")
                                    Catch ex As Exception

                                    End Try
                                    rs.Fields("DecimalDigits").Value = reader.Item("DecimalDigits")
                                    rs.Fields("Format").Value = reader.Item("Format")
                                    rs.Fields("Minimum").Value = reader.Item("Minimum")
                                    rs.Fields("Maximum").Value = reader.Item("Maximum")

                                    rs = ExtractAndSetUnitValue(rs, units, reader)

                                    rs.Fields("Mean_Enabled").Value = reader.Item("Mean_Enabled")
                                    rs.Fields("Mean_Period").Value = reader.Item("Mean_Period")

                                    rs.Fields("Threshold_HighHigh").Value = reader.Item("Threshold_HighHigh")
                                    rs.Fields("Threshold_HighHighSource").Value = reader.Item("Threshold_HighHighSource")
                                    rs.Fields("Threshold_HighHighColor").Value = reader.Item("Threshold_HighHighColor")
                                    rs.Fields("Threshold_HighHighMessageText").Value = reader.Item("Threshold_HighHighMessageText")

                                    rs.Fields("Threshold_High").Value = reader.Item("Threshold_High")
                                    rs.Fields("Threshold_HighSource").Value = reader.Item("Threshold_HighSource")
                                    rs.Fields("Threshold_HighColor").Value = reader.Item("Threshold_HighColor")
                                    rs.Fields("Threshold_HighMessageText").Value = reader.Item("Threshold_HighMessageText")

                                    rs.Fields("Threshold_Normal").Value = reader.Item("Threshold_Normal")
                                    rs.Fields("Threshold_NormalColor").Value = reader.Item("Threshold_NormalColor")


                                    rs.Fields("Threshold_Low").Value = reader.Item("Threshold_Low")
                                    rs.Fields("Threshold_LowSource").Value = reader.Item("Threshold_LowSource")
                                    rs.Fields("Threshold_LowColor").Value = reader.Item("Threshold_LowColor")
                                    rs.Fields("Threshold_LowMessageText").Value = reader.Item("Threshold_LowMessageText")

                                    rs.Fields("Threshold_LowLow").Value = reader.Item("Threshold_LowLow")
                                    rs.Fields("Threshold_LowLowSource").Value = reader.Item("Threshold_LowLowSource")
                                    rs.Fields("Threshold_LowLowColor").Value = reader.Item("Threshold_LowLowColor")
                                    rs.Fields("Threshold_LowLowMessageText").Value = reader.Item("Threshold_LowLowMessageText")

                                    rs.Fields("High_Trigger").Value = reader.Item("High_Trigger")
                                    rs.Fields("High_Trigger_Enabled").Value = reader.Item("High_Trigger_Enabled")

                                    rs.Fields("Low_Trigger").Value = reader.Item("Low_Trigger")
                                    rs.Fields("Low_Trigger_Enabled").Value = reader.Item("Low_Trigger_Enabled")

                                    rs.Fields("SpeechMessageText").Value = reader.Item("SpeechMessageText")
                                    rs.Fields("FormulavisibleForAdminOnly").Value = reader.Item("IsFormulavisibleForAdminOnly")
                                    rs.Fields("EngUnitID").Value = 0
                                    rs.Update()

                                Catch ex As Exception

                                    Console.WriteLine(ex.ToString())

                                End Try

                            End While
                            reader.Close()
                            DatabaseHelper.CloseRecordset(rs)
                        Else
                            MessageBox.Show("Impossible d'ouvrir HMI_Calculated_Value_Numeric")
                        End If
                    End If

                    '*********************************************************************************************
                    If ChkHMINumeric.Checked Then

                        If rdoDatabaseV1.Checked Then
                            If chkUseLike.Checked Then
                                strSelectSource = "SELECT * FROM HMI_Numeric_Channels WHERE Engine_Model like '%" & cboEngineList.Text & "%'"
                            Else
                                strSelectSource = "SELECT * FROM HMI_Numeric_Channels WHERE Engine_Model = '" & cboEngineList.Text & "'"
                            End If
                        ElseIf rdoDatabaseV2.Checked Then
                            strSelectSource = "SELECT * FROM HMI_Numeric_Channels WHERE CollectionName = '" & cboEngineList.Text & "'"
                        End If
                        If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM HMI_Numeric_Channels", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                            Dim cmd As OleDbCommand = New OleDbCommand(strSelectSource, cnSource)
                            Dim reader As OleDbDataReader = cmd.ExecuteReader()

                            While (reader.Read())

                                Try

                                    rs.AddNew()
                                    If rdoDatabaseV1.Checked Then
                                        rs.Fields("CollectionName").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("Engine_Model"))
                                        rs.Fields("ChannelName").Value = reader.Item("Name")
                                    ElseIf rdoDatabaseV2.Checked Then
                                        rs.Fields("CollectionName").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("CollectionName"))
                                        rs.Fields("ChannelName").Value = reader.Item("ChannelName")
                                    End If


                                    rs.Fields("Description").Value = reader.Item("Description")
                                    rs.Fields("Source").Value = reader.Item("Source")
                                    rs.Fields("Destination").Value = reader.Item("Destination")
                                    rs.Fields("Expression").Value = reader.Item("Expression")
                                    rs.Fields("OPC_Group").Value = reader.Item("OPC_Group")
                                    rs.Fields("OPC_ItemID").Value = reader.Item("OPC_ItemID")
                                    rs.Fields("ToolTip").Value = reader.Item("ToolTip")
                                    Try
                                        rs.Fields("Note").Value = reader.Item("Note")
                                    Catch ex As Exception

                                    End Try
                                    rs.Fields("DecimalDigits").Value = reader.Item("DecimalDigits")
                                    rs.Fields("Format").Value = reader.Item("Format")
                                    rs.Fields("Minimum").Value = reader.Item("Minimum")
                                    rs.Fields("Maximum").Value = reader.Item("Maximum")
                                    rs.Fields("EngUnitID").Value = reader.Item("Units")
                                    rs.Fields("Logger_Enabled").Value = reader.Item("Logger_Enabled")
                                    rs.Fields("Logger_Index").Value = reader.Item("Logger_Index")
                                    rs.Fields("Log_Report_Categorie").Value = reader.Item("Log_Report_Categorie")
                                    rs.Fields("Log_Report_Order_In_Categorie").Value = reader.Item("Log_Report_Order_In_Categorie")
                                    rs.Fields("Mean_Enabled").Value = reader.Item("Mean_Enabled")
                                    rs.Fields("Mean_Period").Value = reader.Item("Mean_Period")

                                    rs.Fields("Threshold_HighHigh").Value = reader.Item("Threshold_HighHigh")
                                    rs.Fields("Threshold_HighHighSource").Value = reader.Item("Threshold_HighHighSource")
                                    rs.Fields("Threshold_HighHighColor").Value = reader.Item("Threshold_HighHighColor")
                                    rs.Fields("Threshold_HighHighMessageText").Value = reader.Item("Threshold_HighHighMessageText")

                                    rs.Fields("Threshold_High").Value = reader.Item("Threshold_High")
                                    rs.Fields("Threshold_HighSource").Value = reader.Item("Threshold_HighSource")
                                    rs.Fields("Threshold_HighColor").Value = reader.Item("Threshold_HighColor")
                                    rs.Fields("Threshold_HighMessageText").Value = reader.Item("Threshold_HighMessageText")

                                    rs.Fields("Threshold_Normal").Value = reader.Item("Threshold_Normal")
                                    rs.Fields("Threshold_NormalColor").Value = reader.Item("Threshold_NormalColor")


                                    rs.Fields("Threshold_Low").Value = reader.Item("Threshold_Low")
                                    rs.Fields("Threshold_LowSource").Value = reader.Item("Threshold_LowSource")
                                    rs.Fields("Threshold_LowColor").Value = reader.Item("Threshold_LowColor")
                                    rs.Fields("Threshold_LowMessageText").Value = reader.Item("Threshold_LowMessageText")

                                    rs.Fields("Threshold_LowLow").Value = reader.Item("Threshold_LowLow")
                                    rs.Fields("Threshold_LowLowSource").Value = reader.Item("Threshold_LowLowSource")
                                    rs.Fields("Threshold_LowLowColor").Value = reader.Item("Threshold_LowLowColor")
                                    rs.Fields("Threshold_LowLowMessageText").Value = reader.Item("Threshold_LowLowMessageText")

                                    rs.Fields("High_Trigger").Value = reader.Item("High_Trigger")
                                    rs.Fields("High_Trigger_Enabled").Value = reader.Item("High_Trigger_Enabled")

                                    rs.Fields("Low_Trigger").Value = reader.Item("Low_Trigger")
                                    rs.Fields("Low_Trigger_Enabled").Value = reader.Item("Low_Trigger_Enabled")

                                    rs.Fields("SpeechMessageText").Value = reader.Item("SpeechMessageText")
                                    Try

                                        rs.Fields("FormulavisibleForAdminOnly").Value = reader.Item("IsFormulavisibleForAdminOnly")
                                    Catch ex As Exception

                                    End Try

                                    rs.Update()

                                Catch ex As Exception

                                    Console.WriteLine(ex.ToString())

                                End Try

                            End While
                            reader.Close()
                            DatabaseHelper.CloseRecordset(rs)
                        Else
                            MessageBox.Show("Impossible d'ouvrir HMI_Numeric_Channels")
                        End If
                    End If

                    '*********************************************************************************************
                    If chkArinc429LAbel.Checked Then
                        strSelectSource = "SELECT * FROM DACN1_ARINC429_LABEL WHERE Model = '" & cboEngineList.Text & "'"
                        If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM DACN1_ARINC429_LABEL", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                            Dim cmd As OleDbCommand = New OleDbCommand(strSelectSource, cnSource)
                            Dim reader As OleDbDataReader = cmd.ExecuteReader()

                            While (reader.Read())

                                Try

                                    rs.AddNew()
                                    rs.Fields("Model").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("Model"))
                                    rs.Fields("ID").Value = reader.Item("ID")
                                    rs.Fields("Name").Value = reader.Item("Name")
                                    rs.Fields("Parameter").Value = reader.Item("Parameter")
                                    rs.Fields("Description").Value = reader.Item("Description")
                                    rs.Fields("Arinc_Channel").Value = reader.Item("Arinc_Channel")
                                    rs.Fields("Units").Value = reader.Item("Units")
                                    rs.Fields("DisplayFormat").Value = reader.Item("DisplayFormat")
                                    rs.Fields("Card_Number").Value = reader.Item("Card_Number")
                                    rs.Fields("Core_Number").Value = reader.Item("Core_Number")
                                    rs.Fields("Channel_Number").Value = reader.Item("Channel_Number")
                                    rs.Fields("TX/RX").Value = reader.Item("TX/RX")
                                    rs.Fields("Label").Value = reader.Item("Label")
                                    rs.Fields("SDI").Value = reader.Item("SDI")
                                    rs.Fields("StatusMatrix").Value = reader.Item("StatusMatrix")
                                    rs.Fields("Format").Value = reader.Item("Format")
                                    rs.Fields("Scaling_Factor").Value = reader.Item("Scaling_Factor")
                                    rs.Fields("Nb_Sig_Bits_or_Digits").Value = reader.Item("Nb_Sig_Bits_or_Digits")
                                    rs.Fields("Min_Interval").Value = reader.Item("Min_Interval")
                                    rs.Fields("Max_Interval").Value = reader.Item("Max_Interval")
                                    rs.Fields("DefaultValue").Value = reader.Item("DefaultValue")
                                    rs.Fields("Logger_Enabled").Value = reader.Item("Logger_Enabled")
                                    rs.Fields("Logger_Index").Value = reader.Item("Logger_Index")
                                    rs.Fields("Log_Report_Categorie").Value = reader.Item("Log_Report_Categorie")
                                    rs.Fields("Log_Report_Order_In_Categorie").Value = reader.Item("Log_Report_Order_In_Categorie")

                                    rs.Update()


                                Catch ex As Exception

                                    Console.WriteLine(ex.ToString())

                                End Try

                            End While
                            reader.Close()
                            DatabaseHelper.CloseRecordset(rs)
                        Else
                            MessageBox.Show("Impossible d'ouvrir DACN1_ARINC429_LABEL")
                        End If
                    End If
                    '*********************************************************************************************
                    If chkArinc429InDiscrete.Checked Then
                        strSelectSource = "SELECT * FROM DACN1_ARINC429IN_DISCRETE WHERE Model = '" & cboEngineList.Text & "'"
                        If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM DACN1_ARINC429IN_DISCRETE", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                            Dim cmd As OleDbCommand = New OleDbCommand(strSelectSource, cnSource)
                            Dim reader As OleDbDataReader = cmd.ExecuteReader()

                            While (reader.Read())

                                Try

                                    rs.AddNew()
                                    rs.Fields("Model").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("Model"))
                                    rs.Fields("ID").Value = reader.Item("ID")
                                    rs.Fields("Label").Value = reader.Item("Label")
                                    rs.Fields("Parameter_No").Value = reader.Item("Parameter_No")
                                    rs.Fields("StartBit").Value = reader.Item("StartBit")
                                    rs.Fields("StopBit").Value = reader.Item("StopBit")
                                    rs.Fields("Code").Value = reader.Item("Code")
                                    rs.Fields("Description").Value = reader.Item("Description")

                                    rs.Update()

                                Catch ex As Exception

                                    Console.WriteLine(ex.ToString())

                                End Try

                            End While
                            reader.Close()
                            DatabaseHelper.CloseRecordset(rs)
                        Else
                            MessageBox.Show("Impossible d'ouvrir DACN1_ARINC429IN_DISCRETE")
                        End If
                    End If
                    '*********************************************************************************************

                    If chkArinc429OutDiscrete.Checked Then
                        strSelectSource = "SELECT * FROM DACN1_ARINC429OUT_DISCRETE WHERE Model = '" & cboEngineList.Text & "'"
                        If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM DACN1_ARINC429OUT_DISCRETE", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                            Dim cmd As OleDbCommand = New OleDbCommand(strSelectSource, cnSource)
                            Dim reader As OleDbDataReader = cmd.ExecuteReader()

                            While (reader.Read())

                                Try

                                    rs.AddNew()
                                    rs.Fields("Model").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("Model"))
                                    rs.Fields("ID").Value = reader.Item("ID")
                                    rs.Fields("Label").Value = reader.Item("Label")
                                    rs.Fields("Parameter_No").Value = reader.Item("Parameter_No")
                                    rs.Fields("StartBit").Value = reader.Item("StartBit")
                                    rs.Fields("StopBit").Value = reader.Item("StopBit")
                                    rs.Fields("Code").Value = reader.Item("Code")
                                    rs.Fields("Description").Value = reader.Item("Description")

                                    rs.Update()

                                Catch ex As Exception

                                    Console.WriteLine(ex.ToString())

                                End Try

                            End While
                            reader.Close()
                            DatabaseHelper.CloseRecordset(rs)
                        Else
                            MessageBox.Show("Impossible d'ouvrir DACN1_ARINC429OUT_DISCRETE")
                        End If
                    End If
                    '*********************************************************************************************
                    If chkOmnibusboxUart.Checked Then

                        If rdoDatabaseV1.Checked Then
                            strSelectSource = "SELECT * FROM DACN1_OmnibusBox_IOMGR_Uart WHERE Model = '" & cboEngineList.Text & "'"
                            Debug.Assert(False)
                        ElseIf rdoDatabaseV2.Checked Then
                            strSelectSource = "SELECT * FROM DACN1_OmnibusBox_IOMGR_Uart WHERE EngineModel = '" & cboEngineList.Text & "'"
                        End If


                        If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM DACN1_ARINC429_LABEL", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                            Dim cmd As OleDbCommand = New OleDbCommand(strSelectSource, cnSource)
                            Dim reader As OleDbDataReader = cmd.ExecuteReader()

                            While (reader.Read())

                                Try

                                    rs.AddNew()
                                    If rdoDatabaseV1.Checked Then
                                        rs.Fields("EngineModel").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("Model"))
                                    ElseIf rdoDatabaseV2.Checked Then
                                        rs.Fields("EngineModel").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("EngineModel"))
                                    End If
                                    rs.Fields("ID").Value = reader.Item("ID")
                                    rs.Fields("Name").Value = reader.Item("Name")
                                    If rdoDatabaseV1.Checked Then
                                        rs.Fields("ExportToCsv").Value = True
                                    ElseIf rdoDatabaseV2.Checked Then
                                        rs.Fields("ExportToCsv").Value = reader.Item("ExportToCsv")
                                    End If
                                    rs.Fields("Arinc_Channel").Value = reader.Item("Arinc_Channel")
                                    rs.Fields("Parameter_No").Value = reader.Item("Parameter_No")
                                    rs.Fields("Parameter").Value = reader.Item("Parameter")
                                    rs.Fields("Address").Value = reader.Item("Address")
                                    rs.Fields("Data_Type").Value = reader.Item("Data_Type")
                                    rs.Fields("Units").Value = reader.Item("Units")
                                    rs.Fields("Format").Value = reader.Item("Format")
                                    rs.Fields("Card_Number").Value = reader.Item("Card_Number")
                                    rs.Fields("Core_Number").Value = reader.Item("Core_Number")
                                    rs.Fields("Channel_Number").Value = reader.Item("Channel_Number")
                                    rs.Fields("Slope").Value = reader.Item("Slope")
                                    rs.Fields("Offset").Value = reader.Item("Offset")
                                    rs.Fields("Logger_Enabled").Value = reader.Item("Logger_Enabled")
                                    rs.Fields("Logger_Index").Value = reader.Item("Logger_Index")
                                    rs.Fields("Log_Report_Categorie").Value = reader.Item("Log_Report_Categorie")
                                    rs.Fields("Log_Report_Order_In_Categorie").Value = reader.Item("Log_Report_Order_In_Categorie")

                                    rs.Update()


                                Catch ex As Exception

                                    Console.WriteLine(ex.ToString())

                                End Try

                            End While
                            reader.Close()
                            DatabaseHelper.CloseRecordset(rs)
                        Else
                            MessageBox.Show("Impossible d'ouvrir DACN1_OmnibusBox_IOMGR_Uart")
                        End If
                    End If
                    '*********************************************************************************************

                    If chkOmnibusboxUartInDiscrete.Checked Then

                        If rdoDatabaseV1.Checked Then
                            strSelectSource = "SELECT * FROM DACN1_OmnibusBox_IOMGR_UartIN_Discrete WHERE Model = '" & cboEngineList.Text & "'"
                            Debug.Assert(False)
                        ElseIf rdoDatabaseV2.Checked Then
                            strSelectSource = "SELECT * FROM DACN1_OmnibusBox_IOMGR_UartIN_Discrete WHERE EngineModel = '" & cboEngineList.Text & "'"
                        End If


                        If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM DACN1_ARINC429_LABEL", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                            Dim cmd As OleDbCommand = New OleDbCommand(strSelectSource, cnSource)
                            Dim reader As OleDbDataReader = cmd.ExecuteReader()

                            While (reader.Read())

                                Try

                                    rs.AddNew()
                                    If rdoDatabaseV1.Checked Then
                                        rs.Fields("EngineModel").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("Model"))
                                    ElseIf rdoDatabaseV2.Checked Then
                                        rs.Fields("EngineModel").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("EngineModel"))
                                    End If
                                    rs.Fields("ID").Value = reader.Item("ID")
                                    rs.Fields("Name").Value = reader.Item("Name")
                                    If rdoDatabaseV1.Checked Then
                                        rs.Fields("ExportToCsv").Value = True
                                    ElseIf rdoDatabaseV2.Checked Then
                                        rs.Fields("ExportToCsv").Value = reader.Item("ExportToCsv")
                                    End If

                                    rs.Fields("Parameter_No").Value = reader.Item("Parameter_No")
                                    rs.Fields("Uart_Channel").Value = reader.Item("Uart_Channel")
                                    rs.Fields("StartBit").Value = reader.Item("StartBit")
                                    rs.Fields("StopBit").Value = reader.Item("StopBit")
                                    rs.Fields("Description").Value = reader.Item("Description")

                                    rs.Update()


                                Catch ex As Exception

                                    Console.WriteLine(ex.ToString())

                                End Try

                            End While
                            reader.Close()
                            DatabaseHelper.CloseRecordset(rs)
                        Else
                            MessageBox.Show("Impossible d'ouvrir DACN1_OmnibusBox_IOMGR_UartIN_Discrete")
                        End If
                    End If
                    '*********************************************************************************************
                    If chkLogReport.Checked Then
                        strSelectSource = "SELECT * FROM Log_Report WHERE Engine_Model = '" & cboEngineList.Text & "'"
                        If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM Log_report", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                            Dim cmd As OleDbCommand = New OleDbCommand(strSelectSource, cnSource)
                            Dim reader As OleDbDataReader = cmd.ExecuteReader()

                            While (reader.Read())

                                Try

                                    rs.AddNew()

                                    rs.Fields("Engine_Model").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("Engine_Model"))
                                    rs.Fields("Header").Value = reader.Item("Header")
                                    rs.Fields("Position").Value = reader.Item("Position")

                                    rs.Update()

                                Catch ex As Exception

                                    Console.WriteLine(ex.ToString())

                                End Try

                            End While
                            reader.Close()
                            DatabaseHelper.CloseRecordset(rs)
                        Else
                            MessageBox.Show("Impossible d'ouvrir Log_Report")
                        End If


                        strSelectSource = "SELECT * FROM Log_Report_Categories WHERE Engine_Model = '" & cboEngineList.Text & "'"
                        If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM Log_Report_Categories", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                            Dim cmd As OleDbCommand = New OleDbCommand(strSelectSource, cnSource)
                            Dim reader As OleDbDataReader = cmd.ExecuteReader()

                            While (reader.Read())

                                Try

                                    rs.AddNew()
                                    rs.Fields("Engine_Model").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("Engine_Model"))
                                    rs.Fields("Categories").Value = reader.Item("Categories")
                                    rs.Fields("Order").Value = reader.Item("Order")

                                    rs.Update()

                                Catch ex As Exception

                                    Console.WriteLine(ex.ToString())

                                End Try

                            End While
                            reader.Close()
                            DatabaseHelper.CloseRecordset(rs)
                        Else
                            MessageBox.Show("Impossible d'ouvrir Log_Report_Categories")
                        End If
                    End If
                    '*********************************************************************************************
                    If chkDACN1_NIDAQ_BOOL.Checked Then

                        Debug.Assert(txtProjectIdDestination.Text & "" <> "txtProjectIdDestination cannot be null")
                        Debug.Assert(txtProjectIdSource.Text & "" <> "", "txtProjectIdSource cannot be null")

                        If rdoDatabaseV1.Checked Then
                            'strSelectSource = "SELECT * FROM HMI_Numeric_Channels WHERE Engine_Model = '" & cboEngineList.Text & "'"
                            Debug.Assert(False)
                        ElseIf rdoDatabaseV2.Checked Then
                            strSelectSource = "SELECT * FROM DACN1_NIDAQ_IOMGR_Bool WHERE ProjectID = " & txtProjectIdSource.Text & ""
                        End If
                        If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM DACN1_NIDAQ_IOMGR_Bool", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                            Dim cmd As OleDbCommand = New OleDbCommand(strSelectSource, cnSource)
                            Dim reader As OleDbDataReader = cmd.ExecuteReader()

                            While (reader.Read())

                                Try

                                    rs.AddNew()


                                    rs.Fields("ProjectId").Value = txtProjectIdDestination.Text
                                    rs.Fields("RDataId").Value = reader.Item("RDataId")
                                    rs.Fields("Name").Value = reader.Item("Name")
                                    rs.Fields("ExportToCsv").Value = reader.Item("ExportToCsv")
                                    rs.Fields("FieldEquipmentId").Value = reader.Item("FieldEquipmentId")
                                    rs.Fields("DeviceId").Value = reader.Item("DeviceId")
                                    rs.Fields("Channel").Value = reader.Item("Channel")
                                    rs.Fields("Bit").Value = reader.Item("Bit")
                                    rs.Fields("OPC_Group").Value = reader.Item("OPC_Group")
                                    rs.Fields("OPC_ItemId").Value = reader.Item("OPC_ItemId")


                                    rs.Update()

                                Catch ex As Exception

                                    Console.WriteLine(ex.ToString())
                                End Try

                            End While
                            reader.Close()
                            DatabaseHelper.CloseRecordset(rs)
                        Else
                            MessageBox.Show("Impossible d'ouvrir DACN1_NIDAQ_IOMGR_Bool")
                        End If
                    End If
                    '*********************************************************************************************
                    If chkDACN1_NIDAQ_REAL.Checked Then

                        Debug.Assert(txtProjectIdDestination.Text & "" <> "txtProjectIdDestination cannot be null")
                        Debug.Assert(txtProjectIdSource.Text & "" <> "", "txtProjectIdSource cannot be null")

                        If rdoDatabaseV1.Checked Then
                            'strSelectSource = "SELECT * FROM HMI_Numeric_Channels WHERE Engine_Model = '" & cboEngineList.Text & "'"
                            Debug.Assert(False)
                        ElseIf rdoDatabaseV2.Checked Then
                            strSelectSource = "SELECT * FROM DACN1_NIDAQ_IOMGR_Real WHERE ProjectID = " & txtProjectIdSource.Text & ""
                        End If
                        If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM DACN1_NIDAQ_IOMGR_Real", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                            Dim cmd As OleDbCommand = New OleDbCommand(strSelectSource, cnSource)
                            Dim reader As OleDbDataReader = cmd.ExecuteReader()

                            While (reader.Read())

                                Try

                                    rs.AddNew()


                                    rs.Fields("ProjectId").Value = txtProjectIdDestination.Text
                                    rs.Fields("RDataId").Value = reader.Item("RDataId")
                                    rs.Fields("Name").Value = reader.Item("Name")
                                    rs.Fields("ExportToCsv").Value = reader.Item("ExportToCsv")
                                    rs.Fields("FieldEquipmentId").Value = reader.Item("FieldEquipmentId")
                                    rs.Fields("DeviceId").Value = reader.Item("DeviceId")
                                    rs.Fields("Channel").Value = reader.Item("Channel")
                                    rs.Fields("MinDCValue").Value = reader.Item("MinDCValue")
                                    rs.Fields("MaxDCValue").Value = reader.Item("MaxDCValue")
                                    rs.Fields("TerminalConfiguration").Value = reader.Item("TerminalConfiguration")
                                    rs.Fields("AlarmOn").Value = reader.Item("AlarmOn")
                                    rs.Fields("StatisticsOn").Value = reader.Item("StatisticsOn")
                                    rs.Fields("TextConversionOn").Value = reader.Item("TextConversionOn")
                                    rs.Fields("DataAcquisitionOn").Value = reader.Item("DataAcquisitionOn")
                                    rs.Fields("DataProcessingOn").Value = reader.Item("DataProcessingOn")
                                    rs.Fields("Period in msec").Value = reader.Item("Period in msec")
                                    rs.Fields("TreatOnOption").Value = reader.Item("TreatOnOption")
                                    rs.Fields("DefaultValue").Value = reader.Item("DefaultValue")
                                    rs.Fields("UpperLimit").Value = reader.Item("UpperLimit")
                                    rs.Fields("LowerLimit").Value = reader.Item("LowerLimit")
                                    rs.Fields("DeltaVPlus").Value = reader.Item("DeltaVPlus")
                                    rs.Fields("DeltaVMinus").Value = reader.Item("DeltaVMinus")
                                    rs.Fields("RocPlus").Value = reader.Item("RocPlus")
                                    rs.Fields("RocMinus").Value = reader.Item("RocMinus")
                                    rs.Fields("EventOn").Value = reader.Item("EventOn")
                                    rs.Fields("HighHighLimit").Value = reader.Item("HighHighLimit")
                                    rs.Fields("HighLimit").Value = reader.Item("HighLimit")
                                    rs.Fields("LowLimit").Value = reader.Item("LowLimit")
                                    rs.Fields("LowLowLimit").Value = reader.Item("LowLowLimit")
                                    rs.Fields("HighHighAlarm").Value = reader.Item("HighHighAlarm")
                                    rs.Fields("HighAlarm").Value = reader.Item("HighAlarm")
                                    rs.Fields("LowAlarm").Value = reader.Item("LowAlarm")
                                    rs.Fields("LowLowAlarm").Value = reader.Item("LowLowAlarm")
                                    rs.Fields("HighHighBand").Value = reader.Item("HighHighBand")
                                    rs.Fields("HighBand").Value = reader.Item("HighBand")
                                    rs.Fields("LowBand").Value = reader.Item("LowBand")
                                    rs.Fields("LowLowBand").Value = reader.Item("LowLowBand")
                                    rs.Fields("HighHighAlarmOn").Value = reader.Item("HighHighAlarmOn")
                                    rs.Fields("HighAlarmOn").Value = reader.Item("HighAlarmOn")
                                    rs.Fields("LowAlarmOn").Value = reader.Item("LowAlarmOn")
                                    rs.Fields("LowLowAlarmOn").Value = reader.Item("LowLowAlarmOn")
                                    rs.Fields("HighHighAlarmSource").Value = reader.Item("HighHighAlarmSource")
                                    rs.Fields("HighAlarmSource").Value = reader.Item("HighAlarmSource")
                                    rs.Fields("LowAlarmSource").Value = reader.Item("LowAlarmSource")
                                    rs.Fields("LowLowAlarmSource").Value = reader.Item("LowLowAlarmSource")
                                    rs.Fields("TextPvFormat").Value = reader.Item("TextPvFormat")
                                    rs.Fields("HMIAliasName").Value = reader.Item("HMIAliasName")
                                    rs.Fields("FILEAliasName").Value = reader.Item("FILEAliasName")
                                    rs.Fields("EngUnit").Value = reader.Item("EngUnit")
                                    rs.Fields("EngUnitId").Value = reader.Item("EngUnitId")
                                    rs.Fields("MinMaxOn").Value = reader.Item("MinMaxOn")
                                    rs.Fields("DeltaVOn").Value = reader.Item("DeltaVOn")
                                    rs.Fields("DeltaTOn").Value = reader.Item("DeltaTOn")
                                    rs.Fields("RocOn").Value = reader.Item("RocOn")
                                    rs.Fields("SumVOn").Value = reader.Item("SumVOn")
                                    rs.Fields("SumTOn").Value = reader.Item("SumTOn")
                                    rs.Fields("AvgOn").Value = reader.Item("AvgOn")
                                    rs.Fields("AvgTypeId").Value = reader.Item("AvgTypeId")
                                    rs.Fields("AvgPeriod").Value = reader.Item("AvgPeriod")
                                    rs.Fields("AvgNbSamples").Value = reader.Item("AvgNbSamples")
                                    rs.Fields("QueueOptionId").Value = reader.Item("QueueOptionId")
                                    rs.Fields("ArchOn").Value = reader.Item("ArchOn")
                                    rs.Fields("ArchRate").Value = reader.Item("ArchRate")
                                    rs.Fields("TrigCondId").Value = reader.Item("TrigCondId")
                                    rs.Fields("Trig Level").Value = reader.Item("Trig Level")
                                    rs.Fields("Trig DeadBand").Value = reader.Item("Trig DeadBand")
                                    rs.Fields("TimeOut").Value = reader.Item("TimeOut")
                                    rs.Fields("QueueOption").Value = reader.Item("QueueOption")
                                    rs.Fields("QueueSize").Value = reader.Item("QueueSize")
                                    rs.Fields("PreTrigRecord").Value = reader.Item("PreTrigRecord")
                                    rs.Fields("MaxRecord").Value = reader.Item("MaxRecord")
                                    rs.Fields("DacqPreTrig").Value = reader.Item("DacqPreTrig")
                                    rs.Fields("ProcType").Value = reader.Item("ProcType")
                                    rs.Fields("PolyDegree").Value = reader.Item("PolyDegree")
                                    rs.Fields("NumOfArg").Value = reader.Item("NumOfArg")
                                    rs.Fields("NumOfValidArg").Value = reader.Item("NumOfValidArg")
                                    rs.Fields("Coeff1").Value = reader.Item("Coeff1")
                                    rs.Fields("Coeff2").Value = reader.Item("Coeff2")
                                    rs.Fields("Coeff3").Value = reader.Item("Coeff3")
                                    rs.Fields("Coeff4").Value = reader.Item("Coeff4")
                                    rs.Fields("Coeff5").Value = reader.Item("Coeff5")
                                    rs.Fields("Coeff6").Value = reader.Item("Coeff6")
                                    rs.Fields("Arg1").Value = reader.Item("Arg1")
                                    rs.Fields("Arg2").Value = reader.Item("Arg2")
                                    rs.Fields("Arg3").Value = reader.Item("Arg3")
                                    rs.Fields("Arg4").Value = reader.Item("Arg4")
                                    rs.Fields("Arg5").Value = reader.Item("Arg5")
                                    rs.Fields("Arg6").Value = reader.Item("Arg6")
                                    rs.Fields("Arg7").Value = reader.Item("Arg7")
                                    rs.Fields("Arg8").Value = reader.Item("Arg8")
                                    rs.Fields("Arg9").Value = reader.Item("Arg9")
                                    rs.Fields("Arg10").Value = reader.Item("Arg10")
                                    rs.Fields("Arg11").Value = reader.Item("Arg11")
                                    rs.Fields("Arg12").Value = reader.Item("Arg12")
                                    rs.Fields("Arg13").Value = reader.Item("Arg13")
                                    rs.Fields("Arg14").Value = reader.Item("Arg14")
                                    rs.Fields("Arg15").Value = reader.Item("Arg15")
                                    rs.Fields("Arg16").Value = reader.Item("Arg16")
                                    rs.Fields("Arg17").Value = reader.Item("Arg17")
                                    rs.Fields("Arg18").Value = reader.Item("Arg18")
                                    rs.Fields("Arg19").Value = reader.Item("Arg19")
                                    rs.Fields("Arg20").Value = reader.Item("Arg20")
                                    rs.Fields("HighHighAlarmOn").Value = reader.Item("HighHighAlarmOn")
                                    rs.Fields("HighAlarmOn").Value = reader.Item("HighAlarmOn")
                                    rs.Fields("LowAlarmOn").Value = reader.Item("LowAlarmOn")
                                    rs.Fields("LowLowAlarmOn").Value = reader.Item("LowLowAlarmOn")
                                    rs.Fields("HighHighAlarmName").Value = reader.Item("HighHighAlarmName")
                                    rs.Fields("HighAlarmName").Value = reader.Item("HighAlarmName")
                                    rs.Fields("LowAlarmName").Value = reader.Item("LowAlarmName")
                                    rs.Fields("LowLowAlarmName").Value = reader.Item("LowLowAlarmName")

                                    rs.Fields("ArgSrc").Value = reader.Item("ArgSrc")
                                    rs.Fields("ValidArgOn").Value = reader.Item("ValidArgOn")
                                    rs.Fields("read_v1").Value = reader.Item("read_v1")
                                    rs.Fields("read_v2").Value = reader.Item("read_v2")
                                    rs.Fields("read_v3").Value = reader.Item("read_v3")
                                    rs.Fields("read_v4").Value = reader.Item("read_v4")
                                    rs.Fields("read_v5").Value = reader.Item("read_v5")
                                    rs.Fields("read_v6").Value = reader.Item("read_v6")
                                    rs.Fields("real_v1").Value = reader.Item("real_v1")
                                    rs.Fields("real_v2").Value = reader.Item("real_v2")
                                    rs.Fields("real_v3").Value = reader.Item("real_v3")
                                    rs.Fields("real_v4").Value = reader.Item("real_v4")
                                    rs.Fields("real_v5").Value = reader.Item("real_v5")
                                    rs.Fields("real_v6").Value = reader.Item("real_v6")
                                    rs.Fields("Smoothing_Period").Value = reader.Item("Smoothing_Period")
                                    rs.Fields("margin_accuracy").Value = reader.Item("margin_accuracy")
                                    rs.Fields("nb_p_allowed").Value = reader.Item("nb_p_allowed")
                                    rs.Fields("Calibration_measured").Value = reader.Item("Calibration_measured")
                                    rs.Fields("Channel_Type").Value = reader.Item("Channel_Type")
                                    rs.Fields("Channel_Range").Value = reader.Item("Channel_Range")
                                    rs.Fields("Transducer_Range").Value = reader.Item("Transducer_Range")
                                    rs.Fields("Calibration_Range").Value = reader.Item("Calibration_Range")
                                    rs.Fields("Make1").Value = reader.Item("Make1")
                                    rs.Fields("Make2").Value = reader.Item("Make2")
                                    rs.Fields("Make3").Value = reader.Item("Make3")
                                    rs.Fields("Make4").Value = reader.Item("Make4")
                                    rs.Fields("Model1").Value = reader.Item("Model1")
                                    rs.Fields("Model2").Value = reader.Item("Model2")
                                    rs.Fields("Model3").Value = reader.Item("Model3")
                                    rs.Fields("Model4").Value = reader.Item("Model4")
                                    rs.Fields("Serial_Number1").Value = reader.Item("Serial_Number1")
                                    rs.Fields("Serial_Number2").Value = reader.Item("Serial_Number2")
                                    rs.Fields("Serial_Number3").Value = reader.Item("Serial_Number3")
                                    rs.Fields("Serial_Number4").Value = reader.Item("Serial_Number4")
                                    rs.Fields("Calibration_Due_Date1").Value = reader.Item("Calibration_Due_Date1")
                                    rs.Fields("Calibration_Due_Date2").Value = reader.Item("Calibration_Due_Date2")
                                    rs.Fields("Calibration_Due_Date3").Value = reader.Item("Calibration_Due_Date3")
                                    rs.Fields("Calibration_Due_Date4").Value = reader.Item("Calibration_Due_Date4")
                                    rs.Fields("Comments").Value = reader.Item("Comments")
                                    rs.Fields("Calibrated_By").Value = reader.Item("Calibrated_By")
                                    rs.Fields("Assistant").Value = reader.Item("Assistant")
                                    rs.Fields("Approval").Value = reader.Item("Approval")
                                    rs.Fields("LastCalibDate").Value = reader.Item("LastCalibDate")
                                    rs.Fields("OPC_Group").Value = reader.Item("OPC_Group")
                                    rs.Fields("OPC_ItemId").Value = reader.Item("OPC_ItemId")


                                    rs.Update()

                                Catch ex As Exception

                                    Console.WriteLine(ex.ToString())
                                End Try

                            End While
                            reader.Close()
                            DatabaseHelper.CloseRecordset(rs)
                        Else
                            MessageBox.Show("Impossible d'ouvrir DACN1_NIDAQ_IOMGR_Real")
                        End If
                    End If
                    '*********************************************************************************************
                    If chkDACN1_SERIAL_BOOL.Checked Then

                        Debug.Assert(txtProjectIdDestination.Text & "" <> "txtProjectIdDestination cannot be null")
                        Debug.Assert(txtProjectIdSource.Text & "" <> "", "txtProjectIdSource cannot be null")

                        If rdoDatabaseV1.Checked Then
                            'strSelectSource = "SELECT * FROM HMI_Numeric_Channels WHERE Engine_Model = '" & cboEngineList.Text & "'"
                            Debug.Assert(False)
                        ElseIf rdoDatabaseV2.Checked Then
                            strSelectSource = "SELECT * FROM DACN1_SERIAL_IOMGR_Bool WHERE ProjectID = " & txtProjectIdSource.Text & ""
                        End If
                        If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM DACN1_SERIAL_IOMGR_Bool", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                            Dim cmd As OleDbCommand = New OleDbCommand(strSelectSource, cnSource)
                            Dim reader As OleDbDataReader = cmd.ExecuteReader()

                            While (reader.Read())

                                Try

                                    rs.AddNew()


                                    rs.Fields("ProjectId").Value = txtProjectIdDestination.Text
                                    rs.Fields("RDataId").Value = reader.Item("RDataId")
                                    rs.Fields("Name").Value = reader.Item("Name")
                                    rs.Fields("ExportToCsv").Value = reader.Item("ExportToCsv")
                                    rs.Fields("FieldEquipmentId").Value = reader.Item("FieldEquipmentId")
                                    rs.Fields("DeviceId").Value = reader.Item("DeviceId")
                                    rs.Fields("Channel").Value = reader.Item("Channel")
                                    rs.Fields("Bit").Value = reader.Item("Bit")
                                    rs.Fields("OPC_Group").Value = reader.Item("OPC_Group")
                                    rs.Fields("OPC_ItemId").Value = reader.Item("OPC_ItemId")


                                    rs.Update()

                                Catch ex As Exception

                                    Console.WriteLine(ex.ToString())
                                End Try

                            End While
                            reader.Close()
                            DatabaseHelper.CloseRecordset(rs)
                        Else
                            MessageBox.Show("Impossible d'ouvrir DACN1_SERIAL_IOMGR_Bool")
                        End If
                    End If
                    '*********************************************************************************************
                    If chkDACN1_SERIAL_REAL.Checked Then

                        Debug.Assert(txtProjectIdDestination.Text & "" <> "txtProjectIdDestination cannot be null")
                        Debug.Assert(txtProjectIdSource.Text & "" <> "", "txtProjectIdSource cannot be null")

                        If rdoDatabaseV1.Checked Then
                            'strSelectSource = "SELECT * FROM HMI_Numeric_Channels WHERE Engine_Model = '" & cboEngineList.Text & "'"
                            Debug.Assert(False)
                        ElseIf rdoDatabaseV2.Checked Then
                            strSelectSource = "SELECT * FROM DACN1_SERIAL_IOMGR_Real WHERE ProjectID = " & txtProjectIdSource.Text & ""
                        End If
                        If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM DACN1_SERIAL_IOMGR_Real", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                            Dim cmd As OleDbCommand = New OleDbCommand(strSelectSource, cnSource)
                            Dim reader As OleDbDataReader = cmd.ExecuteReader()

                            While (reader.Read())

                                Try

                                    rs.AddNew()


                                    rs.Fields("ProjectId").Value = txtProjectIdDestination.Text
                                    rs.Fields("RDataId").Value = reader.Item("RDataId")
                                    rs.Fields("Name").Value = reader.Item("Name")
                                    rs.Fields("ExportToCsv").Value = reader.Item("ExportToCsv")
                                    rs.Fields("FieldEquipmentId").Value = reader.Item("FieldEquipmentId")
                                    rs.Fields("DeviceId").Value = reader.Item("DeviceId")
                                    rs.Fields("Base_Address").Value = reader.Item("Base_Address")
                                    rs.Fields("Physical_Channel").Value = reader.Item("Physical_Channel")
                                    rs.Fields("AlarmOn").Value = reader.Item("AlarmOn")
                                    rs.Fields("StatisticsOn").Value = reader.Item("StatisticsOn")
                                    rs.Fields("TextConversionOn").Value = reader.Item("TextConversionOn")
                                    rs.Fields("DataAcquisitionOn").Value = reader.Item("DataAcquisitionOn")
                                    rs.Fields("DataProcessingOn").Value = reader.Item("DataProcessingOn")
                                    rs.Fields("Period in msec").Value = reader.Item("Period in msec")
                                    rs.Fields("TreatOnOption").Value = reader.Item("TreatOnOption")
                                    rs.Fields("DefaultValue").Value = reader.Item("DefaultValue")
                                    rs.Fields("UpperLimit").Value = reader.Item("UpperLimit")
                                    rs.Fields("LowerLimit").Value = reader.Item("LowerLimit")
                                    rs.Fields("DeltaVPlus").Value = reader.Item("DeltaVPlus")
                                    rs.Fields("DeltaVMinus").Value = reader.Item("DeltaVMinus")
                                    rs.Fields("RocPlus").Value = reader.Item("RocPlus")
                                    rs.Fields("RocMinus").Value = reader.Item("RocMinus")
                                    rs.Fields("EventOn").Value = reader.Item("EventOn")
                                    rs.Fields("HighHighLimit").Value = reader.Item("HighHighLimit")
                                    rs.Fields("HighLimit").Value = reader.Item("HighLimit")
                                    rs.Fields("LowLimit").Value = reader.Item("LowLimit")
                                    rs.Fields("LowLowLimit").Value = reader.Item("LowLowLimit")
                                    rs.Fields("HighHighAlarm").Value = reader.Item("HighHighAlarm")
                                    rs.Fields("HighAlarm").Value = reader.Item("HighAlarm")
                                    rs.Fields("LowAlarm").Value = reader.Item("LowAlarm")
                                    rs.Fields("LowLowAlarm").Value = reader.Item("LowLowAlarm")
                                    rs.Fields("HighHighBand").Value = reader.Item("HighHighBand")
                                    rs.Fields("HighBand").Value = reader.Item("HighBand")
                                    rs.Fields("LowBand").Value = reader.Item("LowBand")
                                    rs.Fields("LowLowBand").Value = reader.Item("LowLowBand")
                                    rs.Fields("HighHighAlarmOn").Value = reader.Item("HighHighAlarmOn")
                                    rs.Fields("HighAlarmOn").Value = reader.Item("HighAlarmOn")
                                    rs.Fields("LowAlarmOn").Value = reader.Item("LowAlarmOn")
                                    rs.Fields("LowLowAlarmOn").Value = reader.Item("LowLowAlarmOn")
                                    rs.Fields("HighHighAlarmSource").Value = reader.Item("HighHighAlarmSource")
                                    rs.Fields("HighAlarmSource").Value = reader.Item("HighAlarmSource")
                                    rs.Fields("LowAlarmSource").Value = reader.Item("LowAlarmSource")
                                    rs.Fields("LowLowAlarmSource").Value = reader.Item("LowLowAlarmSource")
                                    rs.Fields("TextPvFormat").Value = reader.Item("TextPvFormat")
                                    rs.Fields("HMIAliasName").Value = reader.Item("HMIAliasName")
                                    rs.Fields("FILEAliasName").Value = reader.Item("FILEAliasName")
                                    rs.Fields("EngUnit").Value = reader.Item("EngUnit")
                                    rs.Fields("EngUnitId").Value = reader.Item("EngUnitId")
                                    rs.Fields("MinMaxOn").Value = reader.Item("MinMaxOn")
                                    rs.Fields("DeltaVOn").Value = reader.Item("DeltaVOn")
                                    rs.Fields("DeltaTOn").Value = reader.Item("DeltaTOn")
                                    rs.Fields("RocOn").Value = reader.Item("RocOn")
                                    rs.Fields("SumVOn").Value = reader.Item("SumVOn")
                                    rs.Fields("SumTOn").Value = reader.Item("SumTOn")
                                    rs.Fields("AvgOn").Value = reader.Item("AvgOn")
                                    rs.Fields("AvgTypeId").Value = reader.Item("AvgTypeId")
                                    rs.Fields("AvgPeriod").Value = reader.Item("AvgPeriod")
                                    rs.Fields("AvgNbSamples").Value = reader.Item("AvgNbSamples")
                                    rs.Fields("QueueOptionId").Value = reader.Item("QueueOptionId")
                                    rs.Fields("ArchOn").Value = reader.Item("ArchOn")
                                    rs.Fields("ArchRate").Value = reader.Item("ArchRate")
                                    rs.Fields("TrigCondId").Value = reader.Item("TrigCondId")
                                    rs.Fields("Trig Level").Value = reader.Item("Trig Level")
                                    rs.Fields("Trig DeadBand").Value = reader.Item("Trig DeadBand")
                                    rs.Fields("TimeOut").Value = reader.Item("TimeOut")
                                    rs.Fields("QueueOption").Value = reader.Item("QueueOption")
                                    rs.Fields("QueueSize").Value = reader.Item("QueueSize")
                                    rs.Fields("PreTrigRecord").Value = reader.Item("PreTrigRecord")
                                    rs.Fields("MaxRecord").Value = reader.Item("MaxRecord")
                                    rs.Fields("DacqPreTrig").Value = reader.Item("DacqPreTrig")
                                    rs.Fields("ProcType").Value = reader.Item("ProcType")
                                    rs.Fields("PolyDegree").Value = reader.Item("PolyDegree")
                                    rs.Fields("NumOfArg").Value = reader.Item("NumOfArg")
                                    rs.Fields("NumOfValidArg").Value = reader.Item("NumOfValidArg")
                                    rs.Fields("Coeff1").Value = reader.Item("Coeff1")
                                    rs.Fields("Coeff2").Value = reader.Item("Coeff2")
                                    rs.Fields("Coeff3").Value = reader.Item("Coeff3")
                                    rs.Fields("Coeff4").Value = reader.Item("Coeff4")
                                    rs.Fields("Coeff5").Value = reader.Item("Coeff5")
                                    rs.Fields("Coeff6").Value = reader.Item("Coeff6")
                                    rs.Fields("Arg1").Value = reader.Item("Arg1")
                                    rs.Fields("Arg2").Value = reader.Item("Arg2")
                                    rs.Fields("Arg3").Value = reader.Item("Arg3")
                                    rs.Fields("Arg4").Value = reader.Item("Arg4")
                                    rs.Fields("Arg5").Value = reader.Item("Arg5")
                                    rs.Fields("Arg6").Value = reader.Item("Arg6")
                                    rs.Fields("Arg7").Value = reader.Item("Arg7")
                                    rs.Fields("Arg8").Value = reader.Item("Arg8")
                                    rs.Fields("Arg9").Value = reader.Item("Arg9")
                                    rs.Fields("Arg10").Value = reader.Item("Arg10")
                                    rs.Fields("Arg11").Value = reader.Item("Arg11")
                                    rs.Fields("Arg12").Value = reader.Item("Arg12")
                                    rs.Fields("Arg13").Value = reader.Item("Arg13")
                                    rs.Fields("Arg14").Value = reader.Item("Arg14")
                                    rs.Fields("Arg15").Value = reader.Item("Arg15")
                                    rs.Fields("Arg16").Value = reader.Item("Arg16")
                                    rs.Fields("Arg17").Value = reader.Item("Arg17")
                                    rs.Fields("Arg18").Value = reader.Item("Arg18")
                                    rs.Fields("Arg19").Value = reader.Item("Arg19")
                                    rs.Fields("Arg20").Value = reader.Item("Arg20")
                                    rs.Fields("HighHighAlarmOn").Value = reader.Item("HighHighAlarmOn")
                                    rs.Fields("HighAlarmOn").Value = reader.Item("HighAlarmOn")
                                    rs.Fields("LowAlarmOn").Value = reader.Item("LowAlarmOn")
                                    rs.Fields("LowLowAlarmOn").Value = reader.Item("LowLowAlarmOn")
                                    rs.Fields("HighHighAlarmName").Value = reader.Item("HighHighAlarmName")
                                    rs.Fields("HighAlarmName").Value = reader.Item("HighAlarmName")
                                    rs.Fields("LowAlarmName").Value = reader.Item("LowAlarmName")
                                    rs.Fields("LowLowAlarmName").Value = reader.Item("LowLowAlarmName")

                                    rs.Fields("ArgSrc").Value = reader.Item("ArgSrc")
                                    rs.Fields("ValidArgOn").Value = reader.Item("ValidArgOn")
                                    rs.Fields("read_v1").Value = reader.Item("read_v1")
                                    rs.Fields("read_v2").Value = reader.Item("read_v2")
                                    rs.Fields("read_v3").Value = reader.Item("read_v3")
                                    rs.Fields("read_v4").Value = reader.Item("read_v4")
                                    rs.Fields("read_v5").Value = reader.Item("read_v5")
                                    rs.Fields("read_v6").Value = reader.Item("read_v6")
                                    rs.Fields("real_v1").Value = reader.Item("real_v1")
                                    rs.Fields("real_v2").Value = reader.Item("real_v2")
                                    rs.Fields("real_v3").Value = reader.Item("real_v3")
                                    rs.Fields("real_v4").Value = reader.Item("real_v4")
                                    rs.Fields("real_v5").Value = reader.Item("real_v5")
                                    rs.Fields("real_v6").Value = reader.Item("real_v6")
                                    rs.Fields("Smoothing_Period").Value = reader.Item("Smoothing_Period")
                                    rs.Fields("margin_accuracy").Value = reader.Item("margin_accuracy")
                                    rs.Fields("nb_p_allowed").Value = reader.Item("nb_p_allowed")
                                    rs.Fields("Calibration_measured").Value = reader.Item("Calibration_measured")
                                    rs.Fields("Channel_Type").Value = reader.Item("Channel_Type")
                                    rs.Fields("Channel_Range").Value = reader.Item("Channel_Range")
                                    rs.Fields("Transducer_Range").Value = reader.Item("Transducer_Range")
                                    rs.Fields("Calibration_Range").Value = reader.Item("Calibration_Range")
                                    rs.Fields("Make1").Value = reader.Item("Make1")
                                    rs.Fields("Make2").Value = reader.Item("Make2")
                                    rs.Fields("Make3").Value = reader.Item("Make3")
                                    rs.Fields("Make4").Value = reader.Item("Make4")
                                    rs.Fields("Model1").Value = reader.Item("Model1")
                                    rs.Fields("Model2").Value = reader.Item("Model2")
                                    rs.Fields("Model3").Value = reader.Item("Model3")
                                    rs.Fields("Model4").Value = reader.Item("Model4")
                                    rs.Fields("Serial_Number1").Value = reader.Item("Serial_Number1")
                                    rs.Fields("Serial_Number2").Value = reader.Item("Serial_Number2")
                                    rs.Fields("Serial_Number3").Value = reader.Item("Serial_Number3")
                                    rs.Fields("Serial_Number4").Value = reader.Item("Serial_Number4")
                                    rs.Fields("Calibration_Due_Date1").Value = reader.Item("Calibration_Due_Date1")
                                    rs.Fields("Calibration_Due_Date2").Value = reader.Item("Calibration_Due_Date2")
                                    rs.Fields("Calibration_Due_Date3").Value = reader.Item("Calibration_Due_Date3")
                                    rs.Fields("Calibration_Due_Date4").Value = reader.Item("Calibration_Due_Date4")
                                    rs.Fields("Comments").Value = reader.Item("Comments")
                                    rs.Fields("Calibrated_By").Value = reader.Item("Calibrated_By")
                                    rs.Fields("Assistant").Value = reader.Item("Assistant")
                                    rs.Fields("Approval").Value = reader.Item("Approval")
                                    rs.Fields("LastCalibDate").Value = reader.Item("LastCalibDate")
                                    rs.Fields("OPC_Group").Value = reader.Item("OPC_Group")
                                    rs.Fields("OPC_ItemId").Value = reader.Item("OPC_ItemId")


                                    rs.Update()

                                Catch ex As Exception

                                    Console.WriteLine(ex.ToString())
                                End Try

                            End While
                            reader.Close()
                            DatabaseHelper.CloseRecordset(rs)
                        Else
                            MessageBox.Show("Impossible d'ouvrir DACN1_SERIAL_IOMGR_Real")
                        End If
                    End If

                    '*********************************************************************************************
                    If chkDACN1_POWERDNA_REAL.Checked Then

                        Debug.Assert(txtProjectIdDestination.Text & "" <> "txtProjectIdDestination cannot be null")
                        Debug.Assert(txtProjectIdSource.Text & "" <> "", "txtProjectIdSource cannot be null")

                        If rdoDatabaseV1.Checked Then
                            'strSelectSource = "SELECT * FROM HMI_Numeric_Channels WHERE Engine_Model = '" & cboEngineList.Text & "'"
                            Debug.Assert(False)
                        ElseIf rdoDatabaseV2.Checked Then
                            strSelectSource = "SELECT * FROM DACN1_POWERDNA_IOMGR_Real_1 WHERE ProjectID = " & txtProjectIdSource.Text & ""
                        End If
                        If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM DACN1_POWERDNA_IOMGR_Real_1", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                            Dim cmd As OleDbCommand = New OleDbCommand(strSelectSource, cnSource)
                            Dim reader As OleDbDataReader = cmd.ExecuteReader()

                            While (reader.Read())

                                Try

                                    rs.AddNew()


                                    rs.Fields("ProjectId").Value = txtProjectIdDestination.Text
                                    rs.Fields("RDataId").Value = reader.Item("RDataId")
                                    rs.Fields("Name").Value = reader.Item("Name")
                                    rs.Fields("ExportToCsv").Value = reader.Item("ExportToCsv")
                                    rs.Fields("FieldEquipmentId").Value = reader.Item("FieldEquipmentId")
                                    rs.Fields("DeviceId").Value = reader.Item("DeviceId")
                                    rs.Fields("In_Channel_Type").Value = reader.Item("In_Channel_Type")
                                    rs.Fields("Base_Address").Value = reader.Item("Base_Address")
                                    rs.Fields("Physical_Channel").Value = reader.Item("Physical_Channel")
                                    rs.Fields("MinDCValue").Value = reader.Item("MinDCValue")
                                    rs.Fields("MaxDCValue").Value = reader.Item("MaxDCValue")
                                    rs.Fields("TerminalConfiguration").Value = reader.Item("TerminalConfiguration")
                                    rs.Fields("AlarmOn").Value = reader.Item("AlarmOn")
                                    rs.Fields("StatisticsOn").Value = reader.Item("StatisticsOn")
                                    rs.Fields("TextConversionOn").Value = reader.Item("TextConversionOn")
                                    rs.Fields("DataAcquisitionOn").Value = reader.Item("DataAcquisitionOn")
                                    rs.Fields("DataProcessingOn").Value = reader.Item("DataProcessingOn")
                                    rs.Fields("Period in msec").Value = reader.Item("Period in msec")
                                    rs.Fields("TreatOnOption").Value = reader.Item("TreatOnOption")
                                    rs.Fields("DefaultValue").Value = reader.Item("DefaultValue")
                                    rs.Fields("UpperLimit").Value = reader.Item("UpperLimit")
                                    rs.Fields("LowerLimit").Value = reader.Item("LowerLimit")
                                    rs.Fields("DeltaVPlus").Value = reader.Item("DeltaVPlus")
                                    rs.Fields("DeltaVMinus").Value = reader.Item("DeltaVMinus")
                                    rs.Fields("RocPlus").Value = reader.Item("RocPlus")
                                    rs.Fields("RocMinus").Value = reader.Item("RocMinus")
                                    rs.Fields("EventOn").Value = reader.Item("EventOn")
                                    rs.Fields("HighHighLimit").Value = reader.Item("HighHighLimit")
                                    rs.Fields("HighLimit").Value = reader.Item("HighLimit")
                                    rs.Fields("LowLimit").Value = reader.Item("LowLimit")
                                    rs.Fields("LowLowLimit").Value = reader.Item("LowLowLimit")
                                    rs.Fields("HighHighAlarm").Value = reader.Item("HighHighAlarm")
                                    rs.Fields("HighAlarm").Value = reader.Item("HighAlarm")
                                    rs.Fields("LowAlarm").Value = reader.Item("LowAlarm")
                                    rs.Fields("LowLowAlarm").Value = reader.Item("LowLowAlarm")
                                    rs.Fields("HighHighBand").Value = reader.Item("HighHighBand")
                                    rs.Fields("HighBand").Value = reader.Item("HighBand")
                                    rs.Fields("LowBand").Value = reader.Item("LowBand")
                                    rs.Fields("LowLowBand").Value = reader.Item("LowLowBand")
                                    rs.Fields("HighHighAlarmOn").Value = reader.Item("HighHighAlarmOn")
                                    rs.Fields("HighAlarmOn").Value = reader.Item("HighAlarmOn")
                                    rs.Fields("LowAlarmOn").Value = reader.Item("LowAlarmOn")
                                    rs.Fields("LowLowAlarmOn").Value = reader.Item("LowLowAlarmOn")
                                    rs.Fields("HighHighAlarmSource").Value = reader.Item("HighHighAlarmSource")
                                    rs.Fields("HighAlarmSource").Value = reader.Item("HighAlarmSource")
                                    rs.Fields("LowAlarmSource").Value = reader.Item("LowAlarmSource")
                                    rs.Fields("LowLowAlarmSource").Value = reader.Item("LowLowAlarmSource")
                                    rs.Fields("TextPvFormat").Value = reader.Item("TextPvFormat")
                                    rs.Fields("HMIAliasName").Value = reader.Item("HMIAliasName")
                                    rs.Fields("FILEAliasName").Value = reader.Item("FILEAliasName")
                                    rs.Fields("EngUnit").Value = reader.Item("EngUnit")
                                    rs.Fields("EngUnitId").Value = reader.Item("EngUnitId")
                                    rs.Fields("MinMaxOn").Value = reader.Item("MinMaxOn")
                                    rs.Fields("DeltaVOn").Value = reader.Item("DeltaVOn")
                                    rs.Fields("DeltaTOn").Value = reader.Item("DeltaTOn")
                                    rs.Fields("RocOn").Value = reader.Item("RocOn")
                                    rs.Fields("SumVOn").Value = reader.Item("SumVOn")
                                    rs.Fields("SumTOn").Value = reader.Item("SumTOn")
                                    rs.Fields("AvgOn").Value = reader.Item("AvgOn")
                                    rs.Fields("AvgTypeId").Value = reader.Item("AvgTypeId")
                                    rs.Fields("AvgPeriod").Value = reader.Item("AvgPeriod")
                                    rs.Fields("AvgNbSamples").Value = reader.Item("AvgNbSamples")
                                    rs.Fields("QueueOptionId").Value = reader.Item("QueueOptionId")
                                    rs.Fields("ArchOn").Value = reader.Item("ArchOn")
                                    rs.Fields("ArchRate").Value = reader.Item("ArchRate")
                                    rs.Fields("TrigCondId").Value = reader.Item("TrigCondId")
                                    rs.Fields("Trig Level").Value = reader.Item("Trig Level")
                                    rs.Fields("Trig DeadBand").Value = reader.Item("Trig DeadBand")
                                    rs.Fields("TimeOut").Value = reader.Item("TimeOut")
                                    rs.Fields("QueueOption").Value = reader.Item("QueueOption")
                                    rs.Fields("QueueSize").Value = reader.Item("QueueSize")
                                    rs.Fields("PreTrigRecord").Value = reader.Item("PreTrigRecord")
                                    rs.Fields("MaxRecord").Value = reader.Item("MaxRecord")
                                    rs.Fields("DacqPreTrig").Value = reader.Item("DacqPreTrig")
                                    rs.Fields("ProcType").Value = reader.Item("ProcType")
                                    rs.Fields("PolyDegree").Value = reader.Item("PolyDegree")
                                    rs.Fields("NumOfArg").Value = reader.Item("NumOfArg")
                                    rs.Fields("NumOfValidArg").Value = reader.Item("NumOfValidArg")
                                    rs.Fields("Coeff1").Value = reader.Item("Coeff1")
                                    rs.Fields("Coeff2").Value = reader.Item("Coeff2")
                                    rs.Fields("Coeff3").Value = reader.Item("Coeff3")
                                    rs.Fields("Coeff4").Value = reader.Item("Coeff4")
                                    rs.Fields("Coeff5").Value = reader.Item("Coeff5")
                                    rs.Fields("Coeff6").Value = reader.Item("Coeff6")
                                    rs.Fields("Arg1").Value = reader.Item("Arg1")
                                    rs.Fields("Arg2").Value = reader.Item("Arg2")
                                    rs.Fields("Arg3").Value = reader.Item("Arg3")
                                    rs.Fields("Arg4").Value = reader.Item("Arg4")
                                    rs.Fields("Arg5").Value = reader.Item("Arg5")
                                    rs.Fields("Arg6").Value = reader.Item("Arg6")
                                    rs.Fields("Arg7").Value = reader.Item("Arg7")
                                    rs.Fields("Arg8").Value = reader.Item("Arg8")
                                    rs.Fields("Arg9").Value = reader.Item("Arg9")
                                    rs.Fields("Arg10").Value = reader.Item("Arg10")
                                    rs.Fields("Arg11").Value = reader.Item("Arg11")
                                    rs.Fields("Arg12").Value = reader.Item("Arg12")
                                    rs.Fields("Arg13").Value = reader.Item("Arg13")
                                    rs.Fields("Arg14").Value = reader.Item("Arg14")
                                    rs.Fields("Arg15").Value = reader.Item("Arg15")
                                    rs.Fields("Arg16").Value = reader.Item("Arg16")
                                    rs.Fields("Arg17").Value = reader.Item("Arg17")
                                    rs.Fields("Arg18").Value = reader.Item("Arg18")
                                    rs.Fields("Arg19").Value = reader.Item("Arg19")
                                    rs.Fields("Arg20").Value = reader.Item("Arg20")
                                    rs.Fields("HighHighAlarmOn").Value = reader.Item("HighHighAlarmOn")
                                    rs.Fields("HighAlarmOn").Value = reader.Item("HighAlarmOn")
                                    rs.Fields("LowAlarmOn").Value = reader.Item("LowAlarmOn")
                                    rs.Fields("LowLowAlarmOn").Value = reader.Item("LowLowAlarmOn")
                                    rs.Fields("HighHighAlarmName").Value = reader.Item("HighHighAlarmName")
                                    rs.Fields("HighAlarmName").Value = reader.Item("HighAlarmName")
                                    rs.Fields("LowAlarmName").Value = reader.Item("LowAlarmName")
                                    rs.Fields("LowLowAlarmName").Value = reader.Item("LowLowAlarmName")

                                    rs.Fields("ArgSrc").Value = reader.Item("ArgSrc")
                                    rs.Fields("ValidArgOn").Value = reader.Item("ValidArgOn")
                                    rs.Fields("read_v1").Value = reader.Item("read_v1")
                                    rs.Fields("read_v2").Value = reader.Item("read_v2")
                                    rs.Fields("read_v3").Value = reader.Item("read_v3")
                                    rs.Fields("read_v4").Value = reader.Item("read_v4")
                                    rs.Fields("read_v5").Value = reader.Item("read_v5")
                                    rs.Fields("read_v6").Value = reader.Item("read_v6")
                                    rs.Fields("real_v1").Value = reader.Item("real_v1")
                                    rs.Fields("real_v2").Value = reader.Item("real_v2")
                                    rs.Fields("real_v3").Value = reader.Item("real_v3")
                                    rs.Fields("real_v4").Value = reader.Item("real_v4")
                                    rs.Fields("real_v5").Value = reader.Item("real_v5")
                                    rs.Fields("real_v6").Value = reader.Item("real_v6")
                                    rs.Fields("Smoothing_Period").Value = reader.Item("Smoothing_Period")
                                    rs.Fields("margin_accuracy").Value = reader.Item("margin_accuracy")
                                    rs.Fields("nb_p_allowed").Value = reader.Item("nb_p_allowed")
                                    rs.Fields("Calibration_measured").Value = reader.Item("Calibration_measured")
                                    rs.Fields("Channel_Type").Value = reader.Item("Channel_Type")
                                    rs.Fields("Channel_Range").Value = reader.Item("Channel_Range")
                                    rs.Fields("Transducer_Range").Value = reader.Item("Transducer_Range")
                                    rs.Fields("Calibration_Range").Value = reader.Item("Calibration_Range")
                                    rs.Fields("Make1").Value = reader.Item("Make1")
                                    rs.Fields("Make2").Value = reader.Item("Make2")
                                    rs.Fields("Make3").Value = reader.Item("Make3")
                                    rs.Fields("Make4").Value = reader.Item("Make4")
                                    rs.Fields("Model1").Value = reader.Item("Model1")
                                    rs.Fields("Model2").Value = reader.Item("Model2")
                                    rs.Fields("Model3").Value = reader.Item("Model3")
                                    rs.Fields("Model4").Value = reader.Item("Model4")
                                    rs.Fields("Serial_Number1").Value = reader.Item("Serial_Number1")
                                    rs.Fields("Serial_Number2").Value = reader.Item("Serial_Number2")
                                    rs.Fields("Serial_Number3").Value = reader.Item("Serial_Number3")
                                    rs.Fields("Serial_Number4").Value = reader.Item("Serial_Number4")
                                    rs.Fields("Calibration_Due_Date1").Value = reader.Item("Calibration_Due_Date1")
                                    rs.Fields("Calibration_Due_Date2").Value = reader.Item("Calibration_Due_Date2")
                                    rs.Fields("Calibration_Due_Date3").Value = reader.Item("Calibration_Due_Date3")
                                    rs.Fields("Calibration_Due_Date4").Value = reader.Item("Calibration_Due_Date4")
                                    rs.Fields("Comments").Value = reader.Item("Comments")
                                    rs.Fields("Calibrated_By").Value = reader.Item("Calibrated_By")
                                    rs.Fields("Assistant").Value = reader.Item("Assistant")
                                    rs.Fields("Approval").Value = reader.Item("Approval")
                                    rs.Fields("LastCalibDate").Value = reader.Item("LastCalibDate")
                                    rs.Fields("OPC_Group").Value = reader.Item("OPC_Group")
                                    rs.Fields("OPC_ItemId").Value = reader.Item("OPC_ItemId")


                                    rs.Update()

                                Catch ex As Exception

                                    Console.WriteLine(ex.ToString())
                                End Try

                            End While
                            reader.Close()
                            DatabaseHelper.CloseRecordset(rs)
                        Else
                            MessageBox.Show("Impossible d'ouvrir DACN1_POWERDNA_IOMGR_Real_1")
                        End If
                    End If
                    '*********************************************************************************************

                    If chkHmiPageMapping.Checked Then

                        Debug.Assert(txtProjectIdDestination.Text & "" <> "txtProjectIdDestination cannot be null")
                        Debug.Assert(txtProjectIdSource.Text & "" <> "", "txtProjectIdSource cannot be null")

                        If rdoDatabaseV1.Checked Then

                            Debug.Assert(False, "Not Supported")
                        ElseIf rdoDatabaseV2.Checked Then
                            strSelectSource = "SELECT * FROM HMI_PAGE_MAPPING WHERE ProjectID = " & txtProjectIdSource.Text & " And CollectionName = '" & cboEngineList.Text & "'"
                        End If
                        If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM HMI_PAGE_MAPPING", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                            Dim cmd As OleDbCommand = New OleDbCommand(strSelectSource, cnSource)
                            Dim reader As OleDbDataReader = cmd.ExecuteReader()

                            While (reader.Read())

                                Try

                                    rs.AddNew()

                                    rs.Fields("ProjectId").Value = txtProjectIdDestination.Text
                                    rs.Fields("CollectionName").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, cboEngineList.Text)
                                    rs.Fields("ControlName").Value = reader.Item("ControlName")
                                    rs.Fields("Source").Value = reader.Item("Source")
                                    rs.Fields("TitleText").Value = reader.Item("TitleText")
                                    rs.Fields("FileName").Value = reader.Item("FileName")
                                    rs.Fields("Enable").Value = reader.Item("Enable")
                                    rs.Fields("Visible").Value = reader.Item("Visible")
                                    rs.Fields("LocationX").Value = reader.Item("LocationX")
                                    rs.Fields("LocationY").Value = reader.Item("LocationY")
                                    rs.Fields("SizeX").Value = reader.Item("SizeX")
                                    rs.Fields("SizeY").Value = reader.Item("SizeY")
                                    rs.Fields("Font").Value = reader.Item("Font")
                                    rs.Fields("UnitsPercent").Value = reader.Item("UnitsPercent")
                                    rs.Fields("ValuePercent").Value = reader.Item("ValuePercent")
                                    rs.Fields("Unavailable").Value = reader.Item("Unavailable")
                                    rs.Fields("Note").Value = reader.Item("Note")

                                    rs.Update()

                                Catch ex As Exception

                                    Console.WriteLine(ex.ToString())
                                End Try

                            End While
                            reader.Close()
                            DatabaseHelper.CloseRecordset(rs)
                        Else
                            MessageBox.Show("Impossible d'ouvrir DACN1_NIDAQ_IOMGR_Bool")
                        End If
                    End If
                    '*********************************************************************************************

                    If chkHmiChannelMapping.Checked Then

                        Debug.Assert(txtProjectIdDestination.Text & "" <> "txtProjectIdDestination cannot be null")
                        Debug.Assert(txtProjectIdSource.Text & "" <> "", "txtProjectIdSource cannot be null")

                        If rdoDatabaseV1.Checked Then
                            Debug.Assert(False, "Not Supported")
                        ElseIf rdoDatabaseV2.Checked Then

                            strSelectSource = "SELECT * FROM HMI_CHANNELS_MAPPING WHERE ProjectID = " & txtProjectIdSource.Text & " And CollectionName = '" & cboEngineList.Text & "'"
                        End If
                        If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM HMI_CHANNELS_MAPPING", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                            Dim cmd As OleDbCommand = New OleDbCommand(strSelectSource, cnSource)
                            Dim reader As OleDbDataReader = cmd.ExecuteReader()

                            While (reader.Read())

                                Try

                                    rs.AddNew()

                                    rs.Fields("ProjectId").Value = txtProjectIdDestination.Text
                                    rs.Fields("CollectionName").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, cboEngineList.Text)
                                    rs.Fields("ChannelName").Value = reader.Item("ChannelName")
                                    rs.Fields("FV_Name").Value = reader.Item("FV_Name")
                                    rs.Fields("Unavailable").Value = reader.Item("Unavailable")

                                    rs.Update()

                                Catch ex As Exception

                                    Console.WriteLine(ex.ToString())
                                End Try

                            End While
                            reader.Close()
                            DatabaseHelper.CloseRecordset(rs)
                        Else
                            MessageBox.Show("Impossible d'ouvrir DACN1_NIDAQ_IOMGR_Bool")
                        End If
                    End If
                    '*********************************************************************************************

                    If chkUUT.Checked Then
                        ImportUUT(cnStringSource, cnStringDestination, cnSource, cnDestination)
                    End If
                    DatabaseHelper.CloseConnection(cnDestination)
                End If
                DatabaseHelper.CloseConnection(cnSource)

                '*********************************************************************************************

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        Finally
        End Try
        MessageBox.Show("Completed")
    End Sub

    Private Shared Function ExtractAndSetUnitValue(rs As ADODB.Recordset, units As IList(Of DBUnit), reader As OleDbDataReader) As ADODB.Recordset

        Dim unit = reader.Item("Units")

        If IsDBNull(unit) Then
            rs.Fields("EngUnitID").Value = unit
        Else
            If units.Any(Function(u) u.DisplayName = unit) Then
                rs.Fields("EngUnitID").Value = units.Single(Function(u) u.DisplayName = unit).ID
            Else
                'Special case for 18942  projet, to modify depending on your project needs
                If unit.Trim() = "º" Then unit = "°"
                If unit.Trim() = "lbf" Then unit = "lbs-ft"
                If unit.Trim() = "liters" Then unit = "Liters"
                If unit.Trim().ToLower().StartsWith("rpm") Then unit = "RPM"

                rs.Fields("EngUnitID").Value = units.Single(Function(u) u.DisplayName = unit.Trim()).ID
            End If
        End If

        Return rs
    End Function

    Private Sub AddLogReportDetail(projectId As String, rs As ADODB.Recordset, reader As OleDbDataReader)
        If rs Is Nothing Then Return
        If reader Is Nothing Then Return

        Try

            If Not reader.Item("Logger_Enabled") Then Return

            rs.AddNew()

            rs.Fields("ProjectID").Value = projectId
            rs.Fields("EngineModel").Value = reader.Item("Engine_Model")
            rs.Fields("PWC_PN").Value = "Default"
            rs.Fields("ChannelName").Value = reader.Item("Name")
            rs.Fields("Category").Value = reader.Item("Log_Report_Categorie")
            rs.Fields("OrderInCategory").Value = reader.Item("Log_Report_Order_In_Categorie")
            rs.Fields("Description").Value = reader.Item("Description")
            rs.Fields("DisplayUnit").Value = reader.Item("Units")
            rs.Fields("Format").Value = reader.Item("Format")

            rs.Update()

        Catch ex As Exception

            Console.WriteLine(ex.ToString())

        End Try

    End Sub

    Private Sub cboEngineList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEngineList.SelectedIndexChanged
        btnImport.Enabled = True
    End Sub

    Private Sub btnReadXmlFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReadXmlFile.Click
        Try
            ' Open an XML file
            Dim reader As New XmlTextReader(txtXmlSource.Text)
            While reader.Read()
                Console.WriteLine(reader.Name)
            End While

            'Dim xmlFile As XmlReader
            'xmlFile = XmlReader.Create(txtXmlSource.Text, New XmlReaderSettings())
            'Dim ds As New DataSet
            'ds.ReadXml(xmlFile)
            'Dim i As Integer
            'For i = 0 To ds.Tables(0).Rows.Count - 1
            '    MsgBox(ds.Tables(0).Rows(i).Item(1))
            'Next

        Catch ex As Exception
            Console.WriteLine("Exception: {0}", e.ToString())
        End Try

    End Sub

    Private Sub chkSubstitute_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSubstitute.CheckedChanged
        txtSubstituteEngine.Enabled = chkSubstitute.Checked
    End Sub


    Private Function Load_Static_Collections(ByRef HMIProject As KineticaRT.Studio.Application.Project) As Boolean
        Dim ChannelCollection As KineticaRT.ChannelCollection
        Dim Reader As OleDbDataReader
        Dim SelectString As String
        Dim TestSession_Connection As OleDbConnection = Nothing
        Dim Cmd As OleDbCommand
        Dim Return_Value As New ArrayList()

        Load_Static_Collections = False

        Try

            'Open the connection.
            If (DatabaseHelper.OpenConnection(CELProjectDestination.Database.OleDbConnectionString, TestSession_Connection)) Then

                SelectString = "SELECT * FROM UUT_Static_Channel_Collection WHERE Model = '" + cboEngineList.Text + "'"
                Cmd = New OleDbCommand(SelectString, TestSession_Connection)
                Reader = Cmd.ExecuteReader()

                While (Reader.Read())

                    'HMIProject.AddChannelCollection(EngineModel + "/" + EngineModel + "_Channels.ksc", EngineModel + "_Channels", True) 'TEST
                    HMIProject.AddChannelCollection(Reader.Item("FileName"), Reader.Item("Description"), True) 'TEST
                    ChannelCollection = GetObject(Reader.Item("Description"))

                    Return_Value.Add(Not ChannelCollection Is Nothing)

                End While

                Load_Static_Collections = Not Return_Value.Contains(False)

            End If
        Catch ex As Exception

        End Try

    End Function


    Private Sub btnReadChannelCollection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReadChannelCollection.Click

        Dim ChannelsInfo As KineticaRT.Studio.Application.Project.ChannelsInfo
        Dim ChannelCollection As KineticaRT.ChannelCollection


        ChannelsInfo = HMIProject.GetChannelsInfo("TEST")
        If Not ChannelsInfo Is Nothing Then
            HMIProject.RemoveChannels(ChannelsInfo)
        End If
        HMIProject.AddChannelCollection(txtXmlSource.Text, "TEST", False)

        ChannelsInfo = HMIProject.GetChannelsInfo("TEST")

        ChannelCollection = ChannelsInfo.Collection

        lstSource.Items.Clear()
        lstDestination.Items.Clear()

        If Not ChannelCollection Is Nothing Then
            For Each Channel As Object In ChannelCollection
                lstSource.Items.Add(Channel.Key.ToString())
            Next
        End If

    End Sub

    Private Sub btnAddRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRight.Click
        lstDestination.Items.Add(lstSource.SelectedItem)
        btnImportChannels.Enabled = True
    End Sub

    Private Sub btnAddLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddLeft.Click
        lstDestination.Items.Remove(lstDestination.SelectedItem)
    End Sub
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        lstDestination.Items.Clear()
    End Sub
    Private Sub btnAddAllToRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddAllToRight.Click

        For Each Item As Object In lstSource.Items
            lstDestination.Items.Add(Item)
        Next
        btnImportChannels.Enabled = True
    End Sub

    Private Sub btnImportChannels_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportChannels.Click
        Dim cnDestination As ADODB.Connection = Nothing
        Dim cnStringDestination As String = Me.CELProjectDestination.Database.OleDbConnectionString

        Dim ChannelsInfo As KineticaRT.Studio.Application.Project.ChannelsInfo
        Dim ChannelCollection As KineticaRT.ChannelCollection

        ChannelsInfo = HMIProject.GetChannelsInfo("TEST")

        ChannelCollection = ChannelsInfo.Collection
        If Not ChannelCollection Is Nothing Then
            If DatabaseHelper.OpenConnection(cnStringDestination, cnDestination) Then

                For Each Item As Object In lstDestination.Items

                    For Each Channel As Object In ChannelCollection
                        If Channel.Key = Item.ToString() Then 'On a trouvé le bon Channel dans la collection

                            Dim rs As ADODB.Recordset = Nothing

                            Select Case Channel.Value.GetType().ToString()
                                Case "KineticaRT.Channels.TextChannel"
                                Case "KineticaRT.Logic.Channels.BooleanExpression"

                                    If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM HMI_Boolean_Channels", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then

                                        Try

                                            rs.AddNew()
                                            rs.Fields("CollectionName").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, cboEngineList.Text)
                                            rs.Fields("ChannelName").Value = Channel.Value.Name
                                            rs.Fields("Description").Value = Channel.Value.Description
                                            'rs.Fields("Source").Value = Channel.Value.Source
                                            'rs.Fields("Destination").Value = Channel.Value.Destination
                                            rs.Fields("Expression").Value = Channel.Value.Text
                                            rs.Fields("ToolTip").Value = Channel.Value.ToolTip

                                            rs.Fields("FalseText").Value = Channel.Value.FalseText
                                            rs.Fields("TrueText").Value = Channel.Value.TrueText
                                            'rs.Fields("Logger_Enabled").Value = reader.Item("Logger_Enabled")
                                            'rs.Fields("Logger_Index").Value = reader.Item("Logger_Index")
                                            'rs.Fields("Log_Report_Categorie").Value = reader.Item("Log_Report_Categorie")
                                            'rs.Fields("Log_Report_Order_In_Categorie").Value = reader.Item("Log_Report_Order_In_Categorie")
                                            'rs.Fields("FalseMessageText").Value = reader.Item("FalseMessageText")
                                            'rs.Fields("TrueMessageText").Value = reader.Item("TrueMessageText")
                                            'rs.Fields("SoundMessage").Value = reader.Item("SoundMessage")


                                            rs.Update()

                                        Catch ex As Exception
                                            Console.WriteLine(ex.ToString())
                                        End Try

                                        DatabaseHelper.CloseRecordset(rs)
                                    Else
                                        MessageBox.Show("Impossible d'ouvrir HMI_Boolean_Channels")
                                    End If
                                Case "KineticaRT.Channels.BooleanChannel"

                                    If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM HMI_Boolean_Channels", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then

                                        Try

                                            rs.AddNew()
                                            rs.Fields("CollectionName").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, cboEngineList.Text)
                                            rs.Fields("ChannelName").Value = Channel.Value.Name
                                            rs.Fields("Description").Value = Channel.Value.Description
                                            'rs.Fields("Source").Value = Channel.Value.Source
                                            'rs.Fields("Destination").Value = Channel.Value.Destination
                                            rs.Fields("ToolTip").Value = Channel.Value.ToolTip

                                            rs.Fields("FalseText").Value = Channel.Value.FalseText
                                            rs.Fields("TrueText").Value = Channel.Value.TrueText
                                            'rs.Fields("Logger_Enabled").Value = reader.Item("Logger_Enabled")
                                            'rs.Fields("Logger_Index").Value = reader.Item("Logger_Index")
                                            'rs.Fields("Log_Report_Categorie").Value = reader.Item("Log_Report_Categorie")
                                            'rs.Fields("Log_Report_Order_In_Categorie").Value = reader.Item("Log_Report_Order_In_Categorie")
                                            'rs.Fields("FalseMessageText").Value = reader.Item("FalseMessageText")
                                            'rs.Fields("TrueMessageText").Value = reader.Item("TrueMessageText")
                                            'rs.Fields("SoundMessage").Value = reader.Item("SoundMessage")


                                            rs.Update()

                                        Catch ex As Exception
                                            Console.WriteLine(ex.ToString())
                                        End Try

                                        DatabaseHelper.CloseRecordset(rs)
                                    Else
                                        MessageBox.Show("Impossible d'ouvrir HMI_Boolean_Channels")
                                    End If
                                Case "KineticaRT.Logic.Channels.BooleanLink"

                                    If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM HMI_Boolean_Channels", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then

                                        Try

                                            rs.AddNew()
                                            rs.Fields("CollectionName").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, cboEngineList.Text)
                                            rs.Fields("ChannelName").Value = Channel.Value.Name
                                            rs.Fields("Description").Value = Channel.Value.Description
                                            rs.Fields("Source").Value = Channel.Value.Source
                                            rs.Fields("Destination").Value = Channel.Value.Destination
                                            rs.Fields("ToolTip").Value = Channel.Value.ToolTip

                                            rs.Fields("FalseText").Value = Channel.Value.FalseText
                                            rs.Fields("TrueText").Value = Channel.Value.TrueText
                                            'rs.Fields("Logger_Enabled").Value = reader.Item("Logger_Enabled")
                                            'rs.Fields("Logger_Index").Value = reader.Item("Logger_Index")
                                            'rs.Fields("Log_Report_Categorie").Value = reader.Item("Log_Report_Categorie")
                                            'rs.Fields("Log_Report_Order_In_Categorie").Value = reader.Item("Log_Report_Order_In_Categorie")
                                            'rs.Fields("FalseMessageText").Value = reader.Item("FalseMessageText")
                                            'rs.Fields("TrueMessageText").Value = reader.Item("TrueMessageText")
                                            'rs.Fields("SoundMessage").Value = reader.Item("SoundMessage")


                                            rs.Update()

                                        Catch ex As Exception
                                            Console.WriteLine(ex.ToString())
                                        End Try

                                        DatabaseHelper.CloseRecordset(rs)
                                    Else
                                        MessageBox.Show("Impossible d'ouvrir HMI_Boolean_Channels")
                                    End If

                                Case "KineticaRT.Channels.VariableChannel"
                                    Console.WriteLine("KineticaRT.Channels.VariableChannel")
                                    MessageBox.Show("Type pas pris en charge " + Channel.Value.GetType().ToString())
                                Case "KineticaRT.Channels.NumericChannel"
                                    Console.WriteLine("KineticaRT.Channels.NumericChannel")
                                    If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM HMI_Numeric_Channels", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then

                                        Try

                                            rs.AddNew()
                                            rs.Fields("CollectionName").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, cboEngineList.Text)
                                            rs.Fields("ChannelName").Value = Channel.Value.Name
                                            rs.Fields("Description").Value = Channel.Value.Description
                                            'rs.Fields("Source").Value = reader.Item("Source")
                                            'rs.Fields("Destination").Value = reader.Item("Destination")
                                            'rs.Fields("Expression").Value = Channel.Value.Text
                                            'rs.Fields("OPC_Group").Value = Channel.Value.Group
                                            'rs.Fields("OPC_ItemID").Value = Channel.Value.ItemID
                                            rs.Fields("ToolTip").Value = Channel.Value.ToolTip

                                            rs.Fields("DecimalDigits").Value = Channel.Value.DecimalDigits
                                            rs.Fields("Format").Value = Channel.Value.Format
                                            rs.Fields("Minimum").Value = Channel.Value.Minimum
                                            rs.Fields("Maximum").Value = Channel.Value.Maximum
                                            rs.Fields("EngUnitID").Value = Channel.Value.Units
                                            'rs.Fields("Logger_Enabled").Value = reader.Item("Logger_Enabled")
                                            'rs.Fields("Logger_Index").Value = reader.Item("Logger_Index")
                                            'rs.Fields("Log_Report_Categorie").Value = reader.Item("Log_Report_Categorie")
                                            'rs.Fields("Log_Report_Order_In_Categorie").Value = reader.Item("Log_Report_Order_In_Categorie")
                                            'rs.Fields("Mean_Enabled").Value = reader.Item("Mean_Enabled")
                                            'rs.Fields("Mean_Period").Value = reader.Item("Mean_Period")

                                            'rs.Fields("Threshold_HighHigh").Value = reader.Item("Threshold_HighHigh")
                                            'rs.Fields("Threshold_HighHighSource").Value = reader.Item("Threshold_HighHighSource")
                                            'rs.Fields("Threshold_HighHighColor").Value = reader.Item("Threshold_HighHighColor")
                                            'rs.Fields("Threshold_HighHighMessageText").Value = reader.Item("Threshold_HighHighMessageText")

                                            'rs.Fields("Threshold_High").Value = reader.Item("Threshold_High")
                                            'rs.Fields("Threshold_HighSource").Value = reader.Item("Threshold_HighSource")
                                            'rs.Fields("Threshold_HighColor").Value = reader.Item("Threshold_HighColor")
                                            'rs.Fields("Threshold_HighMessageText").Value = reader.Item("Threshold_HighMessageText")

                                            'rs.Fields("Threshold_Normal").Value = reader.Item("Threshold_Normal")
                                            'rs.Fields("Threshold_NormalColor").Value = reader.Item("Threshold_NormalColor")


                                            'rs.Fields("Threshold_Low").Value = reader.Item("Threshold_Low")
                                            'rs.Fields("Threshold_LowSource").Value = reader.Item("Threshold_LowSource")
                                            'rs.Fields("Threshold_LowColor").Value = reader.Item("Threshold_LowColor")
                                            'rs.Fields("Threshold_LowMessageText").Value = reader.Item("Threshold_LowMessageText")

                                            'rs.Fields("Threshold_LowLow").Value = reader.Item("Threshold_LowLow")
                                            'rs.Fields("Threshold_LowLowSource").Value = reader.Item("Threshold_LowLowSource")
                                            'rs.Fields("Threshold_LowLowColor").Value = reader.Item("Threshold_LowLowColor")
                                            'rs.Fields("Threshold_LowLowMessageText").Value = reader.Item("Threshold_LowLowMessageText")

                                            'rs.Fields("High_Trigger").Value = reader.Item("High_Trigger")
                                            'rs.Fields("High_Trigger_Enabled").Value = reader.Item("High_Trigger_Enabled")

                                            'rs.Fields("Low_Trigger").Value = reader.Item("Low_Trigger")
                                            'rs.Fields("Low_Trigger_Enabled").Value = reader.Item("Low_Trigger_Enabled")

                                            'rs.Fields("SpeechMessageText").Value = reader.Item("SpeechMessageText")


                                            rs.Update()



                                        Catch ex As Exception

                                            Console.WriteLine(ex.ToString())

                                        End Try

                                        DatabaseHelper.CloseRecordset(rs)
                                    Else
                                        MessageBox.Show("Impossible d'ouvrir HMI_Numeric_Channels")
                                    End If

                                Case "KineticaRT.Logic.Expressions.NumericExpression"
                                    Console.WriteLine("KineticaRT.Logic.Expressions.NumericExpression")
                                    If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM HMI_Numeric_Channels", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then

                                        Try

                                            rs.AddNew()
                                            rs.Fields("CollectionName").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, cboEngineList.Text)
                                            rs.Fields("ChannelName").Value = Channel.Value.Name
                                            rs.Fields("Description").Value = Channel.Value.Description
                                            'rs.Fields("Source").Value = reader.Item("Source")
                                            'rs.Fields("Destination").Value = reader.Item("Destination")
                                            rs.Fields("Expression").Value = Channel.Value.Text
                                            'rs.Fields("OPC_Group").Value = Channel.Value.Group
                                            'rs.Fields("OPC_ItemID").Value = Channel.Value.ItemID
                                            rs.Fields("ToolTip").Value = Channel.Value.ToolTip

                                            rs.Fields("DecimalDigits").Value = Channel.Value.DecimalDigits
                                            rs.Fields("Format").Value = Channel.Value.Format
                                            rs.Fields("Minimum").Value = Channel.Value.Minimum
                                            rs.Fields("Maximum").Value = Channel.Value.Maximum
                                            rs.Fields("EngUnitID").Value = Channel.Value.Units
                                            'rs.Fields("Logger_Enabled").Value = reader.Item("Logger_Enabled")
                                            'rs.Fields("Logger_Index").Value = reader.Item("Logger_Index")
                                            'rs.Fields("Log_Report_Categorie").Value = reader.Item("Log_Report_Categorie")
                                            'rs.Fields("Log_Report_Order_In_Categorie").Value = reader.Item("Log_Report_Order_In_Categorie")
                                            'rs.Fields("Mean_Enabled").Value = reader.Item("Mean_Enabled")
                                            'rs.Fields("Mean_Period").Value = reader.Item("Mean_Period")

                                            'rs.Fields("Threshold_HighHigh").Value = reader.Item("Threshold_HighHigh")
                                            'rs.Fields("Threshold_HighHighSource").Value = reader.Item("Threshold_HighHighSource")
                                            'rs.Fields("Threshold_HighHighColor").Value = reader.Item("Threshold_HighHighColor")
                                            'rs.Fields("Threshold_HighHighMessageText").Value = reader.Item("Threshold_HighHighMessageText")

                                            'rs.Fields("Threshold_High").Value = reader.Item("Threshold_High")
                                            'rs.Fields("Threshold_HighSource").Value = reader.Item("Threshold_HighSource")
                                            'rs.Fields("Threshold_HighColor").Value = reader.Item("Threshold_HighColor")
                                            'rs.Fields("Threshold_HighMessageText").Value = reader.Item("Threshold_HighMessageText")

                                            'rs.Fields("Threshold_Normal").Value = reader.Item("Threshold_Normal")
                                            'rs.Fields("Threshold_NormalColor").Value = reader.Item("Threshold_NormalColor")


                                            'rs.Fields("Threshold_Low").Value = reader.Item("Threshold_Low")
                                            'rs.Fields("Threshold_LowSource").Value = reader.Item("Threshold_LowSource")
                                            'rs.Fields("Threshold_LowColor").Value = reader.Item("Threshold_LowColor")
                                            'rs.Fields("Threshold_LowMessageText").Value = reader.Item("Threshold_LowMessageText")

                                            'rs.Fields("Threshold_LowLow").Value = reader.Item("Threshold_LowLow")
                                            'rs.Fields("Threshold_LowLowSource").Value = reader.Item("Threshold_LowLowSource")
                                            'rs.Fields("Threshold_LowLowColor").Value = reader.Item("Threshold_LowLowColor")
                                            'rs.Fields("Threshold_LowLowMessageText").Value = reader.Item("Threshold_LowLowMessageText")

                                            'rs.Fields("High_Trigger").Value = reader.Item("High_Trigger")
                                            'rs.Fields("High_Trigger_Enabled").Value = reader.Item("High_Trigger_Enabled")

                                            'rs.Fields("Low_Trigger").Value = reader.Item("Low_Trigger")
                                            'rs.Fields("Low_Trigger_Enabled").Value = reader.Item("Low_Trigger_Enabled")

                                            'rs.Fields("SpeechMessageText").Value = reader.Item("SpeechMessageText")


                                            rs.Update()



                                        Catch ex As Exception

                                            Console.WriteLine(ex.ToString())

                                        End Try

                                        DatabaseHelper.CloseRecordset(rs)
                                    Else
                                        MessageBox.Show("Impossible d'ouvrir HMI_Numeric_Channels")
                                    End If

                                Case "KineticaRT.Servers.OpcClient.Item"
                                    If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM HMI_Numeric_Channels", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then

                                        Try

                                            rs.AddNew()
                                            rs.Fields("CollectionName").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, cboEngineList.Text)
                                            rs.Fields("ChannelName").Value = Channel.Value.Name
                                            rs.Fields("Description").Value = Channel.Value.Description
                                            'rs.Fields("Source").Value = reader.Item("Source")
                                            'rs.Fields("Destination").Value = reader.Item("Destination")
                                            'rs.Fields("Expression").Value = Channel.Value.Text
                                            rs.Fields("OPC_Group").Value = Channel.Value.Group
                                            rs.Fields("OPC_ItemID").Value = Channel.Value.ItemID
                                            rs.Fields("ToolTip").Value = Channel.Value.ToolTip

                                            rs.Fields("DecimalDigits").Value = Channel.Value.DecimalDigits
                                            rs.Fields("Format").Value = Channel.Value.Format
                                            rs.Fields("Minimum").Value = Channel.Value.Minimum
                                            rs.Fields("Maximum").Value = Channel.Value.Maximum
                                            rs.Fields("EngUnitID").Value = Channel.Value.Units
                                            'rs.Fields("Logger_Enabled").Value = reader.Item("Logger_Enabled")
                                            'rs.Fields("Logger_Index").Value = reader.Item("Logger_Index")
                                            'rs.Fields("Log_Report_Categorie").Value = reader.Item("Log_Report_Categorie")
                                            'rs.Fields("Log_Report_Order_In_Categorie").Value = reader.Item("Log_Report_Order_In_Categorie")
                                            'rs.Fields("Mean_Enabled").Value = reader.Item("Mean_Enabled")
                                            'rs.Fields("Mean_Period").Value = reader.Item("Mean_Period")

                                            'rs.Fields("Threshold_HighHigh").Value = reader.Item("Threshold_HighHigh")
                                            'rs.Fields("Threshold_HighHighSource").Value = reader.Item("Threshold_HighHighSource")
                                            'rs.Fields("Threshold_HighHighColor").Value = reader.Item("Threshold_HighHighColor")
                                            'rs.Fields("Threshold_HighHighMessageText").Value = reader.Item("Threshold_HighHighMessageText")

                                            'rs.Fields("Threshold_High").Value = reader.Item("Threshold_High")
                                            'rs.Fields("Threshold_HighSource").Value = reader.Item("Threshold_HighSource")
                                            'rs.Fields("Threshold_HighColor").Value = reader.Item("Threshold_HighColor")
                                            'rs.Fields("Threshold_HighMessageText").Value = reader.Item("Threshold_HighMessageText")

                                            'rs.Fields("Threshold_Normal").Value = reader.Item("Threshold_Normal")
                                            'rs.Fields("Threshold_NormalColor").Value = reader.Item("Threshold_NormalColor")


                                            'rs.Fields("Threshold_Low").Value = reader.Item("Threshold_Low")
                                            'rs.Fields("Threshold_LowSource").Value = reader.Item("Threshold_LowSource")
                                            'rs.Fields("Threshold_LowColor").Value = reader.Item("Threshold_LowColor")
                                            'rs.Fields("Threshold_LowMessageText").Value = reader.Item("Threshold_LowMessageText")

                                            'rs.Fields("Threshold_LowLow").Value = reader.Item("Threshold_LowLow")
                                            'rs.Fields("Threshold_LowLowSource").Value = reader.Item("Threshold_LowLowSource")
                                            'rs.Fields("Threshold_LowLowColor").Value = reader.Item("Threshold_LowLowColor")
                                            'rs.Fields("Threshold_LowLowMessageText").Value = reader.Item("Threshold_LowLowMessageText")

                                            'rs.Fields("High_Trigger").Value = reader.Item("High_Trigger")
                                            'rs.Fields("High_Trigger_Enabled").Value = reader.Item("High_Trigger_Enabled")

                                            'rs.Fields("Low_Trigger").Value = reader.Item("Low_Trigger")
                                            'rs.Fields("Low_Trigger_Enabled").Value = reader.Item("Low_Trigger_Enabled")

                                            'rs.Fields("SpeechMessageText").Value = reader.Item("SpeechMessageText")


                                            rs.Update()

                                        Catch ex As Exception

                                            Console.WriteLine(ex.ToString())

                                        End Try

                                        DatabaseHelper.CloseRecordset(rs)
                                    Else
                                        MessageBox.Show("Impossible d'ouvrir HMI_Numeric_Channels")
                                    End If
                                Case "KineticaRT.Servers.OpcClient.BooleanItem"
                                    If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM HMI_Boolean_Channels", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then

                                        Try

                                            rs.AddNew()
                                            rs.Fields("CollectionName").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, cboEngineList.Text)
                                            rs.Fields("ChannelName").Value = Channel.Value.Name
                                            rs.Fields("Description").Value = Channel.Value.Description
                                            'rs.Fields("Source").Value = Channel.Value.Source
                                            'rs.Fields("Destination").Value = Channel.Value.Destination
                                            'rs.Fields("Expression").Value = Channel.Value.Text
                                            rs.Fields("OPC_Group").Value = Channel.Value.Group
                                            rs.Fields("OPC_ItemID").Value = Channel.Value.ItemID
                                            rs.Fields("ToolTip").Value = Channel.Value.ToolTip

                                            rs.Fields("FalseText").Value = Channel.Value.FalseText
                                            rs.Fields("TrueText").Value = Channel.Value.TrueText
                                            'rs.Fields("Logger_Enabled").Value = reader.Item("Logger_Enabled")
                                            'rs.Fields("Logger_Index").Value = reader.Item("Logger_Index")
                                            'rs.Fields("Log_Report_Categorie").Value = reader.Item("Log_Report_Categorie")
                                            'rs.Fields("Log_Report_Order_In_Categorie").Value = reader.Item("Log_Report_Order_In_Categorie")
                                            'rs.Fields("FalseMessageText").Value = reader.Item("FalseMessageText")
                                            'rs.Fields("TrueMessageText").Value = reader.Item("TrueMessageText")
                                            'rs.Fields("SoundMessage").Value = reader.Item("SoundMessage")


                                            rs.Update()

                                        Catch ex As Exception

                                            Console.WriteLine(ex.ToString())

                                        End Try

                                        DatabaseHelper.CloseRecordset(rs)
                                    Else
                                        MessageBox.Show("Impossible d'ouvrir HMI_Boolean_Channels")
                                    End If
                                Case "KineticaRT.Logic.Channels.TextLink"
                                    Console.WriteLine("KineticaRT.Logic.Channels.TextLink")
                                    MessageBox.Show("Type pas pris en charge " + Channel.Value.GetType().ToString())
                                Case "KineticaRT.Servers.OpcClient.TextItem"
                                    Console.WriteLine("KineticaRT.Servers.OpcClient.TextItem")
                                    MessageBox.Show("Type pas pris en charge " + Channel.Value.GetType().ToString())
                                Case "KineticaRT.Logic.Channels.BooleanVariable"
                                    Console.WriteLine("KineticaRT.Logic.Channels.BooleanVariable")
                                    If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM HMI_Boolean_Channels", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then

                                        Try

                                            rs.AddNew()
                                            rs.Fields("CollectionName").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, cboEngineList.Text)
                                            rs.Fields("ChannelName").Value = Channel.Value.Name
                                            rs.Fields("Description").Value = Channel.Value.Description
                                            'rs.Fields("Source").Value = Channel.Value.Source
                                            'rs.Fields("Destination").Value = Channel.Value.Destination
                                            rs.Fields("ToolTip").Value = Channel.Value.ToolTip

                                            rs.Fields("FalseText").Value = Channel.Value.FalseText
                                            rs.Fields("TrueText").Value = Channel.Value.TrueText
                                            'rs.Fields("Logger_Enabled").Value = reader.Item("Logger_Enabled")
                                            'rs.Fields("Logger_Index").Value = reader.Item("Logger_Index")
                                            'rs.Fields("Log_Report_Categorie").Value = reader.Item("Log_Report_Categorie")
                                            'rs.Fields("Log_Report_Order_In_Categorie").Value = reader.Item("Log_Report_Order_In_Categorie")
                                            'rs.Fields("FalseMessageText").Value = reader.Item("FalseMessageText")
                                            'rs.Fields("TrueMessageText").Value = reader.Item("TrueMessageText")
                                            'rs.Fields("SoundMessage").Value = reader.Item("SoundMessage")


                                            rs.Update()

                                        Catch ex As Exception
                                            Console.WriteLine(ex.ToString())
                                        End Try

                                        DatabaseHelper.CloseRecordset(rs)
                                    Else
                                        MessageBox.Show("Impossible d'ouvrir HMI_Boolean_Channels")
                                    End If
                                Case "CEL.KineticaRT.Servers.Logger.NumericThresholdLink"
                                    Console.WriteLine("CEL.KineticaRT.Servers.Logger.NumericThresholdLink")
                                    If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM HMI_Numeric_Channels", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then

                                        Try

                                            rs.AddNew()
                                            rs.Fields("CollectionName").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, cboEngineList.Text)
                                            rs.Fields("ChannelName").Value = Channel.Value.Name
                                            rs.Fields("Description").Value = Channel.Value.Description
                                            rs.Fields("Source").Value = Channel.Value.Source
                                            rs.Fields("Destination").Value = Channel.Value.Destination
                                            'rs.Fields("Expression").Value = Channel.Value.Text
                                            'rs.Fields("OPC_Group").Value = Channel.Value.Group
                                            'rs.Fields("OPC_ItemID").Value = Channel.Value.ItemID
                                            rs.Fields("ToolTip").Value = Channel.Value.ToolTip

                                            rs.Fields("DecimalDigits").Value = Channel.Value.DecimalDigits
                                            rs.Fields("Format").Value = Channel.Value.Format
                                            rs.Fields("Minimum").Value = Channel.Value.Minimum
                                            rs.Fields("Maximum").Value = Channel.Value.Maximum
                                            rs.Fields("EngUnitID").Value = Channel.Value.Units
                                            'rs.Fields("Logger_Enabled").Value = reader.Item("Logger_Enabled")
                                            'rs.Fields("Logger_Index").Value = reader.Item("Logger_Index")
                                            'rs.Fields("Log_Report_Categorie").Value = reader.Item("Log_Report_Categorie")
                                            'rs.Fields("Log_Report_Order_In_Categorie").Value = reader.Item("Log_Report_Order_In_Categorie")
                                            'rs.Fields("Mean_Enabled").Value = reader.Item("Mean_Enabled")
                                            'rs.Fields("Mean_Period").Value = reader.Item("Mean_Period")

                                            rs.Fields("Threshold_HighHigh").Value = Channel.Value.HighHigh
                                            rs.Fields("Threshold_HighHighSource").Value = Channel.Value.HighHighSource
                                            'rs.Fields("Threshold_HighHighColor").Value = Channel.Value.HighHighColor
                                            'rs.Fields("Threshold_HighHighMessageText").Value = reader.Item("Threshold_HighHighMessageText")

                                            rs.Fields("Threshold_High").Value = Channel.Value.High
                                            rs.Fields("Threshold_HighSource").Value = Channel.Value.HighSource
                                            'rs.Fields("Threshold_HighColor").Value = Channel.Value.HighColor
                                            'rs.Fields("Threshold_HighMessageText").Value = reader.Item("Threshold_HighMessageText")

                                            rs.Fields("Threshold_Normal").Value = Channel.Value.Normal
                                            'rs.Fields("Threshold_NormalColor").Value = Channel.Value.NormalColor


                                            rs.Fields("Threshold_Low").Value = Channel.Value.Low
                                            rs.Fields("Threshold_LowSource").Value = Channel.Value.LowSource
                                            'rs.Fields("Threshold_LowColor").Value = reader.Item("Threshold_LowColor")
                                            'rs.Fields("Threshold_LowMessageText").Value = reader.Item("Threshold_LowMessageText")

                                            rs.Fields("Threshold_LowLow").Value = Channel.Value.LowLow
                                            rs.Fields("Threshold_LowLowSource").Value = Channel.Value.LowLowSource
                                            'rs.Fields("Threshold_LowLowColor").Value = reader.Item("Threshold_LowLowColor")
                                            'rs.Fields("Threshold_LowLowMessageText").Value = reader.Item("Threshold_LowLowMessageText")

                                            'plante rs.Fields("High_Trigger").Value = Channel.Value.HighTrigger
                                            'rs.Fields("High_Trigger_Enabled").Value = Channel.Value.HighTriggered

                                            'plante rs.Fields("Low_Trigger").Value = Channel.Value.LowTrigger
                                            'rs.Fields("Low_Trigger_Enabled").Value = Channel.Value.LowTriggered

                                            'rs.Fields("SpeechMessageText").Value = reader.Item("SpeechMessageText")


                                            rs.Update()

                                        Catch ex As Exception

                                            Console.WriteLine(ex.ToString())

                                        End Try

                                        DatabaseHelper.CloseRecordset(rs)
                                    Else
                                        MessageBox.Show("Impossible d'ouvrir HMI_Numeric_Channels")
                                    End If

                                Case Else
                                    MessageBox.Show("Type pas pris en charge " + Channel.Value.GetType().ToString())


                            End Select

                        End If
                    Next


                Next
            End If
        End If

    End Sub

    Private Sub chkAliasBoolean_CheckedChanged(sender As Object, e As EventArgs) Handles chkAliasBoolean.CheckedChanged
        If ChkHMIBoolean.Checked = True Then
            ChkHMIBoolean.Checked = False
        End If
    End Sub

    Private Sub chkCalculatedBoolean_CheckedChanged(sender As Object, e As EventArgs) Handles chkCalculatedBoolean.CheckedChanged
        If ChkHMIBoolean.Checked = True Then
            ChkHMIBoolean.Checked = False
        End If
    End Sub

    Private Sub chkAliasNumeric_CheckedChanged(sender As Object, e As EventArgs) Handles chkAliasNumeric.CheckedChanged
        If ChkHMINumeric.Checked = True Then
            ChkHMINumeric.Checked = False
        End If
    End Sub

    Private Sub chkCalCulatedNumeric_CheckedChanged(sender As Object, e As EventArgs) Handles chkCalCulatedNumeric.CheckedChanged
        If ChkHMINumeric.Checked = True Then
            ChkHMINumeric.Checked = False
        End If
    End Sub

    Private Sub ChkHMIBoolean_CheckedChanged(sender As Object, e As EventArgs) Handles ChkHMIBoolean.CheckedChanged
        If (chkAliasBoolean.Checked = True) Or (chkCalculatedBoolean.Checked = True) Then
            chkAliasBoolean.Checked = False
            chkCalculatedBoolean.Checked = False
        End If
    End Sub

    Private Sub ChkHMINumeric_CheckedChanged(sender As Object, e As EventArgs) Handles ChkHMINumeric.CheckedChanged
        If (chkAliasNumeric.Checked = True) Or (chkCalCulatedNumeric.Checked = True) Then
            chkAliasNumeric.Checked = False
            chkCalCulatedNumeric.Checked = False
        End If
    End Sub

    Private Sub GetColumnNameList(theConnection As OleDbConnection, tableName As String, ByRef columnNames As SortedList(Of String, String))

        Dim schemaColumns As DataTable = theConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, New Object() {Nothing, Nothing, tableName, Nothing})

        For Each row As DataRow In schemaColumns.Rows

            Dim columnName As String = row("COLUMN_NAME").ToString()

            columnNames.Add(columnName, columnName)
        Next

        schemaColumns.Dispose()
    End Sub

    Private Sub GetTableNameList(theConnection As OleDbConnection, ByRef tableNames As SortedList(Of String, String))

        Dim schemaTables As DataTable = theConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, New Object() {Nothing, Nothing, Nothing, "TABLE"})

        For Each row As DataRow In schemaTables.Rows

            Dim tableName As String = row("TABLE_NAME").ToString()

            tableNames.Add(tableName, tableName)
        Next

        schemaTables.Dispose()
    End Sub

    Private Sub ImportUUT(ByVal cnStringSource As String, ByVal cnStringDestination As String, ByVal cnSource As OleDbConnection, ByVal cnDestination As ADODB.Connection)
        Dim rs As ADODB.Recordset = Nothing
        Dim strSelectSource As String
        Dim cnSrcOleDb As OleDbConnection = Nothing
        Dim cnDstOleDb As OleDbConnection = Nothing
        Dim srcColumnNames As SortedList(Of String, String) = New SortedList(Of String, String)
        Dim dstColumnNames As SortedList(Of String, String) = New SortedList(Of String, String)

        DatabaseHelper.OpenConnection(cnStringSource, cnSrcOleDb)
        DatabaseHelper.OpenConnection(cnStringDestination, cnDstOleDb)
        GetColumnNameList(cnSrcOleDb, "UUT", srcColumnNames)
        GetColumnNameList(cnDstOleDb, "UUT", dstColumnNames)
        strSelectSource = "SELECT * FROM UUT WHERE Model = '" & cboEngineList.Text & "'"

        If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM UUT", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
            Dim cmd As OleDbCommand = New OleDbCommand(strSelectSource, cnSource)
            Dim reader As OleDbDataReader = cmd.ExecuteReader()

            While (reader.Read())

                Try
                    rs.AddNew()
                    rs.Fields("Model").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("Model"))
                    '*
                    '* This takes care of all the common column between the tables source.UUT and destination.UUT
                    '*
                    For Each columnName As String In srcColumnNames.Keys
                        If columnName = "Model" Then Continue For
                        If dstColumnNames.ContainsKey(columnName) Then
                            rs.Fields(columnName).Value = reader.Item(columnName)
                        End If
                    Next
                    rs.Update()
                    '*
                    '* If these column exist in the source but not in the destination, it means we are
                    '* importing from the old CELDAS TestSession to the new CELDAS Core TestSession.
                    '* These columns have been normalized.
                    '* They need the UUT row to be already created because of foreign key constraints
                    '* Between UUT_Question.Model to UUT.Model, UUT_Choice.Model to UUT.Model, etc
                    '*
                    For Each columnName As String In srcColumnNames.Keys
                        If columnName = "Model" Then Continue For
                        If dstColumnNames.ContainsKey(columnName) Then
                            '*
                            '* Common column between source.UUT and destination.UUT
                            '* UUT Table has been processed by loop above, nothing to do
                            '* with them for this pass.
                            '*
                        ElseIf columnName.ToUpper().StartsWith("QUESTION ") Then
                            ImportUUTQuestionColumns(IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("Model")), reader, columnName, cnDestination)
                        ElseIf columnName.ToUpper().StartsWith("OPTION ") Then
                            ImportUUTOptionColumns(IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("Model")), reader, columnName, cnDestination)
                        ElseIf columnName.ToUpper().StartsWith("CHOICETITLE ") Then
                            ImportUUTChoiceColumns(IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("Model")), reader, columnName, cnDestination)
                        ElseIf columnName.ToUpper().StartsWith("HMI") OrElse columnName.ToUpper().StartsWith("CRITICAL HMI") Then
                            ImportUUTHMIColumns(IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("Model")), reader, columnName, cnDestination)
                        End If
                    Next

                Catch ex As Exception

                    Console.WriteLine(ex.ToString())

                End Try

            End While
            reader.Close()
            cmd.Dispose()
            DatabaseHelper.CloseRecordset(rs)
            ImportUUTTrip(cnStringSource, cnStringDestination, cnSource, cnDestination)

        Else
            MessageBox.Show("Impossible d'ouvrir UUT")
        End If
        DatabaseHelper.CloseConnection(cnSrcOleDb)
        DatabaseHelper.CloseConnection(cnDstOleDb)
        srcColumnNames.Clear()
        dstColumnNames.Clear()
    End Sub

    Private Sub ImportUUTChoiceColumns(model As String, reader As OleDbDataReader, columnName As String, cnDestination As ADODB.Connection)
        Dim strChoiceNumber As String = columnName.ToUpper().Remove(0, "CHOICETITLE ".Length())
        Dim iChoiceNumber As Integer
        If Integer.TryParse(strChoiceNumber, iChoiceNumber) AndAlso iChoiceNumber.ToString("00").Equals(strChoiceNumber) Then
            If (reader.Item(columnName).ToString() <> "NO TITLE") Then
                Dim rs2 As New ADODB.Recordset
                Dim rs3 As New ADODB.Recordset
                If DatabaseHelper.OpenRecordset(rs2, "SELECT * FROM UUT_Choice", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                    cnDestination.BeginTrans()
                    Try
                        rs2.AddNew()
                        rs2.Fields("Model").Value = model
                        rs2.Fields("ChoiceID").Value = iChoiceNumber.ToString()
                        rs2.Fields("ChoiceTitle").Value = reader.Item(columnName)
                        rs2.Fields("DefaultIndex").Value = "1"
                        rs2.Update()
                        If DatabaseHelper.OpenRecordset(rs3, "SELECT * FROM UUT_Choice_Items", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                            Dim index As Integer = 1
                            Dim done As Boolean = False

                            While Not done
                                Dim strChoiceColumnName As String = "Choice" + iChoiceNumber.ToString("00") + " " + index.ToString("00")
                                Dim obj = reader.Item(strChoiceColumnName)
                                If Not obj Is Nothing Then
                                    If obj.ToString() <> "NO CHOICE" Then
                                        rs3.AddNew()
                                        rs3.Fields("Model").Value = model
                                        rs3.Fields("ChoiceID").Value = iChoiceNumber.ToString()
                                        rs3.Fields("ItemID").Value = index.ToString()
                                        rs3.Fields("ItemDescription").Value = reader.Item(strChoiceColumnName)
                                        rs3.Update()
                                        index += 1
                                    Else
                                        done = True
                                    End If
                                Else
                                    done = True
                                End If
                            End While
                        End If
                        cnDestination.CommitTrans()
                    Catch ex As Exception
                        cnDestination.RollbackTrans()
                        MessageBox.Show("Exception processing '" + columnName + "'" + vbCrLf + ex.ToString())
                    Finally
                        DatabaseHelper.CloseRecordset(rs2)
                    End Try
                End If
            End If
        Else
            MessageBox.Show("Unexpected Option column '" + columnName + "'")
        End If
    End Sub

    Private Sub ImportUUTHMIColumns(model As String, reader As OleDbDataReader, columnName As String, cnDestination As ADODB.Connection)
        Dim strHMINumber As String
        If columnName.ToUpper().StartsWith("HMI") Then
            strHMINumber = columnName.ToUpper().Remove(0, "HMI".Length())
        Else
            strHMINumber = columnName.ToUpper().Remove(0, "CRITICAL HMI".Length())
        End If
        Dim iHMINumber As Integer
        If Integer.TryParse(strHMINumber, iHMINumber) AndAlso iHMINumber.ToString("00").Equals(strHMINumber) Then
            If (reader.Item(columnName).ToString() <> "NO HMI") Then
                Dim rs2 As New ADODB.Recordset
                If DatabaseHelper.OpenRecordset(rs2, "SELECT * FROM UUT_HMI_Window", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                    Try
                        rs2.AddNew()
                        rs2.Fields("Model").Value = model
                        rs2.Fields("ID").Value = iHMINumber.ToString("00")
                        rs2.Fields("HMI_Window_Name").Value = reader.Item("HMI" + iHMINumber.ToString("00"))
                        If reader.Item("HMI" + iHMINumber.ToString("00")).ToString().ToUpper() = _
                            (reader.Item("Model").ToString().Replace("-", "_") + "_Screen").ToString().ToUpper() Then
                            rs2.Fields("MainScreen").Value = True.ToString()
                        End If
                        If columnName.ToUpper().StartsWith("HMI") Then
                            rs2.Fields("Master").Value = True.ToString()
                            rs2.Fields("Slave").Value = False.ToString()
                        Else
                            rs2.Fields("Master").Value = False.ToString()
                            rs2.Fields("Slave").Value = True.ToString()
                        End If
                        rs2.Update()
                    Catch ex As Exception
                        MessageBox.Show("Exception processing '" + columnName + "'")
                    Finally
                        DatabaseHelper.CloseRecordset(rs2)
                    End Try
                End If
            End If
        Else
            MessageBox.Show("Unexpected Option column '" + columnName + "'")
        End If
    End Sub

    Private Sub ImportUUTOptionColumns(model As String, reader As OleDbDataReader, columnName As String, cnDestination As ADODB.Connection)
        Dim strOptionNumber As String = columnName.ToUpper().Remove(0, "OPTION ".Length())
        Dim iOptionNumber As Integer
        If Integer.TryParse(strOptionNumber, iOptionNumber) AndAlso iOptionNumber.ToString("00").Equals(strOptionNumber) Then
            If (reader.Item(columnName).ToString() <> "NO OPTION") Then
                Dim rs2 As New ADODB.Recordset
                If DatabaseHelper.OpenRecordset(rs2, "SELECT * FROM UUT_Option", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                    Try
                        rs2.AddNew()
                        rs2.Fields("Model").Value = model
                        rs2.Fields("OptionID").Value = iOptionNumber.ToString("00")
                        rs2.Fields("OptionTitle").Value = reader.Item("Option " + iOptionNumber.ToString("00"))
                        rs2.Update()
                    Catch ex As Exception
                        MessageBox.Show("Exception processing '" + columnName + "'")
                    Finally
                        DatabaseHelper.CloseRecordset(rs2)
                    End Try
                End If
            End If
        Else
            MessageBox.Show("Unexpected Option column '" + columnName + "'")
        End If
    End Sub

    Private Sub ImportUUTQuestionColumns(model As String, reader As OleDbDataReader, columnName As String, cnDestination As ADODB.Connection)
        Dim strQuestionNumber As String = columnName.ToUpper().Remove(0, "QUESTION ".Length())
        Dim iQuestionNumber As Integer
        If Integer.TryParse(strQuestionNumber, iQuestionNumber) AndAlso iQuestionNumber.ToString("00").Equals(strQuestionNumber) Then
            If (reader.Item(columnName).ToString() <> "NO QUESTION") Then
                Dim rs2 As New ADODB.Recordset
                If DatabaseHelper.OpenRecordset(rs2, "SELECT * FROM UUT_Question", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                    Try
                        rs2.AddNew()
                        rs2.Fields("Model").Value = model
                        rs2.Fields("QuestionID").Value = iQuestionNumber.ToString("00")
                        rs2.Fields("Question").Value = reader.Item("Question " + iQuestionNumber.ToString("00"))
                        rs2.Fields("DataType").Value = reader.Item("Qtype " + iQuestionNumber.ToString("00"))
                        rs2.Fields("HiLimit").Value = reader.Item("Hlimit " + iQuestionNumber.ToString("00"))
                        rs2.Fields("LoLimit").Value = reader.Item("Llimit " + iQuestionNumber.ToString("00"))
                        rs2.Fields("Mandatory").Value = reader.Item("Required " + iQuestionNumber.ToString("00"))
                        rs2.Update()
                    Catch ex As Exception
                        MessageBox.Show("Exception processing '" + columnName + "'")
                    Finally
                        DatabaseHelper.CloseRecordset(rs2)
                    End Try
                End If
            End If
        Else
            MessageBox.Show("Unexpected Question column '" + columnName + "'")
        End If
    End Sub

    Private Sub ImportUUTTrip(ByVal cnStringSource As String, ByVal cnStringDestination As String, ByVal cnSource As OleDbConnection, ByVal cnDestination As ADODB.Connection)
        Dim rs As ADODB.Recordset = Nothing
        Dim strSelectSource As String
        Dim cnSrcOleDb As OleDbConnection = Nothing
        Dim cnDstOleDb As OleDbConnection = Nothing
        Dim srcColumnNames As SortedList(Of String, String) = New SortedList(Of String, String)
        Dim dstColumnNames As SortedList(Of String, String) = New SortedList(Of String, String)
        Dim isNewSchema As Boolean = False

        DatabaseHelper.OpenConnection(cnStringSource, cnSrcOleDb)
        DatabaseHelper.OpenConnection(cnStringDestination, cnDstOleDb)
        GetColumnNameList(cnSrcOleDb, "UUT_Trip", srcColumnNames)
        GetColumnNameList(cnDstOleDb, "UUT_Trip", dstColumnNames)

        If srcColumnNames.Keys.Contains("Model") Then
            strSelectSource = "SELECT * FROM UUT_Trip WHERE Model = '" & cboEngineList.Text & "'"
            isNewSchema = True
        Else
            strSelectSource = "SELECT UUT_Trip.*, UUT.Model FROM UUT_Trip, UUT WHERE UUT_Trip.UUT_ID = UUT.ID AND UUT.Model = '" & cboEngineList.Text & "'"
        End If


        Try
            If DatabaseHelper.OpenRecordset(rs, "SELECT * FROM UUT_Trip", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                Dim cmd As OleDbCommand = New OleDbCommand(strSelectSource, cnSource)
                Dim reader As OleDbDataReader = cmd.ExecuteReader()

                While (reader.Read())

                    If (isNewSchema) Then
                        rs.AddNew()
                        rs.Fields("Model").Value = IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("Model"))
                        '*
                        '* This takes care of all the common column between the tables source.UUT and destination.UUT
                        '*
                        For Each columnName As String In srcColumnNames.Keys
                            If columnName = "Model" Then Continue For
                            If dstColumnNames.ContainsKey(columnName) Then
                                rs.Fields(columnName).Value = reader.Item(columnName)
                            End If
                        Next
                        rs.Update()
                    Else
                        '*
                        '* If these column exist in the source but not in the destination, it means we are
                        '* importing from the old CELDAS TestSession to the new CELDAS Core TestSession.
                        '* These columns have been normalized.
                        '* They need the UUT row to be already created because of foreign key constraints
                        '* Between UUT_Question.Model to UUT.Model, UUT_Choice.Model to UUT.Model, etc
                        '*
                        For Each columnName As String In srcColumnNames.Keys
                            If columnName = "Model" Then Continue For
                            If dstColumnNames.ContainsKey(columnName) Then
                                '*
                                '* Common column between source.UUT_Trip and destination.UUT_Trip
                                '* UUT Table has been processed by loop above, nothing to do
                                '* with them for this pass.
                                '*
                                Continue For
                            ElseIf columnName.ToUpper().StartsWith("TRIP_NAME_") Then
                                If (Not DBNull.Value.Equals(reader.Item(columnName)) AndAlso reader.Item(columnName).ToString() <> String.Empty) Then
                                    ImportUUTTripColumns(IIf(chkSubstitute.Checked, txtSubstituteEngine.Text, reader.Item("Model")), reader, columnName, cnDstOleDb, cnDestination)
                                End If
                            ElseIf columnName.ToUpper().StartsWith("TRIP_VALUE_") Then
                                Continue For
                            ElseIf columnName.ToUpper() = "UUT_ID" Then
                                Continue For
                            Else
                                MessageBox.Show("UUT_Trip colonne inconnue: " + columnName)
                                Continue For
                            End If
                        Next
                    End If
                End While
                reader.Close()
                cmd.Dispose()
                DatabaseHelper.CloseRecordset(rs)
            Else
                MessageBox.Show("Impossible d'ouvrir UUT_Trip")
            End If
        Catch ex As Exception

            MessageBox.Show(ex.ToString())

        Finally
            DatabaseHelper.CloseConnection(cnSrcOleDb)
            DatabaseHelper.CloseConnection(cnDstOleDb)
        End Try

        srcColumnNames.Clear()
        dstColumnNames.Clear()
    End Sub

    Private Sub ImportUUTTripColumns(model As String, reader As OleDbDataReader, columnName As String, cnDstOleDb As OleDbConnection, cnDestination As ADODB.Connection)
        Dim regex As Regex = New Regex("^TRIP_NAME_(\w+)(\d+)")
        Dim strTripTypeCode As String = regex.Replace(columnName.ToUpper(), "$1")
        Dim strSpeedTripNumber As String = columnName.ToUpper().Remove(0, ("TRIP_NAME_" + strTripTypeCode).Length())
        Dim iSpeedTripNumber As Integer
        Dim strTripType As String = String.Empty

        Select Case strTripTypeCode
            Case "F"
                strTripType = "Frequency"
            Case "N"
                strTripType = "Speed"
            Case "Q"
                strTripType = "Torque"
            Case "T"
                strTripType = "Temperature"
            Case "V"
                strTripType = "Voltage"
            Case "VIB"
                strTripType = "Vibration"
            Case Else
                MessageBox.Show("Unexpected Trip Code '" + strTripTypeCode + "', column '" + columnName + "'")
                Exit Sub
        End Select

        If Integer.TryParse(strSpeedTripNumber, iSpeedTripNumber) AndAlso iSpeedTripNumber.ToString("0").Equals(strSpeedTripNumber) Then
            If (reader.Item(columnName).ToString() <> String.Empty) Then
                Dim rs2 As New ADODB.Recordset
                If DatabaseHelper.OpenRecordset(rs2, "SELECT * FROM UUT_Trip", cnDestination, False, ADODB.CursorTypeEnum.adOpenDynamic) Then
                    Try
                        rs2.AddNew()
                        rs2.Fields("Model").Value = model
                        rs2.Fields("ID").Value = iSpeedTripNumber.ToString()
                        rs2.Fields("TypeID").Value = GetTripTypeID(strTripType, cnDstOleDb)
                        rs2.Fields("Name").Value = reader.Item("TRIP_NAME_" + strTripTypeCode + iSpeedTripNumber.ToString("0"))
                        rs2.Fields("Value").Value = reader.Item("TRIP_VALUE_" + strTripTypeCode + iSpeedTripNumber.ToString("0"))
                        rs2.Update()
                    Catch ex As Exception
                        MessageBox.Show("Exception processing '" + columnName + "'" + vbCrLf + ex.ToString())
                    Finally
                        DatabaseHelper.CloseRecordset(rs2)
                    End Try
                End If
            End If
        Else
            MessageBox.Show("Unexpected UUT_Trip column '" + columnName + "'")
        End If
    End Sub

    Private Function GetTripTypeID(strTripType As String, cnDb As IDbConnection) As String
        Dim cmd As IDbCommand = cnDb.CreateCommand()

        cmd.CommandText = "SELECT ID FROM UUT_Trip_Type WHERE Description = '" + strTripType + "'"

        Return cmd.ExecuteScalar().ToString()
    End Function

End Class

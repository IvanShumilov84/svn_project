Imports System
Imports System.Collections
Imports System.IO
Imports System.Windows
Imports System.Windows.Forms
Imports System.Diagnostics
Imports System.Reflection
Imports System.Collections.Generic
Imports KineticaRT
Imports KineticaRT.Controls
Imports KineticaRT.Channels
Imports KineticaRT.Values
Imports System.Data
Imports System.Data.OleDb
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Runtime.CompilerServices

Imports CEL.HMI
Imports CEL.Library
Imports CEL.Data.BusinessObjects

Imports EntitySpaces.DynamicQuery
Imports EntitySpaces.Interfaces
Imports EntitySpaces.Loader

Imports KineticaRT.Studio.Controls

Public Class SpecsGenerator
    Implements INotifyPropertyChanged

    Private Const STARTING_ROW_INDEX As Integer = 5
    Private _currentRowIndex As Integer = STARTING_ROW_INDEX
    Private _qtyOfControlToGenerate As Integer = 0      'Control scanned to be generated
    Private _qtyOfControlGenerated As Integer = 0       'Control generated in Excel's worksheet
    Private _qtyOfControlSkipped As Integer = 0         'Control skipped during scan

    Private _CELInstance As CELDAS = CELDAS.Instance()

    Private _ExcelApp As Excel.Application = CELDAS._ExcelApplication
    Private _ExcelWorkbook As Excel.Workbook

    Private _ControlList As ControlList               'Collection of window where the scanned controls are
    Private _windowsToSkip As New List(Of String)                   'Skipped windows during scanning
    Private _controlToSkip As New List(Of Type)                     'Skipped controls during scanning                        
    Private _enginesWinCollection As List(Of CELDAS.sEngineWindowInfo)

    Private _UUTDynamicChannelsCollectionCollection As New UUTDynamicChannelCollectionCollection()
    Private _engUnitsNamesDictionary As New Dictionary(Of Integer?, String)


    Private _ProjectID As String
    Private _ProjectIDNullableInt As Integer?
    Private _engineFamily As String
    Private _engineModel As String

    'TOBECONTINUE
#Region "Used by SpecsGeneratorForm in CEL->src"
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private Sub NotifyPropertyChanged(<CallerMemberName()> Optional ByVal propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub
#End Region

    Public Sub New()

        _ProjectID = _CELInstance.Project.Name

        _engineFamily = _CELInstance.EngineFamily
        _engineModel = _CELInstance.EngineModel

        'Retrieve the HMI form from the CELDAS instance
        _enginesWinCollection = _CELInstance.EngineWindowCollection

        _ControlList = New ControlList(_ProjectID + " " + _engineModel, String.Empty)

        _windowsToSkip.Clear()
        _controlToSkip.Clear()

        'Load collections from TestSession
        LoadUUTDynamicChannels()
        LoadEngUnitsDictionary()

    End Sub

    'Get the dynamic collection related with the engine model 
    Private Sub LoadUUTDynamicChannels()
        Try
            'Query Database
            esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()
            _UUTDynamicChannelsCollectionCollection.es.Connection.Name = _CELInstance.DefaultConnectionName
            _UUTDynamicChannelsCollectionCollection.Query.Where(_UUTDynamicChannelsCollectionCollection.Query.ProjectID = _CELInstance.Project.Number)
            _UUTDynamicChannelsCollectionCollection.Query.Where(_UUTDynamicChannelsCollectionCollection.Query.EngineModel = _CELInstance.EngineModel)

            If _UUTDynamicChannelsCollectionCollection.Query.Load() Then
                _ProjectIDNullableInt = _UUTDynamicChannelsCollectionCollection.Item(0).ProjectID
            End If

        Catch ex As Exception
            clsTopMostMessageBox.Show("Unable to load: UUTDynamicChannelsCollection in SpecsGenerator", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try

    End Sub

    'Build a dictionary of string engineering units
    Private Sub LoadEngUnitsDictionary()
        Try
            Dim engUnitsCollection As New EngineeringUnitCollection()
            esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()

            engUnitsCollection.es.Connection.Name = _CELInstance.DefaultConnectionName
            If engUnitsCollection.LoadAll() Then
                For Each EngUnit As EngineeringUnit In engUnitsCollection
                    _engUnitsNamesDictionary.Add(EngUnit.Id, EngUnit.DisplayName)
                Next
            Else
                clsTopMostMessageBox.Show("Unable to load: engUnitsCollection in SpecsGenerator", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If


        Catch ex As Exception

        End Try
    End Sub

#Region "Methods to add Control or window during scan"
    Private Sub AddWindowToRejectedList(windowName As String)
        _windowsToSkip.Add(windowName)
    End Sub

    Private Sub AddControlToRejectedList(controlType As Type)
        _controlToSkip.Add(controlType)
    End Sub
#End Region

#Region "Scan all controls on loaded Hmi"
    Private Sub ScanControlsOverHmiWindows()
        'TODO: add a status state for our SpecsGeneratorFrom "Scanning"

        For Each engWin As CELDAS.sEngineWindowInfo In _enginesWinCollection


            'When DEBUG If engWin.MainScreen And Not _windowsToSkip.Contains(engWin.WindowName) Then
            'Check if there is any window to reject from our scan
            If Not _windowsToSkip.Contains(engWin.WindowName) Then

                Dim windowControls As New ControlList(engWin.WindowName, String.Empty)

                windowControls.Parent = _ControlList

                'Search for controls inside our HMI window
                SearchControl(windowControls, engWin.EngineWindow.Controls)

                windowControls.Controls.Sort(Function(x, y) x.Source.CompareTo(y.Source))
                _ControlList.AddContainer(windowControls)
            End If
        Next
    End Sub
#End Region

    Private Sub SearchControl(ByRef ctrlList As ControlList, ByRef controls As System.Windows.Forms.Control.ControlCollection)

        For Each ctrl As System.Windows.Forms.Control In controls
            'Check if there is any controls to reject before scan
            If Not _controlToSkip.Contains(ctrl.GetType) Then
                Dim obj As Object = ctrl
                Dim baseName As String = obj.GetType.BaseType.Name
                Dim childControlbaseName As String = obj.GetType.BaseType.BaseType.Name

                'Check if it's a control or a container 
                'then if we have a control with children it's probably a container, and we dig into the baseName to be sure about that
                If ctrl.HasChildren And Not baseName.Equals("ControlBase") And Not childControlbaseName.Equals("ControlBase") Then
                    'Check if there is any Panels inside our Container because tab contains panels
                    Dim Panels As KineticaRT.Controls.PanelCollection = DirectCast(GetPropValue(ctrl, "Panels"), KineticaRT.Controls.PanelCollection)


                    If childControlbaseName.Contains("ContainerControl") Then 'PANELBASE

                        SearchControl(ctrlList, ctrl.Controls)

                    ElseIf Not Panels Is Nothing Then   'TAB: Check if we got any Panels from DirectCast above

                        'Check if we have more than one panel in this case add them as container, and set their name 
                        For Each pnl As KineticaRT.Controls.PanelInfo In Panels
                            Dim Name As String = DirectCast(GetPropValue(pnl, "DllName"), System.String)
                            Dim Text As String = DirectCast(GetPropValue(pnl, "Text"), System.String)
                            Dim childCtrlList As New ControlList(Name, Text, True)
                            childCtrlList.Parent = ctrlList

                            SearchControl(childCtrlList, pnl.Panel.Controls)

                            childCtrlList.Controls.Sort(Function(x, y) x.Source.CompareTo(y.Source))
                            ctrlList.AddContainer(childCtrlList)

                        Next

                    Else 'Otherwise it's probably a frame

                        Dim Name As String = DirectCast(GetPropValue(ctrl, "Name"), System.String)
                        Dim TitleText As String = DirectCast(GetPropValue(ctrl, "TitleText"), System.String)
                        Dim childCtrlList As New ControlList(Name, TitleText)
                        childCtrlList.Parent = ctrlList

                        If childControlbaseName = "DialBase" Then
                            Me.ControlsToGenerate += 1
                            ctrlList.AddControl(ctrl)
                            SearchControl(ctrlList, ctrl.Controls)
                        Else
                            SearchControl(childCtrlList, ctrl.Controls)

                            childCtrlList.Controls.Sort(Function(x, y) x.Source.CompareTo(y.Source))
                            ctrlList.AddContainer(childCtrlList)
                        End If



                        End If

                Else
                        Me.ControlsToGenerate += 1 'Will be used by our SpecsGeneratorForm
                        ctrlList.AddControl(ctrl)
                End If
            Else
                Me.ControlsSkipped += 1 'Will be used by our SpecsGeneratorForm
            End If
        Next

    End Sub

    Public Sub GenerateSpecs()

        If Not _ControlList.HasContainers Then
            _ExcelWorkbook = _ExcelApp.Workbooks.Add()

            'TODO: Delete all basic worksheets ONLY after the specs were generate BEFORE the workbook will be visible
            CType(_ExcelWorkbook.Sheets(1), Excel.Worksheet).Delete()
            CType(_ExcelWorkbook.Sheets(2), Excel.Worksheet).Delete()

            'The following windows will be ignore during HMI scan
            'TODO: Let the user selects that inside a form same for the rejected controls
            Me.AddWindowToRejectedList("Welcome_Screen")
            Me.AddWindowToRejectedList("UserInputs")
            Me.AddWindowToRejectedList("Test_Password")
            Me.AddWindowToRejectedList("MeasurementUnitSelector")
            Me.AddWindowToRejectedList("SystemCheck_Screen")
            Me.AddWindowToRejectedList("Archiving")
            Me.AddWindowToRejectedList("Statistics")

            'The following controls will be ignore during scan
            Me.AddControlToRejectedList(GetType(System.Windows.Forms.ListView))
            Me.AddControlToRejectedList(GetType(System.Windows.Forms.Label))
            Me.AddControlToRejectedList(GetType(KineticaRT.Controls.PictureButton))

            ScanControlsOverHmiWindows()

            'Generate an Excel's workbook
            _ExcelApp.ScreenUpdating = False
            For Each ctrl As ControlList In _ControlList.Containers
                Dim ExcelWorksheet As Excel.Worksheet = DirectCast(_ExcelWorkbook.Worksheets.Add(), Excel.Worksheet)

                ExcelWorksheet.Name = ctrl.Name

                'Build header in Excel's worksheet
                InitColNames(ExcelWorksheet, _engineModel)

                'Generate each scanned control inside a Excel's worksheet
                BuildExcelSheet(ExcelWorksheet, ctrl)

                _currentRowIndex = STARTING_ROW_INDEX

            Next
            _ExcelApp.ScreenUpdating = True
            _ExcelApp.Visible = True
        End If



    End Sub

#Region "Excel's colums header initialisation"
    Private Sub InitColNames(sheet As Excel.Worksheet, engFamily As String)

        sheet.Names.Add("CompanyName", sheet.Range("A1:F1"))
        With sheet.Range("A1:F1")
            .RowHeight = 30
            .Font.Size = 16
            .Font.Bold = True
            .MergeCells = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
            .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
            .Borders.Weight = Excel.XlBorderWeight.xlMedium
            .Value = "CEL AEROSPACE"
        End With

        sheet.Names.Add("BlankSpace", sheet.Range("A2:F2"))
        With sheet.Range("A2:F2")
            .RowHeight = 30
            .MergeCells = True
        End With

        sheet.Names.Add("EngineFamily", sheet.Range("A3:F3")).Name = engFamily
        With sheet.Range("A3:F3")
            .RowHeight = 30
            .Font.Size = 14
            .Font.Bold = True
            .MergeCells = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
            .Font.Color = System.Drawing.ColorTranslator.ToOle(Drawing.Color.White)
            .Interior.Color = System.Drawing.ColorTranslator.ToOle(Drawing.Color.Purple)
            .Interior.Pattern = Excel.XlPattern.xlPatternSolid
            .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
            .Borders.Weight = Excel.XlBorderWeight.xlMedium
            .Value = engFamily
        End With

        sheet.Names.Add("TagName", sheet.Range("A4"))
        With sheet.Range("A4")
            .RowHeight = 18
            .ColumnWidth = 45
            .Font.Size = 12
            .Font.Bold = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
            .Font.Color = System.Drawing.ColorTranslator.ToOle(Drawing.Color.White)
            .Interior.Color = System.Drawing.ColorTranslator.ToOle(Drawing.Color.Purple)
            .Interior.Pattern = Excel.XlPattern.xlPatternSolid
            .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
            .Borders.Weight = Excel.XlBorderWeight.xlMedium
            .Value = "Tag/Name"
        End With

        sheet.Names.Add("Condition", sheet.Range("B4"))
        With sheet.Range("B4")
            .ColumnWidth = 85
            .Font.Size = 12
            .Font.Bold = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
            .Font.Color = System.Drawing.ColorTranslator.ToOle(Drawing.Color.White)
            .Interior.Color = System.Drawing.ColorTranslator.ToOle(Drawing.Color.Purple)
            .Interior.Pattern = Excel.XlPattern.xlPatternSolid
            .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
            .Borders.Weight = Excel.XlBorderWeight.xlMedium
            .Value = "Condition"
        End With

        sheet.Names.Add("CMMReference", sheet.Range("C4"))
        With sheet.Range("C4")
            .ColumnWidth = 35
            .Font.Size = 12
            .Font.Bold = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
            .Font.Color = System.Drawing.ColorTranslator.ToOle(Drawing.Color.White)
            .Interior.Color = System.Drawing.ColorTranslator.ToOle(Drawing.Color.Purple)
            .Interior.Pattern = Excel.XlPattern.xlPatternSolid
            .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
            .Borders.Weight = Excel.XlBorderWeight.xlMedium
            .Value = "CMM Reference"
        End With

        sheet.Names.Add("HMIDatabase", sheet.Range("D4"))
        With sheet.Range("D4")
            .ColumnWidth = 55
            .Font.Size = 12
            .Font.Bold = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
            .Font.Color = System.Drawing.ColorTranslator.ToOle(Drawing.Color.White)
            .Interior.Color = System.Drawing.ColorTranslator.ToOle(Drawing.Color.Purple)
            .Interior.Pattern = Excel.XlPattern.xlPatternSolid
            .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
            .Borders.Weight = Excel.XlBorderWeight.xlMedium
            .Value = "HMI Database"
        End With

        sheet.Names.Add("Testing", sheet.Range("E4")).Name = "Testing"
        With sheet.Range("E4")
            .ColumnWidth = 55
            .Font.Size = 12
            .Font.Bold = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
            .Font.Color = System.Drawing.ColorTranslator.ToOle(Drawing.Color.White)
            .Interior.Color = System.Drawing.ColorTranslator.ToOle(Drawing.Color.Purple)
            .Interior.Pattern = Excel.XlPattern.xlPatternSolid
            .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
            .Borders.Weight = Excel.XlBorderWeight.xlMedium
            .Value = "Testing"
        End With

        sheet.Names.Add("Results", sheet.Range("F4"))
        With sheet.Range("F4")
            .ColumnWidth = 55
            .Font.Size = 12
            .Font.Bold = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
            .Font.Color = System.Drawing.ColorTranslator.ToOle(Drawing.Color.White)
            .Interior.Color = System.Drawing.ColorTranslator.ToOle(Drawing.Color.Purple)
            .Interior.Pattern = Excel.XlPattern.xlPatternSolid
            .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
            .Borders.Weight = Excel.XlBorderWeight.xlMedium
            .Value = "Results"
        End With
    End Sub
#End Region

#Region "Build an Excel's worsheet"
    Private Sub BuildExcelSheet(ByRef sheet As Excel.Worksheet, ByRef controlList As ControlList)

        If controlList.HasControls Then
            'Check if there is any valid control with a channel inside the container
            If controlList.IsContainValidSource Then
                'Generate the container header
                Dim containerBanner As New System.Text.StringBuilder()

                With sheet.Range("A" + _currentRowIndex.ToString + ":" + "F" + _currentRowIndex.ToString)
                    .MergeCells = True
                    .Interior.Color = System.Drawing.ColorTranslator.ToOle(Drawing.Color.Blue)
                    .Font.Color = System.Drawing.ColorTranslator.ToOle(Drawing.Color.White)
                    .Font.Size = 14
                    .Font.Bold = True
                    .Interior.Pattern = Excel.XlPattern.xlPatternSolid
                    .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                    .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                    .Borders.Weight = Excel.XlBorderWeight.xlMedium

                    If controlList.TitleText IsNot String.Empty Then
                        containerBanner.Append(controlList.TitleText).Append(" - ")
                    End If

                    containerBanner.Append(controlList.Name.Substring(controlList.Name.IndexOf("\") + 1))

                    .Value = containerBanner.ToString
                End With
                _currentRowIndex += 1
            End If

            For Each ctrl As ControlAndType In controlList.Controls

                Dim sourceName As String = String.Empty
                Dim enableSourceName As String = String.Empty
                Dim resetSourceName As String = String.Empty
                Dim commandSource As String = String.Empty
                Dim firstColRangeValue As String = String.Empty
                Dim initialValue As String = String.Empty
                Dim finalValue As String = String.Empty
                Dim fvName As String = String.Empty
                Dim collectionName As String = String.Empty
                Dim btn_type As String = String.Empty

                'It's contain channel reference as exemple a boolean control have a sourceName then
                'we append sourceName, sourceName+"_CMD", sourceName+"_ENB", and finally sourceName+"_RST"
                'The order is the same as the TestSession
                Dim HMIDatabase As New System.Text.StringBuilder
                Dim HMIDatabaseAndExpr As New System.Text.StringBuilder

                'It's the text related to fvName, significant number, and limits of a numeric control
                'Build in the order of the text above
                Dim LimitsText As New System.Text.StringBuilder
                Dim strReader As StringReader = Nothing

                Dim rowCounter As Integer = 0
                Dim rowTotal As Integer = 0

                Dim IsNumericChannels As Boolean = False
                Dim IsBooleanChannels As Boolean = False

                'Check if it's a valid channel
                If ctrl.Source IsNot String.Empty Then

                    Try
                        'Query the HMIBoolean table
                        sourceName = ctrl.Source.ToString
                        esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()
                        Dim HMIBooleanChannelsCollection As New HMIBooleanChannelsCollection
                        Dim openP As New esComparison(esParenthesis.Open)
                        HMIBooleanChannelsCollection.es.Connection.Name = _CELInstance.DefaultConnectionName
                        Dim dynamicQueryProps As New EntitySpaces.DynamicQuery.esDynamicQuerySerializable.DynamicQueryProps(HMIBooleanChannelsCollection.Query)

                        'To put the OR expressions generated by the for loop between parenthesis so we open it first
                        HMIBooleanChannelsCollection.Query.Where(New esComparison(esParenthesis.Open))
                        'To put OR between each field condition
                        dynamicQueryProps.DefaultConjunction = esConjunction.Or

                        'Build our query like SELECT * FROM HMIBoolean WHERE (field = something or field = something or ...)
                        For Each collection As UUTDynamicChannelCollection In _UUTDynamicChannelsCollectionCollection
                            HMIBooleanChannelsCollection.Query.Where(HMIBooleanChannelsCollection.Query.CollectionName = collection.CollectionName)
                        Next

                        HMIBooleanChannelsCollection.Query.Where(New esComparison(esParenthesis.Close))
                        dynamicQueryProps.DefaultConjunction = esConjunction.And

                        'Add in our query SELECT * FROM HMIBoolean WHERE (field = something or field = something or ...) and ChannelName = sometingLookLike..
                        'It used for getting any channel name that contains the sourceName, and his dynamic collections
                        HMIBooleanChannelsCollection.Query.Where(HMIBooleanChannelsCollection.Query.ChannelName.Like(sourceName + "%"))
                        HMIBooleanChannelsCollection.Query.Load()

                        'If we don't find a boolean inside our HMIBoolean table we will automaticaly try in the HMINumeric table
                        If HMIBooleanChannelsCollection.IsValidIndex(0) Then
                            IsBooleanChannels = True

                            rowTotal = HMIBooleanChannelsCollection.Count

                            For Each HMIBooleanChannels As HMIBooleanChannels In HMIBooleanChannelsCollection
                                rowCounter += 1

                                'Write infos about our HMI Control, and we know that is the hmi control because we got his sourceName during the scan
                                If HMIBooleanChannels.ChannelName.Equals(sourceName) Then
                                    collectionName = HMIBooleanChannels.CollectionName.ToString
                                    initialValue = HMIBooleanChannels.InitialValue.ToString
                                    finalValue = HMIBooleanChannels.FinalValue.ToString
                                End If

                                'Add database relation at HMIDatabase field in our worksheet
                                HMIDatabase.AppendLine(HMIBooleanChannels.ChannelName)
                                HMIDatabaseAndExpr.AppendLine(HMIBooleanChannels.ChannelName)

                                If HMIBooleanChannels.Expression IsNot Nothing AndAlso HMIBooleanChannels.Expression IsNot String.Empty Then
                                    HMIDatabaseAndExpr.Append("Expr: ").Append(HMIBooleanChannels.Expression).AppendLine()

                                    If rowCounter < rowTotal Then
                                        HMIDatabaseAndExpr.AppendLine()
                                    End If
                                End If



                                'Try to find a fvName for our HMI Control in ChannelMapping table, if it have not already done during for loop 
                                If fvName Is String.Empty Then
                                    'Check if we have Destination Xor Source to search inside channel mapping table
                                    If HMIBooleanChannels.Destination IsNot String.Empty And HMIBooleanChannels.Source IsNot String.Empty Then
                                        'if the both field are filled with something query destination field first
                                        Dim HMIChannelsMapping As New HMIChannelsMapping
                                        HMIChannelsMapping.Query.Where(HMIChannelsMapping.Query.ProjectID = _ProjectIDNullableInt And _
                                                                       HMIChannelsMapping.Query.CollectionName = HMIBooleanChannels.CollectionName And _
                                                                       HMIChannelsMapping.Query.ChannelName = HMIBooleanChannels.Destination)

                                        'If we dont have anything then query the source field
                                        If HMIChannelsMapping.Query.Load Then
                                            If HMIChannelsMapping.FVName IsNot Nothing Then
                                                fvName = HMIChannelsMapping.FVName
                                            End If
                                        Else
                                            HMIChannelsMapping = New HMIChannelsMapping

                                            HMIChannelsMapping.Query.Where(HMIChannelsMapping.Query.ProjectID = _ProjectIDNullableInt And _
                                                                           HMIChannelsMapping.Query.CollectionName = HMIBooleanChannels.CollectionName And _
                                                                           HMIChannelsMapping.Query.ChannelName = HMIBooleanChannels.Source)

                                            If HMIChannelsMapping.Query.Load Then
                                                If HMIChannelsMapping.FVName IsNot Nothing Then
                                                    fvName = HMIChannelsMapping.FVName
                                                End If
                                            End If
                                        End If

                                    ElseIf HMIBooleanChannels.Destination Is String.Empty And HMIBooleanChannels.Source Is String.Empty Then
                                        'Do nothing
                                    Else
                                        Dim HMIChannelsMapping As New HMIChannelsMapping
                                        'Check who has a empty field
                                        If HMIBooleanChannels.Destination IsNot String.Empty And HMIBooleanChannels.Source Is String.Empty Then
                                            HMIChannelsMapping.Query.Where(HMIChannelsMapping.Query.ProjectID = _ProjectIDNullableInt And _
                                                                           HMIChannelsMapping.Query.CollectionName = HMIBooleanChannels.CollectionName And _
                                                                           HMIChannelsMapping.Query.ChannelName = HMIBooleanChannels.Destination)
                                        ElseIf HMIBooleanChannels.Destination Is String.Empty And HMIBooleanChannels.Source IsNot String.Empty Then
                                            HMIChannelsMapping.Query.Where(HMIChannelsMapping.Query.ProjectID = _ProjectIDNullableInt And _
                                                                           HMIChannelsMapping.Query.CollectionName = HMIBooleanChannels.CollectionName And _
                                                                           HMIChannelsMapping.Query.ChannelName = HMIBooleanChannels.Source)
                                        End If

                                        If HMIChannelsMapping.Query.Load Then
                                            If HMIChannelsMapping.FVName IsNot Nothing Then
                                                fvName = HMIChannelsMapping.FVName
                                            End If
                                        End If
                                    End If
                                End If

                            Next

                        Else
                            'It's the same proccess than HMIBoolean but with HMINumeric
                            'Query the HMINumeric table
                            IsNumericChannels = True
                            Dim HMINumericChannelsCollection As New HMINumericChannelsCollection
                            Dim openPNum As New esComparison(esParenthesis.Open)
                            HMINumericChannelsCollection.es.Connection.Name = _CELInstance.DefaultConnectionName
                            Dim dynamicQueryPropsNum As New EntitySpaces.DynamicQuery.esDynamicQuerySerializable.DynamicQueryProps(HMINumericChannelsCollection.Query)

                            HMINumericChannelsCollection.Query.Where(New esComparison(esParenthesis.Open))
                            dynamicQueryPropsNum.DefaultConjunction = esConjunction.Or
                            For Each collection As UUTDynamicChannelCollection In _UUTDynamicChannelsCollectionCollection

                                HMINumericChannelsCollection.Query.Where(HMINumericChannelsCollection.Query.CollectionName = collection.CollectionName)
                            Next
                            HMINumericChannelsCollection.Query.Where(New esComparison(esParenthesis.Close))

                            dynamicQueryPropsNum.DefaultConjunction = esConjunction.And

                            HMINumericChannelsCollection.Query.Where(HMINumericChannelsCollection.Query.ChannelName.Like(sourceName + "%"))
                            HMINumericChannelsCollection.Query.Load()

                            If HMINumericChannelsCollection.IsValidIndex(0) Then

                                rowTotal = HMINumericChannelsCollection.Count

                                For Each HMINumericChannels As HMINumericChannels In HMINumericChannelsCollection
                                    rowCounter += 1


                                    If HMINumericChannels.ChannelName.Equals(sourceName) Then
                                        collectionName = HMINumericChannels.CollectionName.ToString
                                        initialValue = HMINumericChannels.InitialValue.ToString
                                        finalValue = HMINumericChannels.FinalValue.ToString

                                        If fvName Is String.Empty Then
                                            If HMINumericChannels.Destination IsNot String.Empty And HMINumericChannels.Source IsNot String.Empty Then
                                                Dim HMIChannelsMapping As New HMIChannelsMapping
                                                HMIChannelsMapping.Query.Where(HMIChannelsMapping.Query.ProjectID = _ProjectIDNullableInt And _
                                                                               HMIChannelsMapping.Query.CollectionName = HMINumericChannels.CollectionName And _
                                                                               HMIChannelsMapping.Query.ChannelName = HMINumericChannels.Destination)

                                                If HMIChannelsMapping.Query.Load Then
                                                    If HMIChannelsMapping.FVName IsNot Nothing Then
                                                        fvName = HMIChannelsMapping.FVName
                                                    End If
                                                Else
                                                    HMIChannelsMapping = New HMIChannelsMapping

                                                    HMIChannelsMapping.Query.Where(HMIChannelsMapping.Query.ProjectID = _ProjectIDNullableInt And _
                                                                                   HMIChannelsMapping.Query.CollectionName = HMINumericChannels.CollectionName And _
                                                                                   HMIChannelsMapping.Query.ChannelName = HMINumericChannels.Source)

                                                    If HMIChannelsMapping.Query.Load Then
                                                        If HMIChannelsMapping.FVName IsNot Nothing Then
                                                            fvName = HMIChannelsMapping.FVName
                                                        End If
                                                    End If
                                                End If

                                            ElseIf HMINumericChannels.Destination Is String.Empty And HMINumericChannels.Source Is String.Empty Then
                                                'Do nothing
                                            Else
                                                Dim HMIChannelsMapping As New HMIChannelsMapping

                                                If HMINumericChannels.Destination IsNot String.Empty And HMINumericChannels.Source Is String.Empty Then
                                                    HMIChannelsMapping.Query.Where(HMIChannelsMapping.Query.ProjectID = _ProjectIDNullableInt And _
                                                                                   HMIChannelsMapping.Query.CollectionName = HMINumericChannels.CollectionName And _
                                                                                   HMIChannelsMapping.Query.ChannelName = HMINumericChannels.Destination)
                                                ElseIf HMINumericChannels.Destination Is String.Empty And HMINumericChannels.Source IsNot String.Empty Then
                                                    HMIChannelsMapping.Query.Where(HMIChannelsMapping.Query.ProjectID = _ProjectIDNullableInt And _
                                                                                   HMIChannelsMapping.Query.CollectionName = HMINumericChannels.CollectionName And _
                                                                                   HMIChannelsMapping.Query.ChannelName = HMINumericChannels.Source)
                                                End If

                                                If HMIChannelsMapping.Query.Load Then
                                                    If HMIChannelsMapping.FVName IsNot Nothing Then
                                                        fvName = HMIChannelsMapping.FVName
                                                    End If
                                                End If
                                            End If

                                        End If


                                        If fvName IsNot String.Empty Then
                                            LimitsText.AppendLine(fvName + ": ")
                                        End If

                                        If HMINumericChannels.DecimalDigits IsNot Nothing Then
                                            LimitsText.Append("Significative Numbers: ").Append(HMINumericChannels.DecimalDigits).AppendLine()
                                        End If

                                        'Check if there is at least one limit to be added
                                        'The limit text is build as ColorOfTheLimit + OVERorUNDER + Value + UnitOfTheValue
                                        If HMINumericChannels.ThresholdHighHighSource IsNot String.Empty Or
                                           HMINumericChannels.ThresholdHighSource IsNot String.Empty Or
                                           HMINumericChannels.ThresholdLowSource IsNot String.Empty Or
                                           HMINumericChannels.ThresholdLowLowSource IsNot String.Empty Then

                                            Dim HMINumericLimit As HMINumericChannels = Nothing
                                            LimitsText.Append("Limits: ").AppendLine()
                                            If HMINumericChannels.ThresholdHighHighSource IsNot String.Empty Then
                                                HMINumericLimit = New HMINumericChannels
                                                HMINumericLimit.Query.Where(HMINumericLimit.Query.CollectionName = HMINumericChannels.CollectionName And HMINumericLimit.Query.ChannelName = HMINumericChannels.ThresholdHighHighSource)
                                                If HMINumericLimit.Query.Load() Then
                                                    LimitsText.Append(HMINumericChannels.ThresholdHighHighColor.ToString).Append(" is OVER ").Append(HMINumericLimit.Expression.ToString).Append(_engUnitsNamesDictionary.Item(HMINumericLimit.EngUnitID)).AppendLine()
                                                End If
                                            End If

                                            If HMINumericChannels.ThresholdHighSource IsNot String.Empty Then
                                                HMINumericLimit = New HMINumericChannels
                                                HMINumericLimit.Query.Where(HMINumericLimit.Query.CollectionName = HMINumericChannels.CollectionName And HMINumericLimit.Query.ChannelName = HMINumericChannels.ThresholdHighSource)
                                                If HMINumericLimit.Query.Load() Then
                                                    LimitsText.Append(HMINumericChannels.ThresholdHighColor.ToString).Append(" is OVER ").Append(HMINumericLimit.Expression).Append(_engUnitsNamesDictionary.Item(HMINumericLimit.EngUnitID)).AppendLine()
                                                End If
                                            End If

                                            If HMINumericChannels.ThresholdLowSource IsNot String.Empty Then
                                                HMINumericLimit = New HMINumericChannels
                                                HMINumericLimit.Query.Where(HMINumericLimit.Query.CollectionName = HMINumericChannels.CollectionName And HMINumericLimit.Query.ChannelName = HMINumericChannels.ThresholdLowSource)
                                                If HMINumericLimit.Query.Load() Then
                                                    LimitsText.Append(HMINumericChannels.ThresholdLowColor.ToString).Append(" is UNDER ").Append(HMINumericLimit.Expression).Append(_engUnitsNamesDictionary.Item(HMINumericLimit.EngUnitID)).AppendLine()
                                                End If
                                            End If

                                            If HMINumericChannels.ThresholdLowLowSource IsNot String.Empty Then
                                                HMINumericLimit = New HMINumericChannels
                                                HMINumericLimit.Query.Where(HMINumericLimit.Query.CollectionName = HMINumericChannels.CollectionName And HMINumericLimit.Query.ChannelName = HMINumericChannels.ThresholdLowLowSource)
                                                If HMINumericLimit.Query.Load() Then
                                                    LimitsText.Append(HMINumericChannels.ThresholdLowLowColor.ToString).Append(" is UNDER ").Append(HMINumericLimit.Expression).Append(_engUnitsNamesDictionary.Item(HMINumericLimit.EngUnitID)).AppendLine()
                                                End If
                                            End If

                                            LimitsText.Append("White otherwise")

                                        Else
                                            LimitsText.Append("Limits: None (Always White)")
                                        End If

                                    End If

                                    'The HMIDatabaseText append each channel name that contains the sourceName
                                    HMIDatabase.AppendLine(HMINumericChannels.ChannelName)
                                    HMIDatabaseAndExpr.AppendLine(HMINumericChannels.ChannelName)

                                    If HMINumericChannels.Expression IsNot Nothing AndAlso HMINumericChannels.Expression IsNot String.Empty Then
                                        HMIDatabaseAndExpr.Append("Expr: ").Append(HMINumericChannels.Expression).AppendLine()

                                        If rowCounter < rowTotal Then
                                            HMIDatabaseAndExpr.AppendLine()
                                        End If
                                    End If

                                Next
                            Else
                                HMIDatabaseAndExpr.AppendLine(ctrl.Source).AppendLine(ctrl.Control.Text)
                            End If
                        End If

                        'It's used to read the build HMIDatabase text line by line for conditon field
                        strReader = New StringReader(HMIDatabase.ToString)

                        sheet.Range("A" + _currentRowIndex.ToString + ":" + "F" + _currentRowIndex.ToString).RowHeight = 95

                        With sheet.Range("A" + _currentRowIndex.ToString)
                            .Font.Size = 10
                            .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                            .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                            .Borders.Weight = Excel.XlBorderWeight.xlMedium
                            .WrapText = True

                            firstColRangeValue = "Collection Name: " + collectionName + Environment.NewLine + _
                                                 "Object Name: " + ctrl.Name + Environment.NewLine + _
                                                 "Object Text: """ + ctrl.Description + """"

                            If Not ctrl.Connected Then
                                sheet.Range("A" + _currentRowIndex.ToString + ":" + "F" + _currentRowIndex.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(Drawing.Color.Red)
                                firstColRangeValue = "NOT CONNECTED" + Environment.NewLine + firstColRangeValue
                            End If

                            .Value = firstColRangeValue
                        End With


                        With sheet.Range("B" + _currentRowIndex.ToString)
                            .Font.Size = 10
                            .Font.Bold = True
                            .Font.Underline = Excel.XlUnderlineStyle.xlUnderlineStyleNone
                            .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                            .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                            .Borders.Weight = Excel.XlBorderWeight.xlMedium

                            If TypeOf ctrl.Control Is KineticaRT.Controls.Button Then
                                Dim btn_kinetica As KineticaRT.Controls.Button = DirectCast(ctrl.Control, KineticaRT.Controls.Button)

                                Select Case btn_kinetica.Type
                                    Case ButtonReturnTypes.AutoReset
                                        btn_type = "AutoReset"
                                    Case ButtonReturnTypes.AutoReturn
                                        btn_type = "AutoReturn"
                                    Case ButtonReturnTypes.Normal
                                        btn_type = "Normal"
                                    Case ButtonReturnTypes.SlowAutoReset
                                        btn_type = "SlowAutoReset"
                                    Case ButtonReturnTypes.Sticky
                                        btn_type = "Sticky"
                                End Select


                                strReader.ReadLine()
                                .Value = fvName + ": " + "(" + strReader.ReadLine() + ")" + Environment.NewLine + _
                                         "Enabling: " + "(" + strReader.ReadLine() + ")" + Environment.NewLine + _
                                         "Resetting: " + "(" + strReader.ReadLine() + ")" + Environment.NewLine + _
                                         "Button Type: " + btn_type + Environment.NewLine + _
                                         "Initial state: " + initialValue + Environment.NewLine + _
                                         "Closing state: " + finalValue


                            ElseIf TypeOf ctrl.Control Is CEL.KineticaRT.Controls.Button Then
                                Dim btn_kinetica As CEL.KineticaRT.Controls.Button = DirectCast(ctrl.Control, CEL.KineticaRT.Controls.Button)

                                Select Case btn_kinetica.ReturnType
                                    Case ButtonReturnTypes.AutoReset
                                        btn_type = "AutoReset"
                                    Case ButtonReturnTypes.AutoReturn
                                        btn_type = "AutoReturn"
                                    Case ButtonReturnTypes.Normal
                                        btn_type = "Normal"
                                    Case ButtonReturnTypes.SlowAutoReset
                                        btn_type = "SlowAutoReset"
                                    Case ButtonReturnTypes.Sticky
                                        btn_type = "Sticky"
                                End Select

                                strReader.ReadLine()
                                .Value = fvName + ": " + "(" + strReader.ReadLine() + ")" + Environment.NewLine + _
                                         "Enabling: " + "(" + strReader.ReadLine() + ")" + Environment.NewLine + _
                                         "Resetting: " + "(" + strReader.ReadLine() + ")" + Environment.NewLine + _
                                         "Button Type: " + btn_type + Environment.NewLine + _
                                         "Initial state: " + initialValue + Environment.NewLine + _
                                         "Closing state: " + finalValue
                            ElseIf IsNumericChannels Then
                                .Value = LimitsText.ToString
                            Else
                                If fvName IsNot String.Empty Then
                                    .Value = fvName + ": "
                                End If
                            End If
                        End With

                        'TODO determine if we will autogen some text here if not we can remove the N/A text
                        With sheet.Range("C" + _currentRowIndex.ToString)
                            .Font.Size = 10
                            .WrapText = True
                            .Font.Underline = Excel.XlUnderlineStyle.xlUnderlineStyleNone
                            .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                            .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                            .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                            .Borders.Weight = Excel.XlBorderWeight.xlMedium
                            .Value = "N/A"
                        End With

                        'Fill the field with any channel name that contains the sourceName
                        With sheet.Range("D" + _currentRowIndex.ToString)
                            .Font.Size = 10
                            .WrapText = True
                            .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
                            .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                            .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                            .Borders.Weight = Excel.XlBorderWeight.xlMedium
                            .Value = HMIDatabaseAndExpr.ToString()
                        End With

                        'TODO determine if we will autogen some text here if not we can remove the N/A text
                        With sheet.Range("E" + _currentRowIndex.ToString)
                            .Font.Size = 10
                            .WrapText = True
                            .Font.Underline = Excel.XlUnderlineStyle.xlUnderlineStyleNone
                            .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                            .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                            .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                            .Borders.Weight = Excel.XlBorderWeight.xlMedium
                            .Value = "N/A"
                        End With

                        With sheet.Range("F" + _currentRowIndex.ToString)
                            .Font.Size = 10
                            .WrapText = True
                            .Font.Underline = Excel.XlUnderlineStyle.xlUnderlineStyleNone
                            .VerticalAlignment = Excel.XlVAlign.xlVAlignTop
                            .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                            .Borders.Weight = Excel.XlBorderWeight.xlMedium
                            .Value = String.Empty
                        End With

                    Catch ex As Exception
                        'Dim st As New StackTrace(ex, True)
                        'MessageBox.Show("Line: " & st.GetFrame(0).GetFileLineNumber.ToString)

                    Finally
                        _currentRowIndex += 1
                    End Try
                End If

                Me.ControlsGenerated += 1 'Will be used by SpecsGeneratorForm

            Next
        ElseIf controlList.IsPanel Then
            With sheet.Range("A" + _currentRowIndex.ToString + ":" + "F" + _currentRowIndex.ToString)
                .MergeCells = True
                .Interior.Color = System.Drawing.ColorTranslator.ToOle(Drawing.Color.Blue)
                .Font.Color = System.Drawing.ColorTranslator.ToOle(Drawing.Color.White)
                .Font.Size = 14
                .Font.Bold = True
                .Interior.Pattern = Excel.XlPattern.xlPatternSolid
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                .Borders.Weight = Excel.XlBorderWeight.xlMedium

                .Value = controlList.TitleText + " - " + controlList.Name.Substring(controlList.Name.IndexOf("\") + 1)
            End With

            _currentRowIndex += 1
        End If

        'If there is any container continue to search into containers 
        If controlList.HasContainers Then
            For Each ctrlList As ControlList In controlList.Containers
                BuildExcelSheet(sheet, ctrlList)
            Next
        End If

    End Sub
#End Region

    'This function try to get a property inside a object 
    Private Shared Function GetPropValue(obj As Object, propName As String) As Object
        Dim hmiCtrlType As Type = obj.GetType
        Dim prop_desc As Reflection.PropertyInfo = hmiCtrlType.GetProperty(propName)
        Dim prop As Object = Nothing

        Try
            If prop_desc IsNot Nothing Then
                prop = prop_desc.GetValue(obj, Nothing)
            End If
        Catch ex As Exception
            Return prop
        End Try

        Return prop

    End Function

    Public Property ControlsToGenerate() As Integer
        Get
            Return Me._qtyOfControlToGenerate
        End Get

        Private Set(value As Integer)
            Me._qtyOfControlToGenerate = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property ControlsSkipped As Integer
        Get
            Return Me._qtyOfControlSkipped
        End Get
        Private Set(value As Integer)
            Me._qtyOfControlSkipped = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property ControlsGenerated As Integer
        Get
            Return Me._qtyOfControlGenerated
        End Get
        Private Set(value As Integer)
            Me._qtyOfControlGenerated = value
            NotifyPropertyChanged()
        End Set
    End Property

#Region "Classes used only inside this class"
    Private Class ControlList
        Private _name As String = String.Empty

        Private _TitleText As String = String.Empty
        Private _IsContainValidSource As Boolean = False
        Private _Containers As New List(Of ControlList)    'All control's container like frame, tabs, and panels
        Private _ctrlList As New List(Of ControlAndType)    'All controls inside a container
        Private _IsPanel As Boolean
        Private _ParentName As String = String.Empty
        Private _Parent As ControlList


        Public Sub New(name As String, titleText As String, Optional isPanel As Boolean = False)
            _name = name
            _TitleText = titleText
            _IsPanel = isPanel
            _ctrlList.Clear()
            _Containers.Clear()
        End Sub

        Public Sub AddContainer(container As ControlList)
            _Containers.Add(container)

        End Sub

        Public Sub AddControl(ByRef ctrl As System.Windows.Forms.Control)
            Try
                Dim ctrlAndType As New ControlAndType(ctrl)

                ctrlAndType.ControlType = ctrl.GetType
                ctrlAndType.Name = DirectCast(GetPropValue(ctrl, "Name"), System.String)
                ctrlAndType.Text = DirectCast(GetPropValue(ctrl, "Text"), System.String)
                ctrlAndType.Description = DirectCast(GetPropValue(ctrl, "Description"), System.String)
                ctrlAndType.ResetSource = DirectCast(GetPropValue(ctrl, "ButtonResetStateSource"), System.String)
                ctrlAndType.InputEnableSource = DirectCast(GetPropValue(ctrl, "InputEnabledSource"), System.String)
                ctrlAndType.Source = DirectCast(GetPropValue(ctrl, "Source"), System.String)

                'CType cast to avoid NullReferenceException
                ctrlAndType.InputEnable = CType(GetPropValue(ctrl, "InputEnabled"), Boolean)
                ctrlAndType.Visible = CType(GetPropValue(ctrl, "Visible"), Boolean)
                ctrlAndType.Connected = CType(GetPropValue(ctrl, "Connected"), Boolean)


                'Use to know if the actual controls list contains at least one valid Control
                'It uses to build our container Title inside the Excel's worksheet
                If ctrlAndType.Source IsNot String.Empty And Me.IsContainValidSource = False Then
                    Me.IsContainValidSource = True
                End If

                'Add the control inside the control list
                Me.Add(ctrlAndType)

            Catch ex As Exception
                'MessageBox.Show(ex.Message + " " + ex.Source)
            End Try
        End Sub

        Private Sub Add(ByRef ctrl As ControlAndType)
            _ctrlList.Add(ctrl)
        End Sub

        Public ReadOnly Property HasContainers() As Boolean
            Get
                Return _Containers.IsValidIndex(0)
            End Get
        End Property

        Public ReadOnly Property Containers As List(Of ControlList)
            Get
                Return _Containers
            End Get
        End Property

        Public Property Parent As ControlList
            Get
                Return _Parent
            End Get
            Set(value As ControlList)
                _ParentName = value.Name
                _Parent = value
            End Set
        End Property

        Public Property Name As String
            Get
                Return _name
            End Get
            Set(value As String)
                _name = value
            End Set
        End Property

        Public ReadOnly Property IsPanel As Boolean
            Get
                Return Me._IsPanel
            End Get
        End Property

        Public Property TitleText As String
            Get
                Return _TitleText
            End Get
            Set(value As String)
                _TitleText = value
            End Set
        End Property

        Public ReadOnly Property HasControls As Boolean
            Get
                Return _ctrlList.IsValidIndex(0)
            End Get
        End Property

        Public ReadOnly Property Controls As List(Of ControlAndType)
            Get
                Return _ctrlList
            End Get
        End Property

        'Set if there is any valid control source inside a list
        Public Property IsContainValidSource As Boolean
            Get
                Return Me._IsContainValidSource
            End Get
            Set(value As Boolean)
                _IsContainValidSource = True
            End Set
        End Property

    End Class

    'It's a control inside a HMI
    Private Class ControlAndType
        Private _controlType As Type
        Private _control As System.Windows.Forms.Control

        Private _controlObjName As String = String.Empty
        Private _controlText As String = String.Empty
        Private _controlDescription As String = String.Empty
        Private _btnResetSource As String = String.Empty
        Private _inputEnableSource As String = String.Empty
        Private _source As String = String.Empty

        Private _inputEnable As Boolean = False
        Private _visible As Boolean = False
        Private _connected As Boolean = False

        Public Sub New(ctrl As System.Windows.Forms.Control)
            _control = ctrl
        End Sub

        Public Property ControlType As Type
            Get
                Return _controlType
            End Get

            Set(value As Type)
                _controlType = value
            End Set

        End Property

        Public Property Description As String
            Get
                Return _controlDescription
            End Get
            Set(value As String)
                If value Is Nothing Then
                    _controlDescription = String.Empty
                Else
                    _controlDescription = value
                End If
            End Set
        End Property

        Public Property Name As String
            Get
                Return _controlObjName
            End Get
            Set(value As String)
                If value Is Nothing Then
                    _controlObjName = String.Empty
                Else
                    _controlObjName = value
                End If
            End Set
        End Property

        Public Property Text As String
            Get
                Return _controlText
            End Get
            Set(value As String)
                If value Is Nothing Then
                    _controlText = String.Empty
                Else
                    _controlText = value
                End If
            End Set
        End Property

        Public Property InputEnable As Boolean
            Get
                Return _inputEnable
            End Get

            Set(value As Boolean)
                If value = Nothing Then
                    _inputEnable = False
                Else
                    _inputEnable = value
                End If
            End Set
        End Property

        Public Property ResetSource As String
            Get
                Return _btnResetSource
            End Get

            Set(value As String)
                If value Is Nothing Then
                    _btnResetSource = String.Empty
                Else
                    _btnResetSource = value
                End If
            End Set
        End Property

        Public Property InputEnableSource As String
            Get
                Return _inputEnableSource
            End Get

            Set(value As String)
                If value Is Nothing Then
                    _inputEnableSource = String.Empty
                Else
                    _inputEnableSource = value
                End If

            End Set
        End Property

        Public Property Source As String
            Get
                Return _source
            End Get
            Set(value As String)
                If value Is Nothing Then
                    _source = String.Empty
                Else
                    _source = value
                End If

            End Set
        End Property

        Public Property Visible As Boolean
            Get
                Return _visible
            End Get
            Set(value As Boolean)
                If value = Nothing Then
                    _visible = False
                Else
                    _visible = value
                End If

            End Set
        End Property

        Public Property Connected As Boolean
            Get
                Return _connected
            End Get
            Set(value As Boolean)
                If value = Nothing Then
                    _connected = False
                Else
                    _connected = value
                End If
            End Set
        End Property

        Public ReadOnly Property Control As System.Windows.Forms.Control
            Get
                Return _control
            End Get
        End Property

    End Class
#End Region

End Class
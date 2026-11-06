Option Strict On
Option Explicit On

Imports System.Collections.Generic
Imports System.Windows.Forms

Imports KineticaRT.Channels
Imports KineticaRT.Values
Imports System.Windows.Forms.DataFormats

Namespace CEL.TestProgress

    Public Class TestProgressMgr
        Implements System.IDisposable

        Public Structure TestProgress_DataInfo
            Public EngineStartEventChannelName As String
            Public EngineStartEventChannelValue As Boolean
            Public EngineStartEventDescription As String
            Public EngineStartEventRecordTime As Boolean
            Public EngineStopEventChannelName As String
            Public EngineStopEventChannelValue As Boolean
            Public EngineStopEventDescription As String
            Public EngineStopEventRecordTime As Boolean
            Public TestProgressFrameName As String
            Public TestProgressSheetFileName As String
            Public TestProgressTextFileName As String
            Public UUTModel As String
            Public UUTSerialNumber As String
            Public UUTWorkOrder As String
        End Structure

        '*
        '* Constructors
        '*
        Public Sub New(dataInfo As TestProgress_DataInfo, _
                       Optional testProgressSheet As CEL.TestProgress.TestProgressSheet = Nothing, _
                       Optional testProgressText As String = "")

            Dim dummy As Object = Nothing
            Dim TestProgressFrame As Global.KineticaRT.Controls.Control = Nothing

            m_dataInfo = dataInfo

            m_CELProject.InitializeObjectCallBack(dummy, dataInfo.TestProgressFrameName)
            TestProgressFrame = CType(dummy, Global.KineticaRT.Controls.Control)

            If TestProgressFrame Is Nothing Then
                Throw New System.ArgumentException("Control not found", "dataInfo.TestProgressFrameName")
            Else
                Dim cboTestProgressStep As System.Windows.Forms.ComboBox = _
                    CType(TestProgressFrame.FindObject("cboTestProgressStep"), System.Windows.Forms.ComboBox)
                Dim btnPass As Global.KineticaRT.Studio.Controls.Button = _
                    CType(TestProgressFrame.FindObject("btnPass"), Global.KineticaRT.Studio.Controls.Button)
                Dim btnNA As Global.KineticaRT.Studio.Controls.Button = _
                    CType(TestProgressFrame.FindObject("btnNA"), Global.KineticaRT.Studio.Controls.Button)
                Dim btnFail As Global.KineticaRT.Studio.Controls.Button = _
                    CType(TestProgressFrame.FindObject("btnFail"), Global.KineticaRT.Studio.Controls.Button)
                Dim dgvTestStepCheckList As CEL.KineticaRT.Controls.DataGridView = _
                    CType(TestProgressFrame.FindObject("dgvParameterCheckList"), CEL.KineticaRT.Controls.DataGridView)

                Dim testProgressStepList As System.Collections.Generic.ICollection(Of ITestStep) = GetStepList(dataInfo)

                If testProgressSheet Is Nothing Then
                    testProgressSheet = GetTestProgressSheet(dataInfo)
                End If

                If testProgressText = "" Then
                    testProgressText = GetTestProgressTextFile(dataInfo)
                End If

                If dataInfo.EngineStartEventChannelName IsNot Nothing Then
                    m_engineStartEventChannel = m_CELProject.GetChannelCallBack(dataInfo.EngineStartEventChannelName)
                    If m_engineStartEventChannel Is Nothing Then
                        Throw New System.ArgumentException("Channel not found", "dataInfo.EngineStartEventChannelName = " + _
                                                           dataInfo.EngineStartEventChannelName)
                    End If
                    m_engineStartEventChannelValue = dataInfo.EngineStartEventChannelValue
                End If

                If dataInfo.EngineStartEventDescription IsNot Nothing Then
                    m_engineStartEventDescription = dataInfo.EngineStartEventDescription
                    m_engineStartEventRecordTime = dataInfo.EngineStartEventRecordTime
                End If

                If dataInfo.EngineStopEventChannelName IsNot Nothing Then
                    m_engineStopEventChannel = m_CELProject.GetChannelCallBack(dataInfo.EngineStopEventChannelName)
                    If m_engineStartEventChannel Is Nothing Then
                        Throw New System.ArgumentException("Channel not found", "dataInfo.EngineStopEventChannelName = " + _
                                                           dataInfo.EngineStopEventChannelName)
                    End If
                    m_engineStopEventChannelValue = dataInfo.EngineStopEventChannelValue
                End If

                If dataInfo.EngineStopEventDescription IsNot Nothing Then
                    m_engineStopEventDescription = dataInfo.EngineStopEventDescription
                    m_engineStopEventRecordTime = dataInfo.EngineStopEventRecordTime
                End If

                Initialisation(testProgressStepList, cboTestProgressStep, btnPass, btnNA, btnFail, dgvTestStepCheckList, testProgressSheet, testProgressText)
            End If
        End Sub

        Public Sub New( _
                    testSteps As System.Collections.Generic.ICollection(Of ITestStep), _
                    cboTestProgressStep As System.Windows.Forms.ComboBox, _
                    btnPass As Global.KineticaRT.Studio.Controls.Button, _
                    btnNA As Global.KineticaRT.Studio.Controls.Button, _
                    btnFail As Global.KineticaRT.Studio.Controls.Button, _
                    dgvTestStepCheckList As CEL.KineticaRT.Controls.DataGridView, _
                    Optional testProgressSheet As CEL.TestProgress.TestProgressSheet = Nothing, _
                    Optional testProgressText As String = "" _
                )
            Initialisation(testSteps, cboTestProgressStep, btnPass, btnNA, btnFail, dgvTestStepCheckList, testProgressSheet, testProgressText)
        End Sub

        Private Function GetStepList(dataInfo As TestProgress_DataInfo) As System.Collections.Generic.ICollection(Of CEL.TestProgress.ITestStep)
            Dim testProgressStepList As System.Collections.Generic.ICollection(Of CEL.TestProgress.ITestStep)
            Using testProgressStepListFactory As New CEL.TestProgress.TestProgressStepListDBFactory(m_CELProject)
                If testProgressStepListFactory IsNot Nothing Then
                    testProgressStepList = testProgressStepListFactory.CreateEngineTestProgressStepList(dataInfo.UUTModel)
                Else
                    testProgressStepList = New System.Collections.Generic.List(Of CEL.TestProgress.ITestStep)()
                End If
            End Using
            Return testProgressStepList
        End Function

        Private Function GetTestProgressSheet(dataInfo As TestProgress_DataInfo) As CEL.TestProgress.TestProgressSheet

            Dim progressSheet As CEL.TestProgress.TestProgressSheet = Nothing
            If System.IO.File.Exists(dataInfo.TestProgressSheetFileName) Then
                Using str As New System.IO.StreamReader(dataInfo.TestProgressSheetFileName)
                    Dim ser As New System.Xml.Serialization.XmlSerializer(GetType(CEL.TestProgress.TestProgressSheet))
                    progressSheet = CType(ser.Deserialize(str), TestProgress.TestProgressSheet)
                End Using
            Else
                progressSheet = New CEL.TestProgress.TestProgressSheet(dataInfo.UUTModel, _
                                                                       dataInfo.UUTSerialNumber, _
                                                                       dataInfo.UUTWorkOrder)
            End If

            Return progressSheet
        End Function

        Private Function GetTestProgressTextFile(dataInfo As TestProgress_DataInfo) As String

            Dim textFileContent As String = ""
            If System.IO.File.Exists(dataInfo.TestProgressTextFileName) Then
                Dim txtReader As New System.IO.StreamReader(dataInfo.TestProgressTextFileName)
                textFileContent = txtReader.ReadToEnd()
                txtReader.Close()
            End If
            textFileContent += System.Environment.NewLine + "Engine model         : " + dataInfo.UUTModel + System.Environment.NewLine
            textFileContent += "Engine serial number : " + dataInfo.UUTSerialNumber + System.Environment.NewLine
            textFileContent += "Test Cell            : 002" + System.Environment.NewLine + System.Environment.NewLine

            Return textFileContent
        End Function
        Public ReadOnly Property TestProgressSheet As TestProgressSheet
            Get
                Return m_testProgressSheet
            End Get
        End Property

        Public Property TestProgressText As String
            Get
                Return m_testProgressText
            End Get
            Set(value As String)
                m_testProgressText = value
            End Set
        End Property

        Public Property DataInfo As TestProgress_DataInfo
            Get
                Return m_dataInfo
            End Get
            Set(value As TestProgress_DataInfo)
                m_dataInfo = value
            End Set
        End Property

        Delegate Sub AddTestProgressEntryCallback(entry As TestProgressSheetEntry)

        Public Sub AddTestProgressEntry(entry As TestProgressSheetEntry)
            If m_testProgressSheetView IsNot Nothing Then
                If m_testProgressSheetView.InvokeRequired Then
                    Dim d As New AddTestProgressEntryCallback(AddressOf AddTestProgressEntry)
                    m_testProgressSheetView.BeginInvoke(d, New Object() {entry})
                Else
                    Dim focusedDataRowIndex As Integer = m_testProgressSheetView.GetFocusedDataSourceRowIndex()
                    If focusedDataRowIndex <> DevExpress.XtraGrid.GridControl.InvalidRowHandle And _
                       focusedDataRowIndex <> DevExpress.XtraGrid.GridControl.NewItemRowHandle Then
                        TestProgressSheet.Entries.Insert(focusedDataRowIndex, entry)
                    Else
                        TestProgressSheet.Entries.Add(entry)
                    End If
                    m_testProgressSheetView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle
                End If
            End If

            If m_testProgressText IsNot Nothing Then
                m_testProgressText += System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss  ") + entry.Result + System.Environment.NewLine

                For i As Integer = 1 To entry.CheckItemResults.Count
                    m_testProgressText += "                              " + entry.CheckItemResults(i - 1) + System.Environment.NewLine
                Next
                m_testProgressText += System.Environment.NewLine
            End If
        End Sub

        Public Sub InitTestProgressSheet(testProgressSheet As CEL.TestProgress.TestProgressSheet)
            If Not testProgressSheet Is Nothing Then
                m_testProgressSheet = testProgressSheet
            Else
                m_testProgressSheet = New TestProgressSheet()
            End If
            m_testProgressSheetView = New TestProgressSheetView(m_testProgressSheet, Me)

        End Sub

        Private Sub Initialisation( _
                    testSteps As System.Collections.Generic.ICollection(Of ITestStep), _
                    cboTestProgressStep As System.Windows.Forms.ComboBox, _
                    btnPass As Global.KineticaRT.Studio.Controls.Button, _
                    btnNA As Global.KineticaRT.Studio.Controls.Button, _
                    btnFail As Global.KineticaRT.Studio.Controls.Button, _
                    dgvTestStepCheckList As CEL.KineticaRT.Controls.DataGridView, _
                    testProgressSheet As CEL.TestProgress.TestProgressSheet, _
                    testProgressText As String
                )

            InitTestProgressSheet(testProgressSheet)
            m_testProgressText = testProgressText
            m_testSteps.AddRange(testSteps)
            m_cboTestProgressStep = cboTestProgressStep
            m_btnPass = btnPass
            m_btnNA = btnNA
            m_btnFail = btnFail
            m_dgvTestStepCheckList = dgvTestStepCheckList
            m_dgvTestStepCheckList.AutoGenerateColumns = False
            m_dgvTestStepCheckList.DataSource = m_testStepCheckItemDataSource
            SetHandlers()
            SetControls()
            m_testProgressSheetView.Show()
            m_testProgressSheetView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle
        End Sub

        Private Sub NextStep()
            If m_cboTestProgressStep.SelectedIndex + 1 < m_testSteps.Count Then
                m_cboTestProgressStep.SelectedIndex = m_cboTestProgressStep.SelectedIndex + 1
            End If
        End Sub

        Private Sub OnEngineStartEventValueChanged(sender As IChannel, _
                                              newVal As Value)
            If ((m_engineStartEventChannelValue = True AndAlso m_CELProject.IsTrue(newVal)) Or _
                 (m_engineStartEventChannelValue = False AndAlso m_CELProject.IsFalse(newVal))) Then
                Dim time As String = String.Empty
                If m_engineStartEventRecordTime Then
                    time = System.DateTime.Now.ToString("HH:mm")
                End If
                Me.AddTestProgressEntry(New CEL.TestProgress.TestProgressSheetEntry(time, m_engineStartEventDescription))
            End If

        End Sub

        Private Sub OnEngineStopEventValueChanged(sender As IChannel, _
                                      newVal As Value)
            If ((m_engineStopEventChannelValue = True AndAlso m_CELProject.IsTrue(newVal)) Or _
                 (m_engineStopEventChannelValue = False AndAlso m_CELProject.IsFalse(newVal))) Then
                Dim time As String = String.Empty
                If m_engineStopEventRecordTime Then
                    time = System.DateTime.Now.ToString("HH:mm")
                End If
                Me.AddTestProgressEntry(New CEL.TestProgress.TestProgressSheetEntry(time, m_engineStopEventDescription))
            End If

        End Sub

        Private Sub OnFailValueChanged(sender As Object, newVal As Value)
            If newVal Is Values.True Then
                If TypeOf m_curTestStep Is IPassFail Then
                    Dim passFailStep As IPassFail

                    passFailStep = CType(m_curTestStep, IPassFail)
                    AddTestProgressEntry(passFailStep.Fail(m_testProgressSheet))
                    NextStep()
                End If
            End If
        End Sub

        Private Sub OnNAValueChanged(sender As Object, newVal As Value)
            If newVal Is Values.True Then
                If TypeOf m_curTestStep Is IExecutable Then
                    Dim executableStep As IExecutable

                    executableStep = CType(m_curTestStep, IExecutable)
                    AddTestProgressEntry(executableStep.Execute(m_testProgressSheet))
                    NextStep()
                End If
            End If
        End Sub

        Private Sub OnPassValueChanged(sender As Object, newVal As Value)
            If newVal Is Values.True Then
                If TypeOf m_curTestStep Is IPassFail Then
                    Dim passFailStep As IPassFail

                    passFailStep = CType(m_curTestStep, IPassFail)
                    AddTestProgressEntry(passFailStep.Pass(m_testProgressSheet))
                    NextStep()
                End If
            End If
        End Sub

        Private Sub OnTestStepSelectedIndexChanged(sender As Object, e As System.EventArgs)
            Dim bButtonsPassFailEnable As Boolean = False
            Dim bButtonNAEnable As Boolean = False

            If m_curTestStep IsNot Nothing Then
                m_curTestStep.Active = False
            End If
            If m_cboTestProgressStep.SelectedIndex >= m_testSteps.Count Then
                m_curTestStep = Nothing
            Else
                m_curTestStep = m_testSteps(m_cboTestProgressStep.SelectedIndex)
            End If

            If m_curTestStep IsNot Nothing Then
                m_curTestStep.Active = True
            End If

            If TypeOf m_curTestStep Is IPassFail Then
                bButtonsPassFailEnable = True
                bButtonNAEnable = False
            ElseIf TypeOf m_curTestStep Is IExecutable Then
                bButtonsPassFailEnable = False
                bButtonNAEnable = True
            Else
                bButtonsPassFailEnable = False
                bButtonNAEnable = False
            End If

            m_btnFail.InputEnabled = bButtonsPassFailEnable
            m_btnPass.InputEnabled = bButtonsPassFailEnable
            m_btnNA.InputEnabled = bButtonNAEnable

            m_dgvTestStepCheckList.SuspendLayout()
            m_testStepCheckItemDataSource.DataSource = m_curTestStep.CheckList
            Application.DoEvents()

            Dim row As Integer = 0

            For Each checkItem As CEL.TestProgress.ICheckListItem In m_curTestStep.CheckList
                Dim manualEntryCheckItem As CEL.TestProgress.ManualEntryCheckListItem = TryCast(checkItem, CEL.TestProgress.ManualEntryCheckListItem)
                m_dgvTestStepCheckList.Rows(row).Cells(3).ReadOnly = (manualEntryCheckItem Is Nothing)
                row += 1
            Next
            m_dgvTestStepCheckList.ResumeLayout()
        End Sub

        Private Sub RemoveHandlers()
            If m_cboTestProgressStep IsNot Nothing Then
                RemoveHandler m_cboTestProgressStep.SelectedIndexChanged, AddressOf OnTestStepSelectedIndexChanged
            End If
            If m_btnFail IsNot Nothing Then
                RemoveHandler m_btnFail.ValueChanged, AddressOf OnFailValueChanged
            End If
            If m_btnNA IsNot Nothing Then
                RemoveHandler m_btnNA.ValueChanged, AddressOf OnNAValueChanged
            End If
            If m_btnPass IsNot Nothing Then
                RemoveHandler m_btnPass.ValueChanged, AddressOf OnPassValueChanged
            End If
            If m_engineStartEventChannel IsNot Nothing Then
                RemoveHandler m_engineStartEventChannel.ValueChanged, AddressOf OnEngineStartEventValueChanged
            End If
            If m_engineStopEventChannel IsNot Nothing Then
                RemoveHandler m_engineStopEventChannel.ValueChanged, AddressOf OnEngineStopEventValueChanged
            End If
        End Sub


        Private Sub SetHandlers()
            AddHandler m_cboTestProgressStep.SelectedIndexChanged, AddressOf OnTestStepSelectedIndexChanged
            AddHandler m_btnFail.ValueChanged, AddressOf OnFailValueChanged
            AddHandler m_btnNA.ValueChanged, AddressOf OnNAValueChanged
            AddHandler m_btnPass.ValueChanged, AddressOf OnPassValueChanged
            If m_engineStartEventChannel IsNot Nothing Then
                AddHandler m_engineStartEventChannel.ValueChanged, AddressOf OnEngineStartEventValueChanged
            End If
            If m_engineStopEventChannel IsNot Nothing Then
                AddHandler m_engineStopEventChannel.ValueChanged, AddressOf OnEngineStopEventValueChanged
            End If
        End Sub

        Private Sub SetControls()
            If (m_cboTestProgressStep.InvokeRequired()) Then
                m_cboTestProgressStep.Invoke(New System.Windows.Forms.MethodInvoker(AddressOf SetControls))
            Else
                m_btnFail.InputEnabled = False
                m_btnPass.InputEnabled = False
                m_btnNA.InputEnabled = False
                m_dgvTestStepCheckList.DataSource = m_testStepCheckItemDataSource
                m_cboTestProgressStep.SuspendLayout()
                m_cboTestProgressStep.DataSource = m_testSteps
                m_cboTestProgressStep.DisplayMember = "Title"
                If m_testSteps.Count > 0 Then
                    m_cboTestProgressStep.SelectedIndex = 0
                Else
                    m_cboTestProgressStep.SelectedIndex = -1
                End If
                m_cboTestProgressStep.ResumeLayout()
            End If
        End Sub

        Public Sub AddFreeFormTextEntry(text As String)
            If m_disposed Then
                Throw New System.ObjectDisposedException(Nothing)
            Else
                Me.AddTestProgressEntry(New TestProgressSheetEntry(String.Empty, text))
            End If
        End Sub

        Public Sub Dispose() Implements System.IDisposable.Dispose
            Dispose(True)
            System.GC.SuppressFinalize(Me)
        End Sub

        Protected Sub Dispose(ByVal disposing As Boolean)
            If (Not m_disposed) Then
                If (disposing) Then
                    RemoveHandlers()
                    If m_testProgressSheetView IsNot Nothing Then
                        m_testProgressSheetView.Close()
                        m_testProgressSheetView.Dispose()
                    End If
                    If m_testProgressSheetDataSource IsNot Nothing Then
                        m_testProgressSheetDataSource.Dispose()
                    End If
                    If m_testStepCheckItemDataSource IsNot Nothing Then
                        m_testStepCheckItemDataSource.Dispose()
                    End If
                End If
                m_btnFail = Nothing
                m_btnNA = Nothing
                m_btnPass = Nothing
                m_cboTestProgressStep = Nothing
                m_curTestStep = Nothing
                m_dgvTestStepCheckList.DataSource = Nothing
                m_dgvTestStepCheckList = Nothing
                m_engineStartEventChannel = Nothing
                m_engineStopEventChannel = Nothing
                m_testProgressSheet = Nothing
                m_testProgressSheetDataSource = Nothing
                m_testProgressSheetView = Nothing
                m_testStepCheckItemDataSource = Nothing
                m_testSteps = Nothing
            End If
            m_disposed = True
        End Sub

        Private m_CELProject As CEL.HMI.CELDAS = CEL.HMI.CELDAS.Instance()
        Private m_disposed As Boolean = False
        Private m_testSteps As New List(Of ITestStep)
        Private m_cboTestProgressStep As System.Windows.Forms.ComboBox
        Private m_btnPass As Global.KineticaRT.Studio.Controls.Button
        Private m_btnNA As Global.KineticaRT.Studio.Controls.Button
        Private m_btnFail As Global.KineticaRT.Studio.Controls.Button
        Private m_dataInfo As TestProgress_DataInfo
        Private m_dgvTestStepCheckList As CEL.KineticaRT.Controls.DataGridView
        Private m_curTestStep As ITestStep
        Private m_engineStartEventChannel As IChannel
        Private m_engineStartEventChannelValue As Boolean = True
        Private m_engineStartEventDescription As String = "Engine Starting"
        Private m_engineStartEventRecordTime As Boolean = True
        Private m_engineStopEventChannel As IChannel
        Private m_engineStopEventChannelValue As Boolean = True
        Private m_engineStopEventDescription As String = "Engine Stopping"
        Private m_engineStopEventRecordTime As Boolean = True
        Private m_testProgressSheet As TestProgressSheet
        Private m_testProgressText As String
        Private m_testProgressSheetDataSource As New CEL.Net.BindingSourcePlus 'System.Windows.Forms.BindingSource
        Private m_testProgressSheetView As TestProgressSheetView
        Private m_testStepCheckItemDataSource As New CEL.Net.BindingSourcePlus 'System.Windows.Forms.BindingSource
    End Class

End Namespace


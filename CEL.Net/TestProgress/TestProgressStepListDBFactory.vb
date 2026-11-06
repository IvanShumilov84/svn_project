Option Strict On
Option Explicit On

Imports KineticaRT.Channels
Imports KineticaRT.Values

Namespace CEL.TestProgress

    Public NotInheritable Class TestProgressStepListDBFactory
        Implements ITestProgressStepListFactory, System.IDisposable

        Private m_checkTypes As New CEL.Data.BusinessObjects.TestProgressCheckTypeCollection
        Private m_disposed As Boolean = False
        Private m_testStepTypes As New CEL.Data.BusinessObjects.TestProgressStepTypeCollection
        Private m_CELProject As CEL.HMI.CELDAS

        Public Sub New(celProject As CEL.HMI.CELDAS)
            m_checkTypes.LoadAll()
            m_testStepTypes.LoadAll()
            m_CELProject = celProject
        End Sub

        Public Function CreateEngineTestProgressStepList(EngineModel As String) As System.Collections.Generic.ICollection(Of ITestStep) Implements ITestProgressStepListFactory.CreateEngineTestProgressStepList
            Dim testSteps As New System.Collections.Generic.List(Of ITestStep)
            Dim engineTestStepCollection As New CEL.Data.BusinessObjects.TestProgressTestStepCollection

            engineTestStepCollection.Query.Where(engineTestStepCollection.Query.EngineModel = EngineModel)
            engineTestStepCollection.Query.OrderBy("StepOrder", EntitySpaces.DynamicQuery.esOrderByDirection.Ascending)
            engineTestStepCollection.Query.Load()

            For Each testStep As CEL.Data.BusinessObjects.TestProgressTestStep In engineTestStepCollection
                Dim testStepCheckItems(testStep.TestProgressCheckListItemCollectionByTestStepID.Count - 1) _
                     As ICheckListItem

                Dim index As Integer = 0
                For Each testStepCheckItem As CEL.Data.BusinessObjects.TestProgressCheckListItem In _
                    testStep.TestProgressCheckListItemCollectionByTestStepID
                    testStepCheckItems(index) = CreateStepCheckItem(testStepCheckItem)
                    index += 1
                Next
                testSteps.Add(CreateTestStep(testStep, testStepCheckItems))
            Next

            Return testSteps
        End Function

        Private Function CreateStepCheckItem(testStepCheckItem As CEL.Data.BusinessObjects.TestProgressCheckListItem) As ICheckListItem
            Dim checkType As CEL.Data.BusinessObjects.TestProgressCheckType = Me.m_checkTypes.FindByPrimaryKey(testStepCheckItem.CheckTypeID.Value)
            Dim checkListItem As ICheckListItem

            Dim lowLimit As Double
            Dim highLimit As Double

            If testStepCheckItem.LowLimit.HasValue Then
                lowLimit = testStepCheckItem.LowLimit.Value
            Else
                lowLimit = Double.NegativeInfinity
            End If

            If testStepCheckItem.HighLimit.HasValue Then
                highLimit = testStepCheckItem.HighLimit.Value
            Else
                highLimit = Double.PositiveInfinity
            End If

            Dim krtChannel As IChannel = Nothing
            Select Case checkType.Description
                Case "InstantValue"
                    m_CELProject.InitializeChannelCallBack.Invoke(krtChannel, testStepCheckItem.ChannelName)
                    Dim krtNumericChannel As INumericChannel = CType(krtChannel, INumericChannel)
                    If Not (krtNumericChannel Is Nothing) Then
                        checkListItem = New InstantCheckListItem(testStepCheckItem.ParameterName, testStepCheckItem.Unit, lowLimit, highLimit, krtNumericChannel)
                    Else
                        System.Diagnostics.Trace.WriteLine("TestProgressStepListDBFactory.CreateStepCheckItem: Unknown channel " + testStepCheckItem.ChannelName + _
                                                           ". Using ManualEntry instead")
                        checkListItem = New ManualEntryCheckListItem(testStepCheckItem.ParameterName, testStepCheckItem.Unit, lowLimit, highLimit)
                    End If
                Case "ManualEntry"
                    checkListItem = New ManualEntryCheckListItem(testStepCheckItem.ParameterName, testStepCheckItem.Unit, lowLimit, highLimit)
                Case "MaximumValue"
                    m_CELProject.InitializeChannelCallBack.Invoke(krtChannel, testStepCheckItem.ChannelName)
                    Dim krtNumericChannel As INumericChannel = CType(krtChannel, INumericChannel)
                    If Not (krtNumericChannel Is Nothing) Then
                        checkListItem = New MaxiCheckListItem(testStepCheckItem.ParameterName, testStepCheckItem.Unit, lowLimit, highLimit, krtNumericChannel)
                    Else
                        System.Diagnostics.Trace.WriteLine("TestProgressStepListDBFactory.CreateStepCheckItem: Unknown channel " + testStepCheckItem.ChannelName + _
                                                           ". Using ManualEntry instead")
                        checkListItem = New ManualEntryCheckListItem(testStepCheckItem.ParameterName, testStepCheckItem.Unit, lowLimit, highLimit)
                    End If
                Case Else
                    System.Diagnostics.Trace.WriteLine("TestProgressStepListDBFactory.CreateStepCheckItem: Unknown Check Type " + checkType.Description + _
                                                       ". Using ManualEntry instead")
                    checkListItem = New ManualEntryCheckListItem(testStepCheckItem.ParameterName, testStepCheckItem.Unit, lowLimit, highLimit)
            End Select

            Return checkListItem
        End Function

        Private Function CreateTestStep(testProgressTestStep As CEL.Data.BusinessObjects.TestProgressTestStep, testStepCheckItems As ICheckListItem()) As ITestStep
            Dim testStepType As CEL.Data.BusinessObjects.TestProgressStepType
            Dim testStep As ITestStep

            testStepType = m_testStepTypes.FindByPrimaryKey(testProgressTestStep.StepTypeID.Value)
            Select Case testStepType.Description
                Case "Pass / Fail"
                    testStep = New TestProgressPassFailStep(testProgressTestStep.Title, testStepCheckItems)
                Case "N / A"
                    testStep = New TestProgressNAStep(testProgressTestStep.Title, testStepCheckItems)
                Case Else
                    System.Diagnostics.Trace.WriteLine("TestProgressStepListDBFactory.CreateTestStep: Unknown Step Type {0}. Using N / A instead", _
                        testStepType.Description)
                    testStep = New TestProgressNAStep(testProgressTestStep.Title, testStepCheckItems)
            End Select

            Return testStep
        End Function

        Public Sub Dispose() Implements System.IDisposable.Dispose
            Dispose(True)
            System.GC.SuppressFinalize(Me)
        End Sub

        Protected Sub Dispose(ByVal disposing As Boolean)
            If (Not m_disposed) Then
                If (disposing) Then
                    m_checkTypes.Dispose()
                    m_testStepTypes.Dispose()
                End If
            End If
            m_disposed = True
        End Sub
    End Class
End Namespace


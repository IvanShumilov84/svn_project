Imports System.ServiceModel

Namespace PwcSharedInterface

    <ServiceContract>
    Public Interface IPwc83032

        <OperationContract> _
        Sub CalOn()

        <OperationContract> _
        Sub CloseOutputData()

        <OperationContract> _
        Sub GetAllResults(cHeaders() As String, ByRef PlotVal() As Double, ByRef nVals As Integer)

        <OperationContract> _
        Sub GetBuild(ByRef buildNumber As Integer)

        <OperationContract> _
        Sub GetBuildSpec(ByRef buildSpec As Integer)

        <OperationContract> _
        Sub GetCondition(ByRef iCondition As Integer)

        <OperationContract> _
        Sub GetData()

        <OperationContract> _
        Sub GetEngine(ByRef engineSerialNumber As String)

        <OperationContract> _
        Sub GetEngineModel(ByRef model As String)

        <OperationContract> _
        Function GetNumberT8Probes(engineModel As String) As Integer

        <OperationContract> _
        Sub GetResult(label As String, ByRef value As Single)

        <OperationContract> _
        Sub GetUnitData(ByRef errorNo As Integer)

        <OperationContract> _
        Function GetValue(label As String, ByRef value As String, index As String) As Boolean

        <OperationContract> _
        Sub Initialize()

        <OperationContract> _
        Sub LoadBuildSpec(ByRef errorNo As Integer, buildspec As String, SB1 As String, SB2 As String)

        <OperationContract> _
        Sub OutputData()

        <OperationContract> _
        Sub Process(ByRef errorNo As Integer, save As Integer, output As Integer)

        <OperationContract> _
        Sub ProcessData(ByRef iError As Integer)

        <OperationContract> _
        Function PutUnitValue(label As String, value As String) As Boolean

        <OperationContract> _
        Function PutValue(label As String, value As String, index As String) As Boolean

        <OperationContract> _
        Sub SaveData(ByRef errorNo As Integer)

        <OperationContract> _
        Sub SaveUnitData(ByRef errorNo As Integer)

        <OperationContract> _
        Sub SetBuild(buildNo As Integer)

        <OperationContract> _
        Sub SetCal(calNo As Integer)

        <OperationContract> _
        Sub SetCondition(conditionNo As Integer)

        <OperationContract> _
        Sub SetConfigurationPath(path As String)

        <OperationContract> _
        Sub SetDataPath(path As String)

        <OperationContract> _
        Sub SetEngine(engineSerialNo As String)

        <OperationContract> _
        Sub SetStatus(Status As String)

        <OperationContract> _
        Sub TransferData(ByRef iError As Integer)

    End Interface

End Namespace

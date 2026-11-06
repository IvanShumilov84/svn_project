Imports System.ServiceModel

Namespace PwcSharedInterface

    <ServiceContract>
    Public Interface IPwc83025

        <OperationContract> _
        Sub CreateVaneArea(ByRef iVane As Integer, ByRef iRes() As Integer, ByRef vArea As Single)

        <OperationContract> _
        Sub Define()

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

        '<OperationContract> _
        'Function GetNumberT8Probes(engineModel As String) As Integer

        <OperationContract> _
        Sub GetR3R4(ByRef iRes() As Integer)

        <OperationContract> _
        Sub GetResult(label As String, ByRef value As Single)

        <OperationContract> _
        Sub GetSummary(ByRef Summary As Boolean)

        <OperationContract> _
        Sub GetUnitData()

        <OperationContract> _
        Function GetRealValue(label As String, ByRef value As String, index As String) As Boolean

        <OperationContract> _
        Sub GetStockNumber(ByRef jIndx As Integer, ByRef CStock As String)

        <OperationContract> _
        Sub GetTorqueTrimError(ByRef iError() As Integer)

        <OperationContract> _
        Sub GetTorqueTrimStatus(ByRef iStatus() As Integer)

        <OperationContract> _
        Function GetValue(label As String, ByRef value As String, index As String) As Boolean

        <OperationContract> _
        Sub GetVaneArea(ByRef iVane As Integer, ByRef vArea As Single)

        <OperationContract> _
        Sub GetVaneRange(ByRef IVaneRange As Integer, ByRef rRange() As Integer)

        <OperationContract> _
        Sub Initialize()

        <OperationContract> _
        Sub LoadModel(ByRef errorNo As Integer, buildSpec As Integer, vanecode As Integer)

        <OperationContract> _
        Sub Process(ByRef errorNo As Integer, save As Integer, output As Integer)

        <OperationContract> _
        Sub ProcessTorqueTrim(output As Boolean, iTMTolDouble As Integer, rDynoTmTO As Single, rDynoTm50PC As Single, rEECTO As Single, rEEC50PC As Single, rResGain As Single, rResBias As Single, _
              iErr As Integer)

        <OperationContract> _
        Function PutUnitValue(label As String, unit As String, index As String) As Boolean

        <OperationContract> _
        Function PutValue(label As String, value As String, index As String) As Boolean

        <OperationContract> _
        Sub SaveData(ByRef errorNo As Integer)

        <OperationContract> _
        Sub SaveUnitData()

        <OperationContract> _
        Sub SetBuild(buildNo As Integer)

        <OperationContract> _
        Sub SetCondition(conditionNo As Integer)

        <OperationContract> _
        Sub SetConfigurationPath(path As String)

        <OperationContract> _
        Sub SetDataPath(path As String)

        <OperationContract> _
        Sub SetEngine(engineSerialNo As String)

        <OperationContract> _
        Sub SetSummary(summary As Boolean)

    End Interface

End Namespace

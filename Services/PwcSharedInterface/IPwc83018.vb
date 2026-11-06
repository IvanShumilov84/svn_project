Imports System.ServiceModel

Namespace PwcSharedInterface

    <ServiceContract>
    Public Interface IPwc83018

        <OperationContract> _
        Sub GetBuild(ByRef buildNumber As Integer)

        <OperationContract> _
        Sub GetCondition(ByRef iCondition As Integer)

        <OperationContract> _
        Sub GetData()

        <OperationContract> _
        Sub GetEngine(ByRef engineSerialNumber As String)

        <OperationContract> _
        Sub GetEngineModel(ByRef model As String)

        <OperationContract> _
        Sub GetResult(label As String, ByRef value As Double)

        <OperationContract> _
        Sub GetSummary(ByRef Summary As Boolean)

        <OperationContract> _
        Sub GetUnitData()

        <OperationContract> _
        Function GetRealValue(label As String, ByRef value As String, index As String) As Boolean

        <OperationContract> _
        Function GetValue(label As String, ByRef value As String, index As String) As Boolean

        <OperationContract> _
        Sub LoadModel(ByRef errorNo As Integer, Model As String, Optional ByVal SB1 As String = "        ", Optional ByVal SB2 As String = "        ")

        <OperationContract> _
        Sub NextCond(ByRef errorNo As Integer)

        <OperationContract> _
        Sub Process(ByRef errorNo As Integer, save As Integer, output As Integer)

        <OperationContract> _
        Sub ProcessTorqueTrim(ByRef errorNo As Integer, Dyno As Single(), Torque As Single(), Torque2 As Single(), output As Integer)

        <OperationContract> _
        Function PutUnitValue(label As String, unit As String, index As String) As Boolean

        <OperationContract> _
        Function PutValue(label As String, value As String, index As String) As Boolean

        <OperationContract> _
        Sub ResetCommon()

        <OperationContract> _
        Sub ResetM()

        <OperationContract> _
        Sub ResetTorque()

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

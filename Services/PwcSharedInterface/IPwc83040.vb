Imports System.ServiceModel

Namespace PwcSharedInterface

    <ServiceContract>
    Public Interface IPwc83040

        <OperationContract> _
        Sub CalOff()

        <OperationContract> _
        Sub CalOn()

        <OperationContract> _
        Sub ChooseCal(calNo As Integer)

        <OperationContract> _
        Sub CloseOutputData()

        <OperationContract> _
        Sub ConditionOff(ByRef errorNo As Integer, ByVal Condition As Integer)

        <OperationContract> _
        Sub ConditionOn(ByRef errorNo As Integer, ByVal Condition As Integer)

        <OperationContract> _
        Sub ErrorString(ByVal errorNo As Integer, ByRef String1 As String, ByRef String2 As String)

        <OperationContract> _
        Sub FinalAcceptanceCalcs(ByRef errorNo As Integer, ByVal save As Integer, ByVal output As Integer)

        <OperationContract> _
        Sub GetAllResults(ByRef Headers() As String, ByRef Values() As Double, ByRef Num As Integer)

        <OperationContract> _
        Sub GetBSList(ByRef errorNo As Integer, ByRef BSList() As String, ByRef Num As Integer)

        <OperationContract> _
        Sub GetBuild(ByRef buildNumber As Integer)

        <OperationContract> _
        Sub GetBuildSpec(ByRef buildSpecNumber As Integer)

        <OperationContract> _
        Sub GetCal(ByRef CalNumber As Integer)

        <OperationContract> _
        Sub GetCalList(ByRef CalList() As Integer, ByRef NumberOfCalibrations As Integer)

        <OperationContract> _
        Sub GetCalStat(ByRef CStatus As String)

        <OperationContract> _
        Sub GetCalType(ByRef errorNo As Integer, ByRef CalType As String)

        <OperationContract> _
        Sub GetCommonFile(ByRef errorNo As Integer)

        <OperationContract> _
        Sub GetCondition(ByRef iCondition As Integer)

        <OperationContract> _
        Sub GetConditionList(ByVal ICalNo As Integer, ByRef lc_Cond As Integer(), ByRef lc_Desc As String, ByRef le_Desc As Integer, ByRef lc_Stat As Integer, ByRef le_stat As Integer, ByRef nCond As Integer)

        <OperationContract> _
        Sub GetData(ByVal errorNo As Integer)

        <OperationContract> _
        Sub GetEngine(ByRef engineSerialNumber As String)

        <OperationContract> _
        Sub GetEngineModel(ByRef model As String)

        <OperationContract> _
        Sub GetFAResult(ByVal label As String, ByRef Result As Double)

        <OperationContract> _
        Sub GetLastCal(ByRef CalNumber As Integer)

        <OperationContract> _
        Sub GetLastCondition(ByRef ConditionNumber As Integer)

        <OperationContract> _
        Sub GetResult(ByVal label As String, ByRef value As Double)

        <OperationContract> _
        Sub GetSB(ByRef errorNo As Integer, ByRef BuildSpec As String, ByRef NumSB As Integer, ByRef SBList As String(), ByRef EngineUP As String)

        <OperationContract> _
        Sub GetUnitData(ByRef errorNo As Integer)

        <OperationContract> _
        Function GetValue(ByVal label As String, ByVal Index As String, ByRef value As String) As Boolean

        <OperationContract> _
        Sub GetVersion(ByRef version As String)

        <OperationContract> _
        Sub Initialize()

        <OperationContract> _
        Sub LoadBuildSpec(ByRef errorNo As Integer, ByVal BuildSpec As String, ByVal SB1 As String, ByVal SB2 As String)

        <OperationContract> _
        Sub LoadConditionStat(ByRef errorNo As Integer)

        <OperationContract> _
        Sub NextCond(ByRef errorNo As Integer)

        <OperationContract> _
        Sub OutputData()

        <OperationContract> _
        Sub PFileFirst()

        <OperationContract> _
        Sub PrevCondition(ByRef errorNo As Integer)

        <OperationContract> _
        Sub Process(ByRef errorNo As Integer, ByVal save As Integer, ByVal output As Integer)

        <OperationContract> _
        Sub ProcessData(ByRef errorNo As Integer)

        <OperationContract> _
        Function PutUnitValue(ByVal label As String, ByVal unit As String) As Boolean

        <OperationContract> _
        Function PutValue(ByVal label As String, ByVal value As String, ByVal index As String) As Boolean

        <OperationContract> _
        Sub ReadPFile(ByRef errorNo As Integer)

        <OperationContract> _
        Sub ReadSNFFile(ByRef errorNo As Integer)

        <OperationContract> _
        Sub SaveConditionStat(ByRef errorNo As Integer)

        <OperationContract> _
        Sub SaveData(ByRef errorNo As Integer)

        <OperationContract> _
        Sub SaveUnitData(ByRef errorNo As Integer)

        <OperationContract> _
        Sub SetBuild(ByRef buildNo As Integer)

        <OperationContract> _
        Sub SetCal(ByVal CalNo As Integer)

        <OperationContract> _
        Sub SetCondition(ByVal conditionNo As Integer)

        <OperationContract> _
        Sub SetConfigurationPath(ByVal path As String)

        <OperationContract> _
        Sub SetDataPath(ByVal path As String)

        <OperationContract> _
        Sub SetDateTime()

        <OperationContract> _
        Sub SetEngine(ByVal engine As String)

        <OperationContract> _
        Sub SetNewCal(ByVal CalNo As Integer)

        <OperationContract> _
        Sub SetNewCondition(ByVal conditionNo As Integer)

        <OperationContract> _
        Sub SetStatus(ByVal engine As String)

        <OperationContract> _
        Sub SNFFirst()

        <OperationContract> _
        Sub ToggleCalStat()

        <OperationContract> _
        Sub TransferData(ByRef errorNo As Integer)

    End Interface

End Namespace

Imports System.ServiceModel

Namespace PwcSharedInterface

    '********************************************************************
    '********************************************************************
    '* PW83020 integrates PT6 Family ************************************
    '********************************************************************
    '********************************************************************

    <ServiceContract>
    Public Interface IPwc83020

        '\-=-= Initialize =-=-/

        <OperationContract> _
        Sub Initialize()

        '\-=-= Path =-=-/

        <OperationContract> _
        Sub SetDataPath(ByVal cPath As String)

        <OperationContract> _
        Sub SetConfigurationPath(ByVal cPath As String)

        '\-=-= Units =-=-/

        <OperationContract> _
        Sub GetUnitData()

        <OperationContract> _
        Function PutUnitValue(ByVal label As String, ByVal unit As String, ByVal index As String) As Boolean

        <OperationContract> _
        Sub SaveUnitData()

        '\-=-= Engine Configuration =-=-/

        <OperationContract> _
        Sub SetEngine(ByVal engineSerialNo As String)

        <OperationContract> _
        Sub GetEngine(ByRef engineSerialNumber As String)

        <OperationContract> _
        Sub SetBuild(ByRef iBuild As Integer)

        <OperationContract> _
        Sub GetBuild(ByRef buildNumber As Integer)

        <OperationContract> _
        Sub SetCondition(ByRef conditionNo As Integer)
        
        <OperationContract> _
        Sub GetCondition(ByRef conditionNo As Integer)
        
        <OperationContract> _
        Sub NextCond(ByRef errorNo As Integer)

        <OperationContract> _
        Sub LoadModel(ByRef errorNo As Integer, ByVal Model As String, Optional ByVal SB1 As String = "        ", Optional ByVal SB2 As String = "        ")
        
        <OperationContract> _
        Sub GetEngineModel(ByRef model As String)
        
        '\-=-= Data =-=-/

        <OperationContract> _
        Function PutValue(ByVal label As String, ByVal value As String, ByVal index As String) As Boolean

        <OperationContract>
        Sub GetRatingData(ByRef NumConds As Integer,
                          ByRef NumArr As Integer,
                          ByVal iConds() As Integer,
                          ByVal rSHPDyno() As Single,
                          ByVal rSHPTm() As Single,
                          ByVal rT5A() As Single,
                          ByVal iTempTrimCode() As Integer,
                          ByRef irc As Integer)

        '\-=-= Process =-=-/

        <OperationContract> _
        Sub Process(ByRef errorNo As Integer, ByRef save As Integer, ByRef output As Integer)

        <OperationContract> _
        Sub ProcessBovTest(ByRef iOutput As Integer, ByVal BovBaro() As Single, ByVal BovPplenum() As Single, _
                           ByVal BovP1000() As Single, ByVal BovT1() As Single, ByVal BovNg() As Single, _
                           ByRef BovClosingNg As Single, ByRef iError As Integer)

        <OperationContract> _
        Sub ProcessTorqueTrim(ByRef Output As Integer, ByVal DynoTorque() As Single, ByVal EngineTorque() As Single, ByRef MinPts As Integer, _
                              ByRef DcuTrimGain As Single, ByRef DcuTrimBias As Single, ByRef iError As Integer)

        <OperationContract> _
        Sub RatingAnalysis(ByRef Output As Integer, ByRef NumUsed As Integer, ByVal IcondUsed() As Integer, ByRef iError As Integer)
        

        '\-=-= Not Used =-=-/ 

        <OperationContract> _
        Sub GetData()

        <OperationContract> _
        Sub GetResult(ByVal label As String, ByRef value As Single)

        <OperationContract> _
        Sub GetSummary(ByRef summary As Boolean)

        <OperationContract> _
        Function GetRealValue(ByVal label As String, ByRef value As String, ByVal index As String) As Boolean

        <OperationContract> _
        Function GetValue(ByVal label As String, ByRef value As String, ByVal index As String) As Boolean

        <OperationContract> _
        Sub SaveData(ByRef errorNo As Integer)
        
        <OperationContract> _
        Sub SetSummary(ByVal summary As Boolean)

    End Interface

End Namespace

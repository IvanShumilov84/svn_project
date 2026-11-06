Imports System.ServiceModel

Namespace PwcSharedInterface

    '********************************************************************
    '* PW83000 integrates PT6 Family ************************************
    '********************************************************************

    <ServiceContract>
    Public Interface IPwc83000

        '\-=-= Define =-=-/
        <OperationContract> Sub Define()

        '\-=-= Initialize =-=-/
        <OperationContract> Sub Initialize()

        '\-=-= Path =-=-/
        <OperationContract> Sub SetDataPath(ByVal cPath As String)

        <OperationContract> Sub SetConfigurationPath(ByVal cPath As String)

        '\-=-= Units =-=-/

        <OperationContract> Sub GetUnitData()

        <OperationContract> Function PutUnitValue(ByVal label As String, ByVal unit As String, ByVal index As String) As Boolean

        <OperationContract> Sub SaveUnitData()

        '\-=-= Engine Configuration =-=-/

        <OperationContract> Sub SetEngine(ByVal engineSerialNo As String)

        <OperationContract> Sub GetEngine(ByRef engineSerialNumber As String)

        <OperationContract> Sub SetBuild(ByRef iBuild As Integer)

        <OperationContract> Sub GetBuild(ByRef buildNumber As Integer)

        <OperationContract> Sub SetCondition(ByRef conditionNo As Integer)

        <OperationContract> Sub GetCondition(ByRef conditionNo As Integer)

        <OperationContract> Sub NextCond(ByRef errorNo As Integer)

        <OperationContract> Sub LoadModel(ByRef errorNo As Integer, ByRef build As Integer)

        <OperationContract> Sub GetEngineModel(ByRef model As String)

        <OperationContract> Sub SetITTTrim(ByRef iValue As Integer)

        '\-=-= Data =-=-/

        <OperationContract> Function PutValue(ByVal label As String, ByVal value As String, ByVal index As String) As Boolean

        <OperationContract> Sub GetR1(ByRef rR1 As Double)

        <OperationContract> Sub GetRatingData(ByRef numConds As Integer, ByRef numArr As Integer, ByRef iConds As Integer, ByRef rSHPDyno As Single, _
                                              ByRef rSHPTm As Single, ByRef rT5A As Single, ByRef iTempTrimCode As Integer, ByRef irc As Integer)

        <OperationContract> Sub GetTorqueSystems(ByRef iTMSystemsUsed() As Boolean)

        '\-=-= Process =-=-/

        <OperationContract> Sub Process(ByRef errorNo As Integer, ByRef save As Integer, ByRef output As Integer)

        <OperationContract> Sub ProcessTorqueTrim( _
            ByRef output        As Boolean,     ByRef iTMTolDouble      As Integer, _
            ByRef rDynoTmTO     As Single,      ByRef rDynoTm50PC       As Single, _
            ByRef rDynoEng1TO   As Single,      ByRef rDynoEng150PC     As Single, _
            ByRef rDynoEng2TO   As Single,      ByRef rDynoEng250PC     As Single, _
            ByRef rGainCl       As Single,      ByRef rBiasCl           As Single, _
            ByRef rResGain      As Single,      ByRef rResBias          As Single, _
            ByRef iErr          As Integer)
            '(ByRef output As Integer, ByVal dynoTorque() As Single, ByVal engineTorque() As Single, ByRef minPts As Integer, _
        '                  ByRef dcuTrimGain As Single, ByRef dcuTrimBias As Single, ByRef iError As Integer)

        <OperationContract> Sub GetTorqueTrimError(ByRef iSystem As Integer, ByRef iError() As Integer)

        <OperationContract> Sub GetTorqueLabel(ByRef iSystem As Integer, ByRef cTMLabel As String)

        <OperationContract> Sub RatingAnalysis(ByRef output As Integer, ByRef numUsed As Integer, ByVal icondUsed() As Integer, ByRef iError As Integer)

        '\-=-= Not Used =-=-/

        <OperationContract> Sub GetData()

        <OperationContract> Sub GetResult(ByVal label As String, ByRef value As Single)

        <OperationContract> Sub GetSummary(ByRef summary As Integer)

        <OperationContract> Function GetRealValue(ByVal label As String, ByRef value As String, ByVal index As String) As Boolean

        <OperationContract> Function GetValue(ByVal label As String, ByRef value As String, ByVal index As String) As Boolean

        <OperationContract> Sub SaveData(ByRef errorNo As Integer)

        <OperationContract> Sub SetSummary(ByVal summary As Integer)

    End Interface

End Namespace
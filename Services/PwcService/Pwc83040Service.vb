Imports PwcSharedInterface
Imports System.Text
Imports System.Runtime.InteropServices

Namespace PwcService

    '********************************************************************
    '********************************************************************
    '* PW83040 integrates PWC PW210 Family ******************************
    '********************************************************************
    '********************************************************************

    Public NotInheritable Class Pwc83040Service
        Implements IPwc83040

        '********************************************************************
        '* Private Declare **************************************************
        '********************************************************************

#Region "Declare"

        Private Declare Ansi Sub PWC83040CALOFF _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" ()

        Private Declare Ansi Sub PWC83040CALON _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" ()

        Private Declare Ansi Sub PWC83040CHOOSECAL _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iCalNo As Integer)

        Private Declare Ansi Sub PWC83040CLOSEOUTPUTDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" ()

        Private Declare Ansi Sub PWC83040CONDITIONOFF _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iError As Integer, ByRef iCondition As Integer)

        Private Declare Ansi Sub PWC83040CONDITIONON _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iError As Integer, ByRef iCondition As Integer)

        Private Declare Ansi Sub PWC83040ERRORSTRING _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iError_num As Integer, ByRef Str1 As String, ByVal Str1Size As Integer, ByRef Str2 As String, ByVal Str2Size As Integer)

        Private Declare Ansi Sub PWC83040FACALCS _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iError As Integer, ByRef iSave As Integer, ByRef iOutput As Integer)

        Private Declare Ansi Sub PWC83040GETALLRESULTS _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef Headers As String(), ByRef Values() As Double, ByRef Num As Integer)

        Private Declare Ansi Sub PWC83040GETBSLIST _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iError As Integer, ByRef BSList As String(), ByRef Num As Integer)

        Private Declare Ansi Sub PWC83040GETBUILD _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iBuild As Integer)

        Private Declare Ansi Sub PWC83040GETBUILDSPEC _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iBuildSpec As Integer)

        Private Declare Ansi Sub PWC83040GETCAL _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iCal As Integer)

        Private Declare Ansi Sub PWC83040GETCALLIST _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef CalList() As Integer, ByRef nCal As Integer)

        Private Declare Ansi Sub PWC83040GETCALSTAT _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef CStatus As String, ByVal CStatusSize As Integer)

        Private Declare Ansi Sub PWC83040GETCALTYPE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iError As Integer, ByRef CalType As String, ByVal CalTypeSize As Integer)

        Private Declare Ansi Sub PWC83040GETCOMMONFILE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iError As Integer)

        Private Declare Ansi Sub PWC83040GETCONDITION _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iCondition As Integer)

        Private Declare Ansi Sub PWC83040GETCONDITIONLIST _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByVal ICalNo As Integer, ByRef lc_Cond As Integer(), ByRef lc_Desc As String, ByVal lc_DescSize As Integer, ByRef le_Desc As Integer, ByRef lc_Stat As String, ByVal lc_StatSize As Integer, ByRef le_Stat As Integer, ByRef nCond As Integer)

        Private Declare Ansi Sub PWC83040GETDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iRet As Integer)

        Private Declare Ansi Sub PWC83040GETENGINE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef cEngine As String, ByVal cEngineSize As Integer)

        Private Declare Ansi Sub PWC83040GETENGINEMODEL _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef cModel As String, ByVal cEngineSize As Integer)

        Private Declare Ansi Sub PWC83040GETFARESULT _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByVal cLabel As String, ByVal cLabelSize As Integer, ByRef rResult As Double)

        Private Declare Ansi Sub PWC83040GETLASTCAL _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iLCal As Integer)

        Private Declare Ansi Sub PWC83040GETLASTCONDITION _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iLCondition As Integer)

        Private Declare Ansi Sub PWC83040GETRESULT _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, ByRef rResult As Double)

        Private Declare Ansi Sub PWC83040GETSB _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iError As Integer, ByRef cBuildSpec As String, ByVal cBuildSpecSize As Integer, ByRef NumSB As Integer, ByRef SBlist As String(), ByRef EngineUp As String, ByRef EngineUpSize As Integer)

        Private Declare Ansi Sub PWC83040GETUNITDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iError As Integer)

        Private Declare Ansi Function PWC83040GETVALUE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, _
             ByVal cIndex As String, ByVal cIndexLen As Integer, _
             ByRef cValue As String, ByRef cValueLen As Integer) As Boolean

        Private Declare Ansi Sub PWC83040GETVERSION _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef cVersion As String, ByVal cVersionSize As Integer)

        Private Declare Ansi Sub PWC83040INITIALIZE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" ()

        Private Declare Ansi Sub PWC83040LOADBUILDSPEC _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iError As Integer, ByVal cBuildSpec As String, ByVal cBuildSpecSize As Integer, ByVal SB1 As String, ByVal SB1Size As Integer, ByVal SB2 As String, ByVal SB2Size As Integer)

        Private Declare Ansi Sub PWC83040LOADCONDITIONSTAT _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iError As Integer)

        Private Declare Ansi Sub PWC83040NEXTCOND _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iError As Integer)

        Private Declare Ansi Sub PWC83040OUTPUTDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" ()

        Private Declare Ansi Sub PWC83040PFILEFIRST _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" ()

        Private Declare Ansi Sub PWC83040PREVCONDITION _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iError As Integer)

        Private Declare Ansi Sub PWC83040PROCESS _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iError As Integer, ByVal iSave As Integer, ByVal iOutput As Integer)

        Private Declare Ansi Sub PWC83040PROCESSDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iRet As Integer)

        Private Declare Ansi Function PWC83040PUTUNITVALUE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByVal cLabel As String, ByVal cLabelSize As Integer, ByVal cValue As String, ByVal cValueSize As Integer) As Boolean

        Private Declare Ansi Function PWC83040PUTVALUE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByVal cLabel As String, ByVal cLabelSize As Integer, ByVal cValue As String, ByVal cValueSize As Integer, ByVal cIndex As String, ByVal cIndexSize As Integer) As Boolean

        Private Declare Ansi Sub PWC83040READPFILE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iRet As Integer)

        Private Declare Ansi Sub PWC83040READSNFFILE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iRet As Integer)

        Private Declare Ansi Sub PWC83040SAVECONDITIONSTAT _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iRet As Integer)

        Private Declare Ansi Sub PWC83040SAVEDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iRet As Integer)

        Private Declare Ansi Sub PWC83040SAVEUNITDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iError As Integer)

        Private Declare Ansi Sub PWC83040SETBUILD _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iBuild As Integer)

        Private Declare Ansi Sub PWC83040SETCAL _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iCal As Integer)

        Private Declare Ansi Sub PWC83040SETCONDITION _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iCondition As Integer)

        Private Declare Ansi Sub PWC83040SETCONFIGURATIONPATH _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByVal cPath As String, ByVal cPathLen As Integer)

        Private Declare Ansi Sub PWC83040SETDATAPATH _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByVal cPath As String, ByVal cPathLen As Integer)

        Private Declare Ansi Sub PWC83040SETDATETIME _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" ()

        Private Declare Ansi Sub PWC83040SETENGINE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByVal cEngine As String, ByVal cEngineSize As Integer)

        Private Declare Ansi Sub PWC83040SETNEWCAL _
         Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iCal As Integer)

        Private Declare Ansi Sub PWC83040SETNEWCONDITION _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iCondition As Integer)

        Private Declare Ansi Sub PWC83040SETSTATUS _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByVal cStatus As String, ByVal cStatusSize As Integer)

        Private Declare Ansi Sub PWC83040SNFFirst _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" ()

        Private Declare Ansi Sub PWC83040TOGGLECALSTAT _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" ()

        Private Declare Ansi Sub PWC83040TRANSFERDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83040\exe\PWC83040DLL.dll" _
            (ByRef iRet As Integer)

#End Region

        Private Sub New()
        End Sub

        '********************************************************************
        '* Public Shared ****************************************************
        '********************************************************************

        Public Sub CalOff() Implements IPwc83040.CalOff '''''''''''''''''''''''''''
            PWC83040CALOFF()
        End Sub

        Public Sub CalOn() Implements IPwc83040.CalOn '''''''''''''''''''''''''''
            PWC83040CALON()
        End Sub

        Public Sub ChooseCal(calNo As Integer) Implements IPwc83040.ChooseCal '''''''''''''''''''''''''''
            PWC83040SETCONDITION(calNo)
        End Sub

        Public Sub CloseOutputData() Implements IPwc83040.CloseOutputData '''''''''''''''''''''''''''
            PWC83040CLOSEOUTPUTDATA()
        End Sub

        Public Sub ConditionOff(ByRef errorNo As Integer, Condition As Integer) Implements IPwc83040.ConditionOff '''''''''''''''''''''''''''
            PWC83040CONDITIONOFF(errorNo, Condition)
        End Sub

        Public Sub ConditionOn(ByRef errorNo As Integer, Condition As Integer) Implements IPwc83040.ConditionOn '''''''''''''''''''''''''''
            PWC83040CONDITIONON(errorNo, Condition)
        End Sub

        Public Sub ErrorString(errorNo As Integer, ByRef String1 As String, ByRef String2 As String) Implements IPwc83040.ErrorString '''''''''''''''''''''''''''
            PWC83040ERRORSTRING(errorNo, String1, String1.Length, String2, String2.Length)
        End Sub

        Public Sub FinalAcceptanceCalcs(ByRef errorNo As Integer, save As Integer, output As Integer) Implements IPwc83040.FinalAcceptanceCalcs '''''''''''''''''''''''''''
            PWC83040FACALCS(errorNo, save, output)
        End Sub

        Public Sub GetAllResults(ByRef Headers() As String, ByRef Values() As Double, ByRef Num As Integer) Implements IPwc83040.GetAllResults '''''''''''''''''''''''''''

            ReDim Preserve Headers(100)

            For Each Header As String In Headers
                Header.ToString().Substring(0, 10).Trim()
            Next

            ReDim Preserve Values(100)

            PWC83040GETALLRESULTS(Headers, Values, Num)

        End Sub

        Public Sub GetBSList(ByRef errorNo As Integer, ByRef BSList() As String, ByRef Num As Integer) Implements IPwc83040.GetBSList '''''''''''''''''''''''''''''''

            ReDim Preserve BSList(30)

            For Each BS As String In BSList
                BS.ToString().Substring(0, 26).Trim()
            Next

            PWC83040GETBSLIST(errorNo, BSList, Num)
        End Sub

        Public Sub GetBuild(ByRef buildNumber As Integer) Implements IPwc83040.GetBuild '''''''''''''''''''''''''''
            PWC83040GETBUILD(buildNumber)
        End Sub

        Public Sub GetBuildSpec(ByRef buildSpecNumber As Integer) Implements IPwc83040.GetBuildSpec '''''''''''''''''''''''''''
            PWC83040GETBUILD(buildSpecNumber)
        End Sub

        Public Sub GetCal(ByRef CalNumber As Integer) Implements IPwc83040.GetCal '''''''''''''''''''''''''''
            PWC83040GETCAL(CalNumber)
        End Sub

        Public Sub GetCalList(ByRef CalList() As Integer, ByRef NumberOfCalibrations As Integer) Implements IPwc83040.GetCalList '''''''''''''''''''''''''''

            ReDim Preserve CalList(80)

            PWC83040GETCALLIST(CalList, NumberOfCalibrations)

        End Sub

        Public Sub GetCalStat(ByRef CStatus As String) Implements IPwc83040.GetCalStat '''''''''''''''''''''''''''
            PWC83040GETCALSTAT(CStatus, CStatus.Length)
        End Sub

        Public Sub GetCalType(ByRef errorNo As Integer, ByRef CalType As String) Implements IPwc83040.GetCalType '''''''''''''''''''''''''''
            PWC83040GETCALTYPE(errorNo, CalType, CalType.Length)

        End Sub

        Public Sub GetCommonFile(ByRef errorNo As Integer) Implements IPwc83040.GetCommonFile '''''''''''''''''''''''''''
            PWC83040GETCOMMONFILE(errorNo)
        End Sub

        Public Sub GetCondition(ByRef iCondition As Integer) Implements IPwc83040.GetCondition '''''''''''''''''''''''''''
            PWC83040GETCONDITION(iCondition)
        End Sub

        Public Sub GetConditionList(ByVal ICalNo As Integer, ByRef lc_Cond As Integer(), ByRef lc_Desc As String, ByRef le_Desc As Integer, ByRef lc_Stat As Integer, ByRef le_stat As Integer, ByRef nCond As Integer) Implements IPwc83040.GetConditionList '''''''''''''''''''''''''''

            ReDim Preserve lc_Cond(40)

            For Each Cond As String In lc_Cond
                Cond.ToString().Substring(0, 4).Trim()
            Next

            PWC83040GETCONDITIONLIST(ICalNo, lc_Cond, lc_Desc, lc_Desc.Length, le_Desc, lc_Stat.ToString, lc_Stat.ToString.Length, le_stat, nCond)

        End Sub

        Public Sub GetData(errorNo As Integer) Implements IPwc83040.GetData '''''''''''''''''''''''''''
            PWC83040GETDATA(errorNo)
        End Sub

        Public Sub GetEngine(ByRef engineSerialNumber As String) Implements IPwc83040.GetEngine ''''''''''''''''''''''''''
            PWC83040GETENGINE(engineSerialNumber, engineSerialNumber.Length)
        End Sub

        Public Sub GetEngineModel(ByRef model As String) Implements IPwc83040.GetEngineModel '''''''''''''''''''''''''''
            PWC83040GETENGINEMODEL(model, model.Length)
        End Sub

        Public Sub GetFAResult(ByVal label As String, ByRef Result As Double) Implements IPwc83040.GetFAResult '''''''''''''''''''''''''''
            PWC83040GETFARESULT(label, label.Length, Result)
        End Sub

        Public Sub GetLastCal(ByRef CalNumber As Integer) Implements IPwc83040.GetLastCal '''''''''''''''''''''''''''
            PWC83040GETLASTCAL(CalNumber)
        End Sub

        Public Sub GetLastCondition(ByRef ConditionNumber As Integer) Implements IPwc83040.GetLastCondition '''''''''''''''''''''''''''
            PWC83040GETLASTCONDITION(ConditionNumber)
        End Sub

        Public Sub GetResult(label As String, ByRef value As Double) Implements IPwc83040.GetResult '''''''''''''''''''''''''''
            PWC83040GETRESULT(label, label.Length, value)
        End Sub

        Public Sub GetSB(ByRef errorNo As Integer, ByRef BuildSpec As String, ByRef NumSB As Integer, ByRef SBList As String(), ByRef EngineUP As String) Implements IPwc83040.GetSB '''''''''''''''''''''''''''
            ReDim Preserve SBList(6)
            PWC83040GETSB(errorNo, BuildSpec, BuildSpec.Length, NumSB, SBList, EngineUP, EngineUP.Length)
        End Sub

        Public Sub GetUnitData(ByRef errorNo As Integer) Implements IPwc83040.GetUnitData '''''''''''''''''''''''''''
            PWC83040GETUNITDATA(errorNo)
        End Sub

        Public Function GetValue(ByVal label As String, ByVal Index As String, ByRef value As String) As Boolean Implements IPwc83040.GetValue '''''''''''''''''''''''''''
            GetValue = PWC83040GETVALUE(label, label.Length, Index, Index.Length, value, value.Length)
        End Function

        Public Sub GetVersion(ByRef Version As String) Implements IPwc83040.GetVersion ''''''''''''''''''''''''''
            PWC83040GETVERSION(Version, Version.Length)
        End Sub

        Public Sub Initialize() Implements IPwc83040.Initialize '''''''''''''''''''''''''''
            PWC83040INITIALIZE()
        End Sub

        Public Sub LoadBuildSpec(ByRef errorNo As Integer, BuildSpec As String, SB1 As String, SB2 As String) Implements IPwc83040.LoadBuildSpec '''''''''''''''''''''''''''
            PWC83040LOADBUILDSPEC(errorNo, BuildSpec, BuildSpec.Length, SB1, SB1.Length, SB2, SB2.Length)
        End Sub

        Public Sub LoadConditionStat(ByRef errorNo As Integer) Implements IPwc83040.LoadConditionStat '''''''''''''''''''''''''''
            PWC83040LOADCONDITIONSTAT(errorNo)
        End Sub

        Public Sub NextCond(ByRef errorNo As Integer) Implements IPwc83040.NextCond '''''''''''''''''''''''''''
            PWC83040NEXTCOND(errorNo)
        End Sub

        Public Sub OutputData() Implements IPwc83040.OutputData '''''''''''''''''''''''''''
            PWC83040OUTPUTDATA()
        End Sub

        Public Sub PFileFirst() Implements IPwc83040.PFileFirst '''''''''''''''''''''''''''
            PWC83040PFILEFIRST()
        End Sub

        Public Sub PrevCondition(ByRef errorNo As Integer) Implements IPwc83040.PrevCondition '''''''''''''''''''''''''''
            PWC83040PREVCONDITION(errorNo)
        End Sub

        Public Sub Process(ByRef errorNo As Integer, ByVal save As Integer, ByVal output As Integer) Implements IPwc83040.Process '''''''''''''''''''''''''''
            PWC83040PROCESS(errorNo, save, output)
        End Sub

        Public Sub ProcessdData(ByRef errorNo As Integer) Implements IPwc83040.ProcessData '''''''''''''''''''''''''''
            PWC83040PROCESSDATA(errorNo)
        End Sub

        Public Function PutUnitValue(ByVal label As String, ByVal unit As String) As Boolean Implements IPwc83040.PutUnitValue '''''''''''''''''''''''''''
            PutUnitValue = PWC83040PUTUNITVALUE(label, label.Length, unit, unit.Length)
        End Function

        Public Function PutValue(ByVal label As String, ByVal value As String, ByVal index As String) As Boolean Implements IPwc83040.PutValue '''''''''''''''''''''''''''
            PutValue = PWC83040PUTVALUE(label, label.Length, value, value.Length, index, index.Length)
        End Function

        Public Sub ReadPFile(ByRef errorNo As Integer) Implements IPwc83040.ReadPFile '''''''''''''''''''''''''''
            PWC83040READPFILE(errorNo)
        End Sub

        Public Sub ReadSNFFile(ByRef errorNo As Integer) Implements IPwc83040.ReadSNFFile '''''''''''''''''''''''''''
            PWC83040READSNFFILE(errorNo)
        End Sub

        Public Sub SaveConditionStat(ByRef errorNo As Integer) Implements IPwc83040.SaveConditionStat '''''''''''''''''''''''''''
            PWC83040SAVECONDITIONSTAT(errorNo)
        End Sub

        Public Sub SaveData(ByRef errorNo As Integer) Implements IPwc83040.SaveData '''''''''''''''''''''''''''
            PWC83040SAVEDATA(errorNo)
        End Sub

        Public Sub SaveUnitData(ByRef errorNo As Integer) Implements IPwc83040.SaveUnitData
            PWC83040SAVEUNITDATA(errorNo)
        End Sub

        Public Sub SetBuild(ByRef buildNo As Integer) Implements IPwc83040.SetBuild '''''''''''''''''''''''''''
            PWC83040SETBUILD(buildNo)
        End Sub

        Public Sub SetCal(ByVal CalNo As Integer) Implements IPwc83040.SetCal '''''''''''''''''''''''''''
            PWC83040SETCAL(CalNo)
        End Sub

        Public Sub SetCondition(ByVal conditionNo As Integer) Implements IPwc83040.SetCondition '''''''''''''''''''''''''''
            PWC83040SETCONDITION(conditionNo)
        End Sub

        Public Sub SetConfigurationPath(ByVal path As String) Implements IPwc83040.SetConfigurationPath '''''''''''''''''''''''''''
            PWC83040SETCONFIGURATIONPATH(path, path.Length)
        End Sub

        Public Sub SetDataPath(ByVal path As String) Implements IPwc83040.SetDataPath '''''''''''''''''''''''''''
            PWC83040SETDATAPATH(path, path.Length)
        End Sub

        Public Sub SetDateTime() Implements IPwc83040.SetDateTime '''''''''''''''''''''''''''
            PWC83040SETDATETIME()
        End Sub

        Public Sub SetEngine(ByVal Engine As String) Implements IPwc83040.SetEngine ''''''''''''''''''''''''''
            PWC83040SETENGINE(Engine, Engine.Length)
        End Sub

        Public Sub SetNewCal(ByVal CalNo As Integer) Implements IPwc83040.SetNewCal '''''''''''''''''''''''''''
            PWC83040SETNEWCAL(CalNo)
        End Sub

        Public Sub SetNewCondition(ByVal conditionNo As Integer) Implements IPwc83040.SetNewCondition '''''''''''''''''''''''''''
            PWC83040SETNEWCONDITION(conditionNo)
        End Sub

        Public Sub SetStatus(ByVal Status As String) Implements IPwc83040.SetStatus ''''''''''''''''''''''''''
            PWC83040SETSTATUS(Status, Status.Length)
        End Sub

        Public Sub SNFFirst() Implements IPwc83040.SNFFirst '''''''''''''''''''''''''''
            PWC83040SNFFirst()
        End Sub

        Public Sub ToggleCalStat() Implements IPwc83040.ToggleCalStat '''''''''''''''''''''''''''
            PWC83040TOGGLECALSTAT()
        End Sub

        Public Sub TransferData(ByRef errorNo As Integer) Implements IPwc83040.TransferData
            PWC83040TRANSFERDATA(errorNo)
        End Sub

    End Class

End Namespace


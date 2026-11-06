Imports PwcSharedInterface
Imports System.Text
Imports System.Runtime.InteropServices

Namespace PwcService

    '********************************************************************
    '********************************************************************
    '* PW83020 integrates PT6 Family ************************************
    '********************************************************************
    '********************************************************************

    Public NotInheritable Class Pwc83020Service
        Implements IPwc83020

        '********************************************************************
        '* Private Declare **************************************************
        '********************************************************************

#Region "Declare"

        '\-=-= Initialize =-=-/

        Private Declare Ansi Sub PWC83020INITIALIZE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" ()

        '\-=-= Path =-=-/

        Private Declare Ansi Sub PWC83020SETDATAPATH _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (ByVal cPath As String, ByVal cPathLen As Integer)

        Private Declare Ansi Sub PWC83020SETCONFIGURATIONPATH _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (ByVal cPath As String, ByVal cPathLen As Integer)

        '\-=-= Units =-=-/

        Private Declare Ansi Sub PWC83020GETUNITDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" ()

        Private Declare Ansi Function PWC83020PUTUNITVALUE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, _
             ByVal cUnit As String, ByVal cUnitLen As Integer, _
             ByVal cIndex As String, ByVal cIndexLen As Integer) As Boolean

        Private Declare Ansi Sub PWC83020SAVEUNITDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" ()

        '\-=-= Engine Configuration =-=-/

        Private Declare Ansi Sub PWC83020SETENGINE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (ByVal cEngine As String, ByVal cEngineLen As Integer)

        Private Declare Ansi Sub PWC83020GETENGINE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (<[In], Out> ByVal cEngine As StringBuilder, ByVal cEngineSize As Integer)

        Private Declare Ansi Sub PWC83020SETBUILD _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (ByRef iBuild As Integer)

        Private Declare Ansi Sub PWC83020GETBUILD _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (ByRef iBuild As Integer)

        Private Declare Ansi Sub PWC83020SETCONDITION _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (ByRef iCondition As Integer)

        Private Declare Ansi Sub PWC83020GETCONDITION _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (ByRef iCondition As Integer)

        Private Declare Ansi Sub PWC83020NEXTCOND _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (ByRef iError As Integer)

        Private Declare Ansi Sub PWC83020LOADMODEL _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (ByRef iError As Integer, ByVal cModel As String, ByVal iModel As Integer, _
             ByVal cSB1 As String, ByVal iSB1 As Integer, _
             ByVal cSB2 As String, ByVal iSB2 As Integer)

        Private Declare Ansi Sub PWC83020GETENGINEMODEL _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (<[In], Out> ByVal cModel As StringBuilder, ByVal cEngineSize As Integer)

        '\-=-= Data =-=-/

        Private Declare Ansi Function PWC83020PUTVALUE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, _
             ByVal cValue As String, ByVal cValueLen As Integer, _
             ByVal cIndex As String, ByVal cIndexLen As Integer) As Boolean

        Private Declare Ansi Sub PWC83020GETRATINGDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (ByRef NumConds As Integer,
             ByRef NumArr As Integer,
             <[In](), Out()> ByVal iConds() As Integer,
             <[In](), Out()> ByVal rSHPDyno() As Single,
             <[In](), Out()> ByVal rSHPTm() As Single,
             <[In](), Out()> ByVal rT5A() As Single,
             <[In](), Out()> ByVal iTempTrimCode() As Integer,
             ByRef irc As Integer)


        '\-=-= Process =-=-/

        Private Declare Ansi Sub PWC83020PROCESS _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (ByRef iError As Integer, ByRef iSave As Integer, ByRef iOutput As Integer)

        Private Declare Ansi Sub PWC83020PROCESSBOVTEST _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (ByRef iOutput As Integer, <[In](), Out()> ByVal BovBaro() As Single, <[In](), Out()> ByVal BovPplenum() As Single,
             <[In](), Out()> ByVal BovP1000() As Single, <[In](), Out()> ByVal BovT1() As Single, <[In](), Out()> ByVal BovNg() As Single,
             ByRef BovClosingNg As Single, ByRef iError As Integer)

        Private Declare Ansi Sub PWC83020PROCESSTORQUETRIM _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (ByRef iOutput As Integer, <[In](), Out()> ByVal rDynoTorque() As Single,
             <[In](), Out()> ByVal rEngineTorque() As Single, ByRef iMinPts As Integer,
             ByRef rDcuTrimGain As Single, ByRef rDcuTrimBias As Single, ByRef iError As Integer)

        Private Declare Ansi Sub PWC83020RATINGANALYSIS _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (ByRef iOutput As Integer, ByRef NumUsed As Integer,
             <[In](), Out()> ByVal IcondUsed() As Integer, ByRef iError As Integer)

        '\-=-= Not Used =-=-/

        Private Declare Ansi Sub PWC83020GETDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" ()

        Private Declare Ansi Sub PWC83020GETRESULT _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, ByRef rResult As Single)

        Private Declare Ansi Sub PWC83020GETSUMMARY _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (ByRef iUpdateSummary As Integer)

        Private Declare Ansi Function PWC83020GETREALVALUE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, ByRef fValue As Single,
             ByVal cIndex As String, ByVal cIndexLen As Integer) As Boolean

        Private Declare Ansi Function PWC83020GETVALUE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer,
             ByVal cValue As String, ByVal cValueLen As Integer,
             ByVal cIndex As String, ByVal cIndexLen As Integer) As Boolean

        Private Declare Ansi Sub PWC83020SAVEDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (ByRef iError As Integer)

        Private Declare Ansi Sub PWC83020SETSUMMARY _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83020\exe\PWC83020.dll" _
            (ByVal iUpdateSummary As Integer)

#End Region

        Private Sub New()
        End Sub

        '********************************************************************
        '* Public Shared ****************************************************
        '********************************************************************

        '\-=-= Initialize =-=-/

        Public Sub Initialize() Implements IPwc83020.Initialize
            PWC83020INITIALIZE()
        End Sub

        '\-=-= Path =-=-/

        Public Sub SetDataPath(ByVal path As String) Implements IPwc83020.SetDataPath
            PWC83020SETDATAPATH(path, path.Length)
        End Sub

        Public Sub SetConfigurationPath(ByVal path As String) Implements IPwc83020.SetConfigurationPath
            PWC83020SETCONFIGURATIONPATH(path, path.Length)
        End Sub

        '\-=-= Units =-=-/

        Public Sub GetUnitData() Implements IPwc83020.GetUnitData
            PWC83020GETUNITDATA()
        End Sub

        Public Function PutUnitValue(ByVal label As String, ByVal unit As String, ByVal index As String) As Boolean Implements IPwc83020.PutUnitValue
            PutUnitValue = PWC83020PUTUNITVALUE(label, label.Length, unit, unit.Length, index, index.Length)
        End Function

        Public Sub SaveUnitData() Implements IPwc83020.SaveUnitData
            PWC83020SAVEUNITDATA()
        End Sub

        '\-=-= Engine Configuration =-=-/

        Public Sub SetEngine(ByVal engineSerialNo As String) Implements IPwc83020.SetEngine
            PWC83020SETENGINE(engineSerialNo, engineSerialNo.Length)
        End Sub

        Public Sub GetEngine(ByRef engineSerialNumber As String) Implements IPwc83020.GetEngine
            Dim engineSNFor As StringBuilder = New StringBuilder(6)
            PWC83020GETENGINE(engineSNFor, engineSNFor.Capacity())
            engineSerialNumber = engineSNFor.ToString().Substring(0, 6).Trim()
        End Sub

        Public Sub SetBuild(ByRef iBuild As Integer) Implements IPwc83020.SetBuild
            PWC83020SETBUILD(iBuild)
        End Sub

        Public Sub GetBuild(ByRef buildNumber As Integer) Implements IPwc83020.GetBuild
            PWC83020GETBUILD(buildNumber)
        End Sub

        Public Sub SetCondition(ByRef conditionNo As Integer) Implements IPwc83020.SetCondition
            PWC83020SETCONDITION(conditionNo)
        End Sub

        Public Sub GetCondition(ByRef iCondition As Integer) Implements IPwc83020.GetCondition
            PWC83020GETCONDITION(iCondition)
        End Sub

        Public Sub NextCond(ByRef errorNo As Integer) Implements IPwc83020.NextCond
            PWC83020NEXTCOND(errorNo)
        End Sub

        Public Sub LoadModel(ByRef errorNo As Integer, ByVal Model As String, Optional ByVal SB1 As String = "        ", Optional ByVal SB2 As String = "        ") Implements IPwc83020.LoadModel
            PWC83020LOADMODEL(errorNo, Model.PadRight(20), Model.Length, SB1, SB1.Length, SB2, SB2.Length)
        End Sub

        Public Sub GetEngineModel(ByRef model As String) Implements IPwc83020.GetEngineModel
            Dim engineModelFor As StringBuilder = New StringBuilder(20)
            PWC83020GETENGINEMODEL(engineModelFor, engineModelFor.Capacity())
            model = engineModelFor.ToString().Substring(0, 20).Trim()
        End Sub

        '\-=-= Data =-=-/

        Public Function PutValue(ByVal label As String, ByVal value As String, ByVal index As String) As Boolean Implements IPwc83020.PutValue
            PutValue = PWC83020PUTVALUE(label, label.Length, value, value.Length, index, index.Length)
        End Function

        Public Sub GetRatingData(ByRef NumConds As Integer,
                                 ByRef NumArr As Integer,
                                 ByVal iConds() As Integer,
                                 ByVal rSHPDyno() As Single,
                                 ByVal rSHPTm() As Single,
                                 ByVal rT5A() As Single,
                                 ByVal iTempTrimCode() As Integer,
                                 ByRef irc As Integer) _
                                 Implements IPwc83020.GetRatingData
            PWC83020GETRATINGDATA(NumConds, NumArr, iConds, rSHPDyno, rSHPTm, rT5A, iTempTrimCode, irc)
        End Sub


        '\-=-= Process =-=-/

        Public Sub Process(ByRef errorNo As Integer, ByRef save As Integer, ByRef output As Integer) Implements IPwc83020.Process
            PWC83020PROCESS(errorNo, save, output)
        End Sub

        Public Sub ProcessBovTest(ByRef iOutput As Integer, ByVal BovBaro() As Single, ByVal BovPplenum() As Single, _
                                  ByVal BovP1000() As Single, ByVal BovT1() As Single, ByVal BovNg() As Single, _
                                  ByRef BovClosingNg As Single, ByRef iError As Integer) Implements IPwc83020.ProcessBovTest
            ReDim Preserve BovBaro(2)
            ReDim Preserve BovPplenum(2)
            ReDim Preserve BovP1000(2)
            ReDim Preserve BovT1(2)
            ReDim Preserve BovNg(2)
            PWC83020PROCESSBOVTEST(iOutput, BovBaro, BovPplenum, BovP1000, BovT1, BovNg, BovClosingNg, iError)
        End Sub

        Public Sub ProcessTorqueTrim(ByRef Output As Integer, ByVal DynoTorque() As Single, ByVal EngineTorque() As Single, ByRef MinPts As Integer, _
                                     ByRef DcuTrimGain As Single, ByRef DcuTrimBias As Single, ByRef iError As Integer) Implements IPwc83020.ProcessTorqueTrim
            ReDim Preserve DynoTorque(11)
            ReDim Preserve EngineTorque(11)
            PWC83020PROCESSTORQUETRIM(Output, DynoTorque, EngineTorque, MinPts, DcuTrimGain, DcuTrimBias, iError)
        End Sub

        Public Sub RatingAnalysis(ByRef Output As Integer, ByRef NumUsed As Integer, ByVal IcondUsed() As Integer, ByRef iError As Integer) Implements IPwc83020.RatingAnalysis
            PWC83020RATINGANALYSIS(Output, NumUsed, IcondUsed, iError)
        End Sub

      

        '\-=-= Not Used =-=-/

        Public Sub GetData() Implements IPwc83020.GetData
            PWC83020GETDATA()
        End Sub

        Public Sub GetResult(ByVal label As String, ByRef value As Single) Implements IPwc83020.GetResult
            PWC83020GETRESULT(label, label.Length(), value)
        End Sub

        Public Sub GetSummary(ByRef summary As Boolean) Implements IPwc83020.GetSummary
            PWC83020GETSUMMARY(summary)
        End Sub

        Public Function GetRealValue(ByVal label As String, ByRef value As String, ByVal index As String) As Boolean Implements IPwc83020.GetRealValue
            Dim result As Boolean
            result = PWC83020GETREALVALUE(label, label.Length(), value, index, index.Length())
            GetRealValue = result
        End Function

        Public Function GetValue(ByVal label As String, ByRef value As String, ByVal index As String) As Boolean Implements IPwc83020.GetValue
            Dim result As Boolean
            result = PWC83020GETVALUE(label, label.Length(), value, value.Length, index, index.Length())
            GetValue = result
        End Function

        Public Sub SaveData(ByRef errorNo As Integer) Implements IPwc83020.SaveData
            PWC83020SAVEDATA(errorNo)
        End Sub

        Public Sub SetSummary(ByVal summary As Boolean) Implements IPwc83020.SetSummary
            PWC83020SETSUMMARY(summary)
        End Sub

    End Class

End Namespace


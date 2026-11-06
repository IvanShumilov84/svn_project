Imports PwcSharedInterface
Imports System.Runtime.InteropServices
Imports System.Text

Namespace PwcService

    '********************************************************************
    '* PW83000 Integrates PW100 Family ************************************
    '********************************************************************

    Public NotInheritable Class Pwc83000Service
        Implements IPwc83000


#Region "Declare"

        '\-=-= Initialize =-=-/

        Private Declare Ansi Sub PWC83000INITIALIZE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" ()

        '\-=-= Path =-=-/

        Private Declare Ansi Sub PWC83000SETDATAPATH _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
        (ByVal cPath As String, ByVal cPathLen As Integer)

        Private Declare Ansi Sub PWC83000SETCONFIGURATIONPATH _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
                    (ByVal cPath As String, ByVal cPathLen As Integer)

        Private Declare Ansi Sub PWC83000DEFINE _
           Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" ()

        '\-=-= Units =-=-/

        Private Declare Ansi Sub PWC83000GETUNITDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" ()

        Private Declare Ansi Function PWC83000PUTUNITVALUE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, _
             ByVal cUnit As String, ByVal cUnitLen As Integer, _
             ByVal cIndex As String, ByVal cIndexLen As Integer) As Boolean

        Private Declare Ansi Sub PWC83000SAVEUNITDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" ()

        '\-=-= Engine Configuration =-=-/

        Private Declare Ansi Sub PWC83000SETENGINE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
            (ByVal cEngine As String, ByVal cEngineLen As Integer)

        Private Declare Ansi Sub PWC83000GETENGINE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
            (<[In], Out> ByVal cEngine As StringBuilder, ByVal cEngineSize As Integer)

        Private Declare Ansi Sub PWC83000SETBUILD _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
            (ByRef iBuild As Integer)

        Private Declare Ansi Sub PWC83000GETBUILD _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
            (ByRef iBuild As Integer)

        Private Declare Ansi Sub PWC83000SETCONDITION _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
            (ByRef iCondition As Integer)

        Private Declare Ansi Sub PWC83000GETCONDITION _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
            (ByRef iCondition As Integer)

        Private Declare Ansi Sub PWC83000NEXTCOND _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
            (ByRef iError As Integer)

        Private Declare Ansi Sub PWC83000LOADMODEL _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
            (ByRef iError As Integer, ByRef iBuild As Integer)

        Private Declare Ansi Sub PWC83000GETENGINEMODEL _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
            (<[In], Out> ByVal cModel As StringBuilder, ByVal cEngineSize As Integer)

        Private Declare Ansi Sub PWC83000SETITTTRIM _
           Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
           (ByRef iValue As Integer)

        '\-=-= Data =-=-/

        Private Declare Ansi Function PWC83000PUTVALUE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, _
             ByVal cValue As String, ByVal cValueLen As Integer, _
             ByVal cIndex As String, ByVal cIndexLen As Integer) As Boolean

        Private Declare Ansi Sub PWC83000GETRATINGDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
            (ByRef NumConds As Integer, ByRef NumArr As Integer, _
             ByRef iConds As Integer, ByRef rSHPDyno As Single, _
             ByRef rSHPTm As Single, ByRef rT5A As Single, _
             ByRef iTempTrimCode As Integer, ByRef irc As Integer)

        '\-=-= Process =-=-/

        Private Declare Ansi Sub PWC83000PROCESS _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
            (ByRef iError As Integer, ByRef iSave As Integer, ByRef iOutput As Integer)

        Private Declare Ansi Sub PWC83000PROCESSTORQUETRIM _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
                (ByRef iOutput      As Integer,         ByRef iTMTolDouble As Integer, _
                ByRef rDynoTmTO     As Single,          ByRef rDynoTm50PC As Single, _
                ByRef rEng1TO       As Single,          ByRef rEng150PC As Single, _
                ByRef rEng2TO       As Single,          ByRef rEng250PC As Single, _
                ByRef rGainCl       As Single,          ByRef rBiasCl As Single, _
                ByRef rResGain      As Single,          ByRef rResBias As Single, _
                ByRef iError        As Integer)

        Private Declare Ansi Sub PWC83000GETR1 _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
            (ByRef rR1 As Double)

        Private Declare Ansi Sub PWC83000RATINGANALYSIS _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
            (ByRef iOutput As Integer, ByRef NumUsed As Integer, _
             <[In](), Out()> ByVal IcondUsed() As Integer, ByRef iError As Integer)

        Private Declare Ansi Sub PWC83000GETTORQUETRIMERROR _
        Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
        (ByRef iTorqueSystem As Integer, <[In], Out> ByVal iError() As Integer)

        Private Declare Ansi Sub PWC83000GETTORQUELABEL _
        Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
        (ByRef iSystem As Integer, <[In], Out> ByVal cTMLabl As StringBuilder, ByVal cTMLablSize As Integer)

        Private Declare Ansi Sub PWC83000GETTORQUESYSTEMS _
        Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83000\exe\Pwc83000.dll" _
        (<[In], Out> ByVal lTMsystemsUsed() As Integer)

        '\-=-= Not Used =-=-/

        Private Declare Ansi Sub PWC83000GETDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" ()

        Private Declare Ansi Sub PWC83000GETRESULT _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, ByRef rResult As Single)

        Private Declare Ansi Sub PWC83000GETSUMMARY _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
            (ByRef iUpdateSummary As Integer)

        Private Declare Ansi Function PWC83000GETREALVALUE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, ByRef fValue As Single, _
             ByVal cIndex As String, ByVal cIndexLen As Integer) As Boolean

        Private Declare Ansi Function PWC83000GETVALUE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, _
             ByVal cValue As String, ByVal cValueLen As Integer, _
             ByVal cIndex As String, ByVal cIndexLen As Integer) As Boolean

        Private Declare Ansi Sub PWC83000SAVEDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
            (ByRef iError As Integer)

        Private Declare Ansi Sub PWC83000SETSUMMARY _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83000\exe\PWC83000.dll" _
            (ByRef iUpdateSummary As Integer)

#End Region

        Public Sub Define() Implements IPwc83000.Define
            PWC83000DEFINE()
        End Sub

        Public Sub GetBuild(ByRef buildNumber As Integer) Implements IPwc83000.GetBuild
            PWC83000GETBUILD(buildNumber)
        End Sub

        Public Sub GetCondition(ByRef iconditionNo As Integer) Implements IPwc83000.GetCondition
            PWC83000GETCONDITION(iconditionNo)
        End Sub

        Public Sub GetData() Implements IPwc83000.GetData
            PWC83000GETDATA()
        End Sub

        Public Sub GetEngine(ByRef engineSerialNumber As String) Implements IPwc83000.GetEngine
            Dim engineSNFor As StringBuilder = New StringBuilder(6)
            PWC83000GETENGINE(engineSNFor, engineSNFor.Capacity())
            engineSerialNumber = engineSNFor.ToString().Substring(0, 6).Trim()
        End Sub

        Public Sub GetEngineModel(ByRef model As String) Implements IPwc83000.GetEngineModel
            Dim engineModelFor As StringBuilder = New StringBuilder(20)
            PWC83000GETENGINEMODEL(engineModelFor, engineModelFor.Capacity())
            model = engineModelFor.ToString().Substring(0, 20).Trim()
        End Sub

        Public Sub GetRatingData(ByRef numConds As Integer, ByRef numArr As Integer, ByRef iConds As Integer, ByRef rSHPDyno As Single, ByRef rSHPTm As Single, ByRef rT5A As Single, ByRef iTempTrimCode As Integer, ByRef irc As Integer) Implements IPwc83000.GetRatingData
            PWC83000GETRATINGDATA(numConds, numArr, iConds, rSHPDyno, rSHPTm, rT5A, iTempTrimCode, irc)
        End Sub

        Public Function GetRealValue(label As String, ByRef value As String, index As String) As Boolean Implements IPwc83000.GetRealValue
            Dim result As Boolean
            result = PWC83000GETREALVALUE(label, label.Length(), value, index, index.Length())
            GetRealValue = result
        End Function

        Public Sub GetResult(label As String, ByRef value As Single) Implements IPwc83000.GetResult
            PWC83000GETRESULT(label, label.Length(), value)
        End Sub

        Public Sub GetSummary(ByRef summary As Integer) Implements IPwc83000.GetSummary
            PWC83000GETSUMMARY(summary)
        End Sub


        Public Sub GetTorqueLabel(ByRef iSystem As Integer, ByRef cTMLabel As String) Implements IPwc83000.GetTorqueLabel
            Dim localSB As StringBuilder = New StringBuilder(3)
            Dim ls As String = String.Empty
            PWC83000GETTORQUELABEL(iSystem, localSB, localSB.Capacity())
            ls = localSB.ToString().Substring(0, 3).Trim()
            cTMLabel = ls
        End Sub

        Public Sub GetTorqueSystems(ByRef lTMsystemsUsed() As Boolean) Implements IPwc83000.GetTorqueSystems
            Dim i, lTMsystemsUsedFor(3) As Integer

            ReDim lTMsystemsUsed(lTMsystemsUsedFor.GetUpperBound(0))

            PWC83000GETTORQUESYSTEMS(lTMsystemsUsedFor)

            For i = lTMsystemsUsedFor.GetLowerBound(0) To lTMsystemsUsedFor.GetUpperBound(0)
                lTMsystemsUsed(i) = lTMsystemsUsedFor(i)
            Next
        End Sub

        Public Sub GetTorqueTrimError(ByRef iSystem As Integer, ByRef iError() As Integer) Implements IPwc83000.GetTorqueTrimError
            Dim i As Integer
            Dim localiError(1) As Integer
            ReDim iError(localiError.GetUpperBound(0))

            PWC83000GETTORQUETRIMERROR(iSystem, localiError)

            For i = localiError.GetLowerBound(0) To localiError.GetUpperBound(0)
                iError(i) = localiError(i)
            Next

        End Sub

        Public Sub GetUnitData() Implements IPwc83000.GetUnitData
            PWC83000GETUNITDATA()
        End Sub

        Public Sub GetR1(ByRef r1 As Double) Implements IPwc83000.GetR1
            PWC83000GETR1(r1)
        End Sub

        Public Function GetValue(label As String, ByRef value As String, index As String) As Boolean Implements IPwc83000.GetValue
            Dim result As Boolean
            result = PWC83000GETVALUE(label, label.Length(), value, value.Length, index, index.Length())
            GetValue = result
        End Function

        Public Sub Initialize() Implements IPwc83000.Initialize
            PWC83000INITIALIZE()
        End Sub

        Public Sub LoadModel(ByRef errorNo As Integer, ByRef build As Integer) Implements IPwc83000.LoadModel
            PWC83000LOADMODEL(errorNo, build)
        End Sub

        Public Sub NextCond(ByRef errorNo As Integer) Implements IPwc83000.NextCond
            PWC83000NEXTCOND(errorNo)
        End Sub

        Public Sub Process(ByRef errorNo As Integer, ByRef save As Integer, ByRef output As Integer) Implements IPwc83000.Process
            PWC83000PROCESS(errorNo, save, output)
        End Sub

        'Public Sub ProcessTorqueTrim(ByRef output As Integer, dynoTorque() As Single, engineTorque() As Single, ByRef minPts As Integer, ByRef dcuTrimGain As Single, ByRef dcuTrimBias As Single, ByRef iError As Integer) Implements IPwc83000.ProcessTorqueTrim
        '    ReDim Preserve dynoTorque(11)
        '    ReDim Preserve engineTorque(11)
        '    PWC83000PROCESSTORQUETRIM(output, dynoTorque, engineTorque, minPts, dcuTrimGain, dcuTrimBias, iError)
        'End Sub

        Public Sub ProcessTorqueTrim(ByRef output As Boolean, ByRef iTMTolDouble As Integer, _
            ByRef rDynoTmTO As Single, ByRef rDynoTm50PC As Single, _
            ByRef rDynoEng1TO As Single, ByRef rDynoEng150PC As Single, _
            ByRef rDynoEng2TO As Single, ByRef rDynoEng250PC As Single, _
            ByRef rGainCl As Single, ByRef rBiasCl As Single, _
            ByRef rResGain As Single, ByRef rResBias As Single, _
            ByRef iErr As Integer) Implements IPwc83000.ProcessTorqueTrim
            'ReDim Preserve dynoTorque(11)
            'ReDim Preserve engineTorque(11)
            PWC83000PROCESSTORQUETRIM(output, iTMTolDouble, _
                                      rDynoTmTO, rDynoTm50PC, _
                                      rDynoEng1TO, rDynoEng150PC, _
                                      rDynoEng2TO, rDynoEng250PC, _
                                      rGainCl, rBiasCl, _
                                      rResGain, rResBias, _
                                      iErr)
        End Sub

        Public Function PutUnitValue(label As String, unit As String, index As String) As Boolean Implements IPwc83000.PutUnitValue
            PutUnitValue = PWC83000PUTUNITVALUE(label, label.Length, unit, unit.Length, index, index.Length)
        End Function

        Public Function PutValue(label As String, value As String, index As String) As Boolean Implements IPwc83000.PutValue
            PutValue = PWC83000PUTVALUE(label, label.Length, value, value.Length, index, index.Length)
        End Function

        Public Sub RatingAnalysis(ByRef output As Integer, ByRef numUsed As Integer, icondUsed() As Integer, ByRef iError As Integer) Implements IPwc83000.RatingAnalysis
            PWC83000RATINGANALYSIS(output, numUsed, icondUsed, iError)
        End Sub

        Public Sub SaveData(ByRef errorNo As Integer) Implements IPwc83000.SaveData
            PWC83000SAVEDATA(errorNo)
        End Sub

        Public Sub SaveUnitData() Implements IPwc83000.SaveUnitData
            PWC83000SAVEUNITDATA()
        End Sub

        Public Sub SetBuild(ByRef iBuild As Integer) Implements IPwc83000.SetBuild
            PWC83000SETBUILD(iBuild)
        End Sub

        Public Sub SetCondition(ByRef conditionNo As Integer) Implements IPwc83000.SetCondition
            PWC83000SETCONDITION(conditionNo)
        End Sub

        Public Sub SetConfigurationPath(path As String) Implements IPwc83000.SetConfigurationPath
            PWC83000SETCONFIGURATIONPATH(path, path.Length)
            'PWC83000SETCONFIGURATIONPATH(path)
        End Sub

        Public Sub SetDataPath(cPath As String) Implements IPwc83000.SetDataPath
            PWC83000SETDATAPATH(cPath, cPath.Length)
            'PWC83000SETDATAPATH(cPath)
        End Sub

        Public Sub SetEngine(engineSerialNo As String) Implements IPwc83000.SetEngine
            PWC83000SETENGINE(engineSerialNo, engineSerialNo.Length)
        End Sub

        Public Sub SetITTTrim(ByRef iValue As Integer) Implements IPwc83000.SetITTTrim
            PWC83000SETITTTRIM(iValue)
        End Sub

        Public Sub SetSummary(summary As Integer) Implements IPwc83000.SetSummary
            PWC83000SETSUMMARY(summary)
        End Sub
    End Class

End Namespace
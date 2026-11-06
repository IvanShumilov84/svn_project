Imports PwcSharedInterface
Imports System.Text
Imports System.Runtime.InteropServices

Namespace PwcService

    '********************************************************************
    '********************************************************************
    '* PW83018 integrates PWC PW200 Family ******************************
    '********************************************************************
    '********************************************************************

    Public NotInheritable Class Pwc83018Service
        Implements IPwc83018

        '********************************************************************
        '* Private Declare **************************************************
        '********************************************************************

#Region "Declare"

        Private Declare Ansi Sub PWC83018GETBUILD _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" _
            (ByRef iBuild As Integer)

        Private Declare Ansi Sub PWC83018GETCONDITION _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" _
            (ByRef iCondition As Integer)

        Private Declare Ansi Sub PWC83018GETDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" ()

        Private Declare Ansi Sub PWC83018GETENGINE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" _
            (<[In], Out> ByVal cEngine As StringBuilder, ByVal cEngineSize As Integer)

        Private Declare Ansi Sub PWC83018GETENGINEMODEL _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" _
            (<[In], Out> ByVal cModel As StringBuilder, ByVal cEngineSize As Integer)

        Private Declare Ansi Sub PWC83018GETRESULT _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, _
             ByRef rResult As Double)

        Private Declare Ansi Sub PWC83018GETSUMMARY _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" _
            (ByRef iUpdateSummary As Integer)

        Private Declare Ansi Sub PWC83018GETUNITDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" ()

        Private Declare Ansi Function PWC83018GETVALUE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, _
             ByVal cValue As String, ByVal cValueLen As Integer, _
             ByVal cIndex As String, ByVal cIndexLen As Integer) As Boolean

        Private Declare Ansi Function PWC83018GETREALVALUE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, _
             ByRef fValue As Single, _
             ByVal cIndex As String, ByVal cIndexLen As Integer) As Boolean

        Private Declare Ansi Sub PWC83018LOADMODEL _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" _
            (ByRef iError As Integer, ByVal cModel As String, ByVal iModel As Integer, _
             ByVal cSB1 As String, ByVal iSB1 As Integer, _
             ByVal cSB2 As String, ByVal iSB2 As Integer)

        Private Declare Ansi Sub PWC83018NEXTCOND _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" _
            (ByRef iError As Integer)

        Private Declare Ansi Sub PWC83018PROCESS _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" _
            (ByRef iError As Integer, _
             ByRef iSave As Integer, ByRef iOutput As Integer)

        Private Declare Ansi Sub PWC83018PROCESSTORQUETRIM _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" _
            (ByRef iError As Integer, _
             <[In](), Out()> ByVal rDyno() As Single, _
             <[In](), Out()> ByVal rTorque() As Single, _
             <[In](), Out()> ByVal rTorque2() As Single, _
            ByRef iOutput As Integer)

        Private Declare Ansi Function PWC83018PUTUNITVALUE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, _
             ByVal cUnit As String, ByVal cUnitLen As Integer, _
             ByVal cIndex As String, ByVal cIndexLen As Integer) As Boolean

        Private Declare Ansi Function PWC83018PUTVALUE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, _
             ByVal cValue As String, ByVal cValueLen As Integer, _
             ByVal cIndex As String, ByVal cIndexLen As Integer) As Boolean

        Private Declare Ansi Sub PWC83018SAVEDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" _
            (ByRef iError As Integer)

        Private Declare Ansi Sub PWC83018SAVEUNITDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" ()

        Private Declare Ansi Sub PWC83018RESETCOMMON _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" ()

        Private Declare Ansi Sub PWC83018RESETM _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" ()

        Private Declare Ansi Sub PWC83018RESETTORQUE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" ()

        Private Declare Ansi Sub PWC83018SETBUILD _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" _
            (ByRef iBuild As Integer)

        Private Declare Ansi Sub PWC83018SETCONDITION _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" _
            (ByRef iCondition As Integer)

        Private Declare Ansi Sub PWC83018SETCONFIGURATIONPATH _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" _
            (ByVal cPath As String, ByVal cPathLen As Integer)

        Private Declare Ansi Sub PWC83018SETDATAPATH _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" _
            (ByVal cPath As String, ByVal cPathLen As Integer)

        Private Declare Ansi Sub PWC83018SETENGINE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" _
            (ByVal cEngine As String, ByVal cEngineLen As Integer)

        Private Declare Ansi Sub PWC83018SETSUMMARY _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83018\exe\PWC83018.dll" _
            (ByRef iUpdateSummary As Integer)


#End Region

        Private Sub New()
        End Sub

        '********************************************************************
        '* Public Shared ****************************************************
        '********************************************************************

        Public Sub GetBuild(ByRef buildNumber As Integer) Implements IPwc83018.GetBuild '''''''''''''''''''''''''''
            PWC83018GETBUILD(buildNumber)
        End Sub

        Public Sub GetCondition(ByRef iCondition As Integer) Implements IPwc83018.GetCondition '''''''''''''''''''''''''''
            PWC83018GETCONDITION(iCondition)
        End Sub

        Public Sub GetData() Implements IPwc83018.GetData '''''''''''''''''''''''''''
            PWC83018GETDATA()
        End Sub

        Public Sub GetEngine(ByRef engineSerialNumber As String) Implements IPwc83018.GetEngine ''''''''''''''''''''''''''
            Dim engineSNFor As StringBuilder = New StringBuilder(6)

            PWC83018GETENGINE(engineSNFor, engineSNFor.Capacity())
            engineSerialNumber = engineSNFor.ToString().Substring(0, 6).Trim()
        End Sub

        Public Sub GetEngineModel(ByRef model As String) Implements IPwc83018.GetEngineModel '''''''''''''''''''''''''''
            Dim engineModelFor As StringBuilder = New StringBuilder(20)

            PWC83018GETENGINEMODEL(engineModelFor, engineModelFor.Capacity())
            model = engineModelFor.ToString().Substring(0, 20).Trim()
        End Sub

        Public Sub GetResult(label As String, ByRef value As Double) Implements IPwc83018.GetResult '''''''''''''''''''''''''''
            PWC83018GETRESULT(label, label.Length(), value)
        End Sub

        Public Sub GetSummary(ByRef summary As Boolean) Implements IPwc83018.GetSummary '''''''''''''''''''''''''''
            PWC83018GETSUMMARY(summary)
        End Sub

        Public Sub GetUnitData() Implements IPwc83018.GetUnitData '''''''''''''''''''''''''''
            PWC83018GETUNITDATA()
        End Sub

        Public Function GetRealValue(label As String, ByRef value As String, index As String) As Boolean Implements IPwc83018.GetRealValue '''''''''''''''''''''''''''
            Dim result As Boolean

            result = PWC83018GETREALVALUE(label, label.Length(), value, index, index.Length())

            GetRealValue = result
        End Function

        Public Function GetValue(label As String, ByRef value As String, index As String) As Boolean Implements IPwc83018.GetValue '''''''''''''''''''''''''''
            Dim result As Boolean

            result = PWC83018GETVALUE(label, label.Length(), value, value.Length, index, index.Length())

            GetValue = result
        End Function

        Public Sub LoadModel(ByRef errorNo As Integer, Model As String, Optional ByVal SB1 As String = "        ", Optional ByVal SB2 As String = "        ") Implements IPwc83018.LoadModel '''''''''''''''''''''''''''''''
            PWC83018LOADMODEL(errorNo, Model.PadRight(20), Model.Length, SB1, SB1.Length, SB2, SB2.Length)
        End Sub

        Public Sub NextCond(ByRef errorNo As Integer) Implements IPwc83018.NextCond '''''''''''''''''''''''''''
            PWC83018NEXTCOND(errorNo)
        End Sub

        Public Sub Process(ByRef errorNo As Integer, save As Integer, output As Integer) Implements IPwc83018.Process '''''''''''''''''''''''''''
            PWC83018PROCESS(errorNo, save, output)
        End Sub

        Public Sub ProcessTorqueTrim(ByRef errorNo As Integer, Dyno As Single(), Torque As Single(), Torque2 As Single(), output As Integer) Implements IPwc83018.ProcessTorqueTrim '''''''''''''''''''''''''''

            ReDim Preserve Dyno(50)
            ReDim Preserve Torque(50)
            ReDim Preserve Torque2(50)

            PWC83018PROCESSTORQUETRIM(errorNo, Dyno, _
                                       Torque, Torque2, _
                                       output)

        End Sub

        Public Function PutUnitValue(label As String, unit As String, index As String) As Boolean Implements IPwc83018.PutUnitValue '''''''''''''''''''''''''''
            PutUnitValue = PWC83018PUTUNITVALUE(label, label.Length, unit, unit.Length, index, index.Length)
        End Function

        Public Function PutValue(label As String, value As String, index As String) As Boolean Implements IPwc83018.PutValue '''''''''''''''''''''''''''
            PutValue = PWC83018PUTVALUE(label, label.Length, value, value.Length, index, index.Length)
        End Function

        Public Sub ResetCommon() Implements IPwc83018.ResetCommon
            PWC83018RESETCOMMON()
        End Sub

        Public Sub ResetM() Implements IPwc83018.ResetM
            PWC83018RESETM()
        End Sub

        Public Sub ResetTorque() Implements IPwc83018.ResetTorque
            PWC83018RESETTORQUE()
        End Sub

        Public Sub SaveData(ByRef errorNo As Integer) Implements IPwc83018.SaveData '''''''''''''''''''''''''''
            PWC83018SAVEDATA(errorNo)
        End Sub

        Public Sub SaveUnitData() Implements IPwc83018.SaveUnitData
            PWC83018SAVEUNITDATA()
        End Sub

        Public Sub SetBuild(buildNo As Integer) Implements IPwc83018.SetBuild '''''''''''''''''''''''''''
            PWC83018SETBUILD(buildNo)
        End Sub

        Public Sub SetCondition(conditionNo As Integer) Implements IPwc83018.SetCondition '''''''''''''''''''''''''''
            PWC83018SETCONDITION(conditionNo)
        End Sub

        Public Sub SetConfigurationPath(path As String) Implements IPwc83018.SetConfigurationPath '''''''''''''''''''''''''''
            PWC83018SETCONFIGURATIONPATH(path, path.Length)
        End Sub

        Public Sub SetDataPath(path As String) Implements IPwc83018.SetDataPath '''''''''''''''''''''''''''
            PWC83018SETDATAPATH(path, path.Length)
        End Sub

        Public Sub SetEngine(engineSerialNo As String) Implements IPwc83018.SetEngine '''''''''''''''''''''''''''
            PWC83018SETENGINE(engineSerialNo, engineSerialNo.Length)
        End Sub

        Public Sub SetSummary(summary As Boolean) Implements IPwc83018.SetSummary '''''''''''''''''''''''''''
            PWC83018SETSUMMARY(summary)
        End Sub

    End Class

End Namespace


Imports PwcSharedInterface
Imports System.Text
Imports System.Runtime.InteropServices

Namespace PwcService

    '********************************************************************
    '********************************************************************
    '* PW83025 integrates PWC PW150A ENGINE *****************************
    '********************************************************************
    '********************************************************************

    Public NotInheritable Class Pwc83025Service
        Implements IPwc83025

        '********************************************************************
        '* Private Declare **************************************************
        '********************************************************************

#Region "Declare"

        Private Declare Ansi Sub PWC83025CREATEVANEAREA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByRef iVane As Integer, ByRef iRes() As Integer, ByRef vArea As Single)

        Private Declare Ansi Sub PWC83025DEFINE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" ()

        Private Declare Ansi Sub PWC83025GETBUILD _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByRef iBuild As Integer)

        Private Declare Ansi Sub PWC83025GETBUILDSPEC _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByRef iBuildSpec As Integer)

        Private Declare Ansi Sub PWC83025GETCONDITION _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByRef iCondition As Integer)

        Private Declare Ansi Sub PWC83025GETDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" ()

        Private Declare Ansi Sub PWC83025GETENGINE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (<[In], Out> ByVal cEngine As StringBuilder, ByVal cEngineSize As Integer)

        Private Declare Ansi Sub PWC83025GETENGINEMODEL _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (<[In], Out> ByVal cModel As StringBuilder, ByVal cEngineSize As Integer)

        Private Declare Ansi Sub PWC83025GETR3R4 _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByRef iRes() As Integer)

        Private Declare Ansi Sub PWC83025GETRESULT _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, _
             ByRef rResult As Single)

        Private Declare Ansi Sub PWC83025GETSTOCKNUMBER _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByRef iVaneRange As Integer, <[In], Out> ByVal cStock As StringBuilder, ByVal cStockSize As Integer)

        Private Declare Ansi Sub PWC83025GETSUMMARY _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByRef iUpdateSummary As Integer)

        Private Declare Ansi Sub PWC83025GETUNITDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" ()

        Private Declare Ansi Function PWC83025GETREALVALUE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, _
             ByRef fValue As Single, _
             ByVal cIndex As String, ByVal cIndexLen As Integer) As Boolean

        Private Declare Ansi Sub PWC83025GETTORQUETRIMERROR _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByRef iError() As Integer)

        Private Declare Ansi Sub PWC83025GETTORQUETRIMSTATUS _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByRef iStatus() As Integer)

        Private Declare Ansi Function PWC83025GETVALUE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, _
             <[In], Out> ByVal cValue As StringBuilder, ByVal cValueLen As Integer, _
             ByVal cIndex As String, ByVal cIndexLen As Integer) As Boolean

        Private Declare Ansi Sub PWC83025GETVANEAREA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByRef iVane As Integer, ByRef vArea As Single)

        Private Declare Ansi Sub PWC83025GETVANERANGE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByRef IVaneRange As Integer, ByRef iRange() As Integer)

        Private Declare Ansi Sub PWC83025INITIALIZE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" ()

        Private Declare Ansi Sub PWC83025LOADMODEL _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByRef iError As Integer, ByRef iBuildSpec As Integer, ByRef iVaneCode As Integer)

        Private Declare Ansi Sub PWC83025PROCESS _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByRef iError As Integer, _
             ByRef iSave As Integer, ByRef iOutput As Integer)

        Private Declare Ansi Sub PWC83025PROCESSTORQUETRIM _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByRef iOutput As Integer, ByRef iTMTolDouble As Integer, _
             ByRef rDynoTmTO As Single, ByRef rDynoTm50PC As Single, _
             ByRef rEECTO As Single, ByRef rEEEC50PC As Single, _
             ByRef rResGain As Single, ByRef rResBias As Single, _
             ByRef iError As Integer)

        Private Declare Ansi Function PWC83025PUTUNITVALUE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, _
             ByVal cUnit As String, ByVal cUnitLen As Integer, _
             ByVal cIndex As String, ByVal cIndexLen As Integer) As Boolean

        Private Declare Ansi Function PWC83025PUTVALUE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, _
             ByVal cValue As String, ByVal cValueLen As Integer, _
             ByVal cIndex As String, ByVal cIndexLen As Integer) As Boolean

        Private Declare Ansi Sub PWC83025SAVEDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByRef iError As Integer)

        Private Declare Ansi Sub PWC83025SAVEUNITDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" ()

        Private Declare Ansi Sub PWC83025SETBUILD _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByRef iBuild As Integer)

        Private Declare Ansi Sub PWC83025SETCONDITION _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByRef iCondition As Integer)

        Private Declare Ansi Sub PWC83025SETCONFIGURATIONPATH _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByVal cPath As String, ByVal cPathLen As Integer)

        Private Declare Ansi Sub PWC83025SETDATAPATH _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByVal cPath As String, ByVal cPathLen As Integer)

        Private Declare Ansi Sub PWC83025SETENGINE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByVal cEngine As String, ByVal cEngineLen As Integer)

        Private Declare Ansi Sub PWC83025SETSUMMARY _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83025\exe\PWC83025.dll" _
            (ByRef iUpdateSummary As Integer)


#End Region

        Private Sub New()
        End Sub

        '********************************************************************
        '* Public Shared ****************************************************
        '********************************************************************

        Public Sub CreateVaneArea(ByRef iVane As Integer, ByRef iRes() As Integer, ByRef vArea As Single) Implements IPwc83025.CreateVaneArea '''''''''''''''''''''''''''

            If iVane = 1 Then
                ReDim Preserve iRes(6)
            ElseIf iVane = 2 Then
                ReDim Preserve iRes(7)
            Else
                'In order to make sure no other values are sent except 1 or 2
                iVane = 1
                ReDim Preserve iRes(6)
            End If

            PWC83025CREATEVANEAREA(iVane, iRes, vArea)
        End Sub
        Public Sub Define() Implements IPwc83025.Define '''''''''''''''''''''''''''
            PWC83025DEFINE()
        End Sub

        Public Sub GetBuild(ByRef buildNumber As Integer) Implements IPwc83025.GetBuild '''''''''''''''''''''''''''
            PWC83025GETBUILD(buildNumber)
        End Sub

        Public Sub GetBuildSpec(ByRef buildSpec As Integer) Implements IPwc83025.GetBuildSpec '''''''''''''''''''''''''''
            PWC83025GETBUILDSPEC(buildSpec)
        End Sub

        Public Sub GetCondition(ByRef iCondition As Integer) Implements IPwc83025.GetCondition '''''''''''''''''''''''''''
            PWC83025GETCONDITION(iCondition)
        End Sub

        Public Sub GetData() Implements IPwc83025.GetData '''''''''''''''''''''''''''
            PWC83025GETDATA()
        End Sub

        Public Sub GetEngine(ByRef engineSerialNumber As String) Implements IPwc83025.GetEngine ''''''''''''''''''''''''''
            Dim engineSNFor As StringBuilder = New StringBuilder(8)

            PWC83025GETENGINE(engineSNFor, engineSNFor.Capacity())
            engineSerialNumber = engineSNFor.ToString().Substring(0, 8).Trim()
        End Sub

        Public Sub GetEngineModel(ByRef model As String) Implements IPwc83025.GetEngineModel '''''''''''''''''''''''''''
            Dim engineModelFor As StringBuilder = New StringBuilder(8)

            PWC83025GETENGINEMODEL(engineModelFor, engineModelFor.Capacity())
            model = engineModelFor.ToString().Substring(0, 8).Trim()
        End Sub

        Public Sub GetR3R4(ByRef iRes() As Integer) Implements IPwc83025.GetR3R4 '''''''''''''''''''''''''''

            ReDim Preserve iRes(2)

            PWC83025GETR3R4(iRes)
        End Sub

        Public Sub GetResult(label As String, ByRef value As Single) Implements IPwc83025.GetResult '''''''''''''''''''''''''''
            PWC83025GETRESULT(label, label.Length(), value)
        End Sub

        Public Sub GetSummary(ByRef summary As Boolean) Implements IPwc83025.GetSummary '''''''''''''''''''''''''''
            PWC83025GETSUMMARY(summary)
        End Sub

        Public Sub GetUnitData() Implements IPwc83025.GetUnitData '''''''''''''''''''''''''''
            PWC83025GETUNITDATA()
        End Sub

        Public Function GetRealValue(label As String, ByRef value As String, index As String) As Boolean Implements IPwc83025.GetRealValue '''''''''''''''''''''''''''
            Dim result As Boolean

            result = PWC83025GETREALVALUE(label, label.Length(), value, index, index.Length())

            GetRealValue = result
        End Function

        Public Sub GetStockNumber(ByRef jIndx As Integer, ByRef CStock As String) Implements IPwc83025.GetStockNumber '''''''''''''''''''''''''''

            Dim cStockFor As StringBuilder = New StringBuilder(10)

            PWC83025GETSTOCKNUMBER(jIndx, cStockFor, cStockFor.Capacity)

            CStock = cStockFor.ToString().Substring(0, 10).Trim()

        End Sub

        Public Sub GetTorqueTrimError(ByRef iError() As Integer) Implements IPwc83025.GetTorqueTrimError '''''''''''''''''''''''''''

            ReDim Preserve iError(2)

            PWC83025GETTORQUETRIMERROR(iError)
        End Sub

        Public Sub GetTorqueTrimStatus(ByRef iStatus() As Integer) Implements IPwc83025.GetTorqueTrimStatus '''''''''''''''''''''''''''

            ReDim Preserve iStatus(2)

            PWC83025GETTORQUETRIMSTATUS(iStatus)
        End Sub

        Public Function GetValue(label As String, ByRef value As String, index As String) As Boolean Implements IPwc83025.GetValue '''''''''''''''''''''''''''
            Dim result As Boolean
            Dim valueFor As StringBuilder = New StringBuilder(25)

            result = PWC83025GETVALUE(label, label.Length(), valueFor, valueFor.Capacity, index, index.Length())

            value = valueFor.ToString().Trim()
            GetValue = result
        End Function

        Public Sub GetVaneRange(ByRef iVaneRange As Integer, ByRef rRange() As Integer) Implements IPwc83025.GetVaneRange '''''''''''''''''''''''''''

            ReDim Preserve rRange(2)

            PWC83025GETVANERANGE(iVaneRange, rRange)
        End Sub

        Public Sub GetVaneArea(ByRef iVane As Integer, ByRef rVane As Single) Implements IPwc83025.GetVaneArea '''''''''''''''''''''''''''

            If iVane <> 1 Or iVane <> 2 Then
                iVane = 1
            End If

            PWC83025GETVANEAREA(iVane, rVane)
        End Sub


        Public Sub Initialize() Implements IPwc83025.Initialize '''''''''''''''''''''''''''
            PWC83025INITIALIZE()
        End Sub

        Public Sub LoadModel(ByRef errorNo As Integer, buildSpec As Integer, vanecode As Integer) Implements IPwc83025.LoadModel '''''''''''''''''''''''''''''''
            PWC83025LOADMODEL(errorNo, buildSpec, vanecode)
        End Sub

        Public Sub Process(ByRef errorNo As Integer, save As Integer, output As Integer) Implements IPwc83025.Process '''''''''''''''''''''''''''
            PWC83025PROCESS(errorNo, save, output)
        End Sub

        Public Sub ProcessTorqueTrim(output As Boolean, iTMTolDouble As Integer, rDynoTmTO As Single, rDynoTm50PC As Single, rEECTO As Single, rEEC50PC As Single, rResGain As Single, rResBias As Single, _
              iErr As Integer) Implements IPwc83025.ProcessTorqueTrim '''''''''''''''''''''''''''

            PWC83025PROCESSTORQUETRIM(output, iTMTolDouble, _
                                       rDynoTmTO, rDynoTm50PC, _
                                       rEECTO, rEEC50PC, _
                                       rResGain, rResBias, _
                                       iErr)

        End Sub

        Public Function PutUnitValue(label As String, unit As String, index As String) As Boolean Implements IPwc83025.PutUnitValue '''''''''''''''''''''''''''
            PutUnitValue = PWC83025PUTUNITVALUE(label, label.Length, unit, unit.Length, index, index.Length)
        End Function

        Public Function PutValue(label As String, value As String, index As String) As Boolean Implements IPwc83025.PutValue '''''''''''''''''''''''''''
            PutValue = PWC83025PUTVALUE(label, label.Length, value, value.Length, index, index.Length)
        End Function

        Public Sub SaveData(ByRef errorNo As Integer) Implements IPwc83025.SaveData '''''''''''''''''''''''''''
            PWC83025SAVEDATA(errorNo)
        End Sub

        Public Sub SaveUnitData() Implements IPwc83025.SaveUnitData
            PWC83025SAVEUNITDATA()
        End Sub

        Public Sub SetBuild(buildNo As Integer) Implements IPwc83025.SetBuild '''''''''''''''''''''''''''
            PWC83025SETBUILD(buildNo)
        End Sub

        Public Sub SetCondition(conditionNo As Integer) Implements IPwc83025.SetCondition '''''''''''''''''''''''''''
            PWC83025SETCONDITION(conditionNo)
        End Sub

        Public Sub SetConfigurationPath(path As String) Implements IPwc83025.SetConfigurationPath '''''''''''''''''''''''''''
            PWC83025SETCONFIGURATIONPATH(path, path.Length)
        End Sub

        Public Sub SetDataPath(path As String) Implements IPwc83025.SetDataPath '''''''''''''''''''''''''''
            PWC83025SETDATAPATH(path, path.Length)
        End Sub

        Public Sub SetEngine(engineSerialNo As String) Implements IPwc83025.SetEngine '''''''''''''''''''''''''''
            PWC83025SETENGINE(engineSerialNo, engineSerialNo.Length)
        End Sub

        Public Sub SetSummary(summary As Boolean) Implements IPwc83025.SetSummary '''''''''''''''''''''''''''
            PWC83025SETSUMMARY(summary)
        End Sub

    End Class

End Namespace


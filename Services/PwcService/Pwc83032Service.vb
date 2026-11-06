Imports PwcSharedInterface
Imports System.Text
Imports System.Runtime.InteropServices

Namespace PwcService

    '********************************************************************
    '********************************************************************
    '* PW83032 integrates PWC PW980A ENGINE *****************************
    '********************************************************************
    '********************************************************************

    Public NotInheritable Class Pwc83032Service
        Implements IPwc83032

        '********************************************************************
        '* Private Declare **************************************************
        '********************************************************************

#Region "Declare"

        Private Declare Ansi Sub PWC83032CALON _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            ()

        Private Declare Ansi Sub PWC83032CLOSEOUTPUTDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" ()

        Private Declare Ansi Sub PWC83032GETALLRESULTS _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (ByRef cHeaders() As String, ByRef cHeadersLen As Integer, _
             ByRef PlotVals() As Double, ByRef nVals As Integer)

        Private Declare Ansi Sub PWC83032GETBUILD _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (ByRef iBuild As Integer)

        Private Declare Ansi Sub PWC83032GETBUILDSPEC _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (ByRef iBuildSpec As Integer)

        Private Declare Ansi Sub PWC83032GETCONDITION _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (ByRef iCondition As Integer)

        Private Declare Ansi Sub PWC83032GETDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" ()

        Private Declare Ansi Sub PWC83032GETENGINE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (<[In], Out> ByVal cEngine As StringBuilder, ByVal cEngineSize As Integer)

        Private Declare Ansi Sub PWC83032GETENGINEMODEL _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (<[In], Out> ByVal cModel As StringBuilder, ByVal cEngineSize As Integer)

        Private Declare Ansi Sub PWC83032GETRESULT _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, _
             ByRef rResult As Single)

        Private Declare Ansi Sub PWC83032GETUNITDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (ByRef iError As Integer)


        Private Declare Ansi Function PWC83032GETVALUE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, _
             <[In], Out> ByVal cValue As StringBuilder, ByVal cValueLen As Integer, _
             ByVal cIndex As String, ByVal cIndexLen As Integer) As Boolean

        Private Declare Ansi Sub PWC83032INITIALIZE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" ()

        Private Declare Ansi Sub PWC83032LOADBUILDSPEC _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (ByRef iError As Integer, _
             ByVal cBuildSpec As String, ByVal CBuildSpecLen As Integer, _
             ByVal SB1 As String, ByVal SB1Len As Integer, _
             ByVal SB2 As String, ByVal SB2Len As Integer)

        Private Declare Ansi Sub PWC83032OUTPUTDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" ()

        Private Declare Ansi Sub PWC83032PROCESS _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (ByRef iError As Integer, _
             ByRef iSave As Integer, ByRef iOutput As Integer)

        Private Declare Ansi Sub PWC83032PROCESSDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (ByRef iRet As Integer)

        Private Declare Ansi Function PWC83032PUTUNITVALUE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, _
             ByVal cValue As String, ByVal cValueLen As Integer) As Boolean

        Private Declare Ansi Function PWC83032PUTVALUE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (ByVal cLabel As String, ByVal cLabelLen As Integer, _
             ByVal cValue As String, ByVal cValueLen As Integer, _
             ByVal cIndex As String, ByVal cIndexLen As Integer) As Boolean

        Private Declare Ansi Sub PWC83032SAVEDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (ByRef iError As Integer)

        Private Declare Ansi Sub PWC83032SAVEUNITDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (ByRef iError As Integer)

        Private Declare Ansi Sub PWC83032SETBUILD _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (ByRef iBuild As Integer)

        Private Declare Ansi Sub PWC83032SETCAL _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (ByRef iCal As Integer)

        Private Declare Ansi Sub PWC83032SETCONDITION _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (ByRef iCondition As Integer)

        Private Declare Ansi Sub PWC83032SETCONFIGURATIONPATH _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (ByVal cPath As String, ByVal cPathLen As Integer)

        Private Declare Ansi Sub PWC83032SETDATAPATH _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (ByVal cPath As String, ByVal cPathLen As Integer)

        Private Declare Ansi Sub PWC83032SETENGINE _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (ByVal cEngine As String, ByVal cEngineLen As Integer)

        Private Declare Ansi Sub PWC83032SETSTATUS _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (ByVal cStatus As String, ByVal cStatusLen As Integer)

        Private Declare Ansi Sub PWC83032TRANSFERDATA _
            Lib "C:\Program Files\Pratt & Whitney Canada\PWC83032\exe\PWC83032DLL.dll" _
            (ByRef iRet As Integer)

#End Region

        Private Sub New()
        End Sub

        '********************************************************************
        '* Public Shared ****************************************************
        '********************************************************************

        Public Sub CalOn() Implements IPwc83032.CalOn
            PWC83032CALON()
        End Sub

        Public Sub CloseOutputData() Implements IPwc83032.CloseOutputData
            PWC83032CLOSEOUTPUTDATA()
        End Sub

        Public Sub GetAllResults(cHeaders() As String, ByRef PlotVal() As Double, ByRef nVals As Integer) Implements IPwc83032.GetAllResults
            PWC83032GETALLRESULTS(cHeaders, cHeaders.Length(), PlotVal, nVals)
        End Sub

        Public Sub GetBuild(ByRef buildNumber As Integer) Implements IPwc83032.GetBuild
            PWC83032GETBUILD(buildNumber)
        End Sub

        Public Sub GetBuildSpec(ByRef buildSpec As Integer) Implements IPwc83032.GetBuildSpec
            PWC83032GETBUILDSPEC(buildSpec)
        End Sub

        Public Sub GetCondition(ByRef iCondition As Integer) Implements IPwc83032.GetCondition
            PWC83032GETCONDITION(iCondition)
        End Sub

        Public Sub GetData() Implements IPwc83032.GetData
            PWC83032GETDATA()
        End Sub

        Public Sub GetEngine(ByRef engineSerialNumber As String) Implements IPwc83032.GetEngine
            Dim engineSNFor As StringBuilder = New StringBuilder(8)

            PWC83032GETENGINE(engineSNFor, engineSNFor.Capacity())
            engineSerialNumber = engineSNFor.ToString().Substring(0, 8).Trim()
        End Sub

        Public Sub GetEngineModel(ByRef model As String) Implements IPwc83032.GetEngineModel
            Dim engineModelFor As StringBuilder = New StringBuilder(8)

            PWC83032GETENGINEMODEL(engineModelFor, engineModelFor.Capacity())
            model = engineModelFor.ToString().Substring(0, 8).Trim()
        End Sub

        Public Function GetNumberT8Probes(engineModel As String) As Integer Implements IPwc83032.GetNumberT8Probes
            Dim numberT8Probes As Integer = 6

            If (engineModel.StartsWith("PW115") Or _
                 engineModel.StartsWith("PW116") Or _
                 engineModel.StartsWith("PW118") Or _
                 engineModel.StartsWith("PW120") Or _
                 engineModel.StartsWith("PW121")) Then

                numberT8Probes = 8
            End If

            Return numberT8Probes
        End Function

        Public Sub GetResult(label As String, ByRef value As Single) Implements IPwc83032.GetResult
            PWC83032GETRESULT(label, label.Length(), value)
        End Sub

        Public Sub GetUnitData(ByRef errorNo As Integer) Implements IPwc83032.GetUnitData
            PWC83032GETUNITDATA(errorNo)
        End Sub

        Public Function GetValue(label As String, ByRef value As String, index As String) As Boolean Implements IPwc83032.GetValue
            Dim result As Boolean
            Dim valueFor As StringBuilder = New StringBuilder(25)

            result = PWC83032GETVALUE(label, label.Length(), valueFor, valueFor.Capacity, index, index.Length())

            value = valueFor.ToString().Trim()
            GetValue = result
        End Function

        Public Sub Initialize() Implements IPwc83032.Initialize
            PWC83032INITIALIZE()
        End Sub

        Public Sub LoadBuildSpec(ByRef errorNo As Integer, buildspec As String, SB1 As String, SB2 As String) Implements IPwc83032.LoadBuildSpec
            PWC83032LOADBUILDSPEC(errorNo, buildspec, buildspec.Length, SB1, SB1.Length, SB2, SB2.Length)
        End Sub

        Public Sub OutputData() Implements IPwc83032.OutputData
            PWC83032OUTPUTDATA()
        End Sub

        Public Sub Process(ByRef errorNo As Integer, save As Integer, output As Integer) Implements IPwc83032.Process
            PWC83032PROCESS(errorNo, save, output)
        End Sub

        Public Sub ProcessData(ByRef iError As Integer) Implements IPwc83032.ProcessData
            PWC83032PROCESSDATA(iError)
        End Sub

        Public Function PutUnitValue(label As String, value As String) As Boolean Implements IPwc83032.PutUnitValue
            PutUnitValue = PWC83032PUTUNITVALUE(label, label.Length, value, value.Length)
        End Function

        Public Function PutValue(label As String, value As String, index As String) As Boolean Implements IPwc83032.PutValue
            PutValue = PWC83032PUTVALUE(label, label.Length, value, value.Length, index, index.Length)
        End Function

        Public Sub SaveData(ByRef errorNo As Integer) Implements IPwc83032.SaveData
            PWC83032SAVEDATA(errorNo)
        End Sub

        Public Sub SaveUnitData(ByRef errorNo As Integer) Implements IPwc83032.SaveUnitData
            PWC83032SAVEUNITDATA(errorNo)
        End Sub

        Public Sub SetBuild(buildNo As Integer) Implements IPwc83032.SetBuild
            PWC83032SETBUILD(buildNo)
        End Sub

        Public Sub SetCal(calNo As Integer) Implements IPwc83032.SetCal
            PWC83032SETCAL(calNo)
        End Sub

        Public Sub SetCondition(conditionNo As Integer) Implements IPwc83032.SetCondition
            PWC83032SETCONDITION(conditionNo)
        End Sub

        Public Sub SetConfigurationPath(path As String) Implements IPwc83032.SetConfigurationPath
            PWC83032SETCONFIGURATIONPATH(path, path.Length)
        End Sub

        Public Sub SetDataPath(path As String) Implements IPwc83032.SetDataPath
            PWC83032SETDATAPATH(path, path.Length)
        End Sub

        Public Sub SetEngine(engineSerialNo As String) Implements IPwc83032.SetEngine
            PWC83032SETENGINE(engineSerialNo, engineSerialNo.Length)
        End Sub

        Public Sub SetStatus(Status As String) Implements IPwc83032.SetStatus
            PWC83032SETSTATUS(Status, Status.Length)
        End Sub

        Public Sub TransferData(ByRef iError As Integer) Implements IPwc83032.TransferData
            PWC83032TRANSFERDATA(iError)
        End Sub
    End Class

End Namespace


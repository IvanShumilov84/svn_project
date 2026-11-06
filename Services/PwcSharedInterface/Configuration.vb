Imports System.ServiceModel
Imports System.ServiceModel.Channels

Namespace PwcSharedInterface

    Public Module Configuration

        Function CreateServerUriString() As String
            Return "http://localhost:8082/Pwc"
        End Function

        Function CreateBinding() As Binding
            Return New WSHttpBinding()
        End Function
    End Module
End Namespace

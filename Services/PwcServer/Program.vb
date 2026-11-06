Imports PwcSharedInterface
Imports PwcService
Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports System.ServiceModel.Description
Imports System.Windows.Forms

Module Program

    Sub Main()
        Dim baseAddress As Uri = New Uri(PwcSharedInterface.Configuration.CreateServerUriString())
        Dim arguments() As String = System.Environment.GetCommandLineArgs()
        Dim ServerType As String = ""
        Dim ArgumentIsNotSupported As Boolean = False
        Dim ServiceType As Type = Nothing
        Dim InterfaceType As Type = Nothing

        If arguments.GetUpperBound(0) > 0 And arguments.GetUpperBound(0) < 2 Then
            Dim i As Integer = 0
            For Each argument As String In arguments
                If argument.ToLower = "/pwc83000" Then
                    ServiceType = GetType(Pwc83000Service)
                    InterfaceType = GetType(IPwc83000)
                    ServerType = argument.ToLower
                    ArgumentIsNotSupported = False
                ElseIf argument.ToLower = "/pwc83018" Then
                    ServiceType = GetType(Pwc83018Service)
                    InterfaceType = GetType(IPwc83018)
                    ServerType = argument.ToLower
                    ArgumentIsNotSupported = False
                ElseIf argument.ToLower = "/pwc83020" Then
                    ServiceType = GetType(Pwc83020Service)
                    InterfaceType = GetType(IPwc83020)
                    ServerType = argument.ToLower
                    ArgumentIsNotSupported = False
                ElseIf argument.ToLower = "/pwc83025" Then
                    ServiceType = GetType(Pwc83025Service)
                    InterfaceType = GetType(IPwc83025)
                    ServerType = argument.ToLower
                    ArgumentIsNotSupported = False
                ElseIf argument.ToLower = "/pwc83032" Then
                    ServiceType = GetType(Pwc83032Service)
                    InterfaceType = GetType(IPwc83032)
                    ServerType = argument.ToLower
                    ArgumentIsNotSupported = False
                ElseIf argument.ToLower = "/pwc83040" Then
                    ServiceType = GetType(Pwc83040Service)
                    InterfaceType = GetType(IPwc83040)
                    ServerType = argument.ToLower
                    ArgumentIsNotSupported = False
                Else
                    If i > 0 Then
                        Console.WriteLine("The following argument is invalid or not supported: " + argument.ToLower())
                    End If
                    ArgumentIsNotSupported = True
                End If
                i += 1
            Next

            If ArgumentIsNotSupported = False Then
                Try
                    Using host As New ServiceHost(ServiceType, baseAddress)
                        'Enable metadata publishing.
                        Dim smb As New ServiceMetadataBehavior()
                        smb.HttpGetEnabled = True
                        smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15
                        host.Description.Behaviors.Add(smb)

                        Dim binding As System.ServiceModel.Channels.Binding = PwcSharedInterface.Configuration.CreateBinding()
                        binding.ReceiveTimeout = System.TimeSpan.FromHours(72)
                        binding.OpenTimeout = System.TimeSpan.FromMinutes(2) 'new
                        binding.CloseTimeout = System.TimeSpan.FromMinutes(2) 'new
                        binding.SendTimeout = System.TimeSpan.FromMinutes(2) 'new
                        host.AddServiceEndpoint(InterfaceType, binding, baseAddress)

                        host.Open()

                        Console.WriteLine("The service is ready at {0}", baseAddress)
                        Console.WriteLine("Configured for " & ServerType)
                        Console.WriteLine("Press <Enter> to stop the service.")
                        Console.ReadLine()

                        ' Close the ServiceHost.
                        host.Close()

                    End Using
                Catch timeout As TimeoutException
                    'MessageBox.Show("Program.Main() Timeout Exception: " & timeout.Message, "Timeout", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Console.WriteLine("Program.Main() Timeout Exception: " & timeout.Message)
                Catch commException As CommunicationException
                    'MessageBox.Show("Program.Main() Communication Exception: " & commException.Message, "Timeout", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Console.WriteLine("Program.Main() Communication Exception: " & commException.Message)
                Catch ex As Exception
                    MessageBox.Show("PwcServer: Error : Make sure no process/services like NIApplicationWebServer Service are using port specified in " & baseAddress.ToString & vbCrLf & vbCrLf & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    MessageBox.Show("Use command prompt and type netstat -ano | grep 8080 to diagnose the PID of the service causing this problem")
                
                End Try
            Else
                Console.WriteLine("Invalid Argument : Cannot Start Server. Argument not supported.")
                Console.WriteLine("Argument that are currently supported : pwc83000, pwc83018, pwc83020, pwc83025, pwc83032 or pwc83040")
                Console.WriteLine("ex : PwcServer /pwc83032")
                Console.WriteLine("Press <Enter> to stop the program.")
                Console.ReadLine()
            End If
        Else
            Console.WriteLine("Invalid Argument : Cannot Start Server. No or Too much arguments.")
            Console.WriteLine("Specify Only One Argument that are currently supported : pwc83000, pwc83018, pwc83020, pwc83025, pwc83032 or pwc83040.")
            Console.WriteLine("ex : PwcServer /pwc83032")
            Console.WriteLine("Press <Enter> to stop the program.")
            Console.ReadLine()
        End If

    End Sub

End Module

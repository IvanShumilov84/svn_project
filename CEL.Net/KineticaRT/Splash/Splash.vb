Imports System.Threading
Imports System.Windows.Forms



Public NotInheritable Class Splash
    Implements ISplash
    'TODO: This form can easily be set as the splash screen for the application by going to the "Application" tab
    '  of the Project Designer ("Properties" under the "Project" menu).

    Private Sub SplashScreen1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Set up the dialog text at runtime according to the application's assembly information.  

        'TODO: Customize the application's assembly information in the "Application" pane of the project 
        '  properties dialog (under the "Project" menu).

        'Application title
        '' ''If My.Application.Info.Title <> "" Then
        '' ''    ApplicationTitle.Text = My.Application.Info.Title
        '' ''Else
        '' ''    'If the application title is missing, use the application name, without the extension
        '' ''    ApplicationTitle.Text = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        '' ''End If
        ApplicationTitle.Text = "CELDAS 200™"

        'Format the version information using the text set into the Version control at design time as the
        '  formatting string.  This allows for effective localization if desired.
        '  Build and revision information could be included by using the following code and changing the 
        '  Version control's designtime text to "Version {0}.{1:00}.{2}.{3}" or something similar.  See
        '  String.Format() in Help for more information.
        '
        '    Version.Text = System.String.Format(Version.Text, My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision)

        'Version.Text = System.String.Format(Version.Text, My.Application.Info.Version.Major, My.Application.Info.Version.Minor)
        Version.Text = ""

        'Copyright info
        'Copyright.Text = My.Application.Info.Copyright
        Copyright.Text = "© " + Now.Year.ToString + " CEL Aerospace Test Equipment Ltd."

        Me.Location = New Drawing.Point(625, 50)

    End Sub
    Public Shadows Sub Show() Implements ISplash.Show
        MyBase.Show()
    End Sub
    Public Shadows Sub Hide() Implements ISplash.Hide
        MyBase.Hide()
    End Sub
    Public Sub SetText(ByVal Text As String) Implements ISplash.SetText
        Me.Refresh()
    End Sub

    Public Sub SetProgress(ByVal Text As String) Implements ISplash.SetProgress
        lblProgress.Text = Text
        Me.Refresh()
    End Sub
    Public Sub SetSubProgress(ByVal Text As String) Implements ISplash.SetSubProgress
        lblSubProgress.Text = Text
        Me.Refresh()
    End Sub
    Public Shadows Sub Focus() Implements ISplash.Focus
        Me.Refresh()
        If MyBase.CanFocus Then
            MyBase.Focus()
        End If

    End Sub


End Class

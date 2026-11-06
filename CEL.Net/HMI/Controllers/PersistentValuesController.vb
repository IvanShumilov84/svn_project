Imports System
Imports System.IO
Imports System.Windows.Forms
Imports System.DateTime
Imports System.Runtime.Remoting.Contexts

Imports KineticaRT.Channels
Imports KineticaRT.Values

Namespace CEL.HMI

    <Synchronization>
    Public Class PersistentValuesController 'Persistent Values Manager

        'Pour utiliser ce contrôleur, on doit passer 3 paramètres : un tableau de string pour les channels à enregistrer, l'intervalle de sauvegarde et le full path filename pour la sauvegarde.
        'Le contrôleur procedera ensuite à tenter de lire un fichier deja existant ; si non disponible, initialisera un dictionnaire avec les paramètres a sauvergarder avec les valeurs à zero. 
        'Pour la suite de l'integration, il faut 'appeler la fonction publique dans la worker thread pour la sauvegarde. Lors de la sauvegarde, le dictionnaire contenant les dernières valeurs 
        'sera sérializé. L'autre methode publique est pour resetté les channels a être enregistré dans le fichier des valeurs persistentes.

        Implements IDisposable

        Private CELProject As CELDAS = CELDAS.Instance()

        Private _Sequence As CEL.KineticaRT.Sequence.Sequence

        Private ReadOnly KINETICA_NOT_CONNECTED As New Global.KineticaRT.Values.Text("Not Connected")
        Private ReadOnly KINETICA_FALSE As New Global.KineticaRT.Values.Boolean(False)
        Private ReadOnly KINETICA_STRING_EMPTY As New Global.KineticaRT.Values.Text("")
        Private ReadOnly KINETICA_DOUBLE_ZERO As New Global.KineticaRT.Values.Double(0.0)

        Private _KineticaRTChannels As IChannel()
        Private _ChannelIsBoolean As IBooleanChannel
        Private _ChannelIsText As ITextChannel
        Private _ChannelIsNumeric As INumericChannel
        Private _i As Integer = 0
        Private _Channels As String()
        Private _MinutesBetweenSave As Integer
        Private _FilePath As String
        Private _PersistentValues As New CEL.SerializableDictionary(Of String, String)
        Private _PreviousTime As Date = Now
        Private _NextTimeToSave As Date = Now.AddMinutes(_MinutesBetweenSave) ' delay between save value

        ''' <summary>
        ''' Persistent Values Manager
        ''' </summary>
        ''' <param name="Sequence">Sequence</param>
        ''' <param name="Channels">String Table of Persistent Values</param>
        ''' <param name="MinutesBetweenSave">Minutes between Save</param>
        ''' <param name="FilePath">File Path</param>
        ''' <remarks></remarks>
        Public Sub New(ByRef Sequence As CEL.KineticaRT.Sequence.Sequence, _
                       ByVal Channels As String(), _
                       ByVal MinutesBetweenSave As Integer, _
                       ByVal FilePath As String)

            ReDim Preserve Channels(Channels.Length - 1)
            ReDim Preserve _KineticaRTChannels(Channels.Length - 1)

            _Sequence = Sequence
            _Channels = Channels
            _MinutesBetweenSave = MinutesBetweenSave
            _FilePath = FilePath

            'Pour chaque channel dans le tableau de string passé en parametres dans l'initialisation du controlleur, on initialise ce channel pour avoir acces aux valeurs.
            For Each Channel As String In _Channels
                _Sequence.InitializeChannel(_KineticaRTChannels(_i), Channel)
                If _KineticaRTChannels(_i) Is Nothing Then
                    clsTopMostMessageBox.Show(_KineticaRTChannels(_i).Name.ToString & " could not be added as a persistent value", "Persistent Values Controller Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                End If
                _i += 1
            Next

            'Lorsque les channels sont initialisés, on peut lancer l'initialisation de la gestion des valeurs persistentes
            InitPersistentValues()

        End Sub

        Private Sub InitPersistentValues()

            Dim NewPersistentValues As Boolean = False
            Dim reader As System.Xml.XmlReader = Nothing

            'Verification si un fichier XML avec des valeurs persistentes peut être lu.
            Try
                'Crée le répertoire si il n'existe pas déjà.
                If Path.GetDirectoryName(_FilePath) <> String.Empty Then
                    Directory.CreateDirectory(Path.GetDirectoryName(_FilePath))
                End If

                If File.Exists(_FilePath) Then
                    reader = System.Xml.XmlReader.Create(_FilePath)
                Else
                    'Le fichier XML n'existe pas ou n'est pas valide : nous devons initialiser de nouvelles valeurs persistentes
                    NewPersistentValues = True
                End If
            Catch ex As Exception
                'Le fichier XML n'existe pas ou n'est pas valide : nous devons initialiser de nouvelles valeurs persistentes
                NewPersistentValues = True
                If reader IsNot Nothing Then
                    reader.Close()
                    reader = Nothing
                End If
            End Try

            'Initialisation des nouvelles valeurs persistentes dans le dictionnaire pour chaque channel désiré
            If NewPersistentValues Then
                For Each KineticaRTChannel As IChannel In _KineticaRTChannels

                    'Ici on vérifie si l'utilisateur à mis des valeurs par défault dans ces canaux
                    _ChannelIsBoolean = TryCast(KineticaRTChannel, IBooleanChannel)
                    If Not _ChannelIsBoolean Is Nothing Then
                        'On prend une valeur par défault si c'est le cas
                        _PersistentValues.Add(KineticaRTChannel.Name, KineticaRTChannel.Value.ToString)
                    End If

                    _ChannelIsText = TryCast(KineticaRTChannel, ITextChannel)
                    If Not _ChannelIsText Is Nothing Then
                        'On vérifie si nous avons bien initialisé le canal sinon on le fait nous même
                        If KineticaRTChannel.Value.Equals(KINETICA_NOT_CONNECTED) Then
                            _PersistentValues.Add(KineticaRTChannel.Name, KINETICA_STRING_EMPTY.ToString)
                            KineticaRTChannel.Value = KINETICA_STRING_EMPTY
                        Else
                            'On prend une valeur par défault si c'est le cas
                            _PersistentValues.Add(KineticaRTChannel.Name, KineticaRTChannel.Value.ToString)
                        End If

                    End If

                    _ChannelIsNumeric = TryCast(KineticaRTChannel, INumericChannel)
                    If Not _ChannelIsNumeric Is Nothing Then
                        If KineticaRTChannel.Value.Equals(KINETICA_NOT_CONNECTED) Then
                            _PersistentValues.Add(KineticaRTChannel.Name, KINETICA_DOUBLE_ZERO.ToString)
                            KineticaRTChannel.Value = KINETICA_DOUBLE_ZERO
                        Else
                            'On prend une valeur par défault si c'est le cas
                            _PersistentValues.Add(KineticaRTChannel.Name, KineticaRTChannel.Value.ToString)
                        End If
                    End If
                Next
            Else
                Try
                    'Un fichier XML avec des valeurs persistentes existe : preparation a lire le contenu.
                    reader.MoveToContent()
                    _PersistentValues.ReadXml(reader)
                Catch ex As Exception
                    System.Diagnostics.Trace.WriteLine("Read Persistent Values Error : " + ex.ToString())
                    clsTopMostMessageBox.Show(ex.ToString(), "Read Persistent Values Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Finally
                    reader.Close()
                End Try

                'Assignation des valeurs lues aux channels correspondants.
                For Each KineticaRTChannel As IChannel In _KineticaRTChannels

                    If _PersistentValues.ContainsKey(KineticaRTChannel.Name) Then

                        _ChannelIsBoolean = TryCast(KineticaRTChannel, IBooleanChannel)
                        If Not _ChannelIsBoolean Is Nothing Then
                            KineticaRTChannel.Value = New Global.KineticaRT.Values.Boolean(_PersistentValues.Item(KineticaRTChannel.Name))
                        End If

                        _ChannelIsText = TryCast(KineticaRTChannel, ITextChannel)
                        If Not _ChannelIsText Is Nothing Then
                            KineticaRTChannel.Value = New Global.KineticaRT.Values.Text(_PersistentValues.Item(KineticaRTChannel.Name))
                        End If

                        _ChannelIsNumeric = TryCast(KineticaRTChannel, INumericChannel)
                        If Not _ChannelIsNumeric Is Nothing Then
                            KineticaRTChannel.Value = New Global.KineticaRT.Values.Double(_PersistentValues.Item(KineticaRTChannel.Name))
                        End If

                    Else

                        'Ici on s'assure qu'il n'aille pas de "Not Connected" comme valeur de canal
                        'Ce cas arrive lorsqu'il n'y a pas de valeur initial dans un ITextChannel et un INumericChannel
                        _ChannelIsText = TryCast(KineticaRTChannel, ITextChannel)
                        If Not _ChannelIsText Is Nothing Then
                            If _ChannelIsText.Value.Equals(KINETICA_NOT_CONNECTED) Then
                                KineticaRTChannel.Value = KINETICA_STRING_EMPTY
                            End If
                        End If

                        _ChannelIsNumeric = TryCast(KineticaRTChannel, INumericChannel)
                        If Not _ChannelIsNumeric Is Nothing Then
                            If _ChannelIsNumeric.Value.Equals(KINETICA_NOT_CONNECTED) Then
                                KineticaRTChannel.Value = KINETICA_DOUBLE_ZERO
                            End If
                        End If
                    End If
                Next
            End If
        End Sub

        Public Sub SavePersistentValues(Optional ForceSave As Boolean = False)

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Exit Sub 'Sort de cette fonction si l'archive est en playback
            End If

            If Date.Compare(Now, _NextTimeToSave) >= 0 Or ForceSave = True Then

                'Verification que le dictionnaire a bel et bien initialiser : si une erreur est obtenue a l'ouverture, il se peut que le dictionnaire n'est pas ete initialiser
                If Not (_PersistentValues Is Nothing) Then

                    'On efface les anciennes valeurs associes aux channels persistents dans le dictionnaire
                    _PersistentValues.Clear()

                    'Pour chaque valeur persistente, on y assigne la valeur actuelle dans le dictionnaire
                    For Each KineticaRTChannel As IChannel In _KineticaRTChannels
                        _PersistentValues.Add(KineticaRTChannel.Name, KineticaRTChannel.Value.ToString)
                    Next

                    Dim fileStream As FileStream = Nothing
                    Dim writer As System.Xml.XmlWriter = Nothing

                    Try
                        fileStream = New FileStream(_FilePath, FileMode.Create)

                        'Sauvegarde du dictionnaire en format XML avec comme nom racine "PersistentValues"
                        Try
                            writer = System.Xml.XmlWriter.Create(fileStream)
                            writer.Flush()
                            writer.WriteStartDocument()
                            writer.WriteStartElement("PersistentValues")
                            _PersistentValues.WriteXml(writer)
                            writer.WriteEndDocument()


                        Catch ex As Exception
                            System.Diagnostics.Trace.WriteLine("Save Persistent Values Error : " + ex.ToString())
                            clsTopMostMessageBox.Show(ex.ToString(), "Save Persistent Values Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Finally
                            If writer IsNot Nothing Then
                                writer.Close()
                                writer = Nothing
                            End If
                        End Try



                    Catch ex As Exception
                        System.Diagnostics.Trace.WriteLine("Save Persistent Values Error : " + ex.ToString())
                        clsTopMostMessageBox.Show(ex.ToString(), "Save Persistent Values Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Finally
                        If fileStream IsNot Nothing Then
                            fileStream.Close()
                            fileStream = Nothing
                        End If
                    End Try


                End If

                'On prepare la prochaine sauvegarde dans le temps requis
                '_NextTimeToSave = Now.AddMinutes(CType(_MinutesBetweenSave, Double))
                _NextTimeToSave = Now.AddMinutes(CType(_MinutesBetweenSave, Double))

            End If

        End Sub

        Public Sub ResetPersistentValues()

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Exit Sub 'Sort de cette fonction si l'archive est en playback
            End If

            'On reinitialise les channels qui doivent etre sauvegardes dans le fichier des valeurs persistentes : ex : nouveau test pour un meme moteur avec un nouveau de serie donne.
            For Each KineticaRTChannel As IChannel In _KineticaRTChannels

                _ChannelIsBoolean = TryCast(KineticaRTChannel, IBooleanChannel)
                If Not _ChannelIsBoolean Is Nothing Then
                    KineticaRTChannel.Value = New Global.KineticaRT.Values.Boolean(False)
                End If

                _ChannelIsText = TryCast(KineticaRTChannel, ITextChannel)
                If Not _ChannelIsText Is Nothing Then
                    KineticaRTChannel.Value = New Global.KineticaRT.Values.Text("")
                End If

                _ChannelIsNumeric = TryCast(KineticaRTChannel, INumericChannel)
                If Not _ChannelIsNumeric Is Nothing Then
                    KineticaRTChannel.Value = New Global.KineticaRT.Values.Double(0.0)
                End If
            Next

        End Sub


        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free managed resources when explicitly called

                End If

                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class


End Namespace
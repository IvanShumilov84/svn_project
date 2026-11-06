
Public Class DatabaseInformation

    'Private _Drive As String
    'Public Property Drive() As String
    '    Get
    '        Return _Drive
    '    End Get
    '    Set(ByVal Value As String)
    '        _Drive = Value
    '    End Set
    'End Property
    Private _Path As String
    Public Property Path() As String
        Get
            Return _Path
        End Get
        Set(ByVal Value As String)
            _Path = Value
        End Set
    End Property
    Private _FileName As String
    Public Property FileName() As String
        Get
            Return _FileName
        End Get
        Set(ByVal Value As String)
            _FileName = Value
        End Set
    End Property
    Public ReadOnly Property FullFilePath() As String
        Get
            If Path.Substring(Path.Length - 1) = "\" Then
                Return String.Format("{0}{1}", _Path, _FileName)
            Else
                Return String.Format("{0}\{1}", _Path, _FileName)
            End If
        End Get
    End Property
    Private _WorkGroupFileName As String
    Public Property WorkGroupFileName() As String
        Get
            Return _WorkGroupFileName
        End Get
        Set(ByVal Value As String)
            _WorkGroupFileName = Value
        End Set
    End Property
    Public ReadOnly Property FullFileWorkgroup() As String
        Get
            If Path.Substring(Path.Length - 1) = "\" Then
                Return String.Format("{0}{1}", _Path, _WorkGroupFileName)
            Else
                Return String.Format("{0}\{1}", _Path, _WorkGroupFileName)
            End If
        End Get
    End Property
    Private _Password As String
    Public Property Password() As String
        Get
            Return _Password
        End Get
        Set(ByVal Value As String)
            _Password = Value
        End Set
    End Property
    Private _UserId As String
    Public Property UserId() As String
        Get
            Return _UserId
        End Get
        Set(ByVal Value As String)
            _UserId = Value
        End Set
    End Property

    Public ReadOnly Property OleDbConnectionString() As String
        Get
            Return String.Format("Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0};Jet OLEDB:System database={1}; User ID={2}; Password={3}", FullFilePath, FullFileWorkgroup, _UserId, _Password)
        End Get
    End Property
End Class


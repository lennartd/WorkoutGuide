Imports System.ComponentModel

Public Class Video
    Implements INotifyPropertyChanged
    Public Sub New(ByVal url As String, ByVal title As String, ByVal author As String, ByVal description As String, ByVal duration As Integer, _
                   ByVal image As Windows.Media.ImageSource, ByVal dateAdded As Date, ByVal rating As Integer, ByVal difficulty As String, ByVal categories As Category)
        _url = url : _title = title : _author = author : _description = description : _duration = duration : _image = image : _dateAdded = dateAdded
        _rating = rating : _difficulty = difficulty : _categories = categories


        CreateOpenLinkCommand()
    End Sub

    Private _url As String
    Public Property VideoUrl() As String
        Get
            Return _url
        End Get
        Set(ByVal value As String)
            _url = value
            RaiseProp("VideoUrl")
        End Set
    End Property

    Private _title As String
    Public Property VideoTitle() As String
        Get
            Return _title
        End Get
        Set(ByVal value As String)
            _title = value
            RaiseProp("VideoTitle")
        End Set
    End Property

    Private _author As String
    Public Property VideoAuthor() As String
        Get
            Return _author
        End Get
        Set(ByVal value As String)
            _author = value
            RaiseProp("VideoAuthor")
        End Set
    End Property

    Private _description As String
    Public Property VideoDescription() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
            RaiseProp("VideoDescription")
        End Set
    End Property

    Private _duration As Integer 'in sec
    Public Property VideoDuration() As Integer
        Get
            Return _duration
        End Get
        Set(ByVal value As Integer)
            _duration = value
            RaiseProp("VideoDuration")
        End Set
    End Property

    Private _image As Windows.Media.ImageSource
    Public Property VideoImage() As Windows.Media.ImageSource
        Get
            Return _image
        End Get
        Set(ByVal value As Windows.Media.ImageSource)
            _image = value
            RaiseProp("VideoImage")
        End Set
    End Property

    Private _dateAdded As Date

    Public Property VideoDateAdded() As Date
        Get
            Return _dateAdded
        End Get
        Set(ByVal value As Date)
            _dateAdded = value
            RaiseProp("VideoDateAdded")
        End Set
    End Property

    Private _rating As Integer

    Public Property VideoRating() As Integer
        Get
            Return _rating
        End Get
        Set(ByVal value As Integer)
            _rating = value
            RaiseProp("VideoRating")
        End Set
    End Property

    Private _difficulty As String

    Public Property VideoDifficulty() As String 'einfach/mittel/leicht
        Get
            Return _difficulty
        End Get
        Set(ByVal value As String)
            _difficulty = value
            RaiseProp("VideoDifficulty")
        End Set
    End Property

    Private _categories As Category

    Public Property VideoCategories() As Category
        Get
            Return _categories
        End Get
        Set(ByVal value As Category)
            _categories = value
            RaiseProp("VideoCategories")
        End Set
    End Property


    #Region "Commands"
        Private _openLinkCommand As ICommand
        Public Property OpenLinkCommand() As ICommand
            Get
                Return _openLinkCommand
            End Get
            Set(ByVal value As ICommand)
                _openLinkCommand = value
                RaiseProp("OpenLinkCommand")
            End Set
        End Property

        Private Function CanExecuteOpenLinkCommand() As Boolean
            If VideoUrl = Nothing
                Return False
            End If
            Return True
        End Function

        Private Sub CreateOpenLinkCommand
            OpenLinkCommand = New RelayCommand(AddressOf openLinkExecute, AddressOf CanExecuteOpenLinkCommand)
        End Sub

        Private Sub OpenLinkExecute
            Process.Start(VideoUrl)
        End Sub
    #End Region

   

    Public Sub RaiseProp(ByVal propertie As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertie))
    End Sub

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged


End Class

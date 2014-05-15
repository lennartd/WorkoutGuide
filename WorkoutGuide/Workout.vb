Imports System.ComponentModel
Imports ProtoBuf

<ProtoContract> _
Public Class Workout
     Implements INotifyPropertyChanged

    Public Sub New()

    End Sub

    Public Sub New(ByVal title As String, ByVal description As String, ByVal videos As VideosList)
        _title = title : _description = description : _videos = videos
    End Sub

    Private _title As String
    <ProtoMember(1)> _
    Public Property WorkoutTitle() As String
        Get
            Return _title
        End Get
        Set(ByVal value As String)
            _title = value
            RaiseProp("WorkoutTitle")
        End Set
    End Property

    Private _description As String
    <ProtoMember(2)> _
    Public Property WorkoutDescription() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
            RaiseProp("WorkoutDescription")
        End Set
    End Property

    Private _videos As New VideosList
    <ProtoMember(3)> _
    Public Property WorkoutVideos() As VideosList
        Get
            Return _videos
        End Get
        Set(ByVal value As VideosList)
            _videos = value
            RaiseProp("WorkoutVideos")
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return WorkoutTitle
    End Function

    Public Sub RaiseProp(ByVal propertie As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertie))
    End Sub

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
End Class

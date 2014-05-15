Imports System.ComponentModel
Imports ProtoBuf

<ProtoContract> _
Public Class Difficulty
    Implements INotifyPropertyChanged

    Public Sub New(ByVal easy As Boolean, ByVal intermediate As Boolean, ByVal difficult As Boolean, ByVal doesntMatter As Boolean)
        _easy = easy
        _intermediate = intermediate
        _difficult = difficult
        _doesntMatter = doesntMatter
    End Sub

    Public Sub New()
        _doesntMatter = True
    End Sub

    Private _easy As Boolean
    <ProtoMember(1)> _
    Public Property DifficultyEasy() As Boolean
        Get
            Return _easy
        End Get
        Set(ByVal value As Boolean)
            _easy = value
            RaiseProp("DifficultyEasy")
        End Set
    End Property

    Private _intermediate As Boolean
    <ProtoMember(2)> _
    Public Property DifficlutyIntermediate() As Boolean
        Get
            Return _intermediate
        End Get
        Set(ByVal value As Boolean)
            _intermediate = value
            RaiseProp("DifficlutyIntermediate")
        End Set
    End Property

    Private _difficult As Boolean
    <ProtoMember(3)> _
    Public Property DifficultyDifficult() As Boolean
        Get
            Return _difficult
        End Get
        Set(ByVal value As Boolean)
            _difficult = value
            RaiseProp("DifficultyDifficult")
        End Set
    End Property

    Private _doesntMatter As Boolean
    <ProtoMember(4)> _
    Public Property DifficultyDoesntMatter() As Boolean
        Get
            Return _doesntMatter
        End Get
        Set(ByVal value As Boolean)
            _doesntMatter = value
            RaiseProp("DifficultyDoesntMatter")
        End Set
    End Property

    Public Sub RaiseProp(ByVal propertie As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertie))
    End Sub

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
End Class

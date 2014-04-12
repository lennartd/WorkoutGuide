Imports System.ComponentModel

Public Class SearchFeatures
    Implements INotifyPropertyChanged
    
    Public Sub New(ByVal keyword As String, ByVal categories As Categories, ByVal difficulty As Difficulty, ByVal duration As Duration, dateAdded As DateAdded)
        _keyword = keyword : _categories = categories : _difficulty = difficulty : _duration = duration : _dateAdded = dateAdded
    End Sub

    Private _keyword As String
    Public Property SearchFeaturesKeyword() As String
        Get
            Return _keyword
        End Get
        Set(ByVal value As String)
            _keyword = value
            RaiseProp("SearchFeaturesKeyword")
        End Set
    End Property

    Private _categories As Categories
    Public Property SearchFeaturesCategories() As Categories
        Get
            Return _categories
        End Get
        Set(ByVal value As Categories)
            _categories = value
            RaiseProp("SearchFeaturesCategories")
        End Set
    End Property
   
    Private _difficulty As Difficulty
    Public Property SearchFeaturesDifficulty() As Difficulty
        Get
            Return _difficulty
        End Get
        Set(ByVal value As Difficulty)
            _difficulty = value
            RaiseProp("SearchFeaturesDifficulty")
        End Set
    End Property

    Private _duration As Duration
    Public Property SearchFeaturesDuration() As Duration
        Get
            Return _duration
        End Get
        Set(ByVal value As Duration)
            _duration = value
            RaiseProp("SearchFeaturesDuration")
        End Set
    End Property

    Private _dateAdded As DateAdded
    Public Property SearchFeaturesDateAdded() As DateAdded
        Get
            Return _dateAdded
        End Get
        Set(ByVal value As DateAdded)
            _dateAdded = value
            RaiseProp("SearchFeaturesDateAdded")
        End Set
    End Property

    Public Sub RaiseProp(ByVal propertie As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertie))
    End Sub

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
End Class

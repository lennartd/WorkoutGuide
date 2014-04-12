Imports System.ComponentModel

Public Class DateAdded
     Implements INotifyPropertyChanged
    
    Public Sub New(ByVal today As Boolean, ByVal yesterday As Boolean, ByVal lastWeek As Boolean, ByVal lastMonth As Boolean, ByVal doesntMatter As Boolean)
        _today = today
        _yesterday = yesterday
        _lastWeek = lastWeek
        _lastMonth = lastMonth
        _doesntMatter = doesntMatter
    End Sub

    Public Sub New()
        _doesntMatter = True
    End Sub

    Private _today As Boolean
    Public Property DateAddedToday() As Boolean
        Get
            Return _today
        End Get
        Set(ByVal value As Boolean)
            _today = value
            RaiseProp("DateAddedToday")
        End Set
    End Property

    Private _yesterday As Boolean
    Public Property DateAddedYesterday() As Boolean
        Get
            Return _yesterday
        End Get
        Set(ByVal value As Boolean)
            _yesterday = value
            RaiseProp("DateAddedYesterday")
        End Set
    End Property

    Private _lastWeek As Boolean
    Public Property DateAddedLastWeek() As Boolean
        Get
            Return _lastWeek
        End Get
        Set(ByVal value As Boolean)
            _lastWeek = value
            RaiseProp("DateAddedLastWeek")
        End Set
    End Property

    Private _lastMonth As Boolean
    Public Property DateAddedLastMonth() As Boolean
        Get
            Return _lastMonth
        End Get
        Set(ByVal value As Boolean)
            _lastMonth = value
            RaiseProp("DateAddedLastMonth")
        End Set
    End Property

    Private _doesntMatter As Boolean
    Public Property DateAddedDoesntMatter() As Boolean
        Get
            Return _doesntMatter
        End Get
        Set(ByVal value As Boolean)
            _doesntMatter = value
            RaiseProp("DateAddedDoesntMatter")
        End Set
    End Property

    Public Sub RaiseProp(ByVal propertie As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertie))
    End Sub

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
End Class

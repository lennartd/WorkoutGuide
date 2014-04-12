Imports System.ComponentModel

Public Class Duration
    Implements INotifyPropertyChanged
    
    Public Sub New(ByVal zeroToTen As Boolean, ByVal tenToThirty As Boolean, ByVal overThirty As Boolean, ByVal doesntMatter As Boolean)
        _zeroToTen = zeroToTen : _tenToThirty = tenToThirty : _overThirty = overThirty : _doesntMatter = doesntMatter
    End Sub

    Public Sub New()
        _doesntMatter = True
    End Sub

    Private _zeroToTen As Boolean
    Public Property DurationZeroToTen() As Boolean
        Get
            Return _zeroToTen
        End Get
        Set(ByVal value As Boolean)
            _zeroToTen = value
            RaiseProp("DurationZeroToTen")
        End Set
    End Property

    Private _tenToThirty As Boolean
    Public Property DurationTenToThirty() As Boolean
        Get
            Return _tenToThirty
        End Get
        Set(ByVal value As Boolean)
            _tenToThirty = value
            RaiseProp("DurationTenToThirty")
        End Set
    End Property

    Private _overThirty As Boolean
    Public Property DurationOverThirty() As Boolean
        Get
            Return _overThirty
        End Get
        Set(ByVal value As Boolean)
            _overThirty = value
            RaiseProp("DurationOverThirty")
        End Set
    End Property

    Private _doesntMatter As Boolean
    Public Property DurationDoesntMatter() As Boolean
        Get
            Return _doesntMatter
        End Get
        Set(ByVal value As Boolean)
            _doesntMatter = value
            RaiseProp("DurationDoesntMatter")
        End Set
    End Property

    Public Sub RaiseProp(ByVal propertie As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertie))
    End Sub

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
End Class

Imports System.ComponentModel

Public Class Categories
    Implements INotifyPropertyChanged
    
    Public Sub New(ByVal allMuscles As Boolean, ByVal abs As Boolean, ByVal back As Boolean, ByVal biceps As Boolean, ByVal cardio As Boolean, _
                   ByVal chest As Boolean, ByVal leg As Boolean, ByVal shoulder As Boolean, ByVal speed As Boolean, ByVal triceps as Boolean)
        _allMuscles = allMuscles        
        _abs = abs
        _back = back
        _biceps = biceps
        _cardio = cardio
        _chest = chest
        _leg = leg
        _shoulder = shoulder
        _speed = speed
        _triceps = triceps
    End Sub

    Public Sub New()
        _allMuscles = True
        _abs = True
        _back = True
        _biceps = True
        _cardio = True
        _chest = True
        _leg = True
        _shoulder = True
        _speed = True
        _triceps = True
    End Sub

    Private _allMuscles As Boolean
    Public Property CategoriesAllMuscles() As Boolean
        Get
            Return _allMuscles
        End Get
        Set(ByVal value As Boolean)
            _allMuscles = value
            RaiseProp("CategoriesAllMuscles")
        End Set
    End Property

    Private _abs As Boolean
    Public Property CategoriesAbs() As Boolean
        Get
            Return _abs
        End Get
        Set(ByVal value As Boolean)
            _abs = value
            RaiseProp("CategoriesAbs")
        End Set
    End Property

    Private _back As Boolean
    Public Property CategoriesBack() As Boolean
        Get
            Return _back
        End Get
        Set(ByVal value As Boolean)
            _back = value
            RaiseProp("CategoriesBack")
        End Set
    End Property

    Private _biceps As Boolean
    Public Property CategoriesBiceps() As Boolean
        Get
            Return _biceps
        End Get
        Set(ByVal value As Boolean)
            _biceps = value
            RaiseProp("CategoriesBiceps")
        End Set
    End Property
   
    Private _cardio As Boolean
    Public Property CategoriesCardio() As Boolean
        Get
            Return _cardio
        End Get
        Set(ByVal value As Boolean)
            _cardio = value
            RaiseProp("CategoriesCardio")
        End Set
    End Property

    Private _chest As Boolean
    Public Property CategoriesChest() As Boolean
        Get
            Return _chest
        End Get
        Set(ByVal value As Boolean)
            _chest = value
            RaiseProp("CategoriesChest")
        End Set
    End Property

    Private _leg As Boolean
    Public Property CategoriesLeg() As Boolean
        Get
            Return _leg
        End Get
        Set(ByVal value As Boolean)
            _leg = value
            RaiseProp("CategoriesLeg")
        End Set
    End Property

    Private _shoulder As Boolean
    Public Property CategoriesShoulder() As Boolean
        Get
            Return _shoulder
        End Get
        Set(ByVal value As Boolean)
            _shoulder = value
            RaiseProp("CategoriesShoulder")
        End Set
    End Property

    Private _speed As Boolean
    Public Property CategoriesSpeed() As Boolean
        Get
            Return _speed
        End Get
        Set(ByVal value As Boolean)
            _speed = value
            RaiseProp("CategoriesSpeed")
        End Set
    End Property

    Private _triceps As Boolean
    Public Property CategoriesTriceps() As Boolean
        Get
            Return _triceps
        End Get
        Set(ByVal value As Boolean)
            _triceps = value
            RaiseProp("CategoriesTriceps")
        End Set
    End Property

    Public Sub RaiseProp(ByVal propertie As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertie))
    End Sub

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
End Class


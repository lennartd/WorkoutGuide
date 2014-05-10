Imports System.Collections.ObjectModel

Public Class WorkoutsList
    
Private _workouts As New ObservableCollection(Of Workout)
    Public Property Workouts() As ObservableCollection(Of Workout)
        Get
            Return _workouts
        End Get
        Set(ByVal value As ObservableCollection(Of Workout))
            _workouts = value
        End Set
    End Property

End Class

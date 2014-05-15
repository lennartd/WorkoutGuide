Imports System.Collections.ObjectModel
Imports ProtoBuf

<ProtoContract> _
Public Class WorkoutsList
    
    Private _workouts As New ObservableCollection(Of Workout)
    <ProtoMember(1)> _
    Public Property Workouts() As ObservableCollection(Of Workout)
        Get
            Return _workouts
        End Get
        Set(ByVal value As ObservableCollection(Of Workout))
            _workouts = value
        End Set
    End Property

End Class

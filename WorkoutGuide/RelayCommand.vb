''' <summary>
''' A command whose sole purpose is to relay its functionality to other objects by invoking delegates. The default return value for the CanExecute method is 'true'.
''' </summary>
''' <remarks></remarks>
Public Class RelayCommand
    Implements ICommand

#Region "Declarations"
    Private ReadOnly _canExecute As Func(Of Boolean)
    Private ReadOnly _execute As Action
#End Region
#Region "Constructors"
    Public Sub New(ByVal execute As Action)
 Me.New(execute, Nothing)
    End Sub

    Public Sub New(ByVal execute As Action, ByVal canExecute As Func(Of Boolean))
        If execute Is Nothing Then
            Throw New ArgumentNullException("execute")
        End If
        _Execute = execute
        _CanExecute = canExecute
    End Sub
#End Region
 
#Region "ICommand"
Public Custom Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged
        AddHandler(ByVal value As EventHandler)
            If _CanExecute IsNot Nothing Then
                AddHandler CommandManager.RequerySuggested, value
            End If
        End AddHandler
        RemoveHandler(ByVal value As EventHandler)
            If _CanExecute IsNot Nothing Then
                RemoveHandler CommandManager.RequerySuggested, value
            End If
        End RemoveHandler
        RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
            'This is the RaiseEvent block
            CommandManager.InvalidateRequerySuggested()
        End RaiseEvent
    End Event
    Public Function CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
        If _CanExecute Is Nothing Then
            Return True
        Else
            Return _CanExecute.Invoke
        End If
    End Function
    Public Sub Execute(ByVal parameter As Object) Implements ICommand.Execute
        _Execute.Invoke()
    End Sub
#End Region

End Class


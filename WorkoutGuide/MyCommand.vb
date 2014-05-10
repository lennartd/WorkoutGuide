Imports System.ComponentModel

Public Class MyCommand
	Implements ICommand

    Private _mFunction As Func(Of [String], Integer)
	Public Property [Function]() As Func(Of [String], Integer)
		Get
			Return _mFunction
		End Get
		Set
			_mFunction = Value
            RaiseProp("[Function]")
		End Set
	End Property


	Public Sub ICommand_Execute(ByVal parameter As Object) Implements ICommand.Execute
	    MsgBox(parameter.ToString())
	End Sub

	Public Function ICommand_CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
	    Return True
	End Function


    Public Sub RaiseProp(ByVal propertie As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertie))
    End Sub

	Public Event PropertyChanged As EventHandler Implements ICommand.CanExecuteChanged

End Class

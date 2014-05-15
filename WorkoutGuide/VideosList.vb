Imports System.Collections.ObjectModel
Imports ProtoBuf

<ProtoContract> _
Public Class VideosList

    Private _videos As New ObservableCollection(Of Video)
    <ProtoMember(1)> _
    Public Property Videos() As ObservableCollection(Of Video)
        Get
            Return _videos
        End Get
        Set(ByVal value As ObservableCollection(Of Video))
            _videos = value
        End Set
    End Property

End Class


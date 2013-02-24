' Question structure
Public Class Question

    Dim SQ As String
    Dim SAList As New List(Of String)
    Dim SM As Integer
    Dim QAHasChgd As Boolean = False

    Sub New(ByVal QStr As String, ByVal AStrLst As List(Of String), ByVal MCnt As Integer)

        SQ = QStr
        SAList = AStrLst
        SM = MCnt

    End Sub

    Property Question As String
        Set(ByVal value As String)
            SQ = value
        End Set
        Get
            Return SQ
        End Get
    End Property

    Property AnswerList As List(Of String)
        Set(ByVal value As List(Of String))
            SAList = value
        End Set
        Get
            Return SAList
        End Get
    End Property

    Property Marking As Integer
        Set(ByVal value As Integer)
            SM = value
        End Set
        Get
            Return SM
        End Get
    End Property

End Class
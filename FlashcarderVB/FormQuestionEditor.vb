Public Class FormQuestionEditor

    Public Shared QAMObj As Question

    ' Private variables indicating text changes
    Private Shared QTxtHasChgd As Boolean = False
    Private Shared ATxtHasChgd As Boolean = False

    ' List of answer textboxes
    Private Shared answerTbxList As New List(Of TextBox)

    ' Update on visibility change
    Private Sub VisChgd() Handles MyBase.VisibleChanged

        ' -- Questions --
        txtQs.Text = QAMObj.Question
        QTxtHasChgd = False ' Text has been loaded, so reset the changed flag

        ' -- Markings --
        txtMs.Text = CStr(QAMObj.Marking)

        ' -- Answers --

        ' Answer textboxes
        answerTbxList.Clear()
        For i = 0 To QAMObj.AnswerList.Count - 1

            ' Make new textbox
            MakeAnswerTextbox(QAMObj.AnswerList.Item(i))

        Next

        ' Text has been loaded, so reset the changed flag
        ATxtHasChgd = False

    End Sub

    Private Sub Loader() Handles MyBase.Load

        ' Automatically use the icon/title of the first form
        Me.Icon = Form1.Icon
        Me.Text = Form1.Text + " - Question Editor"

    End Sub

    Public Sub Cancel() Handles btnCancel.Click
        QTxtHasChgd = False
        ATxtHasChgd = False
        Me.Close()
    End Sub

    Public Sub Save() Handles btnSave.Click

        ' Question saving
        If QTxtHasChgd Then
            QAMObj.Question = txtQs.Text
        End If

        ' Answer saving
        If ATxtHasChgd Then
            Dim L As New List(Of String)
            For i = 0 To answerTbxList.Count - 1

                ' Allow user to delete unwanted answers (by making them null)
                If answerTbxList.Item(i).Text.Length <> 0 Then
                    L.Add(answerTbxList.Item(i).Text)
                End If


            Next
            QAMObj.AnswerList = L
        End If

        ' Markings saving
        Try
            If Not String.IsNullOrWhiteSpace(txtMs.Text) Then
                QAMObj.Marking = CInt(txtMs.Text)
            End If
        Catch
        End Try

        ' Update HasChanged variables
        Form1.QHasChgd = QTxtHasChgd OrElse Form1.QHasChgd
        Form1.AHasChgd = ATxtHasChgd OrElse Form1.AHasChgd

        ' Refresh question list
        FormQuestionManager.UpdateMainListView()

        ' Close the question editor
        Me.Close()

    End Sub

    Private Sub QuestionsChanged() Handles txtQs.TextChanged
        QTxtHasChgd = True
    End Sub

    Private Sub AnswersChanged()
        ATxtHasChgd = True
    End Sub

    Private Sub SLoad() Handles MyBase.Load
        Me.Text = Form1.MainTitle & " - Question Editor"
    End Sub

    ' Add an answer
    Private Sub AddAnswer() Handles btnAddAnswer.Click

        ' Answers have changed (because there is a new one)
        ATxtHasChgd = True

        ' Make a new answer textbox
        MakeAnswerTextbox("")

    End Sub

    ' Function that makes an answer textbox
    Private Sub MakeAnswerTextbox(ByVal Answer As String)

        ' -- Textbox instantiation/manipulation --
        ' Create/initialize new textbox
        Dim newTbx As New TextBox
        newTbx.Multiline = True
        newTbx.Size = New Size(373, 70)
        newTbx.Visible = True
        newTbx.ScrollBars = ScrollBars.Vertical

        ' Add it to proper lists
        Me.Controls.Add(newTbx)
        answerTbxList.Add(newTbx)

        ' Position it appropriately
        newTbx.Location = New Point(16, 166 + 90 * (answerTbxList.Count - 1))

        ' Add answer
        newTbx.Text = Answer

        ' Add on-changed handler
        AddHandler newTbx.TextChanged, AddressOf AnswersChanged

        ' -- Form manipulation --
        ' Form resizing
        Me.Height = 285 + answerTbxList.Count * 90

        ' Options panel repositioning
        pnlOptions.Location = New Point(pnlOptions.Location.X, Me.Height - 119)

    End Sub

End Class
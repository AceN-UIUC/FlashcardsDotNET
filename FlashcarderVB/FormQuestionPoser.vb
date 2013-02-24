Public Class FormQuestionPoser

    Public QAMObj As Question
    Public QuestionIsOKd As Boolean = False ' If FALSE when this form is closed, stop the quizzing process

    Private Sub VisChgd(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.VisibleChanged
        lblQ.Text = QAMObj.Question
    End Sub

    Private Sub btnOK_Click(sender As System.Object, e As System.EventArgs) Handles btnOK.Click

        ' Get and format user answer
        Dim UserAnswer As String = Quizzing.FormatAnswer(txtUserAnswer.Text)

        ' Answer checking
        Dim Correct As Boolean = False
        For Each CorrectAnswer As String In QAMObj.AnswerList

            ' Check for answer correctness
            If UserAnswer = Quizzing.FormatAnswer(CorrectAnswer) Then
                Correct = True
                Exit For
            End If

        Next

        ' Report result
        QuestionResponseDialog.Correct = Correct
        QuestionResponseDialog.QAMObj = QAMObj
        QuestionResponseDialog.UserAnswer = UserAnswer
        QuestionResponseDialog.ShowDialog()

        ' Hide form
        Me.Hide()
        QuestionIsOKd = True
        Form1.Show()

    End Sub

    Private Sub Loader() Handles MyBase.Load

        QuestionIsOKd = False

        ' Automatically use the icon/title of the first form
        Me.Icon = Form1.Icon
        Me.Text = Form1.MainTitle + " - Question Poser"

    End Sub

End Class
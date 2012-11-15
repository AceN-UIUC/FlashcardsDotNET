Imports System.Windows.Forms

Public Class QuestionResponseDialog

    ' Variables
    Public QAMObj As Question ' Allows question to be edited using edit form
    Public UserAnswer As String = ""
    Public Correct As Boolean

    Private Sub VisChgd(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.VisibleChanged
        If Correct Then
            lbl.Text = "You're correct!"
        Else
            lbl.Text = "Are you sure? Let's compare answers. Here's mine: " & vbLf & vbLf & QAMObj.AnswerList.Item(0) & vbLf & vbLf & "and here's yours." & vbLf & vbLf & FormQuestionPoser.txtUserAnswer.Text
        End If

        FormQuestionPoser.txtUserAnswer.Text = ""
    End Sub

    Private Sub btnCorrect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCorrect.Click
        QAMObj.Marking -= 1
        HideMe()
    End Sub

    Private Sub btnIncorrect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIncorrect.Click
        QAMObj.Marking += 1
        HideMe()
    End Sub

    Private Sub btnNoMark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNoMark.Click
        HideMe()
    End Sub

    Private Sub HideMe()
        Me.Hide()
        FormQuestionPoser.txtUserAnswer.Text = ""
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        FormQuestionEditor.QAMObj = QAMObj
        FormQuestionEditor.Show()
    End Sub

End Class

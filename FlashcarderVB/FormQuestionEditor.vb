Public Class FormQuestionEditor

    Public QAMObj As Question

    ' Private variables indicating text changes
    Private QTxtHasChgd As Boolean = False
    Private ATxtHasChgd As Boolean = False

    ' Update on visibility change
    Private Sub VisChgd() Handles MyBase.VisibleChanged
        txtAns.Lines = QAMObj.AnswerList.ToArray
        txtQs.Text = QAMObj.Question
        txtMs.Text = CStr(QAMObj.Marking)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        QTxtHasChgd = False
        ATxtHasChgd = False
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Not String.IsNullOrWhiteSpace(txtMs.Text) Then
                QAMObj.Marking = CInt(txtMs.Text)
            End If
        Catch
        End Try

        If Not String.IsNullOrWhiteSpace(txtQs.Text) Then
            QAMObj.Question = txtQs.Text
        End If
        If Not String.IsNullOrWhiteSpace(txtAns.Text) Then
            Dim L As New List(Of String)
            L.AddRange(txtAns.Lines)
            QAMObj.AnswerList = L
        End If

        Form1.QHasChgd = QTxtHasChgd And Not Form1.QHasChgd
        Form1.AHasChgd = ATxtHasChgd And Not Form1.AHasChgd

        Me.Close()
    End Sub

    Private Sub txtQs_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQs.TextChanged
        QTxtHasChgd = True
    End Sub

    Private Sub txtAns_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAns.TextChanged
        ATxtHasChgd = True
    End Sub

    Private Sub SLoad() Handles MyBase.Load
        Me.Text = Form1.MainTitle & " - Question Editor"
    End Sub
End Class
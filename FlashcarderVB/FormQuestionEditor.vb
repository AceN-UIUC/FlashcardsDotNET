Public Class FormQuestionEditor

    Public Shared QAMObj As Question

    ' Private variables indicating text changes
    Private Shared QTxtHasChgd As Boolean = False
    Private Shared ATxtHasChgd As Boolean = False

    ' Update on visibility change
    Private Sub VisChgd() Handles MyBase.VisibleChanged
        txtAns.Lines = QAMObj.AnswerList.ToArray
        txtQs.Text = QAMObj.Question
        txtMs.Text = CStr(QAMObj.Marking)
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

        ' Update HasChanged variables
        Form1.QHasChgd = QTxtHasChgd OrElse Form1.QHasChgd
        Form1.AHasChgd = ATxtHasChgd OrElse Form1.AHasChgd

        ' Refresh question list
        FormQuestionManager.UpdateMainListView()

        Me.Close()
    End Sub

    Private Sub txtQs_TextChanged() Handles txtQs.TextChanged
        QTxtHasChgd = True
    End Sub

    Private Sub txtAns_TextChanged() Handles txtAns.TextChanged
        ATxtHasChgd = True
    End Sub

    Private Sub SLoad() Handles MyBase.Load
        Me.Text = Form1.MainTitle & " - Question Editor"
    End Sub
End Class
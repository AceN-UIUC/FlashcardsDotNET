Public Class FormQuestionEditor

    Public Shared QAMObj As Question

    ' Hotkey PInvokes
    Declare Function RegisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer

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
        For Each tb As TextBox In answerTbxList
            tb.Dispose()
        Next
        answerTbxList.Clear()
        For i = 0 To QAMObj.AnswerList.Count - 1

            ' Make new textbox
            MakeAnswerTextbox(QAMObj.AnswerList.Item(i))

        Next

        ' Text has been loaded, so reset the changed flag
        ATxtHasChgd = False

        ' -- Hotkeys --
        'If Me.Visible Then
        '    RegisterHotKey(Me.Handle, 1, 0, Keys.Back)
        'Else
        '    UnregisterHotKey(Me.Handle, 1)
        'End If

    End Sub

    ' Update icon + form title
    Private Sub Loader() Handles MyBase.Load

        ' Automatically use the icon/title of the first form
        Me.Icon = Form1.Icon
        Me.Text = Form1.MainTitle + " - Question Editor"

    End Sub

    ' On-exit handlers
    Public Sub Cancel() Handles btnCancel.Click
        QTxtHasChgd = False
        ATxtHasChgd = False
        Me.DialogResult = DialogResult.Cancel
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

        ' Dialog result
        Me.DialogResult = DialogResult.OK

        ' Close the question editor
        Me.Close()

    End Sub

    ' On-changed handlers
    Private Sub QuestionsChanged() Handles txtQs.TextChanged
        QTxtHasChgd = True
    End Sub
    Public Sub AnswersChanged(ByVal sender As Object, ByVal e As EventArgs)

        ' - Update answer-has-changed variable -
        ATxtHasChgd = True

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
        newTbx.Name = "answer" & answerTbxList.Count ' NOTE: the count values may not be very predictable (because of the way answer textboxes are handled)

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

    ' Message interceptor (for Backspace hotkey)
    '   Recycled from ContexType
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)

        If m.Msg = &H312 AndAlso m.WParam = 1 AndAlso answerTbxList.Count > 1 Then

            ' Backspace key was pressed
            Dim TextBoxDeleted As Boolean = False ' a one way flag
            For i = 0 To answerTbxList.Count - 1

                Dim tBox As TextBox = answerTbxList.Item(i - If(TextBoxDeleted, 1, 0))

                ' Delete textbox
                If tBox.Focused AndAlso tBox.Text.Length = 0 Then

                    ' Remove the current textbox
                    tBox.Dispose()
                    answerTbxList.RemoveAt(i)

                    ' Initiate repositioning proces
                    TextBoxDeleted = True
                    Continue For

                End If

                ' Reposition other textboxes
                If TextBoxDeleted Then
                    tBox.Location = New Point(tBox.Left, tBox.Top - 90)
                End If

            Next

            ' Form-resizing stuff
            If TextBoxDeleted Then

                ' Resize main form
                Me.Size = New Size(Me.Width, Me.Height - 90)

                ' Reposition options dialog
                pnlOptions.Location = New Point(pnlOptions.Left, pnlOptions.Top - 90)

            End If

        Else

            ' Pass the message on
            MyBase.WndProc(m)

        End If

    End Sub

End Class
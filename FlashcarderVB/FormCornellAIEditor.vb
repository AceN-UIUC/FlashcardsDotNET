Imports System.Text.RegularExpressions

Public Class FormCornellAIEditor

    ' QAM object
    Public Shared QAMObj As Question

    ' Indicates whether the current question is a definition one
    Public Shared IsDefinitionQuestion As Boolean = False

    ' Private variables indicating text changes
    Private Shared QTxtHasChgd As Boolean = False
    Private Shared ATxtHasChgd As Boolean = False

    ' List of answer textboxes
    Private Shared answerTbxList As New List(Of TextBox)

    ' Auto-completing textbox (used to add common phrases to questions)
    Public acTbx As New AutoCompletingTextBox

    ' Misc.
    Public QuestionAnswerTbxSize As Integer

    ' Maintain position through shows/hides
    Private MyPos As New Point(0, 0)
    Private Sub UpdateMyPos() Handles Me.VisibleChanged
        If Me.Visible Then
            Me.Location = MyPos ' Go to stored location on show
        Else
            MyPos = Me.Location ' Update stored location on hide
        End If
    End Sub

    ' Update on visibility change
    Private Sub VisChgd() Handles MyBase.VisibleChanged

        ' -- Questions --
        txtQs.Text = QAMObj.Question
        QTxtHasChgd = False ' Text has been loaded, so reset the changed flag

        ' -- Answers --

        ' - Answer textboxes -

        ' Remove old textboxes
        For i = 0 To answerTbxList.Count - 1
            answerTbxList.Item(i).Dispose()
        Next

        ' Make new textboxes
        answerTbxList.Clear()
        For i = 0 To QAMObj.AnswerList.Count - 1
            MakeAnswerTextbox(QAMObj.AnswerList.Item(i), i <> 0)
        Next

        ' Text has been loaded, so reset the changed flag
        ATxtHasChgd = False

    End Sub

    ' Loading
    Private Sub Loader() Handles MyBase.Load

        ' - Automatically use the icon/title of the first form -
        Me.Icon = Form1.Icon
        Me.Text = Form1.MainTitle + " - Question Editor"

        ' - Auto-completion setup -
        ' Initialize autocomplete textbox
        acTbx.Location = New Point(6, 21)
        acTbx.Size = New Size(262, 106)
        acTbx.Visible = True

        ' Add autocomplete textbox to the groupbox
        gbxAutoCompletion.Controls.Add(acTbx)

    End Sub

    ' Resizing handler
    Private Sub ResizeHandler() Handles MyBase.SizeChanged

        ' Constants
        Dim txtQsInitialSize As New Size(373, 112)
        Dim MyInitialSize As New Size(837, 535)
        Dim txtNotesInitialSize As New Size(408, 436)

        ' Precomputed valuesM
        Dim LeftPanelStart As Integer = txtQs.Left
        Dim LeftPanelSize As Integer = CInt(Math.Round(((LeftPanelStart + txtQsInitialSize.Width) / MyInitialSize.Width) * Me.Width) - LeftPanelStart)
        Dim RightPanelStart As Integer = LeftPanelStart + LeftPanelSize + 10
        QuestionAnswerTbxSize = CInt(Math.Round((Me.Height - 310) / Math.Max(answerTbxList.Count + 1, 2)))
        txtQs.Text = QuestionAnswerTbxSize.ToString + " / HEIGHT: " + Me.Height.ToString ' DEBUG

        ' Label(s)
        Label2.Location = New Point(Label2.Left, txtQs.Top + QuestionAnswerTbxSize + 9)

        ' Question textbox
        txtQs.Size = New Size(LeftPanelSize, QuestionAnswerTbxSize)

        ' Answer textboxes
        Me.MinimumSize = New Size(MyInitialSize.Width, MyInitialSize.Height + (answerTbxList.Count - 1) * QuestionAnswerTbxSize)
        Dim t_idx As Integer = 0
        For Each t As TextBox In answerTbxList
            t.Size = txtQs.Size
            t.Location = Label2.Location + New Point(0, 21 + (QuestionAnswerTbxSize + 10) * t_idx)
            t_idx += 1
        Next

        ' Auto-completion controls
        gbxAutoCompletion.Location = New Point(gbxAutoCompletion.Location.X, Me.Height - 255)
        gbxAutoCompletion.Size = New Size(LeftPanelSize, gbxAutoCompletion.Height)

        btnAddBefore.Location = New Point(gbxAutoCompletion.Width - 103, btnAddBefore.Top)
        btnAddAfter.Location = New Point(gbxAutoCompletion.Width - 103, btnAddAfter.Top)

        cbxAppendQMark.Location = New Point(gbxAutoCompletion.Width - 138, cbxAppendQMark.Top)

        acTbx.Size = New Size(gbxAutoCompletion.Width - 115, gbxAutoCompletion.Height - 60)

        ' Control box (below auto-completion)
        pnlOptions.Location = New Point(pnlOptions.Location.X, Me.Height - 83)

        ' Note textbox
        txtNotes.Size = New Size(Me.Width - 30 - RightPanelStart, Me.Height - 79)
        txtNotes.Location = New Point(RightPanelStart, txtNotes.Top)

    End Sub

#Region "Final operation buttons (Save, Cancel) event handlers"
    Public Sub Skip() Handles btnSkip.Click
        QTxtHasChgd = False
        ATxtHasChgd = False
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
    Public Sub Cancel() Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel the current flashcarding operation in its entirety?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Me.DialogResult = DialogResult.Abort
            Me.Close()
        End If
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
#End Region

    Private Sub QuestionsChanged() Handles txtQs.TextChanged
        QTxtHasChgd = True
    End Sub

    Private Sub AnswersChanged()
        ATxtHasChgd = True
    End Sub

    ' Add an answer
    Private Sub AddAnswer() Handles btnAddAnswer.Click

        ' Answers have changed (because there is a new one)
        ATxtHasChgd = True

        ' Make a new answer textbox
        MakeAnswerTextbox("", answerTbxList.Count > 0)

    End Sub

    ' Function that makes an answer textbox
    Private Sub MakeAnswerTextbox(ByVal Answer As String, ByVal CanResizeForm As Boolean)

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

        ' Add answer
        newTbx.Text = Answer

        ' Add on-changed handler
        AddHandler newTbx.TextChanged, AddressOf AnswersChanged

        ' -- Form manipulation --
        If CanResizeForm Then
            Me.Height += QuestionAnswerTbxSize + 40
        End If

        ' Resize handler adjusts everything else accordingly
        ResizeHandler()

    End Sub

    ' Text adding buttons
    Private Sub AddBefore() Handles btnAddBefore.Click
        txtQs.Text = acTbx.Text.TrimEnd & " " & txtQs.Text
    End Sub
    Private Sub AddAfter() Handles btnAddAfter.Click
        txtQs.Text = txtQs.Text & acTbx.Text
    End Sub

    ' Toggle/update questionmark appending
    Private Sub cbxAppendQMarkChanged() Handles cbxAppendQMark.CheckedChanged, Me.Shown

        ' Skip if form is being hidden (to avoid changing saved QAM object without user's knowledge)
        If Not Me.Visible Then
            Exit Sub
        End If

        ' Remove any existing questionmarks
        txtQs.Text = txtQs.Text.TrimEnd({"?"c, "."c})

        ' Append a new one if necessary
        If cbxAppendQMark.Checked Then
            txtQs.Text &= If(IsDefinitionQuestion, ".", "?")
        End If

    End Sub

    ' Toggle/update first letter capitalization
    Private Sub cbxCapitalizeQuestion() Handles cbxCapQ.CheckedChanged, Me.Shown, btnAddBefore.Click

        ' Check for text that is too short for the system to work properly
        If txtQs.Text.Length < 2 Then
            Exit Sub
        End If

        ' Un/capitalize first letter
        Dim FirstChar As String = txtQs.Text.Substring(0, 1).ToLowerInvariant
        txtQs.Text = If(cbxCapQ.Checked, FirstChar.ToUpperInvariant, FirstChar) & txtQs.Text.Remove(0, 1)

    End Sub
    Private Sub cbxCapitalizeAnswers() Handles cbxCapA.CheckedChanged, Me.Shown, btnAddBefore.Click

        ' Check for text that is too short for the system to work properly
        If txtQs.Text.Length < 2 Then
            Exit Sub
        End If

        ' Un/capitalize first letter
        For i = 0 To answerTbxList.Count - 1

            Dim Text As String = answerTbxList.Item(i).Text
            If Text.Length > 0 Then
                Dim FirstChar As String = Text.Substring(0, 1).ToLowerInvariant
                answerTbxList.Item(i).Text = If(cbxCapA.Checked, FirstChar.ToUpperInvariant, FirstChar) & Text.Remove(0, 1)
            End If

        Next


    End Sub

    Private Sub QuestionsChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQs.TextChanged

    End Sub
End Class
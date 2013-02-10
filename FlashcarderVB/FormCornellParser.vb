Imports System.Text.RegularExpressions

Public Class FormCornellParser

    ' List of extracted question objects
    Public Shared ExtractedQAMList As New List(Of Question)

    Private Sub tb1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tb1.TextChanged

        Dim CurIndex As Integer = 0

        ' Reset highlighting
        tb1.BackColor = Color.White

        ' Highlight according to classification
        For i = 0 To tb1.Lines.Count - 1

            ' Get current line and data about it
            Dim CurLine As String = tb1.Lines.GetValue(i)
            Dim IsHeader As Boolean = i <> tb1.Lines.Count - 1 AndAlso CornellParsingAI.lineIsHeader(CurLine, tb1.Lines.GetValue(i + 1)) ' Some ninja short-circuiting

            ' Definitions - lime green
            If CornellParsingAI.lineIsDefinition(CurLine) Then
                HighlightTBox(CurIndex, CurLine.Length, Color.LimeGreen)
                Dim Q As Question = CornellParsingAI.questionFromDefinition(CurLine)

                ' Write question to output
                Console.WriteLine(Q.Question & " / " & Q.AnswerList.First)

            End If

            ' Headers - pink
            If i < tb1.Lines.Count - 1 AndAlso IsHeader Then
                HighlightTBox(CurIndex, CurLine.Length, Color.HotPink)

                ' Assemble question
                Dim Answer As String = ""
                Dim QLines As List(Of String) = CornellParsingAI.findList(tb1.Lines, i)
                For j = 1 To QLines.Count - 1 ' Skip the header (j = 1)

                    Dim Line As String = QLines.Item(j)

                    ' NOTE: A line can be BOTH a legitimate question header AND a definition!

                    ' The part of the line that is a question (note: definitions have both a question and answer)
                    Dim QuestionPart As String = Line.Trim

                    If CornellParsingAI.lineIsDefinition(Line) Then

                        ' Definitions
                        QuestionPart = QuestionPart.Remove(Line.IndexOf(" - ")).Trim({" "c, CChar(vbTab)}) ' Isolate the question component of the definition

                    End If

                    ' Answers (this applies regardless of whether the line is a definition)
                    Answer &= QuestionPart & If(j <> QLines.Count - 1, ", ", "") ' Don't append a comma to the last line

                Next

                ' Output answer to QAM creation dialog
                If QLines.Count <> 0 Then

                    ' Isolate question
                    Dim QuestionPart As String = QLines.Item(0)

                    ' Isolate answer (here, there is only one)
                    Dim AnswerList As New List(Of String)
                    AnswerList.Add(Answer)

                    ' Update autocomplete suggestions
                    Dim AutoCompleteSuggestions As String() = ({"Who were ",
                                                    "What are the meanings of ",
                                                    "What are the significances of ",
                                                    "Name the [#]",
                                                    "What are the [#] types of",
                                                    "What are the [#]"}) ' Some sample values for now

                    ' Format auto-complete suggestions
                    For j = 0 To AutoCompleteSuggestions.Count - 1

                        Dim CurValue As String = AutoCompleteSuggestions.GetValue(j).ToString

                        ' Regex stuff
                        Dim L As String = "(?<=(\A|\s))"
                        Dim R As String = "(?=(\Z|\s))"

                        ' Singular/plural handlers
                        If Not Answer.Contains(",") Then

                            ' Singular
                            CurValue = Regex.Replace(CurValue, "s" & R, "") ' plurals --> singulars (note: this doesn't affect were --> was or are --> is - those are deliberately after this)
                            CurValue = Regex.Replace(CurValue, L & "are" & R, "is") ' are --> is
                            CurValue = Regex.Replace(CurValue, L & "were" & R, "was") ' were --> was

                        Else

                            ' Plural

                        End If

                        ' Spaces
                        CurValue = CurValue.Trim

                        ' Answer count insertion
                        CurValue = CurValue.Replace("[#]", Regex.Matches(Answer, ",").Count + 1)

                        ' Update answer list (with correctly formatted value)
                        AutoCompleteSuggestions.SetValue(CurValue, j)

                    Next

                    ' Load auto-complete suggestions into Cornell AI question editor
                    FormCornellAIEditor.acTbx.AutoCompleteSuggestions.Clear()
                    FormCornellAIEditor.acTbx.AutoCompleteSuggestions.AddRange(AutoCompleteSuggestions)

                    ' Load QAM details into Cornell AI question editor
                    FormCornellAIEditor.QAMObj = New Question(QLines.Item(0), AnswerList, 0)
                    FormCornellAIEditor.DialogResult = DialogResult.None
                    Dim Result As DialogResult = FormCornellAIEditor.ShowDialog()

                    ' Save finalized QAM object
                    If Result = DialogResult.OK Then
                        ExtractedQAMList.Add(FormCornellAIEditor.QAMObj)
                    End If

                End If

            End If

            ' Track current line index
            CurIndex += CurLine.Length + 1

        Next

        ' Done (DBG)
        Dim Z = 1

    End Sub

    ' Highlighting method
    Public Sub HighlightTBox(ByVal Start As Integer, ByVal Len As Integer, ByVal Clr As Color)
        tb1.Select(Start, Len)
        tb1.SelectionBackColor = Clr
        tb1.DeselectAll()
    End Sub

    Private Sub FormCornellParser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
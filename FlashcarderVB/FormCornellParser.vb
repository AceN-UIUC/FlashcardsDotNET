Public Class FormCornellParser

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
                        QuestionPart = QuestionPart.Remove(Line.IndexOf(" - ")) ' Isolate the question component of the definition
                        Console.WriteLine(CornellParsingAI.questionFromDefinition(Line).Question)

                    End If

                    ' Answers (this applies regardless of whether the line is a definition)
                    Answer &= QuestionPart & ", "

                Next

                ' Output answer
                If QLines.Count <> 0 Then
                    Console.WriteLine(QLines.Item(0) & " / " & Answer)
                End If

            End If

            ' Track current line index
            CurIndex += CurLine.Length + 1

        Next

    End Sub

    ' Highlighting method
    Public Sub HighlightTBox(ByVal Start As Integer, ByVal Len As Integer, ByVal Clr As Color)
        tb1.Select(Start, Len)
        tb1.SelectionBackColor = Clr
        tb1.DeselectAll()
    End Sub

End Class
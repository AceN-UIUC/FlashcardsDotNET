Imports System.Text.RegularExpressions

Public Class CornellParsingAI

    ' GOAL 1: the primary goal is to identify question lines and header lines
    ' GOAL 2: create questions from everything using keyword detection and synonym arrays

    ' NOTE: We need some way of identifying questions similar to the current one and combining them (ask user whether or not to)
    '   Perhaps Levenshtein/findLongestSequence type stuff

    ' Header-and-list fetcher

    ' --- Question assemblers ---

    ' Definitions / Names
    '   Example definitions / names: "lock - when a program waits for an operation to complete", "Flynn - founder of TRON", etc...
    Public Shared Function questionFromDefinition(ByVal CurLine As String) As Question

        ' Input verification
        Dim Idx As Integer = CurLine.IndexOf(" - ")
        If Idx = -1 Then
            Throw New ArgumentException("That is not a valid definition. Valid definitions must have a ' - ' or similar sequence.")
            Exit Function
        End If

        ' Initialize question
        Dim QuestionObj As New Question("", New List(Of String), 0)

        ' Get question text
        QuestionObj.Question = CurLine.Remove(Idx).Trim & "."
        If Regex.Matches(QuestionObj.Question, "[A-Z]").Count > Regex.Matches(QuestionObj.Question, "\s+").Count + 1 Then

            ' Names
            QuestionObj.Question = "Who was " & QuestionObj.Question

        Else

            ' Definitions
            QuestionObj.Question = "Define " & QuestionObj.Question.ToLowerInvariant

        End If

        ' Get answer text
        QuestionObj.AnswerList.Add(CurLine.Remove(0, Idx + 2).Trim)

        ' Return
        Return QuestionObj

    End Function

    ' Lists
    '   This is going to be harder...
    Public Shared Function findList(ByVal Lines As String(), ByVal CurLineIndex As Integer) As List(Of String)

        Dim OutputLines As New List(Of String) ' List of output lines

        Dim BuildingList As Boolean = False ' Flag indicating whether list building is finished
        Dim ListTabCount As Integer = 0 ' # of tabs in the list's lines

        For i = CurLineIndex To Lines.Count - 2 ' The -2 prevents the very last line from being considered (so that there is always a next line)

            Dim Line As String = Lines.GetValue(i).ToString

            Dim IsHeader As Boolean = lineIsHeader(Line, Lines.GetValue(i + 1).ToString)
            Dim TabCount As Integer = Line.Length - Line.Replace(vbTab, "").Length

            ' Check if current line is a header
            If IsHeader AndAlso Not BuildingList Then

                BuildingList = True ' Start building list
                ListTabCount = TabCount + 1 ' Tab count of list's lines
                OutputLines.Add(Line)
                Continue For

            ElseIf IsHeader AndAlso BuildingList AndAlso Not lineIsDefinition(Line) Then

                ' Skip this line if it isn't a definition (because it is the header of the next list)
                Continue For

            End If

            ' Turn off building (only when tab count is less than list's tab count)
            If TabCount < ListTabCount Then
                Exit For
            End If

            ' Make sure current line has the proper # of tabs
            If TabCount <> ListTabCount Then
                Continue For
            End If

            ' Add line to list (if not building)
            OutputLines.Add(Lines.GetValue(i).ToString)

        Next

        ' Return
        Return OutputLines

    End Function

    ' --- Boolean classifiers ---

    ' Returns TRUE if the line is a definition, FALSE otherwise
    Public Shared Function lineIsDefinition(ByVal CurLine As String) As Boolean
        Return CurLine.Contains(" - ") ' A naive approach, but it works
    End Function

    ' Returns TRUE if the line is a header, FALSE otherwise
    Public Shared Function lineIsHeader(ByVal CurLine As String, ByVal NextLine As String) As Boolean

        ' Get tab counts
        Dim CurLineTabs As Integer = CurLine.Length - CurLine.Replace(vbTab, "").Length
        Dim NextLineTabs As Integer = NextLine.Length - NextLine.Replace(vbTab, "").Length

        ' Compare tab counts
        Return NextLineTabs > CurLineTabs

    End Function

End Class

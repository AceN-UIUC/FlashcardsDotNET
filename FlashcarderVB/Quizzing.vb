Imports System.IO
Imports System.Text.RegularExpressions

' Methods for quizzing material
Public Class Quizzing

    ' Error message
    Shared NumMismatchMsg As String = "The number of lines in each file don't match up. The files will be parsed to their maximum extent."

    ' Load a flashcard set into memory
    Shared Function Load(ByVal QPath As String, ByVal APath As String, ByVal MPath As String) As List(Of Question)

        ' Error message boolean (so user doesn't get spammed with error messages)
        Dim NumMismatchUsed As Boolean = False

        ' Initialize lists
        Dim QAMList As New List(Of Question)

        ' - Check file presence -
        Dim FilesAreOK As Boolean = True
        If String.IsNullOrWhiteSpace(Dir(QPath)) Then
            MsgBox("The questions file could not be found at" & vbLf & QPath)
            FilesAreOK = False
        End If
        If String.IsNullOrWhiteSpace(Dir(APath)) Then
            MsgBox("The answers file could not be found at" & vbLf & APath)
            FilesAreOK = False
        End If

        If Not FilesAreOK Then
            MsgBox("The flashcards could not be loaded because of a missing file.")
            Return New List(Of Question)
        End If

        Dim MarkingsExist As Boolean = True
        If String.IsNullOrWhiteSpace(MPath) Then
            MarkingsExist = False
        ElseIf String.IsNullOrWhiteSpace(Dir(MPath)) Then
            MsgBox("The markings file could not be found at:" & vbLf & MPath & vbLf & "This file will be automatically generated.")
            MarkingsExist = False
        End If

        ' Give a default markings path as a target for a new markings list (if no markings list exists)
        If Not MarkingsExist Then
            MPath = QPath.Substring(0, QPath.LastIndexOf("\")) & "\markings.txt"
        End If

        ' --- Identify subject, if necessary ---
        Dim SR As New StreamReader(QPath)
        SubjectChoiceDlg.SubjectList.Clear()
        While Not SR.EndOfStream

            Dim Str As String = SR.ReadLine

            ' Input verification / Line exclusion condition
            If String.IsNullOrWhiteSpace(Str) Then
                Continue While
            End If

            ' Line exclusion conditions
            If Not Str.StartsWith("[") Then
                Continue While
            ElseIf Not Str.EndsWith("]") Then
                Continue While
            End If

            ' Add any discovered subjects to list
            SubjectChoiceDlg.SubjectList.Add(Str.Substring(1).Substring(0, Str.Length - 2))

        End While
        SR.Close()

        If SubjectChoiceDlg.SubjectList.Count = 1 Then

            SubjectChoiceDlg.Subject = SubjectChoiceDlg.SubjectList.Item(0)

        ElseIf SubjectChoiceDlg.SubjectList.Count > 1 Then

            ' Add existing subjects to subject choice dialog
            SubjectChoiceDlg.lbox.Items.Clear()
            For Each S As String In SubjectChoiceDlg.SubjectList
                SubjectChoiceDlg.lbox.Items.Add(S)
            Next

            ' Display subject choice dialog
            SubjectChoiceDlg.ShowDialog()

            ' If no subject chosen, stop loading procedure
            If String.IsNullOrWhiteSpace(SubjectChoiceDlg.Subject) Then
                MsgBox("No subject was chosen. Multiple-subject files require that a specific subject be chosen. The loading process will now be exited. No questions were loaded.", MsgBoxStyle.Exclamation)
                Return New List(Of Question)
            End If

        End If

        ' --- End Subject identification ---

        ' Get questions
        Dim QSR As New StreamReader(QPath)
        Dim EditModeInit As Boolean = String.IsNullOrWhiteSpace(SubjectChoiceDlg.Subject) ' If this is true, a problem has occurred (an earlier input verifier is supposed to prevent that)
        Dim EditMode As Boolean = EditModeInit
        While Not QSR.EndOfStream

            Dim Str As String = QSR.ReadLine

            ' Skip bad file lines
            If String.IsNullOrWhiteSpace(Str) Then
                Continue While ' Null lines
            End If

            ' Edit mode controls (edit mode = desired subject is activated)
            If Not EditMode Then

                ' Line skips
                If Not Str.StartsWith("[") Then
                    Continue While
                ElseIf Not Str.EndsWith("]") Then
                    Continue While
                End If

                ' Subject identification
                EditMode = Str.Substring(1).Substring(0, Str.Length - 2) = SubjectChoiceDlg.Subject

            ElseIf Str.StartsWith("[") Then
                If Str.EndsWith("]") And Not EditModeInit Then
                    EditMode = False
                    Exit While
                End If
            End If

            ' Get question #
            Dim QNum As Integer = 0
            If Str.Substring(0, 5).Contains("=") Then
                QNum = CInt(Str.Substring(0, Str.IndexOf("=")))
            End If

            Try
                If QNum <> 0 Then

                    ' Extend question list, if necessary
                    While QAMList.Count < QNum
                        QAMList.Add(New Question("", New List(Of String), 0))
                    End While

                    ' Add question to list
                    QAMList.Item(QNum - 1).Question = Str.Substring(Str.IndexOf("=") + 1)

                ElseIf QAMList.Count <> 1 Then

                    ' Add question to list (in order of appearance in file)
                    QAMList.Add(New Question(Str, New List(Of String), 0))


                End If
            Catch e As ArgumentOutOfRangeException
                If Not NumMismatchUsed Then
                    NumMismatchUsed = True
                    MsgBox(NumMismatchMsg)
                End If
            End Try

        End While
        QSR.Close()

        ' Get answers
        Dim ASR As New StreamReader(APath)
        Dim Cnt As Integer = 0
        EditMode = EditModeInit
        While Not ASR.EndOfStream

            Dim Str As String = ASR.ReadLine
            If String.IsNullOrWhiteSpace(Str) Then
                Continue While
            End If

            ' Skip bad file lines
            If String.IsNullOrWhiteSpace(Str) Then
                Continue While ' Null lines
            End If

            ' Edit mode controls (edit mode = desired subject is activated)
            If Not EditMode Then

                ' Line skips
                If Not Form1.IsSubjectLine(Str) Then
                    Continue While
                End If

                ' Subject identification
                EditMode = Regex.Match(Str, Form1.SubjectNameRegex).Value = SubjectChoiceDlg.Subject

            ElseIf Form1.IsSubjectLine(Str) And Not EditModeInit Then
                EditMode = False
                Exit While
            End If

            ' Get answer number
            Dim ANum As Integer = 0
            If Integer.TryParse(Str.First, New Integer) Then ' Exclude subject indicators and comments
                If Str.Substring(0, Math.Min(6, Str.Length)).Contains("=") Then

                    Dim Str2 As String = Str.Substring(0, Str.IndexOf("="))

                    ' Trim off last character(s) if they are letters
                    Dim F As String = Str2.Substring(Str2.Length - 1, 1)
                    While F.ToLower <> F.ToUpper
                        Str2 = Str2.Substring(0, Str2.Length - 1)
                        F = Str2.Substring(Str2.Length - 1, 1)
                    End While

                    ' Get answer #
                    ANum = CInt(Str2)

                End If

                ' Add answer to list
                Try
                    If ANum <> 0 Then
                        QAMList.Item(ANum - 1).AnswerList.Add(Str.Substring(Str.IndexOf("=") + 1))
                    Else
                        QAMList.Item(Cnt).AnswerList.Add(Str.Substring(Str.IndexOf("=") + 1))
                    End If
                Catch e As ArgumentOutOfRangeException
                    If Not NumMismatchUsed Then
                        NumMismatchUsed = True
                        MsgBox(NumMismatchMsg)
                    End If
                End Try

                ' Add to count
                Cnt += 1

            End If

        End While
        ASR.Close()

        ' Get (or generate) markings
        Dim MarkCnt As Integer = 0
        EditMode = EditModeInit
        If MarkingsExist Then

            ' Get markings (for missing ones: generate them and assume they are 0)
            Dim MR As New StreamReader(MPath)
            While Not MR.EndOfStream

                Dim Line As String = MR.ReadLine
                If String.IsNullOrWhiteSpace(Line) Then
                    Continue While
                End If

                ' Skip bad file lines
                If String.IsNullOrWhiteSpace(Line) Then
                    Continue While ' Null lines
                End If

                ' Edit mode controls (edit mode = desired subject is activated)
                If Not EditMode Then

                    ' Line skips
                    If Not Form1.IsSubjectLine(Line) Then
                        Continue While
                    End If

                    ' Subject identification
                    EditMode = Regex.Match(Line, Form1.SubjectNameRegex).Value = SubjectChoiceDlg.Subject

                ElseIf Form1.IsSubjectLine(Line) And Not EditModeInit Then
                    EditMode = False
                    Exit While
                End If

                ' Get number (if any)
                Dim MNum As Integer = 0
                Dim RgxMatch As Match = Regex.Match(Line, "^\d+(?=(\=))")
                If RgxMatch.Success Then
                    If Integer.TryParse(RgxMatch.Value, MNum) AndAlso MNum <= QAMList.Count Then ' RgxMatch.value WILL NOT have negative signs, so don't check for them here
                        QAMList.Item(MNum - 1).Marking = CInt(Regex.Match(Line, "(?<=(\=))\d+").Value)
                    ElseIf Not NumMismatchUsed Then
                        NumMismatchUsed = True
                        MsgBox(NumMismatchMsg)
                    End If

                    MarkCnt += 1
                End If

            End While

            MR.Close()

            ' Check verification box (markings have been fixed)
            FormQuestionManager.cbx_VrifyMs.Checked = True

        ElseIf QAMList.Count <> 0 Then

            ' Generate markings (mark everything as 0)
            Try
                Dim LineList As New List(Of String)

                ' Add a spacing line between the current markings-file-text and the new markings
                If File.Exists(MPath) AndAlso New FileInfo(MPath).Length <> 0 Then
                    LineList.Add("")
                End If

                If Not String.IsNullOrWhiteSpace(SubjectChoiceDlg.Subject) Then
                    LineList.Add("[" & SubjectChoiceDlg.Subject & "]")
                End If

                For i = 1 To QAMList.Count
                    LineList.Add(CStr(i) & "=0")
                Next

                File.AppendAllLines(MPath, LineList)

                ' Check verification box (markings have been fixed)
                FormQuestionManager.cbx_VrifyMs.Checked = True

            Catch
                MsgBox("New markings file could not be generated. Check the validity of the markings file-path.")
            End Try

        End If

        ' Re-write markings file if necessary
        If MarkCnt <> QAMList.Count And MarkingsExist Then

            Try
                Dim LineList As New List(Of String)

                LineList.AddRange(File.ReadAllLines(MPath))

                ' Find subject starting point (in line list)
                Dim SStart As Integer = 0
                If Not String.IsNullOrWhiteSpace(SubjectChoiceDlg.Subject) Then
                    SStart = LineList.IndexOf("[" & SubjectChoiceDlg.Subject & "]")
                End If

                If SStart <> -1 Then

                    ' Find subject ending point (in line list)
                    Dim SEnd As Integer = LineList.Count - 1
                    For i = SStart + 1 To SEnd
                        Dim L As String = LineList.Item(i)
                        If Form1.IsSubjectLine(L) Then
                            SEnd = i - 1
                        End If
                    Next

                    ' Insert any markings that dont exist
                    Dim MarkingIdx As Integer = 0
                    If Not String.IsNullOrWhiteSpace(SubjectChoiceDlg.Subject) Then
                        MarkingIdx = 1
                    End If
                    For i = 1 To QAMList.Count

                        ' Check to see if marking exists
                        Dim Exists As Boolean = False
                        Dim Num As Integer = -1
                        For j = SStart To SEnd

                            Dim S As String = LineList.Item(j)
                            If S.Contains("=") AndAlso Integer.TryParse(S.Substring(0, S.IndexOf("=")), Num) Then
                                If Num = i Then
                                    Exists = True
                                    Exit For
                                ElseIf Num = i - 1 Then
                                    MarkingIdx = j + 1
                                End If
                            End If

                        Next

                        ' If marking doesn't exist, add it in
                        If Not Exists Then
                            LineList.Insert(MarkingIdx, CStr(i) & "=0")
                            SEnd += 1 ' To compensate for additional item in LineList
                        End If

                    Next

                End If

                File.WriteAllLines(MPath, LineList)

            Catch
                MsgBox("New markings file could not be generated. Check the validity of the markings file-path.")
            End Try

        End If

        ' Return
        Return QAMList

    End Function

    ' Quiz a flashcard set (the classic method)
    Shared Sub QuizClassic(ByRef QAMList As List(Of Question), ByVal MinIncorrect As Integer, ByVal QuestionCnt As Integer, ByRef QAMWasEdited As Boolean)

        Dim Randomize As Boolean = FormQuestionManager.cbxRandomQs.Checked
        Dim Rand As New Random
        Dim AskedQNums As New List(Of Integer)

        For i = 0 To QuestionCnt - 1

            ' Exit condition
            If AskedQNums.Count = QAMList.Count Then
                Exit For
            End If

            ' Get question index
            Dim QIdx As Integer = i + 1
            If Randomize Then

                QIdx = Rand.Next(1, QAMList.Count + 1)
                While AskedQNums.Contains(QIdx)
                    QIdx = Rand.Next(1, QAMList.Count + 1)
                End While
                AskedQNums.Add(QIdx)

            End If

            ' If the question has been answered correctly too many times, skip it
            If QAMList.Item(QIdx - 1).Marking < MinIncorrect Then
                Continue For
            End If

            ' Get question/answer/marking
            FormQuestionPoser.QAMObj = QAMList.Item(QIdx - 1)

            ' Ask question using question poser form
            '   This pauses the current thread - props to http://stackoverflow.com/questions/9508157/making-a-form-halt-other-program-activity-like-a-messagebox-in-vb-net
            FormQuestionPoser.ShowDialog()

            ' Exit the loop if the quiz process is supposed to be stops
            If Not FormQuestionPoser.QuestionIsOKd Then
                Exit For
            End If

        Next

    End Sub

    ' Format an answer to be in accordance with the general answer format
    Shared Function FormatAnswer(ByVal Str As String)

        ' ---- List of unallowed characters ----
        ' Parenthesis, Brackets, Slashes, Conjunctions, Periods/questionmarks, Spaces
        Dim RemovedChars As String = "()[]/\.?"
        Dim RemovedWords As String() = {" a ", " an ", " the ", " or ", ".", "?"}

        ' --- Format the input string ---
        Str = Str.ToLower

        ' Character removal
        For Each c As Char In RemovedChars
            Str = Str.Replace(c, "")
        Next

        ' Word removal
        For Each w As String In RemovedWords
            Str = Str.Replace(w, "")
        Next

        ' Spaces (this MUST come last to avoid mucking up the stuff above!)
        Str = Regex.Replace(Str, "\s{2,}", "")

        ' Return
        Return Str

    End Function

End Class
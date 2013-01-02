Imports System.IO
Imports System.Speech

' RESTRICTIONS
'   1 question can only have up to 26 anwers

' Types of questions
'   Multichoice
'   Definition
'   Dynamic math? (someday...)

Public Class Form1

    Declare Function Sleep Lib "kernel32" (ByVal dwMilliseconds As Integer) As Integer

    Public MainTitle As String = "Ace's Flashcarder"

    Public LetterStr As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"

    Public QHasChgd As Boolean = False
    Public AHasChgd As Boolean = False


    Private Sub btnIntraMgr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIntraMgr.Click
        FormQuestionManager.Show()
    End Sub

    Private Sub btnInterMgr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInterMgr.Click
        FormFCCoordinator.Show()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = MainTitle & " - Main Menu"
    End Sub
End Class

' Methods for editing material
Public Class Editing

    Shared Sub UpdateQAM(ByVal QAMList As List(Of Question), ByVal QPath As String, ByVal APath As String, ByVal MPath As String, _
                        Optional ByVal Subject As String = "")

        ' (Obligatory) file validity check
        If Dir(QPath).Length < 1 Then
            MsgBox("Question path is invalid (Editing.UpdateQAM).")
        ElseIf Dir(APath).Length < 1 Then
            MsgBox("Answer path is invalid (Editing.UpdateQAM).")
        ElseIf Dir(MPath).Length < 1 Then
            MsgBox("Markings path is invalid (Editing.UpdateQAM).")
        End If

        ' Starting variables
        Dim SubjectChkActive As Boolean = String.IsNullOrWhiteSpace(Subject)
        Dim First As Boolean = True
        Dim EditEngaged As Boolean
        Dim Cnt As Integer = 1
        Dim StrLst As New List(Of String) ' List for storing the file's lines

        ' --------------- Questions ---------------

        ' Start reading (not the most efficient, use FileStream if this is too slow)
        If Form1.QHasChgd AndAlso QPath.Length > 0 AndAlso File.Exists(QPath) Then

            Dim QR As New StreamReader(QPath)
            EditEngaged = String.IsNullOrWhiteSpace(Subject)
            Cnt = 1
            While Not QR.EndOfStream

                Dim Str As String = QR.ReadLine

                ' Turn off edit mode if appropriate
                If EditEngaged And (Str.StartsWith("[") And Str.EndsWith("]")) Then
                    EditEngaged = False
                End If

                ' Check for separate subjects/subject markers
                '   NOTE: This does NOT trigger if the first line is a subject marker (i.e. a 1 subject file)
                If Not First And SubjectChkActive Then
                    If Str.StartsWith("[") And Str.EndsWith("]") Then

                        ' If no subject specified and multiple subjects are detected, alert user and exit everything
                        MsgBox("Subject was not specified when using multiple-subject file. The file has not been modified, but the modification operation was cancelled. (Editing.UpdateQAM)")
                        QR.Close()
                        Exit Sub

                    End If
                End If
                First = False

                ' Depending on edit status, write proper line to line list
                If EditEngaged And Cnt <= QAMList.Count Then
                    StrLst.Add(CStr(Cnt) & "=" & QAMList.Item(Cnt - 1).Question)
                    Cnt += 1
                Else
                    StrLst.Add(Str)
                End If

                ' Turn on edit mode if appropriate
                If Not EditEngaged And Str = "[" & Subject & "]" Then
                    EditEngaged = True
                End If

            End While

            ' Write text
            QR.Close()
            File.WriteAllLines(QPath, StrLst.ToArray)
            StrLst = New List(Of String)

        End If

        ' --------------- Answers ---------------
        ' Note: this recycles some variables from the questions part

        ' Loop
        If Form1.AHasChgd AndAlso APath.Length > 0 AndAlso File.Exists(APath) Then

            ' Declare/reset variables
            Dim AR As New StreamReader(APath)
            EditEngaged = String.IsNullOrWhiteSpace(Subject)
            Cnt = 1

            Dim LastCnt As Integer = -1
            Dim LastLine As String = ""

            While Not AR.EndOfStream

                Dim Str As String = AR.ReadLine

                Try
                    ' Turn off edit mode if appropriate
                    If EditEngaged And (Str.StartsWith("[") And Str.EndsWith("]")) Then
                        StrLst.Add(LastLine) ' If EditEngaged was true for the last line and it wasn't part of a QAM object, this prevents it from being overwritten
                        EditEngaged = False
                    End If

                    ' Skip answers in the old file that have already been loaded into the new file (TODO)
                    'If Str.StartsWith(LastCnt.ToString) Then
                    '    Continue While
                    'End If

                    ' Depending on edit status, write proper line to line list
                    If EditEngaged And Cnt <= QAMList.Count Then
                        For i = 0 To QAMList.Item(Cnt - 1).AnswerList.Count - 1

                            ' Add answer to list
                            StrLst.Add(CStr(Cnt) & Form1.LetterStr.Substring(i, 1) & "=" & QAMList.Item(Cnt - 1).AnswerList.Item(i))

                            '

                        Next

                        LastCnt = Cnt
                        Cnt += 1

                    ElseIf Not EditEngaged Then ' Don't copy things if Edit mode is on AND all the Q/A/M lines have been added
                        StrLst.Add(Str)
                    End If

                Catch
                End Try

                ' Update last line
                LastLine = Str

                ' Turn on edit mode if appropriate
                If Not EditEngaged And Str = "[" & Subject & "]" Then
                    EditEngaged = True
                End If

            End While
            AR.Close()

            ' Write to answers file
            File.WriteAllLines(APath, StrLst)
            StrLst = New List(Of String)

        End If

        ' --------------- Markings ---------------
        ' NOTE: This breaks if the subject header for a particular subject isn't present in the markings file
        If MPath.Length > 0 AndAlso File.Exists(MPath) Then

            ' Declare variables
            Dim MR As New StreamReader(MPath)
            EditEngaged = String.IsNullOrWhiteSpace(Subject)
            Cnt = 1

            ' Update existing markings if they exist
            Dim EditWasEngaged As Boolean = False
            While Not MR.EndOfStream

                Dim Str As String = MR.ReadLine

                Try

                    ' Turn off edit mode if appropriate
                    If EditEngaged And (Str.StartsWith("[") And Str.EndsWith("]")) Then
                        EditEngaged = False
                    End If

                    ' Depending on edit status, write proper line to line list
                    If EditEngaged And Cnt <= QAMList.Count Then

                        StrLst.Add(CStr(Cnt) & "=" & QAMList.Item(Cnt - 1).Marking)
                        Cnt += 1

                    Else
                        StrLst.Add(Str)
                    End If

                Catch
                End Try

                ' Turn on edit mode if appropriate
                If Not EditEngaged And Str = "[" & Subject & "]" Then
                    EditWasEngaged = True
                    EditEngaged = True
                End If

            End While
            MR.Close()

            If EditWasEngaged Then

                ' Write to answers file
                If File.Exists(MPath) Then
                    File.WriteAllLines(MPath, StrLst)
                Else
                    MsgBox("The markings file previously referenced no longer exists. It may have been moved or deleted. No markings have been modified.")
                End If

            Else

                ' - Append markings to answers file -

                ' Generate new string list
                StrLst.Clear()
                StrLst.Add("") ' An empty line
                StrLst.Add("[" & Subject & "]") ' Subject indicator
                For i As Integer = 1 To QAMList.Count
                    StrLst.Add(CStr(i) & "=" & QAMList.Item(i - 1).Marking)
                Next

                ' Append string list to file
                If File.Exists(MPath) Then
                    File.AppendAllLines(MPath, StrLst)
                Else
                    MsgBox("The markings file previously referenced no longer exists. It may have been moved or deleted. No markings have been modified.")
                End If

            End If

        End If


        ' --------------- Return ----------------
        Return

    End Sub

    ' Get file type (Q/A/M)
    Shared Function GetFileType(ByVal FPath As String) As String

        ' Get file name
        Dim FileName As String = FPath.Substring(FPath.LastIndexOf("\") + 1)
        FileName = FileName.Substring(0, FileName.Length - 4).ToLowerInvariant

        ' - Get file type (which of Q/A/M is it) and check appropriate file validation boxes -
        ' By name (accepts "questions" and "qs")
        Dim NameList As String() = {"questions", "qs", "answers", "as", "markings", "ms"}
        Dim FileType As String = ""
        If NameList.Contains(FileName) Then
            FileType = FileName.First
        End If

        ' By file content (only if file name didn't turn up anything)
        If FileType.Length = 0 Then

            ' Get file content
            Dim StrArray As String() = File.ReadAllLines(FPath)

            Dim Threshold As Integer = CInt(Math.Round(Math.Sqrt(StrArray.Count * 0.9)))
            Dim QCnt As Integer = 0
            Dim ACnt As Integer = 0
            Dim MCnt As Integer = 0

            ' Iterate through file content
            For Each S As String In StrArray

                ' Exception handler
                If Not S.Contains("=") Then
                    Continue For
                End If

                ' Get strings on both sides of the equal sign
                Dim LStr As String = S.Substring(0, S.IndexOf("="))
                Dim RStr As String = S.Substring(S.IndexOf("=") + 1)

                ' Questions
                If Integer.TryParse(LStr, New Integer) And Not Integer.TryParse(RStr, New Integer) Then
                    QCnt += 1
                End If

                ' Answers
                If Not Integer.TryParse(LStr, New Integer) And Not Integer.TryParse(RStr, New Integer) Then

                    Dim LStrNum As String = LStr
                    For i = 0 To 9
                        LStrNum = LStrNum.Replace(CStr(i), "")
                    Next

                    If LStrNum.Length - LStr.Length = -1 Then
                        ACnt += 1
                    End If

                End If

                ' Markings
                If Integer.TryParse(LStr, New Integer) And Integer.TryParse(RStr, New Integer) Then
                    MCnt += 1
                End If

                ' Final type identification
                If QCnt > Threshold Then
                    FileType = "q"
                    Exit For
                ElseIf ACnt > Threshold Then
                    FileType = "a"
                    Exit For
                ElseIf MCnt > Threshold Then
                    FileType = "m"
                    Exit For
                End If

            Next

        End If

        ' Return
        Return FileType

    End Function

End Class

' Methods for learning material
Public Class Learning

    ' Vocalizes questions along with their answers
    Public Sub VocalizeQuestions(ByVal P As String, ByVal StartIdx As Integer, ByVal StopIdx As Integer, Optional ByVal MathMode As Boolean = True, _
                                      Optional ByVal WaitBetweenQandA As Integer = 200, Optional ByVal WaitBetweenQs As Integer = 100, _
                                      Optional ByVal SpeakerSpeed As Integer = -3, Optional ByVal SayQuestionNumber As Boolean = True, _
                                      Optional ByVal EnableSkip As Boolean = False, Optional ByVal MinFailures As Integer = 0, _
                                      Optional ByVal EditFormatting As Boolean = False, Optional ByVal QuestionRepeatCnt As Integer = 3, _
                                      Optional ByVal PromptUser As Boolean = True)

        ' ----- User settings -----
        'MathMode As Boolean         ' Changes $ to delta, * to times, and / and \ to divide
        'WaitAfterQ                  ' Wait period between question and answer (in ms)
        'WaitBetweenQs               ' Wait period between questions (in ms)
        'QuestionRepeatCnt           ' Number of times to repeat the question
        'SpeakerSpeed                ' Speaking speed (from -10 to 10 inclusive)
        'SayQuestionNumber           ' If this is TRUE, speaker will specify question number
        'EnableSkip                  ' Enable skip box
        'MinFailures                 ' Minimum number of wrong answers to a question
        'EditFormatting              ' Replace formatting with names of formatting characters (comma, parenthesis)
        ' --- End user settings ---

        ' Get initial data
        Dim Subject As String = INIFunctions.Read(P + "\settings.ini", "Settings", "Subject")
        Dim QCount As Integer = CInt(INIFunctions.Read(P + "\settings.ini", "Settings", "NumQs"))
        Dim QADir As String = INIFunctions.Read(P + "\settings.ini", "Settings", "AnswerDir")

        If StopIdx = 0 Then
            StopIdx = QCount
        End If

        ' Vocalizer set up
        Dim Speaker As New Speech.Synthesis.SpeechSynthesizer
        Speaker.Rate = SpeakerSpeed

        ' Start vocalizing
        Try
            For i = StartIdx To StopIdx

                ' Get Q/A filepath
                Dim P2 As String = P + "\" + QADir

                ' Get question's markings and skip if needed
                Dim CorrectCnt As Integer = CInt(INIFunctions.Read(P2 + "\markings.ini", Subject, CStr(i)))
                If CorrectCnt < MinFailures Then
                    Continue For
                End If

                ' Say current question number
                If SayQuestionNumber Then
                    Speaker.Speak("Question " + CStr(i) + " of " + CStr(QCount))
                End If

                ' Get question and answer (Q and A)
                Dim VQ As String = INIFunctions.Read(P2 + "\questions.ini", Subject, CStr(i))
                Dim VA As String = INIFunctions.Read(P2 + "\answers.ini", Subject, CStr(i) + "A")

                ' Math mode
                Dim NoEqn As Boolean = True
                If MathMode Then

                    ' Delta
                    VQ = VQ.Replace("$", " delta ")
                    VA = VA.Replace("$", " delta ")

                    ' Multiplication
                    VQ = VQ.Replace("*", " times ")
                    VA = VA.Replace("*", " times ")

                    ' Division
                    If VA.Contains("=") Then
                        NoEqn = False
                        VQ = VQ.Replace("/", "\").Replace("\", " divided by")
                        VA = VA.Replace("/", "\").Replace("\", " divided by")
                    End If
                End If

                If NoEqn And EditFormatting Then

                    ' Commas
                    VQ = VQ.Replace(",", " comma ")
                    VA = VA.Replace(",", " comma ")

                    ' Parenthesis
                    VQ = VQ.Replace("(", " in parenthesis, ").Replace(")", " end parenthesis")
                    VA = VA.Replace("(", " in parenthesis, ").Replace(")", " end parenthesis")

                End If

                ' Vocalize
                For j = 1 To QuestionRepeatCnt
                    Speaker.Speak(VQ)
                    Form1.Sleep(WaitBetweenQandA)

                    If EnableSkip And j = 1 Then

                        ' Verbal skip prompt
                        Speaker.Rate = 0
                        Speaker.SpeakAsync("Should I skip this? Press Enter for NO or Tab and Enter for YES")
                        Speaker.Rate = SpeakerSpeed

                        Dim M = MsgBox("Don't skip?", MsgBoxStyle.YesNo)
                        If M = MsgBoxResult.Yes Then
                            Speaker.SpeakAsyncCancelAll()
                            Exit For
                        End If
                    End If

                    Speaker.Speak(VA)
                    Form1.Sleep(WaitBetweenQs)

                Next

                If WaitBetweenQs <> 0 Then
                    Speaker.Speak("Next Question.")
                End If

            Next
        Catch
        End Try

    End Sub

    ' Displays questions along with their answers in a message box format
    Public Sub MsgBoxQuestions(ByVal P As String, ByVal StartIdx As Integer, ByVal StopIdx As Integer, _
                                      Optional ByVal IncludeQuestionNumber As Boolean = True, _
                                      Optional ByVal MinFailures As Integer = 0, _
                                      Optional ByVal EditFormatting As Boolean = False, Optional ByVal QuestionRepeatCnt As Integer = 3, _
                                      Optional ByVal PromptUser As Boolean = True)

    End Sub

End Class

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

                Dim Str As String = MR.ReadLine
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

                ' Get number (if any)
                Dim MNum As Integer = 0
                If Str.Substring(0, Math.Min(Str.Length, 5)).Contains("=") Then
                    MNum = CInt(Str.Substring(0, Str.IndexOf("=")))
                    If MNum > 0 AndAlso MNum <= QAMList.Count Then
                        QAMList.Item(MNum - 1).Marking = CInt(Str.Substring(Str.IndexOf("=") + 1))
                    Else
                        If Not NumMismatchUsed Then
                            NumMismatchUsed = True
                            MsgBox(NumMismatchMsg)
                        End If
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

                If Not String.IsNullOrWhiteSpace(Dir(MPath)) Then
                    If New FileInfo(MPath).Length <> 0 Then
                        LineList.Add("")
                    End If
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
                        If L.StartsWith("[") And L.EndsWith("]") Then
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
                            If S.Contains("=") Then
                                If Integer.TryParse(S.Substring(0, S.IndexOf("=")), Num) Then
                                    If Num = i Then
                                        Exists = True
                                        Exit For
                                    ElseIf Num = i - 1 Then
                                        MarkingIdx = j + 1
                                    End If
                                End If
                            End If

                        Next

                        If Not Exists Then
                            LineList.Insert(MarkingIdx, CStr(i) & "=0")
                            SEnd += 1 ' To compenate for additional item in LineList
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
    Shared Sub FormatAnswer(ByRef Str As String)

        ' ---- List of unallowed characters ----
        ' Parenthesis, Brackets, Slashes, Conjunctions, Periods/questionmarks, Spaces
        Dim RemoveArr As String() = {"(", ")", "[", "]", "/", "\", " a ", " an ", " the ", " or ", ".", "?", " "}

        ' --- Format the input string ---
        Str = Str.ToLower
        For i = 0 To RemoveArr.Count - 1
            Str = Str.Replace(RemoveArr.GetValue(i).ToString, "")
        Next

    End Sub

End Class

' Question structure
Public Class Question

    Dim SQ As String
    Dim SAList As New List(Of String)
    Dim SM As Integer
    Dim QAHasChgd As Boolean = False

    Sub New(ByVal QStr As String, ByVal AStrLst As List(Of String), ByVal MCnt As Integer)

        SQ = QStr
        SAList = AStrLst
        SM = MCnt

    End Sub

    Property Question As String
        Set(ByVal value As String)
            SQ = value
        End Set
        Get
            Return SQ
        End Get
    End Property

    Property AnswerList As List(Of String)
        Set(ByVal value As List(Of String))
            SAList = value
        End Set
        Get
            Return SAList
        End Get
    End Property

    Property Marking As Integer
        Set(ByVal value As Integer)
            SM = value
        End Set
        Get
            Return SM
        End Get
    End Property

End Class

' Temporary class to enable interoperability with AHK variant
Public Class INIFunctions

    Shared Function Read(File As String, Header As String, Name As String) As String

        ' Check if file exists
        If String.IsNullOrWhiteSpace(Dir(File)) Then
            MsgBox("Error at INIFunctions.Read: File referenced does not exist.")
            Return ""
        End If

        ' Get file and value
        Dim Reading As Boolean = False
        Dim SR As New StreamReader(File)
        While Not SR.EndOfStream

            ' Get line
            Dim Line As String = SR.ReadLine

            ' Turn reading mode ON
            If Line = "[" + Header + "]" Then
                Reading = True
                Continue While
            End If

            ' Return answer if found
            If Line.StartsWith(Name + "=") And Reading Then
                Return Line.Substring(Name.Length + 1)
            End If

            ' Turn reading mode OFF
            If Line.StartsWith("[") Then
                Reading = False
                Continue While
            End If

        End While

        ' Return not found value
        Return "NOT FOUND"

    End Function

    Shared Function Write(Header As String, Name As String, Value As String) As Integer

    End Function

End Class
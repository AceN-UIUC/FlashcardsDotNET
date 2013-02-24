Imports System.IO

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
                If EditEngaged AndAlso Form1.IsSubjectLine(Str) Then
                    EditEngaged = False
                End If

                ' Check for separate subjects/subject markers
                '   NOTE: This does NOT trigger if the first line is a subject marker (i.e. a 1 subject file)
                If Not First AndAlso SubjectChkActive AndAlso Form1.IsSubjectLine(Str) Then

                    ' If no subject specified and multiple subjects are detected, alert user and exit everything
                    MsgBox("Subject was not specified when using multiple-subject file. The file has not been modified, but the modification operation was cancelled. (Editing.UpdateQAM)")
                    QR.Close()
                    Exit Sub

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
                    If EditEngaged AndAlso Form1.IsSubjectLine(Str) Then
                        StrLst.Add(LastLine) ' If EditEngaged was true for the last line and it wasn't part of a QAM object, this prevents it from being overwritten
                        EditEngaged = False
                    End If

                    ' Skip answers in the old file that have already been loaded into the new file (TODO)
                    'If Str.StartsWith(LastCnt.ToString) Then
                    '    Continue While
                    'End If

                    ' Depending on edit status, write proper line to line list
                    If EditEngaged AndAlso Cnt <= QAMList.Count Then
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
                    If EditEngaged AndAlso Form1.IsSubjectLine(Str) Then
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
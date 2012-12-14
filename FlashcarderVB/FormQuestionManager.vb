Imports System.IO
Imports System.Collections.ObjectModel

Public Class FormQuestionManager

    ' Variables
    Public QPath As String
    Public APath As String
    Public MPath As String = ""
    Public MainPath As String = ""

    Private QAMList As New List(Of Question)

    Private Sub Loader() Handles MyBase.Load

        ' Automatically use the icon/title of the first form
        Me.Icon = Form1.Icon
        Me.Text = Form1.Text + " - Question Manager"

    End Sub

    Private Sub DragDrop1(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
        Dim Files As String() = e.Data.GetData(DataFormats.FileDrop)
        DragDropHandler(Files)
    End Sub
    Private Sub DragDrop2(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)
        Dim Files As String() = e.Data.GetData(DataFormats.FileDrop)
        DragDropHandler(Files)
    End Sub

    Private Sub DragDropHandler(ByVal Files As String())
        For Each F As String In Files

            ' Execute load file operation
            Try
                LoadFile(F, True)
            Catch e As Exception
                MsgBox("An error has occurred in the file loading process. (" & e.GetType.ToString & ")")
            End Try

        Next

    End Sub

    ' Load referenced files
    'QAMList = Quizzing.Load(QPath, APath, MPath)

    ' Load a file into the question manager
    '   LoadMoreFiles controls whether the program will search for more files to be auto-loaded
    Shared Sub LoadFile(ByVal FPath As String, ByVal LoadMoreFiles As Boolean)

        ' Obligatory verification
        If String.IsNullOrWhiteSpace(Dir(FPath)) Or Not FPath.Contains(".") Then
            MsgBox("The file referenced does not exist. The file loading process will be stopped. (Question Manager - LoadFile)")
            Exit Sub
        End If

        ' Get file type
        Dim FileExt As String = FPath.Substring(FPath.LastIndexOf(".") + 1)

        ' Compare main file path with existing one
        '   If the two are different, reset all the QAM verification checkboxes
        Dim CurMainPath As String = FPath.Substring(0, FPath.LastIndexOf("\") + 1)
        If CurMainPath <> FormQuestionManager.MainPath Then
            FormQuestionManager.MainPath = CurMainPath

            SubjectChoiceDlg.cbxNoMarkingImport.Visible = False
            SubjectChoiceDlg.cbxNoMarkingImport.Checked = False
            SubjectChoiceDlg.Subject = ""

            FormQuestionManager.cbx_VrifyQs.Checked = False
            FormQuestionManager.cbx_VrifyAs.Checked = False
            FormQuestionManager.cbx_VrifyMs.Checked = False
        End If

        ' AHK file (built for backwards compatibility with the original AHK flashcarder)
        '   This works with answers.txt/questions.txt/markings.txt files - they MUST have THESE names!
        If FileExt = "ahk" Then

            ' Variables
            Dim Subject As String = "" ' ex. Magnetism
            Dim BroadSubject As String = "" ' ex. IB Physics

            ' Read file / identify key details
            Dim SR As New StreamReader(FPath)
            While Not SR.EndOfStream

                Dim Str As String = SR.ReadLine

                If Str.EndsWith("Subject") Then ' Backwards compatibility
                    BroadSubject = Str.Substring(10)
                    BroadSubject = BroadSubject.Remove(BroadSubject.IndexOf(","))
                ElseIf Str.StartsWith("?=") Then
                    'Subject = ?
                ElseIf Str.StartsWith("?=") Then
                    'MainPath=?.Replace("/","\")
                    If FormQuestionManager.MainPath.Last = "\" Then
                        FormQuestionManager.MainPath = FormQuestionManager.MainPath.Substring(0, FormQuestionManager.MainPath.Length - 1)
                    End If
                End If

            End While

            ' Check reference location for needed files
            Dim QPath As String = ""
            Dim APath As String = ""
            Dim MPath As String = ""

            If Not String.IsNullOrWhiteSpace(Dir(FormQuestionManager.MainPath & "questions.ini")) Then
                QPath = FormQuestionManager.MainPath & "questions.ini"
            ElseIf Not String.IsNullOrWhiteSpace(Dir(FormQuestionManager.MainPath & "questions.txt")) Then
                QPath = FormQuestionManager.MainPath & "questions.txt"
            End If

            If Not String.IsNullOrWhiteSpace(Dir(FormQuestionManager.MainPath & "answers.ini")) Then
                APath = FormQuestionManager.MainPath & "answers.ini"
            ElseIf Not String.IsNullOrWhiteSpace(Dir(FormQuestionManager.MainPath & "answers.txt")) Then
                APath = FormQuestionManager.MainPath & "answers.txt"
            End If

            If Not String.IsNullOrWhiteSpace(Dir(FormQuestionManager.MainPath & "markings.ini")) Then
                MPath = FormQuestionManager.MainPath & "markings.ini"
            ElseIf Not String.IsNullOrWhiteSpace(Dir(FormQuestionManager.MainPath & "markings.txt")) Then
                MPath = FormQuestionManager.MainPath & "markings.txt"
            End If

            ' Check to make sure reference paths are valid
            If QPath.Length < 1 Or APath.Length < 1 Then
                MsgBox("One of the required files (questions or answers - in either .txt or .ini) does not exist. The file loading process will be stopped. (Question Manager - LoadFile)")
                Exit Sub
            ElseIf String.IsNullOrWhiteSpace(MPath) Then
                MsgBox("No markings file exists. It will be automatically generated.")
            End If

            ' Load files if possible
            If QPath.Length > 1 Then
                LoadFile(QPath, False)
            End If
            If APath.Length > 1 Then
                LoadFile(APath, False)
            End If
            If MPath.Length > 1 Then
                LoadFile(MPath, False)
            End If


        ElseIf FileExt = "txt" Or FileExt = "ini" Then

            ' Standard file
            Dim FileType As String = Editing.GetFileType(FPath)

            ' If no type has been identified, notify user and exit sub
            '   Note: if no markings file is identified, notify user that one will be generated automatically (later on)
            '   As such, this doesn't stop the (entire) process for markings files (only for q/a files, which CAN'T be generated)
            If FileType.Length = 0 Then
                If Not (FormQuestionManager.cbx_VrifyAs.Checked And FormQuestionManager.cbx_VrifyQs.Checked) Then
                    Exit Sub
                End If
            End If

            ' If this is the first of the three required files, ask user whether to use automatic file loading for the others
            '  Note that this only works when the target file contains only three text files OR if the files are explicitly named
            If LoadMoreFiles Then
                If Not FormQuestionManager.cbxAutoLoad.Checked Then
                    LoadMoreFiles = False
                End If
            End If

            ' Check appropriate notification boxes
            If FileType = "q" Then
                FormQuestionManager.QPath = FPath
                FormQuestionManager.cbx_VrifyQs.Enabled = True
                FormQuestionManager.cbx_VrifyQs.Checked = True
                FormQuestionManager.cbx_VrifyQs.Enabled = False
            ElseIf FileType = "a" Then
                FormQuestionManager.APath = FPath
                FormQuestionManager.cbx_VrifyAs.Enabled = True
                FormQuestionManager.cbx_VrifyAs.Checked = True
                FormQuestionManager.cbx_VrifyAs.Enabled = False
            ElseIf FileType = "m" Then
                FormQuestionManager.MPath = FPath
                FormQuestionManager.cbx_VrifyMs.Enabled = True
                FormQuestionManager.cbx_VrifyMs.Checked = True
                FormQuestionManager.cbx_VrifyMs.Enabled = False
            End If

            Dim FileCnt As Integer = 0 - CInt(FormQuestionManager.cbx_VrifyQs.Checked) - CInt(FormQuestionManager.cbx_VrifyAs.Checked) - CInt(FormQuestionManager.cbx_VrifyMs.Checked)

            ' Exit sub if auto-loading functions disabled
            If Not LoadMoreFiles Then
                Exit Sub
            End If

            ' Automatic Loading filepaths - these are guesses of the proper file location and aren't verified
            Dim AL_QPath As String = FormQuestionManager.QPath
            Dim AL_APath As String = FormQuestionManager.APath
            Dim AL_MPath As String = FormQuestionManager.MPath

            If FileCnt = 1 Then

                ' Check to make sure only three text files exist in the referenced directory
                FormQuestionManager.MainPath = FPath.Replace("/", "\")
                FormQuestionManager.MainPath = FormQuestionManager.MainPath.Substring(0, FormQuestionManager.MainPath.LastIndexOf("\") + 1)

                Dim FileArr1 As String() = Directory.GetFiles(FormQuestionManager.MainPath, "*.txt")
                Dim FileArr2 As String() = Directory.GetFiles(FormQuestionManager.MainPath, "*.ini")

                Dim FileList As New List(Of String)
                FileList.AddRange(FileArr1)
                FileList.AddRange(FileArr2)

                ' --- Auto-load files (name identification method) ---

                ' File presence verification variables
                Dim QFound As Boolean = FormQuestionManager.cbx_VrifyQs.Checked
                Dim AFound As Boolean = FormQuestionManager.cbx_VrifyAs.Checked
                Dim MFound As Boolean = FormQuestionManager.cbx_VrifyMs.Checked

                ' Verify presence of all required files
                Dim NameList As String() = {"questions", "qs", "answers", "as", "markings", "ms"}
                For Each S As String In FileList

                    Dim L As String = S.Substring(S.LastIndexOf("\") + 1)
                    L = L.Substring(0, L.Length - 4).ToLowerInvariant

                    If NameList.Contains(L) Then
                        L = L.First

                        If L = "q" Then
                            QFound = True
                            AL_QPath = S
                        End If
                        If L = "a" Then
                            AFound = True
                            AL_APath = S
                        End If
                        If L = "m" Then
                            MFound = True
                            AL_MPath = S
                        End If

                    End If

                Next

                ' Load files
                If QFound And Not String.IsNullOrWhiteSpace(AL_QPath) Then
                    LoadFile(AL_QPath, False)
                End If
                If AFound And Not String.IsNullOrWhiteSpace(AL_APath) Then
                    LoadFile(AL_APath, False)
                End If
                If MFound And Not String.IsNullOrWhiteSpace(AL_MPath) Then
                    LoadFile(AL_MPath, False)
                End If

                ' --- Auto-load files (content identification method) ---
                If FileList.Count > 1 Then

                    ' If more than 3 files are available, check to make sure that they are all of valid types
                    If FileList.Count > 3 Then

                        Dim RemoveIdxs As New List(Of Integer)

                        ' Check files (reverse iteration makes sure that indices in RemoveIdxs are descending, which in turn prevents argumentOutOfRange problems)
                        For i = FileList.Count - 1 To 0 Step -1
                            If String.IsNullOrWhiteSpace(Editing.GetFileType(FileList.Item(i))) Then
                                RemoveIdxs.Add(i)
                            End If
                        Next

                        ' Remove null files
                        For Each N As Integer In RemoveIdxs
                            FileList.RemoveAt(N)
                        Next

                    End If

                    ' If more than 3 files are available (after the type check), request that 3 or fewer files be selected
                    If FileList.Count > 3 Then

                        ' Use the subject selection dialog

                        ' Get dialog's initial details (programmatically)
                        Dim O_lbl As String = SubjectChoiceDlg.Label1.Text
                        Dim O_title As String = SubjectChoiceDlg.Text
                        Dim O_SubjLst As Object = SubjectChoiceDlg.lbox.Items
                        Dim O_Subj As String = SubjectChoiceDlg.Subject

                        ' Co-op it for needed tasks (after all, this isn't the subject choice dialog's primary job)
                        SubjectChoiceDlg.Label1.Text = "Too many files were found by the autoloader. Select the 3 files to load using CTRL/SHIFT."
                        SubjectChoiceDlg.Subject = "FILESELECTMODE" ' Used to tell the subject choice dialog that it is in file selection mode
                        SubjectChoiceDlg.Text = "File Selection Dialog"
                        SubjectChoiceDlg.cbxNoMarkingImport.Visible = True
                        SubjectChoiceDlg.cbxNoMarkingImport.Checked = False

                        SubjectChoiceDlg.lbox.SelectionMode = SelectionMode.MultiExtended
                        SubjectChoiceDlg.lbox.Items.Clear()
                        SubjectChoiceDlg.SubjectList.Clear()
                        For Each Str As String In FileList
                            SubjectChoiceDlg.lbox.Items.Add(Str)
                        Next

                        Dim DlgR As DialogResult = SubjectChoiceDlg.ShowDialog()

                        ' Stop loading process if files aren't selected
                        If DlgR = DialogResult.Cancel Then
                            Exit Sub
                        Else
                            ' Otherwise, gather the results
                            FileList.Clear()
                            FileList.AddRange(SubjectChoiceDlg.SubjectList)
                        End If

                        ' Revert it to its original state
                        SubjectChoiceDlg.Label1.Text = O_lbl
                        SubjectChoiceDlg.Text = O_title

                        SubjectChoiceDlg.lbox.Items.Clear()
                        SubjectChoiceDlg.lbox.Items.AddRange(O_SubjLst)

                        SubjectChoiceDlg.cbxNoMarkingImport.Visible = False
                        SubjectChoiceDlg.cbxNoMarkingImport.Checked = False
                        SubjectChoiceDlg.Subject = O_Subj

                    End If

                    For Each S As String In FileList

                        ' Don't load repeated files
                        If S = FormQuestionManager.QPath Then
                            Continue For
                        ElseIf S = FormQuestionManager.APath Then
                            Continue For
                        ElseIf S = FormQuestionManager.MPath Then
                            Continue For
                        End If

                        If S <> FPath Then
                            LoadFile(S, False)
                        End If

                    Next

                End If

            End If

        End If

        ' If everything is loaded, load the files' contents into a list of QAM objects
        '   Note: Tiered If statements are used because VB doesn't exit an 'and' if statement if the first condition is falsified (tiering = performance)
        If FormQuestionManager.cbx_VrifyQs.Checked Then
            If FormQuestionManager.cbx_VrifyAs.Checked Then

                FormQuestionManager.QAMList = Quizzing.Load(FormQuestionManager.QPath, FormQuestionManager.APath, FormQuestionManager.MPath)


                ' Check that the QAM objects all make sense
                '   If they don't, notify the user and throw everything out
                Dim ErrorOnQAMListLine As Integer = -1
                Dim CurQAMObj As Question
                For i = 0 To FormQuestionManager.QAMList.Count - 1
                    CurQAMObj = FormQuestionManager.QAMList.Item(i)
                    If String.IsNullOrWhiteSpace(CurQAMObj.Question) OrElse _
                        CurQAMObj.AnswerList.Count = 0 Then
                        ErrorOnQAMListLine = i + 1
                        Exit For
                    End If
                Next

                If ErrorOnQAMListLine = -1 Then ' If ErrorOnQAMListLine is -1, the QAM list that was loaded is either VALID or NULL

                    ' Update question list box
                    FormQuestionManager.lvwQAMList.Items.Clear()
                    For i = 0 To FormQuestionManager.QAMList.Count - 1
                        FormQuestionManager.lvwQAMList.Items.Add(CStr(i + 1) & "=" & FormQuestionManager.QAMList.Item(i).Question)
                    Next

                Else

                    ' Uncheck verification checkboxes
                    FormQuestionManager.cbx_VrifyQs.Checked = False
                    FormQuestionManager.cbx_VrifyAs.Checked = False
                    FormQuestionManager.cbx_VrifyMs.Checked = False

                    ' Report invalid QAM list
                    FormQuestionManager.QAMList.Clear()
                    MsgBox("The Q/A/M files are incorrectly formatted (see question " & ErrorOnQAMListLine.ToString & "). Nothing has been loaded.")

                    ' Exit loading process
                    Exit Sub

                End If


            End If
            End If

    End Sub

    Private Sub btnOpenManually_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenManually.Click
        OFDlg.ShowDialog()
    End Sub
    Private Sub ReceiveManualFile() Handles OFDlg.FileOk

        ' Save old file (if one is active)
        If QAMList.Count <> 0 Then
            Editing.UpdateQAM(QAMList, QPath, APath, MPath, SubjectChoiceDlg.Subject)
        End If

        ' Import new file
        If OFDlg.FileNames.Count > 0 Then
            DragDropHandler(OFDlg.FileNames)
        End If

        ' Highlight questions based on markings (if appropriate)
        HighlightMarkings()

    End Sub

    Private Sub QAMLstDblClk(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvwQAMList.DoubleClick
        If lvwQAMList.SelectedIndices.Count = 1 Then

            ' Setup edit window
            FormQuestionEditor.QAMObj = QAMList.Item(lvwQAMList.SelectedIndices.Item(0))

            ' Display edit window
            FormQuestionEditor.ShowDialog()

        End If
    End Sub

    Private Sub QAMSave() Handles Me.FormClosing
        If QAMList.Count <> 0 Then
            Editing.UpdateQAM(QAMList, QPath, APath, MPath, SubjectChoiceDlg.Subject)
        End If
    End Sub

    ' Automatically resize controls
    Private Sub ResizeCtrls(sender As System.Object, e As System.EventArgs) Handles MyBase.SizeChanged

        Dim W As Integer = Me.Size.Width - 44
        Dim CentX As Integer = CInt(gbxActsOpts.Size.Width / 2)

        ' Top label
        Label2.Location = New Point(CentX - 95, 72)

        ' Checklist
        gbxChkList.Size = New Size(W, 43)
        cbx_VrifyAs.Location = New Point(CentX - 38, 16)
        'cbx_VrifyAs.Location = New Point(gbxActsOpts.Size.Width - 5, 16)

        ' QAM List
        lvwQAMList.Size = New Size(W - 2, Me.Size.Height - 310)

        ' Bottom group box
        gbxActsOpts.Location = New Point(10, Me.Size.Height - 194)
        gbxActsOpts.Size = New Size(W, 140)
        btnOpenManually.Location = New Point(CentX - 140, 109)
        btnQuiz.Location = New Point(CentX, 109)

    End Sub

    ' Take quiz
    Private Sub btnQuiz_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuiz.Click
        If tbxMinWrong.BackColor = Color.White Then ' Validate that all inputs used are suitable
            Quizzing.QuizClassic(QAMList, CInt(tbxMinWrong.Text), QAMList.Count, False)
        End If
    End Sub

    Private Sub tbxMinWrongChgd() Handles tbxMinWrong.TextChanged
        Dim TestInt As Integer = 0
        If tbxMinWrong.Text.Length > 0 AndAlso Integer.TryParse(tbxMinWrong.Text, TestInt) Then
            tbxMinWrong.BackColor = Color.White
        Else
            tbxMinWrong.BackColor = Color.Red
        End If
    End Sub

    Private Sub HighlightMarkings() Handles cbxHighlightQs.CheckedChanged

        If cbxHighlightQs.Checked AndAlso cbx_VrifyMs.Checked AndAlso tbxMinWrong.BackColor = Color.White Then
            Dim MinMarking As Integer = CInt(tbxMinWrong.Text)
            For i = 0 To QAMList.Count - 1
                Dim CurMarking As Integer = QAMList.Item(i).Marking
                If CurMarking > MinMarking Then
                    lvwQAMList.Items(i).BackColor = Color.Tomato
                ElseIf CurMarking < MinMarking Then
                    lvwQAMList.Items(i).BackColor = Color.LimeGreen
                Else
                    lvwQAMList.Items(i).BackColor = Color.White
                End If
            Next
        Else
            For i = 0 To QAMList.Count - 1
                lvwQAMList.Items(i).BackColor = Color.White
            Next
        End If

    End Sub

    ' Makes only one column appear in the listView
    Private Sub lvwQAMListResize() Handles lvwQAMList.Resize
        lvwQAMList.Columns.Item(0).Width = lvwQAMList.Width - 4
    End Sub

    ' Event handlers for the validation checkboxes
    '   These are here so that setting the checkbox checked state to FALSE clears the stored file paths
    Private Sub clrQs() Handles cbx_VrifyQs.CheckedChanged
        If Not cbx_VrifyQs.Checked Then
            QPath = ""
        End If
    End Sub
    Private Sub clrAs() Handles cbx_VrifyAs.CheckedChanged
        If Not cbx_VrifyAs.Checked Then
            APath = ""
        End If
    End Sub
    Private Sub clrMs() Handles cbx_VrifyMs.CheckedChanged
        If Not cbx_VrifyMs.Checked Then
            MPath = ""
        End If
    End Sub

End Class
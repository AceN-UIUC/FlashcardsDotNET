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
        Me.Text = Form1.MainTitle + " - Question Manager"

    End Sub

    Private Sub DragDrop1(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
        Dim Files As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
        DragDropHandler(Files)
    End Sub
    Private Sub DragDrop2(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)
        Dim Files As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
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
    Public Sub LoadFile(ByVal FPath As String, ByVal LoadMoreFiles As Boolean)

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
        If CurMainPath <> Me.MainPath Then
            Me.MainPath = CurMainPath

            SubjectChoiceDlg.cbxNoMarkingImport.Visible = False
            SubjectChoiceDlg.cbxNoMarkingImport.Checked = False
            SubjectChoiceDlg.Subject = ""

            Me.cbx_VrifyQs.Checked = False
            Me.cbx_VrifyAs.Checked = False
            Me.cbx_VrifyMs.Checked = False
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
                    If Me.MainPath.Last = "\" Then
                        Me.MainPath = MainPath.Substring(0, Me.MainPath.Length - 1)
                    End If
                End If

            End While

            ' Check reference location for needed files
            Dim QPath As String = ""
            Dim APath As String = ""
            Dim MPath As String = ""

            If File.Exists(Me.MainPath & "questions.ini") Then
                QPath = Me.MainPath & "questions.ini"
            ElseIf File.Exists(Me.MainPath & "questions.txt") Then
                QPath = Me.MainPath & "questions.txt"
            End If

            If File.Exists(Me.MainPath & "answers.ini") Then
                APath = Me.MainPath & "answers.ini"
            ElseIf File.Exists(Me.MainPath & "answers.txt") Then
                APath = Me.MainPath & "answers.txt"
            End If

            If Not (Me.MainPath & "markings.ini") Then
                MPath = Me.MainPath & "markings.ini"
            ElseIf File.Exists(Me.MainPath & "markings.txt") Then
                MPath = Me.MainPath & "markings.txt"
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
                If Not (Me.cbx_VrifyAs.Checked AndAlso Me.cbx_VrifyQs.Checked) Then
                    Exit Sub
                End If
            End If

            ' If this is the first of the three required files, ask user whether to use automatic file loading for the others
            '  Note that this only works when the target file contains only three text files OR if the files are explicitly named
            If LoadMoreFiles AndAlso Not cbxAutoLoad.Checked Then
                LoadMoreFiles = False
            End If

            ' Check appropriate notification boxes
            If FileType = "q" Then
                QPath = FPath
                cbx_VrifyQs.Enabled = True
                cbx_VrifyQs.Checked = True
                cbx_VrifyQs.Enabled = False
            ElseIf FileType = "a" Then
                APath = FPath
                cbx_VrifyAs.Enabled = True
                cbx_VrifyAs.Checked = True
                cbx_VrifyAs.Enabled = False
            ElseIf FileType = "m" Then
                MPath = FPath
                cbx_VrifyMs.Enabled = True
                cbx_VrifyMs.Checked = True
                cbx_VrifyMs.Enabled = False
            End If

            Dim FileCnt As Integer = 0 - CInt(cbx_VrifyQs.Checked) - CInt(cbx_VrifyAs.Checked) - CInt(cbx_VrifyMs.Checked)

            ' Exit sub if auto-loading functions disabled
            If Not LoadMoreFiles Then
                Exit Sub
            End If

            ' Automatic Loading filepaths - these are guesses of the proper file location and aren't verified
            Dim AL_QPath As String = QPath
            Dim AL_APath As String = APath
            Dim AL_MPath As String = MPath

            If FileCnt = 1 Then

                ' Check to make sure only three text files exist in the referenced directory
                MainPath = FPath.Replace("/", "\")
                MainPath = MainPath.Substring(0, MainPath.LastIndexOf("\") + 1)

                Dim FileArr1 As String() = Directory.GetFiles(MainPath, "*.txt")
                Dim FileArr2 As String() = Directory.GetFiles(MainPath, "*.ini")

                Dim FileList As New List(Of String)
                FileList.AddRange(FileArr1)
                FileList.AddRange(FileArr2)

                ' --- Auto-load files (name identification method) ---

                ' File presence verification variables
                Dim QFound As Boolean = cbx_VrifyQs.Checked
                Dim AFound As Boolean = cbx_VrifyAs.Checked
                Dim MFound As Boolean = cbx_VrifyMs.Checked

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
                        If S = QPath Then
                            Continue For
                        ElseIf S = APath Then
                            Continue For
                        ElseIf S = MPath Then
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
        If cbx_VrifyQs.Checked Then
            If cbx_VrifyAs.Checked Then

                QAMList = Quizzing.Load(QPath, APath, MPath)


                ' Check that the QAM objects all make sense
                '   If they don't, notify the user and throw everything out
                Dim ErrorOnQAMListLine As Integer = -1
                Dim CurQAMObj As Question
                For i = 0 To QAMList.Count - 1
                    CurQAMObj = QAMList.Item(i)
                    If String.IsNullOrWhiteSpace(CurQAMObj.Question) OrElse _
                        CurQAMObj.AnswerList.Count = 0 Then
                        ErrorOnQAMListLine = i + 1
                        Exit For
                    End If
                Next

                If ErrorOnQAMListLine = -1 Then ' If ErrorOnQAMListLine is -1, the QAM list that was loaded is either VALID or NULL

                    ' Update question list box
                    UpdateMainListView()

                Else

                    ' Uncheck verification checkboxes
                    cbx_VrifyQs.Checked = False
                    cbx_VrifyAs.Checked = False
                    cbx_VrifyMs.Checked = False

                    ' Report invalid QAM list
                    QAMList.Clear()
                    MsgBox("The Q/A/M files are incorrectly formatted (see question " & ErrorOnQAMListLine.ToString & "). Nothing has been loaded.")

                    ' Exit loading process
                    Exit Sub

                End If


            End If
        End If

    End Sub

    Private Sub btnOpenManually_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenManually.Click

        ' File dialog directory persistence
        If Form1.MasterFileDialogLocation.Length <> 0 AndAlso Directory.Exists(Form1.MasterFileDialogLocation) Then
            OFileDlg.InitialDirectory = Form1.MasterFileDialogLocation
        End If
        Dim DlgResult As DialogResult = OFileDlg.ShowDialog()
        If OFileDlg.FileName.Length > 1 Then
            Form1.MasterFileDialogLocation = Path.GetDirectoryName(OFileDlg.FileName)
        End If

    End Sub
    Private Sub ReceiveManualFile() Handles OFileDlg.FileOk

        ' Save old file (if one is active)
        If QAMList.Count <> 0 Then
            Editing.UpdateQAM(QAMList, QPath, APath, MPath, SubjectChoiceDlg.Subject)
        End If

        ' Import new file
        If OFileDlg.FileNames.Count > 0 Then
            DragDropHandler(OFileDlg.FileNames)
        End If

        ' Highlight questions based on markings (if appropriate)
        HighlightMarkings()

    End Sub

    Public Sub UpdateMainListView()

        ' - Update question list box -

        Dim LViewTopIdx As Integer = 0
        Try
            If lvwQAMList.Items.Count > 0 Then
                LViewTopIdx = lvwQAMList.TopItem.Index
            End If
        Catch ex As NullReferenceException
            ' If the listView isn't initialized, the error it throws as a result will be caught here
        End Try


        '   Remove extra items from listView
        While lvwQAMList.Items.Count > QAMList.Count
            lvwQAMList.Items.RemoveAt(0)
        End While

        '   Set-up new/recycled items from listView
        For i = 0 To QAMList.Count - 1
            If i < lvwQAMList.Items.Count Then

                ' Use existing elements
                lvwQAMList.Items.Item(i).Text = CStr(i + 1) & "=" & QAMList.Item(i).Question

            Else

                ' Add elements
                lvwQAMList.Items.Add(CStr(i + 1) & "=" & QAMList.Item(i).Question)

            End If
        Next

        ' Reset top
        If lvwQAMList.Items.Count > 1 Then
            lvwQAMList.TopItem = lvwQAMList.Items.Item(If(LViewTopIdx < lvwQAMList.Items.Count, LViewTopIdx, 0))
        End If

        ' - Update/refresh highlighting -
        HighlightMarkings()

    End Sub

    Private Sub QAMLstDblClk() Handles lvwQAMList.DoubleClick
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
    Private Sub ResizeCtrls(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged

        Dim W As Integer = Me.Size.Width - 44

        ' Bottom group box (this must be the first thing resized because other things depend on its size)
        gbxActsOpts.Location = New Point(10, Me.Size.Height - 194)
        gbxActsOpts.Size = New Size(W, 140)
        Dim CentX As Integer = CInt(gbxActsOpts.Size.Width / 2)

        ' Bottom group box buttons
        btnOpenManually.Location = New Point(CentX - 140, 109)
        btnQuiz.Location = New Point(CentX, 109)

        ' Top label
        Label2.Location = New Point(CentX - 95, 72)

        ' Checklist
        gbxChkList.Size = New Size(W, 43)
        cbx_VrifyAs.Location = New Point(CentX - 38, 16)
        'cbx_VrifyAs.Location = New Point(gbxActsOpts.Size.Width - 5, 16)

        ' QAM List
        lvwQAMList.Size = New Size(W - 2, Me.Size.Height - 310)

    End Sub

    ' Take quiz
    Private Sub btnQuiz_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuiz.Click
        If tbxMinWrong.BackColor = Color.White Then ' Validate that all inputs used are suitable
            Quizzing.QuizClassic(QAMList, CInt(tbxMinWrong.Text), QAMList.Count, False)
        End If
    End Sub


#Region "On-changed validators"
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
#End Region

#Region "Drag-drop handlers"
    Private Sub lvwQAMList_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lvwQAMList.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub lvwQAMList_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lvwQAMList.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then

            ' Load selected file(s)
            Dim FileList As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
            For Each S As String In FileList
                If S.Length > 1 Then
                    LoadFile(S, True)
                End If
            Next

        End If
    End Sub
    #End Region
#Region "ListView ContextMenuStrip"

    ' Edit button
    Private Sub cmsEdit() Handles btnEdit.Click

        Dim Idxes As ListView.SelectedIndexCollection = lvwQAMList.SelectedIndices
        If Idxes.Count > 0 AndAlso Idxes.Item(0) <> -1 Then
            FormQuestionEditor.QAMObj = QAMList.Item(Idxes.Item(0))
        End If

    End Sub

    ' Delete button
    Private Sub cmsDelete() Handles btnDelete.Click

        Dim Idxes As ListView.SelectedIndexCollection = lvwQAMList.SelectedIndices
        If Idxes.Count > 0 AndAlso Idxes.Item(0) <> -1 Then
            QAMList.RemoveAt(Idxes.Item(0))
            UpdateMainListView() ' Redraw the ListView, now that a question has been deleted
        End If

    End Sub

    ' Add new button
    Private Sub cmsAddNew() Handles btnAddAtEnd.Click
        MsgBox("(WIP) I don't work yet!")
    End Sub

#End Region

End Class
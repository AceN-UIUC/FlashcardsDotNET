Imports System.IO
Imports System.Text.RegularExpressions

Public Class FormFCCoordinator

#Region "Markings criteria"
    Private Sub rbnCOptAllChanged() Handles rbnCOptAll.CheckedChanged
        tbxNum.Visible = Not rbnCOptAll.Checked
    End Sub
    Private Sub rbnCOptMoreThanChanged() Handles rbnCOptMoreThan.CheckedChanged
        tbxNum.Location = New Point(tbxNum.Location.X, rbnCOptMoreThan.Location.Y)
    End Sub
    Private Sub rbnCOptSameAsChanged() Handles rbnCOptSameAs.CheckedChanged
        tbxNum.Location = New Point(tbxNum.Location.X, rbnCOptSameAs.Location.Y)
    End Sub
    Private Sub rbnCOptLessThanChanged() Handles rbnCOptLessThan.CheckedChanged
        tbxNum.Location = New Point(tbxNum.Location.X, rbnCOptLessThan.Location.Y)
    End Sub
#End Region

    ' List of extracted question objects (from the Cornell note parser)
    Public Shared ExtractedQAMList As New List(Of Question)

    ' List of files (used in drag drop operations)
    '   TODO - Would a plain old string be better here? (Esp. since only 1 file is allowed at once?)
    Public FileArr As String()

    Private Sub Loader() Handles MyBase.Load

        ' Automatically use the icon/title of the first form
        Me.Icon = Form1.Icon
        Me.Text = Form1.MainTitle + " - FC Coordinator WIP"

    End Sub

    Private Sub btnOpenManually_Click() Handles btnOpenManually.Click

        ' File dialog directory persistence
        If Form1.MasterFileDialogLocation.Length <> 0 AndAlso Directory.Exists(Form1.MasterFileDialogLocation) Then
            OFileDlg.InitialDirectory = Form1.MasterFileDialogLocation
        End If
        OFileDlg.ShowDialog()
        If OFileDlg.FileName.Length > 1 Then
            Form1.MasterFileDialogLocation = Path.GetDirectoryName(OFileDlg.FileName)
        End If

    End Sub

    Private Sub OFDHandler(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OFileDlg.FileOk
        If Not String.IsNullOrWhiteSpace(OFileDlg.FileName) Then
            FileArr = {OFileDlg.FileName}

            ' Clear validation checkboxes (so two sets of files don't get mixed together)
            '   NOTE: Stored file paths are cleared by cbx_Vrify*s event handlers (which are triggered by the property operations below)
            FormQuestionManager.cbx_VrifyQs.Checked = False
            FormQuestionManager.cbx_VrifyAs.Checked = False
            FormQuestionManager.cbx_VrifyMs.Checked = False

            If TabControl1.SelectedIndex = 1 Then
                tbx_MarkingPath.Text = FileArr.GetValue(0).ToString
            End If

        End If
    End Sub

    Private Sub SDragDrop_MReset(ByVal Files As String())

        ' Get main file path
        Dim FPath As String = Files.GetValue(0).ToString
        If Files.Count > 1 Then
            MsgBox("Multiple files were drag-dropped on the flashcard coordinator. Only the first file will be used.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        ' Input verification
        If txtMarkTgt.BackColor = Color.Red Or tbxNum.BackColor = Color.Red Then
            MsgBox("One of the numerical input values is invalid. Check the red textboxes.")
            Exit Sub
        End If

        ' Check to make sure selected file is a markings file
        Dim FT = Editing.GetFileType(FPath)
        If FT <> "m" Then
            MsgBox("That is not a markings file.")
            Exit Sub
        End If

        ' Get file
        Dim LineList As New List(Of String)
        LineList.AddRange(IO.File.ReadAllLines(FPath))

        ' Check for multiple subjects
        '   If there are multiple subjects, ask the user to pick one
        SubjectChoiceDlg.lbox.Items.Clear()
        For Each L As String In LineList
            If L.StartsWith("[") Then
                If L.EndsWith("]") Then
                    Dim S = L.Substring(1).Substring(0, L.Length - 2)
                    SubjectChoiceDlg.lbox.Items.Add(S)
                    SubjectChoiceDlg.SubjectList.Add(S)
                End If
            End If
        Next
        If SubjectChoiceDlg.lbox.Items.Count > 1 Then
            Dim DR = SubjectChoiceDlg.ShowDialog()
            If DR = DialogResult.Cancel Then
                Exit Sub
            End If
        ElseIf SubjectChoiceDlg.lbox.Items.Count = 1 Then
            SubjectChoiceDlg.Subject = SubjectChoiceDlg.lbox.Items.Item(0).ToString
        End If

        ' Parse through lines and change as needed
        Dim EditMode As Boolean = SubjectChoiceDlg.lbox.Items.Count < 2
        For i = 0 To LineList.Count - 1

            Dim S As String = LineList.Item(i)

            ' Line skips
            If String.IsNullOrWhiteSpace(S) Then
                Continue For
            End If

            ' Edit mode controls
            If S.StartsWith("[") Then
                If EditMode Then
                    EditMode = False
                ElseIf S = "[" & SubjectChoiceDlg.Subject & "]" Then
                    EditMode = True
                End If
            End If

            ' Exception handler
            If Not S.Contains("=") Or Not EditMode Then
                Continue For
            End If

            ' Get strings on both sides of = sign (L includes the =)
            Dim L As String = S.Substring(0, S.IndexOf("=") + 1)
            Dim R As String = S.Substring(L.Length)

            ' Get marking count
            Dim ReqdInt As Integer = CInt(tbxNum.Text)
            Dim RInt As Integer = 0
            If Not Integer.TryParse(R, RInt) Then
                Continue For
            End If

            ' Determine if an edit is needed
            Dim NeedsEdit As Boolean = rbnCOptAll.Checked
            If rbnCOptLessThan.Checked And RInt < ReqdInt Then
                NeedsEdit = True
            ElseIf rbnCOptMoreThan.Checked And RInt > ReqdInt Then
                NeedsEdit = True
            ElseIf rbnCOptSameAs.Checked And RInt <> ReqdInt Then
                NeedsEdit = True
            End If

            ' If no edit needed, continue line loop
            If Not NeedsEdit Then
                Continue For
            End If

            ' If an edit has been performed, re-assemble the line
            LineList.Item(i) = L & txtMarkTgt.Text

        Next

        ' Write to file
        IO.File.WriteAllLines(tbx_MarkingPath.Text, LineList)

    End Sub

    Private Sub btnViewMarkings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewMarkings.Click
        If Not String.IsNullOrWhiteSpace(tbx_MarkingPath.Text) Then
            FormQuestionManager.LoadFile(tbx_MarkingPath.Text, True)
            FormQuestionManager.Show()
        End If
    End Sub

    Private Sub btnRemark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemark.Click
        If Not String.IsNullOrWhiteSpace(tbx_MarkingPath.Text) Then
            If FileArr.Count = 1 Then
                SDragDrop_MReset(FileArr)
            End If
        End If
    End Sub

    Private Sub btnGo() Handles btnCompileFCs.Click

        ' Check that pre-checked file status is OK
        '   NOTE: This checks both the previously checked status (textbox color) and the current status of the files
        '   This is necessary in case the files are moved/deleted between entering their location in (first validation) and activating this method
        If txt_CompilerIn.BackColor <> Color.White OrElse Not IO.File.Exists(txt_CompilerIn.Text) Then
            MsgBox("The input file doesn't exist.")
            Exit Sub
        ElseIf txt_CompilerOut.BackColor <> Color.White OrElse Not IO.Directory.Exists(txt_CompilerOut.Text) Then
            MsgBox("The output folder doesn't exist.")
            Exit Sub
        End If

        ' Start reading notes
        Dim FileLines As String()
        If Regex.IsMatch(txt_NotesIn.Text, Form1.TextRegex) Then
            FileLines = File.ReadAllLines(txt_NotesIn.Text)
        ElseIf Regex.IsMatch(txt_NotesIn.Text, Form1.DocRegex) Then
            FileLines = MSFTOfficeInterop.GetWordLines(txt_NotesIn.Text)
        Else
            MsgBox("The input file's type is invalid.") ' Stay consistent with the errors above
            Exit Sub
        End If

        ' Determine item subject
        Dim Subject As String = txt_CompilerSubject.Text
        If String.IsNullOrWhiteSpace(Subject) AndAlso cbxAppendSubject.Checked Then
            Subject = Path.GetFileNameWithoutExtension(txt_CompilerIn.Text) & "_"
        End If

        ' Generate output file paths
        Dim InputFileName As String = If(String.IsNullOrWhiteSpace(Subject), Subject, "\")
        If Not cbxAppendSubject.Checked Then
            InputFileName = "\"
        End If
        Dim QOutPath As String = txt_CompilerOut.Text & InputFileName & "questions.txt"
        Dim AOutPath As String = txt_CompilerOut.Text & InputFileName & "answers.txt"

        ' Create output folder if necessary (note: this doesn't prompt the user as to whether to create the directory)
        '   The try/catch is to notify the user of any failures
        If Not IO.Directory.Exists(txt_CompilerOut.Text) Then
            Try
                IO.Directory.CreateDirectory(txt_CompilerOut.Text)
            Catch
                MsgBox("The directory specified for the output files doesn't exist and couldn't be created. No files have been modified.")
                Exit Sub
            End Try
        End If

        ' Alert the user if existing files will be modified at the output location
        If File.Exists(QOutPath) AndAlso File.Exists(AOutPath) Then
            If MsgBox("There are existing output files in that location. The output generated here will be appended to them. Continue?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                MsgBox("The flashcard compilation operation was cancelled. No files have been altered.")
                Exit Sub
            End If
        End If

        ' Read file
        Dim LtrStr As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim QuestionCnt As Integer = 0
        Dim AnswerCnt As Integer = 0 ' Used for questions that have multiple valid answers
        Dim CurLineIsQuestion As Boolean = True
        Dim LineQueue_Questions, LineQueue_Answers As New List(Of String) ' A queue, not a stack, according to Dylan @ ACM
        Dim SR As New StreamReader(txt_CompilerIn.Text)

        ' Include empty lines if necessary
        If File.Exists(QOutPath) AndAlso _
            Not String.IsNullOrWhiteSpace(File.ReadLines(QOutPath).LastOrDefault) Then
            LineQueue_Questions.Add("")
        End If
        If File.Exists(AOutPath) AndAlso _
            Not String.IsNullOrWhiteSpace(File.ReadLines(AOutPath).LastOrDefault) Then
            LineQueue_Answers.Add("")
        End If

        ' Include subject, if one is supplied
        If Not String.IsNullOrWhiteSpace(Subject) Then
            LineQueue_Answers.Add("[" & Subject & "]")
            LineQueue_Questions.Add("[" & Subject & "]")
        End If

        ' Write main file
        While Not SR.EndOfStream

            ' Get line
            Dim Line As String = SR.ReadLine

            ' Skip null lines
            If Line.Length = 0 OrElse String.IsNullOrWhiteSpace(Line) Then
                CurLineIsQuestion = True ' The next valid line is a question
                Continue While
            End If

            ' Add line to line queue
            If CurLineIsQuestion Then

                ' Variable updates
                CurLineIsQuestion = False
                QuestionCnt += 1
                AnswerCnt = 0

                ' Add question to line queue
                LineQueue_Questions.Add(QuestionCnt.ToString & "=" & Line)

            Else

                ' Exception handler
                If AnswerCnt > 25 Then
                    MsgBox("Question " & QuestionCnt & " has more than the maximum number (26) of answers.")
                End If

                ' Look for improperly formatted files (answers that look like questions) if the question has more than one answer
                If AnswerCnt = 0 Then
                    Dim Errors As Integer = -CInt(Char.IsUpper(Line.Chars(0))) - CInt(Line.Contains("?"))

                    If Errors = 2 Then
                        MsgBox("This file seems to be improperly formatted. (See question " & QuestionCnt & ".)", MsgBoxStyle.Exclamation)
                    ElseIf Errors = 1 Then
                        MsgBox("This file might be improperly formatted. (See question " & QuestionCnt & ".)", MsgBoxStyle.Information)
                    End If
                End If

                ' Add answer to line queue
                LineQueue_Answers.Add(QuestionCnt.ToString & LtrStr.Chars(AnswerCnt) & "=" & Line)

                ' Variable updates (these happen AFTER the current line is added to the queue)
                AnswerCnt += 1

            End If

        End While

        ' Write to output files
        IO.File.AppendAllLines(QOutPath, LineQueue_Questions)
        IO.File.AppendAllLines(AOutPath, LineQueue_Answers)

    End Sub

#Region "File drag-drop handlers"

    ' === F/C Compiler ===

    ' - tbIn -
    Private Sub tbIn_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles txt_CompilerIn.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub tbIn_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles txt_CompilerIn.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then

            ' Load drag-dropped filepath
            Dim FileList As String() = CType(CType(e.Data.GetData(DataFormats.FileDrop), String()), String())
            If FileList.Count <> 0 Then
                txt_CompilerIn.Text = FileList.GetValue(0).ToString
            End If

            ' Reminder
            If FileList.Count > 1 Then
                MsgBox("The flashcard converter can only process one file at a time. The first file in the drag-drop list will be used.")
            End If

        End If
    End Sub

    ' - tbOut -
    Private Sub tbOut_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles txt_CompilerOut.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub tbOut_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles txt_CompilerOut.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then

            ' Load drag-dropped filepath
            Dim FileList As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
            If FileList.Count <> 0 Then
                txt_CompilerOut.Text = FileList.GetValue(0).ToString
            End If

            ' Reminder
            If FileList.Count > 1 Then
                MsgBox("Only one output directory can be used at a time.")
            End If

        End If
    End Sub

    ' === Markings refresher ===
    Private Sub txtMarkingsPath_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tbx_MarkingPath.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub txtMarkingsPath_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tbx_MarkingPath.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then

            ' Load drag-dropped filepath
            Dim FileList As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
            If FileList.Count <> 0 Then
                tbx_MarkingPath.Text = FileList.GetValue(0).ToString
            End If

            ' Reminder
            If FileList.Count > 1 Then
                MsgBox("The re-marking system can only process one markings file at a time. The first file in the drag-drop list will be used.")
            End If

        End If
    End Sub

    ' === Note parser ===

    ' - txt_NotesIn -
    Private Sub txtNotesIn_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles txt_NotesIn.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub txtNotesIn_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles txt_NotesIn.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then

            ' Load drag-dropped filepath
            Dim FileList As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
            If FileList.Count <> 0 Then
                txt_NotesIn.Text = FileList.GetValue(0).ToString
            End If

            ' Reminder
            If FileList.Count > 1 Then
                MsgBox("The note parsing system can only process one file at a time. The first file in the drag-drop list will be used.")
            End If

        End If
    End Sub

    ' - txt_NotesOut -
    Private Sub txtNotesOut_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles txt_NotesOut.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub txtNotesOut_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles txt_NotesOut.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then

            ' Load drag-dropped filepath
            Dim FileList As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
            If FileList.Count <> 0 Then
                txt_NotesOut.Text = FileList.GetValue(0).ToString
            End If

            ' Reminder
            If FileList.Count > 1 Then
                MsgBox("The note parsing system can only output to one file at a time. The first file in the drag-drop list will be used.")
            End If

        End If
    End Sub

#End Region
#Region "File dialog handlers"
    ' F/C compiler file dialog handlers
    Private Sub OFileDlgIn_Open() Handles btn_OFDIn.Click

        ' File dialog directory persistence
        If Form1.MasterFileDialogLocation.Length <> 0 AndAlso Directory.Exists(Form1.MasterFileDialogLocation) Then
            OFileDlg.InitialDirectory = Form1.MasterFileDialogLocation
        End If
        Dim DlgResult As DialogResult = OFileDlg.ShowDialog()
        If OFileDlg.FileName.Length > 1 Then
            Form1.MasterFileDialogLocation = Path.GetDirectoryName(OFileDlg.FileName)
        End If

        ' Handle resulting file path
        If DlgResult = DialogResult.OK Then
            txt_CompilerIn.Text = OFileDlg.FileName
        End If

    End Sub
    Private Sub OFolderDlgOut_Open() Handles btn_OFDOut.Click

        ' Folder dialog directory persistence
        If Form1.MasterFileDialogLocation.Length <> 0 AndAlso Directory.Exists(Form1.MasterFileDialogLocation) Then
            OFolderDlg.SelectedPath = Form1.MasterFileDialogLocation
        End If
        Dim DlgResult As DialogResult = OFolderDlg.ShowDialog()
        If OFolderDlg.SelectedPath.Length > 1 Then
            Form1.MasterFileDialogLocation = Path.GetDirectoryName(OFolderDlg.SelectedPath)
        End If

        ' Handle resulting file path
        If DlgResult = DialogResult.OK Then
            txt_CompilerOut.Text = OFolderDlg.SelectedPath
        End If

    End Sub

    ' Markings file dialog handler
    Private Sub OFileDlgMarkings_Open() Handles btn_OFDMarkings.Click

        ' File dialog directory persistence
        If Form1.MasterFileDialogLocation.Length <> 0 AndAlso Directory.Exists(Form1.MasterFileDialogLocation) Then
            OFileDlg.InitialDirectory = Form1.MasterFileDialogLocation
        End If
        Dim DlgResult As DialogResult = OFileDlg.ShowDialog()
        If OFileDlg.FileName.Length > 1 Then
            Form1.MasterFileDialogLocation = Path.GetDirectoryName(OFileDlg.FileName)
        End If

        ' Handle resulting file path
        If DlgResult = DialogResult.OK Then
            tbx_MarkingPath.Text = OFileDlg.FileName
        End If

    End Sub

    ' Notes file dialog handlers
    Private Sub OFileDlgNotesIn_Open() Handles btn_NotesOFDIn.Click

        ' File dialog directory persistence
        If Form1.MasterFileDialogLocation.Length <> 0 AndAlso Directory.Exists(Form1.MasterFileDialogLocation) Then
            OFileDlg.InitialDirectory = Form1.MasterFileDialogLocation
        End If
        Dim DlgResult As DialogResult = OFileDlg.ShowDialog()
        If OFileDlg.FileName.Length > 1 Then
            Form1.MasterFileDialogLocation = Path.GetDirectoryName(OFileDlg.FileName)
        End If

        ' Handle resulting file path
        If DlgResult = DialogResult.OK Then
            txt_NotesIn.Text = OFileDlg.FileName
        End If

    End Sub
    Private Sub OFileDlgNotesOut_Open() Handles btn_NotesOFDOut.Click

        ' Folder dialog directory persistence
        If Form1.MasterFileDialogLocation.Length <> 0 AndAlso Directory.Exists(Form1.MasterFileDialogLocation) Then
            OFolderDlg.SelectedPath = Form1.MasterFileDialogLocation
        End If
        Dim DlgResult As DialogResult = OFolderDlg.ShowDialog()
        If OFolderDlg.SelectedPath.Length > 1 Then
            Form1.MasterFileDialogLocation = Path.GetDirectoryName(OFolderDlg.SelectedPath)
        End If

        ' Handle resulting file path
        If DlgResult = DialogResult.OK Then
            txt_NotesOut.Text = OFolderDlg.SelectedPath
        End If

    End Sub
#End Region
#Region "Textbox validation (Functions + Event Handlers)"

#Region "Functions"
    ' Validate file textbox (this assumes that the target file path is in the provided TextBox)
    Public Shared Sub ValidateFileTextbox(ByRef Textbox As TextBox, ByVal MustExist As Boolean)

        Dim Text As String = Textbox.Text
        If Text.Length = 0 OrElse Not Text.Contains(".") OrElse (MustExist AndAlso Not File.Exists(Text)) Then
            Textbox.BackColor = Color.Red
        Else
            Textbox.BackColor = Color.White
        End If

    End Sub

    ' Validate folder textbox (this assumes that the target file path is in the provided TextBox)
    Public Shared Sub ValidateFolderTextbox(ByRef Textbox As TextBox, ByVal MustExist As Boolean)

        ' Make sure the output folder directory isn't ending with a \, isn't to a file (i.e. it SHOULD NOT contain a "."), and is non-null
        Dim Text As String = Textbox.Text
        If Text.Length = 0 OrElse Text.Last = "\" OrElse Text.Contains(".") OrElse (MustExist AndAlso Not Directory.Exists(Text)) Then
            Textbox.BackColor = Color.Red
        Else
            Textbox.BackColor = Color.White
        End If

    End Sub
#End Region
#Region "Event Handlers"

    ' === F/C compiler ===
    Private Sub tbOutChgd() Handles txt_CompilerOut.TextChanged
        ValidateFolderTextbox(txt_CompilerOut, False)
    End Sub
    Private Sub tbInChgd() Handles txt_CompilerIn.TextChanged
        ValidateFileTextbox(txt_CompilerIn, True)
    End Sub

    ' === Note parser ===
    Private Sub txtNotesInChgd() Handles txt_NotesIn.TextChanged
        ValidateFileTextbox(txt_NotesIn, True)
    End Sub
    Private Sub txtNotesOutChgd() Handles txt_NotesOut.TextChanged
        ValidateFolderTextbox(txt_NotesOut, True)
    End Sub

    ' === Numerical inputs for re-marking system ===
    Private Sub tbxNum_TextChanged() Handles tbxNum.TextChanged
        If Integer.TryParse(tbxNum.Text, New Integer) Then
            tbxNum.BackColor = Color.White
        Else
            tbxNum.BackColor = Color.Red
        End If
    End Sub
    Private Sub txtMarkTgt_TextChanged() Handles txtMarkTgt.TextChanged
        If Integer.TryParse(txtMarkTgt.Text, New Integer) Then
            txtMarkTgt.BackColor = Color.White
        Else
            txtMarkTgt.BackColor = Color.Red
        End If
    End Sub

#End Region

#End Region

#Region "Cornell note parser specific stuff"

    ' Parsing handler
    Private Sub StartParsing() Handles btnNotesGo.Click

        Dim CurIndex As Integer = 0

        ' Make sure textboxes are valid
        ValidateFileTextbox(txt_NotesIn, True)
        ValidateFolderTextbox(txt_NotesOut, False)
        If txt_NotesIn.BackColor = Color.Red OrElse txt_NotesOut.BackColor = Color.Red Then
            MsgBox("Invalid file path(s) specified. Note parsing operation will be cancelled.")
            Exit Sub
        End If

        ' Start reading notes
        Dim FileLines As String()
        If Regex.IsMatch(txt_NotesIn.Text, Form1.TextRegex) Then
            FileLines = File.ReadAllLines(txt_NotesIn.Text)
        ElseIf Regex.IsMatch(txt_NotesIn.Text, Form1.DocRegex) Then
            FileLines = MSFTOfficeInterop.GetWordLines(txt_NotesIn.Text)
        Else
            MsgBox("Invalid file type. Note parsing operation will be cancelled.")
            Exit Sub
        End If

        FormCornellAIEditor.txtNotes.Lines = FileLines

        ' Highlight according to classification
        For i = 0 To FileLines.Count - 1

            ' Get current line and data about it
            Dim CurLine As String = FileLines.GetValue(i).ToString
            Dim IsHeader As Boolean = i <> FileLines.Count - 1 AndAlso CornellParsingAI.lineIsHeader(CurLine, FileLines.GetValue(i + 1).ToString) ' Some ninja short-circuiting
            Dim IsDefinition As Boolean = CornellParsingAI.lineIsDefinition(CurLine)

            ' Definitions - lime green
            If CornellParsingAI.lineIsDefinition(CurLine) Then

                ' Write question to output
                FormCornellAIEditor.IsDefinitionQuestion = True
                AddQuestion(CornellParsingAI.questionFromDefinition(CurLine))

            End If

            ' Headers - pink
            If i < FileLines.Count - 1 AndAlso IsHeader Then

                ' Find header and its attached elements
                Dim Answer As String = ""
                Dim QLines As List(Of String) = CornellParsingAI.findList(FileLines, i)

                ' Remove definition from header, if applicable
                Dim FirstLine As String = QLines.Item(0)
                If CornellParsingAI.lineIsDefinition(FirstLine) Then

                    FirstLine = FirstLine.Remove(FirstLine.IndexOf(" - ")).Trim({" "c, CChar(vbTab)}) ' Isolate the question component of the definition

                End If

                ' Assemble question
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
                Answer = Answer.Trim

                ' Output answer to QAM creation dialog
                If QLines.Count <> 0 Then

                    ' Isolate answer (here, there is only one)
                    Dim AnswerList As New List(Of String)
                    AnswerList.Add(Answer)

                    ' Present question to user and add it to the question list, if applicable
                    FormCornellAIEditor.IsDefinitionQuestion = False
                    AddQuestion(New Question(FirstLine.Trim, AnswerList, 0))

                End If

            End If

            ' Track current line index
            CurIndex += CurLine.Length + 1

        Next

        ' === Compile questions ===
        ' Get temporary file location
        Dim TempFileLoc As String = txt_NotesOut.Text & "\fcvb_temp.txt"

        ' Make sure temporary file location is OK to use
        If File.Exists(TempFileLoc) Then
            If MsgBox("A file 'fcvb_temp.txt' already exists in path " & Path.GetDirectoryName(txt_NotesOut.Text) & _
                      ". In order to continue, it must be deleted. Delete it?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                ' Delete conflicting file 
                Try
                    File.Delete(TempFileLoc)
                Catch

                    ' Stop the operation
                    MsgBox("Unable to delete temporary file. The note parsing operation has been cancelled.")
                    ExtractedQAMList.Clear()
                    Exit Sub

                End Try
            Else

                ' Stop the operation
                MsgBox("Note parsing operation cancelled by user.")
                ExtractedQAMList.Clear()
                Exit Sub

            End If
        End If

        ' Save questions to a temporary file (in question-answer (Q/A) format)
        Dim SWriter As New StreamWriter(TempFileLoc)

        '   Add empty line to file if necessary
        For Each Item As Question In ExtractedQAMList

            ' Question
            SWriter.WriteLine(Item.Question)

            ' Answers
            For Each Answer As String In Item.AnswerList
                SWriter.WriteLine(Answer)
            Next

            ' Spacing
            SWriter.WriteLine()

        Next

        Dim Errored As Boolean = False
        Try
            ' Perform write
            SWriter.Flush()

        Catch

            ' Catch errors
            MsgBox("Temporary question-answer file could not be written to. The note parsing operation has been cancelled.")
            Errored = True

        Finally

            ' Clean up SWriter
            SWriter.Close()
            SWriter.Dispose()

            ' Clear list of extracted QAM objects
            ExtractedQAMList.Clear()

        End Try

        ' Exit sub (if an error occurred)
        If Errored Then
            Exit Sub
        End If

        ' Compile questions (Q/A format --> official flashcard format)
        '   TODO/NOTE: This is hackish because it inserts info into GUI elements (without restoring them!) instead of passing said info as function parameters (which is proper behavior)
        txt_CompilerIn.Text = TempFileLoc
        txt_CompilerOut.Text = txt_NotesOut.Text
        txt_CompilerSubject.Text = txt_NotesNewSubjectName.Text
        btnGo() ' Call the compilation method

        ' Delete the temporary file
        Try
            File.Delete(TempFileLoc)
        Catch ex As Exception
            MsgBox("Could not delete temporary file located at " & TempFileLoc & ". Remember to delete it before running the note parser again.")
        End Try

        ' Notify user of operation's completion
        MsgBox("Cornell parsing operation complete.")

    End Sub

    ' Question adding method
    Public Sub AddQuestion(ByRef Question As Question)

        ' Format question
        'If Question.Question.Length <> 0 AndAlso Char.IsLower(Question.Question.First) Then
        '    Question.Question = CStr(Question.Question.Substring(0, 1).ToUpperInvariant) & Question.Question.Remove(0, 1)
        'End If

        ' Update auto-complete suggestions
        Dim AutoCompleteSuggestions As String() = ({"Who were",
                                        "What are the meanings of",
                                        "What are the significances of",
                                        "Name the [#]",
                                        "What are the [#] types of",
                                        "What are the [#]",
                                        "([#] things)",
                                        "Define"}) ' Some sample values for now

        ' Format auto-complete suggestions
        For j = 0 To AutoCompleteSuggestions.Count - 1

            Dim CurValue As String = AutoCompleteSuggestions.GetValue(j).ToString

            ' Regex stuff
            Dim L As String = "(?<=(\A|\s))"
            Dim R As String = "(?=(\Z|\s))"

            ' Singular/plural handlers
            If Not Question.AnswerList.FirstOrDefault.Contains(",") Then

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
            CurValue = CurValue.Replace("[#]", (Regex.Matches(Question.AnswerList.FirstOrDefault, ",").Count + 1).ToString)

            ' Update answer list (with correctly formatted value)
            AutoCompleteSuggestions.SetValue(CurValue, j)

        Next

        ' Load auto-complete suggestions into Cornell AI question editor
        FormCornellAIEditor.acTbx.AutoCompleteSuggestions.Clear()
        FormCornellAIEditor.acTbx.AutoCompleteSuggestions.AddRange(AutoCompleteSuggestions)

        ' Load QAM details into Cornell AI question editor
        FormCornellAIEditor.QAMObj = Question
        FormCornellAIEditor.DialogResult = DialogResult.None
        Dim Result As DialogResult = FormCornellAIEditor.ShowDialog()

        ' Save finalized QAM object
        If Result = DialogResult.OK Then
            ExtractedQAMList.Add(FormCornellAIEditor.QAMObj)
        End If

    End Sub

#End Region

    ' Append-subject-checkbox cooperation
    Private Sub cbxSubjectCooperate1() Handles TabControl1.SelectedIndexChanged, Me.VisibleChanged
        If TabControl1.SelectedIndex = 0 Then
            cbxNotesAppendSubject.Checked = cbxAppendSubject.Checked
        Else
            cbxAppendSubject.Checked = cbxNotesAppendSubject.Checked
        End If
    End Sub

End Class
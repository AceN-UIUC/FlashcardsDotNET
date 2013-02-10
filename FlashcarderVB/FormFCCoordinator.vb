Imports System.IO

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

    Public FileArr As String()

    Private Sub Loader() Handles MyBase.Load

        ' Automatically use the icon/title of the first form
        Me.Icon = Form1.Icon
        Me.Text = Form1.Text + " - FC Coordinator WIP"

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
        If tbx_In.BackColor <> Color.White OrElse Not IO.File.Exists(tbx_In.Text) Then
            MsgBox("The input file is invalid.")
            Exit Sub
        ElseIf txt_Out.BackColor <> Color.White OrElse Not IO.Directory.Exists(txt_Out.Text) Then
            MsgBox("The output folder is invalid.")
            Exit Sub
        End If

        ' Generate output file paths
        Dim InputFileName As String = tbx_In.Text.Substring(tbx_In.Text.LastIndexOf("\"))
        If InputFileName.Contains(".") Then
            InputFileName = InputFileName.Substring(0, InputFileName.IndexOf(".")) & "_"
        Else
            InputFileName &= "_"
        End If
        If Not cbxAppendSubject.Checked Then
            InputFileName = "\"
        End If
        Dim QOutPath As String = txt_Out.Text & InputFileName & "questions.txt"
        Dim AOutPath As String = txt_Out.Text & InputFileName & "answers.txt"

        ' Create output folder if necessary (note: this doesn't prompt the user as to whether to create the directory)
        '   The try/catch is to notify the user of any failures
        If Not IO.Directory.Exists(txt_Out.Text) Then
            Try
                IO.Directory.CreateDirectory(txt_Out.Text)
            Catch
                MsgBox("The directory specified for the output files doesn't exist and couldn't be created. No files have been modified.")
                Exit Sub
            End Try
        End If

        ' Alert the user if existing files will be modified at the output location
        If Dir(QOutPath) <> "" AndAlso Dir(AOutPath) <> "" Then
            If MsgBox("There are existing output files in that location. The output generated here will be appended to them. Continue?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                MsgBox("No files have been altered.")
                Exit Sub
            End If
        End If

        ' Read file
        Dim LtrStr As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim QuestionCnt As Integer = 0
        Dim AnswerCnt As Integer = 0 ' Used for questions that have multiple valid answers
        Dim CurLineIsQuestion As Boolean = True
        Dim LineQueue_Questions, LineQueue_Answers As New List(Of String) ' A queue, not a stack, according to Dylan @ ACM
        Dim SR As New StreamReader(tbx_In.Text)
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

    Private Sub StartParsing() Handles Button1.Click

        ' DBG
        FormCornellParser.Show()

    End Sub

#Region "File drag-drop handlers"

    ' === F/C Compiler ===

    ' - tbIn -
    Private Sub tbIn_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tbx_In.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub tbIn_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tbx_In.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then

            ' Load drag-dropped filepath
            Dim FileList As String() = e.Data.GetData(DataFormats.FileDrop)
            If FileList.Count <> 0 Then
                tbx_In.Text = FileList.GetValue(0).ToString
            End If

            ' Reminder
            If FileList.Count > 1 Then
                MsgBox("The flashcard converter can only process one file at a time. The first file in the drag-drop list will be used.")
            End If

        End If
    End Sub

    ' - tbOut -
    Private Sub tbOut_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles txt_Out.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub tbOut_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles txt_Out.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then

            ' Load drag-dropped filepath
            Dim FileList As String() = e.Data.GetData(DataFormats.FileDrop)
            If FileList.Count <> 0 Then
                txt_Out.Text = FileList.GetValue(0).ToString
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
            Dim FileList As String() = e.Data.GetData(DataFormats.FileDrop)
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
            Dim FileList As String() = e.Data.GetData(DataFormats.FileDrop)
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
            Dim FileList As String() = e.Data.GetData(DataFormats.FileDrop)
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
            tbx_In.Text = OFileDlg.FileName
        End If

    End Sub
    Private Sub OFolderDlgOut_Open() Handles btn_OFDOut.Click

        ' File dialog directory persistence
        If Form1.MasterFileDialogLocation.Length <> 0 AndAlso Directory.Exists(Form1.MasterFileDialogLocation) Then
            OFolderDlg.SelectedPath = Form1.MasterFileDialogLocation
        End If
        Dim DlgResult As DialogResult = OFolderDlg.ShowDialog()
        If OFolderDlg.SelectedPath.Length > 1 Then
            Form1.MasterFileDialogLocation = Path.GetDirectoryName(OFolderDlg.SelectedPath)
        End If

        ' Handle resulting file path
        If DlgResult = DialogResult.OK Then
            txt_Out.Text = OFolderDlg.SelectedPath
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
            txt_NotesOut.Text = OFileDlg.FileName
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

    ' NOTE: These are in no particular order

    Private Sub tbOutChgd() Handles txt_Out.TextChanged
        ValidateFolderTextbox(txt_Out, False)
    End Sub
    Private Sub txtNotesInChgd() Handles txt_NotesIn.TextChanged
        ValidateFileTextbox(txt_NotesIn, True)
    End Sub
    Private Sub tbInChgd() Handles tbx_In.TextChanged
        ValidateFileTextbox(tbx_In, True)
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

End Class
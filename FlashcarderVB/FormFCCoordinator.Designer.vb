<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormFCCoordinator
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.btn_NotesOFDOut = New System.Windows.Forms.Button()
        Me.btn_NotesOFDIn = New System.Windows.Forms.Button()
        Me.txt_NotesOut = New System.Windows.Forms.TextBox()
        Me.txt_NotesIn = New System.Windows.Forms.TextBox()
        Me.btnNotesGo = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.btn_OFDMarkings = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtMarkTgt = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnViewMarkings = New System.Windows.Forms.Button()
        Me.btnRemark = New System.Windows.Forms.Button()
        Me.btnOpenManually = New System.Windows.Forms.Button()
        Me.tbx_MarkingPath = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.tbxNum = New System.Windows.Forms.TextBox()
        Me.rbnCOptLessThan = New System.Windows.Forms.RadioButton()
        Me.rbnCOptSameAs = New System.Windows.Forms.RadioButton()
        Me.rbnCOptMoreThan = New System.Windows.Forms.RadioButton()
        Me.rbnCOptAll = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
<<<<<<< HEAD
        Me.btn_OFDOut = New System.Windows.Forms.Button()
        Me.btn_OFDIn = New System.Windows.Forms.Button()
        Me.cbxAppendSubject = New System.Windows.Forms.CheckBox()
        Me.btnCompileFCs = New System.Windows.Forms.Button()
=======
        Me.cbxAppendSubject = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
>>>>>>> 25af17bc353be6d73e5bb06eb61d844c911f90af
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_CompilerOut = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_CompilerIn = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
<<<<<<< HEAD
        Me.OFileDlg = New System.Windows.Forms.OpenFileDialog()
        Me.OFolderDlg = New System.Windows.Forms.FolderBrowserDialog()
=======
        Me.OFDlg = New System.Windows.Forms.OpenFileDialog()
>>>>>>> 25af17bc353be6d73e5bb06eb61d844c911f90af
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(0, 25)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(402, 265)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.btn_NotesOFDOut)
        Me.TabPage1.Controls.Add(Me.btn_NotesOFDIn)
        Me.TabPage1.Controls.Add(Me.txt_NotesOut)
        Me.TabPage1.Controls.Add(Me.txt_NotesIn)
        Me.TabPage1.Controls.Add(Me.btnNotesGo)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(394, 236)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Notes --> Q/A"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'btn_NotesOFDOut
        '
        Me.btn_NotesOFDOut.Location = New System.Drawing.Point(365, 68)
        Me.btn_NotesOFDOut.Name = "btn_NotesOFDOut"
        Me.btn_NotesOFDOut.Size = New System.Drawing.Size(23, 23)
        Me.btn_NotesOFDOut.TabIndex = 10
        Me.btn_NotesOFDOut.Text = "+"
        Me.btn_NotesOFDOut.UseVisualStyleBackColor = True
        '
        'btn_NotesOFDIn
        '
        Me.btn_NotesOFDIn.Location = New System.Drawing.Point(365, 34)
        Me.btn_NotesOFDIn.Name = "btn_NotesOFDIn"
        Me.btn_NotesOFDIn.Size = New System.Drawing.Size(23, 23)
        Me.btn_NotesOFDIn.TabIndex = 9
        Me.btn_NotesOFDIn.Text = "+"
        Me.btn_NotesOFDIn.UseVisualStyleBackColor = True
        '
        'txt_NotesOut
        '
        Me.txt_NotesOut.Location = New System.Drawing.Point(82, 69)
        Me.txt_NotesOut.Name = "txt_NotesOut"
        Me.txt_NotesOut.Size = New System.Drawing.Size(277, 22)
        Me.txt_NotesOut.TabIndex = 3
        '
        'txt_NotesIn
        '
        Me.txt_NotesIn.Location = New System.Drawing.Point(82, 35)
        Me.txt_NotesIn.Name = "txt_NotesIn"
        Me.txt_NotesIn.Size = New System.Drawing.Size(277, 22)
        Me.txt_NotesIn.TabIndex = 2
        '
        'btnNotesGo
        '
        Me.btnNotesGo.Location = New System.Drawing.Point(164, 117)
        Me.btnNotesGo.Name = "btnNotesGo"
        Me.btnNotesGo.Size = New System.Drawing.Size(75, 28)
        Me.btnNotesGo.TabIndex = 1
        Me.btnNotesGo.Text = "Go"
        Me.btnNotesGo.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(377, 85)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "This converts Cornell-format notes into questions/answers." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Notes file:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Outp" & _
            "ut file:"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.btn_OFDMarkings)
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Controls.Add(Me.btnOpenManually)
        Me.TabPage2.Controls.Add(Me.tbx_MarkingPath)
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(394, 236)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Refresh Markings"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'btn_OFDMarkings
        '
        Me.btn_OFDMarkings.Location = New System.Drawing.Point(361, 31)
        Me.btn_OFDMarkings.Name = "btn_OFDMarkings"
        Me.btn_OFDMarkings.Size = New System.Drawing.Size(23, 23)
        Me.btn_OFDMarkings.TabIndex = 8
        Me.btn_OFDMarkings.Text = "+"
        Me.btn_OFDMarkings.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtMarkTgt)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.btnViewMarkings)
        Me.GroupBox2.Controls.Add(Me.btnRemark)
        Me.GroupBox2.Location = New System.Drawing.Point(205, 95)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(179, 127)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Remarking Options"
        '
        'txtMarkTgt
        '
        Me.txtMarkTgt.Location = New System.Drawing.Point(72, 22)
        Me.txtMarkTgt.Name = "txtMarkTgt"
        Me.txtMarkTgt.Size = New System.Drawing.Size(41, 22)
        Me.txtMarkTgt.TabIndex = 3
        Me.txtMarkTgt.Text = "0"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 17)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Mark as"
        '
        'btnViewMarkings
        '
        Me.btnViewMarkings.Location = New System.Drawing.Point(17, 69)
        Me.btnViewMarkings.Name = "btnViewMarkings"
        Me.btnViewMarkings.Size = New System.Drawing.Size(145, 23)
        Me.btnViewMarkings.TabIndex = 1
        Me.btnViewMarkings.Text = "View QAM List"
        Me.btnViewMarkings.UseVisualStyleBackColor = True
        '
        'btnRemark
        '
        Me.btnRemark.Location = New System.Drawing.Point(17, 98)
        Me.btnRemark.Name = "btnRemark"
        Me.btnRemark.Size = New System.Drawing.Size(145, 23)
        Me.btnRemark.TabIndex = 0
        Me.btnRemark.Text = "Remark"
        Me.btnRemark.UseVisualStyleBackColor = True
        '
        'btnOpenManually
        '
        Me.btnOpenManually.Location = New System.Drawing.Point(155, 57)
        Me.btnOpenManually.Name = "btnOpenManually"
        Me.btnOpenManually.Size = New System.Drawing.Size(84, 32)
        Me.btnOpenManually.TabIndex = 4
        Me.btnOpenManually.Text = "Open File"
        Me.btnOpenManually.UseVisualStyleBackColor = True
        '
        'tbx_MarkingPath
        '
        Me.tbx_MarkingPath.Enabled = False
        Me.tbx_MarkingPath.Location = New System.Drawing.Point(39, 31)
        Me.tbx_MarkingPath.Name = "tbx_MarkingPath"
        Me.tbx_MarkingPath.Size = New System.Drawing.Size(316, 22)
        Me.tbx_MarkingPath.TabIndex = 3
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.tbxNum)
        Me.GroupBox1.Controls.Add(Me.rbnCOptLessThan)
        Me.GroupBox1.Controls.Add(Me.rbnCOptSameAs)
        Me.GroupBox1.Controls.Add(Me.rbnCOptMoreThan)
        Me.GroupBox1.Controls.Add(Me.rbnCOptAll)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 95)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(187, 131)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Marking Filter"
        '
        'tbxNum
        '
        Me.tbxNum.Location = New System.Drawing.Point(128, 50)
        Me.tbxNum.Name = "tbxNum"
        Me.tbxNum.Size = New System.Drawing.Size(54, 23)
        Me.tbxNum.TabIndex = 4
        Me.tbxNum.Text = "0"
        Me.tbxNum.Visible = False
        '
        'rbnCOptLessThan
        '
        Me.rbnCOptLessThan.AutoSize = True
        Me.rbnCOptLessThan.Location = New System.Drawing.Point(6, 106)
        Me.rbnCOptLessThan.Name = "rbnCOptLessThan"
        Me.rbnCOptLessThan.Size = New System.Drawing.Size(96, 21)
        Me.rbnCOptLessThan.TabIndex = 3
        Me.rbnCOptLessThan.TabStop = True
        Me.rbnCOptLessThan.Text = "Less Than"
        Me.rbnCOptLessThan.UseVisualStyleBackColor = True
        '
        'rbnCOptSameAs
        '
        Me.rbnCOptSameAs.AutoSize = True
        Me.rbnCOptSameAs.Location = New System.Drawing.Point(6, 77)
        Me.rbnCOptSameAs.Name = "rbnCOptSameAs"
        Me.rbnCOptSameAs.Size = New System.Drawing.Size(112, 21)
        Me.rbnCOptSameAs.TabIndex = 2
        Me.rbnCOptSameAs.TabStop = True
        Me.rbnCOptSameAs.Text = "Not Equal To"
        Me.rbnCOptSameAs.UseVisualStyleBackColor = True
        '
        'rbnCOptMoreThan
        '
        Me.rbnCOptMoreThan.AutoSize = True
        Me.rbnCOptMoreThan.Location = New System.Drawing.Point(6, 50)
        Me.rbnCOptMoreThan.Name = "rbnCOptMoreThan"
        Me.rbnCOptMoreThan.Size = New System.Drawing.Size(115, 21)
        Me.rbnCOptMoreThan.TabIndex = 1
        Me.rbnCOptMoreThan.Text = "Greater Than"
        Me.rbnCOptMoreThan.UseVisualStyleBackColor = True
        '
        'rbnCOptAll
        '
        Me.rbnCOptAll.AutoSize = True
        Me.rbnCOptAll.Checked = True
        Me.rbnCOptAll.Location = New System.Drawing.Point(6, 23)
        Me.rbnCOptAll.Name = "rbnCOptAll"
        Me.rbnCOptAll.Size = New System.Drawing.Size(96, 21)
        Me.rbnCOptAll.TabIndex = 0
        Me.rbnCOptAll.TabStop = True
        Me.rbnCOptAll.Text = "Everything"
        Me.rbnCOptAll.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(315, 51)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "This refreshes the markings of a flashcard group" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "File:"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.btn_OFDOut)
        Me.TabPage3.Controls.Add(Me.btn_OFDIn)
        Me.TabPage3.Controls.Add(Me.cbxAppendSubject)
        Me.TabPage3.Controls.Add(Me.btnCompileFCs)
        Me.TabPage3.Controls.Add(Me.Label7)
        Me.TabPage3.Controls.Add(Me.txt_CompilerOut)
        Me.TabPage3.Controls.Add(Me.Label6)
        Me.TabPage3.Controls.Add(Me.txt_CompilerIn)
        Me.TabPage3.Controls.Add(Me.Label3)
        Me.TabPage3.Location = New System.Drawing.Point(4, 25)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(394, 236)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Text <--> Flashcard"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
<<<<<<< HEAD
        'btn_OFDOut
        '
        Me.btn_OFDOut.Location = New System.Drawing.Point(361, 109)
        Me.btn_OFDOut.Name = "btn_OFDOut"
        Me.btn_OFDOut.Size = New System.Drawing.Size(23, 23)
        Me.btn_OFDOut.TabIndex = 8
        Me.btn_OFDOut.Text = "+"
        Me.btn_OFDOut.UseVisualStyleBackColor = True
        '
        'btn_OFDIn
        '
        Me.btn_OFDIn.Location = New System.Drawing.Point(361, 53)
        Me.btn_OFDIn.Name = "btn_OFDIn"
        Me.btn_OFDIn.Size = New System.Drawing.Size(23, 23)
        Me.btn_OFDIn.TabIndex = 7
        Me.btn_OFDIn.Text = "+"
        Me.btn_OFDIn.UseVisualStyleBackColor = True
        '
=======
>>>>>>> 25af17bc353be6d73e5bb06eb61d844c911f90af
        'cbxAppendSubject
        '
        Me.cbxAppendSubject.AutoSize = True
        Me.cbxAppendSubject.Location = New System.Drawing.Point(6, 153)
        Me.cbxAppendSubject.Name = "cbxAppendSubject"
        Me.cbxAppendSubject.Size = New System.Drawing.Size(249, 21)
        Me.cbxAppendSubject.TabIndex = 6
        Me.cbxAppendSubject.Text = "Append subject to output file name"
        Me.cbxAppendSubject.UseVisualStyleBackColor = True
        '
<<<<<<< HEAD
        'btnCompileFCs
=======
        'Button1
>>>>>>> 25af17bc353be6d73e5bb06eb61d844c911f90af
        '
        Me.btnCompileFCs.Location = New System.Drawing.Point(164, 204)
        Me.btnCompileFCs.Name = "btnCompileFCs"
        Me.btnCompileFCs.Size = New System.Drawing.Size(75, 23)
        Me.btnCompileFCs.TabIndex = 5
        Me.btnCompileFCs.Text = "Go"
        Me.btnCompileFCs.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 90)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(111, 17)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Output FOLDER"
        '
        'txt_CompilerOut
        '
        Me.txt_CompilerOut.AllowDrop = True
        Me.txt_CompilerOut.Location = New System.Drawing.Point(6, 110)
        Me.txt_CompilerOut.Name = "txt_CompilerOut"
        Me.txt_CompilerOut.Size = New System.Drawing.Size(349, 22)
        Me.txt_CompilerOut.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 33)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 17)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Input FILE"
        '
        'txt_CompilerIn
        '
        Me.txt_CompilerIn.AllowDrop = True
        Me.txt_CompilerIn.Location = New System.Drawing.Point(6, 53)
        Me.txt_CompilerIn.Name = "txt_CompilerIn"
        Me.txt_CompilerIn.Size = New System.Drawing.Size(349, 22)
        Me.txt_CompilerIn.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(317, 17)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "This converts between text and flashcard formats"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 2)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(358, 17)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "To open a file, drag-drop it or click the Open File button"
        '
        'OFileDlg
        '
<<<<<<< HEAD
        Me.OFileDlg.Filter = "AHK Shortcuts|*.ahk|Flashcards|*.ini|All Files|*.*"
        '
=======
>>>>>>> 25af17bc353be6d73e5bb06eb61d844c911f90af
        'FormFCCoordinator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 289)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "FormFCCoordinator"
        Me.Text = "FC Coordinator WIP"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents tbxNum As System.Windows.Forms.TextBox
    Friend WithEvents rbnCOptLessThan As System.Windows.Forms.RadioButton
    Friend WithEvents rbnCOptSameAs As System.Windows.Forms.RadioButton
    Friend WithEvents rbnCOptMoreThan As System.Windows.Forms.RadioButton
    Friend WithEvents rbnCOptAll As System.Windows.Forms.RadioButton
    Friend WithEvents btnOpenManually As System.Windows.Forms.Button
    Friend WithEvents tbx_MarkingPath As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRemark As System.Windows.Forms.Button
    Friend WithEvents OFileDlg As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnViewMarkings As System.Windows.Forms.Button
    Friend WithEvents txtMarkTgt As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnCompileFCs As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txt_CompilerOut As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_CompilerIn As System.Windows.Forms.TextBox
    Friend WithEvents cbxAppendSubject As System.Windows.Forms.CheckBox
    Friend WithEvents btn_OFDOut As System.Windows.Forms.Button
    Friend WithEvents btn_OFDIn As System.Windows.Forms.Button
    Friend WithEvents OFolderDlg As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents btnNotesGo As System.Windows.Forms.Button
    Friend WithEvents btn_NotesOFDOut As System.Windows.Forms.Button
    Friend WithEvents btn_NotesOFDIn As System.Windows.Forms.Button
    Friend WithEvents txt_NotesOut As System.Windows.Forms.TextBox
    Friend WithEvents txt_NotesIn As System.Windows.Forms.TextBox
    Friend WithEvents btn_OFDMarkings As System.Windows.Forms.Button
End Class

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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtMarkTgt = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnViewMarkings = New System.Windows.Forms.Button()
        Me.btnRemark = New System.Windows.Forms.Button()
        Me.btnOpenManually = New System.Windows.Forms.Button()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.tbxNum = New System.Windows.Forms.TextBox()
        Me.rbnCOptLessThan = New System.Windows.Forms.RadioButton()
        Me.rbnCOptSameAs = New System.Windows.Forms.RadioButton()
        Me.rbnCOptMoreThan = New System.Windows.Forms.RadioButton()
        Me.rbnCOptAll = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.cbxAppendSubject = New System.Windows.Forms.CheckBox()
        Me.btnCompileFCs = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.tbOut = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.tbIn = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.OFDlg = New System.Windows.Forms.OpenFileDialog()
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
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(0, 25)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(402, 265)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(394, 236)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Combine"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(302, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "This combines multiple flashcard sets into one."
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Controls.Add(Me.btnOpenManually)
        Me.TabPage2.Controls.Add(Me.txtFilePath)
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
        Me.btnViewMarkings.Location = New System.Drawing.Point(6, 69)
        Me.btnViewMarkings.Name = "btnViewMarkings"
        Me.btnViewMarkings.Size = New System.Drawing.Size(145, 23)
        Me.btnViewMarkings.TabIndex = 1
        Me.btnViewMarkings.Text = "View QAM List"
        Me.btnViewMarkings.UseVisualStyleBackColor = True
        '
        'btnRemark
        '
        Me.btnRemark.Location = New System.Drawing.Point(6, 98)
        Me.btnRemark.Name = "btnRemark"
        Me.btnRemark.Size = New System.Drawing.Size(145, 23)
        Me.btnRemark.TabIndex = 0
        Me.btnRemark.Text = "Remark"
        Me.btnRemark.UseVisualStyleBackColor = True
        '
        'btnOpenManually
        '
        Me.btnOpenManually.Location = New System.Drawing.Point(127, 57)
        Me.btnOpenManually.Name = "btnOpenManually"
        Me.btnOpenManually.Size = New System.Drawing.Size(84, 32)
        Me.btnOpenManually.TabIndex = 4
        Me.btnOpenManually.Text = "Open File"
        Me.btnOpenManually.UseVisualStyleBackColor = True
        '
        'txtFilePath
        '
        Me.txtFilePath.Enabled = False
        Me.txtFilePath.Location = New System.Drawing.Point(39, 31)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(346, 22)
        Me.txtFilePath.TabIndex = 3
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
        Me.TabPage3.Controls.Add(Me.cbxAppendSubject)
        Me.TabPage3.Controls.Add(Me.btnCompileFCs)
        Me.TabPage3.Controls.Add(Me.Label7)
        Me.TabPage3.Controls.Add(Me.tbOut)
        Me.TabPage3.Controls.Add(Me.Label6)
        Me.TabPage3.Controls.Add(Me.tbIn)
        Me.TabPage3.Controls.Add(Me.Label3)
        Me.TabPage3.Location = New System.Drawing.Point(4, 25)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(394, 236)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Text <--> Flashcard"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
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
        'btnCompileFCs
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
        'tbOut
        '
        Me.tbOut.AllowDrop = True
        Me.tbOut.Location = New System.Drawing.Point(6, 110)
        Me.tbOut.Name = "tbOut"
        Me.tbOut.Size = New System.Drawing.Size(378, 22)
        Me.tbOut.TabIndex = 3
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
        'tbIn
        '
        Me.tbIn.AllowDrop = True
        Me.tbIn.Location = New System.Drawing.Point(6, 53)
        Me.tbIn.Name = "tbIn"
        Me.tbIn.Size = New System.Drawing.Size(378, 22)
        Me.tbIn.TabIndex = 1
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
        'TabPage4
        '
        Me.TabPage4.Location = New System.Drawing.Point(4, 25)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(394, 236)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Split/Tier"
        Me.TabPage4.UseVisualStyleBackColor = True
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
        'OFDlg
        '
        Me.OFDlg.Filter = "AHK Shortcuts|*.ahk|Flashcards|*.ini|All Files|*.*"
        '
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
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRemark As System.Windows.Forms.Button
    Friend WithEvents OFDlg As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnViewMarkings As System.Windows.Forms.Button
    Friend WithEvents txtMarkTgt As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents btnCompileFCs As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents tbOut As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tbIn As System.Windows.Forms.TextBox
    Friend WithEvents cbxAppendSubject As System.Windows.Forms.CheckBox
End Class

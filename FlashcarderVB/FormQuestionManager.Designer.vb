<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormQuestionManager
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
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnOpenManually = New System.Windows.Forms.Button()
        Me.cbx_VrifyQs = New System.Windows.Forms.CheckBox()
        Me.cbx_VrifyAs = New System.Windows.Forms.CheckBox()
        Me.cbx_VrifyMs = New System.Windows.Forms.CheckBox()
        Me.gbxChkList = New System.Windows.Forms.GroupBox()
        Me.OFDlg = New System.Windows.Forms.OpenFileDialog()
        Me.btnQuiz = New System.Windows.Forms.Button()
        Me.cbxAutoLoad = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.gbxActsOpts = New System.Windows.Forms.GroupBox()
        Me.cbxHighlightQs = New System.Windows.Forms.CheckBox()
        Me.tbxMinWrong = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbxRandomQs = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lvwQAMList = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.gbxChkList.SuspendLayout()
        Me.gbxActsOpts.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(475, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "(Hint: Drag a flashcard set's AHK shortcut or one of its Q/A/M files to start)"
        '
        'btnOpenManually
        '
        Me.btnOpenManually.Location = New System.Drawing.Point(105, 125)
        Me.btnOpenManually.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnOpenManually.Name = "btnOpenManually"
        Me.btnOpenManually.Size = New System.Drawing.Size(140, 30)
        Me.btnOpenManually.TabIndex = 1
        Me.btnOpenManually.Text = "Open File Manually"
        Me.btnOpenManually.UseVisualStyleBackColor = True
        '
        'cbx_VrifyQs
        '
        Me.cbx_VrifyQs.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbx_VrifyQs.AutoSize = True
        Me.cbx_VrifyQs.Enabled = False
        Me.cbx_VrifyQs.Location = New System.Drawing.Point(5, 21)
        Me.cbx_VrifyQs.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cbx_VrifyQs.Name = "cbx_VrifyQs"
        Me.cbx_VrifyQs.Size = New System.Drawing.Size(94, 21)
        Me.cbx_VrifyQs.TabIndex = 3
        Me.cbx_VrifyQs.Text = "Questions"
        Me.cbx_VrifyQs.UseVisualStyleBackColor = True
        '
        'cbx_VrifyAs
        '
        Me.cbx_VrifyAs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbx_VrifyAs.AutoSize = True
        Me.cbx_VrifyAs.Enabled = False
        Me.cbx_VrifyAs.Location = New System.Drawing.Point(197, 20)
        Me.cbx_VrifyAs.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cbx_VrifyAs.Name = "cbx_VrifyAs"
        Me.cbx_VrifyAs.Size = New System.Drawing.Size(83, 21)
        Me.cbx_VrifyAs.TabIndex = 4
        Me.cbx_VrifyAs.Text = "Answers"
        Me.cbx_VrifyAs.UseVisualStyleBackColor = True
        '
        'cbx_VrifyMs
        '
        Me.cbx_VrifyMs.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbx_VrifyMs.AutoSize = True
        Me.cbx_VrifyMs.Enabled = False
        Me.cbx_VrifyMs.Location = New System.Drawing.Point(404, 21)
        Me.cbx_VrifyMs.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cbx_VrifyMs.Name = "cbx_VrifyMs"
        Me.cbx_VrifyMs.Size = New System.Drawing.Size(87, 21)
        Me.cbx_VrifyMs.TabIndex = 5
        Me.cbx_VrifyMs.Text = "Markings"
        Me.cbx_VrifyMs.UseVisualStyleBackColor = True
        '
        'gbxChkList
        '
        Me.gbxChkList.Controls.Add(Me.cbx_VrifyQs)
        Me.gbxChkList.Controls.Add(Me.cbx_VrifyMs)
        Me.gbxChkList.Controls.Add(Me.cbx_VrifyAs)
        Me.gbxChkList.Location = New System.Drawing.Point(13, 30)
        Me.gbxChkList.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.gbxChkList.Name = "gbxChkList"
        Me.gbxChkList.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.gbxChkList.Size = New System.Drawing.Size(497, 53)
        Me.gbxChkList.TabIndex = 6
        Me.gbxChkList.TabStop = False
        Me.gbxChkList.Text = "List of loaded items"
        '
        'OFDlg
        '
        Me.OFDlg.Filter = "Flashcards|*.txt;*.ini|AHK Shortcuts|*.ahk|All Files|*.*"
        '
        'btnQuiz
        '
        Me.btnQuiz.Location = New System.Drawing.Point(247, 125)
        Me.btnQuiz.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnQuiz.Name = "btnQuiz"
        Me.btnQuiz.Size = New System.Drawing.Size(104, 30)
        Me.btnQuiz.TabIndex = 7
        Me.btnQuiz.Text = "Take Quiz"
        Me.btnQuiz.UseVisualStyleBackColor = True
        '
        'cbxAutoLoad
        '
        Me.cbxAutoLoad.AutoSize = True
        Me.cbxAutoLoad.Checked = True
        Me.cbxAutoLoad.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbxAutoLoad.Location = New System.Drawing.Point(7, 22)
        Me.cbxAutoLoad.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cbxAutoLoad.Name = "cbxAutoLoad"
        Me.cbxAutoLoad.Size = New System.Drawing.Size(157, 21)
        Me.cbxAutoLoad.TabIndex = 9
        Me.cbxAutoLoad.Text = "Enable auto-loading"
        Me.cbxAutoLoad.UseVisualStyleBackColor = True
        '
        'gbxActsOpts
        '
        Me.gbxActsOpts.Controls.Add(Me.cbxHighlightQs)
        Me.gbxActsOpts.Controls.Add(Me.tbxMinWrong)
        Me.gbxActsOpts.Controls.Add(Me.Label3)
        Me.gbxActsOpts.Controls.Add(Me.cbxRandomQs)
        Me.gbxActsOpts.Controls.Add(Me.cbxAutoLoad)
        Me.gbxActsOpts.Controls.Add(Me.btnOpenManually)
        Me.gbxActsOpts.Controls.Add(Me.btnQuiz)
        Me.gbxActsOpts.Location = New System.Drawing.Point(13, 283)
        Me.gbxActsOpts.Margin = New System.Windows.Forms.Padding(4)
        Me.gbxActsOpts.Name = "gbxActsOpts"
        Me.gbxActsOpts.Padding = New System.Windows.Forms.Padding(4)
        Me.gbxActsOpts.Size = New System.Drawing.Size(497, 161)
        Me.gbxActsOpts.TabIndex = 10
        Me.gbxActsOpts.TabStop = False
        Me.gbxActsOpts.Text = "Actions / Options"
        '
        'cbxHighlightQs
        '
        Me.cbxHighlightQs.AutoSize = True
        Me.cbxHighlightQs.Location = New System.Drawing.Point(240, 22)
        Me.cbxHighlightQs.Name = "cbxHighlightQs"
        Me.cbxHighlightQs.Size = New System.Drawing.Size(250, 21)
        Me.cbxHighlightQs.TabIndex = 13
        Me.cbxHighlightQs.Text = "Highlight questions by wrong count"
        Me.cbxHighlightQs.UseVisualStyleBackColor = True
        '
        'tbxMinWrong
        '
        Me.tbxMinWrong.Location = New System.Drawing.Point(295, 71)
        Me.tbxMinWrong.MaxLength = 10
        Me.tbxMinWrong.Name = "tbxMinWrong"
        Me.tbxMinWrong.Size = New System.Drawing.Size(33, 22)
        Me.tbxMinWrong.TabIndex = 12
        Me.tbxMinWrong.Text = "0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(290, 17)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Ignore questions with wrong counts less than"
        '
        'cbxRandomQs
        '
        Me.cbxRandomQs.AutoSize = True
        Me.cbxRandomQs.Checked = True
        Me.cbxRandomQs.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbxRandomQs.Location = New System.Drawing.Point(7, 49)
        Me.cbxRandomQs.Name = "cbxRandomQs"
        Me.cbxRandomQs.Size = New System.Drawing.Size(169, 21)
        Me.cbxRandomQs.TabIndex = 10
        Me.cbxRandomQs.Text = "Randomize Questions"
        Me.cbxRandomQs.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(115, 89)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(249, 17)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Double-click a question below to edit it"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lvwQAMList
        '
        Me.lvwQAMList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.lvwQAMList.Location = New System.Drawing.Point(14, 109)
        Me.lvwQAMList.Name = "lvwQAMList"
        Me.lvwQAMList.Size = New System.Drawing.Size(490, 167)
        Me.lvwQAMList.TabIndex = 12
        Me.lvwQAMList.UseCompatibleStateImageBehavior = False
        Me.lvwQAMList.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = ""
        '
        'FormQuestionManager
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(523, 457)
        Me.Controls.Add(Me.lvwQAMList)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.gbxActsOpts)
        Me.Controls.Add(Me.gbxChkList)
        Me.Controls.Add(Me.Label1)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MinimumSize = New System.Drawing.Size(535, 359)
        Me.Name = "FormQuestionManager"
        Me.Text = "Ace's Flashcarder - Question Manager"
        Me.gbxChkList.ResumeLayout(False)
        Me.gbxChkList.PerformLayout()
        Me.gbxActsOpts.ResumeLayout(False)
        Me.gbxActsOpts.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnOpenManually As System.Windows.Forms.Button
    Friend WithEvents cbx_VrifyQs As System.Windows.Forms.CheckBox
    Friend WithEvents cbx_VrifyAs As System.Windows.Forms.CheckBox
    Friend WithEvents cbx_VrifyMs As System.Windows.Forms.CheckBox
    Friend WithEvents gbxChkList As System.Windows.Forms.GroupBox
    Friend WithEvents OFDlg As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnQuiz As System.Windows.Forms.Button
    Friend WithEvents cbxAutoLoad As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents gbxActsOpts As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbxRandomQs As System.Windows.Forms.CheckBox
    Friend WithEvents tbxMinWrong As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbxHighlightQs As System.Windows.Forms.CheckBox
    Friend WithEvents lvwQAMList As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
End Class

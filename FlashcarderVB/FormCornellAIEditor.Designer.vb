<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormCornellAIEditor
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
        Me.txtQs = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlOptions = New System.Windows.Forms.Panel()
        Me.btnAddAnswer = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.gbxAutoCompletion = New System.Windows.Forms.GroupBox()
        Me.cbxCapQ = New System.Windows.Forms.CheckBox()
        Me.cbxAppendQMark = New System.Windows.Forms.CheckBox()
        Me.btnAddAfter = New System.Windows.Forms.Button()
        Me.btnAddBefore = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.cbxCapA = New System.Windows.Forms.CheckBox()
        Me.pnlOptions.SuspendLayout()
        Me.gbxAutoCompletion.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtQs
        '
        Me.txtQs.Location = New System.Drawing.Point(16, 25)
        Me.txtQs.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtQs.Multiline = True
        Me.txtQs.Name = "txtQs"
        Me.txtQs.Size = New System.Drawing.Size(373, 112)
        Me.txtQs.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 146)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 17)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Answer(s)"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 17)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Question"
        '
        'pnlOptions
        '
        Me.pnlOptions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pnlOptions.Controls.Add(Me.btnAddAnswer)
        Me.pnlOptions.Controls.Add(Me.btnCancel)
        Me.pnlOptions.Controls.Add(Me.btnSave)
        Me.pnlOptions.Location = New System.Drawing.Point(16, 430)
        Me.pnlOptions.Name = "pnlOptions"
        Me.pnlOptions.Size = New System.Drawing.Size(377, 31)
        Me.pnlOptions.TabIndex = 9
        '
        'btnAddAnswer
        '
        Me.btnAddAnswer.Location = New System.Drawing.Point(102, 3)
        Me.btnAddAnswer.Name = "btnAddAnswer"
        Me.btnAddAnswer.Size = New System.Drawing.Size(99, 23)
        Me.btnAddAnswer.TabIndex = 8
        Me.btnAddAnswer.Text = "Add Answer"
        Me.btnAddAnswer.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(288, 3)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(207, 3)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'gbxAutoCompletion
        '
        Me.gbxAutoCompletion.Controls.Add(Me.cbxCapA)
        Me.gbxAutoCompletion.Controls.Add(Me.cbxCapQ)
        Me.gbxAutoCompletion.Controls.Add(Me.cbxAppendQMark)
        Me.gbxAutoCompletion.Controls.Add(Me.btnAddAfter)
        Me.gbxAutoCompletion.Controls.Add(Me.btnAddBefore)
        Me.gbxAutoCompletion.Location = New System.Drawing.Point(16, 258)
        Me.gbxAutoCompletion.Name = "gbxAutoCompletion"
        Me.gbxAutoCompletion.Size = New System.Drawing.Size(377, 166)
        Me.gbxAutoCompletion.TabIndex = 10
        Me.gbxAutoCompletion.TabStop = False
        Me.gbxAutoCompletion.Text = "Auto-Completion"
        '
        'cbxCapQ
        '
        Me.cbxCapQ.AutoSize = True
        Me.cbxCapQ.Location = New System.Drawing.Point(6, 139)
        Me.cbxCapQ.Name = "cbxCapQ"
        Me.cbxCapQ.Size = New System.Drawing.Size(53, 21)
        Me.cbxCapQ.TabIndex = 12
        Me.cbxCapQ.Text = "Q/q"
        Me.cbxCapQ.UseVisualStyleBackColor = True
        '
        'cbxAppendQMark
        '
        Me.cbxAppendQMark.AutoSize = True
        Me.cbxAppendQMark.Checked = True
        Me.cbxAppendQMark.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbxAppendQMark.Location = New System.Drawing.Point(239, 139)
        Me.cbxAppendQMark.Name = "cbxAppendQMark"
        Me.cbxAppendQMark.Size = New System.Drawing.Size(132, 21)
        Me.cbxAppendQMark.TabIndex = 11
        Me.cbxAppendQMark.Text = "(Q) Append ? / ."
        Me.cbxAppendQMark.UseVisualStyleBackColor = True
        '
        'btnAddAfter
        '
        Me.btnAddAfter.Location = New System.Drawing.Point(274, 56)
        Me.btnAddAfter.Name = "btnAddAfter"
        Me.btnAddAfter.Size = New System.Drawing.Size(89, 29)
        Me.btnAddAfter.TabIndex = 2
        Me.btnAddAfter.Text = "Add after"
        Me.btnAddAfter.UseVisualStyleBackColor = True
        '
        'btnAddBefore
        '
        Me.btnAddBefore.Location = New System.Drawing.Point(274, 21)
        Me.btnAddBefore.Name = "btnAddBefore"
        Me.btnAddBefore.Size = New System.Drawing.Size(89, 29)
        Me.btnAddBefore.TabIndex = 0
        Me.btnAddBefore.Text = "Add before"
        Me.btnAddBefore.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(439, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 17)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Notes File"
        '
        'txtNotes
        '
        Me.txtNotes.Location = New System.Drawing.Point(442, 25)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtNotes.Size = New System.Drawing.Size(365, 436)
        Me.txtNotes.TabIndex = 12
        '
        'cbxCapA
        '
        Me.cbxCapA.AutoSize = True
        Me.cbxCapA.Location = New System.Drawing.Point(64, 139)
        Me.cbxCapA.Name = "cbxCapA"
        Me.cbxCapA.Size = New System.Drawing.Size(51, 21)
        Me.cbxCapA.TabIndex = 13
        Me.cbxCapA.Text = "A/a"
        Me.cbxCapA.UseVisualStyleBackColor = True
        '
        'FormCornellAIEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(819, 466)
        Me.Controls.Add(Me.txtNotes)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.gbxAutoCompletion)
        Me.Controls.Add(Me.pnlOptions)
        Me.Controls.Add(Me.txtQs)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FormCornellAIEditor"
        Me.Text = "Ace's Flashcarder - Cornell AI Question Editor"
        Me.pnlOptions.ResumeLayout(False)
        Me.gbxAutoCompletion.ResumeLayout(False)
        Me.gbxAutoCompletion.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtQs As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlOptions As System.Windows.Forms.Panel
    Friend WithEvents btnAddAnswer As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents gbxAutoCompletion As System.Windows.Forms.GroupBox
    Friend WithEvents btnAddAfter As System.Windows.Forms.Button
    Friend WithEvents btnAddBefore As System.Windows.Forms.Button
    Friend WithEvents cbxAppendQMark As System.Windows.Forms.CheckBox
    Friend WithEvents cbxCapQ As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents cbxCapA As System.Windows.Forms.CheckBox
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class QuestionResponseDialog
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
        Me.btnNoMark = New System.Windows.Forms.Button()
        Me.btnCorrect = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnIncorrect = New System.Windows.Forms.Button()
        Me.lbl = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnNoMark
        '
        Me.btnNoMark.Location = New System.Drawing.Point(477, 175)
        Me.btnNoMark.Name = "btnNoMark"
        Me.btnNoMark.Size = New System.Drawing.Size(110, 23)
        Me.btnNoMark.TabIndex = 2
        Me.btnNoMark.Text = "&Don't Mark"
        Me.btnNoMark.UseVisualStyleBackColor = True
        '
        'btnCorrect
        '
        Me.btnCorrect.Location = New System.Drawing.Point(245, 175)
        Me.btnCorrect.Name = "btnCorrect"
        Me.btnCorrect.Size = New System.Drawing.Size(110, 23)
        Me.btnCorrect.TabIndex = 0
        Me.btnCorrect.Text = "Mark &Correct"
        Me.btnCorrect.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(593, 175)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(43, 23)
        Me.btnEdit.TabIndex = 3
        Me.btnEdit.Text = "&Edit"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnIncorrect
        '
        Me.btnIncorrect.Location = New System.Drawing.Point(361, 175)
        Me.btnIncorrect.Name = "btnIncorrect"
        Me.btnIncorrect.Size = New System.Drawing.Size(110, 23)
        Me.btnIncorrect.TabIndex = 1
        Me.btnIncorrect.Text = "Mark &Incorrect"
        Me.btnIncorrect.UseVisualStyleBackColor = True
        '
        'lbl
        '
        Me.lbl.AutoEllipsis = True
        Me.lbl.Location = New System.Drawing.Point(13, 13)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(614, 159)
        Me.lbl.TabIndex = 4
        Me.lbl.Text = "lbl"
        '
        'QuestionResponseDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(639, 203)
        Me.Controls.Add(Me.lbl)
        Me.Controls.Add(Me.btnIncorrect)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnCorrect)
        Me.Controls.Add(Me.btnNoMark)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "QuestionResponseDialog"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Results Dialog"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnNoMark As System.Windows.Forms.Button
    Friend WithEvents btnCorrect As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnIncorrect As System.Windows.Forms.Button
    Friend WithEvents lbl As System.Windows.Forms.Label

End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SubjectChoiceDlg
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtBox = New System.Windows.Forms.TextBox()
        Me.lbox = New System.Windows.Forms.ListBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.cbxNoMarkingImport = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 4)
        Me.Label1.MaximumSize = New System.Drawing.Size(573, 100)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(478, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Select a subject from the list ot type a subject name to narrow your search."
        '
        'txtBox
        '
        Me.txtBox.Location = New System.Drawing.Point(12, 24)
        Me.txtBox.Name = "txtBox"
        Me.txtBox.Size = New System.Drawing.Size(503, 22)
        Me.txtBox.TabIndex = 2
        '
        'lbox
        '
        Me.lbox.FormattingEnabled = True
        Me.lbox.ItemHeight = 16
        Me.lbox.Location = New System.Drawing.Point(12, 45)
        Me.lbox.Name = "lbox"
        Me.lbox.Size = New System.Drawing.Size(503, 116)
        Me.lbox.TabIndex = 3
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(427, 171)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(89, 28)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Cancel"
        '
        'cbxNoMarkingImport
        '
        Me.cbxNoMarkingImport.AutoSize = True
        Me.cbxNoMarkingImport.Location = New System.Drawing.Point(12, 176)
        Me.cbxNoMarkingImport.Name = "cbxNoMarkingImport"
        Me.cbxNoMarkingImport.Size = New System.Drawing.Size(167, 21)
        Me.cbxNoMarkingImport.TabIndex = 5
        Me.cbxNoMarkingImport.Text = "Don't import markings"
        Me.cbxNoMarkingImport.UseVisualStyleBackColor = True
        '
        'SubjectChoiceDlg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(529, 212)
        Me.Controls.Add(Me.cbxNoMarkingImport)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.lbox)
        Me.Controls.Add(Me.txtBox)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SubjectChoiceDlg"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Select a Subject"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtBox As System.Windows.Forms.TextBox
    Friend WithEvents lbox As System.Windows.Forms.ListBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents cbxNoMarkingImport As System.Windows.Forms.CheckBox

End Class

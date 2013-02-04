<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormQuestionEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormQuestionEditor))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtQs = New System.Windows.Forms.TextBox()
        Me.txtMs = New System.Windows.Forms.TextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pnlOptions = New System.Windows.Forms.Panel()
        Me.btnAddAnswer = New System.Windows.Forms.Button()
        Me.pnlOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Question"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 146)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 17)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Answer(s)"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 17)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Marking"
        '
        'txtQs
        '
        Me.txtQs.Location = New System.Drawing.Point(16, 25)
        Me.txtQs.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtQs.Multiline = True
        Me.txtQs.Name = "txtQs"
        Me.txtQs.Size = New System.Drawing.Size(373, 112)
        Me.txtQs.TabIndex = 3
        '
        'txtMs
        '
        Me.txtMs.Location = New System.Drawing.Point(7, 24)
        Me.txtMs.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtMs.Name = "txtMs"
        Me.txtMs.Size = New System.Drawing.Size(55, 22)
        Me.txtMs.TabIndex = 5
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(207, 29)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(288, 29)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'pnlOptions
        '
        Me.pnlOptions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pnlOptions.Controls.Add(Me.btnAddAnswer)
        Me.pnlOptions.Controls.Add(Me.Label3)
        Me.pnlOptions.Controls.Add(Me.btnCancel)
        Me.pnlOptions.Controls.Add(Me.txtMs)
        Me.pnlOptions.Controls.Add(Me.btnSave)
        Me.pnlOptions.Location = New System.Drawing.Point(16, 256)
        Me.pnlOptions.Name = "pnlOptions"
        Me.pnlOptions.Size = New System.Drawing.Size(377, 64)
        Me.pnlOptions.TabIndex = 8
        '
        'btnAddAnswer
        '
        Me.btnAddAnswer.Location = New System.Drawing.Point(102, 29)
        Me.btnAddAnswer.Name = "btnAddAnswer"
        Me.btnAddAnswer.Size = New System.Drawing.Size(99, 23)
        Me.btnAddAnswer.TabIndex = 8
        Me.btnAddAnswer.Text = "Add Answer"
        Me.btnAddAnswer.UseVisualStyleBackColor = True
        '
        'FormQuestionEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(405, 328)
        Me.Controls.Add(Me.pnlOptions)
        Me.Controls.Add(Me.txtQs)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "FormQuestionEditor"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Ace's Flashcarder - Question Editor"
        Me.pnlOptions.ResumeLayout(False)
        Me.pnlOptions.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtQs As System.Windows.Forms.TextBox
    Friend WithEvents txtMs As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents pnlOptions As System.Windows.Forms.Panel
    Friend WithEvents btnAddAnswer As System.Windows.Forms.Button
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormQuestionPoser
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormQuestionPoser))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblQ = New System.Windows.Forms.Label()
        Me.txtUserAnswer = New System.Windows.Forms.TextBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(51, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(283, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Enter your answer to the following question:"
        '
        'lblQ
        '
        Me.lblQ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblQ.Location = New System.Drawing.Point(16, 36)
        Me.lblQ.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblQ.Name = "lblQ"
        Me.lblQ.Size = New System.Drawing.Size(346, 190)
        Me.lblQ.TabIndex = 1
        '
        'txtUserAnswer
        '
        Me.txtUserAnswer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserAnswer.Location = New System.Drawing.Point(16, 230)
        Me.txtUserAnswer.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUserAnswer.Multiline = True
        Me.txtUserAnswer.Name = "txtUserAnswer"
        Me.txtUserAnswer.Size = New System.Drawing.Size(346, 77)
        Me.txtUserAnswer.TabIndex = 2
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(137, 315)
        Me.btnOK.Margin = New System.Windows.Forms.Padding(4)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(100, 28)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'FormQuestionPoser
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(379, 346)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtUserAnswer)
        Me.Controls.Add(Me.lblQ)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FormQuestionPoser"
        Me.Text = "Ace's Flashcarder - Question"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblQ As System.Windows.Forms.Label
    Friend WithEvents txtUserAnswer As System.Windows.Forms.TextBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
End Class

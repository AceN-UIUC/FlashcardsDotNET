<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.btnIntraMgr = New System.Windows.Forms.Button()
        Me.btnInterMgr = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.MaximumSize = New System.Drawing.Size(370, 900)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(360, 34)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "This is a base form and control panel (for now). Closing this form closes everyth" & _
            "ing else automatically."
        '
        'btnIntraMgr
        '
        Me.btnIntraMgr.Location = New System.Drawing.Point(16, 50)
        Me.btnIntraMgr.Name = "btnIntraMgr"
        Me.btnIntraMgr.Size = New System.Drawing.Size(332, 28)
        Me.btnIntraMgr.TabIndex = 1
        Me.btnIntraMgr.Text = "Flashcard Intra-Manager / Quizzer"
        Me.btnIntraMgr.UseVisualStyleBackColor = True
        '
        'btnInterMgr
        '
        Me.btnInterMgr.Location = New System.Drawing.Point(16, 84)
        Me.btnInterMgr.Name = "btnInterMgr"
        Me.btnInterMgr.Size = New System.Drawing.Size(332, 28)
        Me.btnInterMgr.TabIndex = 2
        Me.btnInterMgr.Text = "Flashcard Inter-Manager"
        Me.btnInterMgr.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(379, 255)
        Me.Controls.Add(Me.btnInterMgr)
        Me.Controls.Add(Me.btnIntraMgr)
        Me.Controls.Add(Me.Label1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Form1"
        Me.Text = "Ace's Flashcarder"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnIntraMgr As System.Windows.Forms.Button
    Friend WithEvents btnInterMgr As System.Windows.Forms.Button

End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AutoCompletingTextBox
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.tb1 = New System.Windows.Forms.TextBox()
        Me.lb1 = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'tb1
        '
        Me.tb1.Location = New System.Drawing.Point(0, 0)
        Me.tb1.Name = "tb1"
        Me.tb1.Size = New System.Drawing.Size(330, 22)
        Me.tb1.TabIndex = 0
        '
        'lb1
        '
        Me.lb1.FormattingEnabled = True
        Me.lb1.ItemHeight = 16
        Me.lb1.Items.AddRange(New Object() {"(No matches found.)"})
        Me.lb1.Location = New System.Drawing.Point(0, 21)
        Me.lb1.Name = "lb1"
        Me.lb1.Size = New System.Drawing.Size(330, 116)
        Me.lb1.TabIndex = 1
        '
        'AutoCompletingTextBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lb1)
        Me.Controls.Add(Me.tb1)
        Me.Name = "AutoCompletingTextBox"
        Me.Size = New System.Drawing.Size(330, 132)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tb1 As System.Windows.Forms.TextBox
    Friend WithEvents lb1 As System.Windows.Forms.ListBox

End Class

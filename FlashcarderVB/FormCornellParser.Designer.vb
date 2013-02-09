<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormCornellParser
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
        Me.tb1 = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'tb1
        '
        Me.tb1.Location = New System.Drawing.Point(13, 13)
        Me.tb1.Name = "tb1"
        Me.tb1.Size = New System.Drawing.Size(463, 305)
        Me.tb1.TabIndex = 0
        Me.tb1.Text = ""
        '
        'FormCornellParser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(488, 330)
        Me.Controls.Add(Me.tb1)
        Me.Name = "FormCornellParser"
        Me.Text = "FormCornellParser"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tb1 As System.Windows.Forms.RichTextBox
End Class

Imports System.Windows.Forms

Public Class SubjectChoiceDlg

    Public Subject As String
    Public SubjectList As New List(Of String)

    Private Sub OKClick()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub CancelClick() Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub txtBox_TextChanged() Handles txtBox.TextChanged
        lbox.Items.Clear()
        For Each S As String In SubjectList
            If S.ToLowerInvariant.StartsWith(txtBox.Text.ToLowerInvariant) Then
                lbox.Items.Add(S)
            End If
        Next
    End Sub

    Private Sub lbox_SelectedIndexChanged() Handles lbox.SelectedIndexChanged
        If Subject <> "FILESELECTMODE" Then
            If lbox.SelectedIndex <> -1 Then
                Subject = lbox.Items.Item(lbox.SelectedIndex).ToString
                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If
        ElseIf lbox.SelectedIndices.Count = 3 + CInt(cbxNoMarkingImport.Checked) Then
            For Each S As Integer In lbox.SelectedIndices
                SubjectList.Add(lbox.Items.Item(S).ToString)
            Next
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Subject = ""
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cbxNoMarkingImport_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxNoMarkingImport.CheckedChanged
        If cbxNoMarkingImport.Checked Then
            Label1.Text = Label1.Text.Replace("3", "2")
        Else
            Label1.Text = Label1.Text.Replace("2", "3")
        End If
    End Sub

End Class

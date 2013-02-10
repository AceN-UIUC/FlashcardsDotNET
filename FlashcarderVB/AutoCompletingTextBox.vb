Public Class AutoCompletingTextBox

    ' List of autocomplete suggestions
    Public AutoCompleteSuggestions As New List(Of String)

    ' Active text
    Public Shadows Property Text As String
        Get
            Return tb1.Text
        End Get
        Set(ByVal value As String)
            tb1.Text = value
        End Set
    End Property

    ' Populate auto-complete
    Private Sub tb1_TextChanged() Handles tb1.TextChanged, Me.VisibleChanged

        ' Search for matching suggestions (that aren't identical to what's been typed)
        lb1.Items.Clear()
        If tb1 IsNot Nothing AndAlso tb1.Text.Length <> 0 Then
            For Each Suggestion As String In AutoCompleteSuggestions
                If Suggestion.Length > tb1.Text.Length AndAlso Suggestion.StartsWith(tb1.Text) Then
                    lb1.Items.Add(Suggestion)
                End If
            Next
        End If

        ' If nothing was found, notify the user
        If lb1.Items.Count = 0 Then
            lb1.Items.Add("(No matches found.)")
        End If
        lb1.SelectedIndex = -1

    End Sub

    ' Auto-complete the textbox
    Private Sub lb1_SelectedIndexChanged() Handles lb1.SelectedIndexChanged
        If lb1.SelectedIndex >= 0 AndAlso lb1.Items(lb1.SelectedIndex).ToString <> "(No matches found.)" Then

            tb1.Text = lb1.Items(lb1.SelectedIndex).ToString

        End If
    End Sub

    ' Size changes
    Private Sub ResizeComponents() Handles Me.Resize

        ' Textbox
        tb1.Width = Me.Width

        ' Listbox
        lb1.Size = New Size(Me.Width, Me.Height - tb1.Height)

    End Sub

End Class

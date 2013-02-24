Imports System.Text.RegularExpressions
Imports System.IO
Imports Microsoft.Office.Interop

Public Class MSFTOfficeInterop

    ' Word instance
    Private Shared WordAppBrowser As New Word.Application

    ' Get lines from a Word document
    Public Shared Function GetWordLines(ByVal FilePath As String) As String()

        ' Validate file
        If Not Regex.IsMatch(FilePath, Form1.DocRegex) OrElse Not File.Exists(FilePath) Then
            Return {""} ' Return null result
        End If

        ' Read file
        WordAppBrowser.Documents.Open(FilePath, ReadOnly:=True, AddToRecentFiles:=False, Visible:=False)
        Form1.Sleep(500)
        Try
            Return WordAppBrowser.ActiveDocument.Content.Text().Replace(vbLf, vbCrLf).Split(vbCrLf)
        Catch
            Return {""} ' Return null
        End Try

    End Function

End Class

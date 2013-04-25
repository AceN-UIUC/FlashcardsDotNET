Imports System.Text.RegularExpressions
Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices

Public Class MSFTOfficeInterop

    ' Word instance
    Public Shared WordAppBrowser As New Word.Application

    ' Get lines from a Word document
    Public Shared Function GetWordLines(ByVal FilePath As String) As String()

        Dim Output As String() = {""}

        ' Validate file
        If Not Regex.IsMatch(FilePath, Form1.DocRegex) OrElse Not File.Exists(FilePath) Then
            Return {""} ' Return null result
        End If

        ' Read file
        '   NOTE: WordAppBrowser starts out hidden - so unless it is sent an un-hide command, it will stay hidden
        Try

            ' Open document
            WordAppBrowser.Documents.Open(FilePath, ReadOnly:=True, AddToRecentFiles:=False)

            ' Wait for Word app to load
            Form1.Sleep(1500)

            ' Fetch text from and close active document
            Try
                Output = WordAppBrowser.ActiveDocument.Content.Text().Replace(vbLf, vbCrLf).Split(vbCrLf)
            Catch e As COMException
                Throw e ' Let the parent try-catch layout handle the exception
            Finally
                WordAppBrowser.ActiveDocument.Close(SaveChanges:=False) ' This always executes, even if its Try throws an exception
            End Try

        Catch e As COMException

            ' RPC error detected - use the simple fix of creating a new instance (let the user deal with closing any old disconnected ones)
            WordAppBrowser = New Word.Application

            ' Pass the error to the calling function (so it can alert the user)
            Throw e

        Catch e As Exception

            ' Pass the error to the calling function (so it can alert the user)
            Throw e

        End Try

        ' Return output
        Return Output

    End Function

    ' Show standard error messagebox
    Public Shared Sub ShowErrorMessagebox()
        MsgBox("The Microsoft Word document-reading instance has been restarted. Try to do what you were doing again; if that fails, close all Microsoft Word processes, then restart the program.", MsgBoxStyle.Exclamation)
    End Sub

End Class

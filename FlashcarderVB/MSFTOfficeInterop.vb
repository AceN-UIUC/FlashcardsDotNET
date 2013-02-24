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
        WordAppBrowser.Documents.Open(FilePath, ReadOnly:=True, AddToRecentFiles:=False)
        Form1.Sleep(1500) ' Wait for the document to load

        ' The reason for using the Output variable is so that the function's output can be easily monitored
        '   Tl;dr: DON'T ONE-LINE THE TWO LINES BELOW!
        ' Also: if a COM exception with the Word API occurs, it should be passed to the parent function
        Try
            Output = WordAppBrowser.ActiveDocument.Content.Text().Replace(vbLf, vbCrLf).Split(vbCrLf)
        Catch e As Exception

            ' Close word browser's active document, and pass exception to the method that called this one
            WordAppBrowser.ActiveDocument.Close(SaveChanges:=False) ' Close active Word doc
            Throw e ' Pass exception along

        End Try

        ' Close active document
        WordAppBrowser.ActiveDocument.Close(SaveChanges:=False)

        ' Return output
        Return Output

    End Function

End Class

Imports System.IO
Imports System.Speech
Imports System.Text.RegularExpressions

' RESTRICTIONS
'   1 question can only have up to 26 anwers

' Types of questions
'   Multichoice
'   Definition
'   Dynamic math? (someday...)

Public Class Form1

    ' Regex patterns
    Public Shared DocRegex As String = "\.doc(x|)"
    Public Shared TextRegex As String = "\.(txt|csv|ini)"
    Public Shared SubjectNameRegex As String = "(?<=(\[)).+(?=(\]))" ' Extracts "Subject Name" from "[Subject Name]"

    Declare Function Sleep Lib "kernel32" (ByVal dwMilliseconds As Integer) As Integer

    Public Shared MasterFileDialogLocation As String = ""

    ' The main title that all of the windows of the program use
    Public Shared MainTitle As String = "Ace's Flashcarder"

    ' The alphabet
    Public Shared LetterStr As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"

    ' Has-changed flags
    Public Shared QHasChgd As Boolean = False
    Public Shared AHasChgd As Boolean = False

#Region "GUI logic (2 event handlers and an on-load handler)"
    Private Sub btnIntraMgr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIntraMgr.Click
        FormQuestionManager.Show()
    End Sub

    Private Sub btnInterMgr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInterMgr.Click
        FormFCCoordinator.Show()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = MainTitle & " - Main Menu"
    End Sub
#End Region

    ' Function that checks if a given line is a subject line
    Public Shared Function IsSubjectLine(ByVal Str As String)
        Return Regex.IsMatch(Str, "^\[.+\]$")
    End Function

    ' MSFT Word instance disposal
    '   NOTE: This function works well here because Form1's closure causes everything else to close (on purpose)
    Public Shared Sub ClosingTasks() Handles Me.FormClosing
        MSFTOfficeInterop.WordAppBrowser.Quit(SaveChanges:=False)
    End Sub

End Class
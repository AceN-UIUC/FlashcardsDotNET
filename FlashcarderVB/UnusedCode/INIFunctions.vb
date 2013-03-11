Imports System.IO

' Temporary class to enable interoperability with AHK variant (May or may not work as of 2/2013)
Public Class INIFunctions

    Shared Function Read(ByVal File As String, ByVal Header As String, ByVal Name As String) As String

        ' Check if file exists
        If String.IsNullOrWhiteSpace(Dir(File)) Then
            MsgBox("Error at INIFunctions.Read: File referenced does not exist.")
            Return ""
        End If

        ' Get file and value
        Dim Reading As Boolean = False
        Dim SR As New StreamReader(File)
        While Not SR.EndOfStream

            ' Get line
            Dim Line As String = SR.ReadLine

            ' Turn reading mode ON
            If Line = "[" + Header + "]" Then
                Reading = True
                Continue While
            End If

            ' Return answer if found
            If Line.StartsWith(Name + "=") And Reading Then
                Return Line.Substring(Name.Length + 1)
            End If

            ' Turn reading mode OFF
            If Form1.IsSubjectLine(Line) Then
                Reading = False
                Continue While
            End If

        End While

        ' Return not found value
        Return "NOT FOUND"

    End Function

    Shared Function Write(ByVal Header As String, ByVal Name As String, ByVal Value As String) As Integer

    End Function

End Class
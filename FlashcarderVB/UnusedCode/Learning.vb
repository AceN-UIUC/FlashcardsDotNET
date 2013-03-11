' Methods for learning material (WIP)
Public Class Learning

    ' Vocalizes questions along with their answers
    Public Sub VocalizeQuestions(ByVal P As String, ByVal StartIdx As Integer, ByVal StopIdx As Integer, Optional ByVal MathMode As Boolean = True, _
                                      Optional ByVal WaitBetweenQandA As Integer = 200, Optional ByVal WaitBetweenQs As Integer = 100, _
                                      Optional ByVal SpeakerSpeed As Integer = -3, Optional ByVal SayQuestionNumber As Boolean = True, _
                                      Optional ByVal EnableSkip As Boolean = False, Optional ByVal MinFailures As Integer = 0, _
                                      Optional ByVal EditFormatting As Boolean = False, Optional ByVal QuestionRepeatCnt As Integer = 3, _
                                      Optional ByVal PromptUser As Boolean = True)

        ' ----- User settings -----
        'MathMode As Boolean         ' Changes $ to delta, * to times, and / and \ to divide
        'WaitAfterQ                  ' Wait period between question and answer (in ms)
        'WaitBetweenQs               ' Wait period between questions (in ms)
        'QuestionRepeatCnt           ' Number of times to repeat the question
        'SpeakerSpeed                ' Speaking speed (from -10 to 10 inclusive)
        'SayQuestionNumber           ' If this is TRUE, speaker will specify question number
        'EnableSkip                  ' Enable skip box
        'MinFailures                 ' Minimum number of wrong answers to a question
        'EditFormatting              ' Replace formatting with names of formatting characters (comma, parenthesis)
        ' --- End user settings ---

        ' Get initial data
        Dim Subject As String = INIFunctions.Read(P + "\settings.ini", "Settings", "Subject")
        Dim QCount As Integer = CInt(INIFunctions.Read(P + "\settings.ini", "Settings", "NumQs"))
        Dim QADir As String = INIFunctions.Read(P + "\settings.ini", "Settings", "AnswerDir")

        If StopIdx = 0 Then
            StopIdx = QCount
        End If

        ' Vocalizer set up
        Dim Speaker As New Speech.Synthesis.SpeechSynthesizer
        Speaker.Rate = SpeakerSpeed

        ' Start vocalizing
        Try
            For i = StartIdx To StopIdx

                ' Get Q/A filepath
                Dim P2 As String = P + "\" + QADir

                ' Get question's markings and skip if needed
                Dim CorrectCnt As Integer = CInt(INIFunctions.Read(P2 + "\markings.ini", Subject, CStr(i)))
                If CorrectCnt < MinFailures Then
                    Continue For
                End If

                ' Say current question number
                If SayQuestionNumber Then
                    Speaker.Speak("Question " + CStr(i) + " of " + CStr(QCount))
                End If

                ' Get question and answer (Q and A)
                Dim VQ As String = INIFunctions.Read(P2 + "\questions.ini", Subject, CStr(i))
                Dim VA As String = INIFunctions.Read(P2 + "\answers.ini", Subject, CStr(i) + "A")

                ' Math mode
                Dim NoEqn As Boolean = True
                If MathMode Then

                    ' Delta
                    VQ = VQ.Replace("$", " delta ")
                    VA = VA.Replace("$", " delta ")

                    ' Multiplication
                    VQ = VQ.Replace("*", " times ")
                    VA = VA.Replace("*", " times ")

                    ' Division
                    If VA.Contains("=") Then
                        NoEqn = False
                        VQ = VQ.Replace("/", "\").Replace("\", " divided by")
                        VA = VA.Replace("/", "\").Replace("\", " divided by")
                    End If
                End If

                If NoEqn And EditFormatting Then

                    ' Commas
                    VQ = VQ.Replace(",", " comma ")
                    VA = VA.Replace(",", " comma ")

                    ' Parenthesis
                    VQ = VQ.Replace("(", " in parenthesis, ").Replace(")", " end parenthesis")
                    VA = VA.Replace("(", " in parenthesis, ").Replace(")", " end parenthesis")

                End If

                ' Vocalize
                For j = 1 To QuestionRepeatCnt
                    Speaker.Speak(VQ)
                    Form1.Sleep(WaitBetweenQandA)

                    If EnableSkip And j = 1 Then

                        ' Verbal skip prompt
                        Speaker.Rate = 0
                        Speaker.SpeakAsync("Should I skip this? Press Enter for NO or Tab and Enter for YES")
                        Speaker.Rate = SpeakerSpeed

                        Dim M = MsgBox("Don't skip?", MsgBoxStyle.YesNo)
                        If M = MsgBoxResult.Yes Then
                            Speaker.SpeakAsyncCancelAll()
                            Exit For
                        End If
                    End If

                    Speaker.Speak(VA)
                    Form1.Sleep(WaitBetweenQs)

                Next

                If WaitBetweenQs <> 0 Then
                    Speaker.Speak("Next Question.")
                End If

            Next
        Catch
        End Try

    End Sub

    ' Displays questions along with their answers in a message box format
    Public Sub MsgBoxQuestions(ByVal P As String, ByVal StartIdx As Integer, ByVal StopIdx As Integer, _
                                      Optional ByVal IncludeQuestionNumber As Boolean = True, _
                                      Optional ByVal MinFailures As Integer = 0, _
                                      Optional ByVal EditFormatting As Boolean = False, Optional ByVal QuestionRepeatCnt As Integer = 3, _
                                      Optional ByVal PromptUser As Boolean = True)

    End Sub

End Class
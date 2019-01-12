Imports Microsoft.VisualBasic

Public Class SearchContract

    Shared Function HighlightSearchParameters(ByVal _SearchString As String, ByVal _TextToHighlight As String) As String

        Dim FirstParameterNameOfDescription As String = ""
        Dim StartPosition As Integer
        Dim EndPosition As Integer
        Dim OccurenceOfSpace As Integer
        Dim sayac As Integer = 0
        Dim DonguBiter As Boolean = False

        While DonguBiter = False

            If sayac = 0 Then
                StartPosition = 0
            Else
                StartPosition = OccurenceOfSpace
            End If

            EndPosition = InStr(StartPosition + 1, Trim(_SearchString), " ")
            OccurenceOfSpace = EndPosition
            sayac = sayac + 1

            If EndPosition = 0 Then
                Dim DescriptionParameter_Value_FirstLoop As String = Mid(Trim(_SearchString), StartPosition + 1, Len(Trim(_SearchString)) - StartPosition).ToString

                If sayac = 1 Then
                    _TextToHighlight = ReturnMarkedText(sayac, _TextToHighlight, DescriptionParameter_Value_FirstLoop)
                Else
                    Select Case sayac
                        Case 2
                            _TextToHighlight = ReturnMarkedText(sayac, _TextToHighlight, DescriptionParameter_Value_FirstLoop)
                        Case 3
                            _TextToHighlight = ReturnMarkedText(sayac, _TextToHighlight, DescriptionParameter_Value_FirstLoop)
                        Case 4
                            _TextToHighlight = ReturnMarkedText(sayac, _TextToHighlight, DescriptionParameter_Value_FirstLoop)
                        Case 5
                            _TextToHighlight = ReturnMarkedText(sayac, _TextToHighlight, DescriptionParameter_Value_FirstLoop)
                        Case 6
                            _TextToHighlight = ReturnMarkedText(sayac, _TextToHighlight, DescriptionParameter_Value_FirstLoop)
                        Case 7
                            _TextToHighlight = ReturnMarkedText(sayac, _TextToHighlight, DescriptionParameter_Value_FirstLoop)
                        Case 8
                            _TextToHighlight = ReturnMarkedText(sayac, _TextToHighlight, DescriptionParameter_Value_FirstLoop)
                        Case 9
                            _TextToHighlight = ReturnMarkedText(sayac, _TextToHighlight, DescriptionParameter_Value_FirstLoop)
                        Case 10
                            _TextToHighlight = ReturnMarkedText(sayac, _TextToHighlight, DescriptionParameter_Value_FirstLoop)
                    End Select
                End If

                DonguBiter = True
            Else
                Dim DescriptionParameter_Value_OtherLoops As String = Mid(Trim(_SearchString), StartPosition + 1, EndPosition - StartPosition - 1).ToString

                If sayac = 1 Then
                    _TextToHighlight = ReturnMarkedText(1, _TextToHighlight, DescriptionParameter_Value_OtherLoops)
                Else
                    Select Case sayac
                        Case 2
                            _TextToHighlight = ReturnMarkedText(sayac, _TextToHighlight, DescriptionParameter_Value_OtherLoops)
                        Case 3
                            _TextToHighlight = ReturnMarkedText(sayac, _TextToHighlight, DescriptionParameter_Value_OtherLoops)
                        Case 4
                            _TextToHighlight = ReturnMarkedText(sayac, _TextToHighlight, DescriptionParameter_Value_OtherLoops)
                        Case 5
                            _TextToHighlight = ReturnMarkedText(sayac, _TextToHighlight, DescriptionParameter_Value_OtherLoops)
                        Case 6
                            _TextToHighlight = ReturnMarkedText(sayac, _TextToHighlight, DescriptionParameter_Value_OtherLoops)
                        Case 7
                            _TextToHighlight = ReturnMarkedText(sayac, _TextToHighlight, DescriptionParameter_Value_OtherLoops)
                        Case 8
                            _TextToHighlight = ReturnMarkedText(sayac, _TextToHighlight, DescriptionParameter_Value_OtherLoops)
                        Case 9
                            _TextToHighlight = ReturnMarkedText(sayac, _TextToHighlight, DescriptionParameter_Value_OtherLoops)
                        Case 10
                            _TextToHighlight = ReturnMarkedText(sayac, _TextToHighlight, DescriptionParameter_Value_OtherLoops)
                    End Select
                End If

            End If
        End While

        Return _TextToHighlight

    End Function

    Shared Function ReturnMarkedText(ByVal NumberOf As Integer, ByVal TextOf As String, ByVal TextHighlight As String) As String

        TextOf = Text.RegularExpressions.Regex.Replace(TextOf, TextHighlight, "<font color = 'red'><u><strong>" & TextHighlight & "</strong></u></font>", Text.RegularExpressions.RegexOptions.IgnoreCase)

        Return TextOf

        Exit Function

        ' THIS SECTION DISABLED, BECAUSE IT CAUSES CORRUPTION ON HTML RESULT. THE REASON IS STYLE TAG.
        'Select Case NumberOf
        '    Case -2
        '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #99FF66" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
        '    Case -1
        '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #A8FFFF" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
        '    Case 0
        '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #FFFF00" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
        '    Case 1
        '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #FFFF00;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
        '    Case 2
        '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #ADFF2F;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
        '    Case 3
        '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #FFB6C1;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
        '    Case 4
        '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #87CEFA;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
        '    Case 5
        '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #DA70D6;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
        '    Case 6
        '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #FF0000;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
        '    Case 7
        '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #8FBC8B;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
        '    Case 8
        '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #1E90FF;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
        '    Case 9
        '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #FF8C00;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
        '    Case 10
        '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #FFD700;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
        'End Select
        'Return TextOf
    End Function


End Class

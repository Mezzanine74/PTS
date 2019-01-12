Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.db

    Public Class CustomClass

        Shared Function DateToEntityFromTextBox(ByVal _textboxvalue As String) As System.Nullable(Of DateTime)

            Return If(String.IsNullOrEmpty(_textboxvalue), DirectCast(Nothing, System.Nullable(Of DateTime)), DateTime.ParseExact(_textboxvalue, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))

        End Function


        Shared Function ConvertDateTimeToTruncDateTime(ByVal _input As DateTime) As DateTime

            Dim _time As DateTime
            _time = _input

            Dim _timeYear As String = _time.Year.ToString
            Dim _timeMonth As String = If(_time.Month.ToString.Length = 1, "0" + _time.Month.ToString, _time.Month.ToString)
            Dim _timeDay As String = If(_time.Day.ToString.Length = 1, "0" + _time.Day.ToString, _time.Day.ToString)

            Return DateToEntityFromTextBox(_timeDay + "/" + _timeMonth + "/" + _timeYear)

        End Function

    End Class


End Namespace

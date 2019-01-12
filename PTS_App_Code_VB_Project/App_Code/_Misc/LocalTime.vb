Imports Microsoft.VisualBasic

Public Class LocalTime
  Shared Function GetTime() As DateTime
    Dim zoneId As String = "Russian Standard Time"
    Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
    Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)
    Return result
  End Function
End Class

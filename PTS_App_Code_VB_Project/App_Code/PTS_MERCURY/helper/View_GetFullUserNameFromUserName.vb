Imports Microsoft.VisualBasic
Namespace PTS_MERCURY.helper

    Public Class View_GetFullUserNameFromUserName

        Shared Function GetUserName(_username As String) As String

            Dim _return As String = ""

            Try

                Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                    _return = (From c In db.View_GetFullUserNameFromUserName Where c.UserName = _username).ToList()(0).UserNameSurnameFromEmail

                End Using

            Catch ex As Exception

            End Try

            Return _return

        End Function

    End Class

End Namespace

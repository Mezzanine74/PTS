Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

    Public Class aspnet_Membership

        Shared Function GetEmailFromUserName(_username As String) As String

            Dim _return As String = ""

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = (From C In db.aspnet_Users Join D In db.aspnet_Membership On C.UserId Equals D.UserId Where C.UserName = _username Select New With {D.LoweredEmail}).ToList()(0).LoweredEmail

            End Using

            Return _return

        End Function

    End Class

End Namespace


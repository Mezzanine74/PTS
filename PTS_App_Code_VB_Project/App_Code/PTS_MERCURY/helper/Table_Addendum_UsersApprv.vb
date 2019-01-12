Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

    Public Class Table_Addendum_UsersApprv

        Shared Sub Insert(_addendumid As Integer, _username As String)

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim A As New PTS_MERCURY.db.Table_Addendum_UsersApprv With {.AddendumID = _addendumid, .UserName = _username, .WhenApproved = LocalTime.GetTime(), .Exception = False}

                db.Table_Addendum_UsersApprv.Attach(A)
                db.Table_Addendum_UsersApprv.Add(A)
                db.SaveChanges()

                db.Dispose()

            End Using

        End Sub
    End Class

End Namespace

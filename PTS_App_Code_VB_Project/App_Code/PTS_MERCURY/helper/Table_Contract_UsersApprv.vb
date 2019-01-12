Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

    Public Class Table_Contract_UsersApprv

        Shared Sub Insert(_contractid As Integer, _username As String)

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim A As New PTS_MERCURY.db.Table_Contract_UsersApprv With {.ContractID = _contractid, .UserName = _username, .WhenApproved = LocalTime.GetTime(), .Exception = False}

                db.Table_Contract_UsersApprv.Attach(A)
                db.Table_Contract_UsersApprv.Add(A)
                db.SaveChanges()

                db.Dispose()

            End Using

        End Sub

    End Class

End Namespace

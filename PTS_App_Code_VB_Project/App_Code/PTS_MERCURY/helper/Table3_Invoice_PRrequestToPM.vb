Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

    Public Class Table3_Invoice_PRrequestToPM

        Shared Sub ApproveForPM(_invoiceID As Integer, _username As String)

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If (Aggregate C In db.Table3_Invoice_PRrequestToPM Where C.InvoiceID = _invoiceID Into Count()) > 0 Then

                    Dim A = (From C In db.Table3_Invoice_PRrequestToPM Where C.InvoiceID = _invoiceID).ToList()(0)
                    Dim _localtime As DateTime = LocalTime.GetTime()

                    A.WhoApproved = _username
                    A.ApprovedOrNot = True
                    A.WhenApproved = _localtime

                    db.SaveChanges()

                End If

            End Using

        End Sub

        Shared Sub RemoveApprovalForPM(_invoiceID As Integer, _username As String)

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If (Aggregate C In db.Table3_Invoice_PRrequestToPM Where C.InvoiceID = _invoiceID Into Count()) > 0 Then

                    Dim A = (From C In db.Table3_Invoice_PRrequestToPM Where C.InvoiceID = _invoiceID).ToList()(0)
                    Dim _localtime As DateTime = LocalTime.GetTime()

                    A.WhoApproved = Nothing
                    A.ApprovedOrNot = False
                    A.WhenApproved = Nothing

                    db.SaveChanges()

                End If

            End Using

        End Sub

    End Class

End Namespace

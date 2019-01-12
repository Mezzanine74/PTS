Imports Microsoft.VisualBasic
Imports System.Net.Mail

Namespace PTS_MERCURY.helper

    Public Class View_PaymentRequestApprovalStatus

        Shared Function IfAllApproved(_invoiceId As Integer) As Boolean

            Dim _return As Boolean = False

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If (Aggregate C In db.View_PaymentRequestApprovalStatus Where C.InvoiceID = _invoiceId And C.ApprovedOrNot = 0 Into Count()) = 0 Then
                    _return = True
                End If

            End Using

            Return _return

        End Function

        Shared Function GetUserNamessByInvoiceId(_invoiceid As Integer) As String

            Dim _return As String = ""

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim _count As Integer = (Aggregate C In db.View_PaymentRequestApprovalStatus Where C.InvoiceID = _invoiceid Into Count())
                Dim _name As String = ""
                For i = 0 To _count - 1
                    _name = db.View_PaymentRequestApprovalStatus.Where(Function(e) e.InvoiceID = _invoiceid).ToList()(i).ApprovalRequiredPersons

                    If i = 0 Then
                        _return = db.View_GetFullUserNameFromUserName.Where(Function(e) e.UserName = _name).ToList()(0).UserNameSurnameFromEmail
                    Else
                        _return = _return + ", " + db.View_GetFullUserNameFromUserName.Where(Function(e) e.UserName = _name).ToList()(0).UserNameSurnameFromEmail
                    End If

                Next

            End Using

            Return _return

        End Function

        Shared Function GetEmailsByInvoiceId(_invoiceId As Integer, _mm As MailMessage) As MailMessage

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim _count As Integer = (Aggregate C In db.View_PaymentRequestApprovalStatus Where C.InvoiceID = _invoiceId Into Count())
                For i = 0 To _count - 1
                    _mm.To.Add(db.View_PaymentRequestApprovalStatus.Where(Function(e) e.InvoiceID = _invoiceId).ToList()(i).email)
                Next

            End Using

            Return _mm


        End Function



    End Class

End Namespace

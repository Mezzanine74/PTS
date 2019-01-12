Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

Public Class Table3_Invoice_PDF

Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities



        Shared Function MaxId() As Int32

            Dim _return As Int32 = 0

            If (Aggregate C In db.Table3_Invoice_PDF Into Count()) > 0 Then
                _return = (Aggregate C In db.Table3_Invoice_PDF Into Max(C.InvoiceID))
            End If

            Return _return + 1

        End Function


        Shared Function CountItems() As Integer 

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table3_Invoice_PDF Into Count()

            End Using

            Return _return

        End Function


        Shared Function GetRowByPrimaryKey(_InvoiceID As Int32) As PTS_MERCURY.db.Table3_Invoice_PDF

            Dim _return As PTS_MERCURY.db.Table3_Invoice_PDF

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            If CountItems() > 0 Then

              _return = (From C In db.Table3_Invoice_PDF Where C.InvoiceID = _InvoiceID).ToList()(0)

            Else

               _return = Nothing

            End If

            End Using

            Return _return

        End Function

        Shared Function GetPDFlink(_invoiceid As Integer) As String

            Dim _return As String = ""

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If (Aggregate C In db.Table3_Invoice_PDF Join D In db.Table3_Invoice_PRrequestToPM On C.InvoiceID Equals D.InvoiceID Where C.InvoiceID = _invoiceid Into Count()) > 0 Then
                    _return = (From C In db.Table3_Invoice_PDF Join D In db.Table3_Invoice_PRrequestToPM On C.InvoiceID Equals D.InvoiceID Where C.InvoiceID = _invoiceid).ToList()(0).C.LinkPDF
                End If

            End Using

            Return _return

        End Function

    End Class
End Namespace


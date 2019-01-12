Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

Public Class Table4_PaymentRequest

Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities



        Shared Function MaxId() As Int32

            Dim _return As Int32 = 0

            If (Aggregate C In db.Table4_PaymentRequest Into Count()) > 0 Then
                _return = (Aggregate C In db.Table4_PaymentRequest Into Max(C.PayReqNo))
            End If

            Return _return + 1

        End Function


        Shared Function CountItems() As Integer 

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table4_PaymentRequest Into Count()

            End Using

            Return _return

        End Function


        Shared Function CountItemsByInvoiceID(_InvoiceId As Int32) As Integer

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table4_PaymentRequest Where C.InvoiceID = _InvoiceId Into Count()

            End Using

            Return _return

        End Function

        Shared Function GetRowByPrimaryKeyByInvoiceId(_InvoiceId As Int32) As PTS_MERCURY.db.Table4_PaymentRequest

            Dim _return As PTS_MERCURY.db.Table4_PaymentRequest

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If CountItemsByInvoiceID(_InvoiceId) > 0 Then

                    _return = (From C In db.Table4_PaymentRequest Where C.InvoiceID = _InvoiceId).ToList()(0)

                Else

                    _return = Nothing

                End If

            End Using

            Return _return

        End Function


        Shared Function GetRowByPrimaryKey(_PayReqNo As Int32) As PTS_MERCURY.db.Table4_PaymentRequest

            Dim _return As PTS_MERCURY.db.Table4_PaymentRequest

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If CountItems() > 0 Then

                    _return = (From C In db.Table4_PaymentRequest Where C.PayReqNo = _PayReqNo).ToList()(0)

                Else

                    _return = Nothing

                End If

            End Using

            Return _return

        End Function


        Shared Function GetDropDownListSiteRecordNo() As IQueryable(Of db.Table4_PaymentRequest)

            Return From C In db.Table4_PaymentRequest

        End Function

    End Class
End Namespace


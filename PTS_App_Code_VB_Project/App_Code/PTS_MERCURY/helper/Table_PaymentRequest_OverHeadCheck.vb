Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

Public Class Table_PaymentRequest_OverHeadCheck

Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities



        Shared Function MaxId() As Int32

            Dim _return As Int32 = 0

            If (Aggregate C In db.Table_PaymentRequest_OverHeadCheck Into Count()) > 0 Then
                _return = (Aggregate C In db.Table_PaymentRequest_OverHeadCheck Into Max(C.PayReqNo))
            End If

            Return _return + 1

        End Function


        Shared Function CountItems() As Integer 

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table_PaymentRequest_OverHeadCheck Into Count()

            End Using

            Return _return

        End Function


        Shared Function GetRowByPrimaryKey(_PayReqNo As Int32) As PTS_MERCURY.db.Table_PaymentRequest_OverHeadCheck

            Dim _return As PTS_MERCURY.db.Table_PaymentRequest_OverHeadCheck

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            If CountItems() > 0 Then

              _return = (From C In db.Table_PaymentRequest_OverHeadCheck Where C.PayReqNo = _PayReqNo).ToList()(0)

            Else

               _return = Nothing

            End If

            End Using

            Return _return

        End Function


    End Class
End Namespace


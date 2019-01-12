Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

Public Class Table3_Invoice

Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities



        Shared Function MaxId() As Int32

            Dim _return As Int32 = 0

            If (Aggregate C In db.Table3_Invoice Into Count()) > 0 Then
                _return = (Aggregate C In db.Table3_Invoice Into Max(C.InvoiceID))
            End If

            Return _return + 1

        End Function


        Shared Function CountItems() As Integer 

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table3_Invoice Into Count()

            End Using

            Return _return

        End Function


        Shared Function GetRowByPrimaryKey(_InvoiceID As Int32) As PTS_MERCURY.db.Table3_Invoice

            Dim _return As PTS_MERCURY.db.Table3_Invoice

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            If CountItems() > 0 Then

              _return = (From C In db.Table3_Invoice Where C.InvoiceID = _InvoiceID).ToList()(0)

            Else

               _return = Nothing

            End If

            End Using

            Return _return

        End Function


        Shared Function GetDropDownListInvoice_No() As IQueryable(Of db.Table3_Invoice)

            Return From C In db.Table3_Invoice

        End Function

    End Class
End Namespace


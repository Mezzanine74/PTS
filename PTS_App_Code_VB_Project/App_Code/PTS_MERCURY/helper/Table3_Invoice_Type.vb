Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

    Public Class Table3_Invoice_Type

        Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

        Shared Function GetRowByPrimaryKey(id As Int32) As PTS_MERCURY.db.Table3_Invoice_Type

            Dim _return As PTS_MERCURY.db.Table3_Invoice_Type

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If (Aggregate C In db.Table3_Invoice_Type Where C.Type_id = id Into Count()) > 0 Then

                    _return = (From C In db.Table3_Invoice_Type Where C.Type_id = id).ToList()(0)

                Else

                    _return = Nothing

                End If

            End Using

            Return _return

        End Function


        Shared Function GetDropDownListInvoice_Type() As IQueryable(Of db.Table3_Invoice_Type)

            Return From C In db.Table3_Invoice_Type

        End Function

        Public Class InvoiceType
            Public Property Type_id As Integer
            Public Property Type_name As String
        End Class

        Shared Function GetTypeFromInvoiceId(invoiceid As Integer) As InvoiceType

            Dim _return As New InvoiceType

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim a = (From C In db.Table3_Invoice_Type_Junction Join D In db.Table3_Invoice_Type
                     On C.Type_id Equals D.Type_id Where C.InvoiceID = invoiceid Select D.Type_id, D.Type_name).ToList()

                If a.Count() > 0 Then
                    _return.Type_id = a(0).Type_id
                    _return.Type_name = a(0).Type_name
                Else
                    _return = Nothing
                End If

            End Using

            Return _return

        End Function

    End Class
End Namespace


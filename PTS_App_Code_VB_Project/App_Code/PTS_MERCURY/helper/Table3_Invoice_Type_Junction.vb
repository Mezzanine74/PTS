Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

    Public Class Table3_Invoice_Type_Junction

        Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

        Shared Function GetRowByPrimaryKey(id As Int32) As PTS_MERCURY.db.Table3_Invoice_Type_Junction

            Dim _return As PTS_MERCURY.db.Table3_Invoice_Type_Junction

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If (Aggregate C In db.Table3_Invoice_Type_Junction Where C.InvoiceID = id Into Count()) > 0 Then

                    _return = (From C In db.Table3_Invoice_Type_Junction Where C.InvoiceID = id).ToList()(0)

                Else

                    _return = Nothing

                End If

            End Using

            Return _return

        End Function

        Shared Sub Insert(invoiceid As Integer, type_id As Integer)

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If (Aggregate C In db.Table3_Invoice_Type_Junction Where C.InvoiceID = invoiceid Into Count()) > 0 Then

                    'do nothing

                Else

                    Dim a = New PTS_App_Code_VB_Project.PTS_MERCURY.db.Table3_Invoice_Type_Junction
                    a.InvoiceID = invoiceid
                    a.Type_id = type_id
                    db.Table3_Invoice_Type_Junction.Attach(a)
                    db.Table3_Invoice_Type_Junction.Add(a)
                    db.SaveChanges()

                End If

            End Using

        End Sub

        Shared Sub Delete(invoiceid As Integer)

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If (Aggregate C In db.Table3_Invoice_Type_Junction Where C.InvoiceID = invoiceid Into Count()) > 0 Then

                    Dim a = (From c In db.Table3_Invoice_Type_Junction Where c.InvoiceID = invoiceid).ToList()(0)

                    db.Table3_Invoice_Type_Junction.Remove(a)
                    db.SaveChanges()

                End If

            End Using

        End Sub

        Shared Sub Update(invoiceid As Integer, type_id As Integer)

            Dim _existingtypeid As Integer = 0

            If (Aggregate C In db.Table3_Invoice_Type_Junction Where C.InvoiceID = invoiceid Into Count()) > 0 Then
                _existingtypeid = GetRowByPrimaryKey(invoiceid).Type_id
            End If

            If type_id <> 0 And _existingtypeid <> type_id Then
                Delete(invoiceid)
                Insert(invoiceid, type_id)
            End If

            If type_id = 0 And _existingtypeid > 0 Then
                Delete(invoiceid)
            End If

        End Sub

    End Class
End Namespace


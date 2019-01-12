Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

Public Class Table3_Invoice_AdditionalUserApprovals

Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities



        Shared Function MaxId() As Int32

            Dim _return As Int32 = 0

            If (Aggregate C In db.Table3_Invoice_AdditionalUserApprovals Into Count()) > 0 Then
                _return = (Aggregate C In db.Table3_Invoice_AdditionalUserApprovals Into Max(C.id))
            End If

            Return _return + 1

        End Function


        Shared Function CountItems() As Integer 

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table3_Invoice_AdditionalUserApprovals Into Count()

            End Using

            Return _return

        End Function


        Shared Function GetRowByPrimaryKey(_id As Int32) As PTS_MERCURY.db.Table3_Invoice_AdditionalUserApprovals

            Dim _return As PTS_MERCURY.db.Table3_Invoice_AdditionalUserApprovals

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            If CountItems() > 0 Then

              _return = (From C In db.Table3_Invoice_AdditionalUserApprovals Where C.id = _id).ToList()(0)

            Else

               _return = Nothing

            End If

            End Using

            Return _return

        End Function

        Shared Sub Insert(_InvoiceId As Integer, _username As String)

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim _localtime As DateTime = LocalTime.GetTime()

                Dim A As New db.Table3_Invoice_AdditionalUserApprovals With {.InvoiceId = _InvoiceId, .UserName = _username, .WhenApproved = _localtime}

                db.Table3_Invoice_AdditionalUserApprovals.Attach(A)
                db.Table3_Invoice_AdditionalUserApprovals.Add(A)

                db.SaveChanges()

            End Using

        End Sub

        Shared Sub Delete(_InvoiceId As Integer, _username As String)

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim _localtime As DateTime = LocalTime.GetTime()

                Dim A = (From C In db.Table3_Invoice_AdditionalUserApprovals Where C.InvoiceId = _InvoiceId And C.UserName = _username).ToList()(0)

                db.Table3_Invoice_AdditionalUserApprovals.Attach(A)
                db.Table3_Invoice_AdditionalUserApprovals.Remove(A)

                db.SaveChanges()

            End Using

        End Sub

        Shared Function CountItemsByInvoiceIdUserName(_InvoiceId As Integer, _username As String) As Integer

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table3_Invoice_AdditionalUserApprovals Where C.InvoiceId = _InvoiceId And C.UserName = _username Into Count()

            End Using

            Return _return

        End Function

    End Class
End Namespace


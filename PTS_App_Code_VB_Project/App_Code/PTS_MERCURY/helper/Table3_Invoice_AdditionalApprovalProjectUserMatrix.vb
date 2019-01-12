Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

Public Class Table3_Invoice_AdditionalApprovalProjectUserMatrix

Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities



        Shared Function MaxId() As Int32

            Dim _return As Int32 = 0

            If (Aggregate C In db.Table3_Invoice_AdditionalApprovalProjectUserMatrix Into Count()) > 0 Then
                _return = (Aggregate C In db.Table3_Invoice_AdditionalApprovalProjectUserMatrix Into Max(C.id))
            End If

            Return _return + 1

        End Function


        Shared Function CountItems() As Integer 

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table3_Invoice_AdditionalApprovalProjectUserMatrix Into Count()

            End Using

            Return _return

        End Function


        Shared Function GetRowByPrimaryKey(_id As Int32) As PTS_MERCURY.db.Table3_Invoice_AdditionalApprovalProjectUserMatrix

            Dim _return As PTS_MERCURY.db.Table3_Invoice_AdditionalApprovalProjectUserMatrix

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            If CountItems() > 0 Then

              _return = (From C In db.Table3_Invoice_AdditionalApprovalProjectUserMatrix Where C.id = _id).ToList()(0)

            Else

               _return = Nothing

            End If

            End Using

            Return _return

        End Function


    End Class
End Namespace


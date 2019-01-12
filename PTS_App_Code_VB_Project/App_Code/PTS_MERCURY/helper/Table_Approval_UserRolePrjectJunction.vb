Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

Public Class Table_Approval_UserRolePrjectJunction

Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities



        Shared Function MaxId() As Int32

            Dim _return As Int32 = 0

            If (Aggregate C In db.Table_Approval_UserRolePrjectJunction Into Count()) > 0 Then
                _return = (Aggregate C In db.Table_Approval_UserRolePrjectJunction Into Max(C.id))
            End If

            Return _return + 1

        End Function


        Shared Function CountItems() As Integer 

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table_Approval_UserRolePrjectJunction Into Count()

            End Using

            Return _return

        End Function


        Shared Function GetRowByPrimaryKey(_id As Int32) As PTS_MERCURY.db.Table_Approval_UserRolePrjectJunction

            Dim _return As PTS_MERCURY.db.Table_Approval_UserRolePrjectJunction

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            If CountItems() > 0 Then

              _return = (From C In db.Table_Approval_UserRolePrjectJunction Where C.id = _id).ToList()(0)

            Else

               _return = Nothing

            End If

            End Using

            Return _return

        End Function


    End Class
End Namespace


Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

Public Class Table_ApprUserExcpAccessToActiveUsers

Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities



        Shared Function MaxId() As Int16

            Dim _return As Int16 = 0

            If (Aggregate C In db.Table_ApprUserExcpAccessToActiveUsers Into Count()) > 0 Then
                _return = (Aggregate C In db.Table_ApprUserExcpAccessToActiveUsers Into Max(C.ProjectID))
            End If

            Return _return + 1

        End Function


        Shared Function CountItems() As Integer 

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table_ApprUserExcpAccessToActiveUsers Into Count()

            End Using

            Return _return

        End Function


        Shared Function GetRowByPrimaryKey(_ProjectID As Int16) As PTS_MERCURY.db.Table_ApprUserExcpAccessToActiveUsers

            Dim _return As PTS_MERCURY.db.Table_ApprUserExcpAccessToActiveUsers

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            If CountItems() > 0 Then

              _return = (From C In db.Table_ApprUserExcpAccessToActiveUsers Where C.ProjectID = _ProjectID).ToList()(0)

            Else

               _return = Nothing

            End If

            End Using

            Return _return

        End Function


    End Class
End Namespace


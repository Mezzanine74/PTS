Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

Public Class Table_PO_Akt

Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities



        Shared Function MaxId() As Int32

            Dim _return As Int32 = 0

            If (Aggregate C In db.Table_PO_Akt Into Count()) > 0 Then
                _return = (Aggregate C In db.Table_PO_Akt Into Max(C.ID_Akt))
            End If

            Return _return + 1

        End Function


        Shared Function CountItems() As Integer 

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table_PO_Akt Into Count()

            End Using

            Return _return

        End Function


        Shared Function GetRowByPrimaryKey(_ID_Akt As Int32) As PTS_MERCURY.db.Table_PO_Akt

            Dim _return As PTS_MERCURY.db.Table_PO_Akt

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            If CountItems() > 0 Then

              _return = (From C In db.Table_PO_Akt Where C.ID_Akt = _ID_Akt).ToList()(0)

            Else

               _return = Nothing

            End If

            End Using

            Return _return

        End Function


    End Class
End Namespace


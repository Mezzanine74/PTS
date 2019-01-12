Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

Public Class Table_UserPhotos

Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities




        Shared Function CountItems() As Integer 

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table_UserPhotos Into Count()

            End Using

            Return _return

        End Function


        Shared Function GetRowByPrimaryKey(_UserName As String) As PTS_MERCURY.db.Table_UserPhotos

            Dim _return As PTS_MERCURY.db.Table_UserPhotos

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            If CountItems() > 0 Then

              _return = (From C In db.Table_UserPhotos Where C.UserName = _UserName).ToList()(0)

            Else

               _return = Nothing

            End If

            End Using

            Return _return

        End Function


    End Class
End Namespace


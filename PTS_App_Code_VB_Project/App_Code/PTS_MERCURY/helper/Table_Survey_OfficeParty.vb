Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

Public Class Table_Survey_OfficeParty

Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities



        Shared Function MaxId() As Int32

            Dim _return As Int32 = 0

            If (Aggregate C In db.Table_Survey_OfficeParty Into Count()) > 0 Then
                _return = (Aggregate C In db.Table_Survey_OfficeParty Into Max(C.Id))
            End If

            Return _return + 1

        End Function


        Shared Function CountItems() As Integer 

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table_Survey_OfficeParty Into Count()

            End Using

            Return _return

        End Function


        Shared Function GetRowByPrimaryKey(_Id As Int32) As PTS_MERCURY.db.Table_Survey_OfficeParty

            Dim _return As PTS_MERCURY.db.Table_Survey_OfficeParty

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            If CountItems() > 0 Then

              _return = (From C In db.Table_Survey_OfficeParty Where C.Id = _Id).ToList()(0)

            Else

               _return = Nothing

            End If

            End Using

            Return _return

        End Function


    End Class
End Namespace


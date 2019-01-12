Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

Public Class Table_PO_Nakladnaya

Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities



        Shared Function MaxId() As Int32

            Dim _return As Int32 = 0

            If (Aggregate C In db.Table_PO_Nakladnaya Into Count()) > 0 Then
                _return = (Aggregate C In db.Table_PO_Nakladnaya Into Max(C.ID_Nak))
            End If

            Return _return + 1

        End Function


        Shared Function CountItems() As Integer 

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table_PO_Nakladnaya Into Count()

            End Using

            Return _return

        End Function


        Shared Function GetRowByPrimaryKey(_ID_Nak As Int32) As PTS_MERCURY.db.Table_PO_Nakladnaya

            Dim _return As PTS_MERCURY.db.Table_PO_Nakladnaya

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            If CountItems() > 0 Then

              _return = (From C In db.Table_PO_Nakladnaya Where C.ID_Nak = _ID_Nak).ToList()(0)

            Else

               _return = Nothing

            End If

            End Using

            Return _return

        End Function


        Shared Function GetDropDownListPO_No() As IQueryable(Of db.Table_PO_Nakladnaya)

            Return From C In db.Table_PO_Nakladnaya

        End Function

    End Class
End Namespace


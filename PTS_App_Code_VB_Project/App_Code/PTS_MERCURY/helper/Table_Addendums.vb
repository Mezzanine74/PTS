Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

    Public Class Table_Addendums

        Shared Function CountItems() As Integer

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table_Addendums Into Count()

            End Using

            Return _return

        End Function

        Shared Function CountItemsByAddendumId(_addendumId As Integer) As Integer

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table_Addendums Where C.AddendumID = _addendumId Into Count()

            End Using

            Return _return

        End Function

        Shared Function GetRowByPrimaryKey(_id As Integer) As PTS_MERCURY.db.Table_Addendums

            Dim _return As PTS_MERCURY.db.Table_Addendums

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If CountItemsByAddendumId(_id) > 0 Then

                    _return = (From C In db.Table_Addendums Where C.AddendumID = _id).ToList()(0)

                Else

                    _return = Nothing

                End If

            End Using

            Return _return

        End Function


    End Class

End Namespace

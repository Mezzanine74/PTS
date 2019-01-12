Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

    Public Class Table6_Supplier

        Shared Function CountItems() As Integer

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table6_Supplier Into Count()

            End Using

            Return _return

        End Function

        Shared Function GetRowByPrimaryKey(_id As String) As PTS_MERCURY.db.Table6_Supplier

            Dim _return As PTS_MERCURY.db.Table6_Supplier

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If CountItems() > 0 Then

                    _return = (From C In db.Table6_Supplier Where C.SupplierID = _id).ToList()(0)

                Else

                    _return = Nothing

                End If

            End Using

            Return _return

        End Function

    End Class

End Namespace

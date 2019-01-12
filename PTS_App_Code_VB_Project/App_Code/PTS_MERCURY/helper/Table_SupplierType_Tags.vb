Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

    Public Class Table_SupplierType_Tags

        Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

        Shared Function MaxId() As Int16

            Dim _return As Int16 = 0

            If (Aggregate C In db.Table_SupplierType_Tags Into Count()) > 0 Then
                _return = (Aggregate C In db.Table_SupplierType_Tags Into Max(C.TagId))
            End If

            Return _return + 1

        End Function

        Shared Function CountItemsByTagId(_TagId As Int16) As Integer

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table_SupplierType_Tags Where C.TagId = _TagId Into Count()

            End Using

            Return _return

        End Function

        Shared Function GetRowByPrimaryKey(_TagId As Int16) As PTS_MERCURY.db.Table_SupplierType_Tags

            Dim _return As PTS_MERCURY.db.Table_SupplierType_Tags

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If CountItemsByTagId(_TagId) > 0 Then

                    _return = (From C In db.Table_SupplierType_Tags Where C.TagId = _TagId).ToList()(0)

                Else

                    _return = Nothing

                End If

            End Using

            Return _return

        End Function

    End Class
End Namespace


Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

Public Class Table_SupplierType_Junction

Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities



        Shared Function MaxId() As Int32

            Dim _return As Int32 = 0

            If (Aggregate C In db.Table_SupplierType_Junction Into Count()) > 0 Then
                _return = (Aggregate C In db.Table_SupplierType_Junction Into Max(C.id))
            End If

            Return _return + 1

        End Function


        Shared Function CountItemsByid(_id As Int32) As Integer 

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table_SupplierType_Junction Where C.id = _id Into Count()

            End Using

            Return _return

        End Function


        Shared Function GetRowByPrimaryKey(_id As Int32) As PTS_MERCURY.db.Table_SupplierType_Junction

            Dim _return As PTS_MERCURY.db.Table_SupplierType_Junction

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            If CountItemsByid(_id) > 0 Then

              _return = (From C In db.Table_SupplierType_Junction Where C.id = _id).ToList()(0)

            Else

               _return = Nothing

            End If

            End Using

            Return _return

        End Function


    End Class
End Namespace


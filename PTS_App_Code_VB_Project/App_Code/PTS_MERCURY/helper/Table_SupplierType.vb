Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

Public Class Table_SupplierType

Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities



        Shared Function MaxId() As Int16

            Dim _return As Int16 = 0

            If (Aggregate C In db.Table_SupplierType Into Count()) > 0 Then
                _return = (Aggregate C In db.Table_SupplierType Into Max(C.SupplierTypeId))
            End If

            Return _return + 1

        End Function


        Shared Function CountItemsBySupplierTypeId(_SupplierTypeId As Int16) As Integer 

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table_SupplierType Where C.SupplierTypeId = _SupplierTypeId Into Count()

            End Using

            Return _return

        End Function

        Shared Function CountItemsBySupplierType(_SupplierType As String) As Integer

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table_SupplierType Where C.SupplierType = _SupplierType.Trim() Into Count()

            End Using

            Return _return

        End Function

        Shared Function GetRowByPrimaryKey(_SupplierTypeId As Int16) As PTS_MERCURY.db.Table_SupplierType

            Dim _return As PTS_MERCURY.db.Table_SupplierType

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            If CountItemsBySupplierTypeId(_SupplierTypeId) > 0 Then

              _return = (From C In db.Table_SupplierType Where C.SupplierTypeId = _SupplierTypeId).ToList()(0)

            Else

               _return = Nothing

            End If

            End Using

            Return _return

        End Function

        Shared Function GetTagIdByTag(_tag As String) As Integer

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Try
                    _return = (From C In db.Table_SupplierType Where C.SupplierType = _tag).ToList()(0).SupplierTypeId
                Catch ex As Exception

                End Try

            End Using

            Return _return

        End Function

        Shared Function GetDropDownListSupplierType() As IQueryable(Of db.Table_SupplierType)

            Return From C In db.Table_SupplierType Order By C.SupplierType

        End Function

    End Class
End Namespace


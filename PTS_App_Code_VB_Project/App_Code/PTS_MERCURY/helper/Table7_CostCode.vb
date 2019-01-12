Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

Public Class Table7_CostCode

Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities




        Shared Function CountItemsByCostCode(_CostCode As String) As Integer 

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table7_CostCode Where C.CostCode = _CostCode Into Count()

            End Using

            Return _return

        End Function


        Shared Function GetRowByPrimaryKey(_CostCode As String) As PTS_MERCURY.db.Table7_CostCode

            Dim _return As PTS_MERCURY.db.Table7_CostCode

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            If CountItemsByCostCode(_CostCode) > 0 Then

              _return = (From C In db.Table7_CostCode Where C.CostCode = _CostCode).ToList()(0)

            Else

               _return = Nothing

            End If

            End Using

            Return _return

        End Function

        Public Class Table7_CostCode_For_DDL

            Property CostCode As String
            Property CodeDescription As String

        End Class

        Shared Function GetDropDownListCodeDescription() As IQueryable(Of Table7_CostCode_For_DDL)

            Return (From C In db.Table7_CostCode).Select(Function(e) New Table7_CostCode_For_DDL With {.CostCode = e.CostCode, .CodeDescription = e.CostCode.Trim() + " " + e.CodeDescription.Trim()}).OrderBy(Function(e) e.CodeDescription)


        End Function

    End Class
End Namespace


Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

Public Class Table7_CostDivision2

Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities




        Shared Function CountItemsByCostDivision2ID(_CostDivision2ID As String) As Integer 

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table7_CostDivision2 Where C.CostDivision2ID = _CostDivision2ID Into Count()

            End Using

            Return _return

        End Function


        Shared Function GetRowByPrimaryKey(_CostDivision2ID As String) As PTS_MERCURY.db.Table7_CostDivision2

            Dim _return As PTS_MERCURY.db.Table7_CostDivision2

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            If CountItemsByCostDivision2ID(_CostDivision2ID) > 0 Then

              _return = (From C In db.Table7_CostDivision2 Where C.CostDivision2ID = _CostDivision2ID).ToList()(0)

            Else

               _return = Nothing

            End If

            End Using

            Return _return

        End Function

        Public Class Table7_CostDivision2_For_DDL
            Public Property CostDivision2ID() As String
                Get
                    Return _CostDivision2ID
                End Get
                Set(value As String)
                    _CostDivision2ID = value
                End Set
            End Property
            Private _CostDivision2ID As String

            Public Property CostDivision2Description() As String
                Get
                    Return _CostDivision2Description
                End Get
                Set(value As String)
                    _CostDivision2Description = value
                End Set
            End Property
            Private _CostDivision2Description As String

        End Class

        Shared Function GetDropDownListCostDivision2Description() As IQueryable(Of Table7_CostDivision2_For_DDL)

            Return (From C In db.Table7_CostDivision2).Select(Function(e) New Table7_CostDivision2_For_DDL With {.CostDivision2ID = e.CostDivision2ID, .CostDivision2Description = e.CostDivision2ID + " " + e.CostDivision2Description}).OrderBy(Function(e) e.CostDivision2Description)

        End Function

    End Class
End Namespace


Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

    Public Class Table7_CostDivision

        Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities


        Shared Function CountItemsByCostVidisionID(_CostVidisionID As String) As Integer

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table7_CostDivision Where C.CostVidisionID = _CostVidisionID Into Count()

            End Using

            Return _return

        End Function


        Shared Function GetRowByPrimaryKey(_CostVidisionID As String) As PTS_MERCURY.db.Table7_CostDivision

            Dim _return As PTS_MERCURY.db.Table7_CostDivision

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If CountItemsByCostVidisionID(_CostVidisionID) > 0 Then

                    _return = (From C In db.Table7_CostDivision Where C.CostVidisionID = _CostVidisionID).ToList()(0)

                Else

                    _return = Nothing

                End If

            End Using

            Return _return

        End Function

        Public Class Table7_CostDivision_For_DDL
            Public Property CostVidisionID() As String
                Get
                    Return _CostVidisionID
                End Get
                Set(value As String)
                    _CostVidisionID = value
                End Set
            End Property
            Private _CostVidisionID As String

            Public Property CostDivisionDescription() As String
                Get
                    Return _CostDivisionDescription
                End Get
                Set(value As String)
                    _CostDivisionDescription = value
                End Set
            End Property
            Private _CostDivisionDescription As String

        End Class

        Shared Function GetDropDownListCostDivisionDescription() As IQueryable(Of Table7_CostDivision_For_DDL)

            Return (From C In db.Table7_CostDivision).Select(Function(e) New Table7_CostDivision_For_DDL With {.CostVidisionID = e.CostVidisionID, .CostDivisionDescription = e.CostVidisionID + " " + e.CostDivisionDescription}).OrderBy(Function(e) e.CostDivisionDescription)


        End Function

    End Class
End Namespace


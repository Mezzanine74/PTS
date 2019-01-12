Namespace PTS_MERCURY.helper

    Public Class Table_Budget_PlannedToSpendConstraints

        Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

        Shared Sub Insert(_budgetid As Integer)

            Try
                Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                    Dim _item As New db.Table_Budget_PlannedToSpendConstraints With {
                        .BudgetID = _budgetid,
                        .TimeStamp = DateTime.Now
                        }

                    db.Table_Budget_PlannedToSpendConstraints.Attach(_item)
                    db.Table_Budget_PlannedToSpendConstraints.Add(_item)

                    db.SaveChanges()
                    db.Dispose()

                End Using

            Catch ex As Exception

            End Try

        End Sub

        Shared Sub Delete(_budgetid As Integer)

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim A = (From C In db.Table_Budget_PlannedToSpendConstraints Where C.BudgetID = _budgetid).ToList()(0)

                db.Table_Budget_PlannedToSpendConstraints.Attach(A)
                db.Table_Budget_PlannedToSpendConstraints.Remove(A)
                db.SaveChanges()
                db.Dispose()

            End Using

        End Sub

        Shared Function IfExist(_budgetid As Integer) As Boolean

            Dim _return As Boolean = False

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If (Aggregate C In db.Table_Budget_PlannedToSpendConstraints Where C.BudgetID = _budgetid Into Count()) > 0 Then
                    _return = True
                End If

                db.Dispose()

            End Using

            Return _return

        End Function

    End Class

End Namespace
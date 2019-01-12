Imports Microsoft.VisualBasic
Namespace PTS_MERCURY.helper

    Public Class View_ContractsAddendumsNonExecutedTotal

        Shared Function GetView_ContractsAddendumsNonExecutedTotal(projectid As Int16, costcode As String) As PTS_MERCURY.db.View_ContractsAddendumsNonExecutedTotal

            Dim _return As New PTS_MERCURY.db.View_ContractsAddendumsNonExecutedTotal

            _return = Nothing

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If (Aggregate C In db.View_ContractsAddendumsNonExecutedTotal Where C.ProjectID And C.CostCode = costcode Into Count()) > 0 Then
                    _return = (From C In db.View_ContractsAddendumsNonExecutedTotal Where C.ProjectID = projectid And C.CostCode = costcode).ToList()(0)
                End If

            End Using

            Return _return

        End Function

    End Class

End Namespace

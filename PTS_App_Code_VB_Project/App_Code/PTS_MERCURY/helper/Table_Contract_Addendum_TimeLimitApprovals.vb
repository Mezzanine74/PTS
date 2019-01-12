Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper
    Public Class Table_Contract_Addendum_TimeLimitApprovals

        Shared Function MaxId() As Integer

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = (Aggregate C In db.Table_Contract_Addendum_TimeLimitApprovals Into Max()).Id

            End Using

            Return _return + 1

        End Function

        Shared Sub Insert(_contractid As Integer, _addendumid As Integer, _username As String, _action As String)

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim A As New PTS_MERCURY.db.Table_Contract_Addendum_TimeLimitApprovals With {.Id = MaxId(),
                                                                                          .ContractId = _contractid,
                                                                                          .AddendumId = _addendumid,
                                                                                          .ActionTime = LocalTime.GetTime(),
                                                                                          .UserName = _username,
                                                                                          .Action = _action}

            End Using

        End Sub

    End Class
End Namespace

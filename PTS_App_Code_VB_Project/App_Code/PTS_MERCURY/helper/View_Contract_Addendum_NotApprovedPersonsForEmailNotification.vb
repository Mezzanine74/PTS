Imports Microsoft.VisualBasic
Namespace PTS_MERCURY.helper

    Public Class View_Contract_Addendum_NotApprovedPersonsForEmailNotification

        Shared Function GetUserContractAddendumStatus(_username As String) As PTS_MERCURY.db.View_Contract_Addendum_NotApprovedPersonsForEmailNotification

            Dim _return As PTS_MERCURY.db.View_Contract_Addendum_NotApprovedPersonsForEmailNotification
            _return = Nothing

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If (Aggregate C In db.View_Contract_Addendum_NotApprovedPersonsForEmailNotification Where C.UserName = _username Into Count()) > 0 Then

                    _return = (From C In db.View_Contract_Addendum_NotApprovedPersonsForEmailNotification Where C.UserName = _username).ToList()(0)

                End If

            End Using

            Return _return

        End Function

        Shared Function ReturnMissingApprovalsForUser(_username As String) As String

            Dim _return As String = ""

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim A = db.SP_MissingApprovalsOnContractsAndAddendums(_username).ToList()(0)

                Try
                    Dim ContractCount As Integer = A.Count_Contract
                    Dim AddendumCount As Integer = A.Count_Addendum

                    If ContractCount > 0 And AddendumCount = 0 Then
                        _return = ContractCount.ToString() + " " + BodyTexts.Ref("wG0lVd0vAEO6Q2aeT834fQ")
                    ElseIf ContractCount > 0 And AddendumCount > 0 Then
                        _return = ContractCount.ToString() + " " + BodyTexts.Ref("vKvwOHM6xke5HLiDvO8uRg") + " " + AddendumCount.ToString() + " " + BodyTexts.Ref("3s7gtf9DUEqjyEMk11LNUg")
                    ElseIf ContractCount = 0 And AddendumCount > 0 Then
                        _return = AddendumCount.ToString() + " " + BodyTexts.Ref("qZME3k6pYEOHfW5WwU1wpw")
                    End If

                Catch ex As Exception

                End Try

            End Using

            Return _return

        End Function

    End Class

End Namespace


Partial Class ApprovalMatrixCheckTolerance
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If LocalTime.GetTime.DayOfWeek.ToString <> "Saturday" AndAlso LocalTime.GetTime.DayOfWeek.ToString <> "Sunday" Then
            If LocalTime.GetTime.Hour = 8 Then
                If LocalTime.GetTime.Minute >= 54 AndAlso LocalTime.GetTime.Minute <= 59 Then

                    For i = 0 To PTS_MERCURY.helper.View_CheckTolerance.CountRow() - 1

                        Dim _contractid As Integer = PTS_MERCURY.helper.View_CheckTolerance.GetRows(i).ContractID
                        Dim _addendumid As Integer = PTS_MERCURY.helper.View_CheckTolerance.GetRows(i).AddendumId
                        Dim _username As String = PTS_MERCURY.helper.View_CheckTolerance.GetRows(i).UserName

                        ' Approve on behalf of user
                        If _addendumid = 0 Then
                            ' contract
                            PTS_MERCURY.helper.Table_Contract_UsersApprv.Insert( _
                                _contractid, _
                                _username)

                            PTS_MERCURY.helper.Table_Contract_Addendum_TimeLimitApprovals.Insert(_contractid, _addendumid, _username, "approved")

                        Else
                            ' addendum
                            PTS_MERCURY.helper.Table_Addendum_UsersApprv.Insert( _
                                _addendumid, _
                                _username)

                            PTS_MERCURY.helper.Table_Contract_Addendum_TimeLimitApprovals.Insert(_contractid, _addendumid, _username, "approved")

                        End If

                    Next

                    Dim _count As Integer = 0
                    _count = PTS_MERCURY.helper.View_CheckTolerance.GetView_CheckToleranceGroupBy.ToList().Count()

                    If (_count) > 0 Then

                        For i = 0 To _count - 1

                            PTS_MERCURY.helper.EmailGenerator.AutoApprovalOnApprovalMatrixEmailGenerator.Send(PTS_MERCURY.helper.View_CheckTolerance.GetView_CheckToleranceGroupBy.ToList()(i).ContractId, PTS_MERCURY.helper.View_CheckTolerance.GetView_CheckToleranceGroupBy.ToList()(i).AddendumId)

                        Next

                    End If


                End If
            End If
        End If

    End Sub
End Class

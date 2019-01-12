
Partial Class Pages_PTS_EmailBodies_AutoApprovalByPTSContract
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init

        WebUserControlContractDetails.contractidParameter = PTS_MERCURY.helper.Garbage.GetQueryString(PTS_MERCURY.helper.Garbage.QueryStringParameter.ContractId)

    End Sub
End Class

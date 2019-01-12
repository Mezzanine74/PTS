
Partial Class Pages_PTS_EmailBodies_AutoApprovalByPTSAddendum
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init

        WebUserControlAddendumDetails.addendumidParameter = PTS_MERCURY.helper.Garbage.GetQueryString(PTS_MERCURY.helper.Garbage.QueryStringParameter.AddendumId)

    End Sub

End Class

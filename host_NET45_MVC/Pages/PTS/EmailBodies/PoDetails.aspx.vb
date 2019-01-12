
Partial Class Pages_PTS_EmailBodies_PoDetails
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init

        Dim SqlDataSourcePoToApprove As SqlDataSource = TryCast(POdetailsForEmail.FindControl("SqlDataSourcePoToApprove"), SqlDataSource)
        SqlDataSourcePoToApprove.SelectParameters(PTS_MERCURY.helper.Garbage.QueryStringParameter.PO_No).DefaultValue = PTS_MERCURY.helper.Garbage.GetQueryString(PTS_MERCURY.helper.Garbage.QueryStringParameter.PO_No)

    End Sub
End Class

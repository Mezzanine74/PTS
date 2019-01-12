
Partial Class BalanceOnDifferentCurrencies
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        RenderReport.Render("excel", ReportViewerContractAddendum, "BalanceOnDifferentCurrencies")

    End Sub
End Class

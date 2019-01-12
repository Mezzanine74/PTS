
Partial Class CertificationOverView
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        RenderReport.Render("HTML", ReportViewerCostReport, "CertificationOverView")

    End Sub

    Protected Sub ImageButtonExportExcel_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonExportExcel.Click

        RenderReport.Render("excel", ReportViewerCostReport, "CertificationOverView")

    End Sub

    Protected Sub ImageButtonRefresh_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonRefresh.Click

        RenderReport.Render("HTML", ReportViewerCostReport, "CertificationOverView")

    End Sub

End Class

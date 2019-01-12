
Partial Class PendingSummaryByProject
    Inherits System.Web.UI.Page

  Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    RenderReport.Render("html", ReportViewerFollowUpReportBySupplierExcVAT, "PendingSummaryByProject")
  End Sub
End Class


Partial Class SupplierPoTotalsPerYear
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        RenderReport.Render("HTML", ReportViewerSubPoList, "ReportSupplierBreakdownPerYear")
    End Sub
End Class

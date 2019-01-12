
Partial Class SubPoList
    Inherits System.Web.UI.Page

  Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    RenderReport.Render("HTML", ReportViewerSubPoList, "SubPoReport", "ProjectID", Request.QueryString("ProjectID"))
  End Sub
End Class

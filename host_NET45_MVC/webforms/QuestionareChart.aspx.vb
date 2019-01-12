Imports System.Net
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms
Imports Microsoft.ReportingServices
Imports System.Data.SqlClient

Partial Class QuestionareChart
    Inherits System.Web.UI.Page

  Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    If Not IsPostBack Then
      RenderReport.Render("html", ReportViewerCostReport, "QuestionareChart")
    End If
  End Sub

  Protected Sub ImageButtonRefresh_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonRefresh.Click
    RenderReport.Render("html", ReportViewerCostReport, "QuestionareChart")
  End Sub
End Class

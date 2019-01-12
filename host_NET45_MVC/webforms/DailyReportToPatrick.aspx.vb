Imports System.Net
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms
Imports Microsoft.ReportingServices
Imports System.Data.SqlClient

Partial Class CostReport_RRS_Patrick
  Inherits System.Web.UI.Page

  Protected Sub ImageButtonExportExcel_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonExportExcel.Click
    RenderReport.Render("excel", ReportViewerCostReport, "DailyReportToPatrickRev1")
  End Sub
End Class


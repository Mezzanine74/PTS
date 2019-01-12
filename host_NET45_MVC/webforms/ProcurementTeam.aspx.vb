Imports System.Net
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms
Imports Microsoft.ReportingServices
Imports System.Data.SqlClient

Partial Class ProcurementTeam
    Inherits System.Web.UI.Page

  Public Sub RenderReport_(ByVal _ReportViewerControl As ReportViewer, ByVal _ReportPath As String)

    RenderReport.Render("html", _ReportViewerControl, _ReportPath)

  End Sub

  Public Sub RenderReportToExcel(ByVal _ReportViewerControl As ReportViewer, ByVal _ReportPath As String)

    RenderReport.Render("excel", _ReportViewerControl, _ReportPath)

  End Sub

  Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    If Not IsPostBack Then
      RenderReport_(ReportViewerProcurementReportTotal, "ProcurementReportTotal")
      RenderReport_(ReportViewerProcurementReportByProjects, "ProcurementReportByProjects")
    End If
  End Sub

  Protected Sub ImageButtonExportExcelProcurementReportTotal_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonExportExcelProcurementReportTotal.Click
    RenderReportToExcel(ReportViewerProcurementReportTotal, "ProcurementReportTotal")
  End Sub

  Protected Sub ImageButtonExportExcelProcurementReportByProjects_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonExportExcelProcurementReportByProjects.Click
    RenderReportToExcel(ReportViewerProcurementReportTotal, "ProcurementReportByProjects")
  End Sub

  Protected Sub ImageButtonRefresh_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonRefresh.Click
    RenderReport_(ReportViewerProcurementReportTotal, "ProcurementReportTotal")
    RenderReport_(ReportViewerProcurementReportByProjects, "ProcurementReportByProjects")
  End Sub
End Class

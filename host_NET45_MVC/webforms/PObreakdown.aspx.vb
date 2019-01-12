Imports System.Net
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms
Imports Microsoft.ReportingServices
Imports System.Data.SqlClient

Partial Class PObreakdown
  Inherits System.Web.UI.Page

  Protected Sub ButtonRunReport_Click(sender As Object, e As System.EventArgs) Handles ButtonRunReport.Click
    RenderReport.Render("html", ReportViewerFollowUpReportBySupplierExcVAT, "PObreakdown", "ProjectID", DropDownListPrj.SelectedValue, "Currency", DropDownListCurrency.SelectedValue, "SupplierID", DropDownListSupplier.SelectedValue)
  End Sub

  Protected Sub ImageButtonExportExcelProcurementReportTotal_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonExportExcelProcurementReportTotal.Click
    RenderReport.Render("excel", ReportViewerFollowUpReportBySupplierExcVAT, "PObreakdown", "ProjectID", DropDownListPrj.SelectedValue, "Currency", DropDownListCurrency.SelectedValue, "SupplierID", DropDownListSupplier.SelectedValue)
  End Sub

  Protected Sub ImageButtonRefresh_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonRefresh.Click
    RenderReport.Render("html", ReportViewerFollowUpReportBySupplierExcVAT, "PObreakdown", "ProjectID", DropDownListPrj.SelectedValue, "Currency", DropDownListCurrency.SelectedValue, "SupplierID", DropDownListSupplier.SelectedValue)
  End Sub

  Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    ' it provides query parameter for DropDownListSupplier
    If IsPostBack Or Not IsPostBack Then
      TextBoxUserName.Text = Page.User.Identity.Name
    End If
  End Sub

  Protected Sub DropDownListPrj_DataBound(sender As Object, e As System.EventArgs) Handles DropDownListPrj.DataBound
    If Not IsPostBack Then
      Dim lst1 As New ListItem("Select Project", "0")
      Me.DropDownListPrj.Items.Insert(0, lst1)
    End If
  End Sub

End Class

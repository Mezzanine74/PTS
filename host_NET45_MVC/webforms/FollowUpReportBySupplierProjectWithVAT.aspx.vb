Imports System.Net
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms
Imports Microsoft.ReportingServices
Imports System.Data.SqlClient

Partial Class _Nakl_FollowUpReportBySupplierProjectWithVAT
    Inherits System.Web.UI.Page

    Protected Sub ButtonRunReport_Click(sender As Object, e As System.EventArgs) Handles ButtonRunReport.Click
        RenderReport.Render("html", ReportViewerFollowUpReportBySupplierExcVAT, "_Nakl_FollowUpReportBySupplierBreakdownWithVAT", "SupplierID", DropDownListSupplier.SelectedValue, "Currency", DropDownListCurrency.SelectedValue)
    End Sub

    Protected Sub ImageButtonExportExcelProcurementReportTotal_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonExportExcelProcurementReportTotal.Click
        RenderReport.Render("excel", ReportViewerFollowUpReportBySupplierExcVAT, "_Nakl_FollowUpReportBySupplierBreakdownWithVAT", "SupplierID", DropDownListSupplier.SelectedValue, "Currency", DropDownListCurrency.SelectedValue)
    End Sub

    Protected Sub ImageButtonRefresh_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonRefresh.Click
        RenderReport.Render("html", ReportViewerFollowUpReportBySupplierExcVAT, "_Nakl_FollowUpReportBySupplierBreakdownWithVAT", "SupplierID", DropDownListSupplier.SelectedValue, "Currency", DropDownListCurrency.SelectedValue)
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub DropDownListSupplier_DataBound(sender As Object, e As System.EventArgs) Handles DropDownListSupplier.DataBound
        If Not IsPostBack Then
            Dim lst1 As New ListItem("Select Supplier", "0")
            Me.DropDownListSupplier.Items.Insert(0, lst1)
        End If
    End Sub

End Class

Imports System.Net
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms
Imports Microsoft.ReportingServices
Imports System.Data.SqlClient

Partial Class AdvancePayments
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    ' it provides query parameter for DropDownListSupplier
    If IsPostBack Or Not IsPostBack Then
      TextBoxUserName.Text = Page.User.Identity.Name
    End If
  End Sub


  Protected Sub ButtonRunReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonRunReport.Click
    DropDownListCurrency.DataBind()
    RenderReport.Render("html", ReportViewerCostReport, "AdvancePaymentsDollar", "ProjectID", DropDownListPrj.SelectedValue)
  End Sub

  Protected Sub DropDownListPrj_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.DataBound
    If Not IsPostBack Then
      Dim lst1 As New ListItem("Select Project", "0")
      Me.DropDownListPrj.Items.Insert(0, lst1)
    End If
  End Sub


  Protected Sub DropDownListPrj_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.SelectedIndexChanged

  End Sub

End Class

Imports System.Net
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms
Imports Microsoft.ReportingServices
Imports System.Data.SqlClient

Partial Class FollowUpReport22
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    ' it provides query parameter for DropDownListSupplier
    If IsPostBack Or Not IsPostBack Then
      TextBoxUserName.Text = Page.User.Identity.Name
    End If

  End Sub

  Protected Sub DropDownListPrj_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.DataBound
    If Not IsPostBack Then
      Dim lst1 As New ListItem("Select Project", "0")
      Me.DropDownListPrj.Items.Insert(0, lst1)
    End If
  End Sub

  Public Sub RenderReport_(ByVal _ReportPath As String, ByVal _WhichReport As String)

    If _WhichReport = "today" Then
      RenderReport.Render("html", ReportViewerFollowUp, _ReportPath, "ProjectID", DropDownListPrj.SelectedValue)
    ElseIf _WhichReport = "backup" Then
      RenderReport.Render("html", ReportViewerFollowUp, _ReportPath, "ProjectID", DropDownListPrj.SelectedValue, "BackupDate", DropDownListBackUpDate.SelectedValue.ToString)
    End If

  End Sub

  Protected Sub ButtonRunReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonRunReport.Click

    If DropDownListBackUpDate.SelectedValue = "Today" Then
      If DropDownListCurrency.SelectedValue.ToString = "Dollar" Then
        RenderReport_("FollowUpReport_Dollar_SSRS", "today")
      ElseIf DropDownListCurrency.SelectedValue.ToString = "Euro" Then
        RenderReport_("FollowUpReport_Euro_SSRS", "today")
      ElseIf DropDownListCurrency.SelectedValue.ToString = "Ruble" Then
        RenderReport_("FollowUpReport_Ruble_SSRS", "today")
      End If
    ElseIf DropDownListBackUpDate.SelectedValue <> "Today" Then
      If DropDownListCurrency.SelectedValue.ToString = "Dollar" Then
        RenderReport_("FollowUpReport_Dollar_SSRS_backup", "backup")
      ElseIf DropDownListCurrency.SelectedValue.ToString = "Euro" Then
        RenderReport_("FollowUpReport_Euro_SSRS_backup", "backup")
      ElseIf DropDownListCurrency.SelectedValue.ToString = "Ruble" Then
        RenderReport_("FollowUpReport_Ruble_SSRS_backup", "backup")
      End If
    Else
      ReportViewerFollowUp.Visible = False
    End If

  End Sub

  Protected Sub DropDownListBackUpDate_TextChanged(sender As Object, e As System.EventArgs) Handles DropDownListBackUpDate.TextChanged
  End Sub

  Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
    If IsPostBack Or Not IsPostBack Then
      If ReportViewerFollowUp.Visible = True Then
        LabelVATstatus.Visible = True
        LabelVATstatus.Text = "<div style=" + """" + "background-image: url('images/ExcVAT.png'); background-repeat: repeat-x; height: 15px;" + """" + "></div>"
      Else
        LabelVATstatus.Visible = False
      End If
    End If
  End Sub
End Class

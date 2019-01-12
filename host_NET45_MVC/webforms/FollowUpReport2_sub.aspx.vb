Imports System.Net
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms
Imports Microsoft.ReportingServices
Imports System.Data.SqlClient

Partial Class FollowUpReport2_sub
    Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If IsPostBack OrElse Not IsPostBack Then
      If Request.QueryString("Date") = "Today" Then
        If Request.QueryString("Currency") = "Dollar" Then
          RenderReport_("FollowUpReport_Dollar_SSRS_sub")
        ElseIf Request.QueryString("Currency") = "Euro" Then
          RenderReport_("FollowUpReport_Euro_SSRS_sub")
        ElseIf Request.QueryString("Currency") = "Ruble" Then
          RenderReport_("FollowUpReport_Ruble_SSRS_sub")
        End If
      Else
        If Request.QueryString("Currency") = "Dollar" Then
          RenderReport_("FollowUpReport_Dollar_SSRS_backup_sub")
        ElseIf Request.QueryString("Currency") = "Euro" Then
          RenderReport_("FollowUpReport_Euro_SSRS_backup_sub")
        ElseIf Request.QueryString("Currency") = "Ruble" Then
          RenderReport_("FollowUpReport_Ruble_SSRS_backup_sub")
        End If
      End If
    End If
  End Sub

  Public Sub RenderReport_(ByVal _ReportPath As String)

    If Request.QueryString("Date") = "Today" Then
      RenderReport.Render("html", ReportViewerFollowUp_sub, _ReportPath, "ProjectID", Request.QueryString("ProjectID"), "CostCode", Request.QueryString("CostCode"))
    Else
      RenderReport.Render("html", ReportViewerFollowUp_sub, _ReportPath, "ProjectID", Request.QueryString("ProjectID"), _
                          "CostCode", Request.QueryString("CostCode"), "BackupDate", Request.QueryString("Date"))
    End If
  End Sub

End Class

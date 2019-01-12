Imports PTS_App_Code_VB_Project

Partial Class _1StoPTS_summary
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then

            If Request.QueryString("year") IsNot Nothing Then
                RenderReport.Render("HTML", ReportViewerCostReport, "1S_costs_on_PTS", "Year", Request.QueryString("year"))
            Else
                RenderReport.Render("HTML", ReportViewerCostReport, "1S_costs_on_PTS")
            End If

        End If

    End Sub

End Class

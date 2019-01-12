
Partial Class SalaryBreakdown
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then

            RenderReport.Render("HTML", ReportViewerCostReport, "SalaryBreakDown", "ProjectID", 0, "Year", DropDownListYear.SelectedValue.ToString)

        End If

        If IsPostBack Then

            RenderReport.Render("HTML", ReportViewerCostReport, "SalaryBreakDown", "ProjectID", DropDownListProject.SelectedValue, "Year", DropDownListYear.SelectedValue.ToString)

        End If

    End Sub

    Protected Sub DropDownListProject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownListProject.SelectedIndexChanged

        Page_Load(Nothing, Nothing)

    End Sub

    Protected Sub DropDownListYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownListYear.SelectedIndexChanged

        Page_Load(Nothing, Nothing)

    End Sub

    Protected Sub ImageButtonExportExcel_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButtonExportExcel.Click

        RenderReport.Render("excel", ReportViewerCostReport, "SalaryBreakDown", "ProjectID", DropDownListProject.SelectedValue, "Year", DropDownListYear.SelectedValue.ToString)

    End Sub
End Class

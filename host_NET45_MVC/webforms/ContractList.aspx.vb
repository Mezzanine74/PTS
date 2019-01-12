
Partial Class ContractList
    Inherits System.Web.UI.Page

    Protected Sub DropDownListPrj_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownListPrj.SelectedIndexChanged

        RenderReport.Render("HTML", ReportViewerCostReport, "ContractList", "ProjectID", DropDownListPrj.SelectedValue)

    End Sub
End Class

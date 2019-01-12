
Partial Class PoBreakdownFor1SReconsoliation
    Inherits System.Web.UI.Page

    Protected Sub ButtonRun_Click(sender As Object, e As EventArgs) Handles ButtonRun.Click

        RenderReport.Render("HTML", ReportViewerContractAddendum, "PoBreakdownFor1SReconsoliation", "ProjectID", DropDownListPrj.SelectedValue)

    End Sub

    Protected Sub ButtonExportExcel_Click(sender As Object, e As EventArgs) Handles ButtonExportExcel.Click

        RenderReport.Render("excel", ReportViewerContractAddendum, "PoBreakdownFor1SReconsoliation", "ProjectID", DropDownListPrj.SelectedValue)

    End Sub
End Class

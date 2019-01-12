
Partial Class ContractAddendumPaymentKC
    Inherits System.Web.UI.Page

    Protected Sub DropDownListPrj_SelectedIndexChanged(sender As Object, e As EventArgs)

        RenderReport.Render("html", ReportViewerContractAddendum, "ContractAddendumPaymentKC", "ProjectId", DropDownListPrj.SelectedValue)


    End Sub

    Protected Sub ImageButtonExportExcel_Click(sender As Object, e As ImageClickEventArgs)

        RenderReport.Render("excel", ReportViewerContractAddendum, "ContractAddendumPaymentKC", "ProjectId", DropDownListPrj.SelectedValue, "excel", 1)

    End Sub
End Class

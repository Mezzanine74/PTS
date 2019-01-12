
Partial Class ContractAddendums
    Inherits System.Web.UI.Page

    Protected Sub ButtonRun_Click(sender As Object, e As EventArgs) Handles ButtonRun.Click

        If PTS.CoreTables.CreateDataReader.Create_Table1_Project(DropDownListPrj.SelectedValue).NewPoPolicy = True Then
            RenderReport.Render("HTML", ReportViewerContractAddendum, "ContractAddendumsMainNewPoPolicy", "ProjectID", DropDownListPrj.SelectedValue)
        Else
            RenderReport.Render("HTML", ReportViewerContractAddendum, "ContractAddendumsMain", "ProjectID", DropDownListPrj.SelectedValue)
        End If

    End Sub

    Protected Sub ButtonExportExcel_Click(sender As Object, e As EventArgs) Handles ButtonExportExcel.Click

        If DropDownListPrj.SelectedValue = 210 Then
            RenderReport.Render("excel", ReportViewerContractAddendum, "ContractAddendumsMainNewPoPolicy_IKEA", "ProjectID", DropDownListPrj.SelectedValue)
        Else
            If PTS.CoreTables.CreateDataReader.Create_Table1_Project(DropDownListPrj.SelectedValue).NewPoPolicy = True Then
                RenderReport.Render("excel", ReportViewerContractAddendum, "ContractAddendumsMainNewPoPolicy", "ProjectID", DropDownListPrj.SelectedValue)
            Else
                RenderReport.Render("excel", ReportViewerContractAddendum, "ContractAddendumsMain", "ProjectID", DropDownListPrj.SelectedValue)
            End If
        End If

    End Sub

    Protected Sub DropDownListPrj_SelectedIndexChanged(sender As Object, e As EventArgs)

        ButtonExportExcel_Click(Nothing, Nothing)

    End Sub
End Class

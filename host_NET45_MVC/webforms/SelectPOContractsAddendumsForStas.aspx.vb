
Partial Class SelectPOContractsAddendumsForStas
    Inherits System.Web.UI.Page

    Protected Sub ButtonExcel_Click(sender As Object, e As EventArgs)

        PTSMainClass.ExportGridExcel(GridViewReport, "Report for " + DropDownListPrj.SelectedItem.ToString())

    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Exit Sub
    End Sub

End Class

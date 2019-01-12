
Partial Class PTS_1S
    Inherits System.Web.UI.Page

    Protected Sub PaymentDateTextBox_TextChanged(sender As Object, e As EventArgs) Handles PaymentDateTextBox.TextChanged

        Dim ObjSource As ObjectDataSource = WebUserControl_PTS1Scomparison.FindControl("ObjectDataSourceComparison")
        ObjSource.SelectParameters("CreatedBy").DefaultValue = Convert.ToDateTime(PaymentDateTextBox.Text)

    End Sub

    Private Sub ExportToExcel(ByVal strFileName As String)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.Charset = ""
        Me.EnableViewState = False
        Dim oStringWriter As New System.IO.StringWriter
        Dim oHtmlTextWriter As New System.Web.UI.HtmlTextWriter(oStringWriter)

        Dim GridToRender As GridView = WebUserControl_PTS1Scomparison.FindControl("GridViewComparison")

        GridToRender.RenderControl(oHtmlTextWriter)

        Response.Write(oStringWriter.ToString())
        Response.[End]()

    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Exit Sub
    End Sub

    Protected Sub ImageButtonExcel_Click(sender As Object, e As ImageClickEventArgs)
        ExportToExcel("Report.xls")
    End Sub
End Class

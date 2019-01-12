
Partial Class po
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Request.QueryString("PO") IsNot Nothing Then
            RenderReport.Render("html", ReportViewerPO, "_Nakl_FollowUpReportSubExcVATByPO", "PO_No", Request.QueryString("PO"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        End If

    End Sub
End Class

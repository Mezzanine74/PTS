
Partial Class SearchContractsInspectErrors
    Inherits System.Web.UI.Page

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim hyperlink_ As HyperLink = DirectCast(e.Row.FindControl("HyperlinkInspection"), HyperLink)
            hyperlink_.NavigateUrl = DataBinder.Eval(e.Row.DataItem, "Link").ToString
            hyperlink_.Text = DataBinder.Eval(e.Row.DataItem, "Link").ToString
        End If

    End Sub
End Class

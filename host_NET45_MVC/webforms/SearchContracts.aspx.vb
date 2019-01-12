
Partial Class SearchContracts
    Inherits System.Web.UI.Page

    Protected Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click

        ObjectDataSourceSearch.SelectParameters("SearchMode").DefaultValue = RadioButtonListSearchMode.SelectedValue

        Session("SearchParameter") = TextBoxSearch.Text.Trim

        Using Adapter As New MercuryTableAdapters.Table_SearchHistoryTableAdapter
            Try
                Adapter.Insert(Page.User.Identity.Name.ToString, TextBoxSearch.Text, DateTime.Now)

                Dim a As New MyCommonTasks
                a.SendEmailToAdmin(Page.User.Identity.Name.ToString.Trim + " has searched contract for > " + Mid(TextBoxSearch.Text, 1, 15) + " ...", TextBoxSearch.Text)
                a = Nothing

            Catch ex As Exception

            End Try
        End Using

    End Sub

    Protected Sub RadioButtonListSearchMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadioButtonListSearchMode.SelectedIndexChanged

        ButtonSearch_Click(Nothing, Nothing)

    End Sub

    Protected Sub GridViewSearch_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewSearch.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            ' Column number 4 refers to Contract Description
            e.Row.Cells(4).Text = SearchContract.HighlightSearchParameters(TextBoxSearch.Text, e.Row.Cells(4).Text)

        End If

    End Sub
End Class

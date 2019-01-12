Imports System.Data.SqlClient

Partial Class SearchContractsNotEnteredItems
    Inherits System.Web.UI.Page

    Protected Sub GridViewNotEnteredItems_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridViewNotEnteredItems.RowCommand

        If (e.CommandName = "OpenFile") Then
            Dim LinkToFile As String = e.CommandArgument.ToString
            Dim openpdf As New MyCommonTasks
            openpdf.OpenPDF(LinkToFile)
            openpdf = Nothing
        End If

    End Sub

    Protected Sub GridViewNotEnteredItems_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewNotEnteredItems.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            'it defines type of PDF image if it exist or not.
            PTSMainClass.ProvideImageFromFile(DirectCast(e.Row.FindControl("ImageButtonLink"), ImageButton), DataBinder.Eval(e.Row.DataItem, "LinkToTemplatefile_DOC"))

            Dim HyperlinkFeedLink As HyperLink = DirectCast(e.Row.FindControl("HyperlinkFeedLink"), HyperLink)
            HyperlinkFeedLink.NavigateUrl = "~/webforms/SearchContractsFeed.aspx?ContractID=" + DataBinder.Eval(e.Row.DataItem, "ContractID").ToString

        End If

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        GridViewNotEnteredItems.DataBind()

    End Sub

    Protected Sub ButtonIgnore_Click(sender As Object, e As EventArgs)

        For Each _row As GridViewRow In GridViewNotEnteredItems.Rows

            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = "IF (SELECT COUNT(ContractID) FROM [dbo].[Table_Contracts_IgnoreSearchFeed] WHERE ContractID = @ContractID ) = 0 " + _
                                          " INSERT INTO [dbo].[Table_Contracts_IgnoreSearchFeed] ([ContractID]) VALUES (@ContractID) "
                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.Text

                'syntax for parameter adding
                Dim UserParm As SqlParameter = cmd.Parameters.Add("@ContractID", System.Data.SqlDbType.Int)
                UserParm.Value = _row.Cells(0).Text
                Dim dr As SqlDataReader = cmd.ExecuteReader
                con.Close()
                dr.Close()
            End Using

        Next

        GridViewNotEnteredItems.DataBind()

    End Sub
End Class

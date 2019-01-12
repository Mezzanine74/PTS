Imports System.Data.SqlClient

Partial Class fix_ratesOsman22
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click

    End Sub

    Protected Sub GridViewFixRate_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridViewFixRate.RowCommand

        If (e.CommandName = "ExportToExcel") Then
            Dim ProjectID As Integer = e.CommandArgument

            Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
            RenderReport.Render("disc", ReportViewerFxRate, "BalanceOnDifferentCurrenciesByProject", _
                                "ProjectID", ProjectID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                "DailyReportToPatrickRev/ProjectCurrencyBalance " + UniqueString1 + ".xls")

            Dim path As String = ("~/" + "DailyReportToPatrickRev/ProjectCurrencyBalance " + UniqueString1 + ".xls")

            Dim OpenExcel As New MyCommonTasks
            OpenExcel.OpenPDF(path)
            OpenExcel = Nothing

        End If

    End Sub

    Protected Sub GridViewFixRate_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewFixRate.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            For Each cell As TableCell In e.Row.Cells
                If IsNumeric(cell.Text) Then
                    If Convert.ToDecimal(cell.Text) = 0.0 Then
                        cell.Text = String.Empty
                    End If
                End If
            Next

            Dim HyperLinkBalanceEuro As HyperLink = DirectCast(e.Row.FindControl("HyperLinkBalanceEuro"), HyperLink)
            Dim HyperLinkBalanceDollar As HyperLink = DirectCast(e.Row.FindControl("HyperLinkBalanceDollar"), HyperLink)
            Dim HyperLinkBalanceRuble As HyperLink = DirectCast(e.Row.FindControl("HyperLinkBalanceRuble"), HyperLink)

            If DataBinder.Eval(e.Row.DataItem, "BalanceEuroWithVAT") = 0 Then
                HyperLinkBalanceEuro.Visible = False
            End If

            If DataBinder.Eval(e.Row.DataItem, "BalanceDollarWithVAT") = 0 Then
                HyperLinkBalanceDollar.Visible = False
            End If

            If DataBinder.Eval(e.Row.DataItem, "BalanceRubleWithVAT") = 0 Then
                HyperLinkBalanceRuble.Visible = False
            End If


        End If

    End Sub
End Class

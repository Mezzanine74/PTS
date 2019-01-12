
Partial Class BalanceForeignCurrency
    Inherits System.Web.UI.Page

    Dim _Paid As Decimal = 0
    Dim _Balance As Decimal = 0

    Protected Sub GridViewBalanceBreakdownForeignCurrency_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewBalanceBreakdownForeignCurrency.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "PaidWithVAT")) Then
                _Paid += DataBinder.Eval(e.Row.DataItem, "PaidWithVAT")
            End If

            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "BalanceWithVAT")) Then
                _Balance += DataBinder.Eval(e.Row.DataItem, "BalanceWithVAT")
            End If

        End If

        If e.Row.RowType = DataControlRowType.Footer Then

            Dim LiteralPaidFooter As Literal = DirectCast(e.Row.FindControl("LiteralPaidFooter"), Literal)
            Dim LiteralBalanceFooter As Literal = DirectCast(e.Row.FindControl("LiteralBalanceFooter"), Literal)

            LiteralPaidFooter.Text = String.Format("{0:N2}", _Paid)
            LiteralBalanceFooter.Text = String.Format("{0:N2}", _Balance)

        End If

    End Sub
End Class

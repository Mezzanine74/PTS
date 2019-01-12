
Partial Class WebUserControl_PTS1Scomparison
    Inherits System.Web.UI.UserControl

    Dim PaymentAmount_1S As Decimal = 0.0
    Dim PaymentAmount_PTS As Decimal = 0.0
    Dim SupposedToBePaid As Decimal = 0.0
    Dim DifferenceInRealPayments As Decimal = 0.0

    Protected Sub GridViewComparison_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridViewComparison.RowCommand

        If (e.CommandName = "DeleteFrom1S") Then
            Dim Id As Decimal = e.CommandArgument.ToString

            Using Adapter As New MercuryTableAdapters.Table_Payments_1STableAdapter

                Adapter.DeleteById(Id)
                Adapter.Dispose()
                GridViewComparison.DataBind()
            End Using

        End If

    End Sub


    Protected Sub GridViewComparison_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewComparison.RowDataBound

        Dim LiteralPaymentAmount_1SFooter As Literal = DirectCast(e.Row.FindControl("LiteralPaymentAmount_1SFooter"), Literal)
        Dim LiteralPaymentAmount_PTSFooter As Literal = DirectCast(e.Row.FindControl("LiteralPaymentAmount_PTSFooter"), Literal)
        Dim LiteralSupposedToBePaidFooter As Literal = DirectCast(e.Row.FindControl("LiteralSupposedToBePaidFooter"), Literal)
        Dim LiteralDifferenceInRealPaymentsFooter As Literal = DirectCast(e.Row.FindControl("LiteralDifferenceInRealPaymentsFooter"), Literal)
        Dim LinkButtonDelete As LinkButton = DirectCast(e.Row.FindControl("LinkButtonDelete"), LinkButton)

        If e.Row.RowType = DataControlRowType.DataRow Then

            If Page.User.Identity.Name.ToLower <> "savas" Then
                LinkButtonDelete.Visible = False
            End If

            e.Row.Cells(5).Attributes("style") = "border-left-style: solid;    border-left-width: thick;    border-left-color: #FF0000;"

            ' Highligt all differences
            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "DifferenceInRealPayments")) Then
                If DataBinder.Eval(e.Row.DataItem, "DifferenceInRealPayments") <> 0 Then
                    e.Row.Cells(13).BackColor = System.Drawing.Color.Orange
                End If
            End If

            ' loop all cells then highlight if it is empty
            If DataBinder.Eval(e.Row.DataItem, "MATCH") <> 0 Then
                e.Row.BackColor = System.Drawing.Color.Yellow
            End If

            ' Calculate SUM
            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "PaymentAmount_1S")) Then
                PaymentAmount_1S += DataBinder.Eval(e.Row.DataItem, "PaymentAmount_1S")
            End If

            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "PaymentAmount_PTS")) Then
                PaymentAmount_PTS += DataBinder.Eval(e.Row.DataItem, "PaymentAmount_PTS")
            End If

            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "SupposedToBePaid")) Then
                SupposedToBePaid += DataBinder.Eval(e.Row.DataItem, "SupposedToBePaid")
            End If

            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "DifferenceInRealPayments")) Then
                DifferenceInRealPayments += DataBinder.Eval(e.Row.DataItem, "DifferenceInRealPayments")
            End If

        End If

        ' Assign SUM to footer controls
        If e.Row.RowType = DataControlRowType.Footer Then
            If LiteralPaymentAmount_1SFooter IsNot Nothing Then
                LiteralPaymentAmount_1SFooter.Text = String.Format("{0:N2}", PaymentAmount_1S)
                LiteralPaymentAmount_PTSFooter.Text = String.Format("{0:N2}", PaymentAmount_PTS)
                LiteralSupposedToBePaidFooter.Text = String.Format("{0:N2}", SupposedToBePaid)
                LiteralDifferenceInRealPaymentsFooter.Text = String.Format("{0:N2}", DifferenceInRealPayments)
            End If
        End If

    End Sub

End Class

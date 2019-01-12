
Partial Class FX_Dollar
    Inherits System.Web.UI.Page

    Protected Sub GridViewFxRate_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewFxRate.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Try
                Dim lt As Literal = DirectCast(e.Row.FindControl("LtR"), Literal)
                Dim gainloss As Decimal = Convert.ToDecimal(1959000 / DataBinder.Eval(e.Row.DataItem, "Citibank") - 30000)
                If gainloss > 2000 Then
                    lt.Text = String.Format("{0:N2}", gainloss) + " :)"
                Else
                    lt.Text = String.Format("{0:N2}", gainloss)
                End If

            Catch ex As Exception

            End Try

        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Page.User.Identity.Name.ToLower <> "savas" Then
            GridViewFxRate.Columns(3).Visible = False
        End If

    End Sub
End Class

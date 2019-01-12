
Partial Class BannedPo
    Inherits System.Web.UI.Page

  Protected Sub GridViewBannedPo_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewBannedPo.RowDataBound
    If e.Row.RowType = DataControlRowType.DataRow Then
      If Len(DataBinder.Eval(e.Row.DataItem, "PaymentDate").ToString) = 0 Then
                e.Row.BackColor = System.Drawing.Color.Yellow
                e.Row.Cells(8).Text = "PENDING"
                e.Row.Cells(9).Text = "PENDING"
                e.Row.Cells(8).ForeColor = System.Drawing.Color.Blue
                e.Row.Cells(9).ForeColor = System.Drawing.Color.Blue
                e.Row.Cells(8).Font.Bold = True
                e.Row.Cells(9).Font.Bold = True

            End If

            If DataBinder.Eval(e.Row.DataItem, "Diff") < 0 Then
                e.Row.Cells(11).ForeColor = System.Drawing.Color.Red
                e.Row.Cells(11).BackColor = System.Drawing.Color.MistyRose
            End If

      If DataBinder.Eval(e.Row.DataItem, "Diff") = 0 And _
        Len(DataBinder.Eval(e.Row.DataItem, "PaymentDate").ToString) <> 0 Then
        e.Row.Cells(11).Style.Add("text-align", "center")
        e.Row.Cells(11).Text = "<img src=" + """" + "images/GreenMark.png" + """" + " />"
      End If


    End If
  End Sub
End Class


Partial Class MatchBryanToPo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub GridViewBryanToPo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewBryanToPo.Load

    End Sub

    Protected Sub GridViewBryanToPo_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewBryanToPo.RowCommand

    End Sub

    Protected Sub GridViewBryanToPo_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewBryanToPo.RowDataBound

        Dim labelPoNoItem As Label = DirectCast(e.Row.FindControl("labelPoNoItem"), Label)
        If e.Row.RowType = DataControlRowType.DataRow Then
            If labelPoNoItem IsNot Nothing Then
                If Len(labelPoNoItem.Text) = 0 Then
                    e.Row.BackColor = System.Drawing.Color.LightSalmon
                End If
            End If
        End If


        Dim SqlDataSourcePoNo As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourcePoNo"), SqlDataSource)
        If SqlDataSourcePoNo IsNot Nothing Then
            SqlDataSourcePoNo.SelectParameters("SupplierName").DefaultValue = DataBinder.Eval(e.Row.DataItem, "SupplierName")
        End If

        Dim DropDownListPOnoEdit As DropDownList = DirectCast(e.Row.FindControl("DropDownListPOnoEdit"), DropDownList)
        If DropDownListPOnoEdit IsNot Nothing Then
            If Len(DataBinder.Eval(e.Row.DataItem, "PO_No").ToString) = 0 Then
                'DropDownListPOnoEdit.SelectedValue = ""
            Else
                'DropDownListPOnoEdit.SelectedValue = DataBinder.Eval(e.Row.DataItem, "PO_No")
            End If
        End If

    End Sub

    Protected Sub GridViewBryanToPo_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridViewBryanToPo.RowUpdating
        Dim index As Integer = Convert.ToInt32(e.RowIndex)
        Dim row As GridViewRow = GridViewBryanToPo.Rows(index)
        Dim DropDownListPOnoEdit As DropDownList = DirectCast(row.FindControl("DropDownListPOnoEdit"), DropDownList)
        If Len(DropDownListPOnoEdit.SelectedValue.ToString) = 0 Then
            e.NewValues("PO_No") = Nothing
        Else
            e.NewValues("PO_No") = DropDownListPOnoEdit.SelectedValue.ToString
        End If
    End Sub
End Class

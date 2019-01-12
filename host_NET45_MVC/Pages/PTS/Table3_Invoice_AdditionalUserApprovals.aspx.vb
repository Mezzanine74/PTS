Partial Class Pages_PTS_Table3_Invoice_AdditionalUserApprovals
    Inherits System.Web.UI.Page

    Protected Sub FormviewTable3_Invoice_AdditionalUserApprovals_CallingDataMethods(sender As Object, e As CallingDataMethodsEventArgs)
        e.DataMethodsObject = New PTS_MERCURY.BL.BL()
    End Sub


    Public Function GetDropDownListUserName() As IQueryable(Of PTS_MERCURY.db.aspnet_Users)
        Return PTS_MERCURY.helper.aspnet_Users.GetDropDownListUserName()
    End Function

    Protected Sub FormviewTable3_Invoice_AdditionalUserApprovals_ItemInserting(sender As Object, e As FormViewInsertEventArgs)
        e.Values("UserName") = TryCast(FormviewTable3_Invoice_AdditionalUserApprovals.FindControl("DropDownListUserName"), DropDownList).SelectedValue
    End Sub

    Protected Sub GridviewTable3_Invoice_AdditionalUserApprovals_CallingDataMethods(sender As Object, e As CallingDataMethodsEventArgs)
        e.DataMethodsObject = New PTS_MERCURY.BL.BL()
    End Sub


    Protected Sub GridviewTable3_Invoice_AdditionalUserApprovals_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        Dim _row As GridViewRow = GridviewTable3_Invoice_AdditionalUserApprovals.Rows(e.RowIndex)
        e.NewValues("UserName") = TryCast(_row.FindControl("DropDownListUserName"), DropDownList).SelectedValue
    End Sub


    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        GridViewTable3_Invoice_AdditionalUserApprovals.DataBind()
    End Sub

End Class


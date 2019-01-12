Partial Class Pages_PTS_Table3_Invoice_AdditionalApprovalProjectUserMatrix
    Inherits System.Web.UI.Page

    Protected Sub FormviewTable3_Invoice_AdditionalApprovalProjectUserMatrix_CallingDataMethods(sender As Object, e As CallingDataMethodsEventArgs)
        e.DataMethodsObject = New PTS_MERCURY.BL.BL()
    End Sub

    Public Function GetDropDownListProjectName() As IQueryable(Of PTS_MERCURY.db.Table1_Project)
        Return PTS_MERCURY.helper.Table1_Project.GetDropDownListProjectName()
    End Function


    Public Function GetDropDownListUserName() As IQueryable(Of PTS_MERCURY.db.aspnet_Users)
        Return PTS_MERCURY.helper.aspnet_Users.GetDropDownListUserName()
    End Function

    Protected Sub FormviewTable3_Invoice_AdditionalApprovalProjectUserMatrix_ItemInserting(sender As Object, e As FormViewInsertEventArgs)
        e.Values("ProjectId") = TryCast(FormviewTable3_Invoice_AdditionalApprovalProjectUserMatrix.FindControl("DropDownListProjectName"), DropDownList).SelectedValue
        e.Values("UserName") = TryCast(FormviewTable3_Invoice_AdditionalApprovalProjectUserMatrix.FindControl("DropDownListUserName"), DropDownList).SelectedValue
    End Sub

    Protected Sub GridviewTable3_Invoice_AdditionalApprovalProjectUserMatrix_CallingDataMethods(sender As Object, e As CallingDataMethodsEventArgs)
        e.DataMethodsObject = New PTS_MERCURY.BL.BL()
    End Sub


    Protected Sub GridviewTable3_Invoice_AdditionalApprovalProjectUserMatrix_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        Dim _row As GridViewRow = GridviewTable3_Invoice_AdditionalApprovalProjectUserMatrix.Rows(e.RowIndex)
        e.NewValues("ProjectId") = TryCast(_row.FindControl("DropDownListProjectName"), DropDownList).SelectedValue
        e.NewValues("UserName") = TryCast(_row.FindControl("DropDownListUserName"), DropDownList).SelectedValue
    End Sub


    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        GridViewTable3_Invoice_AdditionalApprovalProjectUserMatrix.DataBind()
    End Sub

End Class


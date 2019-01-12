Partial Class Pages_PTS_Table_Addendum_UsersApprv_IgnoreUser
Inherits System.Web.UI.Page

Protected Sub FormviewTable_Addendum_UsersApprv_IgnoreUser_CallingDataMethods(sender As Object, e As CallingDataMethodsEventArgs)
e.DataMethodsObject = New PTS_MERCURY.BL.BL()
End Sub


Public Function GetDropDownListUserName() As IQueryable(Of PTS_MERCURY.db.aspnet_Users)
Return PTS_MERCURY.helper.aspnet_Users.GetDropDownListUserName()
End Function


Protected Sub GridviewTable_Addendum_UsersApprv_IgnoreUser_CallingDataMethods(sender As Object, e As CallingDataMethodsEventArgs)
        e.DataMethodsObject = New PTS_MERCURY.BL.BL()
End Sub


Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
GridViewTable_Addendum_UsersApprv_IgnoreUser.DataBind()
End Sub

End Class


Partial Class Pages_PTS_Table1_Project
Inherits System.Web.UI.Page

Protected Sub FormviewTable1_Project_CallingDataMethods(sender As Object, e As CallingDataMethodsEventArgs)
        e.DataMethodsObject = New PTS_MERCURY.BL.BL()
End Sub


Protected Sub GridviewTable1_Project_CallingDataMethods(sender As Object, e As CallingDataMethodsEventArgs)
        e.DataMethodsObject = New PTS_MERCURY.BL.BL()
End Sub


Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
GridViewTable1_Project.DataBind()
End Sub

End Class


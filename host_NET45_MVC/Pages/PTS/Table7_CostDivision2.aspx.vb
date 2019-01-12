Partial Class Pages_PTS_Table7_CostDivision2
Inherits System.Web.UI.Page

Protected Sub FormviewTable7_CostDivision2_CallingDataMethods(sender As Object, e As CallingDataMethodsEventArgs)
        e.DataMethodsObject = New PTS_MERCURY.BL.BL()
End Sub


Protected Sub GridviewTable7_CostDivision2_CallingDataMethods(sender As Object, e As CallingDataMethodsEventArgs)
        e.DataMethodsObject = New PTS_MERCURY.BL.BL()
End Sub


Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
GridViewTable7_CostDivision2.DataBind()
End Sub

End Class


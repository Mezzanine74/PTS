Partial Class Pages_PTS_Table7_CostDivision
Inherits System.Web.UI.Page

Protected Sub FormviewTable7_CostDivision_CallingDataMethods(sender As Object, e As CallingDataMethodsEventArgs)
        e.DataMethodsObject = New PTS_MERCURY.BL.BL()
End Sub


    Public Function GetDropDownListCostDivision2Description() As IQueryable(Of PTS_MERCURY.helper.Table7_CostDivision2.Table7_CostDivision2_For_DDL)
        Return PTS_MERCURY.helper.Table7_CostDivision2.GetDropDownListCostDivision2Description()
    End Function


Protected Sub GridviewTable7_CostDivision_CallingDataMethods(sender As Object, e As CallingDataMethodsEventArgs)
        e.DataMethodsObject = New PTS_MERCURY.BL.BL()
End Sub


Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
GridViewTable7_CostDivision.DataBind()
End Sub

End Class


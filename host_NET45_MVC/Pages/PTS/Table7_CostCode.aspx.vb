Partial Class Pages_PTS_Table7_CostCode
    Inherits System.Web.UI.Page

    Protected Sub FormviewTable7_CostCode_CallingDataMethods(sender As Object, e As CallingDataMethodsEventArgs)
        e.DataMethodsObject = New PTS_MERCURY.BL.BL()
    End Sub

    Public Function GetDropDownListCostDivisionDescription() As IQueryable(Of PTS_MERCURY.helper.Table7_CostDivision.Table7_CostDivision_For_DDL)
        Return PTS_MERCURY.helper.Table7_CostDivision.GetDropDownListCostDivisionDescription()
    End Function


    Protected Sub GridviewTable7_CostCode_CallingDataMethods(sender As Object, e As CallingDataMethodsEventArgs)
        e.DataMethodsObject = New PTS_MERCURY.BL.BL()
    End Sub


    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        GridViewTable7_CostCode.DataBind()
    End Sub

End Class


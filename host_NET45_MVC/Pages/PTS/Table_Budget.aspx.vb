Partial Class Pages_PTS_Table_Budget
    Inherits System.Web.UI.Page

    Protected Sub FormviewTable_Budget_CallingDataMethods(sender As Object, e As CallingDataMethodsEventArgs)
        e.DataMethodsObject = New PTS_MERCURY.BL.BL()
    End Sub

    Public Function GetDropDownListDropDownListCurrency() As IQueryable(Of PTS_MERCURY.helper.Currency.Currency_DDL)
        Return PTS_MERCURY.helper.Currency.GetCurrency_DDL
    End Function

    Public Function GetDropDownListProjectName() As IQueryable(Of PTS_MERCURY.db.Table1_Project)
        Return PTS_MERCURY.helper.Table1_Project.GetDropDownListProjectName()
    End Function


    Public Function GetDropDownListCodeDescription() As IQueryable(Of PTS_MERCURY.helper.Table7_CostCode.Table7_CostCode_For_DDL)
        Return PTS_MERCURY.helper.Table7_CostCode.GetDropDownListCodeDescription()
    End Function


    Protected Sub GridviewTable_Budget_CallingDataMethods(sender As Object, e As CallingDataMethodsEventArgs)
        e.DataMethodsObject = New PTS_MERCURY.BL.BL()
    End Sub


    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        ProjectID.Value = CType(DropDownListProjectName.SelectedValue, Integer)
        GridviewTable_Budget.DataBind()
    End Sub

End Class


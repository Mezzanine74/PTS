Imports System.Drawing

Partial Class Admin_Default
    Inherits System.Web.UI.Page

    Protected Sub GridViewProjects_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewProjects.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            ' Providing ProjectParameters
            Dim ObjectDataSourceTable_Approval_UserPositionPrjJunction As ObjectDataSource = DirectCast(e.Row.FindControl("ObjectDataSourceTable_Approval_UserPositionPrjJunction"), ObjectDataSource)
            ObjectDataSourceTable_Approval_UserPositionPrjJunction.SelectParameters("ProjectID").DefaultValue = sender.DataKeys(e.Row.RowIndex).Values(0)

            Dim ObjectDataSourceTable_PersonRequestPo As ObjectDataSource = DirectCast(e.Row.FindControl("ObjectDataSourceTable_PersonRequestPo"), ObjectDataSource)
            ObjectDataSourceTable_PersonRequestPo.SelectParameters("ProjectID").DefaultValue = sender.DataKeys(e.Row.RowIndex).Values(0)

            Dim ObjectDataSourceTable_ContractControlExceptional As ObjectDataSource = DirectCast(e.Row.FindControl("ObjectDataSourceTable_ContractControlExceptional"), ObjectDataSource)
            ObjectDataSourceTable_ContractControlExceptional.SelectParameters("ProjectID").DefaultValue = sender.DataKeys(e.Row.RowIndex).Values(0)

            Dim ObjectDataSourceTable_Approval_Scn_Prj_Users As ObjectDataSource = DirectCast(e.Row.FindControl("ObjectDataSourceTable_Approval_Scn_Prj_Users"), ObjectDataSource)
            ObjectDataSourceTable_Approval_Scn_Prj_Users.SelectParameters("ProjectID").DefaultValue = sender.DataKeys(e.Row.RowIndex).Values(0)

            Dim ObjectDataSourceTable_Approval_UserRolePrjectJunction As ObjectDataSource = DirectCast(e.Row.FindControl("ObjectDataSourceTable_Approval_UserRolePrjectJunction"), ObjectDataSource)
            ObjectDataSourceTable_Approval_UserRolePrjectJunction.SelectParameters("ProjectID").DefaultValue = sender.DataKeys(e.Row.RowIndex).Values(0)

        End If

    End Sub

    Protected Sub FormViewTable_Approval_UserPositionPrjJunction_DataBound(sender As Object, e As EventArgs)

        ' Providing ProjectID for each FormView controls
        Dim FormViewTable_Approval_UserPositionPrjJunction As FormView = sender
        Dim ProjectIDTextBox As TextBox = FormViewTable_Approval_UserPositionPrjJunction.FindControl("ProjectIDTextBox")

        Dim gvrow As GridViewRow = CType(sender, FormView).NamingContainer
        Dim rowindex As Integer = CType(gvrow, GridViewRow).RowIndex

        If ProjectIDTextBox IsNot Nothing Then

            ProjectIDTextBox.Text = GridViewProjects.DataKeys(rowindex).Values(0)

        End If

    End Sub

    Protected Sub FormViewTable_PersonRequestPo_DataBound(sender As Object, e As EventArgs)

        FormViewTable_Approval_UserPositionPrjJunction_DataBound(sender, Nothing)

    End Sub

    Protected Sub FormViewTable_Approval_UserRolePrjectJunction_DataBound(sender As Object, e As EventArgs)

        FormViewTable_Approval_UserPositionPrjJunction_DataBound(sender, Nothing)

    End Sub

    Protected Sub DropDownListPrj_Load(sender As Object, e As EventArgs) Handles DropDownListPrj.Load

        If Not IsPostBack Then
            Dim ddl As DropDownList = sender

            ddl.SelectedIndex = 1

        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If IsPostBack Or Not IsPostBack Then
            If Not Page.User.IsInRole("Admin") Then
                Response.Redirect("~/AccessDenied.aspx")
            End If

        End If

    End Sub
End Class

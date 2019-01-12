Imports System.Data.SqlClient
Imports PTS_App_Code_VB_Project

Partial Class z
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Or Not IsPostBack Then
            If Not Page.User.IsInRole("Admin") Then
                Response.Redirect("~/webforms/AccessDenied.aspx")
            End If

        End If

    End Sub

    Protected Sub ButtonEnterUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonEnterUser.Click
        Dim createStatus As MembershipCreateStatus
        Dim NewUser As MembershipUser = Membership.CreateUser(TextBoxUserName.Text, TextBoxPassword.Text, TextBoxEmailAdress.Text)

        ' update users password
        UpdatePasswordText(TextBoxUserName.Text, TextBoxPassword.Text)

        BindUserControls()

        DropDownListUserToDelete.DataBind()

    End Sub

    Protected Sub UpdatePasswordText(ByVal _UserName As String, ByVal _Password As String)

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " UPDATE [aspnet_Membership] " + _
                    "    SET [password_text] = @Password " + _
                    "  WHERE UserId IN " + _
                    "  ( " + _
                    "  Select UserId from aspnet_Users where UserName = @UserName " + _
                    "  ) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar, 256)
            UserName.Value = _UserName
            Dim Password As SqlParameter = cmd.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar, 256)
            Password.Value = _Password

            Dim dr As SqlDataReader = cmd.ExecuteReader
            con.Close()
            dr.Close()
            con.Dispose()
        End Using

    End Sub

    Protected Sub ButtonDeleteUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDeleteUser.Click
        SqlDataSourceUserToDelete.DeleteCommand = "" +
        " DELETE FROM aspnet_Profile WHERE UserID IN (SELECT [UserId] FROM [dbo].[aspnet_Users] WHERE [UserName] = '" + DropDownListUserToDelete.SelectedValue.ToString + "') " +
        " DELETE FROM aspnet_Membership WHERE UserID IN (SELECT [UserId] FROM [dbo].[aspnet_Users] WHERE [UserName] = '" + DropDownListUserToDelete.SelectedValue.ToString + "') " +
        " DELETE FROM aspnet_UsersInRoles WHERE UserID IN (SELECT [UserId] FROM [dbo].[aspnet_Users] WHERE [UserName] = '" + DropDownListUserToDelete.SelectedValue.ToString + "') " +
        " DELETE FROM aspnet_PersonalizationPerUser WHERE UserID IN (SELECT [UserId] FROM [dbo].[aspnet_Users] WHERE [UserName] = '" + DropDownListUserToDelete.SelectedValue.ToString + "') " +
        " DELETE FROM Table_Prj_User_Junction WHERE UserID IN (SELECT [UserId] FROM [dbo].[aspnet_Users] WHERE [UserName] = '" + DropDownListUserToDelete.SelectedValue.ToString + "') " +
        " DELETE FROM Table_PersonApprovePo WHERE [UserName] = '" + DropDownListUserToDelete.SelectedValue.ToString + "'" +
        " DELETE FROM Table_PersonRequestPo WHERE [UserName] = '" + DropDownListUserToDelete.SelectedValue.ToString + "'" +
        " DELETE FROM Table_ApprovalPayment_Scenario_Users WHERE [UserName] = '" + DropDownListUserToDelete.SelectedValue.ToString + "'" +
        " DELETE FROM Table_Contract_User_Junction WHERE [UserName] = '" + DropDownListUserToDelete.SelectedValue.ToString + "'" +
        " DELETE FROM aspnet_Users WHERE [UserName] = '" + DropDownListUserToDelete.SelectedValue.ToString + "' "
        SqlDataSourceUserToDelete.Delete()
        ' fresh all related controls
        DropDownListUserToDelete.DataBind()

        BindUserControls()

    End Sub

    Protected Sub ButtonGiveFullProjectForRequiredPersons_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonGiveFullProjectForRequiredPersons.Click
        SqlDataSourceCommon.InsertCommand = "" +
          " INSERT INTO [dbo].[Table_Prj_User_Junction] " +
          " ([ProjectID] " +
          " ,[UserID]) " +
          "  " +
          "  (SELECT " +
          "  ProjectID " +
          "  ,UserId  " +
          "  FROM " +
          "  dbo.ViewUserProjectCombination2 " +
          "  WHERE     (UserName IN (SELECT     TOP (100) PERCENT RTRIM(dbo.aspnet_Users.UserName) AS UserName " +
          " FROM         dbo.Table_Prj_User_Junction INNER JOIN " +
          "                       dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId " +
          " GROUP BY RTRIM(dbo.aspnet_Users.UserName) " +
          " HAVING      (COUNT(dbo.Table_Prj_User_Junction.UserID) > 0.9 * " +
          "                           (SELECT     COUNT(ProjectID) AS CountOfPrj " +
          "                             FROM          dbo.Table1_Project)) " +
          " ORDER BY COUNT(dbo.Table_Prj_User_Junction.UserID) DESC)) AND (ProjectID NOT IN (777, 778, 34, 999))) "
        SqlDataSourceCommon.Insert()
        ' fresh all related controls
        DropDownListUserToDelete.DataBind()

        BindUserControls()

    End Sub

    Protected Sub ButtonGiveFullAccessForSpecificUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonGiveFullAccessForSpecificUser.Click
        SqlDataSourceCommon.InsertCommand = "" +
        " INSERT INTO [dbo].[Table_Prj_User_Junction] " +
        " ([ProjectID] " +
        " ,[UserID]) " +
        "  " +
        "  (SELECT " +
        "  ProjectID " +
        "  ,UserId  " +
        "  FROM " +
        "  dbo.ViewUserProjectCombination2 " +
        "  WHERE     (UserName = '" + DropDownListUserToDelete.SelectedValue.ToString + "') AND (ProjectID NOT IN (777, 778, 34, 999))) "
        SqlDataSourceCommon.Insert()
        ' fresh all related controls
        DropDownListUserToDelete.DataBind()

        BindUserControls()

    End Sub

    Protected Sub ButtonDeleteProjectsFromUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDeleteProjectsFromUser.Click
        SqlDataSourceCommon.DeleteCommand = "" +
          " DELETE FROM [dbo].[Table_Prj_User_Junction] " +
          " WHERE [UserID] = (SELECT UserId " +
          " FROM         dbo.aspnet_Users " +
          " WHERE     (RTRIM(UserName) = '" + DropDownListUserToDelete.SelectedValue.ToString + "')) "
        SqlDataSourceCommon.Delete()
        ' fresh all related controls
        DropDownListUserToDelete.DataBind()

        BindUserControls()

    End Sub

    Protected Sub DropDownListUserToDelete_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListUserToDelete.SelectedIndexChanged
        ' fresh all related controls
        Dim dl As DropDownList = sender

        BindUserControls()

        'LabelPass.Text = PTS_MERCURY.db.QuickTables.aspnet_Membership(PTS_MERCURY.db.QuickTables.aspnet_Users(dl.SelectedValue.Trim).UserId).password_text

        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            Try
                Dim A = (From C In db.aspnet_Membership Join D In db.aspnet_Users On C.UserId Equals D.UserId Where D.UserName = dl.SelectedValue.Trim Select New With {C.password_text}).ToList()(0).password_text.Trim()
                LabelPass.Text = A

            Catch ex As Exception
                LabelPass.Text = String.Empty
            End Try

        End Using




    End Sub

    Protected Sub BindUserControls()

        DataListRoles.DataBind()
        DataListProject.DataBind()

    End Sub

    Protected Sub ButtonChangePassword_Click(sender As Object, e As EventArgs) Handles ButtonChangePassword.Click

        Dim username As String = TextBoxUserNamePAsswordChange.Text.Trim
        Dim password As String = TextBoxNewPassword.Text.Trim
        Dim mu As MembershipUser = Membership.GetUser(username)

        mu.UnlockUser()
        Membership.UpdateUser(mu)

        mu.ChangePassword(mu.ResetPassword(), password)

        ' update PasswordText in database
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String =
                " update aspnet_Membership " +
                " set password_text = @Password " +
                " WHERE UserId =  " +
                " ( " +
                " SELECT UserId from aspnet_Users WHERE username = @UserName " +
                " ) "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim _UserName As SqlParameter = cmd.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar, 256)
            _UserName.Value = username
            Dim _Password As SqlParameter = cmd.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar, 256)
            _Password.Value = password

            Dim dr As SqlDataReader = cmd.ExecuteReader
            con.Close()
            dr.Close()
        End Using

        BindUserControls()

    End Sub

    Protected Sub DataListRoles_ItemDataBound(sender As Object, e As DataListItemEventArgs)

        If e.Item.ItemType = ListItemType.Item _
          OrElse e.Item.ItemType = ListItemType.AlternatingItem _
          OrElse e.Item.ItemType = ListItemType.Separator Then

            Dim CheckBoxUserRole As CheckBox = DirectCast(e.Item.FindControl("CheckBoxUserRole"), CheckBox)
            Dim LabelRoleName As Label = DirectCast(e.Item.FindControl("LabelRoleName"), Label)

            Dim _Role As String = DataBinder.Eval(e.Item.DataItem, "RoleName").ToString()

            If Roles.IsUserInRole(DropDownListUserToDelete.SelectedItem.ToString, _Role) Then

                CheckBoxUserRole.Checked = True
                CheckBoxUserRole.BorderStyle = BorderStyle.Solid
                CheckBoxUserRole.BorderWidth = 3

                'LabelRoleName.CssClass = "label label-lg label-success arrowed-in arrowed-in-right"
                LabelRoleName.Font.Bold = True

            Else

                CheckBoxUserRole.Checked = False

            End If

        End If

    End Sub

    Protected Sub DataListProject_ItemDataBound(sender As Object, e As DataListItemEventArgs)

        If e.Item.ItemType = ListItemType.Item _
          OrElse e.Item.ItemType = ListItemType.AlternatingItem _
          OrElse e.Item.ItemType = ListItemType.Separator Then

            Dim CheckBoxUserProject As CheckBox = DirectCast(e.Item.FindControl("CheckBoxUserProject"), CheckBox)
            Dim LabelProject As Label = DirectCast(e.Item.FindControl("LabelProject"), Label)
            Dim _ProjectID As String = DataBinder.Eval(e.Item.DataItem, "ProjectID").ToString()

            If AdminPTS.IsUserInProject(DropDownListUserToDelete.SelectedItem.ToString, _ProjectID) Then

                CheckBoxUserProject.Checked = True
                CheckBoxUserProject.BorderStyle = BorderStyle.Solid
                CheckBoxUserProject.BorderWidth = 3

                'LabelProject.CssClass = "label label-lg label-success arrowed-in arrowed-in-right"
                LabelProject.Font.Bold = True

            Else

                CheckBoxUserProject.Checked = False

            End If

        End If

    End Sub

    Protected Sub ButtonProcessRolesProjectsForUser_Click(sender As Object, e As EventArgs) Handles ButtonProcessRolesProjectsForUser.Click

        ' Process Roles
        For Each Item As DataListItem In DataListRoles.Items

            Dim CheckBoxUserRole As CheckBox = DirectCast(Item.FindControl("CheckBoxUserRole"), CheckBox)
            Dim LabelRoleName As Label = DirectCast(Item.FindControl("LabelRoleName"), Label)

            If CheckBoxUserRole.Checked = True Then

                If Not Roles.IsUserInRole(DropDownListUserToDelete.SelectedItem.ToString, LabelRoleName.Text) Then
                    Roles.AddUserToRole(DropDownListUserToDelete.SelectedItem.ToString, LabelRoleName.Text)
                End If

            End If

            If CheckBoxUserRole.Checked = False Then

                If Roles.IsUserInRole(DropDownListUserToDelete.SelectedItem.ToString, LabelRoleName.Text) Then
                    Roles.RemoveUserFromRole(DropDownListUserToDelete.SelectedItem.ToString, LabelRoleName.Text)
                End If

            End If

        Next
        ' END Process Roles

        ' Process Projects
        For Each Item As DataListItem In DataListProject.Items

            Dim CheckBoxUserProject As CheckBox = DirectCast(Item.FindControl("CheckBoxUserProject"), CheckBox)
            Dim LabelProjectID As Label = DirectCast(Item.FindControl("LabelProjectID"), Label)

            If CheckBoxUserProject.Checked = True Then

                If Not AdminPTS.IsUserInProject(DropDownListUserToDelete.SelectedItem.ToString, LabelProjectID.Text) Then
                    AdminPTS.AddUserToProject(DropDownListUserToDelete.SelectedItem.ToString, LabelProjectID.Text)
                End If

            End If

            If CheckBoxUserProject.Checked = False Then

                If AdminPTS.IsUserInProject(DropDownListUserToDelete.SelectedItem.ToString, LabelProjectID.Text) Then
                    AdminPTS.RemoveUserFromProject(DropDownListUserToDelete.SelectedItem.ToString, LabelProjectID.Text)
                End If

            End If

        Next

        BindUserControls()

    End Sub

    Protected Sub DropDownListUserToDelete_SelectedIndexChanged1(sender As Object, e As EventArgs)

        Dim ddl As DropDownList = sender

        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            Dim user = From C In db.aspnet_Users Join D In db.aspnet_Membership On C.UserId Equals D.UserId Where C.UserName = ddl.SelectedValue
                    Select New With {C.UserName, D.LoweredEmail}

            LabelUserName.Text = user.ToList()(0).UserName
            LabelEmail.Text = user.ToList()(0).LoweredEmail

            db.Dispose()

        End Using

    End Sub

    Protected Sub ButtonCreateRole_Click(sender As Object, e As EventArgs)

        Roles.CreateRole(TextBoxCreateRole.Text.Trim.ToLower)

        BindUserControls()

    End Sub
End Class

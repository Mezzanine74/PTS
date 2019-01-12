Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.IO
Imports PTS_App_Code_VB_Project.PTS.CoreTables
Imports PTS_App_Code_VB_Project.PTS_MERCURY.helper.EmailGenerator

Partial Class poApprovalFrame
    Inherits System.Web.UI.Page

    Dim Notification As New _GiveNotification

    Protected Sub GridViewFrameApproval_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewFrameApproval.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim SqlDataSourceFrameApproval As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourceFrameApproval"), SqlDataSource)
            If SqlDataSourceFrameApproval IsNot Nothing Then
                SqlDataSourceFrameApproval.SelectParameters("PO_No").DefaultValue = DataBinder.Eval(e.Row.DataItem, "PO_No").ToString
            End If

            Dim DataListFrameApproval As DataList = DirectCast(e.Row.FindControl("DataListFrameApproval"), DataList)
            If CreateDataReader.Create_aspnet_Users(Page.User.Identity.Name.ToLower).ApproveFramePo = False Then
                DataListFrameApproval.Enabled = False
            ElseIf CreateDataReader.Create_aspnet_Users(Page.User.Identity.Name.ToLower).ApproveFramePo = True Then
                DataListFrameApproval.Enabled = True
            End If

            If Page.User.Identity.Name.ToLower = "savas" Then
                DataListFrameApproval.Enabled = True
            End If

        End If
    End Sub

    Protected Sub DataListFrameApproval_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataListItemEventArgs)

        Dim ImageButtonApproval As ImageButton = DirectCast(e.Item.FindControl("ImageButtonApproval"), ImageButton)

        If ImageButtonApproval IsNot Nothing Then
            ' Define imageURL as per approval status
            If DataBinder.Eval(e.Item.DataItem, "Approval") = 0 Then
                ImageButtonApproval.ImageUrl = "~/images/ContractNotApprove.png"
            ElseIf DataBinder.Eval(e.Item.DataItem, "Approval") = 1 Then
                ImageButtonApproval.ImageUrl = "~/images/ContractApprove.png"
            End If

            If DataBinder.Eval(e.Item.DataItem, "UserName").ToString.ToLower = Page.User.Identity.Name.ToString.ToLower OrElse
                Page.User.Identity.Name.ToLower = "savas" Then
                ImageButtonApproval.Enabled = True
            Else
                ImageButtonApproval.Enabled = False
            End If

        End If

    End Sub

    Protected Sub DataListFrameApproval_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataListCommandEventArgs)

        If (e.CommandName = "Approval") Then
            Dim ItemIndex As String = e.Item.ItemIndex.ToString

            Dim datalistframeapproval As DataList = source
            Dim item As DataListItem = datalistframeapproval.Items(ItemIndex)

            Dim LiteralUserName As Literal = DirectCast(item.FindControl("LiteralUserName"), Literal)
            Dim LiteralPONo As Literal = DirectCast(item.FindControl("LiteralPONo"), Literal)
            Dim LiteralApproval As Literal = DirectCast(item.FindControl("LiteralApproval"), Literal)

            ' It is approval or NOT approval ?
            If LiteralApproval.Text = "0" Then
                ' APPROVE
                If Page.User.Identity.Name.ToLower = "savas" Then
                    ApproveFramePo(LiteralPONo.Text, "patrick")
                Else
                    ApproveFramePo(LiteralPONo.Text, LiteralUserName.Text)
                End If

                ' Check If all user approved
                If AllUsersApproved(LiteralPONo.Text) Then
                    ' Approve PO and Send Email Notification
                    ApprovePO(LiteralPONo.Text)
                    SendEmailNotification(LiteralPONo.Text)
                End If
            ElseIf LiteralApproval.Text = "1" Then
                ' NOT APPROVE
                RemoveFramePo(LiteralPONo.Text, LiteralUserName.Text)
            End If

            GridViewFrameApproval.DataBind()

        End If
    End Sub

    Protected Sub SendEmailNotification(ByVal _PO_No As String)

        'send email of grid to required person
        Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "PO Approval")
        Dim mm1 As New MailMessage()
        mm1.From = MailFrom

        ' ADD AUTHORIZED PERSON
        Using con22 As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con22.Open()
            Dim sqlstring22 As String = _
                        " SELECT Email FROM(  " + _
                        " SELECT     RTRIM(dbo.Table_PersonRequestPo.Email) AS Email    " + _
                        " FROM         dbo.Table2_PONo INNER JOIN    " + _
                        "                       dbo.Table_PersonRequestPo ON dbo.Table2_PONo.RequestedBy = dbo.Table_PersonRequestPo.UserName    " + _
                        " WHERE     (dbo.Table2_PONo.PO_No = @PO_No)    " + _
                        "     " + _
                        " UNION ALL    " + _
                        "   " + _
                        " SELECT     RTRIM(dbo.aspnet_Membership.LoweredEmail) AS LoweredEmail " + _
                        " FROM         dbo.Table_Approval_UserRolePrjectJunction INNER JOIN " + _
                        " 					  dbo.Table2_PONo ON dbo.Table_Approval_UserRolePrjectJunction.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN " + _
                        " 					  dbo.aspnet_Users INNER JOIN " + _
                        " 					  dbo.aspnet_Membership ON dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId AND dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId ON  " + _
                        " 					  dbo.Table_Approval_UserRolePrjectJunction.UserName = dbo.aspnet_Users.UserName " + _
                        " WHERE     (dbo.Table2_PONo.PO_No = @PO_No) AND (dbo.Table_Approval_UserRolePrjectJunction.RoleName = N'InitiateContractAndAddendum') " + _
                        "     " + _
                        " UNION ALL    " + _
                        "     " + _
                        " SELECT     RTRIM(dbo.aspnet_Membership.Email) AS Email    " + _
                        " FROM         dbo.aspnet_Membership INNER JOIN    " + _
                        "                       dbo.aspnet_Users ON dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId AND     " + _
                        "                       dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId INNER JOIN    " + _
                        "                       dbo.Table2_PONo ON dbo.aspnet_Users.UserName = dbo.Table2_PONo.PersonCreated    " + _
                        " WHERE     (dbo.Table2_PONo.PO_No = @PO_No)    " + _
                        "    " + _
                        " UNION ALL   " + _
                        "    " + _
                        " SELECT     RTRIM(dbo.aspnet_Membership.LoweredEmail) AS LoweredEmail " + _
                        " FROM         dbo.aspnet_Users INNER JOIN " + _
                        " 					  dbo.aspnet_Membership ON dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId AND dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId INNER JOIN " + _
                        " 					  dbo.Table_Approval_UserPositionPrjJunction ON dbo.aspnet_Users.UserName = dbo.Table_Approval_UserPositionPrjJunction.UserName INNER JOIN " + _
                        " 					  dbo.Table_Approval_PositionEmployee ON dbo.Table_Approval_UserPositionPrjJunction.PositionID = dbo.Table_Approval_PositionEmployee.PositionID INNER JOIN " + _
                        " 					  dbo.Table2_PONo ON dbo.Table_Approval_UserPositionPrjJunction.ProjectID = dbo.Table2_PONo.Project_ID " + _
                        " WHERE     (dbo.Table_Approval_PositionEmployee.PositionName = N'Cost Controller') AND (dbo.Table2_PONo.PO_No = @PO_No) " + _
                        " ) AS DataSource1    " + _
                        " GROUP BY Email    "

            Dim cmd22 As New SqlCommand(sqlstring22, con22)
            cmd22.CommandType = System.Data.CommandType.Text
            Dim PO_No__ As SqlParameter = cmd22.Parameters.Add("@PO_No", System.Data.SqlDbType.NVarChar, 20)
            PO_No__.Value = _PO_No
            Dim dr22 As SqlDataReader = cmd22.ExecuteReader
            'Dim osman As String = ""
            While dr22.Read
                If dr22(0).ToString.Length <> 0 Then
                    mm1.To.Add(dr22(0).ToString)
                    'osman = osman + dr22(0).ToString
                End If
            End While

            ' Remove this after implementing general notification matrix
            Using context As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities
                Dim _TablePO = (From C In context.Table2_PONo Where C.PO_No = _PO_No).ToList()(0)
                If _TablePO.Project_ID = 197 Or _TablePO.Project_ID = 195 Then
                    mm1.To.Add("anton.mokrov@mercuryeng.ru")
                End If
                context.Dispose()
            End Using
            ' Remove this after implementing general notification matrix

            mm1.Bcc.Add("sk@mercuryeng.ru")
            dr22.Close()
            con22.Close()
            con22.Dispose()
        End Using

        ' Produce PO details for email body
        Dim _EmailBodyForPoDetails As String = ""
        _EmailBodyForPoDetails = ContractView.ProduceEmailBodyForPoDetails(_PO_No, POdetailsForEmail)

        Dim ProjectID As String = Convert.ToString(Convert.ToInt16(Mid(_PO_No, 4, 3)))

        mm1.Subject = _PO_No + " был одобрен"
        mm1.Body = "<h3><span style=" + """" + "color: #009900" + """" + ">Утверждено руководством" + "</span></h3>" +
          " <a href=" + """" + "http://pts.mercuryeng.ru/webforms/invoicedefine.aspx?PoNo=" + _PO_No + "&ProjectID=" + ProjectID + "&PO_link=" + """" + " target=" + """" + "_blank" + """" + ">ССЫЛКА ДЛЯ ОПРЕДЕЛЕНИЯ СЧЕТА</a> " +
          _EmailBodyForPoDetails
        mm1.IsBodyHtml = True

        Dim smtp As New SmtpClient_RussianEncoded

        Try
            smtp.Send(mm1)
        Catch ex As Exception
        End Try


    End Sub

    Protected Sub ApprovePO(ByVal _PO_No As String)
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " UPDATE [dbo].[Table2_PONo] SET [Approved] = 1, PersonApproved = N'Managemenet' WHERE PO_No = @PO_No "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim PO_No As SqlParameter = cmd.Parameters.Add("@PO_No", System.Data.SqlDbType.NVarChar)
            PO_No.Value = Convert.ToString(_PO_No)

            Dim dr As SqlDataReader = cmd.ExecuteReader

            Notification._GiveNotification(Page.Request.Headers("X-Requested-With") _
                                           , Page,
                                           "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">" + _PO_No + " has been approved!</p>")

            con.Close()
            dr.Close()
        End Using
    End Sub

    Protected Function AllUsersApproved(ByVal _PO_No As String) As Boolean
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT     COUNT(Approval) AS Count " +
      " FROM         (SELECT     CASE WHEN Table_PO_FrContApprovalUsers.UserName IS NULL THEN 0 ELSE 1 END AS Approval, FramContractPOApprovalUsers.PO_No " +
      "                        FROM          (SELECT     RTRIM(dbo.Table2_PONo.PO_No) AS PO_No, RTRIM(dbo.aspnet_Users.UserName) AS UserName " +
      "                                                FROM          dbo.aspnet_Users CROSS JOIN " +
      "                                                                       dbo.Table2_PONo " +
      "                                                WHERE      (dbo.Table2_PONo.FrameContractPO = 1) AND (dbo.Table2_PONo.Approved <> 1) AND (dbo.aspnet_Users.ApproveFramePo = 1))  " +
      "                                               AS FramContractPOApprovalUsers LEFT OUTER JOIN " +
      "                                               dbo.Table_PO_FrContApprovalUsers ON FramContractPOApprovalUsers.PO_No = dbo.Table_PO_FrContApprovalUsers.PO_No AND  " +
      "                                               FramContractPOApprovalUsers.UserName = dbo.Table_PO_FrContApprovalUsers.UserName " +
      "                        WHERE      (FramContractPOApprovalUsers.PO_No = @PO_No) AND (CASE WHEN Table_PO_FrContApprovalUsers.UserName IS NULL THEN 0 ELSE 1 END = 0)) " +
      "                        AS DataSource "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim PO_No As SqlParameter = cmd.Parameters.Add("@PO_No", System.Data.SqlDbType.NVarChar)
            PO_No.Value = Convert.ToString(_PO_No)

            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim _return As Boolean = False
            While dr.Read
                If dr(0) = 0 Then
                    _return = True
                End If
            End While
            Return _return
            con.Close()
            dr.Close()
        End Using
    End Function

    Protected Sub ApproveFramePo(ByVal _PO_No As String, ByVal _UserName As String)
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " DELETE FROM [dbo].[Table_PO_FrContApprovalUsers] WHERE PO_No= @PO_No AND UserName= @UserName " +
                                  " INSERT INTO [Table_PO_FrContApprovalUsers] " +
                                  "            ([PO_No] " +
                                  "            ,[UserName] " +
                                  "            ,[WhenApproved]) " +
                                  "      VALUES " +
                                  "            (@PO_No " +
                                  "            ,@UserName " +
                                  "            ,@WhenApproved) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim PO_No As SqlParameter = cmd.Parameters.Add("@PO_No", System.Data.SqlDbType.NVarChar)
            PO_No.Value = Convert.ToString(_PO_No)
            Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar)
            UserName.Value = _UserName
            Dim WhenApproved As SqlParameter = cmd.Parameters.Add("@WhenApproved", System.Data.SqlDbType.SmallDateTime)
            WhenApproved.Value = LocalTime.GetTime

            Dim dr As SqlDataReader = cmd.ExecuteReader
            con.Close()
            dr.Close()
        End Using
    End Sub

    Protected Sub RemoveFramePo(ByVal _PO_No As String, ByVal _UserName As String)
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " DELETE FROM [dbo].[Table_PO_FrContApprovalUsers] WHERE PO_No= @PO_No AND UserName= @UserName "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim PO_No As SqlParameter = cmd.Parameters.Add("@PO_No", System.Data.SqlDbType.NVarChar)
            PO_No.Value = Convert.ToString(_PO_No)
            Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar)
            UserName.Value = _UserName
            Dim WhenApproved As SqlParameter = cmd.Parameters.Add("@WhenApproved", System.Data.SqlDbType.SmallDateTime)
            WhenApproved.Value = LocalTime.GetTime

            Dim dr As SqlDataReader = cmd.ExecuteReader
            con.Close()
            dr.Close()
        End Using
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Exit Sub
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        SqlDataSourceApprovaFrameContract.SelectParameters("username").DefaultValue = Page.User.Identity.Name.ToLower

    End Sub
End Class

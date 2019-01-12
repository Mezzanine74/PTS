Imports System.Data.SqlClient
Partial Class NotificationsZOO
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    If Not IsPostBack Then
      TimerNotificationsRead.Enabled = True
    End If

    If DropDownListNotificationType.SelectedValue = 0 Then
      SqlDataSourceNotifications.SelectCommand = "  SELECT NotificationID, Notification, Status, TimeStamp, NotificationType FROM (   " + _
" SELECT     TOP (100) PERCENT dbo.Table_Notification.NotificationID, dbo.Table_Notification.Notification, N'NotRead' AS Status, dbo.Table_Notification.TimeStamp,  " + _
"                       dbo.Table_Notification.NotificationType " + _
" FROM         dbo.aspnet_Users INNER JOIN " + _
"                       dbo.Table_Prj_User_Junction ON dbo.aspnet_Users.UserId = dbo.Table_Prj_User_Junction.UserID INNER JOIN " + _
"                       dbo.Table_Notification ON dbo.Table_Prj_User_Junction.ProjectID = dbo.Table_Notification.ProjectID LEFT OUTER JOIN " + _
"                       dbo.Table_Notification_Read ON dbo.aspnet_Users.UserName = dbo.Table_Notification_Read.UserName AND  " + _
"                       dbo.Table_Notification.NotificationID = dbo.Table_Notification_Read.NotificationID " + _
" WHERE     (dbo.Table_Notification_Read.UserName IS NULL) AND (dbo.aspnet_Users.UserName = N'" + Page.User.Identity.Name.ToString + "') AND (dbo.Table_Notification_Read.NotificationID IS NULL) " + _
"    " + _
"         UNION ALL   " + _
"    " + _
"  SELECT     TOP (100) PERCENT dbo.Table_Notification.NotificationID, dbo.Table_Notification.Notification, N'Read' AS Status, dbo.Table_Notification.TimeStamp,   " + _
"                        dbo.Table_Notification.NotificationType  " + _
"  FROM         dbo.aspnet_Users INNER JOIN  " + _
"                        dbo.Table_Prj_User_Junction ON dbo.aspnet_Users.UserId = dbo.Table_Prj_User_Junction.UserID INNER JOIN  " + _
"                        dbo.Table_Notification ON dbo.Table_Prj_User_Junction.ProjectID = dbo.Table_Notification.ProjectID LEFT OUTER JOIN  " + _
"                        dbo.Table_Notification_Read ON dbo.Table_Notification.NotificationID = dbo.Table_Notification_Read.NotificationID  " + _
"  WHERE     (dbo.aspnet_Users.UserName = N'" + Page.User.Identity.Name.ToString + "') AND (dbo.Table_Notification_Read.UserName = N'" + Page.User.Identity.Name.ToString + "')  " + _
"         ) AS DATASOURCE1   " + _
"          WHERE TimeStamp > (CONVERT(nVarChar(4), YEAR(GETDATE())) + N'/' + (CASE WHEN LEN(CONVERT(nVarChar(2), month(GETDATE()))) = 1 THEN N'0' + CONVERT(nVarChar(2),    " + _
"                               month(GETDATE())) ELSE CONVERT(nVarChar(2), month(GETDATE())) END) + N'/' + (CASE WHEN LEN(CONVERT(nVarChar(2), day(GETDATE() - 0)))    " + _
"                               = 1 THEN N'0' + CONVERT(nVarChar(2), day(GETDATE() - 0)) ELSE CONVERT(nVarChar(2), day(GETDATE() - 0)) END))   " + _
"         ORDER BY NotificationID DESC  "

    ElseIf DropDownListNotificationType.SelectedValue = 1 Then
      SqlDataSourceNotifications.SelectCommand = "  SELECT NotificationID, Notification, Status, TimeStamp, NotificationType FROM (   " + _
" SELECT     TOP (100) PERCENT dbo.Table_Notification.NotificationID, dbo.Table_Notification.Notification, N'NotRead' AS Status, dbo.Table_Notification.TimeStamp,  " + _
"                       dbo.Table_Notification.NotificationType " + _
" FROM         dbo.aspnet_Users INNER JOIN " + _
"                       dbo.Table_Prj_User_Junction ON dbo.aspnet_Users.UserId = dbo.Table_Prj_User_Junction.UserID INNER JOIN " + _
"                       dbo.Table_Notification ON dbo.Table_Prj_User_Junction.ProjectID = dbo.Table_Notification.ProjectID LEFT OUTER JOIN " + _
"                       dbo.Table_Notification_Read ON dbo.aspnet_Users.UserName = dbo.Table_Notification_Read.UserName AND  " + _
"                       dbo.Table_Notification.NotificationID = dbo.Table_Notification_Read.NotificationID " + _
" WHERE     (dbo.Table_Notification_Read.UserName IS NULL) AND (dbo.aspnet_Users.UserName = N'" + Page.User.Identity.Name.ToString + "') AND (dbo.Table_Notification.NotificationType=1)  AND (dbo.Table_Notification_Read.NotificationID IS NULL) " + _
"    " + _
"         UNION ALL   " + _
"    " + _
"  SELECT     TOP (100) PERCENT dbo.Table_Notification.NotificationID, dbo.Table_Notification.Notification, N'Read' AS Status, dbo.Table_Notification.TimeStamp,   " + _
"                        dbo.Table_Notification.NotificationType  " + _
"  FROM         dbo.aspnet_Users INNER JOIN  " + _
"                        dbo.Table_Prj_User_Junction ON dbo.aspnet_Users.UserId = dbo.Table_Prj_User_Junction.UserID INNER JOIN  " + _
"                        dbo.Table_Notification ON dbo.Table_Prj_User_Junction.ProjectID = dbo.Table_Notification.ProjectID LEFT OUTER JOIN  " + _
"                        dbo.Table_Notification_Read ON dbo.Table_Notification.NotificationID = dbo.Table_Notification_Read.NotificationID  " + _
"  WHERE     (dbo.aspnet_Users.UserName = N'" + Page.User.Identity.Name.ToString + "') AND (dbo.Table_Notification.NotificationType=1)  AND (dbo.Table_Notification_Read.UserName = N'" + Page.User.Identity.Name.ToString + "')  " + _
"         ) AS DATASOURCE1   " + _
"          WHERE TimeStamp > (CONVERT(nVarChar(4), YEAR(GETDATE())) + N'/' + (CASE WHEN LEN(CONVERT(nVarChar(2), month(GETDATE()))) = 1 THEN N'0' + CONVERT(nVarChar(2),    " + _
"                               month(GETDATE())) ELSE CONVERT(nVarChar(2), month(GETDATE())) END) + N'/' + (CASE WHEN LEN(CONVERT(nVarChar(2), day(GETDATE() - 0)))    " + _
"                               = 1 THEN N'0' + CONVERT(nVarChar(2), day(GETDATE() - 0)) ELSE CONVERT(nVarChar(2), day(GETDATE() - 0)) END))   " + _
"         ORDER BY NotificationID DESC  "

    ElseIf DropDownListNotificationType.SelectedValue = 2 Then
      SqlDataSourceNotifications.SelectCommand = "  SELECT NotificationID, Notification, Status, TimeStamp, NotificationType FROM (   " + _
" SELECT     TOP (100) PERCENT dbo.Table_Notification.NotificationID, dbo.Table_Notification.Notification, N'NotRead' AS Status, dbo.Table_Notification.TimeStamp,  " + _
"                       dbo.Table_Notification.NotificationType " + _
" FROM         dbo.aspnet_Users INNER JOIN " + _
"                       dbo.Table_Prj_User_Junction ON dbo.aspnet_Users.UserId = dbo.Table_Prj_User_Junction.UserID INNER JOIN " + _
"                       dbo.Table_Notification ON dbo.Table_Prj_User_Junction.ProjectID = dbo.Table_Notification.ProjectID LEFT OUTER JOIN " + _
"                       dbo.Table_Notification_Read ON dbo.aspnet_Users.UserName = dbo.Table_Notification_Read.UserName AND  " + _
"                       dbo.Table_Notification.NotificationID = dbo.Table_Notification_Read.NotificationID " + _
" WHERE     (dbo.Table_Notification_Read.UserName IS NULL) AND (dbo.aspnet_Users.UserName = N'" + Page.User.Identity.Name.ToString + "') AND (dbo.Table_Notification.NotificationType=2)  AND (dbo.Table_Notification_Read.NotificationID IS NULL) " + _
"    " + _
"         UNION ALL   " + _
"    " + _
"  SELECT     TOP (100) PERCENT dbo.Table_Notification.NotificationID, dbo.Table_Notification.Notification, N'Read' AS Status, dbo.Table_Notification.TimeStamp,   " + _
"                        dbo.Table_Notification.NotificationType  " + _
"  FROM         dbo.aspnet_Users INNER JOIN  " + _
"                        dbo.Table_Prj_User_Junction ON dbo.aspnet_Users.UserId = dbo.Table_Prj_User_Junction.UserID INNER JOIN  " + _
"                        dbo.Table_Notification ON dbo.Table_Prj_User_Junction.ProjectID = dbo.Table_Notification.ProjectID LEFT OUTER JOIN  " + _
"                        dbo.Table_Notification_Read ON dbo.Table_Notification.NotificationID = dbo.Table_Notification_Read.NotificationID  " + _
"  WHERE     (dbo.aspnet_Users.UserName = N'" + Page.User.Identity.Name.ToString + "') AND (dbo.Table_Notification.NotificationType=2)  AND (dbo.Table_Notification_Read.UserName = N'" + Page.User.Identity.Name.ToString + "')  " + _
"         ) AS DATASOURCE1   " + _
"          WHERE TimeStamp > (CONVERT(nVarChar(4), YEAR(GETDATE())) + N'/' + (CASE WHEN LEN(CONVERT(nVarChar(2), month(GETDATE()))) = 1 THEN N'0' + CONVERT(nVarChar(2),    " + _
"                               month(GETDATE())) ELSE CONVERT(nVarChar(2), month(GETDATE())) END) + N'/' + (CASE WHEN LEN(CONVERT(nVarChar(2), day(GETDATE() - 0)))    " + _
"                               = 1 THEN N'0' + CONVERT(nVarChar(2), day(GETDATE() - 0)) ELSE CONVERT(nVarChar(2), day(GETDATE() - 0)) END))   " + _
"         ORDER BY NotificationID DESC  "
    ElseIf DropDownListNotificationType.SelectedValue = 3 Then
      SqlDataSourceNotifications.SelectCommand = "  SELECT NotificationID, Notification, Status, TimeStamp, NotificationType FROM (   " + _
" SELECT     TOP (100) PERCENT dbo.Table_Notification.NotificationID, dbo.Table_Notification.Notification, N'NotRead' AS Status, dbo.Table_Notification.TimeStamp,  " + _
"                       dbo.Table_Notification.NotificationType " + _
" FROM         dbo.aspnet_Users INNER JOIN " + _
"                       dbo.Table_Prj_User_Junction ON dbo.aspnet_Users.UserId = dbo.Table_Prj_User_Junction.UserID INNER JOIN " + _
"                       dbo.Table_Notification ON dbo.Table_Prj_User_Junction.ProjectID = dbo.Table_Notification.ProjectID LEFT OUTER JOIN " + _
"                       dbo.Table_Notification_Read ON dbo.aspnet_Users.UserName = dbo.Table_Notification_Read.UserName AND  " + _
"                       dbo.Table_Notification.NotificationID = dbo.Table_Notification_Read.NotificationID " + _
" WHERE     (dbo.Table_Notification_Read.UserName IS NULL) AND (dbo.aspnet_Users.UserName = N'" + Page.User.Identity.Name.ToString + "') AND (dbo.Table_Notification.NotificationType=3)  AND (dbo.Table_Notification_Read.NotificationID IS NULL) " + _
"    " + _
"         UNION ALL   " + _
"    " + _
"  SELECT     TOP (100) PERCENT dbo.Table_Notification.NotificationID, dbo.Table_Notification.Notification, N'Read' AS Status, dbo.Table_Notification.TimeStamp,   " + _
"                        dbo.Table_Notification.NotificationType  " + _
"  FROM         dbo.aspnet_Users INNER JOIN  " + _
"                        dbo.Table_Prj_User_Junction ON dbo.aspnet_Users.UserId = dbo.Table_Prj_User_Junction.UserID INNER JOIN  " + _
"                        dbo.Table_Notification ON dbo.Table_Prj_User_Junction.ProjectID = dbo.Table_Notification.ProjectID LEFT OUTER JOIN  " + _
"                        dbo.Table_Notification_Read ON dbo.Table_Notification.NotificationID = dbo.Table_Notification_Read.NotificationID  " + _
"  WHERE     (dbo.aspnet_Users.UserName = N'" + Page.User.Identity.Name.ToString + "') AND (dbo.Table_Notification.NotificationType=3)  AND (dbo.Table_Notification_Read.UserName = N'" + Page.User.Identity.Name.ToString + "')  " + _
"         ) AS DATASOURCE1   " + _
"          WHERE TimeStamp > (CONVERT(nVarChar(4), YEAR(GETDATE())) + N'/' + (CASE WHEN LEN(CONVERT(nVarChar(2), month(GETDATE()))) = 1 THEN N'0' + CONVERT(nVarChar(2),    " + _
"                               month(GETDATE())) ELSE CONVERT(nVarChar(2), month(GETDATE())) END) + N'/' + (CASE WHEN LEN(CONVERT(nVarChar(2), day(GETDATE() - 0)))    " + _
"                               = 1 THEN N'0' + CONVERT(nVarChar(2), day(GETDATE() - 0)) ELSE CONVERT(nVarChar(2), day(GETDATE() - 0)) END))   " + _
"         ORDER BY NotificationID DESC  "
    End If


  End Sub

  Protected Sub GridViewNotifications_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewNotifications.RowDataBound

    Dim MyTask As New MyCommonTasks
    MyTask.HoverEffectOnGridviewCells(sender, e.Row)

    If e.Row.RowType = DataControlRowType.DataRow Then
      If DataBinder.Eval(e.Row.DataItem, "Status").ToString = "NotRead" Then
                e.Row.BackColor = System.Drawing.Color.LightBlue
            End If

      Dim LabelNotification As Label = DirectCast(e.Row.FindControl("LabelNotification"), Label)
      Dim StartOfText As Integer = 0
      Dim EndOfText As Integer = 0
      Dim ImageNotification As String = ""
      Dim LabelNotifications_1Segment As String = ""
      Dim LabelNotifications_2Segment As String = ""
      Dim LabelNotifications_3Segment As String = ""

      If DataBinder.Eval(e.Row.DataItem, "NotificationType") = 1 Then
        ImageNotification = "<span " + "style=" + """" + "float: left" + """" + "" + "><img alt=" + """" + "" + """" + " src=" + """" + "Images/NewPaymentRequest_notification.bmp" + """" + " />" + "</span>"
      ElseIf DataBinder.Eval(e.Row.DataItem, "NotificationType") = 2 Then
        ImageNotification = "<span " + "style=" + """" + "float: left" + """" + "" + "><img alt=" + """" + "" + """" + " src=" + """" + "Images/Approval_notification.bmp" + """" + " />" + "</span>"
      ElseIf DataBinder.Eval(e.Row.DataItem, "NotificationType") = 3 Then
        ImageNotification = "<span " + "style=" + """" + "float: left" + """" + "" + "><img alt=" + """" + "" + """" + " src=" + """" + "Images/Contract_notification.bmp" + """" + " />" + "</span>"
      End If

      If LabelNotification IsNot Nothing Then
        StartOfText = InStr(LabelNotification.Text.ToString, "| Project:") + Len("| Project:")
        EndOfText = StartOfText + InStr(Mid(LabelNotification.Text.ToString, StartOfText, Len(LabelNotification.Text.ToString) - StartOfText), "|")

        LabelNotifications_1Segment = Mid(LabelNotification.Text.ToString, 1, StartOfText)
        LabelNotifications_2Segment = Mid(LabelNotification.Text.ToString, StartOfText, EndOfText - StartOfText - 1) + " "
        LabelNotifications_3Segment = Mid(LabelNotification.Text.ToString, EndOfText - 1, Len(LabelNotification.Text.ToString))

        LabelNotification.Text = ImageNotification + LabelNotifications_1Segment + "<span style=" + """" + " padding: 3px; border: thin solid #808080; margin: 1px; background-color: " + GetBackColor(DataBinder.Eval(e.Row.DataItem, "NotificationID").ToString) + "; color: " + GetForeColor(DataBinder.Eval(e.Row.DataItem, "NotificationID").ToString) + "; font-size: 10px; font-weight: bold;" + """" + ">" + LabelNotifications_2Segment + "</span>" + LabelNotifications_3Segment

      End If
    End If
  End Sub

  Protected Sub TimerNotificationsRead_Tick(sender As Object, e As System.EventArgs) Handles TimerNotificationsRead.Tick
    ' change all unread into READ
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT     TOP (100) PERCENT dbo.Table_Notification.NotificationID " + _
        " FROM         dbo.aspnet_Users INNER JOIN " + _
        "                       dbo.Table_Prj_User_Junction ON dbo.aspnet_Users.UserId = dbo.Table_Prj_User_Junction.UserID INNER JOIN " + _
        "                       dbo.Table_Notification ON dbo.Table_Prj_User_Junction.ProjectID = dbo.Table_Notification.ProjectID LEFT OUTER JOIN " + _
        "                       dbo.Table_Notification_Read ON dbo.aspnet_Users.UserName = dbo.Table_Notification_Read.UserName AND  " + _
        "                       dbo.Table_Notification.NotificationID = dbo.Table_Notification_Read.NotificationID " + _
        " WHERE     (dbo.Table_Notification_Read.UserName IS NULL) AND (dbo.aspnet_Users.UserName = N'" + Page.User.Identity.Name.ToString + "') AND  " + _
        "                       (dbo.Table_Notification.TimeStamp > CONVERT(nVarChar(4), YEAR(GETDATE())) + N'/' + (CASE WHEN LEN(CONVERT(nVarChar(2), month(GETDATE())))  " + _
        "                       = 1 THEN N'0' + CONVERT(nVarChar(2), month(GETDATE())) ELSE CONVERT(nVarChar(2), month(GETDATE())) END)  " + _
        "                       + N'/' + (CASE WHEN LEN(CONVERT(nVarChar(2), day(GETDATE() - 0))) = 1 THEN N'0' + CONVERT(nVarChar(2), day(GETDATE() - 0))  " + _
        "                       ELSE CONVERT(nVarChar(2), day(GETDATE() - 0)) END)) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            Dim dr As SqlDataReader = cmd.ExecuteReader

            Try

                ' while read, change all unread into READ
                While dr.Read
                    Dim conMakeRead As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                    conMakeRead.Open()
                    Dim sqlstringMakeRead As String = " INSERT INTO [Table_Notification_Read] " +
                                                                              "            ([NotificationID] " +
                                                                              "            ,[UserName]) " +
                                                                              "      VALUES " +
                                                                              "            ( " + dr(0).ToString + " " +
                                                                              "            , N'" + Page.User.Identity.Name.ToString + "' ) "
                    Dim cmdMakeRead As New SqlCommand(sqlstringMakeRead, conMakeRead)
                    cmdMakeRead.CommandType = System.Data.CommandType.Text

                    Dim drMakeRead As SqlDataReader = cmdMakeRead.ExecuteReader
                    conMakeRead.Close()
                    drMakeRead.Close()
                End While

            Catch ex As Exception

            End Try

            con.Close()
            dr.Close()
            con.Dispose()

        End Using

        ' Disable timer
        TimerNotificationsRead.Enabled = False
        GridViewNotifications.DataBind()

    End Sub

    Protected Sub DropDownListNotificationType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListNotificationType.SelectedIndexChanged
        GridViewNotifications.DataBind()
    End Sub

    Protected Function GetBackColor(ByVal NotificationID As String) As String
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT     RTRIM(dbo.Table1_Project.BackColor) AS BackColor " +
                    " FROM         dbo.Table1_Project INNER JOIN " +
                    "                       dbo.Table_Notification ON dbo.Table1_Project.ProjectID = dbo.Table_Notification.ProjectID " +
                    " WHERE     (dbo.Table_Notification.NotificationID = " + NotificationID + ") "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim BackColor As String = ""
            While dr.Read
                If Len(dr(0).ToString) = 7 Then
                    BackColor = dr(0).ToString
                Else
                    BackColor = "#FFFFFF"
                End If
            End While
            con.Close()
            dr.Close()
            con.Dispose()
            Return BackColor

        End Using
    End Function

    Protected Function GetForeColor(ByVal NotificationID As String) As String
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT     RTRIM(dbo.Table1_Project.ForeColor) AS ForeColor " +
                    " FROM         dbo.Table1_Project INNER JOIN " +
                    "                       dbo.Table_Notification ON dbo.Table1_Project.ProjectID = dbo.Table_Notification.ProjectID " +
                    " WHERE     (dbo.Table_Notification.NotificationID = " + NotificationID + ") "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim ForeColor As String = ""
            While dr.Read
                If Len(dr(0).ToString) = 7 Then
                    ForeColor = dr(0).ToString
                Else
                    ForeColor = "#000000"
                End If
            End While
            con.Close()
            dr.Close()
            con.Dispose()
            Return ForeColor

        End Using
    End Function

End Class

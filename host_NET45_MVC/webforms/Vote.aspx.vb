Imports System.IO
Imports System.Net.Mail
Imports System.Data.SqlClient
Imports PTS_App_Code_VB_Project.PTS_MERCURY.helper.EmailGenerator

Partial Class Vote
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        LiteralUser.Text = GetFullName(Page.User.Identity.Name.ToString.ToLower)

        If Page.User.Identity.Name.ToLower = "elena.kalinicheva" _
            OrElse Page.User.Identity.Name.ToLower = "savas" _
            OrElse Page.User.Identity.Name.ToLower = "irina.molodtsova" Then

            ButtonResults.Visible = True

        End If

    End Sub

    Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender

        LabelStatus.Text = GetStatus()

    End Sub

    Protected Sub ImageButtonYES_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonYES.Click

        UpdateVote(True)

    End Sub

    Protected Sub ImageButtonNO_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonNO.Click

        UpdateVote(False)

    End Sub


    Protected Sub UpdateVote(ByVal _Vote As Boolean)

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain

            con.Open()
            Dim sqlstring As String = " UPDATE [aspnet_Users] SET [Vote_Xmas] = @Vote_Xmas, [Vote_Time] = GETDATE() WHERE UserName = @UserName "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim Vote_Xmas As SqlParameter = cmd.Parameters.Add("@Vote_Xmas", System.Data.SqlDbType.Bit)
            Vote_Xmas.Value = _Vote
            Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar)
            UserName.Value = Page.User.Identity.Name.ToString.ToLower

            Dim dr As SqlDataReader = cmd.ExecuteReader
            con.Close()
            con.Dispose()
            dr.Close()

        End Using

    End Sub

    Protected Function GetStatus() As String

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain

            con.Open()
            Dim sqlstring As String = " SELECT [Vote_Xmas], [Vote_Time] FROM [aspnet_Users] WHERE UserName = @UserName "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar)
            UserName.Value = Page.User.Identity.Name.ToString.ToLower

            Dim dr As SqlDataReader = cmd.ExecuteReader

            Dim _return As String = ""

            While dr.Read
                If IsDBNull(dr(0)) Then
                    _return = "You didnt vote yet"
                    LabelStatus.ForeColor = System.Drawing.Color.Gray
                    Return _return
                    Exit Function
                ElseIf dr(0) = True Then
                    _return = "You are attending.<br />Your vote saved.<br />Last update " + dr(1).ToString
                    LabelStatus.ForeColor = System.Drawing.Color.Green
                    Return _return
                    Exit Function
                ElseIf dr(0) = False Then
                    _return = "You are Not attending.<br />Your vote saved.<br />Last update " + dr(1).ToString
                    LabelStatus.ForeColor = System.Drawing.Color.Red
                    Return _return
                    Exit Function
                End If
            End While

            con.Close()
            con.Dispose()
            dr.Close()

        End Using

    End Function

    Protected Function GetFullName(ByVal _UserName As String) As String

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain

            con.Open()
            Dim sqlstring As String = " SELECT     UPPER(REPLACE(LEFT(dbo.aspnet_Membership.Email, PATINDEX('%@%', dbo.aspnet_Membership.Email) - 1), '.', ' ')) AS UserName " +
    " FROM         dbo.aspnet_Membership INNER JOIN " +
    "                       dbo.aspnet_Users ON dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId AND dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId " +
    " WHERE     (dbo.aspnet_Users.UserName = @UserName) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar)
            UserName.Value = Page.User.Identity.Name.ToString.ToLower

            Dim dr As SqlDataReader = cmd.ExecuteReader

            While dr.Read
                Return dr(0).ToString
            End While

            con.Close()
            con.Dispose()
            dr.Close()

        End Using

    End Function

    Protected Sub ButtonResults_Click(sender As Object, e As System.EventArgs) Handles ButtonResults.Click
        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        Dim strFilePathPendingList As String = String.Empty

        Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
        strFilePathPendingList = Server.MapPath("~/FeedOlgaFile/Xmas_Survey_Result") +
          " " + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString +
          "-" + Mid(result.ToString, 1, 2).ToString + " _" + UniqueString1 + ".xls"

        Dim oStringWriter As New System.IO.StringWriter()
        Dim oHtmlTextWriter As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(oStringWriter)
        Dim objStreamWriter As StreamWriter = File.AppendText(strFilePathPendingList)

        GridView1.RenderControl(oHtmlTextWriter)

        objStreamWriter.WriteLine(oStringWriter.ToString())
        objStreamWriter.Close()

        '(1) Create the MailMessage instance
        Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "P T S")
        Dim mm1 As New MailMessage()
        mm1.From = MailFrom

        'mm1.To.Add("elena.kalinicheva@mercuryeng.ru")
        mm1.To.Add(getEmail())

        mm1.Subject = "Xmas Survey Result"

        mm1.Body = "Auto Email From PTS"

        mm1.IsBodyHtml = True

        ' add attachment if exist
        Dim path As String = strFilePathPendingList
        Dim fileToAttach As System.IO.FileInfo = New System.IO.FileInfo(path)
        If fileToAttach.Exists Then
            ' JUST ACTIVATE IF REQUIRED
            mm1.Attachments.Add(New Attachment(path))
        End If

        '(3) Create the SmtpClient object
        Dim smtp As New SmtpClient_RussianEncoded

        smtp.Send(mm1)
    End Sub

    Protected Function getEmail() As String

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain

            con.Open()
            Dim sqlstring As String = " SELECT     RTRIM(dbo.aspnet_Membership.LoweredEmail) AS LoweredEmail " +
    " FROM         dbo.aspnet_Membership INNER JOIN " +
    "                       dbo.aspnet_Users ON dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId AND dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId " +
    " WHERE     (dbo.aspnet_Users.UserName = @UserName) "


            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar)
            UserName.Value = Page.User.Identity.Name.ToString.ToLower

            Dim dr As SqlDataReader = cmd.ExecuteReader

            While dr.Read
                Return dr(0).ToString
            End While

            con.Close()
            con.Dispose()
            dr.Close()

        End Using

    End Function

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Exit Sub
    End Sub

End Class

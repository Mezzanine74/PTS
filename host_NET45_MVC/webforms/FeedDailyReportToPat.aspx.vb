Imports System.IO
Imports System.Net.Mail
Imports System.Data.SqlClient

Partial Class FeedDailyReportToPat
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If LocalTime.GetTime.DayOfWeek.ToString <> "Saturday" AndAlso LocalTime.GetTime.DayOfWeek.ToString <> "Sunday" Then
            If LocalTime.GetTime.Hour = 5 Then
                If LocalTime.GetTime.Minute >= 54 AndAlso LocalTime.GetTime.Minute <= 59 Then

                    SendEmailPendingFollowUp()
                    SendEmailBalanceOnDifferenceCurrency()

                    Dim zoneId As String = "Russian Standard Time"
                    Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
                    Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)
                    result = result.AddDays(-1)

                    Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                    RenderReport.Render("disc", ReportViewerDailyReportToPat, "DailyReportToPatrickRev1", _
                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                        "DailyReportToPatrickRev/Daily project analysis from PTS " + result.ToString("dd-MM-yyyy") + UniqueString1 + ".xls")

                    Dim UniqueString2 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                    RenderReport.Render("disc", ReportViewerDailyReportToPat, "DailyReportToPatrickRev1Chart", _
                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                        "DailyReportToPatrickRev/Daily project analysis from PTS as CHART " + result.ToString("dd-MM-yyyy") + UniqueString2 + ".xls")

                    ' SEND EMAIL
                    Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                    con.Open()
                    Dim sqlstring As String = " SELECT rtrim(LoweredEmail) FROM [aspnet_Users] inner join aspnet_Membership on aspnet_Users.UserId = aspnet_Membership.UserId where SendEmailDailyReport = 1 "

                    Dim cmd As New SqlCommand(sqlstring, con)
                    cmd.CommandType = System.Data.CommandType.Text

                    Dim dr As SqlDataReader = cmd.ExecuteReader

                    Dim mail As System.Net.Mail.MailMessage = Nothing
                    Dim smtpZ As System.Net.Mail.SmtpClient = Nothing

                    mail = New System.Net.Mail.MailMessage()

                    mail.From = New System.Net.Mail.MailAddress("savas.karaduman@mercuryeng.ru", "Savas Karaduman")

                    While dr.Read
                        mail.To.Add(dr(0).ToString)
                    End While

                    Dim path As String = Server.MapPath("~/DailyReportToPatrickRev/Daily project analysis from PTS " + result.ToString("dd-MM-yyyy") + UniqueString1 + ".xls")
                    Dim pathToChart As String = Server.MapPath("~/DailyReportToPatrickRev/Daily project analysis from PTS as CHART " + result.ToString("dd-MM-yyyy") + UniqueString2 + ".xls")

                    Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
                    If file.Exists Then
                        mail.Attachments.Add(New Attachment(path))
                    End If

                    Dim fileToChart As System.IO.FileInfo = New System.IO.FileInfo(pathToChart)
                    If fileToChart.Exists Then
                        mail.Attachments.Add(New Attachment(pathToChart))
                    End If

                    mail.Subject = "Daily Report From PTS " + result.ToString("dd-MM-yyyy")
                    mail.Body = "<span style=" + """" + " font-size: 11px;	color: #FF0000;	font-style: italic;	padding: 5px;	margin: 3px;	border-style: dotted;	border-width: thin; background-color: #FFFFCC;" + """" + "> Auto Generated Email From PTS </span><br/><hr/> " +
                      "Dear All," + "<br/>" +
                      "<br/>" +
                      "Please see attached." + "<br/>" +
                      "<br/>" +
                      "Regards." + "<br/>" +
                      "<br/>" +
                      "Savas"

                    mail.IsBodyHtml = True

                    smtpZ = New System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings("MailServerHost").ToString)
                    smtpZ.Credentials = New System.Net.NetworkCredential(SavasEmailCredentials.GetEmailSavas, SavasEmailCredentials.GetPasswordSavasEMail)
                    smtpZ.UseDefaultCredentials = False
                    smtpZ.Port = 25
                    smtpZ.EnableSsl = False

                    'Try
                    smtpZ.Send(mail)
                    'Catch ex As Exception
                    'End Try

                    con.Close()
                    dr.Close()


                End If
            End If
        End If

    End Sub

    Protected Sub SendEmailPendingFollowUp()

        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)
        result = result.AddDays(-1)

        Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
        Dim UniqueString2 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
        RenderReport.Render("disc", ReportViewerDailyReportToPat, "PendingListDailyFollowRev2",
                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                            "DailyReportToPatrickRev/PendingList Daily FollowUp Project By " + result.ToString("dd-MM-yyyy") + UniqueString1 + ".xls")

        RenderReport.Render("disc", ReportViewerDailyReportToPat, "PendingListDailyFollowSupplierBy",
                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                            "DailyReportToPatrickRev/PendingList Daily FollowUp Supplier By " + result.ToString("dd-MM-yyyy") + UniqueString2 + ".xls")

        ' SEND EMAIL
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT rtrim(LoweredEmail) FROM [aspnet_Users] inner join aspnet_Membership on aspnet_Users.UserId = aspnet_Membership.UserId where [SendEmailPendingFollow] = 1 "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            Dim dr As SqlDataReader = cmd.ExecuteReader

            Dim mail As System.Net.Mail.MailMessage = Nothing
            Dim smtpZ As System.Net.Mail.SmtpClient = Nothing

            mail = New System.Net.Mail.MailMessage()

            mail.From = New System.Net.Mail.MailAddress("savas.karaduman@mercuryeng.ru", "Savas Karaduman")

            While dr.Read
                mail.To.Add(dr(0).ToString)
            End While

            Dim path As String = Server.MapPath("~/DailyReportToPatrickRev/PendingList Daily FollowUp Project By " + result.ToString("dd-MM-yyyy") + UniqueString1 + ".xls")
            Dim path2 As String = Server.MapPath("~/DailyReportToPatrickRev/PendingList Daily FollowUp Supplier By " + result.ToString("dd-MM-yyyy") + UniqueString2 + ".xls")

            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
            If file.Exists Then
                mail.Attachments.Add(New Attachment(path))
            End If

            Dim file2 As System.IO.FileInfo = New System.IO.FileInfo(path2)
            If file2.Exists Then
                mail.Attachments.Add(New Attachment(path2))
            End If

            mail.Subject = "Pending List Daily FollowUp From PTS " + result.ToString("dd-MM-yyyy")
            mail.Body = "<span style=" + """" + " font-size: 11px;	color: #FF0000;	font-style: italic;	padding: 5px;	margin: 3px;	border-style: dotted;	border-width: thin; background-color: #FFFFCC;" + """" + "> Auto Generated Email From PTS </span><br/><hr/> " +
              "Dear All," + "<br/>" +
              "<br/>" +
              "Please see attached." + "<br/>" +
              "<br/>" +
              "Regards." + "<br/>" +
              "<br/>" +
              "PTS"

            mail.IsBodyHtml = True

            smtpZ = New System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings("MailServerHost").ToString)
            smtpZ.Credentials = New System.Net.NetworkCredential(SavasEmailCredentials.GetEmailSavas, SavasEmailCredentials.GetPasswordSavasEMail)
            smtpZ.UseDefaultCredentials = False
            smtpZ.Port = 25
            smtpZ.EnableSsl = False

            'Try
            smtpZ.Send(mail)
            'Catch ex As Exception
            'End Try

            con.Close()
            dr.Close()

        End Using

    End Sub

    Protected Sub SendEmailBalanceOnDifferenceCurrency()

        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)
        result = result.AddDays(-1)

        Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
        RenderReport.Render("disc", ReportViewerDailyReportToPat, "BalanceOnDifferentCurrencies",
                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                            "DailyReportToPatrickRev/Balance On Different Currencies By Project" + result.ToString("dd-MM-yyyy") + UniqueString1 + ".xls")

        ' SEND EMAIL
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT rtrim(LoweredEmail) FROM [aspnet_Users] inner join aspnet_Membership on aspnet_Users.UserId = aspnet_Membership.UserId where [SendEmailDailyReport] = 1 "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            Dim dr As SqlDataReader = cmd.ExecuteReader

            Dim mail As System.Net.Mail.MailMessage = Nothing
            Dim smtpZ As System.Net.Mail.SmtpClient = Nothing

            mail = New System.Net.Mail.MailMessage()

            mail.From = New System.Net.Mail.MailAddress("savas.karaduman@mercuryeng.ru", "Savas Karaduman")

            While dr.Read
                mail.To.Add(dr(0).ToString)
            End While

            Dim path As String = Server.MapPath("DailyReportToPatrickRev/Balance On Different Currencies By Project" + result.ToString("dd-MM-yyyy") + UniqueString1 + ".xls")

            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
            If file.Exists Then
                mail.Attachments.Add(New Attachment(path))
            End If

            mail.Subject = "Balance On Different Currencies By Project From PTS " + result.ToString("dd-MM-yyyy")
            mail.Body = "<span style=" + """" + " font-size: 11px;	color: #FF0000;	font-style: italic;	padding: 5px;	margin: 3px;	border-style: dotted;	border-width: thin; background-color: #FFFFCC;" + """" + "> Auto Generated Email From PTS </span><br/><hr/> " + _
              "Dear All," + "<br/>" + _
              "<br/>" + _
              "Please see attached." + "<br/>" + _
              "<br/>" + _
              "Regards." + "<br/>" + _
              "<br/>" + _
              "PTS"

            mail.IsBodyHtml = True

            smtpZ = New System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings("MailServerHost").ToString)
            smtpZ.Credentials = New System.Net.NetworkCredential(SavasEmailCredentials.GetEmailSavas, SavasEmailCredentials.GetPasswordSavasEMail)
            smtpZ.UseDefaultCredentials = False
            smtpZ.Port = 25
            smtpZ.EnableSsl = False

            'Try
            smtpZ.Send(mail)
            'Catch ex As Exception
            'End Try

            con.Close()
            dr.Close()

        End Using

    End Sub


End Class

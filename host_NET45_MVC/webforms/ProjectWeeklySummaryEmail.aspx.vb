Imports System.Net
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms
Imports Microsoft.ReportingServices
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.IO

Partial Class ProjectWeeklySummaryEmail
    Inherits System.Web.UI.Page

    Public Sub RenderReport_()

        RenderReport.Render("html", ReportViewerDeliveryReport, "ProjectSummaryWeekly", "DayOfRun", GetDayOfRun)

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        SendEmailToManagement_PaymentsSinceJan()

        If Not IsPostBack Or IsPostBack Then
            RenderReport_()
            SendEmailToAll()
        End If

    End Sub

    Protected Sub Button_Test_Click(sender As Object, e As System.EventArgs) Handles Button_Test.Click
        SendEmailToAll()
    End Sub

    Protected Sub SendEmailToAll()

        If LocalTime.GetTime.DayOfWeek.ToString = "Monday" Then
            If LocalTime.GetTime.Hour = 8 Then
                If LocalTime.GetTime.Minute >= 44 AndAlso LocalTime.GetTime.Minute <= 49 Then

                    ' Start To Check Who Enters documents this week
                    Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain

                        con.Open()
                        Dim sqlstring As String = " SELECT     RTRIM(dbo.aspnet_Membership.LoweredEmail) AS LoweredEmail " + _
                                                  " FROM         dbo.aspnet_Users INNER JOIN " + _
                                                  "       dbo.aspnet_Membership ON dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId AND  " + _
                                                  "       dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId " + _
                                                  " WHERE     (dbo.aspnet_Users.SendEmailWeeklyProjectSummary = 1) "

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

                        Dim FileToPath As String = ProduceExcelFilePathForThisUser(GetDayOfRun.Replace("/", "-"))
                        CreateExcelFileForThisUserThenSaveToDisk(FileToPath)

                        Dim path As String = Server.MapPath(FileToPath)
                        Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
                        If file.Exists Then
                            mail.Attachments.Add(New Attachment(path))
                        End If


                        mail.Subject = "Project Weekly Summary " + GetDayOfRun()
                        mail.Body = "<span style=" + """" + " font-size: 11px;	color: #FF0000;	font-style: italic;	padding: 5px;	margin: 3px;	border-style: dotted;	border-width: thin; background-color: #FFFFCC;" + """" + "> Auto Generated Email From PTS </span><br/><hr/> " +
                          "Dear All," + "<br/>" +
                          "<br/>" +
                          "Project Weekly Summary attached." + "<br/>" +
                          "<br/>" +
                          "Regards." + "<br/>" +
                          "<br/>" +
                          "Savas"

                        mail.IsBodyHtml = True

                        smtpZ = New System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings("MailServerHost").ToString)
                        smtpZ.Credentials = New System.Net.NetworkCredential("savas.karaduman@mercuryeng.ru", "thee8Coh")
                        smtpZ.UseDefaultCredentials = False
                        smtpZ.Port = 25
                        smtpZ.EnableSsl = False

                        Try
                            smtpZ.Send(mail)
                        Catch ex As Exception
                        End Try

                        con.Close()
                        dr.Close()

                    End Using

                    ' Send Client Contracts Notification
                    Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                        Dim Contracts = (From C In db.Table_Contracts
                                         Join Supl In db.Table6_Supplier On C.SupplierID Equals Supl.SupplierID
                                         Where C.ProjectID = 999 And C.SignByMercury = True And C.SignBySupplier = False
                                         Select New With
                                        {
                                         .TypeOfDoc = "Contract",
                                         .ContractID = C.ContractID,
                                         .AddendumID = 0,
                                         .SupplierName = Supl.SupplierName,
                                         .ContractNo = C.ContractNo,
                                         .ContractDate = C.ContractDate,
                                        .ContractDescription = C.ContractDescription,
                                        .ContractValue_woVAT = C.ContractValue_woVAT,
                                        .ContractCurrency = C.ContractCurrency,
                                        .ContractType = C.ContractType,
                                        .SignBySupplier = C.SignBySupplier,
                                        .SignByMercury = C.SignByMercury
                                         })

                        Dim Addendums = (From C In db.Table_Contracts
                                         Join Supl In db.Table6_Supplier On C.SupplierID Equals Supl.SupplierID
                                         Join Addn In db.Table_Addendums On C.ContractID Equals Addn.AddendumID
                                         Where C.ProjectID = 999 And C.SignByMercury = True And C.SignBySupplier = False
                                         Select New With
                                        {
                                         .TypeOfDoc = "Addendum",
                                         .ContractID = C.ContractID,
                                         .AddendumID = Addn.AddendumID,
                                         .SupplierName = Supl.SupplierName,
                                         .ContractNo = Addn.AddendumNo,
                                        .ContractDate = Addn.AddendumDate,
                                        .ContractDescription = Addn.AddendumDescription,
                                        .ContractValue_woVAT = Addn.AddendumValue_woVAT,
                                        .ContractCurrency = C.ContractCurrency,
                                        .ContractType = "",
                                        .SignBySupplier = Addn.AddendumSignBySupplier,
                                        .SignByMercury = Addn.AddendumSignByMercury
                                         })

                        GridViewContractNotification.DataSource = Contracts.Concat(Addendums) _
                            .OrderBy(Function(c) c.ContractID).ThenBy(Function(c) c.AddendumID) _
                            .ToList() _
                            .Select(Function(c) New With {
                                         .TypeOfDoc = c.TypeOfDoc,
                                         .ContractID = c.ContractID,
                                         .AddendumID = 0,
                                         .SupplierName = c.SupplierName,
                                         .ContractNo = c.ContractNo,
                                        .ContractDate = String.Format("{0:dd/MM/yyyy}", c.ContractDate),
                                        .ContractDescription = c.ContractDescription,
                                        .ContractValue_woVAT = c.ContractValue_woVAT,
                                        .ContractCurrency = c.ContractCurrency,
                                        .ContractType = c.ContractType,
                                        .SignBySupplier = c.SignBySupplier,
                                        .SignByMercury = c.SignByMercury
                                    })

                        GridViewContractNotification.DataBind()

                        ' send email

                        Dim mail As System.Net.Mail.MailMessage = Nothing
                        Dim smtpZ As System.Net.Mail.SmtpClient = Nothing

                        mail = New System.Net.Mail.MailMessage()

                        mail.From = New System.Net.Mail.MailAddress("savas.karaduman@mercuryeng.ru", "Savas Karaduman")

                        Dim _roleName = "ContractLeadGirls"
                        Dim membership = (From C In db.aspnet_Membership
                                          Join usr In db.aspnet_Users On C.UserId Equals usr.UserId
                                          Join usrinrole In db.aspnet_UsersInRoles On usr.UserId Equals usrinrole.UserId
                                          Join role In db.aspnet_Roles On usrinrole.RoleId Equals role.RoleId
                                          Where role.RoleName = _roleName
                                          Select New With
                            {
                             .email_ = C.LoweredEmail
                             }).ToList

                        For i = 1 To membership.Count - 1

                            mail.To.Add(membership(i).email_)

                        Next

                        mail.Bcc.Add("savas.karaduman@mercuryeng.ru")

                        Dim FileToPath As String = ProduceExcelFilePathForThisUserContract(GetDayOfRun.Replace("/", "-"))
                        CreateExcelFileForThisUserThenSaveToDiskContract(FileToPath)

                        Dim path As String = Server.MapPath(FileToPath)
                        Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
                        If file.Exists Then
                            mail.Attachments.Add(New Attachment(path))
                        End If


                        mail.Subject = "Client Contracts Notification " + GetDayOfRun()
                        mail.Body = "<span style=" + """" + " font-size: 11px;	color: #FF0000;	font-style: italic;	padding: 5px;	margin: 3px;	border-style: dotted;	border-width: thin; background-color: #FFFFCC;" + """" + "> Auto Generated Email From PTS </span><br/><hr/> " +
                          "Dear All," + "<br/>" +
                          "<br/>" +
                          "Client Contract Notification is attached. You can see pending contrats and addendums by Client" + "<br/>" +
                          "These are approved by Mercury but not approved by Clients yet." +
                          "<br/>" +
                          "Regards." + "<br/>" +
                          "<br/>" +
                          "Savas"

                        mail.IsBodyHtml = True

                        smtpZ = New System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings("MailServerHost").ToString)
                        smtpZ.Credentials = New System.Net.NetworkCredential("savas.karaduman@mercuryeng.ru", "thee8Coh")
                        smtpZ.UseDefaultCredentials = False
                        smtpZ.Port = 25
                        smtpZ.EnableSsl = False

                        Try
                            smtpZ.Send(mail)
                        Catch ex As Exception
                        End Try

                    End Using

                End If
            End If
        End If

    End Sub

    Protected Function ProduceExcelFilePathForThisUser(ByVal _Date As String) As String
        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        Dim strFilePathPendingList As String = String.Empty

        Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
        strFilePathPendingList = "~/ProjectWeeklySummary/ProjectWeeklySummary_" + _Date + "_" + UniqueString1 + ".xls"
        Return (strFilePathPendingList)
    End Function

    Protected Function ProduceExcelFilePathForThisUserContract(ByVal _Date As String) As String
        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        Dim strFilePathPendingList As String = String.Empty

        Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
        strFilePathPendingList = "~/ProjectWeeklySummary/ClientContractNotification_" + _Date + "_" + UniqueString1 + ".xls"
        Return (strFilePathPendingList)
    End Function


    Protected Sub CreateExcelFileForThisUserThenSaveToDisk(ByVal FilePath_ As String)
        ReportViewerDeliveryReport.Visible = True
        ReportViewerDeliveryReport.Reset()
        ReportViewerDeliveryReport.ServerReport.ReportPath = ConfigurationManager.AppSettings("ReportPath").ToString + "ProjectSummaryWeekly"
        ReportViewerDeliveryReport.ServerReport.ReportServerUrl = New System.Uri(ConfigurationManager.AppSettings("ReportServerURL").ToString)
        ReportViewerDeliveryReport.ServerReport.ReportServerCredentials = New ReportCredentials

        Dim myparams As New List(Of ReportParameter)
        Dim myparameter As ReportParameter

        myparameter = New ReportParameter("DayOfRun", GetDayOfRun)
        myparams.Add(myparameter)

        ReportViewerDeliveryReport.ServerReport.SetParameters(myparams)


        Dim warnings As Warning
        Dim streamIds As String
        Dim mimeType As String = String.Empty
        Dim encoding As String = String.Empty
        Dim extension As String = "xls"

        Dim bytes As Byte() = ReportViewerDeliveryReport.ServerReport.Render("Excel", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        Dim oFileStream As System.IO.FileStream
        Dim strFilePath As String = String.Empty
        strFilePath = Server.MapPath(FilePath_)

        oFileStream = New System.IO.FileStream(strFilePath, System.IO.FileMode.Create)
        oFileStream.Write(bytes, 0, bytes.Length)
        oFileStream.Close()
    End Sub

    Protected Function GetDayOfRun() As String
        Dim _date As DateTime = LocalTime.GetTime
        _date = _date.AddDays(-1)

        Dim _day As String = _date.Day.ToString
        Dim _month As String = _date.Month.ToString
        Dim _year As String = _date.Year.ToString

        If Len(_day) = 1 Then
            _day = "0" + _day
        End If

        If Len(_month) = 1 Then
            _month = "0" + _month
        End If
        Return _date.Year.ToString + "/" + _month + "/" + _day

    End Function

    Protected Sub SendEmailToManagement_PaymentsSinceJan()

        If LocalTime.GetTime.DayOfWeek.ToString = "Monday" Then
            If LocalTime.GetTime.Hour = 5 Then
                If LocalTime.GetTime.Minute >= 54 AndAlso LocalTime.GetTime.Minute <= 59 Then

                    Dim zoneId As String = "Russian Standard Time"
                    Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
                    Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)
                    result = result.AddDays(-1)

                    Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                    RenderReport.Render("disc", ReportViewerSendEmailToManagement_PaymentsSinceJan, "PaymentsSinceJANbyProject",
                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        "DailyReportToPatrickRev/PaymentsSinceJANbyProject " + result.ToString("dd-MM-yyyy") + UniqueString1 + ".xls")

                    Dim UniqueString2 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                    RenderReport.Render("disc", ReportViewerSendEmailToManagement_PaymentsSinceJan, "PaymentsSinceJANbySupplier",
                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        "DailyReportToPatrickRev/PaymentsSinceJANbySupplier " + result.ToString("dd-MM-yyyy") + UniqueString2 + ".xls")

                    ' SEND EMAIL
                    Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                    con.Open()
                    Dim sqlstring As String = " SELECT rtrim(LoweredEmail) FROM [aspnet_Users] inner join aspnet_Membership on aspnet_Users.UserId = aspnet_Membership.UserId where SendEmailPaymentsSinceJan = 1 "

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
                    mail.Bcc.Add("sk@mercuryeng.ru")

                    Dim pathProjectBy As String = Server.MapPath("~/DailyReportToPatrickRev/PaymentsSinceJANbyProject " + result.ToString("dd-MM-yyyy") + UniqueString1 + ".xls")
                    Dim pathSupplierBy As String = Server.MapPath("~/DailyReportToPatrickRev/PaymentsSinceJANbySupplier " + result.ToString("dd-MM-yyyy") + UniqueString2 + ".xls")

                    Dim file As System.IO.FileInfo = New System.IO.FileInfo(pathProjectBy)
                    If file.Exists Then
                        mail.Attachments.Add(New Attachment(pathProjectBy))
                    End If

                    Dim fileToChart As System.IO.FileInfo = New System.IO.FileInfo(pathSupplierBy)
                    If fileToChart.Exists Then
                        mail.Attachments.Add(New Attachment(pathSupplierBy))
                    End If

                    mail.Subject = "Daily Report From PTS " + result.ToString("dd-MM-yyyy")
                    mail.Body = "<span style=" + """" + " font-size: 11px;	color: #FF0000;	font-style: italic;	padding: 5px;	margin: 3px;	border-style: dotted;	border-width: thin; background-color: #FFFFCC;" + """" + "> Auto Generated Email From PTS </span><br/><hr/> " + _
                      "Dear All," + "<br/>" + _
                      "<br/>" + _
                      "Please see attached file which represents payments to Projects and Suppliers respectively." + "<br/>" + _
                      "<br/>" + _
                      "Regards." + "<br/>" + _
                      "<br/>" + _
                      "Savas"

                    mail.IsBodyHtml = True

                    smtpZ = New System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings("MailServerHost").ToString)
                    smtpZ.Credentials = New System.Net.NetworkCredential("savas.karaduman@mercuryeng.ru", "thee8Coh")
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

    Private Sub CreateExcelFileForThisUserThenSaveToDiskContract(FileToPath As String)

        Dim oStringWriter As New System.IO.StringWriter()
        Dim oHtmlTextWriter As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(oStringWriter)
        Dim objStreamWriter As StreamWriter = File.AppendText(Server.MapPath(FileToPath))

        GridViewContractNotification.RenderControl(oHtmlTextWriter)

        objStreamWriter.WriteLine(oStringWriter.ToString())
        objStreamWriter.Close()

    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Exit Sub
    End Sub

End Class

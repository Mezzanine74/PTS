Imports System.Net
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms
Imports Microsoft.ReportingServices
Imports System.Data.SqlClient
Imports System.Net.Mail

Partial Class DeliveryPackingListEmail
    Inherits System.Web.UI.Page
    Protected Function GetStartDateAsString() As String

        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        Dim StartDate As DateTime = result
        ' Reduce one day, today may be Sunday
        If StartDate.DayOfWeek.ToString.ToLower = "sunday" Then
            StartDate = StartDate.AddDays(-1)
        End If
        While StartDate.DayOfWeek.ToString.ToLower <> "sunday"
            StartDate = StartDate.AddDays(-1)
        End While

        Dim StartDateToParameter As String = StartDate.Year.ToString + "/" + StartDate.Month.ToString + "/" + StartDate.Day.ToString

        Return StartDateToParameter

    End Function

    Protected Function GetFinishDateAsString() As String

        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        Dim FinishDate As DateTime = result
        While FinishDate.DayOfWeek.ToString.ToLower <> "monday"
            FinishDate = FinishDate.AddDays(1)
        End While

        Dim FinishDateToParameter As String = FinishDate.Year.ToString + "/" + FinishDate.Month.ToString + "/" + FinishDate.Day.ToString

        Return FinishDateToParameter

    End Function


    Public Sub RenderReport_()

        Dim _personentered As String = ""
        If Not IsPostBack Then
            _personentered = Page.User.Identity.Name.ToString
        Else
            If DropDownListUserName.SelectedIndex <> 0 Then
                _personentered = DropDownListUserName.SelectedValue.ToString
            ElseIf DropDownListUserName.SelectedIndex = 0 Then
                _personentered = Page.User.Identity.Name.ToString
            End If
        End If

        RenderReport.Render("html", ReportViewerDeliveryReport, "DeliveryPackingListRev1", "PersonEntered", _personentered, "EnteredByStartOfWeek", GetStartDateAsString, "EnteredByEndOfWeek", GetFinishDateAsString)

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack AndAlso Page.User.Identity.Name.ToLower <> "savas" _
                                   AndAlso Page.User.Identity.Name.ToLower <> "mariya" AndAlso Page.User.Identity.Name.ToLower <> "nadezhda.klochkova" Then
            DropDownListUserName.Visible = False
        End If

        If Not IsPostBack Or IsPostBack Then
            RenderReport_()
            SendEmailToAll()
        End If

        Response.Write(GetFinishDateAsString)

    End Sub

    Protected Sub DropDownListUserName_DataBound(sender As Object, e As System.EventArgs) Handles DropDownListUserName.DataBound
        Dim lst As New ListItem("Select User", "")
        DropDownListUserName.Items.Insert(0, lst)
    End Sub

    Protected Sub Button_Test_Click(sender As Object, e As System.EventArgs) Handles Button_Test.Click
        sendEmailToAll()
    End Sub

    Protected Sub SendEmailToAll()

        If LocalTime.GetTime.DayOfWeek.ToString.ToLower = "sunday" Then
            If LocalTime.GetTime.Hour = 22 Then
                If LocalTime.GetTime.Minute >= 54 AndAlso LocalTime.GetTime.Minute <= 59 Then

                    ' Start To Check Who Enters documents this week
                    Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                    con.Open()
                    Dim sqlstring As String = " SELECT     RTRIM(DataSource_PersonEnteredThisWeek.PersonEntered) AS PersonEntered, RTRIM(dbo.aspnet_Membership.LoweredEmail) AS email " + _
                "  FROM         dbo.aspnet_Membership INNER JOIN " + _
                "                        dbo.aspnet_Users ON dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId AND  " + _
                "                        dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId INNER JOIN " + _
                "                            ( " + _
                " 								SELECT PersonEntered FROM( " + _
                " 									SELECT     PersonEntered " + _
                " 									FROM         (SELECT     PersonCreated AS PersonEntered " + _
                " 														   FROM          dbo.Table_PO_Nakladnaya " + _
                " 														   WHERE      (CreatedBy > @StartDate) AND (CreatedBy < @FinishDate) " + _
                " 														   GROUP BY PersonCreated " + _
                " 														   HAVING      (PersonCreated <> N'savas') " + _
                " 														   UNION ALL " + _
                " 														   SELECT     PersonCreated AS PersonEntered " + _
                " 														   FROM         dbo.Table_PO_Akt " + _
                " 														   WHERE     (CreatedBy > @StartDate) AND (CreatedBy < @FinishDate) " + _
                " 														   GROUP BY PersonCreated " + _
                " 														   HAVING      (PersonCreated <> N'savas')) AS Datasource1 " + _
                " 									GROUP BY PersonEntered " + _
                " 								) AS Datasource_ " + _
                "                          ) AS DataSource_PersonEnteredThisWeek ON   " + _
                "                        dbo.aspnet_Users.UserName = DataSource_PersonEnteredThisWeek.PersonEntered  "

                    Dim cmd As New SqlCommand(sqlstring, con)
                    cmd.CommandType = System.Data.CommandType.Text

                    'syntax for parameter adding
                    Dim StartDate As SqlParameter = cmd.Parameters.Add("@StartDate", System.Data.SqlDbType.NVarChar, 50)
                    StartDate.Value = GetStartDateAsString()
                    Dim FinishDate As SqlParameter = cmd.Parameters.Add("@FinishDate", System.Data.SqlDbType.NVarChar, 50)
                    FinishDate.Value = GetFinishDateAsString()

                    Dim dr As SqlDataReader = cmd.ExecuteReader

                    Dim mail As System.Net.Mail.MailMessage = Nothing
                    Dim smtpZ As System.Net.Mail.SmtpClient = Nothing

                    mail = New System.Net.Mail.MailMessage()

                    mail.From = New System.Net.Mail.MailAddress("procurement@mercuryeng.ru", "PTS")
                    mail.To.Add("anna.stolyarova@ mercuryeng.ru")
                    mail.CC.Add("elena.kuznetsova@asteros.ru")
                    mail.To.Add("stanislav.malutin@mercuryeng.ru")
                    mail.Bcc.Add("savas.karaduman@mercuryeng.ru")

                    While dr.Read
                        mail.To.Add(dr(1).ToString)
                        Dim FileToPath As String = ProduceExcelFilePathForThisUser(dr(0).ToString)
                        CreateExcelFileForThisUserThenSaveToDisk(dr(0).ToString, FileToPath)

                        Dim path As String = Server.MapPath(FileToPath)
                        Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
                        If file.Exists Then
                            mail.Attachments.Add(New Attachment(path))
                        End If

                    End While

                    mail.Subject = "Сводные документы доставки " + GetStartDateAsString() + " <> " + GetFinishDateAsString()
                    mail.Body = "<span style=" + """" + " font-size: 11px;	color: #FF0000;	font-style: italic;	padding: 5px;	margin: 3px;	border-style: dotted;	border-width: thin; background-color: #FFFFCC;" + """" + "> Автоматическая электронная почта с ПТС </span><br/><hr/> " +
                      "Уважаемые Anna Stolyarova," + "<br/>" +
                      "<br/>" +
                      "Упакованный список документов доставки, которые вводятся в ПТС на прошлой неделе, прилагается." + "<br/>" +
                      "Пожалуйста, проверьте и сообщите нам, если у вас есть все оригиналы." + "<br/>" +
                      "<br/>" +
                      "Regards." + "<br/>" +
                      "<br/>" +
                      "ПТС"

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

                End If
            End If
        End If

    End Sub

    Protected Function ProduceExcelFilePathForThisUser(ByVal UserName_ As String) As String
        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        Dim strFilePathPendingList As String = String.Empty

        Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
        strFilePathPendingList = "~/DeliveryPackingLists/DeliveryPackingListBy_" + UserName_ + "_" + _
          " " + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + _
          "-" + Mid(result.ToString, 1, 2).ToString + " _" + UniqueString1 + ".xls"
        Return (strFilePathPendingList)
    End Function

    Protected Sub CreateExcelFileForThisUserThenSaveToDisk(ByVal UserName_ As String, FilePath_ As String)
        ReportViewerDeliveryReport.Visible = True
        ReportViewerDeliveryReport.Reset()
        ReportViewerDeliveryReport.ServerReport.ReportPath = ConfigurationManager.AppSettings("ReportPath").ToString + "DeliveryPackingListRev1"
        ReportViewerDeliveryReport.ServerReport.ReportServerUrl = New System.Uri(ConfigurationManager.AppSettings("ReportServerURL").ToString)
        ReportViewerDeliveryReport.ServerReport.ReportServerCredentials = New ReportCredentials

        Dim myparams As New List(Of ReportParameter)
        Dim myparameter As ReportParameter

        myparameter = New ReportParameter("PersonEntered", UserName_)
        myparams.Add(myparameter)
        myparameter = New ReportParameter("EnteredByStartOfWeek", GetStartDateAsString)
        myparams.Add(myparameter)
        myparameter = New ReportParameter("EnteredByEndOfWeek", GetFinishDateAsString)
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

End Class

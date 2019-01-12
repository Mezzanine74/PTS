Imports Microsoft.VisualBasic
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Web.HttpContext
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Data
Imports System.Web.UI.WebControls
Imports System.Configuration
Imports System.Web
Imports System.Web.UI
Imports System.Text.RegularExpressions
Imports PTS_App_Code_VB_Project.PTS_MERCURY.helper.EmailGenerator

Public Class MyCommonTasks

    Public Sub HoverEffectOnGridviewCells(ByVal GridviewToHighlight As GridView, ByVal RowToHighlight As GridViewRow)
        'If RowToHighlight.RowType = DataControlRowType.DataRow Then
        '  For i = 0 To GridviewToHighlight.Columns.Count - 1
        '    RowToHighlight.Cells(i).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F0F8FF';")
        '    RowToHighlight.Cells(i).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
        '  Next
        'End If
    End Sub

    Public Sub uploadFileUsingFTP(ByVal CompleteFTPPath As String, ByVal CompleteLocalPath As String, _
                                  Optional ByVal UName As String = "", Optional ByVal PWD As String = "")
        'Create a FTP Request Object and Specfiy a Complete Path 
        Dim reqObj As FtpWebRequest = WebRequest.Create(CompleteFTPPath)
        'Call A FileUpload Method of FTP Request Object
        reqObj.Method = WebRequestMethods.Ftp.UploadFile
        'If you want to access Resourse Protected You need to give User Name      and PWD
        reqObj.Credentials = New NetworkCredential(UName, PWD)
        'FileStream object read file from Local Drive
        Dim streamObj As FileStream = File.OpenRead(CompleteLocalPath)
        'Store File in Buffer
        Dim buffer(streamObj.Length) As Byte
        'Read File from Buffer
        streamObj.Read(buffer, 0, buffer.Length)
        'Close FileStream Object Set its Value to nothing
        streamObj.Close()
        streamObj = Nothing
        'Upload File to ftp://localHost/ set its object to nothing
        reqObj.GetRequestStream().Write(buffer, 0, buffer.Length)
        reqObj = Nothing
    End Sub

    Public Sub DownloadFileUsingFTP(ByVal FileToUpload As String, ByVal FileFromDownload As String, _
                                    ByVal username As String, ByVal password As String)

        Dim ftp As System.Net.FtpWebRequest = _
        CType(FtpWebRequest.Create(FileFromDownload), FtpWebRequest)
        ftp.Credentials = New  _
            System.Net.NetworkCredential(username, password)
        ftp.KeepAlive = False
        'we want a binary transfer, not textual data
        ftp.UseBinary = True
        ftp.Method = System.Net.WebRequestMethods.Ftp.DownloadFile
        Using response As System.Net.FtpWebResponse = _
              CType(ftp.GetResponse, System.Net.FtpWebResponse)
            Using responseStream As IO.Stream = response.GetResponseStream
                'loop to read & write to file
                Using fs As New IO.FileStream(FileToUpload, IO.FileMode.Create)
                    Dim buffer(2047) As Byte
                    Dim read As Integer = 0
                    Do
                        read = responseStream.Read(buffer, 0, buffer.Length)
                        fs.Write(buffer, 0, read)
                    Loop Until read = 0
                    responseStream.Close()
                    fs.Flush()
                    fs.Close()
                End Using
                responseStream.Close()
            End Using
            response.Close()
        End Using
    End Sub

    Public Function CheckFileExistOnFTP(ByVal FileName As String, _
                                        ByVal username As String, ByVal password As String) As Boolean

        Dim request = DirectCast(WebRequest.Create(FileName), FtpWebRequest)
        request.Credentials = New NetworkCredential(username, password)
        request.Method = WebRequestMethods.Ftp.GetDateTimestamp
        Dim FileExist As Boolean = True
        Try
            Dim response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
        Catch ex As WebException
            Dim response As FtpWebResponse = DirectCast(ex.Response, FtpWebResponse)
            If response.StatusCode = FtpStatusCode.ActionNotTakenFileUnavailable Then
                'Does not exist     
                FileExist = False
            End If
        End Try
        Return FileExist
    End Function

    Public Sub OpenPDF(ByVal PathToFile As String)
        ' check file if exist on Local
        Dim path As String = Current.Server.MapPath(PathToFile)
        Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
        If file.Exists Then
            Current.Response.Clear()
            Current.Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
            Current.Response.AddHeader("Content-Length", file.Length.ToString())
            Current.Response.ContentType = "application/octet-stream"
            Current.Response.WriteFile(file.FullName)
            Current.Response.End()
            'Current.Response.Write("find in local file")
        Else
            ' try to find it on FTP server
            'Dim username As String = "savas"
            'Dim password As String = "akdeniz7"
            Dim username As String = "savas.karaduman"
            Dim password As String = "PTS_Admin_Password"

            Dim FileOnFTP As String = ""
            Dim IfFileExistOnFTP As New Boolean
            If Len(PathToFile) = 0 OrElse String.IsNullOrEmpty(PathToFile) Then
                FileOnFTP = "Empty"
                IfFileExistOnFTP = False
            Else
                'FileOnFTP = "ftp://174.120.207.3/mezzanine74.org/wwwroot/" + _
                '           Mid(PathToFile, 2, Len(PathToFile) - 1)
                FileOnFTP = "ftp://pts.mercuryeng.ru/" + _
                           Mid(PathToFile, 2, Len(PathToFile) - 1)
                IfFileExistOnFTP = CheckFileExistOnFTP(FileOnFTP, username, password)
            End If

            If IfFileExistOnFTP = True Then
                'Current.Response.Write(">file find in FTP server")
                ' file exist on FTP
                ' upload file from FTP to local
                Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                Dim UniqueString2 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                Dim FileToUpload As String = Current.Server.MapPath("~/REQUEST/TEMPORARY/" + UniqueString1 + UniqueString2 + Right(PathToFile, 4))
                DownloadFileUsingFTP(FileToUpload, FileOnFTP, username, password)

                'Current.Response.Write(">file downloaded from FTP server")

                ' check downloaded FTP file 
                Dim fileToCheck As System.IO.FileInfo = New System.IO.FileInfo(Current.Server.MapPath("~/REQUEST/TEMPORARY/" + UniqueString1 + UniqueString2 + Right(PathToFile, 4)))
                If fileToCheck.Exists Then
                    ' open download dialog box for this file
                    Current.Response.Clear()
                    Current.Response.AddHeader("Content-Disposition", "attachment; filename=" & fileToCheck.Name)
                    Current.Response.AddHeader("Content-Length", fileToCheck.Length.ToString())
                    Current.Response.ContentType = "application/octet-stream"
                    Current.Response.WriteFile(fileToCheck.FullName)
                    Current.Response.End()
                    'Current.Response.Write(">file sent to client by dialog box from local server")
                Else
                    'Current.Response.Write(">file can not be found on local server")
                End If
            Else
                'Current.Response.Write("file doesnt exist nowhere")
            End If
        End If
    End Sub

    Public Sub DeleteAllFileOnLocalOnFTP(ByVal PathToFile As String)

        ' Delete local file
        Dim path As String = Current.Server.MapPath(PathToFile)
        Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
        If file.Exists Then
            System.IO.File.Delete(path)
        End If

        Try
            ' Delete FTP file
            Dim FileOnFTP As String = ""
            FileOnFTP = "ftp://pts.mercuryeng.ru/" + _
                      Mid(PathToFile, 2, Len(PathToFile) - 1)

            Dim ftpReq As FtpWebRequest = WebRequest.Create(FileOnFTP)
            ftpReq.Method = WebRequestMethods.Ftp.DeleteFile
            ftpReq.Credentials = New NetworkCredential("savas.karaduman", "PTS_Admin_Password")

            Dim ftpResp As FtpWebResponse = ftpReq.GetResponse
            'MsgBox(ftpResp.StatusDescription)
        Catch ex As Exception
            'MsgBox(ex.ToString)
        End Try

    End Sub

    Public Sub GoToReport(ByVal username As String, ByVal password As String, ByVal URL As String)
        Current.Response.Redirect("http://mezzanine74.org/login.aspx?username=" + _
                          username + _
                          "&password=" + _
                          password + _
                          "&ReturnUrl=%2f" + URL)
    End Sub

    Public Sub SendNotification(ByVal Notification As String, ByVal projectID As String, ByVal NotificationType As Integer)
        Try
            ' Add Notifications
            Dim zoneId As String = "Russian Standard Time"
            Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
            Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
            con.Open()
            Dim sqlstring As String = " INSERT INTO [Table_Notification] " + _
                   " ([Notification] " + _
                   " ,[ProjectID] " + _
                   " ,[TimeStamp] " + _
                   " ,[NotificationType]) " + _
            " VALUES " + _
                   " ( @Notification " + _
                   " ," + projectID + "" + _
                   " ," + "'" + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + "-" + Mid(result.ToString, 1, 2).ToString + " " + Mid(result.ToString, 12, 2).ToString + ":" + Mid(result.ToString, 15, 2).ToString + ":" + Mid(result.ToString, 18, 2).ToString + "'" + "" + _
                   " ," + NotificationType.ToString + ")"

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            Dim UserParm As SqlParameter = cmd.Parameters.Add("@Notification", Data.SqlDbType.NVarChar, 500)
            UserParm.Value = Notification


            Dim dr As SqlDataReader = cmd.ExecuteReader
            con.Close()
            dr.Close()

        Catch ex As Exception

        End Try
    End Sub

    Public Sub SendEmailForContract(ByVal ProjectID As Integer, ByVal Notification As String, Optional ByVal _Subject As String = Nothing)

        ' Process notification at the very beginning

        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)

            con.Open()
            Dim sqlstring As String = "SELECT     RTRIM(dbo.aspnet_Membership.Email) AS Email " + _
        " FROM         dbo.aspnet_Membership INNER JOIN " + _
        "                       dbo.aspnet_Users ON dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId INNER JOIN " + _
        "                       dbo.Table_Prj_User_Junction ON dbo.aspnet_Users.UserId = dbo.Table_Prj_User_Junction.UserID INNER JOIN " + _
        "                       dbo.Table1_Project ON dbo.Table_Prj_User_Junction.ProjectID = dbo.Table1_Project.ProjectID " + _
        " WHERE     (RTRIM(dbo.Table1_Project.ProjectID) = @ProjectID) AND (dbo.aspnet_Users.SendEmailContract = 1)  "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            Dim Project_ID As SqlParameter = cmd.Parameters.Add("@ProjectID", Data.SqlDbType.Int)

            Project_ID.Value = ProjectID
            Dim dr As SqlDataReader = cmd.ExecuteReader

            Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "Mercury Russia Procurement")
            Dim mm1 As New MailMessage()
            mm1.From = MailFrom

            While dr.Read
                If dr(0).ToString.Length <> 0 Then
                    mm1.Bcc.Add(dr(0).ToString)
                End If
            End While

            If ProjectID = 999 Then
                ' it is Client. So copy to Finance people

                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
                    Dim cmd1 As New SqlCommand()
                    cmd1.Connection = conn

                    conn.Open()

                    cmd1.CommandType = CommandType.Text
                    cmd1.CommandText = " SELECT rtrim(LoweredEmail) AS Email FROM [dbo].[aspnet_Users] INNER JOIN aspnet_Membership ON aspnet_Membership.UserId = [aspnet_Users].UserId where [SendEmailClientContracts] = 1 "

                    Dim dr1 As SqlDataReader = cmd1.ExecuteReader

                    While dr1.Read
                        mm1.Bcc.Add(dr1(0).ToString)
                    End While

                    dr1.Close()
                    conn.Dispose()

                End Using

            End If

            dr.Close()

            If _Subject = Nothing Then
                mm1.Subject = "No Subject"
            Else
                mm1.Subject = _Subject
            End If

            mm1.Body = Notification.Replace("<a href=" + """", "<a href=" + """" + Current.Request.Url.Host.ToLower())
            mm1.IsBodyHtml = True

            Dim smtp As New SmtpClient_RussianEncoded
            Try
                smtp.Send(mm1)
            Catch ex As Exception
            End Try
            con.Close()
        End Using
    End Sub

    Public Sub SendEmailToAdmin(ByVal Subject As String, ByVal Body As String, Optional ByVal Attachment As Attachment = Nothing, Optional ByVal CopyToGmail As Boolean = False)

        Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "Mercury Russia Procurement")
        Dim mm1 As New MailMessage()
        mm1.From = MailFrom
        'mm1.To.Add("savas.erzin@gmail.com")
        mm1.To.Add("savas.karaduman@mercuryeng.ru")

        If CopyToGmail = True Then
            mm1.To.Add("savas.erzin@gmail.com")
        End If

        If Attachment IsNot Nothing Then
            mm1.Attachments.Add(Attachment)
        End If

        mm1.Subject = Subject
        mm1.Body = Body
        mm1.IsBodyHtml = True

        Dim smtp As New SmtpClient_RussianEncoded
        Try
            smtp.Send(mm1)
        Catch ex As Exception
        End Try

    End Sub

    Public Sub UpdateExchangeRate()
        Exit Sub
        Try
            ' it finds max available date in the website as MaxDate
            Dim strURL0 As String = "http://cbr.ru/eng/daily.aspx"
            Dim objWebRequest0 As System.Net.HttpWebRequest
            Dim objWebResponse0 As System.Net.HttpWebResponse
            Dim streamReader0 As System.IO.StreamReader
            Dim strHTML0 As String
            Dim MaxDate As String

            objWebRequest0 = CType(System.Net.WebRequest.Create(strURL0), System.Net.HttpWebRequest)
            objWebRequest0.Method = "GET"
            objWebResponse0 = CType(objWebRequest0.GetResponse(), System.Net.HttpWebResponse)

            streamReader0 = New System.IO.StreamReader(objWebResponse0.GetResponseStream)
            strHTML0 = streamReader0.ReadToEnd

            Dim intPos001, intPos002 As Integer

            intPos001 = strHTML0.IndexOf("from", 0)
            intPos002 = strHTML0.IndexOf("from", intPos001 + 4)
            MaxDate = strHTML0.Substring(intPos002 + 10, 10)

            streamReader0.Close()
            objWebResponse0.Close()
            objWebRequest0.Abort()

            ' it provides all missing dates to sqlreader for rates
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
            con.Open()
            Dim sqlstring As String = "SELECT dbo.Table9_Date.Date AS MissingDate FROM dbo.Table8_ExchangeRates RIGHT OUTER JOIN dbo.Table9_Date ON dbo.Table8_ExchangeRates.Date = dbo.Table9_Date.Date WHERE (dbo.Table9_Date.Date <= @Date) AND (dbo.Table8_ExchangeRates.Date IS NULL)"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text
            Dim MaximumDate As SqlParameter = cmd.Parameters.Add("@Date", Data.SqlDbType.SmallDateTime, 10)
            MaximumDate.Value = Mid(MaxDate, 1, 2) + "/" + Mid(MaxDate, 4, 2) + "/" + Mid(MaxDate, 7, 4)
            Dim dr As SqlDataReader = cmd.ExecuteReader

            ' this part loops all missing date and insert all of them into Rate table
            While dr.Read
                If dr(0).ToString.Length <> 0 Then
                    Dim strURL As String = "http://cbr.ru/eng/currency_base/daily.aspx?C_month=" + Mid((dr(0).ToString), 4, 2) + "&C_year=" + Mid((dr(0).ToString), 7, 4) + "&date_req=" + Mid((dr(0).ToString), 1, 2) + "." + Mid((dr(0).ToString), 4, 2) + "." + Mid((dr(0).ToString), 7, 4)
                    Dim objWebRequest As System.Net.HttpWebRequest
                    Dim objWebResponse As System.Net.HttpWebResponse
                    Dim streamReader As System.IO.StreamReader
                    Dim strHTML As String
                    Dim RateDollar As String
                    Dim RateEuro As String


                    objWebRequest = CType(System.Net.WebRequest.Create(strURL), System.Net.HttpWebRequest)
                    objWebRequest.Method = "GET"
                    objWebResponse = CType(objWebRequest.GetResponse(), System.Net.HttpWebResponse)

                    streamReader = New System.IO.StreamReader(objWebResponse.GetResponseStream)
                    strHTML = streamReader.ReadToEnd

                    Dim intPos1, intPos2, intPos3 As Integer

                    intPos1 = strHTML.IndexOf("US Dollar", 0)
                    intPos2 = strHTML.IndexOf("align=", intPos1)
                    intPos3 = strHTML.IndexOf("</td>", intPos2)
                    RateDollar = strHTML.Substring(intPos3 - 7, 7)

                    Dim intPos01, intPos02, intPos03 As Integer
                    intPos01 = strHTML.IndexOf("Euro", 0)
                    intPos02 = strHTML.IndexOf("align", intPos01)
                    intPos03 = strHTML.IndexOf("</td>", intPos02)
                    RateEuro = strHTML.Substring(intPos03 - 7, 7)

                    'it provides INSERT query for missing dates
                    Dim con2 As New SqlConnection(ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
                    con2.Open()
                    Dim sqlstringInsert As String = "INSERT INTO [dbo].[Table8_ExchangeRates] ([Date],[RubbleDollar],[RubbleEuro]) VALUES (@Date,@RubbleDollar,@RubbleEuro)"
                    Dim cmd2 As New SqlCommand(sqlstringInsert, con2)
                    cmd2.CommandType = Data.CommandType.Text
                    Dim MissingDate As SqlParameter = cmd2.Parameters.Add("@Date", Data.SqlDbType.SmallDateTime, 10)
                    Dim RubleDollar As SqlParameter = cmd2.Parameters.Add("@RubbleDollar", Data.SqlDbType.Decimal, 5)
                    Dim RubleEuro As SqlParameter = cmd2.Parameters.Add("@RubbleEuro", Data.SqlDbType.Decimal, 5)

                    MissingDate.Value = Mid((dr(0).ToString), 1, 2) + "/" + Mid((dr(0).ToString), 4, 2) + "/" + Mid((dr(0).ToString), 7, 4)
                    RubleDollar.Value = Convert.ToDecimal(RateDollar)
                    RubleEuro.Value = Convert.ToDecimal(RateEuro)

                    cmd2.ExecuteNonQuery()
                    con2.Close()

                    streamReader.Close()
                    objWebResponse.Close()
                    objWebRequest.Abort()

                End If
            End While
            con.Close()
        Catch asd As Exception
            SendEmailToAdmin("UpdateExchangeRateError", asd.Message)
        End Try
    End Sub

    Public Sub UpdateExchangeRateRevision()

        Try

            ' it finds max available date in the website as MaxDate
            Dim strURL0 As String = "http://cbr.ru/eng/daily.aspx"
            Dim objWebRequest0 As System.Net.HttpWebRequest
            Dim objWebResponse0 As System.Net.HttpWebResponse
            Dim streamReader0 As System.IO.StreamReader
            Dim strHTML0 As String
            Dim MaxDate As String

            objWebRequest0 = CType(System.Net.WebRequest.Create(strURL0), System.Net.HttpWebRequest)

            Dim cookieContainer As New CookieContainer()
            objWebRequest0.CookieContainer = cookieContainer

            objWebRequest0.Method = "GET"
            objWebResponse0 = CType(objWebRequest0.GetResponse(), System.Net.HttpWebResponse)

            streamReader0 = New System.IO.StreamReader(objWebResponse0.GetResponseStream)
            strHTML0 = streamReader0.ReadToEnd

            Dim intPos001, intPos002 As Integer

            intPos001 = strHTML0.IndexOf("/eng/currency_base/daily.aspx?date_req=", 0)
            intPos002 = strHTML0.IndexOf("/eng/currency_base/daily.aspx?date_req=", intPos001 + 39)
            MaxDate = strHTML0.Substring(intPos002 + 39, 10)

            streamReader0.Close()
            objWebResponse0.Close()
            objWebRequest0.Abort()

            '' it provides all missing dates to sqlreader for rates
            'Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
            'con.Open()
            'Dim sqlstring As String = "SELECT dbo.Table9_Date.Date AS MissingDate FROM dbo.Table8_ExchangeRates RIGHT OUTER JOIN dbo.Table9_Date ON dbo.Table8_ExchangeRates.Date = dbo.Table9_Date.Date WHERE (dbo.Table9_Date.Date <= @Date) AND (dbo.Table8_ExchangeRates.Date IS NULL)"
            'Dim cmd As New SqlCommand(sqlstring, con)
            'cmd.CommandType = Data.CommandType.Text
            'Dim MaximumDate As SqlParameter = cmd.Parameters.Add("@Date", Data.SqlDbType.SmallDateTime, 10)
            'MaximumDate.Value = Mid(MaxDate, 1, 2) + "/" + Mid(MaxDate, 4, 2) + "/" + Mid(MaxDate, 7, 4)
            'Dim dr As SqlDataReader = cmd.ExecuteReader

            Using adapter As New ExchangeRatesFeedTableAdapters.Table9_DateTableAdapter

                Dim table As New ExchangeRatesFeed.Table9_DateDataTable

                table = adapter.GetData(Mid(MaxDate, 7, 4) + "-" + Mid(MaxDate, 4, 2) + "-" + Mid(MaxDate, 1, 2))

                For Each _row As ExchangeRatesFeed.Table9_DateRow In table
                    ' single value returns
                    If _row.MissingDate.ToString.Length <> 0 Then

                        Dim _year As String = _row.MissingDate.Year.ToString.Trim
                        Dim _month As String = _row.MissingDate.Month.ToString.Trim
                        Dim _day As String = _row.MissingDate.Day.ToString.Trim

                        If _month.Length = 1 Then
                            _month = "0" + _month
                        End If

                        If _day.Length = 1 Then
                            _day = "0" + _day
                        End If

                        Dim strURL As String = "http://cbr.ru/eng/currency_base/daily.aspx?date_req=" + _day + "." + _month + "." + _year
                        Dim objWebRequest As System.Net.HttpWebRequest
                        Dim objWebResponse As System.Net.HttpWebResponse
                        Dim streamReader As System.IO.StreamReader
                        Dim strHTML As String
                        Dim RateDollar As String
                        Dim RateEuro As String

                        objWebRequest = CType(System.Net.WebRequest.Create(strURL), System.Net.HttpWebRequest)
                        objWebRequest.Method = "GET"
                        objWebResponse = CType(objWebRequest.GetResponse(), System.Net.HttpWebResponse)

                        streamReader = New System.IO.StreamReader(objWebResponse.GetResponseStream)
                        strHTML = streamReader.ReadToEnd

                        Dim intPos1, intPos2, intPos3 As Integer

                        intPos1 = strHTML.IndexOf("US Dollar", 0)
                        intPos2 = strHTML.IndexOf("align=", intPos1)
                        intPos3 = strHTML.IndexOf("</td>", intPos2)
                        RateDollar = strHTML.Substring(intPos3 - 7, 7)

                        Dim intPos01, intPos02, intPos03 As Integer
                        intPos01 = strHTML.IndexOf("Euro", 0)
                        intPos02 = strHTML.IndexOf("align", intPos01)
                        intPos03 = strHTML.IndexOf("</td>", intPos02)
                        RateEuro = strHTML.Substring(intPos03 - 7, 7)

                        'it provides INSERT query for missing dates
                        'Dim con2 As New SqlConnection(ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
                        'con2.Open()
                        'Dim sqlstringInsert As String = "INSERT INTO [dbo].[Table8_ExchangeRates] ([Date],[RubbleDollar],[RubbleEuro]) VALUES (@Date,@RubbleDollar,@RubbleEuro)"
                        'Dim cmd2 As New SqlCommand(sqlstringInsert, con2)
                        'cmd2.CommandType = Data.CommandType.Text
                        'Dim MissingDate As SqlParameter = cmd2.Parameters.Add("@Date", Data.SqlDbType.SmallDateTime, 10)
                        'Dim RubleDollar As SqlParameter = cmd2.Parameters.Add("@RubbleDollar", Data.SqlDbType.Decimal, 5)
                        'Dim RubleEuro As SqlParameter = cmd2.Parameters.Add("@RubbleEuro", Data.SqlDbType.Decimal, 5)

                        'MissingDate.Value = Mid((dr(0).ToString), 1, 2) + "/" + Mid((dr(0).ToString), 4, 2) + "/" + Mid((dr(0).ToString), 7, 4)
                        'RubleDollar.Value = Convert.ToDecimal(RateDollar)
                        'RubleEuro.Value = Convert.ToDecimal(RateEuro)

                        'cmd2.ExecuteNonQuery()
                        'con2.Close()

                        Using adapter2 As New ExchangeRatesFeedTableAdapters.Table8_ExchangeRatesTableAdapter
                            adapter2.Insert(_day + "-" + _month + "-" + _year, Convert.ToDecimal(RateDollar), Convert.ToDecimal(RateEuro))
                            adapter2.Dispose()
                        End Using

                        streamReader.Close()
                        objWebResponse.Close()
                        objWebRequest.Abort()

                    End If
                Next

                adapter.Dispose()

            End Using

        Catch ex As Exception

        End Try


    End Sub

    Public Sub UpdateFollowUsersTable(ByVal _page As Page, ByVal _sqlDataSource As SqlDataSource)

        ' MAINTANANCE
        'If HttpContext.Current.Request.ServerVariables("URL").ToString <> "/login.aspx" Then
        '    If HttpContext.Current.User.Identity.Name.ToLower.ToString <> "savas" Then
        '        _page.Visible = False
        '        HttpContext.Current.Response.Write("maintanance")
        '        Exit Sub
        '    End If
        'End If

        If HttpContext.Current.Request.ServerVariables("URL").ToUpper.ToString.IndexOf("/L/") = 0 Then
            ' -- DONT ALLOW LIBIDO ENTRIES	
            Exit Sub
        End If

        ' ........ REFRESH LASTACTIVITYDATE ..........
        If Not _page.IsPostBack Or _page.IsPostBack Then
            ' To refresh LastActivityDate column correctly
            Dim user As String = HttpContext.Current.User.Identity.Name.ToLower

            If user Is Nothing Then
            Else
                Using cn As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                    Dim cmd As New System.Data.SqlClient.SqlCommand()
                    cmd.Connection = cn
                    cmd.CommandText = "UPDATE aspnet_Users SET LastActivityDate = @LastActivityDate WHERE UserName = @UserName"
                    cmd.Parameters.AddWithValue("@UserName", user)
                    cmd.Parameters.AddWithValue("@LastActivityDate", LocalTime.GetTime())
                    cmd.CommandType = System.Data.CommandType.Text
                    cn.Open()
                    cmd.ExecuteNonQuery()
                    cn.Close()
                    cn.Dispose()
                End Using
            End If
        End If

        ' ________ REFRESH LASTACTIVITYDATE ..........
        If Not _page.IsPostBack Then
            Try
                ' it is for User Name
                _sqlDataSource.InsertParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name.ToString

                ' it is for Page Name
                _sqlDataSource.InsertParameters("PageName").DefaultValue = HttpContext.Current.Request.ServerVariables("URL").ToString

                ' it is for Visit Time

                _sqlDataSource.InsertParameters("VisitTime").DefaultValue = LocalTime.GetTime()
                _sqlDataSource.InsertParameters("VisitTime").Type = TypeCode.DateTime

                ' it is for Ip Adress
                _sqlDataSource.InsertParameters("IpAdress").DefaultValue = HttpContext.Current.Request.ServerVariables("remote_addr")

                ' it is for BrowserType
                _sqlDataSource.InsertParameters("BrowserType").DefaultValue = HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version

                ' it is for BrowserPlatform
                If HttpContext.Current.Request.UserAgent.Contains("Windows NT 6.0") Then
                    _sqlDataSource.InsertParameters("BrowserPlatform").DefaultValue = "Vista"
                ElseIf HttpContext.Current.Request.UserAgent.Contains("Windows NT 6.1") Then
                    _sqlDataSource.InsertParameters("BrowserPlatform").DefaultValue = "Windows 7"
                Else
                    _sqlDataSource.InsertParameters("BrowserPlatform").DefaultValue = HttpContext.Current.Request.Browser.Platform
                End If

                ' it is for Country
                '_sqlDataSource.InsertParameters("Country").DefaultValue = GetCountryByIp.GetCountry(HttpContext.Current.Request.ServerVariables("remote_addr"))
                _sqlDataSource.InsertParameters("Country").DefaultValue = "-"

                'Execute insertion
                _sqlDataSource.Insert()

            Catch ex As Exception

            End Try
        End If

    End Sub

    Shared Function GetNameSurnameFromEmail(ByVal _email As String) As String

        Dim position As Integer = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(_email.ToLower).IndexOf("@")

        Return (Mid(System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(_email.ToLower), 1, position))

    End Function

    Shared Function ReturnHighlightedResult(ByVal TextBody As String, ByVal TextSearch As String) As String

        Return Regex.Replace(TextBody, TextSearch, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #FFFF00;" + """" + ">" & TextSearch & "</span>", RegexOptions.IgnoreCase)

        'Dim StartPosition As Integer
        'Dim EndPosition As Integer
        'Dim OccurenceOfSpace As Integer
        'Dim sayac As Integer = 0
        'Dim DonguBiter As Boolean = False

        'Dim StartPosition2 As Integer
        'Dim EndPosition2 As Integer
        'Dim OccurenceOfSpace2 As Integer
        'Dim sayac2 As Integer = 0
        'Dim DonguBiter2 As Boolean = False

        'If Not String.IsNullOrEmpty(TextBody) AndAlso Not String.IsNullOrEmpty(TextSearch) Then
        '    ' start logic
        '    While DonguBiter2 = False

        '        If sayac2 = 0 Then
        '            StartPosition2 = 0
        '        Else
        '            StartPosition2 = OccurenceOfSpace2
        '        End If

        '        EndPosition2 = InStr(StartPosition2 + 1, Trim(TextSearch), " ")
        '        OccurenceOfSpace2 = EndPosition2
        '        sayac2 = sayac2 + 1

        '        If EndPosition2 = 0 Then
        '            Dim TextOne As String = Mid(Trim(TextSearch), StartPosition2 + 1, Len(Trim(TextSearch)) - StartPosition2).ToString
        '            If sayac2 = 1 Then
        '                TextBody = ReturnMarkedText(sayac2, TextBody, TextOne)
        '            Else
        '                Select Case sayac2
        '                    Case 2
        '                        TextBody = ReturnMarkedText(sayac2, TextBody, TextOne)
        '                    Case 3
        '                        TextBody = ReturnMarkedText(sayac2, TextBody, TextOne)
        '                    Case 4
        '                        TextBody = ReturnMarkedText(sayac2, TextBody, TextOne)
        '                    Case 5
        '                        TextBody = ReturnMarkedText(sayac2, TextBody, TextOne)
        '                    Case 6
        '                        TextBody = ReturnMarkedText(sayac2, TextBody, TextOne)
        '                    Case 7
        '                        TextBody = ReturnMarkedText(sayac2, TextBody, TextOne)
        '                    Case 8
        '                        TextBody = ReturnMarkedText(sayac2, TextBody, TextOne)
        '                    Case 9
        '                        TextBody = ReturnMarkedText(sayac2, TextBody, TextOne)
        '                    Case 10
        '                        TextBody = ReturnMarkedText(sayac2, TextBody, TextOne)
        '                End Select
        '            End If
        '            DonguBiter2 = True
        '        Else
        '            Dim TextTwo As String = Mid(Trim(TextSearch), StartPosition2 + 1, EndPosition2 - StartPosition2 - 1).ToString
        '            If sayac2 = 1 Then
        '                TextBody = ReturnMarkedText(1, TextBody, TextTwo)
        '            Else
        '                Select Case sayac2
        '                    Case 2
        '                        TextBody = ReturnMarkedText(sayac2, TextBody, TextTwo)
        '                    Case 3
        '                        TextBody = ReturnMarkedText(sayac2, TextBody, TextTwo)
        '                    Case 4
        '                        TextBody = ReturnMarkedText(sayac2, TextBody, TextTwo)
        '                    Case 5
        '                        TextBody = ReturnMarkedText(sayac2, TextBody, TextTwo)
        '                    Case 6
        '                        TextBody = ReturnMarkedText(sayac2, TextBody, TextTwo)
        '                    Case 7
        '                        TextBody = ReturnMarkedText(sayac2, TextBody, TextTwo)
        '                    Case 8
        '                        TextBody = ReturnMarkedText(sayac2, TextBody, TextTwo)
        '                    Case 9
        '                        TextBody = ReturnMarkedText(sayac2, TextBody, TextTwo)
        '                    Case 10
        '                        TextBody = ReturnMarkedText(sayac2, TextBody, TextTwo)
        '                End Select
        '            End If
        '        End If
        '    End While
        '    sayac2 = 0
        '    DonguBiter2 = False
        'End If

        'Return TextBody

    End Function

    'Shared Function ReturnMarkedText(ByVal NumberOf As Integer, ByVal TextOf As String, ByVal TextHighlight As String) As String

    'Return Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #FFFF00;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)

    ' can be activated later for different color for each word
    'Select Case NumberOf
    '    Case -2
    '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #99FF66" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
    '    Case -1
    '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #A8FFFF" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
    '    Case 0
    '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #FFFF00" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
    '    Case 1
    '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #FFFF00;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
    '    Case 2
    '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #ADFF2F;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
    '    Case 3
    '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #FFB6C1;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
    '    Case 4
    '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #87CEFA;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
    '    Case 5
    '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #DA70D6;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
    '    Case 6
    '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #FF0000;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
    '    Case 7
    '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #8FBC8B;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
    '    Case 8
    '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #1E90FF;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
    '    Case 9
    '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #FF8C00;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
    '    Case 10
    '        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #FFD700;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
    'End Select

    'End Function

End Class

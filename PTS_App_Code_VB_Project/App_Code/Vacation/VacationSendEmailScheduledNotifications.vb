Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports PTS_App_Code_VB_Project.PTS_MERCURY.helper.EmailGenerator

Public Class VacationSendEmailScheduledNotifications

    Shared Sub SendEmail(ByVal _GridView As GridView, ByVal _SqlDataSource As SqlDataSource, ByVal _NotificationType As String)

        Select Case _NotificationType.ToLower

            Case "hrreviewedonly"
                Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                    con.Open()
                    Dim sqlstring As String = "[Vac].[SP_GetEmailsForScheduledNotification]"
                    Dim cmd As New SqlCommand(sqlstring, con)
                    cmd.CommandType = Data.CommandType.StoredProcedure

                    'syntax for parameter adding
                    Dim NotificationType As SqlParameter = cmd.Parameters.Add("@NotificationType", Data.SqlDbType.NVarChar, 256)
                    NotificationType.Value = _NotificationType

                    Dim dr As SqlDataReader = cmd.ExecuteReader

                    While dr.Read
                        Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "Vacation Notification")
                        Dim mm1 As New MailMessage()
                        mm1.From = MailFrom
                        mm1.To.Add("sk@mercuryeng.ru")
                        ' activate later
                        'mm1.To.Add(dr(1).ToString)
                        mm1.Subject = "Your vacation is pending for your approval which is already revied by HR"
                        mm1.Body = "Hello " + VacationTables.GetUserNameFromEmailAdress(dr(0).ToString) + " <br/> <br/>" + _
                            ProduceEmailBodyFromGridview(_GridView, _SqlDataSource, _NotificationType, dr(0).ToString)

                        mm1.IsBodyHtml = True
                        Dim smtp As New SmtpClient_RussianEncoded
                        smtp.Send(mm1)
                        MailFrom = Nothing

                    End While

                    con.Close()
                    dr.Close()
                End Using

            Case "noonereviewed"
                Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                    con.Open()
                    Dim sqlstring As String = "[Vac].[SP_GetEmailsForScheduledNotification]"
                    Dim cmd As New SqlCommand(sqlstring, con)
                    cmd.CommandType = Data.CommandType.StoredProcedure

                    'syntax for parameter adding
                    Dim NotificationType As SqlParameter = cmd.Parameters.Add("@NotificationType", Data.SqlDbType.NVarChar, 256)
                    NotificationType.Value = _NotificationType

                    Dim dr As SqlDataReader = cmd.ExecuteReader

                    Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "Vacation Notification")
                    Dim mm1 As New MailMessage()
                    mm1.From = MailFrom

                    While dr.Read
                        ' Activate Later
                        'mm1.To.Add(dr(0).ToString)
                    End While

                    mm1.To.Add("sk@mercuryeng.ru")
                    mm1.Subject = "These vacations requests are not reviewed by HR yet"
                    mm1.Body = "Hello " + " <br/> <br/>" + _
                        ProduceEmailBodyFromGridview(_GridView, _SqlDataSource, _NotificationType)

                    mm1.IsBodyHtml = True
                    Dim smtp As New SmtpClient_RussianEncoded
                    smtp.Send(mm1)
                    MailFrom = Nothing

                    con.Close()
                    dr.Close()
                End Using

            Case "requesterreviewed"
                Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                    con.Open()
                    Dim sqlstring As String = "[Vac].[SP_GetEmailsForScheduledNotification]"
                    Dim cmd As New SqlCommand(sqlstring, con)
                    cmd.CommandType = Data.CommandType.StoredProcedure

                    'syntax for parameter adding
                    Dim NotificationType As SqlParameter = cmd.Parameters.Add("@NotificationType", Data.SqlDbType.NVarChar, 256)
                    NotificationType.Value = _NotificationType

                    Dim dr As SqlDataReader = cmd.ExecuteReader

                    While dr.Read

                        Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "Vacation Notification")
                        Dim mm1 As New MailMessage()
                        mm1.From = MailFrom

                        ' Activate Later
                        'mm1.To.Add(dr(0).ToString)

                        mm1.To.Add("sk@mercuryeng.ru")
                        mm1.Subject = "These vacations requests are waiting for your approval"
                        mm1.Body = "Hello " + VacationTables.GetUserNameFromEmailAdress(dr(1).ToString) + " <br/> <br/>" + _
                            ProduceEmailBodyFromGridview(_GridView, _SqlDataSource, _NotificationType, dr(1).ToString)

                        mm1.IsBodyHtml = True
                        Dim smtp As New SmtpClient_RussianEncoded
                        smtp.Send(mm1)
                        MailFrom = Nothing

                    End While

                    con.Close()
                    dr.Close()
                End Using

        End Select

    End Sub

    Shared Function ProduceEmailBodyFromGridview(ByVal _GridView As GridView, ByVal _SqlDataSource As SqlDataSource, ByVal _NotificationType As String, Optional ByVal _EmplyName As String = "_") As String

        _SqlDataSource.SelectParameters("NotificationType").DefaultValue = _NotificationType
        _SqlDataSource.SelectParameters("EmployeeName").DefaultValue = _EmplyName

        _GridView.DataBind()

        Dim stringWrite As New System.IO.StringWriter()
        Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
        _GridView.RenderControl(htmlWrite)
        Return stringWrite.ToString

    End Function


End Class

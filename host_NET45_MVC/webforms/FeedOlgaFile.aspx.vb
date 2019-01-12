Imports System.IO
Imports System.Net.Mail
Imports System.Data.SqlClient
Imports PTS_App_Code_VB_Project.PTS_MERCURY.helper.EmailGenerator

Partial Class FeedOlgaFile
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click

        SendEmailToOlga()


        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        Dim strFilePathPendingList As String = String.Empty

        Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
        strFilePathPendingList = Server.MapPath("~/FeedOlgaFile/FeedDailyCashBalance") + _
          " " + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + _
          "-" + Mid(result.ToString, 1, 2).ToString + " _" + UniqueString1 + ".xls"

        Dim oStringWriter As New System.IO.StringWriter()
        Dim oHtmlTextWriter As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(oStringWriter)
        Dim objStreamWriter As StreamWriter = File.AppendText(strFilePathPendingList)

        GridViewFeedOlgaFile.RenderControl(oHtmlTextWriter)

        objStreamWriter.WriteLine(oStringWriter.ToString())
        objStreamWriter.Close()

        '(1) Create the MailMessage instance
        Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "P T S")
        Dim mm1 As New MailMessage()
        mm1.From = MailFrom

        mm1.To.Add("julia.ovseenko@mercuryeng.ru")
        mm1.To.Add("dzera.gigolaeva@mercuryeng.ru")
        mm1.To.Add("Inna.ugnivenko@mercuryeng.ru")
        mm1.To.Add("sk@mercuryeng.ru")


        mm1.Subject = "Feeding Daily Cash Balance File"

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

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Exit Sub
    End Sub

    Protected Sub SendEmailToOlga()

        If LocalTime.GetTime.DayOfWeek.ToString <> "Saturday" AndAlso LocalTime.GetTime.DayOfWeek.ToString <> "Sunday" Then
            If LocalTime.GetTime.Hour = 8 Then
                If LocalTime.GetTime.Minute >= 54 AndAlso LocalTime.GetTime.Minute <= 59 Then


                    If IsPostBack Or Not IsPostBack Then
                        Dim zoneId As String = "Russian Standard Time"
                        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
                        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

                        Dim strFilePathPendingList As String = String.Empty

                        Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                        strFilePathPendingList = Server.MapPath("~/FeedOlgaFile/FeedDailyCashBalance") + _
              " " + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + _
              "-" + Mid(result.ToString, 1, 2).ToString + " _" + UniqueString1 + ".xls"

                        Dim oStringWriter As New System.IO.StringWriter()
                        Dim oHtmlTextWriter As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(oStringWriter)
                        Dim objStreamWriter As StreamWriter = File.AppendText(strFilePathPendingList)

                        GridViewFeedOlgaFile.RenderControl(oHtmlTextWriter)

                        objStreamWriter.WriteLine(oStringWriter.ToString())
                        objStreamWriter.Close()

                        '(1) Create the MailMessage instance
                        Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "P T S")
                        Dim mm1 As New MailMessage()
                        mm1.From = MailFrom

                        mm1.To.Add("julia.ovseenko@mercuryeng.ru")
                        mm1.To.Add("dzera.gigolaeva@mercuryeng.ru")
                        mm1.To.Add("Inna.ugnivenko@mercuryeng.ru")
                        mm1.To.Add("sk@mercuryeng.ru")


                        mm1.Subject = "Feeding Daily Cash Balance File"

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
                    End If

                End If
            End If
        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        SendEmailToOlga()
        ResetPaymentListNote()
    End Sub

    Protected Sub ResetPaymentListNote()
        Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        con.Open()
        Dim sqlstring As String = " UPDATE [Table_PaymentListNotes] set [Notes] = NULL where [Id] = 1 "
        Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text
        Dim dr As SqlDataReader = cmd.ExecuteReader
        con.Close()
        dr.Close()
    End Sub
End Class

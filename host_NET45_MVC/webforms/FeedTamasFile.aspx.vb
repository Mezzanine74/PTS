Imports System.IO
Imports System.Net.Mail
Imports PTS_App_Code_VB_Project.PTS_MERCURY.helper.EmailGenerator

Partial Class FeedTamasFile
  Inherits System.Web.UI.Page

  Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
    SendEmailToTamas()
  End Sub

  Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    Exit Sub
  End Sub

  Protected Sub SendEmailToTamas()
        If LocalTime.GetTime.DayOfWeek.ToString = "Monday" Then
            If LocalTime.GetTime.Hour = 8 Then
                If LocalTime.GetTime.Minute >= 54 AndAlso LocalTime.GetTime.Minute <= 59 Then

                    If IsPostBack Or Not IsPostBack Then
                        Dim zoneId As String = "Russian Standard Time"
                        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
                        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

                        Dim strFilePathPendingList As String = String.Empty

                        strFilePathPendingList = Server.MapPath("~/FeedTamasFile/FeedTamasFile") + " " + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + "-" + Mid(result.ToString, 1, 2).ToString + ".xls"
                        Dim oStringWriter As New System.IO.StringWriter()
                        Dim oHtmlTextWriter As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(oStringWriter)
                        Dim objStreamWriter As StreamWriter = File.AppendText(strFilePathPendingList)

                        For i = 0 To GridViewFeedTamasFile.Rows.Count - 1
                            Dim row As GridViewRow = GridViewFeedTamasFile.Rows(i)
                            row.Cells(0).Attributes.Add("class", "textmode")
                        Next

                        GridViewFeedTamasFile.RenderControl(oHtmlTextWriter)

                        Dim style As String = "<style> .textmode { mso-number-format:\@; } </style>"

                        objStreamWriter.WriteLine(style)
                        objStreamWriter.WriteLine(oStringWriter.ToString())
                        objStreamWriter.Close()

                        '(1) Create the MailMessage instance
                        Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "P T S")
                        Dim mm1 As New MailMessage()
                        mm1.From = MailFrom

                        mm1.To.Add("tamas.simon@mercuryeng.ru")


                        mm1.Subject = "Feeding Tamas File"

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
    SendEmailToTamas()
  End Sub
End Class

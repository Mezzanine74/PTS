Imports System.IO
Imports System.Net.Mail
Imports System.Data.SqlClient
Imports PTS_App_Code_VB_Project.PTS_MERCURY.helper.EmailGenerator

Partial Class FeedTamasValidation
    Inherits System.Web.UI.Page

    Protected Sub ProduceFile(ByVal _FilePath As String)
        Dim oStringWriter As New System.IO.StringWriter()
        Dim oHtmlTextWriter As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(oStringWriter)
        Dim objStreamWriter As StreamWriter = File.AppendText(_FilePath)

        GridViewValidation.RenderControl(oHtmlTextWriter)

        objStreamWriter.WriteLine(oStringWriter.ToString())
        objStreamWriter.Close()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If LocalTime.GetTime.DayOfWeek.ToString <> "Saturday" AndAlso LocalTime.GetTime.DayOfWeek.ToString <> "Sunday" Then
            If LocalTime.GetTime.Hour = 5 Then
                If LocalTime.GetTime.Minute >= 54 AndAlso LocalTime.GetTime.Minute <= 59 Then

                    If GridViewValidation.Rows.Count > 0 Then
                        ' take local date
                        Dim zoneId As String = "Russian Standard Time"
                        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
                        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

                        ' start to produce excel file
                        Dim strFilePathPendingListExcel As String = String.Empty
                        Dim strFilePathPendingListHtml As String = String.Empty

                        Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")

                        strFilePathPendingListExcel = Server.MapPath("~/FeedTamasValidation/Validation") + _
                          " " + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + _
                          "-" + Mid(result.ToString, 1, 2).ToString + " _" + UniqueString1 + ".xls"

                        ProduceFile(strFilePathPendingListExcel)

                        ' prepare email
                        '(1) Create the MailMessage instance
                        Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "Cost Report Validation")
                        Dim mm1 As New MailMessage()
                        mm1.From = MailFrom

                        mm1.To.Add("roman.chernov@mercuryeng.ru")
                        mm1.To.Add("tamas.simon@mercuryeng.ru")

                        mm1.Subject = "Cost Report Validation " + result.ToString("dd/MM/yyyy")

                        mm1.Body = "<span style=" + """" + " font-size: 11px;	color: #FF0000;	font-style: italic;	padding: 5px;	margin: 3px;	border-style: dotted;	border-width: thin; background-color: #FFFFCC;" + """" + "> Auto Generated Email From PTS </span><br/><hr/> " + _
                        " There are some line items in Cost Report where Committed Cost is bigger than Updated Planned Cost. Breakdown attached."
                        mm1.IsBodyHtml = True

                        ' add attachment if exist
                        Dim pathExcel As String = strFilePathPendingListExcel
                        Dim fileToAttachExcel As System.IO.FileInfo = New System.IO.FileInfo(pathExcel)
                        If fileToAttachExcel.Exists Then
                            ' JUST ACTIVATE IF REQUIRED
                            mm1.Attachments.Add(New Attachment(pathExcel))
                        End If

                        '(3) Create the SmtpClient object
                        Dim smtp As New SmtpClient_RussianEncoded

                        smtp.Send(mm1)

                    End If

                End If
            End If
        End If

    End Sub

    'Protected Function getContent() As String
    '  Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
    '  con.Open()
    '  Dim sqlstring As String = " SELECT Notes FROM Table_PaymentListNotes WHERE id = 1"
    '  Dim cmd As New SqlCommand(sqlstring, con)
    '  cmd.CommandType =System.Data.CommandType.Text
    '  Dim dr As SqlDataReader = cmd.ExecuteReader
    '  Dim Notes As String = ""
    '  While dr.Read
    '    Notes = dr(0).ToString
    '  End While
    '  con.Close()
    '  dr.Close()
    '  Return Notes
    'End Function

    '  Protected Sub RemoveOldViewStates()
    '      Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
    '      con.Open()
    '      Dim sqlstring As String = "RemoveOldViewStates"
    '      Dim cmd As New SqlCommand(sqlstring, con)
    '      cmd.CommandType =System.Data.CommandType.StoredProcedure
    '      cmd.ExecuteNonQuery()
    '      con.Close()
    '  End Sub

    Public Overloads Overrides Sub verifyrenderinginserverform(ByVal control As Control)
        Exit Sub
    End Sub

    'Protected Sub GridViewDSP3HIDDEN_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewDSP3HIDDEN.RowCreated
    '  If e.Row.RowType = DataControlRowType.Header Then
    '    For i = 0 To e.Row.Cells.Count - 1
    '      e.Row.Cells(i).BackColor = System.Drawing.Color.Blue
    '      e.Row.Cells(i).ForeColor = System.Drawing.Color.White
    '      e.Row.Cells(i).Font.Size = 8
    '    Next
    '  End If

    '  If e.Row.RowType = DataControlRowType.DataRow Then
    '    For i = 0 To e.Row.Cells.Count - 1
    '      e.Row.Cells(i).Font.Size = 8
    '    Next
    '  End If
    'End Sub
End Class

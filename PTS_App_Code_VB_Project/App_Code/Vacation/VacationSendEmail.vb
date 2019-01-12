Imports Microsoft.VisualBasic
Imports System.Net.Mail
Imports System.Data.SqlClient
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports PTS_App_Code_VB_Project.PTS_MERCURY.helper.EmailGenerator

Public Class VacationSendEmail

    Shared Sub SendEmailNotification(ByVal _Type As String, ByVal _EmailBody As String, ByVal _RequestId As Integer, _
                                     Optional ByVal _AprPersonBeingProcessed As String = "")

        Select Case _Type.ToLower
            Case "requesttohr"
                Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "Vacation Request")
                Dim mm1 As New MailMessage()
                mm1.From = MailFrom
                mm1.Body = GetPrefixEmailBody("requesttohr", _RequestId) + _EmailBody
                ' Activate this on deployment stage
                'GetEmailAdressForNotification("requesttohr", _RequestId, mm1)
                mm1.To.Add("sk@mercuryeng.ru")
                mm1.Subject = "New vacation request from " + VacationTables.GetUserNameFromEmailAdress(VacationTables.Table_Table_RequestByID(_RequestId).EmplyName)
                mm1.IsBodyHtml = True
                Dim smtp As New SmtpClient_RussianEncoded
                smtp.Send(mm1)

            Case "hrreviewed"
                Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "Vacation Review")
                Dim mm1 As New MailMessage()
                mm1.From = MailFrom
                mm1.Body = GetPrefixEmailBody("hrreviewed", _RequestId) + _EmailBody
                ' Activate this on deployment stage
                'GetEmailAdressForNotification("hrreviewed", _RequestId, mm1)
                mm1.To.Add("sk@mercuryeng.ru")
                mm1.Subject = "Your vacation request has been reviewed by HR. Please review it."
                mm1.IsBodyHtml = True
                Dim smtp As New SmtpClient_RussianEncoded
                smtp.Send(mm1)

            Case "requesterreviewed"
                Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "Vacation Review")
                Dim mm1 As New MailMessage()
                mm1.From = MailFrom
                mm1.Body = GetPrefixEmailBody("requesterreviewed", _RequestId) + _EmailBody
                ' Activate this on deployment stage
                'GetEmailAdressForNotification("hrreviewed", _RequestId, mm1)
                mm1.To.Add("sk@mercuryeng.ru")
                mm1.Subject = VacationTables.GetUserNameFromEmailAdress(VacationTables.Table_Table_RequestByID(_RequestId).EmplyName) + " requests vacation which is revied by HR already."
                mm1.IsBodyHtml = True
                Dim smtp As New SmtpClient_RussianEncoded
                smtp.Send(mm1)

            Case "requesterdeleted"
                Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "Vacation Deleted")
                Dim mm1 As New MailMessage()
                mm1.From = MailFrom
                mm1.Body = GetPrefixEmailBody("requesterdeleted", _RequestId) + _EmailBody
                ' Activate this on deployment stage
                'GetEmailAdressForNotification("hrreviewed", _RequestId, mm1)
                mm1.To.Add("sk@mercuryeng.ru")
                mm1.Subject = VacationTables.GetUserNameFromEmailAdress(VacationTables.Table_Table_RequestByID(_RequestId).EmplyName) + " deleted vacation request."
                mm1.IsBodyHtml = True
                Dim smtp As New SmtpClient_RussianEncoded
                smtp.Send(mm1)

            Case "aprremoval"
                Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "Vacation Approval")
                Dim mm1 As New MailMessage()
                mm1.From = MailFrom
                mm1.Body = GetPrefixEmailBody("aprremoval", _RequestId, _AprPersonBeingProcessed) + _EmailBody
                ' Activate this on deployment stage
                'GetEmailAdressForNotification("aprremoval", _RequestId, mm1) 
                mm1.To.Add("sk@mercuryeng.ru")
                mm1.Subject = "Approval on Vacation request from " + VacationTables.GetUserNameFromEmailAdress(VacationTables.Table_Table_RequestByID(_RequestId).EmplyName) + " has been removed by " + _AprPersonBeingProcessed
                mm1.IsBodyHtml = True
                Dim smtp As New SmtpClient_RussianEncoded
                smtp.Send(mm1)

            Case "aprprovided"
                Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "Vacation Approval")
                Dim mm1 As New MailMessage()
                mm1.From = MailFrom
                mm1.Body = GetPrefixEmailBody("aprprovided", _RequestId, _AprPersonBeingProcessed) + _EmailBody
                ' Activate this on deployment stage
                'GetEmailAdressForNotification("aprprovided", _RequestId, mm1) 
                mm1.To.Add("sk@mercuryeng.ru")
                mm1.Subject = _AprPersonBeingProcessed + " has been approved Vacation request from " + VacationTables.GetUserNameFromEmailAdress(VacationTables.Table_Table_RequestByID(_RequestId).EmplyName)
                mm1.IsBodyHtml = True
                Dim smtp As New SmtpClient_RussianEncoded
                smtp.Send(mm1)

            Case "fullapproval"
                Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "Vacation Approval")
                Dim mm1 As New MailMessage()
                mm1.From = MailFrom
                mm1.Body = GetPrefixEmailBody("fullapproval", _RequestId) + _EmailBody
                ' Activate this on deployment stage
                'GetEmailAdressForNotification("fullapproval", _RequestId, mm1) 
                mm1.To.Add("sk@mercuryeng.ru")
                mm1.Subject = "Everybody approved Vacation request from " + VacationTables.GetUserNameFromEmailAdress(VacationTables.Table_Table_RequestByID(_RequestId).EmplyName)
                mm1.IsBodyHtml = True
                Dim smtp As New SmtpClient_RussianEncoded
                smtp.Send(mm1)

        End Select

    End Sub

    Shared Function ProduceEmailBodyFromFormview(ByVal _Formview As FormView) As String

        Dim stringWrite As New System.IO.StringWriter()
        Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
        _Formview.RenderControl(htmlWrite)
        Return stringWrite.ToString

    End Function

    Shared Sub GetEmailAdressForNotification(ByVal _Type As String, ByVal _RequestId As Integer, ByVal _mm As MailMessage)

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "[Vac].[SP_GetEmailsForNotification]"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim Type As SqlParameter = cmd.Parameters.Add("@NotificationType", Data.SqlDbType.NVarChar, 256)
            Type.Value = _Type.ToLower
            Dim RequestId As SqlParameter = cmd.Parameters.Add("@RequestId", Data.SqlDbType.Int)
            RequestId.Value = _RequestId
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                _mm.To.Add(dr(0))
            End While
            con.Close()
            dr.Close()
        End Using

    End Sub

    Shared Sub FormViewEmail_DataBound(ByVal _Formview As FormView)

        Dim LiteralCalendarDays As Literal = _Formview.FindControl("LiteralCalendarDays")

        If LiteralCalendarDays IsNot Nothing Then

            If String.IsNullOrEmpty(LiteralCalendarDays.Text) Then

                LiteralCalendarDays.Text = "Not provided by HR yet."

            End If

            Dim LiteralCommentFromEmployee As Literal = _Formview.FindControl("LiteralCommentFromEmployee")

            If String.IsNullOrEmpty(LiteralCommentFromEmployee.Text) Then

                LiteralCommentFromEmployee.Text = "No comment."

            End If

            Dim LiteralHRreview As Literal = _Formview.FindControl("LiteralHRreview")

            If LiteralHRreview.Text.Trim.ToLower = "false" Then

                LiteralHRreview.Text = "Not reviewed by HR yet."

            ElseIf LiteralHRreview.Text.Trim.ToLower = "true" Then

                LiteralHRreview.Text = "Reviewed"

            End If

            Dim LiteralReviewedByRequesterAfterHR As Literal = _Formview.FindControl("LiteralReviewedByRequesterAfterHR")

            If LiteralReviewedByRequesterAfterHR.Text.Trim.ToLower = "false" Then

                LiteralReviewedByRequesterAfterHR.Text = "Not reviewed by Requester yet."

            ElseIf LiteralReviewedByRequesterAfterHR.Text.Trim.ToLower = "true" Then

                LiteralReviewedByRequesterAfterHR.Text = "Reviewed"

            End If

            Dim LiteralCommentFromHR As Literal = _Formview.FindControl("LiteralCommentFromHR")

            If String.IsNullOrEmpty(LiteralCommentFromHR.Text) Then

                LiteralCommentFromHR.Text = "No comment."

            End If

            Dim LiteralApprvOrNotEmail As Literal = _Formview.FindControl("LiteralApprvOrNotEmail")

            If LiteralApprvOrNotEmail.Text.Trim.ToLower = "false" Then

                LiteralApprvOrNotEmail.Text = "Not approved by everybody yet."

            ElseIf LiteralApprvOrNotEmail.Text.Trim.ToLower = "true" Then

                LiteralApprvOrNotEmail.Text = "Approved"

            End If

            Dim LiteralFullApprovalTime As Literal = _Formview.FindControl("LiteralFullApprovalTime")

            If String.IsNullOrEmpty(LiteralFullApprovalTime.Text) Then

                LiteralFullApprovalTime.Text = "Not approved yet."

            End If

        End If

    End Sub

    Shared Sub PrepareEmail(ByVal _Type As String, _
                               ByVal _SqlDataSourceEmailNotification As SqlDataSource, _
                               ByVal _RequestID As Integer, _
                               ByVal _FormViewEmail As FormView, _
                               Optional ByVal _AprPersonBeingProcessed As String = "")

        _SqlDataSourceEmailNotification.SelectParameters("RequestId").DefaultValue = _RequestID

        _FormViewEmail.DataBind()

        VacationSendEmail.FormViewEmail_DataBound(_FormViewEmail)

        Dim ObjectDataSourceApprMxEmail As ObjectDataSource = _FormViewEmail.FindControl("ObjectDataSourceApprMxEmail")
        Dim LiteralId As Literal = _FormViewEmail.FindControl("LiteralId")
        ObjectDataSourceApprMxEmail.SelectParameters("RequestId").DefaultValue = LiteralId.Text
        Dim GridViewApprMxEmail As GridView = _FormViewEmail.FindControl("GridViewApprMxEmail")
        GridViewApprMxEmail.DataBind()

        VacationSendEmail.SendEmailNotification(_Type, VacationSendEmail.ProduceEmailBodyFromFormview(_FormViewEmail), _RequestID, _AprPersonBeingProcessed)

    End Sub

    Shared Function GetPrefixEmailBody(ByVal _Type As String, Optional ByVal _RequestId As Integer = 0, Optional ByVal _AprPersonBeingProcessed As String = "") As String

        Select Case _Type.ToLower
            Case "requesttohr"

                Return _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><font size=" + """" + "3" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'>Hello, </span> " + _
                "     <o:p></o:p></font></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><font size=" + """" + "3" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'>&nbsp;</span> " + _
                " <o:p></o:p></font></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><font size=" + """" + "3" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'>" + VacationTables.GetUserNameFromEmailAdress(VacationTables.Table_Table_RequestByID(_RequestId).EmplyName) + " requested vacation. You can see details below. </span> " + _
                " <o:p></o:p></font></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">Please go to </font></span><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: #1f497d; mso-ansi-language: en-us; mso-themecolor: dark2'><a shape=" + """" + "rect" + """" + " href=" + """" + "http://10.8.35.7/Vacation/HRcheckSummary.aspx" + """" + "><font color=" + """" + "#0000ff" + """" + " size=" + """" + "3" + """" + ">THIS PAGE</font></a><font size=" + """" + "3" + """" + "> </font></span><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">and provide required parameters.</font></span><span lang=" + """" + "EN-US" + """" + " style='font-size: 11pt; font-family: " + """" + "calibri" + """" + "," + """" + "sans-serif" + """" + "; color: #1f497d; mso-ansi-language: en-us; mso-ascii-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: " + """" + "times new roman" + """" + "; mso-bidi-theme-font: minor-bidi; mso-themecolor: dark2'> </span><span lang=" + """" + "EN-US" + """" + " style=" + """" + "mso-ansi-language: en-us" + """" + "> " + _
                " <o:p></o:p></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style=" + """" + "color: #1f497d; mso-ansi-language: en-us; mso-themecolor: dark2" + """" + "><font size=" + """" + "3" + """" + " face=" + """" + "Times New Roman" + """" + ">&nbsp;</font></span><span lang=" + """" + "EN-US" + """" + " style=" + """" + "mso-ansi-language: en-us" + """" + "> " + _
                " <o:p></o:p></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">Regards</font> " + _
                " <o:p></o:p></span></p> "

            Case "hrreviewed"
                Return _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">Hello " + VacationTables.GetUserNameFromEmailAdress(VacationTables.Table_Table_RequestByID(_RequestId).EmplyName) + ",  " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">&nbsp; " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">HR has reviewed your vacation request. You can see details below.  " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">Please go to </font></span><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: #1f497d; mso-ansi-language: en-us; mso-themecolor: dark2'><a shape=" + """" + "rect" + """" + " href=" + """" + "http://10.8.35.7/Vacation/FollowMyRequest.aspx" + """" + "><font color=" + """" + "#0000ff" + """" + " size=" + """" + "3" + """" + ">THIS PAGE</font></a><font size=" + """" + "3" + """" + "> </font></span><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">and approve your request. " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">Once you approve, your managers will receive email about your vacation request. " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">You will be notified instantly after their approval. " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">&nbsp;</font> " + _
                " <o:p></o:p></span></p>" + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">Regards</font> " + _
                " <o:p></o:p></span></p> "

            Case "requesterreviewed"
                Return _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><font size=" + """" + "3" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'>Hello, </span><span lang=" + """" + "EN-US" + """" + " style=" + """" + "mso-ansi-language: en-us" + """" + "> " + _
                " <o:p></o:p></span></font></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">&nbsp;</font></span><span lang=" + """" + "EN-US" + """" + " style=" + """" + "mso-ansi-language: en-us" + """" + "><font size=" + """" + "3" + """" + "><font color=" + """" + "#000000" + """" + "><font face=" + """" + "Times New Roman" + """" + ">  " + _
                " <o:p></o:p></font></font></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">" + VacationTables.GetUserNameFromEmailAdress(VacationTables.Table_Table_RequestByID(_RequestId).EmplyName) + " has vacation request. You can see details below. " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">HR department has already reviewed.  " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">It needs to be approved by yourself. Please go to </font></span><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: #1f497d; mso-ansi-language: en-us; mso-themecolor: dark2'><a shape=" + """" + "rect" + """" + " href=" + """" + "http://10.8.35.7/Vacation/ApprMx.aspx" + """" + "><font color=" + """" + "#0000ff" + """" + " size=" + """" + "3" + """" + ">THIS PAGE</font></a><font size=" + """" + "3" + """" + "> </font></span><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">for your approval.</font></span><span lang=" + """" + "EN-US" + """" + " style='font-size: 11pt; font-family: " + """" + "calibri" + """" + "," + """" + "sans-serif" + """" + "; color: #1f497d; mso-ansi-language: en-us; mso-ascii-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: " + """" + "times new roman" + """" + "; mso-bidi-theme-font: minor-bidi; mso-themecolor: dark2'> </span><span lang=" + """" + "EN-US" + """" + " style=" + """" + "mso-ansi-language: en-us" + """" + "> " + _
                " <o:p></o:p></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">&nbsp;  " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">Regards</font> " + _
                " <o:p></o:p></span></p> "

            Case "requesterdeleted"
                Return _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">Hello,  " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">&nbsp;  " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">" + VacationTables.GetUserNameFromEmailAdress(VacationTables.Table_Table_RequestByID(_RequestId).EmplyName) + " has deleted vacation request. You can see details below.  " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'> " + _
                " <o:p><font size=" + """" + "3" + """" + ">&nbsp;</font></o:p></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">Regards " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><font face=" + """" + "Times New Roman" + """" + "><span lang=" + """" + "EN-US" + """" + " style=" + """" + "color: #1f497d; mso-ansi-language: en-us; mso-themecolor: dark2" + """" + "><font size=" + """" + "3" + """" + ">&nbsp;</font></span><span lang=" + """" + "EN-US" + """" + " style=" + """" + "mso-ansi-language: en-us" + """" + "><font color=" + """" + "#000000" + """" + " size=" + """" + "3" + """" + "> </font></span></font> " + _
                " <o:p></o:p></p> "

            Case "aprremoval"
                Return _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">Hello " + VacationTables.GetUserNameFromEmailAdress(VacationTables.Table_Table_RequestByID(_RequestId).EmplyName) + ",  " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">&nbsp;  " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + "> " + _AprPersonBeingProcessed + "  has removed approval on your vacation request. " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">You can track your requests on </font></span><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: #1f497d; mso-ansi-language: en-us; mso-themecolor: dark2'><a shape=" + """" + "rect" + """" + " href=" + """" + "http://10.8.35.7/Vacation/FollowMyRequest.aspx" + """" + "><font color=" + """" + "#0000ff" + """" + " size=" + """" + "3" + """" + ">THIS PAGE</font></a></span><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">. " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'> " + _
                " <o:p><font size=" + """" + "3" + """" + ">&nbsp;</font></o:p></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">Regards " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'> " + _
                " <o:p><font size=" + """" + "3" + """" + ">&nbsp;</font></o:p></span></p> "

            Case "aprprovided"
                Return _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">Hello " + VacationTables.GetUserNameFromEmailAdress(VacationTables.Table_Table_RequestByID(_RequestId).EmplyName) + ",  " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">&nbsp;  " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + "> " + _AprPersonBeingProcessed + "  has approved your vacation request. " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">You can track your requests on </font></span><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: #1f497d; mso-ansi-language: en-us; mso-themecolor: dark2'><a shape=" + """" + "rect" + """" + " href=" + """" + "http://10.8.35.7/Vacation/FollowMyRequest.aspx" + """" + "><font color=" + """" + "#0000ff" + """" + " size=" + """" + "3" + """" + ">THIS PAGE</font></a></span><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">. " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'> " + _
                " <o:p><font size=" + """" + "3" + """" + ">&nbsp;</font></o:p></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">Regards " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'> " + _
                " <o:p><font size=" + """" + "3" + """" + ">&nbsp;</font></o:p></span></p> "

            Case "fullapproval"
                Return _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">Hello,  " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">&nbsp;  " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">This vacation request has been approved by everybody. " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'> " + _
                " <o:p><font size=" + """" + "3" + """" + ">&nbsp;</font></o:p></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; color: black; mso-ansi-language: en-us'><font size=" + """" + "3" + """" + ">Regards " + _
                " <o:p></o:p></font></span></p> " + _
                " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-size: 11pt; font-family: " + """" + "calibri" + """" + "," + """" + "sans-serif" + """" + "; color: #1f497d; mso-ansi-language: en-us; mso-ascii-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: " + """" + "times new roman" + """" + "; mso-bidi-theme-font: minor-bidi; mso-themecolor: dark2'> " + _
                " <o:p>&nbsp;</o:p></span></p> "



        End Select


    End Function

    Shared Sub SendEmailToNewUserForAccount(ByVal PossibleUserName As String, ByVal PasswordUser As String, ByVal email As String)

        Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "PTS Account")
        Dim mm1 As New MailMessage()
        mm1.From = MailFrom
        mm1.Body = GetEmailBodyForNewUser(PossibleUserName, PasswordUser)
        ' Activate this on deployment stage
        'mm1.To.Add(email)
        mm1.To.Add("sk@mercuryeng.ru")
        mm1.Subject = " Your PTS Account Details"
        mm1.IsBodyHtml = True
        Dim smtp As New SmtpClient_RussianEncoded
        smtp.Send(mm1)

    End Sub

    Shared Function GetEmailBodyForNewUser(ByVal PossibleUserName As String, ByVal PasswordUser As String) As String

        Return _
        " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-size: 12pt; font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; mso-ansi-language: en-us'><font color=" + """" + "#000000" + """" + ">Hello " + VacationTables.GetUserNameFromEmailAdress(PossibleUserName) + ", " + _
        " <o:p></o:p></font></span></p> " + _
        " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-size: 12pt; font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; mso-ansi-language: en-us'> " + _
        " <o:p><font color=" + """" + "#000000" + """" + ">&nbsp;</font></o:p></span></p> " + _
        " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-size: 12pt; font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; mso-ansi-language: en-us'><font color=" + """" + "#000000" + """" + ">This is automatic email from PTS. " + _
        " <o:p></o:p></font></span></p> " + _
        " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-size: 12pt; font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; mso-ansi-language: en-us'><font color=" + """" + "#000000" + """" + ">A new account has been created for you. " + _
        " <o:p></o:p></font></span></p> " + _
        " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-size: 12pt; font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; mso-ansi-language: en-us'><font color=" + """" + "#000000" + """" + ">You will need this account to track and approve your vacation requests. " + _
        " <o:p></o:p></font></span></p> " + _
        " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-size: 12pt; font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; mso-ansi-language: en-us'> " + _
        " <o:p><font color=" + """" + "#000000" + """" + ">&nbsp;</font></o:p></span></p> " + _
        " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-size: 12pt; font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; mso-ansi-language: en-us'><font color=" + """" + "#000000" + """" + ">Please keep your user details secure and don’t share with anyone else. " + _
        " <o:p></o:p></font></span></p> " + _
        " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-size: 12pt; font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; mso-ansi-language: en-us'> " + _
        " <o:p><font color=" + """" + "#000000" + """" + ">&nbsp;</font></o:p></span></p> " + _
        " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-size: 12pt; font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; mso-ansi-language: en-us'><font color=" + """" + "#000000" + """" + ">URL: </font><a shape=" + """" + "rect" + """" + " href=" + """" + "http://10.8.35.7/Vacation/Default.aspx" + """" + "><font color=" + """" + "#0000ff" + """" + ">http://10.8.35.7/Vacation/Default.aspx</font></a> " + _
        " <o:p></o:p></span></p> " + _
        " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-size: 12pt; font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; mso-ansi-language: en-us'><font color=" + """" + "#000000" + """" + ">User Name: " + PossibleUserName + " " + _
        " <o:p></o:p></font></span></p> " + _
        " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-size: 12pt; font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; mso-ansi-language: en-us'><font color=" + """" + "#000000" + """" + ">Password: " + PasswordUser + " " + _
        " <o:p></o:p></font></span></p> " + _
        " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-size: 12pt; font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; mso-ansi-language: en-us'> " + _
        " <o:p><font color=" + """" + "#000000" + """" + ">&nbsp;</font></o:p></span></p> " + _
        " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-size: 12pt; font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; mso-ansi-language: en-us'><font color=" + """" + "#000000" + """" + ">You can change your password in this link: </font><a shape=" + """" + "rect" + """" + " href=" + """" + "http://10.8.35.7/usercontrolpanel.aspx" + """" + "><font color=" + """" + "#0000ff" + """" + ">http://10.8.35.7/usercontrolpanel.aspx</font></a> " + _
        " <o:p></o:p></span></p> " + _
        " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-size: 12pt; font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; mso-ansi-language: en-us'> " + _
        " <o:p><font color=" + """" + "#000000" + """" + ">&nbsp;</font></o:p></span></p> " + _
        " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-size: 12pt; font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; mso-ansi-language: en-us'><font color=" + """" + "#000000" + """" + ">If you have any query, please contact to system admin </font><a shape=" + """" + "rect" + """" + " href=" + """" + "mailto:stanislav.malutin@mercuryeng.ru" + """" + "><font color=" + """" + "#0000ff" + """" + ">Savas Karaduman</font></a> " + _
        " <o:p></o:p></span></p> " + _
        " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-size: 12pt; font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; mso-ansi-language: en-us'> " + _
        " <o:p><font color=" + """" + "#000000" + """" + ">&nbsp;</font></o:p></span></p> " + _
        " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-size: 12pt; font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; mso-ansi-language: en-us'><font color=" + """" + "#000000" + """" + ">Regards. " + _
        " <o:p></o:p></font></span></p> " + _
        " <p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span lang=" + """" + "EN-US" + """" + " style='font-size: 12pt; font-family: " + """" + "tahoma" + """" + "," + """" + "sans-serif" + """" + "; mso-ansi-language: en-us'><font color=" + """" + "#000000" + """" + ">PTS</font> " + _
        " <o:p></o:p></span></p> "

    End Function

End Class

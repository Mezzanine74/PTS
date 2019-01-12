Imports System.Net.Mail
Imports PTS_App_Code_VB_Project.PTS_MERCURY.helper.EmailGenerator

Partial Class ContractAddendumClientNotification
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If LocalTime.GetTime.DayOfWeek.ToString <> "Saturday" AndAlso LocalTime.GetTime.DayOfWeek.ToString <> "Sunday" Then
            If LocalTime.GetTime.Hour = 8 Then
                If LocalTime.GetTime.Minute >= 54 AndAlso LocalTime.GetTime.Minute <= 59 Then

                    ' one week earlier notification
                    Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                        Dim Today As DateTime = PTS_MERCURY.db.CustomClass.ConvertDateTimeToTruncDateTime(DateTime.Now.AddDays(0))

                        ' Contracts
                        Try
                            Dim _crt As DateTime = Today.AddDays(+7)
                            Dim Notf = (From C In db.Table_Contracts_ClientAdditional Where C.AktOfWork = False And C.CompletionDate = _crt).ToList()

                            If Notf.Count > 0 Then
                                For i = 0 To Notf.Count - 1
                                    SendEmail(1, Notf(i).ContractID)
                                Next
                            End If

                        Catch ex As Exception

                        End Try

                        Try
                            Dim _crt As DateTime = Today.AddDays(0)
                            Dim Notf = (From C In db.Table_Contracts_ClientAdditional Where C.AktOfWork = False And C.CompletionDate = _crt).ToList()

                            If Notf.Count > 0 Then
                                For i = 0 To Notf.Count - 1
                                    SendEmail(2, Notf(i).ContractID)
                                Next
                            End If

                        Catch ex As Exception

                        End Try

                        Try
                            Dim _crt As DateTime = Today.AddDays(-15)
                            Dim Notf = (From C In db.Table_Contracts_ClientAdditional Where C.AktOfWork = False And C.CompletionDate = _crt).ToList()

                            If Notf.Count > 0 Then
                                For i = 0 To Notf.Count - 1
                                    SendEmail(3, Notf(i).ContractID)
                                Next
                            End If

                        Catch ex As Exception

                        End Try

                        Try
                            Dim _crt As DateTime = Today.AddDays(-30)
                            Dim Notf = (From C In db.Table_Contracts_ClientAdditional Where C.AktOfWork = False And C.CompletionDate = _crt).ToList()

                            If Notf.Count > 0 Then
                                For i = 0 To Notf.Count - 1
                                    SendEmail(4, Notf(i).ContractID)
                                Next
                            End If

                        Catch ex As Exception

                        End Try

                        ' Addendums
                        Try
                            Dim _crt As DateTime = Today.AddDays(+7)
                            Dim Notf = (From C In db.Table_Addendums_ClientAdditional Where C.AktOfWork = False And C.CompletionDate = _crt).ToList()

                            If Notf.Count > 0 Then
                                For i = 0 To Notf.Count - 1
                                    SendEmailAdd(1, Notf(i).AddendumID)
                                Next
                            End If

                        Catch ex As Exception

                        End Try

                        Try
                            Dim _crt As DateTime = Today.AddDays(0)
                            Dim Notf = (From C In db.Table_Addendums_ClientAdditional Where C.AktOfWork = False And C.CompletionDate = _crt).ToList()

                            If Notf.Count > 0 Then
                                For i = 0 To Notf.Count - 1
                                    SendEmailAdd(2, Notf(i).AddendumID)
                                Next
                            End If

                        Catch ex As Exception

                        End Try

                        Try
                            Dim _crt As DateTime = Today.AddDays(-15)
                            Dim Notf = (From C In db.Table_Addendums_ClientAdditional Where C.AktOfWork = False And C.CompletionDate = _crt).ToList()

                            If Notf.Count > 0 Then
                                For i = 0 To Notf.Count - 1
                                    SendEmailAdd(3, Notf(i).AddendumID)
                                Next
                            End If

                        Catch ex As Exception

                        End Try

                        Try
                            Dim _crt As DateTime = Today.AddDays(-30)
                            Dim Notf = (From C In db.Table_Addendums_ClientAdditional Where C.AktOfWork = False And C.CompletionDate = _crt).ToList()

                            If Notf.Count > 0 Then
                                For i = 0 To Notf.Count - 1
                                    SendEmailAdd(4, Notf(i).AddendumID)
                                Next
                            End If

                        Catch ex As Exception

                        End Try


                    End Using

                End If
            End If
        End If

    End Sub

    Public Sub SendEmail(ByVal _NotfNumber As Integer, ByVal _ContractID As Integer)

        Dim sqls As SqlDataSource = WebUserControl_ContractEmailBody.FindControl("SqlDataSourceContractEmailBody")
        Dim FormViewContractEmailBody As FormView = WebUserControl_ContractEmailBody.FindControl("FormViewContractEmailBody")
        sqls.SelectParameters("ContractID").DefaultValue = _ContractID

        Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "P T S")

        Dim mm1 As New MailMessage()

        mm1.From = MailFrom

        mm1.IsBodyHtml = True

        If _NotfNumber = 1 Then
            mm1.Body = "<h3>Warning!</h3>" + _
                "After one week, following project should finish according to contract" + _
                "<br/>" + _
                "However Akt Of Works not signed yet." + _
                "<br/><br/>" + _
                "P T S" + _
                "<hr/>" + _
                ContractView.ProduceHTMLforContractEmailBody(WebUserControl_ContractEmailBody, _ContractID)
        ElseIf _NotfNumber = 2 Then
            mm1.Body = "<h3>Warning!</h3>" + _
                "Today, following project is going to finish according to contract" + _
                "<br/>" + _
                "However Akt Of Works not signed yet." + _
                "<br/><br/>" + _
                "P T S" + _
                "<hr/>" + _
                ContractView.ProduceHTMLforContractEmailBody(WebUserControl_ContractEmailBody, _ContractID)

        ElseIf _NotfNumber = 3 Then
            mm1.Body = "<h3>Warning!</h3>" + _
                "Two weeks ago, following project finished according to contract" + _
                "<br/>" + _
                "However Akt Of Works not signed yet." + _
                "<br/><br/>" + _
                "P T S" + _
                "<hr/>" + _
                ContractView.ProduceHTMLforContractEmailBody(WebUserControl_ContractEmailBody, _ContractID)

        ElseIf _NotfNumber = 4 Then
            mm1.Body = "<h3>Warning!</h3>" + _
                "One month ago, following project finished according to contract" + _
                "<br/>" + _
                "However Akt Of Works not signed yet." + _
                "<br/><br/>" + _
                "P T S" + _
                "<hr/>" + _
                ContractView.ProduceHTMLforContractEmailBody(WebUserControl_ContractEmailBody, _ContractID)

        End If

        GetEmail(mm1, _ContractID)

        If _NotfNumber = 1 Then
            mm1.Subject = "Client contract will finish next week !"
        ElseIf _NotfNumber = 2 Then
            mm1.Subject = "Client contract is going to finish TODAY !"
        ElseIf _NotfNumber = 3 Then
            mm1.Subject = "Client contract finished two weeks ago !"
        ElseIf _NotfNumber = 4 Then
            mm1.Subject = "Client contract finished one month ago!"
        End If

        Dim smtp As New SmtpClient_RussianEncoded

        smtp.Send(mm1)

    End Sub

    Public Sub SendEmailAdd(ByVal _NotfNumber As Integer, ByVal _AddendumID As Integer)

        Dim sqls As SqlDataSource = WebUserControl_AddendumEmailBody.FindControl("SqlDataSourceAddendumsEmailBody")
        Dim FormViewAddendumsEmailBody As FormView = WebUserControl_AddendumEmailBody.FindControl("FormViewAddendumsEmailBody")
        sqls.SelectParameters("AddendumID").DefaultValue = _AddendumID

        Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "P T S")

        Dim mm1 As New MailMessage()

        mm1.From = MailFrom

        mm1.IsBodyHtml = True

        If _NotfNumber = 1 Then
            mm1.Body = "<h3>Warning!</h3>" + _
                "After one week, following project should finish according to addendum" + _
                "<br/>" + _
                "However Akt Of Works not signed yet." + _
                "<br/><br/>" + _
                "P T S" + _
                "<hr/>" + _
                ContractView.ProduceHTMLforContractEmailBody(WebUserControl_AddendumEmailBody, _AddendumID)
        ElseIf _NotfNumber = 2 Then
            mm1.Body = "<h3>Warning!</h3>" + _
                "Today, following project is going to finish according to addendum" + _
                "<br/>" + _
                "However Akt Of Works not signed yet." + _
                "<br/><br/>" + _
                "P T S" + _
                "<hr/>" + _
                ContractView.ProduceHTMLforContractEmailBody(WebUserControl_AddendumEmailBody, _AddendumID)

        ElseIf _NotfNumber = 3 Then
            mm1.Body = "<h3>Warning!</h3>" + _
                "Two weeks ago, following project finished according to addendum" + _
                "<br/>" + _
                "However Akt Of Works not signed yet." + _
                "<br/><br/>" + _
                "P T S" + _
                "<hr/>" + _
                ContractView.ProduceHTMLforContractEmailBody(WebUserControl_AddendumEmailBody, _AddendumID)

        ElseIf _NotfNumber = 4 Then
            mm1.Body = "<h3>Warning!</h3>" + _
                "One month ago, following project finished according to addendum" + _
                "<br/>" + _
                "However Akt Of Works not signed yet." + _
                "<br/><br/>" + _
                "P T S" + _
                "<hr/>" + _
                ContractView.ProduceHTMLforContractEmailBody(WebUserControl_AddendumEmailBody, _AddendumID)

        End If

        GetEmailAdd(mm1, _AddendumID)

        If _NotfNumber = 1 Then
            mm1.Subject = "Client addendum will finish next week !"
        ElseIf _NotfNumber = 2 Then
            mm1.Subject = "Client addendum is going to finish TODAY !"
        ElseIf _NotfNumber = 3 Then
            mm1.Subject = "Client addendum finished two weeks ago !"
        ElseIf _NotfNumber = 4 Then
            mm1.Subject = "Client addendum finished one month ago!"
        End If

        Dim smtp As New SmtpClient_RussianEncoded

        smtp.Send(mm1)

    End Sub

    Public Sub GetEmail(ByVal _mm1 As MailMessage, ByVal _contractID As Integer)

        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            Dim receivers = From C In db.Table_Contracts
                            Join D In db.Table_Contract_ProjectIDforClient On C.ContractID Equals D.ContractID
                            Join EE In db.Table1_Project On D.ProjectID Equals EE.ProjectID
                            Join F In db.aspnet_Users On EE.ProjectManager Equals F.UserName
                            Join G In db.aspnet_Membership On F.UserId Equals G.UserId
                            Where C.ContractID = _contractID
                            Select New With {G.LoweredEmail}

            If receivers.ToList().Count > 0 Then

                For i = 0 To receivers.ToList().Count - 1
                    _mm1.To.Add(receivers.ToList()(i).LoweredEmail)
                Next

            End If

            _mm1.To.Add("Patrick.pigott@mercuryeng.ru")
            _mm1.To.Add("larisa.kozlova@mercuryeng.ru")
            _mm1.To.Add("olga.matrosova@mercuryeng.ru")
            _mm1.To.Add("sk@mercuryeng.ru")

        End Using

    End Sub

    Public Sub GetEmailAdd(ByVal _mm1 As MailMessage, ByVal _addendumID As Integer)

        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            Dim receivers = From C In db.Table_Contracts
                            Join Addendum In db.Table_Addendums On C.ContractID Equals Addendum.ContractID
                            Join D In db.Table_Contract_ProjectIDforClient On C.ContractID Equals D.ContractID
                            Join EE In db.Table1_Project On D.ProjectID Equals EE.ProjectID
                            Join F In db.aspnet_Users On EE.ProjectManager Equals F.UserName
                            Join G In db.aspnet_Membership On F.UserId Equals G.UserId
                            Where Addendum.AddendumID = _addendumID
                            Select New With {G.LoweredEmail}

            If receivers.ToList().Count > 0 Then

                For i = 0 To receivers.ToList().Count - 1
                    _mm1.To.Add(receivers.ToList()(i).LoweredEmail)
                Next

            End If

            _mm1.To.Add("Patrick.pigott@mercuryeng.ru")
            _mm1.To.Add("larisa.kozlova@mercuryeng.ru")
            _mm1.To.Add("olga.matrosova@mercuryeng.ru")
            _mm1.To.Add("sk@mercuryeng.ru")

        End Using

    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Exit Sub
    End Sub

End Class

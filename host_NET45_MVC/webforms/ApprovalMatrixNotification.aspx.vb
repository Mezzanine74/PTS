Imports PTS_App_Code_VB_Project.PTS.CoreTables
Imports System.Data
Imports System.IO
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Data.SqlClient
Imports PTS_App_Code_VB_Project.PTS_MERCURY.helper.EmailGenerator

Partial Class ApprovalMatrixNotification
    Inherits System.Web.UI.Page

    Public Class NotApprovedContractsAndAddendumsByPersons

        Friend UserName As String
        Friend LoweredEmail As String
        Friend Count_Contract As Integer
        Friend Count_Addendum As String
        Friend UserNameSurname As String

    End Class

    Protected Function Get_NotApprovedContractsAndAddendumsByPersons(ByVal _UserName As String) As NotApprovedContractsAndAddendumsByPersons

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT [UserName] " + _
                                    "       ,[LoweredEmail] " + _
                                    "       ,[Count_Contract] " + _
                                    "       ,[Count_Addendum] " + _
                                    " ,REPLACE(LEFT(LoweredEmail, PATINDEX('%@%', LoweredEmail) - 1), '.', ' ')  AS UserNameSurname " + _
                                    "   FROM [ApprMx].[View_Contract_Addendum_NotApprovedPersonsForEmailNotification] WHERE UserName = @UserName AND UserName <> N'evgeniya.tragova'  "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            'syntax for parameter adding
            Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", System.Data.SqlDbType.VarChar)
            UserName.Value = _UserName
            Dim ReturnValue As New NotApprovedContractsAndAddendumsByPersons

            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                ReturnValue.UserName = dr(0)
                ReturnValue.LoweredEmail = dr(1)
                ReturnValue.Count_Contract = dr(2)
                ReturnValue.Count_Addendum = dr(3)
                ReturnValue.UserNameSurname = dr(4)
            End While
            Return ReturnValue
            con.Close()
            con.Dispose()
            dr.Close()
        End Using

    End Function

    Protected Function ReturnHTML(ByVal _Gridview As GridView) As String

        Dim sb As New StringBuilder()
        Dim sw As New StringWriter(sb)
        Dim hw As New HtmlTextWriter(sw)
        _Gridview.RenderControl(hw)
        Return sb.ToString()

    End Function

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Exit Sub
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If LocalTime.GetTime.DayOfWeek.ToString <> "Saturday" AndAlso LocalTime.GetTime.DayOfWeek.ToString <> "Sunday" Then
            If LocalTime.GetTime.Hour = 8 Then
                If LocalTime.GetTime.Minute >= 54 AndAlso LocalTime.GetTime.Minute <= 59 Then
                    GridViewNotApprovedPerson.DataBind()

                    If GridViewNotApprovedPerson.Rows.Count = 0 Then
                        Exit Sub
                    End If

                    For Each row As GridViewRow In GridViewNotApprovedPerson.Rows

                        Dim StringNotApprovedPerson As String = DirectCast(row.FindControl("LabelNotApprovedPerson"), Label).Text

                        'If Not (StringNotApprovedPerson.ToString.ToLower = "eoin.vaughan" Or StringNotApprovedPerson.ToString.ToLower = "ronan.lynch") Then

                        'SqlDataSourceNotApprovedContractsOrAddendums.SelectParameters("username").DefaultValue = StringNotApprovedPerson.ToLower
                        'GridviewNotApprovedContractsOrAddendums.DataBind()

                        ' prepare email
                        '(1) Create the MailMessage instance
                        Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "PTS")
                        'Dim MailFrom As New System.Net.Mail.MailAddress("savas.karaduman@mercuryeng.ru", "Savas Karaduman")
                        Dim mm1 As New MailMessage()
                        mm1.From = MailFrom
                        mm1.Bcc.Add("sk@mercuryeng.ru")

                        mm1.To.Add(Get_NotApprovedContractsAndAddendumsByPersons(StringNotApprovedPerson).LoweredEmail)

                        mm1.Subject = "Ожидаемая матрица одобрения" ' Approval Matrix Pending

                        Dim _URL As String = DomainPTS.GetDomain + "/webforms/ApprovalMatrix.aspx"
                        If Get_NotApprovedContractsAndAddendumsByPersons(StringNotApprovedPerson).UserName.ToLower = "thomas.mcdonnell" Then
                            _URL = DomainPTS.GetDomain + "/webforms/login.aspx?ReturnUrl=%2fApprovalMatrix.aspx&username=thomas.mcdonnell&password=_KoSamui&FilterType=Show Only My Items"
                        End If

                        ' Produce Contract And Addendum Text
                        Dim ContractAddendumInfoText As String = ""
                        If Get_NotApprovedContractsAndAddendumsByPersons(StringNotApprovedPerson).Count_Contract > 0 And _
                            Get_NotApprovedContractsAndAddendumsByPersons(StringNotApprovedPerson).Count_Addendum = 0 Then
                            ' CONTRACT > 0 AND ADDENDUM = 0
                            ContractAddendumInfoText = Get_NotApprovedContractsAndAddendumsByPersons(StringNotApprovedPerson).Count_Contract.ToString + " контракты"

                        End If
                        If Get_NotApprovedContractsAndAddendumsByPersons(StringNotApprovedPerson).Count_Contract > 0 And _
                            Get_NotApprovedContractsAndAddendumsByPersons(StringNotApprovedPerson).Count_Addendum > 0 Then
                            ' CONTRACT > 0 AND ADDENDUM > 0
                            ContractAddendumInfoText = Get_NotApprovedContractsAndAddendumsByPersons(StringNotApprovedPerson).Count_Contract.ToString + " контракты" + _
                                " а также " + Get_NotApprovedContractsAndAddendumsByPersons(StringNotApprovedPerson).Count_Addendum.ToString + " Дополнительное соглашение"
                        End If
                        If Get_NotApprovedContractsAndAddendumsByPersons(StringNotApprovedPerson).Count_Contract = 0 And _
                            Get_NotApprovedContractsAndAddendumsByPersons(StringNotApprovedPerson).Count_Addendum > 0 Then
                            ' CONTRACT = 0 AND ADDENDUM > 0
                            ContractAddendumInfoText = Get_NotApprovedContractsAndAddendumsByPersons(StringNotApprovedPerson).Count_Addendum.ToString + " Дополнительное соглашение"
                        End If

                        mm1.Body = "<span style=" + """" + " font-size: 11px;	color: #FF0000;	font-style: italic;	padding: 5px;	margin: 3px;	border-style: dotted;	border-width: thin; background-color: #FFFFCC;" + """" + "> Auto Generated Email From PTS </span><br/><hr/> " + _
                                    "     <style type=" + """" + "text/css" + """" + "> " + _
                                    "  " + _
                                    "         .style0 " + _
                                    "         { " + _
                                    "             width: 100px; " + _
                                    "             height:35px; " + _
                                    "             background-color: #8CDFFF; " + _
                                    "             text-align: center; " + _
                                    "         } " + _
                                    "  " + _
                                    "         .style1 " + _
                                    "         { " + _
                                    "             width: 100px; " + _
                                    "             height:20px; " + _
                                    "             background-color: #D4EAF8; " + _
                                    "             text-align: center; " + _
                                    "         } " + _
                                    "          " + _
                                    "         .style2 " + _
                                    "         { " + _
                                    "             width: 100px; " + _
                                    "             height:35px; " + _
                                    "             background-color: #F2FAFD; " + _
                                    "             text-align: center;        " + _
                                    "         } " + _
                                    "  " + _
                                    "         .style3 " + _
                                    "         { " + _
                                    "             width: 100px; " + _
                                    "             height:35px; " + _
                                    "             background-color: #F2FAFD; " + _
                                    "             text-align: center; " + _
                                    "         } " + _
                                    "          " + _
                                    "     </style> " + _
                                    "     <br/> " + _
                                    "     Уважаемые " + Get_NotApprovedContractsAndAddendumsByPersons(StringNotApprovedPerson).UserNameSurname.ToUpperInvariant + " ," + _
                                    "     <br/> " + _
                                    "     <br/> " + _
                                    ContractAddendumInfoText + _
                                    " ожидая вашего одобрения " + _
                                    "     <br/> " + _
                                    "     Вы можете увидеть подробности в этом " + _
                                    " <a href=" + """" + _URL + """" + " target=" + """" + "_blank" + """" + ">ССЫЛКА</a> " + _
                                    "     <br/> " + _
                                    "     <br/> " + _
                                    "     ПТС "

                        ' cancelled temporarily, because HTML inside gridview templates 
                        ' if activation required, entire Gridview should be taken here from ApprovalMatrix.aspx
                        'ReturnHTML(GridviewNotApprovedContractsOrAddendums)

                        mm1.IsBodyHtml = True
                        '(3) Create the SmtpClient object
                        Dim smtp As New SmtpClient_RussianEncoded

                        smtp.Send(mm1)

                        'End If

                    Next
                End If
            End If
        End If

        If LocalTime.GetTime.DayOfWeek.ToString <> "Saturday" AndAlso LocalTime.GetTime.DayOfWeek.ToString <> "Sunday" Then
            If LocalTime.GetTime.Hour = 8 Then
                If LocalTime.GetTime.Minute >= 54 AndAlso LocalTime.GetTime.Minute <= 59 Then
                    GridViewApprovedButNotExecutedContracts.DataBind()

                    If GridViewApprovedButNotExecutedContracts.Rows.Count = 0 Then
                        Exit Sub
                    End If

                    For Each row As GridViewRow In GridViewApprovedButNotExecutedContracts.Rows

                        Dim StringApprovedButNotExecutedContract As String = DirectCast(row.FindControl("LabelApprovedButNotExecutedContract"), Label).Text

                        'SqlDataSourceNotApprovedContractsOrAddendums.SelectParameters("username").DefaultValue = StringNotApprovedPerson.ToLower
                        'GridviewNotApprovedContractsOrAddendums.DataBind()

                        ' prepare email
                        '(1) Create the MailMessage instance
                        Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "PTS")
                        'Dim MailFrom As New System.Net.Mail.MailAddress("savas.karaduman@mercuryeng.ru", "Savas Karaduman")
                        Dim mm1 As New MailMessage()
                        mm1.From = MailFrom
                        mm1.Bcc.Add("sk@mercuryeng.ru")

                        GetEmailAdress(mm1, Convert.ToInt32(StringApprovedButNotExecutedContract))

                        mm1.Subject = "Контракты, одобренные всеми, но еще не подписанный заказ" ' Contracts approved by everybody but PO has not been raised yet
                        mm1.Body = "<span style=" + """" + " font-size: 11px;	color: #FF0000;	font-style: italic;	padding: 5px;	margin: 3px;	border-style: dotted;	border-width: thin; background-color: #FFFFCC;" + """" + "> Auto Generated Email From PTS </span><br/><hr/> " + _
                                    "     <style type=" + """" + "text/css" + """" + "> " + _
                                    "  " + _
                                    "         .style0 " + _
                                    "         { " + _
                                    "             width: 100px; " + _
                                    "             height:35px; " + _
                                    "             background-color: #8CDFFF; " + _
                                    "             text-align: center; " + _
                                    "         } " + _
                                    "  " + _
                                    "         .style1 " + _
                                    "         { " + _
                                    "             width: 100px; " + _
                                    "             height:20px; " + _
                                    "             background-color: #D4EAF8; " + _
                                    "             text-align: center; " + _
                                    "         } " + _
                                    "          " + _
                                    "         .style2 " + _
                                    "         { " + _
                                    "             width: 100px; " + _
                                    "             height:35px; " + _
                                    "             background-color: #F2FAFD; " + _
                                    "             text-align: center;        " + _
                                    "         } " + _
                                    "  " + _
                                    "         .style3 " + _
                                    "         { " + _
                                    "             width: 100px; " + _
                                    "             height:35px; " + _
                                    "             background-color: #F2FAFD; " + _
                                    "             text-align: center; " + _
                                    "         } " + _
                                    "          " + _
                                    "     </style> " + _
                                    "     <br/> " + _
                                    "     Уважаемые все! ," + _
                                    "     <br/> " + _
                                    "     <br/> " + _
                                    " Следующее контракт одобрен всеми. Но заказ на покупку еще не поднят." + _
                                    "     <br/> " + _
                                    " Если у вас есть срочный запрос на оплату, недостающие предметы следует пересмотреть." + _
                                    "     <br/> " + _
                                    "     Вы можете увидеть подробности в этом " + _
                                    " <a href=" + """" + DomainPTS.GetDomain + "/webforms/contractdetails.aspx?ContractID=" + StringApprovedButNotExecutedContract + """" + " target=" + """" + "_blank" + """" + ">ССЫЛКА</a> " + _
                                    "     <br/> " + _
                                    "     <br/> " + _
                                    "     ПТС "
                        mm1.IsBodyHtml = True
                        '(3) Create the SmtpClient object
                        Dim smtp As New SmtpClient_RussianEncoded

                        smtp.Send(mm1)
                    Next
                End If
            End If
        End If

        If LocalTime.GetTime.DayOfWeek.ToString <> "Saturday" AndAlso LocalTime.GetTime.DayOfWeek.ToString <> "Sunday" Then
            If LocalTime.GetTime.Hour = 8 Then
                If LocalTime.GetTime.Minute >= 54 AndAlso LocalTime.GetTime.Minute <= 59 Then
                    GridViewApprovedButNotExecutedAddendums.DataBind()

                    If GridViewApprovedButNotExecutedAddendums.Rows.Count = 0 Then
                        Exit Sub
                    End If

                    For Each row As GridViewRow In GridViewApprovedButNotExecutedAddendums.Rows

                        Dim StringApprovedButNotExecutedAddendum As String = DirectCast(row.FindControl("LabelApprovedButNotExecutedAddendum"), Label).Text

                        'SqlDataSourceNotApprovedContractsOrAddendums.SelectParameters("username").DefaultValue = StringNotApprovedPerson.ToLower
                        'GridviewNotApprovedContractsOrAddendums.DataBind()

                        ' prepare email
                        '(1) Create the MailMessage instance
                        Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "PTS")
                        'Dim MailFrom As New System.Net.Mail.MailAddress("savas.karaduman@mercuryeng.ru", "Savas Karaduman")
                        Dim mm1 As New MailMessage()
                        mm1.From = MailFrom
                        mm1.Bcc.Add("sk@mercuryeng.ru")

                        GetEmailAdressAddendum(mm1, Convert.ToInt32(StringApprovedButNotExecutedAddendum))

                        mm1.Subject = "Дополнительное соглашение, одобренное всеми. Но заказ на покупку еще не поднятю" 'Addendum approved by everybody but PO has not been raised yet
                        mm1.Body = "<span style=" + """" + " font-size: 11px;	color: #FF0000;	font-style: italic;	padding: 5px;	margin: 3px;	border-style: dotted;	border-width: thin; background-color: #FFFFCC;" + """" + "> Auto Generated Email From PTS </span><br/><hr/> " + _
                                    "     <style type=" + """" + "text/css" + """" + "> " + _
                                    "  " + _
                                    "         .style0 " + _
                                    "         { " + _
                                    "             width: 100px; " + _
                                    "             height:35px; " + _
                                    "             background-color: #8CDFFF; " + _
                                    "             text-align: center; " + _
                                    "         } " + _
                                    "  " + _
                                    "         .style1 " + _
                                    "         { " + _
                                    "             width: 100px; " + _
                                    "             height:20px; " + _
                                    "             background-color: #D4EAF8; " + _
                                    "             text-align: center; " + _
                                    "         } " + _
                                    "          " + _
                                    "         .style2 " + _
                                    "         { " + _
                                    "             width: 100px; " + _
                                    "             height:35px; " + _
                                    "             background-color: #F2FAFD; " + _
                                    "             text-align: center;        " + _
                                    "         } " + _
                                    "  " + _
                                    "         .style3 " + _
                                    "         { " + _
                                    "             width: 100px; " + _
                                    "             height:35px; " + _
                                    "             background-color: #F2FAFD; " + _
                                    "             text-align: center; " + _
                                    "         } " + _
                                    "          " + _
                                    "     </style> " + _
                                    "     <br/> " + _
                                    "     Уважаемые все! ," + _
                                    "     <br/> " + _
                                    "     <br/> " + _
                                    " Следующее дополнительное соглашение одобрено всеми, но заказ на поставку еще не запущен." + _
                                    "     <br/> " + _
                                    " Если у вас есть срочная оплата, недостающие предметы должны быть пересмотрены." + _
                                    "     <br/> " + _
                                    "     Вы можете увидеть подробности в этом " + _
                                    " <a href=" + """" + DomainPTS.GetDomain + "/webforms/addendumdetails.aspx?AddendumID=" + StringApprovedButNotExecutedAddendum + """" + " target=" + """" + "_blank" + """" + ">ССЫЛКА</a> " + _
                                    "     <br/> " + _
                                    "     <br/> " + _
                                    "     ПТС "
                        mm1.IsBodyHtml = True
                        '(3) Create the SmtpClient object
                        Dim smtp As New SmtpClient_RussianEncoded

                        smtp.Send(mm1)
                    Next
                End If
            End If
        End If

        If LocalTime.GetTime.DayOfWeek.ToString <> "Saturday" AndAlso LocalTime.GetTime.DayOfWeek.ToString <> "Sunday" Then
            If LocalTime.GetTime.Hour = 8 Then
                If LocalTime.GetTime.Minute >= 54 AndAlso LocalTime.GetTime.Minute <= 59 Then
                    Try
                        SendEmailNominatedConfirmationNotification()
                    Catch ex As Exception

                    End Try

                End If
            End If
        End If

        If LocalTime.GetTime.DayOfWeek.ToString <> "Saturday" AndAlso LocalTime.GetTime.DayOfWeek.ToString <> "Sunday" Then
            If LocalTime.GetTime.Hour = 8 Then
                If LocalTime.GetTime.Minute >= 54 AndAlso LocalTime.GetTime.Minute <= 59 Then
                    Try
                        SendInsuranceNotificationToMain()
                        SendInsuranceNotificationToPM()
                    Catch ex As Exception

                    End Try

                End If
            End If
        End If


    End Sub

    Protected Sub GetEmailAdress(ByVal mm As MailMessage, ByVal _ContractID As Integer)

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "ContractApprovedNotExecutedNotificationEmailGenerator"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.StoredProcedure
            'syntax for parameter adding
            Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", System.Data.SqlDbType.Int)
            ContractID.Value = _ContractID

            's.IndexOf("BAR", StringComparison.CurrentCultureIgnoreCase)

            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read

                'If Not (dr(0).ToString.IndexOf("eoin.vaughan", StringComparison.CurrentCultureIgnoreCase) > 0 Or dr(0).ToString.IndexOf("ronan.lynch", StringComparison.CurrentCultureIgnoreCase) > 0) Then
                mm.To.Add(dr(0))
                'End If

            End While
            con.Close()
            con.Dispose()
            dr.Close()
        End Using
    End Sub

    Protected Sub GetEmailAdressAddendum(ByVal mm As MailMessage, ByVal _AddendumID As Integer)
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "AddendumApprovedNotExecutedNotificationEmailGenerator"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.StoredProcedure
            'syntax for parameter adding
            Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", System.Data.SqlDbType.Int)
            AddendumID.Value = _AddendumID

            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                'If Not (dr(0).ToString.IndexOf("eoin.vaughan", StringComparison.CurrentCultureIgnoreCase) > 0 Or dr(0).ToString.IndexOf("ronan.lynch", StringComparison.CurrentCultureIgnoreCase) > 0) Then
                mm.To.Add(dr(0))
                'End If
            End While
            con.Close()
            con.Dispose()
            dr.Close()
        End Using
    End Sub

    Protected Sub SendEmailNominatedConfirmationNotification()

        Dim adapter As New MercuryTableAdapters.ContractMissingNominatedConfirmationTableAdapter
        If adapter.GetCount = 0 Then
            adapter = Nothing
            Exit Sub
        End If

        Dim table As New Mercury.ContractMissingNominatedConfirmationDataTable
        Dim _strbuilder As New StringBuilder
        table = adapter.GetData
        For Each row As Mercury.ContractMissingNominatedConfirmationRow In table
            Dim url As String = DomainPTS.GetDomain + "/webforms/contractdetails.aspx?ContractID=" + row.ContractID.ToString
            _strbuilder.Append("<a href=" + """" + url + """" + " target=" + """" + "_blank" + """" + ">" + url + "</a>" + "<br/><br/>")
        Next
        Response.Write(_strbuilder)

        ' prepare email
        '(1) Create the MailMessage instance
        Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "PTS")
        'Dim MailFrom As New System.Net.Mail.MailAddress("savas.karaduman@mercuryeng.ru", "Savas Karaduman")
        Dim mm1 As New MailMessage()
        mm1.From = MailFrom

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "SP_GetUserEmailInRole"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim Role_ As SqlParameter = cmd.Parameters.Add("@RoleName", System.Data.SqlDbType.NVarChar, 256)
            Role_.Value = "ESTM"
            Dim dr As SqlDataReader = cmd.ExecuteReader

            While dr.Read
                mm1.To.Add(dr(0).ToString)
            End While

            con.Close()
            dr.Close()
        End Using

        mm1.Bcc.Add("sk@mercuryeng.ru")

        mm1.Subject = "Подтверждаемые субподрядчики должны быть подтверждены"
        mm1.Body = "<span style=" + """" + " font-size: 11px;	color: #FF0000;	font-style: italic;	padding: 5px;	margin: 3px;	border-style: dotted;	border-width: thin; background-color: #FFFFCC;" + """" + "> Auto Generated Email From PTS </span><br/><hr/> " + _
                    "     <style type=" + """" + "text/css" + """" + "> " + _
                    "  " + _
                    "         .style0 " + _
                    "         { " + _
                    "             width: 100px; " + _
                    "             height:35px; " + _
                    "             background-color: #8CDFFF; " + _
                    "             text-align: center; " + _
                    "         } " + _
                    "  " + _
                    "         .style1 " + _
                    "         { " + _
                    "             width: 100px; " + _
                    "             height:20px; " + _
                    "             background-color: #D4EAF8; " + _
                    "             text-align: center; " + _
                    "         } " + _
                    "          " + _
                    "         .style2 " + _
                    "         { " + _
                    "             width: 100px; " + _
                    "             height:35px; " + _
                    "             background-color: #F2FAFD; " + _
                    "             text-align: center;        " + _
                    "         } " + _
                    "  " + _
                    "         .style3 " + _
                    "         { " + _
                    "             width: 100px; " + _
                    "             height:35px; " + _
                    "             background-color: #F2FAFD; " + _
                    "             text-align: center; " + _
                    "         } " + _
                    "          " + _
                    "     </style> " + _
                    "     <br/> " + _
                    "     Уважаемые " + getNamesToEmail() + " ," + _
                    "     <br/> " + _
                    "     <br/> " + _
                    " Следующие контракты назначаются в качестве назначенного субподрядчика." + _
                    "     <br/> " + _
                    " Это требует вашего подтверждения." + _
                    "     <br/> " + _
                    "     Вы можете увидеть подробности ниже " + _
                    "     <br/><hr/> " + _
                    _strbuilder.ToString + _
                    "     <br/> " + _
                    "     <br/> " + _
                    "     ПТС "
        mm1.IsBodyHtml = True
        '(3) Create the SmtpClient object
        Dim smtp As New SmtpClient_RussianEncoded

        smtp.Send(mm1)

        _strbuilder = Nothing
        table = Nothing
        adapter = Nothing

    End Sub

    Protected Function getNamesToEmail() As String

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = _
                        " DECLARE @email NVARCHAR(max); " + _
                        " SET @email = '' " + _
                        " SELECT  @email = rtrim(@email) + upper(rtrim(REPLACE(LEFT(LoweredEmail, PATINDEX('%@%', LoweredEmail) - 1), '.', ' '))) + N' ,'  " + _
                        " FROM aspnet_Membership  " + _
                        " inner join aspnet_Users on aspnet_Users.UserId = aspnet_Membership.UserId  " + _
                        " inner join aspnet_UsersInRoles on aspnet_UsersInRoles.UserId = aspnet_Users.UserId " + _
                        " inner join aspnet_Roles on aspnet_Roles.RoleId = aspnet_UsersInRoles.RoleId  " + _
                        " where RoleName = N'ESTM' and UserName <> N'savas' " + _
                        " " + _
                        " SELECT @email "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                Return dr(0).ToString
            End While
            con.Close()
            dr.Close()
        End Using

    End Function

    Protected Sub BindGrid()

        Using Context As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            Dim today As DateTime = PTS_MERCURY.db.CustomClass.ConvertDateTimeToTruncDateTime(DateTime.Now)

            ' one week
            Dim twoWeekLAter As DateTime = today.AddDays(+7)

            Dim InsuranceDetails = ( _
                From c In Context.Table1_ProjectInsurCertf
                Join p In Context.Table1_Project On c.ProjectID Equals p.ProjectID
                Where c.InsuranceFinish >= today And c.InsuranceFinish <= twoWeekLAter
                Group By ProjectName = p.ProjectName, ProjectID = c.ProjectID, InsuranceFinish = c.InsuranceFinish
                Into g = Group
                Select New With {ProjectName, ProjectID, InsuranceFinish}
                ).ToList

            GridViewInsuranceDetails.DataSource = InsuranceDetails
            GridViewInsuranceDetails.DataBind()

        End Using

    End Sub

    Protected Sub SendInsuranceNotificationToMain()

        BindGrid()

        ' Send email to main receiver
        If GridViewInsuranceDetails.Rows.Count > 0 Then
            ' There is something, send it

            Dim textToEmail As String = ""

            Using swriter As New StringWriter

                Using gridHTML As New HtmlTextWriter(swriter)

                    GridViewInsuranceDetails.RenderControl(gridHTML)

                    textToEmail = swriter.ToString

                End Using

            End Using

            Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "PTS Insurance Warning")

            Dim mm1 As New MailMessage()

            mm1.From = MailFrom

            mm1.IsBodyHtml = True

            mm1.Body = "Здравствуйте, <br/> <br/> Следующие страховки будут просрочены в течение 7 дней <br/> <br/> PTS <br/> <hr /> <br/>" + textToEmail

            mm1.To.Add("savas.karaduman@mercuryeng.ru")
            'mm1.To.Add("alex.gadjiev@ mercuryeng.ru")

            mm1.Subject = "Предупреждение об истечении срока страхования"

            Dim smtp As New SmtpClient_RussianEncoded

            smtp.Send(mm1)

        End If

    End Sub

    Protected Sub SendInsuranceNotificationToPM()

        BindGrid()

        For Each _row As GridViewRow In GridViewInsuranceDetails.Rows
            Try

                ' ProjectID
                Dim prjid As Int16 = Convert.ToInt16(_row.Cells(1).Text)

                Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                    Dim PM = (From C In db.Table_Approval_UserPositionPrjJunction
                     Join Users In db.aspnet_Users On C.UserName Equals Users.UserName
                     Join mmbership In db.aspnet_Membership On Users.UserId Equals mmbership.UserId
                     Where C.PositionID = 11 And C.ProjectID = prjid
                     Select New With {.email = mmbership.LoweredEmail.Trim}).ToList()(0)

                    Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "PTS Insurance Warning")

                    Dim mm1 As New MailMessage()

                    mm1.From = MailFrom

                    mm1.IsBodyHtml = True

                    mm1.Body = "Здравствуйте " + MyCommonTasks.GetNameSurnameFromEmail(PM.email) + ", <br/> <br/> Страхование по следующему проекту будет истекло в течение 7 дней <br/> <br/> PTS <br/> <hr /> <br/>" + _
                        "<strong>название проекта: </strong>" + _row.Cells(0).Text.ToString + _
                        "<br/><strong>Идентификатор проекта: </strong>" + prjid.ToString + _
                        "<br/><strong>Страхование продолжается: </strong>" + _row.Cells(2).Text.ToString()

                    mm1.To.Add(PM.email.Trim.ToString())
                    mm1.To.Add("savas.karaduman@mercuryeng.ru")

                    mm1.Subject = "Предупреждение об истечении срока страхования"

                    Dim smtp As New SmtpClient_RussianEncoded

                    smtp.Send(mm1)

                End Using

            Catch ex As Exception

            End Try
        Next
    End Sub

End Class

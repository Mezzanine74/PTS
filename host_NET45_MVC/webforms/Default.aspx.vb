Imports System.Net
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms
Imports Microsoft.ReportingServices
Imports System.Data.SqlClient
Imports PTS_App_Code_VB_Project

Partial Class Default_2OsmanZOOTO
    Inherits System.Web.UI.Page

    Dim Notification As New _GiveNotification

    Protected Function UserInApprovalPersonList(ByVal _username As String) As Boolean

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT COUNT(UserName) FROM [Table_Approval_Project_Users] WHERE UserName = @UserName "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            'syntax for parameter adding
            Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar)
            UserName.Value = _username

            Dim ReturnValue As Boolean = False
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                If dr(0) > 0 Then
                    ReturnValue = True
                End If
            End While
            Return ReturnValue
            con.Close()
            con.Dispose()
            dr.Close()
        End Using
    End Function


    Protected Function GetUnReadComments() As Integer

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String =
                                         "SELECT COUNT(*) FROM ( " +
                                         "SELECT [C_or_A] " +
                                         "  FROM [ApprMx].[Notf_ContractCommentsUnRead] " +
                                         "WHERE C_or_A = N'C' AND ForWho = @UserName AND FromWho <> @UserName " +
                                         " AND [ApprMx].[Notf_ContractCommentsUnRead].C_or_A_id NOT IN ( SELECT ContractID FROM Table_Contracts WHERE POexecuted = 1 AND Exceptional = 0 ) " +
                                         "  " +
                                         "UNION ALL " +
                                         "" +
                                         "SELECT [C_or_A] " +
                                         "  FROM [ApprMx].[Notf_AddendumCommentsUnRead] " +
                                         "WHERE C_or_A = N'A' AND ForWho = @UserName AND FromWho <> @UserName " +
                                         " AND [ApprMx].[Notf_AddendumCommentsUnRead].C_or_A_id NOT IN ( SELECT AddendumID FROM Table_Addendums WHERE POexecuted = 1 AND Exceptional = 0 ) " +
                                         ") AS Source "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim UserParm As SqlParameter = cmd.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar, 256)
            UserParm.Value = If(Roles.IsUserInRole("ContractLeadGirls") = True, "lawyers", Page.User.Identity.Name.ToString.ToLower)
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                Return dr(0)
            End While
            con.Close()
            dr.Close()
        End Using

    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        LiteralApprovalMatrix.Text = BodyTexts.Ref("KMpM7Up4H0miRnYJuPBH/Q")

        LiteralUnreadComm.Text = If(GetUnReadComments() = 0, "", "<a href=" + """" + "/webforms/ApprovalMatrix.aspx" + """" + "   class=" + """" + "badge badge-danger icon-animated-vertical" + """" + ">" + GetUnReadComments.ToString + " " + BodyTexts.Ref("/ZmNcTvxF0ulrZIY8zOLtw") + "</a>")

        Dim _username As String = ""
        If User.IsInRole("ContractLeadGirls") Then
            _username = "lawyers"
        Else
            _username = Page.User.Identity.Name.Trim().ToLower()
        End If

        Dim MissingApprovalsText As String = PTS_MERCURY.helper.View_Contract_Addendum_NotApprovedPersonsForEmailNotification.ReturnMissingApprovalsForUser(_username)
        LiteralUserMissingApprovals.Text = If(MissingApprovalsText.Trim().Length() > 0,
                                              "<a href=" + """" + "/webforms/ApprovalMatrix.aspx" + """" + " style = " + """" + "display: block;text-align: left;width: 125px;white-space: normal; background-color: red;" + """" + "   class=" + """" + "badge badge-danger icon-animated-vertical" + """" + ">" + MissingApprovalsText + " </a>",
                                              "")

        'LiteralCrudeOil.Text = YahooFinance.GetCruedOil

        ' Deactivated
        'Using Adapter As New MercuryTableAdapters.Table_UserFaceBoookResponseTableAdapter

        '    If Adapter.GetCountByUserName(Page.User.Identity.Name.ToLower) = 0 Then
        '        ModalPopupExtenderFacebookInvitation.Show()
        '        LiteralUserFullName.Text = System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(Mid(Membership.GetUser(User.Identity.Name).Email, 1, Membership.GetUser(User.Identity.Name).Email.IndexOf("@")).Replace(".", " "))
        '    End If

        '    Adapter.Dispose()

        'End Using
        ' END OF Deactivated

        If Not IsPostBack Then
            'RenderReport("ReportGaugeTotalUserTodayRev")
        End If

        LiteralApprovalMatrix.Visible = True

        If Session("GiveApprovalNotification") IsNot Nothing _
          AndAlso Session("GiveApprovalNotification") = "GiveNotificationOnDefaultPage" Then
            Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Approval request sent!</p>")
            Session.Remove("GiveApprovalNotification")
        End If

        If Not IsPostBack Then
            UpdateHyperlinkNotification()
        End If

        If Roles.IsUserInRole("CostStaffPassive") Then
            ImageButtonBudget.Visible = "true"
            Divreport.InnerText = "Reports for Cost Staff"
        Else
            ImageButtonBudget.Visible = "false"
            Divreport.InnerText = ""
        End If

        If Not IsPostBack Then
            ' IT FEEDS AGEING TABLE
            Dim zoneId2Ageing As String = "Russian Standard Time"
            Dim tzi2Ageing As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId2Ageing)
            Dim result2Ageing As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi2Ageing)

            If result2Ageing.DayOfWeek.ToString = "Monday" Then
                Dim LastMonday As String = "'" + Mid(result2Ageing.ToString, 4, 2).ToString + "/" + Mid(result2Ageing.ToString, 1, 2).ToString + "/" + Mid(result2Ageing.ToString, 7, 4).ToString + "'"
                SqlDataSourceAgeingInsert.InsertCommand = " INSERT INTO [Table_Ageing] " +
        "            ([ProjectID] " +
        "            ,[DayOfRun] " +
        "            ,[PO_Total] " +
        "            ,[Paid] " +
        "            ,[Pending] " +
        "            ,[Balance]) " +
        " (SELECT [ProjectID] " +
        "       ," + LastMonday.ToString + " AS DayOfRun " +
        "       ,(CASE WHEN [OverallPoTotalEuroExcVAT] IS NULL THEN 0 ELSE [OverallPoTotalEuroExcVAT] END) AS PO_Total " +
        "       ,(CASE WHEN [OverallEuroPaidExcVAT] IS NULL THEN 0 ELSE [OverallEuroPaidExcVAT] END) AS Paid " +
        "       ,(CASE WHEN [OverallEuroPendingExcVAT] IS NULL THEN 0 ELSE [OverallEuroPendingExcVAT] END) AS Pending " +
        "       ,(CASE WHEN [OverallBalanceEuroExcVAT] IS NULL THEN 0 ELSE  [OverallBalanceEuroExcVAT] END) AS Balance " +
        "   FROM [dbo].[View_CostCodeSummary1] " +
        "   WHERE ProjectID IN  " +
        "   (SELECT     ProjectID " +
        " FROM         dbo.Table1_Project " +
        " WHERE     (Report = 1))) "

                Try
                    'SqlDataSourceAgeingInsert.Insert()
                Catch
                End Try
            End If
        End If

        If IsPostBack Or Not IsPostBack Then
            Dim UpdateExchangeRates As New MyCommonTasks
            UpdateExchangeRates.UpdateExchangeRate()
        End If

        ' it feeds FollowUp History table
        If Not IsPostBack Then
            Dim zoneIdFollowUp As String = "Russian Standard Time"
            Dim tziFollowUp As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneIdFollowUp)
            Dim resultFollowUp As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tziFollowUp)
            Dim DayOfRun As String = "'" + Mid(resultFollowUp.ToString, 7, 4).ToString + "-" + Mid(resultFollowUp.ToString, 4, 2).ToString + "-" + Mid(resultFollowUp.ToString, 1, 2).ToString + " 00:00:00" + "'"
            SqlDataSourceFollowUp.InsertCommand = " INSERT INTO Table_FollowUpReportSummary " +
      "            ([ProjectID] " +
      "            ,[DayOfRun] " +
      "            ,[OverallPoTotalDollarExcVAT] " +
      "            ,[OverallPoTotalEuroExcVAT] " +
      "            ,[OverallPoTotalRubleExcVAT] " +
      "            ,[OverallDollarPaidExcVAT] " +
      "            ,[OverallEuroPaidExcVAT] " +
      "            ,[OverallRublePaidExcVAT] " +
      "            ,[OverallDollarPendingExcVAT] " +
      "            ,[OverallEuroPendingExcVAT] " +
      "            ,[OverallRublePendingExcVAT] " +
      "            ,[OverallDoneDollarPO] " +
      "            ,[OverallDoneEuroPO] " +
      "            ,[OverallDoneRublePO] " +
      "            ,[OverallPartialDollarPO] " +
      "            ,[OverallPartialEuroPO] " +
      "            ,[OverallPartialRublePO] " +
      "            ,[OverallDollarPaidWthVAT] " +
      "            ,[OverallEuroPaidWthVAT] " +
      "            ,[OverallRublePaidWthVAT]) " +
      " ( SELECT     dbo.View_CostCodeSummary1.ProjectID," + DayOfRun + ", dbo.View_CostCodeSummary1.OverallPoTotalDollarExcVAT, dbo.View_CostCodeSummary1.OverallPoTotalEuroExcVAT,  " +
      "                       dbo.View_CostCodeSummary1.OverallPoTotalRubleExcVAT, dbo.View_CostCodeSummary1.OverallDollarPaidExcVAT,  " +
      "                       dbo.View_CostCodeSummary1.OverallEuroPaidExcVAT, dbo.View_CostCodeSummary1.OverallRublePaidExcVAT,  " +
      "                       dbo.View_CostCodeSummary1.OverallDollarPendingExcVAT, dbo.View_CostCodeSummary1.OverallEuroPendingExcVAT,  " +
      "                       dbo.View_CostCodeSummary1.OverallRublePendingExcVAT, dbo.View_CostCodeSummary1.OverallDoneDollarPO, dbo.View_CostCodeSummary1.OverallDoneEuroPO, " +
      "                        dbo.View_CostCodeSummary1.OverallDoneRublePO, dbo.View_CostCodeSummary1.OverallPartialDollarPO, dbo.View_CostCodeSummary1.OverallPartialEuroPO,  " +
      "                       dbo.View_CostCodeSummary1.OverallPartialRublePO,  " +
      "                       dbo.View_CostCodeSummary1.OverallDollarPaidExcVAT + dbo.View_CostCodeSummary2.VATpaidDollar AS OverallDollarPaidWthVAT,  " +
      "                       dbo.View_CostCodeSummary1.OverallEuroPaidExcVAT + dbo.View_CostCodeSummary2.VATpaidEuro AS OverallEuroPaidWthVAT,  " +
      "                       dbo.View_CostCodeSummary1.OverallRublePaidExcVAT + dbo.View_CostCodeSummary2.VATpaidRuble AS OverallRublePaidWthVAT " +
      " FROM         dbo.View_CostCodeSummary1 INNER JOIN " +
      "                       dbo.View_CostCodeSummary2 ON dbo.View_CostCodeSummary1.ProjectID = dbo.View_CostCodeSummary2.ProjectID " +
      " WHERE     (dbo.View_CostCodeSummary1.ProjectID IN " +
      "                           (SELECT     ProjectID " +
      "                             FROM          dbo.Table1_Project " +
      "                             WHERE      (Report = 'True'))) AND (dbo.View_CostCodeSummary1.OverallPoTotalDollarExcVAT IS NOT NULL)) "


            Try
                'SqlDataSourceFollowUp.Insert()
            Catch
            End Try

        End If

        ' this block of code highlightes and provides tooltip for icons
        If Not IsPostBack Or IsPostBack Then
            If Roles.IsUserInRole("MercuryStaff") Then
                ImageButtonPM_Approval.Enabled = True
                ImageButtonPM_Approval.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
                ImageButtonPM_Approval.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            Else
                ImageButtonPM_Approval.Enabled = False
                ImageButtonPM_Approval.ToolTip = ""
                ImageButtonPM_Approval.Attributes.Add("style", "cursor: not-allowed;")

            End If

            If Roles.IsUserInRole("MercuryStaff") AndAlso Roles.IsUserInRole("Enter_Nakladnaya") Then
                ImageButtonNakladnaya.Enabled = True
                ImageButtonNakladnaya.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
                ImageButtonNakladnaya.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            Else
                ImageButtonNakladnaya.Enabled = False
                ImageButtonNakladnaya.ToolTip = ""
                ImageButtonNakladnaya.Attributes.Add("style", "cursor: not-allowed;")
            End If

            If Roles.IsUserInRole("MercuryStaff") AndAlso Roles.IsUserInRole("EnterPurchaseOrder") Then
                ImageButtonPO.Enabled = True
                ImageButtonPOEdit.Enabled = True
                ImageButtonInvoice.Enabled = True
                ImageButtonInvoiceEdit.Enabled = True
                ImageButtonPaymentReq.Enabled = True
                ImageButtonPaymentReqEdit.Enabled = True
                ImageButtonPO.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
                ImageButtonPO.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
                ImageButtonPOEdit.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
                ImageButtonPOEdit.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
                ImageButtonInvoice.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
                ImageButtonInvoice.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
                ImageButtonInvoiceEdit.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
                ImageButtonInvoiceEdit.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
                ImageButtonPaymentReq.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
                ImageButtonPaymentReq.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
                ImageButtonPaymentReqEdit.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
                ImageButtonPaymentReqEdit.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            Else
                ImageButtonPO.Enabled = False
                ImageButtonPOEdit.Enabled = False
                ImageButtonInvoice.Enabled = False
                ImageButtonInvoiceEdit.Enabled = False
                ImageButtonPaymentReq.Enabled = False
                ImageButtonPaymentReqEdit.Enabled = False
                ImageButtonPO.ToolTip = ""
                ImageButtonPOEdit.ToolTip = ""
                ImageButtonInvoice.ToolTip = ""
                ImageButtonInvoiceEdit.ToolTip = ""
                ImageButtonPaymentReq.ToolTip = ""
                ImageButtonPaymentReqEdit.ToolTip = ""
                ImageButtonPO.Attributes.Add("style", "cursor: not-allowed;")
                ImageButtonPOEdit.Attributes.Add("style", "cursor: not-allowed;")
                ImageButtonInvoice.Attributes.Add("style", "cursor: not-allowed;")
                ImageButtonInvoiceEdit.Attributes.Add("style", "cursor: not-allowed;")
                ImageButtonPaymentReq.Attributes.Add("style", "cursor: not-allowed;")
                ImageButtonPaymentReqEdit.Attributes.Add("style", "cursor: not-allowed;")
            End If

            If Roles.IsUserInRole("MercuryStaff") AndAlso Roles.IsUserInRole("FollowUpReports") Then
                ImageButtonFollowUpReports.Enabled = True
                ImageButtonFollowUpReports.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
                ImageButtonFollowUpReports.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")

                ImageButtonBalanceBreakDown.Enabled = True
                ImageButtonBalanceBreakDown.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
                ImageButtonBalanceBreakDown.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")

                ImageButtonFollowUpReportBackUp.Enabled = True
                ImageButtonFollowUpReportBackUp.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
                ImageButtonFollowUpReportBackUp.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")

                ImageButtonFollowUpReportsWithVAT.Enabled = True
                ImageButtonFollowUpReportsWithVAT.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
                ImageButtonFollowUpReportsWithVAT.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")

                ImageButtonFollowUpReportsExcVAT.Enabled = True
                ImageButtonFollowUpReportsExcVAT.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
                ImageButtonFollowUpReportsExcVAT.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")

                ImageButtonFollowUpReportBySupplierExcVAT.Enabled = True
                ImageButtonFollowUpReportBySupplierExcVAT.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
                ImageButtonFollowUpReportBySupplierExcVAT.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")

                ImageButtonFollowUpReportBySupplierWithVAT.Enabled = True
                ImageButtonFollowUpReportBySupplierWithVAT.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
                ImageButtonFollowUpReportBySupplierWithVAT.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")


            Else
                ImageButtonFollowUpReports.Enabled = False
                ImageButtonFollowUpReports.ToolTip = ""
                ImageButtonFollowUpReports.Attributes.Add("style", "cursor: not-allowed;")

                ImageButtonBalanceBreakDown.Enabled = False
                ImageButtonBalanceBreakDown.ToolTip = ""
                ImageButtonBalanceBreakDown.Attributes.Add("style", "cursor: not-allowed;")

                ImageButtonFollowUpReportBackUp.Enabled = False
                ImageButtonFollowUpReportBackUp.ToolTip = ""
                ImageButtonFollowUpReportBackUp.Attributes.Add("style", "cursor: not-allowed;")

                ImageButtonFollowUpReportsWithVAT.Enabled = False
                ImageButtonFollowUpReportsWithVAT.ToolTip = ""
                ImageButtonFollowUpReportsWithVAT.Attributes.Add("style", "cursor: not-allowed;")

                ImageButtonFollowUpReportsExcVAT.Enabled = False
                ImageButtonFollowUpReportsExcVAT.ToolTip = ""
                ImageButtonFollowUpReportsExcVAT.Attributes.Add("style", "cursor: not-allowed;")

                ImageButtonFollowUpReportBySupplierExcVAT.Enabled = False
                ImageButtonFollowUpReportBySupplierExcVAT.ToolTip = ""
                ImageButtonFollowUpReportBySupplierExcVAT.Attributes.Add("style", "cursor: not-allowed;")

                ImageButtonFollowUpReportBySupplierWithVAT.Enabled = False
                ImageButtonFollowUpReportBySupplierWithVAT.ToolTip = ""
                ImageButtonFollowUpReportBySupplierWithVAT.Attributes.Add("style", "cursor: not-allowed;")

            End If

            If Page.User.Identity.Name.ToLower() = "dzera" OrElse Page.User.Identity.Name.ToLower() = "inna" OrElse Page.User.Identity.Name.ToLower() = "savas" _
                OrElse Page.User.Identity.Name.ToLower() = "mariya" OrElse Page.User.Identity.Name.ToLower() = "katya" OrElse Page.User.Identity.Name.ToLower() = "natalia.surskaya" _
                OrElse Page.User.Identity.Name.ToLower() = "elmira.shabaeva" OrElse Page.User.Identity.Name.ToLower() = "mariya.podobueva" OrElse Page.User.Identity.Name.ToLower() = "natalia.larionova" Then
                ImageButtonPayLog.Enabled = True
                ImageButtonPayLogEdit.Enabled = True
                ImageButtonPayLog.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
                ImageButtonPayLog.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
                ImageButtonPayLogEdit.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
                ImageButtonPayLogEdit.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            Else
                ImageButtonPayLog.Enabled = False
                ImageButtonPayLogEdit.Enabled = False
                ImageButtonPayLog.ToolTip = ""
                ImageButtonPayLogEdit.ToolTip = ""
                ImageButtonPayLog.Attributes.Add("style", "cursor: not-allowed;")
                ImageButtonPayLogEdit.Attributes.Add("style", "cursor: not-allowed;")
            End If

            ' These items are available for evetyone
            ImageButtonPackingListToday.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
            ImageButtonPackingListToday.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            ImageButtonPayments.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
            ImageButtonPayments.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            ImageButtonPending.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
            ImageButtonPending.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            ImageButtonMonitoring.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
            ImageButtonMonitoring.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            ImageButtonOpen_Po.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
            ImageButtonOpen_Po.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            ImageButtonContract.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
            ImageButtonContract.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            ImageButtonMissingDocs.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
            ImageButtonMissingDocs.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            ImageButtonSearch.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
            ImageButtonSearch.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            ImageButtonFrameApproval.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
            ImageButtonFrameApproval.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            ImageButtonBudget.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
            ImageButtonBudget.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            ImageButtonPObreakdown.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
            ImageButtonPObreakdown.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            ImageButtonComparePo.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
            ImageButtonComparePo.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            ImageButtonFixRates.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
            ImageButtonFixRates.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            ImageButtonBalanceBreakDown.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
            ImageButtonBalanceBreakDown.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            ImageButtonOthers.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
            ImageButtonOthers.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            ImageButtonApprovalMatrix.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
            ImageButtonApprovalMatrix.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            ImageButtonContractVersusPo.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
            ImageButtonContractVersusPo.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            ImageButtonSearchContractBreakdown.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
            ImageButtonSearchContractBreakdown.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            ImageButtonIntAudit.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
            ImageButtonIntAudit.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
        End If

        If Roles.IsUserInRole("MercuryStaff") AndAlso Roles.IsUserInRole("LawyerOnSite") Then
            ImageButtonPO.Enabled = True
            ImageButtonPO.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            ImageButtonPO.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
            ImageButtonPO.Attributes.Add("style", "cursor: allowed;")
        End If

        If Roles.IsUserInRole("ContractLeadGirls") Then

            ImageButtonPO.Enabled = True
            ImageButtonPO.Attributes.Add("onmouseout", "this.style.border= '1px solid #FFFFFF';")
            ImageButtonPO.Attributes.Add("onmouseover", "this.style.border= '1px solid #808080';")
            ImageButtonPO.Attributes.Add("style", "cursor: allowed;")

        End If

        ProcessPMcounter()
        ProcessFramecounter()

        LiteralDollar.Text = GetRates.Dollar
        LiteralEuro.Text = GetRates.Euro

    End Sub

    Protected Sub ProcessPMcounter()

        If GetPMcounter() = 0 Then
            HyperLinkPMCounter.Visible = False
        ElseIf GetPMcounter() > 0 Then
            HyperLinkPMCounter.Visible = True
            HyperLinkPMCounter.Text = GetPMcounter().ToString
        End If

    End Sub

    Protected Sub ProcessFramecounter()

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String =
            " SELECT     COUNT(derivedtbl_1.ProjectID) AS Count " +
            " FROM         (SELECT     Table_Approval_UserPositionPrjJunction.ProjectID, Table_Approval_UserPositionPrjJunction.UserName " +
            "                        FROM          Table_Approval_UserPositionPrjJunction INNER JOIN " +
            "                                               Table_Approval_PositionEmployee ON Table_Approval_UserPositionPrjJunction.PositionID = Table_Approval_PositionEmployee.PositionID " +
            "                        WHERE      (Table_Approval_PositionEmployee.PositionName IN (N'project manager', N'cost controller')) " +
            "                        UNION ALL " +
            "                        SELECT     ProjectID, UserName " +
            "                        FROM         Table_Approval_UserRolePrjectJunction " +
            "                        WHERE     (RoleName = N'InitiateContractAndAddendum')) AS derivedtbl_1 INNER JOIN " +
            "                           (SELECT     Project_ID " +
            "                             FROM          Table2_PONo " +
            "                             WHERE      (FrameContractPO = 1) AND (Approved = 0)) AS derivedtbl_2 ON derivedtbl_1.ProjectID = derivedtbl_2.Project_ID " +
            " WHERE     (derivedtbl_1.UserName = @username ) "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim UserParm As SqlParameter = cmd.Parameters.Add("@username", System.Data.SqlDbType.NVarChar, 256)
            UserParm.Value = Page.User.Identity.Name.ToLower
            Dim dr As SqlDataReader = cmd.ExecuteReader

            While dr.Read

                If dr(0) = 0 Then
                    HyperLinkFramePO.Visible = False
                Else
                    HyperLinkFramePO.Visible = True
                    HyperLinkFramePO.Text = dr(0).ToString

                End If

            End While

            con.Close()
            dr.Close()
        End Using

    End Sub

    Protected Function GetPMcounter() As Integer

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "dbo.SP_GetNumberNonApprovedPRforPM"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim UserParm As SqlParameter = cmd.Parameters.Add("@PM", System.Data.SqlDbType.NVarChar, 256)
            UserParm.Value = Page.User.Identity.Name
            Dim dr As SqlDataReader = cmd.ExecuteReader

            While dr.Read
                Return dr(0)
            End While

            con.Close()
            dr.Close()
        End Using

    End Function

    Protected Sub NotificationTimer_Tick(sender As Object, e As System.EventArgs) Handles NotificationTimer.Tick
        UpdateHyperlinkNotification()
    End Sub

    Protected Sub UpdateHyperlinkNotification()
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT     COUNT(dbo.Table_Notification.Notification) AS CountOfNotification " +
        " FROM         dbo.aspnet_Users INNER JOIN " +
        "                       dbo.Table_Prj_User_Junction ON dbo.aspnet_Users.UserId = dbo.Table_Prj_User_Junction.UserID INNER JOIN " +
        "                       dbo.Table_Notification ON dbo.Table_Prj_User_Junction.ProjectID = dbo.Table_Notification.ProjectID LEFT OUTER JOIN " +
        "                       dbo.Table_Notification_Read ON dbo.aspnet_Users.UserName = dbo.Table_Notification_Read.UserName AND  " +
        "                       dbo.Table_Notification.NotificationID = dbo.Table_Notification_Read.NotificationID " +
        " WHERE     (dbo.aspnet_Users.UserName = N'" + Page.User.Identity.Name.ToString + "') AND (dbo.Table_Notification_Read.NotificationID IS NULL) AND (dbo.Table_Notification_Read.UserName IS NULL) AND  " +
        "                       (dbo.Table_Notification.TimeStamp > CONVERT(nVarChar(4), YEAR(GETDATE())) + N'/' + (CASE WHEN LEN(CONVERT(nVarChar(2), month(GETDATE())))  " +
        "                       = 1 THEN N'0' + CONVERT(nVarChar(2), month(GETDATE())) ELSE CONVERT(nVarChar(2), month(GETDATE())) END) + N'/' + (CASE WHEN LEN(CONVERT(nVarChar(2),  " +
        "                       day(GETDATE() - 0))) = 1 THEN N'0' + CONVERT(nVarChar(2), day(GETDATE() - 0)) ELSE CONVERT(nVarChar(2), day(GETDATE() - 0)) END)) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            Dim dr As SqlDataReader = cmd.ExecuteReader

            While dr.Read
                HyperLinkNotification.Text = dr(0).ToString
            End While

            con.Close()
            dr.Close()

        End Using

        If HyperLinkNotification.Text = "0" Then
            'HyperLinkNotification.Visible = False
            HyperLinkNotification.CssClass = "badge badge-default"
            HyperLinkNotification.Visible = True
            HyperLinkNotification.Attributes.Add("onclick", "javascript:w= window.open('Notifications.aspx" + "','Notifications','left=100,top=10,width=1000,height=700,toolbar=0,resizable=0,scrollbars=yes');")
        Else
            HyperLinkNotification.CssClass = "badge badge-danger"
            HyperLinkNotification.Visible = True
            HyperLinkNotification.Attributes.Add("onclick", "javascript:w= window.open('Notifications.aspx" + "','Notifications','left=100,top=10,width=1000,height=700,toolbar=0,resizable=0,scrollbars=yes');")
        End If

    End Sub

    'Protected Sub GridViewOnlineUsers_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewOnlineUsers.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim LabelUser As Label = DirectCast(e.Row.FindControl("LabelUser"), Label)
    '        Dim LabelPageUserOn As Label = DirectCast(e.Row.FindControl("LabelPageUserOn"), Label)
    '        If InStr(LabelUser.Text, "savas") > 0 Then
    '            LabelUser.ForeColor = System.Drawing.Color.Red
    '            e.Row.Cells(1).Text = "<a href=" + """" + "mailto:savas.karaduman@mercuryeng.ru?Subject=PTS! " + """" + ">send e-mail</a>"
    '            e.Row.Cells(1).Font.Size = 7
    '        End If
    '        If InStr(LabelPageUserOn.Text, "SupplierAdressBook") > 0 Then
    '            LabelPageUserOn.Text = ""
    '        End If
    '    End If
    'End Sub

    Protected Sub PMTimer_Tick(sender As Object, e As EventArgs) Handles PMTimer.Tick

        ProcessPMcounter()

    End Sub

    Protected Sub Frametimer_Tick(sender As Object, e As EventArgs) Handles Frametimer.Tick

        ProcessFramecounter()

    End Sub

    Public Class LastRates

        Friend _Date As DateTime
        Friend Dollar As Decimal
        Friend Euro As Decimal

    End Class

    Protected Function GetRates() As LastRates
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "SP_GetExcRateMax"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim dr As SqlDataReader = cmd.ExecuteReader

            While dr.Read

                Dim _rates As New LastRates

                _rates._Date = dr(0)
                _rates.Dollar = dr(1)
                _rates.Euro = dr(2)

                Return _rates

                _rates = Nothing

            End While

            con.Close()
            dr.Close()
        End Using

    End Function

End Class

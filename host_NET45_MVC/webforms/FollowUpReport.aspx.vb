Imports System.Net
Imports System.Data.SqlClient
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms
Imports Microsoft.ReportingServices

Partial Class _Nakl_FollowUpReportDeliveryFinanceFasterTREV
    Inherits System.Web.UI.Page

    Protected Sub DropDownListPrj_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.DataBound
        ' it provides Select Project Statement for DDL
        If Not IsPostBack Then
            Dim lst As New ListItem("Select Project", "0")
            DropDownListPrj.Items.Insert(0, lst)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' it provides query parameter for DropDownListSupplier
        If IsPostBack Or Not IsPostBack Then
            TextBoxUserName.Text = Page.User.Identity.Name
        End If

        ' it removes SELECT PROJECT after posback
        If IsPostBack Then
            Dim controll1 As New String(Page.Request.Params.Get("__EVENTTARGET"))
            If (Not controll1 Is Nothing) Or (controll1 <> "") Then
                If controll1 = "ctl00$MainContent$DropDownListPrj" Then
                    If Me.DropDownListPrj.Items(0).ToString = "Select Project" Then
                        Me.DropDownListPrj.Items.RemoveAt(0)
                    End If
                End If
            End If
        End If

    End Sub

    Protected Sub ButtonRunReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonRunReport.Click

        RenderReport.Render("html", ReportViewerDeliveryReport, "_Nakl_FollowUpReportExcVATRev", "ProjectID", DropDownListPrj.SelectedValue.ToString, _
                            "Currency", DropDownListCurrency.SelectedValue.ToString)

        LabelProjectName.Text = "Report For " + DropDownListPrj.SelectedItem.Text + " in " + DropDownListCurrency.SelectedItem.Text
        labelWhichCurrency.Text = DropDownListCurrency.SelectedItem.Text

        ' it provides criteria for Excel Output
        LabelProjectIDforExcel.Text = DropDownListPrj.SelectedValue.ToString

    End Sub

    Protected Sub SqlDataSourceRubleToExcel_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles SqlDataSourceRubleToExcel.Selecting
        If Not DropDownListCurrency.SelectedValue.Equals("Rub") Then
            e.Cancel = True
        Else
            e.Command.CommandTimeout = 120
            e.Cancel = False
        End If
    End Sub

    Protected Sub SqlDataSourceDollarToExcel_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles SqlDataSourceDollarToExcel.Selecting
        If Not DropDownListCurrency.SelectedValue.Equals("Dollar") Then
            e.Cancel = True
        Else
            e.Command.CommandTimeout = 120
            e.Cancel = False
        End If
    End Sub

    Protected Sub SqlDataSourceEuroToExcel_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles SqlDataSourceEuroToExcel.Selecting
        If Not DropDownListCurrency.SelectedValue.Equals("Euro") Then
            e.Cancel = True
        Else
            e.Command.CommandTimeout = 120
            e.Cancel = False '
        End If
    End Sub

    Private Sub ExportToExcel()

        Dim _currency As String = ""

        If DropDownListCurrency.SelectedValue.ToString = "Rub" Then
            _currency = "Ruble"
        Else
            _currency = DropDownListCurrency.SelectedValue.ToString
        End If

        RenderReport.Render("Excel", ReportViewerDeliveryReport, "_Nakl_FollowUpReportSubExcVATrev", _
                            "Currency", _
                            _currency, _
                            "ProjectID", _
                            DropDownListPrj.SelectedValue.ToString, _
                            "CostCode", _
                            "ALL")

        Exit Sub

        '    Response.Clear()
        'Dim PrjName As String = DropDownListPrj.SelectedItem.ToString.Trim
        'Dim Currency As String = labelWhichCurrency.Text.ToString
        'Dim CostCode As String = LabelCriteriaCostCodeForExcel.Text.ToString
        'If CostCode = "" Then
        '  Response.AddHeader("content-disposition", "attachment; filename=Report " + PrjName + " in " + Currency + " for ALL Cost Code " + ".xls")

        'Else
        '  Response.AddHeader("content-disposition", "attachment; filename=Report " + PrjName + " in " + Currency + " for Cost Code " + CostCode + ".xls")
        'End If

        'Response.Buffer = True
        'Response.ContentType = "application/vnd.ms-excel"
        'Response.Charset = ""
        'Me.EnableViewState = False
        'Dim oStringWriter As New System.IO.StringWriter
        'Dim oHtmlTextWriter As New System.Web.UI.HtmlTextWriter(oStringWriter)

        'If labelWhichCurrency.Text = "Rub" Then
        '  GridViewRubleToExcel.RenderControl(oHtmlTextWriter)
        'ElseIf labelWhichCurrency.Text = "Dollar" Then
        '  GridViewDollarToExcel.RenderControl(oHtmlTextWriter)
        'ElseIf labelWhichCurrency.Text = "Euro" Then
        '  GridViewEuroToExcel.RenderControl(oHtmlTextWriter)
        'End If

        'Dim zoneId As String = "Russian Standard Time"
        'Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        'Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        'If CostCode = "" Then
        '  Response.Write("<p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span lang=EN-US style='color:red;mso-ansi-language:EN-US'>" + PrjName + " in " + Currency + " for ALL Cost Code " + ", printed by " + String.Format("{0:dddd, MMMM d, yyyy, h:mm tt}", result) + "<o:p></o:p></span></b></p>")
        'Else
        '  Response.Write("<p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span lang=EN-US style='color:red;mso-ansi-language:EN-US'>" + PrjName + " in " + Currency + " for Cost Code " + CostCode + ", printed by " + String.Format("{0:dddd, MMMM d, yyyy, h:mm tt}", result) + "<o:p></o:p></span></b></p>")
        'End If

        'Response.Write(oStringWriter.ToString())
        'Response.[End]()

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImageButton1.Click

        'If labelWhichCurrency.Text = "" Then
        'GridViewFollowUpRuble.DataBind()
        'GridViewFollowUpDollar.DataBind()
        'GridViewFollowUpEuro.DataBind()
        'GridViewCostSummaryRuble.DataBind()
        'GridViewCostSummaryDollar.DataBind()
        'GridViewCostSummaryEuro.DataBind()
        'GridViewRubleToExcel.DataBind()
        'GridViewDollarToExcel.DataBind()
        'GridViewEuroToExcel.DataBind()
        'LabelProjectName.Text = "Report For " + DropDownListPrj.SelectedItem.Text + " in " + DropDownListCurrency.SelectedItem.Text
        'labelWhichCurrency.Text = DropDownListCurrency.SelectedItem.Text
        'End If

        'If labelWhichCurrency.Text = "Rub" Then
        '  GridViewRubleToExcel.DataSource = SqlDataSourceRubleToExcel
        '  SqlDataSourceRubleToExcel.DataBind()
        '  GridViewRubleToExcel.DataBind()
        '  ExportToExcel("Report.xls", GridViewRubleToExcel)
        'ElseIf labelWhichCurrency.Text = "Dollar" Then
        '  GridViewDollarToExcel.DataSource = SqlDataSourceDollarToExcel
        '  SqlDataSourceDollarToExcel.DataBind()
        '  GridViewDollarToExcel.DataBind()
        '  ExportToExcel("Report.xls", GridViewDollarToExcel)
        'ElseIf labelWhichCurrency.Text = "Euro" Then
        '  GridViewEuroToExcel.DataSource = SqlDataSourceEuroToExcel
        '  SqlDataSourceEuroToExcel.DataBind()
        '  GridViewEuroToExcel.DataBind()
        '  ExportToExcel("Report.xls", GridViewEuroToExcel)
        'End If

        ExportToExcel()

    End Sub

    Protected Sub ImageButtonOld_Click(ByVal sender As Object, e As System.EventArgs) Handles ImageButtonOld.Click

        'If labelWhichCurrency.Text = "" Then
        'GridViewFollowUpRuble.DataBind()
        'GridViewFollowUpDollar.DataBind()
        'GridViewFollowUpEuro.DataBind()
        'GridViewCostSummaryRuble.DataBind()
        'GridViewCostSummaryDollar.DataBind()
        'GridViewCostSummaryEuro.DataBind()
        'GridViewRubleToExcel.DataBind()
        'GridViewDollarToExcel.DataBind()
        'GridViewEuroToExcel.DataBind()
        'LabelProjectName.Text = "Report For " + DropDownListPrj.SelectedItem.Text + " in " + DropDownListCurrency.SelectedItem.Text
        'labelWhichCurrency.Text = DropDownListCurrency.SelectedItem.Text
        'End If

        If labelWhichCurrency.Text = "Rub" Then
            GridViewRubleToExcel.DataSource = SqlDataSourceRubleToExcel
            SqlDataSourceRubleToExcel.DataBind()
            GridViewRubleToExcel.DataBind()
            ExportToExcelOld("Report.xls", GridViewRubleToExcel)
        ElseIf labelWhichCurrency.Text = "Dollar" Then
            GridViewDollarToExcel.DataSource = SqlDataSourceDollarToExcel
            SqlDataSourceDollarToExcel.DataBind()
            GridViewDollarToExcel.DataBind()
            ExportToExcelOld("Report.xls", GridViewDollarToExcel)
        ElseIf labelWhichCurrency.Text = "Euro" Then
            GridViewEuroToExcel.DataSource = SqlDataSourceEuroToExcel
            SqlDataSourceEuroToExcel.DataBind()
            GridViewEuroToExcel.DataBind()
            ExportToExcelOld("Report.xls", GridViewEuroToExcel)
        End If

    End Sub

    Private Sub ExportToExcelOld(ByVal strFileName As String, ByVal dg As GridView)

        Response.Clear()
        Dim PrjName As String = DropDownListPrj.SelectedItem.ToString.Trim
        Dim Currency As String = labelWhichCurrency.Text.ToString
        Dim CostCode As String = LabelCriteriaCostCodeForExcel.Text.ToString
        If CostCode = "" Then
            Response.AddHeader("content-disposition", "attachment; filename=Report " + PrjName + " in " + Currency + " for ALL Cost Code " + ".xls")

        Else
            Response.AddHeader("content-disposition", "attachment; filename=Report " + PrjName + " in " + Currency + " for Cost Code " + CostCode + ".xls")
        End If

        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.Charset = ""
        Me.EnableViewState = False
        Dim oStringWriter As New System.IO.StringWriter
        Dim oHtmlTextWriter As New System.Web.UI.HtmlTextWriter(oStringWriter)

        If labelWhichCurrency.Text = "Rub" Then
            GridViewRubleToExcel.RenderControl(oHtmlTextWriter)
        ElseIf labelWhichCurrency.Text = "Dollar" Then
            GridViewDollarToExcel.RenderControl(oHtmlTextWriter)
        ElseIf labelWhichCurrency.Text = "Euro" Then
            GridViewEuroToExcel.RenderControl(oHtmlTextWriter)
        End If

        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        If CostCode = "" Then
            Response.Write("<p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span lang=EN-US style='color:red;mso-ansi-language:EN-US'>" + PrjName + " in " + Currency + " for ALL Cost Code " + ", printed by " + String.Format("{0:dddd, MMMM d, yyyy, h:mm tt}", result) + "<o:p></o:p></span></b></p>")
        Else
            Response.Write("<p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span lang=EN-US style='color:red;mso-ansi-language:EN-US'>" + PrjName + " in " + Currency + " for Cost Code " + CostCode + ", printed by " + String.Format("{0:dddd, MMMM d, yyyy, h:mm tt}", result) + "<o:p></o:p></span></b></p>")
        End If

        Response.Write(oStringWriter.ToString())
        Response.[End]()

    End Sub


    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Exit Sub
    End Sub

    Protected Sub GridViewRubleToExcel_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewRubleToExcel.RowDataBound
        Dim LabelInvoice_No As Label = DirectCast(e.Row.FindControl("LabelInvoice_No"), Label)
        Dim LabelInvoice_Date As Label = DirectCast(e.Row.FindControl("LabelInvoice_Date"), Label)
        Dim LabelInvoiceValue As Label = DirectCast(e.Row.FindControl("LabelInvoiceValue"), Label)
        Dim LabelSiteRecordNo As Label = DirectCast(e.Row.FindControl("LabelSiteRecordNo"), Label)
        Dim LabelPaymentDate As Label = DirectCast(e.Row.FindControl("LabelPaymentDate"), Label)
        Dim LabelRublePendingExcVAT As Label = DirectCast(e.Row.FindControl("LabelRublePendingExcVAT"), Label)
        Dim LabelRublePaidExcVAT As Label = DirectCast(e.Row.FindControl("LabelRublePaidExcVAT"), Label)
        Dim LabelOrderValueRuble As Label = DirectCast(e.Row.FindControl("LabelOrderValueRuble"), Label)
        Dim LabelVATpaidRuble As Label = DirectCast(e.Row.FindControl("LabelVATpaidRuble"), Label)

        If LabelInvoice_No IsNot Nothing AndAlso LabelInvoice_No.Text = "" Then
            LabelInvoice_No.Text = "Open"
            LabelInvoice_Date.Text = "Open"
            LabelInvoiceValue.Text = "Open"
            LabelSiteRecordNo.Text = "Open"
            LabelPaymentDate.Text = "Open"
            LabelRublePendingExcVAT.Text = "Open"
            LabelRublePaidExcVAT.Text = "Open"
            LabelVATpaidRuble.Text = "Open"

            LabelInvoice_No.Font.Italic = True
            LabelInvoice_Date.Font.Italic = True
            LabelInvoiceValue.Font.Italic = True
            LabelSiteRecordNo.Font.Italic = True
            LabelPaymentDate.Font.Italic = True
            LabelRublePendingExcVAT.Font.Italic = True
            LabelRublePaidExcVAT.Font.Italic = True
            LabelVATpaidRuble.Font.Italic = True

            LabelInvoice_No.ForeColor = System.Drawing.Color.Gray
            LabelInvoice_Date.ForeColor = System.Drawing.Color.Gray
            LabelInvoiceValue.ForeColor = System.Drawing.Color.Gray
            LabelSiteRecordNo.ForeColor = System.Drawing.Color.Gray
            LabelPaymentDate.ForeColor = System.Drawing.Color.Gray
            LabelRublePendingExcVAT.ForeColor = System.Drawing.Color.Gray
            LabelRublePaidExcVAT.ForeColor = System.Drawing.Color.Gray
            LabelOrderValueRuble.Font.Bold = True
            LabelVATpaidRuble.ForeColor = System.Drawing.Color.Gray

        End If

    End Sub

    Protected Sub GridViewDollarToExcel_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewDollarToExcel.RowDataBound
        Dim LabelInvoice_No As Label = DirectCast(e.Row.FindControl("LabelInvoice_No"), Label)
        Dim LabelInvoice_Date As Label = DirectCast(e.Row.FindControl("LabelInvoice_Date"), Label)
        Dim LabelInvoiceValue As Label = DirectCast(e.Row.FindControl("LabelInvoiceValue"), Label)
        Dim LabelSiteRecordNo As Label = DirectCast(e.Row.FindControl("LabelSiteRecordNo"), Label)
        Dim LabelPaymentDate As Label = DirectCast(e.Row.FindControl("LabelPaymentDate"), Label)
        Dim LabelDollarPendingExcVAT As Label = DirectCast(e.Row.FindControl("LabelDollarPendingExcVAT"), Label)
        Dim LabelDollarPaidExcVAT As Label = DirectCast(e.Row.FindControl("LabelDollarPaidExcVAT"), Label)
        Dim LabelOrderValueDollar As Label = DirectCast(e.Row.FindControl("LabelOrderValueDollar"), Label)
        Dim LabelVATpaidDollar As Label = DirectCast(e.Row.FindControl("LabelVATpaidDollar"), Label)

        If LabelInvoice_No IsNot Nothing AndAlso LabelInvoice_No.Text = "" Then
            LabelInvoice_No.Text = "Open"
            LabelInvoice_Date.Text = "Open"
            LabelInvoiceValue.Text = "Open"
            LabelSiteRecordNo.Text = "Open"
            LabelPaymentDate.Text = "Open"
            LabelDollarPendingExcVAT.Text = "Open"
            LabelDollarPaidExcVAT.Text = "Open"
            LabelVATpaidDollar.Text = "Open"

            LabelInvoice_No.Font.Italic = True
            LabelInvoice_Date.Font.Italic = True
            LabelInvoiceValue.Font.Italic = True
            LabelSiteRecordNo.Font.Italic = True
            LabelPaymentDate.Font.Italic = True
            LabelDollarPendingExcVAT.Font.Italic = True
            LabelDollarPaidExcVAT.Font.Italic = True
            LabelVATpaidDollar.Font.Italic = True

            LabelInvoice_No.ForeColor = System.Drawing.Color.Gray
            LabelInvoice_Date.ForeColor = System.Drawing.Color.Gray
            LabelInvoiceValue.ForeColor = System.Drawing.Color.Gray
            LabelSiteRecordNo.ForeColor = System.Drawing.Color.Gray
            LabelPaymentDate.ForeColor = System.Drawing.Color.Gray
            LabelDollarPendingExcVAT.ForeColor = System.Drawing.Color.Gray
            LabelDollarPaidExcVAT.ForeColor = System.Drawing.Color.Gray
            LabelOrderValueDollar.Font.Bold = True
            LabelVATpaidDollar.ForeColor = System.Drawing.Color.Gray

        End If
    End Sub

    Protected Sub GridViewEuroToExcel_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewEuroToExcel.RowDataBound
        Dim LabelInvoice_No As Label = DirectCast(e.Row.FindControl("LabelInvoice_No"), Label)
        Dim LabelInvoice_Date As Label = DirectCast(e.Row.FindControl("LabelInvoice_Date"), Label)
        Dim LabelInvoiceValue As Label = DirectCast(e.Row.FindControl("LabelInvoiceValue"), Label)
        Dim LabelSiteRecordNo As Label = DirectCast(e.Row.FindControl("LabelSiteRecordNo"), Label)
        Dim LabelPaymentDate As Label = DirectCast(e.Row.FindControl("LabelPaymentDate"), Label)
        Dim LabelEuroPendingExcVAT As Label = DirectCast(e.Row.FindControl("LabelEuroPendingExcVAT"), Label)
        Dim LabelEuroPaidExcVAT As Label = DirectCast(e.Row.FindControl("LabelEuroPaidExcVAT"), Label)
        Dim LabelOrderValueEuro As Label = DirectCast(e.Row.FindControl("LabelOrderValueEuro"), Label)
        Dim LabelVATpaidEuro As Label = DirectCast(e.Row.FindControl("LabelVATpaidEuro"), Label)

        If LabelInvoice_No IsNot Nothing AndAlso LabelInvoice_No.Text = "" Then
            LabelInvoice_No.Text = "Open"
            LabelInvoice_Date.Text = "Open"
            LabelInvoiceValue.Text = "Open"
            LabelSiteRecordNo.Text = "Open"
            LabelPaymentDate.Text = "Open"
            LabelEuroPendingExcVAT.Text = "Open"
            LabelEuroPaidExcVAT.Text = "Open"
            LabelVATpaidEuro.Text = "Open"

            LabelInvoice_No.Font.Italic = True
            LabelInvoice_Date.Font.Italic = True
            LabelInvoiceValue.Font.Italic = True
            LabelSiteRecordNo.Font.Italic = True
            LabelPaymentDate.Font.Italic = True
            LabelEuroPendingExcVAT.Font.Italic = True
            LabelEuroPaidExcVAT.Font.Italic = True
            LabelVATpaidEuro.Font.Italic = True

            LabelInvoice_No.ForeColor = System.Drawing.Color.Gray
            LabelInvoice_Date.ForeColor = System.Drawing.Color.Gray
            LabelInvoiceValue.ForeColor = System.Drawing.Color.Gray
            LabelSiteRecordNo.ForeColor = System.Drawing.Color.Gray
            LabelPaymentDate.ForeColor = System.Drawing.Color.Gray
            LabelEuroPendingExcVAT.ForeColor = System.Drawing.Color.Gray
            LabelEuroPaidExcVAT.ForeColor = System.Drawing.Color.Gray
            LabelOrderValueEuro.Font.Bold = True
            LabelVATpaidEuro.ForeColor = System.Drawing.Color.Gray

        End If
    End Sub

    Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        If IsPostBack Or Not IsPostBack Then
            If ReportViewerDeliveryReport.Visible Then
                'LabelVATstatus.Visible = True
                'LabelVATstatus.Text = "<div style=" + """" + "background-image: url('images/ExcVAT.png'); background-repeat: repeat-x; height: 15px;" + """" + "></div>"
            Else
                'LabelVATstatus.Visible = False
            End If
        End If
    End Sub

    Protected Sub ImageButtonExcelSummary_Click(sender As Object, e As System.EventArgs)

        RenderReport.Render("Excel", ReportViewerDeliveryReport, "_Nakl_FollowUpReportExcVATRev", "ProjectID", DropDownListPrj.SelectedValue.ToString, _
                            "Currency", DropDownListCurrency.SelectedValue.ToString)

    End Sub

End Class

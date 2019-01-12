Imports System.Net
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms
Imports Microsoft.ReportingServices
Imports System.Data.SqlClient

Partial Class _Nakl_CostReport_RRS__2
    Inherits System.Web.UI.Page

    Protected Sub AutoUpdatePlannedToSpend()

        If DropDownListPrj.SelectedItem.ToString <> "Select Project" Then

            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = "dbo.SP_CostReportAutoUpdatePlannedToSpend"
                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.StoredProcedure

                'syntax for parameter adding
                Dim ProjectId As SqlParameter = cmd.Parameters.Add("@ProjectId", System.Data.SqlDbType.SmallInt)
                ProjectId.Value = DropDownListPrj.SelectedValue
                Dim dr As SqlDataReader = cmd.ExecuteReader
                con.Close()
                dr.Close()
            End Using

        End If


    End Sub

    Protected Sub renderAllReports()
        ' determine if user is FinanceStaff or SiteStaff_ then run the report
        If DropDownListPrj.SelectedItem.ToString <> "Select Project" Then

            'If Not DropDownListPrj.SelectedValue = 210 Then
            AutoUpdatePlannedToSpend()
            'End If

            If DropDownListPrj.SelectedValue <> 900 AndAlso DropDownListPrj.SelectedValue <> 210 AndAlso DropDownListPrj.SelectedValue <> 212 Then
                ' not Asteros
                If Page.User.IsInRole("CostReport_SiteStaff") Then
                    If DropDownListCurrency.SelectedValue.ToString = "Dollar" Then
                        RenderReport.Render("HTML", ReportViewerCostReport, "_Nakl_CostReportInDollarWthSubPoWthPaidSite", "ProjectID", DropDownListPrj.SelectedValue)
                    ElseIf DropDownListCurrency.SelectedValue.ToString = "Euro" Then
                        RenderReport.Render("HTML", ReportViewerCostReport, "_Nakl_CostReportInEuroWthSubPoWthPaidSite", "ProjectID", DropDownListPrj.SelectedValue)
                    ElseIf DropDownListCurrency.SelectedValue.ToString = "Rub" Then
                        RenderReport.Render("HTML", ReportViewerCostReport, "_Nakl_CostReportInRubleWthSubPoWthPaidSite", "ProjectID", DropDownListPrj.SelectedValue)
                    End If
                ElseIf Page.User.IsInRole("CostReport_FinanceStaff") Then
                    If DropDownListCurrency.SelectedValue.ToString = "Dollar" Then
                        RenderReport.Render("HTML", ReportViewerCostReport, "_Nakl_CostReportInDollarWthSubPoWthPaid", "ProjectID", DropDownListPrj.SelectedValue)
                    ElseIf DropDownListCurrency.SelectedValue.ToString = "Euro" Then
                        RenderReport.Render("HTML", ReportViewerCostReport, "_Nakl_CostReportInEuroWthSubPoWthPaid", "ProjectID", DropDownListPrj.SelectedValue)
                    ElseIf DropDownListCurrency.SelectedValue.ToString = "Rub" Then
                        RenderReport.Render("HTML", ReportViewerCostReport, "_Nakl_CostReportInRubleWthSubPoWthPaid", "ProjectID", DropDownListPrj.SelectedValue)
                    End If
                End If
            ElseIf DropDownListPrj.SelectedValue = 900 Then
                RenderReport.Render("HTML", ReportViewerCostReport, "_Nakl_CostReportInEuroWthSubPoWthPaidAsterosME", "ProjectID", DropDownListPrj.SelectedValue)

            ElseIf DropDownListPrj.SelectedValue = 207 Then
                RenderReport.Render("HTML", ReportViewerCostReport, "_Nakl_CostReportInEuroWthSubPoWthPaidHyatElectrical207", "ProjectID", DropDownListPrj.SelectedValue)

            ElseIf DropDownListPrj.SelectedValue = 210 Then
                RenderReport.Render("HTML", ReportViewerCostReport, "_Nakl_CostReportInEuroWthSubPoWthPaidIKEA", "ProjectID", DropDownListPrj.SelectedValue)

            ElseIf DropDownListPrj.SelectedValue = 212 Then
                RenderReport.Render("HTML", ReportViewerCostReport, "_Nakl_CostReportInEuroWthSubPoWthPaid_BLD5", "ProjectID", DropDownListPrj.SelectedValue)

            End If

        Else
            ReportViewerCostReport.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.User.Identity.Name.ToLower = "tamas" _
            OrElse Page.User.Identity.Name.ToLower = "patrick" _
            OrElse Page.User.Identity.Name.ToLower = "savas" Then
            ButtonRunForTamasDCCRubleExcel.Visible = True
            ButtonRunForTamasDCCRubleHTML.Visible = True
            TextBoxExchangeRateTamas.Visible = True
        Else
            ButtonRunForTamasDCCRubleExcel.Visible = False
            ButtonRunForTamasDCCRubleHTML.Visible = False
            TextBoxExchangeRateTamas.Visible = False
        End If

        If Not IsPostBack Then
            TextBoxExchangeRateTamas.Text = getExchangeRateTamas()
        End If


        If Page.User.IsInRole("CostReport_SiteStaff") OrElse Page.User.IsInRole("CostReport_FinanceStaff") Then
        Else
            Response.Redirect("~/webforms/AccessDenied.aspx")
        End If

        ' it provides query parameter for DropDownListSupplier
        If IsPostBack Or Not IsPostBack Then
            TextBoxUserName.Text = Page.User.Identity.Name
        End If

        DataListOpenDocuments.DataBind()
        If DataListOpenDocuments.Items.Count > 0 Then
            If Page.User.IsInRole("CostStaffActive") Then
                DataListOpenDocuments.Visible = True
                labelWarning.Visible = True
            Else
                DataListOpenDocuments.Visible = False
                labelWarning.Visible = False
            End If
        Else
            DataListOpenDocuments.Visible = False
            labelWarning.Visible = False
        End If


    End Sub

    Protected Sub ButtonRunReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonRunReport.Click
        'DropDownListCurrency.DataBind()

    End Sub

    Protected Sub DropDownListPrj_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.DataBound
        If Not IsPostBack Then
            Dim lst1 As New ListItem("Select Project", "0")
            Me.DropDownListPrj.Items.Insert(0, lst1)
        End If
    End Sub

    Protected Sub DropDownListPrj_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.SelectedIndexChanged
        DropDownListCurrency.DataBind()
        Dim QueryStrings As String = "?ProjectID=" + DropDownListPrj.SelectedValue.ToString + "&Currency=" + DropDownListCurrency.SelectedValue.ToString
        'HyperLinkEditData.Attributes.Add("onclick", "javascript:w= window.open('CostReportEdit.aspx" + QueryStrings + "','CostReportEdit','left=100,top=10,width=1150,height=700,toolbar=0,resizable=0,scrollbars=yes');")

        HyperLinkEditData.NavigateUrl = "~/webforms/CostReportEdit.aspx" + QueryStrings

        HyperLinkSubPoList.Attributes.Add("onclick", "javascript:w= window.open('SubPoList.aspx?ProjectID=" + DropDownListPrj.SelectedValue.ToString + "','SubPoList','');")

        ' show EDIT button only if DDL has a project value and user has Active role
        If Len(DropDownListPrj.SelectedValue.ToString) > 0 Then
            If DropDownListPrj.SelectedItem.ToString <> "Select Project" Then
                If Page.User.IsInRole("CostStaffActive") Then
                    ' activate later when you fix report edit problem
                    HyperLinkEditData.Visible = True
                End If
            Else
                HyperLinkEditData.Visible = False
            End If
        End If

        renderAllReports()

    End Sub

    Protected Sub ImageButtonExportExcel_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonExportExcel.Click
        ' determine if user is FinanceStaff or SiteStaff_ then run the report
        If DropDownListPrj.SelectedItem.ToString <> "Select Project" Then

            If DropDownListPrj.SelectedValue <> 900 AndAlso DropDownListPrj.SelectedValue <> 210 AndAlso DropDownListPrj.SelectedValue <> 212 Then
                ' not Asteros
                If Page.User.IsInRole("CostReport_SiteStaff") Then
                    If DropDownListCurrency.SelectedValue.ToString = "Dollar" Then
                        RenderReport.Render("Excel", ReportViewerCostReport, "_Nakl_CostReportInDollarWthSubPoWthPaidSite", "ProjectID", DropDownListPrj.SelectedValue)
                    ElseIf DropDownListCurrency.SelectedValue.ToString = "Euro" Then
                        RenderReport.Render("Excel", ReportViewerCostReport, "_Nakl_CostReportInEuroWthSubPoWthPaidSite", "ProjectID", DropDownListPrj.SelectedValue)
                    ElseIf DropDownListCurrency.SelectedValue.ToString = "Rub" Then
                        RenderReport.Render("Excel", ReportViewerCostReport, "_Nakl_CostReportInRubleWthSubPoWthPaidSite", "ProjectID", DropDownListPrj.SelectedValue)
                    End If
                ElseIf Page.User.IsInRole("CostReport_FinanceStaff") Then
                    If DropDownListCurrency.SelectedValue.ToString = "Dollar" Then
                        RenderReport.Render("Excel", ReportViewerCostReport, "_Nakl_CostReportInDollarWthSubPoWthPaid", "ProjectID", DropDownListPrj.SelectedValue)
                    ElseIf DropDownListCurrency.SelectedValue.ToString = "Euro" Then
                        RenderReport.Render("Excel", ReportViewerCostReport, "_Nakl_CostReportInEuroWthSubPoWthPaid", "ProjectID", DropDownListPrj.SelectedValue)
                    ElseIf DropDownListCurrency.SelectedValue.ToString = "Rub" Then
                        RenderReport.Render("Excel", ReportViewerCostReport, "_Nakl_CostReportInRubleWthSubPoWthPaid", "ProjectID", DropDownListPrj.SelectedValue)
                    End If
                End If
            ElseIf DropDownListPrj.SelectedValue = 900 Then
                RenderReport.Render("Excel", ReportViewerCostReport, "_Nakl_CostReportInEuroWthSubPoWthPaidAsterosME", "ProjectID", DropDownListPrj.SelectedValue, "excel", 1)

            ElseIf DropDownListPrj.SelectedValue = 207 Then
                RenderReport.Render("Excel", ReportViewerCostReport, "_Nakl_CostReportInEuroWthSubPoWthPaidHyatElectrical207", "ProjectID", DropDownListPrj.SelectedValue)

            ElseIf DropDownListPrj.SelectedValue = 210 Then
                RenderReport.Render("Excel", ReportViewerCostReport, "_Nakl_CostReportInEuroWthSubPoWthPaidIKEA", "ProjectID", DropDownListPrj.SelectedValue, "excel", 1)

            ElseIf DropDownListPrj.SelectedValue = 212 Then
                RenderReport.Render("Excel", ReportViewerCostReport, "_Nakl_CostReportInEuroWthSubPoWthPaid_BLD5", "ProjectID", DropDownListPrj.SelectedValue, "excel", 1)

            End If

        Else
            ReportViewerCostReport.Visible = False
        End If
    End Sub

    Protected Sub ImageButtonRefresh_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonRefresh.Click
        renderAllReports()
    End Sub

    Protected Sub DataListOpenDocuments_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DataListOpenDocuments.ItemDataBound
        ' it cause imagebutton to postback. Otherwise, openExcel button does not work.
        If DirectCast(e.Item.FindControl("HyperLinkSubPo"), HyperLink) IsNot Nothing Then
            DirectCast(e.Item.FindControl("HyperLinkSubPo"), HyperLink).Attributes.Add("onclick", "javascript:w= window.open('POsub.aspx?Po_No=" + DataBinder.Eval(e.Item.DataItem, "PO_No").ToString + "','POsub','left=100,top=100,width=750,height=600,toolbar=0,resizable=0,scrollbars=yes');")
        End If
    End Sub

    Protected Sub DataListOpenDocuments_Load(sender As Object, e As System.EventArgs) Handles DataListOpenDocuments.Load
    End Sub

    Protected Sub ButtonRunForTamasDCCRubleHTML_Click(sender As Object, e As System.EventArgs) Handles ButtonRunForTamasDCCRubleHTML.Click
        UpdateExchangeRateTamas()
        TextBoxExchangeRateTamas.Text = getExchangeRateTamas()
        RenderReport.Render("HTML", ReportViewerCostReport, "_Nakl_CostReportInRubleWthSubPoWthPaid_DataCenter", "ProjectID", 108)
    End Sub

    Protected Sub ButtonRunForTamasDCCRubleExcel_Click(sender As Object, e As System.EventArgs) Handles ButtonRunForTamasDCCRubleExcel.Click
        UpdateExchangeRateTamas()
        TextBoxExchangeRateTamas.Text = getExchangeRateTamas()
        RenderReport.Render("Excel", ReportViewerCostReport, "_Nakl_CostReportInRubleWthSubPoWthPaid_DataCenter", "ProjectID", 108)
    End Sub

    Protected Function getExchangeRateTamas() As Decimal

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT [ExchangeRate_TamasProvide] FROM [Table_ExchangeRateTamas] WHERE ProjectId = 108 "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            Dim _return As Decimal = 0
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                _return = dr(0)
            End While

            Return _return

            con.Close()
            dr.Close()
            con.Dispose()

        End Using

    End Function

    Protected Sub UpdateExchangeRateTamas()

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " UPDATE [Table_ExchangeRateTamas] SET ExchangeRate_TamasProvide = @ExchangeRate_TamasProvide WHERE ProjectId = 108 "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            Dim ExchangeRate_TamasProvide As SqlParameter = cmd.Parameters.Add("@ExchangeRate_TamasProvide", System.Data.SqlDbType.Decimal)
            ExchangeRate_TamasProvide.Value = Convert.ToDecimal(TextBoxExchangeRateTamas.Text)

            Dim dr As SqlDataReader = cmd.ExecuteReader

            con.Close()
            dr.Close()
            con.Dispose()

        End Using

    End Sub

End Class

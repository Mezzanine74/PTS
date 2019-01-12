Imports System.Net
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms
Imports Microsoft.ReportingServices
Imports System.Data.SqlClient

Partial Class ProjectBalanceBreakdown
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' it provides query parameter for DropDownListSupplier
        If IsPostBack Or Not IsPostBack Then
            If Not User.IsInRole("FollowUpReports") Then
                Response.Redirect("~/webforms/AccessDenied.aspx")
            Else
                TextBoxUserName.Text = Page.User.Identity.Name
            End If
        End If
    End Sub

    Public Sub RenderReport_(ByVal _ReportPath As String)

        RenderReport.Render("html", ReportViewerProjectBalanceBreakdown, _ReportPath, "ProjectName", DropDownListPrj.SelectedValue)

    End Sub

    Public Sub RenderReportToExcel(ByVal _ReportPath As String)

        RenderReport.Render("excel", ReportViewerProjectBalanceBreakdown, _ReportPath, "ProjectName", DropDownListPrj.SelectedValue)

    End Sub


    Protected Sub ButtonRunReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonRunReport.Click

        ' determine if user is FinanceStaff or SiteStaff_ then run the report
        If DropDownListPrj.SelectedItem.ToString <> "Select Project" Then
            If RadioButtonListReportType.SelectedValue = 1 Then
                If DropDownListCurrency.SelectedValue.ToString = "Dollar" Then
                    RenderReport_("ProjectBalanceBreakdown_Dollar")
                ElseIf DropDownListCurrency.SelectedValue.ToString = "Euro" Then
                    RenderReport_("ProjectBalanceBreakdown_Euro")
                ElseIf DropDownListCurrency.SelectedValue.ToString = "Rub" Then
                    RenderReport_("ProjectBalanceBreakdown_Ruble")
                End If
            ElseIf RadioButtonListReportType.SelectedValue = 2 Then

                RenderReport.Render("html", ReportViewerProjectBalanceBreakdown, "ProjectBalanceBreakdown", "ProjectID", GetProjectID(DropDownListPrj.SelectedValue.ToString), _
                                    "Currency", DropDownListCurrency.SelectedValue.ToString)

            ElseIf RadioButtonListReportType.SelectedValue = 3 Then
                If DropDownListCurrency.SelectedValue.ToString = "Dollar" Then
                    RenderReport_("ProjectBalanceBreakdown_DollarWthPendingBalance")
                ElseIf DropDownListCurrency.SelectedValue.ToString = "Euro" Then
                    RenderReport_("ProjectBalanceBreakdown_EuroWthPendingBalance")
                ElseIf DropDownListCurrency.SelectedValue.ToString = "Rub" Then
                    RenderReport_("ProjectBalanceBreakdown_RubleWthPendingBalance")
                End If
            End If
        Else
            ReportViewerProjectBalanceBreakdown.Visible = False
        End If


    End Sub

    Protected Sub DropDownListPrj_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.DataBound
        If Not IsPostBack Then
            Dim lst1 As New ListItem("Select Project", "")
            Me.DropDownListPrj.Items.Insert(0, lst1)
        End If
    End Sub


    Protected Sub DropDownListPrj_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.SelectedIndexChanged

    End Sub

    Protected Sub ButtonExportToExcel_Click(sender As Object, e As System.EventArgs) Handles ButtonExportToExcel.Click

        If DropDownListPrj.SelectedItem.ToString <> "Select Project" Then
            If RadioButtonListReportType.SelectedValue = 1 Then
                If DropDownListCurrency.SelectedValue.ToString = "Dollar" Then
                    RenderReportToExcel("ProjectBalanceBreakdown_Dollar")
                ElseIf DropDownListCurrency.SelectedValue.ToString = "Euro" Then
                    RenderReportToExcel("ProjectBalanceBreakdown_Euro")
                ElseIf DropDownListCurrency.SelectedValue.ToString = "Rub" Then
                    RenderReportToExcel("ProjectBalanceBreakdown_Ruble")
                End If
            ElseIf RadioButtonListReportType.SelectedValue = 2 Then

                RenderReport.Render("excel", ReportViewerProjectBalanceBreakdown, "ProjectBalanceBreakdown", "ProjectID", GetProjectID(DropDownListPrj.SelectedValue.ToString), _
                                    "Currency", DropDownListCurrency.SelectedValue.ToString)

            ElseIf RadioButtonListReportType.SelectedValue = 3 Then
                If DropDownListCurrency.SelectedValue.ToString = "Dollar" Then
                    RenderReportToExcel("ProjectBalanceBreakdown_DollarWthPendingBalance")
                ElseIf DropDownListCurrency.SelectedValue.ToString = "Euro" Then
                    RenderReportToExcel("ProjectBalanceBreakdown_EuroWthPendingBalance")
                ElseIf DropDownListCurrency.SelectedValue.ToString = "Rub" Then
                    RenderReportToExcel("ProjectBalanceBreakdown_RubleWthPendingBalance")
                End If
            End If
        Else
            ReportViewerProjectBalanceBreakdown.Visible = False
        End If

    End Sub

    Protected Function GetProjectID(ByVal _ProjectName As String) As Integer

        Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        Using con
            con.Open()
            Dim sqlstring As String = " SELECT ProjectID FROM Table1_Project WHERE ProjectName = @ProjectName "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim ProjectName As SqlParameter = cmd.Parameters.Add("@ProjectName", System.Data.SqlDbType.NVarChar, 256)
            ProjectName.Value = _ProjectName
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim _return As Integer = 0
            While dr.Read
                _return = dr(0)
            End While
            Return _return
            con.Close()
            dr.Close()
            con.Dispose()
        End Using

    End Function

End Class

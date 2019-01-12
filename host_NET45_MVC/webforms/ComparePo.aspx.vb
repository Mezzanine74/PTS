Imports System.Net
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms
Imports Microsoft.ReportingServices
Imports System.Data.SqlClient
Partial Class ComparePo
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ' it provides query parameter for DropDownListSupplier
        If IsPostBack Or Not IsPostBack Then
            TextBoxUserName.Text = Page.User.Identity.Name
        End If

        If Not IsPostBack Then
            If Request.QueryString("ProjectID") IsNot Nothing _
                And Request.QueryString("StartDate") IsNot Nothing _
                And Request.QueryString("FinishDate") IsNot Nothing Then
                RenderReport.Render("html", ReportViewerComparePo, "ComparePo_Euro", "ProjectID", Request.QueryString("ProjectID"), _
                                        "StartDate", Request.QueryString("StartDate"), _
                                        "FinishDate", Request.QueryString("FinishDate"))
                Exit Sub
            End If
        End If

    End Sub

    Protected Sub ButtonRunReport_Click(sender As Object, e As System.EventArgs) Handles ButtonRunReport.Click
        If DropDownListStartDate.SelectedIndex <> 0 OrElse DropDownListFinishDate.SelectedIndex <> 0 Then
            Dim StartDate As DateTime = Convert.ToDateTime(DropDownListStartDate.SelectedValue.ToString)
            Dim FinishDate As DateTime = Convert.ToDateTime(DropDownListFinishDate.SelectedValue.ToString)

            If StartDate >= FinishDate Then
                LabelWarning.Text = "Finish Date must be greater than Start Date"
            Else
                LabelWarning.Text = ""
                ' run report
                If DropDownListPrj.SelectedItem.ToString <> "Select Project" Then
                    If DropDownListCurrency.SelectedValue.ToString = "Dollar" Then
                        RenderReport.Render("html", ReportViewerComparePo, "ComparePo_Dollar", "ProjectID", _DropDownListPrj.SelectedValue, _
                                            "StartDate", DropDownListStartDate.SelectedValue.ToString, _
                                            "FinishDate", DropDownListFinishDate.SelectedValue.ToString)
                    ElseIf DropDownListCurrency.SelectedValue.ToString = "Euro" Then
                        RenderReport.Render("html", ReportViewerComparePo, "ComparePo_Euro", "ProjectID", _DropDownListPrj.SelectedValue, _
                                            "StartDate", DropDownListStartDate.SelectedValue.ToString, _
                                            "FinishDate", DropDownListFinishDate.SelectedValue.ToString)
                    ElseIf DropDownListCurrency.SelectedValue.ToString = "Ruble" Then
                        RenderReport.Render("html", ReportViewerComparePo, "ComparePo_Ruble", "ProjectID", _DropDownListPrj.SelectedValue, _
                                            "StartDate", DropDownListStartDate.SelectedValue.ToString, _
                                            "FinishDate", DropDownListFinishDate.SelectedValue.ToString)
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub DropDownListPrj_DataBound(sender As Object, e As System.EventArgs) Handles DropDownListPrj.DataBound
        If Not IsPostBack Then
            Dim lst1 As New ListItem("Select Project", "0")
            Me.DropDownListPrj.Items.Insert(0, lst1)
        End If
    End Sub

    Protected Sub ButtonExportToExcel_Click(sender As Object, e As System.EventArgs) Handles ButtonExportToExcel.Click
        If DropDownListStartDate.SelectedIndex <> 0 OrElse DropDownListFinishDate.SelectedIndex <> 0 Then
            Dim StartDate As DateTime = Convert.ToDateTime(DropDownListStartDate.SelectedValue.ToString)
            Dim FinishDate As DateTime = Convert.ToDateTime(DropDownListFinishDate.SelectedValue.ToString)

            If StartDate >= FinishDate Then
                LabelWarning.Text = "Finish Date must be greater than Start Date"
            Else
                LabelWarning.Text = ""

                ' run report
                If DropDownListPrj.SelectedItem.ToString <> "Select Project" Then
                    If DropDownListCurrency.SelectedValue.ToString = "Dollar" Then
                        RenderReport.Render("excel", ReportViewerComparePo, "ComparePo_Dollar", "ProjectID", _DropDownListPrj.SelectedValue, _
                                            "StartDate", DropDownListStartDate.SelectedValue.ToString, _
                                            "FinishDate", DropDownListFinishDate.SelectedValue.ToString)
                    ElseIf DropDownListCurrency.SelectedValue.ToString = "Euro" Then
                        RenderReport.Render("excel", ReportViewerComparePo, "ComparePo_Euro", "ProjectID", _DropDownListPrj.SelectedValue, _
                                            "StartDate", DropDownListStartDate.SelectedValue.ToString, _
                                            "FinishDate", DropDownListFinishDate.SelectedValue.ToString)
                    ElseIf DropDownListCurrency.SelectedValue.ToString = "Ruble" Then
                        RenderReport.Render("excel", ReportViewerComparePo, "ComparePo_Ruble", "ProjectID", _DropDownListPrj.SelectedValue, _
                                            "StartDate", DropDownListStartDate.SelectedValue.ToString, _
                                            "FinishDate", DropDownListFinishDate.SelectedValue.ToString)
                    End If
                End If
            End If
        End If
    End Sub

End Class
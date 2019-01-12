Imports System.Data.SqlClient
Imports AjaxControlToolkit

Partial Class Monitoring
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Or Not IsPostBack Then

            If Page.User.Identity.Name.ToLower = "galkin" OrElse Page.User.Identity.Name.ToLower = "savas" Then
                LinkButtonItPayments.Visible = True
            Else
                LinkButtonItPayments.Visible = False
            End If

            ' it provides query parameter for DropDownListProject
            If Page.User.Identity.Name.ToLower = "loseva" OrElse _
              Page.User.Identity.Name.ToLower = "mikhaleva" Then
                TextBoxUserName.Text = "savas"
            Else
                TextBoxUserName.Text = Page.User.Identity.Name
            End If
        End If

    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Exit Sub
    End Sub

    Protected Sub GridViewMonitor_PageIndexChanged(sender As Object, e As EventArgs) Handles GridViewMonitor.PageIndexChanged

        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalGridResult" + "').modal({}) });", True)

    End Sub

    Protected Sub GridViewMonitor_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewMonitor.PreRender

    End Sub

    Protected Sub GridViewMonitor_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewMonitor.RowCommand
        If (e.CommandName = "OpenPdf") Then
            Dim LinkToFile As String = e.CommandArgument.ToString
            Dim openpdf As New MyCommonTasks
            openpdf.OpenPDF(LinkToFile)
        End If
    End Sub

    Protected Sub GridViewMonitor_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewMonitor.RowDataBound

        Dim MyTask As New MyCommonTasks
        MyTask.HoverEffectOnGridviewCells(sender, e.Row)

        If e.Row.RowType = DataControlRowType.DataRow Then
            'it defines type of PDF image if it exist or not.
            If DataBinder.Eval(e.Row.DataItem, "AttachmentExist") = True Then
                DirectCast(e.Row.FindControl("ImageButtonPdf"), ImageButton).ImageUrl = "~/Images/pdf.bmp"
            Else
                DirectCast(e.Row.FindControl("ImageButtonPdf"), ImageButton).ImageUrl = "~/Images/pdf_bw.bmp"
            End If
        End If

        ' It fixes column width problem
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Label1 As Label = DirectCast(e.Row.FindControl("Label2"), Label)

            If Label1 IsNot Nothing Then
                Label1.Text = Label1.Text.Replace(",", "," + " ")
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LabelSupplierID As Label = DirectCast(e.Row.FindControl("LabelSupplierID"), Label)
            If Len(LabelSupplierID.Text.Trim) = 12 Then
                LabelSupplierID.ForeColor = System.Drawing.Color.Green
            End If

            ' hide or show details link
            Dim HypSupplierDetails As HyperLink = DirectCast(e.Row.FindControl("HypSupplierDetails"), HyperLink)
            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = " SELECT COUNT([SupplierID]) FROM [dbo].[Table6_SupplierDetails] Where SupplierID = @SupplierID "
                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.Text

                'syntax for parameter adding
                Dim SupplierID As SqlParameter = cmd.Parameters.Add("@SupplierID", System.Data.SqlDbType.NVarChar, 12)
                SupplierID.Value = DataBinder.Eval(e.Row.DataItem, "SupplierID").ToString
                Dim dr As SqlDataReader = cmd.ExecuteReader
                While dr.Read
                    If dr(0) > 0 Then
                        HypSupplierDetails.Visible = True
                    Else
                        HypSupplierDetails.Visible = False
                    End If
                End While
                con.Close()
                dr.Close()
            End Using

        End If

    End Sub

    Protected Sub GridViewSupplier_PageIndexChanged(sender As Object, e As EventArgs) Handles GridViewSupplier.PageIndexChanged



    End Sub

    Protected Sub GridViewSupplier_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewSupplier.PreRender
        '' Define SelectCommand statement based on Selected PrjValue
        '' SHOW GRANTED PROJECT
        'If Not IsPostBack Then
        '    ' select all suppliers based on UserName
        '    SqlDataSourceMonitoringSupplier.SelectParameters("Criteria").DefaultValue = 1
        '    SqlDataSourceMonitoringSupplier.SelectParameters("ProjectID").DefaultValue = DropDownListProject.SelectedValue.ToString
        '    SqlDataSourceMonitoringSupplier.SelectParameters("UserName").DefaultValue = Page.User.Identity.Name.ToString

        'ElseIf IsPostBack Then
        '    ' select suppliers based on username and PrjValue selected
        '    If DropDownListProject.SelectedValue = 0 Then
        '        ' All projects selected. Select suppliers for all projects for this user
        '        SqlDataSourceMonitoringSupplier.SelectParameters("Criteria").DefaultValue = 2
        '        SqlDataSourceMonitoringSupplier.SelectParameters("ProjectID").DefaultValue = DropDownListProject.SelectedValue.ToString
        '        SqlDataSourceMonitoringSupplier.SelectParameters("UserName").DefaultValue = Page.User.Identity.Name.ToString

        '    Else
        '        ' select suppliers for specific selected project for this user.
        '        SqlDataSourceMonitoringSupplier.SelectParameters("Criteria").DefaultValue = 3
        '        SqlDataSourceMonitoringSupplier.SelectParameters("ProjectID").DefaultValue = DropDownListProject.SelectedValue.ToString
        '        SqlDataSourceMonitoringSupplier.SelectParameters("UserName").DefaultValue = Page.User.Identity.Name.ToString

        '    End If
        'End If
    End Sub

    Protected Sub GridViewSupplier_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewSupplier.RowCommand
        If (e.CommandName = "FindSupplierName") Then
            labelSupplierIDTransfer.Text = e.CommandArgument.ToString
            labelAllSuppliersMode.Text = "False"

            SqlDataSourceSupplierModal.SelectParameters("ProjectID").DefaultValue = DropDownListProject.SelectedValue
            SqlDataSourceSupplierModal.SelectParameters("SupplierID").DefaultValue = labelSupplierIDTransfer.Text
            SqlDataSourceSupplierModal.SelectParameters("UserName").DefaultValue = Page.User.Identity.Name.ToLower

            GridViewSupplierModal.DataBind()

            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalGridResult" + "').modal({}) });", True)

        End If
    End Sub

    Protected Sub GridViewSupplier_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewSupplier.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim HyperlinkDocPTSValue As HyperLink = DirectCast(e.Row.FindControl("HyperlinkDocPTSValue"), HyperLink)
            Dim HyperlinkDoc1SValue As HyperLink = DirectCast(e.Row.FindControl("HyperlinkDoc1SValue"), HyperLink)
            If DropDownListProject.SelectedValue = 0 Then
                HyperlinkDocPTSValue.NavigateUrl = String.Empty
                HyperlinkDoc1SValue.NavigateUrl = String.Empty
            Else
                HyperlinkDocPTSValue.NavigateUrl = "~/webforms/nakladnaya.aspx?ProjectID=" + DataBinder.Eval(e.Row.DataItem, "ProjectID").ToString + "&SupplierID=" + DataBinder.Eval(e.Row.DataItem, "SupplierID").ToString
                HyperlinkDoc1SValue.NavigateUrl = "~/webforms/PTS_1S_DeliveryDocComparisonGrid.aspx?ProjectID=" + DataBinder.Eval(e.Row.DataItem, "ProjectID").ToString + "&SupplierID=" + DataBinder.Eval(e.Row.DataItem, "SupplierID").ToString
            End If
        End If

        If e.Row.RowType = DataControlRowType.Header Then
            Dim AutoCompleteExtenderSupplier As AutoCompleteExtender = DirectCast(e.Row.FindControl("AutoCompleteExtenderSupplier"), AutoCompleteExtender)
            If AutoCompleteExtenderSupplier IsNot Nothing Then
                AutoCompleteExtenderSupplier.ContextKey = DropDownListProject.SelectedValue
            End If

        End If


    End Sub

    Protected Sub LinkButtonAllProjects_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        labelAllSuppliersMode.Text = "True"

        'SqlDataSourceMonitor.DataBind()
        'GridViewMonitor.DataBind()
    End Sub

    Protected Sub GridViewSupplier_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewSupplier.Load

    End Sub

    Protected Sub LinkButtonItPayments_Click(sender As Object, e As System.EventArgs) Handles LinkButtonItPayments.Click
        Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
        RenderReport.Render("disc", ReportViewer_, "IT_Payments", _
                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                            "ItPayments/ITpayments" + UniqueString1 + ".xls")

        Dim openpdf As New MyCommonTasks
        openpdf.OpenPDF("~/ItPayments/ITpayments" + UniqueString1 + ".xls")
    End Sub

    Protected Sub DropDownListProject_SelectedIndexChanged(sender As Object, e As EventArgs)

        labelSupplierIDTransfer.Text = ""
        labelAllSuppliersMode.Text = "False"

    End Sub

    Protected Sub DropDownListProject_DataBound(sender As Object, e As EventArgs)

        If Not IsPostBack Then
            sender.SelectedIndex = 0
        End If

    End Sub

    Protected Sub GridViewSupplier_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridViewSupplier.PageIndexChanging



    End Sub

    Protected Sub ImageButtonExcel_Click(sender As Object, e As EventArgs)

        Dim grd As GridView = WebUserControl_MonitoringExcelOutput.FindControl("GridViewExcel")

        labelSupplierIDTransfer.Text = "0"

        grd.DataSource = SqlDataSourceMonitor
        grd.DataBind()
        grd.AllowPaging = False

        PTSMainClass.ExportGridExcel(grd, "monitoring")

    End Sub

    Protected Sub TextBoxFindSupplier_TextChanged(sender As Object, e As EventArgs)

        Try
            Dim Txt As TextBox = sender

            labelSupplierIDTransfer.Text = Mid(Txt.Text.Trim, 1, Txt.Text.IndexOf(" "))
            labelAllSuppliersMode.Text = "False"

            SqlDataSourceSupplierModal.SelectParameters("ProjectID").DefaultValue = DropDownListProject.SelectedValue
            SqlDataSourceSupplierModal.SelectParameters("SupplierID").DefaultValue = labelSupplierIDTransfer.Text
            SqlDataSourceSupplierModal.SelectParameters("UserName").DefaultValue = Page.User.Identity.Name.ToLower

            GridViewSupplierModal.DataBind()

            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalGridResult" + "').modal({}) });", True)

            Txt.Text = String.Empty

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub GridViewSupplierModal_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewSupplierModal.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim HyperlinkDocPTSValue As HyperLink = DirectCast(e.Row.FindControl("HyperlinkDocPTSValue"), HyperLink)
            Dim HyperlinkDoc1SValue As HyperLink = DirectCast(e.Row.FindControl("HyperlinkDoc1SValue"), HyperLink)
            If DropDownListProject.SelectedValue = 0 Then
                HyperlinkDocPTSValue.NavigateUrl = String.Empty
                HyperlinkDoc1SValue.NavigateUrl = String.Empty
            Else
                HyperlinkDocPTSValue.NavigateUrl = "~/webforms/nakladnaya.aspx?ProjectID=" + DataBinder.Eval(e.Row.DataItem, "ProjectID").ToString + "&SupplierID=" + DataBinder.Eval(e.Row.DataItem, "SupplierID").ToString
                HyperlinkDoc1SValue.NavigateUrl = "~/webforms/PTS_1S_DeliveryDocComparisonGrid.aspx?ProjectID=" + DataBinder.Eval(e.Row.DataItem, "ProjectID").ToString + "&SupplierID=" + DataBinder.Eval(e.Row.DataItem, "SupplierID").ToString
            End If

        End If

    End Sub

    Protected Sub LinkButtonExcelOnModal_Click(sender As Object, e As EventArgs)

        Dim grd As GridView = WebUserControl_MonitoringExcelOutput.FindControl("GridViewExcel")

        grd.DataSource = SqlDataSourceMonitor
        grd.DataBind()
        grd.AllowPaging = False

        PTSMainClass.ExportGridExcel(grd, "monitoring")

    End Sub

    Protected Sub SqlDataSourceMonitoringSupplier_Init(sender As Object, e As EventArgs)

        Dim sql As SqlDataSource = sender
        sql.SelectParameters("UserName").DefaultValue = Page.User.Identity.Name.ToLower

    End Sub

    Protected Sub SqlDataSourceMonitor_Init(sender As Object, e As EventArgs)

        Dim sql As SqlDataSource = sender
        sql.SelectParameters("UserName").DefaultValue = Page.User.Identity.Name.ToLower

    End Sub

    Protected Sub SqlDataSourceSupplierModal_Init(sender As Object, e As EventArgs)

        Dim sql As SqlDataSource = sender
        sql.SelectParameters("UserName").DefaultValue = Page.User.Identity.Name.ToLower

    End Sub
End Class

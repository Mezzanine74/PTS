Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Data.SqlClient
Imports System.IO


Partial Class PaymentsASDFASDDF2
    Inherits System.Web.UI.Page

    Protected Sub GridViewDSP3_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewDSP3.RowCommand
        If (e.CommandName = "OpenPdf") Then
            Dim LinkToFile As String = e.CommandArgument.ToString
            Dim openpdf As New MyCommonTasks
            openpdf.OpenPDF(LinkToFile)
        End If
    End Sub

    Protected Sub GridViewDSP3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewDSP3.RowDataBound

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

    End Sub

    'Private Sub ExportToExcel(ByVal strFileName As String, ByVal dg As GridView)


    '    Response.Clear()
    '    Response.ContentType = "application/vnd.xls"
    '    Response.AddHeader("content-disposition", "attachment;filename=readings.xls")

    '    Dim swriter As New StringWriter()
    '    Dim hwriter As New HtmlTextWriter(swriter)

    '    Dim frm As New HtmlForm()
    '    Me.GridViewDSP3HIDDEN.Parent.Controls.Add(frm)
    '    frm.Attributes("runat") = "server"
    '    frm.Controls.Add(Me.GridViewDSP3HIDDEN)
    '    frm.RenderControl(hwriter)

    '    Response.Write(swriter.ToString())
    '    Response.[End]()

    'End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Exit Sub
    End Sub


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim zoneId As String = "Russian Standard Time"
            Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
            Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

            Dim today As String = (Mid(result.ToString, 1, 2) + "/" + Mid(result.ToString, 4, 2) + "/" + Mid(result.ToString, 7, 4)).ToString

            TextBoxDateRange.Text = today + " - " + today

        End If

        PrepareStartFinishParameters()

        ' project
        'SqlDataSourcePrj.SelectParameters("Start").DefaultValue = (Mid(start, 7, 4) + "/" + Mid(start, 4, 2) + "/" + Mid(start, 1, 2)).ToString
        'SqlDataSourcePrj.SelectParameters("Start").Type = TypeCode.DateTime

        'SqlDataSourcePrj.SelectParameters("Finish").DefaultValue = (Mid(finish, 7, 4) + "/" + Mid(finish, 4, 2) + "/" + Mid(finish, 1, 2)).ToString
        'SqlDataSourcePrj.SelectParameters("Finish").Type = TypeCode.DateTime

        'DropDownListPrj.DataBind()
        'DropDownListPrj.SelectedValue = Session("SelectedProject")

        ' supplier
        'SqlDataSourceSupplier.SelectParameters("Start").DefaultValue = (Mid(start, 7, 4) + "/" + Mid(start, 4, 2) + "/" + Mid(start, 1, 2)).ToString
        'SqlDataSourceSupplier.SelectParameters("Start").Type = TypeCode.DateTime

        'SqlDataSourceSupplier.SelectParameters("Finish").DefaultValue = (Mid(finish, 7, 4) + "/" + Mid(finish, 4, 2) + "/" + Mid(finish, 1, 2)).ToString
        'SqlDataSourceSupplier.SelectParameters("Finish").Type = TypeCode.DateTime

        'SqlDataSourceSupplier.SelectParameters("ProjectID").DefaultValue = DropDownListPrj.SelectedValue
        'SqlDataSourceSupplier.SelectParameters("ProjectID").Type = TypeCode.Int16

        'DropDownListSupplier.DataBind()

        'lbl.Text = Session("SelectedProject") + " " + (Mid(start, 7, 4) + "/" + Mid(start, 4, 2) + "/" + Mid(start, 1, 2)).ToString + " " + (Mid(finish, 7, 4) + "/" + Mid(finish, 4, 2) + "/" + Mid(finish, 1, 2)).ToString + " projectid:" + DropDownListPrj.SelectedValue

    End Sub

    Protected Sub PrepareStartFinishParameters()

        Dim start As String = Mid(TextBoxDateRange.Text.Trim, 1, 10)
        Dim finish As String = Mid(TextBoxDateRange.Text, 14, 10)

        lblStart.Text = (Mid(start, 7, 4) + "/" + Mid(start, 4, 2) + "/" + Mid(start, 1, 2)).ToString
        lblFinish.Text = (Mid(finish, 7, 4) + "/" + Mid(finish, 4, 2) + "/" + Mid(finish, 1, 2)).ToString

    End Sub

    Protected Sub ButtonExcel_Click(sender As Object, e As EventArgs)

        GridViewDSP3HIDDEN.DataBind()
        ExportToExcel.Export(GridViewDSP3HIDDEN, SqlDataSourceDSP3, "Payments " + TextBoxDateRange.Text.Trim + " " + DropDownListPrj.SelectedItem.ToString.Trim)

    End Sub

End Class

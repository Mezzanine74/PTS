Partial Class PaymentsCheck
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Or Not IsPostBack Then
            ' it provides query parameter for DropDownListProject
            If Page.User.Identity.Name.ToLower = "loseva" OrElse _
              Page.User.Identity.Name.ToLower = "mikhaleva" Then
                TextBoxUserName.Text = "savas"
            Else
                TextBoxUserName.Text = Page.User.Identity.Name
            End If
        End If

    End Sub

    Private Sub ExportToExcel(ByVal strFileName As String, ByVal dg As GridView)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.ContentEncoding = System.Text.Encoding.Unicode
        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble())
        Response.Charset = ""
        Me.EnableViewState = False
        Dim oStringWriter As New System.IO.StringWriter
        Dim oHtmlTextWriter As New System.Web.UI.HtmlTextWriter(oStringWriter)

        GridViewMonitor_PreRender(Nothing, Nothing)

        dg.DataSource = SqlDataSourceMonitor
        dg.DataBind()
        dg.AllowPaging = False
        dg.RenderControl(oHtmlTextWriter)

        Response.Write(oStringWriter.ToString())
        Response.[End]()

    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Exit Sub
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


    End Sub

    Protected Sub ListBoxPrj_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBoxPrj.DataBound
        If Not IsPostBack Then
            ListBoxPrj.SelectedIndex = 0
        End If
    End Sub

    Protected Sub ListBoxPrj_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBoxPrj.SelectedIndexChanged

        labelSupplierIDTransfer.Text = ""
        labelAllSuppliersMode.Text = "False"

        'SqlDataSourceMonitor.DataBind()
        'SqlDataSourceMonitoringSupplier.DataBind()
        'GridViewMonitor.DataBind()
        'GridViewSupplier.DataBind()

    End Sub

    Protected Sub LinkButtonAllProjects_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        labelAllSuppliersMode.Text = "True"

        'SqlDataSourceMonitor.DataBind()
        'GridViewMonitor.DataBind()
    End Sub

    Protected Sub SqlDataSourceMonitor_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourceMonitor.Selected
    End Sub

    Protected Sub SqlDataSourceMonitor_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles SqlDataSourceMonitor.Selecting

    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click

    End Sub

End Class

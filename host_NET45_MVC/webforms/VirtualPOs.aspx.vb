
Partial Class VirtualPOs
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If IsPostBack Or Not IsPostBack Then
            If (Not Page.User.IsInRole("Finance")) Then
                Response.Redirect("~/webforms/AccessDenied.aspx")
            End If

        End If

    End Sub

    Protected Sub ImageButtonExcel_Click(sender As Object, e As ImageClickEventArgs)

        ExportToExcel("VirtualPOs.xls", GridViewVirtualPO)

    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Exit Sub
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

        dg.DataBind()
        dg.AllowPaging = False
        dg.RenderControl(oHtmlTextWriter)

        Response.Write(oStringWriter.ToString())
        Response.[End]()

    End Sub

End Class

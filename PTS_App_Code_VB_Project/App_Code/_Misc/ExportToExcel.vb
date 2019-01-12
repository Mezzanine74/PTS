Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Web.UI.WebControls

Public Class ExportToExcel

    Shared Sub Export(ByVal _Grid As GridView, ByVal _SqlDatasource As SqlDataSource, ByVal _Header As String)

        _Grid.DataSource = _SqlDatasource
        _Grid.DataBind()
        _Grid.AllowPaging = False

        HttpContext.Current.Response.Clear()

        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + _
                                               _Header + ".xls")

        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.Buffer = True
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Unicode
        HttpContext.Current.Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble())
        HttpContext.Current.Response.Charset = ""
        Dim oStringWriter As New System.IO.StringWriter
        Dim oHtmlTextWriter As New System.Web.UI.HtmlTextWriter(oStringWriter)
        _Grid.RenderControl(oHtmlTextWriter)
        HttpContext.Current.Response.Write(oStringWriter.ToString())
        HttpContext.Current.Response.[End]()


    End Sub



End Class

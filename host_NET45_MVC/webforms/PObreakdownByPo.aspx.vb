Imports System.Data.SqlClient

Partial Class PObreakdownByPo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ' FEED NumberOfConnection Table in Database
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "DB_NumberOfConnection"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim dr As SqlDataReader = cmd.ExecuteReader
            con.Close()
            dr.Close()
        End Using
        ' ________________________________ End Of FEED


        If Request.QueryString("PO_No") IsNot Nothing Then
            RenderReport.Render("html", ReportViewerFollowUpReportBySupplierExcVAT, "PObreakdownByPo", _
                                "PO_No", Request.QueryString("PO_No").ToString)
        Else
            RenderReport.Render("html", ReportViewerFollowUpReportBySupplierExcVAT, "PObreakdownByPo", "PO_No", "PO-108-0001")

        End If
    End Sub
End Class

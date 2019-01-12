Imports System.Data.SqlClient
Partial Class FollowUpReportSubSupplierByExcVAT
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Try
                RenderReport.Render("HTML", ReportViewer_, "_Nakl_FollowUpReportSubSupplierByExcVAT", _
                                    "Currency", _
                                    Request.QueryString("Currency").ToString, _
                                    "ProjectID", _
                                    Request.QueryString("ProjectID").ToString, _
                                    "SupplierID", _
                                    Request.QueryString("SupplierID").ToString)
            Catch ex As Exception
                ' do nothing
            End Try
        End If

    End Sub

End Class
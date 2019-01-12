Imports System.Data.SqlClient
Imports System.Data

Partial Class PTS_1S_DeliveryDocComparison
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        RenderReport.Render("excel", ReportViewerPO, "PTS_1S_DeliveryDocComparisonFinalAlfaProjects", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

    End Sub


End Class

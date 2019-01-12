Imports System.Data.SqlClient
Partial Class DeliveryDocBrkdwnByProjectCostCode
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        RenderReport.Render("HTML", ReportViewer_, "DeliveryPackingListRev1ByProjectCostCode", _
                    "ProjectID", _
                    Request.QueryString("ProjectID").ToString, _
                    "CostCode", _
                    Request.QueryString("CostCode").ToString)

    End Sub
End Class
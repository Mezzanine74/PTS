Imports System.Data.SqlClient
Imports System.Data

Partial Class PO_Versus_DeliveryDocs
    Inherits System.Web.UI.Page

    Protected Sub ddlProject_SelectedIndexChanged(sender As Object, e As EventArgs)

        If ddlProject.SelectedValue <> 0 Then
            RenderReport.Render("excel", ReportViewerPO, "PO_versus_DeliveryDocs", "ProjectID", ddlProject.SelectedValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        Else
            _GiveNotification.Gritter_Error(Page, "Error", "Select Project", "center")
        End If

    End Sub
End Class

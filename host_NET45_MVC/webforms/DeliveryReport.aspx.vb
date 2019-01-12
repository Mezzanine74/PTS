
Partial Class DeliveryReport
    Inherits System.Web.UI.Page

    Protected Sub ButtonRun_Click(sender As Object, e As EventArgs) Handles ButtonRun.Click

        Dim _Date1 As String = Mid(TxtDate1.Text, 7, 4) + "-" + Mid(TxtDate1.Text, 4, 2) + "-" + Mid(TxtDate1.Text, 1, 2)
        Dim _Date2 As String = Mid(TxtDate2.Text, 7, 4) + "-" + Mid(TxtDate2.Text, 4, 2) + "-" + Mid(TxtDate2.Text, 1, 2)

        RenderReport.Render("HTML", ReportViewerDelivery, "Delivery_MainReport", "SupplierID", Mid(TextBoxSupplier.Text, 1, 12), "Date1", _Date1, "Date2", _Date2)

    End Sub

    Protected Sub ButtonExportExcel_Click(sender As Object, e As EventArgs) Handles ButtonExportExcel.Click

        Dim _Date1 As String = Mid(TxtDate1.Text, 7, 4) + "-" + Mid(TxtDate1.Text, 4, 2) + "-" + Mid(TxtDate1.Text, 1, 2)
        Dim _Date2 As String = Mid(TxtDate2.Text, 7, 4) + "-" + Mid(TxtDate2.Text, 4, 2) + "-" + Mid(TxtDate2.Text, 1, 2)

        RenderReport.Render("excel", ReportViewerDelivery, "Delivery_MainReport", "SupplierID", Mid(TextBoxSupplier.Text, 1, 12), "Date1", _Date1, "Date2", _Date2)

    End Sub
End Class

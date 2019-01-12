
Partial Class JTI
    Inherits System.Web.UI.Page

    Protected Sub ButtonExcel_Click(sender As Object, e As EventArgs)

        PTSMainClass.ExportGridExcel(GridViewJTI, "JTI PO Control " + DateTime.Now.ToString)

    End Sub

    Protected Sub GridViewJTI_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewJTI.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            If e.Row.Cells(0).Text.Trim.ToLower = "fixed" Then
                e.Row.Cells(0).BackColor = System.Drawing.Color.LightGray
            End If

            If e.Row.Cells(0).Text.Trim.ToLower = "new pos" Then
                e.Row.Cells(0).BackColor = System.Drawing.Color.LightGreen
            End If

            If IsNumeric(e.Row.Cells(14).Text) Then
                If Convert.ToDecimal(e.Row.Cells(14).Text) <> 0.0 Then
                    e.Row.Cells(14).BackColor = System.Drawing.Color.OrangeRed
                End If
            End If

        End If


    End Sub

    Protected Sub ButtonExcelWeeklyPayment_Click(sender As Object, e As EventArgs)

        RenderReport.Render("excel", ReportViewer_, "PaymentWeekly", _
                            Nothing, _
                            Nothing, _
                            Nothing, _
                            Nothing, _
                            Nothing, _
                            Nothing)

    End Sub
End Class

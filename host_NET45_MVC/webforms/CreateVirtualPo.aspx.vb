
Partial Class CreateVirtualPo
    Inherits System.Web.UI.Page

    Protected Sub SupplierIDTextBox_TextChanged(sender As Object, e As EventArgs)

        ' Check if supplier Exist

        Using Adapter As New MercuryTableAdapters.Table6_SupplierTableAdapter
            If Adapter.GetCountBySupplierID(sender.Text) = 0 Then
                ' give message that it is dublicating
                LabelSplrError.Text = "Supplier doesnt exist"
            Else
                LabelSplrError.Text = String.Empty
            End If

            Adapter.Dispose()

        End Using

    End Sub


    Protected Sub ButtonEnter_Click(sender As Object, e As EventArgs)

        Using adapter As New MercuryTableAdapters.QueriesTableAdapterCommon

            Try
                adapter.VirtualInvoiceProducer(DropDownListProject.SelectedValue, _
                                                SupplierIDTextBox.Text, _
                                                 TotalPriceTextBox.Text, _
                                                  DropDownListCurrency.SelectedValue, _
                                                   TextBoxVATpercent.Text)

                adapter.Dispose()
                Response.Redirect("~/webforms/default.aspx")
            Catch ex As Exception
                Response.Write("<h1>Transaction rolledback. Apply to admin</h1>")
            End Try

        End Using

    End Sub
End Class

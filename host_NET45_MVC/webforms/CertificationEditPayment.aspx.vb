
Partial Class CertificationEditPayment
    Inherits System.Web.UI.Page

    Protected Sub AddSelectToDDL(ByVal _ddl As DropDownList)

        Dim lst As New ListItem("_Select", "0")
        _ddl.Items.Insert(0, lst)

    End Sub

    Protected Sub DropDownListDocument_DataBound(sender As Object, e As EventArgs) Handles DropDownListDocument.DataBound

        AddSelectToDDL(sender)

    End Sub

    Protected Sub DropDownListInvoice_DataBound(sender As Object, e As EventArgs) Handles DropDownListInvoice.DataBound

        AddSelectToDDL(sender)

    End Sub

    Protected Sub DropDownListPrj_DataBound(sender As Object, e As EventArgs) Handles DropDownListPrj.DataBound

        AddSelectToDDL(sender)

    End Sub

    Protected Sub DropDownListClient_DataBound(sender As Object, e As EventArgs) Handles DropDownListClient.DataBound

        AddSelectToDDL(sender)

    End Sub

    Protected Sub GridViewPayment_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewPayment.RowDataBound

        ' trim Date textbox, formatting doesnt work on ASPX page
        Dim PaymentDateTextBox As TextBox = DirectCast(e.Row.FindControl("PaymentDateTextBox"), TextBox)
        If PaymentDateTextBox IsNot Nothing Then
            PaymentDateTextBox.Text = Mid(PaymentDateTextBox.Text, 1, 10)
        End If

        ' provide validation
        Dim TextBoxInvoiceID As TextBox = DirectCast(e.Row.FindControl("TextBoxInvoiceID"), TextBox)
        Dim TextBoxPaymentValue As TextBox = DirectCast(e.Row.FindControl("TextBoxPaymentValue"), TextBox)
        Dim CV As CompareValidator = DirectCast(e.Row.FindControl("CompareValidatorInvTotal"), CompareValidator)

        If CV IsNot Nothing Then
            Using table As New CertificationTableAdapters.QueriesTableAdapterTotalSums
                CV.ValueToCompare = table.GetTotalInvoiceValueByInvoiceID(TextBoxInvoiceID.Text) - table.GetTotalPaymentValueByInvoice(TextBoxInvoiceID.Text) + _
                    +Convert.ToDecimal(TextBoxPaymentValue.Text)
                CV.ErrorMessage = "Value cannot be greater than " + CV.ValueToCompare.ToString
            End Using

        End If

    End Sub

End Class

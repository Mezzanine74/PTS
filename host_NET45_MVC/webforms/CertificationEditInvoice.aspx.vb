
Partial Class CertificationEditInvoice
    Inherits System.Web.UI.Page

    Protected Sub AddSelectToDDL(ByVal _ddl As DropDownList)

        Dim lst As New ListItem("_Select", "0")
        _ddl.Items.Insert(0, lst)

    End Sub

    Protected Sub DropDownListPrj_DataBound(sender As Object, e As EventArgs)

        AddSelectToDDL(sender)

    End Sub

    Protected Sub DropDownListClient_DataBound(sender As Object, e As EventArgs) Handles DropDownListClient.DataBound

        AddSelectToDDL(sender)

    End Sub

    Protected Sub DropDownListDocument_DataBound(sender As Object, e As EventArgs) Handles DropDownListDocument.DataBound

        AddSelectToDDL(sender)

    End Sub

    Protected Sub GridViewInvoice_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewInvoice.RowDataBound

        ' trim Date textbox, formatting doesnt work on ASPX page
        Dim TextBoxInvoiceDate As TextBox = DirectCast(e.Row.FindControl("TextBoxInvoiceDate"), TextBox)
        If TextBoxInvoiceDate IsNot Nothing Then
            TextBoxInvoiceDate.Text = Mid(TextBoxInvoiceDate.Text, 1, 10)
        End If

        ' provide YES or NO to DDL Actual for ItemTemplate
        If e.Row.RowState = DataControlRowState.Normal Or e.Row.RowState = DataControlRowState.Alternate Then
            Dim DropDownListActual As DropDownList = DirectCast(e.Row.FindControl("DropDownListActual"), DropDownList)
            If DropDownListActual IsNot Nothing Then
                DropDownListActual.ForeColor = System.Drawing.Color.Red

                Dim liYES As New ListItem("YES", "YES", True)
                Dim liNO As New ListItem("NO", "NO", True)

                DropDownListActual.Items.Clear()

                If DataBinder.Eval(e.Row.DataItem, "Actual") = True Then
                    DropDownListActual.Items.Add(liYES)
                ElseIf DataBinder.Eval(e.Row.DataItem, "Actual") = False Then
                    DropDownListActual.Items.Add(liNO)
                End If

                liYES = Nothing
                liNO = Nothing

            End If
        End If

        ' provide validation
        Dim TextBoxDocumentID As TextBox = DirectCast(e.Row.FindControl("TextBoxDocumentID"), TextBox)
        Dim TextBoxInvoiceValue As TextBox = DirectCast(e.Row.FindControl("TextBoxInvoiceValue"), TextBox)
        Dim CV As CompareValidator = DirectCast(e.Row.FindControl("CompareValidatorInvTotal"), CompareValidator)

        If CV IsNot Nothing Then
            Using table As New CertificationTableAdapters.QueriesTableAdapterTotalSums
                CV.ValueToCompare = table.GetTotalDocumentValue(TextBoxDocumentID.Text) - table.GetTotalInvoiceValueByDocument(TextBoxDocumentID.Text) + _
                    +Convert.ToDecimal(TextBoxInvoiceValue.Text)
                CV.ErrorMessage = "Value cannot be greater than " + CV.ValueToCompare.ToString
            End Using

        End If

    End Sub

End Class

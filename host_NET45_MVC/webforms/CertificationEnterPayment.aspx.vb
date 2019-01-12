
Partial Class CertificationEnterPayment
    Inherits System.Web.UI.Page

    Protected Sub AddSelectToDDL(ByVal _ddl As DropDownList)

        Dim lst As New ListItem("_Select", "0")
        _ddl.Items.Insert(0, lst)

    End Sub

    Protected Sub DropDownListDocument_DataBound(sender As Object, e As EventArgs) Handles DropDownListDocument.DataBound

        AddSelectToDDL(sender)

        If Not Page.IsPostBack AndAlso Request.QueryString("InvoiceID") IsNot Nothing Then
            Dim InvoiceID As Integer = Request.QueryString("InvoiceID")
            Using adapter As New CertificationTableAdapters.QueriesTableAdapterTotalSums
                DropDownListDocument.SelectedValue = adapter.GetDocumentIDByInvoiceID(InvoiceID)
            End Using
        End If

    End Sub

    Protected Sub DropDownListInvoice_DataBound(sender As Object, e As EventArgs) Handles DropDownListInvoice.DataBound

        AddSelectToDDL(sender)

        If Not Page.IsPostBack AndAlso Request.QueryString("InvoiceID") IsNot Nothing Then
            Dim InvoiceID As Integer = Request.QueryString("InvoiceID")
            DropDownListInvoice.SelectedValue = InvoiceID

            Using adapter As New CertificationTableAdapters.QueriesTableAdapterTotalSums
                ' provide validator parameters
                Dim CV As CompareValidator = FormViewPayment.FindControl("CompareValidatorInvTotal")

                CV.ValueToCompare = (adapter.GetTotalInvoiceValueByInvoiceID(DropDownListInvoice.SelectedValue) - adapter.GetTotalPaymentValueByInvoice(DropDownListInvoice.SelectedValue)) * 1.18
                CV.ErrorMessage = "Value cannot be greater than " + CV.ValueToCompare.ToString

                Dim TextBoxPaymentValue As TextBox = FormViewPayment.FindControl("TextBoxPaymentValue")
                TextBoxPaymentValue.Text = Convert.ToString((adapter.GetTotalInvoiceValueByInvoiceID(DropDownListInvoice.SelectedValue) - adapter.GetTotalPaymentValueByInvoice(DropDownListInvoice.SelectedValue)) * 1.18)

                If (adapter.GetTotalInvoiceValueByInvoiceID(DropDownListInvoice.SelectedValue) - adapter.GetTotalPaymentValueByInvoice(DropDownListInvoice.SelectedValue)) = 0 Then
                    FormViewPayment.Enabled = False
                    LabelError.Visible = True
                Else
                    FormViewPayment.Enabled = True
                    LabelError.Visible = False
                End If

            End Using

        End If

    End Sub

    Protected Sub DropDownListPrj_DataBound(sender As Object, e As EventArgs) Handles DropDownListPrj.DataBound

        AddSelectToDDL(sender)

        If Not Page.IsPostBack AndAlso Request.QueryString("InvoiceID") IsNot Nothing Then
            Dim InvoiceID As Integer = Request.QueryString("InvoiceID")
            Using adapter As New CertificationTableAdapters.QueriesTableAdapterTotalSums
                DropDownListPrj.SelectedValue = adapter.GetProjectIDByInvoiceID(InvoiceID)
            End Using

        End If

    End Sub

    Protected Sub DropDownListClient_DataBound(sender As Object, e As EventArgs) Handles DropDownListClient.DataBound

        AddSelectToDDL(sender)

        If Not Page.IsPostBack AndAlso Request.QueryString("InvoiceID") IsNot Nothing Then
            Dim InvoiceID As Integer = Request.QueryString("InvoiceID")
            Using adapter As New CertificationTableAdapters.QueriesTableAdapterTotalSums
                DropDownListClient.SelectedValue = adapter.GetClientIDByInvoiceID(InvoiceID)
            End Using

        End If

    End Sub

    Protected Sub FormViewPayment_ItemInserting(sender As Object, e As FormViewInsertEventArgs) Handles FormViewPayment.ItemInserting

        Using _adapter As New CertificationTableAdapters.Table_PaymentsTableAdapter
            e.Values("PaymentID") = _adapter.GetNextPaymentID
        End Using

        e.Values("InvoiceID") = DropDownListInvoice.SelectedValue

        Dim TextBoxPaymentValue As TextBox = FormViewPayment.FindControl("TextBoxPaymentValue")
        e.Values("PaymentAmount") = Math.Round(Convert.ToDecimal(TextBoxPaymentValue.Text) / 1.18, 2)

    End Sub

    Protected Sub DropDownListInvoice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownListInvoice.SelectedIndexChanged

        Dim ddl As DropDownList = sender
        Dim CV As CompareValidator = FormViewPayment.FindControl("CompareValidatorInvTotal")
        If ddl.SelectedValue <> 0 Then

            Using table As New CertificationTableAdapters.QueriesTableAdapterTotalSums
                CV.ValueToCompare = (table.GetTotalInvoiceValueByInvoiceID(ddl.SelectedValue) - table.GetTotalPaymentValueByInvoice(ddl.SelectedValue)) * 1.18
                CV.ErrorMessage = "Value cannot be greater than " + CV.ValueToCompare.ToString

                Dim TextBoxPaymentValue As TextBox = FormViewPayment.FindControl("TextBoxPaymentValue")
                TextBoxPaymentValue.Text = Convert.ToString((table.GetTotalInvoiceValueByInvoiceID(DropDownListInvoice.SelectedValue) - table.GetTotalPaymentValueByInvoice(DropDownListInvoice.SelectedValue)) * 1.18)

                If (table.GetTotalInvoiceValueByInvoiceID(DropDownListInvoice.SelectedValue) - table.GetTotalPaymentValueByInvoice(DropDownListInvoice.SelectedValue)) = 0 Then
                    FormViewPayment.Enabled = False
                    LabelError.Visible = True
                Else
                    FormViewPayment.Enabled = True
                    LabelError.Visible = False
                End If

            End Using

        Else

            CV.ValueToCompare = 0
            CV.ErrorMessage = "Select Invoice"

        End If

        ' provide parameter to gridviewHostiry
        ProvideParametersForGrid()

    End Sub

    Protected Sub GridviewDocumentsHistory_DataBound(sender As Object, e As EventArgs) Handles GridviewDocumentsHistory.DataBound

        Dim grd As GridView = sender
        If grd.Rows.Count > 0 Then
            ' visible
            LabelDocHistory.Visible = True
        Else
            ' invisible
            LabelDocHistory.Visible = False
        End If

    End Sub

    Protected Sub GridviewDocumentsHistory_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridviewDocumentsHistory.RowCommand

        If (e.CommandName = "OpenDocument") Then
            Dim LinkToFile As String = e.CommandArgument.ToString
            Dim openpdf As New MyCommonTasks
            openpdf.OpenPDF(LinkToFile)
        End If

    End Sub

    Protected Sub GridviewDocumentsHistory_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridviewDocumentsHistory.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            PTSMainClass.ProvideImageFromFile(DirectCast(e.Row.FindControl("ImageButtonItem"), ImageButton), DataBinder.Eval(e.Row.DataItem, "ScanLink"))

            ' provide hyperlink parameters
            Dim HypeLinkContract As HyperLink = DirectCast(e.Row.FindControl("HyperlinkFileContract"), HyperLink)
            If DataBinder.Eval(e.Row.DataItem, "ContractID") = 0 Then
                ' invisible
                HypeLinkContract.Visible = False
            Else
                ' visible
                HypeLinkContract.NavigateUrl = "~/webforms/contractDetails.aspx?ContractId=" + DataBinder.Eval(e.Row.DataItem, "ContractID").ToString
                HypeLinkContract.Visible = True
            End If

            Dim HypeLinkAddendum As HyperLink = DirectCast(e.Row.FindControl("HyperlinkFileAddendum"), HyperLink)
            If DataBinder.Eval(e.Row.DataItem, "AddendumID") = 0 Then
                ' invisible
                HypeLinkAddendum.Visible = False
            Else
                'visible
                HypeLinkAddendum.NavigateUrl = "~/webforms/AddendumDetails.aspx?ContractId=" + DataBinder.Eval(e.Row.DataItem, "AddendumID").ToString
                HypeLinkAddendum.Visible = True
            End If

            ' provide parameter to InvoiceHistory
            Dim SqlDataSourceInvoiceHistory As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourceInvoiceHistory"), SqlDataSource)
            SqlDataSourceInvoiceHistory.SelectParameters("DocumentID").DefaultValue = DataBinder.Eval(e.Row.DataItem, "DocumentID")

        End If

    End Sub

    Protected Sub GridviewInvoiceHistory_DataBound(sender As Object, e As EventArgs)

        Dim grd As GridView = sender
        Dim Row As GridViewRow = CType(grd.NamingContainer, GridViewRow)
        Dim LabelInvoiceHistory As Label = DirectCast(Row.FindControl("LabelInvoiceHistory"), Label)

        If grd.Rows.Count > 0 Then
            ' visible
            LabelInvoiceHistory.Visible = True
        Else
            ' invisible
            LabelInvoiceHistory.Visible = False
        End If

    End Sub

    Protected Sub ProvideParametersForGrid()

        SqlDataSourceDocumentsHistory.SelectParameters("ProjectID").DefaultValue = DropDownListPrj.SelectedValue
        SqlDataSourceDocumentsHistory.SelectParameters("ClientID").DefaultValue = DropDownListClient.SelectedValue
        SqlDataSourceDocumentsHistory.SelectParameters("DocumentID").DefaultValue = DropDownListDocument.SelectedValue

    End Sub

    Protected Sub DropDownListPrj_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownListPrj.SelectedIndexChanged

        ' provide parameter to gridviewHostiry
        ProvideParametersForGrid()

    End Sub

    Protected Sub DropDownListClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownListClient.SelectedIndexChanged

        ' provide parameter to gridviewHostiry
        ProvideParametersForGrid()

    End Sub

    Protected Sub DropDownListDocument_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownListDocument.SelectedIndexChanged

        ' provide parameter to gridviewHostiry
        ProvideParametersForGrid()

    End Sub

    Protected Sub GridviewInvoiceHistory_RowDataBound(sender As Object, e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then
            ' provide parameter to InvoiceHistory
            Dim SqlDataSourcePaymentHistory As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourcePaymentHistory"), SqlDataSource)
            SqlDataSourcePaymentHistory.SelectParameters("InvoiceID").DefaultValue = DataBinder.Eval(e.Row.DataItem, "InvoiceID")

        End If

    End Sub

    Protected Sub GridviewPaymentHistory_DataBound(sender As Object, e As EventArgs)

        Dim grd As GridView = sender
        Dim Row As GridViewRow = CType(grd.NamingContainer, GridViewRow)
        Dim LabelPaymentHistory As Label = DirectCast(Row.FindControl("LabelPaymentHistory"), Label)

        If grd.Rows.Count > 0 Then
            ' visible
            LabelPaymentHistory.Visible = True
        Else
            ' invisible
            LabelPaymentHistory.Visible = False
        End If

    End Sub
End Class

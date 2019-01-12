
Partial Class CertificationEnterInvoice
    Inherits System.Web.UI.Page

    Dim _invoiceID As Integer = 0

    Protected Sub AddSelectToDDL(ByVal _ddl As DropDownList)

        Dim lst As New ListItem("_Select", "0")
        _ddl.Items.Insert(0, lst)

    End Sub

    Protected Sub DropDownListPrj_DataBound(sender As Object, e As EventArgs)

        AddSelectToDDL(sender)

        If Not Page.IsPostBack AndAlso Request.QueryString("DocumentID") IsNot Nothing Then
            Dim documentId As Integer = Request.QueryString("DocumentID")
            Using adapter As New CertificationTableAdapters.QueriesTableAdapterTotalSums
                DropDownListPrj.SelectedValue = adapter.GetProjectIDByDocumentID(documentId)
            End Using

        End If

    End Sub

    Protected Sub DropDownListClient_DataBound(sender As Object, e As EventArgs) Handles DropDownListClient.DataBound

        AddSelectToDDL(sender)

        If Not Page.IsPostBack AndAlso Request.QueryString("DocumentID") IsNot Nothing Then
            Dim documentId As Integer = Request.QueryString("DocumentID")
            Using adapter As New CertificationTableAdapters.QueriesTableAdapterTotalSums
                DropDownListClient.SelectedValue = adapter.GetClientIDByDocumentID(documentId).ToString
            End Using
        End If

    End Sub

    Protected Sub DropDownListDocument_DataBound(sender As Object, e As EventArgs) Handles DropDownListDocument.DataBound

        AddSelectToDDL(sender)

        If Not Page.IsPostBack AndAlso Request.QueryString("DocumentID") IsNot Nothing Then
            Dim documentId As Integer = Request.QueryString("DocumentID")
            Using adapter As New CertificationTableAdapters.QueriesTableAdapterTotalSums
                DropDownListDocument.SelectedValue = documentId

                ' provide validator parameters
                Dim CV As CompareValidator = FormViewEnterInvoice.FindControl("CompareValidatorDocTotal")

                CV.ValueToCompare = adapter.GetTotalDocumentValue(DropDownListDocument.SelectedValue) - adapter.GetTotalInvoiceValueByDocument(DropDownListDocument.SelectedValue)
                CV.ErrorMessage = "Value cannot be greater than " + CV.ValueToCompare.ToString

                Dim TextBoxInvoiceValue As TextBox = FormViewEnterInvoice.FindControl("TextBoxInvoiceValue")
                TextBoxInvoiceValue.Text = Convert.ToString(adapter.GetTotalDocumentValue(DropDownListDocument.SelectedValue) - adapter.GetTotalInvoiceValueByDocument(DropDownListDocument.SelectedValue))

                If (adapter.GetTotalDocumentValue(DropDownListDocument.SelectedValue) - adapter.GetTotalInvoiceValueByDocument(DropDownListDocument.SelectedValue)) = 0 Then
                    FormViewEnterInvoice.Enabled = False
                    LabelError.Visible = True
                Else
                    FormViewEnterInvoice.Enabled = True
                    LabelError.Visible = False
                End If

            End Using

        End If

    End Sub

    Protected Sub FormViewEnterInvoice_ItemInserted(sender As Object, e As FormViewInsertedEventArgs) Handles FormViewEnterInvoice.ItemInserted

        Response.Redirect("~/webforms/CertificationEnterPayment.aspx?InvoiceID=" + _invoiceID.ToString)

    End Sub

    Protected Sub FormViewEnterInvoice_ItemInserting(sender As Object, e As FormViewInsertEventArgs) Handles FormViewEnterInvoice.ItemInserting

        Using adapter As New CertificationTableAdapters.QueriesTableAdapterTotalSums
            Dim TxtBoxInvoiceValueBeingEntered As TextBox = FormViewEnterInvoice.FindControl("TextBoxInvoiceValue")
            If Math.Round(Convert.ToDecimal(TxtBoxInvoiceValueBeingEntered.Text), 2) > (adapter.GetTotalDocumentValue(DropDownListDocument.SelectedValue) - adapter.GetTotalInvoiceValueByDocument(DropDownListDocument.SelectedValue)) Then
                e.Cancel = True
                Response.Write("<h1 style=" + """" + "color:Red;" + """" + ">Total Sum Violation. Transaction cancelled!</h1>")
            End If
        End Using

        Using _adapter As New CertificationTableAdapters.Table_InvoiceTableAdapter
            _invoiceID = _adapter.GetNextInvoiceID
            e.Values("InvoiceID") = _invoiceID
            _adapter.Dispose()
        End Using

        ' get Actual value from Dropdownlist
        Dim DropDownListActual As DropDownList = FormViewEnterInvoice.FindControl("DropDownListActual")
        If DropDownListActual.SelectedValue.ToString.ToLower = "yes" Then
            e.Values("Actual") = 1
        ElseIf DropDownListActual.SelectedValue.ToString.ToLower = "no" Then
            e.Values("Actual") = 0
        End If

        e.Values("DocumentID") = DropDownListDocument.SelectedValue

    End Sub

    Protected Sub DropDownListDocument_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownListDocument.SelectedIndexChanged

        Dim ddl As DropDownList = sender
        Dim CV As CompareValidator = FormViewEnterInvoice.FindControl("CompareValidatorDocTotal")
        If ddl.SelectedValue <> 0 Then

            Using table As New CertificationTableAdapters.QueriesTableAdapterTotalSums
                CV.ValueToCompare = table.GetTotalDocumentValue(ddl.SelectedValue) - table.GetTotalInvoiceValueByDocument(ddl.SelectedValue)
                CV.ErrorMessage = "Value cannot be greater than " + CV.ValueToCompare.ToString

                Dim TextBoxInvoiceValue As TextBox = FormViewEnterInvoice.FindControl("TextBoxInvoiceValue")
                TextBoxInvoiceValue.Text = Convert.ToString(table.GetTotalDocumentValue(DropDownListDocument.SelectedValue) - table.GetTotalInvoiceValueByDocument(DropDownListDocument.SelectedValue))

                If (table.GetTotalDocumentValue(ddl.SelectedValue) - table.GetTotalInvoiceValueByDocument(ddl.SelectedValue)) = 0 Then
                    FormViewEnterInvoice.Enabled = False
                    LabelError.Visible = True
                Else
                    FormViewEnterInvoice.Enabled = True
                    LabelError.Visible = False
                End If

            End Using

        Else

            CV.ValueToCompare = 0
            CV.ErrorMessage = "Select Document"

        End If

        ' provide parameter to gridviewHostiry
        ProvideParametersForGrid()

    End Sub

    Protected Sub DropDownListPrj_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownListPrj.SelectedIndexChanged

        ' provide parameter to gridviewHostiry
        ProvideParametersForGrid()

    End Sub

    Protected Sub DropDownListClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownListClient.SelectedIndexChanged

        ' provide parameter to gridviewHostiry
        ProvideParametersForGrid()

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

End Class

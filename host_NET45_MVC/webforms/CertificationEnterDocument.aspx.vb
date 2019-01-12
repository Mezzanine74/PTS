Imports AjaxControlToolkit

Partial Class CertificationEnterDocument
    Inherits System.Web.UI.Page

    Dim _DocumentID As Integer = 0

    Protected Sub AddSelectToDDL(ByVal _ddl As DropDownList)

        Dim lst As New ListItem("_Select", "0")
        _ddl.Items.Insert(0, lst)

    End Sub

    Protected Sub DropDownListDocType_DataBound(sender As Object, e As EventArgs)

        AddSelectToDDL(sender)

    End Sub

    Protected Sub DropDownListPrj_DataBound(sender As Object, e As EventArgs)

        AddSelectToDDL(sender)

    End Sub

    Protected Sub DropDownListClient_DataBound(sender As Object, e As EventArgs)

        AddSelectToDDL(sender)

    End Sub

    Protected Sub DropDownListFxRateType_DataBound(sender As Object, e As EventArgs)

        AddSelectToDDL(sender)

    End Sub

    Protected Sub DropDownListAccount_DataBound(sender As Object, e As EventArgs)

        AddSelectToDDL(sender)

    End Sub

    Protected Sub FormViewDocumentEnter_ItemInserted(sender As Object, e As FormViewInsertedEventArgs) Handles FormViewDocumentEnter.ItemInserted

        Response.Redirect("~/webforms/CertificationEnterInvoice.aspx?DocumentID=" + _DocumentID.ToString)

    End Sub

    Protected Sub FormViewDocumentEnter_ItemInserting(sender As Object, e As FormViewInsertEventArgs) Handles FormViewDocumentEnter.ItemInserting

        Using _adapter As New CertificationTableAdapters.Table_DocumentsTableAdapter
            _DocumentID = _adapter.GetNextDocumentID
            e.Values("DocumentID") = _DocumentID
        End Using

        ' take ClientID on codebehind. Bind method on ASPX page cause error "Databinding methods such as Eval(), XPath(), and Bind() can only be used in the context of a databound control"
        Dim ddl As DropDownList = FormViewDocumentEnter.FindControl("DropDownListClient")
        e.Values("ClientID") = ddl.SelectedValue

    End Sub

    Protected Sub ButtonUpload_Click(sender As Object, e As EventArgs)

        Dim FileUploadScanFile As FileUpload = FormViewDocumentEnter.FindControl("FileUploadScanFile")
        Dim TextBoxStoreLink As TextBox = FormViewDocumentEnter.FindControl("TextBoxStoreLink")
        Dim LabelInfo As Label = FormViewDocumentEnter.FindControl("LabelInfo")

        If FileUploadScanFile.HasFile Then

            Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
            FileUploadScanFile.SaveAs(MapPath("~/Certification/" + UniqueString1 + _
                                              System.IO.Path.GetExtension(FileUploadScanFile.PostedFile.FileName)))
            TextBoxStoreLink.Text = "~/Certification/" + UniqueString1 + _
                                              System.IO.Path.GetExtension(FileUploadScanFile.PostedFile.FileName)
            LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
            LabelInfo.Text = FileUploadScanFile.PostedFile.FileName + " has been loaded successfully"

        End If


    End Sub

    Protected Sub DropDownListDocType_SelectedIndexChanged(sender As Object, e As EventArgs)

        ' all logic goes here
        Dim ddl As DropDownList = sender

        ProvideValidation(ddl, FormViewDocumentEnter)

    End Sub

    Protected Sub ProvideValidation(ByVal ddl As DropDownList, ByVal FrmView As FormView)

        Dim TextBoxExcValue As TextBox = FrmView.FindControl("TextBoxExcValue")
        Dim RequiredFieldValidatorExtValue As RequiredFieldValidator = FrmView.FindControl("RequiredFieldValidatorExtValue")
        Dim CompareValidatorTextBoxExcValue As CompareValidator = FrmView.FindControl("CompareValidatorTextBoxExcValue")

        Dim TextBoxAdvance As TextBox = FrmView.FindControl("TextBoxAdvance")
        Dim RequiredFieldValidatorAdvance As RequiredFieldValidator = FrmView.FindControl("RequiredFieldValidatorAdvance")
        Dim CompareValidatorTextBoxAdvance As CompareValidator = FrmView.FindControl("CompareValidatorTextBoxAdvance")

        Dim TextBoxRetention As TextBox = FrmView.FindControl("TextBoxRetention")
        Dim RequiredFieldValidatorRetention As RequiredFieldValidator = FrmView.FindControl("RequiredFieldValidatorRetention")
        Dim CompareValidatorTextBoxRetention As CompareValidator = FrmView.FindControl("CompareValidatorTextBoxRetention")

        If ddl.SelectedValue = 1 Then
            '--1	KS-2,3
            TextBoxExcValue.Enabled = True
            TextBoxAdvance.Enabled = True
            TextBoxRetention.Enabled = True
            RequiredFieldValidatorExtValue.Enabled = True
            CompareValidatorTextBoxExcValue.Enabled = True
            RequiredFieldValidatorAdvance.Enabled = False
            CompareValidatorTextBoxAdvance.Enabled = False
            RequiredFieldValidatorRetention.Enabled = False
            CompareValidatorTextBoxRetention.Enabled = False

        ElseIf ddl.SelectedValue = 2 Then
            '--2	Tavarnaya Nakladnaya
            TextBoxExcValue.Enabled = True
            TextBoxAdvance.Enabled = True
            TextBoxRetention.Enabled = True
            RequiredFieldValidatorExtValue.Enabled = True
            CompareValidatorTextBoxExcValue.Enabled = True
            RequiredFieldValidatorAdvance.Enabled = False
            CompareValidatorTextBoxAdvance.Enabled = False
            RequiredFieldValidatorRetention.Enabled = False
            CompareValidatorTextBoxRetention.Enabled = False

        ElseIf ddl.SelectedValue = 3 Then
            '--3	Act
            TextBoxExcValue.Enabled = True
            TextBoxAdvance.Enabled = True
            TextBoxRetention.Enabled = True
            RequiredFieldValidatorExtValue.Enabled = True
            CompareValidatorTextBoxExcValue.Enabled = True
            RequiredFieldValidatorAdvance.Enabled = False
            CompareValidatorTextBoxAdvance.Enabled = False
            RequiredFieldValidatorRetention.Enabled = False
            CompareValidatorTextBoxRetention.Enabled = False

        ElseIf ddl.SelectedValue = 4 Then
            '--4	Invoice For Advance
            TextBoxExcValue.Enabled = False
            TextBoxExcValue.Text = 0
            TextBoxAdvance.Enabled = True
            TextBoxRetention.Enabled = False
            TextBoxRetention.Text = 0
            RequiredFieldValidatorExtValue.Enabled = False
            CompareValidatorTextBoxExcValue.Enabled = False
            RequiredFieldValidatorAdvance.Enabled = True
            CompareValidatorTextBoxAdvance.Enabled = True
            RequiredFieldValidatorRetention.Enabled = False
            CompareValidatorTextBoxRetention.Enabled = False

        ElseIf ddl.SelectedValue = 5 Then
            '--5	Fines
            TextBoxExcValue.Enabled = True
            TextBoxAdvance.Enabled = False
            TextBoxAdvance.Text = 0
            TextBoxRetention.Enabled = False
            TextBoxRetention.Text = 0
            RequiredFieldValidatorExtValue.Enabled = True
            CompareValidatorTextBoxExcValue.Enabled = True
            RequiredFieldValidatorAdvance.Enabled = False
            CompareValidatorTextBoxAdvance.Enabled = False
            RequiredFieldValidatorRetention.Enabled = False
            CompareValidatorTextBoxRetention.Enabled = False

        ElseIf ddl.SelectedValue = 6 Then
            '--6	Invoice For Retention
            TextBoxExcValue.Enabled = False
            TextBoxExcValue.Text = 0
            TextBoxAdvance.Enabled = False
            TextBoxAdvance.Text = 0
            TextBoxRetention.Enabled = True
            RequiredFieldValidatorExtValue.Enabled = False
            CompareValidatorTextBoxExcValue.Enabled = False
            RequiredFieldValidatorAdvance.Enabled = False
            CompareValidatorTextBoxAdvance.Enabled = False
            RequiredFieldValidatorRetention.Enabled = True
            CompareValidatorTextBoxRetention.Enabled = True

        End If

    End Sub

    Protected Sub DropDownListFxRateType_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim ddl As DropDownList = sender
        Dim TextBoxFxRate As TextBox = FormViewDocumentEnter.FindControl("TextBoxFxRate")
        '1	N/A
        '2	Fixed In Contract
        '3	On Payment
        '4	Fixed By Document

        If ddl.SelectedValue = 1 Or ddl.SelectedValue = 3 Then
            TextBoxFxRate.Text = 0
            TextBoxFxRate.Enabled = False
        Else
            TextBoxFxRate.Text = String.Empty
            TextBoxFxRate.Enabled = True
        End If

    End Sub

    Protected Sub BtnPickContract_Click(sender As Object, e As EventArgs)

        Dim ModalPopupExtenderPickContract As ModalPopupExtender = FormViewDocumentEnter.FindControl("ModalPopupExtenderPickContract")
        Dim GridViewPickContract As GridView = FormViewDocumentEnter.FindControl("GridViewPickContract")

        ModalPopupExtenderPickContract.Show()
        GridViewPickContract.DataBind()

    End Sub


    Protected Sub BtnPickAddendum_Click(sender As Object, e As EventArgs)

        Dim ModalPopupExtenderPickAddendum As ModalPopupExtender = FormViewDocumentEnter.FindControl("ModalPopupExtenderPickAddendum")
        Dim GridViewPickAddendum As GridView = FormViewDocumentEnter.FindControl("GridViewPickAddendum")

        ModalPopupExtenderPickAddendum.Show()
        GridViewPickAddendum.DataBind()

    End Sub

    Protected Sub GridViewPickContract_RowCommand(sender As Object, e As GridViewCommandEventArgs)

        Dim gvr As GridView = sender

        If e.CommandName = "pick" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gvr.Rows(index)
            Dim TextBoxContractID As TextBox = FormViewDocumentEnter.FindControl("TextBoxContractID")
            TextBoxContractID.Text = row.Cells(1).Text
            Dim ModalPopupExtenderPickContract As ModalPopupExtender = FormViewDocumentEnter.FindControl("ModalPopupExtenderPickContract")
            ModalPopupExtenderPickContract.Hide()

            ' reset Addendum controls
            Dim TextBoxAddendumID As TextBox = FormViewDocumentEnter.FindControl("TextBoxAddendumID")
            TextBoxAddendumID.Text = 0
            Dim RequiredFieldValidatorTextBoxAddendumID As RequiredFieldValidator = FormViewDocumentEnter.FindControl("RequiredFieldValidatorTextBoxAddendumID")
            RequiredFieldValidatorTextBoxAddendumID.Enabled = False

            Dim DropDownListFxRateType As DropDownList = FormViewDocumentEnter.FindControl("DropDownListFxRateType")
            Dim TextBoxFxRate As TextBox = FormViewDocumentEnter.FindControl("TextBoxFxRate")
            ' if ruble contract, then set FxRateType as "N/A"
            If row.Cells(5).Text.ToLower = "rub" Then
                DropDownListFxRateType.SelectedIndex = 1
                TextBoxFxRate.Text = 0
                TextBoxFxRate.Enabled = False
            Else
                DropDownListFxRateType.SelectedIndex = 0
                TextBoxFxRate.Text = ""
                TextBoxFxRate.Enabled = True
            End If

        End If

    End Sub

    Protected Sub GridViewPickAddendum_RowDataBound(sender As Object, e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim BtnPickThis As Button = DirectCast(e.Row.FindControl("BtnPickThis"), Button)
            If BtnPickThis IsNot Nothing Then
                If DataBinder.Eval(e.Row.DataItem, "AddendumID") = 0 Then
                    BtnPickThis.Visible = False
                    e.Row.Cells(0).Text = "Contract"
                    e.Row.BackColor = System.Drawing.Color.LightBlue
                End If
            End If
        End If

    End Sub

    Protected Sub GridViewPickAddendum_RowCommand(sender As Object, e As GridViewCommandEventArgs)

        Dim gvr As GridView = sender

        If e.CommandName = "pick" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gvr.Rows(index)
            Dim TextBoxAddendumID As TextBox = FormViewDocumentEnter.FindControl("TextBoxAddendumID")
            TextBoxAddendumID.Text = row.Cells(2).Text
            Dim ModalPopupExtenderPickAddendum As ModalPopupExtender = FormViewDocumentEnter.FindControl("ModalPopupExtenderPickAddendum")
            ModalPopupExtenderPickAddendum.Hide()

            ' reset Addendum controls
            Dim TextBoxContractID As TextBox = FormViewDocumentEnter.FindControl("TextBoxContractID")
            TextBoxContractID.Text = 0
            Dim RequiredFieldValidatorTextBoxContractID As RequiredFieldValidator = FormViewDocumentEnter.FindControl("RequiredFieldValidatorTextBoxContractID")
            RequiredFieldValidatorTextBoxContractID.Enabled = False

            Dim DropDownListFxRateType As DropDownList = FormViewDocumentEnter.FindControl("DropDownListFxRateType")
            Dim TextBoxFxRate As TextBox = FormViewDocumentEnter.FindControl("TextBoxFxRate")
            ' if ruble contract, then set FxRateType as "N/A"
            If row.Cells(6).Text.ToLower = "rub" Then
                DropDownListFxRateType.SelectedIndex = 1
                TextBoxFxRate.Text = 0
                TextBoxFxRate.Enabled = False
            Else
                DropDownListFxRateType.SelectedIndex = 0
                TextBoxFxRate.Text = ""
                TextBoxFxRate.Enabled = True
            End If


        End If

    End Sub

    Protected Sub DropDownListPrj_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim TextBoxContractID As TextBox = FormViewDocumentEnter.FindControl("TextBoxContractID")
        TextBoxContractID.Text = String.Empty
        Dim RequiredFieldValidatorTextBoxContractID As RequiredFieldValidator = FormViewDocumentEnter.FindControl("RequiredFieldValidatorTextBoxContractID")
        RequiredFieldValidatorTextBoxContractID.Enabled = True

        Dim TextBoxAddendumID As TextBox = FormViewDocumentEnter.FindControl("TextBoxAddendumID")
        TextBoxAddendumID.Text = String.Empty
        Dim RequiredFieldValidatorTextBoxAddendumID As RequiredFieldValidator = FormViewDocumentEnter.FindControl("RequiredFieldValidatorTextBoxAddendumID")
        RequiredFieldValidatorTextBoxAddendumID.Enabled = True

        Dim ddl As DropDownList = sender
        SqlDataSourceSummary.SelectParameters("ProjectID").DefaultValue = ddl.SelectedValue

        ' provide parameter to gridviewHostiry
        SqlDataSourceDocumentsHistory.SelectParameters("ProjectID").DefaultValue = ddl.SelectedValue

    End Sub

    Protected Sub DropDownListClient_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim ddl As DropDownList = sender
        SqlDataSourceSummary.SelectParameters("ClientID").DefaultValue = ddl.SelectedValue

        ' provide parameter to gridviewHostiry
        SqlDataSourceDocumentsHistory.SelectParameters("ClientID").DefaultValue = ddl.SelectedValue

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

            ' provide image to imagebutton
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

        End If

    End Sub

    Protected Sub GridViewSummary_DataBound(sender As Object, e As EventArgs) Handles GridViewSummary.DataBound
        Dim grd As GridView = sender
        If grd.Rows.Count > 0 Then
            ' visible
            LabelSummary.Visible = True
        Else
            ' invisible
            LabelSummary.Visible = False
        End If
    End Sub

End Class

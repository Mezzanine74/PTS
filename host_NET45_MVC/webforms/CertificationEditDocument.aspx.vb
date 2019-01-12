
Partial Class CertificationEditDocument
    Inherits System.Web.UI.Page

    Protected Sub AddSelectToDDL(ByVal _ddl As DropDownList)

        Dim lst As New ListItem("_Select", "0")
        _ddl.Items.Insert(0, lst)

    End Sub

    Protected Sub DropDownListPrj_DataBound(sender As Object, e As EventArgs)

        AddSelectToDDL(sender)

    End Sub

    Protected Sub DropDownListClient_DataBound(sender As Object, e As EventArgs)

        AddSelectToDDL(sender)

    End Sub

    Protected Sub GridViewDocumentByProjectIDandClientID_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridViewDocumentByProjectIDandClientID.RowCommand

        If (e.CommandName = "OpenDocument") Then
            Dim LinkToFile As String = e.CommandArgument.ToString
            Dim openpdf As New MyCommonTasks
            openpdf.OpenPDF(LinkToFile)
        End If

        If (e.CommandName = "UploadDocument") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridViewDocumentByProjectIDandClientID.Rows(index)
            Dim FileUploadScanFile As FileUpload = DirectCast(row.FindControl("FileUploadScanFile"), FileUpload)
            Dim TextBoxStoreLink As TextBox = DirectCast(row.FindControl("TextBoxStoreLink"), TextBox)
            Dim LabelInfo As Label = DirectCast(row.FindControl("LabelInfo"), Label)

            If FileUploadScanFile.HasFile Then

                Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                FileUploadScanFile.SaveAs(MapPath("~/Certification/" + UniqueString1 + _
                                                  System.IO.Path.GetExtension(FileUploadScanFile.PostedFile.FileName)))
                TextBoxStoreLink.Text = "~/Certification/" + UniqueString1 + _
                                                  System.IO.Path.GetExtension(FileUploadScanFile.PostedFile.FileName)
                LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
                LabelInfo.Text = FileUploadScanFile.PostedFile.FileName + " has been loaded successfully"

            End If
        End If

    End Sub


    Protected Sub GridViewDocumentByProjectIDandClientID_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewDocumentByProjectIDandClientID.RowDataBound

        ' This will provide source to Client DDL in Item mode. It should be placed in the beginning, otherwise DisableDDLWithoutDisable function will be useless
        Dim DropDownListPrjItem As DropDownList = DirectCast(e.Row.FindControl("DropDownListPrjItem"), DropDownList)
        Dim ObjectDataSourceClientByProjectItem As ObjectDataSource = DirectCast(e.Row.FindControl("ObjectDataSourceClientByProjectItem"), ObjectDataSource)
        If ObjectDataSourceClientByProjectItem IsNot Nothing Then
            ObjectDataSourceClientByProjectItem.SelectParameters("ProjectID").DefaultValue = DropDownListPrjItem.SelectedValue
        End If

        ' This will provide source to Client DDL in Item mode. It should be placed in the beginning, otherwise DisableDDLWithoutDisable function will be useless
        Dim DropDownListPrjEdit As DropDownList = DirectCast(e.Row.FindControl("DropDownListPrjEdit"), DropDownList)
        Dim ObjectDataSourceClientByProjectEdit As ObjectDataSource = DirectCast(e.Row.FindControl("ObjectDataSourceClientByProjectEdit"), ObjectDataSource)
        If ObjectDataSourceClientByProjectEdit IsNot Nothing Then
            ObjectDataSourceClientByProjectEdit.SelectParameters("ProjectID").DefaultValue = DropDownListPrjEdit.SelectedValue
        End If

        ' it provides postback ability to invoice upload button
        If DirectCast(e.Row.FindControl("ButtonUploadEdit"), Button) IsNot Nothing Then
            ScriptManager1.RegisterPostBackControl(DirectCast(e.Row.FindControl("ButtonUploadEdit"), Button))
        End If

        If DirectCast(e.Row.FindControl("ImageButtonItem"), ImageButton) IsNot Nothing Then
            ScriptManager1.RegisterPostBackControl(DirectCast(e.Row.FindControl("ImageButtonItem"), ImageButton))
        End If

        If e.Row.RowState = DataControlRowState.Normal OrElse e.Row.RowState = DataControlRowState.Alternate Then

            DisableDDLWithoutDisable(DirectCast(e.Row.FindControl("DropDownListDocType"), DropDownList))
            DisableDDLWithoutDisable(DirectCast(e.Row.FindControl("DropDownListPrjItem"), DropDownList))
            DisableDDLWithoutDisable(DirectCast(e.Row.FindControl("DropDownListClientItem"), DropDownList))
            DisableDDLWithoutDisable(DirectCast(e.Row.FindControl("DropDownListFxRateType"), DropDownList))
            DisableDDLWithoutDisable(DirectCast(e.Row.FindControl("DropDownListAccount"), DropDownList))

            ' provide image to imagebutton
            PTSMainClass.ProvideImageFromFile(DirectCast(e.Row.FindControl("ImageButtonItem"), ImageButton), DataBinder.Eval(e.Row.DataItem, "ScanLink"))

        End If

        ' trim Date textbox, formatting doesnt work on ASPX page
        Dim TextBoxDocDate As TextBox = DirectCast(e.Row.FindControl("TextBoxDocDate"), TextBox)
        If TextBoxDocDate IsNot Nothing Then
            TextBoxDocDate.Text = Mid(TextBoxDocDate.Text, 1, 10)
        End If

        ' Provide validation
        Dim CompareValidatorTextBoxExcValue As CompareValidator = DirectCast(e.Row.FindControl("CompareValidatorTextBoxExcValue"), CompareValidator)
        If CompareValidatorTextBoxExcValue IsNot Nothing Then
            ProvideValidation(DirectCast(e.Row.FindControl("DropDownListDocType"), DropDownList), e.Row)
        End If

    End Sub

    Protected Sub DisableDDLWithoutDisable(ByVal _ddl As DropDownList)

        If _ddl IsNot Nothing Then
            _ddl.ForeColor = System.Drawing.Color.Red
            If _ddl.SelectedItem IsNot Nothing AndAlso _ddl.SelectedValue IsNot Nothing Then
                Dim li As New ListItem(_ddl.SelectedItem.Text, _ddl.SelectedValue, True)
                _ddl.Items.Clear()
                _ddl.Items.Add(li)
                li = Nothing
            End If
        End If

    End Sub

    Protected Sub ButtonUploadEdit_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub DropDownListDocType_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim ddl As DropDownList = sender
        Dim gvrow As GridViewRow = CType(sender, DropDownList).NamingContainer
        Dim rowindex As Integer = CType(gvrow, GridViewRow).RowIndex

        ProvideValidation(ddl, gvrow)

    End Sub

    Protected Sub ProvideValidation(ByVal ddl As DropDownList, ByVal gvrow As GridViewRow)

        Dim TextBoxExcValue As TextBox = DirectCast(gvrow.FindControl("TextBoxExcValue"), TextBox)
        Dim RequiredFieldValidatorExtValue As RequiredFieldValidator = DirectCast(gvrow.FindControl("RequiredFieldValidatorExtValue"), RequiredFieldValidator)
        Dim CompareValidatorTextBoxExcValue As CompareValidator = DirectCast(gvrow.FindControl("CompareValidatorTextBoxExcValue"), CompareValidator)

        Dim TextBoxAdvance As TextBox = DirectCast(gvrow.FindControl("TextBoxAdvance"), TextBox)
        Dim RequiredFieldValidatorAdvance As RequiredFieldValidator = DirectCast(gvrow.FindControl("RequiredFieldValidatorAdvance"), RequiredFieldValidator)
        Dim CompareValidatorTextBoxAdvance As CompareValidator = DirectCast(gvrow.FindControl("CompareValidatorTextBoxAdvance"), CompareValidator)

        Dim TextBoxRetention As TextBox = DirectCast(gvrow.FindControl("TextBoxRetention"), TextBox)
        Dim RequiredFieldValidatorRetention As RequiredFieldValidator = DirectCast(gvrow.FindControl("RequiredFieldValidatorRetention"), RequiredFieldValidator)
        Dim CompareValidatorTextBoxRetention As CompareValidator = DirectCast(gvrow.FindControl("CompareValidatorTextBoxRetention"), CompareValidator)

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

    Protected Sub GridViewDocumentByProjectIDandClientID_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GridViewDocumentByProjectIDandClientID.RowUpdating

        Dim index As Integer = Convert.ToInt32(e.RowIndex)
        Dim row As GridViewRow = GridViewDocumentByProjectIDandClientID.Rows(index)

        Dim TextBoxExcValue As TextBox = CType(row.FindControl("TextBoxExcValue"), TextBox)
        Dim TextBoxAdvance As TextBox = CType(row.FindControl("TextBoxAdvance"), TextBox)
        Dim TextBoxRetention As TextBox = CType(row.FindControl("TextBoxRetention"), TextBox)
        Dim TextBoxDocID As TextBox = CType(row.FindControl("TextBoxDocID"), TextBox)

        Dim ExcValue As Decimal = Math.Round(Convert.ToDecimal(TextBoxExcValue.Text), 2)
        Dim Advance As Decimal = Math.Round(Convert.ToDecimal(TextBoxAdvance.Text), 2)
        Dim Retention As Decimal = Math.Round(Convert.ToDecimal(TextBoxRetention.Text), 2)
        Dim TotalDocValueRevised As Decimal = 0

        Dim ddl As DropDownList = CType(row.FindControl("DropDownListDocType"), DropDownList)

        If ddl.SelectedValue = 1 Then
            '--1	KS-2,3
            TotalDocValueRevised = ExcValue - Advance - Retention

        ElseIf ddl.SelectedValue = 2 Then
            '--2	Tavarnaya Nakladnaya
            TotalDocValueRevised = ExcValue - Advance - Retention

        ElseIf ddl.SelectedValue = 3 Then
            '--3	Act
            TotalDocValueRevised = ExcValue - Advance - Retention

        ElseIf ddl.SelectedValue = 4 Then
            '--4	Invoice For Advance
            TotalDocValueRevised = Advance

        ElseIf ddl.SelectedValue = 5 Then
            '--5	Fines
            TotalDocValueRevised = ExcValue

        ElseIf ddl.SelectedValue = 6 Then
            '--6	Invoice For Retention
            TotalDocValueRevised = Retention

        End If

        ' Compare total revised DOC value to Total Invoice Value
        Using table As New CertificationTableAdapters.QueriesTableAdapterTotalSums
            If TotalDocValueRevised < table.GetTotalInvoiceValueByDocument(TextBoxDocID.Text) Then
                ' cancel update
                Dim LabelError As Label = CType(row.FindControl("LabelError"), Label)
                LabelError.Text = "Cannot Update. Total document value " + TotalDocValueRevised.ToString + " is cannot be smaller than total Invoice Value " + table.GetTotalInvoiceValueByDocument(TextBoxDocID.Text).ToString
                e.Cancel = True

            End If
        End Using

    End Sub
End Class

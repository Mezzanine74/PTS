Imports System
Imports System.IO
Imports System.Net
Imports System.Data.SqlClient
Imports System.Net.Mail

Partial Class _contractEnterAprMxPTM
    Inherits System.Web.UI.Page

    Protected Sub DropDownListPrj_DataBound(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsPostBack Then
            Dim lst1 As New ListItem("Select Project", "0")
            Dim DropDownListPrj As DropDownList = FormViewContract.FindControl("DropDownListPrj")
            DropDownListPrj.Items.Insert(0, lst1)
        End If
    End Sub

    'Protected Sub TextBoxOfferSupplier_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim LblShwSplr1 As Label = FormViewContract.FindControl("LabelSupplierNameOffer")
    '    Dim TextBoxSupplierID As TextBox = sender

    '    TextBoxSupplierID.Text = Left(TextBoxSupplierID.Text.ToString, 12)

    '    LblShwSplr1.Text = GetSupplierName(TextBoxSupplierID.Text)

    'End Sub

    Protected Sub x_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim LblShwSplr1 As Label = FormViewContract.FindControl("LabelShowSupplier")
        Dim TextBoxSupplierID As TextBox = sender

        TextBoxSupplierID.Text = Left(TextBoxSupplierID.Text.ToString, 12)

        LblShwSplr1.Text = GetSupplierName(TextBoxSupplierID.Text)

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim SqlDataSourcePrjClient As SqlDataSource = FormViewContract.FindControl("SqlDataSourcePrjClient")
        SqlDataSourcePrjClient.SelectParameters("username").DefaultValue = Page.User.Identity.Name.ToLower

        If Not (Roles.IsUserInRole("LawyerOnSite") Or Roles.IsUserInRole("ContractLeadGirls") Or Roles.IsUserInRole("InitiateContractAndAddendum")) Then
            ' user not to allow
            Response.Redirect("~/webforms/AccessDenied.aspx")
            Exit Sub

        End If

        Dim ButtonSubmit As LinkButton = FormViewContract.FindControl("InsertButton")
        PreventMultipleClick.PreventMultipleClicks(ButtonSubmit)

        Dim Task As New MyCommonTasks
        If Not IsPostBack Then
            ' check session variable if page coming from POrequest
            Dim userName As String = Page.User.Identity.Name.ToLower.ToString

            If GetCurrentScenario() = 0 Then

                ' no scenario exist for this user. return to POrequest page
                Response.Redirect("~/webforms/PORequest.aspx")
                Exit Sub

            ElseIf GetCurrentScenario() = -2 Then

                ' This is not possible. But i provided this codes just in case
                ' multiple scenario exist for this user. Delete all scenario for this user, go to POrequest page
                Task.SendEmailToAdmin("More than one scenario occurs in user " + userName, "all scenarios for this user deleted")
                SessionsPoRequest.DeleteALLSessions(userName)
                Response.Redirect("~/webforms/PORequest.aspx")
                Exit Sub

            ElseIf GetCurrentScenario() = -1 Then

                ' This is frame contract
                'ShowOrHideCommercialOfferPanel()
                AssignAllSessionVaiablesToControls()

            ElseIf GetCurrentScenario() > 0 Then

                If GetCurrentScenario() = 1 And ContractView.SmallContract() = False Then
                    Response.Redirect("~/webforms/pocreateNew.aspx")
                    Exit Sub
                End If

                ' This is the place to define Offer Panel to be shown or not
                'ShowOrHideCommercialOfferPanel()

                'Assing all session variables to controls
                AssignAllSessionVaiablesToControls()

            End If
        End If

        If Not IsPostBack Or IsPostBack Then
            ' it adds jscript into insert button
            'Dim FormViewSupplier As FormView = FormViewContract.FindControl("FormViewSupplier")
            'Dim Inst As LinkButton = FormViewSupplier.FindControl("InsertButton")

            'Inst.Attributes.Add("onclick", "return SupplierInsert();")

            'Dim checkBoxClient As CheckBox = FormViewSupplier.FindControl("CheckBoxClient")
            'checkBoxClient.Attributes.Add("onclick", "ClickEnable();")
        End If

        If Not IsPostBack Or IsPostBack Then
            If GetCurrentScenario() = 1 Then
                Dim RequiredFieldValidatorBudgetPDF As RequiredFieldValidator = TryCast(FormViewContract.FindControl("RequiredFieldValidatorBudgetPDF"), RequiredFieldValidator)
                RequiredFieldValidatorBudgetPDF.Enabled = False
            End If
        End If

        Dim TextBoxBudget As TextBox = FormViewContract.FindControl("TextBoxBudget")
        If TextBoxBudget IsNot Nothing Then
            TextBoxBudget.Text = 0.1
        End If

    End Sub

    Protected Sub SqlDataSourceSupplierEnter_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs)


    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        Dim tr_clientproject As System.Web.UI.HtmlControls.HtmlTableRow = DirectCast(FormViewContract.FindControl("tr_clientproject"), System.Web.UI.HtmlControls.HtmlTableRow)
        Dim DropDownListPrj As DropDownList = FormViewContract.FindControl("DropDownListPrj")
        Dim CompareValidatorPrjClient As CompareValidator = FormViewContract.FindControl("CompareValidatorPrjClient")
        If DropDownListPrj.SelectedValue = "999" Then
            tr_clientproject.Visible = True
            CompareValidatorPrjClient.Enabled = True
        End If

        'AssignCountOfOfferToTextBoxForValidation()

        If IsPostBack Then

            Dim FrmViewSupplier As FormView = FormViewContract.FindControl("FormViewSupplier")
            Dim aBtn As Button = FormViewContract.FindControl("ButtonEnterSupplier")

            If FrmViewSupplier.Visible = True Then
                aBtn.Text = "Hide Supplier Window"
            ElseIf FrmViewSupplier.Visible = False Then
                aBtn.Text = "Add New Supplier"
            End If
        End If

        If IsPostBack Then
            Dim controll As New String(Page.Request.Params.Get("__EVENTTARGET"))
            If (Not controll Is Nothing) Or (controll <> "") Then
                If controll = "ctl00$MainContent$FormViewContract$x" Then

                    Dim LblShwSplr As Label = FormViewContract.FindControl("LabelShowSupplier")
                    Dim LabelErr As Label = FormViewContract.FindControl("LabelSplrError")
                    Dim TxtSupplier As TextBox = FormViewContract.FindControl("x")

                    If LblShwSplr.Text = "" Then
                        LabelErr.Text = "INN Number is not 10 digit or Supplier does not exist"
                        TxtSupplier.BackColor = System.Drawing.Color.Yellow
                    ElseIf LblShwSplr.Text <> "" Then
                        LabelErr.Text = ""
                        TxtSupplier.BackColor = System.Drawing.Color.White
                    End If
                End If
            End If
        End If

        'If IsPostBack Then
        '    Dim controll As New String(Page.Request.Params.Get("__EVENTTARGET"))
        '    If (Not controll Is Nothing) Or (controll <> "") Then
        '        If controll = "ctl00$MainContent$FormViewContract$TextBoxOfferSupplier" Then

        '            Dim LblShwSplr As Label = FormViewContract.FindControl("LabelSupplierNameOffer")
        '            Dim LabelErr As Label = FormViewContract.FindControl("LabelSupplierErrorOffer")
        '            Dim TxtSupplier As TextBox = FormViewContract.FindControl("TextBoxOfferSupplier")

        '            If LblShwSplr.Text = "" Then
        '                LabelErr.Text = "INN Number is not 10 digit or Supplier does not exist"
        '                TxtSupplier.BackColor = System.Drawing.Color.Yellow
        '            ElseIf LblShwSplr.Text <> "" Then
        '                LabelErr.Text = ""
        '                TxtSupplier.BackColor = System.Drawing.Color.White
        '            End If
        '        End If
        '    End If
        'End If


    End Sub

    Protected Sub ButtonEnterSupplier_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FrmViewSupplier As FormView = FormViewContract.FindControl("FormViewSupplier")
        Dim Btn As Button = FormViewContract.FindControl("ButtonEnterSupplier")

        If Btn.Text = "Add New Supplier" Then
            FrmViewSupplier.Visible = True
            Dim SupplierIDTextBox As TextBox = FrmViewSupplier.FindControl("SupplierIDTextBox")
            Dim SupplierNameTextBox As TextBox = FrmViewSupplier.FindControl("SupplierNameTextBox")
            'Dim CheckBoxClient As CheckBox = FrmViewSupplier.FindControl("CheckBoxClient")
            Dim VAT_FreeCheckBox2 As CheckBox = FrmViewSupplier.FindControl("VAT_FreeCheckBox2")

            SupplierIDTextBox.Text = ""
            SupplierNameTextBox.Text = ""
            'CheckBoxClient.Checked = False
            VAT_FreeCheckBox2.Checked = False

        End If

        If Btn.Text = "Hide Supplier Window" Then
            FrmViewSupplier.Visible = False
            Btn.Text = "Add New Supplier"
        End If

    End Sub

    Protected Sub SqlDataSourceSupplierEnter_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
        Dim command As System.Data.Common.DbCommand = e.Command
        Dim FrmViewSupplier As FormView = FormViewContract.FindControl("FormViewSupplier")
        Dim TextBoxSupplier As TextBox = FormViewContract.FindControl("x")

        Dim LblShwSplr As Label = FormViewContract.FindControl("LabelShowSupplier")
        Dim LabelSplrError As Label = FormViewContract.FindControl("LabelSplrError")

        FrmViewSupplier.Visible = False
        TextBoxSupplier.Text = command.Parameters("@SupplierID").Value
        TextBoxSupplier.BackColor = System.Drawing.Color.White

        LblShwSplr.Text = GetSupplierName(command.Parameters("@SupplierID").Value)
        LabelSplrError.Text = ""

    End Sub

    Protected Sub FormViewContract_ItemInserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertEventArgs) Handles FormViewContract.ItemInserting

        Dim LabelSplrError As Label = FormViewContract.FindControl("LabelSplrError")
        If LabelSplrError.Text = "INN Number is not 10 digit or Supplier does not exist" Then
            e.Cancel = True

        Else
            Dim DropDownListPrj As DropDownList = FormViewContract.FindControl("DropDownListPrj")
            Dim TextBoxSupplierID As TextBox = FormViewContract.FindControl("x")

            e.Cancel = False

            ' update attachmentExist column
            Dim TextLink As TextBox = FormViewContract.FindControl("LinkToPDFcopyTextBox")
            If Len(TextLink.Text) > 0 OrElse Not String.IsNullOrEmpty(TextLink.Text) Then
                e.Values("AttachmentExist") = True
            Else
                e.Values("AttachmentExist") = False
            End If

            Dim ContractNoTextBox As TextBox = FormViewContract.FindControl("ContractNoTextBox")
            Dim ContractValue_woVATTextBox As TextBox = FormViewContract.FindControl("ContractValue_woVATTextBox")
            Dim TextBoxContractValue_withVAT As TextBox = FormViewContract.FindControl("TextBoxContractValue_withVAT")
            Dim TextBoxVAT As TextBox = FormViewContract.FindControl("TextBoxVAT")
            Dim DropDownListCurrency As DropDownList = FormViewContract.FindControl("DropDownListCurrency")
            Dim DropDownListContractType As DropDownList = FormViewContract.FindControl("DropDownListContractType")
            Dim ContractDateTextBox As TextBox = FormViewContract.FindControl("ContractDateTextBox")
            Dim TextBoxCostCode As TextBox = FormViewContract.FindControl("TextBoxCostCode")
            Dim TextBoxRequestedBy As TextBox = FormViewContract.FindControl("TextBoxRequestedBy")
            Dim DropDownListPenalty As DropDownList = FormViewContract.FindControl("DropDownListPenalty")
            Dim TextBoxPenaltyNote As TextBox = FormViewContract.FindControl("TextBoxPenaltyNote")
            Dim DropDownListPenaltyToSupplier As DropDownList = FormViewContract.FindControl("DropDownListPenaltyToSupplier")
            Dim TextBoxPenaltyNoteToSupplier As TextBox = FormViewContract.FindControl("TextBoxPenaltyNoteToSupplier")
            Dim TextBoxRetention As TextBox = FormViewContract.FindControl("TextBoxRetention")
            Dim TextBoxStartDate As TextBox = FormViewContract.FindControl("TextBoxStartDate")
            Dim TextBoxFinishDate As TextBox = FormViewContract.FindControl("TextBoxFinishDate")
            Dim TextBoxBudget As TextBox = FormViewContract.FindControl("TextBoxBudget")
            Dim TextBoxAdvance As TextBox = FormViewContract.FindControl("TextBoxAdvance")
            Dim TextBoxInterim As TextBox = FormViewContract.FindControl("TextBoxInterim")
            Dim TextBoxShipment As TextBox = FormViewContract.FindControl("TextBoxShipment")
            Dim TextBoxDelivery As TextBox = FormViewContract.FindControl("TextBoxDelivery")
            Dim LabelPaymentTermsValidationNotification As Label = FormViewContract.FindControl("LabelPaymentTermsValidationNotification")

            If ContractView.PaymentTermsValidated(TextBoxAdvance.Text, TextBoxInterim.Text, TextBoxShipment.Text,
                                     TextBoxDelivery.Text, TextBoxRetention.Text) = False Then

                e.Cancel = True
                LabelPaymentTermsValidationNotification.Visible = True
                Exit Sub
            End If

            e.Values("ProjectID") = DropDownListPrj.SelectedValue.ToString

            ' Contract No required, but we relased validation control. DONT REMOVE THIS.
            If String.IsNullOrEmpty(ContractNoTextBox.Text) Then
                e.Values("ContractNo") = String.Empty
            End If
            ' Contract No required, but we relased validation control. DONT REMOVE THIS.

            e.Values("Nominated") = GetNominated(Page.User.Identity.Name.ToLower.ToString + "Scenario" + GetCurrentScenario().ToString)
            e.Values("FrameContract") = GetFrameContract(Page.User.Identity.Name.ToLower.ToString + "Scenario" + GetCurrentScenario().ToString)

            If ContractValue_woVATTextBox.Text <> "" Then
                e.Values("ContractValue_woVAT") = Convert.ToDecimal(ContractValue_woVATTextBox.Text)
            ElseIf ContractValue_woVATTextBox.Text = "" Then
                e.Values("ContractValue_woVAT") = Nothing
            End If

            If TextBoxContractValue_withVAT.Text <> "" Then
                e.Values("ContractValue_withVAT") = Convert.ToDecimal(TextBoxContractValue_withVAT.Text)
            ElseIf TextBoxContractValue_withVAT.Text = "" Then
                e.Cancel = True
                Exit Sub
            End If

            If TextBoxVAT.Text <> "" Then
                e.Values("VATpercent") = Convert.ToDecimal(TextBoxVAT.Text)
            ElseIf TextBoxVAT.Text = "" Then
                e.Cancel = True
                Exit Sub
            End If

            If TextBoxCostCode.Text <> "" Then
                e.Values("CostCode") = TextBoxCostCode.Text
            ElseIf TextBoxVAT.Text = "" Then
                e.Values("CostCode") = Nothing
            End If

            If TextBoxRequestedBy.Text <> "" Then
                e.Values("RequestedBy") = TextBoxRequestedBy.Text
            ElseIf TextBoxRequestedBy.Text = "" Then
                e.Values("RequestedBy") = Nothing
            End If

            e.Values("ContractCurrency") = DropDownListCurrency.SelectedValue.ToString
            e.Values("ContractType") = DropDownListContractType.SelectedValue.ToString

            If ContractDateTextBox.Text <> "" Then
                SqlDataSourceContract.InsertParameters("ContractDate").DefaultValue _
                  = Convert.ToDateTime(Mid(ContractDateTextBox.Text.ToString, 1, 2).ToString +
                                       "/" + Mid(ContractDateTextBox.Text.ToString, 4, 2).ToString +
                                       "/" + Mid(ContractDateTextBox.Text.ToString, 7, 4).ToString)
            ElseIf ContractDateTextBox.Text = "" Then
                SqlDataSourceContract.InsertParameters("ContractDate").DefaultValue = Nothing
            End If

            ' INSERTING ADDITIONAL COMMERCIAL CONTROLS

            If String.IsNullOrEmpty(TextBoxBudget.Text) Then
                e.Values("Budget") = Nothing
            End If

            If String.IsNullOrEmpty(TextBoxAdvance.Text) Then
                e.Values("Advance") = Nothing
            End If

            If String.IsNullOrEmpty(TextBoxInterim.Text) Then
                e.Values("Interim") = Nothing
            End If

            If String.IsNullOrEmpty(TextBoxShipment.Text) Then
                e.Values("Shipment") = Nothing
            End If

            If String.IsNullOrEmpty(TextBoxDelivery.Text) Then
                e.Values("Delivery") = Nothing
            End If

            If DropDownListPenalty.SelectedValue.ToString.Trim = "-" Then
                e.Values("Penalties") = Nothing
            Else
                e.Values("Penalties") = DropDownListPenalty.SelectedValue
            End If

            If String.IsNullOrEmpty(TextBoxPenaltyNote.Text) Then
                e.Values("PenaltiesNote") = Nothing
            Else
                e.Values("PenaltiesNote") = TextBoxPenaltyNote.Text
            End If

            If DropDownListPenaltyToSupplier.SelectedValue.ToString.Trim = "-" Then
                e.Values("PenaltiesToSupplier") = Nothing
            Else
                e.Values("PenaltiesToSupplier") = DropDownListPenaltyToSupplier.SelectedValue
            End If

            If String.IsNullOrEmpty(TextBoxPenaltyNoteToSupplier.Text) Then
                e.Values("PenaltiesToSupplierNote") = Nothing
            Else
                e.Values("PenaltiesToSupplierNote") = TextBoxPenaltyNoteToSupplier.Text
            End If

            If String.IsNullOrEmpty(TextBoxStartDate.Text) Then
                e.Values("StartDate") = Nothing
            Else
                e.Values("StartDate") = Convert.ToDateTime(Mid(TextBoxStartDate.Text.ToString, 1, 2).ToString +
                                         "/" + Mid(TextBoxStartDate.Text.ToString, 4, 2).ToString +
                                         "/" + Mid(TextBoxStartDate.Text.ToString, 7, 4).ToString)
            End If

            If String.IsNullOrEmpty(TextBoxStartDate.Text) Then
                e.Values("FinishDate") = Nothing
            Else
                e.Values("FinishDate") = Convert.ToDateTime(Mid(TextBoxFinishDate.Text.ToString, 1, 2).ToString +
                                   "/" + Mid(TextBoxFinishDate.Text.ToString, 4, 2).ToString +
                                   "/" + Mid(TextBoxFinishDate.Text.ToString, 7, 4).ToString)
            End If


            If String.IsNullOrEmpty(TextBoxRetention.Text) Then
                e.Values("Retention") = Nothing
            Else
                e.Values("Retention") = TextBoxRetention.Text
            End If

            ' ________END OF  INSERTING ADDITIONAL COMMERCIAL CONTROLS

            Dim zoneId As String = "Russian Standard Time"
            Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
            Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

            SqlDataSourceContract.InsertParameters("CreatedBy").DefaultValue = result
            SqlDataSourceContract.InsertParameters("PersonCreated").DefaultValue = Page.User.Identity.Name.ToString

            If Right(LiteralTitle.Text, 2) = "-1" Then
                SqlDataSourceContract.InsertParameters("Scenario").DefaultValue =
                  Convert.ToInt32(Right(LiteralTitle.Text, 2))

            Else
                SqlDataSourceContract.InsertParameters("Scenario").DefaultValue =
                  Convert.ToInt32(Right(LiteralTitle.Text, 1))

            End If

        End If

    End Sub

    'Protected Sub ButtonUploadOffer_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim FileToUpload As FileUpload = FormViewContract.FindControl("FileUploadOffer")
    '    Dim DropProject As DropDownList = FormViewContract.FindControl("DropDownListPrj")
    '    Dim LabelInfo As Label = FormViewContract.FindControl("LabelInfoOfferUpload")
    '    Dim TextBoxOfferFilePath As TextBox = FormViewContract.FindControl("TextBoxOfferFilePath")

    '    If DropProject.SelectedItem.ToString = "Select Project" Then
    '        LabelInfo.ForeColor = System.Drawing.Color.Red
    '        LabelInfo.Text = "Please select Project Name"
    '    Else
    '        If Len(System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)) > 0 Then
    '            If System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".zip" Then
    '                'If FileToUpload.PostedFile.ContentLength / 1000 > 5000 Then
    '                '    LabelInfo.ForeColor = System.Drawing.Color.Red
    '                '    LabelInfo.Text = "PDF file size must be less than 5MB"
    '                '    TextLink.Text = ""
    '                'Else
    '                If Directory.Exists(Server.MapPath("~/CommercialOffers/") + DropProject.SelectedItem.ToString + "/") Then
    '                    Dim UniqueString11 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
    '                    Dim UniqueString12 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
    '                    FileToUpload.SaveAs(MapPath("~/CommercialOffers/" + DropProject.SelectedItem.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)))
    '                    TextBoxOfferFilePath.Text = "~/CommercialOffers/" + DropProject.SelectedItem.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)
    '                    LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
    '                    LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
    '                Else
    '                    Dim UniqueString21 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
    '                    Dim UniqueString22 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
    '                    Directory.CreateDirectory(Server.MapPath("~/CommercialOffers/") + DropProject.SelectedItem.ToString)
    '                    FileToUpload.SaveAs(MapPath("~/CommercialOffers/" + DropProject.SelectedItem.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)))
    '                    TextBoxOfferFilePath.Text = "~/CommercialOffers/" + DropProject.SelectedItem.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)
    '                    LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
    '                    LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
    '                End If
    '                'End If
    '            Else
    '                LabelInfo.ForeColor = System.Drawing.Color.Red
    '                LabelInfo.Text = "Please select ZIP (compressed file) format"
    '            End If
    '        Else
    '            LabelInfo.ForeColor = System.Drawing.Color.Red
    '            LabelInfo.Text = "you did not specify any file"
    '        End If
    '    End If
    'End Sub

    Protected Sub ButtonUploadDOC_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim DropProject As DropDownList = FormViewContract.FindControl("DropDownListPrj")
        Dim TextLink As TextBox = FormViewContract.FindControl("LinkToTemplatefile_DOCTextBox")

        Dim UniqueString21 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
        Dim UniqueString22 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")

        'Dim webFormCommon = New host_NET45_MVC.App_Code.WebFormCommon

        If TextLink.Text.Trim().Length > 0 AndAlso IO.Directory.Exists(Server.MapPath(TextLink.Text)) Then
            'webFormCommon.processFileManager(Page, panelContainer, TextLink.Text, 1)
            ASPxFileManager1.Visible = True
            ASPxFileManager1.Settings.RootFolder = TextLink.Text.Trim()
            ASPxFileManager1.SettingsUpload.Enabled = True
            ASPxFileManager1.SettingsEditing.AllowDelete = True
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)

        Else
            Dim _directory As String = "~/CONTRACT/" + DropProject.SelectedItem.ToString + "/" + UniqueString21 + UniqueString22
            TextLink.Text = _directory

            Dim _createDirectory As String = Server.MapPath(_directory)
            Directory.CreateDirectory(_createDirectory)
            'webFormCommon.processFileManager(Page, panelContainer, TextLink.Text, 1)
            ASPxFileManager1.Visible = True
            ASPxFileManager1.Settings.RootFolder = _directory
            ASPxFileManager1.SettingsUpload.Enabled = True
            ASPxFileManager1.SettingsEditing.AllowDelete = True
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)

        End If

        'Dim FileToUpload As FileUpload = FormViewContract.FindControl("FileUploadDOC")

        ''Dim TxtInvoiceID As TextBox = FormViewContract.FindControl("InvoiceIDTextBox")
        'Dim LabelInfo As Label = FormViewContract.FindControl("LabelInfoDOC")


        'If DropProject.SelectedItem.ToString = "Select Project" Then
        '    LabelInfo.ForeColor = System.Drawing.Color.Red
        '    LabelInfo.Text = "Please select Project Name"
        '    TextLink.Text = ""
        'Else
        '    If FileToUpload.HasFile Then
        '        If System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".doc" _
        '            OrElse System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".docx" _
        '            OrElse System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".pdf" _
        '            OrElse System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".zip" Then
        '            'If FileToUpload.PostedFile.ContentLength / 1000 > 5000 Then
        '            '    LabelInfo.ForeColor = System.Drawing.Color.Red
        '            '    LabelInfo.Text = "PDF file size must be less than 5MB"
        '            '    TextLink.Text = ""
        '            'Else
        '            If Directory.Exists(Server.MapPath("~/CONTRACT/") + DropProject.SelectedItem.ToString + "/") Then
        '                Dim UniqueString11 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
        '                Dim UniqueString12 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
        '                FileToUpload.SaveAs(MapPath("~/CONTRACT/" + DropProject.SelectedItem.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)))
        '                TextLink.Text = "~/CONTRACT/" + DropProject.SelectedItem.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)
        '                LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
        '                LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
        '            Else
        '                Dim UniqueString21 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
        '                Dim UniqueString22 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
        '                Directory.CreateDirectory(Server.MapPath("~/CONTRACT/") + DropProject.SelectedItem.ToString)
        '                FileToUpload.SaveAs(MapPath("~/CONTRACT/" + DropProject.SelectedItem.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)))
        '                TextLink.Text = "~/CONTRACT/" + DropProject.SelectedItem.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)
        '                LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
        '                LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
        '            End If
        '            'End If
        '        Else
        '            LabelInfo.ForeColor = System.Drawing.Color.Red
        '            LabelInfo.Text = "Please select MS Word, ZIP or PDF format file"
        '            TextLink.Text = ""
        '        End If
        '    Else
        '        TextLink.Text = ""
        '        LabelInfo.ForeColor = System.Drawing.Color.Red
        '        LabelInfo.Text = "you did not specify any file"
        '    End If
        'End If
    End Sub

    Protected Sub ButtonUploadPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FileToUpload As FileUpload = FormViewContract.FindControl("FileUploadPDF")
        Dim DropProject As DropDownList = FormViewContract.FindControl("DropDownListPrj")
        'Dim TxtInvoiceID As TextBox = FormViewContract.FindControl("InvoiceIDTextBox")
        Dim LabelInfo As Label = FormViewContract.FindControl("LabelInfoPDF")
        Dim TextLink As TextBox = FormViewContract.FindControl("LinkToPDFcopyTextBox")

        If DropProject.SelectedItem.ToString = "Select Project" Then
            LabelInfo.ForeColor = System.Drawing.Color.Red
            LabelInfo.Text = "Please select Project Name"
            TextLink.Text = ""
        Else
            If FileToUpload.HasFile Then
                If System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".pdf" _
                  OrElse System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".doc" _
                  OrElse System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".docx" Then
                    'If FileToUpload.PostedFile.ContentLength / 1000 > 5000 Then
                    '    LabelInfo.ForeColor = System.Drawing.Color.Red
                    '    LabelInfo.Text = "PDF file size must be less than 5MB"
                    '    TextLink.Text = ""
                    'Else
                    If Directory.Exists(Server.MapPath("~/CONTRACT/") + DropProject.SelectedItem.ToString + "/") Then
                        Dim UniqueString11 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                        Dim UniqueString12 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                        FileToUpload.SaveAs(MapPath("~/CONTRACT/" + DropProject.SelectedItem.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)))
                        TextLink.Text = "~/CONTRACT/" + DropProject.SelectedItem.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)
                        LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
                        LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
                    Else
                        Dim UniqueString21 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                        Dim UniqueString22 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                        Directory.CreateDirectory(Server.MapPath("~/CONTRACT/") + DropProject.SelectedItem.ToString)
                        FileToUpload.SaveAs(MapPath("~/CONTRACT/" + DropProject.SelectedItem.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)))
                        TextLink.Text = "~/CONTRACT/" + DropProject.SelectedItem.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)
                        LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
                        LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
                    End If
                    'End If
                Else
                    LabelInfo.ForeColor = System.Drawing.Color.Red
                    LabelInfo.Text = "Please select PDF, DOC or DOCX format file"
                    TextLink.Text = ""
                End If
            Else
                TextLink.Text = ""
                LabelInfo.ForeColor = System.Drawing.Color.Red
                LabelInfo.Text = "you did not specify any file"
            End If
        End If
    End Sub

    Protected Sub FormViewContract_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormViewContract.Load
        If IsPostBack Or Not IsPostBack Then
            ' it cause button to postback. 
            Dim ButtonUploadDOC As Button = FormViewContract.FindControl("ButtonUploadDOC")
            Dim ButtonUploadPDF As Button = FormViewContract.FindControl("ButtonUploadPDF")
            'Dim ButtonUploadOffer As Button = FormViewContract.FindControl("ButtonUploadOffer")
            'Dim ButtonInsertOffer As Button = FormViewContract.FindControl("ButtonInsertOffer")


        End If

    End Sub

    Protected Function GetSupplierName(ByVal _supplierID As String) As String

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT [SupplierName] FROM [Table6_Supplier] WHERE ([SupplierID] = @SupplierID) "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim SupplierID As SqlParameter = cmd.Parameters.Add("@SupplierID", System.Data.SqlDbType.NVarChar)
            SupplierID.Value = _supplierID
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim _return As String = ""
            While dr.Read
                _return = dr(0).ToString
            End While
            con.Close()
            dr.Close()
            con.Dispose()
            Return _return
        End Using


    End Function

    Protected Function GetCurrentScenario() As Integer

        ' This Stored procedure returns 4 different value
        ' 0  no scenario exist for this user
        ' 1  scenario exist for this user
        ' -1  Frame Contract scenario exist for this user
        ' -2 multiple scenario exist for this user 
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "ScenarioCurrent"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.StoredProcedure
            Dim userName As String = Page.User.Identity.Name.ToLower.ToString

            'syntax for parameter adding
            Dim Scenario As SqlParameter = cmd.Parameters.Add("@Scenario", System.Data.SqlDbType.NVarChar)
            Scenario.Value = userName + "Scenario"
            Dim ReturnValue As Integer
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                ReturnValue = dr(0)
            End While
            Return ReturnValue
            con.Close()
            dr.Close()
            con.Dispose()
        End Using

    End Function

    'Protected Sub ButtonInsertOffer_Click(sender As Object, e As System.EventArgs)
    '    ' Assign Values
    '    Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
    '        con.Open()
    '        Dim sqlstring As String = " INSERT INTO [Table_ScenarioOffers] " + _
    '                              "            ([Scenario] " + _
    '                              "            ,[SupplierID] " + _
    '                              "            ,[OfferValueWithVAT] " + _
    '                              "            ,[Currency] " + _
    '                              "            ,[Attachment]) " + _
    '                              "      VALUES " + _
    '                              "            (@Scenario " + _
    '                              "            ,@SupplierID " + _
    '                              "            ,@OfferValueWithVAT " + _
    '                              "            ,@Currency " + _
    '                              "            ,@Attachment) "

    '        Dim cmd As New SqlCommand(sqlstring, con)
    '        cmd.CommandType =System.Data.CommandType.Text

    '        'syntax for parameter adding
    '        Dim userName As String = Page.User.Identity.Name.ToLower.ToString
    '        Dim Scenario As SqlParameter = cmd.Parameters.Add("@Scenario",System.Data.SqlDbType.NVarChar)
    '        Scenario.Value = userName + "Scenario" + GetCurrentScenario().ToString

    '        Dim SupplierID As SqlParameter = cmd.Parameters.Add("@SupplierID",System.Data.SqlDbType.NVarChar)
    '        Dim TextBoxOfferSupplier As TextBox = FormViewContract.FindControl("TextBoxOfferSupplier")
    '        SupplierID.Value = TextBoxOfferSupplier.Text

    '        Dim OfferValueWithVAT As SqlParameter = cmd.Parameters.Add("@OfferValueWithVAT",System.Data.SqlDbType.Decimal)
    '        Dim TextBoxOfferValueWithVAT As TextBox = FormViewContract.FindControl("TextBoxOfferValueWithVAT")
    '        OfferValueWithVAT.Value = Convert.ToDecimal(TextBoxOfferValueWithVAT.Text)

    '        Dim Currency As SqlParameter = cmd.Parameters.Add("@Currency",System.Data.SqlDbType.NVarChar)
    '        Dim DropDownListCurrencyOffer As DropDownList = FormViewContract.FindControl("DropDownListCurrencyOffer")
    '        Currency.Value = DropDownListCurrencyOffer.SelectedValue

    '        Dim Attachment As SqlParameter = cmd.Parameters.Add("@Attachment",System.Data.SqlDbType.NVarChar)
    '        Dim TextBoxOfferFilePath As TextBox = FormViewContract.FindControl("TextBoxOfferFilePath")
    '        Attachment.Value = TextBoxOfferFilePath.Text

    '        Dim dr As SqlDataReader = cmd.ExecuteReader

    '        ' reset all controls
    '        Dim LabelSupplierNameOffer As Label = FormViewContract.FindControl("LabelSupplierNameOffer")
    '        LabelSupplierNameOffer.Text = String.Empty
    '        TextBoxOfferSupplier.Text = String.Empty
    '        TextBoxOfferValueWithVAT.Text = String.Empty
    '        DropDownListCurrencyOffer.SelectedIndex = 0
    '        TextBoxOfferFilePath.Text = String.Empty
    '        Dim LabelInfoOfferUpload As Label = FormViewContract.FindControl("LabelInfoOfferUpload")
    '        LabelInfoOfferUpload.Text = String.Empty

    '        'Dim RequiredFieldValidatorOfferSupplier As RequiredFieldValidator = FormViewContract.FindControl("RequiredFieldValidatorOfferSupplier")

    '        ' refresh Gridview
    '        Dim GridViewOffers As GridView = FormViewContract.FindControl("GridViewOffers")
    '        GridViewOffers.DataBind()

    '        con.Close()
    '        dr.Close()
    '        con.Dispose()
    '    End Using


    'End Sub

    'Protected Sub AssignCountOfOfferToTextBoxForValidation()

    '    Dim TextBoxOfferCount As TextBox = FormViewContract.FindControl("TextBoxOfferCount")
    '    Dim GridViewOffers As GridView = FormViewContract.FindControl("GridViewOffers")
    '    GridViewOffers.DataBind()
    '    TextBoxOfferCount.Text = GridViewOffers.Rows.Count.ToString

    'End Sub

    'Protected Sub GridViewOffers_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
    '    If (e.CommandName = "OpenZip") Then
    '        Dim LinkToFile As String = e.CommandArgument.ToString
    '        Dim openpdf As New MyCommonTasks
    '        openpdf.OpenPDF(LinkToFile)
    '    End If
    'End Sub

    'Protected Sub GridViewOffers_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
    'End Sub

    Protected Sub SqlDataSourceContract_Inserted(sender As Object, e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourceContract.Inserted

        Dim scenario_ As String = Replace(LiteralTitle.Text, " ", "")
        Dim name As String = Page.User.Identity.Name.ToLower.ToString
        Dim contractid As Integer = e.Command.Parameters("@id").Value

        InsertOffersToContract(contractid, name + scenario_)

        ' delete current scenario
        SessionsPoRequest.RemoveScenarioSession(scenario_)

        ' send email to required approval person
        ' parameter 1 for contract enter
        ' Prepare Email Body from WebUserControl_ContractEmailBody

        Dim DropDownListProjectClient As DropDownList = FormViewContract.FindControl("DropDownListProjectClient")

        If DropDownListProjectClient.SelectedValue = "999" Then
            Using db As New PTS_App_Code_VB_Project.PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim A = New PTS_App_Code_VB_Project.PTS_MERCURY.db.Table_Contract_ProjectIDforClient With {.ContractID = contractid, .ProjectID = DropDownListProjectClient.SelectedValue}

                db.Table_Contract_ProjectIDforClient.Add(A)
                db.SaveChanges()

            End Using
        End If

        SendEmailApprovalMatrix.Send(contractid, 1, Nothing, Nothing, ContractView.ProduceHTMLforContractEmailBody(WebUserControl_ContractEmailBody, contractid))

        Response.Redirect("~/webforms/default.aspx")

    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Exit Sub
    End Sub

    Protected Sub InsertOffersToContract(ByVal _ContractID As Integer, ByVal _Scenario As String)
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " INSERT INTO [Table_Contract_Offers] " +
                                      "            ([ContractID] " +
                                      "            ,[SupplierID] " +
                                      "            ,[OfferValueWithVAT] " +
                                      "            ,[Currency] " +
                                      "            ,[Attachment]) " +
                                      "             " +
                                      " ( " +
                                      " SELECT @ContractID " +
                                      "       ,[SupplierID] " +
                                      "       ,[OfferValueWithVAT] " +
                                      "       ,[Currency] " +
                                      "       ,[Attachment] " +
                                      "   FROM [Table_ScenarioOffers] " +
                                      " WHERE Scenario = @Scenario " +
                                      " ) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", System.Data.SqlDbType.Int)
            ContractID.Value = _ContractID
            Dim Scenario As SqlParameter = cmd.Parameters.Add("@Scenario", System.Data.SqlDbType.NVarChar)
            Scenario.Value = _Scenario
            Dim dr As SqlDataReader = cmd.ExecuteReader
            con.Close()
            dr.Close()
            con.Dispose()
        End Using

    End Sub

    ' I WANTED THIS AS SEPERATE CLASS IN APP_CODE, BUT NOT WORKING

    Protected Function GetVariables(ByVal userName_Scenario As String) As ScenarioVariables

        Dim userName As String = HttpContext.Current.User.Identity.Name.ToLower.ToString
        Dim _return As New ScenarioVariables

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT [Scenario] " +
                              "       ,[ProjectID] " +
                              "       ,[TotalPrice] " +
                              "       ,RTRIM([VAT]) AS VAT " +
                              "       ,RTRIM([Currency]) AS Currency " +
                              "       ,RTRIM([CostCode]) AS CostCode " +
                              "       ,RTRIM([RequestedBy]) AS RequestedBy " +
                              "   FROM [Table_Scenario] " +
                              " WHERE Scenario LIKE '%' + @Scenario + '%' "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim Scenario As SqlParameter = cmd.Parameters.Add("@Scenario", System.Data.SqlDbType.NVarChar)
            Scenario.Value = userName_Scenario
            Dim dr As SqlDataReader = cmd.ExecuteReader

            While dr.Read

                _return.ProjectID = dr("ProjectID")
                _return.TotalPrice = dr("TotalPrice")
                _return.VAT = dr("VAT")
                _return.Currency = dr("Currency")
                _return.CostCode = dr("CostCode")

                If String.IsNullOrEmpty(dr("RequestedBy").ToString) Then
                    _return.RequestedBy = Nothing
                Else
                    _return.RequestedBy = dr("RequestedBy")
                End If

            End While

            con.Close()
            dr.Close()
            con.Dispose()
            Return _return
        End Using

    End Function

    Public Class ScenarioVariables

        Friend ProjectID As Integer
        Friend TotalPrice As Decimal
        Friend VAT As Integer
        Friend Currency As String
        Friend CostCode As String
        Friend RequestedBy As String

    End Class

    Protected Sub AssignAllSessionVaiablesToControls()
        Dim DropDownListPrj As DropDownList = FormViewContract.FindControl("DropDownListPrj")
        Dim TextBoxContractValue_withVAT As TextBox = FormViewContract.FindControl("TextBoxContractValue_withVAT")
        Dim TextBoxVAT As TextBox = FormViewContract.FindControl("TextBoxVAT")
        Dim DropDownListCurrency As DropDownList = FormViewContract.FindControl("DropDownListCurrency")
        Dim TextBoxCostCode As TextBox = FormViewContract.FindControl("TextBoxCostCode")
        Dim TextBoxRequestedBy As TextBox = FormViewContract.FindControl("TextBoxRequestedBy")

        Dim userName As String = Page.User.Identity.Name.ToLower.ToString

        For i = GetCurrentScenario() To GetCurrentScenario()
            If SessionsPoRequest.ScenarioSessionExist("Scenario" + i.ToString) Then

                LiteralTitle.Text = "Scenario " + i.ToString

                DropDownListPrj.SelectedValue = GetVariables(userName + "Scenario" + i.ToString).ProjectID
                DropDownListPrj.Enabled = False
                TextBoxContractValue_withVAT.Text = GetVariables(userName + "Scenario" + i.ToString).TotalPrice
                TextBoxContractValue_withVAT.Enabled = False
                TextBoxVAT.Text = GetVariables(userName + "Scenario" + i.ToString).VAT
                TextBoxVAT.Enabled = False
                DropDownListCurrency.SelectedValue = GetVariables(userName + "Scenario" + i.ToString).Currency
                DropDownListCurrency.Enabled = False
                TextBoxCostCode.Text = GetVariables(userName + "Scenario" + i.ToString).CostCode
                TextBoxCostCode.Enabled = False
                TextBoxRequestedBy.Text = GetVariables(userName + "Scenario" + i.ToString).RequestedBy
                TextBoxRequestedBy.Enabled = False
                Exit For
            End If
        Next

        ' if Frame Contract, relase constraints on additional controls
        Dim RequiredFieldValidatorStartDate As RequiredFieldValidator = FormViewContract.FindControl("RequiredFieldValidatorStartDate")
        Dim RequiredFieldValidatorFinishDate As RequiredFieldValidator = FormViewContract.FindControl("RequiredFieldValidatorFinishDate")
        Dim RequiredFieldValidatorPenalty As RequiredFieldValidator = FormViewContract.FindControl("RequiredFieldValidatorPenalty")
        Dim RequiredFieldValidatorPenaltyToSupplier As RequiredFieldValidator = FormViewContract.FindControl("RequiredFieldValidatorPenaltyToSupplier")
        Dim RequiredFieldValidatorBudget As RequiredFieldValidator = FormViewContract.FindControl("RequiredFieldValidatorBudget")
        Dim RequiredFieldValidatorAdvance As RequiredFieldValidator = FormViewContract.FindControl("RequiredFieldValidatorAdvance")
        Dim RequiredFieldValidatorInterim As RequiredFieldValidator = FormViewContract.FindControl("RequiredFieldValidatorInterim")
        Dim RequiredFieldValidatorShipment As RequiredFieldValidator = FormViewContract.FindControl("RequiredFieldValidatorShipment")
        Dim RequiredFieldValidatorDelivery As RequiredFieldValidator = FormViewContract.FindControl("RequiredFieldValidatorDelivery")
        Dim RequiredFieldValidatorRetention As RequiredFieldValidator = FormViewContract.FindControl("RequiredFieldValidatorRetention")
        Dim RequiredFieldValidatorDeliveryTerms As RequiredFieldValidator = FormViewContract.FindControl("RequiredFieldValidatorDeliveryTerms")
        Dim RequiredFieldValidatorGuaranteePeriod As RequiredFieldValidator = FormViewContract.FindControl("RequiredFieldValidatorGuaranteePeriod")

        If GetCurrentScenario() = -1 Then
            RequiredFieldValidatorStartDate.Enabled = False
            RequiredFieldValidatorFinishDate.Enabled = False
            RequiredFieldValidatorPenalty.Enabled = False
            RequiredFieldValidatorPenaltyToSupplier.Enabled = False
            RequiredFieldValidatorBudget.Enabled = False
            RequiredFieldValidatorAdvance.Enabled = False
            RequiredFieldValidatorInterim.Enabled = False
            RequiredFieldValidatorShipment.Enabled = False
            RequiredFieldValidatorDelivery.Enabled = False
            RequiredFieldValidatorRetention.Enabled = False
            RequiredFieldValidatorDeliveryTerms.Enabled = False
            RequiredFieldValidatorGuaranteePeriod.Enabled = False
        End If

    End Sub

    'Protected Sub ShowOrHideCommercialOfferPanel()

    '    Dim PanelOffer As Panel = FormViewContract.FindControl("PanelOffer")
    '    Dim PanelNominated As Panel = FormViewContract.FindControl("PanelNominated")
    '    Dim userName As String = Page.User.Identity.Name.ToLower.ToString

    '    If GetNominated(Page.User.Identity.Name.ToLower.ToString + "Scenario" + GetCurrentScenario().ToString) And _
    '      GetCurrentScenario() >= 4 Then

    '        PanelNominated.Visible = True
    '    ElseIf Not GetNominated(Page.User.Identity.Name.ToLower.ToString + "Scenario" + GetCurrentScenario().ToString) Then

    '        PanelNominated.Visible = False
    '    End If

    '    If GetCurrentScenario() >= 4 And _
    '      Not GetNominated(Page.User.Identity.Name.ToLower.ToString + "Scenario" + GetCurrentScenario().ToString) Then
    '        PanelOffer.Visible = True
    '        Dim SqlDataSourceOffers As SqlDataSource = FormViewContract.FindControl("SqlDataSourceOffers")
    '        SqlDataSourceOffers.SelectParameters("Scenario").DefaultValue = _
    '          userName + "Scenario" + GetCurrentScenario().ToString
    '    Else
    '        PanelOffer.Visible = False
    '    End If

    'End Sub

    Protected Function GetNominated(ByVal _Scenario As String) As Boolean

        Return False

        'Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        '    con.Open()
        '    Dim sqlstring As String = "SELECT Nominated FROM Table_Scenario WHERE Scenario = @Scenario"
        '    Dim cmd As New SqlCommand(sqlstring, con)
        '    cmd.CommandType =System.Data.CommandType.Text

        '    'syntax for parameter adding
        '    Dim Scenario As SqlParameter = cmd.Parameters.Add("@Scenario",System.Data.SqlDbType.NVarChar)
        '    Scenario.Value = _Scenario
        '    Dim ReturnValue As Boolean
        '    Dim dr As SqlDataReader = cmd.ExecuteReader
        '    While dr.Read
        '        ReturnValue = dr(0)
        '    End While
        '    Return ReturnValue
        '    con.Close()
        '    dr.Close()
        '    con.Dispose()
        'End Using

    End Function

    Protected Function GetFrameContract(ByVal _Scenario As String) As Boolean

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "SELECT FrameContract FROM Table_Scenario WHERE Scenario = @Scenario"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim Scenario As SqlParameter = cmd.Parameters.Add("@Scenario", System.Data.SqlDbType.NVarChar)
            Scenario.Value = _Scenario
            Dim ReturnValue As Boolean
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                ReturnValue = dr(0)
            End While
            Return ReturnValue
            con.Close()
            dr.Close()
            con.Dispose()
        End Using

    End Function


    Protected Sub DropDownListPenalty_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim RequiredFieldValidatorPenaltyNote As RequiredFieldValidator = FormViewContract.FindControl("RequiredFieldValidatorPenaltyNote")
        Dim DDL As DropDownList = sender

        If DDL.SelectedValue = 1 Then ' it is YES
            RequiredFieldValidatorPenaltyNote.Enabled = True
        Else
            RequiredFieldValidatorPenaltyNote.Enabled = False
        End If

    End Sub

    Protected Sub DropDownListPenaltyToSupplier_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim RequiredFieldValidatorPenaltyToSupplierNote As RequiredFieldValidator = FormViewContract.FindControl("RequiredFieldValidatorPenaltyToSupplierNote")
        Dim DDL As DropDownList = sender

        If DDL.SelectedValue = 1 Then ' it is YES
            RequiredFieldValidatorPenaltyToSupplierNote.Enabled = True
        Else
            RequiredFieldValidatorPenaltyToSupplierNote.Enabled = False
        End If

    End Sub

    Protected Sub FormViewSupplier_ItemInserting(sender As Object, e As FormViewInsertEventArgs)

        Dim FormViewSupplier As FormView = FormViewContract.FindControl("FormViewSupplier")
        Dim SupplierIDTextBox As TextBox = FormViewSupplier.FindControl("SupplierIDTextBox")
        Dim LabelSupplierMessage As Label = FormViewSupplier.FindControl("LabelSupplierMessage")
        Dim sqlsupplier As SqlDataSource = FormViewContract.FindControl("SqlDataSourceSupplierEnter")

        Using Adapter As New MercuryTableAdapters.Table6_SupplierTableAdapter
            If Adapter.GetCountBySupplierID(SupplierIDTextBox.Text) > 0 Then
                ' give message that it is dublicating
                LabelSupplierMessage.Visible = True
                e.Cancel = True
            End If

            Adapter.Dispose()

        End Using

    End Sub

    Protected Sub CheckBoxPersonSupplier_CheckedChanged(sender As Object, e As EventArgs)

        Dim FormViewSupplier As FormView = FormViewContract.FindControl("FormViewSupplier")
        Dim RegularExpressionValidatorSupplierEnter As RegularExpressionValidator = FormViewSupplier.FindControl("RegularExpressionValidatorSupplierEnter")

        Dim cbx As CheckBox = sender

        If cbx.Checked Then
            RegularExpressionValidatorSupplierEnter.ValidationExpression = "\d{12}"
            RegularExpressionValidatorSupplierEnter.ErrorMessage = "INN number to be 12 digit!"
        Else
            RegularExpressionValidatorSupplierEnter.ValidationExpression = "\d{10}"
            RegularExpressionValidatorSupplierEnter.ErrorMessage = "INN number to be 10 digit!"
        End If

        RegularExpressionValidatorSupplierEnter.Validate()

    End Sub

    Protected Sub InsertButton_Click1(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        Dim sqlsupplier As SqlDataSource = FormViewContract.FindControl("SqlDataSourceSupplierEnter")
        sqlsupplier.InsertParameters("CreatedBy").DefaultValue = result.ToString
        sqlsupplier.InsertParameters("CreatedBy").Type = TypeCode.DateTime

        sqlsupplier.InsertParameters("PersonCreated").DefaultValue = Page.User.Identity.Name.ToString
        sqlsupplier.InsertParameters("PersonCreated").Type = TypeCode.String

    End Sub

    Protected Sub ButtonBudgetPDFUpload_Click(sender As Object, e As EventArgs)

        Dim FileToUpload As FileUpload = FormViewContract.FindControl("FileUploadBudgetPDF")
        Dim DropProject As DropDownList = FormViewContract.FindControl("DropDownListPrj")
        'Dim TxtInvoiceID As TextBox = FormViewContract.FindControl("InvoiceIDTextBox")
        Dim LabelInfo As Label = FormViewContract.FindControl("LabelInfoBudgetPDF")
        Dim TextLink As TextBox = FormViewContract.FindControl("TextBoxBudgetPDF")

        If DropProject.SelectedItem.ToString = "Select Project" Then
            LabelInfo.ForeColor = System.Drawing.Color.Red
            LabelInfo.Text = "Please select Project Name"
            TextLink.Text = ""
        Else
            If FileToUpload.HasFile Then
                If System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".pdf" _
                  OrElse System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".doc" _
                  OrElse System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".docx" Then
                    'If FileToUpload.PostedFile.ContentLength / 1000 > 5000 Then
                    '    LabelInfo.ForeColor = System.Drawing.Color.Red
                    '    LabelInfo.Text = "PDF file size must be less than 5MB"
                    '    TextLink.Text = ""
                    'Else
                    If Directory.Exists(Server.MapPath("~/CONTRACT/") + DropProject.SelectedItem.ToString + "/") Then
                        Dim UniqueString11 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                        Dim UniqueString12 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                        FileToUpload.SaveAs(MapPath("~/CONTRACT/" + DropProject.SelectedItem.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)))
                        TextLink.Text = "~/CONTRACT/" + DropProject.SelectedItem.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)
                        LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
                        LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
                    Else
                        Dim UniqueString21 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                        Dim UniqueString22 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                        Directory.CreateDirectory(Server.MapPath("~/CONTRACT/") + DropProject.SelectedItem.ToString)
                        FileToUpload.SaveAs(MapPath("~/CONTRACT/" + DropProject.SelectedItem.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)))
                        TextLink.Text = "~/CONTRACT/" + DropProject.SelectedItem.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)
                        LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
                        LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
                    End If
                    'End If
                Else
                    LabelInfo.ForeColor = System.Drawing.Color.Red
                    LabelInfo.Text = "Please select PDF, DOC or DOCX format file"
                    TextLink.Text = ""
                End If
            Else
                TextLink.Text = ""
                LabelInfo.ForeColor = System.Drawing.Color.Red
                LabelInfo.Text = "you did not specify any file"
            End If
        End If

    End Sub

    Private Sub FormViewContract_ItemInserted(sender As Object, e As FormViewInsertedEventArgs) Handles FormViewContract.ItemInserted



    End Sub
End Class

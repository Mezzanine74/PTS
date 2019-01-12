Imports System
Imports System.IO
Imports System.Net
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports PTS_App_Code_VB_Project.PTS.CoreTables

Partial Class _addendumenter_2REV___
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim ButtonSubmit As LinkButton = FormViewAddendums.FindControl("InsertButton")
        PreventMultipleClick.PreventMultipleClicks(ButtonSubmit)

        If Not IsPostBack Then
            If Not PreviousPage Is Nothing Then
                Dim ContPlaceHold As ContentPlaceHolder = PreviousPage.Master.FindControl("MainContent")
                Dim LabelContractID As Label = ContPlaceHold.FindControl("LabelContractID")
                LabelContractIDonAddendum.Text = LabelContractID.Text
            End If
        End If

        If Not (Roles.IsUserInRole("InitiateContractAndAddendum") Or _
                ApprovalMatrixCreateDataReader.GetCountByUserRoleProjectID(Page.User.Identity.Name.ToLower, "LawyerOnSite", PTS.CoreTables.CreateDataReader.Create_Table_Contract(LabelContractIDonAddendum.Text).ProjectID) = 1 Or _
                (PTS.CoreTables.CreateDataReader.Create_Table_Contract(LabelContractIDonAddendum.Text).ProjectID = 175 And User.IsInRole("ContractLeadGirls") = True)) Then
            ' user not to allow
            If Not User.IsInRole("ContractLeadGirls") = True Then
                Response.Redirect("~/webforms/AccessDenied.aspx")
            End If
        End If

        ' it transfer previous page onto here...
        If Not IsPostBack Then
            If Not PreviousPage Is Nothing Then
                Dim previousPageURL As String = Request.UrlReferrer.AbsolutePath.ToString
                Dim PageFileInfo As New System.IO.FileInfo(previousPageURL)
                If PageFileInfo.Name.ToString.ToLower = "contractview.aspx" Then
                    Dim ContPlaceHold As ContentPlaceHolder = PreviousPage.Master.FindControl("MainContent")
                    Dim LabelProjectName As Label = ContPlaceHold.FindControl("LabelProjectName")
                    Dim LabelPOno As Label = ContPlaceHold.FindControl("LabelPOno")
                    Dim LabelSupplierName As Label = ContPlaceHold.FindControl("LabelSupplierNameV")
                    Dim LabelContractNo As Label = ContPlaceHold.FindControl("LabelContractNo")
                    Dim LabelContractDate As Label = ContPlaceHold.FindControl("LabelContractDate")
                    Dim LabelContractValue As Label = ContPlaceHold.FindControl("LabelContractValue")
                    Dim LabelContractValueWithVAT As Label = ContPlaceHold.FindControl("LabelContractValueWithVAT")
                    Dim LabelContractCurrency As Label = ContPlaceHold.FindControl("LabelContractCurrencyV")
                    Dim LabelContractType As Label = ContPlaceHold.FindControl("LabelContractType")
                    Dim LabelContractDescription As Label = ContPlaceHold.FindControl("LabelContractDescription")
                    Dim LabelContractID As Label = ContPlaceHold.FindControl("LabelContractID")
                    Dim LabelGridViewPagingStatusOnContractView As Label = ContPlaceHold.FindControl("LabelGridViewPagingStatus")
                    Dim LabelGridViewPageSizeOnContractView As Label = ContPlaceHold.FindControl("LabelGridViewPageSize")
                    Dim LabelGridViewPageNumberOnContractView As Label = ContPlaceHold.FindControl("LabelGridViewPageNumber")

                    Dim LabelProjectNameInFormView As Label = FormViewAddendums.FindControl("LabelProjectName")
                    Dim LabelPOnoInFormView As Label = FormViewAddendums.FindControl("LabelPOno")
                    Dim LabelSupplierNameInFormView As Label = FormViewAddendums.FindControl("LabelSupplierName")
                    Dim LabelContractNameInFormView As Label = FormViewAddendums.FindControl("LabelContractName")
                    Dim LabelContractDateInFormView As Label = FormViewAddendums.FindControl("LabelContractDate")
                    Dim LabelContractValueInFormView As Label = FormViewAddendums.FindControl("LabelContractValue")
                    Dim LabelCurrencyInFormView As Label = FormViewAddendums.FindControl("LabelCurrency")
                    Dim LabelContractTypeInFormView As Label = FormViewAddendums.FindControl("LabelContractType")
                    Dim LabelContractDescriptionInFormView As Label = FormViewAddendums.FindControl("LabelContractDescription")

                    LabelProjectNameInFormView.Text = LabelProjectName.Text
                    LabelPOnoInFormView.Text = LabelPOno.Text
                    LabelSupplierNameInFormView.Text = LabelSupplierName.Text
                    LabelContractNameInFormView.Text = LabelContractNo.Text
                    LabelContractDateInFormView.Text = LabelContractDate.Text
                    LabelContractValueInFormView.Text = LabelContractValueWithVAT.Text
                    LabelCurrencyInFormView.Text = LabelContractCurrency.Text
                    LabelContractTypeInFormView.Text = LabelContractType.Text
                    LabelContractDescriptionInFormView.Text = LabelContractDescription.Text
                    LabelContractIDonAddendum.Text = LabelContractID.Text
                    LabelGridViewPagingStatusOnAddendum.Text = LabelGridViewPagingStatusOnContractView.Text
                    LabelGridViewPageSizeOnAddendum.Text = LabelGridViewPageSizeOnContractView.Text
                    LabelGridViewPageNumberOnAddendum.Text = LabelGridViewPageNumberOnContractView.Text

                    ' if it is from Frame Contract, Assing DDL Adddendum Type to "Regular Addendum" and freeze DDL
                    If CreateDataReader.Create_Table_Contract(LabelContractIDonAddendum.Text).FrameContract = True Then
                        Dim DropDownListAddendumType As DropDownList = FormViewAddendums.FindControl("DropDownListAddendumType")
                        ' THIS SECTION DEACTIVATED AFTER ENABLING ZERO VALUE ADDENDUM to frame contracts
                        'DropDownListAddendumType.SelectedIndex = 1
                        'DropDownListAddendumType.Enabled = False
                        ' execute Selected Index Change event externally
                        'DropDownListAddendumType_SelectedIndexChanged(DropDownListAddendumType, Nothing)
                        ' //////// THIS SECTION DEACTIVATED AFTER ENABLING ZERO VALUE ADDENDUM to frame contracts

                        ' it removes REPLACE ADDENDUM
                        DropDownListAddendumType.Items.RemoveAt(2)

                        ' Cost Code controls to be activated only for Addendums to Frame Contract
                        Dim SqlDataSourceCostCode As SqlDataSource = FormViewAddendums.FindControl("SqlDataSourceCostCode")
                        SqlDataSourceCostCode.SelectParameters("ProjectID").DefaultValue = GetProjectIDFromContractID(LabelContractIDonAddendum.Text)
                        SqlDataSourceCostCode.SelectParameters("UserName").DefaultValue = Page.User.Identity.Name.ToLower

                        Dim DropDownListCostCode As DropDownList = FormViewAddendums.FindControl("DropDownListCostCode")
                        Dim CompareValidatorCostCode As CompareValidator = FormViewAddendums.FindControl("CompareValidatorCostCode")
                        Dim CompareValidatorCostCodeBudget As CompareValidator = FormViewAddendums.FindControl("CompareValidatorCostCodeBudget")

                        DropDownListCostCode.Visible = True
                        CompareValidatorCostCode.Visible = True
                        CompareValidatorCostCodeBudget.Visible = True

                        DropDownListCostCode_SelectedIndexChanged(DropDownListCostCode, Nothing)

                    End If

                End If
            End If
        End If

        Dim SqlDataSourceRequestedBy As SqlDataSource = FormViewAddendums.FindControl("SqlDataSourceRequestedBy")
        SqlDataSourceRequestedBy.SelectParameters("ProjectID").DefaultValue = CreateDataReader.Create_Table_Contract(Convert.ToInt32(LabelContractIDonAddendum.Text)).ProjectID

        Dim TextBoxBudget As TextBox = FormViewAddendums.FindControl("TextBoxBudget")
        If TextBoxBudget IsNot Nothing Then
            TextBoxBudget.Text = 0.1
        End If

    End Sub

    Protected Function GetProjectIDFromContractID(ByVal _ContractID As Integer) As Integer
        Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        con.Open()
        Dim sqlstring As String = " SELECT ProjectID FROM Table_Contracts WHERE ContractID = @ContractID "
        Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text

        'syntax for parameter adding
        Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", System.Data.SqlDbType.Int)
        ContractID.Value = _ContractID
        Dim _return As Integer = 0
        Dim dr As SqlDataReader = cmd.ExecuteReader
        While dr.Read
            _return = dr(0)
        End While

        Return _return

        con.Close()
        dr.Close()
    End Function

    Protected Sub FormViewAddendums_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertedEventArgs) Handles FormViewAddendums.ItemInserted

        Dim oooo As New MyCommonTasks
        oooo.SendEmailToAdmin("inserted", "inserted", Nothing, True)

        ' send notification
        Dim SignByMercuryCheckBox As CheckBox = FormViewAddendums.FindControl("SignByMercuryCheckBox")
        Dim LinkToPDFcopyTextBox As TextBox = FormViewAddendums.FindControl("LinkToPDFcopyTextBox")
        Dim LabelProjectName As Label = FormViewAddendums.FindControl("LabelProjectName")
        Dim ContractValue_woVATTextBox As TextBox = FormViewAddendums.FindControl("ContractValue_woVATTextBox")
        Dim LabelShowSupplier As Label = FormViewAddendums.FindControl("LabelSupplierName")
        Dim ContractNoTextBox As TextBox = FormViewAddendums.FindControl("ContractNoTextBox")
        Dim LabelContractName As Label = FormViewAddendums.FindControl("LabelContractName")
        Dim LabelCurrency As Label = FormViewAddendums.FindControl("LabelCurrency")

        Dim Notification As String = ""
        Dim SendNotification As New MyCommonTasks

        Dim contractValue As String = ContractValue_woVATTextBox.Text
        If contractValue = "" Then
            contractValue = "0.0"
        End If

        Dim DetailedInfo As String = "| Project: " + LabelProjectName.Text + _
      " |SupplierName= " + GetSupplierName(LabelContractIDonAddendum.Text) + _
    " |AddendumNo= " + ContractNoTextBox.Text + _
    " |ContractNo= " + LabelContractName.Text + _
    " |AddendumValue= " + String.Format("{0:#,##0.00}", Convert.ToDecimal(contractValue)) + _
    LabelCurrency.Text.ToString

        If SignByMercuryCheckBox.Checked = False AndAlso Len(LinkToPDFcopyTextBox.Text) = 0 Then
            'New Addendum> Not Signed> No Attachment
            Notification = "New Addendum> Not Signed> No Attachment" + DetailedInfo
            SendNotification.SendNotification(Notification, GetProjectID(LabelProjectName.Text), 3)
            SendNotification.SendEmailForContract(GetProjectID(LabelProjectName.Text), Notification)
        ElseIf SignByMercuryCheckBox.Checked = True AndAlso Len(LinkToPDFcopyTextBox.Text) = 0 Then
            'New Addendum> Signed> No Attachment
            Notification = "New Addendum> Signed> No Attachment" + DetailedInfo
            SendNotification.SendNotification(Notification, GetProjectID(LabelProjectName.Text), 3)
            SendNotification.SendEmailForContract(GetProjectID(LabelProjectName.Text), Notification)
        ElseIf SignByMercuryCheckBox.Checked = True AndAlso Len(LinkToPDFcopyTextBox.Text) > 1 Then
            'New Addendum> Signed> Attachment added
            Notification = "New Addendum> Signed> " + "<a href=" + """" + Replace(LinkToPDFcopyTextBox.Text, "~", "") + """" + " target=" + """" + "_blank" + """" + ">Attachment</a>" + " added" + DetailedInfo
            SendNotification.SendNotification(Notification, GetProjectID(LabelProjectName.Text), 3)
            SendNotification.SendEmailForContract(GetProjectID(LabelProjectName.Text), Notification)
        ElseIf SignByMercuryCheckBox.Checked = False AndAlso Len(LinkToPDFcopyTextBox.Text) > 1 Then
            'New Addendum> Not Signed> Attachment added
            Notification = "New Addendum> Not Signed> " + "<a href=" + """" + Replace(LinkToPDFcopyTextBox.Text, "~", "") + """" + " target=" + """" + "_blank" + """" + ">Attachment</a>" + " added" + DetailedInfo
            SendNotification.SendNotification(Notification, GetProjectID(LabelProjectName.Text), 3)
            SendNotification.SendEmailForContract(GetProjectID(LabelProjectName.Text), Notification)
        End If

    End Sub

    Protected Sub FormViewAddendums_ItemInserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertEventArgs) Handles FormViewAddendums.ItemInserting

        Dim ContractValue_woVATTextBox As TextBox = FormViewAddendums.FindControl("ContractValue_woVATTextBox")
        Dim TextBoxAddendumValue_withVAT As TextBox = FormViewAddendums.FindControl("TextBoxAddendumValue_withVAT")
        Dim TextBoxVAT As TextBox = FormViewAddendums.FindControl("TextBoxVAT")
        Dim ContractDateTextBox As TextBox = FormViewAddendums.FindControl("ContractDateTextBox")
        Dim DropDownListCostCode As DropDownList = FormViewAddendums.FindControl("DropDownListCostCode")
        Dim TextBoxRetention As TextBox = FormViewAddendums.FindControl("TextBoxRetention")
        Dim TextBoxBudget As TextBox = FormViewAddendums.FindControl("TextBoxBudget")
        Dim TextBoxAdvance As TextBox = FormViewAddendums.FindControl("TextBoxAdvance")
        Dim TextBoxInterim As TextBox = FormViewAddendums.FindControl("TextBoxInterim")
        Dim TextBoxShipment As TextBox = FormViewAddendums.FindControl("TextBoxShipment")
        Dim TextBoxDelivery As TextBox = FormViewAddendums.FindControl("TextBoxDelivery")
        Dim LabelPaymentTermsValidationNotification As Label = FormViewAddendums.FindControl("LabelPaymentTermsValidationNotification")

        If ContractView.PaymentTermsValidated(TextBoxAdvance.Text, TextBoxInterim.Text, TextBoxShipment.Text, _
                                 TextBoxDelivery.Text, TextBoxRetention.Text) = False Then

            e.Cancel = True
            LabelPaymentTermsValidationNotification.Visible = True
            Exit Sub
        End If

        If CreateDataReader.Create_Table_Contract(LabelContractIDonAddendum.Text).FrameContract = True Then
            ' Cost Code make sense. Take it to database for addendum to Frame Contract
            e.Values("CostCode") = DropDownListCostCode.SelectedValue.ToString
        End If

        ' update AttachmentExist column
        Dim TextLink As TextBox = FormViewAddendums.FindControl("LinkToPDFcopyTextBox")
        If Len(TextLink.Text) > 0 OrElse Not String.IsNullOrEmpty(TextLink.Text) Then
            e.Values("AttachmentExist") = True
        Else
            e.Values("AttachmentExist") = False
        End If

        If ContractValue_woVATTextBox.Text <> "" Then
            e.Values("AddendumValue_woVAT") = Convert.ToDecimal(ContractValue_woVATTextBox.Text)
        ElseIf ContractValue_woVATTextBox.Text = "" Then
            e.Values("AddendumValue_woVAT") = Nothing
        End If

        If TextBoxAddendumValue_withVAT.Text <> "" Then
            e.Values("AddendumValue_WithVAT") = Convert.ToDecimal(TextBoxAddendumValue_withVAT.Text)
        ElseIf TextBoxAddendumValue_withVAT.Text = "" Then
            e.Values("AddendumValue_WithVAT") = Nothing
        End If

        If TextBoxVAT.Text <> "" Then
            e.Values("VATpercent") = Convert.ToDecimal(TextBoxVAT.Text)
        ElseIf TextBoxVAT.Text = "" Then
            e.Values("VATpercent") = Nothing
        End If

        If ContractDateTextBox.Text <> "" Then
            e.Values("AddendumDate") = Convert.ToDateTime(Mid(ContractDateTextBox.Text.ToString, 1, 2).ToString + "/" + Mid(ContractDateTextBox.Text.ToString, 4, 2).ToString + "/" + Mid(ContractDateTextBox.Text.ToString, 7, 4).ToString)
        ElseIf ContractDateTextBox.Text = "" Then
            e.Values("AddendumDate") = Nothing
        End If

        e.Values("ContractID") = Convert.ToInt32(LabelContractIDonAddendum.Text)

        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        e.Values("CreatedBy") = result
        e.Values("PersonCreated") = Page.User.Identity.Name.ToString

        ' Commercial items
        Dim TextBoxRequestedBy As TextBox = FormViewAddendums.FindControl("TextBoxRequestedBy")
        Dim DropDownListPenalty As DropDownList = FormViewAddendums.FindControl("DropDownListPenalty")
        Dim TextBoxPenaltyNote As TextBox = FormViewAddendums.FindControl("TextBoxPenaltyNote")
        Dim DropDownListPenaltyToSupplier As DropDownList = FormViewAddendums.FindControl("DropDownListPenaltyToSupplier")
        Dim TextBoxPenaltyNoteToSupplier As TextBox = FormViewAddendums.FindControl("TextBoxPenaltyNoteToSupplier")
        Dim TextBoxStartDate As TextBox = FormViewAddendums.FindControl("TextBoxStartDate")
        Dim TextBoxFinishDate As TextBox = FormViewAddendums.FindControl("TextBoxFinishDate")
        Dim DropDownListAddendumType As DropDownList = FormViewAddendums.FindControl("DropDownListAddendumType")
        Dim DropDownListRequestedBy As DropDownList = FormViewAddendums.FindControl("DropDownListRequestedBy")
        Dim LabelCurrency As Label = FormViewAddendums.FindControl("LabelCurrency")

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

        If String.IsNullOrEmpty(DropDownListPenalty.SelectedValue.ToString) Then
            e.Values("Penalties") = Nothing
        Else
            e.Values("Penalties") = DropDownListPenalty.SelectedValue
        End If

        If String.IsNullOrEmpty(TextBoxPenaltyNote.Text) Then
            e.Values("PenaltiesNote") = Nothing
        Else
            e.Values("PenaltiesNote") = TextBoxPenaltyNote.Text
        End If

        If String.IsNullOrEmpty(DropDownListPenaltyToSupplier.SelectedValue.ToString) Then
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
            e.Values("StartDate") = Convert.ToDateTime(Mid(TextBoxStartDate.Text.ToString, 1, 2).ToString + _
                                     "/" + Mid(TextBoxStartDate.Text.ToString, 4, 2).ToString + _
                                     "/" + Mid(TextBoxStartDate.Text.ToString, 7, 4).ToString)
        End If

        If String.IsNullOrEmpty(TextBoxStartDate.Text) Then
            e.Values("FinishDate") = Nothing
        Else
            e.Values("FinishDate") = Convert.ToDateTime(Mid(TextBoxFinishDate.Text.ToString, 1, 2).ToString + _
                               "/" + Mid(TextBoxFinishDate.Text.ToString, 4, 2).ToString + _
                               "/" + Mid(TextBoxFinishDate.Text.ToString, 7, 4).ToString)
        End If

        If String.IsNullOrEmpty(TextBoxRetention.Text) Then
            e.Values("AddendumRetention") = Nothing
        Else
            e.Values("AddendumRetention") = TextBoxRetention.Text
        End If

        e.Values("AddendumTypes") = DropDownListAddendumType.SelectedValue

        If DropDownListRequestedBy.SelectedValue.ToString = "0" Then
            e.Values("RequestedBy") = Nothing
        Else
            e.Values("RequestedBy") = DropDownListRequestedBy.SelectedValue.ToString
        End If

        e.Values("Scenario") = CalculateScenario.Calculate( _
          Convert.ToDecimal(TextBoxAddendumValue_withVAT.Text), _
          Convert.ToDecimal(TextBoxVAT.Text), _
          LabelCurrency.Text)
        ' _______________________________________________________________ END OF Commercial Items

        ' validation for budget exceeding
        Dim _newvalue As Decimal = 0.0

        _newvalue = Math.Round(IIf(IsNumeric(TextBoxAddendumValue_withVAT.Text), TextBoxAddendumValue_withVAT.Text, 0) / ((100 + (IIf(IsNumeric(TextBoxVAT.Text), TextBoxVAT.Text, 0))) / 100), 2)
        Dim _costcode As String = ""
        Dim _projectid As Integer = 0

        If CreateDataReader.Create_Table_Contract(LabelContractIDonAddendum.Text).FrameContract = True Then
            ' Cost Code make sense. Take it to database for addendum to Frame Contract
            _costcode = DropDownListCostCode.SelectedValue.ToString
        Else
            Try
                _costcode = PTS_MERCURY.helper.Table_Contracts.GetRowByPrimaryKey(Convert.ToInt32(LabelContractIDonAddendum.Text)).CostCode.Trim()
            Catch ex As Exception

            End Try
        End If

        _projectid = PTS_MERCURY.helper.Table_Contracts.GetRowByPrimaryKey(Convert.ToInt32(LabelContractIDonAddendum.Text)).ProjectID

        'If PTS_MERCURY.helper.Table_Budget.GetBudgetEmailControlFailed(Page, _
        '                                                               _projectid, _
        '                                                               _costcode, _
        '                                                               0, _
        '                                                               _newvalue) = _
        '                                                           True AndAlso DropDownListAddendumType.SelectedValue <> 3 Then

        If DropDownListAddendumType.SelectedValue <> 3 Then
            If PTS_MERCURY.helper.Table_Budget.GetBudgetEmailControlFailed(Page, _
                                                                   _projectid, _
                                                                   _costcode, _
                                                                   0, _
                                                                   _newvalue) = _
                                                               True Then

                ' error messages coming from PTS_MERCURY.helper.Table_Budget.GetBudgetEmailControlFailed

                e.Cancel = True
                Exit Sub

            End If
        End If

    End Sub

    Protected Sub ButtonUploadDOC_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim LabelProjectName As Label = FormViewAddendums.FindControl("LabelProjectName")
        Dim TextLink As TextBox = FormViewAddendums.FindControl("LinkToTemplatefile_DOCTextBox")

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
            Dim _directory As String = "~/CONTRACT/" + LabelProjectName.Text + "/" + UniqueString21 + UniqueString22
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

        'Dim FileToUpload As FileUpload = FormViewAddendums.FindControl("FileUploadDOC")
        'Dim LabelProjectName As Label = FormViewAddendums.FindControl("LabelProjectName")
        'Dim LabelInfo As Label = FormViewAddendums.FindControl("LabelInfoDOC")
        'Dim TextLink As TextBox = FormViewAddendums.FindControl("LinkToTemplatefile_DOCTextBox")

        'If LabelProjectName.Text.ToString = "Select Project" Then
        '    LabelInfo.ForeColor = System.Drawing.Color.Red
        '    LabelInfo.Text = "Please select Project Name"
        '    TextLink.Text = ""
        'Else
        '    If FileToUpload.HasFile Then
        '        If System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".doc" _
        '            OrElse System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".docx" _
        '            OrElse System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".pdf" _
        '            OrElse System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".zip" Then
        '            'If FileToUpload.PostedFile.ContentLength / 1000 > 50000 Then
        '            '    LabelInfo.ForeColor = System.Drawing.Color.Red
        '            '    LabelInfo.Text = "Attached file size must be less than 50MB"
        '            '    TextLink.Text = ""
        '            'Else
        '            If Directory.Exists(Server.MapPath("~/CONTRACT/") + LabelProjectName.Text.ToString + "/") Then
        '                Dim UniqueString11 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
        '                Dim UniqueString12 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
        '                FileToUpload.SaveAs(MapPath("~/CONTRACT/" + LabelProjectName.Text.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)))
        '                TextLink.Text = "~/CONTRACT/" + LabelProjectName.Text.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)
        '                LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
        '                LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
        '            Else
        '                Dim UniqueString21 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
        '                Dim UniqueString22 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
        '                Directory.CreateDirectory(Server.MapPath("~/CONTRACT/") + LabelProjectName.Text.ToString)
        '                FileToUpload.SaveAs(MapPath("~/CONTRACT/" + LabelProjectName.Text.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)))
        '                TextLink.Text = "~/CONTRACT/" + LabelProjectName.Text.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)
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
        Dim FileToUpload As FileUpload = FormViewAddendums.FindControl("FileUploadPDF")
        Dim LabelProjectName As Label = FormViewAddendums.FindControl("LabelProjectName")
        Dim LabelInfo As Label = FormViewAddendums.FindControl("LabelInfoPDF")
        Dim TextLink As TextBox = FormViewAddendums.FindControl("LinkToPDFcopyTextBox")

        If LabelProjectName.Text.ToString = "Select Project" Then
            LabelInfo.ForeColor = System.Drawing.Color.Red
            LabelInfo.Text = "Please select Project Name"
            TextLink.Text = ""
        Else
            If FileToUpload.HasFile Then
                If System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".pdf" _
                  OrElse System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".doc" _
                  OrElse System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".docx" Then
                    If FileToUpload.PostedFile.ContentLength / 1000 > 5000 Then
                        LabelInfo.ForeColor = System.Drawing.Color.Red
                        LabelInfo.Text = "PDF file size must be less than 5MB"
                        TextLink.Text = ""
                    Else
                        If Directory.Exists(Server.MapPath("~/CONTRACT/") + LabelProjectName.Text.ToString + "/") Then
                            Dim UniqueString11 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                            Dim UniqueString12 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                            FileToUpload.SaveAs(MapPath("~/CONTRACT/" + LabelProjectName.Text.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)))
                            TextLink.Text = "~/CONTRACT/" + LabelProjectName.Text.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)
                            LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
                            LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
                        Else
                            Dim UniqueString21 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                            Dim UniqueString22 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                            Directory.CreateDirectory(Server.MapPath("~/CONTRACT/") + LabelProjectName.Text.ToString)
                            FileToUpload.SaveAs(MapPath("~/CONTRACT/" + LabelProjectName.Text.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)))
                            TextLink.Text = "~/CONTRACT/" + LabelProjectName.Text.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)
                            LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
                            LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
                        End If
                    End If
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

    Protected Sub FormViewContract_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormViewAddendums.Load
        If IsPostBack Or Not IsPostBack Then
            ' it cause button to postback. 
            Dim ButtonUploadDOC As Button = FormViewAddendums.FindControl("ButtonUploadDOC")
            Dim ButtonUploadPDF As Button = FormViewAddendums.FindControl("ButtonUploadPDF")
            Dim DropDownListAddendumType As DropDownList = FormViewAddendums.FindControl("DropDownListAddendumType")


        End If
    End Sub

    Protected Function GetProjectID(ByVal ProjectName As String) As String
        Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        con.Open()
        Dim sqlstring As String = "SELECT     ProjectID FROM  dbo.Table1_Project WHERE  (ProjectName = N'" + ProjectName + "')"
        Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text
        Dim dr As SqlDataReader = cmd.ExecuteReader
        Dim ProjectID As String = ""
        While dr.Read
            ProjectID = dr(0).ToString
        End While
        con.Close()
        dr.Close()
        Return ProjectID
    End Function

    Protected Function GetSupplierName(ByVal ContractID As String) As String
        Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        con.Open()
        Dim sqlstring As String = " SELECT     RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName " +
    " FROM         dbo.Table6_Supplier INNER JOIN " +
    "                       dbo.Table_Contracts ON dbo.Table6_Supplier.SupplierID = dbo.Table_Contracts.SupplierID " +
    " WHERE     (dbo.Table_Contracts.ContractID = " + ContractID + ") "

        Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text
        Dim dr As SqlDataReader = cmd.ExecuteReader
        Dim SupplierName As String = ""
        While dr.Read
            SupplierName = dr(0).ToString
        End While
        con.Close()
        dr.Close()
        Return SupplierName
    End Function

    Protected Sub DropDownListCostCode_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Dim DropDownListCostCode As DropDownList = sender
        Dim CompareValidatorCostCode As CompareValidator = FormViewAddendums.FindControl("CompareValidatorCostCode")
        Dim TextBoxCostCodeError As TextBox = FormViewAddendums.FindControl("TextBoxCostCodeError")

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain

            con.Open()
            Dim sqlstring As String = " SELECT     RTRIM(Type) AS Type" +
                                                    " FROM Table1_Project" +
                                                    " WHERE     ProjectID =" + GetProjectIDFromContractID(LabelContractIDonAddendum.Text).ToString
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                Dim ProjectType As String
                ProjectType = dr(0).ToString()
                If ProjectType = "DataCenter" Then
                    If DropDownListCostCode.SelectedValue.ToString = "0" Then
                        TextBoxCostCodeError.Text = "Valid"
                    ElseIf Len(DropDownListCostCode.SelectedValue.ToString) = 10 Then
                        TextBoxCostCodeError.Text = "Valid"
                    ElseIf Len(DropDownListCostCode.SelectedValue.ToString) = 1 AndAlso Not IsNumeric(DropDownListCostCode.SelectedValue.ToString) Then
                        TextBoxCostCodeError.Text = "NotValid"
                        CompareValidatorCostCode.Validate()
                    ElseIf Len(DropDownListCostCode.SelectedValue.ToString) > 1 AndAlso Len(DropDownListCostCode.SelectedValue.ToString) < 10 Then
                        TextBoxCostCodeError.Text = "NotValid"
                        CompareValidatorCostCode.Validate()
                    End If
                Else
                    TextBoxCostCodeError.Text = "Valid"
                End If
            End While
            con.Close()
            dr.Close()
        End Using

    End Sub

    Protected Sub SqlDataSourceAddendums_Inserted(sender As Object, e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourceAddendums.Inserted

        Dim AddendumID As Integer = e.Command.Parameters("@id").Value
        ' CHECK ADDENDUM IF IT IS READY FOR PO
        ' AddendumReadyToTurnPO
        'AND
        '(Scenario = 0 AND AddendumType = 3) OR ((Scenario > 0 AND AddendumType > 0 AND AddendumType < 3))
        'AND
        ' POexecuted = False
        If ContractView.AddendumReadyToTurnPo(AddendumID) _
              And
              (
                  (ContractView.ScenarioNoForThisAddendum(AddendumID) = 0 _
                   And CreateDataReader.Create_Table_Addendums(AddendumID).AddendumTypes = 3) _
                  Or
                  (ContractView.ScenarioNoForThisAddendum(AddendumID) > 0 _
                   And CreateDataReader.Create_Table_Addendums(AddendumID).AddendumTypes > 0 _
                   And CreateDataReader.Create_Table_Addendums(AddendumID).AddendumTypes < 3)
               ) _
            And ContractView.GetPoExecutionFromAddendum(AddendumID) = False Then

            ' Then execute InsertOrUpdatePoFromContract
            ContractView.InsertOrUpdatePoFromContract(
              GetProjectIDFromContractID(ContractView.GetContractIDfromAddendumID(AddendumID)),
              ContractView.GetContractIDfromAddendumID(AddendumID),
              AddendumID,
              Page.User.Identity.Name.ToLower,
              POdetailsForEmail)

        End If
        ' ......./ END OF CHECK ADDENDUM IF IT IS READY FOR PO

        ' Send Email to required approval person
        ' Parameter 2 for Addendum Enter
        SendEmailApprovalMatrix.Send(AddendumID, 2, Nothing, Nothing, ContractView.ProduceHTMLforAddendumEmailBody(WebUserControl_AddendumEmailBody, AddendumID))

        Response.Redirect("contractview.aspx", False)

    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Exit Sub
    End Sub

    Protected Sub DropDownListRequestedBy_DataBound(sender As Object, e As System.EventArgs)
        Dim DropDownListRequestedBy As DropDownList = sender
        Dim lst2 As New ListItem("Select Person", "0")
        DropDownListRequestedBy.Items.Insert(0, lst2)
    End Sub

    Protected Sub DropDownListAddendumType_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Dim RequiredFieldValidatorStartDate As RequiredFieldValidator = FormViewAddendums.FindControl("RequiredFieldValidatorStartDate")
        Dim RequiredFieldValidatorFinishDate As RequiredFieldValidator = FormViewAddendums.FindControl("RequiredFieldValidatorFinishDate")
        Dim RequiredFieldValidatorPenalty As RequiredFieldValidator = FormViewAddendums.FindControl("RequiredFieldValidatorPenalty")
        Dim RequiredFieldValidatorPenaltyToSupplier As RequiredFieldValidator = FormViewAddendums.FindControl("RequiredFieldValidatorPenaltyToSupplier")
        Dim RequiredFieldValidatorBudget As RequiredFieldValidator = FormViewAddendums.FindControl("RequiredFieldValidatorBudget")
        Dim RequiredFieldValidatorAdvance As RequiredFieldValidator = FormViewAddendums.FindControl("RequiredFieldValidatorAdvance")
        Dim RequiredFieldValidatorInterim As RequiredFieldValidator = FormViewAddendums.FindControl("RequiredFieldValidatorInterim")
        Dim RequiredFieldValidatorShipment As RequiredFieldValidator = FormViewAddendums.FindControl("RequiredFieldValidatorShipment")
        Dim RequiredFieldValidatorDelivery As RequiredFieldValidator = FormViewAddendums.FindControl("RequiredFieldValidatorDelivery")
        Dim RequiredFieldValidatorAddendumRetention As RequiredFieldValidator = FormViewAddendums.FindControl("RequiredFieldValidatorAddendumRetention")
        Dim RequiredFieldValidatorDeliveryTerms As RequiredFieldValidator = FormViewAddendums.FindControl("RequiredFieldValidatorDeliveryTerms")
        Dim RequiredFieldValidatorGuaranteePeriod As RequiredFieldValidator = FormViewAddendums.FindControl("RequiredFieldValidatorGuaranteePeriod")
        Dim CompareValidatorRequested As CompareValidator = FormViewAddendums.FindControl("CompareValidatorRequested")
        'Dim CompareValidatorCostCodeBudget As CompareValidator = FormViewAddendums.FindControl("CompareValidatorCostCodeBudget")
        Dim RequiredFieldValidatorDate As RequiredFieldValidator = FormViewAddendums.FindControl("RequiredFieldValidatorDate")
        Dim CompareValidatorAddValueWithVAT As CompareValidator = FormViewAddendums.FindControl("CompareValidatorAddValueWithVAT")
        Dim CompareValidatorReplaceAddendum As CompareValidator = FormViewAddendums.FindControl("CompareValidatorReplaceAddendum")

        Dim TextBoxAddendumValue_withVAT As TextBox = FormViewAddendums.FindControl("TextBoxAddendumValue_withVAT")
        Dim TextBoxVAT As TextBox = FormViewAddendums.FindControl("TextBoxVAT")

        Dim _ddl As DropDownList = sender

        If _ddl.SelectedValue = 1 Then
            ' Regular Addendum
            RequiredFieldValidatorStartDate.Enabled = True
            RequiredFieldValidatorFinishDate.Enabled = True
            RequiredFieldValidatorPenalty.Enabled = True
            RequiredFieldValidatorPenaltyToSupplier.Enabled = True
            RequiredFieldValidatorBudget.Enabled = True
            RequiredFieldValidatorAdvance.Enabled = True
            RequiredFieldValidatorInterim.Enabled = True
            RequiredFieldValidatorShipment.Enabled = True
            RequiredFieldValidatorDelivery.Enabled = True
            RequiredFieldValidatorAddendumRetention.Enabled = True
            RequiredFieldValidatorDeliveryTerms.Enabled = True
            RequiredFieldValidatorGuaranteePeriod.Enabled = True
            CompareValidatorRequested.Enabled = True
            'CompareValidatorCostCodeBudget.Enabled = True
            RequiredFieldValidatorDate.Enabled = False
            CompareValidatorAddValueWithVAT.Enabled = True
            CompareValidatorReplaceAddendum.Enabled = False

            TextBoxAddendumValue_withVAT.Text = String.Empty
            TextBoxVAT.Text = String.Empty

            TextBoxAddendumValue_withVAT.Enabled = True
            TextBoxVAT.Enabled = True

        ElseIf _ddl.SelectedValue = 2 Then
            ' Replacement Addendum
            RequiredFieldValidatorStartDate.Enabled = True
            RequiredFieldValidatorFinishDate.Enabled = True
            RequiredFieldValidatorPenalty.Enabled = True
            RequiredFieldValidatorPenaltyToSupplier.Enabled = True
            RequiredFieldValidatorBudget.Enabled = True
            RequiredFieldValidatorAdvance.Enabled = True
            RequiredFieldValidatorInterim.Enabled = True
            RequiredFieldValidatorShipment.Enabled = True
            RequiredFieldValidatorDelivery.Enabled = True
            RequiredFieldValidatorAddendumRetention.Enabled = True
            RequiredFieldValidatorDeliveryTerms.Enabled = True
            RequiredFieldValidatorGuaranteePeriod.Enabled = True
            CompareValidatorRequested.Enabled = True
            'CompareValidatorCostCodeBudget.Enabled = True
            RequiredFieldValidatorDate.Enabled = False
            CompareValidatorAddValueWithVAT.Enabled = True

            If CreateDataReader.Create_Table_Contract(Convert.ToInt32(LabelContractIDonAddendum.Text)).ProjectID <> 999 Then
                CompareValidatorReplaceAddendum.Enabled = True
                CompareValidatorReplaceAddendum.ErrorMessage = "It cannot be less than Total Invoice value > " + String.Format("{0:###,###,###.00}", ReplaceAddendumCheck.GetTotalInvoice_WithVAT_AgainstPO(PTS.CoreTables.CreateDataReader.Create_Table_Contract(LabelContractIDonAddendum.Text).PO_No))
                CompareValidatorReplaceAddendum.ValueToCompare = ReplaceAddendumCheck.GetTotalInvoice_WithVAT_AgainstPO(PTS.CoreTables.CreateDataReader.Create_Table_Contract(LabelContractIDonAddendum.Text).PO_No)
            End If

            TextBoxAddendumValue_withVAT.Text = String.Empty
            TextBoxVAT.Text = String.Empty

            TextBoxAddendumValue_withVAT.Enabled = True
            TextBoxVAT.Enabled = True

        ElseIf _ddl.SelectedValue = 3 Then
            ' ZERO value addendum
            ' Release validation controls
            RequiredFieldValidatorStartDate.Enabled = False
            RequiredFieldValidatorFinishDate.Enabled = False
            RequiredFieldValidatorPenalty.Enabled = True
            RequiredFieldValidatorPenaltyToSupplier.Enabled = True
            RequiredFieldValidatorBudget.Enabled = False
            RequiredFieldValidatorAdvance.Enabled = False
            RequiredFieldValidatorInterim.Enabled = False
            RequiredFieldValidatorShipment.Enabled = False
            RequiredFieldValidatorDelivery.Enabled = False
            RequiredFieldValidatorAddendumRetention.Enabled = False
            RequiredFieldValidatorDeliveryTerms.Enabled = False
            RequiredFieldValidatorGuaranteePeriod.Enabled = False
            CompareValidatorRequested.Enabled = False
            'CompareValidatorCostCodeBudget.Enabled = False
            RequiredFieldValidatorDate.Enabled = True
            CompareValidatorAddValueWithVAT.Enabled = False
            CompareValidatorReplaceAddendum.Enabled = False

            ' set value ZERO
            TextBoxAddendumValue_withVAT.Enabled = False
            TextBoxVAT.Enabled = False

            TextBoxAddendumValue_withVAT.Text = 0.0
            TextBoxVAT.Text = 0

        End If
    End Sub

    Protected Sub DropDownListPenalty_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim RequiredFieldValidatorPenaltyMercuryNote As RequiredFieldValidator = FormViewAddendums.FindControl("RequiredFieldValidatorPenaltyMercuryNote")
        Dim DDL As DropDownList = sender

        If DDL.SelectedValue = 1 Then ' it is YES
            RequiredFieldValidatorPenaltyMercuryNote.Enabled = True
        Else
            RequiredFieldValidatorPenaltyMercuryNote.Enabled = False
        End If

    End Sub

    Protected Sub DropDownListPenaltyToSupplier_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim RequiredFieldValidatorPenaltySupplierNote As RequiredFieldValidator = FormViewAddendums.FindControl("RequiredFieldValidatorPenaltySupplierNote")
        Dim DDL As DropDownList = sender

        If DDL.SelectedValue = 1 Then ' it is YES
            RequiredFieldValidatorPenaltySupplierNote.Enabled = True
        Else
            RequiredFieldValidatorPenaltySupplierNote.Enabled = False
        End If

    End Sub

    Protected Sub ButtonBudgetPDFUpload_Click(sender As Object, e As EventArgs)

        Dim FileToUpload As FileUpload = FormViewAddendums.FindControl("FileUploadBudgetPDF")
        Dim LabelProjectName As Label = FormViewAddendums.FindControl("LabelProjectName")
        Dim LabelInfo As Label = FormViewAddendums.FindControl("LabelInfoBudgetPDF")
        Dim TextLink As TextBox = FormViewAddendums.FindControl("TextBoxBudgetPDF")

        If LabelProjectName.Text.ToString = "Select Project" Then
            LabelInfo.ForeColor = System.Drawing.Color.Red
            LabelInfo.Text = "Please select Project Name"
            TextLink.Text = ""
        Else
            If FileToUpload.HasFile Then
                If System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".pdf" _
                  OrElse System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".doc" _
                  OrElse System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".docx" Then
                    If FileToUpload.PostedFile.ContentLength / 1000 > 5000 Then
                        LabelInfo.ForeColor = System.Drawing.Color.Red
                        LabelInfo.Text = "PDF file size must be less than 5MB"
                        TextLink.Text = ""
                    Else
                        If Directory.Exists(Server.MapPath("~/CONTRACT/") + LabelProjectName.Text.ToString + "/") Then
                            Dim UniqueString11 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                            Dim UniqueString12 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                            FileToUpload.SaveAs(MapPath("~/CONTRACT/" + LabelProjectName.Text.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)))
                            TextLink.Text = "~/CONTRACT/" + LabelProjectName.Text.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)
                            LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
                            LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
                        Else
                            Dim UniqueString21 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                            Dim UniqueString22 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                            Directory.CreateDirectory(Server.MapPath("~/CONTRACT/") + LabelProjectName.Text.ToString)
                            FileToUpload.SaveAs(MapPath("~/CONTRACT/" + LabelProjectName.Text.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)))
                            TextLink.Text = "~/CONTRACT/" + LabelProjectName.Text.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)
                            LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
                            LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
                        End If
                    End If
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
End Class

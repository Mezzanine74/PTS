Imports System
Imports System.IO
Imports System.Net
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports PTS_App_Code_VB_Project.PTS.CoreTables
Imports System.Data
Imports PTS_App_Code_VB_Project

Partial Class contractView_ApprovalMatrixPTM3SS
    Inherits System.Web.UI.Page

    Dim _prjID_Search As String = "0"
    Dim _supplierID_Search As String = "0"

    Dim EmailToAdmin As New MyCommonTasks

    Dim _mycommonTask As New MyCommonTasks

    Dim SupplierNameCarrier As String = ""
    Dim SupplierNameErrorOrName As String = ""

    ' Variables for TextBox_TextChanged
    Dim ProjectName As String = ""
    Dim ContractNo As String = ""
    Dim ContractDate As String = ""
    Dim ContractDescription As String = ""
    Dim ContractType As String = ""
    Dim SignedBySupplier As Boolean = Nothing
    Dim SignedByMercury As Boolean = Nothing
    Dim CollectedBySupplier As Boolean = Nothing
    Dim ContractGivenTo As String = ""
    Dim ContractValue As Decimal = 0.0
    Dim ContractCurrency As String = ""
    Dim Retention As Decimal = 0.0
    Dim Archived As Boolean = Nothing
    Dim Note As String = ""
    Dim LinkToDOC As String = ""
    Dim LinkToPDF As String = ""
    Dim PoNoBeforeEdit As String = ""

    Protected Sub GridViewShowContract_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GridViewShowContract.RowCancelingEdit
        Session("SupplierID2") = ""
    End Sub

    Protected Sub GridViewShowContract_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewShowContract.RowCommand
        'Dim webFormCommon = New host_NET45_MVC.App_Code.WebFormCommon
        If (e.CommandName = "OpenPDF") Then
            Dim LinkToFile As String = e.CommandArgument.ToString
            'webFormCommon.processFileManager(Page, panelContainer, LinkToFile, 0)
            ASPxFileManager1.Visible = True
            ASPxFileManager1.Settings.RootFolder = LinkToFile
            ASPxFileManager1.SettingsEditing.AllowDelete = False
            ASPxFileManager1.SettingsUpload.Enabled = False

            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)

        End If

        If (e.CommandName = "OpenDOC") Then
            Dim LinkToFile As String = e.CommandArgument.ToString
            'webFormCommon.processFileManager(Page, panelContainer, LinkToFile, 0)
            ASPxFileManager1.Visible = True
            ASPxFileManager1.Settings.RootFolder = LinkToFile
            ASPxFileManager1.SettingsEditing.AllowDelete = False
            ASPxFileManager1.SettingsUpload.Enabled = False

            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)

        End If

        If (e.CommandName = "OpenPDFedit") Then

            Dim LinkToFile As String = e.CommandArgument.ToString

            Dim gvr As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            'LabelTest.Text = gvr.ToString()

            Dim LinkToPDFcopyTextBoxEdit As TextBox = DirectCast(gvr.FindControl("LinkToPDFcopyTextBoxEdit"), TextBox)
            Dim LiteralContractID As Literal = DirectCast(gvr.FindControl("LiteralContractID"), Literal)

            Dim UniqueString21 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
            Dim UniqueString22 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")

            If LinkToFile.Trim().Length > 0 AndAlso IO.Directory.Exists(Server.MapPath(LinkToFile)) Then
                'webFormCommon.processFileManager(Page, panelContainer, LinkToFile, hiddenFileManagerUploadContractPDFMode.Value)
                ASPxFileManager1.Visible = True
                ASPxFileManager1.Settings.RootFolder = LinkToFile

                If hiddenFileManagerUploadContractPDFMode.Value = 1 Then
                    ASPxFileManager1.SettingsUpload.Enabled = True
                    ASPxFileManager1.SettingsEditing.AllowDelete = True
                Else
                    ASPxFileManager1.SettingsUpload.Enabled = False
                    ASPxFileManager1.SettingsEditing.AllowDelete = False
                End If
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)

            Else
                Dim prjId As Integer = PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table_Contracts.GetRowByPrimaryKey(LiteralContractID.Text).ProjectID
                Dim prjName As String = PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table1_Project.GetRowByPrimaryKey(prjId).ProjectName
                Dim _directory As String = "~/CONTRACT/" + prjName.Trim() + "/" + UniqueString21 + UniqueString22
                LinkToPDFcopyTextBoxEdit.Text = _directory

                Dim _createDirectory As String = Server.MapPath(_directory)
                Directory.CreateDirectory(_createDirectory)

                'webFormCommon.processFileManager(Page, panelContainer, LinkToFile, hiddenFileManagerUploadContractPDFMode.Value)
                ASPxFileManager1.Visible = True
                ASPxFileManager1.Settings.RootFolder = _directory

                If hiddenFileManagerUploadContractPDFMode.Value = 1 Then
                    ASPxFileManager1.SettingsUpload.Enabled = True
                    ASPxFileManager1.SettingsEditing.AllowDelete = True
                Else
                    ASPxFileManager1.SettingsUpload.Enabled = False
                    ASPxFileManager1.SettingsEditing.AllowDelete = False
                End If
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)

            End If

        End If

        If (e.CommandName = "OpenDOCedit") Then

            Dim LinkToFile As String = e.CommandArgument.ToString

            Dim gvr As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            'LabelTest.Text = gvr.ToString()

            Dim LinkToTemplatefile_DOCTextBoxEdit As TextBox = DirectCast(gvr.FindControl("LinkToTemplatefile_DOCTextBoxEdit"), TextBox)
            Dim LiteralContractID As Literal = DirectCast(gvr.FindControl("LiteralContractID"), Literal)

            Dim UniqueString21 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
            Dim UniqueString22 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")

            If LinkToFile.Trim().Length > 0 AndAlso IO.Directory.Exists(Server.MapPath(LinkToFile)) Then
                'webFormCommon.processFileManager(Page, panelContainer, LinkToFile, hiddenFileManagerUploadContractDOCMode.Value)
                ASPxFileManager1.Visible = True
                ASPxFileManager1.Settings.RootFolder = LinkToFile

                If hiddenFileManagerUploadContractDOCMode.Value = 1 Then
                    ASPxFileManager1.SettingsUpload.Enabled = True
                    ASPxFileManager1.SettingsEditing.AllowDelete = True
                Else
                    ASPxFileManager1.SettingsUpload.Enabled = False
                    ASPxFileManager1.SettingsEditing.AllowDelete = False
                End If
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)

            Else
                Dim prjId As Integer = PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table_Contracts.GetRowByPrimaryKey(LiteralContractID.Text).ProjectID
                Dim prjName As String = PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table1_Project.GetRowByPrimaryKey(prjId).ProjectName
                Dim _directory As String = "~/CONTRACT/" + prjName.Trim() + "/" + UniqueString21 + UniqueString22
                LinkToTemplatefile_DOCTextBoxEdit.Text = _directory

                Dim _createDirectory As String = Server.MapPath(_directory)
                Directory.CreateDirectory(_createDirectory)

                'webFormCommon.processFileManager(Page, panelContainer, LinkToFile, hiddenFileManagerUploadContractDOCMode.Value)
                ASPxFileManager1.Visible = True
                ASPxFileManager1.Settings.RootFolder = _directory

                If hiddenFileManagerUploadContractDOCMode.Value = 1 Then
                    ASPxFileManager1.SettingsUpload.Enabled = True
                    ASPxFileManager1.SettingsEditing.AllowDelete = True
                Else
                    ASPxFileManager1.SettingsUpload.Enabled = False
                    ASPxFileManager1.SettingsEditing.AllowDelete = False
                End If
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)

            End If

        End If

        If (e.CommandName = "OpenBudgetPDF") Then
            Dim LinkToFile As String = e.CommandArgument.ToString
            Dim openpdf As New MyCommonTasks
            openpdf.OpenPDF(LinkToFile)
        End If

        If (e.CommandName = "ClientContractAktToWork") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridViewShowContract.Rows(index)

            ' open PDF here
            Dim LabelContractIDitem As Label = DirectCast(row.FindControl("LabelContractIDitem"), Label)
            PTSMainClass.OpenFile(Page, PTS_MERCURY.db.QuickTables.Table_Contracts_ClientAdditional(LabelContractIDitem.Text).LinkToAktOfWork)

        End If

        ' ClientData AktOfWork
        If (e.CommandName = "AktOfWorkContract") Then
            '' Retrieve the row index stored in the CommandArgument property.
            'Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            '' Retrieve the row that contains the button 
            '' from the Rows collection.
            'Dim row As GridViewRow = GridViewShowContract.Rows(index)

            'Dim FileUploadAktOfWorkContract As FileUpload = DirectCast(row.FindControl("FileUploadAktOfWorkContract"), FileUpload)
            'Dim DDLProjectEdit As DropDownList = DirectCast(row.FindControl("DDLProjectEdit"), DropDownList)
            'Dim LabelLinkAktOfWorkContract As Label = DirectCast(row.FindControl("LabelLinkAktOfWorkContract"), Label)

            'If DDLProjectEdit IsNot Nothing Then

            '    If DDLProjectEdit.SelectedItem.ToString = "Select Project" Then
            '        ' This is not applicable
            '    Else
            '        If FileUploadAktOfWorkContract.HasFile Then
            '            If System.IO.Path.GetExtension(FileUploadAktOfWorkContract.PostedFile.FileName) = ".pdf" Then
            '                If Directory.Exists(Server.MapPath("~/CONTRACT/") + DDLProjectEdit.SelectedItem.ToString + "/") Then
            '                    Dim UniqueString11 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
            '                    Dim UniqueString12 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
            '                    FileUploadAktOfWorkContract.SaveAs(MapPath("~/CONTRACT/" + DDLProjectEdit.SelectedItem.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileUploadAktOfWorkContract.PostedFile.FileName)))
            '                    LabelLinkAktOfWorkContract.Text = "~/CONTRACT/" + DDLProjectEdit.SelectedItem.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileUploadAktOfWorkContract.PostedFile.FileName)
            '                    _GiveNotification.Gritter_Error(Page, BodyTexts.Ref("auG3R9xVU0e2V3RrFYwIOA"), FileUploadAktOfWorkContract.PostedFile.FileName + " " + BodyTexts.Ref("nT+75PL7wkutDMuL3dxlAQ"), "success")
            '                Else
            '                    Dim UniqueString21 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
            '                    Dim UniqueString22 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
            '                    Directory.CreateDirectory(Server.MapPath("~/CONTRACT/") + DDLProjectEdit.SelectedItem.ToString)
            '                    FileUploadAktOfWorkContract.SaveAs(MapPath("~/CONTRACT/" + DDLProjectEdit.SelectedItem.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileUploadAktOfWorkContract.PostedFile.FileName)))
            '                    LabelLinkAktOfWorkContract.Text = "~/CONTRACT/" + DDLProjectEdit.SelectedItem.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileUploadAktOfWorkContract.PostedFile.FileName)
            '                    _GiveNotification.Gritter_Error(Page, BodyTexts.Ref("auG3R9xVU0e2V3RrFYwIOA"), FileUploadAktOfWorkContract.PostedFile.FileName + " " + BodyTexts.Ref("nT+75PL7wkutDMuL3dxlAQ"), "success")
            '                End If
            '            Else
            '                LabelLinkAktOfWorkContract.Text = String.Empty
            '                _GiveNotification.Gritter_Error(Page, BodyTexts.Ref("eyzM1TdLxU2COy8hN6LbdQ"), BodyTexts.Ref("1RrcCK6rikW3nasKLE81Ug"), BodyTexts.Ref("eyzM1TdLxU2COy8hN6LbdQ"))
            '            End If
            '        Else
            '            LabelLinkAktOfWorkContract.Text = String.Empty
            '            _GiveNotification.Gritter_Error(Page, BodyTexts.Ref("eyzM1TdLxU2COy8hN6LbdQ"), BodyTexts.Ref("tqE5J0iPv0CoQEbW88409w"), BodyTexts.Ref("eyzM1TdLxU2COy8hN6LbdQ"))

            '        End If
            '    End If
            'End If
            Dim LinkToFile As String

            Dim gvr As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)

            Dim LabelLinkAktOfWorkContract As Label = DirectCast(gvr.FindControl("LabelLinkAktOfWorkContract"), Label)
            LinkToFile = LabelLinkAktOfWorkContract.Text

            Dim UniqueString21 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
            Dim UniqueString22 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")

            If LinkToFile.Trim().Length > 0 AndAlso IO.Directory.Exists(Server.MapPath(LinkToFile)) Then
                'webFormCommon.processFileManager(Page, panelContainer, LinkToFile, hiddenFileManagerUploadAddendumDOCMode.Value)
                ASPxFileManager1.Visible = True
                ASPxFileManager1.Settings.RootFolder = LinkToFile

                If hiddenFileManagerUploadContractDOCMode.Value = 1 And (User.IsInRole("ContractLeadGirls") Or User.IsInRole("ContractSupportGirl")) Then
                    ASPxFileManager1.SettingsUpload.Enabled = True
                    ASPxFileManager1.SettingsEditing.AllowDelete = True
                Else
                    ASPxFileManager1.SettingsUpload.Enabled = False
                    ASPxFileManager1.SettingsEditing.AllowDelete = False
                End If
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)

            Else
                Dim _directory As String = "~/CONTRACT/" + DropDownListPrjID.SelectedItem.ToString + "/" + UniqueString21 + UniqueString22
                LabelLinkAktOfWorkContract.Text = _directory

                Dim _createDirectory As String = Server.MapPath(_directory)
                Directory.CreateDirectory(_createDirectory)

                'webFormCommon.processFileManager(Page, panelContainer, LinkToFile, hiddenFileManagerUploadAddendumDOCMode.Value)
                ASPxFileManager1.Visible = True
                ASPxFileManager1.Settings.RootFolder = _directory

                If hiddenFileManagerUploadContractDOCMode.Value = 1 And (User.IsInRole("ContractLeadGirls") Or User.IsInRole("ContractSupportGirl")) Then
                    ASPxFileManager1.SettingsUpload.Enabled = True
                    ASPxFileManager1.SettingsEditing.AllowDelete = True
                Else
                    ASPxFileManager1.SettingsUpload.Enabled = False
                    ASPxFileManager1.SettingsEditing.AllowDelete = False
                End If
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)

            End If

        End If

        If (e.CommandName = "AktOfWorkContractItem") Then

            Dim LinkToFile As String

            Dim gvr As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)

            Dim LabelLinkAktOfWorkContractItem As Label = DirectCast(gvr.FindControl("LabelLinkAktOfWorkContractItem"), Label)
            LinkToFile = LabelLinkAktOfWorkContractItem.Text

            Dim UniqueString21 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
            Dim UniqueString22 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")

            If LinkToFile.Trim().Length > 0 AndAlso IO.Directory.Exists(Server.MapPath(LinkToFile)) Then
                'webFormCommon.processFileManager(Page, panelContainer, LinkToFile, hiddenFileManagerUploadAddendumDOCMode.Value)
                ASPxFileManager1.Visible = True
                ASPxFileManager1.Settings.RootFolder = LinkToFile
                ASPxFileManager1.SettingsUpload.Enabled = False
                ASPxFileManager1.SettingsEditing.AllowDelete = False
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)
            End If

        End If

        ' it will upload new Budget files if required
        If (e.CommandName = "UploadBudgetPDF") Then
            ' Retrieve the row index stored in the CommandArgument property.
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            ' Retrieve the row that contains the button 
            ' from the Rows collection.
            Dim row As GridViewRow = GridViewShowContract.Rows(index)

            Dim FileUploadBudgetPDF As FileUpload = DirectCast(row.FindControl("FileUploadBudgetPDF"), FileUpload)
            Dim DDLProjectEdit As DropDownList = DirectCast(row.FindControl("DDLProjectEdit"), DropDownList)
            Dim labelBudgetInfo As Label = DirectCast(row.FindControl("labelBudgetInfo"), Label)
            Dim TextBoxBudgetLinkPDF As TextBox = DirectCast(row.FindControl("TextBoxBudgetLinkPDF"), TextBox)

            If DDLProjectEdit IsNot Nothing Then
                If DDLProjectEdit.SelectedItem.ToString = "Select Project" Then
                    ' This is not applicable
                Else
                    If FileUploadBudgetPDF.HasFile Then
                        If System.IO.Path.GetExtension(FileUploadBudgetPDF.PostedFile.FileName) = ".doc" _
                            OrElse System.IO.Path.GetExtension(FileUploadBudgetPDF.PostedFile.FileName) = ".docx" _
                            OrElse System.IO.Path.GetExtension(FileUploadBudgetPDF.PostedFile.FileName) = ".pdf" Then
                            'If FileUploadDOCEdit.PostedFile.ContentLength / 1000 > 5000 Then
                            '    labelInfoForAttachments.ForeColor = System.Drawing.Color.Red
                            '    labelInfoForAttachments.Text = "PDF file size must be less than 5MB"
                            '    LinkToTemplatefile_DOCTextBoxEdit.Text = ""
                            'Else
                            If Directory.Exists(Server.MapPath("~/CONTRACT/") + DDLProjectEdit.SelectedItem.ToString + "/") Then
                                Dim UniqueString11 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                                Dim UniqueString12 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                                FileUploadBudgetPDF.SaveAs(MapPath("~/CONTRACT/" + DDLProjectEdit.SelectedItem.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileUploadBudgetPDF.PostedFile.FileName)))
                                TextBoxBudgetLinkPDF.Text = "~/CONTRACT/" + DDLProjectEdit.SelectedItem.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileUploadBudgetPDF.PostedFile.FileName)
                                labelBudgetInfo.ForeColor = System.Drawing.Color.DarkGreen
                                labelBudgetInfo.Text = FileUploadBudgetPDF.PostedFile.FileName + " " + BodyTexts.Ref("nT+75PL7wkutDMuL3dxlAQ")
                            Else
                                Dim UniqueString21 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                                Dim UniqueString22 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                                Directory.CreateDirectory(Server.MapPath("~/CONTRACT/") + DDLProjectEdit.SelectedItem.ToString)
                                FileUploadBudgetPDF.SaveAs(MapPath("~/CONTRACT/" + DDLProjectEdit.SelectedItem.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileUploadBudgetPDF.PostedFile.FileName)))
                                TextBoxBudgetLinkPDF.Text = "~/CONTRACT/" + DDLProjectEdit.SelectedItem.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileUploadBudgetPDF.PostedFile.FileName)
                                labelBudgetInfo.ForeColor = System.Drawing.Color.DarkGreen
                                labelBudgetInfo.Text = FileUploadBudgetPDF.PostedFile.FileName + " " + BodyTexts.Ref("nT+75PL7wkutDMuL3dxlAQ")
                            End If
                            'End If
                        Else
                            labelBudgetInfo.ForeColor = System.Drawing.Color.Red
                            labelBudgetInfo.Text = BodyTexts.Ref("wxvsz3lzp0ORoQ7ioO6Xjw")
                            TextBoxBudgetLinkPDF.Text = ""
                        End If
                    Else
                        TextBoxBudgetLinkPDF.Text = ""
                        labelBudgetInfo.ForeColor = System.Drawing.Color.Red
                        labelBudgetInfo.Text = BodyTexts.Ref("tqE5J0iPv0CoQEbW88409w")
                    End If
                End If
            End If
        End If

        If (e.CommandName = "AddAddendum") Then
            ' Retrieve the row index stored in the CommandArgument property.
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            ' Retrieve the row that contains the button 
            ' from the Rows collection.
            Dim row As GridViewRow = GridViewShowContract.Rows(index)

            Dim LabelPOnoItem As Label = DirectCast(row.FindControl("LabelPOnoItem"), Label)
            Dim LabelSupplierName As Label = DirectCast(row.FindControl("LabelSupplierName"), Label)
            Dim LabelContractNoItem As Label = DirectCast(row.FindControl("LabelContractNoItem"), Label)
            Dim LabelContractDateItem As Label = DirectCast(row.FindControl("LabelContractDateItem"), Label)
            Dim LabelContractValueItem As Label = DirectCast(row.FindControl("LabelContractValueItem"), Label)
            Dim LiteralContractValueWithVATItemValue As Literal = DirectCast(row.FindControl("LiteralContractValueWithVATItemValue"), Literal)
            Dim LabelContractCurrency As Label = DirectCast(row.FindControl("LabelContractCurrency"), Label)
            Dim LabelContractTypeItem As Label = DirectCast(row.FindControl("LabelContractTypeItem"), Label)
            Dim LabelContractDescItem As Label = DirectCast(row.FindControl("LabelContractDescItem"), Label)
            Dim LabelContractIDitem As Label = DirectCast(row.FindControl("LabelContractIDitem"), Label)

            ' Assing all controls to fly addendum page
            LabelProjectName.Text = DropDownListPrjID.SelectedItem.ToString
            If GridViewShowContract.AllowPaging = True Then
                LabelGridViewPagingStatus.Text = BodyTexts.Ref("2TUx46Nv1UeQLerDF4GQjA")
            ElseIf GridViewShowContract.AllowPaging = False Then
                LabelGridViewPagingStatus.Text = BodyTexts.Ref("sl3la1Y+vUmYXJQRX9Mosw")
            End If
            LabelGridViewPageSize.Text = DDLpageSize.SelectedValue.ToString
            If GridViewShowContract.AllowPaging = True Then
                LabelGridViewPageNumber.Text = GridViewShowContract.PageIndex.ToString
            ElseIf GridViewShowContract.AllowPaging = False Then
                LabelGridViewPageNumber.Text = "NoPaging"
            End If
            LabelPOno.Text = LabelPOnoItem.Text.ToString
            LabelSupplierNameV.Text = LabelSupplierName.Text.ToString
            LabelContractNo.Text = LabelContractNoItem.Text.ToString
            LabelContractDate.Text = LabelContractDateItem.Text.ToString
            LabelContractValue.Text = LabelContractValueItem.Text.ToString
            LabelContractValueWithVAT.Text = LiteralContractValueWithVATItemValue.Text.ToString
            LabelContractCurrencyV.Text = LabelContractCurrency.Text.ToString
            LabelContractType.Text = LabelContractTypeItem.Text.ToString
            LabelContractDescription.Text = LabelContractDescItem.Text.ToString
            LabelContractID.Text = LabelContractIDitem.Text.ToString
        End If

        If (e.CommandName = "ShowAddendum") Then
            ' Retrieve the row index stored in the CommandArgument property.
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            ' Retrieve the row that contains the button 
            ' from the Rows collection.
            Dim row As GridViewRow = GridViewShowContract.Rows(index)

            Dim SqlDataSourceShowAddendum As SqlDataSource = DirectCast(row.FindControl("SqlDataSourceShowAddendum"), SqlDataSource)
            Dim LabelContractIDitem As Label = DirectCast(row.FindControl("LabelContractIDitem"), Label)
            Dim LabelHideShow As Label = DirectCast(row.FindControl("LabelHideShow"), Label)
            Dim LinkButtonNumberOfAddendum As LinkButton = DirectCast(row.FindControl("LinkButtonNumberOfAddendum"), LinkButton)

            If Convert.ToInt32(LabelHideShow.Text) Mod 2 = 0 Then
                ' Provide parameter value for SqlDataSource
                SqlDataSourceShowAddendum.SelectParameters("ContractID").DefaultValue = Convert.ToInt32(LabelContractIDitem.Text.ToString)
                SqlDataSourceShowAddendum.SelectParameters("ContractID").Type = TypeCode.Int32
                LinkButtonNumberOfAddendum.Text = BodyTexts.Ref("XFYuByLmokK0QJmEd43FJQ")
            ElseIf Convert.ToInt32(LabelHideShow.Text) Mod 2 = 1 Then
                ' It will hide Addendum Gridview bt providing Nothing parameter
                SqlDataSourceShowAddendum.SelectParameters("ContractID").DefaultValue = Nothing
                SqlDataSourceShowAddendum.SelectParameters("ContractID").Type = TypeCode.Int32

                ' To show number of addendums under the contract
                If LabelContractIDitem IsNot Nothing Then
                    Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                        con.Open()
                        Dim sqlstring As String = "SELECT     COUNT(dbo.Table_Addendums.AddendumID) AS NumberOfAddendum" +
                                                        " FROM         dbo.Table_Addendums INNER JOIN " +
                                                        " dbo.Table_Contracts ON dbo.Table_Addendums.ContractID = dbo.Table_Contracts.ContractID " +
                                                        " WHERE     (dbo.Table_Addendums.ContractID = " + LabelContractIDitem.Text + " ) "
                        Dim cmd As New SqlCommand(sqlstring, con)
                        cmd.CommandType = System.Data.CommandType.Text
                        Dim dr As SqlDataReader = cmd.ExecuteReader
                        While dr.Read
                            If Convert.ToInt32(dr(0).ToString) > 0 Then
                                LinkButtonNumberOfAddendum.Text = "#" + dr(0).ToString + " " + BodyTexts.Ref("U8IZNfQXVEKjw+Zetzwq/Q")
                            End If
                        End While
                        con.Close()
                        dr.Close()
                    End Using
                End If

            End If

            LabelHideShow.Text = Convert.ToString(Convert.ToInt32(LabelHideShow.Text) + 1)

            Dim LabelRowIndexOnGridviewContract As Label = DirectCast(row.FindControl("LabelRowIndexOnGridviewContract"), Label)
            LabelRowIndexOnGridviewContract.Text = (e.CommandArgument)

        End If

        ' Frame Contract Ticket
        If (e.CommandName = "RaisePoForFrameContract") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridViewShowContract.Rows(index)
            Dim LabelContractIDitem As Label = DirectCast(row.FindControl("LabelContractIDitem"), Label)

            Session("FrameTicket") = Guid.NewGuid().ToString().GetHashCode().ToString("x")
            InsertContractIDToFrameContractTicket(Convert.ToInt32(LabelContractIDitem.Text), Session("FrameTicket"))

        End If

        ' Exceptional approval
        If (e.CommandName = "ExceptionalApproveContract") Then
            Dim rowIndex As String = e.CommandArgument
            Dim GridviewShowContract As GridView = sender
            Dim row As GridViewRow = GridviewShowContract.Rows(rowIndex)

            ' Find ContractID
            Dim LabelContractIDitem As Label = DirectCast(row.FindControl("LabelContractIDitem"), Label)
            If LabelContractIDitem IsNot Nothing Then

                ' Insert Or Update PO with exceptional approval
                ContractView.InsertOrUpdatePOWithExceptionalApproval(CreateDataReader.Create_Table_Contract(Convert.ToInt32(LabelContractIDitem.Text)).ProjectID,
                                                                     Convert.ToInt32(LabelContractIDitem.Text),
                                                                     0,
                                                                     Page.User.Identity.Name.ToString.ToLower,
                                                                     POdetailsForEmail)

            End If
        End If

    End Sub

    Protected Sub InsertContractIDToFrameContractTicket(ByVal _ContractID As Integer, ByVal _UniqueText As String)
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " DELETE FROM [Table_FrameContractTickets] WHERE ContractID = @ContractID " +
              " INSERT INTO [Table_FrameContractTickets] " +
                                  "            ([ContractID] " +
                                  "            ,[PersonCreated] " +
                                  "            ,[WhenCreated], UniqueText) " +
                                  "      VALUES " +
                                  "            ( @ContractID " +
                                  "            , @PersonCreated " +
                                  "            , @WhenCreated, @UniqueText ) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", System.Data.SqlDbType.Int)
            ContractID.Value = _ContractID
            Dim PersonCreated As SqlParameter = cmd.Parameters.Add("@PersonCreated", System.Data.SqlDbType.NVarChar, 256)
            PersonCreated.Value = Page.User.Identity.Name
            Dim WhenCreated As SqlParameter = cmd.Parameters.Add("@WhenCreated", System.Data.SqlDbType.SmallDateTime)
            WhenCreated.Value = LocalTime.GetTime
            Dim UniqueText As SqlParameter = cmd.Parameters.Add("@UniqueText", System.Data.SqlDbType.NVarChar)
            UniqueText.Value = _UniqueText
            Dim dr As SqlDataReader = cmd.ExecuteReader
            con.Close()
            dr.Close()

        End Using
    End Sub

    Protected Sub GridViewShowContract_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewShowContract.RowCreated

        ' fix width problem because of File Upload control
        Dim PanelClientContractAdditionalDetailsEdit As Panel = DirectCast(e.Row.FindControl("PanelClientContractAdditionalDetailsEdit"), Panel)
        If PanelClientContractAdditionalDetailsEdit IsNot Nothing Then
            PanelClientContractAdditionalDetailsEdit.Width = 1000
        End If

    End Sub

    Protected Sub GridViewShowContract_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewShowContract.RowDataBound

        Dim _NewGeneration As Boolean = False

        If e.Row.RowType = DataControlRowType.DataRow Then
            _NewGeneration = DataBinder.Eval(e.Row.DataItem, "NewGeneration")
        End If

        Dim _POexecuted As Boolean = False

        If e.Row.RowType = DataControlRowType.DataRow Then
            _POexecuted = DataBinder.Eval(e.Row.DataItem, "POexecuted")
        End If

        Dim _Exceptional As Boolean = False

        If e.Row.RowType = DataControlRowType.DataRow Then
            _Exceptional = DataBinder.Eval(e.Row.DataItem, "Exceptional")
        End If

        Dim _Logo As String = ""

        If e.Row.RowType = DataControlRowType.DataRow Then
            _Logo = DataBinder.Eval(e.Row.DataItem, "Logo")
        End If

        Dim _CountCommon As Integer = 0

        If e.Row.RowType = DataControlRowType.DataRow Then
            _CountCommon = DataBinder.Eval(e.Row.DataItem, "CountCommon")
        End If

        Dim _SupplierNew As Integer = 0

        If e.Row.RowType = DataControlRowType.DataRow Then
            _SupplierNew = DataBinder.Eval(e.Row.DataItem, "SupplierNew")
        End If

        Dim _AddendumCount As Integer = 0

        If e.Row.RowType = DataControlRowType.DataRow Then
            _AddendumCount = DataBinder.Eval(e.Row.DataItem, "AddendumCount")
        End If

        Dim _OpenPOValue As Decimal = 0

        If e.Row.RowType = DataControlRowType.DataRow Then
            _OpenPOValue = DataBinder.Eval(e.Row.DataItem, "OpenPOValue")
        End If

        Dim _InitiateContractAndAddendum As Integer = 0

        If e.Row.RowType = DataControlRowType.DataRow Then
            _InitiateContractAndAddendum = DataBinder.Eval(e.Row.DataItem, "InitiateContractAndAddendum").ToString
        End If

        Dim _ProjectID As Integer = DataBinder.Eval(e.Row.DataItem, "ProjectID")

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim HyperlinkContractCoverPage As HyperLink = DirectCast(e.Row.FindControl("HyperlinkContractCoverPage"), HyperLink)
            If HyperlinkContractCoverPage IsNot Nothing Then
                HyperlinkContractCoverPage.Text = BodyTexts.Ref("KdxNrj+A5kaqJc9uSjZUkg")
                HyperlinkContractCoverPage.NavigateUrl = "javascript: w=window.open('/PtsContractual/ContractsAddendums/ContractCoverPage/" + DataBinder.Eval(e.Row.DataItem, "ContractId").ToString() + "','GoogleWindow', 'width=1200, height=1000'); w.print();"
            End If

            Dim _ButtonEdit As Button = DirectCast(e.Row.FindControl("ButtonEdit"), Button)
            Dim _ButtonDelete As Button = DirectCast(e.Row.FindControl("ButtonDelete"), Button)
            Dim _LinkButtonRaisePO As LinkButton = DirectCast(e.Row.FindControl("LinkButtonRaisePO"), LinkButton)
            Dim _LinkButtonAddAddendum As LinkButton = DirectCast(e.Row.FindControl("LinkButtonAddAddendum"), LinkButton)
            Dim _LabelFrameContract As Label = DirectCast(e.Row.FindControl("LabelFrameContract"), Label)
            Dim _ButtonExceptionalApprove As Button = DirectCast(e.Row.FindControl("ButtonExceptionalApprove"), Button)

            If _ButtonEdit IsNot Nothing Then
                _ButtonEdit.Text = BodyTexts.Ref("aOfX+llnm0OCrn21IqoxvQ")
                _ButtonDelete.Text = BodyTexts.Ref("JTHzJ7uo+kqPfIAg4uM8HA")
                _ButtonDelete.OnClientClick = "return confirm('" + BodyTexts.Ref("dcGVT5LdJUGa1sSlsf1pNA") + "');"
                _LinkButtonRaisePO.Text = BodyTexts.Ref("ny7QVq3na0asTTEXQWIkpA")
                _LinkButtonAddAddendum.Text = BodyTexts.Ref("rFg4uhfSXUa3uSrwSP3gMQ")
                _LabelFrameContract.Text = BodyTexts.Ref("T1ijB5Ksf0aasoiUOy8ohg")
                _LabelFrameContract.ToolTip = BodyTexts.Ref("PL3jnEGHkUa1nNpNspvyVA")
                _ButtonExceptionalApprove.Text = BodyTexts.Ref("PVsElor12Uqia5rKU4mbyw")
                _ButtonExceptionalApprove.OnClientClick = "return confirm('" + BodyTexts.Ref("Gw/MIjfzXkK6qXqfgqi4CA") + "');"
            End If

            Dim _ButtonUpdate As Button = DirectCast(e.Row.FindControl("ButtonUpdate"), Button)
            Dim _ButtonCancel As Button = DirectCast(e.Row.FindControl("ButtonCancel"), Button)

            If _ButtonUpdate IsNot Nothing Then
                _ButtonUpdate.Text = BodyTexts.Ref("KTfnFlGK8kWuzdJQLcZe2w")
                _ButtonCancel.Text = BodyTexts.Ref("DRM8lAyeUECweQVjPi1Hlg")
            End If

            Dim _LabelContractNoEdit As Label = DirectCast(e.Row.FindControl("LabelContractNoEdit"), Label)
            Dim _LabelContractDateEdit As Label = DirectCast(e.Row.FindControl("LabelContractDateEdit"), Label)
            Dim _LabelSupplierIDEdit As Label = DirectCast(e.Row.FindControl("LabelSupplierIDEdit"), Label)
            Dim _LabelContractDescriptionEdit As Label = DirectCast(e.Row.FindControl("LabelContractDescriptionEdit"), Label)
            Dim _LabelTypeEdit As Label = DirectCast(e.Row.FindControl("LabelTypeEdit"), Label)
            Dim _LabelSignedSupplierEdit As Label = DirectCast(e.Row.FindControl("LabelSignedSupplierEdit"), Label)
            Dim _LabelSignMercEdit As Label = DirectCast(e.Row.FindControl("LabelSignMercEdit"), Label)
            Dim _LabelGivenToEdit As Label = DirectCast(e.Row.FindControl("LabelGivenToEdit"), Label)
            Dim _LabelCurrencyEdit As Label = DirectCast(e.Row.FindControl("LabelCurrencyEdit"), Label)
            Dim _LabelRetentionEdit As Label = DirectCast(e.Row.FindControl("LabelRetentionEdit"), Label)
            Dim _LabelArchivedEdit As Label = DirectCast(e.Row.FindControl("LabelArchivedEdit"), Label)
            Dim _LabelNote As Label = DirectCast(e.Row.FindControl("LabelNote"), Label)
            Dim _LabelPaymentTermsValidationNotification As Label = DirectCast(e.Row.FindControl("LabelPaymentTermsValidationNotification"), Label)
            Dim _LiteralCommercialRoleTitle As Literal = DirectCast(e.Row.FindControl("LiteralCommercialRoleTitle"), Literal)
            Dim _LiteralContractValueExcVAT As Literal = DirectCast(e.Row.FindControl("LiteralContractValueExcVAT"), Literal)
            Dim _LiteralContractValueWithVAT As Literal = DirectCast(e.Row.FindControl("LiteralContractValueWithVAT"), Literal)

            If _LabelContractNoEdit IsNot Nothing Then
                _LabelContractNoEdit.Text = BodyTexts.Ref("b/QwmB2AWEmXNrVeVHPG7g")
                _LabelContractDateEdit.Text = BodyTexts.Ref("aUlY16DSxEuv5OYTuG/x+g")
                _LabelSupplierIDEdit.Text = BodyTexts.Ref("WCc0K46nMU6P4KVjfJyRyQ")
                _LabelContractDescriptionEdit.Text = BodyTexts.Ref("foHRgj5mokmXEh62sTOoiQ")
                _LabelTypeEdit.Text = BodyTexts.Ref("ofeMLaQHw0mejHavxFcTrg")
                _LabelSignedSupplierEdit.Text = BodyTexts.Ref("vXdfZGm13kewiqmHmdWdyQ")
                _LabelSignMercEdit.Text = BodyTexts.Ref("hL8TNg+mj02+XJTLz0GqUA")
                _LabelGivenToEdit.Text = BodyTexts.Ref("1epQY1LCBkqFks1Ea7MVfw")
                _LabelCurrencyEdit.Text = BodyTexts.Ref("mB+gkXNFaEegQ/Gm8GRgsA")
                _LabelRetentionEdit.Text = BodyTexts.Ref("AG8NVnjS50yHEBirA9v9Jw")
                _LabelArchivedEdit.Text = BodyTexts.Ref("mXA5GI226kq6JCs9nCrlaQ")
                _LabelNote.Text = BodyTexts.Ref("b7cyUgbKckK/KGTaEbD5kA")
                _LabelPaymentTermsValidationNotification.Text = BodyTexts.Ref("clXDy9WL2EyOH/r8/yjvRA")
                _LiteralCommercialRoleTitle.Text = BodyTexts.Ref("jrpT5e3Nm0iuK4hvdj36Hw")
                _LiteralContractValueExcVAT.Text = BodyTexts.Ref("evcsKWMl9kKqtxZvm/CKhw")
                _LiteralContractValueWithVAT.Text = BodyTexts.Ref("0jX4dgMOYk6FdWKaP0+M9A")
            End If

            'If DataBinder.Eval(e.Row.DataItem, "ContractId") = 6030 Then
            '    e.Row.Enabled = False
            '    e.Row.Attributes.Add("style", "background-image: url('http://pts.mercuryeng.ru/images/temporarily_frozen.png');opacity: 0.4")

            'End If

            Dim TxtBoxTagsContract As TextBox = TryCast(e.Row.FindControl("TxtBoxTagsContract"), TextBox)

            Dim PanelTagContract As Panel = TryCast(e.Row.FindControl("PanelTagContract"), Panel)

            If _NewGeneration = False Then
                If PanelTagContract IsNot Nothing Then
                    PanelTagContract.Visible = False
                End If
            End If

            Dim _type As String = "c"
            Dim _index As Integer = DataBinder.Eval(e.Row.DataItem, "ContractID")

        End If

        ' Assign SqlDataSource Select Command as per Generation Type of Project
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim SqlDataSourcePrjEdit As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourcePrjEdit"), SqlDataSource)
            Dim DDLProjectEdit As DropDownList = DirectCast(e.Row.FindControl("DDLProjectEdit"), DropDownList)
            If SqlDataSourcePrjEdit IsNot Nothing Then
                SqlDataSourcePrjEdit.SelectParameters("UserName").DefaultValue = Page.User.Identity.Name.ToString.ToLower
                If _NewGeneration = True Then
                    SqlDataSourcePrjEdit.SelectCommand = " SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName, dbo.aspnet_Users.UserName " +
          "              FROM         dbo.Table1_Project INNER JOIN " +
          "              dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN " +
          "              dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId INNER JOIN " +
          "              dbo.Table_Contracts ON dbo.Table1_Project.ProjectID = dbo.Table_Contracts.ProjectID " +
          "              GROUP BY dbo.Table1_Project.ProjectID, dbo.Table1_Project.ProjectName, dbo.aspnet_Users.UserName " +
          "              HAVING      (dbo.aspnet_Users.UserName = @UserName) " +
          "              ORDER BY ProjectName "
                Else
                    SqlDataSourcePrjEdit.SelectCommand = " SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName, dbo.aspnet_Users.UserName " +
          "              FROM         dbo.Table1_Project INNER JOIN " +
          "              dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN " +
          "              dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId INNER JOIN " +
          "              dbo.Table_Contracts ON dbo.Table1_Project.ProjectID = dbo.Table_Contracts.ProjectID " +
          "              WHERE     (dbo.Table1_Project.NewGeneration <> 1) " +
          "              GROUP BY dbo.Table1_Project.ProjectID, dbo.Table1_Project.ProjectName, dbo.aspnet_Users.UserName " +
          "              HAVING      (dbo.aspnet_Users.UserName = @UserName) " +
          "              ORDER BY ProjectName "
                End If

                DDLProjectEdit.DataBind()
                DDLProjectEdit.SelectedValue = _ProjectID

            End If
        End If

        If e.Row.RowType = DataControlRowType.Header Then
            Dim LabelLarisaTakHatela As Label = DirectCast(e.Row.FindControl("LabelLarisaTakHatela"), Label)
            If _ProjectID = 999 Then
                LabelLarisaTakHatela.Text = "<font>" + BodyTexts.Ref("bDQ0ptw5NEeLgwHPjMW74A") + "</font><br /><font>" + BodyTexts.Ref("svyz7GDL2Eucxx5pcD6WwQ") + "</font>"
            Else
                LabelLarisaTakHatela.Text = "<font>" + BodyTexts.Ref("1epQY1LCBkqFks1Ea7MVfw") + "</font>"
            End If
        End If

        ' Exceptional Approval button will appear only for person who has this role
        ' It appears only if po not executed for only new generation contracts.
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ButtonExceptionalApprove As Button = DirectCast(e.Row.FindControl("ButtonExceptionalApprove"), Button)
            If ButtonExceptionalApprove IsNot Nothing Then
                'Person has this role
                If HiddenButtonExceptionalApproveShowOrNot.Value = True Then
                    ' po not executed 
                    If DataBinder.Eval(e.Row.DataItem, "PoExecuted") = False Then
                        ' new generation contract
                        If _NewGeneration = True Then
                            ButtonExceptionalApprove.Visible = True
                        End If
                    End If
                End If

            End If
        End If

        ' it fixes stringlenth problem in LabelContractNoItem
        If DirectCast(e.Row.FindControl("LabelContractNoItem"), Label) IsNot Nothing Then
            DirectCast(e.Row.FindControl("LabelContractNoItem"), Label).Text = DirectCast(e.Row.FindControl("LabelContractNoItem"), Label).Text.Replace("/", " /")
        End If

        ' it highlights Supplier Name if temporary
        If DirectCast(e.Row.FindControl("LabelSupplierName"), Label) IsNot Nothing AndAlso DirectCast(e.Row.FindControl("LabelSupplierName"), Label).Text = "TEMPORARY" Then
            DirectCast(e.Row.FindControl("LabelSupplierName"), Label).ForeColor = System.Drawing.Color.Red
            DirectCast(e.Row.FindControl("LabelSupplierName"), Label).Font.Bold = True
        End If

        If DirectCast(e.Row.FindControl("LabelSupplierName"), Label) IsNot Nothing AndAlso DirectCast(e.Row.FindControl("LabelSupplierName"), Label).Text = "PERSON" Then
            DirectCast(e.Row.FindControl("LabelSupplierName"), Label).Text = ""
            DirectCast(e.Row.FindControl("ImagePersonOrNot"), Image).ImageUrl = "~/Images/Person.png"
        ElseIf DirectCast(e.Row.FindControl("LabelSupplierName"), Label) IsNot Nothing AndAlso DirectCast(e.Row.FindControl("LabelSupplierName"), Label).Text <> "PERSON" Then
            DirectCast(e.Row.FindControl("ImagePersonOrNot"), Image).Visible = False
        End If

        'it defines CurrencyImage.
        If DirectCast(e.Row.FindControl("LabelContractCurrency"), Label) IsNot Nothing Then
            If DirectCast(e.Row.FindControl("LabelContractCurrency"), Label).Text = "Rub" Then
                DirectCast(e.Row.FindControl("ImageCurrency"), Image).ImageUrl = "~/Images/ruble_.png"
            ElseIf DirectCast(e.Row.FindControl("LabelContractCurrency"), Label).Text = "Dollar" Then
                DirectCast(e.Row.FindControl("ImageCurrency"), Image).ImageUrl = "~/Images/dollar_.png"
            ElseIf DirectCast(e.Row.FindControl("LabelContractCurrency"), Label).Text = "Euro" Then
                DirectCast(e.Row.FindControl("ImageCurrency"), Image).ImageUrl = "~/Images/euro_.png"
            ElseIf DirectCast(e.Row.FindControl("LabelContractCurrency"), Label).Text = "GBP" Then
                DirectCast(e.Row.FindControl("ImageCurrency"), Image).ImageUrl = "~/Images/GBP.png"
            ElseIf DirectCast(e.Row.FindControl("LabelContractCurrency"), Label).Text = String.Empty Or DirectCast(e.Row.FindControl("LabelContractCurrency"), Label).Text = Nothing Then
                DirectCast(e.Row.FindControl("ImageCurrency"), Image).ImageUrl = "~/Images/question_mark.png"
            End If
        End If

        ' it highlights entire row if SignedBySupplier and SignedByMercury both not checked
        If e.Row.RowType = DataControlRowType.DataRow Then
            If DataBinder.Eval(e.Row.DataItem, "SignByMercury").ToString = True AndAlso DataBinder.Eval(e.Row.DataItem, "SignBySupplier").ToString = True Then
                ' do nothing
            Else
                e.Row.BackColor = System.Drawing.Color.Gold
            End If

        End If

        ' it adds % to retention
        If DirectCast(e.Row.FindControl("LabelRetention"), Label) IsNot Nothing Then
            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "Retention")) Then
                If Convert.ToDecimal(DirectCast(e.Row.FindControl("LabelRetention"), Label).Text) = 0.0 Then
                    DirectCast(e.Row.FindControl("LabelRetention"), Label).Text = "n/a"
                ElseIf Convert.ToDecimal(DirectCast(e.Row.FindControl("LabelRetention"), Label).Text) = 99.9 Then
                    DirectCast(e.Row.FindControl("LabelRetention"), Label).Text = "not defined"
                Else
                    DirectCast(e.Row.FindControl("LabelRetention"), Label).Text = DirectCast(e.Row.FindControl("LabelRetention"), Label).Text.ToString + "%"
                End If
            End If
        End If

        'it defines type of PDF and DOC image if it exist or not.
        Try
            If DirectCast(e.Row.FindControl("ImageButtonLinkToTemplatefile_DOC"), ImageButton) IsNot Nothing Then
                Dim path As String = ""
                If Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkToTemplatefile_DOC")).Length > 0 Then
                    path = Server.MapPath(DataBinder.Eval(e.Row.DataItem, "LinkToTemplatefile_DOC"))
                End If

                If (e.Row.RowState And DataControlRowState.Edit) > 0 Then
                    DirectCast(e.Row.FindControl("ImageButtonLinkToTemplatefile_DOC"), ImageButton).Visible = True
                    DirectCast(e.Row.FindControl("ImageButtonLinkToTemplatefile_DOC"), ImageButton).ImageUrl = "~/Images/folder.png"
                Else
                    If IO.Directory.Exists(path) Then
                        DirectCast(e.Row.FindControl("ImageButtonLinkToTemplatefile_DOC"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonLinkToTemplatefile_DOC"), ImageButton).ImageUrl = "~/Images/folder.png"
                    Else
                        DirectCast(e.Row.FindControl("ImageButtonLinkToTemplatefile_DOC"), ImageButton).Visible = False
                    End If
                End If

            End If

        Catch ex As Exception

        End Try

        Try
            If DirectCast(e.Row.FindControl("ImageButtonLinkToPDFcopy"), ImageButton) IsNot Nothing Then
                Dim path As String = ""
                If Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkToPDFcopy")).Length > 0 Then
                    path = Server.MapPath(DataBinder.Eval(e.Row.DataItem, "LinkToPDFcopy"))
                End If

                If (e.Row.RowState And DataControlRowState.Edit) > 0 Then
                    DirectCast(e.Row.FindControl("ImageButtonLinkToPDFcopy"), ImageButton).Visible = True
                    DirectCast(e.Row.FindControl("ImageButtonLinkToPDFcopy"), ImageButton).ImageUrl = "~/Images/folder.png"
                Else
                    If IO.Directory.Exists(path) Then
                        DirectCast(e.Row.FindControl("ImageButtonLinkToPDFcopy"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonLinkToPDFcopy"), ImageButton).ImageUrl = "~/Images/folder.png"
                    Else
                        DirectCast(e.Row.FindControl("ImageButtonLinkToPDFcopy"), ImageButton).Visible = False
                    End If
                End If

            End If
        Catch ex As Exception

        End Try

        If DirectCast(e.Row.FindControl("ImageButtonBudgetPDF"), ImageButton) IsNot Nothing Then

            If System.IO.File.Exists(Server.MapPath(DataBinder.Eval(e.Row.DataItem, "BudgetLinkToPDF").ToString)) Then
                If Right(DataBinder.Eval(e.Row.DataItem, "BudgetLinkToPDF").ToString, 3).ToString = "pdf" Then
                    DirectCast(e.Row.FindControl("ImageButtonBudgetPDF"), ImageButton).ImageUrl = "~/Images/pdf.bmp"
                ElseIf Right(DataBinder.Eval(e.Row.DataItem, "BudgetLinkToPDF").ToString, 3).ToString = "doc" Then
                    DirectCast(e.Row.FindControl("ImageButtonBudgetPDF"), ImageButton).ImageUrl = "~/Images/Word.jpg"
                ElseIf Right(DataBinder.Eval(e.Row.DataItem, "BudgetLinkToPDF").ToString, 4).ToString = "docx" Then
                    DirectCast(e.Row.FindControl("ImageButtonBudgetPDF"), ImageButton).ImageUrl = "~/Images/Word.jpg"
                End If
            Else
                DirectCast(e.Row.FindControl("ImageButtonBudgetPDF"), ImageButton).Visible = False

            End If

        End If

        If DirectCast(e.Row.FindControl("LabelSortNumber"), Label) IsNot Nothing Then
            DirectCast(e.Row.FindControl("LabelSortNumber"), Label).Text = e.Row.RowIndex.ToString() + 1
        End If

        ' HIDE-SHOW Item control buttons  depends on the role situation 
        HIDE_SHOW_edit_delete_AddAdendum_RaisePo_buttons_depends_on_the_role_situation_Contract(e.Row)

        ' store session to check Client and supplier comtability
        Dim SupplierIDTextBoxEdit As TextBox = DirectCast(e.Row.FindControl("SupplierIDTextBox"), TextBox)
        If SupplierIDTextBoxEdit IsNot Nothing Then
            Session("SupplierID1") = SupplierIDTextBoxEdit.Text
        End If

        ' Decide edit controls to be enabled or not depending on user Role
        If (e.Row.RowState And DataControlRowState.Edit) > 0 Then
            Decide_edit_controls_to_be_enabled_or_not_depending_on_user_Role_Contract(e.Row)
        End If

        ' take original PO_no to evaluate on updating event
        Dim DropDownListPOnoEdit_ As DropDownList = DirectCast(e.Row.FindControl("DropDownListPOnoEdit"), DropDownList)
        If DropDownListPOnoEdit_ IsNot Nothing Then
            DirectCast(e.Row.FindControl("LabelPoNoBeforeEdit"), Label).Text = DataBinder.Eval(e.Row.DataItem, "PO_No").ToString
        End If

        ' Check SupplierID Edit box if user can update or not depending on Po count situation
        Dim TextBoxPO_Count As TextBox = DirectCast(e.Row.FindControl("TextBoxPO_Count"), TextBox)
        If Roles.IsUserInRole("Contract") OrElse Roles.IsUserInRole("ContractEditPo") AndAlso DropDownListPOnoEdit_ IsNot Nothing Then
            If TextBoxPO_Count IsNot Nothing Then
                If Convert.ToInt32(TextBoxPO_Count.Text) > 0 Then
                    SupplierIDTextBoxEdit.Enabled = False
                End If
            End If
        End If

        ' To show number of addendums under the contract
        Dim LinkButtonNumberOfAddendum As LinkButton = DirectCast(e.Row.FindControl("LinkButtonNumberOfAddendum"), LinkButton)

        If LinkButtonNumberOfAddendum IsNot Nothing Then
            If _AddendumCount > 0 Then
                LinkButtonNumberOfAddendum.Text = "#" + _AddendumCount.ToString() + " " + BodyTexts.Ref("U8IZNfQXVEKjw+Zetzwq/Q")
            Else
                LinkButtonNumberOfAddendum.Visible = False
            End If
        End If

        ' it evaluates SupplierID textbox and show supplier name or error
        Dim SupplierIDTextBox As TextBox = DirectCast(e.Row.FindControl("SupplierIDTextBox"), TextBox)
        Dim LabelSupplierNameInEdit As Label = DirectCast(e.Row.FindControl("LabelSupplierNameInEdit"), Label)
        Dim TextBoxToValidate As TextBox = DirectCast(e.Row.FindControl("TextBoxToValidate"), TextBox)


        If SupplierIDTextBox IsNot Nothing Then
            ' it reserves Index number
            LabelEditModeIndex.Text = e.Row.RowIndex.ToString
        End If


        If SupplierIDTextBox IsNot Nothing Then
            If SupplierNameCarrier = "" Then
                SqlDataSourceforSupplierNameCheck.SelectParameters("SupplierID").DefaultValue = Left(SupplierIDTextBox.Text.ToString, 12)
                SqlDataSourceforSupplierNameCheck.SelectParameters("SupplierID").Type = TypeCode.String
                DDLforSupplierNameCheck.DataBind()
                LabelSupplierNameInEdit.Text = DDLforSupplierNameCheck.SelectedItem.ToString
                LabelSupplierNameInEdit.ForeColor = System.Drawing.ColorTranslator.FromHtml("#BA55D3")
                SupplierIDTextBox.Text = Left(SupplierIDTextBox.Text.ToString, 12)
            Else
                SqlDataSourceforSupplierNameCheck.SelectParameters("SupplierID").DefaultValue = Left(SupplierNameCarrier.ToString, 12)
                SupplierIDTextBox.Text = Left(SupplierNameCarrier.ToString, 12)

                SqlDataSourceforSupplierNameCheck.SelectParameters("SupplierID").Type = TypeCode.String

                DDLforSupplierNameCheck.DataBind()

                If DDLforSupplierNameCheck.Items.Count = 0 Then
                    LabelSupplierNameInEdit.Text = BodyTexts.Ref("B6PzDRT/zEqApdFc6aDA3g")
                    LabelSupplierNameInEdit.ForeColor = System.Drawing.Color.Red
                    TextBoxToValidate.Text = BodyTexts.Ref("B6PzDRT/zEqApdFc6aDA3g")
                Else
                    LabelSupplierNameInEdit.Text = DDLforSupplierNameCheck.SelectedItem.ToString
                    LabelSupplierNameInEdit.ForeColor = System.Drawing.ColorTranslator.FromHtml("#BA55D3")
                    TextBoxToValidate.Text = BodyTexts.Ref("ushyBMdsI0GWMsqeqFJNWg")
                End If

                ' start to transfer all items from TextChanged event
                Dim li1 As ListItem = DirectCast(e.Row.FindControl("DDLProjectEdit"), DropDownList).Items.FindByValue(ProjectName)
                If li1 IsNot Nothing Then
                    DirectCast(e.Row.FindControl("DDLProjectEdit"), DropDownList).SelectedIndex = DirectCast(e.Row.FindControl("DDLProjectEdit"), DropDownList).Items.IndexOf(li1)
                End If

                Dim li2 As ListItem = DirectCast(e.Row.FindControl("DropDownListContractTypeEdit"), DropDownList).Items.FindByValue(ContractType)
                If li2 IsNot Nothing Then
                    DirectCast(e.Row.FindControl("DropDownListContractTypeEdit"), DropDownList).SelectedIndex = DirectCast(e.Row.FindControl("DropDownListContractTypeEdit"), DropDownList).Items.IndexOf(li2)
                End If

                Dim li3 As ListItem = DirectCast(e.Row.FindControl("DropDownListCurrencyEdit"), DropDownList).Items.FindByValue(ContractCurrency)
                If li3 IsNot Nothing Then
                    DirectCast(e.Row.FindControl("DropDownListCurrencyEdit"), DropDownList).SelectedIndex = DirectCast(e.Row.FindControl("DropDownListCurrencyEdit"), DropDownList).Items.IndexOf(li3)
                End If

                DirectCast(e.Row.FindControl("TextBoxRetention"), TextBox).Text = Retention
                DirectCast(e.Row.FindControl("TextBoxContractNoEdit"), TextBox).Text = ContractNo
                DirectCast(e.Row.FindControl("TextBoxContractDateEdit"), TextBox).Text = ContractDate
                DirectCast(e.Row.FindControl("TextBoxContractDescriptionEdit"), TextBox).Text = ContractDescription

                If ContractValue = 0.0 Then
                    DirectCast(e.Row.FindControl("TextBoxContractValueEdit"), TextBox).Text = ""
                Else
                    DirectCast(e.Row.FindControl("TextBoxContractValueEdit"), TextBox).Text = ContractValue
                End If

                DirectCast(e.Row.FindControl("TextBoxNoteEdit"), TextBox).Text = Note
                DirectCast(e.Row.FindControl("LinkToTemplatefile_DOCTextBoxEdit"), TextBox).Text = LinkToDOC
                DirectCast(e.Row.FindControl("LinkToPDFcopyTextBoxEdit"), TextBox).Text = LinkToPDF

                DirectCast(e.Row.FindControl("SignBySupplierCheckBoxEdit"), CheckBox).Checked = SignedBySupplier
                DirectCast(e.Row.FindControl("SignByMercuryCheckBoxEdit"), CheckBox).Checked = SignedByMercury
                DirectCast(e.Row.FindControl("CollectionBySupplierCheckBoxEdit"), CheckBox).Checked = CollectedBySupplier
                DirectCast(e.Row.FindControl("TextBoxContractGivenTo"), TextBox).Text = ContractGivenTo
                DirectCast(e.Row.FindControl("ArchivedByMercuryCheckBoxEdit"), CheckBox).Checked = Archived

            End If
        End If

        ' it provides hover effect
        Dim LabelHooverToolTipContract As Label = DirectCast(e.Row.FindControl("LabelHooverToolTipContract"), Label)
        Dim LabelCreatedBy As Label = DirectCast(e.Row.FindControl("LabelCreatedBy"), Label)
        Dim LabelUpdatedBy As Label = DirectCast(e.Row.FindControl("LabelUpdatedBy"), Label)
        Dim LabelPersonCreated As Label = DirectCast(e.Row.FindControl("LabelPersonCreated"), Label)
        Dim LabelPersonUpdated As Label = DirectCast(e.Row.FindControl("LabelPersonUpdated"), Label)

        If LabelHooverToolTipContract IsNot Nothing Then
            If LabelCreatedBy.Text <> "" AndAlso LabelUpdatedBy.Text = "" Then
                LabelHooverToolTipContract.Text = "________" + LabelPersonCreated.Text + " " + BodyTexts.Ref("Lo804eOFgEWuqE9mzDqRKA") + " " + LabelCreatedBy.Text
            End If
        End If

        If LabelHooverToolTipContract IsNot Nothing Then
            If LabelCreatedBy.Text <> "" AndAlso LabelUpdatedBy.Text <> "" Then
                LabelHooverToolTipContract.Text = "________" + LabelPersonCreated.Text + " " + BodyTexts.Ref("Lo804eOFgEWuqE9mzDqRKA") + " " + LabelCreatedBy.Text + ", " + BodyTexts.Ref("dZFrjB5AC0SY9RVgUdmHgA") + ": " + LabelPersonUpdated.Text + " " + BodyTexts.Ref("Dw9Srpu14ky6r9CxlBeJpw") + " " + LabelUpdatedBy.Text
            End If
        End If

        If LabelHooverToolTipContract IsNot Nothing Then
            If LabelCreatedBy.Text = "" AndAlso LabelUpdatedBy.Text = "" Then
                LabelHooverToolTipContract.Text = "________" + BodyTexts.Ref("tg9jeQD/GEWPUOLYI+DnQA")
            End If
        End If

        If LabelHooverToolTipContract IsNot Nothing Then
            If LabelCreatedBy.Text = "" AndAlso LabelUpdatedBy.Text <> "" Then
                LabelHooverToolTipContract.Text = "________" + BodyTexts.Ref("tg9jeQD/GEWPUOLYI+DnQA") + ", " + BodyTexts.Ref("dZFrjB5AC0SY9RVgUdmHgA") + ": " + LabelPersonUpdated.Text + " " + BodyTexts.Ref("Dw9Srpu14ky6r9CxlBeJpw") + " " + LabelUpdatedBy.Text
            End If
        End If

        ' it provides select parameters for SqlDataSourcePoNoEdit
        Dim SqlDataSourcePoNo As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourcePoNo"), SqlDataSource)
        If SqlDataSourcePoNo IsNot Nothing Then
            SqlDataSourcePoNo.SelectParameters("Project_ID").DefaultValue = _ProjectID
            SqlDataSourcePoNo.SelectParameters("SupplierID").DefaultValue = DirectCast(e.Row.FindControl("SupplierIDTextBox"), TextBox).Text
        End If

        Dim DropDownListPOnoEdit As DropDownList = DirectCast(e.Row.FindControl("DropDownListPOnoEdit"), DropDownList)
        If DropDownListPOnoEdit IsNot Nothing Then
            If Len(DataBinder.Eval(e.Row.DataItem, "PO_No").ToString) = 0 Then
                DropDownListPOnoEdit.SelectedValue = ""
            Else
                DropDownListPOnoEdit.SelectedValue = DataBinder.Eval(e.Row.DataItem, "PO_No")
            End If
        End If

        ' it provides a warning if contract value doesnt exist while we have po against that.
        Dim LabelPOnoItem As Label = DirectCast(e.Row.FindControl("LabelPOnoItem"), Label)
        Dim LabelContractValueItem As Label = DirectCast(e.Row.FindControl("LabelContractValueItem"), Label)
        If LabelContractValueItem IsNot Nothing And
            _NewGeneration = False Then
            If Len(LabelPOnoItem.Text) > 0 Then
                If Len(LabelContractValueItem.Text) = 0 Then
                    LabelContractValueItem.Text = "<div style=" + """" + "border: medium solid #FF0000; padding: 2px; text-align: left; background-color: #FFE4E1;" + """" + "><p><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "font-weight: bold; font-size: 10px; color: #ff0000; " + """" + ">There is a PO against this contract but no Contract Value defined yet? Ple</span></font><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "font-weight: bold; font-size: 10px; color: #ff0000; " + """" + ">ase</span></font><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "color: #cc0000; font-weight: bold; font-size: 10px; " + """" + "> </span></font><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "font-weight: bold; color: #0000ff; font-size: 10pt; " + """" + ">click edit</span></font><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "font-weight: bold; font-size: 10px; color: #ff0000; " + """" + "> to see</span></font><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "font-weight: bold; font-size: 10px; color: #ff0000; " + """" + "> " + BodyTexts.Ref("KVeyhXa6/EGKIPl+KmVhmQ") + ".</span></font></p></div>"
                ElseIf Convert.ToDecimal(LabelContractValueItem.Text.Replace(",", "")) = 0 Then
                    LabelContractValueItem.Text = "<div style=" + """" + "border: medium solid #FF0000; padding: 2px; text-align: left; background-color: #FFE4E1;" + """" + "><p><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "font-weight: bold; font-size: 10px; color: #ff0000; " + """" + ">There is a PO against this contract but no Contract Value defined yet? Ple</span></font><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "font-weight: bold; font-size: 10px; color: #ff0000; " + """" + ">ase</span></font><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "color: #cc0000; font-weight: bold; font-size: 10px; " + """" + "> </span></font><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "font-weight: bold; color: #0000ff; font-size: 10pt; " + """" + ">click edit</span></font><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "font-weight: bold; font-size: 10px; color: #ff0000; " + """" + "> to see</span></font><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "font-weight: bold; font-size: 10px; color: #ff0000; " + """" + "> " + BodyTexts.Ref("KVeyhXa6/EGKIPl+KmVhmQ") + ".</span></font></p></div>"
                End If
            End If
        End If

        ' it provides Project Name if ALL Project Selected
        Dim LabelProjectNameItem As Label = DirectCast(e.Row.FindControl("LabelProjectNameItem"), Label)
        'If DropDownListPrjID.SelectedValue = 0 Then
        If LabelProjectNameItem IsNot Nothing Then
            LabelProjectNameItem.Text = "<span style=" + """" + "color: #0099ff; font-size: 9px; font-weight: bold" + """" + ">" + DataBinder.Eval(e.Row.DataItem, "ProjectName").ToString + "</span>"
        End If
        'End If

        ' HIDE unnecessary controls for PO items without any contract
        Dim LinkButtonAddAddendum As LinkButton = DirectCast(e.Row.FindControl("LinkButtonAddAddendum"), LinkButton)
        Dim ButtonEdit As Button = DirectCast(e.Row.FindControl("ButtonEdit"), Button)
        Dim ButtonDelete As Button = DirectCast(e.Row.FindControl("ButtonDelete"), Button)

        If e.Row.RowType = DataControlRowType.DataRow Then
            If DataBinder.Eval(e.Row.DataItem, "ContractID") = 0 Then
                Dim LabelContractTypeItem As Label = DirectCast(e.Row.FindControl("LabelContractTypeItem"), Label)
                Dim LabelSortNumber As Label = DirectCast(e.Row.FindControl("LabelSortNumber"), Label)
                e.Row.BackColor = System.Drawing.Color.SkyBlue
                LabelPOnoItem.BackColor = System.Drawing.Color.White
                LabelPOnoItem.Font.Bold = True
                LabelPOnoItem.BorderStyle = BorderStyle.Solid
                LabelPOnoItem.BorderColor = System.Drawing.Color.Black
                LabelPOnoItem.BorderWidth = 2
                LinkButtonAddAddendum.Visible = False
                ButtonEdit.Visible = False
                'CheckThisLine(DataBinder.Eval(e.Row.DataItem, "ContractID"), 744)
                ButtonDelete.Visible = False
                LinkButtonAddAddendum.Visible = False
                LabelContractTypeItem.Visible = False
                LabelSortNumber.Visible = False
            End If
        End If

        ' ==========================================================================================================

        '               THESE PARTS ADDED AFTER APPROVAL MATRIX. SOME CODE ABOVE MAY BE CHANGED

        ' ==========================================================================================================

        ' Commercial items
        Dim DropDownListPenalty As DropDownList = DirectCast(e.Row.FindControl("DropDownListPenalty"), DropDownList)
        If DropDownListPenalty IsNot Nothing Then
            If IsDBNull(DataBinder.Eval(e.Row.DataItem, "Penalties")) Then
                DropDownListPenalty.SelectedIndex = 0
            ElseIf DataBinder.Eval(e.Row.DataItem, "Penalties") = True Then
                DropDownListPenalty.SelectedValue = 1
            ElseIf DataBinder.Eval(e.Row.DataItem, "Penalties") = False Then
                DropDownListPenalty.SelectedValue = 0
            End If
        End If

        Dim DropDownListPenaltyToSupplier As DropDownList = DirectCast(e.Row.FindControl("DropDownListPenaltyToSupplier"), DropDownList)
        If DropDownListPenaltyToSupplier IsNot Nothing Then
            If IsDBNull(DataBinder.Eval(e.Row.DataItem, "PenaltiesToSupplier")) Then
                DropDownListPenaltyToSupplier.SelectedIndex = 0
            ElseIf DataBinder.Eval(e.Row.DataItem, "PenaltiesToSupplier") = True Then
                DropDownListPenaltyToSupplier.SelectedValue = 1
            ElseIf DataBinder.Eval(e.Row.DataItem, "PenaltiesToSupplier") = False Then
                DropDownListPenaltyToSupplier.SelectedValue = 0
            End If
        End If

        ' hide PO match control if project is new generation
        If DropDownListPOnoEdit_ IsNot Nothing Then
            If _NewGeneration = True Then
                DropDownListPOnoEdit_.Visible = False
            ElseIf _NewGeneration = False Then
                DropDownListPOnoEdit_.Visible = True
            End If
        End If

        ' Define select parameter for approval matrix
        Dim SqlDataSourceApprovalStatus As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourceApprovalStatus"), SqlDataSource)
        Dim SqlDataSourceOffers As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourceOffers"), SqlDataSource)
        Dim SqlDataSourceMissingItemsForApproval As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourceMissingItemsForApproval"), SqlDataSource)

        If SqlDataSourceApprovalStatus IsNot Nothing Then
            SqlDataSourceApprovalStatus.SelectParameters("ContractID").DefaultValue =
              DataBinder.Eval(e.Row.DataItem, "ContractID")
        End If

        If SqlDataSourceOffers IsNot Nothing Then
            SqlDataSourceOffers.SelectParameters("ContractID").DefaultValue =
              DataBinder.Eval(e.Row.DataItem, "ContractID")
        End If

        Dim PanelFrameContract As Panel = DirectCast(e.Row.FindControl("PanelFrameContract"), Panel)
        If PanelFrameContract IsNot Nothing Then
            If DataBinder.Eval(e.Row.DataItem, "FrameContract") Then
                PanelFrameContract.Visible = True
            Else
                PanelFrameContract.Visible = False
            End If
        End If

        Dim ImageRequestedBy As UserControl = DirectCast(e.Row.FindControl("ImageRequestedBy"), UserControl)
        If ImageRequestedBy IsNot Nothing Then
            If IsDBNull(DataBinder.Eval(e.Row.DataItem, "RequestedBy")) OrElse String.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "RequestedBy")) Then
                ImageRequestedBy.Visible = False
            Else
                ImageRequestedBy.Visible = True
            End If
        End If

        ' This is updated after small Contract allowance.
        If DataBinder.Eval(e.Row.DataItem, "Scenario") >= 1 OrElse DataBinder.Eval(e.Row.DataItem, "Scenario") = -1 Then
            If SqlDataSourceMissingItemsForApproval IsNot Nothing Then
                SqlDataSourceMissingItemsForApproval.SelectParameters("ContractID").DefaultValue =
                  DataBinder.Eval(e.Row.DataItem, "ContractID")
            End If
        End If

        ' If Not New Generation Project, HIDE Commercial Section both in Item and Edit Mode
        Dim PanelCommercialTermsContractItem As Panel = DirectCast(e.Row.FindControl("PanelCommercialTermsContractItem"), Panel)
        Dim PanelCommercialTermsContractEdit As Panel = DirectCast(e.Row.FindControl("PanelCommercialTermsContractEdit"), Panel)

        If _NewGeneration = True Then
            If PanelCommercialTermsContractItem IsNot Nothing Then
                PanelCommercialTermsContractItem.Visible = True
            End If
            If PanelCommercialTermsContractEdit IsNot Nothing Then
                PanelCommercialTermsContractEdit.Visible = True
            End If
        Else
            If PanelCommercialTermsContractItem IsNot Nothing Then
                PanelCommercialTermsContractItem.Visible = False
            End If
            If PanelCommercialTermsContractEdit IsNot Nothing Then
                PanelCommercialTermsContractEdit.Visible = False
            End If
        End If


        ' Decide which Contract Value TextBox to show? WITH VAT or EXC VAT
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' --------------------------------------------- EDIT MODE ---------------------------------------------
            ' Titles
            Dim LiteralContractValueExcVAT As Literal = DirectCast(e.Row.FindControl("LiteralContractValueExcVAT"), Literal)
            Dim LiteralContractValueWithVAT As Literal = DirectCast(e.Row.FindControl("LiteralContractValueWithVAT"), Literal)
            Dim LiteralVAT As Literal = DirectCast(e.Row.FindControl("LiteralVAT"), Literal)
            Dim LiteralBudgetEdit As Literal = DirectCast(e.Row.FindControl("LiteralBudgetEdit"), Literal)
            ' Controls
            Dim TextBoxContractValueExcEdit As TextBox = DirectCast(e.Row.FindControl("TextBoxContractValueEdit"), TextBox)
            Dim TextBoxContractValueWithVATEdit_ As TextBox = DirectCast(e.Row.FindControl("TextBoxContractValueWithVATEdit"), TextBox)
            Dim TextBoxVAT_ As TextBox = DirectCast(e.Row.FindControl("TextBoxVAT"), TextBox)
            Dim TextBoxBudget As TextBox = DirectCast(e.Row.FindControl("TextBoxBudget"), TextBox)
            ' Validations
            Dim RequiredFieldValidatorTextBoxVAT As RequiredFieldValidator = DirectCast(e.Row.FindControl("RequiredFieldValidatorTextBoxVAT"), RequiredFieldValidator)
            Dim RequiredFieldValidatorContractValueWithVATEdit As RequiredFieldValidator = DirectCast(e.Row.FindControl("RequiredFieldValidatorContractValueWithVATEdit"), RequiredFieldValidator)

            If DataBinder.Eval(e.Row.DataItem, "Scenario") >= 1 And LiteralContractValueExcVAT IsNot Nothing Then
                ' With VAT controls to show
                ' Titles
                LiteralContractValueExcVAT.Visible = False
                LiteralContractValueWithVAT.Visible = True
                LiteralVAT.Visible = True
                LiteralBudgetEdit.Visible = True
                ' Controls
                TextBoxContractValueExcEdit.Visible = False
                TextBoxContractValueWithVATEdit_.Visible = True
                TextBoxVAT_.Visible = True
                TextBoxBudget.Visible = True
            ElseIf DataBinder.Eval(e.Row.DataItem, "Scenario") < 1 _
              And DataBinder.Eval(e.Row.DataItem, "Scenario") <> -1 _
                And LiteralContractValueExcVAT IsNot Nothing Then
                ' EXC VAT controls to show
                ' Titles
                LiteralContractValueExcVAT.Visible = True
                LiteralContractValueWithVAT.Visible = False
                LiteralVAT.Visible = False
                LiteralBudgetEdit.Visible = False
                ' Controls
                TextBoxContractValueExcEdit.Visible = True
                TextBoxContractValueWithVATEdit_.Visible = False
                TextBoxVAT_.Visible = False
                TextBoxBudget.Visible = False
                ' Validation
                RequiredFieldValidatorTextBoxVAT.Enabled = False
                RequiredFieldValidatorContractValueWithVATEdit.Enabled = False
            ElseIf DataBinder.Eval(e.Row.DataItem, "Scenario") = -1 And LiteralContractValueExcVAT IsNot Nothing Then
                ' EXC VAT controls to show
                ' Titles
                LiteralContractValueExcVAT.Visible = False
                LiteralContractValueWithVAT.Visible = True
                LiteralVAT.Visible = True
                LiteralBudgetEdit.Visible = True
                ' Controls
                TextBoxContractValueExcEdit.Visible = False
                TextBoxContractValueWithVATEdit_.Visible = True
                TextBoxVAT_.Visible = True
                TextBoxContractValueWithVATEdit_.Enabled = False
                TextBoxVAT_.Enabled = False
                TextBoxBudget.Visible = True
                ' Validation
                RequiredFieldValidatorTextBoxVAT.Enabled = False
                RequiredFieldValidatorContractValueWithVATEdit.Enabled = False

            End If

            ' ---------------------------------------------  ITEM MODE---------------------------------------------
            ' Titles
            Dim LiteralContractValueExcVATItem As Literal = DirectCast(e.Row.FindControl("LiteralContractValueExcVATItem"), Literal)
            Dim LiteralContractValueWithVATItem As Literal = DirectCast(e.Row.FindControl("LiteralContractValueWithVATItem"), Literal)
            Dim LiteralVATItem As Literal = DirectCast(e.Row.FindControl("LiteralVATItem"), Literal)
            Dim LiteralBudgetTitle As Literal = DirectCast(e.Row.FindControl("LiteralBudgetTitle"), Literal)
            ' Controls
            Dim LabelContractValueItemValue As Label = DirectCast(e.Row.FindControl("LabelContractValueItem"), Label)
            Dim LiteralContractValueWithVATItemValue As Literal = DirectCast(e.Row.FindControl("LiteralContractValueWithVATItemValue"), Literal)
            Dim LiteralVATItemValue As Literal = DirectCast(e.Row.FindControl("LiteralVATItemValue"), Literal)
            Dim LiteralBudgetValue As Literal = DirectCast(e.Row.FindControl("LiteralBudgetValue"), Literal)

            If DataBinder.Eval(e.Row.DataItem, "Scenario") >= 1 And LiteralContractValueExcVATItem IsNot Nothing Then
                ' With VAT controls to show
                ' Titles
                LiteralContractValueExcVATItem.Visible = False
                LiteralContractValueWithVATItem.Visible = True
                LiteralVATItem.Visible = True
                LiteralBudgetTitle.Visible = True
                ' Controls
                LabelContractValueItemValue.Visible = False
                LiteralContractValueWithVATItemValue.Visible = True
                LiteralVATItemValue.Visible = True
                LiteralBudgetValue.Visible = True
                If String.IsNullOrEmpty(LiteralBudgetValue.Text) = True Then
                    LiteralBudgetValue.Text = BodyTexts.Ref("17X0eiQ29kWDycnVbeJR+w")
                End If
            ElseIf DataBinder.Eval(e.Row.DataItem, "Scenario") < 1 And LiteralContractValueExcVATItem IsNot Nothing Then
                ' EXC VAT controls to show
                ' Titles
                LiteralContractValueExcVATItem.Visible = True
                LiteralContractValueWithVATItem.Visible = False
                LiteralVATItem.Visible = False
                LiteralBudgetTitle.Visible = False
                ' Controls
                LabelContractValueItemValue.Visible = True
                LiteralContractValueWithVATItemValue.Visible = False
                LiteralVATItemValue.Visible = False
                LiteralBudgetValue.Visible = False
            End If

        End If

        ' HIDE PROJECT and PO DDL FROM NEW GENERATION PROJECTS
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim DDLProjectEdit As DropDownList = DirectCast(e.Row.FindControl("DDLProjectEdit"), DropDownList)

            If DDLProjectEdit IsNot Nothing Then
                If _NewGeneration = True Then
                    DDLProjectEdit.CssClass = "hidepanel"
                    DropDownListPOnoEdit.CssClass = "hidepanel"
                Else
                    DDLProjectEdit.Visible = True
                End If
            End If
        End If

        ' LinkButtonAddAddendum PostBackURL as per NEW GENERATION PROJECTS
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LinkButtonAddAddendum2 As LinkButton = DirectCast(e.Row.FindControl("LinkButtonAddAddendum"), LinkButton)
            If LinkButtonAddAddendum2 IsNot Nothing Then
                If _NewGeneration = True Then
                    LinkButtonAddAddendum2.PostBackUrl = "~/webforms/addendumenterNew.aspx"
                Else
                    LinkButtonAddAddendum2.PostBackUrl = "~/webforms/addendumenter.aspx"
                End If
            End If
        End If

        ' THIS IS MILESTONE. DONT DO ANYTHING YET
        ' DETERMINE if Contract Ready To Turn into Purchase Order
        If e.Row.RowType = DataControlRowType.DataRow Then
            If _POexecuted And
                _Exceptional = False Then
                ' if po executed then backcolor to be highlighted
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#DCFBC8")

                ' disable Approval gridview, it is approved.
                Dim GridviewApprovalStatus As GridView = DirectCast(e.Row.FindControl("GridviewApprovalStatus"), GridView)
                If GridviewApprovalStatus IsNot Nothing Then
                    GridviewApprovalStatus.Enabled = False
                End If

                If DataBinder.Eval(e.Row.DataItem, "Scenario") > 0 Then
                    ' show message to user about PO status and its value
                    If DirectCast(e.Row.FindControl("LabelPoStatus"), Label) IsNot Nothing Then
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).Text = BodyTexts.Ref("MSZXoc5r7UCGY8sgVpFZug") + DataBinder.Eval(e.Row.DataItem, "Scenario").ToString
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).CssClass = "label label-success arrowed-in arrowed-in-right"
                        DirectCast(e.Row.FindControl("LabelReadyForPo"), Label).Text = POvalue(DataBinder.Eval(e.Row.DataItem, "ContractID"))
                    End If
                End If

                If DataBinder.Eval(e.Row.DataItem, "Scenario") = -1 Then
                    ' show message to user about PO status and its value
                    If DirectCast(e.Row.FindControl("LabelPoStatus"), Label) IsNot Nothing Then
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).Text = BodyTexts.Ref("LrPdBvnoeEO5I4mO5eWQ6A") + DataBinder.Eval(e.Row.DataItem, "Scenario").ToString
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).CssClass = "label label-success arrowed-in arrowed-in-right"
                        DirectCast(e.Row.FindControl("LabelReadyForPo"), Label).Text = "0"
                    End If
                End If

            ElseIf _POexecuted And
                _Exceptional = True Then
                ' if po EXCEPTIONALLY executed then backcolor to be highlighted
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFBBBB")

                ' if all required person approved, disable Approval control. If not, show. Because it still needs approval
                Dim GridviewApprovalStatus As GridView = DirectCast(e.Row.FindControl("GridviewApprovalStatus"), GridView)
                If GridviewApprovalStatus IsNot Nothing Then
                    If ContractView.AllRequiredPersonsApprovedContract(DataBinder.Eval(e.Row.DataItem, "ContractID"), 0) Then
                        GridviewApprovalStatus.Enabled = False
                    Else
                        GridviewApprovalStatus.Enabled = True
                    End If
                End If

                If DataBinder.Eval(e.Row.DataItem, "Scenario") > 0 Then
                    ' show message to user about PO status and its value
                    If DirectCast(e.Row.FindControl("LabelPoStatus"), Label) IsNot Nothing Then
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).Text = BodyTexts.Ref("MSZXoc5r7UCGY8sgVpFZug") + DataBinder.Eval(e.Row.DataItem, "Scenario").ToString
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).CssClass = "label label-success arrowed-in arrowed-in-right"
                        DirectCast(e.Row.FindControl("LabelReadyForPo"), Label).Text = POvalue(DataBinder.Eval(e.Row.DataItem, "ContractID"))
                    End If
                End If

                If DataBinder.Eval(e.Row.DataItem, "Scenario") = -1 Then
                    ' show message to user about PO status and its value
                    If DirectCast(e.Row.FindControl("LabelPoStatus"), Label) IsNot Nothing Then
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).Text = BodyTexts.Ref("LrPdBvnoeEO5I4mO5eWQ6A") + DataBinder.Eval(e.Row.DataItem, "Scenario").ToString
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).CssClass = "label label-success arrowed-in arrowed-in-right"
                        DirectCast(e.Row.FindControl("LabelReadyForPo"), Label).Text = "0"
                    End If
                End If

                ' Change the Exceptional Button text 
                Dim ButtonExceptionalApprove As Button = DirectCast(e.Row.FindControl("ButtonExceptionalApprove"), Button)
                If ButtonExceptionalApprove IsNot Nothing Then
                    ButtonExceptionalApprove.Visible = True
                    ButtonExceptionalApprove.Enabled = False
                End If

            Else

                If DirectCast(e.Row.FindControl("LabelPoStatus"), Label) IsNot Nothing Then
                    If _NewGeneration = True Then
                        ' this message to be shown only for NEW GENERATION projects.
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).Text = BodyTexts.Ref("k/u/FC7Hu0qixXN8P/BRAg") + DataBinder.Eval(e.Row.DataItem, "Scenario").ToString
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).CssClass = "label label-warning arrowed arrowed-right"
                    End If
                End If

            End If

            Dim lawyersComment As HtmlGenericControl = DirectCast(e.Row.FindControl("DivLawyerComment"), HtmlGenericControl)
            If lawyersComment IsNot Nothing Then
                If Len(Convert.ToString((DataBinder.Eval(e.Row.DataItem, "Remark")))) > 0 Then
                    lawyersComment.Visible = True
                Else
                    lawyersComment.Visible = False
                End If
            End If

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LiteralPenalties As Literal = DirectCast(e.Row.FindControl("LiteralPenalties"), Literal)
            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "Penalties")) Then
                If LiteralPenalties IsNot Nothing Then
                    If DataBinder.Eval(e.Row.DataItem, "Penalties") = True Then
                        LiteralPenalties.Text = BodyTexts.Ref("2TUx46Nv1UeQLerDF4GQjA")
                    ElseIf DataBinder.Eval(e.Row.DataItem, "Penalties") = False Then
                        LiteralPenalties.Text = BodyTexts.Ref("sl3la1Y+vUmYXJQRX9Mosw")
                    End If
                End If
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LiteralPenaltiesToSupplier As Literal = DirectCast(e.Row.FindControl("LiteralPenaltiesToSupplier"), Literal)
            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "PenaltiesToSupplier")) Then
                If LiteralPenaltiesToSupplier IsNot Nothing Then
                    If DataBinder.Eval(e.Row.DataItem, "PenaltiesToSupplier") = True Then
                        LiteralPenaltiesToSupplier.Text = BodyTexts.Ref("2TUx46Nv1UeQLerDF4GQjA")
                    ElseIf DataBinder.Eval(e.Row.DataItem, "PenaltiesToSupplier") = False Then
                        LiteralPenaltiesToSupplier.Text = BodyTexts.Ref("sl3la1Y+vUmYXJQRX9Mosw")
                    End If
                End If
            End If
        End If

        Dim _hyperlink As HyperLink = DirectCast(e.Row.FindControl("HyperlinkPoNo"), HyperLink)
        If _hyperlink IsNot Nothing Then
            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "PO_No")) Then
                _hyperlink.Visible = True
                If _OpenPOValue = 0 Then
                    _hyperlink.Enabled = False
                    _hyperlink.CssClass = "badge badge-grey cursor_notallowed"
                    _hyperlink.Attributes.Add("data-rel", "tooltip")
                    _hyperlink.Attributes.Add("data-original-title", BodyTexts.Ref("EKM2PaNpIEmON26Y4A2/sQ"))
                Else
                    If _InitiateContractAndAddendum = 1 Then
                        _hyperlink.Enabled = True
                        _hyperlink.CssClass = "badge badge-yellow"
                    Else
                        _hyperlink.Enabled = False
                        _hyperlink.CssClass = "badge badge-grey cursor_notallowed"
                        _hyperlink.Attributes.Add("data-rel", "tooltip")
                        _hyperlink.Attributes.Add("data-original-title", BodyTexts.Ref("qoDpxpXsBU+Q+c3/75kcZw"))
                    End If
                End If
            Else
                _hyperlink.Visible = False
            End If
        End If

        ' this is here for SAVAS temporarily.
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Page.User.Identity.Name.ToLower = "savas" Then
                If DirectCast(e.Row.FindControl("LabelContractIDitem"), Label) IsNot Nothing Then
                    DirectCast(e.Row.FindControl("LabelContractIDitem"), Label).Visible = True
                    DirectCast(e.Row.FindControl("LabelCvWthVAT"), Label).Visible = True
                    DirectCast(e.Row.FindControl("LabelContractNoItem"), Label).ForeColor = System.Drawing.Color.Brown
                    DirectCast(e.Row.FindControl("LabelContractNoItem"), Label).Font.Size = 14
                    DirectCast(e.Row.FindControl("LabelContractNoItem"), Label).Font.Underline = True
                    DirectCast(e.Row.FindControl("LabelContractNoItem"), Label).Text = DirectCast(e.Row.FindControl("LabelContractNoItem"), Label).Text + "<br/>"

                    If IsDBNull(DataBinder.Eval(e.Row.DataItem, "ContractValue_woVAT")) Then
                        DirectCast(e.Row.FindControl("LabelCvWthVAT"), Label).Text = "No Value"
                    Else
                        DirectCast(e.Row.FindControl("LabelCvWthVAT"), Label).Text = String.Format("{0:#,##0.00}", DataBinder.Eval(e.Row.DataItem, "ContractValue_woVAT") * 1.18)
                    End If
                End If
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            ' Show New Supplier Warning
            Dim panelNewSupplier As Panel = DirectCast(e.Row.FindControl("panelNewSupplier"), Panel)

            If panelNewSupplier IsNot Nothing Then

                If _SupplierNew = 0 And _ProjectID <> 999 Then
                    panelNewSupplier.Visible = True
                Else
                    panelNewSupplier.Visible = False
                End If
            End If

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LinkButtonAddAddendum_ As LinkButton = DirectCast(e.Row.FindControl("LinkButtonAddAddendum"), LinkButton)
            Dim ButtonEdit_ As Button = DirectCast(e.Row.FindControl("ButtonEdit"), Button)
            Dim ButtonDelete_ As Button = DirectCast(e.Row.FindControl("ButtonDelete"), Button)
            Dim LinkButtonRaisePO_ As LinkButton = DirectCast(e.Row.FindControl("LinkButtonRaisePO"), LinkButton)

            ' These are contracts to disable (Stroimastergroup, Alfabank)
            If DataBinder.Eval(e.Row.DataItem, "ContractID") = 2992 Or DataBinder.Eval(e.Row.DataItem, "ContractID") = 4565 Then

                LinkButtonAddAddendum_.Enabled = False
                ButtonEdit_.Enabled = False
                ButtonDelete_.Enabled = False
                LinkButtonRaisePO_.Enabled = False

            Else

                'bring here Common Contract logic
                If _CountCommon > 0 Then
                    If Roles.IsUserInRole("EnterPurchaseOrder") Then
                        '' show raise PO
                        LinkButtonRaisePO_.Enabled = True
                        LinkButtonRaisePO_.Visible = True

                    End If
                End If
            End If
        End If

        ' Activate PanelClientContractAdditionalDetails
        If _ProjectID = 999 Then
            If e.Row.RowType = DataControlRowType.DataRow Then
                ' item mode
                Dim PanelClientContractAdditionalDetails As Panel = DirectCast(e.Row.FindControl("PanelClientContractAdditionalDetails"), Panel)
                'Dim LiteralPenaltyClient As Literal = DirectCast(e.Row.FindControl("LiteralPenaltyClient"), Literal)

                If PanelClientContractAdditionalDetails IsNot Nothing Then
                    PanelClientContractAdditionalDetails.Visible = True
                End If

                ' edit mode
                Dim PanelClientContractAdditionalDetailsEdit As Panel = DirectCast(e.Row.FindControl("PanelClientContractAdditionalDetailsEdit"), Panel)

                If PanelClientContractAdditionalDetailsEdit IsNot Nothing Then
                    PanelClientContractAdditionalDetailsEdit.Visible = True
                End If

            End If
        End If

        If _ProjectID = 999 Then
            If e.Row.RowType = DataControlRowType.DataRow Then
                ' fixing new line problem
                'Dim LiteralDeliveryTermsClient As Literal = DirectCast(e.Row.FindControl("LiteralDeliveryTermsClient"), Literal)
                'Dim LiteralPenaltyNotesClient As Literal = DirectCast(e.Row.FindControl("LiteralPenaltyNotesClient"), Literal)
                'If LiteralDeliveryTermsClient IsNot Nothing Then
                '    LiteralDeliveryTermsClient.Text = DataBinder.Eval(e.Row.DataItem, "DeliveryTerms").ToString.Replace(Environment.NewLine, "<br />")
                '    LiteralPenaltyNotesClient.Text = DataBinder.Eval(e.Row.DataItem, "PenaltiesNote").ToString.Replace(Environment.NewLine, "<br />")
                'End If

                ' additional client data

                Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities
                    Dim ContractID As Int32 = DataBinder.Eval(e.Row.DataItem, "ContractID")

                    Dim clientAdditional = (From C In db.Table_Contracts_ClientAdditional Where C.ContractID = ContractID Select C.CompletionDate, C.AktOfWork, C.LinkToAktOfWork).ToList()
                    If clientAdditional.Count > 0 Then

                        'Item Template
                        Dim LiteralCompletionDate As Literal = DirectCast(e.Row.FindControl("LiteralCompletionDate"), Literal)
                        Dim CheckBoxAktOfWorkItem As UserControl = DirectCast(e.Row.FindControl("CheckBoxAktOfWorkItem"), UserControl)
                        Dim LabelLinkAktOfWorkContractItem As Label = DirectCast(e.Row.FindControl("LabelLinkAktOfWorkContractItem"), Label)
                        Dim ImageButtonClientContractAktToWorkContractItem As ImageButton = DirectCast(e.Row.FindControl("ImageButtonClientContractAktToWorkContractItem"), ImageButton)

                        If LabelLinkAktOfWorkContractItem IsNot Nothing Then
                            LabelLinkAktOfWorkContractItem.Text = clientAdditional(0).LinkToAktOfWork.Trim()
                            If LabelLinkAktOfWorkContractItem.Text.Trim().Length > 0 Then
                                ImageButtonClientContractAktToWorkContractItem.Visible = True
                            End If
                            Dim CheckBox_ As CheckBox = DirectCast(CheckBoxAktOfWorkItem.FindControl("CheckBox"), CheckBox)
                            CheckBox_.Checked = clientAdditional(0).AktOfWork
                        End If

                        If LiteralCompletionDate IsNot Nothing Then
                            'LiteralCompletionDate.Text = clientAdditional(0).CompletionDate
                            If Not clientAdditional(0).CompletionDate.HasValue Then
                                LiteralCompletionDate.Text = String.Empty
                            Else
                                LiteralCompletionDate.Text = clientAdditional(0).CompletionDate
                            End If

                        End If

                        'Edit Template
                        Dim TextBoxCompletionDateEdit As TextBox = DirectCast(e.Row.FindControl("TextBoxCompletionDateEdit"), TextBox)
                        Dim CheckBoxAktOfWorkEdit As UserControl = DirectCast(e.Row.FindControl("CheckBoxAktOfWorkEdit"), UserControl)
                        Dim LabelLinkAktOfWorkContract As Label = DirectCast(e.Row.FindControl("LabelLinkAktOfWorkContract"), Label)

                        If TextBoxCompletionDateEdit IsNot Nothing Then

                            'TextBoxCompletionDateEdit.Text = clientAdditional(0).CompletionDate

                            If Not clientAdditional(0).CompletionDate.HasValue Then
                                TextBoxCompletionDateEdit.Text = String.Empty
                            Else
                                TextBoxCompletionDateEdit.Text = clientAdditional(0).CompletionDate
                            End If

                            DirectCast(CheckBoxAktOfWorkEdit.FindControl("CheckBox"), CheckBox).Checked = clientAdditional(0).AktOfWork
                            LabelLinkAktOfWorkContract.Text = clientAdditional(0).LinkToAktOfWork.Trim
                        End If

                    End If

                    db.Dispose()

                End Using
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim tblCommonNewGN As HtmlTable = DirectCast(e.Row.FindControl("tblCommonNewGN"), HtmlTable)

            If tblCommonNewGN IsNot Nothing Then
                If _NewGeneration = True Then
                    tblCommonNewGN.Visible = True
                Else
                    tblCommonNewGN.Visible = False
                End If

            End If

            Dim PN As Label = DirectCast(e.Row.FindControl("LabelProjectNameItem"), Label)
            Dim ImageLogo As Image = DirectCast(e.Row.FindControl("ImageLogo"), Image)

            If PN IsNot Nothing Then
                ImageLogo.ImageUrl = _Logo
                PN.Visible = True
            End If

        End If

    End Sub

    Protected Sub InsertPoFromAddendum(ByVal _Project_ID As Integer _
                           , ByVal _AddendumID As Integer _
                           , ByVal _PersonCreated As String)

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "InsertPoFromAddendum"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim ProjectID As SqlParameter = cmd.Parameters.Add("@ProjectID", System.Data.SqlDbType.Int)
            Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", System.Data.SqlDbType.Int)
            Dim PersonCreated As SqlParameter = cmd.Parameters.Add("@PersonCreated", System.Data.SqlDbType.NVarChar)
            Dim outputIdParam As SqlParameter = cmd.Parameters.Add("@PO_No_out", System.Data.SqlDbType.NVarChar, 11)
            outputIdParam.Direction = System.Data.ParameterDirection.Output


            ' assign parameter values
            ProjectID.Value = _Project_ID
            AddendumID.Value = _AddendumID
            PersonCreated.Value = _PersonCreated

            cmd.ExecuteNonQuery()

            con.Close()

            ' send email notification
            If Not IsDBNull(outputIdParam.Value) Then
                'Parameter 3, PO raised from signed contract
                SendEmailApprovalMatrix.Send(_AddendumID, 3, outputIdParam.Value.ToString, "insert")
            End If

        End Using
    End Sub

    ' Dont remove at the moment. Probably you will never user this later, then will be deleted.
    Protected Function ContractExist(ByVal ContractID_ As Integer) As Boolean
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT count([ContractID]) FROM [Table_ContractClientData] WHERE ContractID = @ContractID "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", System.Data.SqlDbType.Int)
            ContractID.Value = ContractID_
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim returnValue As Boolean = False
            While dr.Read
                If dr(0) = 0 Then
                    returnValue = False
                ElseIf dr(0) = 1 Then
                    returnValue = True
                End If
            End While
            Return returnValue
            con.Close()
            dr.Close()
        End Using
    End Function

    Protected Sub Page_DataBinding(sender As Object, e As EventArgs) Handles Me.DataBinding

    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        TextBoxEntryInterval.Attributes.Add("placeholder", BodyTexts.Ref("KmvRdiNMB0CjdFDWlFkG2g"))
        TextBoxApprovalInterval.Attributes.Add("placeholder", BodyTexts.Ref("/BFB8a1YN0S3U9og0yYsEA"))
        TextBoxSearch.Attributes.Add("placeholder", BodyTexts.Ref("3Gzn7sYlDk6NSadpTd6Xyg"))

        btnSearchAdvance.Text = BodyTexts.Ref("x/slU4rYmkSvuBtKR49oQg")
        textBoxSearchInputContractNo.Attributes.Add("placeholder", BodyTexts.Ref("b/QwmB2AWEmXNrVeVHPG7g"))
        textBoxSearchInputContractDate.Attributes.Add("placeholder", BodyTexts.Ref("aUlY16DSxEuv5OYTuG/x+g"))

        If Not IsPostBack Then
            btnSearchContract.Attributes.Add("class", "btn btn-lg btn-success animate-on-load module")
        Else
            btnSearchContract.Attributes.Add("class", "btn btn-lg btn-success")
        End If

        If Not IsPostBack Then
            If Roles.IsUserInRole("Contract") Then
                Session("SupplierID1") = ""
                Session("SupplierID2") = ""
            End If
        End If

        If IsPostBack Or Not IsPostBack Then
            TextBoxUserName.Text = Page.User.Identity.Name
            SqlDataSourceForContractGrid.SelectParameters("SupplierID").ConvertEmptyStringToNull = False
        End If


        If IsPostBack Then
            If DDLpageSize.SelectedValue = "20" Then
                GridViewShowContract.AllowPaging = True
                GridViewShowContract.PagerSettings.Position = PagerPosition.TopAndBottom
                GridViewShowContract.PagerStyle.CssClass = "pager2"
                GridViewShowContract.PageSize = 20
            ElseIf DDLpageSize.SelectedValue = "40" Then
                GridViewShowContract.AllowPaging = True
                GridViewShowContract.PagerSettings.Position = PagerPosition.TopAndBottom
                GridViewShowContract.PagerStyle.CssClass = "pager2"
                GridViewShowContract.PageSize = 40
            ElseIf DDLpageSize.SelectedValue = "60" Then
                GridViewShowContract.AllowPaging = True
                GridViewShowContract.PagerSettings.Position = PagerPosition.TopAndBottom
                GridViewShowContract.PagerStyle.CssClass = "pager2"
                GridViewShowContract.PageSize = 60
            ElseIf DDLpageSize.SelectedValue = "ALL" Then
                GridViewShowContract.AllowPaging = False
            End If
        End If

        If Not IsPostBack Then
            If PreviousPage Is Nothing Then
                DropDownListPrjID.DataBind()
                DropDownListPrjID.SelectedValue = DropDownListPrjID.Items(0).Value
                'If DropDownListPrjID.Items.Count = 1 Then
                '    DropDownListPrjID.SelectedValue = DropDownListPrjID.Items(0).Value
                'Else
                '    DropDownListPrjID.SelectedValue = DropDownListPrjID.Items(1).Value
                'End If

                ' Refreash the latest contract No
                Using conLabel As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                    conLabel.Open()
                    Dim sqlstringLabel As String = " SELECT TOP 1 RTRIM(ContractNo) AS ContractNo FROM Table_Contracts " +
                                                    " WHERE ProjectID = " + DropDownListPrjID.SelectedValue.ToString +
                                                    " ORDER BY CreatedBy Desc "
                    Dim cmdLabel As New SqlCommand(sqlstringLabel, conLabel)
                    cmdLabel.CommandType = System.Data.CommandType.Text
                    Dim drLabel As SqlDataReader = cmdLabel.ExecuteReader
                    While drLabel.Read
                        LabelTheLatestContractNo.Text = "<span style=" + """" + "color: #000000; font-size: 10px; font-weight: bold" + """" + ">" + BodyTexts.Ref("kl/IJinKsU2uUj15K9hvNw") + "</span> <span style=" + """" + "color: #ff0000; font-size: 12px; font-weight: bold; border: medium solid #000000; padding: 1px" + """" + ">" + drLabel(0).ToString + "</span>"
                    End While
                    drLabel.Close()
                    conLabel.Close()
                End Using
                ' paging adjusted
                GridViewShowContract.AllowPaging = True
                GridViewShowContract.PagerSettings.Position = PagerPosition.TopAndBottom
                GridViewShowContract.PagerStyle.CssClass = "pager2"
                GridViewShowContract.PageSize = 20
                DDLpageSize.SelectedValue = 20

            ElseIf Not PreviousPage Is Nothing Then
                ' it transfer previous page onto here to run gridview on the same project....
                Dim previousPageURL As String = Request.UrlReferrer.AbsolutePath.ToString
                Dim PageFileInfo As New System.IO.FileInfo(previousPageURL)

                If PageFileInfo.Name = "Addendumenter.aspx" OrElse PageFileInfo.Name = "addendumenter.aspx" Then
                    Dim ContPlaceHold As ContentPlaceHolder = PreviousPage.Master.FindControl("MainContent")
                    Dim FormViewAddendums As FormView = ContPlaceHold.FindControl("FormViewAddendums")
                    Dim LabelProjectName As Label = FormViewAddendums.FindControl("LabelProjectName")
                    Dim LabelGridViewPagingStatusOnAddendum As Label = ContPlaceHold.FindControl("LabelGridViewPagingStatusOnAddendum")
                    Dim LabelGridViewPageSizeOnAddendum As Label = ContPlaceHold.FindControl("LabelGridViewPageSizeOnAddendum")
                    Dim LabelGridViewPageNumberOnAddendum As Label = ContPlaceHold.FindControl("LabelGridViewPageNumberOnAddendum")

                    DropDownListPrjID.DataBind()
                    DropDownListPrjID.Items.FindByText(LabelProjectName.Text).Selected = True

                    If LabelGridViewPagingStatusOnAddendum.Text = BodyTexts.Ref("2TUx46Nv1UeQLerDF4GQjA") Then
                        GridViewShowContract.AllowPaging = True
                        GridViewShowContract.PagerSettings.Position = PagerPosition.TopAndBottom
                        GridViewShowContract.PagerStyle.CssClass = "pager2"
                        GridViewShowContract.PageSize = Convert.ToInt32(LabelGridViewPageSizeOnAddendum.Text)
                        GridViewShowContract.PageIndex = Convert.ToInt32(LabelGridViewPageNumberOnAddendum.Text)
                    ElseIf LabelGridViewPagingStatusOnAddendum.Text = BodyTexts.Ref("sl3la1Y+vUmYXJQRX9Mosw") Then
                        GridViewShowContract.AllowPaging = False
                    End If
                    DDLpageSize.DataBind()
                    DDLpageSize.Items.FindByText(LabelGridViewPageSizeOnAddendum.Text).Selected = True

                    ' Refreash the latest contract No
                    Using conLabel As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                        conLabel.Open()
                        Dim sqlstringLabel As String = " SELECT TOP 1 RTRIM(ContractNo) AS ContractNo FROM Table_Contracts " +
                                                        " WHERE ProjectID = " + DropDownListPrjID.SelectedValue.ToString +
                                                        " ORDER BY CreatedBy Desc "
                        Dim cmdLabel As New SqlCommand(sqlstringLabel, conLabel)
                        cmdLabel.CommandType = System.Data.CommandType.Text
                        Dim drLabel As SqlDataReader = cmdLabel.ExecuteReader
                        While drLabel.Read
                            LabelTheLatestContractNo.Text = "<span style=" + """" + "color: #000000; font-size: 10px; font-weight: bold" + """" + ">" + BodyTexts.Ref("kl/IJinKsU2uUj15K9hvNw") + "</span> <span style=" + """" + "color: #ff0000; font-size: 12px; font-weight: bold; border: medium solid #000000; padding: 1px" + """" + ">" + drLabel(0).ToString + "</span>"
                        End While
                        drLabel.Close()
                        conLabel.Close()
                    End Using
                ElseIf PageFileInfo.Name.IndexOf("Addendumenter.aspx") = -1 OrElse PageFileInfo.Name.IndexOf("addendumenter.aspx") = -1 Then
                    GridViewShowContract.AllowPaging = True
                    GridViewShowContract.PagerSettings.Position = PagerPosition.TopAndBottom
                    GridViewShowContract.PagerStyle.CssClass = "pager2"
                    GridViewShowContract.PageSize = 20
                    DDLpageSize.SelectedValue = 20

                    DropDownListPrjID.DataBind()
                    If DropDownListPrjID.Items.Count = 1 Then
                        DropDownListPrjID.SelectedValue = DropDownListPrjID.Items(0).Value
                    Else
                        DropDownListPrjID.SelectedValue = DropDownListPrjID.Items(0).Value
                    End If

                End If
            End If
        ElseIf IsPostBack Then
            ' Refreash the latest contract No
            Using conLabel As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                conLabel.Open()
                Dim sqlstringLabel As String = " SELECT TOP 1 RTRIM(ContractNo) AS ContractNo FROM Table_Contracts " +
                                                " WHERE ProjectID = " + DropDownListPrjID.SelectedValue.ToString +
                                                " ORDER BY CreatedBy Desc "
                Dim cmdLabel As New SqlCommand(sqlstringLabel, conLabel)
                cmdLabel.CommandType = System.Data.CommandType.Text
                Dim drLabel As SqlDataReader = cmdLabel.ExecuteReader
                While drLabel.Read
                    LabelTheLatestContractNo.Text = "<span style=" + """" + "color: #000000; font-size: 10px; font-weight: bold" + """" + ">" + BodyTexts.Ref("kl/IJinKsU2uUj15K9hvNw") + "</span> <span style=" + """" + "color: #ff0000; font-size: 12px; font-weight: bold; border: medium solid #000000; padding: 1px" + """" + ">" + drLabel(0).ToString + "</span>"
                End While
                drLabel.Close()
                conLabel.Close()
            End Using
        End If


        ' it will decide on SqlDataSource SelectCommands depending on DDLproject
        If IsPostBack Then
            If DropDownListPrjID.SelectedValue = 0 Then
                LabelTheLatestContractNo.Text = ""

                SqlDataSourceSupplier.SelectCommand = " SELECT     TOP (100) PERCENT dbo.Table_Contracts.SupplierID, RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName " +
                " FROM         dbo.Table_Contracts INNER JOIN " +
                "                       dbo.Table6_Supplier ON dbo.Table_Contracts.SupplierID = dbo.Table6_Supplier.SupplierID INNER JOIN " +
                "                       dbo.Table1_Project ON dbo.Table_Contracts.ProjectID = dbo.Table1_Project.ProjectID " +
                " WHERE     (dbo.Table_Contracts.ProjectID IN " +
                "                           (SELECT     Table1_Project_1.ProjectID " +
                "                             FROM          dbo.Table1_Project AS Table1_Project_1 INNER JOIN " +
                "                                                    dbo.Table_Prj_User_Junction ON Table1_Project_1.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN " +
                "                                                    dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId INNER JOIN " +
                "                                                    dbo.Table_Contracts AS Table_Contracts_1 ON Table1_Project_1.ProjectID = Table_Contracts_1.ProjectID " +
                "                             GROUP BY Table1_Project_1.ProjectID, Table1_Project_1.ProjectName, dbo.aspnet_Users.UserName " +
                "                             HAVING      (dbo.aspnet_Users.UserName = N'" + Page.User.Identity.Name.ToString + "'))) " +
                " GROUP BY dbo.Table_Contracts.SupplierID, RTRIM(dbo.Table6_Supplier.SupplierName) " +
                " ORDER BY SupplierName "
            End If
        End If

        HiddenButtonExceptionalApproveShowOrNot.Value = CreateDataReader.Create_aspnet_Users(Page.User.Identity.Name.ToString.ToLower).AuthorizedForExceptionalApprove

    End Sub


    Protected Sub DropDownListSupplier_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListSupplier.DataBound
        Dim lst1 As New ListItem(BodyTexts.Ref("gpewEjB60EOKnX4h+Horiw"), String.Empty)
        DropDownListSupplier.Items.Insert(0, lst1)

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        GridViewExcel.DataSource = SqlDataSourceExcel
        GridViewExcel.DataBind()

        Dim filename As String = ""

        If DropDownListSupplier.SelectedValue = "" Then
            filename = DropDownListPrjID.SelectedItem.ToString + " " + BodyTexts.Ref("XyDq1Cduo0Wn9VDvNGQKXQ")
        Else
            filename = DropDownListPrjID.SelectedItem.ToString + " " + BodyTexts.Ref("/Nx/GCD57U+aKvH2wUCJTg") + " " + DropDownListSupplier.SelectedItem.ToString.Replace("*", "").Replace(".", "").Replace("/", "").Replace("\", "").Replace("[", "").Replace("]", "").Replace(":", "").Replace(";", "").Replace("|", "").Replace("=", "")
        End If

        PTSMainClass.ExportGridExcel(GridViewExcel, filename)

    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Exit Sub
    End Sub

    Protected Sub GridViewExcel_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewExcel.RowDataBound

        If DirectCast(e.Row.FindControl("CheckBoxSignByMercury"), CheckBox) IsNot Nothing Then
            If DirectCast(e.Row.FindControl("CheckBoxSignByMercury"), CheckBox).Checked = True Then
                DirectCast(e.Row.FindControl("LabelSignByMercury"), Label).Text = BodyTexts.Ref("tkDlbbplmkelESxzKrJfNw")
            Else
                DirectCast(e.Row.FindControl("LabelSignByMercury"), Label).Text = ""
            End If
        End If

        If DirectCast(e.Row.FindControl("CheckBoxSignBySupplier"), CheckBox) IsNot Nothing Then
            If DirectCast(e.Row.FindControl("CheckBoxSignBySupplier"), CheckBox).Checked = True Then
                DirectCast(e.Row.FindControl("LabelSignBySupplier"), Label).Text = BodyTexts.Ref("tkDlbbplmkelESxzKrJfNw")
            Else
                DirectCast(e.Row.FindControl("LabelSignBySupplier"), Label).Text = ""
            End If
        End If

        If DirectCast(e.Row.FindControl("CheckBoxArchivedByMercury"), CheckBox) IsNot Nothing Then
            If DirectCast(e.Row.FindControl("CheckBoxArchivedByMercury"), CheckBox).Checked = True Then
                DirectCast(e.Row.FindControl("LabelArchivedByMercury"), Label).Text = BodyTexts.Ref("XTAwgfczOEqcWRQlKaoBRA")
            Else
                DirectCast(e.Row.FindControl("LabelArchivedByMercury"), Label).Text = ""
            End If
        End If

        If DirectCast(e.Row.FindControl("LabelRetention"), Label) IsNot Nothing Then
            If String.IsNullOrEmpty(DirectCast(e.Row.FindControl("LabelRetention"), Label).Text) Then
                DirectCast(e.Row.FindControl("LabelRetention"), Label).Text = BodyTexts.Ref("jD//OczLHUixSBZ5Z3yTBQ")
            Else
                If Convert.ToDecimal(DirectCast(e.Row.FindControl("LabelRetention"), Label).Text) = 0.0 Then
                    DirectCast(e.Row.FindControl("LabelRetention"), Label).Text = BodyTexts.Ref("jD//OczLHUixSBZ5Z3yTBQ")
                End If
            End If
        End If

        If DirectCast(e.Row.FindControl("LabelSortNumber"), Label) IsNot Nothing Then
            DirectCast(e.Row.FindControl("LabelSortNumber"), Label).Text = e.Row.RowIndex.ToString() + 1
        End If

        ' it highlights entire row if SignedBySupplier and SignedByMercury both not checked
        If DirectCast(e.Row.FindControl("CheckBoxSignByMercury"), CheckBox) IsNot Nothing Then
            If DirectCast(e.Row.FindControl("CheckBoxSignByMercury"), CheckBox).Checked = True AndAlso DirectCast(e.Row.FindControl("CheckBoxSignBySupplier"), CheckBox).Checked = True Then
                ' do nothing
            Else
                e.Row.BackColor = System.Drawing.Color.Gold
            End If
        End If

    End Sub

    Protected Sub GridViewShowContract_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridViewShowContract.RowDeleting
        Dim index As Integer = Convert.ToInt32(e.RowIndex)
        Dim row As GridViewRow = GridViewShowContract.Rows(index)

        ' delete existing PDF and DOC files for this specific contract
        Dim LinkToDOC As String = Server.MapPath(CreateDataReader.Create_Table_Contract((GridViewShowContract.DataKeys(index).Value)).LinkToTemplatefile_DOC)

        ' IT CAN BE ACTICATED LATER. CANNOT DELETE IF ATTACHMENT EXIST Access to the path 'E:\host\CONTRACT\Data Center Sberbank - 108\91d2bfb946e468a9.pdf' is denied
        'If Right(LinkToDOC, 4) = ".doc" OrElse Right(LinkToDOC, 5) = ".docx" Then
        'System.IO.File.Delete(LinkToDOC)
        'End If

        ' make a connection to take LinkToPDF contract
        Using conPDF As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            conPDF.Open()
            Dim sqlstringPDF As String = "SELECT RTRIM(LinkToPDFcopy) AS LinkToPDFcopy FROM Table_Contracts WHERE ContractID = " + GridViewShowContract.DataKeys(index).Value.ToString
            Dim cmdPDF As New SqlCommand(sqlstringPDF, conPDF)
            cmdPDF.CommandType = System.Data.CommandType.Text
            Dim drPDF As SqlDataReader = cmdPDF.ExecuteReader
            Dim LinkToPDF As String = ""
            While drPDF.Read
                LinkToPDF = drPDF(0).ToString
            End While
            conPDF.Close()
            drPDF.Close()
        End Using
        ' IT CAN BE ACTICATED LATER. CANNOT DELETE IF ATTACHMENT EXIST Access to the path 'E:\host\CONTRACT\Data Center Sberbank - 108\91d2bfb946e468a9.pdf' is denied
        'Dim MyTask As New MyCommonTasks
        'MyTask.DeleteAllFileOnLocalOnFTP(LinkToPDF)


        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        Dim InstanceOfDeletion As String = "'" + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + "-" + Mid(result.ToString, 1, 2).ToString + " " + Mid(result.ToString, 12, 2).ToString + ":" + Mid(result.ToString, 15, 2).ToString + ":" + Mid(result.ToString, 18, 2).ToString + "'"

        Using cn As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            Dim cmd As New System.Data.SqlClient.SqlCommand()
            cmd.Connection = cn

            cmd.CommandText = "UPDATE Table_Contracts SET DeletedBy = " + InstanceOfDeletion + ", PersonDeleted = " + "'" + Page.User.Identity.Name.ToString + "'" + " WHERE ContractID = " + DirectCast(row.FindControl("LabelContractIDitem"), Label).Text
            cmd.CommandType = System.Data.CommandType.Text
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
        End Using
    End Sub

    Protected Sub GridViewShowContract_RowUpdated(sender As Object, e As System.Web.UI.WebControls.GridViewUpdatedEventArgs) Handles GridViewShowContract.RowUpdated
        Session("SupplierID2") = ""

        Dim index As Integer = Convert.ToInt32(GridViewShowContract.EditIndex)
        Dim row As GridViewRow = GridViewShowContract.Rows(index)

        ' CHECK CONTRACT IF IT IS READY FOR PO
        Dim LiteralContractID As Literal = DirectCast(row.FindControl("LiteralContractID"), Literal)

        ContractView.Initiate_Checking_Contract_Or_Addendum_To_Insert_Or_Update_Po_Then_Finally_Execute(
                                                Convert.ToInt32(LiteralContractID.Text),
                                                0,
                                                POdetailsForEmail)

    End Sub

    Protected Sub GridViewShowContract_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridViewShowContract.RowUpdating

        Dim index As Integer = Convert.ToInt32(e.RowIndex)
        Dim row As GridViewRow = GridViewShowContract.Rows(index)

        Dim _NewGeneration As Boolean = False

        If row.RowType = DataControlRowType.DataRow Then
            _NewGeneration = DataBinder.Eval(row.DataItem, "NewGeneration")
        End If

        Dim _ContractID As Decimal = Convert.ToDecimal(CType(row.FindControl("LiteralContractID"), Literal).Text)
        If _NewGeneration = True Then
            ContractAndAddendumUpdateNotification.SendContractUpdateNotification(e, row)
        End If

        Dim LabelProjectSupplierComtability As Label = DirectCast(row.FindControl("LabelProjectSupplierComtability"), Label)
        Dim DDLProjectEdit As DropDownList = DirectCast(row.FindControl("DDLProjectEdit"), DropDownList)
        ' check if client and supplier are compatible

        ' it updates UpdatedBy
        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        e.NewValues("UpdatedBy") = result
        e.NewValues("PersonUpdated") = Page.User.Identity.Name.ToString

        ' It updates Contract value
        Dim TextBoxContractValueEdit As TextBox = DirectCast(row.FindControl("TextBoxContractValueEdit"), TextBox)
        If TextBoxContractValueEdit.Text <> "" Then
            e.NewValues("ContractValue_woVAT") = Convert.ToDecimal(TextBoxContractValueEdit.Text)
        ElseIf TextBoxContractValueEdit.Text = "" Then
            e.NewValues("ContractValue_woVAT") = Nothing
        End If

        ' it updates contract date
        Dim TextBoxContractDateEdit As TextBox = DirectCast(row.FindControl("TextBoxContractDateEdit"), TextBox)
        If TextBoxContractDateEdit.Text <> "" Then
            e.NewValues("ContractDate") = Convert.ToDateTime(Mid(TextBoxContractDateEdit.Text.ToString, 1, 2).ToString + "/" + Mid(TextBoxContractDateEdit.Text.ToString, 4, 2).ToString + "/" + Mid(TextBoxContractDateEdit.Text.ToString, 7, 4).ToString)
        ElseIf TextBoxContractDateEdit.Text = "" Then
            e.NewValues("ContractDate") = Nothing
        End If

        ' it updates ProjectID
        e.NewValues("ProjectID") = DDLProjectEdit.SelectedValue

        ' it updates PO_No
        Dim DropDownListPOnoEdit As DropDownList = DirectCast(row.FindControl("DropDownListPOnoEdit"), DropDownList)
        If _NewGeneration = False Then
            If Len(DropDownListPOnoEdit.SelectedValue.ToString) = 0 Then
                e.NewValues("PO_No") = Nothing
            Else
                SqlDataSourcePoDublication.SelectCommand = " SELECT PO_No FROM " +
                                                          " (SELECT  dbo.Table_Contracts.PO_No " +
                                                          " FROM  dbo.Table_Contracts " +
                                                          " WHERE dbo.Table_Contracts.PO_No = '" + DropDownListPOnoEdit.SelectedValue.ToString + "' " +
                                                          "  " +
                                                          " UNION ALL " +
                                                          "  " +
                                                          " SELECT   dbo.Table_Addendums.PO_No " +
                                                          " FROM         dbo.Table_Addendums  " +
                                                          " WHERE dbo.Table_Addendums.PO_No = '" + DropDownListPOnoEdit.SelectedValue.ToString + "') " +
                                                          " AS DataSource1 "
                DropDownListPoDublication.DataBind()
                If DropDownListPoDublication.Items.Count > 0 Then
                    If DirectCast(row.FindControl("LabelPoNoBeforeEdit"), Label).Text <> DropDownListPOnoEdit.SelectedValue.ToString Then
                        e.Cancel = True
                        DirectCast(row.FindControl("LabelWarningPoDublication"), Label).Text = "PO already assigned to another contract or addendum under this supplier!"
                        DirectCast(row.FindControl("LabelWarningPoDublication"), Label).ForeColor = System.Drawing.Color.Red
                        DirectCast(row.FindControl("LabelWarningPoDublication"), Label).BorderStyle = BorderStyle.Solid
                        DirectCast(row.FindControl("LabelWarningPoDublication"), Label).BorderWidth = 2
                        DirectCast(row.FindControl("LabelWarningPoDublication"), Label).BorderColor = System.Drawing.Color.Red
                    ElseIf DirectCast(row.FindControl("LabelPoNoBeforeEdit"), Label).Text = DropDownListPOnoEdit.SelectedValue.ToString Then
                        e.NewValues("PO_No") = DropDownListPOnoEdit.SelectedValue
                    End If
                ElseIf DropDownListPoDublication.Items.Count = 0 Then
                    e.NewValues("PO_No") = DropDownListPOnoEdit.SelectedValue
                End If
            End If
        End If

        ' update AttachmentExist column
        Dim TextLink As TextBox = DirectCast(row.FindControl("LinkToPDFcopyTextBoxEdit"), TextBox)
        If Len(TextLink.Text) > 0 OrElse Not String.IsNullOrEmpty(TextLink.Text) Then
            e.NewValues("AttachmentExist") = True
        Else
            e.NewValues("AttachmentExist") = False
        End If

        ' Delete existing PDF and DOC file if user replace with another one.
        Dim LinkToPDFcopyTextBoxEdit As TextBox = DirectCast(row.FindControl("LinkToPDFcopyTextBoxEdit"), TextBox)
        Dim LinkToTemplatefile_DOCTextBoxEdit As TextBox = DirectCast(row.FindControl("LinkToTemplatefile_DOCTextBoxEdit"), TextBox)

        Dim OldPDF As String = ""
        If e.OldValues("LinkToPDFcopy") = Nothing Then
            OldPDF = ""
        Else
            OldPDF = e.OldValues("LinkToPDFcopy").ToString
        End If

        Dim CheckBoxSignByMercury As CheckBox = DirectCast(row.FindControl("SignByMercuryCheckBoxEdit"), CheckBox)
        Dim OldSignedStatus As String = e.OldValues("SignByMercury").ToString

        Dim Notification As String = ""
        Dim SendNotification As New MyCommonTasks

        Dim contractValue As String = DirectCast(row.FindControl("TextBoxContractValueEdit"), TextBox).Text
        If contractValue = "" Then
            contractValue = "0.0"
        End If

        ' Commercial items
        Dim TextBoxStartDate As TextBox = DirectCast(row.FindControl("TextBoxStartDate"), TextBox)
        Dim DropDownListPenalty As DropDownList = DirectCast(row.FindControl("DropDownListPenalty"), DropDownList)
        Dim TextBoxPenaltyNote As TextBox = DirectCast(row.FindControl("TextBoxPenaltyNote"), TextBox)
        Dim DropDownListPenaltyToSupplier As DropDownList = DirectCast(row.FindControl("DropDownListPenaltyToSupplier"), DropDownList)
        Dim TextBoxPenaltyNoteSupplier As TextBox = DirectCast(row.FindControl("TextBoxPenaltyNoteSupplier"), TextBox)
        Dim TextBoxFinishDate As TextBox = DirectCast(row.FindControl("TextBoxFinishDate"), TextBox)

        If String.IsNullOrEmpty(TextBoxStartDate.Text) Then
            e.NewValues("StartDate") = Nothing
        Else
            e.NewValues("StartDate") = Convert.ToDateTime(Mid(TextBoxStartDate.Text.ToString, 1, 2).ToString +
                                     "/" + Mid(TextBoxStartDate.Text.ToString, 4, 2).ToString +
                                     "/" + Mid(TextBoxStartDate.Text.ToString, 7, 4).ToString)
        End If

        If String.IsNullOrEmpty(TextBoxFinishDate.Text) Then
            e.NewValues("FinishDate") = Nothing
        Else
            e.NewValues("FinishDate") = Convert.ToDateTime(Mid(TextBoxFinishDate.Text.ToString, 1, 2).ToString +
                               "/" + Mid(TextBoxFinishDate.Text.ToString, 4, 2).ToString +
                               "/" + Mid(TextBoxFinishDate.Text.ToString, 7, 4).ToString)
        End If

        Dim TextBoxDeliveryTerms As TextBox = DirectCast(row.FindControl("TextBoxDeliveryTerms"), TextBox)
        e.NewValues("DeliveryTerms") = TextBoxDeliveryTerms.Text

        If String.IsNullOrEmpty(DropDownListPenalty.SelectedValue.ToString) Then
            e.NewValues("Penalties") = Nothing
        Else
            e.NewValues("Penalties") = DropDownListPenalty.SelectedValue
        End If

        If String.IsNullOrEmpty(TextBoxPenaltyNote.Text) Then
            e.NewValues("PenaltiesNote") = Nothing
        Else
            e.NewValues("PenaltiesNote") = TextBoxPenaltyNote.Text
        End If

        If String.IsNullOrEmpty(DropDownListPenaltyToSupplier.SelectedValue.ToString) Then
            e.NewValues("PenaltiesToSupplier") = Nothing
        Else
            e.NewValues("PenaltiesToSupplier") = DropDownListPenaltyToSupplier.SelectedValue
        End If

        If String.IsNullOrEmpty(TextBoxPenaltyNoteSupplier.Text) Then
            e.NewValues("PenaltiesToSupplierNote") = Nothing
        Else
            e.NewValues("PenaltiesToSupplierNote") = TextBoxPenaltyNoteSupplier.Text
        End If

        ' __________END OF  Commercial items


        Dim DetailedInfo As String = "| " + BodyTexts.Ref("Mu+drePgpEK6h8XzFfTOJg") + ": " + DDLProjectEdit.SelectedItem.Text +
          " |" + BodyTexts.Ref("WCc0K46nMU6P4KVjfJyRyQ") + "= " + DirectCast(row.FindControl("LabelSupplierNameInEdit"), Label).Text +
        " |" + BodyTexts.Ref("b/QwmB2AWEmXNrVeVHPG7g") + "= " + DirectCast(row.FindControl("TextBoxContractNoEdit"), TextBox).Text +
        " |" + BodyTexts.Ref("2nt0zE4uHkGJVtJknm6BKA") + "= " + String.Format("{0:#,##0.00}", Convert.ToDecimal(contractValue)) +
        DirectCast(row.FindControl("DropDownListCurrencyEdit"), DropDownList).SelectedValue +
        Environment.NewLine + Environment.NewLine +
        " <a href=" + "'" + "http://pts.mercuryeng.ru/webforms/contractdetails.aspx?ContractID=" + _ContractID.ToString + "'" + " target=" + """" + "_blank" + """" + ">" + BodyTexts.Ref("Yy7eoH4zGk6Ri7BCX7Behg") + "</a> "

        ' start to write your criterias
        If OldSignedStatus = False AndAlso OldPDF = "" AndAlso CheckBoxSignByMercury.Checked = True AndAlso Len(LinkToPDFcopyTextBoxEdit.Text) = 0 Then
            'Contract> Signed> No Attachment
            Notification = BodyTexts.Ref("cjd/EBzfh0qvnWLM4bKJNA") + DetailedInfo
            SendNotification.SendNotification(Notification, DDLProjectEdit.SelectedValue.ToString, 3)
            SendNotification.SendEmailForContract(DDLProjectEdit.SelectedValue, Notification, BodyTexts.Ref("cjd/EBzfh0qvnWLM4bKJNA"))
        ElseIf OldSignedStatus = False AndAlso OldPDF = "" AndAlso CheckBoxSignByMercury.Checked = True AndAlso Len(LinkToPDFcopyTextBoxEdit.Text) > 0 Then
            'Contract> Signed> Attachment added
            Notification = BodyTexts.Ref("vEp54tDjLEG37AQ/K/oUHQ") + " " + "<a href=" + """" + Replace(LinkToPDFcopyTextBoxEdit.Text, "~", "") + """" + " target=" + """" + "_blank" + """" + ">" + BodyTexts.Ref("0AlnhV1jr0GcYHql+D2yVw") + "</a>" + " added" + DetailedInfo
            SendNotification.SendNotification(Notification, DDLProjectEdit.SelectedValue.ToString, 3)
            SendNotification.SendEmailForContract(DDLProjectEdit.SelectedValue, Notification, BodyTexts.Ref("cMWueuo62U+WWg8rwBMrMg"))
        ElseIf OldSignedStatus = False AndAlso OldPDF <> "" AndAlso CheckBoxSignByMercury.Checked = True AndAlso OldPDF = LinkToPDFcopyTextBoxEdit.Text Then
            'Contract> Signed> existed Attachment
            Notification = BodyTexts.Ref("y9ndKwD5vUa+wnDIKDp4uQ") + " " + "<a href=" + """" + Replace(LinkToPDFcopyTextBoxEdit.Text, "~", "") + """" + " target=" + """" + "_blank" + """" + ">" + BodyTexts.Ref("0AlnhV1jr0GcYHql+D2yVw") + "</a>" + DetailedInfo
            SendNotification.SendNotification(Notification, DDLProjectEdit.SelectedValue.ToString, 3)
            SendNotification.SendEmailForContract(DDLProjectEdit.SelectedValue, Notification, "Contract> Signed> existed Attachment")
        ElseIf OldSignedStatus = True AndAlso OldPDF = "" AndAlso CheckBoxSignByMercury.Checked = True AndAlso Len(LinkToPDFcopyTextBoxEdit.Text) > 0 Then
            'Contract> Attachment added
            Notification = BodyTexts.Ref("XdMvsx34P0Ks1/UnypJn2w") + " " + "<a href=" + """" + Replace(LinkToPDFcopyTextBoxEdit.Text, "~", "") + """" + " target=" + """" + "_blank" + """" + ">" + BodyTexts.Ref("0AlnhV1jr0GcYHql+D2yVw") + "</a>" + " added" + DetailedInfo
            SendNotification.SendNotification(Notification, DDLProjectEdit.SelectedValue.ToString, 3)
            SendNotification.SendEmailForContract(DDLProjectEdit.SelectedValue, Notification, BodyTexts.Ref("uWCLNOX/aE2Oh9qDMc2WIg"))
        ElseIf OldSignedStatus = True AndAlso CheckBoxSignByMercury.Checked = False Then
            'Contract> Sign removed
            Notification = BodyTexts.Ref("5nfR7M/mDkSH9UVBh8eEUw") + DetailedInfo
            SendNotification.SendNotification(Notification, DDLProjectEdit.SelectedValue.ToString, 3)
            SendNotification.SendEmailForContract(DDLProjectEdit.SelectedValue, Notification, BodyTexts.Ref("90TYEd0hm0eFPh5DlsiwtQ"))
        ElseIf OldSignedStatus = True AndAlso CheckBoxSignByMercury.Checked = True AndAlso OldPDF <> LinkToPDFcopyTextBoxEdit.Text AndAlso Len(LinkToPDFcopyTextBoxEdit.Text) > 0 Then
            'Contract> Attachment changed
            Notification = BodyTexts.Ref("XdMvsx34P0Ks1/UnypJn2w") + " " + "<a href=" + """" + Replace(LinkToPDFcopyTextBoxEdit.Text, "~", "") + """" + " target=" + """" + "_blank" + """" + ">" + BodyTexts.Ref("0AlnhV1jr0GcYHql+D2yVw") + "</a>" + " " + BodyTexts.Ref("1vUjGDtI6E6bcy/63P8Iow") + DetailedInfo
            SendNotification.SendNotification(Notification, DDLProjectEdit.SelectedValue.ToString, 3)
            SendNotification.SendEmailForContract(DDLProjectEdit.SelectedValue, Notification, BodyTexts.Ref("EPDAy8gVQ0Cf5cDlmn0WKQ"))
            ' IT CAN BE ACTICATED LATER. CANNOT DELETE IF ATTACHMENT EXIST Access to the path 'E:\host\CONTRACT\Data Center Sberbank - 108\91d2bfb946e468a9.pdf' is denied
            'Dim MyTask As New MyCommonTasks
            'MyTask.DeleteAllFileOnLocalOnFTP(OldPDF)
        End If

        If Convert.ToString(e.OldValues("LinkToTemplatefile_DOC")) <> "" Then
            If e.OldValues("LinkToTemplatefile_DOC").ToString <> LinkToTemplatefile_DOCTextBoxEdit.Text Then
                ' delete DOC
                ' IT CAN BE ACTICATED LATER. CANNOT DELETE IF ATTACHMENT EXIST Access to the path 'E:\host\CONTRACT\Data Center Sberbank - 108\91d2bfb946e468a9.pdf' is denied
                'Dim LinkToDOC As String = Server.MapPath(e.OldValues("LinkToTemplatefile_DOC").ToString)
                'If Right(LinkToDOC, 4) = ".doc" OrElse Right(LinkToDOC, 5) = ".docx" Then
                'System.IO.File.Delete(LinkToDOC)
                'End If
            End If
        End If

        ' it will remove PDF and DOC files if CheckBoxes are checked.
        Dim CheckBoxBudgetDeleteDoc As CheckBox = DirectCast(row.FindControl("CheckBoxBudgetDeleteDoc"), CheckBox)

        If CheckBoxBudgetDeleteDoc.Checked = True Then
            e.NewValues("BudgetLinkToPDF") = Nothing
        End If

        ' Check Scenario Range if Contract Value goes out of range
        Dim LabelScenario As Label = DirectCast(row.FindControl("LabelScenario"), Label)
        Dim DropDownListCurrencyEdit As DropDownList = DirectCast(row.FindControl("DropDownListCurrencyEdit"), DropDownList)
        Dim TextBoxVAT As TextBox = DirectCast(row.FindControl("TextBoxVAT"), TextBox)
        Dim TextBoxContractValueWithVATEdit As TextBox = DirectCast(row.FindControl("TextBoxContractValueWithVATEdit"), TextBox)
        Dim LabelScenarioOutOfRange As Label = DirectCast(row.FindControl("LabelScenarioOutOfRange"), Label)

        If _NewGeneration = True Then
            If LabelScenario IsNot Nothing Then
                If Convert.ToInt32(LabelScenario.Text) > 0 Then

                    Dim _Scenario As Integer = Convert.ToInt32(LabelScenario.Text)
                    Dim _returnMax As Decimal = 0.0
                    Dim _returnMin As Decimal = 0.0
                    Dim Max_InEuroExcVAT As Decimal = 0
                    Dim Min_InEuroExcVAT As Decimal = 0
                    Dim _ContractCurrency As String = DropDownListCurrencyEdit.SelectedValue.ToString
                    Dim _ContractVAT As Integer = Convert.ToInt32(TextBoxVAT.Text)

                    If _Scenario = 1 Then
                        Max_InEuroExcVAT = 5000
                    ElseIf _Scenario = 2 Then
                        Max_InEuroExcVAT = 50000
                    ElseIf _Scenario = 3 Then
                        Max_InEuroExcVAT = 100000
                    ElseIf _Scenario = 4 Then
                        Max_InEuroExcVAT = 250000
                    ElseIf _Scenario = 5 Then
                        Max_InEuroExcVAT = 1000000
                    ElseIf _Scenario = 6 Then
                        Max_InEuroExcVAT = 5000000
                    ElseIf _Scenario = 7 Then
                        Max_InEuroExcVAT = 1000000000
                    End If

                    If _Scenario = 1 Then
                        Min_InEuroExcVAT = 0.1
                    ElseIf _Scenario = 2 Then
                        Min_InEuroExcVAT = 5000
                    ElseIf _Scenario = 3 Then
                        Min_InEuroExcVAT = 50000
                    ElseIf _Scenario = 4 Then
                        Min_InEuroExcVAT = 100000
                    ElseIf _Scenario = 5 Then
                        Min_InEuroExcVAT = 250000
                    ElseIf _Scenario = 6 Then
                        Min_InEuroExcVAT = 1000000
                    ElseIf _Scenario = 7 Then
                        Min_InEuroExcVAT = 5000000
                    End If

                    Dim _date As DateTime = CreateDataReader.Create_Table_Contract(_ContractID).CreatedBy

                    If _ContractCurrency.ToString.ToLower = "rub" Then
                        _returnMax = Math.Round(Max_InEuroExcVAT * (MaxExchRate.GetReferringEuro(_date)) * ((100 + _ContractVAT) / 100), 2)
                    ElseIf _ContractCurrency.ToString.ToLower = "dollar" Then
                        _returnMax = Math.Round(Max_InEuroExcVAT * (MaxExchRate.GetReferringEuro(_date)) / (MaxExchRate.GetReferringDollar(_date)) * ((100 + _ContractVAT) / 100), 2)
                    ElseIf _ContractCurrency.ToString.ToLower = "euro" Then
                        _returnMax = Math.Round(Max_InEuroExcVAT * ((100 + _ContractVAT) / 100), 2)
                    End If

                    If _ContractCurrency.ToString.ToLower = "rub" Then
                        _returnMin = Math.Round(Min_InEuroExcVAT * (MaxExchRate.GetReferringEuro(_date)) * ((100 + _ContractVAT) / 100), 2)
                    ElseIf _ContractCurrency.ToString.ToLower = "dollar" Then
                        _returnMin = Math.Round(Min_InEuroExcVAT * (MaxExchRate.GetReferringEuro(_date)) / (MaxExchRate.GetReferringDollar(_date)) * ((100 + _ContractVAT) / 100), 2)
                    ElseIf _ContractCurrency.ToString.ToLower = "euro" Then
                        _returnMin = Math.Round(Min_InEuroExcVAT * ((100 + _ContractVAT) / 100), 2)
                    End If

                    If Math.Round(Convert.ToDecimal(TextBoxContractValueWithVATEdit.Text), 2) >= _returnMax Then
                        LabelScenarioOutOfRange.Text = " " + BodyTexts.Ref("2ry0d2Oye0ibC//+OuGvKw") + "#" + _Scenario.ToString + ". " +
                          BodyTexts.Ref("CAxtgd13QEyAg6rI/KlVog") + " " + String.Format("{0:#,##0.00}", _returnMax) + " " + _ContractCurrency + " " + BodyTexts.Ref("R3SQpAx4d0KPkUr5AqPkpA")
                        e.Cancel = True
                    End If

                    If Math.Round(Convert.ToDecimal(TextBoxContractValueWithVATEdit.Text), 2) < _returnMin Then
                        LabelScenarioOutOfRange.Text = " " + BodyTexts.Ref("2ry0d2Oye0ibC//+OuGvKw") + "#" + _Scenario.ToString + ". " +
                          BodyTexts.Ref("c2wdB/Hznk6uK4IlCEv9QQ") + " " + String.Format("{0:#,##0.00}", _returnMin) + " " + _ContractCurrency + " " + BodyTexts.Ref("R3SQpAx4d0KPkUr5AqPkpA")
                        e.Cancel = True
                    End If

                End If
            End If
        End If

        ' Check if Validation controls on Penalty notes to be enabled or not
        Dim DDL_supplierPenalty As DropDownList = DirectCast(row.FindControl("DropDownListPenaltyToSupplier"), DropDownList)
        Dim DDL_MercuryPenalty As DropDownList = DirectCast(row.FindControl("DropDownListPenalty"), DropDownList)
        Dim _TextBoxPenaltyNote As TextBox = DirectCast(row.FindControl("TextBoxPenaltyNote"), TextBox)
        Dim _TextBoxPenaltyNoteSupplier As TextBox = DirectCast(row.FindControl("TextBoxPenaltyNoteSupplier"), TextBox)
        Dim LabelValidationPenaltyMercuryNote As Label = DirectCast(row.FindControl("LabelValidationPenaltyMercuryNote"), Label)
        Dim LabelValidationPenaltySupplierNote As Label = DirectCast(row.FindControl("LabelValidationPenaltySupplierNote"), Label)

        If DDL_MercuryPenalty.SelectedIndex = 1 And String.IsNullOrEmpty(_TextBoxPenaltyNote.Text) Then ' it is YES
            LabelValidationPenaltyMercuryNote.Text = BodyTexts.Ref("dtdbYH24dkWJyDEPdP4SNQ")
            e.Cancel = True
        Else
            LabelValidationPenaltyMercuryNote.Text = String.Empty
        End If

        If DDL_supplierPenalty.SelectedIndex = 1 And String.IsNullOrEmpty(_TextBoxPenaltyNoteSupplier.Text) Then ' it is YES
            LabelValidationPenaltySupplierNote.Text = BodyTexts.Ref("dtdbYH24dkWJyDEPdP4SNQ")
            e.Cancel = True
        Else
            LabelValidationPenaltySupplierNote.Text = String.Empty
        End If

        ' Validate Payment Terms if it is exceeding 100%
        Dim TextBoxAdvance As TextBox = DirectCast(row.FindControl("TextBoxAdvance"), TextBox)
        Dim TextBoxInterim As TextBox = DirectCast(row.FindControl("TextBoxInterim"), TextBox)
        Dim TextBoxShipment As TextBox = DirectCast(row.FindControl("TextBoxShipment"), TextBox)
        Dim TextBoxDelivery As TextBox = DirectCast(row.FindControl("TextBoxDelivery"), TextBox)
        Dim TextBoxRetention As TextBox = DirectCast(row.FindControl("TextBoxRetention"), TextBox)
        Dim LabelPaymentTermsValidationNotification As Label = DirectCast(row.FindControl("LabelPaymentTermsValidationNotification"), Label)

        If ContractView.PaymentTermsValidated(TextBoxAdvance.Text, TextBoxInterim.Text, TextBoxShipment.Text,
                                 TextBoxDelivery.Text, TextBoxRetention.Text) = False Then
            e.Cancel = True
            LabelPaymentTermsValidationNotification.Visible = True
        End If
        ' END OF Validate Payment Terms if it is exceeding 100%

        ' Update Additional Client Data
        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities
            Dim ClientData = (From C In db.Table_Contracts_ClientAdditional Where C.ContractID = _ContractID).ToList

            Dim TextBoxCompletionDateEdit As TextBox = DirectCast(row.FindControl("TextBoxCompletionDateEdit"), TextBox)
            Dim CheckBoxAktOfWorkEdit As UserControl = DirectCast(row.FindControl("CheckBoxAktOfWorkEdit"), UserControl)
            Dim LabelLinkAktOfWorkContract As Label = DirectCast(row.FindControl("LabelLinkAktOfWorkContract"), Label)
            'Dim CheckBoxDeleteContractAktOfWork As UserControl = DirectCast(row.FindControl("CheckBoxDeleteContractAktOfWork"), UserControl)

            'If DirectCast(CheckBoxAktOfWorkEdit.FindControl("CheckBox"), CheckBox).Checked = False AndAlso
            '    String.IsNullOrEmpty(TextBoxCompletionDateEdit.Text) = True AndAlso
            '    (DirectCast(CheckBoxDeleteContractAktOfWork.FindControl("CheckBox"), CheckBox).Checked = True Or String.IsNullOrEmpty(LabelLinkAktOfWorkContract.Text) = True) Then

            '    If ClientData.Count > 0 Then
            '        ' delete
            '        db.Table_Contracts_ClientAdditional.Remove(ClientData(0))
            '        db.SaveChanges()

            '    End If

            'Else

            If ClientData.Count > 0 Then
                ' update
                If String.IsNullOrEmpty(TextBoxCompletionDateEdit.Text) Then
                    ClientData(0).CompletionDate = Nothing
                Else
                    ClientData(0).CompletionDate = PTS_MERCURY.db.CustomClass.DateToEntityFromTextBox(TextBoxCompletionDateEdit.Text)
                End If

                ClientData(0).AktOfWorkWhen = LocalTime.GetTime
                ClientData(0).AktOfWork = DirectCast(CheckBoxAktOfWorkEdit.FindControl("CheckBox"), CheckBox).Checked
                ' execute delete checkbox
                ClientData(0).LinkToAktOfWork = LabelLinkAktOfWorkContract.Text

                db.SaveChanges()

            Else
                ' insert

                Dim entry As New PTS_MERCURY.db.Table_Contracts_ClientAdditional With {
                        .ContractID = _ContractID,
                        .AktOfWork = DirectCast(CheckBoxAktOfWorkEdit.FindControl("CheckBox"), CheckBox).Checked,
                        .CompletionDate = PTS_MERCURY.db.CustomClass.DateToEntityFromTextBox(TextBoxCompletionDateEdit.Text),
                        .AktOfWorkWhen = LocalTime.GetTime,
                        .LinkToAktOfWork = LabelLinkAktOfWorkContract.Text}

                db.Table_Contracts_ClientAdditional.Add(entry)
                db.SaveChanges()

            End If

            'End If

            db.Dispose()

        End Using

        ' validation for budget exceeding
        Dim _newvalue As Decimal = 0.0
        Dim _oldvalue As Decimal = 0.0
        _oldvalue = e.OldValues("ContractValue_withVAT")

        _oldvalue = Math.Round(IIf(IsNumeric(_oldvalue), _oldvalue, 0) / ((100 + (IIf(IsNumeric(TextBoxVAT.Text), TextBoxVAT.Text, 0))) / 100), 2)
        _newvalue = Math.Round(IIf(IsNumeric(TextBoxContractValueWithVATEdit.Text), TextBoxContractValueWithVATEdit.Text, 0) / ((100 + (IIf(IsNumeric(TextBoxVAT.Text), TextBoxVAT.Text, 0))) / 100), 2)
        Dim _costcode As String = ""

        Try
            _costcode = PTS_MERCURY.helper.Table_Contracts.GetRowByPrimaryKey(_ContractID).CostCode.Trim()
        Catch ex As Exception

        End Try

        Dim _projectid As Integer = PTS_MERCURY.helper.Table_Contracts.GetRowByPrimaryKey(_ContractID).ProjectID

        If PTS_MERCURY.helper.Table_Budget.GetBudgetEmailControlFailed(Page,
                                                                       _projectid,
                                                                       _costcode,
                                                                       _oldvalue,
                                                                       _newvalue) =
                                                                   True Then

            e.Cancel = True

        End If

    End Sub

    ' CAN BE ACTIVATED LATER. CANCELLED, KSENIA GOT PROBLEM
    'Protected Function ClientAndSupplierIDCompatible(ByVal ProjectID As Integer, ByVal SupplierID As String) As Boolean
    '  Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
    '    con.Open()
    '    Dim sqlstring As String = " SELECT     Client " + _
    '                                            " FROM         dbo.Table6_Supplier " + _
    '                                            " WHERE     (SupplierID = @SupplierID) "

    '    Dim cmd As New SqlCommand(sqlstring, con)
    '    cmd.CommandType =System.Data.CommandType.Text

    '    'syntax for parameter adding
    '    Dim SupplierIDParameter As SqlParameter = cmd.Parameters.Add("@SupplierID",System.Data.SqlDbType.NVarChar, 10)
    '    SupplierIDParameter.Value = SupplierID
    '    Dim dr As SqlDataReader = cmd.ExecuteReader
    '    Dim IsCompatible As Boolean = False
    '    While dr.Read
    '      If SupplierID = "0000000000" Then
    '        IsCompatible = True
    '      Else
    '        If dr(0) = True Then
    '          If ProjectID = 999 Then
    '            IsCompatible = True
    '          Else
    '            IsCompatible = False
    '          End If
    '        ElseIf dr(0) = False Then
    '          If ProjectID = 999 Then
    '            IsCompatible = False
    '          Else
    '            IsCompatible = True
    '          End If
    '        End If
    '      End If
    '    End While
    '    con.Close()
    '    dr.Close()
    '    Return IsCompatible
    '  End Using
    'End Function

    Protected Sub GridViewShowAddendum_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        Dim _NewGeneration As Boolean = False

        If e.Row.RowType = DataControlRowType.DataRow Then
            _NewGeneration = DataBinder.Eval(e.Row.DataItem, "NewGeneration")
        End If

        Dim _POexecuted As Boolean = False

        If e.Row.RowType = DataControlRowType.DataRow Then
            _POexecuted = DataBinder.Eval(e.Row.DataItem, "POexecuted")
        End If

        Dim _Exceptional As Boolean = False

        If e.Row.RowType = DataControlRowType.DataRow Then
            _Exceptional = DataBinder.Eval(e.Row.DataItem, "Exceptional")
        End If

        Dim _ProjectID As Integer = 0

        If e.Row.RowType = DataControlRowType.DataRow Then
            _ProjectID = DataBinder.Eval(e.Row.DataItem, "ProjectID")
        End If

        Dim _AddendumLinkToTemplatefile_DOC As String = ""

        If e.Row.RowType = DataControlRowType.DataRow Then
            Try
                _AddendumLinkToTemplatefile_DOC = DataBinder.Eval(e.Row.DataItem, "AddendumLinkToTemplatefile_DOC")
            Catch ex As Exception

            End Try
        End If

        Dim _InitiateContractAndAddendum As Integer = 0

        If e.Row.RowType = DataControlRowType.DataRow Then
            _InitiateContractAndAddendum = DataBinder.Eval(e.Row.DataItem, "InitiateContractAndAddendum").ToString
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim HyperlinkAddendumCoverPage As HyperLink = DirectCast(e.Row.FindControl("HyperlinkAddendumCoverPage"), HyperLink)
            If HyperlinkAddendumCoverPage IsNot Nothing Then
                HyperlinkAddendumCoverPage.Text = BodyTexts.Ref("KdxNrj+A5kaqJc9uSjZUkg")
                HyperlinkAddendumCoverPage.NavigateUrl = "javascript: w=window.open('/PtsContractual/ContractsAddendums/AddendumCoverPage/" + DataBinder.Eval(e.Row.DataItem, "AddendumId").ToString() + "','GoogleWindow', 'width=1200, height=1000'); w.print();"
            End If

            Dim _ButtonEdit As Button = DirectCast(e.Row.FindControl("ButtonEdit"), Button)
            Dim _ButtonDelete As Button = DirectCast(e.Row.FindControl("ButtonDelete"), Button)
            Dim _ButtonExceptionalApprove As Button = DirectCast(e.Row.FindControl("ButtonExceptionalApprove"), Button)

            If _ButtonEdit IsNot Nothing Then
                _ButtonEdit.Text = BodyTexts.Ref("aOfX+llnm0OCrn21IqoxvQ")
                _ButtonDelete.Text = BodyTexts.Ref("JTHzJ7uo+kqPfIAg4uM8HA")
                _ButtonDelete.OnClientClick = "return confirm('" + BodyTexts.Ref("dcGVT5LdJUGa1sSlsf1pNA") + "');"
                _ButtonExceptionalApprove.Text = BodyTexts.Ref("PVsElor12Uqia5rKU4mbyw")
                _ButtonExceptionalApprove.OnClientClick = "return confirm('" + BodyTexts.Ref("Wx35FGRzzUKoVrrPr3SHxQ") + "');"
            End If

            Dim _ButtonUpdate As Button = DirectCast(e.Row.FindControl("ButtonUpdate"), Button)
            Dim _ButtonCancel As Button = DirectCast(e.Row.FindControl("ButtonCancel"), Button)

            If _ButtonUpdate IsNot Nothing Then
                _ButtonUpdate.Text = BodyTexts.Ref("KTfnFlGK8kWuzdJQLcZe2w")
                _ButtonCancel.Text = BodyTexts.Ref("DRM8lAyeUECweQVjPi1Hlg")
            End If

            Dim _LabelAddendumNoEdit As Label = DirectCast(e.Row.FindControl("LabelAddendumNoEdit"), Label)
            Dim _LabelDateEdit As Label = DirectCast(e.Row.FindControl("LabelDateEdit"), Label)
            Dim _LabelDescriptionEdit As Label = DirectCast(e.Row.FindControl("LabelDescriptionEdit"), Label)
            Dim _LabelSignSup As Label = DirectCast(e.Row.FindControl("LabelSignSup"), Label)
            Dim _LabelSignMercEdit As Label = DirectCast(e.Row.FindControl("LabelSignMercEdit"), Label)
            Dim _LabelGivenToEdit As Label = DirectCast(e.Row.FindControl("LabelGivenToEdit"), Label)
            Dim _LabelRetentionEdit As Label = DirectCast(e.Row.FindControl("LabelRetentionEdit"), Label)
            Dim _LabelArchievedEdit As Label = DirectCast(e.Row.FindControl("LabelArchievedEdit"), Label)
            Dim _LabelNoteEdit As Label = DirectCast(e.Row.FindControl("LabelNoteEdit"), Label)
            Dim _LabelPaymentTermsValidationNotification As Label = DirectCast(e.Row.FindControl("LabelPaymentTermsValidationNotification"), Label)
            Dim _LiteralCommercialRoleTitle As Literal = DirectCast(e.Row.FindControl("LiteralCommercialRoleTitle"), Literal)
            Dim _LiteralLiteralAddendumValueExcVAT As Literal = DirectCast(e.Row.FindControl("LiteralLiteralAddendumValueExcVAT"), Literal)
            Dim _LiteralAddendumValueWithVAT As Literal = DirectCast(e.Row.FindControl("LiteralAddendumValueWithVAT"), Literal)
            Dim _LiteralVAT As Literal = DirectCast(e.Row.FindControl("LiteralVAT"), Literal)

            If _LabelAddendumNoEdit IsNot Nothing Then
                _LabelAddendumNoEdit.Text = ""
                _LabelDateEdit.Text = ""
                _LabelDescriptionEdit.Text = BodyTexts.Ref("foHRgj5mokmXEh62sTOoiQ")
                _LabelSignSup.Text = BodyTexts.Ref("vXdfZGm13kewiqmHmdWdyQ")
                _LabelSignMercEdit.Text = BodyTexts.Ref("hL8TNg+mj02+XJTLz0GqUA")
                _LabelGivenToEdit.Text = BodyTexts.Ref("1epQY1LCBkqFks1Ea7MVfw")
                _LabelRetentionEdit.Text = BodyTexts.Ref("AG8NVnjS50yHEBirA9v9Jw")
                _LabelArchievedEdit.Text = BodyTexts.Ref("mXA5GI226kq6JCs9nCrlaQ")
                _LabelNoteEdit.Text = BodyTexts.Ref("b7cyUgbKckK/KGTaEbD5kA")
                _LabelPaymentTermsValidationNotification.Text = BodyTexts.Ref("clXDy9WL2EyOH/r8/yjvRA")
                _LiteralCommercialRoleTitle.Text = BodyTexts.Ref("jrpT5e3Nm0iuK4hvdj36Hw")
                _LiteralLiteralAddendumValueExcVAT.Text = ""
                _LiteralAddendumValueWithVAT.Text = ""
                _LiteralVAT.Text = ""
            End If

        End If

        ' Exceptional Approval button will appear only for person who has this role
        ' It appears only if po not executed for only new generation contracts.
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ButtonExceptionalApprove As Button = DirectCast(e.Row.FindControl("ButtonExceptionalApprove"), Button)
            If ButtonExceptionalApprove IsNot Nothing Then
                'Person has this role
                If HiddenButtonExceptionalApproveShowOrNot.Value = True Then
                    ' po not executed 
                    If DataBinder.Eval(e.Row.DataItem, "PoExecuted") = False Then
                        ' new generation contract
                        If _NewGeneration = True Then
                            ButtonExceptionalApprove.Visible = True
                        End If
                    End If
                End If

            End If
        End If

        ' it highlights entire row if SignedBySupplier and SignedByMercury both not checked
        If DirectCast(e.Row.FindControl("CheckBoxSignByMercury"), CheckBox) IsNot Nothing Then
            If DirectCast(e.Row.FindControl("CheckBoxSignByMercury"), CheckBox).Checked = True AndAlso DataBinder.Eval(e.Row.DataItem, "AddendumSignBySupplier") = True Then
                ' do nothing
            Else
                e.Row.BackColor = System.Drawing.Color.Gold
            End If
        End If

        ' it adds % to retention
        If DirectCast(e.Row.FindControl("LabelRetention"), Label) IsNot Nothing Then
            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "AddendumRetention")) Then
                If Convert.ToDecimal(DirectCast(e.Row.FindControl("LabelRetention"), Label).Text) = 0.0 Then
                    DirectCast(e.Row.FindControl("LabelRetention"), Label).Text = BodyTexts.Ref("jD//OczLHUixSBZ5Z3yTBQ")
                ElseIf Convert.ToDecimal(DirectCast(e.Row.FindControl("LabelRetention"), Label).Text) = 99.9 Then
                    DirectCast(e.Row.FindControl("LabelRetention"), Label).Text = BodyTexts.Ref("9B6RQjhQrkufUFX03wVlyQ")
                Else
                    DirectCast(e.Row.FindControl("LabelRetention"), Label).Text = DirectCast(e.Row.FindControl("LabelRetention"), Label).Text.ToString + " %"
                End If
            End If
        End If

        'it defines type of PDF and DOC image if it exist or not.
        Try
            If DirectCast(e.Row.FindControl("ImageButtonLinkToTemplatefile_DOC"), ImageButton) IsNot Nothing Then

                Dim path As String = "---"
                If Convert.ToString(DataBinder.Eval(e.Row.DataItem, "AddendumLinkToTemplatefile_DOC")).Length > 0 Then
                    path = Server.MapPath(DataBinder.Eval(e.Row.DataItem, "AddendumLinkToTemplatefile_DOC"))
                End If

                If (e.Row.RowState And DataControlRowState.Edit) > 0 Then
                    DirectCast(e.Row.FindControl("ImageButtonLinkToTemplatefile_DOC"), ImageButton).Visible = True
                    DirectCast(e.Row.FindControl("ImageButtonLinkToTemplatefile_DOC"), ImageButton).ImageUrl = "~/Images/folder.png"
                Else
                    If IO.Directory.Exists(path) Then
                        DirectCast(e.Row.FindControl("ImageButtonLinkToTemplatefile_DOC"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonLinkToTemplatefile_DOC"), ImageButton).ImageUrl = "~/Images/folder.png"
                    Else
                        DirectCast(e.Row.FindControl("ImageButtonLinkToTemplatefile_DOC"), ImageButton).Visible = False
                    End If
                End If

            End If
        Catch ex As Exception

        End Try

        Try
            If DirectCast(e.Row.FindControl("ImageButtonLinkToPDFcopy"), ImageButton) IsNot Nothing Then

                Dim path As String = "---"
                If Convert.ToString(DataBinder.Eval(e.Row.DataItem, "AddendumLinkToPDFcopy")).Length > 0 Then
                    path = Server.MapPath(DataBinder.Eval(e.Row.DataItem, "AddendumLinkToPDFcopy"))
                End If

                If (e.Row.RowState And DataControlRowState.Edit) > 0 Then
                    DirectCast(e.Row.FindControl("ImageButtonLinkToPDFcopy"), ImageButton).Visible = True
                    DirectCast(e.Row.FindControl("ImageButtonLinkToPDFcopy"), ImageButton).ImageUrl = "~/Images/folder.png"
                Else
                    If IO.Directory.Exists(path) Then
                        DirectCast(e.Row.FindControl("ImageButtonLinkToPDFcopy"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonLinkToPDFcopy"), ImageButton).ImageUrl = "~/Images/folder.png"
                    Else
                        DirectCast(e.Row.FindControl("ImageButtonLinkToPDFcopy"), ImageButton).Visible = False
                    End If
                End If

            End If
        Catch ex As Exception

        End Try

        If DirectCast(e.Row.FindControl("ImageButtonBudgetPDF"), ImageButton) IsNot Nothing Then

            If System.IO.File.Exists(Server.MapPath(DataBinder.Eval(e.Row.DataItem, "BudgetLinkToPDF").ToString)) Then
                If Right(DataBinder.Eval(e.Row.DataItem, "BudgetLinkToPDF").ToString, 3).ToString = "pdf" Then
                    DirectCast(e.Row.FindControl("ImageButtonBudgetPDF"), ImageButton).ImageUrl = "~/Images/pdf.bmp"
                ElseIf Right(DataBinder.Eval(e.Row.DataItem, "BudgetLinkToPDF").ToString, 3).ToString = "doc" Then
                    DirectCast(e.Row.FindControl("ImageButtonBudgetPDF"), ImageButton).ImageUrl = "~/Images/Word.jpg"
                ElseIf Right(DataBinder.Eval(e.Row.DataItem, "BudgetLinkToPDF").ToString, 4).ToString = "docx" Then
                    DirectCast(e.Row.FindControl("ImageButtonBudgetPDF"), ImageButton).ImageUrl = "~/Images/Word.jpg"
                End If
            Else
                DirectCast(e.Row.FindControl("ImageButtonBudgetPDF"), ImageButton).Visible = False

            End If

        End If

        If DirectCast(e.Row.FindControl("LabelSortNumber"), Label) IsNot Nothing Then
            DirectCast(e.Row.FindControl("LabelSortNumber"), Label).Text = BodyTexts.Ref("U8IZNfQXVEKjw+Zetzwq/Q") + " #" + Convert.ToString(e.Row.RowIndex + 1)
        End If

        ' HIDE-SHOW control buttons  depends on the role situation 
        HIDE_SHOW_edit_delete_buttons_depends_on_the_role_situation_Addendum(e.Row)

        ' Decide edit controls to be enabled or not depending on user Role
        Decide_edit_controls_to_be_enabled_or_not_depending_on_user_Role_Addendum(e.Row)

        ' take original PO_no to evaluate on updating event
        Dim DropDownListPOnoEdit_ As DropDownList = DirectCast(e.Row.FindControl("DropDownListPOnoEdit"), DropDownList)
        If DropDownListPOnoEdit_ IsNot Nothing Then
            DirectCast(e.Row.FindControl("LabelPoNoBeforeEdit"), Label).Text = DataBinder.Eval(e.Row.DataItem, "PO_No").ToString
        End If

        ' It activates Delete CheckBoxes in Edit Mode

        Dim LinkToTemplatefile_DOCTextBoxEdit As TextBox = DirectCast(e.Row.FindControl("LinkToTemplatefile_DOCTextBoxEdit"), TextBox)
        Dim LinkToPDFcopyTextBoxEdit As TextBox = DirectCast(e.Row.FindControl("LinkToPDFcopyTextBoxEdit"), TextBox)

        ' it provides hover effent
        Dim LabelHooverToolTipAddendum As Label = DirectCast(e.Row.FindControl("LabelHooverToolTipAddendum"), Label)
        Dim LabelCreatedBy As Label = DirectCast(e.Row.FindControl("LabelCreatedBy"), Label)
        Dim LabelUpdatedBy As Label = DirectCast(e.Row.FindControl("LabelUpdatedBy"), Label)
        Dim LabelPersonCreated As Label = DirectCast(e.Row.FindControl("LabelPersonCreated"), Label)
        Dim LabelPersonUpdated As Label = DirectCast(e.Row.FindControl("LabelPersonUpdated"), Label)

        If LabelHooverToolTipAddendum IsNot Nothing Then
            If LabelCreatedBy.Text <> "" AndAlso LabelUpdatedBy.Text = "" Then
                LabelHooverToolTipAddendum.Text = "________" + LabelPersonCreated.Text + " " + BodyTexts.Ref("Lo804eOFgEWuqE9mzDqRKA") + " " + LabelCreatedBy.Text
            End If
        End If

        If LabelHooverToolTipAddendum IsNot Nothing Then
            If LabelCreatedBy.Text <> "" AndAlso LabelUpdatedBy.Text <> "" Then
                LabelHooverToolTipAddendum.Text = "________" + LabelPersonCreated.Text + " " + BodyTexts.Ref("Lo804eOFgEWuqE9mzDqRKA") + " " + LabelCreatedBy.Text + ", " + BodyTexts.Ref("dZFrjB5AC0SY9RVgUdmHgA") + ": " + LabelPersonUpdated.Text + " " + BodyTexts.Ref("Dw9Srpu14ky6r9CxlBeJpw") + " " + LabelUpdatedBy.Text
            End If
        End If

        If LabelHooverToolTipAddendum IsNot Nothing Then
            If LabelCreatedBy.Text = "" AndAlso LabelUpdatedBy.Text = "" Then
                LabelHooverToolTipAddendum.Text = "________" + BodyTexts.Ref("tg9jeQD/GEWPUOLYI+DnQA")
            End If
        End If

        If LabelHooverToolTipAddendum IsNot Nothing Then
            If LabelCreatedBy.Text = "" AndAlso LabelUpdatedBy.Text <> "" Then
                LabelHooverToolTipAddendum.Text = "________" + BodyTexts.Ref("tg9jeQD/GEWPUOLYI+DnQA") + ", " + BodyTexts.Ref("dZFrjB5AC0SY9RVgUdmHgA") + ": " + LabelPersonUpdated.Text + " " + BodyTexts.Ref("Dw9Srpu14ky6r9CxlBeJpw") + " " + LabelUpdatedBy.Text
            End If
        End If

        ' it provides select parameters for SqlDataSourcePoNoEdit
        Dim SqlDataSourcePoNo As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourcePoNo"), SqlDataSource)
        If SqlDataSourcePoNo IsNot Nothing Then
            SqlDataSourcePoNo.SelectParameters("Project_ID").DefaultValue = _ProjectID
            SqlDataSourcePoNo.SelectParameters("AddendumID").DefaultValue = DataBinder.Eval(e.Row.DataItem, "AddendumID")
        End If

        Dim DropDownListPOnoEdit As DropDownList = DirectCast(e.Row.FindControl("DropDownListPOnoEdit"), DropDownList)
        If DropDownListPOnoEdit IsNot Nothing Then
            If Len(DataBinder.Eval(e.Row.DataItem, "PO_No").ToString) = 0 Then
                DropDownListPOnoEdit.SelectedValue = ""
            Else
                DropDownListPOnoEdit.SelectedValue = DataBinder.Eval(e.Row.DataItem, "PO_No")
            End If
        End If

        ' it provides a warning if contract value doesnt exist while we have po against that.
        Dim LabelPOnoItem As Label = DirectCast(e.Row.FindControl("LabelPOnoItem"), Label)
        Dim LabelContractValueItem As Label = DirectCast(e.Row.FindControl("LabelContractValueItem"), Label)
        If LabelContractValueItem IsNot Nothing Then
            If _NewGeneration = False Then
                If Len(LabelPOnoItem.Text) > 0 Then
                    If Len(LabelContractValueItem.Text) = 0 Then
                        LabelContractValueItem.Text = "<div style=" + """" + "border: medium solid #FF0000; padding: 2px; text-align: left; background-color: #FFE4E1;" + """" + "><p><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "font-weight: bold; font-size: 10px; color: #ff0000; " + """" + ">There is a PO against this contract but no Contract Value defined yet? Ple</span></font><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "font-weight: bold; font-size: 10px; color: #ff0000; " + """" + ">ase</span></font><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "color: #cc0000; font-weight: bold; font-size: 10px; " + """" + "> </span></font><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "font-weight: bold; color: #0000ff; font-size: 10pt; " + """" + ">click edit</span></font><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "font-weight: bold; font-size: 10px; color: #ff0000; " + """" + "> to see</span></font><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "font-weight: bold; font-size: 10px; color: #ff0000; " + """" + "> PO value in dropdownlist.</span></font></p></div>"
                    ElseIf Convert.ToDecimal(LabelContractValueItem.Text.Replace(",", "")) = 0 Then
                        LabelContractValueItem.Text = "<div style=" + """" + "border: medium solid #FF0000; padding: 2px; text-align: left; background-color: #FFE4E1;" + """" + "><p><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "font-weight: bold; font-size: 10px; color: #ff0000; " + """" + ">There is a PO against this contract but no Contract Value defined yet? Ple</span></font><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "font-weight: bold; font-size: 10px; color: #ff0000; " + """" + ">ase</span></font><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "color: #cc0000; font-weight: bold; font-size: 10px; " + """" + "> </span></font><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "font-weight: bold; color: #0000ff; font-size: 10pt; " + """" + ">click edit</span></font><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "font-weight: bold; font-size: 10px; color: #ff0000; " + """" + "> to see</span></font><font face=" + """" + "arial, helvetica, sans-serif" + """" + "><span style=" + """" + "font-weight: bold; font-size: 10px; color: #ff0000; " + """" + "> PO value in dropdownlist.</span></font></p></div>"
                    End If
                End If
            End If
        End If

        Dim DropDownListPenalty As DropDownList = DirectCast(e.Row.FindControl("DropDownListPenalty"), DropDownList)
        If DropDownListPenalty IsNot Nothing Then
            If IsDBNull(DataBinder.Eval(e.Row.DataItem, "Penalties")) Then
                DropDownListPenalty.SelectedIndex = 0
            ElseIf DataBinder.Eval(e.Row.DataItem, "Penalties") = True Then
                DropDownListPenalty.SelectedValue = 1
            ElseIf DataBinder.Eval(e.Row.DataItem, "Penalties") = False Then
                DropDownListPenalty.SelectedValue = 0
            End If
        End If

        Dim DropDownListPenaltyToSupplier As DropDownList = DirectCast(e.Row.FindControl("DropDownListPenaltyToSupplier"), DropDownList)
        If DropDownListPenaltyToSupplier IsNot Nothing Then
            If IsDBNull(DataBinder.Eval(e.Row.DataItem, "PenaltiesToSupplier")) Then
                DropDownListPenaltyToSupplier.SelectedIndex = 0
            ElseIf DataBinder.Eval(e.Row.DataItem, "PenaltiesToSupplier") = True Then
                DropDownListPenaltyToSupplier.SelectedValue = 1
            ElseIf DataBinder.Eval(e.Row.DataItem, "PenaltiesToSupplier") = False Then
                DropDownListPenaltyToSupplier.SelectedValue = 0
            End If
        End If

        ' hide PO match control if project is new generation
        If DropDownListPOnoEdit_ IsNot Nothing Then
            If _NewGeneration = True Then
                DropDownListPOnoEdit_.CssClass = "hidepanel"
            ElseIf _NewGeneration = False Then
                DropDownListPOnoEdit_.Visible = True
            End If
        End If

        ' Define select parameter for approval matrix
        Dim SqlDataSourceApprovalStatus As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourceApprovalStatus"), SqlDataSource)
        Dim SqlDataSourceMissingItemsForApproval As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourceMissingItemsForApproval"), SqlDataSource)

        If SqlDataSourceApprovalStatus IsNot Nothing Then
            SqlDataSourceApprovalStatus.SelectParameters("AddendumID").DefaultValue =
              DataBinder.Eval(e.Row.DataItem, "AddendumID")
        End If


        ' Addendum Type 3 is ZERO ADDENDUM, it is Scenario 0, should be provided seperately. _
        If IsDBNull(DataBinder.Eval(e.Row.DataItem, "AddendumTypes")) = False Then
            If (DataBinder.Eval(e.Row.DataItem, "Scenario")) >= 1 _
                OrElse DataBinder.Eval(e.Row.DataItem, "AddendumTypes") = 3 Then
                If SqlDataSourceMissingItemsForApproval IsNot Nothing Then
                    SqlDataSourceMissingItemsForApproval.SelectParameters("AddendumID").DefaultValue =
                      DataBinder.Eval(e.Row.DataItem, "AddendumID")
                End If
            End If
        End If

        ' If Not New Generation Project, HIDE Commercial Section both in Item and Edit Mode
        If Not _NewGeneration Then

            Dim PanelCommercialTermsAddendumItem As Panel = DirectCast(e.Row.FindControl("PanelCommercialTermsAddendumItem"), Panel)
            If PanelCommercialTermsAddendumItem IsNot Nothing Then
                PanelCommercialTermsAddendumItem.Visible = False
            End If

            Dim PanelCommercialTermsAddendumEdit As Panel = DirectCast(e.Row.FindControl("PanelCommercialTermsAddendumEdit"), Panel)
            If PanelCommercialTermsAddendumEdit IsNot Nothing Then
                PanelCommercialTermsAddendumEdit.Visible = False
            End If

        Else
            ' If New Generation Project, HIDE Signed By Mercury checkbox. Because it is automatically signed by Code.
            ' This is cancelled as per Larisa Request. Will be updated by manually after Declan signiture.
            'Dim SignByMercuryCheckBoxEdit As CheckBox = DirectCast(e.Row.FindControl("SignByMercuryCheckBoxEdit"), CheckBox)
            'If SignByMercuryCheckBoxEdit IsNot Nothing Then
            '  SignByMercuryCheckBoxEdit.Visible = False
            'End If

        End If

        ' --------------------------------------------- EDIT MODE ---------------------------------------------
        ' Titles
        Dim LiteralLiteralAddendumValueExcVAT As Literal = DirectCast(e.Row.FindControl("LiteralLiteralAddendumValueExcVAT"), Literal)
        Dim LiteralAddendumValueWithVAT As Literal = DirectCast(e.Row.FindControl("LiteralAddendumValueWithVAT"), Literal)
        Dim LiteralVAT As Literal = DirectCast(e.Row.FindControl("LiteralVAT"), Literal)
        Dim LiteralBudgetEdit As Literal = DirectCast(e.Row.FindControl("LiteralBudgetEdit"), Literal)
        ' Controls
        Dim TextBoxAddendumValueEdit_ As TextBox = DirectCast(e.Row.FindControl("TextBoxAddendumValueEdit"), TextBox)
        Dim TextBoxAddendumValueWithVATEdit As TextBox = DirectCast(e.Row.FindControl("TextBoxAddendumValueWithVATEdit"), TextBox)
        Dim TextBoxVAT As TextBox = DirectCast(e.Row.FindControl("TextBoxVAT"), TextBox)
        Dim TextBoxBudget As TextBox = DirectCast(e.Row.FindControl("TextBoxBudget"), TextBox)
        ' Validations
        Dim RequiredFieldValidatorAddendumValueWithVATEdit As RequiredFieldValidator = DirectCast(e.Row.FindControl("RequiredFieldValidatorAddendumValueWithVATEdit"), RequiredFieldValidator)
        Dim RequiredFieldValidatorTextBoxVAT As RequiredFieldValidator = DirectCast(e.Row.FindControl("RequiredFieldValidatorTextBoxVAT"), RequiredFieldValidator)

        If (DataBinder.Eval(e.Row.DataItem, "Scenario")) >= 1 And LiteralLiteralAddendumValueExcVAT IsNot Nothing Then
            ' With VAT controls to show
            ' Titles
            LiteralLiteralAddendumValueExcVAT.Visible = False
            LiteralAddendumValueWithVAT.Visible = True
            LiteralVAT.Visible = True
            LiteralBudgetEdit.Visible = True
            ' Controls
            TextBoxAddendumValueEdit_.Visible = False
            TextBoxAddendumValueWithVATEdit.Visible = True
            TextBoxVAT.Visible = True
            TextBoxBudget.Visible = True
        ElseIf (DataBinder.Eval(e.Row.DataItem, "Scenario")) < 1 And LiteralLiteralAddendumValueExcVAT IsNot Nothing Then
            ' EXC VAT controls to show
            ' Titles
            LiteralLiteralAddendumValueExcVAT.Visible = True
            LiteralAddendumValueWithVAT.Visible = False
            LiteralVAT.Visible = False
            LiteralBudgetEdit.Visible = False
            ' Controls
            TextBoxAddendumValueEdit_.Visible = True
            TextBoxAddendumValueWithVATEdit.Visible = False
            TextBoxVAT.Visible = False
            TextBoxBudget.Visible = False
            ' Validation
            RequiredFieldValidatorAddendumValueWithVATEdit.Enabled = False
            RequiredFieldValidatorTextBoxVAT.Enabled = False
        End If

        Dim PanelAddendumType As Panel = DirectCast(e.Row.FindControl("PanelAddendumType"), Panel)
        Dim LiteralAddendumType As Label = DirectCast(e.Row.FindControl("LiteralAddendumType"), Label)
        If PanelAddendumType IsNot Nothing Then
            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "AddendumTypes")) Then
                If DataBinder.Eval(e.Row.DataItem, "AddendumTypes") = 1 Then
                    PanelAddendumType.Visible = True
                    LiteralAddendumType.Text = BodyTexts.Ref("+6d7KJolF0S53C7h0M2DsA")
                    LiteralAddendumType.ToolTip = BodyTexts.Ref("/KuDc9aMykmeEHiCfXc5rg")
                ElseIf DataBinder.Eval(e.Row.DataItem, "AddendumTypes") = 2 Then
                    PanelAddendumType.Visible = True
                    LiteralAddendumType.Text = BodyTexts.Ref("T2WmpbQsg0GsQ+h3nxRO7A")
                    LiteralAddendumType.ToolTip = BodyTexts.Ref("2PqszAQm8Ey7ONkldN7L9g")
                ElseIf DataBinder.Eval(e.Row.DataItem, "AddendumTypes") = 3 Then
                    PanelAddendumType.Visible = True
                    LiteralAddendumType.Text = BodyTexts.Ref("eR9uqJYgjkCAx/vLGu9O2A")
                    LiteralAddendumType.ToolTip = BodyTexts.Ref("nDFsU8TOGkChbCicla6KLA")
                Else
                    PanelAddendumType.Visible = False
                    LiteralAddendumType.Text = ""
                End If
            Else
                PanelAddendumType.Visible = False
                LiteralAddendumType.Text = ""
            End If
        End If

        ' ---------------------------------------------  ITEM MODE---------------------------------------------
        ' Titles
        Dim LiteralAddendumValueExcVATItem As Literal = DirectCast(e.Row.FindControl("LiteralAddendumValueExcVATItem"), Literal)
        Dim LiteralAddendumValueWithVATItem As Literal = DirectCast(e.Row.FindControl("LiteralAddendumValueWithVATItem"), Literal)
        Dim LiteralVATItem As Literal = DirectCast(e.Row.FindControl("LiteralVATItem"), Literal)
        Dim LiteralBudgetTitle As Literal = DirectCast(e.Row.FindControl("LiteralBudgetTitle"), Literal)
        ' Controls
        Dim LabelContractValueItem_ As Label = DirectCast(e.Row.FindControl("LabelContractValueItem"), Label)
        Dim LiteralAddendumValueWithVATItemValue As Literal = DirectCast(e.Row.FindControl("LiteralAddendumValueWithVATItemValue"), Literal)
        Dim LiteralVATItemValue As Literal = DirectCast(e.Row.FindControl("LiteralVATItemValue"), Literal)
        Dim LiteralBudgetValue As Literal = DirectCast(e.Row.FindControl("LiteralBudgetValue"), Literal)

        If (DataBinder.Eval(e.Row.DataItem, "Scenario")) >= 1 And LiteralAddendumValueExcVATItem IsNot Nothing Then
            ' With VAT controls to show
            ' Titles
            LiteralAddendumValueExcVATItem.Visible = False
            LiteralAddendumValueWithVATItem.Visible = True
            LiteralVATItem.Visible = True
            LiteralBudgetTitle.Visible = True
            ' Controls
            LabelContractValueItem_.Visible = False
            LiteralAddendumValueWithVATItemValue.Visible = True
            LiteralVATItemValue.Visible = True
            LiteralBudgetValue.Visible = True
            If String.IsNullOrEmpty(LiteralBudgetValue.Text) = True Then
                LiteralBudgetValue.Text = BodyTexts.Ref("17X0eiQ29kWDycnVbeJR+w")
            End If
        ElseIf (DataBinder.Eval(e.Row.DataItem, "Scenario")) < 1 And LiteralAddendumValueExcVATItem IsNot Nothing Then
            ' EXC VAT controls to show
            ' Titles
            LiteralAddendumValueExcVATItem.Visible = True
            LiteralAddendumValueWithVATItem.Visible = False
            LiteralVATItem.Visible = False
            LiteralBudgetTitle.Visible = False
            ' Controls
            LabelContractValueItem_.Visible = True
            LiteralAddendumValueWithVATItemValue.Visible = False
            LiteralVATItemValue.Visible = False
            LiteralBudgetValue.Visible = False
        End If

        ' HIDE CONTRACT NO DDL FROM NEW GENERATION PROJECTS
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim DDLContractID As DropDownList = DirectCast(e.Row.FindControl("DDLContractID"), DropDownList)
            Dim SqlDataSourceContractID As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourceContractID"), SqlDataSource)
            If DDLContractID IsNot Nothing Then
                SqlDataSourceContractID.SelectParameters("ProjectID").DefaultValue = _ProjectID
                DDLContractID.DataBind()
                DDLContractID.SelectedValue = DataBinder.Eval(e.Row.DataItem, "ContractID")
                If _NewGeneration = True Then
                    DDLContractID.Visible = False
                Else
                    DDLContractID.Visible = True
                End If
            End If
        End If

        '' Hide EDIT button if addendum executed for PO update
        '' PO executed, Scenario <> 0
        'Dim _ButtonEdit As Button = DirectCast(e.Row.FindControl("ButtonEdit"), Button)
        'If _ButtonEdit IsNot Nothing Then
        '    If CreateDataReader.Create_Table_Addendums(DataBinder.Eval(e.Row.DataItem, "AddendumID")).POexecuted = True _
        '      And (CreateDataReader.Create_Table_Addendums(DataBinder.Eval(e.Row.DataItem, "AddendumID")).Scenario <> 0 _
        '           Or CreateDataReader.Create_Table_Addendums(DataBinder.Eval(e.Row.DataItem, "AddendumID")).AddendumTypes = 3) Then

        '        _ButtonEdit.Visible = False
        '    Else
        '        _ButtonEdit.Visible = True
        '    End If
        'End If

        ' DETERMINE if Addendum Ready To Turn into Purchase Order
        If e.Row.RowType = DataControlRowType.DataRow Then
            If _POexecuted = True And
                _Exceptional = False Then
                ' if po executed then backcolor to be highlighted
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#DCFBC8")

                ' disable Approval gridview, it is approved.
                Dim GridviewApprovalStatusAddendum_ As GridView = DirectCast(e.Row.FindControl("GridviewApprovalStatusAddendum"), GridView)
                If GridviewApprovalStatusAddendum_ IsNot Nothing Then
                    GridviewApprovalStatusAddendum_.Enabled = False
                End If

                ' show PO status to user
                If DataBinder.Eval(e.Row.DataItem, "AddendumTypes") = 1 Then
                    ' REGULAR ADDENDUM
                    If DirectCast(e.Row.FindControl("LabelPoStatus"), Label) IsNot Nothing Then
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).Text = BodyTexts.Ref("mmS9MYLh5kiUt80H4wsvhw") + DataBinder.Eval(e.Row.DataItem, "Scenario").ToString
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).CssClass = "label label-success arrowed-in arrowed-in-right"
                    End If
                End If

                If DataBinder.Eval(e.Row.DataItem, "AddendumTypes") = 2 Then
                    ' REPLACE ADDENDUM
                    If DirectCast(e.Row.FindControl("LabelPoStatus"), Label) IsNot Nothing Then
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).Text = BodyTexts.Ref("3m1xtAgeYUGV8EUsdVW+tQ") + DataBinder.Eval(e.Row.DataItem, "Scenario").ToString
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).CssClass = "label label-success arrowed-in arrowed-in-right"
                    End If
                End If

                If DataBinder.Eval(e.Row.DataItem, "AddendumTypes") = 3 Then
                    ' ZERO VALUE ADDENDUM
                    If DirectCast(e.Row.FindControl("LabelPoStatus"), Label) IsNot Nothing Then
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).Text = BodyTexts.Ref("kpVHUYyIiEqHCB+7xmt52w") + DataBinder.Eval(e.Row.DataItem, "Scenario").ToString
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).CssClass = "label label-success arrowed-in arrowed-in-right"
                    End If
                End If

            ElseIf _POexecuted = True And
                            _Exceptional = True Then
                ' if po executed then backcolor to be highlighted
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFBBBB")

                ' if all required person approved, disable Approval control. If not, show. Because it still needs approval
                Dim GridviewApprovalStatusAddendum_ As GridView = DirectCast(e.Row.FindControl("GridviewApprovalStatusAddendum"), GridView)
                If GridviewApprovalStatusAddendum_ IsNot Nothing Then
                    If ContractView.AllRequiredPersonsApprovedAddendum(DataBinder.Eval(e.Row.DataItem, "AddendumID"), 0) Then
                        GridviewApprovalStatusAddendum_.Enabled = False
                    Else
                        GridviewApprovalStatusAddendum_.Enabled = True
                    End If
                End If

                ' show PO status to user
                If DataBinder.Eval(e.Row.DataItem, "AddendumTypes") = 1 Then
                    ' REGULAR ADDENDUM
                    If DirectCast(e.Row.FindControl("LabelPoStatus"), Label) IsNot Nothing Then
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).Text = BodyTexts.Ref("mmS9MYLh5kiUt80H4wsvhw") + DataBinder.Eval(e.Row.DataItem, "Scenario").ToString
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).CssClass = "label label-success arrowed-in arrowed-in-right"
                    End If
                End If

                If DataBinder.Eval(e.Row.DataItem, "AddendumTypes") = 2 Then
                    ' REPLACE ADDENDUM
                    If DirectCast(e.Row.FindControl("LabelPoStatus"), Label) IsNot Nothing Then
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).Text = BodyTexts.Ref("3m1xtAgeYUGV8EUsdVW+tQ") + DataBinder.Eval(e.Row.DataItem, "Scenario").ToString
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).CssClass = "label label-success arrowed-in arrowed-in-right"
                    End If
                End If

                If DataBinder.Eval(e.Row.DataItem, "AddendumTypes") = 3 Then
                    ' ZERO VALUE ADDENDUM
                    If DirectCast(e.Row.FindControl("LabelPoStatus"), Label) IsNot Nothing Then
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).Text = BodyTexts.Ref("kpVHUYyIiEqHCB+7xmt52w") + DataBinder.Eval(e.Row.DataItem, "Scenario").ToString
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).CssClass = "label label-success arrowed-in arrowed-in-right"
                    End If
                End If

                ' Change the Exceptional Button text 
                Dim ButtonExceptionalApprove As Button = DirectCast(e.Row.FindControl("ButtonExceptionalApprove"), Button)
                If ButtonExceptionalApprove IsNot Nothing Then
                    ButtonExceptionalApprove.Visible = True
                    ButtonExceptionalApprove.Enabled = False
                    ButtonExceptionalApprove.Text = BodyTexts.Ref("PVsElor12Uqia5rKU4mbyw")
                End If

            Else

                If DirectCast(e.Row.FindControl("LabelPoStatus"), Label) IsNot Nothing Then
                    If _NewGeneration = True Then
                        ' this message to be shown only for NEW GENERATION projects.
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).Text = BodyTexts.Ref("k/u/FC7Hu0qixXN8P/BRAg") + DataBinder.Eval(e.Row.DataItem, "Scenario").ToString
                        DirectCast(e.Row.FindControl("LabelPoStatus"), Label).CssClass = "label label-warning arrowed arrowed-right"
                    End If
                End If

            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim LiteralPenalties As Literal = DirectCast(e.Row.FindControl("LiteralPenalties"), Literal)

            If LiteralPenalties IsNot Nothing Then
                If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "Penalties")) Then

                    If DataBinder.Eval(e.Row.DataItem, "Penalties") = True Then

                        LiteralPenalties.Text = BodyTexts.Ref("2TUx46Nv1UeQLerDF4GQjA") ' to make Larisa happy

                    ElseIf DataBinder.Eval(e.Row.DataItem, "Penalties") = False Then

                        LiteralPenalties.Text = BodyTexts.Ref("sl3la1Y+vUmYXJQRX9Mosw") ' to make Larisa happy

                    End If
                End If
            End If

        End If

        Dim _hyperlink As HyperLink = DirectCast(e.Row.FindControl("HyperlinkPoNo"), HyperLink)
        If _hyperlink IsNot Nothing Then
            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "PO_No")) Then
                _hyperlink.Visible = True
                If OpenPo.GetOpen(DataBinder.Eval(e.Row.DataItem, "PO_No")) = 0 Then
                    _hyperlink.Enabled = False
                    _hyperlink.CssClass = "badge badge-grey cursor_notallowed"
                    _hyperlink.Attributes.Add("data-rel", "tooltip")
                    _hyperlink.Attributes.Add("data-original-title", BodyTexts.Ref("EKM2PaNpIEmON26Y4A2/sQ"))
                Else
                    If _InitiateContractAndAddendum = 1 Then
                        _hyperlink.Enabled = True
                        _hyperlink.CssClass = "badge badge-yellow"
                    Else
                        _hyperlink.Enabled = False
                        _hyperlink.CssClass = "badge badge-grey cursor_notallowed"
                        _hyperlink.Attributes.Add("data-rel", "tooltip")
                        _hyperlink.Attributes.Add("data-original-title", BodyTexts.Ref("qoDpxpXsBU+Q+c3/75kcZw"))
                    End If
                End If
            Else
                _hyperlink.Visible = False
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ButtonEdit_ As Button = DirectCast(e.Row.FindControl("ButtonEdit"), Button)
            Dim ButtonDelete_ As Button = DirectCast(e.Row.FindControl("ButtonDelete"), Button)
            ' These are contracts to disable (Stroimastergroup, Alfabank)
            If DataBinder.Eval(e.Row.DataItem, "ContractID") = 2992 Or DataBinder.Eval(e.Row.DataItem, "ContractID") = 4565 Then

                ButtonEdit_.Enabled = False
                ButtonDelete_.Enabled = False

            End If
        End If

        ' Activate PanelClientContractAdditionalDetails
        If _ProjectID = 999 Then
            If e.Row.RowType = DataControlRowType.DataRow Then
                ' item mode
                Dim PanelClientAddendumAdditionalDetails As Panel = DirectCast(e.Row.FindControl("PanelClientAddendumAdditionalDetails"), Panel)
                'Dim LiteralPenaltyClient As Literal = DirectCast(e.Row.FindControl("LiteralPenaltyClient"), Literal)

                If PanelClientAddendumAdditionalDetails IsNot Nothing Then
                    PanelClientAddendumAdditionalDetails.Visible = True
                End If

                ' edit mode
                Dim PanelClientAddendumAdditionalDetailsEdit As Panel = DirectCast(e.Row.FindControl("PanelClientAddendumAdditionalDetailsEdit"), Panel)
                'Dim RequiredFieldValidatorPenaltyMercuryNoteEdit As RequiredFieldValidator = DirectCast(e.Row.FindControl("RequiredFieldValidatorPenaltyMercuryNoteEdit"), RequiredFieldValidator)
                'Dim CheckBoxPenaltyToMercuryEdit As CheckBox = DirectCast(e.Row.FindControl("CheckBoxPenaltyToMercuryEdit"), CheckBox)

                If PanelClientAddendumAdditionalDetailsEdit IsNot Nothing Then
                    PanelClientAddendumAdditionalDetailsEdit.Visible = True
                End If

            End If
        End If

        If _ProjectID = 999 Then
            If e.Row.RowType = DataControlRowType.DataRow Then
                ' additional client data

                Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities
                    Dim AddendumID As Int32 = DataBinder.Eval(e.Row.DataItem, "AddendumID")

                    Dim clientAdditional = (From C In db.Table_Addendums_ClientAdditional Where C.AddendumID = AddendumID).ToList()
                    If clientAdditional.Count > 0 Then

                        'Item Template
                        Dim LiteralCompletionDate As Literal = DirectCast(e.Row.FindControl("LiteralCompletionDate"), Literal)
                        Dim CheckBoxAktOfWorkItem As UserControl = DirectCast(e.Row.FindControl("CheckBoxAktOfWorkItem"), UserControl)
                        Dim LabelLinkAktOfWorkAddendumItem As Label = DirectCast(e.Row.FindControl("LabelLinkAktOfWorkAddendumItem"), Label)
                        Dim ImageButtonClientContractAktToWorkAddendumItem As ImageButton = DirectCast(e.Row.FindControl("ImageButtonClientContractAktToWorkAddendumItem"), ImageButton)

                        If LabelLinkAktOfWorkAddendumItem IsNot Nothing Then
                            LabelLinkAktOfWorkAddendumItem.Text = clientAdditional(0).LinkToAktOfWork.Trim()
                            If LabelLinkAktOfWorkAddendumItem.Text.Trim().Length > 0 Then
                                ImageButtonClientContractAktToWorkAddendumItem.Visible = True
                            End If
                            Dim CheckBox_ As CheckBox = DirectCast(CheckBoxAktOfWorkItem.FindControl("CheckBox"), CheckBox)
                            CheckBox_.Checked = clientAdditional(0).AktOfWork

                        End If

                        If LiteralCompletionDate IsNot Nothing Then
                            If Not clientAdditional(0).CompletionDate.HasValue Then
                                LiteralCompletionDate.Text = String.Empty
                            Else
                                LiteralCompletionDate.Text = clientAdditional(0).CompletionDate
                            End If

                        End If

                        'Edit Template
                        Dim TextBoxCompletionDateEdit As TextBox = DirectCast(e.Row.FindControl("TextBoxCompletionDateEdit"), TextBox)
                        Dim CheckBoxAktOfWorkEdit As UserControl = DirectCast(e.Row.FindControl("CheckBoxAktOfWorkEdit"), UserControl)
                        Dim LabelLinkAktOfWorkAddendum As Label = DirectCast(e.Row.FindControl("LabelLinkAktOfWorkAddendum"), Label)

                        If TextBoxCompletionDateEdit IsNot Nothing Then

                            If Not clientAdditional(0).CompletionDate.HasValue Then
                                TextBoxCompletionDateEdit.Text = String.Empty
                            Else
                                TextBoxCompletionDateEdit.Text = clientAdditional(0).CompletionDate
                            End If

                            DirectCast(CheckBoxAktOfWorkEdit.FindControl("CheckBox"), CheckBox).Checked = clientAdditional(0).AktOfWork
                            LabelLinkAktOfWorkAddendum.Text = clientAdditional(0).LinkToAktOfWork.Trim
                        End If

                    End If

                    db.Dispose()

                End Using
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim tblAddCommonNewGN As HtmlTable = DirectCast(e.Row.FindControl("tblAddCommonNewGN"), HtmlTable)

            If tblAddCommonNewGN IsNot Nothing Then
                If _NewGeneration = True Then
                    tblAddCommonNewGN.Visible = True
                Else
                    tblAddCommonNewGN.Visible = False
                End If
            End If

        End If

    End Sub

    ' dont delete this. it is from Table_AddendumClientData
    Protected Function AddendumExist(ByVal AddendumID_ As Integer) As Boolean
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT count([AddendumID]) FROM [Table_AddendumClientData] WHERE AddendumID = @AddendumID "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", System.Data.SqlDbType.Int)
            AddendumID.Value = AddendumID_
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim returnValue As Boolean = False
            While dr.Read
                If dr(0) = 0 Then
                    returnValue = False
                ElseIf dr(0) = 1 Then
                    returnValue = True
                End If
            End While
            Return returnValue
            con.Close()
            dr.Close()
        End Using
    End Function

    Protected Sub GridViewShowAddendum_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)

        Dim GridViewShowAddendum As GridView = TryCast(sender, GridView)

        Dim index As Integer = Convert.ToInt32(e.RowIndex)
        Dim row As GridViewRow = GridViewShowAddendum.Rows(index)

        Dim _NewGeneration As Boolean = False

        If row.RowType = DataControlRowType.DataRow Then
            _NewGeneration = DataBinder.Eval(row.DataItem, "NewGeneration")
        End If

        Dim _AddendumID As Decimal = Convert.ToDecimal(CType(row.FindControl("LiteralAddenumID"), Literal).Text)

        Dim _ProjectID As Integer = CreateDataReader.Create_Table_Contract(CreateDataReader.Create_Table_Addendums(_AddendumID).ContractID).ProjectID

        If _NewGeneration = True Then
            ContractAndAddendumUpdateNotification.SendAddendumUpdateNotification(e, row)
        End If

        ' it updates UpdatedBy
        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        e.NewValues("UpdatedBy") = result
        e.NewValues("PersonUpdated") = Page.User.Identity.Name.ToString

        ' It updates addendum value
        Dim TextBoxAddendumValueEdit As TextBox = DirectCast(row.FindControl("TextBoxAddendumValueEdit"), TextBox)
        If TextBoxAddendumValueEdit.Text <> "" Then
            e.NewValues("AddendumValue_woVAT") = Convert.ToDecimal(TextBoxAddendumValueEdit.Text)
        ElseIf TextBoxAddendumValueEdit.Text = "" Then
            e.NewValues("AddendumValue_woVAT") = Nothing
        End If

        ' it updates addendum date
        Dim TextBoxAddendumDateEdit As TextBox = DirectCast(row.FindControl("TextBoxAddendumDateEdit"), TextBox)
        If TextBoxAddendumDateEdit.Text <> "" Then
            e.NewValues("AddendumDate") = Convert.ToDateTime(Mid(TextBoxAddendumDateEdit.Text.ToString, 1, 2).ToString + "/" + Mid(TextBoxAddendumDateEdit.Text.ToString, 4, 2).ToString + "/" + Mid(TextBoxAddendumDateEdit.Text.ToString, 7, 4).ToString)
        ElseIf TextBoxAddendumDateEdit.Text = "" Then
            e.NewValues("AddendumDate") = Nothing
        End If

        ' it updates ContractID
        Dim DDLContractID As DropDownList = DirectCast(row.FindControl("DDLContractID"), DropDownList)
        e.NewValues("ContractID") = DDLContractID.SelectedValue

        ' it updates PO_No
        Dim DropDownListPOnoEdit As DropDownList = DirectCast(row.FindControl("DropDownListPOnoEdit"), DropDownList)
        If Len(DropDownListPOnoEdit.SelectedValue.ToString) = 0 Then
            e.NewValues("PO_No") = Nothing
        Else
            SqlDataSourcePoDublication.SelectCommand = " SELECT PO_No FROM " +
                                                " (SELECT  dbo.Table_Contracts.PO_No " +
                                                " FROM  dbo.Table_Contracts " +
                                                " WHERE dbo.Table_Contracts.PO_No = '" + DropDownListPOnoEdit.SelectedValue.ToString + "' " +
                                                "  " +
                                                " UNION ALL " +
                                                "  " +
                                                " SELECT   dbo.Table_Addendums.PO_No " +
                                                " FROM         dbo.Table_Addendums  " +
                                                " WHERE dbo.Table_Addendums.PO_No = '" + DropDownListPOnoEdit.SelectedValue.ToString + "') " +
                                                " AS DataSource1 "
            DropDownListPoDublication.DataBind()
            If DropDownListPoDublication.Items.Count > 0 Then
                If DirectCast(row.FindControl("LabelPoNoBeforeEdit"), Label).Text <> DropDownListPOnoEdit.SelectedValue.ToString Then
                    e.Cancel = True
                    DirectCast(row.FindControl("LabelWarningPoDublication"), Label).Text = "PO already assigned to another contract or addendum under this supplier!"
                    DirectCast(row.FindControl("LabelWarningPoDublication"), Label).ForeColor = System.Drawing.Color.Red
                    DirectCast(row.FindControl("LabelWarningPoDublication"), Label).BorderStyle = BorderStyle.Solid
                    DirectCast(row.FindControl("LabelWarningPoDublication"), Label).BorderWidth = 2
                    DirectCast(row.FindControl("LabelWarningPoDublication"), Label).BorderColor = System.Drawing.Color.Red
                ElseIf DirectCast(row.FindControl("LabelPoNoBeforeEdit"), Label).Text = DropDownListPOnoEdit.SelectedValue.ToString Then
                    e.NewValues("PO_No") = DropDownListPOnoEdit.SelectedValue
                End If
            ElseIf DropDownListPoDublication.Items.Count = 0 Then
                e.NewValues("PO_No") = DropDownListPOnoEdit.SelectedValue
            End If
        End If

        ' update AttachmentExist column
        Dim TextLink As TextBox = DirectCast(row.FindControl("LinkToPDFcopyTextBoxEdit"), TextBox)
        If Len(TextLink.Text) > 0 OrElse Not String.IsNullOrEmpty(TextLink.Text) Then
            e.NewValues("AttachmentExist") = True
        Else
            e.NewValues("AttachmentExist") = False
        End If


        ' Delete existing PDF and DOC file if user replace with another one.
        Dim LinkToPDFcopyTextBoxEdit As TextBox = DirectCast(row.FindControl("LinkToPDFcopyTextBoxEdit"), TextBox)
        Dim LinkToTemplatefile_DOCTextBoxEdit As TextBox = DirectCast(row.FindControl("LinkToTemplatefile_DOCTextBoxEdit"), TextBox)

        Dim OldPDF As String = ""
        If e.OldValues("AddendumLinkToPDFcopy") = Nothing Then
            OldPDF = ""
        Else
            OldPDF = e.OldValues("AddendumLinkToPDFcopy").ToString
        End If

        Dim CheckBoxSignByMercury As CheckBox = DirectCast(row.FindControl("SignByMercuryCheckBoxEdit"), CheckBox)
        Dim OldSignedStatus As String = e.OldValues("AddendumSignByMercury").ToString

        Dim Notification As String = ""
        Dim SendNotification As New MyCommonTasks

        Dim contractValue As String = DirectCast(row.FindControl("TextBoxAddendumValueEdit"), TextBox).Text
        If contractValue = "" Then
            contractValue = "0.0"
        End If

        ' Commercial items
        Dim TextBoxStartDate As TextBox = DirectCast(row.FindControl("TextBoxStartDate"), TextBox)
        Dim DropDownListPenalty As DropDownList = DirectCast(row.FindControl("DropDownListPenalty"), DropDownList)
        Dim TextBoxPenaltyNote As TextBox = DirectCast(row.FindControl("TextBoxPenaltyNote"), TextBox)
        Dim DropDownListPenaltyToSupplier As DropDownList = DirectCast(row.FindControl("DropDownListPenaltyToSupplier"), DropDownList)
        Dim TextBoxPenaltyNoteSupplier As TextBox = DirectCast(row.FindControl("TextBoxPenaltyNoteSupplier"), TextBox)
        Dim TextBoxFinishDate As TextBox = DirectCast(row.FindControl("TextBoxFinishDate"), TextBox)

        If String.IsNullOrEmpty(TextBoxStartDate.Text) Then
            e.NewValues("StartDate") = Nothing
        Else
            e.NewValues("StartDate") = Convert.ToDateTime(Mid(TextBoxStartDate.Text.ToString, 1, 2).ToString +
                                     "/" + Mid(TextBoxStartDate.Text.ToString, 4, 2).ToString +
                                     "/" + Mid(TextBoxStartDate.Text.ToString, 7, 4).ToString)
        End If

        If String.IsNullOrEmpty(TextBoxFinishDate.Text) Then
            e.NewValues("FinishDate") = Nothing
        Else
            e.NewValues("FinishDate") = Convert.ToDateTime(Mid(TextBoxFinishDate.Text.ToString, 1, 2).ToString +
                               "/" + Mid(TextBoxFinishDate.Text.ToString, 4, 2).ToString +
                               "/" + Mid(TextBoxFinishDate.Text.ToString, 7, 4).ToString)
        End If

        Dim TextBoxDeliveryTerms As TextBox = DirectCast(row.FindControl("TextBoxDeliveryTerms"), TextBox)
        e.NewValues("DeliveryTerms") = TextBoxDeliveryTerms.Text

        If String.IsNullOrEmpty(DropDownListPenalty.SelectedValue.ToString) Then
            e.NewValues("Penalties") = Nothing
        Else
            e.NewValues("Penalties") = DropDownListPenalty.SelectedValue
        End If

        If String.IsNullOrEmpty(TextBoxPenaltyNote.Text) Then
            e.NewValues("PenaltiesNote") = Nothing
        Else
            e.NewValues("PenaltiesNote") = TextBoxPenaltyNote.Text
        End If

        If String.IsNullOrEmpty(DropDownListPenaltyToSupplier.SelectedValue.ToString) Then
            e.NewValues("PenaltiesToSupplier") = Nothing
        Else
            e.NewValues("PenaltiesToSupplier") = DropDownListPenaltyToSupplier.SelectedValue
        End If

        If String.IsNullOrEmpty(TextBoxPenaltyNoteSupplier.Text) Then
            e.NewValues("PenaltiesToSupplierNote") = Nothing
        Else
            e.NewValues("PenaltiesToSupplierNote") = TextBoxPenaltyNoteSupplier.Text
        End If

        ' __________END OF  Commercial items

        Dim DetailedInfo As String = "| " + BodyTexts.Ref("Mu+drePgpEK6h8XzFfTOJg") + ": " + CreateDataReader.Create_Table1_Project(_ProjectID).ProjectName.ToString +
          " |" + BodyTexts.Ref("WCc0K46nMU6P4KVjfJyRyQ") + "= " + DropDownListSupplier.SelectedItem.Text +
        " |" + BodyTexts.Ref("U8IZNfQXVEKjw+Zetzwq/Q") + "= " + DirectCast(row.FindControl("TextBoxAddendumNoEdit"), TextBox).Text +
        " |" + BodyTexts.Ref("b/QwmB2AWEmXNrVeVHPG7g") + "= " + CreateDataReader.Create_Table_Contract(DirectCast(row.FindControl("DDLContractID"), DropDownList).SelectedValue).ContractNo +
        " |" + BodyTexts.Ref("pZ3e0ec+i0uw4NVBhi2sMw") + "= " + String.Format("{0:#,##0.00}", Convert.ToDecimal(contractValue)) +
        CreateDataReader.Create_Table_Contract(DirectCast(row.FindControl("DDLContractID"), DropDownList).SelectedValue).ContractCurrency +
        Environment.NewLine + Environment.NewLine +
        " <a href=" + "'" + "http://pts.mercuryeng.ru/webforms/addendumdetails.aspx?AddendumID=" + _AddendumID.ToString + "'" + " target=" + """" + "_blank" + """" + ">" + BodyTexts.Ref("10llrtwOLEWkhl3iEOclsA") + "</a> "

        ' start to write your criterias
        If OldSignedStatus = False AndAlso OldPDF = "" AndAlso CheckBoxSignByMercury.Checked = True AndAlso Len(LinkToPDFcopyTextBoxEdit.Text) = 0 Then
            'Addendum> Signed> No Attachment
            Notification = BodyTexts.Ref("fLluYtntoEOa3vqv32Vyrg") + DetailedInfo
            SendNotification.SendNotification(Notification, _ProjectID.ToString, 3)
            SendNotification.SendEmailForContract(_ProjectID, Notification, BodyTexts.Ref("fLluYtntoEOa3vqv32Vyrg"))
        ElseIf OldSignedStatus = False AndAlso OldPDF = "" AndAlso CheckBoxSignByMercury.Checked = True AndAlso Len(LinkToPDFcopyTextBoxEdit.Text) > 0 Then
            'Addendum> Signed> Attachment added
            Notification = BodyTexts.Ref("UFAGaoN/70uyQmMAaK3fkQ") + " " + "<a href=" + """" + Replace(LinkToPDFcopyTextBoxEdit.Text, "~", "") + """" + " target=" + """" + "_blank" + """" + ">" + BodyTexts.Ref("0AlnhV1jr0GcYHql+D2yVw") + "</a>" + " " + BodyTexts.Ref("7GbSWxA4KE+wGZK57ZKzAw") + DetailedInfo
            SendNotification.SendNotification(Notification, _ProjectID.ToString, 3)
            SendNotification.SendEmailForContract(_ProjectID, Notification, BodyTexts.Ref("fdUyZHExcU+kNGAJXiMhkQ"))
        ElseIf OldSignedStatus = False AndAlso OldPDF <> "" AndAlso CheckBoxSignByMercury.Checked = True AndAlso OldPDF = LinkToPDFcopyTextBoxEdit.Text Then
            'Addendum> Signed> existed Attachment
            Notification = BodyTexts.Ref("+ax2KIMibEOf6QkjxMI6dA") + " " + "<a href=" + """" + Replace(LinkToPDFcopyTextBoxEdit.Text, "~", "") + """" + " target=" + """" + "_blank" + """" + ">" + BodyTexts.Ref("0AlnhV1jr0GcYHql+D2yVw") + "</a>" + DetailedInfo
            SendNotification.SendNotification(Notification, _ProjectID.ToString, 3)
            SendNotification.SendEmailForContract(_ProjectID, Notification, BodyTexts.Ref("f/rwXsqaT0WYF9PMRw+yvA"))
        ElseIf OldSignedStatus = True AndAlso OldPDF = "" AndAlso CheckBoxSignByMercury.Checked = True AndAlso Len(LinkToPDFcopyTextBoxEdit.Text) > 0 Then
            'Addendum> Attachment added
            Notification = BodyTexts.Ref("quLZltTfe0WkNZvFM7jqHw") + " " + "<a href=" + """" + Replace(LinkToPDFcopyTextBoxEdit.Text, "~", "") + """" + " target=" + """" + "_blank" + """" + ">" + BodyTexts.Ref("0AlnhV1jr0GcYHql+D2yVw") + "</a>" + BodyTexts.Ref("7GbSWxA4KE+wGZK57ZKzAw") + " " + DetailedInfo
            SendNotification.SendNotification(Notification, _ProjectID.ToString, 3)
            SendNotification.SendEmailForContract(_ProjectID, Notification, BodyTexts.Ref("kJICylY5I0iivEZik5SAmA"))
        ElseIf OldSignedStatus = True AndAlso CheckBoxSignByMercury.Checked = False Then
            'Addendum> Sign removed
            Notification = BodyTexts.Ref("y4gcM273B0qrDyXTUvNF5w") + DetailedInfo
            SendNotification.SendNotification(Notification, _ProjectID.ToString, 3)
            SendNotification.SendEmailForContract(_ProjectID, Notification, BodyTexts.Ref("Td2Ozs7qB06C1gPGt1YVOA"))
        ElseIf OldSignedStatus = True AndAlso CheckBoxSignByMercury.Checked = True AndAlso OldPDF <> LinkToPDFcopyTextBoxEdit.Text AndAlso Len(LinkToPDFcopyTextBoxEdit.Text) > 0 Then
            'Addendum> Attachment changed
            Notification = BodyTexts.Ref("quLZltTfe0WkNZvFM7jqHw") + " " + "<a href=" + """" + Replace(LinkToPDFcopyTextBoxEdit.Text, "~", "") + """" + " target=" + """" + "_blank" + """" + ">" + BodyTexts.Ref("0AlnhV1jr0GcYHql+D2yVw") + "</a>" + " " + BodyTexts.Ref("QmpgGBIXH0qHEMpgEl3D8Q") + DetailedInfo
            SendNotification.SendNotification(Notification, _ProjectID.ToString, 3)
            SendNotification.SendEmailForContract(_ProjectID, Notification, BodyTexts.Ref("pkFVF14BrUS4Ryxveo/w2w"))
            ' IT CAN BE ACTICATED LATER. CANNOT DELETE IF ATTACHMENT EXIST Access to the path 'E:\host\CONTRACT\Data Center Sberbank - 108\91d2bfb946e468a9.pdf' is denied
            'Dim MyTask As New MyCommonTasks
            'MyTask.DeleteAllFileOnLocalOnFTP(OldPDF)
        End If

        If Convert.ToString(e.OldValues("AddendumLinkToTemplatefile_DOC")) <> "" Then
            If e.OldValues("AddendumLinkToTemplatefile_DOC").ToString <> LinkToTemplatefile_DOCTextBoxEdit.Text Then
                ' delete DOC
                ' IT CAN BE ACTICATED LATER. CANNOT DELETE IF ATTACHMENT EXIST Access to the path 'E:\host\CONTRACT\Data Center Sberbank - 108\91d2bfb946e468a9.pdf' is denied
                'Dim LinkToDOC As String = Server.MapPath(e.OldValues("AddendumLinkToTemplatefile_DOC").ToString)
                'If Right(LinkToDOC, 4) = ".doc" OrElse Right(LinkToDOC, 5) = ".docx" Then
                'System.IO.File.Delete(LinkToDOC)
                'End If
            End If
        End If

        ' it will remove PDF and DOC files if CheckBoxes are checked.
        Dim CheckBoxBudgetDeleteDoc As CheckBox = DirectCast(row.FindControl("CheckBoxBudgetDeleteDoc"), CheckBox)

        If CheckBoxBudgetDeleteDoc.Checked = True Then
            e.NewValues("BudgetLinkToPDF") = Nothing
        End If

        ' Check Scenario Range if Contract Value goes out of range
        Dim LabelScenario As Label = DirectCast(row.FindControl("LabelScenario"), Label)
        Dim TextBoxVAT As TextBox = DirectCast(row.FindControl("TextBoxVAT"), TextBox)
        Dim TextBoxAddendumValueWithVATEdit As TextBox = DirectCast(row.FindControl("TextBoxAddendumValueWithVATEdit"), TextBox)
        Dim LabelScenarioOutOfRange As Label = DirectCast(row.FindControl("LabelScenarioOutOfRange"), Label)
        Dim LiteralAddenumID As Literal = DirectCast(row.FindControl("LiteralAddenumID"), Literal)

        If _NewGeneration = True Then
            If LabelScenario IsNot Nothing Then
                If Convert.ToInt32(LabelScenario.Text) > 0 Then

                    Dim _Scenario As Integer = Convert.ToInt32(LabelScenario.Text)
                    Dim _returnMax As Decimal = 0.0
                    Dim _returnMin As Decimal = 0.0
                    Dim Max_InEuroExcVAT As Decimal = 0
                    Dim Min_InEuroExcVAT As Decimal = 0
                    Dim _ContractCurrency As String = ContractView.GetContractCurrency(Convert.ToInt32(LiteralAddenumID.Text))
                    Dim _ContractVAT As Integer = Convert.ToInt32(TextBoxVAT.Text)

                    If _Scenario = 1 Then
                        Max_InEuroExcVAT = 5000
                    ElseIf _Scenario = 2 Then
                        Max_InEuroExcVAT = 50000
                    ElseIf _Scenario = 3 Then
                        Max_InEuroExcVAT = 100000
                    ElseIf _Scenario = 4 Then
                        Max_InEuroExcVAT = 250000
                    ElseIf _Scenario = 5 Then
                        Max_InEuroExcVAT = 1000000
                    ElseIf _Scenario = 6 Then
                        Max_InEuroExcVAT = 5000000
                    ElseIf _Scenario = 7 Then
                        Max_InEuroExcVAT = 1000000000
                    End If

                    If _Scenario = 1 Then
                        Min_InEuroExcVAT = 0.1
                    ElseIf _Scenario = 2 Then
                        Min_InEuroExcVAT = 5000
                    ElseIf _Scenario = 3 Then
                        Min_InEuroExcVAT = 50000
                    ElseIf _Scenario = 4 Then
                        Min_InEuroExcVAT = 100000
                    ElseIf _Scenario = 5 Then
                        Min_InEuroExcVAT = 250000
                    ElseIf _Scenario = 6 Then
                        Min_InEuroExcVAT = 1000000
                    ElseIf _Scenario = 7 Then
                        Min_InEuroExcVAT = 5000000
                    End If

                    Dim _date As DateTime = CreateDataReader.Create_Table_Addendums(_AddendumID).CreatedBy

                    If _ContractCurrency.ToString.ToLower = "rub" Then
                        _returnMax = Math.Round(Max_InEuroExcVAT * (MaxExchRate.GetReferringEuro(_date)) * ((100 + _ContractVAT) / 100), 2)
                    ElseIf _ContractCurrency.ToString.ToLower = "dollar" Then
                        _returnMax = Math.Round(Max_InEuroExcVAT * (MaxExchRate.GetReferringEuro(_date)) / (MaxExchRate.GetReferringDollar(_date)) * ((100 + _ContractVAT) / 100), 2)
                    ElseIf _ContractCurrency.ToString.ToLower = "euro" Then
                        _returnMax = Math.Round(Max_InEuroExcVAT * ((100 + _ContractVAT) / 100), 2)
                    End If

                    If _ContractCurrency.ToString.ToLower = "rub" Then
                        _returnMin = Math.Round(Min_InEuroExcVAT * (MaxExchRate.GetReferringEuro(_date)) * ((100 + _ContractVAT) / 100), 2)
                    ElseIf _ContractCurrency.ToString.ToLower = "dollar" Then
                        _returnMin = Math.Round(Min_InEuroExcVAT * (MaxExchRate.GetReferringEuro(_date)) / (MaxExchRate.GetReferringDollar(_date)) * ((100 + _ContractVAT) / 100), 2)
                    ElseIf _ContractCurrency.ToString.ToLower = "euro" Then
                        _returnMin = Math.Round(Min_InEuroExcVAT * ((100 + _ContractVAT) / 100), 2)
                    End If

                    If Math.Round(Convert.ToDecimal(TextBoxAddendumValueWithVATEdit.Text), 2) >= _returnMax Then
                        LabelScenarioOutOfRange.Text = " It is Scenario#" + _Scenario.ToString + ". " +
                          BodyTexts.Ref("zLrEBSfslk26b+vGy0QXTw") + " " + String.Format("{0:#,##0.00}", _returnMax) + " " + _ContractCurrency + " " + BodyTexts.Ref("uqTxdKORn0q7E+ivUFvbyw")
                        e.Cancel = True
                    End If

                    If Math.Round(Convert.ToDecimal(TextBoxAddendumValueWithVATEdit.Text), 2) < _returnMin Then
                        LabelScenarioOutOfRange.Text = " It is Scenario#" + _Scenario.ToString + ". " +
                          BodyTexts.Ref("p9Qgd6EyUUylYubxIN4SsQ") + " " + String.Format("{0:#,##0.00}", _returnMin) + " " + _ContractCurrency + " " + BodyTexts.Ref("uqTxdKORn0q7E+ivUFvbyw")
                        e.Cancel = True
                    End If

                End If
            End If
        End If

        ' Check if Validation controls on Penalty notes to be enabled or not
        Dim DDL_supplierPenalty As DropDownList = DirectCast(row.FindControl("DropDownListPenaltyToSupplier"), DropDownList)
        Dim DDL_MercuryPenalty As DropDownList = DirectCast(row.FindControl("DropDownListPenalty"), DropDownList)
        Dim _TextBoxPenaltyNote As TextBox = DirectCast(row.FindControl("TextBoxPenaltyNote"), TextBox)
        Dim _TextBoxPenaltyNoteSupplier As TextBox = DirectCast(row.FindControl("TextBoxPenaltyNoteSupplier"), TextBox)
        Dim LabelValidationPenaltyMercuryNote As Label = DirectCast(row.FindControl("LabelValidationPenaltyMercuryNote"), Label)
        Dim LabelValidationPenaltySupplierNote As Label = DirectCast(row.FindControl("LabelValidationPenaltySupplierNote"), Label)

        If DDL_MercuryPenalty.SelectedIndex = 1 And String.IsNullOrEmpty(_TextBoxPenaltyNote.Text) Then ' it is YES
            LabelValidationPenaltyMercuryNote.Text = BodyTexts.Ref("dtdbYH24dkWJyDEPdP4SNQ")
            e.Cancel = True
        Else
            LabelValidationPenaltyMercuryNote.Text = String.Empty
        End If

        If DDL_supplierPenalty.SelectedIndex = 1 And String.IsNullOrEmpty(_TextBoxPenaltyNoteSupplier.Text) Then ' it is YES
            LabelValidationPenaltySupplierNote.Text = BodyTexts.Ref("dtdbYH24dkWJyDEPdP4SNQ")
            e.Cancel = True
        Else
            LabelValidationPenaltySupplierNote.Text = String.Empty
        End If

        ' Validate Payment Terms if it is exceeding 100%
        Dim TextBoxAdvance As TextBox = DirectCast(row.FindControl("TextBoxAdvance"), TextBox)
        Dim TextBoxInterim As TextBox = DirectCast(row.FindControl("TextBoxInterim"), TextBox)
        Dim TextBoxShipment As TextBox = DirectCast(row.FindControl("TextBoxShipment"), TextBox)
        Dim TextBoxDelivery As TextBox = DirectCast(row.FindControl("TextBoxDelivery"), TextBox)
        Dim TextBoxRetention As TextBox = DirectCast(row.FindControl("TextBoxRetention"), TextBox)
        Dim LabelPaymentTermsValidationNotification As Label = DirectCast(row.FindControl("LabelPaymentTermsValidationNotification"), Label)

        If ContractView.PaymentTermsValidated(TextBoxAdvance.Text, TextBoxInterim.Text, TextBoxShipment.Text,
                                 TextBoxDelivery.Text, TextBoxRetention.Text) = False Then
            e.Cancel = True
            LabelPaymentTermsValidationNotification.Visible = True
        End If
        ' END OF Validate Payment Terms if it is exceeding 100%

        ' Update Additional Client Data
        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities
            Dim ClientData = (From C In db.Table_Addendums_ClientAdditional Where C.AddendumID = _AddendumID).ToList

            Dim TextBoxCompletionDateEdit As TextBox = DirectCast(row.FindControl("TextBoxCompletionDateEdit"), TextBox)
            Dim CheckBoxAktOfWorkEdit As UserControl = DirectCast(row.FindControl("CheckBoxAktOfWorkEdit"), UserControl)
            Dim LabelLinkAktOfWorkAddendum As Label = DirectCast(row.FindControl("LabelLinkAktOfWorkAddendum"), Label)

            If ClientData.Count > 0 Then
                ' update
                ClientData(0).CompletionDate = PTS_MERCURY.db.CustomClass.DateToEntityFromTextBox(TextBoxCompletionDateEdit.Text)

                ClientData(0).AktOfWorkWhen = LocalTime.GetTime
                ClientData(0).AktOfWork = DirectCast(CheckBoxAktOfWorkEdit.FindControl("CheckBox"), CheckBox).Checked
                ClientData(0).LinkToAktOfWork = LabelLinkAktOfWorkAddendum.Text

                db.SaveChanges()

            Else
                ' insert
                Dim entry As New PTS_MERCURY.db.Table_Addendums_ClientAdditional With {
                    .AddendumID = _AddendumID,
                    .AktOfWork = DirectCast(CheckBoxAktOfWorkEdit.FindControl("CheckBox"), CheckBox).Checked,
                    .CompletionDate = PTS_MERCURY.db.CustomClass.DateToEntityFromTextBox(TextBoxCompletionDateEdit.Text),
                    .AktOfWorkWhen = LocalTime.GetTime,
                    .LinkToAktOfWork = LabelLinkAktOfWorkAddendum.Text}

                db.Table_Addendums_ClientAdditional.Add(entry)
                db.SaveChanges()

            End If

            'End If

            db.Dispose()

        End Using

        Try
            ' validation for budget exceeding
            Dim _newvalue As Decimal = 0.0
            Dim _oldvalue As Decimal = 0.0
            _oldvalue = e.OldValues("AddendumValue_WithVAT")

            _oldvalue = Math.Round(IIf(IsNumeric(_oldvalue), _oldvalue, 0) / ((100 + (IIf(IsNumeric(TextBoxVAT.Text), TextBoxVAT.Text, 0))) / 100), 2)
            _newvalue = Math.Round(IIf(IsNumeric(TextBoxAddendumValueWithVATEdit.Text), TextBoxAddendumValueWithVATEdit.Text, 0) / ((100 + (IIf(IsNumeric(TextBoxVAT.Text), TextBoxVAT.Text, 0))) / 100), 2)

            Dim _contractid As Integer = PTS_MERCURY.helper.Table_Addendums.GetRowByPrimaryKey(_AddendumID).ContractID

            Dim _costcode As String = PTS_MERCURY.helper.Table_Contracts.GetRowByPrimaryKey(_contractid).CostCode.Trim()

            If CreateDataReader.Create_Table_Contract(_contractid).FrameContract = True Then
                ' Cost Code make sense. Take it to database for addendum to Frame Contract
                _costcode = PTS_MERCURY.helper.Table_Addendums.GetRowByPrimaryKey(_AddendumID).CostCode.Trim()
            Else
                _costcode = PTS_MERCURY.helper.Table_Contracts.GetRowByPrimaryKey(_contractid).CostCode.Trim()
            End If

            _ProjectID = PTS_MERCURY.helper.Table_Contracts.GetRowByPrimaryKey(_contractid).ProjectID

            If PTS_MERCURY.helper.Table_Budget.GetBudgetEmailControlFailed(Page,
                                                                           _ProjectID,
                                                                           _costcode,
                                                                           _oldvalue,
                                                                           _newvalue) =
                                                                       True Then

                e.Cancel = True

            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub GridViewShowAddendum_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        ' it finds the sender Gridview
        Dim GridViewShowAddendum As GridView = TryCast(sender, GridView)
        'Dim webFormCommon = New host_NET45_MVC.App_Code.WebFormCommon

        If (e.CommandName = "OpenPDF") Then
            Dim LinkToFile As String = e.CommandArgument.ToString
            'webFormCommon.processFileManager(Page, panelContainer, LinkToFile, 0)
            ASPxFileManager1.Visible = True
            ASPxFileManager1.Settings.RootFolder = LinkToFile
            ASPxFileManager1.SettingsEditing.AllowDelete = False
            ASPxFileManager1.SettingsUpload.Enabled = False

            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)

        End If

        If (e.CommandName = "OpenDOC") Then
            Dim LinkToFile As String = e.CommandArgument.ToString
            'webFormCommon.processFileManager(Page, panelContainer, LinkToFile, 0)
            ASPxFileManager1.Visible = True
            ASPxFileManager1.Settings.RootFolder = LinkToFile
            ASPxFileManager1.SettingsEditing.AllowDelete = False
            ASPxFileManager1.SettingsUpload.Enabled = False

            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)

        End If

        If (e.CommandName = "OpenPDFeditAdd") Then
            Dim LinkToFile As String = e.CommandArgument.ToString

            Dim gvr As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            'LabelTest.Text = gvr.ToString()

            Dim LinkToPDFcopyTextBoxEdit As TextBox = DirectCast(gvr.FindControl("LinkToPDFcopyTextBoxEdit"), TextBox)
            Dim LiteralAddenumID As Literal = DirectCast(gvr.FindControl("LiteralAddenumID"), Literal)

            Dim UniqueString21 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
            Dim UniqueString22 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")

            If LinkToFile.Trim().Length > 0 AndAlso IO.Directory.Exists(Server.MapPath(LinkToFile)) Then
                'webFormCommon.processFileManager(Page, panelContainer, LinkToFile, hiddenFileManagerUploadAddendumPDFMode.Value)
                ASPxFileManager1.Visible = True
                ASPxFileManager1.Settings.RootFolder = LinkToFile

                If hiddenFileManagerUploadAddendumPDFMode.Value = 1 Then
                    ASPxFileManager1.SettingsUpload.Enabled = True
                    ASPxFileManager1.SettingsEditing.AllowDelete = True
                Else
                    ASPxFileManager1.SettingsUpload.Enabled = False
                    ASPxFileManager1.SettingsEditing.AllowDelete = False
                End If
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)

            Else
                Dim cntrId As Integer = PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table_Addendums.GetRowByPrimaryKey(LiteralAddenumID.Text).ContractID
                Dim prjId As Integer = PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table_Contracts.GetRowByPrimaryKey(cntrId).ProjectID
                Dim prjName As String = PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table1_Project.GetRowByPrimaryKey(prjId).ProjectName
                Dim _directory As String = "~/CONTRACT/" + prjName.Trim() + "/" + UniqueString21 + UniqueString22

                LinkToPDFcopyTextBoxEdit.Text = _directory

                Dim _createDirectory As String = Server.MapPath(_directory)
                Directory.CreateDirectory(_createDirectory)

                'webFormCommon.processFileManager(Page, panelContainer, LinkToFile, hiddenFileManagerUploadAddendumPDFMode.Value)
                ASPxFileManager1.Visible = True
                ASPxFileManager1.Settings.RootFolder = _directory

                If hiddenFileManagerUploadAddendumPDFMode.Value = 1 Then
                    ASPxFileManager1.SettingsUpload.Enabled = True
                    ASPxFileManager1.SettingsEditing.AllowDelete = True
                Else
                    ASPxFileManager1.SettingsUpload.Enabled = False
                    ASPxFileManager1.SettingsEditing.AllowDelete = False
                End If
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)

            End If

        End If

        If (e.CommandName = "OpenDOCeditAdd") Then

            Dim LinkToFile As String = e.CommandArgument.ToString

            Dim gvr As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            'LabelTest.Text = gvr.ToString()

            Dim LinkToTemplatefile_DOCTextBoxEdit As TextBox = DirectCast(gvr.FindControl("LinkToTemplatefile_DOCTextBoxEdit"), TextBox)
            Dim LiteralAddenumID As Literal = DirectCast(gvr.FindControl("LiteralAddenumID"), Literal)

            Dim UniqueString21 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
            Dim UniqueString22 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")

            If LinkToFile.Trim().Length > 0 AndAlso IO.Directory.Exists(Server.MapPath(LinkToFile)) Then
                'webFormCommon.processFileManager(Page, panelContainer, LinkToFile, hiddenFileManagerUploadAddendumDOCMode.Value)
                ASPxFileManager1.Visible = True
                ASPxFileManager1.Settings.RootFolder = LinkToFile

                If hiddenFileManagerUploadAddendumDOCMode.Value = 1 Then
                    ASPxFileManager1.SettingsUpload.Enabled = True
                    ASPxFileManager1.SettingsEditing.AllowDelete = True
                Else
                    ASPxFileManager1.SettingsUpload.Enabled = False
                    ASPxFileManager1.SettingsEditing.AllowDelete = False
                End If
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)

            Else
                Dim cntrId As Integer = PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table_Addendums.GetRowByPrimaryKey(LiteralAddenumID.Text).ContractID
                Dim prjId As Integer = PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table_Contracts.GetRowByPrimaryKey(cntrId).ProjectID
                Dim prjName As String = PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table1_Project.GetRowByPrimaryKey(prjId).ProjectName
                Dim _directory As String = "~/CONTRACT/" + prjName.Trim() + "/" + UniqueString21 + UniqueString22
                LinkToTemplatefile_DOCTextBoxEdit.Text = _directory

                Dim _createDirectory As String = Server.MapPath(_directory)
                Directory.CreateDirectory(_createDirectory)

                'webFormCommon.processFileManager(Page, panelContainer, LinkToFile, hiddenFileManagerUploadAddendumDOCMode.Value)
                ASPxFileManager1.Visible = True
                ASPxFileManager1.Settings.RootFolder = _directory

                If hiddenFileManagerUploadAddendumDOCMode.Value = 1 Then
                    ASPxFileManager1.SettingsUpload.Enabled = True
                    ASPxFileManager1.SettingsEditing.AllowDelete = True
                Else
                    ASPxFileManager1.SettingsUpload.Enabled = False
                    ASPxFileManager1.SettingsEditing.AllowDelete = False
                End If
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)

            End If

        End If

        If (e.CommandName = "OpenBudgetPDF") Then
            Dim LinkToFile As String = e.CommandArgument.ToString
            Dim openpdf As New MyCommonTasks
            openpdf.OpenPDF(LinkToFile)
        End If

        If (e.CommandName = "ClientAddendumAktToWork") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            Dim Grid As GridView = sender

            Dim row As GridViewRow = Grid.Rows(index)

            ' open PDF here
            Dim LabelAddendumIDitem As Label = DirectCast(row.FindControl("LabelAddendumIDitem"), Label)
            PTSMainClass.OpenFile(Page, PTS_MERCURY.db.QuickTables.Table_Addendums_ClientAdditional(LabelAddendumIDitem.Text).LinkToAktOfWork)

        End If

        ' ClientData AktOfWork
        If (e.CommandName = "AktOfWorkAddendum") Then

            Dim LinkToFile As String

            Dim gvr As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)

            Dim LabelLinkAktOfWorkAddendum As Label = DirectCast(gvr.FindControl("LabelLinkAktOfWorkAddendum"), Label)
            LinkToFile = LabelLinkAktOfWorkAddendum.Text

            Dim UniqueString21 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
            Dim UniqueString22 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")

            If LinkToFile.Trim().Length > 0 AndAlso IO.Directory.Exists(Server.MapPath(LinkToFile)) Then
                'webFormCommon.processFileManager(Page, panelContainer, LinkToFile, hiddenFileManagerUploadAddendumDOCMode.Value)
                ASPxFileManager1.Visible = True
                ASPxFileManager1.Settings.RootFolder = LinkToFile

                If hiddenFileManagerUploadAddendumDOCMode.Value = 1 And (User.IsInRole("ContractLeadGirls") Or User.IsInRole("ContractSupportGirl")) Then
                    ASPxFileManager1.SettingsUpload.Enabled = True
                    ASPxFileManager1.SettingsEditing.AllowDelete = True
                Else
                    ASPxFileManager1.SettingsUpload.Enabled = False
                    ASPxFileManager1.SettingsEditing.AllowDelete = False
                End If
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)

            Else
                Dim _directory As String = "~/CONTRACT/" + DropDownListPrjID.SelectedItem.ToString + "/" + UniqueString21 + UniqueString22
                LabelLinkAktOfWorkAddendum.Text = _directory

                Dim _createDirectory As String = Server.MapPath(_directory)
                Directory.CreateDirectory(_createDirectory)

                'webFormCommon.processFileManager(Page, panelContainer, LinkToFile, hiddenFileManagerUploadAddendumDOCMode.Value)
                ASPxFileManager1.Visible = True
                ASPxFileManager1.Settings.RootFolder = _directory

                If hiddenFileManagerUploadAddendumDOCMode.Value = 1 And (User.IsInRole("ContractLeadGirls") Or User.IsInRole("ContractSupportGirl")) Then
                    ASPxFileManager1.SettingsUpload.Enabled = True
                    ASPxFileManager1.SettingsEditing.AllowDelete = True
                Else
                    ASPxFileManager1.SettingsUpload.Enabled = False
                    ASPxFileManager1.SettingsEditing.AllowDelete = False
                End If
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)

            End If

        End If

        If (e.CommandName = "AktOfWorkAddendumItem") Then

            Dim LinkToFile As String

            Dim gvr As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)

            Dim LabelLinkAktOfWorkAddendumItem As Label = DirectCast(gvr.FindControl("LabelLinkAktOfWorkAddendumItem"), Label)
            LinkToFile = LabelLinkAktOfWorkAddendumItem.Text

            Dim UniqueString21 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
            Dim UniqueString22 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")

            If LinkToFile.Trim().Length > 0 AndAlso IO.Directory.Exists(Server.MapPath(LinkToFile)) Then
                'webFormCommon.processFileManager(Page, panelContainer, LinkToFile, hiddenFileManagerUploadAddendumDOCMode.Value)
                ASPxFileManager1.Visible = True
                ASPxFileManager1.Settings.RootFolder = LinkToFile
                ASPxFileManager1.SettingsUpload.Enabled = False
                ASPxFileManager1.SettingsEditing.AllowDelete = False
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)
            End If

        End If

        ' it will upload new Budget files if required
        If (e.CommandName = "UploadBudgetPDFAddendum") Then
            ' Retrieve the row index stored in the CommandArgument property.
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            ' Retrieve the row that contains the button 
            ' from the Rows collection.
            Dim row As GridViewRow = GridViewShowAddendum.Rows(index)

            Dim FileUploadBudgetPDF As FileUpload = DirectCast(row.FindControl("FileUploadBudgetPDF"), FileUpload)
            Dim DDLProjectEdit As DropDownList = DropDownListPrjID
            Dim labelBudgetInfo As Label = DirectCast(row.FindControl("labelBudgetInfo"), Label)
            Dim TextBoxBudgetLinkPDF As TextBox = DirectCast(row.FindControl("TextBoxBudgetLinkPDF"), TextBox)

            If DDLProjectEdit.SelectedItem.ToString = "Select Project" Then
                ' This is not applicable
            Else
                If FileUploadBudgetPDF.HasFile Then
                    If System.IO.Path.GetExtension(FileUploadBudgetPDF.PostedFile.FileName) = ".doc" _
                        OrElse System.IO.Path.GetExtension(FileUploadBudgetPDF.PostedFile.FileName) = ".docx" _
                        OrElse System.IO.Path.GetExtension(FileUploadBudgetPDF.PostedFile.FileName) = ".pdf" Then
                        'If FileUploadBudgetPDF.PostedFile.ContentLength / 1000 > 5000 Then
                        '    labelInfoForAttachments.ForeColor = System.Drawing.Color.Red
                        '    labelInfoForAttachments.Text = "PDF file size must be less than 5MB"
                        '    LinkToTemplatefile_DOCTextBoxEdit.Text = ""
                        'Else
                        If Directory.Exists(Server.MapPath("~/CONTRACT/") + DDLProjectEdit.SelectedItem.ToString + "/") Then
                            Dim UniqueString11 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                            Dim UniqueString12 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                            FileUploadBudgetPDF.SaveAs(MapPath("~/CONTRACT/" + DDLProjectEdit.SelectedItem.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileUploadBudgetPDF.PostedFile.FileName)))
                            TextBoxBudgetLinkPDF.Text = "~/CONTRACT/" + DDLProjectEdit.SelectedItem.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileUploadBudgetPDF.PostedFile.FileName)
                            labelBudgetInfo.ForeColor = System.Drawing.Color.DarkGreen
                            labelBudgetInfo.Text = FileUploadBudgetPDF.PostedFile.FileName + " " + BodyTexts.Ref("nT+75PL7wkutDMuL3dxlAQ")
                        Else
                            Dim UniqueString21 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                            Dim UniqueString22 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                            Directory.CreateDirectory(Server.MapPath("~/CONTRACT/") + DDLProjectEdit.SelectedItem.ToString)
                            FileUploadBudgetPDF.SaveAs(MapPath("~/CONTRACT/" + DDLProjectEdit.SelectedItem.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileUploadBudgetPDF.PostedFile.FileName)))
                            TextBoxBudgetLinkPDF.Text = "~/CONTRACT/" + DDLProjectEdit.SelectedItem.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileUploadBudgetPDF.PostedFile.FileName)
                            labelBudgetInfo.ForeColor = System.Drawing.Color.DarkGreen
                            labelBudgetInfo.Text = FileUploadBudgetPDF.PostedFile.FileName + " " + BodyTexts.Ref("nT+75PL7wkutDMuL3dxlAQ")
                        End If
                        'End If
                    Else
                        labelBudgetInfo.ForeColor = System.Drawing.Color.Red
                        labelBudgetInfo.Text = BodyTexts.Ref("wxvsz3lzp0ORoQ7ioO6Xjw")
                        TextBoxBudgetLinkPDF.Text = ""
                    End If
                Else
                    TextBoxBudgetLinkPDF.Text = ""
                    labelBudgetInfo.ForeColor = System.Drawing.Color.Red
                    labelBudgetInfo.Text = BodyTexts.Ref("tqE5J0iPv0CoQEbW88409w")
                End If
            End If
        End If


        ' Exceptional approval
        If (e.CommandName = "ExceptionalApproveAddendum") Then
            Dim rowIndex As String = e.CommandArgument
            Dim GridviewShowAddendum_ As GridView = sender
            Dim row As GridViewRow = GridviewShowAddendum_.Rows(rowIndex)

            ' Find ContractID
            Dim LabelAddendumIDitem As Label = DirectCast(row.FindControl("LabelAddendumIDitem"), Label)
            If LabelAddendumIDitem IsNot Nothing Then

                ' Insert Or Update PO with exceptional approval
                ContractView.InsertOrUpdatePOWithExceptionalApproval(CreateDataReader.Create_Table_Contract(CreateDataReader.Create_Table_Addendums(Convert.ToInt32(LabelAddendumIDitem.Text)).ContractID).ProjectID,
                                                                     CreateDataReader.Create_Table_Addendums(Convert.ToInt32(LabelAddendumIDitem.Text)).ContractID,
                                                                     Convert.ToInt32(LabelAddendumIDitem.Text),
                                                                     Page.User.Identity.Name.ToString.ToLower,
                                                                     POdetailsForEmail)

            End If
        End If

    End Sub

    Protected Sub GridViewShowAddendum_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)
        ' it finds the sender Gridview
        Dim GridViewShowAddendum As GridView = TryCast(sender, GridView)

        Dim index As Integer = Convert.ToInt32(e.RowIndex)
        Dim row As GridViewRow = GridViewShowAddendum.Rows(index)

        ' delete existing PDF and DOC files for this specific contract
        Dim LinkToDOC As String = Server.MapPath(CreateDataReader.Create_Table_Contract((GridViewShowAddendum.DataKeys(index).Value)).LinkToTemplatefile_DOC)

        'If Right(LinkToDOC, 4) = ".doc" OrElse Right(LinkToDOC, 5) = ".docx" Then
        'System.IO.File.Delete(LinkToDOC)
        'End If

        ' make a connection to take LinkToPDF contract
        Using conPDF As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            conPDF.Open()
            Dim sqlstringPDF As String = "SELECT RTRIM(AddendumLinkToPDFcopy) AS AddendumLinkToPDFcopy FROM Table_Addendums WHERE AddendumID = " + GridViewShowAddendum.DataKeys(index).Value.ToString
            Dim cmdPDF As New SqlCommand(sqlstringPDF, conPDF)
            cmdPDF.CommandType = System.Data.CommandType.Text
            Dim drPDF As SqlDataReader = cmdPDF.ExecuteReader
            Dim LinkToPDF As String = ""
            While drPDF.Read
                LinkToPDF = drPDF(0).ToString
            End While
            conPDF.Close()
            drPDF.Close()
        End Using

        'Dim mytask As New MyCommonTasks
        'mytask.DeleteAllFileOnLocalOnFTP(LinkToPDF)

        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        Dim InstanceOfDeletion As String = "'" + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + "-" + Mid(result.ToString, 1, 2).ToString + " " + Mid(result.ToString, 12, 2).ToString + ":" + Mid(result.ToString, 15, 2).ToString + ":" + Mid(result.ToString, 18, 2).ToString + "'"

        Using cn As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            Dim cmd As New System.Data.SqlClient.SqlCommand()
            cmd.Connection = cn

            cmd.CommandText = "UPDATE Table_Addendums SET DeletedBy = " + InstanceOfDeletion + ", PersonDeleted = " + "'" + Page.User.Identity.Name.ToString + "'" + " WHERE AddendumID = " + DirectCast(row.FindControl("LabelAddendumIDitem"), Label).Text
            cmd.CommandType = System.Data.CommandType.Text
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
        End Using

    End Sub

    Protected Sub GridViewShowAddendum_RowUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdatedEventArgs)
        ' after update addendum, gridviewcontract to be updated
        GridViewShowContract.DataBind()

        Dim GridViewShowAddendum As GridView = sender
        Dim index As Integer = Convert.ToInt32(GridViewShowAddendum.EditIndex)
        Dim row As GridViewRow = GridViewShowAddendum.Rows(index)

        ' CHECK CONTRACT IF IT IS READY FOR PO
        Dim LiteralAddenumID As Literal = DirectCast(row.FindControl("LiteralAddenumID"), Literal)

        ContractView.Initiate_Checking_Contract_Or_Addendum_To_Insert_Or_Update_Po_Then_Finally_Execute(
                                                CreateDataReader.Create_Table_Addendums(Convert.ToInt32(LiteralAddenumID.Text)).ContractID,
                                                Convert.ToInt32(LiteralAddenumID.Text),
                                                POdetailsForEmail)

    End Sub

    Protected Sub GridViewShowAddendum_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs)
    End Sub

    Protected Sub SqlDataSourceShowAddendum_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
        ' after delete addendum, gridviewcontract to be updated
        GridViewShowContract.DataBind()
    End Sub

    Protected Sub ImageButtonHeaderContractNoASC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        GridViewShowContract.Sort("ContractNo", SortDirection.Ascending)
    End Sub

    Protected Sub ImageButtonHeaderContractNoDESC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        GridViewShowContract.Sort("ContractNo", SortDirection.Descending)
    End Sub

    Protected Sub ImageButtonHeaderContractDateASC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        GridViewShowContract.Sort("ContractDate", SortDirection.Ascending)
    End Sub

    Protected Sub ImageButtonHeaderContractDateDESC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        GridViewShowContract.Sort("ContractDate", SortDirection.Descending)
    End Sub

    Protected Sub ImageButtonHeaderSupplierASC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        GridViewShowContract.Sort("SupplierName", SortDirection.Ascending)
    End Sub

    Protected Sub ImageButtonHeaderSupplierDESC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        GridViewShowContract.Sort("SupplierName", SortDirection.Descending)
    End Sub

    Protected Sub ImageButtonHeaderDescriptionASC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        GridViewShowContract.Sort("ContractDescription", SortDirection.Ascending)
    End Sub

    Protected Sub ImageButtonHeaderDescriptionDESC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        GridViewShowContract.Sort("ContractDescription", SortDirection.Descending)
    End Sub

    Protected Sub ImageButtonHeaderContractTypeASC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        GridViewShowContract.Sort("ContractType", SortDirection.Ascending)
    End Sub

    Protected Sub ImageButtonHeaderContractTypeDESC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        GridViewShowContract.Sort("ContractType", SortDirection.Descending)
    End Sub

    Protected Sub ImageButtonHeaderSignedBySupplierASC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        GridViewShowContract.Sort("SignBySupplier", SortDirection.Ascending)
    End Sub

    Protected Sub ImageButtonHeaderSignedBySupplierDESC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        GridViewShowContract.Sort("SignBySupplier", SortDirection.Descending)
    End Sub

    Protected Sub ImageButtonHeaderSignedByMercuryASC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        GridViewShowContract.Sort("SignByMercury", SortDirection.Ascending)
    End Sub

    Protected Sub ImageButtonHeaderSignedByMercuryDESC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        GridViewShowContract.Sort("SignByMercury", SortDirection.Descending)
    End Sub

    Protected Sub ImageButtonHeaderCollectedBySupplierASC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        GridViewShowContract.Sort("CollectionBySupplier", SortDirection.Ascending)
    End Sub

    Protected Sub ImageButtonHeaderCollectedBySupplierDESC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        GridViewShowContract.Sort("CollectionBySupplier", SortDirection.Descending)
    End Sub

    Protected Sub ImageButtonHeaderContractValueASC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        GridViewShowContract.Sort("ContractValue_woVAT", SortDirection.Ascending)
    End Sub

    Protected Sub ImageButtonHeaderContractValueDESC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        GridViewShowContract.Sort("ContractValue_woVAT", SortDirection.Descending)
    End Sub

    Protected Sub ImageButtonHeaderRetentionASC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        GridViewShowContract.Sort("Retention", SortDirection.Ascending)
    End Sub

    Protected Sub ImageButtonHeaderRetentionDESC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        GridViewShowContract.Sort("Retention", SortDirection.Descending)
    End Sub

    Protected Sub ImageButtonHeaderArchivedASC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        GridViewShowContract.Sort("ArchivedByMercury", SortDirection.Ascending)
    End Sub

    Protected Sub ImageButtonHeaderArchivedDESC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        GridViewShowContract.Sort("ArchivedByMercury", SortDirection.Descending)
    End Sub

    Protected Sub SupplierIDTextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim SupplierIDTextBox As TextBox = TryCast(sender, TextBox)
        SupplierNameCarrier = SupplierIDTextBox.Text.ToString
        Session("SupplierID2") = SupplierIDTextBox.Text.ToString
        Dim row As GridViewRow = GridViewShowContract.Rows(Convert.ToInt32(LabelEditModeIndex.Text))
        ProjectName = DirectCast(row.FindControl("DDLProjectEdit"), DropDownList).SelectedValue.ToString
        ContractNo = DirectCast(row.FindControl("TextBoxContractNoEdit"), TextBox).Text.ToString
        ContractDate = DirectCast(row.FindControl("TextBoxContractDateEdit"), TextBox).Text.ToString
        ContractDescription = DirectCast(row.FindControl("TextBoxContractDescriptionEdit"), TextBox).Text.ToString
        ContractType = DirectCast(row.FindControl("DropDownListContractTypeEdit"), DropDownList).SelectedValue.ToString
        SignedBySupplier = DirectCast(row.FindControl("SignBySupplierCheckBoxEdit"), CheckBox).Checked
        SignedByMercury = DirectCast(row.FindControl("SignByMercuryCheckBoxEdit"), CheckBox).Checked
        CollectedBySupplier = DirectCast(row.FindControl("CollectionBySupplierCheckBoxEdit"), CheckBox).Checked
        ContractGivenTo = DirectCast(row.FindControl("TextBoxContractGivenTo"), TextBox).Text.ToString

        If DirectCast(row.FindControl("TextBoxContractValueEdit"), TextBox).Text.ToString = "" Then
            ContractValue = 0.0
        Else
            ContractValue = DirectCast(row.FindControl("TextBoxContractValueEdit"), TextBox).Text.ToString
        End If


        ContractCurrency = DirectCast(row.FindControl("DropDownListCurrencyEdit"), DropDownList).SelectedValue.ToString
        Retention = DirectCast(row.FindControl("TextBoxRetention"), TextBox).Text.ToString
        Archived = DirectCast(row.FindControl("ArchivedByMercuryCheckBoxEdit"), CheckBox).Checked
        Note = DirectCast(row.FindControl("TextBoxNoteEdit"), TextBox).Text.ToString
        LinkToDOC = DirectCast(row.FindControl("LinkToTemplatefile_DOCTextBoxEdit"), TextBox).Text.ToString
        LinkToPDF = DirectCast(row.FindControl("LinkToPDFcopyTextBoxEdit"), TextBox).Text.ToString
        GridViewShowContract.DataBind()
    End Sub

    Protected Sub DataListInsurance_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataListInsurance.Load
        If IsPostBack Or Not IsPostBack Then
            If DataListInsurance.Items.Count > 0 Then
                LabelInsurance.Visible = True
            ElseIf DataListInsurance.Items.Count = 0 Then
                LabelInsurance.Visible = False
            End If
        End If
    End Sub

    Protected Sub ImageButtonNotes_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonNotes.Click

    End Sub

    Protected Sub GridViewShowAddendum_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        ' fix width problem because of File Upload control
        Dim PanelClientAddendumAdditionalDetailsEdit As Panel = DirectCast(e.Row.FindControl("PanelClientAddendumAdditionalDetailsEdit"), Panel)
        If PanelClientAddendumAdditionalDetailsEdit IsNot Nothing Then
            PanelClientAddendumAdditionalDetailsEdit.Width = 1000
        End If

    End Sub

    Protected Sub GridViewOffers_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
    End Sub

    Protected Sub GridViewOffers_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        If (e.CommandName = "OpenZip") Then
            Dim LinkToFile As String = e.CommandArgument.ToString
            Dim openpdf As New MyCommonTasks
            openpdf.OpenPDF(LinkToFile)
        End If
    End Sub

    Protected Sub GridviewApprovalStatus_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)

        ' This class is being shared by ContractViewPage and Contract-Addendum details page
        ContractView.GridviewApprovalStatus_RowDataBound(e.Row)


    End Sub

    Protected Sub GridviewApprovalStatusAddendum_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)

        '    ' This class is being shared by ContractViewPage and Contract-Addendum details page
        ContractView.GridviewApprovalStatusAddendum_RowDataBound(e.Row)


    End Sub

    Protected Sub GridviewApprovalStatus_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        ' This class is being shared by ContractViewPage and Contract-Addendum details page
        ContractView.GridviewApprovalStatus_RowCommand(sender, e, POdetailsForEmail, Page, WebUserControl_ContractEmailBody)

        GridViewShowContract.DataBind()

    End Sub

    Protected Sub GridviewApprovalStatusAddendum_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        ' This class is being shared by ContractViewPage and Contract-Addendum details page
        ContractView.GridviewApprovalStatusAddendum_RowCommand(sender, e, POdetailsForEmail, Page, WebUserControl_AddendumEmailBody)

        GridViewShowContract.DataBind()

    End Sub

    Protected Function POvalue(ByVal _ContractID As Integer) As Decimal

        Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        con.Open()
        Dim sqlstring As String = "_PoValue"
        Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.StoredProcedure

        'syntax for parameter adding
        Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", System.Data.SqlDbType.Int)
        ContractID.Value = _ContractID
        Dim outputPO_value As SqlParameter = cmd.Parameters.Add("@po_value_return", System.Data.SqlDbType.Decimal)
        outputPO_value.Direction = System.Data.ParameterDirection.Output

        Dim _return As Decimal = 0
        cmd.ExecuteNonQuery()
        If Not IsDBNull(outputPO_value.Value) Then
            _return = outputPO_value.Value
        End If

        Return _return
        con.Close()

    End Function

    Protected Sub Decide_edit_controls_to_be_enabled_or_not_depending_on_user_Role_Contract(ByVal _row As GridViewRow)

        Dim _NewGeneration As Boolean = False

        If _row.RowType = DataControlRowType.DataRow Then
            _NewGeneration = DataBinder.Eval(_row.DataItem, "NewGeneration")
        End If

        Dim _ProjectID As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _ProjectID = DataBinder.Eval(_row.DataItem, "ProjectID").ToString
        End If

        Dim _Commercial_Items As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _Commercial_Items = DataBinder.Eval(_row.DataItem, "Commercial_Items").ToString
        End If

        Dim _ESTM As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _ESTM = DataBinder.Eval(_row.DataItem, "ESTM").ToString
        End If

        Dim _InitiateContractAndAddendum As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _InitiateContractAndAddendum = DataBinder.Eval(_row.DataItem, "InitiateContractAndAddendum").ToString
        End If

        Dim _ContractLeadGirls As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _ContractLeadGirls = DataBinder.Eval(_row.DataItem, "ContractLeadGirls").ToString
        End If

        Dim _LawyerOnSite As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _LawyerOnSite = DataBinder.Eval(_row.DataItem, "LawyerOnSite").ToString
        End If

        Dim _ContractSupportGirl As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _ContractSupportGirl = DataBinder.Eval(_row.DataItem, "ContractSupportGirl").ToString
        End If

        Dim _LawyersCanEnterContract As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _LawyersCanEnterContract = DataBinder.Eval(_row.DataItem, "LawyersCanEnterContract").ToString
        End If

        Dim _ContractControlExceptional As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _ContractControlExceptional = DataBinder.Eval(_row.DataItem, "ContractControlExceptional").ToString
        End If

        Dim _Contract_User_Junction As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _Contract_User_Junction = DataBinder.Eval(_row.DataItem, "Contract_User_Junction").ToString
        End If

        Dim _SignBySupplier As Boolean = False

        If _row.RowType = DataControlRowType.DataRow Then
            _SignBySupplier = DataBinder.Eval(_row.DataItem, "SignBySupplier").ToString
        End If

        Dim _SignByMercury As Boolean = False

        If _row.RowType = DataControlRowType.DataRow Then
            _SignByMercury = DataBinder.Eval(_row.DataItem, "SignByMercury").ToString
        End If

        Dim SupplierSigned_MercurySigned As Boolean = False

        If _SignBySupplier = 1 And _SignByMercury = 1 Then
            SupplierSigned_MercurySigned = True
        End If

        If _NewGeneration = False Then
            'Define all control variables here
            Dim DropDownListPOnoEdit_ As DropDownList = DirectCast(_row.FindControl("DropDownListPOnoEdit"), DropDownList)
            Dim DDLProjectEdit As DropDownList = DirectCast(_row.FindControl("DDLProjectEdit"), DropDownList)
            Dim TextBoxContractNoEdit As TextBox = DirectCast(_row.FindControl("TextBoxContractNoEdit"), TextBox)
            Dim TextBoxContractDateEdit As TextBox = DirectCast(_row.FindControl("TextBoxContractDateEdit"), TextBox)
            Dim SupplierIDTextBoxEdit As TextBox = DirectCast(_row.FindControl("SupplierIDTextBox"), TextBox)
            Dim TextBoxContractDescriptionEdit As TextBox = DirectCast(_row.FindControl("TextBoxContractDescriptionEdit"), TextBox)
            Dim DropDownListContractTypeEdit As DropDownList = DirectCast(_row.FindControl("DropDownListContractTypeEdit"), DropDownList)
            Dim SignBySupplierCheckBoxEdit As CheckBox = DirectCast(_row.FindControl("SignBySupplierCheckBoxEdit"), CheckBox)
            Dim SignByMercuryCheckBoxEdit As CheckBox = DirectCast(_row.FindControl("SignByMercuryCheckBoxEdit"), CheckBox)
            Dim TextBoxContractGivenTo As TextBox = DirectCast(_row.FindControl("TextBoxContractGivenTo"), TextBox)
            Dim TextBoxContractValueEdit As TextBox = DirectCast(_row.FindControl("TextBoxContractValueEdit"), TextBox)
            Dim DropDownListCurrencyEdit As DropDownList = DirectCast(_row.FindControl("DropDownListCurrencyEdit"), DropDownList)
            Dim TextBoxRetention As TextBox = DirectCast(_row.FindControl("TextBoxRetention"), TextBox)
            Dim ArchivedByMercuryCheckBoxEdit As CheckBox = DirectCast(_row.FindControl("ArchivedByMercuryCheckBoxEdit"), CheckBox)
            Dim TextBoxNoteEdit As TextBox = DirectCast(_row.FindControl("TextBoxNoteEdit"), TextBox)
            'Dim CheckBoxDeletePDFEdit As CheckBox = DirectCast(_row.FindControl("CheckBoxDeletePDF"), CheckBox)
            'Dim FileUploadPDFEdit As FileUpload = DirectCast(_row.FindControl("FileUploadPDFEdit"), FileUpload)
            'Dim ButtonUploadPDFEdit As Button = DirectCast(_row.FindControl("ButtonUploadPDFEdit"), Button)
            'Dim CheckBoxDeleteDOC As CheckBox = DirectCast(_row.FindControl("CheckBoxDeleteDOC"), CheckBox)
            'Dim FileUploadDOCEdit As FileUpload = DirectCast(_row.FindControl("FileUploadDOCEdit"), FileUpload)
            'Dim ButtonUploadDOCEdit As Button = DirectCast(_row.FindControl("ButtonUploadDOCEdit"), Button)

            ' Enable SignByMercuryCheckBoxEdit only for ContractLeadGirls
            If DropDownListPOnoEdit_ IsNot Nothing Then
                If Roles.IsUserInRole("ContractLeadGirls") Or Roles.IsUserInRole("ContractSupportGirl") Then
                    SignByMercuryCheckBoxEdit.Enabled = True
                Else
                    SignByMercuryCheckBoxEdit.Enabled = False
                End If
            End If

            If Roles.IsUserInRole("Contract") OrElse Roles.IsUserInRole("ContractEditPo") Then
                If Not Roles.IsUserInRole("Contract") AndAlso Roles.IsUserInRole("ContractEditPo") AndAlso DropDownListPOnoEdit_ IsNot Nothing Then
                    ' TRUE
                    DropDownListPOnoEdit_.Enabled = True
                    ' FALSE
                    DDLProjectEdit.Enabled = False
                    TextBoxContractNoEdit.Enabled = False
                    TextBoxContractDateEdit.Enabled = False
                    SupplierIDTextBoxEdit.Enabled = False
                    TextBoxContractDescriptionEdit.Enabled = False
                    DropDownListContractTypeEdit.Enabled = False
                    SignBySupplierCheckBoxEdit.Enabled = False
                    TextBoxContractGivenTo.Enabled = False
                    TextBoxContractValueEdit.Enabled = False
                    DropDownListCurrencyEdit.Enabled = False
                    TextBoxRetention.Enabled = False
                    ArchivedByMercuryCheckBoxEdit.Enabled = False
                    TextBoxNoteEdit.Enabled = False
                    'CheckBoxDeletePDFEdit.Enabled = False
                    'FileUploadPDFEdit.Enabled = False
                    hiddenFileManagerUploadContractPDFMode.Value = 0
                    'ButtonUploadPDFEdit.Enabled = False
                    'CheckBoxDeleteDOC.Enabled = False
                    'FileUploadDOCEdit.Enabled = False
                    hiddenFileManagerUploadContractDOCMode.Value = 0
                    'ButtonUploadDOCEdit.Enabled = False

                End If
                If Roles.IsUserInRole("Contract") AndAlso Not Roles.IsUserInRole("ContractEditPo") AndAlso DropDownListPOnoEdit_ IsNot Nothing Then
                    ' FALSE
                    DropDownListPOnoEdit_.Enabled = False
                    ' TRUE
                    DDLProjectEdit.Enabled = True
                    TextBoxContractNoEdit.Enabled = True
                    TextBoxContractDateEdit.Enabled = True
                    SupplierIDTextBoxEdit.Enabled = True
                    TextBoxContractDescriptionEdit.Enabled = True
                    DropDownListContractTypeEdit.Enabled = True
                    SignBySupplierCheckBoxEdit.Enabled = True
                    TextBoxContractGivenTo.Enabled = True
                    TextBoxContractValueEdit.Enabled = True
                    DropDownListCurrencyEdit.Enabled = True
                    TextBoxRetention.Enabled = True
                    ArchivedByMercuryCheckBoxEdit.Enabled = True
                    TextBoxNoteEdit.Enabled = True
                    'CheckBoxDeletePDFEdit.Enabled = True
                    'FileUploadPDFEdit.Enabled = True
                    hiddenFileManagerUploadContractPDFMode.Value = 1
                    'ButtonUploadPDFEdit.Enabled = True
                    'CheckBoxDeleteDOC.Enabled = True
                    'FileUploadDOCEdit.Enabled = True
                    hiddenFileManagerUploadContractDOCMode.Value = 1
                    'ButtonUploadDOCEdit.Enabled = True

                End If
                If Roles.IsUserInRole("Contract") AndAlso Roles.IsUserInRole("ContractEditPo") AndAlso DropDownListPOnoEdit_ IsNot Nothing Then
                    ' TRUE
                    DropDownListPOnoEdit_.Enabled = True
                    DDLProjectEdit.Enabled = True
                    TextBoxContractNoEdit.Enabled = True
                    TextBoxContractDateEdit.Enabled = True
                    SupplierIDTextBoxEdit.Enabled = True
                    TextBoxContractDescriptionEdit.Enabled = True
                    DropDownListContractTypeEdit.Enabled = True
                    SignBySupplierCheckBoxEdit.Enabled = True
                    TextBoxContractGivenTo.Enabled = True
                    TextBoxContractValueEdit.Enabled = True
                    DropDownListCurrencyEdit.Enabled = True
                    TextBoxRetention.Enabled = True
                    ArchivedByMercuryCheckBoxEdit.Enabled = True
                    TextBoxNoteEdit.Enabled = True
                    'CheckBoxDeletePDFEdit.Enabled = True
                    'FileUploadPDFEdit.Enabled = True
                    hiddenFileManagerUploadContractPDFMode.Value = 1
                    'ButtonUploadPDFEdit.Enabled = True
                    'CheckBoxDeleteDOC.Enabled = True
                    'FileUploadDOCEdit.Enabled = True
                    hiddenFileManagerUploadContractDOCMode.Value = 1
                    'ButtonUploadDOCEdit.Enabled = True

                End If
                If _Contract_User_Junction > 0 _
                        And Roles.IsUserInRole("Contract") = True _
                        And Roles.IsUserInRole("ContractEditPo") = True _
                        And Roles.IsUserInRole("ContractLeadGirls") = False _
                        And SupplierSigned_MercurySigned = True _
                        And DropDownListPOnoEdit_ IsNot Nothing Then
                    ' TRUE
                    DropDownListPOnoEdit_.Enabled = True
                    ' FALSE
                    DDLProjectEdit.Enabled = False
                    TextBoxContractNoEdit.Enabled = False
                    TextBoxContractDateEdit.Enabled = False
                    SupplierIDTextBoxEdit.Enabled = False
                    TextBoxContractDescriptionEdit.Enabled = False
                    DropDownListContractTypeEdit.Enabled = False
                    SignBySupplierCheckBoxEdit.Enabled = False
                    TextBoxContractGivenTo.Enabled = False
                    TextBoxContractValueEdit.Enabled = False
                    DropDownListCurrencyEdit.Enabled = False
                    TextBoxRetention.Enabled = False
                    ArchivedByMercuryCheckBoxEdit.Enabled = False
                    TextBoxNoteEdit.Enabled = False
                    'CheckBoxDeletePDFEdit.Enabled = False
                    'FileUploadPDFEdit.Enabled = False
                    hiddenFileManagerUploadContractPDFMode.Value = 0
                    'ButtonUploadPDFEdit.Enabled = False
                    'CheckBoxDeleteDOC.Enabled = False
                    'FileUploadDOCEdit.Enabled = False
                    hiddenFileManagerUploadContractDOCMode.Value = 0
                    'ButtonUploadDOCEdit.Enabled = False

                End If
            Else
                ' do nothing
            End If

        End If ' If Project NewGeneration FALSE

        If _NewGeneration = True Then
            Dim DropDownListPOnoEdit_ As DropDownList = DirectCast(_row.FindControl("DropDownListPOnoEdit"), DropDownList)

            Dim LiteralCommercialRoleTitle As Literal = DirectCast(_row.FindControl("LiteralCommercialRoleTitle"), Literal)
            If Page.User.Identity.Name.ToLower = "savas" Then
                If LiteralCommercialRoleTitle IsNot Nothing Then
                    LiteralCommercialRoleTitle.Visible = False
                End If
            End If

            ' There is a role conflict concern here.
            ' INSERT_UPDATE trigger on Table_ContractControlExceptional will validate role conflict
            ' All 4 ROLEs in the following section cannot exist for a specific user at the same time.
            ' Each user can have only one of them.
            ' UserRole table has a trigger which validates role conflict

            If Not Page.User.Identity.Name.ToLower = "savas" Then
                If DropDownListPOnoEdit_ IsNot Nothing Then ' If this check not here, then server send email for each row to Admin !!
                    If (_ContractLeadGirls = 1 Or
                        _ContractSupportGirl = 1 Or
                        _LawyerOnSite = 1) And
                          Not _InitiateContractAndAddendum = 1 And
                          Not _Commercial_Items = 1 And
                          Not _ContractControlExceptional > 0 Then
                        EnableContractControls(1, 0, 0, 0, _row)

                    ElseIf (_ContractLeadGirls = 1 Or
                    _ContractSupportGirl = 1 Or
                    _LawyerOnSite = 1) And
                      _InitiateContractAndAddendum = 1 And
                      Not _Commercial_Items = 1 And
                      Not _ContractControlExceptional > 0 Then
                        EnableContractControls(1, 0, 0, 0, _row) ' same as above. This is for Lawyers on site who has InitiateContra... role

                    ElseIf Not (_ContractLeadGirls = 1 Or
                        _ContractSupportGirl = 1 Or
                        _LawyerOnSite = 1) And
                      Not _InitiateContractAndAddendum = 1 And
                      Not _Commercial_Items = 1 And
                      _ContractControlExceptional > 0 Then
                        EnableContractControls(0, 0, 0, 1, _row)

                    ElseIf Not (_ContractLeadGirls = 1 Or
                        _ContractSupportGirl = 1 Or
                        _LawyerOnSite = 1) And
                          Not _InitiateContractAndAddendum = 1 And
                          _Commercial_Items = 1 And
                          Not _ContractControlExceptional > 0 Then
                        EnableContractControls(0, 0, 1, 0, _row) ' This is to be same with EnableContractControls(0, 1, 0, _row), see below

                    ElseIf Not (_ContractLeadGirls = 1 Or
                        _ContractSupportGirl = 1 Or
                        _LawyerOnSite = 1) And
                          _InitiateContractAndAddendum = 1 And
                          Not _Commercial_Items = 1 And
                          Not _ContractControlExceptional > 0 Then
                        EnableContractControls(0, 1, 0, 0, _row) ' This is to be same with EnableContractControls(0, 0, 1, _row), see above

                        'shouldnt be, Send Email to Admin
                    ElseIf _ContractLeadGirls = 1 And
                          _InitiateContractAndAddendum = 1 And
                          Not _Commercial_Items = 1 Then
                        If Not _LawyersCanEnterContract > 0 Then
                            _mycommonTask.SendEmailToAdmin("Role Conflict",
                                             Page.User.Identity.Name.ToLower +
                                             " has ContractLeadGirls and InitiateContractAndAddendum roles together. This is forbidden",
                                             Nothing)
                            EnableContractControls(0, 0, 0, 0, _row)
                        End If

                    ElseIf _ContractLeadGirls = 1 And
                          Not _InitiateContractAndAddendum = 1 And
                          _Commercial_Items = 1 Then
                        If Not _LawyersCanEnterContract > 0 Then
                            _mycommonTask.SendEmailToAdmin("Role Conflict",
                                             Page.User.Identity.Name.ToLower +
                                             " has ContractLeadGirls and Commercial_Items roles together. This is forbidden",
                                             Nothing)
                            EnableContractControls(0, 0, 0, 0, _row)
                        End If

                    ElseIf _ContractLeadGirls = 1 And
                          _InitiateContractAndAddendum = 1 And
                          _Commercial_Items = 1 Then
                        If Not _LawyersCanEnterContract > 0 Then
                            _mycommonTask.SendEmailToAdmin("Role Conflict",
                                             Page.User.Identity.Name.ToLower +
                                             " has ContractLeadGirls and InitiateContractAndAddendum and Commercial_Items roles together. This is forbidden",
                                             Nothing)
                            EnableContractControls(0, 0, 0, 0, _row)

                        End If

                    End If
                End If
            End If

            ' This is added to allow PDF upload after approval.
            ' It is cancelled now, may be activated later
            'If DropDownListPOnoEdit_ IsNot Nothing Then ' If this check not here, then server send email for each row to Admin !!
            '    If Roles.IsUserInRole("ContractLeadGirls") And _
            '              Not Roles.IsUserInRole("ContractEditPo") And _
            '              Not Roles.IsUserInRole("Commercial_Items") And _
            '              CreateDataReader.Create_Table1_Project(DropDownListPrjID.SelectedValue).NewGeneration = True And _
            '              DataBinder.Eval(_row.DataItem, "POexecuted") = True Then
            '        EnableContractControls(2, 0, 0, _row)
            '    End If
            'End If
        End If ' If Project NewGeneration TRUE

    End Sub

    Protected Sub EnableContractControls(ByVal _ContractLeadGirls As Integer,
                                         ByVal _InitiateContractAndAddendum As Integer,
                                         ByVal _Commercial_Items As Integer,
                                         ByVal _ContractControlExceptional As Integer,
                                         ByVal _row As GridViewRow)

        'Define all control variables here
        Dim DropDownListPOnoEdit_ As DropDownList = DirectCast(_row.FindControl("DropDownListPOnoEdit"), DropDownList)
        Dim DDLProjectEdit As DropDownList = DirectCast(_row.FindControl("DDLProjectEdit"), DropDownList)
        Dim TextBoxContractNoEdit As TextBox = DirectCast(_row.FindControl("TextBoxContractNoEdit"), TextBox)
        Dim TextBoxContractDateEdit As TextBox = DirectCast(_row.FindControl("TextBoxContractDateEdit"), TextBox)
        Dim SupplierIDTextBoxEdit As TextBox = DirectCast(_row.FindControl("SupplierIDTextBox"), TextBox)
        Dim TextBoxContractDescriptionEdit As TextBox = DirectCast(_row.FindControl("TextBoxContractDescriptionEdit"), TextBox)
        Dim DropDownListContractTypeEdit As DropDownList = DirectCast(_row.FindControl("DropDownListContractTypeEdit"), DropDownList)
        Dim SignBySupplierCheckBoxEdit As CheckBox = DirectCast(_row.FindControl("SignBySupplierCheckBoxEdit"), CheckBox)
        Dim SignByMercuryCheckBoxEdit As CheckBox = DirectCast(_row.FindControl("SignByMercuryCheckBoxEdit"), CheckBox)
        Dim TextBoxContractGivenTo As TextBox = DirectCast(_row.FindControl("TextBoxContractGivenTo"), TextBox)
        Dim TextBoxContractValueEdit As TextBox = DirectCast(_row.FindControl("TextBoxContractValueEdit"), TextBox)
        Dim DropDownListCurrencyEdit As DropDownList = DirectCast(_row.FindControl("DropDownListCurrencyEdit"), DropDownList)
        Dim TextBoxRetention As TextBox = DirectCast(_row.FindControl("TextBoxRetention"), TextBox)
        Dim ArchivedByMercuryCheckBoxEdit As CheckBox = DirectCast(_row.FindControl("ArchivedByMercuryCheckBoxEdit"), CheckBox)
        Dim TextBoxNoteEdit As TextBox = DirectCast(_row.FindControl("TextBoxNoteEdit"), TextBox)
        Dim TextBoxContractValueWithVATEdit As TextBox = DirectCast(_row.FindControl("TextBoxContractValueWithVATEdit"), TextBox)
        Dim TextBoxVAT As TextBox = DirectCast(_row.FindControl("TextBoxVAT"), TextBox)

        ' Commercial Items
        Dim TextBoxStartDate As TextBox = DirectCast(_row.FindControl("TextBoxStartDate"), TextBox)
        Dim _DropDownListPenalty As DropDownList = DirectCast(_row.FindControl("DropDownListPenalty"), DropDownList)
        Dim TextBoxPenaltyNote As TextBox = DirectCast(_row.FindControl("TextBoxPenaltyNote"), TextBox)
        Dim TextBoxDeliveryTerms As TextBox = DirectCast(_row.FindControl("TextBoxDeliveryTerms"), TextBox)
        Dim TextBoxGuaranteePeriod As TextBox = DirectCast(_row.FindControl("TextBoxGuaranteePeriod"), TextBox)
        Dim TextBoxFinishDate As TextBox = DirectCast(_row.FindControl("TextBoxFinishDate"), TextBox)
        Dim TextBoxAdvance As TextBox = DirectCast(_row.FindControl("TextBoxAdvance"), TextBox)
        Dim LiteralCommercialRoleTitle As Literal = DirectCast(_row.FindControl("LiteralCommercialRoleTitle"), Literal) ' Variable BLOCK
        Dim DropDownListPenaltyToSupplier As DropDownList = DirectCast(_row.FindControl("DropDownListPenaltyToSupplier"), DropDownList)
        Dim TextBoxPenaltyNoteSupplier As TextBox = DirectCast(_row.FindControl("TextBoxPenaltyNoteSupplier"), TextBox)
        Dim TextBoxInterim As TextBox = DirectCast(_row.FindControl("TextBoxInterim"), TextBox)
        Dim TextBoxShipment As TextBox = DirectCast(_row.FindControl("TextBoxShipment"), TextBox)
        Dim TextBoxDelivery As TextBox = DirectCast(_row.FindControl("TextBoxDelivery"), TextBox)

        Dim _Exceptional As Boolean = False

        If _row.RowType = DataControlRowType.DataRow Then
            _Exceptional = DataBinder.Eval(_row.DataItem, "Exceptional")
        End If

        If Not Page.User.Identity.Name.ToLower = "savas" Then
            If DropDownListPOnoEdit_ IsNot Nothing Then

                If _ContractLeadGirls = 1 And _InitiateContractAndAddendum = 0 And _Commercial_Items = 0 And _ContractControlExceptional = 0 And DataBinder.Eval(_row.DataItem, "PoExecuted") = False Then
                    ' Enable Contract Controls Except Currency
                    DDLProjectEdit.Enabled = True
                    TextBoxContractNoEdit.Enabled = True
                    TextBoxContractDateEdit.Enabled = True
                    SupplierIDTextBoxEdit.Enabled = True
                    TextBoxContractDescriptionEdit.Enabled = True
                    DropDownListContractTypeEdit.Enabled = True
                    SignBySupplierCheckBoxEdit.Enabled = True
                    SignByMercuryCheckBoxEdit.Enabled = True
                    TextBoxContractGivenTo.Enabled = True
                    TextBoxContractValueEdit.Enabled = True
                    DropDownListCurrencyEdit.Enabled = False
                    TextBoxRetention.Enabled = True
                    ArchivedByMercuryCheckBoxEdit.Enabled = True
                    TextBoxNoteEdit.Enabled = True
                    hiddenFileManagerUploadContractPDFMode.Value = 1
                    hiddenFileManagerUploadContractDOCMode.Value = 1
                    TextBoxContractValueWithVATEdit.Enabled = True
                    TextBoxVAT.Enabled = True

                    'Disable Commercial Items
                    LiteralCommercialRoleTitle.Visible = False
                End If ' EnableContractControls(1, 0, 0, 0, _row), POExecuted FALSE

                If _ContractLeadGirls = 1 And _InitiateContractAndAddendum = 0 And _Commercial_Items = 0 And _ContractControlExceptional = 0 And DataBinder.Eval(_row.DataItem, "PoExecuted") = True _
                    And _Exceptional = False Then
                    ' Disable Contract Controls except Signed By Mercury and PDF attachment
                    DDLProjectEdit.Enabled = False
                    TextBoxContractNoEdit.Enabled = True
                    TextBoxContractDateEdit.Enabled = True
                    SupplierIDTextBoxEdit.Enabled = False
                    TextBoxContractDescriptionEdit.Enabled = False
                    DropDownListContractTypeEdit.Enabled = False
                    SignBySupplierCheckBoxEdit.Enabled = False
                    SignByMercuryCheckBoxEdit.Enabled = True
                    TextBoxContractGivenTo.Enabled = True
                    TextBoxContractValueEdit.Enabled = False
                    DropDownListCurrencyEdit.Enabled = False
                    TextBoxRetention.Enabled = False
                    ArchivedByMercuryCheckBoxEdit.Enabled = True
                    TextBoxNoteEdit.Enabled = True
                    hiddenFileManagerUploadContractPDFMode.Value = 1
                    hiddenFileManagerUploadContractDOCMode.Value = 1
                    TextBoxContractValueWithVATEdit.Enabled = False
                    TextBoxVAT.Enabled = False

                    'Disable Commercial Items
                    LiteralCommercialRoleTitle.Visible = False
                End If ' EnableContractControls(1, 0, 0, 0, _row), POExecuted TRUE, Exceptional FALSE

                If _ContractLeadGirls = 1 And _InitiateContractAndAddendum = 0 And _Commercial_Items = 0 And _ContractControlExceptional = 0 And DataBinder.Eval(_row.DataItem, "PoExecuted") = True _
                    And _Exceptional = True Then
                    ' Enable All Contract Controls except ContractValue and Currency
                    DDLProjectEdit.Enabled = True
                    TextBoxContractNoEdit.Enabled = True
                    TextBoxContractDateEdit.Enabled = True
                    SupplierIDTextBoxEdit.Enabled = True
                    TextBoxContractDescriptionEdit.Enabled = True
                    DropDownListContractTypeEdit.Enabled = True
                    SignBySupplierCheckBoxEdit.Enabled = True
                    SignByMercuryCheckBoxEdit.Enabled = True
                    TextBoxContractGivenTo.Enabled = True
                    TextBoxContractValueEdit.Enabled = False
                    DropDownListCurrencyEdit.Enabled = False
                    TextBoxRetention.Enabled = True
                    ArchivedByMercuryCheckBoxEdit.Enabled = True
                    TextBoxNoteEdit.Enabled = True
                    hiddenFileManagerUploadContractPDFMode.Value = 1
                    hiddenFileManagerUploadContractDOCMode.Value = 1
                    TextBoxContractValueWithVATEdit.Enabled = False
                    TextBoxVAT.Enabled = False

                    'Disable Commercial Items
                    LiteralCommercialRoleTitle.Visible = False
                End If ' EnableContractControls(1, 0, 0, 0, _row), POExecuted TRUE, Exceptional TRUE

                If _ContractLeadGirls = 0 And _InitiateContractAndAddendum = 0 And _Commercial_Items = 0 And _ContractControlExceptional = 1 And DataBinder.Eval(_row.DataItem, "PoExecuted") = False Then
                    DDLProjectEdit.Enabled = False
                    TextBoxContractNoEdit.Enabled = True
                    TextBoxContractDateEdit.Enabled = True
                    SupplierIDTextBoxEdit.Enabled = False
                    TextBoxContractDescriptionEdit.Enabled = False
                    DropDownListContractTypeEdit.Enabled = False
                    SignBySupplierCheckBoxEdit.Enabled = True
                    SignByMercuryCheckBoxEdit.Enabled = True
                    TextBoxContractGivenTo.Enabled = False
                    TextBoxContractValueEdit.Enabled = False
                    DropDownListCurrencyEdit.Enabled = False
                    TextBoxRetention.Enabled = False
                    ArchivedByMercuryCheckBoxEdit.Enabled = False
                    TextBoxNoteEdit.Enabled = False
                    hiddenFileManagerUploadContractPDFMode.Value = 1
                    hiddenFileManagerUploadContractDOCMode.Value = 1
                    TextBoxContractValueWithVATEdit.Enabled = False
                    TextBoxVAT.Enabled = False

                    'Commercial Items
                    TextBoxStartDate.Enabled = False
                    _DropDownListPenalty.Enabled = False
                    TextBoxPenaltyNote.Enabled = False
                    TextBoxDeliveryTerms.Enabled = False
                    TextBoxGuaranteePeriod.Enabled = False
                    TextBoxFinishDate.Enabled = False
                    TextBoxAdvance.Enabled = False
                    DropDownListPenaltyToSupplier.Enabled = False
                    TextBoxPenaltyNoteSupplier.Enabled = False
                    TextBoxInterim.Enabled = False
                    TextBoxShipment.Enabled = False
                    TextBoxDelivery.Enabled = False
                    LiteralCommercialRoleTitle.Visible = True
                End If ' EnableContractControls(0, 0, 0, 1, _row), POExecuted FALSE

                If _ContractLeadGirls = 0 And _InitiateContractAndAddendum = 0 And _Commercial_Items = 0 And _ContractControlExceptional = 1 And DataBinder.Eval(_row.DataItem, "PoExecuted") = True _
                    And _Exceptional = False Then
                    DDLProjectEdit.Enabled = False
                    TextBoxContractNoEdit.Enabled = True
                    TextBoxContractDateEdit.Enabled = True
                    SupplierIDTextBoxEdit.Enabled = False
                    TextBoxContractDescriptionEdit.Enabled = False
                    DropDownListContractTypeEdit.Enabled = False
                    SignBySupplierCheckBoxEdit.Enabled = False
                    SignByMercuryCheckBoxEdit.Enabled = True
                    TextBoxContractGivenTo.Enabled = False
                    TextBoxContractValueEdit.Enabled = False
                    DropDownListCurrencyEdit.Enabled = False
                    TextBoxRetention.Enabled = False
                    ArchivedByMercuryCheckBoxEdit.Enabled = False
                    TextBoxNoteEdit.Enabled = False
                    hiddenFileManagerUploadContractPDFMode.Value = 1
                    hiddenFileManagerUploadContractDOCMode.Value = 1
                    TextBoxContractValueWithVATEdit.Enabled = False
                    TextBoxVAT.Enabled = False

                    'Commercial Items
                    TextBoxStartDate.Enabled = False
                    _DropDownListPenalty.Enabled = False
                    TextBoxPenaltyNote.Enabled = False
                    TextBoxDeliveryTerms.Enabled = False
                    TextBoxGuaranteePeriod.Enabled = False
                    TextBoxFinishDate.Enabled = False
                    TextBoxAdvance.Enabled = False
                    DropDownListPenaltyToSupplier.Enabled = False
                    TextBoxPenaltyNoteSupplier.Enabled = False
                    TextBoxInterim.Enabled = False
                    TextBoxShipment.Enabled = False
                    TextBoxDelivery.Enabled = False
                    LiteralCommercialRoleTitle.Visible = True
                End If ' EnableContractControls(0, 0, 0, 1, _row), POExecuted TRUE, Exceptional FALSE

                If _ContractLeadGirls = 0 And _InitiateContractAndAddendum = 0 And _Commercial_Items = 0 And _ContractControlExceptional = 1 And DataBinder.Eval(_row.DataItem, "PoExecuted") = True _
                    And _Exceptional = True Then
                    DDLProjectEdit.Enabled = False
                    TextBoxContractNoEdit.Enabled = True
                    TextBoxContractDateEdit.Enabled = True
                    SupplierIDTextBoxEdit.Enabled = False
                    TextBoxContractDescriptionEdit.Enabled = False
                    DropDownListContractTypeEdit.Enabled = False
                    SignBySupplierCheckBoxEdit.Enabled = True
                    SignByMercuryCheckBoxEdit.Enabled = True
                    TextBoxContractGivenTo.Enabled = False
                    TextBoxContractValueEdit.Enabled = False
                    DropDownListCurrencyEdit.Enabled = False
                    TextBoxRetention.Enabled = False
                    ArchivedByMercuryCheckBoxEdit.Enabled = False
                    TextBoxNoteEdit.Enabled = False
                    hiddenFileManagerUploadContractPDFMode.Value = 1
                    hiddenFileManagerUploadContractDOCMode.Value = 1
                    TextBoxContractValueWithVATEdit.Enabled = False
                    TextBoxVAT.Enabled = False

                    'Commercial Items
                    TextBoxStartDate.Enabled = False
                    _DropDownListPenalty.Enabled = False
                    TextBoxPenaltyNote.Enabled = False
                    TextBoxDeliveryTerms.Enabled = False
                    TextBoxGuaranteePeriod.Enabled = False
                    TextBoxFinishDate.Enabled = False
                    TextBoxAdvance.Enabled = False
                    DropDownListPenaltyToSupplier.Enabled = False
                    TextBoxPenaltyNoteSupplier.Enabled = False
                    TextBoxInterim.Enabled = False
                    TextBoxShipment.Enabled = False
                    TextBoxDelivery.Enabled = False
                    LiteralCommercialRoleTitle.Visible = True
                End If ' EnableContractControls(0, 0, 0, 1, _row), POExecuted TRUE, Exceptional TRUE

                If _ContractLeadGirls = 0 And _InitiateContractAndAddendum = 1 And _Commercial_Items = 0 And _ContractControlExceptional = 0 Then
                    ' Enable Contract Controls
                    DDLProjectEdit.Enabled = True
                    TextBoxContractNoEdit.Enabled = True
                    TextBoxContractDateEdit.Enabled = True
                    SupplierIDTextBoxEdit.Enabled = True
                    TextBoxContractDescriptionEdit.Enabled = True
                    DropDownListContractTypeEdit.Enabled = True
                    SignBySupplierCheckBoxEdit.Enabled = False
                    SignByMercuryCheckBoxEdit.Enabled = False
                    TextBoxContractGivenTo.Enabled = True
                    TextBoxContractValueEdit.Enabled = True
                    DropDownListCurrencyEdit.Enabled = False
                    TextBoxRetention.Enabled = True
                    ArchivedByMercuryCheckBoxEdit.Enabled = True
                    TextBoxNoteEdit.Enabled = True
                    hiddenFileManagerUploadContractPDFMode.Value = 1
                    hiddenFileManagerUploadContractDOCMode.Value = 1
                    TextBoxContractValueWithVATEdit.Enabled = True
                    TextBoxVAT.Enabled = True

                    'Enable Commercial Items
                    TextBoxStartDate.Enabled = True
                    _DropDownListPenalty.Enabled = True
                    TextBoxPenaltyNote.Enabled = True
                    TextBoxDeliveryTerms.Enabled = True
                    TextBoxGuaranteePeriod.Enabled = True
                    TextBoxFinishDate.Enabled = True
                    TextBoxAdvance.Enabled = True
                    DropDownListPenaltyToSupplier.Enabled = True
                    TextBoxPenaltyNoteSupplier.Enabled = True
                    TextBoxInterim.Enabled = True
                    TextBoxShipment.Enabled = True
                    TextBoxDelivery.Enabled = True
                    LiteralCommercialRoleTitle.Visible = False
                End If ' EnableContractControls(0, 1, 0, 0, _row)

                If _ContractLeadGirls = 0 And _InitiateContractAndAddendum = 0 And _Commercial_Items = 1 And _ContractControlExceptional = 0 Then
                    ' Enable Contract Controls
                    DDLProjectEdit.Enabled = True
                    TextBoxContractNoEdit.Enabled = True
                    TextBoxContractDateEdit.Enabled = True
                    SupplierIDTextBoxEdit.Enabled = True
                    TextBoxContractDescriptionEdit.Enabled = True
                    DropDownListContractTypeEdit.Enabled = True
                    SignBySupplierCheckBoxEdit.Enabled = False
                    SignByMercuryCheckBoxEdit.Enabled = False
                    TextBoxContractGivenTo.Enabled = True
                    TextBoxContractValueEdit.Enabled = True
                    DropDownListCurrencyEdit.Enabled = False
                    TextBoxRetention.Enabled = True
                    ArchivedByMercuryCheckBoxEdit.Enabled = True
                    TextBoxNoteEdit.Enabled = True
                    hiddenFileManagerUploadContractPDFMode.Value = 1
                    hiddenFileManagerUploadContractDOCMode.Value = 1
                    TextBoxContractValueWithVATEdit.Enabled = True
                    TextBoxVAT.Enabled = True

                    'Enable Commercial Items
                    TextBoxStartDate.Enabled = True
                    _DropDownListPenalty.Enabled = True
                    TextBoxPenaltyNote.Enabled = True
                    TextBoxDeliveryTerms.Enabled = True
                    TextBoxGuaranteePeriod.Enabled = True
                    TextBoxFinishDate.Enabled = True
                    TextBoxAdvance.Enabled = True
                    DropDownListPenaltyToSupplier.Enabled = True
                    TextBoxPenaltyNoteSupplier.Enabled = True
                    TextBoxInterim.Enabled = True
                    TextBoxShipment.Enabled = True
                    TextBoxDelivery.Enabled = True
                    LiteralCommercialRoleTitle.Visible = False
                End If ' EnableContractControls(0, 0, 1, 0, _row)

            End If
        End If

    End Sub


    Protected Sub Decide_edit_controls_to_be_enabled_or_not_depending_on_user_Role_Addendum(ByVal _row As GridViewRow)

        Dim _AddendumID As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _AddendumID = DataBinder.Eval(_row.DataItem, "AddendumID").ToString
        End If

        Dim _NewGeneration As Boolean = False

        If _row.RowType = DataControlRowType.DataRow Then
            _NewGeneration = DataBinder.Eval(_row.DataItem, "NewGeneration")
        End If

        Dim _ProjectID As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _ProjectID = DataBinder.Eval(_row.DataItem, "ProjectID").ToString
        End If

        Dim _Commercial_Items As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _Commercial_Items = DataBinder.Eval(_row.DataItem, "Commercial_Items").ToString
        End If

        Dim _ESTM As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _ESTM = DataBinder.Eval(_row.DataItem, "ESTM").ToString
        End If

        Dim _InitiateContractAndAddendum As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _InitiateContractAndAddendum = DataBinder.Eval(_row.DataItem, "InitiateContractAndAddendum").ToString
        End If

        Dim _ContractLeadGirls As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _ContractLeadGirls = DataBinder.Eval(_row.DataItem, "ContractLeadGirls").ToString
        End If

        Dim _LawyerOnSite As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _LawyerOnSite = DataBinder.Eval(_row.DataItem, "LawyerOnSite").ToString
        End If

        Dim _ContractSupportGirl As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _ContractSupportGirl = DataBinder.Eval(_row.DataItem, "ContractSupportGirl").ToString
        End If

        Dim _LawyersCanEnterContract As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _LawyersCanEnterContract = DataBinder.Eval(_row.DataItem, "LawyersCanEnterContract").ToString
        End If

        Dim _Contract_User_Junction As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _Contract_User_Junction = DataBinder.Eval(_row.DataItem, "Contract_User_Junction").ToString
        End If

        Dim _SignBySupplier As Boolean = False

        If _row.RowType = DataControlRowType.DataRow Then
            _SignBySupplier = DataBinder.Eval(_row.DataItem, "AddendumSignBySupplier").ToString
        End If

        Dim _SignByMercury As Boolean = False

        If _row.RowType = DataControlRowType.DataRow Then
            _SignByMercury = DataBinder.Eval(_row.DataItem, "AddendumSignByMercury").ToString
        End If

        Dim SupplierSigned_MercurySigned As Boolean = False

        If _SignBySupplier = 1 And _SignByMercury = 1 Then
            SupplierSigned_MercurySigned = True
        End If

        Dim _ContractControlExceptional As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _ContractControlExceptional = DataBinder.Eval(_row.DataItem, "ContractControlExceptional").ToString
        End If

        If _NewGeneration = False Then
            ' Decide edit controls to be enabled or not depending on user Role
            'Define all control variables here
            Dim DropDownListPOnoEdit_ As DropDownList = DirectCast(_row.FindControl("DropDownListPOnoEdit"), DropDownList)
            Dim DDLContractID As DropDownList = DirectCast(_row.FindControl("DDLContractID"), DropDownList)
            Dim TextBoxAddendumNoEdit As TextBox = DirectCast(_row.FindControl("TextBoxAddendumNoEdit"), TextBox)
            Dim TextBoxAddendumDateEdit As TextBox = DirectCast(_row.FindControl("TextBoxAddendumDateEdit"), TextBox)
            Dim TextBoxAddendumDescriptionEdit As TextBox = DirectCast(_row.FindControl("TextBoxAddendumDescriptionEdit"), TextBox)
            Dim SignBySupplierCheckBoxEdit As CheckBox = DirectCast(_row.FindControl("SignBySupplierCheckBoxEdit"), CheckBox)
            Dim SignByMercuryCheckBoxEdit As CheckBox = DirectCast(_row.FindControl("SignByMercuryCheckBoxEdit"), CheckBox)
            Dim TextBoxAddendumGivenTo As TextBox = DirectCast(_row.FindControl("TextBoxAddendumGivenTo"), TextBox)
            Dim TextBoxAddendumValueEdit As TextBox = DirectCast(_row.FindControl("TextBoxAddendumValueEdit"), TextBox)
            Dim TextBoxRetention As TextBox = DirectCast(_row.FindControl("TextBoxRetention"), TextBox)
            Dim ArchivedByMercuryCheckBoxEdit As CheckBox = DirectCast(_row.FindControl("ArchivedByMercuryCheckBoxEdit"), CheckBox)
            Dim TextBoxNoteEdit As TextBox = DirectCast(_row.FindControl("TextBoxNoteEdit"), TextBox)

            ' Enable SignByMercuryCheckBoxEdit only for ContractLeadGirls
            If DropDownListPOnoEdit_ IsNot Nothing Then
                If Roles.IsUserInRole("ContractLeadGirls") Or Roles.IsUserInRole("ContractSupportGirl") Then
                    SignByMercuryCheckBoxEdit.Enabled = True
                Else
                    SignByMercuryCheckBoxEdit.Enabled = False
                End If
            End If

            If Roles.IsUserInRole("Contract") OrElse Roles.IsUserInRole("ContractEditPo") Then
                If Not Roles.IsUserInRole("Contract") AndAlso Roles.IsUserInRole("ContractEditPo") AndAlso DropDownListPOnoEdit_ IsNot Nothing Then
                    ' TRUE
                    DropDownListPOnoEdit_.Enabled = True
                    ' FALSE
                    DDLContractID.Enabled = False
                    TextBoxAddendumNoEdit.Enabled = False
                    TextBoxAddendumDateEdit.Enabled = False
                    TextBoxAddendumDescriptionEdit.Enabled = False
                    SignBySupplierCheckBoxEdit.Enabled = False
                    TextBoxAddendumGivenTo.Enabled = False
                    TextBoxAddendumValueEdit.Enabled = False
                    TextBoxRetention.Enabled = False
                    ArchivedByMercuryCheckBoxEdit.Enabled = False
                    TextBoxNoteEdit.Enabled = False
                    hiddenFileManagerUploadAddendumPDFMode.Value = 0
                    hiddenFileManagerUploadAddendumDOCMode.Value = 0

                End If
                If Roles.IsUserInRole("Contract") AndAlso Not Roles.IsUserInRole("ContractEditPo") AndAlso DropDownListPOnoEdit_ IsNot Nothing Then
                    ' FALSE
                    DropDownListPOnoEdit_.Enabled = False
                    ' TRUE
                    DDLContractID.Enabled = True
                    TextBoxAddendumNoEdit.Enabled = True
                    TextBoxAddendumDateEdit.Enabled = True
                    TextBoxAddendumDescriptionEdit.Enabled = True
                    SignBySupplierCheckBoxEdit.Enabled = True
                    TextBoxAddendumGivenTo.Enabled = True
                    TextBoxAddendumValueEdit.Enabled = True
                    TextBoxRetention.Enabled = True
                    ArchivedByMercuryCheckBoxEdit.Enabled = True
                    TextBoxNoteEdit.Enabled = True
                    hiddenFileManagerUploadAddendumPDFMode.Value = 1
                    hiddenFileManagerUploadAddendumDOCMode.Value = 1

                End If
                If Roles.IsUserInRole("Contract") AndAlso Roles.IsUserInRole("ContractEditPo") AndAlso DropDownListPOnoEdit_ IsNot Nothing Then
                    ' TRUE
                    DropDownListPOnoEdit_.Enabled = True
                    DDLContractID.Enabled = True
                    TextBoxAddendumNoEdit.Enabled = True
                    TextBoxAddendumDateEdit.Enabled = True
                    TextBoxAddendumDescriptionEdit.Enabled = True
                    SignBySupplierCheckBoxEdit.Enabled = True
                    TextBoxAddendumGivenTo.Enabled = True
                    TextBoxAddendumValueEdit.Enabled = True
                    TextBoxRetention.Enabled = True
                    ArchivedByMercuryCheckBoxEdit.Enabled = True
                    TextBoxNoteEdit.Enabled = True
                    hiddenFileManagerUploadAddendumPDFMode.Value = 1
                    hiddenFileManagerUploadAddendumDOCMode.Value = 1

                End If
                If _Contract_User_Junction > 0 _
                        And Roles.IsUserInRole("Contract") = True _
                        And Roles.IsUserInRole("ContractEditPo") = True _
                        And Roles.IsUserInRole("ContractLeadGirls") = False _
                        And SupplierSigned_MercurySigned = True _
                        And DropDownListPOnoEdit_ IsNot Nothing Then
                    ' TRUE
                    DropDownListPOnoEdit_.Enabled = True
                    ' FALSE
                    DDLContractID.Enabled = False
                    TextBoxAddendumNoEdit.Enabled = False
                    TextBoxAddendumDateEdit.Enabled = False
                    TextBoxAddendumDescriptionEdit.Enabled = False
                    SignBySupplierCheckBoxEdit.Enabled = False
                    TextBoxAddendumGivenTo.Enabled = False
                    TextBoxAddendumValueEdit.Enabled = False
                    TextBoxRetention.Enabled = False
                    ArchivedByMercuryCheckBoxEdit.Enabled = False
                    TextBoxNoteEdit.Enabled = False
                    hiddenFileManagerUploadAddendumPDFMode.Value = 0
                    hiddenFileManagerUploadAddendumDOCMode.Value = 0

                End If
            Else
                ' do nothing
            End If
        End If ' Project NewGeneraion FALSE

        If _NewGeneration = True Then

            Dim DropDownListPOnoEdit_ As DropDownList = DirectCast(_row.FindControl("DropDownListPOnoEdit"), DropDownList)

            Dim LiteralCommercialRoleTitle As Literal = DirectCast(_row.FindControl("LiteralCommercialRoleTitle"), Literal)
            If Page.User.Identity.Name.ToLower = "savas" Then
                If LiteralCommercialRoleTitle IsNot Nothing Then
                    LiteralCommercialRoleTitle.Visible = False
                End If
            End If

            If Not Page.User.Identity.Name.ToLower = "savas" Then
                If DropDownListPOnoEdit_ IsNot Nothing Then ' If this check not here, then server send email for each row to Admin !!
                    If (_ContractLeadGirls = 1 Or
                        _ContractSupportGirl = 1 Or
                        _LawyerOnSite = 1) And
                          Not _InitiateContractAndAddendum = 1 And
                          Not _Commercial_Items = 1 And
                          Not _ContractControlExceptional = 1 Then
                        EnableContractControlsAddendum(1, 0, 0, 0, _row)

                    ElseIf (_ContractLeadGirls = 1 Or
                    _ContractSupportGirl = 1 Or
                    _LawyerOnSite = 1) And
                      _InitiateContractAndAddendum = 1 And
                      Not _Commercial_Items = 1 And
                      Not _ContractControlExceptional = 1 Then
                        EnableContractControlsAddendum(1, 0, 0, 0, _row) ' same as above. this is for LawyersonSite who has Initiatecontra.... role

                    ElseIf Not (_ContractLeadGirls = 1 Or
                        _ContractSupportGirl = 1 Or
                        _LawyerOnSite = 1) And
                      Not _InitiateContractAndAddendum = 1 And
                      Not _Commercial_Items = 1 And
                      _ContractControlExceptional = 1 Then
                        EnableContractControlsAddendum(0, 0, 0, 1, _row)

                    ElseIf Not (_ContractLeadGirls = 1 Or
                        _ContractSupportGirl = 1 Or
                        _LawyerOnSite = 1) And
                          Not _InitiateContractAndAddendum = 1 And
                          _Commercial_Items = 1 And
                          Not _ContractControlExceptional = 1 Then
                        EnableContractControlsAddendum(0, 0, 1, 0, _row) ' This is to be same with EnableContractControls(0, 1, 0, 0, _row), see below

                    ElseIf Not (_ContractLeadGirls = 1 Or
                        _ContractSupportGirl = 1 Or
                        _LawyerOnSite = 1) And
                          _InitiateContractAndAddendum = 1 And
                          Not _Commercial_Items = 1 And
                          Not _ContractControlExceptional = 1 Then
                        EnableContractControlsAddendum(0, 1, 0, 0, _row) ' This is to be same with EnableContractControls(0, 0, 1, 0, _row), see above

                        'shouldnt be, Send Email to Admin
                    ElseIf _ContractLeadGirls = 1 And
                          _InitiateContractAndAddendum = 1 And
                          Not _Commercial_Items = 1 Then
                        If Not _LawyersCanEnterContract = 1 Then
                            _mycommonTask.SendEmailToAdmin("Role Conflict",
                                             Page.User.Identity.Name.ToLower +
                                             " has ContractLeadGirls and InitiateContractAndAddendum roles together. This is forbidden",
                                             Nothing)
                            EnableContractControlsAddendum(0, 0, 0, 0, _row)
                        End If

                    ElseIf _ContractLeadGirls = 1 And
                          Not _InitiateContractAndAddendum = 1 And
                          _Commercial_Items = 1 Then
                        If Not _LawyersCanEnterContract = 1 Then
                            _mycommonTask.SendEmailToAdmin("Role Conflict",
                                             Page.User.Identity.Name.ToLower +
                                             " has ContractLeadGirls and Commercial_Items roles together. This is forbidden",
                                             Nothing)
                            EnableContractControlsAddendum(0, 0, 0, 0, _row)
                        End If

                    ElseIf _ContractLeadGirls = 1 And
                          _InitiateContractAndAddendum = 1 And
                          _Commercial_Items = 1 Then
                        If Not _LawyersCanEnterContract = 1 Then
                            _mycommonTask.SendEmailToAdmin("Role Conflict",
                                             Page.User.Identity.Name.ToLower +
                                             " has ContractLeadGirls and InitiateContractAndAddendum and Commercial_Items roles together. This is forbidden",
                                             Nothing)
                            EnableContractControlsAddendum(0, 0, 0, 0, _row)
                        End If

                    End If
                End If
            End If

        End If ' Project NewGeneraion TRUE

    End Sub

    Protected Sub EnableContractControlsAddendum(ByVal _ContractLeadGirls As Integer,
                                       ByVal _InitiateContractAndAddendum As Integer,
                                       ByVal _Commercial_Items As Integer,
                                       ByVal _ContractControlExceptional As Integer,
                                       ByVal _row As GridViewRow)

        Dim DropDownListPOnoEdit_ As DropDownList = DirectCast(_row.FindControl("DropDownListPOnoEdit"), DropDownList)
        Dim DDLContractID As DropDownList = DirectCast(_row.FindControl("DDLContractID"), DropDownList)
        Dim TextBoxAddendumNoEdit As TextBox = DirectCast(_row.FindControl("TextBoxAddendumNoEdit"), TextBox)
        Dim TextBoxAddendumDateEdit As TextBox = DirectCast(_row.FindControl("TextBoxAddendumDateEdit"), TextBox)
        Dim TextBoxAddendumDescriptionEdit As TextBox = DirectCast(_row.FindControl("TextBoxAddendumDescriptionEdit"), TextBox)
        Dim SignBySupplierCheckBoxEdit As CheckBox = DirectCast(_row.FindControl("SignBySupplierCheckBoxEdit"), CheckBox)
        Dim SignByMercuryCheckBoxEdit As CheckBox = DirectCast(_row.FindControl("SignByMercuryCheckBoxEdit"), CheckBox)
        Dim TextBoxAddendumGivenTo As TextBox = DirectCast(_row.FindControl("TextBoxAddendumGivenTo"), TextBox)
        Dim TextBoxAddendumValueEdit As TextBox = DirectCast(_row.FindControl("TextBoxAddendumValueEdit"), TextBox)
        Dim TextBoxRetention As TextBox = DirectCast(_row.FindControl("TextBoxRetention"), TextBox)
        Dim ArchivedByMercuryCheckBoxEdit As CheckBox = DirectCast(_row.FindControl("ArchivedByMercuryCheckBoxEdit"), CheckBox)
        Dim TextBoxNoteEdit As TextBox = DirectCast(_row.FindControl("TextBoxNoteEdit"), TextBox)
        Dim TextBoxAddendumValueWithVATEdit As TextBox = DirectCast(_row.FindControl("TextBoxAddendumValueWithVATEdit"), TextBox)
        Dim TextBoxVAT As TextBox = DirectCast(_row.FindControl("TextBoxVAT"), TextBox)

        ' Commercial Items
        Dim TextBoxStartDate As TextBox = DirectCast(_row.FindControl("TextBoxStartDate"), TextBox)
        Dim _DropDownListPenalty As DropDownList = DirectCast(_row.FindControl("DropDownListPenalty"), DropDownList)
        Dim TextBoxPenaltyNote As TextBox = DirectCast(_row.FindControl("TextBoxPenaltyNote"), TextBox)
        Dim TextBoxDeliveryTerms As TextBox = DirectCast(_row.FindControl("TextBoxDeliveryTerms"), TextBox)
        Dim TextBoxGuaranteePeriod As TextBox = DirectCast(_row.FindControl("TextBoxGuaranteePeriod"), TextBox)
        Dim TextBoxFinishDate As TextBox = DirectCast(_row.FindControl("TextBoxFinishDate"), TextBox)
        Dim TextBoxAdvance As TextBox = DirectCast(_row.FindControl("TextBoxAdvance"), TextBox)
        Dim LiteralCommercialRoleTitle As Literal = DirectCast(_row.FindControl("LiteralCommercialRoleTitle"), Literal) ' Variable BLOCK
        Dim DropDownListPenaltyToSupplier As DropDownList = DirectCast(_row.FindControl("DropDownListPenaltyToSupplier"), DropDownList)
        Dim TextBoxPenaltyNoteSupplier As TextBox = DirectCast(_row.FindControl("TextBoxPenaltyNoteSupplier"), TextBox)
        Dim TextBoxInterim As TextBox = DirectCast(_row.FindControl("TextBoxInterim"), TextBox)
        Dim TextBoxShipment As TextBox = DirectCast(_row.FindControl("TextBoxShipment"), TextBox)
        Dim TextBoxDelivery As TextBox = DirectCast(_row.FindControl("TextBoxDelivery"), TextBox)

        Dim _Exceptional As Boolean = False

        If _row.RowType = DataControlRowType.DataRow Then
            _Exceptional = DataBinder.Eval(_row.DataItem, "Exceptional")
        End If

        If Not Page.User.Identity.Name.ToLower = "savas" Then
            If DropDownListPOnoEdit_ IsNot Nothing Then

                If _ContractLeadGirls = 1 And _InitiateContractAndAddendum = 0 And _Commercial_Items = 0 And _ContractControlExceptional = 0 And DataBinder.Eval(_row.DataItem, "PoExecuted") = False Then
                    ' Enable Contract Controls
                    DDLContractID.Enabled = True
                    TextBoxAddendumNoEdit.Enabled = True
                    TextBoxAddendumDateEdit.Enabled = True
                    TextBoxAddendumDescriptionEdit.Enabled = True
                    SignBySupplierCheckBoxEdit.Enabled = True
                    SignByMercuryCheckBoxEdit.Enabled = True
                    TextBoxAddendumGivenTo.Enabled = True
                    TextBoxAddendumValueEdit.Enabled = True
                    TextBoxRetention.Enabled = True
                    ArchivedByMercuryCheckBoxEdit.Enabled = True
                    TextBoxNoteEdit.Enabled = True
                    hiddenFileManagerUploadAddendumPDFMode.Value = 1
                    hiddenFileManagerUploadAddendumDOCMode.Value = 1
                    TextBoxAddendumValueWithVATEdit.Enabled = True
                    TextBoxVAT.Enabled = True

                    'Disable Commercial Items
                    TextBoxStartDate.Enabled = False
                    _DropDownListPenalty.Enabled = False
                    TextBoxPenaltyNote.Enabled = False
                    TextBoxDeliveryTerms.Enabled = False
                    TextBoxGuaranteePeriod.Enabled = False
                    TextBoxFinishDate.Enabled = False
                    TextBoxAdvance.Enabled = False
                    DropDownListPenaltyToSupplier.Enabled = False
                    TextBoxPenaltyNoteSupplier.Enabled = False
                    TextBoxInterim.Enabled = False
                    TextBoxShipment.Enabled = False
                    TextBoxDelivery.Enabled = False
                    LiteralCommercialRoleTitle.Visible = True
                End If ' EnableContractControlsAddendum(1, 0, 0, 0, _row), POexecuted FALSE

                If _ContractLeadGirls = 1 And _InitiateContractAndAddendum = 0 And _Commercial_Items = 0 And _ContractControlExceptional = 0 And DataBinder.Eval(_row.DataItem, "PoExecuted") = True And
                    _Exceptional = False Then
                    ' Enable Contract Controls
                    DDLContractID.Enabled = False
                    TextBoxAddendumNoEdit.Enabled = True
                    TextBoxAddendumDateEdit.Enabled = True
                    TextBoxAddendumDescriptionEdit.Enabled = False
                    If DataBinder.Eval(_row.DataItem, "AddendumTypes") = 3 Then
                        ' if it is ZERO VALUE addendum, Supplier Signed control should be visible. Because PO being executed without Supplier Signed, Lawyers needs to update Supplier Signed.
                        SignBySupplierCheckBoxEdit.Enabled = True
                    Else
                        SignBySupplierCheckBoxEdit.Enabled = False
                    End If
                    SignByMercuryCheckBoxEdit.Enabled = True
                    TextBoxAddendumGivenTo.Enabled = True
                    TextBoxAddendumValueEdit.Enabled = False
                    TextBoxRetention.Enabled = False
                    ArchivedByMercuryCheckBoxEdit.Enabled = True
                    TextBoxNoteEdit.Enabled = True
                    hiddenFileManagerUploadAddendumPDFMode.Value = 1
                    hiddenFileManagerUploadAddendumDOCMode.Value = 1
                    TextBoxAddendumValueWithVATEdit.Enabled = False
                    TextBoxVAT.Enabled = False

                    'Disable Commercial Items
                    TextBoxStartDate.Enabled = False
                    _DropDownListPenalty.Enabled = False
                    TextBoxPenaltyNote.Enabled = False
                    TextBoxDeliveryTerms.Enabled = False
                    TextBoxGuaranteePeriod.Enabled = False
                    TextBoxFinishDate.Enabled = False
                    TextBoxAdvance.Enabled = False
                    DropDownListPenaltyToSupplier.Enabled = False
                    TextBoxPenaltyNoteSupplier.Enabled = False
                    TextBoxInterim.Enabled = False
                    TextBoxShipment.Enabled = False
                    TextBoxDelivery.Enabled = False
                    LiteralCommercialRoleTitle.Visible = True
                End If ' EnableContractControlsAddendum(1, 0, 0, 0, _row), POexecuted TRUE, Exceptional FALSE

                If _ContractLeadGirls = 1 And _InitiateContractAndAddendum = 0 And _Commercial_Items = 0 And _ContractControlExceptional = 0 And DataBinder.Eval(_row.DataItem, "PoExecuted") = True And
                    _Exceptional = True Then
                    ' Enable Contract Controls
                    DDLContractID.Enabled = True
                    TextBoxAddendumNoEdit.Enabled = True
                    TextBoxAddendumDateEdit.Enabled = True
                    TextBoxAddendumDescriptionEdit.Enabled = True
                    SignBySupplierCheckBoxEdit.Enabled = True
                    SignByMercuryCheckBoxEdit.Enabled = True
                    TextBoxAddendumGivenTo.Enabled = True
                    TextBoxAddendumValueEdit.Enabled = True
                    TextBoxRetention.Enabled = True
                    ArchivedByMercuryCheckBoxEdit.Enabled = True
                    TextBoxNoteEdit.Enabled = True
                    hiddenFileManagerUploadAddendumPDFMode.Value = 1
                    hiddenFileManagerUploadAddendumDOCMode.Value = 1
                    TextBoxAddendumValueWithVATEdit.Enabled = False
                    TextBoxVAT.Enabled = False

                    'Disable Commercial Items
                    TextBoxStartDate.Enabled = False
                    _DropDownListPenalty.Enabled = False
                    TextBoxPenaltyNote.Enabled = False
                    TextBoxDeliveryTerms.Enabled = False
                    TextBoxGuaranteePeriod.Enabled = False
                    TextBoxFinishDate.Enabled = False
                    TextBoxAdvance.Enabled = False
                    DropDownListPenaltyToSupplier.Enabled = False
                    TextBoxPenaltyNoteSupplier.Enabled = False
                    TextBoxInterim.Enabled = False
                    TextBoxShipment.Enabled = False
                    TextBoxDelivery.Enabled = False
                    LiteralCommercialRoleTitle.Visible = True
                End If ' EnableContractControlsAddendum(1, 0, 0, 0, _row), POexecuted TRUE, Exceptional TRUE

                If _ContractLeadGirls = 0 And _InitiateContractAndAddendum = 0 And _Commercial_Items = 0 And _ContractControlExceptional = 1 And DataBinder.Eval(_row.DataItem, "PoExecuted") = False Then
                    ' Enable Contract Controls
                    DDLContractID.Enabled = False
                    TextBoxAddendumNoEdit.Enabled = True
                    TextBoxAddendumDateEdit.Enabled = True
                    TextBoxAddendumDescriptionEdit.Enabled = False
                    SignBySupplierCheckBoxEdit.Enabled = True
                    SignByMercuryCheckBoxEdit.Enabled = True
                    TextBoxAddendumGivenTo.Enabled = False
                    TextBoxAddendumValueEdit.Enabled = False
                    TextBoxRetention.Enabled = False
                    ArchivedByMercuryCheckBoxEdit.Enabled = False
                    TextBoxNoteEdit.Enabled = False
                    hiddenFileManagerUploadAddendumPDFMode.Value = 1
                    hiddenFileManagerUploadAddendumDOCMode.Value = 1
                    TextBoxAddendumValueWithVATEdit.Enabled = False
                    TextBoxVAT.Enabled = False

                    'Disable Commercial Items
                    TextBoxStartDate.Enabled = False
                    _DropDownListPenalty.Enabled = False
                    TextBoxPenaltyNote.Enabled = False
                    TextBoxDeliveryTerms.Enabled = False
                    TextBoxGuaranteePeriod.Enabled = False
                    TextBoxFinishDate.Enabled = False
                    TextBoxAdvance.Enabled = False
                    DropDownListPenaltyToSupplier.Enabled = False
                    TextBoxPenaltyNoteSupplier.Enabled = False
                    TextBoxInterim.Enabled = False
                    TextBoxShipment.Enabled = False
                    TextBoxDelivery.Enabled = False
                    LiteralCommercialRoleTitle.Visible = True
                End If ' EnableContractControlsAddendum(0, 0, 0, 1, _row), POexecuted FALSE

                If _ContractLeadGirls = 0 And _InitiateContractAndAddendum = 0 And _Commercial_Items = 0 And _ContractControlExceptional = 1 And DataBinder.Eval(_row.DataItem, "PoExecuted") = True And
                    _Exceptional = False Then
                    ' Enable Contract Controls
                    DDLContractID.Enabled = False
                    TextBoxAddendumNoEdit.Enabled = True
                    TextBoxAddendumDateEdit.Enabled = True
                    TextBoxAddendumDescriptionEdit.Enabled = False
                    SignBySupplierCheckBoxEdit.Enabled = False
                    SignByMercuryCheckBoxEdit.Enabled = True
                    TextBoxAddendumGivenTo.Enabled = False
                    TextBoxAddendumValueEdit.Enabled = False
                    TextBoxRetention.Enabled = False
                    ArchivedByMercuryCheckBoxEdit.Enabled = False
                    TextBoxNoteEdit.Enabled = False
                    hiddenFileManagerUploadAddendumPDFMode.Value = 1
                    hiddenFileManagerUploadAddendumDOCMode.Value = 1
                    TextBoxAddendumValueWithVATEdit.Enabled = False
                    TextBoxVAT.Enabled = False

                    'Disable Commercial Items
                    TextBoxStartDate.Enabled = False
                    _DropDownListPenalty.Enabled = False
                    TextBoxPenaltyNote.Enabled = False
                    TextBoxDeliveryTerms.Enabled = False
                    TextBoxGuaranteePeriod.Enabled = False
                    TextBoxFinishDate.Enabled = False
                    TextBoxAdvance.Enabled = False
                    DropDownListPenaltyToSupplier.Enabled = False
                    TextBoxPenaltyNoteSupplier.Enabled = False
                    TextBoxInterim.Enabled = False
                    TextBoxShipment.Enabled = False
                    TextBoxDelivery.Enabled = False
                    LiteralCommercialRoleTitle.Visible = True
                End If ' EnableContractControlsAddendum(0, 0, 0, 1, _row), POexecuted TRUE, Exceptional FALSE

                If _ContractLeadGirls = 0 And _InitiateContractAndAddendum = 0 And _Commercial_Items = 0 And _ContractControlExceptional = 1 And DataBinder.Eval(_row.DataItem, "PoExecuted") = True And
                    _Exceptional = True Then
                    ' Enable Contract Controls
                    DDLContractID.Enabled = False
                    TextBoxAddendumNoEdit.Enabled = True
                    TextBoxAddendumDateEdit.Enabled = True
                    TextBoxAddendumDescriptionEdit.Enabled = False
                    SignBySupplierCheckBoxEdit.Enabled = True
                    SignByMercuryCheckBoxEdit.Enabled = True
                    TextBoxAddendumGivenTo.Enabled = False
                    TextBoxAddendumValueEdit.Enabled = False
                    TextBoxRetention.Enabled = False
                    ArchivedByMercuryCheckBoxEdit.Enabled = False
                    TextBoxNoteEdit.Enabled = False
                    hiddenFileManagerUploadAddendumPDFMode.Value = 1
                    hiddenFileManagerUploadAddendumDOCMode.Value = 1
                    TextBoxAddendumValueWithVATEdit.Enabled = False
                    TextBoxVAT.Enabled = False

                    'Disable Commercial Items
                    TextBoxStartDate.Enabled = False
                    _DropDownListPenalty.Enabled = False
                    TextBoxPenaltyNote.Enabled = False
                    TextBoxDeliveryTerms.Enabled = False
                    TextBoxGuaranteePeriod.Enabled = False
                    TextBoxFinishDate.Enabled = False
                    TextBoxAdvance.Enabled = False
                    DropDownListPenaltyToSupplier.Enabled = False
                    TextBoxPenaltyNoteSupplier.Enabled = False
                    TextBoxInterim.Enabled = False
                    TextBoxShipment.Enabled = False
                    TextBoxDelivery.Enabled = False
                    LiteralCommercialRoleTitle.Visible = True
                End If ' EnableContractControlsAddendum(0, 0, 0, 1, _row), POexecuted TRUE, Exceptional TRUE

                If _ContractLeadGirls = 0 And _InitiateContractAndAddendum = 1 And _Commercial_Items = 0 And _ContractControlExceptional = 0 Then
                    ' Enable Contract Controls
                    DDLContractID.Enabled = True
                    TextBoxAddendumNoEdit.Enabled = True
                    TextBoxAddendumDateEdit.Enabled = True
                    TextBoxAddendumDescriptionEdit.Enabled = True
                    SignBySupplierCheckBoxEdit.Enabled = False
                    SignByMercuryCheckBoxEdit.Enabled = False
                    TextBoxAddendumGivenTo.Enabled = True
                    TextBoxAddendumValueEdit.Enabled = True
                    TextBoxRetention.Enabled = True
                    ArchivedByMercuryCheckBoxEdit.Enabled = True
                    TextBoxNoteEdit.Enabled = True
                    hiddenFileManagerUploadAddendumPDFMode.Value = 1
                    hiddenFileManagerUploadAddendumDOCMode.Value = 1
                    TextBoxAddendumValueWithVATEdit.Enabled = True
                    TextBoxVAT.Enabled = True

                    'Enable Commercial Items
                    TextBoxStartDate.Enabled = True
                    _DropDownListPenalty.Enabled = True
                    TextBoxPenaltyNote.Enabled = True
                    TextBoxDeliveryTerms.Enabled = True
                    TextBoxGuaranteePeriod.Enabled = True
                    TextBoxFinishDate.Enabled = True
                    TextBoxAdvance.Enabled = True
                    DropDownListPenaltyToSupplier.Enabled = True
                    TextBoxPenaltyNoteSupplier.Enabled = True
                    TextBoxInterim.Enabled = True
                    TextBoxShipment.Enabled = True
                    TextBoxDelivery.Enabled = True
                    LiteralCommercialRoleTitle.Visible = False
                End If ' EnableContractControlsAddendum(0, 1, 0, 0, _row)

                If _ContractLeadGirls = 0 And _InitiateContractAndAddendum = 0 And _Commercial_Items = 1 And _ContractControlExceptional = 0 Then
                    ' Enable Contract Controls
                    DDLContractID.Enabled = True
                    TextBoxAddendumNoEdit.Enabled = True
                    TextBoxAddendumDateEdit.Enabled = True
                    TextBoxAddendumDescriptionEdit.Enabled = True
                    SignBySupplierCheckBoxEdit.Enabled = False
                    SignByMercuryCheckBoxEdit.Enabled = False
                    TextBoxAddendumGivenTo.Enabled = True
                    TextBoxAddendumValueEdit.Enabled = True
                    TextBoxRetention.Enabled = True
                    ArchivedByMercuryCheckBoxEdit.Enabled = True
                    TextBoxNoteEdit.Enabled = True
                    hiddenFileManagerUploadAddendumPDFMode.Value = 1
                    hiddenFileManagerUploadAddendumDOCMode.Value = 1
                    TextBoxAddendumValueWithVATEdit.Enabled = True
                    TextBoxVAT.Enabled = True

                    'Enable Commercial Items
                    TextBoxStartDate.Enabled = True
                    _DropDownListPenalty.Enabled = True
                    TextBoxPenaltyNote.Enabled = True
                    TextBoxDeliveryTerms.Enabled = True
                    TextBoxGuaranteePeriod.Enabled = True
                    TextBoxFinishDate.Enabled = True
                    TextBoxAdvance.Enabled = True
                    DropDownListPenaltyToSupplier.Enabled = True
                    TextBoxPenaltyNoteSupplier.Enabled = True
                    TextBoxInterim.Enabled = True
                    TextBoxShipment.Enabled = True
                    TextBoxDelivery.Enabled = True
                    LiteralCommercialRoleTitle.Visible = False
                End If ' EnableContractControlsAddendum(0, 0, 1, 0, _row)

            End If
        End If

    End Sub

    Protected Sub HIDE_SHOW_edit_delete_AddAdendum_RaisePo_buttons_depends_on_the_role_situation_Contract(ByVal _row As GridViewRow)

        Dim _ProjectID As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _ProjectID = DataBinder.Eval(_row.DataItem, "ProjectID")
        End If

        Dim _NewGeneration As Boolean = False

        If _row.RowType = DataControlRowType.DataRow Then
            _NewGeneration = DataBinder.Eval(_row.DataItem, "NewGeneration")
        End If

        Dim _POexecuted As Boolean = False

        If _row.RowType = DataControlRowType.DataRow Then
            _POexecuted = DataBinder.Eval(_row.DataItem, "POexecuted")
        End If

        Dim _Commercial_Items As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _Commercial_Items = DataBinder.Eval(_row.DataItem, "Commercial_Items").ToString
        End If

        Dim _ESTM As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _ESTM = DataBinder.Eval(_row.DataItem, "ESTM").ToString
        End If

        Dim _InitiateContractAndAddendum As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _InitiateContractAndAddendum = DataBinder.Eval(_row.DataItem, "InitiateContractAndAddendum").ToString
        End If

        Dim _ContractLeadGirls As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _ContractLeadGirls = DataBinder.Eval(_row.DataItem, "ContractLeadGirls").ToString
        End If

        Dim _LawyerOnSite As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _LawyerOnSite = DataBinder.Eval(_row.DataItem, "LawyerOnSite").ToString
        End If

        Dim _ContractSupportGirl As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _ContractSupportGirl = DataBinder.Eval(_row.DataItem, "ContractSupportGirl").ToString
        End If

        Dim _FrameContract As Boolean = False

        If _row.RowType = DataControlRowType.DataRow Then
            _FrameContract = DataBinder.Eval(_row.DataItem, "FrameContract").ToString
        End If

        Dim _Scenario As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _Scenario = DataBinder.Eval(_row.DataItem, "Scenario").ToString
        End If

        Dim _SignBySupplier As Boolean = False

        If _row.RowType = DataControlRowType.DataRow Then
            _SignBySupplier = DataBinder.Eval(_row.DataItem, "SignBySupplier").ToString
        End If

        Dim _SignByMercury As Boolean = False

        If _row.RowType = DataControlRowType.DataRow Then
            _SignByMercury = DataBinder.Eval(_row.DataItem, "SignByMercury").ToString
        End If

        Dim SupplierSigned_MercurySigned As Boolean = False

        If _SignBySupplier = 1 And _SignByMercury = 1 Then
            SupplierSigned_MercurySigned = True
        End If

        Dim _ContractControlExceptional As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _ContractControlExceptional = DataBinder.Eval(_row.DataItem, "ContractControlExceptional").ToString
        End If

        Dim _Contract_User_Junction As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _Contract_User_Junction = DataBinder.Eval(_row.DataItem, "Contract_User_Junction").ToString
        End If

        Dim LinkButtonAddAddendum As LinkButton = DirectCast(_row.FindControl("LinkButtonAddAddendum"), LinkButton)
        Dim ButtonEdit As Button = DirectCast(_row.FindControl("ButtonEdit"), Button)
        Dim ButtonDelete As Button = DirectCast(_row.FindControl("ButtonDelete"), Button)
        Dim LinkButtonRaisePO As LinkButton = DirectCast(_row.FindControl("LinkButtonRaisePO"), LinkButton)

        If LinkButtonAddAddendum IsNot Nothing Then
            If _ProjectID = 0 Then
                LinkButtonAddAddendum.Visible = False
                ButtonEdit.Visible = False
                'CheckThisLine(DataBinder.Eval(_row.DataItem, "ContractID"), 4226)
                ButtonDelete.Visible = False
            ElseIf _ProjectID <> 0 Then

                If _NewGeneration = True Then

                    ' if PO executed, EDIT, DELETE false
                    ' if PO executed and FrameContract and Scenario -1 , AddAddendum FALSE
                    ' if PO executed and Not FrameContract and Not Scenario -1 , AddAddendum TRUE
                    If _POexecuted = True Then

                        ' Show Edit button to ContractLeadGirls and Savas. Then hide all Edit controls except SignedByMercury and PDF controls
                        ' This is requirement after our meeting
                        If _ContractLeadGirls = 1 Or
                            _ContractSupportGirl = 1 Or
                            _ContractControlExceptional > 0 Or
                            _LawyerOnSite = 1 Or
                            Page.User.Identity.Name.ToLower = "savas" Then
                            ButtonEdit.Visible = True
                        Else
                            ButtonEdit.Visible = False
                        End If

                        ButtonDelete.Visible = False

                        ' THIS IS FROZEN. BUDGET DISABLED
                        If _FrameContract = True Then
                            If _Scenario = -1 Then
                                LinkButtonAddAddendum.Visible = False
                                If _InitiateContractAndAddendum = 1 Then

                                    LinkButtonRaisePO.Visible = True
                                    LinkButtonRaisePO.Enabled = True


                                    LinkButtonAddAddendum.Visible = True

                                ElseIf _LawyerOnSite = 1 Then
                                    LinkButtonRaisePO.Visible = False
                                    LinkButtonAddAddendum.Visible = True
                                Else
                                    LinkButtonRaisePO.Visible = False
                                End If
                            End If
                        End If

                        If _FrameContract = False Then
                            If _Scenario <> -1 Then
                                If _Scenario > 0 Then
                                    If _InitiateContractAndAddendum = 1 Or
                                        _LawyerOnSite = 1 Then
                                        LinkButtonAddAddendum.Visible = True
                                    Else
                                        LinkButtonAddAddendum.Visible = False
                                    End If
                                End If
                            End If
                        End If
                    End If

                    ' if PO not executed
                    If _POexecuted = False Then
                        LinkButtonAddAddendum.Visible = False
                        ButtonEdit.Visible = False
                        ButtonDelete.Visible = False

                        If _ContractLeadGirls = 1 OrElse
                         _ContractSupportGirl = 1 OrElse
                          _Commercial_Items = 1 OrElse
                          _InitiateContractAndAddendum = 1 OrElse
                          _ContractControlExceptional > 0 OrElse
                          _LawyerOnSite = 1 Then
                            ButtonEdit.Visible = True
                            'CheckThisLine(DataBinder.Eval(_row.DataItem, "ContractID"), 4237)
                        End If

                        If _ContractLeadGirls = 1 Or
                            _ContractSupportGirl = 1 Or
                            _LawyerOnSite = 1 Then
                            ButtonDelete.Visible = True
                        End If

                    End If

                End If ' project newgeneration TRUE

                If _NewGeneration = False Then
                    If _Contract_User_Junction > 0 _
                        And (Roles.IsUserInRole("ContractLeadGirls") = True Or Roles.IsUserInRole("ContractSupportGirl") = True) _
                        Then

                        LinkButtonAddAddendum.Visible = True
                        ButtonEdit.Visible = True
                        'CheckThisLine(DataBinder.Eval(_row.DataItem, "ContractID"), 4259)
                        ButtonDelete.Visible = True

                    ElseIf _Contract_User_Junction > 0 _
                        And Roles.IsUserInRole("Contract") = True _
                        And Roles.IsUserInRole("ContractLeadGirls") = False _
                        And SupplierSigned_MercurySigned = False Then

                        LinkButtonAddAddendum.Visible = True
                        ButtonEdit.Visible = True
                        'CheckThisLine(DataBinder.Eval(_row.DataItem, "ContractID"), 4268)
                        ButtonDelete.Visible = True

                    ElseIf _Contract_User_Junction > 0 _
                        And Roles.IsUserInRole("Contract") = True _
                        And Roles.IsUserInRole("ContractEditPo") = False _
                        And Roles.IsUserInRole("ContractLeadGirls") = False _
                        And SupplierSigned_MercurySigned = True Then

                        LinkButtonAddAddendum.Visible = True
                        ButtonEdit.Visible = False
                        'CheckThisLine(DataBinder.Eval(_row.DataItem, "ContractID"), 4279)
                        ButtonDelete.Visible = False

                    ElseIf _Contract_User_Junction > 0 _
                        And Roles.IsUserInRole("Contract") = False _
                        And Roles.IsUserInRole("ContractEditPo") = True _
                        And Roles.IsUserInRole("ContractLeadGirls") = False _
                        And SupplierSigned_MercurySigned = False Then

                        LinkButtonAddAddendum.Visible = False
                        ButtonEdit.Visible = True
                        'CheckThisLine(DataBinder.Eval(_row.DataItem, "ContractID"), 4290)
                        ButtonDelete.Visible = False

                        ' INSERTED LATER
                        ' INSERTED LATER
                        ' INSERTED LATER
                        ' INSERTED LATER
                    ElseIf _Contract_User_Junction > 0 _
                        And Roles.IsUserInRole("Contract") = False _
                        And Roles.IsUserInRole("ContractEditPo") = True _
                        And Roles.IsUserInRole("ContractLeadGirls") = False _
                        And SupplierSigned_MercurySigned = True Then

                        LinkButtonAddAddendum.Visible = False
                        ButtonEdit.Visible = True
                        'CheckThisLine(DataBinder.Eval(_row.DataItem, "ContractID"), 4290)
                        ButtonDelete.Visible = False

                    ElseIf _Contract_User_Junction > 0 _
                        And Roles.IsUserInRole("Contract") = True _
                        And Roles.IsUserInRole("ContractEditPo") = True _
                        And Roles.IsUserInRole("ContractLeadGirls") = False _
                        And SupplierSigned_MercurySigned = True Then

                        LinkButtonAddAddendum.Visible = True
                        ButtonEdit.Visible = True
                        'CheckThisLine(DataBinder.Eval(_row.DataItem, "ContractID"), 4301)
                        ButtonDelete.Visible = False

                    ElseIf _Contract_User_Junction > 0 _
                        And Roles.IsUserInRole("Contract") = True _
                        And Roles.IsUserInRole("ContractEditPo") = True _
                        And Roles.IsUserInRole("ContractLeadGirls") = True _
                        And SupplierSigned_MercurySigned = False Then

                        LinkButtonAddAddendum.Visible = True
                        ButtonEdit.Visible = True
                        'CheckThisLine(DataBinder.Eval(_row.DataItem, "ContractID"), 4312)
                        ButtonDelete.Visible = True

                    ElseIf _Contract_User_Junction > 0 _
                        And Roles.IsUserInRole("Contract") = True _
                        And Roles.IsUserInRole("ContractEditPo") = True _
                        And Roles.IsUserInRole("ContractLeadGirls") = True _
                        And SupplierSigned_MercurySigned = True Then

                        LinkButtonAddAddendum.Visible = True
                        ButtonEdit.Visible = True
                        'CheckThisLine(DataBinder.Eval(_row.DataItem, "ContractID"), 4323)
                        ButtonDelete.Visible = True

                    Else

                        LinkButtonAddAddendum.Visible = False
                        ButtonEdit.Visible = False
                        'CheckThisLine(DataBinder.Eval(_row.DataItem, "ContractID"), 4330)
                        ButtonDelete.Visible = False

                    End If
                End If ' project newgeneration FALSE

            End If

        End If

    End Sub

    Protected Sub HIDE_SHOW_edit_delete_buttons_depends_on_the_role_situation_Addendum(ByVal _row As GridViewRow)

        Dim _ContractID As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _ContractID = DataBinder.Eval(_row.DataItem, "ContractID")
        End If

        Dim _NewGeneration As Boolean = False

        If _row.RowType = DataControlRowType.DataRow Then
            _NewGeneration = DataBinder.Eval(_row.DataItem, "NewGeneration")
        End If

        Dim _POexecuted As Boolean = False

        If _row.RowType = DataControlRowType.DataRow Then
            _POexecuted = DataBinder.Eval(_row.DataItem, "POexecuted")
        End If

        Dim _Commercial_Items As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _Commercial_Items = DataBinder.Eval(_row.DataItem, "Commercial_Items").ToString
        End If

        Dim _ESTM As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _ESTM = DataBinder.Eval(_row.DataItem, "ESTM").ToString
        End If

        Dim _InitiateContractAndAddendum As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _InitiateContractAndAddendum = DataBinder.Eval(_row.DataItem, "InitiateContractAndAddendum").ToString
        End If

        Dim _ContractLeadGirls As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _ContractLeadGirls = DataBinder.Eval(_row.DataItem, "ContractLeadGirls").ToString
        End If

        Dim _LawyerOnSite As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _LawyerOnSite = DataBinder.Eval(_row.DataItem, "LawyerOnSite").ToString
        End If

        Dim _ContractSupportGirl As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _ContractSupportGirl = DataBinder.Eval(_row.DataItem, "ContractSupportGirl").ToString
        End If

        Dim _ContractControlExceptional As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _ContractControlExceptional = DataBinder.Eval(_row.DataItem, "ContractControlExceptional").ToString
        End If

        Dim _Contract_User_Junction As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _Contract_User_Junction = DataBinder.Eval(_row.DataItem, "Contract_User_Junction").ToString
        End If

        Dim _SignBySupplier As Boolean = False

        If _row.RowType = DataControlRowType.DataRow Then
            _SignBySupplier = DataBinder.Eval(_row.DataItem, "AddendumSignBySupplier").ToString
        End If

        Dim _SignByMercury As Boolean = False

        If _row.RowType = DataControlRowType.DataRow Then
            _SignByMercury = DataBinder.Eval(_row.DataItem, "AddendumSignByMercury").ToString
        End If

        Dim SupplierSigned_MercurySigned As Boolean = False

        If _SignBySupplier = 1 And _SignByMercury = 1 Then
            SupplierSigned_MercurySigned = True
        End If

        Dim _ProjectID As Integer = 0

        If _row.RowType = DataControlRowType.DataRow Then
            _ProjectID = DataBinder.Eval(_row.DataItem, "ProjectID").ToString
        End If

        Dim ButtonEdit As Button = DirectCast(_row.FindControl("ButtonEdit"), Button)
        Dim ButtonDelete As Button = DirectCast(_row.FindControl("ButtonDelete"), Button)

        If ButtonEdit IsNot Nothing Then
            If _ProjectID = 0 Then
                ButtonEdit.Visible = False
                ButtonDelete.Visible = False
            ElseIf _ProjectID <> 0 Then

                If _NewGeneration = True Then

                    If _POexecuted = True Then
                        ' Show Edit button to ContractLeadGirls and Savas. Then hide all Edit controls except SignedByMercury and PDF controls
                        ' This is requirement after our meeting
                        If _ContractLeadGirls = 1 Or
                            _ContractSupportGirl = 1 Or
                            _LawyerOnSite = 1 Or
                            _ContractControlExceptional > 0 _
                            Or Page.User.Identity.Name.ToLower = "savas" Then
                            ButtonEdit.Visible = True
                        Else
                            ButtonEdit.Visible = False
                        End If

                        ButtonDelete.Visible = False
                    End If ' POexecuted TRUE

                    If _POexecuted = False Then
                        ButtonEdit.Visible = False
                        ButtonDelete.Visible = False
                        If _ContractLeadGirls = 1 OrElse
                         _ContractSupportGirl = 1 OrElse
                         _LawyerOnSite = 1 OrElse
                          _Commercial_Items = 1 OrElse
                          _InitiateContractAndAddendum = 1 OrElse
                          _ContractControlExceptional > 0 Then
                            ButtonEdit.Visible = True
                            'CheckThisLine(DataBinder.Eval(_row.DataItem, "ContractID"), 4237)
                        End If

                        If _ContractLeadGirls = 1 Or
                            _ContractSupportGirl = 1 Or
                            _LawyerOnSite = 1 Then
                            ButtonDelete.Visible = True
                        End If

                    End If ' POexecuted FALSE

                End If ' project newgeneration TRUE

                If _NewGeneration = False Then
                    If _Contract_User_Junction > 0 _
                        And (Roles.IsUserInRole("ContractLeadGirls") = True Or Roles.IsUserInRole("ContractSupportGirl") = True) _
                        Then

                        ButtonEdit.Visible = True
                        ButtonDelete.Visible = True

                    ElseIf _Contract_User_Junction > 0 _
                        And Roles.IsUserInRole("Contract") = True _
                        And Roles.IsUserInRole("ContractLeadGirls") = False _
                        And SupplierSigned_MercurySigned = False Then

                        ButtonEdit.Visible = True
                        ButtonDelete.Visible = True

                    ElseIf _Contract_User_Junction > 0 _
                        And Roles.IsUserInRole("Contract") = True _
                        And Roles.IsUserInRole("ContractEditPo") = False _
                        And Roles.IsUserInRole("ContractLeadGirls") = False _
                        And SupplierSigned_MercurySigned = True Then

                        ButtonEdit.Visible = False
                        ButtonDelete.Visible = False

                    ElseIf _Contract_User_Junction > 0 _
                        And Roles.IsUserInRole("Contract") = False _
                        And Roles.IsUserInRole("ContractEditPo") = True _
                        And Roles.IsUserInRole("ContractLeadGirls") = False _
                        And SupplierSigned_MercurySigned = False Then

                        ButtonEdit.Visible = True
                        ButtonDelete.Visible = False

                    ElseIf _Contract_User_Junction > 0 _
                        And Roles.IsUserInRole("Contract") = False _
                        And Roles.IsUserInRole("ContractEditPo") = True _
                        And Roles.IsUserInRole("ContractLeadGirls") = False _
                        And SupplierSigned_MercurySigned = True Then

                        ButtonEdit.Visible = True
                        ButtonDelete.Visible = False

                    ElseIf _Contract_User_Junction > 0 _
                        And Roles.IsUserInRole("Contract") = True _
                        And Roles.IsUserInRole("ContractEditPo") = True _
                        And Roles.IsUserInRole("ContractLeadGirls") = False _
                        And SupplierSigned_MercurySigned = True Then

                        ButtonEdit.Visible = True
                        ButtonDelete.Visible = False

                    ElseIf _Contract_User_Junction > 0 _
                        And Roles.IsUserInRole("Contract") = True _
                        And Roles.IsUserInRole("ContractEditPo") = True _
                        And Roles.IsUserInRole("ContractLeadGirls") = True _
                        And SupplierSigned_MercurySigned = False Then

                        ButtonEdit.Visible = True
                        ButtonDelete.Visible = True

                    ElseIf _Contract_User_Junction > 0 _
                        And Roles.IsUserInRole("Contract") = True _
                        And Roles.IsUserInRole("ContractEditPo") = True _
                        And Roles.IsUserInRole("ContractLeadGirls") = True _
                        And SupplierSigned_MercurySigned = True Then

                        ButtonEdit.Visible = True
                        ButtonDelete.Visible = True

                    Else

                        ButtonEdit.Visible = False
                        ButtonDelete.Visible = False

                    End If
                End If ' project newgeneration FALSE

            End If

        End If

    End Sub

    Protected Sub CheckThisLine(ByVal ContractID As Integer, ByVal LineNumber As Integer)


    End Sub

    Protected Sub DDLestm_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim gvr As GridViewRow = DirectCast(DirectCast(sender, DropDownList).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent, GridViewRow)
        Dim LabelContractIDitem As Label = DirectCast(gvr.FindControl("LabelContractIDitem"), Label)

        Dim DDL As DropDownList = sender

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "ExecuteESTMConfirmationContract"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", System.Data.SqlDbType.Int)
            ContractID.Value = Convert.ToInt32(LabelContractIDitem.Text)

            Dim ConfirmationStatus As SqlParameter = cmd.Parameters.Add("@ConfirmationStatus", System.Data.SqlDbType.Int)
            ConfirmationStatus.Value = DDL.SelectedValue

            cmd.ExecuteNonQuery()
            con.Close()
        End Using

        ' CHECK CONTRACT IF IT IS READY FOR PO
        ContractView.Initiate_Checking_Contract_Or_Addendum_To_Insert_Or_Update_Po_Then_Finally_Execute(
                                                Convert.ToInt32(LabelContractIDitem.Text),
                                                0,
                                                POdetailsForEmail)

        GridViewShowContract.DataBind()

    End Sub


    Protected Sub ButtonResult_Click(sender As Object, e As EventArgs)


    End Sub

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender



        If Session("_prjID_Search") IsNot Nothing And Session("_supplierID_Search") IsNot Nothing Then
            DropDownListPrjID.SelectedValue = Session("_prjID_Search")
            DropDownListSupplier.DataBind()
            DropDownListSupplier.SelectedValue = Session("_supplierID_Search")
            GridViewShowContract.DataBind()

            ' remove sessions for search
            Session.Remove("_prjID_Search")
            Session.Remove("_supplierID_Search")

        End If

    End Sub

    Protected Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs)

        Try
            Dim _ContractID As String = TextBoxSearch.Text

            Dim _start As Integer = 0
            Dim _finish As Integer = 0

            _start = 0
            _finish = _ContractID.IndexOf("|", 0)

            _ContractID = Convert.ToInt32(Mid(_ContractID, _start + 2, _finish - _start - 1).Trim.Replace(".", String.Empty))

            Session("_prjID_Search") = PTS.CoreTables.CreateDataReader.Create_Table_Contract(_ContractID).ProjectID

            Dim _SupplierID As String = ""

            If PTS.CoreTables.CreateDataReader.Create_Table_Contract(_ContractID).SupplierID.Trim.Length = 10 Then
                _SupplierID = PTS.CoreTables.CreateDataReader.Create_Table_Contract(_ContractID).SupplierID + "  "
            Else
                _SupplierID = PTS.CoreTables.CreateDataReader.Create_Table_Contract(_ContractID).SupplierID
            End If

            Session("_supplierID_Search") = _SupplierID

            TextBoxSearch.Text = String.Empty

        Catch ex As Exception
            TextBoxSearch.Text = String.Empty
        End Try

    End Sub

    Protected Sub TextBoxEntryInterval_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnSearchAdvance_Click(sender As Object, e As EventArgs)
        'GridViewShowContract.DataBind()
    End Sub

End Class


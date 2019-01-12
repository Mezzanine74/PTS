Imports System.Net.Mail
Imports System.Data.SqlClient
Imports PTS_App_Code_VB_Project.PTS_MERCURY.helper.EmailGenerator

Partial Class projectsREV
    Inherits System.Web.UI.Page

    Protected Sub GridViewProjects_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewProjects.Load
        If Not IsPostBack Then
            GridViewProjects.Sort("ProjectID", SortDirection.Descending)
        End If
    End Sub

    Protected Sub GridViewProjects_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewProjects.RowCommand
        If (e.CommandName = "Update") Then

            Dim row As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)
            Dim InsuranceStart As String = ""
            Dim InsuranceFinish As String = ""
            Dim TextBoxInsuranceStart As TextBox = DirectCast(row.FindControl("TextBoxInsuranceStart"), TextBox)
            Dim TextBoxInsuranceFinish As TextBox = DirectCast(row.FindControl("TextBoxInsuranceFinish"), TextBox)
            Dim TextBoxBackColor As TextBox = DirectCast(row.FindControl("TextBoxBackColor"), TextBox)
            Dim TextBoxForeColor As TextBox = DirectCast(row.FindControl("TextBoxForeColor"), TextBox)

            SqlDataSourceProjects.UpdateParameters("BackColor").Type = TypeCode.String
            SqlDataSourceProjects.UpdateParameters("BackColor").DefaultValue = TextBoxBackColor.Text

            SqlDataSourceProjects.UpdateParameters("ForeColor").Type = TypeCode.String
            SqlDataSourceProjects.UpdateParameters("ForeColor").DefaultValue = TextBoxForeColor.Text

            If TextBoxInsuranceStart.Text = "" Then
                InsuranceStart = " NULL "
            Else
                InsuranceStart = "CONVERT(DATETIME, '" + Mid(TextBoxInsuranceStart.Text, 7, 4).ToString + "-" + Mid(TextBoxInsuranceStart.Text, 4, 2).ToString + "-" + Mid(TextBoxInsuranceStart.Text, 1, 2).ToString + " 00:00:00" + "', 102)"
            End If

            If TextBoxInsuranceFinish.Text = "" Then
                InsuranceFinish = " NULL "
            Else
                InsuranceFinish = "CONVERT(DATETIME, '" + Mid(TextBoxInsuranceFinish.Text, 7, 4).ToString + "-" + Mid(TextBoxInsuranceFinish.Text, 4, 2).ToString + "-" + Mid(TextBoxInsuranceFinish.Text, 1, 2).ToString + " 00:00:00" + "', 102)"
            End If

            ' determine currency NULL or not
            Dim Currency As String = ""
            If DirectCast(row.FindControl("DropDownListCurrency"), DropDownList).Text = "-" Then
                Currency = "NULL"
            Else
                Currency = "'" + DirectCast(row.FindControl("DropDownListCurrency"), DropDownList).Text + "'"
            End If
            SqlDataSourceProjects.UpdateCommand = "UPDATE [Table1_Project] " + _
                       " SET [ProjectID] = " + DirectCast(row.FindControl("TextBoxProjectIDEdit"), TextBox).Text + " " + _
                          " ,[ProjectName] = '" + DirectCast(row.FindControl("TextBoxProjectNameEdit"), TextBox).Text + "' " + _
                          " ,[CurrentStatus] = @CurrentStatus " + _
                          " ,[Type] = @Type " + _
                          " ,[Report] = @Report " + _
                          " ,[BackUpRequired] = @BackUpRequired " + _
                          " ,[NakladnayaEnabled] = @NakladnayaEnabled " + _
                          " ,[POcreate] = @POcreate " + _
                          " ,[InsuranceStart] = " + InsuranceStart + _
                          " ,[InsuranceFinish] = " + InsuranceFinish + _
                          " ,[ContractCurrency] = " + Currency + _
                          " ,[BackColor] = @BackColor " + _
                          " ,[ForeColor] = @ForeColor" + _
                     " WHERE [ProjectID] = " + LabelKeepProjectID.Text + ""

        End If

        If (e.CommandName = "InsuranceCertificate") Then

            ProjectIDTextBox.Text = e.CommandArgument.ToString()

            LabelProjectName.Text = ProjectIDTextBox.Text.Trim + " - " + PTS.CoreTables.CreateDataReader.Create_Table1_Project(ProjectIDTextBox.Text).ProjectName

            'ModalPopupExtenderInsuranceCertificate.Show()

            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalInsuranceCertificate" + "').modal({}) });", True)

        End If

    End Sub

    Protected Sub GridViewProjects_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewProjects.RowDataBound

        Dim MyTask As New MyCommonTasks
        MyTask.HoverEffectOnGridviewCells(sender, e.Row)


        Dim LinkButtonEdit As LinkButton = DirectCast(e.Row.FindControl("LinkButtonEdit"), LinkButton)
        Dim LinkButtonDelete As LinkButton = DirectCast(e.Row.FindControl("LinkButtonDelete"), LinkButton)

        Dim TextBoxProjectIDEdit As TextBox = DirectCast(e.Row.FindControl("TextBoxProjectIDEdit"), TextBox)
        Dim TextBoxProjectNameEdit As TextBox = DirectCast(e.Row.FindControl("TextBoxProjectNameEdit"), TextBox)
        Dim CheckBoxCurrentStatusEdit As CheckBox = DirectCast(e.Row.FindControl("CheckBoxCurrentStatusEdit"), CheckBox)
        Dim DropDownListProjectType As DropDownList = DirectCast(e.Row.FindControl("DropDownListProjectType"), DropDownList)
        Dim DropDownListCurrency As DropDownList = DirectCast(e.Row.FindControl("DropDownListCurrency"), DropDownList)
        Dim CheckBoxReportRequiredEdit As CheckBox = DirectCast(e.Row.FindControl("CheckBoxReportRequiredEdit"), CheckBox)
        Dim CheckBoxBackUpRequiredEdit As CheckBox = DirectCast(e.Row.FindControl("CheckBoxBackUpRequiredEdit"), CheckBox)
        Dim CheckBoxNakladnayaEnabledEdit As CheckBox = DirectCast(e.Row.FindControl("CheckBoxNakladnayaEnabledEdit"), CheckBox)
        Dim CheckBoxPOcreateEdit As CheckBox = DirectCast(e.Row.FindControl("CheckBoxPOcreateEdit"), CheckBox)
        Dim LabelDaySinceLastActionItem As Label = DirectCast(e.Row.FindControl("LabelDaySinceLastActionItem"), Label)
        Dim LabelDaySinceLastActionEdit As Label = DirectCast(e.Row.FindControl("LabelDaySinceLastActionEdit"), Label)
        Dim ColorPanel As Panel = DirectCast(e.Row.FindControl("Color"), Panel)

        If LabelDaySinceLastActionItem IsNot Nothing AndAlso Convert.ToInt32(LabelDaySinceLastActionItem.Text) < 0 Then
            LabelDaySinceLastActionItem.Text = "Nothing Yet"
            e.Row.Cells(8).BackColor = System.Drawing.Color.Silver
        End If

        If LabelDaySinceLastActionEdit IsNot Nothing AndAlso Convert.ToInt32(LabelDaySinceLastActionEdit.Text) < 0 Then
            LabelDaySinceLastActionEdit.Text = "Nothing Yet"
            e.Row.Cells(8).BackColor = System.Drawing.Color.Silver
        End If

        If Roles.IsUserInRole("Contract") OrElse User.Identity.Name.ToString.Trim.ToLower() = "alex.gadjiev" OrElse User.Identity.Name.ToString.ToLower() = "larisa" OrElse User.Identity.Name.ToString.ToLower() = "daria.pussep" Then
            ' in case of itemmode
            If LinkButtonDelete IsNot Nothing Then
                LinkButtonDelete.Visible = False
                LinkButtonEdit.Visible = True
            End If
            ' in case of edit mode
            If TextBoxProjectIDEdit IsNot Nothing AndAlso Page.User.Identity.Name.ToLower <> "savas" Then
                TextBoxProjectIDEdit.Visible = False
                TextBoxProjectNameEdit.Visible = False
                CheckBoxCurrentStatusEdit.Visible = False
                DropDownListProjectType.Visible = False
                DropDownListCurrency.Visible = False
                CheckBoxReportRequiredEdit.Visible = False
                CheckBoxBackUpRequiredEdit.Visible = False
                CheckBoxNakladnayaEnabledEdit.Visible = False
                CheckBoxPOcreateEdit.Visible = False
                ColorPanel.Visible = False
            End If
        Else

            If User.Identity.Name.ToString.ToLower() <> "savas" Then
                If LinkButtonDelete IsNot Nothing Then
                    LinkButtonDelete.Visible = False
                    LinkButtonEdit.Visible = False
                End If
            End If

        End If

        ' take ProjectID for Update issue
        If TextBoxProjectIDEdit IsNot Nothing Then
            LabelKeepProjectID.Text = TextBoxProjectIDEdit.Text
        End If

        ' highlight edit row
        If DropDownListProjectType IsNot Nothing Then
            e.Row.BackColor = System.Drawing.Color.LightSteelBlue
        End If

        ' put BlueDot if checked
        Dim CheckBoxCurrentStatusItem As CheckBox = DirectCast(e.Row.FindControl("CheckBoxCurrentStatusItem"), CheckBox)
        Dim CheckBoxReportRequiredItem As CheckBox = DirectCast(e.Row.FindControl("CheckBoxReportRequiredItem"), CheckBox)
        Dim CheckBoxBackUpRequiredItem As CheckBox = DirectCast(e.Row.FindControl("CheckBoxBackUpRequiredItem"), CheckBox)
        Dim CheckBoxNakladnayaEnabledItem As CheckBox = DirectCast(e.Row.FindControl("CheckBoxNakladnayaEnabledItem"), CheckBox)
        Dim CheckBoxPOcreateItem As CheckBox = DirectCast(e.Row.FindControl("CheckBoxPOcreateItem"), CheckBox)
        Dim ImageCurrentStatus As Image = DirectCast(e.Row.FindControl("ImageCurrentStatus"), Image)
        Dim ImageReport As Image = DirectCast(e.Row.FindControl("ImageReport"), Image)
        Dim ImageBackUpRequired As Image = DirectCast(e.Row.FindControl("ImageBackUpRequired"), Image)
        Dim ImageNakladnayaEnabledRequired As Image = DirectCast(e.Row.FindControl("ImageNakladnayaEnabledRequired"), Image)
        Dim ImagePOcreate As Image = DirectCast(e.Row.FindControl("ImagePOcreate"), Image)

        If CheckBoxCurrentStatusItem IsNot Nothing Then

            If CheckBoxCurrentStatusItem.Checked = True Then
                ImageCurrentStatus.Visible = True
            Else
                ImageCurrentStatus.Visible = False
            End If

            If CheckBoxReportRequiredItem.Checked = True Then
                ImageReport.Visible = True
            Else
                ImageReport.Visible = False
            End If

            If CheckBoxBackUpRequiredItem.Checked = True Then
                ImageBackUpRequired.Visible = True
            Else
                ImageBackUpRequired.Visible = False
            End If

            If CheckBoxNakladnayaEnabledItem.Checked = True Then
                ImageNakladnayaEnabledRequired.Visible = True
            Else
                ImageNakladnayaEnabledRequired.Visible = False
            End If


            If CheckBoxPOcreateItem.Checked = True Then
                ImagePOcreate.Visible = True
            Else
                ImagePOcreate.Visible = False
            End If

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LinkButtonInsuranceCertificate As LinkButton = DirectCast(e.Row.FindControl("LinkButtonInsuranceCertificate"), LinkButton)
            Using Adapter As New MercuryTableAdapters.Table1_ProjectInsurCertfTableAdapter
                If Adapter.GetCountByProjectID(DataBinder.Eval(e.Row.DataItem, "ProjectID")) = 0 Then
                    LinkButtonInsuranceCertificate.Text = "Add Data"
                    LinkButtonInsuranceCertificate.ForeColor = System.Drawing.Color.DarkGray
                    If Not Roles.IsUserInRole("EnterInsuranceCertificate") Then
                        LinkButtonInsuranceCertificate.Visible = False
                    End If
                Else
                    LinkButtonInsuranceCertificate.Text = "See Details"
                    LinkButtonInsuranceCertificate.ForeColor = System.Drawing.Color.Green
                End If
            End Using
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ImageLogo As Image = DirectCast(e.Row.FindControl("ImageLogo"), Image)
            If ImageLogo IsNot Nothing Then
                If Len(DataBinder.Eval(e.Row.DataItem, "Logo").ToString.Trim) < 15 Then
                    ImageLogo.Visible = False
                Else
                    ImageLogo.Visible = True
                End If
            End If
        End If

    End Sub

    Protected Sub GridViewProjects_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridViewProjects.RowUpdating
    End Sub

    Protected Sub ImageButtonAddProject_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonAddProject.Click
        If Convert.ToInt32(LabelFormViewVisibleStatus.Text) Mod 2 = 0 Then
            FormViewInsertProject.Visible = True
            LabelFormViewVisibleStatus.Text = Convert.ToString(Convert.ToInt32(LabelFormViewVisibleStatus.Text) + 1)
            ImageButtonAddProject.ImageUrl = "~/Images/minus.png"
            Dim DropDownListProjectType As DropDownList = FormViewInsertProject.FindControl("DropDownListProjectType")
            Dim CheckBoxCurrent As CheckBox = FormViewInsertProject.FindControl("CheckBoxCurrent")
            Dim CheckBoxReporting As CheckBox = FormViewInsertProject.FindControl("CheckBoxReporting")
            Dim CheckBoxBackUp As CheckBox = FormViewInsertProject.FindControl("CheckBoxBackUp")
            Dim CheckBoxPOcreate As CheckBox = FormViewInsertProject.FindControl("CheckBoxPOcreate")

            DropDownListProjectType.SelectedValue = "FitOut"
            CheckBoxCurrent.Checked = True
            CheckBoxReporting.Checked = True
            CheckBoxBackUp.Checked = True
            CheckBoxPOcreate.Checked = True
            Exit Sub
        ElseIf Convert.ToInt32(LabelFormViewVisibleStatus.Text) Mod 2 = 1 Then
            FormViewInsertProject.Visible = False
            LabelFormViewVisibleStatus.Text = Convert.ToString(Convert.ToInt32(LabelFormViewVisibleStatus.Text) + 1)
            ImageButtonAddProject.ImageUrl = "~/Images/insert.png"
        End If
    End Sub

    Protected Sub FormViewInsertProject_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertedEventArgs) Handles FormViewInsertProject.ItemInserted
        GridViewProjects.DataBind()
        FormViewInsertProject.Visible = False
        ImageButtonAddProject.ImageUrl = "~/Images/insert.png"
    End Sub

    Protected Sub FormViewInsertProject_ItemInserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertEventArgs) Handles FormViewInsertProject.ItemInserting
        Dim DropDownListProjectType As DropDownList = FormViewInsertProject.FindControl("DropDownListProjectType")
        e.Values("Type") = DropDownListProjectType.SelectedValue
    End Sub

    Protected Sub FormViewInsertProject_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormViewInsertProject.Load
        If Not IsPostBack Then
            Dim DropDownListProjectType As DropDownList = FormViewInsertProject.FindControl("DropDownListProjectType")
            Dim CheckBoxCurrent As CheckBox = FormViewInsertProject.FindControl("CheckBoxCurrent")
            Dim CheckBoxReporting As CheckBox = FormViewInsertProject.FindControl("CheckBoxReporting")
            Dim CheckBoxBackUp As CheckBox = FormViewInsertProject.FindControl("CheckBoxBackUp")
            Dim CheckBoxPOcreate As CheckBox = FormViewInsertProject.FindControl("CheckBoxPOcreate")

            DropDownListProjectType.SelectedValue = "FitOut"
            CheckBoxCurrent.Checked = True
            CheckBoxReporting.Checked = True
            CheckBoxBackUp.Checked = True
            CheckBoxPOcreate.Checked = True
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Roles.IsUserInRole("EnterInsuranceCertificate") Then
            LinkButtonShowHide.Visible = False
            LinkButtonAdd4Doc.Visible = False
        End If

        If Page.User.Identity.Name = "savas" Then
        Else
            ImageButtonAddProject.Visible = False
        End If

        RefresImages()

    End Sub

    Protected Sub ButtonUploadInsuranceCertificate_Click(sender As Object, e As EventArgs)

        'ModalPopupExtenderInsuranceCertificate.Show()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalInsuranceCertificate" + "').modal({}) });", True)

        Dim FileUploadInsuranceCertificate As FileUpload = FormViewInsCert.FindControl("FileUploadInsuranceCertificate")
        Dim TextBoxFileLink As TextBox = FormViewInsCert.FindControl("TextBoxFileLink")
        Dim LabelFileUploaded As Label = FormViewInsCert.FindControl("LabelFileUploaded")

        If FileUploadInsuranceCertificate.HasFile Then
            Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
            FileUploadInsuranceCertificate.SaveAs(MapPath("~/InsuranceCertification/" + UniqueString1 +
                                              System.IO.Path.GetExtension(FileUploadInsuranceCertificate.PostedFile.FileName)))
            TextBoxFileLink.Text = "~/InsuranceCertification/" + UniqueString1 +
                                              System.IO.Path.GetExtension(FileUploadInsuranceCertificate.PostedFile.FileName)
            LabelFileUploaded.ForeColor = System.Drawing.Color.DarkGreen
            LabelFileUploaded.Text = FileUploadInsuranceCertificate.PostedFile.FileName + " has been loaded successfully"

        End If

    End Sub

    Protected Sub ButtonInsertInsuranceCertificate_Click(sender As Object, e As EventArgs)

        'ModalPopupExtenderInsuranceCertificate.Show()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalInsuranceCertificate" + "').modal({}) });", True)

    End Sub

    Protected Sub LinkButtonEdit_Click(sender As Object, e As EventArgs)

        'ModalPopupExtenderInsuranceCertificate.Show()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalInsuranceCertificate" + "').modal({}) });", True)

    End Sub

    Protected Sub LinkButtonDelete_Click(sender As Object, e As EventArgs)

        'ModalPopupExtenderInsuranceCertificate.Show()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalInsuranceCertificate" + "').modal({}) });", True)

    End Sub

    Protected Sub LinkButtonUpdate_Click(sender As Object, e As EventArgs)

        'ModalPopupExtenderInsuranceCertificate.Show()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalInsuranceCertificate" + "').modal({}) });", True)

    End Sub

    Protected Sub LinkButton3_Click(sender As Object, e As EventArgs)

        'ModalPopupExtenderInsuranceCertificate.Show()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalInsuranceCertificate" + "').modal({}) });", True)

    End Sub

    Protected Sub FormViewInsCert_ItemInserted(sender As Object, e As FormViewInsertedEventArgs) Handles FormViewInsCert.ItemInserted

        GridViewInstCert.DataBind()
        LinkButtonShowHide.Visible = True
        FormViewInsCert.Visible = False
        GridViewProjects.DataBind()

    End Sub

    Protected Sub GridViewInstCert_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridViewInstCert.RowCommand

        If (e.CommandName = "UploadOnEdit") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridViewInstCert.Rows(index)
            Dim FileUploadInsuranceCertificate As FileUpload = DirectCast(row.FindControl("FileUploadInsuranceCertificate"), FileUpload)
            Dim TextBoxFileLink As TextBox = DirectCast(row.FindControl("TextBoxFileLink"), TextBox)
            Dim LabelFileUploaded As Label = DirectCast(row.FindControl("LabelFileUploaded"), Label)

            If FileUploadInsuranceCertificate.HasFile Then
                Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                FileUploadInsuranceCertificate.SaveAs(MapPath("~/InsuranceCertification/" + UniqueString1 +
                                                  System.IO.Path.GetExtension(FileUploadInsuranceCertificate.PostedFile.FileName)))
                TextBoxFileLink.Text = "~/InsuranceCertification/" + UniqueString1 +
                                                  System.IO.Path.GetExtension(FileUploadInsuranceCertificate.PostedFile.FileName)
                LabelFileUploaded.ForeColor = System.Drawing.Color.DarkGreen
                LabelFileUploaded.Text = FileUploadInsuranceCertificate.PostedFile.FileName + " has been loaded successfully"

            End If

            'ModalPopupExtenderInsuranceCertificate.Show()
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalInsuranceCertificate" + "').modal({}) });", True)

        End If

        If (e.CommandName = "OpenDocument") Then
            Dim LinkToFile As String = e.CommandArgument.ToString
            Dim openpdf As New MyCommonTasks
            openpdf.OpenPDF(LinkToFile)
            openpdf = Nothing
        End If

    End Sub

    Protected Sub GridViewInstCert_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewInstCert.RowDataBound

        If DirectCast(e.Row.FindControl("ImageButtonItem"), ImageButton) IsNot Nothing Then
            If IsDBNull(DataBinder.Eval(e.Row.DataItem, "DocumentReference")) Then
                DirectCast(e.Row.FindControl("ImageButtonItem"), ImageButton).Visible = False
            Else
                PTSMainClass.ProvideImageFromFile(DirectCast(e.Row.FindControl("ImageButtonItem"), ImageButton), DataBinder.Eval(e.Row.DataItem, "DocumentReference"))
            End If
        End If

        If Not Roles.IsUserInRole("EnterInsuranceCertificate") Then
            GridViewInstCert.Columns(0).Visible = False
        End If

    End Sub

    Protected Sub LinkButtonShowHide_Click(sender As Object, e As EventArgs)

        'ModalPopupExtenderInsuranceCertificate.Show()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalInsuranceCertificate" + "').modal({}) });", True)

        FormViewInsCert.Visible = True
        LinkButtonShowHide.Visible = False

    End Sub

    Protected Sub GridViewInstCert_RowDeleted(sender As Object, e As GridViewDeletedEventArgs) Handles GridViewInstCert.RowDeleted

        GridViewProjects.DataBind()

    End Sub

    Protected Sub GridViewInstCert_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GridViewInstCert.RowUpdating

        Dim row = GridViewInstCert.Rows(e.RowIndex)
        Dim CheckBoxDelete As CheckBox = CType(row.FindControl("CheckBoxDelete"), CheckBox)
        Dim ID As Integer = CInt(GridViewInstCert.DataKeys(e.RowIndex).Value.ToString())

        If CheckBoxDelete.Checked = True Then
            e.NewValues("DocumentReference") = Nothing
        End If

        ' Prepare variables for email body
        Dim _remarks As String
        Dim _insuranceStart As String
        Dim _insuranceFinish As String
        Dim _documentReference As String

        If Len(e.NewValues("Remarks")) = 0 Then
            _remarks = ""
        Else
            _remarks = e.NewValues("Remarks")
        End If

        If Len(e.NewValues("InsuranceStart")) = 0 Then
            _insuranceStart = ""
        Else
            _insuranceStart = String.Format("{0:dd/MM/yyyy}", e.NewValues("InsuranceStart"))
        End If

        If Len(e.NewValues("InsuranceFinish")) = 0 Then
            _insuranceFinish = ""
        Else
            _insuranceFinish = String.Format("{0:dd/MM/yyyy}", e.NewValues("InsuranceFinish"))
        End If

        If Len(e.NewValues("DocumentReference")) = 0 Then
            _documentReference = ""
        Else
            _documentReference = " <a href=" + "'" + "http://pts.mercuryeng.ru/webforms/showFile.aspx?link=" + Convert.ToString(e.NewValues("DocumentReference")).Replace("~", "") + "'" + " target=" + """" + "_blank" + """" + ">SEE DOCUMENT</a> "
        End If

        ' Check 
        If Len(e.OldValues("DocumentReference")) = 0 And Len(e.NewValues("DocumentReference")) > 0 Then
            ' very new attachment
            SendMail("Insurance Certificate attached under Project: " + ProjectIDTextBox.Text.Trim + " - " + PTS.CoreTables.CreateDataReader.Create_Table1_Project(ProjectIDTextBox.Text.Trim).ProjectName, _remarks, _insuranceStart, _insuranceFinish, _documentReference)
        ElseIf Len(e.OldValues("DocumentReference")) > 0 And Len(e.NewValues("DocumentReference")) > 0 Then
            If e.OldValues("DocumentReference") <> e.NewValues("DocumentReference") Then
                ' attachment updated
                SendMail("Insurance Certificate updated under Project: " + ProjectIDTextBox.Text.Trim + " - " + PTS.CoreTables.CreateDataReader.Create_Table1_Project(ProjectIDTextBox.Text.Trim).ProjectName, _remarks, _insuranceStart, _insuranceFinish, _documentReference)
            End If
        ElseIf Len(e.OldValues("DocumentReference")) > 0 And Len(e.NewValues("DocumentReference")) = 0 Then
            ' attachment deleted
            SendMail("Insurance Certificate removed under Project: " + ProjectIDTextBox.Text.Trim + " - " + PTS.CoreTables.CreateDataReader.Create_Table1_Project(ProjectIDTextBox.Text.Trim).ProjectName, _remarks, _insuranceStart, _insuranceFinish, _documentReference)
        End If

    End Sub

    Protected Sub SendMail(ByVal Subject As String, ByVal _remarks As String, ByVal _InsStart As String, ByVal _InsFinish As String, ByVal _DocRef As String, Optional ByVal _DocType As String = "", Optional ByVal _FromGeneralPolicy As Boolean = False)
        Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "Mercury Russia Procurement")
        Dim mm1 As New MailMessage()
        mm1.From = MailFrom
        'mm1.To.Add("savas.erzin@gmail.com")
        Dim ProjectIDString As String = ""
        If Len(ProjectIDTextBox.Text.Trim) = 0 Then
            ProjectIDString = "0"
        Else
            ProjectIDString = ProjectIDTextBox.Text.Trim
        End If

        AddReceipents(mm1, Convert.ToInt16(ProjectIDString), _FromGeneralPolicy)

        mm1.Subject = Subject
        If _DocType = "" Then
            mm1.Body =
                        "    <table> " +
            "        <tr style=" + """" + "background-color:lightblue;" + """" + "> " +
            "            <td style=" + """" + "padding:5px;" + """" + "> " +
            "                Project Name " +
            "            </td> " +
            "            <td style=" + """" + "width:150px;padding:5px;" + """" + "> " +
            "                Remark " +
            "            </td> " +
            "            <td style=" + """" + "padding:5px;" + """" + "> " +
            "                Insurance Start " +
            "            </td> " +
            "            <td style=" + """" + "padding:5px;" + """" + "> " +
            "                Insurance Finish " +
            "            </td> " +
            "            <td style=" + """" + "padding:5px;" + """" + "> " +
            "                Link To Document " +
            "            </td> " +
            "        </tr> " +
            "        <tr> " +
            "            <td style=" + """" + "padding:5px;" + """" + "> " +
            "                " + ProjectIDString + " - " + PTS.CoreTables.CreateDataReader.Create_Table1_Project(ProjectIDString).ProjectName + " " +
            "            </td> " +
            "            <td style=" + """" + "padding:5px;" + """" + "> " +
            "                " + _remarks + " " +
            "            </td> " +
            "            <td style=" + """" + "padding:5px;" + """" + "> " +
            "                " + _InsStart + " " +
            "            </td> " +
            "            <td style=" + """" + "padding:5px;" + """" + "> " +
            "                " + _InsFinish + " " +
            "            </td> " +
            "            <td style=" + """" + "padding:5px;" + """" + "> " +
            "                " + _DocRef + " " +
            "            </td> " +
            "        </tr> " +
            "    </table> "
        ElseIf _DocType = "4Doc" Then
            If _DocRef = String.Empty Then
                mm1.Body = "Document removed"
            Else
                mm1.Body = "Document updated > " + _DocRef
            End If
        End If
        mm1.IsBodyHtml = True

        Dim smtp As New SmtpClient_RussianEncoded
        Try
            smtp.Send(mm1)
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub FormViewInsCert_ItemInserting(sender As Object, e As FormViewInsertEventArgs) Handles FormViewInsCert.ItemInserting

        ' Prepare variables for email body
        Dim _remarks As String
        Dim _insuranceStart As String
        Dim _insuranceFinish As String
        Dim _documentReference As String

        If Len(e.Values("Remarks")) = 0 Then
            _remarks = ""
        Else
            _remarks = e.Values("Remarks")
        End If

        If Len(e.Values("InsuranceStart")) = 0 Then
            _insuranceStart = ""
        Else
            _insuranceStart = String.Format("{0:dd/MM/yyyy}", e.Values("InsuranceStart"))
        End If

        If Len(e.Values("InsuranceFinish")) = 0 Then
            _insuranceFinish = ""
        Else
            _insuranceFinish = String.Format("{0:dd/MM/yyyy}", e.Values("InsuranceFinish"))
        End If

        If Len(e.Values("DocumentReference")) = 0 Then
            _documentReference = ""
        Else
            _documentReference = " <a href=" + "'" + "http://pts.mercuryeng.ru/webforms/showFile.aspx?link=" + Convert.ToString(e.Values("DocumentReference")).Replace("~", "") + "'" + " target=" + """" + "_blank" + """" + ">SEE DOCUMENT</a> "
        End If

        SendMail("Insurance Certificate entered under Project: " + ProjectIDTextBox.Text.Trim + " - " + PTS.CoreTables.CreateDataReader.Create_Table1_Project(ProjectIDTextBox.Text.Trim).ProjectName, _remarks, _insuranceStart, _insuranceFinish, _documentReference)

    End Sub

    Protected Sub AddReceipents(ByVal _mm As MailMessage, ByVal _ProjectID As Integer, Optional ByVal _FromGeneralPolicy As Boolean = False)

        If _FromGeneralPolicy = False Then
            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = " SELECT     RTRIM(dbo.aspnet_Membership.LoweredEmail) AS LoweredEmail " +
                " FROM         dbo.aspnet_Users INNER JOIN " +
                "                       dbo.aspnet_Membership ON dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId AND dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId INNER JOIN " +
                "                       dbo.Table_Project_User_InsurancePolicyNotification ON dbo.aspnet_Users.UserName = dbo.Table_Project_User_InsurancePolicyNotification.UserName " +
                " WHERE     (dbo.Table_Project_User_InsurancePolicyNotification.ProjectID = @ProjectID) "

                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.Text

                Dim ProjectID As SqlParameter = cmd.Parameters.Add("@ProjectID", System.Data.SqlDbType.SmallInt)
                ProjectID.Value = _ProjectID

                Dim dr As SqlDataReader = cmd.ExecuteReader
                While dr.Read
                    _mm.To.Add(dr(0).ToString)
                End While

                con.Close()
                dr.Close()
            End Using
        Else
            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = " SELECT     RTRIM(dbo.aspnet_Membership.LoweredEmail) AS LoweredEmail " +
                " FROM         dbo.aspnet_Users INNER JOIN " +
                "                       dbo.aspnet_Membership ON dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId AND dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId INNER JOIN " +
                "                           (SELECT     UserName " +
                "                             FROM          dbo.Table_Project_User_InsurancePolicyNotification " +
                "                             GROUP BY UserName) AS AllUsersInNotification ON dbo.aspnet_Users.UserName = AllUsersInNotification.UserName "

                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.Text

                Dim dr As SqlDataReader = cmd.ExecuteReader
                While dr.Read
                    _mm.To.Add(dr(0).ToString)
                End While

                con.Close()
                dr.Close()
            End Using
        End If

    End Sub

    Protected Sub ButtonUploadDoc1_Click(sender As Object, e As EventArgs)

        If FileUploadDoc1.HasFile Then
            Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
            FileUploadDoc1.SaveAs(MapPath("~/InsuranceCertification/" + UniqueString1 +
                                              System.IO.Path.GetExtension(FileUploadDoc1.PostedFile.FileName)))
            LabelLinkDoc1.Text = "~/InsuranceCertification/" + UniqueString1 +
                                              System.IO.Path.GetExtension(FileUploadDoc1.PostedFile.FileName)
            LabelFileUploadInfo1.ForeColor = System.Drawing.Color.DarkGreen
            LabelFileUploadInfo1.Text = FileUploadDoc1.PostedFile.FileName + " has been loaded successfully"

        End If

    End Sub

    Protected Sub ButtonSaveDoc1_Click(sender As Object, e As EventArgs)

        Using Adapter As New MercuryTableAdapters.Table1_InsurPolicyDocsTableAdapter
            If CheckBoxDoc1.Checked = True Then
                Adapter.UpdateInsPolicy1(Nothing)
                SendMail("GENERAL INSURANCE POLICY removed: ", String.Empty, String.Empty, String.Empty, String.Empty, "4Doc", True)
            Else
                Adapter.UpdateInsPolicy1(LabelLinkDoc1.Text)
                SendMail("GENERAL INSURANCE POLICY updated: ", String.Empty, String.Empty, String.Empty, " <a href=" + "'" + "http://pts.mercuryeng.ru/webforms/showFile.aspx?link=" + LabelLinkDoc1.Text.Replace("~", "") + "'" + " target=" + """" + "_blank" + """" + ">SEE DOCUMENT</a> ", "4Doc", True)
            End If
            Adapter.Dispose()
            RefresImages()
            CheckBoxDoc1.Checked = False
        End Using

    End Sub

    Protected Sub ImageButtonDoc1_Click(sender As Object, e As ImageClickEventArgs)

        Using Adapter As New MercuryTableAdapters.Table1_InsurPolicyDocsTableAdapter
            Dim table As New Mercury.Table1_InsurPolicyDocsDataTable
            table = Adapter.GetData
            For Each _row As Mercury.Table1_InsurPolicyDocsRow In table
                Dim openpdf As New MyCommonTasks
                openpdf.OpenPDF(_row.InsrPolicyDoc1)
                openpdf = Nothing
            Next
            table.Dispose()
            Adapter.Dispose()
        End Using

    End Sub

    Protected Sub ButtonUploadDoc2_Click(sender As Object, e As EventArgs)

        If FileUploadDoc2.HasFile Then
            Dim UniqueString2 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
            FileUploadDoc2.SaveAs(MapPath("~/InsuranceCertification/" + UniqueString2 +
                                              System.IO.Path.GetExtension(FileUploadDoc2.PostedFile.FileName)))
            LabelLinkDoc2.Text = "~/InsuranceCertification/" + UniqueString2 +
                                              System.IO.Path.GetExtension(FileUploadDoc2.PostedFile.FileName)
            LabelFileUploadInfo2.ForeColor = System.Drawing.Color.DarkGreen
            LabelFileUploadInfo2.Text = FileUploadDoc2.PostedFile.FileName + " has been loaded successfully"

        End If

    End Sub

    Protected Sub ButtonSaveDoc2_Click(sender As Object, e As EventArgs)

        Using Adapter As New MercuryTableAdapters.Table1_InsurPolicyDocsTableAdapter
            If CheckBoxDoc2.Checked = True Then
                Adapter.UpdateInsPolicy2(Nothing)
                SendMail("S.R.O.-S INSURANCE removed: ", String.Empty, String.Empty, String.Empty, String.Empty, "4Doc", True)
            Else
                Adapter.UpdateInsPolicy2(LabelLinkDoc2.Text)
                SendMail("S.R.O.-S INSURANCE updated: ", String.Empty, String.Empty, String.Empty, " <a href=" + "'" + "http://pts.mercuryeng.ru/webforms/showFile.aspx?link=" + LabelLinkDoc2.Text.Replace("~", "") + "'" + " target=" + """" + "_blank" + """" + ">SEE DOCUMENT</a> ", "4Doc", True)
            End If
            Adapter.Dispose()
            RefresImages()
            CheckBoxDoc2.Checked = False
        End Using

    End Sub

    Protected Sub ImageButtonDoc2_Click(sender As Object, e As ImageClickEventArgs)

        Using Adapter As New MercuryTableAdapters.Table1_InsurPolicyDocsTableAdapter
            Dim table As New Mercury.Table1_InsurPolicyDocsDataTable
            table = Adapter.GetData
            For Each _row As Mercury.Table1_InsurPolicyDocsRow In table
                Dim openpdf As New MyCommonTasks
                openpdf.OpenPDF(_row.InsrPolicyDoc2)
                openpdf = Nothing
            Next
            table.Dispose()
            Adapter.Dispose()
        End Using

    End Sub

    Protected Sub ButtonUploadDoc3_Click(sender As Object, e As EventArgs)

        If FileUploadDoc3.HasFile Then
            Dim UniqueString3 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
            FileUploadDoc3.SaveAs(MapPath("~/InsuranceCertification/" + UniqueString3 +
                                              System.IO.Path.GetExtension(FileUploadDoc3.PostedFile.FileName)))
            LabelLinkDoc3.Text = "~/InsuranceCertification/" + UniqueString3 +
                                              System.IO.Path.GetExtension(FileUploadDoc3.PostedFile.FileName)
            LabelFileUploadInfo3.ForeColor = System.Drawing.Color.DarkGreen
            LabelFileUploadInfo3.Text = FileUploadDoc3.PostedFile.FileName + " has been loaded successfully"

        End If

    End Sub

    Protected Sub ButtonSaveDoc3_Click(sender As Object, e As EventArgs)

        Using Adapter As New MercuryTableAdapters.Table1_InsurPolicyDocsTableAdapter
            If CheckBoxDoc3.Checked = True Then
                Adapter.UpdateInsPolicy3(Nothing)
                SendMail("S.R.O.-P INSURANCE removed: ", String.Empty, String.Empty, String.Empty, String.Empty, "4Doc", True)
            Else
                Adapter.UpdateInsPolicy3(LabelLinkDoc3.Text)
                SendMail("S.R.O.-P INSURANCE updated: ", String.Empty, String.Empty, String.Empty, " <a href=" + "'" + "http://pts.mercuryeng.ru/webforms/showFile.aspx?link=" + LabelLinkDoc3.Text.Replace("~", "") + "'" + " target=" + """" + "_blank" + """" + ">SEE DOCUMENT</a> ", "4Doc", True)
            End If
            Adapter.Dispose()
            RefresImages()
            CheckBoxDoc3.Checked = False
        End Using

    End Sub

    Protected Sub ImageButtonDoc3_Click(sender As Object, e As ImageClickEventArgs)

        Using Adapter As New MercuryTableAdapters.Table1_InsurPolicyDocsTableAdapter
            Dim table As New Mercury.Table1_InsurPolicyDocsDataTable
            table = Adapter.GetData
            For Each _row As Mercury.Table1_InsurPolicyDocsRow In table
                Dim openpdf As New MyCommonTasks
                openpdf.OpenPDF(_row.InsrPolicyDoc3)
                openpdf = Nothing
            Next
            table.Dispose()
            Adapter.Dispose()
        End Using

    End Sub

    Protected Sub ButtonUploadDoc4_Click(sender As Object, e As EventArgs)

        If FileUploadDoc4.HasFile Then
            Dim UniqueString4 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
            FileUploadDoc4.SaveAs(MapPath("~/InsuranceCertification/" + UniqueString4 +
                                              System.IO.Path.GetExtension(FileUploadDoc4.PostedFile.FileName)))
            LabelLinkDoc4.Text = "~/InsuranceCertification/" + UniqueString4 +
                                              System.IO.Path.GetExtension(FileUploadDoc4.PostedFile.FileName)
            LabelFileUploadInfo4.ForeColor = System.Drawing.Color.DarkGreen
            LabelFileUploadInfo4.Text = FileUploadDoc4.PostedFile.FileName + " has been loaded successfully"

        End If

    End Sub

    Protected Sub ButtonSaveDoc4_Click(sender As Object, e As EventArgs)

        Using Adapter As New MercuryTableAdapters.Table1_InsurPolicyDocsTableAdapter
            If CheckBoxDoc4.Checked = True Then
                Adapter.UpdateInsPolicy4(Nothing)
                SendMail("G.O. + PROF. INSURANCE removed: ", String.Empty, String.Empty, String.Empty, String.Empty, "4Doc", True)
            Else
                Adapter.UpdateInsPolicy4(LabelLinkDoc4.Text)
                SendMail("G.O. + PROF. INSURANCE updated: ", String.Empty, String.Empty, String.Empty, " <a href=" + "'" + "http://pts.mercuryeng.ru/webforms/showFile.aspx?link=" + LabelLinkDoc4.Text.Replace("~", "") + "'" + " target=" + """" + "_blank" + """" + ">SEE DOCUMENT</a> ", "4Doc", True)
            End If
            Adapter.Dispose()
            RefresImages()
            CheckBoxDoc4.Checked = False
        End Using

    End Sub

    Protected Sub ImageButtonDoc4_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButtonDoc4.Click

        Using Adapter As New MercuryTableAdapters.Table1_InsurPolicyDocsTableAdapter
            Dim table As New Mercury.Table1_InsurPolicyDocsDataTable
            table = Adapter.GetData
            For Each _row As Mercury.Table1_InsurPolicyDocsRow In table
                Dim openpdf As New MyCommonTasks
                openpdf.OpenPDF(_row.InsrPolicyDoc4)
                openpdf = Nothing
            Next
            table.Dispose()
            Adapter.Dispose()
        End Using

    End Sub

    Protected Sub RefresImages()
        Using Adapter As New MercuryTableAdapters.Table1_InsurPolicyDocsTableAdapter
            Dim table As New Mercury.Table1_InsurPolicyDocsDataTable
            table = Adapter.GetData
            For Each _row As Mercury.Table1_InsurPolicyDocsRow In table
                PTSMainClass.ProvideImageFromFile(ImageButtonDoc1, _row.InsrPolicyDoc1)
                PTSMainClass.ProvideImageFromFile(ImageButtonAdd1, "~/InsuranceCertification/Addendum1_To_GENERAL_INSURANCE_POLICY.pdf")
                PTSMainClass.ProvideImageFromFile(ImageButtonDoc2, _row.InsrPolicyDoc2)
                PTSMainClass.ProvideImageFromFile(ImageButtonDoc3, _row.InsrPolicyDoc3)
                PTSMainClass.ProvideImageFromFile(ImageButtonDoc4, _row.InsrPolicyDoc4)
            Next
            table.Dispose()
            Adapter.Dispose()
        End Using

        PTSMainClass.ProvideImageFromFile(ImageButtonIns1, "~/Insurance/Damage To Our Works.doc")
        PTSMainClass.ProvideImageFromFile(ImageButtonIns2, "~/Insurance/Damage To Third Person.doc")

    End Sub

    Protected Sub LinkButtonAdd4Doc_Click(sender As Object, e As EventArgs)

        If (Convert.ToInt16(LiteralSayac.Text) Mod 2) = 0 Then
            Panel4DocActivation.Visible = True
            LinkButtonAdd4Doc.Text = "Hide Document Entry"
        ElseIf (Convert.ToInt16(LiteralSayac.Text) Mod 2) = 1 Then
            Panel4DocActivation.Visible = False
            LinkButtonAdd4Doc.Text = "Add or Update Documents"
        End If

        LiteralSayac.Text = Convert.ToString(Convert.ToInt16(LiteralSayac.Text) + 1)

    End Sub

    Protected Sub ImageButtonIns1_Click(sender As Object, e As ImageClickEventArgs)

        Dim openpdf As New MyCommonTasks
        openpdf.OpenPDF("~/Insurance/Damage To Our Works.doc")
        openpdf = Nothing


    End Sub

    Protected Sub ImageButtonIns2_Click(sender As Object, e As ImageClickEventArgs)

        Dim openpdf As New MyCommonTasks
        openpdf.OpenPDF("~/Insurance/Damage To Third Person.doc")
        openpdf = Nothing

    End Sub

    Protected Sub ImageButtonAdd1_Click(sender As Object, e As ImageClickEventArgs)

        Dim openpdf As New MyCommonTasks
        openpdf.OpenPDF("~/InsuranceCertification/Addendum1_To_GENERAL_INSURANCE_POLICY.pdf")
        openpdf = Nothing

    End Sub

End Class

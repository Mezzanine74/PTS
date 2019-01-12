Imports System
Imports System.IO
Imports System.Net
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports AjaxControlToolkit
Imports PTS_App_Code_VB_Project.PTS_MERCURY.helper.EmailGenerator

Partial Class __paymentrequestACoptimizationZZ222
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' show skip box only to admin
        Dim cbx_skippm As CheckBox = FormViewPayReq.FindControl("cbx_skippm")

        If User.IsInRole("admin") OrElse Page.User.Identity.Name.ToLower = "elmira.shabaeva" OrElse Page.User.Identity.Name.ToLower = "mariya.podobueva" OrElse Page.User.Identity.Name.ToLower = "natalia.larionova" Then
            cbx_skippm.Visible = True
        Else
            cbx_skippm.Visible = False
        End If

        ' it provides query parameter for DropDownListSupplier
        If IsPostBack Or Not IsPostBack Then
            TextBoxUserName.Text = Page.User.Identity.Name
        End If

        ' it runs if it is coming from IncoiceDefine page 
        If Not IsPostBack Then
            If Not PreviousPage Is Nothing Then
                Dim previousPageURL As String = Request.UrlReferrer.AbsolutePath.ToString
                Dim PageFileInfo As New System.IO.FileInfo(previousPageURL)
                If PageFileInfo.Name = "invoicedefine.aspx" Then

                    Dim ContPlaceHold As ContentPlaceHolder = PreviousPage.Master.FindControl("MainContent")
                    Dim FormViewInvoicePrevious As FormView = ContPlaceHold.FindControl("FormViewInvoice")
                    Dim DrpProjectPrevious As DropDownList = FormViewInvoicePrevious.FindControl("DropDownListPrjID")

                    Dim TxtInvoiceIDfromPreviousPage As TextBox = ContPlaceHold.FindControl("TextBoxStoreInvoiceID")
                    Dim TxtInvoiceIDThisPage As TextBox = FormViewPayReq.FindControl("InvoiceIDTextBox")

                    TxtInvoiceIDThisPage.Text = TxtInvoiceIDfromPreviousPage.Text

                    ' refresh navigateURL for hyperlink cover page
                    Dim HyperLinkProduceCoverPage As HyperLink = FormViewPayReq.FindControl("HyperLinkProduceCoverPage")
                    Dim PRN As String = ""
                    Dim PRNtextBox As TextBox = FormViewPayReq.FindControl("SiteRecordNoTextBox")
                    If String.IsNullOrEmpty(PRNtextBox.Text) Then
                        PRN = "?"
                    Else
                        PRN = PRNtextBox.Text
                    End If
                    HyperLinkProduceCoverPage.NavigateUrl = "~/webforms/PRN_coverpage.aspx?InvoiceID=" + TxtInvoiceIDThisPage.Text + "&PRN=" + PRN

                    ' It runs Project dropdownlist very first time

                    Dim DropDownPrjj0 As DropDownList = FormViewPayReq.FindControl("DropDownListProject")
                    Using con0 As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                        con0.Open()
                        Dim sqlstring0 As String = " SELECT * FROM ( " + _
                        "                         SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID))  " + _
                        "                                               AS ProjectName, dbo.Table_Approval_UserRolePrjectJunction.UserName " + _
                        "                         FROM         dbo.Table1_Project INNER JOIN " + _
                        "                                               dbo.Table_Approval_UserRolePrjectJunction ON dbo.Table1_Project.ProjectID = dbo.Table_Approval_UserRolePrjectJunction.ProjectID " + _
                        "                         WHERE     (dbo.Table1_Project.CurrentStatus = 1) AND (dbo.Table_Approval_UserRolePrjectJunction.UserName = @UserName ) AND  " + _
                        "                                               (dbo.Table_Approval_UserRolePrjectJunction.RoleName = N'InitiateContractAndAddendum') " + _
                        "  " + _
                        "                         UNION ALL " + _
                        "  " + _
                        "                         SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID))  " + _
                        "                                               AS ProjectName, dbo.aspnet_Users.UserName " + _
                        "                         FROM         dbo.Table1_Project INNER JOIN " + _
                        "                                               dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN " + _
                        "                                               dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId " + _
                        "                         WHERE     (dbo.Table1_Project.CurrentStatus = 1) AND (dbo.aspnet_Users.UserName = @UserName) AND (dbo.Table1_Project.NewGeneration = 0) " + _
                        "                         ) AS Source " + _
                        "                         ORDER BY ProjectName "


                        Dim cmd0 As New SqlCommand(sqlstring0, con0)

                        cmd0.CommandType = System.Data.CommandType.Text

                        Dim UserParm As SqlParameter = cmd0.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar, 256)
                        UserParm.Value = Page.User.Identity.Name

                        Dim dr0 As SqlDataReader = cmd0.ExecuteReader

                        DropDownPrjj0.DataSource = dr0
                        DropDownPrjj0.DataTextField = "ProjectName"
                        DropDownPrjj0.DataValueField = "ProjectID"
                        DropDownPrjj0.DataBind()
                        con0.Close()
                        con0.Dispose()
                        dr0.Close()

                        'ActivateButtonSendRequestToPM(DrpProjectPrevious.SelectedValue)
                        'ChekcifPMrequestSentearlier(Convert.ToInt32(TxtInvoiceIDThisPage.Text), DrpProjectPrevious.SelectedValue)

                    End Using

                    ' it takes previous Project name here
                    DropDownPrjj0.SelectedValue = DrpProjectPrevious.SelectedValue


                    ' it takes invoiceID not in pending yet 
                    Dim DrpInvoiceNotInPendingYet As DropDownList = FormViewPayReq.FindControl("DropDownListInvoiceIDNotInPendingYet")
                    DrpInvoiceNotInPendingYet.DataBind()
                    DrpInvoiceNotInPendingYet.SelectedValue = Convert.ToInt32(TxtInvoiceIDfromPreviousPage.Text)

                    ' it provide blue Label values
                    Dim Drp01 As DropDownList = FormViewPayReq.FindControl("DropDownListSupplierName")
                    Dim Drp02 As DropDownList = FormViewPayReq.FindControl("DropDownListDescription")
                    Dim Drp03 As DropDownList = FormViewPayReq.FindControl("DropDownListInvoiceNo")
                    Dim Drp04 As DropDownList = FormViewPayReq.FindControl("DropDownListInvoiceDate")
                    Dim Drp05 As DropDownList = FormViewPayReq.FindControl("DropDownListInvoiceValue")
                    Dim Drp06 As DropDownList = FormViewPayReq.FindControl("DropDownListCurrency")

                    Dim Lbl01 As Label = FormViewPayReq.FindControl("LabelSupplierName")
                    Dim Lbl02 As Label = FormViewPayReq.FindControl("LabelDescription")
                    Dim Lbl03 As Label = FormViewPayReq.FindControl("LabelInvoiceNo")
                    Dim Lbl04 As Label = FormViewPayReq.FindControl("LabelInvoiceDate")
                    Dim Lbl05 As Label = FormViewPayReq.FindControl("LabelInvoiceValue")
                    Dim Lbl06 As Label = FormViewPayReq.FindControl("LabelCurrency")

                    Drp01.DataBind()
                    Drp02.DataBind()
                    Drp03.DataBind()
                    Drp04.DataBind()
                    Drp05.DataBind()
                    Drp06.DataBind()

                    Lbl01.Text = Drp01.Items(0).ToString
                    Lbl02.Text = Drp02.Items(0).ToString
                    Lbl03.Text = Drp03.Items(0).ToString
                    Lbl04.Text = Format(DateTime.Parse(Drp04.Items(0).ToString), "dd/MM/yyyy")
                    Lbl05.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(Drp05.Items(0).ToString))
                    Lbl06.Text = "  " + Drp06.Items(0).ToString

                    ' it focus on Site Record No
                    Dim TxtSiteRecNo1 As TextBox = FormViewPayReq.FindControl("SiteRecordNoTextBox")
                    TxtSiteRecNo1.Focus()

                    ' it binds dropdownlistFinanceCheck again
                    Dim DropDownListInvoiceIDNotInPendingYet As DropDownList = FormViewPayReq.FindControl("DropDownListInvoiceIDNotInPendingYet")

                    Dim TextBoxContractReference As TextBox = FormViewPayReq.FindControl("TextBoxContractReference")
                    Dim LabelContractReference As Label = FormViewPayReq.FindControl("LabelContractReference")
                    If DropDownPrjj0.SelectedValue = 108 Then
                        TextBoxContractReference.Visible = True
                        LabelContractReference.Visible = True
                    Else
                        TextBoxContractReference.Visible = False
                        LabelContractReference.Visible = False
                    End If

                    RefreshSiteRecHelper()

                    ' Show or Hide Invoice PDF control
                    If PTS.CoreTables.CreateDataReader.Create_Table1_Project(DrpProjectPrevious.SelectedValue).PR_Coverpage_Approval_Compulsory = True _
                        And InvoicePDFTables.GetCountOfInvoicePDF(DropDownListInvoiceIDNotInPendingYet.SelectedValue) > 0 Then
                        ShowOrHidePDFcontrols(False)
                    ElseIf PTS.CoreTables.CreateDataReader.Create_Table1_Project(DrpProjectPrevious.SelectedValue).PR_Coverpage_Approval_Compulsory = False Then
                        ShowOrHidePDFcontrols(True)
                    End If

                    ShowHideHelperInvDetails()

                End If
            End If
        End If

        ' it provides TODAY to payreqdate textboxes
        If Not IsPostBack Then
            Dim TxtPayReqDateHolder As TextBox = FormViewPayReq.FindControl("PayReqDateTextBoxHolder")
            Dim TxtPayReqDate As TextBox = FormViewPayReq.FindControl("TextBoxPayReqDate")

            TxtPayReqDateHolder.Text = Mid(TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time")).ToString(), 1, 10).ToString()
            TxtPayReqDate.Text = Format(DateTime.Parse(TxtPayReqDateHolder.Text), "MM/dd/yyyy")
        End If

        ' It provides Select Project to combobox if not is postback and if previous page is nothing
        If Not IsPostBack Then
            If PreviousPage Is Nothing Then
                Dim DropDownPrjj As DropDownList = FormViewPayReq.FindControl("DropDownListProject")
                Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                    con.Open()
                    Dim sqlstring As String = " SELECT * FROM ( " +
                    "                         SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID))  " +
                    "                                               AS ProjectName, dbo.Table_Approval_UserRolePrjectJunction.UserName " +
                    "                         FROM         dbo.Table1_Project INNER JOIN " +
                    "                                               dbo.Table_Approval_UserRolePrjectJunction ON dbo.Table1_Project.ProjectID = dbo.Table_Approval_UserRolePrjectJunction.ProjectID " +
                    "                         WHERE     (dbo.Table1_Project.CurrentStatus = 1) AND (dbo.Table_Approval_UserRolePrjectJunction.UserName = @UserName) AND  " +
                    "                                               (dbo.Table_Approval_UserRolePrjectJunction.RoleName = N'InitiateContractAndAddendum') " +
                    "  " +
                    "                         UNION ALL " +
                    "  " +
                    "                         SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID))  " +
                    "                                               AS ProjectName, dbo.aspnet_Users.UserName " +
                    "                         FROM         dbo.Table1_Project INNER JOIN " +
                    "                                               dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN " +
                    "                                               dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId " +
                    "                         WHERE     (dbo.Table1_Project.CurrentStatus = 1) AND (dbo.aspnet_Users.UserName = @UserName) AND (dbo.Table1_Project.NewGeneration = 0) " +
                    "                         ) AS Source " +
                    "                         ORDER BY ProjectName "
                    Dim cmd As New SqlCommand(sqlstring, con)

                    cmd.CommandType = System.Data.CommandType.Text

                    Dim UserParm As SqlParameter = cmd.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar, 256)
                    UserParm.Value = Page.User.Identity.Name

                    Dim dr As SqlDataReader = cmd.ExecuteReader

                    DropDownPrjj.DataSource = dr
                    DropDownPrjj.DataTextField = "ProjectName"
                    DropDownPrjj.DataValueField = "ProjectID"
                    DropDownPrjj.DataBind()
                    con.Close()
                    con.Dispose()
                    dr.Close()

                End Using

                Dim lst As New ListItem("Select Project", "0")
                DropDownPrjj.Items.Insert(0, lst)

                DropDownPrjj.Focus()

                ShowOrHidePDFcontrols(True)

            End If
        End If

        ' It provides Select Project to combobox if not is postback and if previous page is not invoicedefine.aspx
        If Not IsPostBack Then
            If Not PreviousPage Is Nothing Then

                Dim previousPageURLl As String = Request.UrlReferrer.AbsolutePath.ToString
                Dim PageFileInfoo As New System.IO.FileInfo(previousPageURLl)
                If PageFileInfoo.Name <> "invoicedefine.aspx" Then

                    Dim DropDownPrjjj As DropDownList = FormViewPayReq.FindControl("DropDownListProject")
                    Using conn As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                        conn.Open()
                        Dim sqlstringg As String = "SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) as ProjectName, dbo.aspnet_Users.UserName FROM         dbo.Table1_Project INNER JOIN  dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN  dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId WHERE  (Table1_Project.CurrentStatus = 1) AND   (dbo.aspnet_Users.UserName = @UserName) ORDER BY dbo.Table1_Project.ProjectName"
                        Dim cmdd As New SqlCommand(sqlstringg, conn)

                        cmdd.CommandType = System.Data.CommandType.Text

                        Dim UserParm As SqlParameter = cmdd.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar, 256)
                        UserParm.Value = Page.User.Identity.Name

                        Dim dr As SqlDataReader = cmdd.ExecuteReader

                        DropDownPrjjj.DataSource = dr
                        DropDownPrjjj.DataTextField = "ProjectName"
                        DropDownPrjjj.DataValueField = "ProjectID"
                        DropDownPrjjj.DataBind()

                        conn.Close()
                        conn.Dispose()
                        dr.Close()

                    End Using


                    Dim lstt As New ListItem("Select Project", "0")
                    DropDownPrjjj.Items.Insert(0, lstt)

                    DropDownPrjjj.Focus()
                End If
            End If
        End If

        ' it disables insert button
        If Not IsPostBack Then
            Dim InsBut As LinkButton = FormViewPayReq.FindControl("InsertButton")
            ' disabled after validation controls
            'InsBut.Enabled = False
        End If


        'It removes Select Project from combobox
        If IsPostBack Then
            Dim DropDownPrji As DropDownList = FormViewPayReq.FindControl("DropDownListProject")
            Dim controll1 As New String(Page.Request.Params.Get("__EVENTTARGET"))

            If (Not controll1 Is Nothing) Or (controll1 <> "") Then
                If controll1 = "ctl00$MainContent$FormViewPayReq$DropDownListProject" Then
                    If DropDownPrji.Items(0).ToString = "Select Project" Then
                        DropDownPrji.Items.RemoveAt(0)
                    End If
                End If
            End If
        End If

        ' If postback is coming from DropDownListInvoiceIDNotInPendingYet, then it removes Select Invoice statement from first row
        If IsPostBack Then
            Dim DrDwnLstInvoiceIDNotInPendingYet As DropDownList = FormViewPayReq.FindControl("DropDownListInvoiceIDNotInPendingYet")
            Dim controll2 As New String(Page.Request.Params.Get("__EVENTTARGET"))

            If (Not controll2 Is Nothing) Or (controll2 <> "") Then
                If controll2 = "ctl00$MainContent$FormViewPayReq$DropDownListInvoiceIDNotInPendingYet" Then
                    If DrDwnLstInvoiceIDNotInPendingYet.Items(0).ToString = "Select Invoice | Select Invoice | Select Invoice | Select Invoice | Select Invoice | " Then
                        DrDwnLstInvoiceIDNotInPendingYet.Items.RemoveAt(0)
                    End If
                End If
            End If
        End If

        If IsPostBack Then
            Dim ddl_prj As DropDownList = FormViewPayReq.FindControl("DropDownListProject")
            ' Show or Hide Invoice PDF control
            Dim DropDownListInvoiceIDNotInPendingYet_R As DropDownList = FormViewPayReq.FindControl("DropDownListInvoiceIDNotInPendingYet")
            If PTS.CoreTables.CreateDataReader.Create_Table1_Project(ddl_prj.SelectedValue).PR_Coverpage_Approval_Compulsory = True _
                And InvoicePDFTables.GetCountOfInvoicePDF(DropDownListInvoiceIDNotInPendingYet_R.SelectedValue) > 0 Then
                ShowOrHidePDFcontrols(False)
            ElseIf PTS.CoreTables.CreateDataReader.Create_Table1_Project(ddl_prj.SelectedValue).PR_Coverpage_Approval_Compulsory = False Then
                ShowOrHidePDFcontrols(True)
            End If


        End If

        Dim ddl_prj_R As DropDownList = FormViewPayReq.FindControl("DropDownListProject")
        Dim DropDownListInvoiceIDNotInPendingYet_R_R As DropDownList = FormViewPayReq.FindControl("DropDownListInvoiceIDNotInPendingYet")

        ActivateButtonSendRequestToPM(ddl_prj_R.SelectedValue)
        ChekcifPMrequestSentearlier(DropDownListInvoiceIDNotInPendingYet_R_R.SelectedValue, ddl_prj_R.SelectedValue)

    End Sub

    Protected Sub ShowHideHelperInvDetails()
        Dim ddlPrj As DropDownList = FormViewPayReq.FindControl("DropDownListProject")
        If ddlPrj.SelectedValue <> 0 Then
            Dim labelsitereqnoHelper As Label = FormViewPayReq.FindControl("labelsitereqnoHelper")
            labelsitereqnoHelper.Visible = True

            Dim LabelInvoiceValue As Label = FormViewPayReq.FindControl("LabelInvoiceValue")
            If IsNumeric(LabelInvoiceValue.Text) Then
                Dim Div_InvoiceDetails As HtmlGenericControl = FormViewPayReq.FindControl("Div_InvoiceDetails")
                Div_InvoiceDetails.Visible = True
            End If
        End If

    End Sub

    Protected Sub ShowReportViewer()
        ' Provide parameter for ReportViewer
        Dim DDL As DropDownList = FormViewPayReq.FindControl("DropDownListInvoiceIDNotInPendingYet")
        If DDL.SelectedValue <> 0 And (GetDiff_To_Pay(getPONo(DDL.SelectedValue))) > 0 Then
            RenderReport.Render("html", ReportViewerFollowUpReportBySupplierExcVAT, "PObreakdownByPo",
                                  "PO_No", getPONo(DDL.SelectedValue))
        Else
            ReportViewerFollowUpReportBySupplierExcVAT.Visible = False
        End If
    End Sub

    Protected Sub ButtonUploadInvoice_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FileToUpload As FileUpload = FormViewPayReq.FindControl("FileUploadInvoice")
        Dim DropProject As DropDownList = FormViewPayReq.FindControl("DropDownListProject")
        Dim TxtInvoiceID As TextBox = FormViewPayReq.FindControl("InvoiceIDTextBox")
        Dim LabelInfo As Label = FormViewPayReq.FindControl("LabelInfoInvoice")
        Dim TextLink As TextBox = FormViewPayReq.FindControl("TextBoxLinkToInvoice")
        Dim ModalPopupExtender1 As ModalPopupExtender = FormViewPayReq.FindControl("ModalPopupExtender1")
        Dim panEdit As Panel = FormViewPayReq.FindControl("panEdit")

        If FileToUpload.HasFile Then
            If System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) <> ".pdf" Then
                LabelInfo.ForeColor = System.Drawing.Color.Red
                LabelInfo.Text = "Please select PDF format file"
                TextLink.Text = ""
            Else
                If FileToUpload.PostedFile.ContentLength / 1000 > 1500 Then
                    ModalPopupExtender1.Show()
                    panEdit.CssClass = "PanelWarning"
                Else
                    If Directory.Exists(Server.MapPath("~/REQUEST/") + DropProject.SelectedItem.ToString + "/") Then
                        Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                        FileToUpload.SaveAs(MapPath("~/REQUEST/" + DropProject.SelectedItem.ToString + "/" + TxtInvoiceID.Text + UniqueString1 + ".pdf"))
                        TextLink.Text = "~/REQUEST/" + DropProject.SelectedItem.ToString + "/" + TxtInvoiceID.Text + UniqueString1 + ".pdf"
                        LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
                        LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
                    Else
                        Dim UniqueString2 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                        Directory.CreateDirectory(Server.MapPath("~/REQUEST/") + DropProject.SelectedItem.ToString)
                        FileToUpload.SaveAs(MapPath("~/REQUEST/" + DropProject.SelectedItem.ToString + "/" + TxtInvoiceID.Text + UniqueString2 + ".pdf"))
                        TextLink.Text = "~/REQUEST/" + DropProject.SelectedItem.ToString + "/" + TxtInvoiceID.Text + UniqueString2 + ".pdf"
                        LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
                        LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
                    End If
                End If
            End If
        Else
            TextLink.Text = ""
            LabelInfo.ForeColor = System.Drawing.Color.Red
            LabelInfo.Text = "you did not specify any file"
        End If



        ' It reloads files
        'Dim path As String = Server.MapPath("~/REQUEST/AON/" + "somethingnothing" + ".xls")
        'Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)

        '        If file.Exists Then
        'Response.Clear()
        'Response.AddHeader("Content-Disposition", "attachment; filename=" & File.Name)
        'Response.AddHeader("Content-Length", File.Length.ToString())
        'Response.ContentType = "application/octet-stream"
        'Response.WriteFile(File.FullName)
        'Response.End()
        'Else
        'Response.Write("This file does not exist.")

        '        End If



        ' If FileToUpload.HasFile Then



        '            If Directory.Exists(Server.MapPath("~/REQUEST/AON/")) Then
        'Directory.CreateDirectory(Server.MapPath("~/REQUEST/").ToString + "BBB")

        'Directory.Delete(Server.MapPath("~/REQUEST/AON/"))

        'End If

        '************
        'If Directory.Exists(Server.MapPath("~/REQUEST/AON/").ToString) Then
        'LabelInfo.Text = "YEES"
        'Else
        'LabelInfo.Text = "NO"
        'End If


        '************


        'If (FileToUpload.PostedFile.ContentLength / 1000) > 4000 Then
        '            LabelInfo.Text = "more than 800KB - "
        'Else
        'If FileToUpload.PostedFile.ContentType = "application/pdf" Then
        'FileToUpload.SaveAs(MapPath("~/REQUEST/AON/" + "somethingnothing" + ".pdf"))

        'LabelInfo.Text = Path.GetExtension(MapPath("~/REQUEST/AON/" + "somethingnothing" + ".pdf").ToString)

        'LabelInfo.Text = path.GetExtension(FileToUpload.PostedFile.FileName)

        'End If
        'End If
        'Else
        'LabelInfo.Text = "You have not specified a file."
        '        End If

    End Sub

    Protected Sub DropDownListProject_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim TextBoxContractReference As TextBox = FormViewPayReq.FindControl("TextBoxContractReference")
        Dim LabelContractReference As Label = FormViewPayReq.FindControl("LabelContractReference")
        Dim DropDownPrjj0 As DropDownList = sender
        If DropDownPrjj0.SelectedValue = 108 Then
            TextBoxContractReference.Visible = True
            LabelContractReference.Visible = True
        Else
            TextBoxContractReference.Visible = False
            LabelContractReference.Visible = False
        End If


        ' it rejects Marina from TNK-BP to Access Denied Page
        Dim DropDownListProject As DropDownList = FormViewPayReq.FindControl("DropDownListProject")

        If Page.User.Identity.Name.ToString = "marina" OrElse Page.User.Identity.Name.ToString = "n.komleva" AndAlso DropDownListProject.SelectedValue = 123 Then
            Response.Redirect("~/webforms/AccessDenied.aspx")
        End If

        'It runs requery for DropDownListInvoiceIDNotInPendingYet
        Dim DrpDwnListInvoiceIDNotInPendingYet As DropDownList = FormViewPayReq.FindControl("DropDownListInvoiceIDNotInPendingYet")
        DrpDwnListInvoiceIDNotInPendingYet.DataBind()
        DrpDwnListInvoiceIDNotInPendingYet.Focus()

        ' It resets Invoice ID 
        Dim TxtInvcID As TextBox = FormViewPayReq.FindControl("InvoiceIDTextBox")
        TxtInvcID.Text = ""

        ' It resets blue labels 
        Dim Lbl01 As Label = FormViewPayReq.FindControl("LabelSupplierName")
        Dim Lbl02 As Label = FormViewPayReq.FindControl("LabelDescription")
        Dim Lbl03 As Label = FormViewPayReq.FindControl("LabelInvoiceNo")
        Dim Lbl04 As Label = FormViewPayReq.FindControl("LabelInvoiceDate")
        Dim Lbl05 As Label = FormViewPayReq.FindControl("LabelInvoiceValue")
        Dim Lbl06 As Label = FormViewPayReq.FindControl("LabelCurrency")

        Lbl01.Text = ""
        Lbl02.Text = ""
        Lbl03.Text = ""
        Lbl04.Text = ""
        Lbl05.Text = ""
        Lbl06.Text = ""

        'It resets other items
        Dim Xxx01 As TextBox = FormViewPayReq.FindControl("SiteRecordNoTextBox")
        Dim Xxx03 As Label = FormViewPayReq.FindControl("LabelInfoInvoice")

        Dim Xxx05 As TextBox = FormViewPayReq.FindControl("InvoiceIDTextBox")
        Dim Xxx06 As TextBox = FormViewPayReq.FindControl("TextBoxLinkToInvoice")



        Xxx01.Text = ""
        Xxx03.Text = ""

        Xxx05.Text = ""
        Xxx06.Text = ""

        RefreshSiteRecHelper()

        ' Check visibility of Button Send Request to PM
        'ActivateButtonSendRequestToPM(sender.SelectedValue)

        ShowHideHelperInvDetails()

    End Sub

    Protected Sub ActivateButtonSendRequestToPM(ByVal _ProjectID As Integer)

        ' Check visibility of Button Send Request to PM
        Dim ButtonSendRequestToPM As Button = FormViewPayReq.FindControl("ButtonSendRequestToPM")
        ButtonSendRequestToPM.Text = "Send Request To PM"
        ButtonSendRequestToPM.Enabled = True

        If PTS.CoreTables.CreateDataReader.Create_Table1_Project(_ProjectID).PR_Coverpage_Approval_Allowed = True Then
            ButtonSendRequestToPM.Visible = True
        Else
            ButtonSendRequestToPM.Visible = False
        End If

    End Sub

    Protected Sub DropDownListInvoiceIDNotInPendingYet_DataBound(ByVal sender As Object, ByVal e As System.EventArgs)
        ' It resets "DropDownListInvoiceIDNotInPendingYet" after DropDownListProject fires
        Dim DrpDwnListInvoiceIDNotInPendingYet As DropDownList = FormViewPayReq.FindControl("DropDownListInvoiceIDNotInPendingYet")
        Dim controll As New String(Page.Request.Params.Get("__EVENTTARGET"))
        If (Not controll Is Nothing) Or (controll <> "") Then
            If controll = "ctl00$MainContent$FormViewPayReq$DropDownListProject" Then
                Dim lst As New ListItem("Select Invoice | Select Invoice | Select Invoice | Select Invoice | Select Invoice | ", "0")
                DrpDwnListInvoiceIDNotInPendingYet.Items.Insert(0, lst)
            End If
        End If

        'If previous page is nothing and if it is not postback then it resets DropDownListInvoiceIDNotInPendingYet 
        If Not IsPostBack Then
            If PreviousPage Is Nothing Then
                Dim DrpDwnListInvoiceIDNotInPendingYet1 As DropDownList = FormViewPayReq.FindControl("DropDownListInvoiceIDNotInPendingYet")
                Dim lst As New ListItem("Select Invoice | Select Invoice | Select Invoice | Select Invoice | Select Invoice | ", "0")
                DrpDwnListInvoiceIDNotInPendingYet1.Items.Insert(0, lst)
            End If
        End If

        'If not postback and if it is coming from previous page and not from invoicedefine.aspx then it resets DropDownListInvoiceIDNotInPendingYet 
        If Not IsPostBack Then
            If Not PreviousPage Is Nothing Then
                Dim previousPageURL As String = Request.UrlReferrer.AbsolutePath.ToString
                Dim PageFileInfo As New System.IO.FileInfo(previousPageURL)
                If PageFileInfo.Name <> "invoicedefine.aspx" Then
                    Dim DrpDwnListInvoiceIDNotInPendingYet1 As DropDownList = FormViewPayReq.FindControl("DropDownListInvoiceIDNotInPendingYet")
                    Dim lst As New ListItem("Select Invoice | Select Invoice | Select Invoice | Select Invoice | Select Invoice | ", "0")
                    DrpDwnListInvoiceIDNotInPendingYet1.Items.Insert(0, lst)
                End If
            End If
        End If


    End Sub

    Protected Function getPONo(ByVal _InvoiceID As Integer) As String
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT RTRIM(PO_No) FROM Table3_Invoice WHERE InvoiceID = @InvoiceID "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim InvoiceID As SqlParameter = cmd.Parameters.Add("@InvoiceID", System.Data.SqlDbType.Int)
            InvoiceID.Value = _InvoiceID
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim _return As String = ""
            While dr.Read

                _return = dr(0).ToString

            End While

            Return _return

            con.Close()
            con.Dispose()
            dr.Close()

        End Using

    End Function

    Protected Function GetDiff_To_Pay(ByVal _PO_No As String) As Decimal
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "POpaymentsMoreThanCollected"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim PO_No As SqlParameter = cmd.Parameters.Add("@PO_No", System.Data.SqlDbType.NVarChar)
            PO_No.Value = _PO_No
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim _return As Decimal = 0.0
            While dr.Read

                If IsDBNull(dr(0)) Then
                    _return = 0.0
                Else
                    _return = dr(0)
                End If

            End While

            Return _return

            con.Close()
            con.Dispose()
            dr.Close()

        End Using

    End Function

    Protected Sub DropDownListInvoiceIDNotInPendingYet_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Drp1 As DropDownList = FormViewPayReq.FindControl("DropDownListSupplierName")
        Dim Drp2 As DropDownList = FormViewPayReq.FindControl("DropDownListDescription")
        Dim Drp3 As DropDownList = FormViewPayReq.FindControl("DropDownListInvoiceNo")
        Dim Drp4 As DropDownList = FormViewPayReq.FindControl("DropDownListInvoiceDate")
        Dim Drp5 As DropDownList = FormViewPayReq.FindControl("DropDownListInvoiceValue")
        Dim Drp6 As DropDownList = FormViewPayReq.FindControl("DropDownListCurrency")
        Dim DropDownListProject As DropDownList = FormViewPayReq.FindControl("DropDownListProject")

        Dim Lbl1 As Label = FormViewPayReq.FindControl("LabelSupplierName")
        Dim Lbl2 As Label = FormViewPayReq.FindControl("LabelDescription")
        Dim Lbl3 As Label = FormViewPayReq.FindControl("LabelInvoiceNo")
        Dim Lbl4 As Label = FormViewPayReq.FindControl("LabelInvoiceDate")
        Dim Lbl5 As Label = FormViewPayReq.FindControl("LabelInvoiceValue")
        Dim Lbl6 As Label = FormViewPayReq.FindControl("LabelCurrency")

        Drp1.DataBind()
        Drp2.DataBind()
        Drp3.DataBind()
        Drp4.DataBind()
        Drp5.DataBind()
        Drp6.DataBind()

        Lbl1.Text = Drp1.Items(0).ToString
        Lbl2.Text = Drp2.Items(0).ToString
        Lbl3.Text = Drp3.Items(0).ToString
        Lbl4.Text = Format(DateTime.Parse(Drp4.Items(0).ToString), "dd/MM/yyyy")
        Lbl5.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(Drp5.Items(0).ToString))
        Lbl6.Text = "  " + Drp6.Items(0).ToString

        ' It defines new selected Invoice ID 
        Dim TxtInvcID As TextBox = FormViewPayReq.FindControl("InvoiceIDTextBox")
        Dim DrpNotInPendingYet As DropDownList = FormViewPayReq.FindControl("DropDownListInvoiceIDNotInPendingYet")
        TxtInvcID.Text = DrpNotInPendingYet.SelectedValue.ToString

        'It resets other items
        Dim Xx01 As TextBox = FormViewPayReq.FindControl("SiteRecordNoTextBox")
        Dim Xx03 As Label = FormViewPayReq.FindControl("LabelInfoInvoice")

        Dim Xx05 As TextBox = FormViewPayReq.FindControl("TextBoxLinkToInvoice")



        Xx01.Text = ""
        Xx03.Text = ""

        Xx05.Text = ""


        ' it focus on Site Record No
        Dim TxtSiteRecNo As TextBox = FormViewPayReq.FindControl("SiteRecordNoTextBox")
        TxtSiteRecNo.Focus()

        ' refresh navigateURL for hyperlink cover page
        Dim HyperLinkProduceCoverPage As HyperLink = FormViewPayReq.FindControl("HyperLinkProduceCoverPage")
        Dim DropDownListInvoiceIDNotInPendingYet As DropDownList = FormViewPayReq.FindControl("DropDownListInvoiceIDNotInPendingYet")
        Dim PRN As String = ""
        Dim PRNtextBox As TextBox = FormViewPayReq.FindControl("SiteRecordNoTextBox")
        If String.IsNullOrEmpty(PRNtextBox.Text) Then
            PRN = "?"
        Else
            PRN = PRNtextBox.Text
        End If
        HyperLinkProduceCoverPage.NavigateUrl = "~/webforms/PRN_coverpage.aspx?InvoiceID=" + DropDownListInvoiceIDNotInPendingYet.SelectedValue.ToString + "&PRN=" + PRN

        'ChekcifPMrequestSentearlier(sender.selectedvalue, DropDownListProject.SelectedValue)

        ' Show or Hide Invoice PDF control
        If PTS.CoreTables.CreateDataReader.Create_Table1_Project(DropDownListProject.SelectedValue).PR_Coverpage_Approval_Compulsory = True _
            And InvoicePDFTables.GetCountOfInvoicePDF(DrpNotInPendingYet.SelectedValue) > 0 Then
            ShowOrHidePDFcontrols(False)
        ElseIf PTS.CoreTables.CreateDataReader.Create_Table1_Project(DropDownListProject.SelectedValue).PR_Coverpage_Approval_Compulsory = False Then
            ShowOrHidePDFcontrols(True)
        End If

        ShowHideHelperInvDetails()

    End Sub

    Protected Sub ShowOrHidePDFcontrols(ByVal _Show As Boolean)

        Dim LabelAttachRequest As Label = FormViewPayReq.FindControl("LabelAttachRequest")
        Dim FileUploadInvoice As FileUpload = FormViewPayReq.FindControl("FileUploadInvoice")
        Dim ButtonUploadInvoice As Button = FormViewPayReq.FindControl("ButtonUploadInvoice")
        Dim RequiredFieldValidator1 As RequiredFieldValidator = FormViewPayReq.FindControl("RequiredFieldValidator1")
        Dim LabelPaymentTerm As Label = FormViewPayReq.FindControl("LabelPaymentTerm")
        Dim DropDownListPaymentTerm As DropDownList = FormViewPayReq.FindControl("DropDownListPaymentTerm")
        Dim HyperLinkProduceCoverPage As HyperLink = FormViewPayReq.FindControl("HyperLinkProduceCoverPage")
        Dim LinkButtonCoverPage As LinkButton = FormViewPayReq.FindControl("LinkButtonCoverPage")
        Dim InsertButton As LinkButton = FormViewPayReq.FindControl("InsertButton")
        Dim SpanWarning As HtmlGenericControl = FormViewPayReq.FindControl("SpanWarning")


        If _Show = True Then
            LabelAttachRequest.Visible = True
            FileUploadInvoice.Visible = True
            ButtonUploadInvoice.Visible = True
            RequiredFieldValidator1.Visible = True
            LabelPaymentTerm.Visible = True
            DropDownListPaymentTerm.Visible = True
            HyperLinkProduceCoverPage.Visible = True

            Dim ddlPrj As DropDownList = FormViewPayReq.FindControl("DropDownListProject")
            If ddlPrj.SelectedValue <> 0 Then
                Dim DropDownListInvoiceIDNotInPendingYet As DropDownList = FormViewPayReq.FindControl("DropDownListInvoiceIDNotInPendingYet")
                If DropDownListInvoiceIDNotInPendingYet.SelectedIndex > 0 Then
                    If _Show = True Then
                        LinkButtonCoverPage.Visible = True
                    End If
                End If
            End If

            InsertButton.Visible = True
            SpanWarning.Visible = False

        ElseIf _Show = False Then
            LabelAttachRequest.Visible = False
            FileUploadInvoice.Visible = False
            ButtonUploadInvoice.Visible = False
            RequiredFieldValidator1.Visible = False
            LabelPaymentTerm.Visible = False
            DropDownListPaymentTerm.Visible = False
            HyperLinkProduceCoverPage.Visible = False
            InsertButton.Visible = False
            SpanWarning.Visible = True

        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        ' it checks if all items are inserted successfully then insert button activated 
        If IsPostBack Then
            Dim X01 As TextBox = FormViewPayReq.FindControl("InvoiceIDTextBox")
            Dim X02 As TextBox = FormViewPayReq.FindControl("SiteRecordNoTextBox")
            Dim X03 As TextBox = FormViewPayReq.FindControl("TextBoxPayReqDate")
            Dim X04 As TextBox = FormViewPayReq.FindControl("TextBoxLinkToInvoice")
            Dim InsBut1 As LinkButton = FormViewPayReq.FindControl("InsertButton")

            If (X01.Text <> "") And (X02.Text <> "") And (X03.Text <> "") And (X04.Text <> "") Then
                ' disabled after validation control
                'InsBut1.Enabled = True
                'InsBut1.Focus()
            Else
                'disabled after validation control
                'InsBut1.Enabled = False
            End If
        End If

        ShowReportViewer()

    End Sub

    Protected Sub InsertButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ' it defines UpdatedBy parameter as DateTime.Now
        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        SqlDataSourcePayReq.InsertParameters("CreatedBy").DefaultValue = result.ToString()
        SqlDataSourcePayReq.InsertParameters("CreatedBy").Type = TypeCode.DateTime

        SqlDataSourcePayReq.InsertParameters("PersonCreated").DefaultValue = Page.User.Identity.Name.ToString
        SqlDataSourcePayReq.InsertParameters("PersonCreated").Type = TypeCode.String

    End Sub

    Protected Sub SqlDataSourcePayReq_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourcePayReq.Inserted
        Response.Redirect("~/webforms/Default.aspx")
    End Sub

    Protected Sub FormViewPayReq_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertedEventArgs) Handles FormViewPayReq.ItemInserted

        ' Find PAyReqNo
        Dim PayReqNoForTemporary As Integer
        Using conPayReqNo1 As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            Dim InvoiceIDTextBox As TextBox = FormViewPayReq.FindControl("InvoiceIDTextBox")
            conPayReqNo1.Open()
            Dim sqlstringPayReqNo1 As String = " SELECT dbo.Table4_PaymentRequest.PayReqNo " +
        "                        FROM  dbo.Table4_PaymentRequest " +
        "                        WHERE (dbo.Table4_PaymentRequest.InvoiceID = " + InvoiceIDTextBox.Text.ToString + ") "
            Dim cmdPayReqNo1 As New SqlCommand(sqlstringPayReqNo1, conPayReqNo1)
            cmdPayReqNo1.CommandType = System.Data.CommandType.Text
            Dim drPayReqNo1 As SqlDataReader = cmdPayReqNo1.ExecuteReader
            While drPayReqNo1.Read
                PayReqNoForTemporary = Convert.ToInt32(drPayReqNo1(0).ToString)
            End While
            conPayReqNo1.Close()
            conPayReqNo1.Dispose()
            drPayReqNo1.Close()

        End Using

        ' Take delivery details into Table_Delivery as DeliveryDate, DeliveryValue based on PayReqNo
        Using cn77 As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            Dim cmd77 As New System.Data.SqlClient.SqlCommand()
            cmd77.Connection = cn77
            cmd77.CommandText = " INSERT INTO [Table5_PayLog_Temporary] " +
                              "            ([PayReqNo]) " +
                              "      VALUES " +
                              "            (" + PayReqNoForTemporary.ToString + ") "
            cmd77.CommandType = System.Data.CommandType.Text
            cn77.Open()
            cmd77.ExecuteNonQuery()
            cn77.Close()
            cn77.Dispose()
        End Using

    End Sub

    Protected Sub FormViewPayReq_ItemInserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertEventArgs) Handles FormViewPayReq.ItemInserting

        Dim TextBoxContractReference As TextBox = FormViewPayReq.FindControl("TextBoxContractReference")
        If Not String.IsNullOrEmpty(TextBoxContractReference.Text.ToString) Then
            e.Values("ContractReference") = LTrim(RTrim(TextBoxContractReference.Text.ToString))
        Else
            e.Values("ContractReference") = Nothing
        End If

        Dim PayReqDateTextBoxHolder As TextBox = FormViewPayReq.FindControl("PayReqDateTextBoxHolder")

        e.Values("PayReqDate") = Convert.ToDateTime(Mid(PayReqDateTextBoxHolder.Text.ToString, 1, 2).ToString + "/" + Mid(PayReqDateTextBoxHolder.Text.ToString, 4, 2).ToString + "/" + Mid(PayReqDateTextBoxHolder.Text.ToString, 7, 4).ToString)

        e.Values("ActivityCode") = String.Empty

        e.Values("Approved") = "Not Approved"
        e.Values("PersonApprove") = Nothing
        e.Values("LastAction") = Nothing

        ' this block will identify PaymentTerm date
        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        Dim ListEmpty As String = "-"
        Dim List_NextTuesday As String = "Next Tuesday"
        Dim List_NextThursday As String = "Next Thursday"
        Dim List_Today As String = "TODAY"
        Dim List_Tomorrow As String = "TOMORROW"
        Dim List_ThisThurday As String = "This Thursday"

        Dim DropDownListPaymentTerm As DropDownList = FormViewPayReq.FindControl("DropDownListPaymentTerm")

        If result.DayOfWeek.ToString = "Friday" Then
            If DropDownListPaymentTerm.SelectedValue = ListEmpty Then
                e.Values("PaymentTerm") = DBNull.Value
            ElseIf DropDownListPaymentTerm.SelectedValue = List_NextTuesday Then
                result = result.AddDays(4)
                e.Values("PaymentTerm") = Convert.ToDateTime(Mid(result.ToString, 1, 2).ToString + "/" + Mid(result.ToString, 4, 2).ToString + "/" + Mid(result.ToString, 7, 4).ToString)
            ElseIf DropDownListPaymentTerm.SelectedValue = List_NextThursday Then
                result = result.AddDays(6)
                e.Values("PaymentTerm") = Convert.ToDateTime(Mid(result.ToString, 1, 2).ToString + "/" + Mid(result.ToString, 4, 2).ToString + "/" + Mid(result.ToString, 7, 4).ToString)
            End If
        ElseIf result.DayOfWeek.ToString = "Saturday" Then
            If DropDownListPaymentTerm.SelectedValue = ListEmpty Then
                e.Values("PaymentTerm") = DBNull.Value
            ElseIf DropDownListPaymentTerm.SelectedValue = List_NextTuesday Then
                result = result.AddDays(3)
                e.Values("PaymentTerm") = Convert.ToDateTime(Mid(result.ToString, 1, 2).ToString + "/" + Mid(result.ToString, 4, 2).ToString + "/" + Mid(result.ToString, 7, 4).ToString)
            ElseIf DropDownListPaymentTerm.SelectedValue = List_NextThursday Then
                result = result.AddDays(5)
                e.Values("PaymentTerm") = Convert.ToDateTime(Mid(result.ToString, 1, 2).ToString + "/" + Mid(result.ToString, 4, 2).ToString + "/" + Mid(result.ToString, 7, 4).ToString)
            End If
        ElseIf result.DayOfWeek.ToString = "Sunday" Then
            If DropDownListPaymentTerm.SelectedValue = ListEmpty Then
                e.Values("PaymentTerm") = DBNull.Value
            ElseIf DropDownListPaymentTerm.SelectedValue = List_NextTuesday Then
                result = result.AddDays(2)
                e.Values("PaymentTerm") = Convert.ToDateTime(Mid(result.ToString, 1, 2).ToString + "/" + Mid(result.ToString, 4, 2).ToString + "/" + Mid(result.ToString, 7, 4).ToString)
            ElseIf DropDownListPaymentTerm.SelectedValue = List_NextThursday Then
                result = result.AddDays(4)
                e.Values("PaymentTerm") = Convert.ToDateTime(Mid(result.ToString, 1, 2).ToString + "/" + Mid(result.ToString, 4, 2).ToString + "/" + Mid(result.ToString, 7, 4).ToString)
            End If
        ElseIf result.DayOfWeek.ToString = "Monday" Then
            If DropDownListPaymentTerm.SelectedValue = ListEmpty Then
                e.Values("PaymentTerm") = DBNull.Value
            ElseIf DropDownListPaymentTerm.SelectedValue = List_Tomorrow Then
                result = result.AddDays(1)
                e.Values("PaymentTerm") = Convert.ToDateTime(Mid(result.ToString, 1, 2).ToString + "/" + Mid(result.ToString, 4, 2).ToString + "/" + Mid(result.ToString, 7, 4).ToString)
            ElseIf DropDownListPaymentTerm.SelectedValue = List_ThisThurday Then
                result = result.AddDays(3)
                e.Values("PaymentTerm") = Convert.ToDateTime(Mid(result.ToString, 1, 2).ToString + "/" + Mid(result.ToString, 4, 2).ToString + "/" + Mid(result.ToString, 7, 4).ToString)
            ElseIf DropDownListPaymentTerm.SelectedValue = List_NextTuesday Then
                result = result.AddDays(8)
                e.Values("PaymentTerm") = Convert.ToDateTime(Mid(result.ToString, 1, 2).ToString + "/" + Mid(result.ToString, 4, 2).ToString + "/" + Mid(result.ToString, 7, 4).ToString)
            ElseIf DropDownListPaymentTerm.SelectedValue = List_NextThursday Then
                result = result.AddDays(10)
                e.Values("PaymentTerm") = Convert.ToDateTime(Mid(result.ToString, 1, 2).ToString + "/" + Mid(result.ToString, 4, 2).ToString + "/" + Mid(result.ToString, 7, 4).ToString)
            End If
        ElseIf result.DayOfWeek.ToString = "Tuesday" Then
            If DropDownListPaymentTerm.SelectedValue = ListEmpty Then
                e.Values("PaymentTerm") = DBNull.Value
            ElseIf DropDownListPaymentTerm.SelectedValue = List_Today Then
                result = result
                e.Values("PaymentTerm") = Convert.ToDateTime(Mid(result.ToString, 1, 2).ToString + "/" + Mid(result.ToString, 4, 2).ToString + "/" + Mid(result.ToString, 7, 4).ToString)
            ElseIf DropDownListPaymentTerm.SelectedValue = List_ThisThurday Then
                result = result.AddDays(2)
                e.Values("PaymentTerm") = Convert.ToDateTime(Mid(result.ToString, 1, 2).ToString + "/" + Mid(result.ToString, 4, 2).ToString + "/" + Mid(result.ToString, 7, 4).ToString)
            ElseIf DropDownListPaymentTerm.SelectedValue = List_NextTuesday Then
                result = result.AddDays(7)
                e.Values("PaymentTerm") = Convert.ToDateTime(Mid(result.ToString, 1, 2).ToString + "/" + Mid(result.ToString, 4, 2).ToString + "/" + Mid(result.ToString, 7, 4).ToString)
            ElseIf DropDownListPaymentTerm.SelectedValue = List_NextThursday Then
                result = result.AddDays(9)
                e.Values("PaymentTerm") = Convert.ToDateTime(Mid(result.ToString, 1, 2).ToString + "/" + Mid(result.ToString, 4, 2).ToString + "/" + Mid(result.ToString, 7, 4).ToString)
            End If
        ElseIf result.DayOfWeek.ToString = "Wednesday" Then
            If DropDownListPaymentTerm.SelectedValue = ListEmpty Then
                e.Values("PaymentTerm") = DBNull.Value
            ElseIf DropDownListPaymentTerm.SelectedValue = List_Tomorrow Then
                result = result.AddDays(1)
                e.Values("PaymentTerm") = Convert.ToDateTime(Mid(result.ToString, 1, 2).ToString + "/" + Mid(result.ToString, 4, 2).ToString + "/" + Mid(result.ToString, 7, 4).ToString)
            ElseIf DropDownListPaymentTerm.SelectedValue = List_NextTuesday Then
                result = result.AddDays(6)
                e.Values("PaymentTerm") = Convert.ToDateTime(Mid(result.ToString, 1, 2).ToString + "/" + Mid(result.ToString, 4, 2).ToString + "/" + Mid(result.ToString, 7, 4).ToString)
            ElseIf DropDownListPaymentTerm.SelectedValue = List_NextThursday Then
                result = result.AddDays(8)
                e.Values("PaymentTerm") = Convert.ToDateTime(Mid(result.ToString, 1, 2).ToString + "/" + Mid(result.ToString, 4, 2).ToString + "/" + Mid(result.ToString, 7, 4).ToString)
            End If
        ElseIf result.DayOfWeek.ToString = "Thursday" Then
            If DropDownListPaymentTerm.SelectedValue = ListEmpty Then
                e.Values("PaymentTerm") = DBNull.Value
            ElseIf DropDownListPaymentTerm.SelectedValue = List_Today Then
                result = result
                e.Values("PaymentTerm") = Convert.ToDateTime(Mid(result.ToString, 1, 2).ToString + "/" + Mid(result.ToString, 4, 2).ToString + "/" + Mid(result.ToString, 7, 4).ToString)
            ElseIf DropDownListPaymentTerm.SelectedValue = List_NextTuesday Then
                result = result.AddDays(5)
                e.Values("PaymentTerm") = Convert.ToDateTime(Mid(result.ToString, 1, 2).ToString + "/" + Mid(result.ToString, 4, 2).ToString + "/" + Mid(result.ToString, 7, 4).ToString)
            ElseIf DropDownListPaymentTerm.SelectedValue = List_NextThursday Then
                result = result.AddDays(7)
                e.Values("PaymentTerm") = Convert.ToDateTime(Mid(result.ToString, 1, 2).ToString + "/" + Mid(result.ToString, 4, 2).ToString + "/" + Mid(result.ToString, 7, 4).ToString)
            End If
        End If
        ' /_ this block will identify PaymentTerm date

        ' THIS BLOCK FOR SENDING EMAIL
        ' ***************************************
        ' ACTIVATE LATER


        ' send notification email

        ' THIS PART MAY BE ACTIVATED LATER. IT SEND EMAIL TO ONLY SERGEI AND OTHERS
        '        ' Send e-mail to persons who will approve request.
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "SELECT     RTRIM(dbo.aspnet_Membership.Email) AS Email " +
      " FROM         dbo.aspnet_Membership INNER JOIN " +
      "                       dbo.aspnet_Users ON dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId INNER JOIN " +
      "                       dbo.Table_Prj_User_Junction ON dbo.aspnet_Users.UserId = dbo.Table_Prj_User_Junction.UserID INNER JOIN " +
      "                       dbo.Table1_Project ON dbo.Table_Prj_User_Junction.ProjectID = dbo.Table1_Project.ProjectID " +
      " WHERE     (RTRIM(dbo.Table1_Project.ProjectID) = @ProjectID) AND (dbo.aspnet_Users.SendEmailPaymentReq = 1)  "

            Dim cmd As New SqlCommand(sqlstring, con)

            cmd.CommandType = System.Data.CommandType.Text

            Dim ProjectN As SqlParameter = cmd.Parameters.Add("@ProjectID", System.Data.SqlDbType.Int)
            Dim DropDownPrj As DropDownList = FormViewPayReq.FindControl("DropDownListProject")

            ProjectN.Value = DropDownPrj.SelectedValue
            Dim dr As SqlDataReader = cmd.ExecuteReader
            ' __________THIS PART MAY BE ACTIVATED LATER. IT SEND EMAIL TO ONLY SERGEI AND OTHERS

            '(1) Create the MailMessage instance
            Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "Mercury Russia Procurement")
            Dim mm1 As New MailMessage()
            mm1.From = MailFrom


            ' THIS PART MAY BE ACTIVATED LATER. IT SEND EMAIL TO ONLY SERGEI AND OTHERS
            While dr.Read
                If dr(0).ToString.Length <> 0 Then
                    mm1.Bcc.Add(dr(0).ToString)
                End If
            End While
            con.Close()
            con.Dispose()
            dr.Close()
            ' __________THIS PART MAY BE ACTIVATED LATER. IT SEND EMAIL TO ONLY SERGEI AND OTHERS

            ' THIS PART MAY BE REMOVED LATER IF WE NEED TO SEND NOT ONLY TO SERGEI AND OTHERS

            'mm1.To.Add("savas.karaduman@mercuryeng.ru")
            'mm1.To.Add("sergei.borisov@mercuryeng.ru")
            'mm1.To.Add("kirill.golubin@mercuryeng.ru")
            'mm1.To.Add("pavel.silantiev@mercuryeng.ru")
            'mm1.CC.Add("jim.collender@mercuryeng.ru")

            ' ______THIS PART MAY BE REMOVED LATER IF WE NEED TO SEND NOT ONLY TO SERGEI AND OTHERS



            Dim LabelSupplierName As Label = FormViewPayReq.FindControl("LabelSupplierName")
            Dim LabelInvoiceNo As Label = FormViewPayReq.FindControl("LabelInvoiceNo")
            Dim LabelInvoiceDate As Label = FormViewPayReq.FindControl("LabelInvoiceDate")
            Dim LabelDescription As Label = FormViewPayReq.FindControl("LabelDescription")
            Dim LabelInvoiceValue As Label = FormViewPayReq.FindControl("LabelInvoiceValue")
            Dim LabelCurrency As Label = FormViewPayReq.FindControl("LabelCurrency")

            '(2) Assign the MailMessage's properties
            Dim urg As String = ""
            If e.Values("Urgency").ToString = "Urgent" Then
                urg = "Urgent ! "
            Else
                urg = ""
            End If


            Dim ErrorMsg As String = ""

            ErrorMsg = urg + "Новый запрос платежа от " + Page.User.Identity.Name.ToString + "| проект: " + DropDownPrj.SelectedItem.ToString +
                                  "| поставщик : " + Trim(LabelSupplierName.Text.ToString) +
                                  "| Номер счета : " + Trim(LabelInvoiceNo.Text.ToString) +
                                  "| Дата счета : " + Trim(LabelInvoiceDate.Text.ToString) +
                                  "| Описание : " + Trim(LabelDescription.Text.ToString)

            mm1.Subject = ErrorMsg.Replace(Environment.NewLine, " ")

            ' update Notification table
            Dim SendNotification As New MyCommonTasks
            Dim DropDownPrjj0 As DropDownList = FormViewPayReq.FindControl("DropDownListProject")
            SendNotification.SendNotification(ErrorMsg, DropDownPrjj0.SelectedValue.ToString, 1)

            mm1.Body = "<div style=" + """" + "color: #FF0000; font-weight: bold" + """" + "> Это автоматическая электронная почта. Пожалуйста, не отвечайте </div>" +
        " <hr />" +
        "    <table style=" + """" + "background-color: #C0C0C0; font-family: tahoma; font-size: medium;" + """" + " > " +
        "        <tr> " +
        "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
        "                Новый запрос платежа от</td> " +
        "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
        "                " + Page.User.Identity.Name.ToString + "</td> " +
        "        </tr> " +
        "        <tr> " +
        "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
        "                проект:</td> " +
        "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
        "                " + DropDownPrj.SelectedItem.ToString + "</td> " +
        "        </tr> " +
        "        <tr> " +
        "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
        "                поставщик :</td> " +
        "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
        "                " + Trim(LabelSupplierName.Text.ToString) + "</td> " +
        "        </tr> " +
        "        <tr> " +
        "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
        "                Номер счета :</td> " +
        "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
        "                " + Trim(LabelInvoiceNo.Text.ToString) + "</td> " +
        "        </tr> " +
        "        <tr> " +
        "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
        "                Дата счета :</td> " +
        "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
        "                " + Trim(LabelInvoiceDate.Text.ToString) + "</td> " +
        "        </tr> " +
        "        <tr> " +
        "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
        "                Описание :</td> " +
        "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
        "                " + Trim(LabelDescription.Text.ToString) + " </td> " +
        "        </tr> " +
        "        <tr> " +
        "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
        "                Запись сайта Номер :</td> " +
        "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
        "                " + Trim(e.Values("SiteRecordNo").ToString) + "</td> " +
        "        </tr> " +
        "        <tr> " +
        "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
        "                Стоимость счета без НДС :</td> " +
        "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
        "                " + LabelInvoiceValue.Text.ToString + "</td> " +
        "        </tr> " +
        "        <tr> " +
        "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
        "                валюта :</td> " +
        "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
        "                " + Trim(LabelCurrency.Text.ToString) + "</td> " +
        "        </tr> " +
        "        <tr> " +
        "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
        "                Срочность :</td> " +
        "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
        "                " + Trim(e.Values("Urgency").ToString) + "</td> " +
        "        </tr> " +
        "    </table> "

            mm1.IsBodyHtml = True

            ' add attachment if exist
            Dim path As String = Server.MapPath(e.Values("LinkToInvoice").ToString)
            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
            If file.Exists Then
                ' JUST ACTIVATE IF REQUIRED
                'mm1.Attachments.Add(New Attachment(Server.MapPath(e.Values("LinkToInvoice").ToString)))
            End If

            '(3) Create the SmtpClient object
            Dim smtp As New SmtpClient_RussianEncoded

            '(4) Send the MailMessage (will use the Web.config settings)
            ' JUST ACTIVATE IF REQUIRED
            Dim LabelSupplierNameII As Label = FormViewPayReq.FindControl("LabelSupplierName")

            If LabelSupplierNameII.Text = "MERCURY, OOO" Then
            Else

                'Try
                smtp.Send(mm1)
                'Catch ex As Exception

                'End Try

            End If

            '***************************************
            '______________________________

        End Using


    End Sub

    Protected Sub FormViewPayReq_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormViewPayReq.Load

    End Sub

    Protected Sub FormViewPayReq_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormViewPayReq.PreRender

    End Sub

    Protected Sub DropDownListYear_DataBound(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DropDownListYear As DropDownList = FormViewPayReq.FindControl("DropDownListYear")
        If Not IsPostBack Then
            DropDownListYear.SelectedValue = DateTime.Now.Year.ToString
        End If

        ' if postback coming from DropDownListInvoiceIDNotInPendingYet, then Select Current Year
        Dim DrpDwnListInvoiceIDNotInPendingYet As DropDownList = FormViewPayReq.FindControl("DropDownListInvoiceIDNotInPendingYet")
        Dim controll As New String(Page.Request.Params.Get("__EVENTTARGET"))
        If (Not controll Is Nothing) Or (controll <> "") Then
            If controll = "ctl00$MainContent$FormViewPayReq$DropDownListInvoiceIDNotInPendingYet" Then
                DropDownListYear.SelectedValue = DateTime.Now.Year.ToString
            End If
        End If

    End Sub

    Protected Sub DropDownListPaymentTerm_DataBound(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DropDownListPaymentTerm As DropDownList = sender

        Dim ListEmpty As New ListItem("-", "-")
        Dim List_NextTuesday As New ListItem("Next Tuesday", "Next Tuesday")
        Dim List_NextThursday As New ListItem("Next Thursday", "Next Thursday")
        Dim List_Today As New ListItem("TODAY", "TODAY")
        Dim List_Tomorrow As New ListItem("TOMORROW", "TOMORROW")
        Dim List_ThisThurday As New ListItem("This Thursday", "This Thursday")

        Dim zoneId2Ageing As String = "Russian Standard Time"
        Dim tzi2Ageing As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId2Ageing)
        Dim result2Ageing As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi2Ageing)
        If result2Ageing.DayOfWeek.ToString = "Friday" OrElse result2Ageing.DayOfWeek.ToString = "Saturday" OrElse result2Ageing.DayOfWeek.ToString = "Sunday" Then
            DropDownListPaymentTerm.Items.Insert(0, ListEmpty)
            DropDownListPaymentTerm.Items.Insert(1, List_NextTuesday)
            DropDownListPaymentTerm.Items.Insert(2, List_NextThursday)
        ElseIf result2Ageing.DayOfWeek.ToString = "Monday" Then
            DropDownListPaymentTerm.Items.Insert(0, ListEmpty)
            DropDownListPaymentTerm.Items.Insert(1, List_Tomorrow)
            DropDownListPaymentTerm.Items.Insert(2, List_ThisThurday)
            DropDownListPaymentTerm.Items.Insert(3, List_NextTuesday)
            DropDownListPaymentTerm.Items.Insert(4, List_NextThursday)
        ElseIf result2Ageing.DayOfWeek.ToString = "Tuesday" Then
            DropDownListPaymentTerm.Items.Insert(0, ListEmpty)
            DropDownListPaymentTerm.Items.Insert(1, List_Today)
            DropDownListPaymentTerm.Items.Insert(2, List_ThisThurday)
            DropDownListPaymentTerm.Items.Insert(3, List_NextTuesday)
            DropDownListPaymentTerm.Items.Insert(4, List_NextThursday)
        ElseIf result2Ageing.DayOfWeek.ToString = "Wednesday" Then
            DropDownListPaymentTerm.Items.Insert(0, ListEmpty)
            DropDownListPaymentTerm.Items.Insert(1, List_Tomorrow)
            DropDownListPaymentTerm.Items.Insert(2, List_NextTuesday)
            DropDownListPaymentTerm.Items.Insert(3, List_NextThursday)
        ElseIf result2Ageing.DayOfWeek.ToString = "Thursday" Then
            DropDownListPaymentTerm.Items.Insert(0, ListEmpty)
            DropDownListPaymentTerm.Items.Insert(1, List_Today)
            DropDownListPaymentTerm.Items.Insert(2, List_NextTuesday)
            DropDownListPaymentTerm.Items.Insert(3, List_NextThursday)
        End If
    End Sub

    Protected Sub LinkButtonCoverPage_Click(sender As Object, e As System.EventArgs)
        ' refresh navigateURL for hyperlink cover page
        Dim DropDownListInvoiceIDNotInPendingYet As DropDownList = FormViewPayReq.FindControl("DropDownListInvoiceIDNotInPendingYet")
        Dim PRN As String = ""
        Dim PRNtextBox As TextBox = FormViewPayReq.FindControl("SiteRecordNoTextBox")
        If String.IsNullOrEmpty(PRNtextBox.Text) Then
            PRN = "?"
        Else
            PRN = PRNtextBox.Text
        End If
        Response.Write("<script type='text/javascript'>window.open('PRN_coverpage.aspx?InvoiceID=" + DropDownListInvoiceIDNotInPendingYet.SelectedValue.ToString + "&PRN=" + PRN + "','_blank');</script>")
    End Sub

    Protected Sub DropDownListProject_Load(sender As Object, e As EventArgs)

    End Sub

    Protected Sub DropDownListProject_PreRender(sender As Object, e As EventArgs)

    End Sub

    Protected Sub RefreshSiteRecHelper()

        ' Feed Site Record No helper
        Dim _dl As DropDownList = FormViewPayReq.FindControl("DropDownListProject")
        SqlDataSourceSiteReqNoHelper.SelectParameters("ProjectID").DefaultValue = _dl.SelectedValue

        'Dim SiteRecordNoTextBox As TextBox = FormViewPayReq.FindControl("SiteRecordNoTextBox")
        'Dim GridViewSiteReqNoHelper As GridView = FormViewPayReq.FindControl("GridViewSiteReqNoHelper")
        'GridViewSiteReqNoHelper.DataBind()
        'SiteRecordNoTextBox.Text = GridViewSiteReqNoHelper.Rows(0).Cells(0).Text

    End Sub

    Protected Sub ChekcifPMrequestSentearlier(ByVal _InvoiceID As Integer, ByVal _ProjectID As Integer)
        Dim ButtonSendRequestToPM As Button = FormViewPayReq.FindControl("ButtonSendRequestToPM")
        Using Adapter As New MercuryTableAdapters.Table3_Invoice_PRrequestToPMTableAdapter
            If Adapter.GetCountOfInvoiceID(_InvoiceID) = 1 Then
                ButtonSendRequestToPM.Enabled = False
                ButtonSendRequestToPM.Text = "Request Already Sent"
            Else
                ButtonSendRequestToPM.Enabled = True
                ButtonSendRequestToPM.Text = "Send Request To PM"
            End If
            Adapter.Dispose()
        End Using

        ' Stop user to continue clicking INSERT unless PM approve

        Dim InsertButton As LinkButton = FormViewPayReq.FindControl("InsertButton")

        Using Adapter As New MercuryTableAdapters.Table3_Invoice_PRrequestToPMTableAdapter
            If PTS.CoreTables.CreateDataReader.Create_Table1_Project(_ProjectID).PR_Coverpage_Approval_Compulsory = True Then
                If Adapter.GetCountOfApprovedInvoiceID(_InvoiceID) = 1 Then
                    ' enable INSERT
                    InsertButton.Enabled = True
                    InsertButton.Text = "Insert"
                Else
                    ' disable INSERT
                    InsertButton.Enabled = False
                    InsertButton.Text = "PM not approved yet"
                End If

            Else
                ' enable INSERT
                InsertButton.Enabled = True
                InsertButton.Text = "Insert"
            End If

        End Using



    End Sub

    Protected Sub ButtonSendRequestToPM_Click(sender As Object, e As EventArgs)

        ' show skip box only to admin
        Dim cbx_skippm As CheckBox = FormViewPayReq.FindControl("cbx_skippm")

        ' validate and send request to PM
        Dim DropDownListInvoiceIDNotInPendingYet As DropDownList = FormViewPayReq.FindControl("DropDownListInvoiceIDNotInPendingYet")
        Dim SiteRecordNoTextBox As TextBox = FormViewPayReq.FindControl("SiteRecordNoTextBox")
        Dim PayReqDateTextBoxHolder As TextBox = FormViewPayReq.FindControl("PayReqDateTextBoxHolder")

        Using adapter As New MercuryTableAdapters.Table3_Invoice_PRrequestToPMTableAdapter

            If adapter.GetCountOfInvoiceID(DropDownListInvoiceIDNotInPendingYet.SelectedValue) = 0 Then
                ' no request sent yet
                adapter.Insert(DropDownListInvoiceIDNotInPendingYet.SelectedValue,
                               SiteRecordNoTextBox.Text,
                                Page.User.Identity.Name.ToLower,
                                 0,
                                  Nothing,
                                   Nothing,
                                   Convert.ToDateTime(Mid(PayReqDateTextBoxHolder.Text.ToString, 1, 2).ToString + "/" + Mid(PayReqDateTextBoxHolder.Text.ToString, 4, 2).ToString + "/" + Mid(PayReqDateTextBoxHolder.Text.ToString, 7, 4).ToString))

                If adapter.GetCountOfInvoiceID(DropDownListInvoiceIDNotInPendingYet.SelectedValue) > 0 Then
                    If cbx_skippm.Checked = False Then
                        ' insert succesfull
                        ' Send email notification to PM
                        Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "Mercury Russia Procurement")
                        Dim mm1 As New MailMessage()
                        mm1.From = MailFrom
                        Dim DropDownListProject As DropDownList = FormViewPayReq.FindControl("DropDownListProject")
                        ' add TO
                        PTS_MERCURY.helper.View_PaymentRequestApprovalStatus.GetEmailsByInvoiceId(DropDownListInvoiceIDNotInPendingYet.SelectedValue, mm1)
                        mm1.CC.Add(GetRequesterEmail(DropDownListInvoiceIDNotInPendingYet.SelectedValue))
                        mm1.Bcc.Add("sk@mercuryeng.ru")

                        mm1.Subject = GetSubject(DropDownListInvoiceIDNotInPendingYet.SelectedValue)
                        mm1.Body = GetBody(DropDownListInvoiceIDNotInPendingYet.SelectedValue)
                        mm1.IsBodyHtml = True

                        Dim smtp As New SmtpClient_RussianEncoded
                        smtp.Send(mm1)
                    End If
                End If
            Else

            End If
            adapter.Dispose()
        End Using
        Dim DropDownListProject2 As DropDownList = FormViewPayReq.FindControl("DropDownListProject")
        'ChekcifPMrequestSentearlier(DropDownListInvoiceIDNotInPendingYet.SelectedValue, DropDownListProject2.SelectedValue)

        Response.Redirect("~/webforms/default.aspx")

    End Sub

    Protected Function GetPMEmail(ByVal _ProjectID As Integer) As String

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String =
            " SELECT     RTRIM(dbo.aspnet_Membership.LoweredEmail) AS LoweredEmail " +
            " FROM         dbo.aspnet_Membership INNER JOIN " +
            "                       dbo.aspnet_Users ON dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId AND dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId INNER JOIN " +
            "                       dbo.Table1_Project ON dbo.aspnet_Users.UserName = dbo.Table1_Project.ProjectManager " +
            " WHERE     (dbo.Table1_Project.ProjectID = @ProjectID) "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim ProjectID As SqlParameter = cmd.Parameters.Add("@ProjectID", System.Data.SqlDbType.Int)
            ProjectID.Value = _ProjectID
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                Return dr(0)
            End While
            con.Close()
            dr.Close()
        End Using
    End Function

    Protected Function GetRequesterEmail(ByVal _invoiceid As Integer) As String

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String =
            " SELECT     RTRIM(dbo.aspnet_Membership.LoweredEmail) AS LoweredEmail " +
            " FROM         dbo.aspnet_Users INNER JOIN " +
            "                       dbo.aspnet_Membership ON dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId AND dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId INNER JOIN " +
            "                       dbo.Table3_Invoice ON dbo.aspnet_Users.UserName = dbo.Table3_Invoice.PersonCreated " +
            " WHERE     (dbo.Table3_Invoice.InvoiceID = @InvoiceId) "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim ProjectID As SqlParameter = cmd.Parameters.Add("@InvoiceId", System.Data.SqlDbType.Int)
            ProjectID.Value = _invoiceid
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                Return dr(0)
            End While
            con.Close()
            dr.Close()
        End Using
    End Function

    Protected Function GetSubject(ByVal _InvoiceID As Integer) As String

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT     RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName, RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName, RTRIM(dbo.Table3_Invoice.Invoice_No)  " +
                "                       AS Invoice_No " +
                " FROM         dbo.Table2_PONo INNER JOIN " +
                "                       dbo.Table1_Project ON dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID INNER JOIN " +
                "                       dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN " +
                "                       dbo.Table3_Invoice_PRrequestToPM ON dbo.Table3_Invoice.InvoiceID = dbo.Table3_Invoice_PRrequestToPM.InvoiceID INNER JOIN " +
                "                       dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID " +
                " WHERE     (dbo.Table3_Invoice_PRrequestToPM.InvoiceID = @InvoiceID) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim InvoiceID As SqlParameter = cmd.Parameters.Add("@InvoiceID", System.Data.SqlDbType.Int)
            InvoiceID.Value = _InvoiceID
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                Return "Запрошенная оплата | " + dr(0) + " | " + dr(1) + " | " + dr(2) + " |"
            End While
            con.Close()
            dr.Close()
        End Using

    End Function

    Protected Function GetBody(ByVal _invoiceid As Integer) As String

        Dim _return As String = ""

        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            Dim Requester = (From I In db.Table3_Invoice Where I.InvoiceID = _invoiceid Select New With {I.PersonCreated}).ToList()(0).PersonCreated.Trim

            Dim PM_Fullname = PTS_MERCURY.helper.View_PaymentRequestApprovalStatus.GetUserNamessByInvoiceId(_invoiceid)
            Dim Requester_FullName = (From C In db.View_GetFullUserNameFromUserName Where C.UserName = Requester Select New With {C.UserNameSurnameFromEmail}).ToList()(0).UserNameSurnameFromEmail

            _return = " Здравствуйте " + PM_Fullname + ", " + "<br/><br/>" + _
            " " + Requester_FullName + " Просил вас утвердить этот запрос платежа." + "<br/>" + _
            " Нажмите следующую ссылку.. " + "<br/>" + _
            "  " + "<br/>" + _
            " С уважением  " + "<br/>" + _
            " ПТС " + _
            " <hr/> " + _
            "<a href=" + """" + "http://pts.mercuryeng.ru/webforms/PR_PMapproval.aspx?invoiceid=" + _invoiceid.ToString + """" + " target=" + """" + "_blank" + """" + ">ПОЖАЛУЙСТА, НАЖМИТЕ ДЛЯ СТРАНИЦЫ ОФИЦИАЛЬНОГО УТВЕРЖДЕНИЯ</a> "

        End Using

        Return _return

    End Function

    Protected Sub GridViewSiteReqNoHelper_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewSiteReqNoHelper.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim LabelSiteRecordNo = DirectCast(e.Row.FindControl("LabelSiteRecordNo"), Label)

            If LabelSiteRecordNo IsNot Nothing Then
                LabelSiteRecordNo.Attributes.Add("data-siterecordno", DataBinder.Eval(e.Row.DataItem, "SiteRecordNo").ToString.Trim)
            End If

        End If

    End Sub

End Class

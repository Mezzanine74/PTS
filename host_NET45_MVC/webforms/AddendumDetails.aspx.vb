
Imports PTS_App_Code_VB_Project
Imports PTS_App_Code_VB_Project.PTS.CoreTables

Partial Class AddendumDetails2
    Inherits System.Web.UI.Page

    Dim _addendumId As Integer = 0

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init

        _addendumId = PTS_MERCURY.helper.Garbage.GetQueryString(PTS_MERCURY.helper.Garbage.QueryStringParameter.AddendumId)

    End Sub

    Protected Sub GridviewApprovalStatusAddendum_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)

        '    ' This class is being shared by ContractViewPage and Contract-Addendum details page
        ContractView.GridviewApprovalStatusAddendum_RowDataBound(e.Row)

    End Sub

    Protected Sub GridviewApprovalStatusAddendum_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        ' This class is being shared by ContractViewPage and Contract-Addendum details page
        ContractView.GridviewApprovalStatusAddendum_RowCommand(sender, e, POdetailsForEmail, Page, WebUserControl_AddendumEmailBody)

        GridviewApprovalStatusAddendum.DataBind()

    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Exit Sub
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim LabelCreatedBy As String = ""
        Dim LabelPersonCreated As String = ""
        Dim LabelUpdatedBy As String = ""
        Dim LabelPersonUpdated As String = ""

        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            If (Aggregate C In db.Table_Addendums Where C.AddendumID = _addendumId Into Count()) > 0 Then

                Dim _Addendum = (From C In db.Table_Addendums Where C.AddendumID = _addendumId).ToList()(0)
                If _Addendum.CreatedBy.HasValue Then
                    LabelCreatedBy = _Addendum.CreatedBy
                Else
                    LabelCreatedBy = ""
                End If

                LabelPersonCreated = _Addendum.PersonCreated

                If _Addendum.UpdatedBy.HasValue Then
                    LabelUpdatedBy = _Addendum.UpdatedBy
                Else
                    LabelUpdatedBy = ""
                End If

                LabelPersonUpdated = _Addendum.PersonUpdated
            End If

            db.Dispose()

        End Using

        If LabelHooverToolTipContract IsNot Nothing Then
            If LabelCreatedBy <> "" AndAlso LabelUpdatedBy = "" Then
                LabelHooverToolTipContract.Text = "________" + LabelPersonCreated + " created by " + LabelCreatedBy
            End If
        End If

        If LabelHooverToolTipContract IsNot Nothing Then
            If LabelCreatedBy <> "" AndAlso LabelUpdatedBy <> "" Then
                LabelHooverToolTipContract.Text = "________" + LabelPersonCreated + " created by " + LabelCreatedBy + ", the latest update: " + LabelPersonUpdated + " by " + LabelUpdatedBy
            End If
        End If

        If LabelHooverToolTipContract IsNot Nothing Then
            If LabelCreatedBy = "" AndAlso LabelUpdatedBy = "" Then
                LabelHooverToolTipContract.Text = "________" + "Exported from Excel file"
            End If
        End If

        If LabelHooverToolTipContract IsNot Nothing Then
            If LabelCreatedBy = "" AndAlso LabelUpdatedBy <> "" Then
                LabelHooverToolTipContract.Text = "________" + "Exported from Excel file" + ", the latest update: " + LabelPersonUpdated + " by " + LabelUpdatedBy
            End If
        End If

        Dim sqls As SqlDataSource = WebUserControl_AddendumEmailBody.FindControl("SqlDataSourceAddendumsEmailBody")
        Dim FormViewAddendumsEmailBody As FormView = WebUserControl_AddendumEmailBody.FindControl("FormViewAddendumsEmailBody")


        sqls.SelectParameters("AddendumID").DefaultValue = _addendumId

        ' add here REPLACE ADDENDUM logic
        Dim _AddendumID_ As Integer = _addendumId
        If PTS.CoreTables.CreateDataReader.Create_Table_Contract(PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_AddendumID_).ContractID).ProjectID <> 999 Then
            If PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_AddendumID_).AddendumTypes = 2 Then
                If PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_AddendumID_).POexecuted = False Then
                    If PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_AddendumID_).AddendumValue_WithVAT < _
                        ReplaceAddendumCheck.GetTotalInvoice_WithVAT_AgainstPO(PTS.CoreTables.CreateDataReader.Create_Table_Contract(PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_AddendumID_).ContractID).PO_No) Then
                        Form.Disabled = True
                        Response.Write("<h1>THIS REPLACE ADDENDUM CANNOT BE PROCEED.<br/>IT EXCEEDS TOTAL INVOICED VALUE UNDER " + _
                            PTS.CoreTables.CreateDataReader.Create_Table_Contract(PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_addendumId).ContractID).PO_No + "</h1>")

                    Else
                        Form.Disabled = False
                    End If
                End If
            Else
                Form.Disabled = False
            End If
        End If

    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete

        ' Show Approval Status
        If IsPostBack Or Not IsPostBack Then
            If PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_addendumId).POexecuted = True And _
                PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_addendumId).Exceptional = False Then
                LabelPOstatus.Visible = True
                LabelPOstatus.Text = "Closed to approval. PO executed."
            ElseIf PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_addendumId).POexecuted = True And _
                PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_addendumId).Exceptional = True Then
                LabelPOstatus.Visible = True
                LabelPOstatus.Text = "PO raised exceptionally."
            Else
                LabelPOstatus.Visible = False
            End If
        End If

        If Not _
          String.IsNullOrEmpty(PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_addendumId).AddendumLinkToPDFcopy) Then

            LiteralPDFheading.Text = LiteralPDFheading.Text + "<br/>" + "<br/>"
            LiteralPDFheading.Visible = True
            HyperLinkPDF.Visible = True

            Dim path As String = "---"
            If CreateDataReader.Create_Table_Addendums(_addendumId).AddendumLinkToPDFcopy.Length > 0 Then
                path = Server.MapPath(CreateDataReader.Create_Table_Addendums(_addendumId).AddendumLinkToPDFcopy)
            End If

            If IO.Directory.Exists(path) Then
                HyperLinkPDF.Visible = True
                HyperLinkPDF.ImageUrl = "~/Images/folder.png"
            End If
        End If

        If Not _
          String.IsNullOrEmpty(PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_addendumId).AddendumLinkToTemplatefile_DOC) Then

            LiteralDOCheading.Text = LiteralDOCheading.Text + "<br/>" + "<br/>"
            LiteralDOCheading.Visible = True
            HyperLinkDOC.Visible = True

            Dim path As String = "---"
            If CreateDataReader.Create_Table_Addendums(_addendumId).AddendumLinkToTemplatefile_DOC.Length > 0 Then
                path = Server.MapPath(CreateDataReader.Create_Table_Addendums(_addendumId).AddendumLinkToTemplatefile_DOC)
            End If

            If IO.Directory.Exists(path) Then
                HyperLinkDOC.Visible = True
                HyperLinkDOC.ImageUrl = "~/Images/folder.png"
            End If

        End If

    End Sub

    Protected Sub HyperLinkDOC_Click(sender As Object, e As ImageClickEventArgs) Handles HyperLinkDOC.Click

        'Dim webFormCommon = New host_NET45_MVC.App_Code.WebFormCommon
        'webFormCommon.processFileManager(Page, panelContainer, CreateDataReader.Create_Table_Addendums(_addendumId).AddendumLinkToTemplatefile_DOC, 0)
        ASPxFileManager1.Visible = True
        ASPxFileManager1.Settings.RootFolder = CreateDataReader.Create_Table_Addendums(_addendumId).AddendumLinkToTemplatefile_DOC
        ASPxFileManager1.SettingsUpload.Enabled = False
        ASPxFileManager1.SettingsEditing.AllowDelete = False
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)

    End Sub

    Protected Sub HyperLinkPDF_Click(sender As Object, e As ImageClickEventArgs) Handles HyperLinkPDF.Click

        'Dim webFormCommon = New host_NET45_MVC.App_Code.WebFormCommon
        'webFormCommon.processFileManager(Page, panelContainer, CreateDataReader.Create_Table_Addendums(_addendumId).AddendumLinkToPDFcopy, 0)
        ASPxFileManager1.Visible = True
        ASPxFileManager1.Settings.RootFolder = CreateDataReader.Create_Table_Addendums(_addendumId).AddendumLinkToPDFcopy
        ASPxFileManager1.SettingsUpload.Enabled = False
        ASPxFileManager1.SettingsEditing.AllowDelete = False
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)


    End Sub
End Class

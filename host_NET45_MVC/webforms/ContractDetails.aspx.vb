Imports PTS_App_Code_VB_Project.PTS.CoreTables
Imports System.Data.SqlClient

Partial Class ContractDetails2
    Inherits System.Web.UI.Page

    Dim _contractid As Integer = 0

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init

        _contractid = PTS_MERCURY.helper.Garbage.GetQueryString(PTS_MERCURY.helper.Garbage.QueryStringParameter.ContractId)

    End Sub

    Protected Sub GridviewApprovalStatus_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)

        ' This class is being shared by ContractViewPage and Contract-Addendum details page
        ContractView.GridviewApprovalStatus_RowDataBound(e.Row)

    End Sub

    Protected Sub GridviewApprovalStatus_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        ' This class is being shared by ContractViewPage and Contract-Addendum details page
        ContractView.GridviewApprovalStatus_RowCommand(sender, e, POdetailsForEmail, Page, WebUserControl_ContractEmailBody)

        GridviewApprovalStatus.DataBind()

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

            If (Aggregate C In db.Table_Contracts Where C.ContractID = _contractid Into Count()) > 0 Then

                Dim _contract = (From C In db.Table_Contracts Where C.ContractID = _contractid).ToList()(0)
                If _contract.CreatedBy.HasValue Then
                    LabelCreatedBy = _contract.CreatedBy
                Else
                    LabelCreatedBy = ""
                End If

                LabelPersonCreated = _contract.PersonCreated

                If _contract.UpdatedBy.HasValue Then
                    LabelUpdatedBy = _contract.UpdatedBy
                Else
                    LabelUpdatedBy = ""
                End If

                LabelPersonUpdated = _contract.PersonUpdated
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

        If Not IsPostBack Then
            Dim sqls As SqlDataSource = WebUserControl_ContractEmailBody.FindControl("SqlDataSourceContractEmailBody")
            Dim FormViewContractEmailBody As FormView = WebUserControl_ContractEmailBody.FindControl("FormViewContractEmailBody")
            sqls.SelectParameters("ContractID").DefaultValue = _contractid

            Dim LabelContractValue As Label = FormViewContractEmailBody.FindControl("TextBoxContractValue_withVAT")
            If CreateDataReader.Create_Table_Contract(_contractid).FrameContract = True Then
                LabelContractValue.Text = "Frame Contract"
                LabelContractValue.ForeColor = System.Drawing.Color.Red
            End If

            'show PanelNominated if required and give required functionality
            If CreateDataReader.Create_Table_Contract(_contractid).Nominated = True _
                AndAlso CreateDataReader.Create_Table_Contract(_contractid).Scenario >= 4 Then
                PanelNominated.Visible = True
                LabelESTM_Approval.Text = "This contract has been defined as Nominated Subcontractor." _
                    + " It requires confirmation from Estimating Manager." _
                    + " Please see status below:"

                DDLestm.SelectedValue = CreateDataReader.Create_Table_Contract(_contractid).NominatedApprovedByESTM

                If CreateDataReader.Create_Table1_Project(CreateDataReader.Create_Table_Contract(_contractid).ProjectID).NewGeneration = True Then
                    ' new generation project
                    If CreateDataReader.Create_Table_Contract(_contractid).POexecuted = False Then
                        ' enable depends on ESTM role
                        If User.IsInRole("ESTM") Then
                            DDLestm.Enabled = True
                        Else
                            DDLestm.Enabled = False
                        End If
                    ElseIf CreateDataReader.Create_Table_Contract(_contractid).Exceptional = True And CreateDataReader.Create_Table_Contract(_contractid).POexecuted = True Then
                        ' enable depends on ESTM role
                        If User.IsInRole("ESTM") Then
                            DDLestm.Enabled = True
                        Else
                            DDLestm.Enabled = False
                        End If
                    ElseIf CreateDataReader.Create_Table_Contract(_contractid).Exceptional = False And CreateDataReader.Create_Table_Contract(_contractid).POexecuted = True Then
                        ' disable free from ESTM role
                        DDLestm.Enabled = False
                    End If
                End If
            Else
                PanelNominated.Visible = False
            End If
        End If

    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete

        ' Show Approval Status
        If CreateDataReader.Create_Table_Contract(_contractid).POexecuted = True And _
            CreateDataReader.Create_Table_Contract(_contractid).Exceptional = False Then
            LabelPOstatus.Visible = True
            LabelPOstatus.Text = "Closed to approval. PO executed."
        ElseIf CreateDataReader.Create_Table_Contract(_contractid).POexecuted = True And _
         CreateDataReader.Create_Table_Contract(_contractid).Exceptional = True Then
            LabelPOstatus.Visible = True
            LabelPOstatus.Text = "PO raised exceptionally"
        Else
            LabelPOstatus.Visible = False
        End If

        If IsPostBack Or Not IsPostBack Then
            If Not _
              String.IsNullOrEmpty(CreateDataReader.Create_Table_Contract(_contractid).LinkToPDFcopy) Then
                'LiteralPDFheading.Text = LiteralPDFheading.Text + "<br/>" + "<br/>"
                LiteralPDFheading.Visible = True
                HyperLinkPDF.Visible = True

                Dim path As String = "---"
                If CreateDataReader.Create_Table_Contract(_contractid).LinkToPDFcopy.Length > 0 Then
                    path = Server.MapPath(CreateDataReader.Create_Table_Contract(_contractid).LinkToPDFcopy)
                End If

                If IO.Directory.Exists(path) Then
                    HyperLinkPDF.Visible = True
                    HyperLinkPDF.ImageUrl = "~/Images/folder.png"
                End If

            End If
        End If

        If IsPostBack Or Not IsPostBack Then
            If Not _
              String.IsNullOrEmpty(CreateDataReader.Create_Table_Contract(_contractid).LinkToTemplatefile_DOC) Then

                'LiteralDOCheading.Text = LiteralDOCheading.Text + "<br/>" + "<br/>"
                LiteralDOCheading.Visible = True
                HyperLinkDOC.Visible = True

                Dim path As String = "---"
                If CreateDataReader.Create_Table_Contract(_contractid).LinkToTemplatefile_DOC.Length > 0 Then
                    path = Server.MapPath(CreateDataReader.Create_Table_Contract(_contractid).LinkToTemplatefile_DOC)
                End If

                If IO.Directory.Exists(path) Then
                    HyperLinkDOC.Visible = True
                    HyperLinkDOC.ImageUrl = "~/Images/folder.png"
                End If

            End If
        End If

    End Sub

    Protected Sub GridViewOffers_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If (e.CommandName = "OpenZip") Then
            Dim LinkToFile As String = e.CommandArgument.ToString
            Dim openpdf As New MyCommonTasks
            openpdf.OpenPDF(LinkToFile)
        End If
    End Sub

    Protected Sub GridViewOffers_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        ' enable scriptmanager for postback
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    ScriptManager1.RegisterPostBackControl(DirectCast(e.Row.FindControl("ImageButtonOfferZip"), ImageButton))
        'End If
    End Sub

    Protected Sub ExecuteESTMconfirmation(ByVal _ddl As DropDownList)

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "ExecuteESTMConfirmationContract"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", System.Data.SqlDbType.Int)
            ContractID.Value = Convert.ToInt32(_contractid)

            Dim ConfirmationStatus As SqlParameter = cmd.Parameters.Add("@ConfirmationStatus", System.Data.SqlDbType.Int)
            ConfirmationStatus.Value = _ddl.SelectedValue

            cmd.ExecuteNonQuery()
            con.Close()
        End Using

        ' CHECK CONTRACT IF IT IS READY FOR PO
        ContractView.Initiate_Checking_Contract_Or_Addendum_To_Insert_Or_Update_Po_Then_Finally_Execute( _
                                                Convert.ToInt32(_contractid), _
                                                0, _
                                                POdetailsForEmail)

        GridviewMissingItemsForApproval.DataBind()
        GridviewApprovalStatus.DataBind()

    End Sub

    Protected Sub HyperLinkDOC_Click(sender As Object, e As ImageClickEventArgs) Handles HyperLinkDOC.Click

        'Dim webFormCommon = New host_NET45_MVC.App_Code.WebFormCommon
        'webFormCommon.processFileManager(Page, panelContainer, CreateDataReader.Create_Table_Contract(_contractid).LinkToTemplatefile_DOC, 0)
        ASPxFileManager1.Visible = True
        ASPxFileManager1.Settings.RootFolder = CreateDataReader.Create_Table_Contract(_contractid).LinkToTemplatefile_DOC
        ASPxFileManager1.SettingsUpload.Enabled = False
        ASPxFileManager1.SettingsEditing.AllowDelete = False
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)

    End Sub

    Protected Sub HyperLinkPDF_Click(sender As Object, e As ImageClickEventArgs) Handles HyperLinkPDF.Click

        'Dim webFormCommon = New host_NET45_MVC.App_Code.WebFormCommon
        'webFormCommon.processFileManager(Page, panelContainer, CreateDataReader.Create_Table_Contract(_contractid).LinkToPDFcopy, 0)
        ASPxFileManager1.Visible = True
        ASPxFileManager1.Settings.RootFolder = CreateDataReader.Create_Table_Contract(_contractid).LinkToPDFcopy
        ASPxFileManager1.SettingsUpload.Enabled = False
        ASPxFileManager1.SettingsEditing.AllowDelete = False
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFileManager" + "').modal('show') });", True)

    End Sub

    Protected Sub DDLestm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLestm.SelectedIndexChanged

        If IsPostBack Then
            ExecuteESTMconfirmation(sender)
        End If

    End Sub
End Class

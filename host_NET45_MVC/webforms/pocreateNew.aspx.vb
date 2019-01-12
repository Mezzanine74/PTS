Imports System.Data.SqlClient
Imports System.Net.Mail
Imports AjaxControlToolkit
Imports PTS_App_Code_VB_Project.PTS_MERCURY.helper.EmailGenerator

Partial Class _pocreateAprMx
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

    End Sub

    Protected Sub DropDownListProject_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        ' Bind RequestedBy DDL
        ' Check first requested By required or not
        Dim DDLprojectID_ As DropDownList = sender
        Dim projectID_ As Integer = DDLprojectID_.SelectedValue
        Dim SqlDataSourceRequestedBy As SqlDataSource = FormViewPO.FindControl("SqlDataSourceRequestedBy")
        Dim DropDownListRequestedBy As DropDownList = FormViewPO.FindControl("DropDownListRequestedBy")
        Dim LabelRequestedBy As Label = FormViewPO.FindControl("LabelRequestedBy")
        Dim CompareValidatorRequested As CompareValidator = FormViewPO.FindControl("CompareValidatorRequested")
        If RequestedByRequired(projectID_) = True Then
            'DropDownListRequestedBy.DataBind()
            DropDownListRequestedBy.Visible = True
            LabelRequestedBy.Visible = True
            CompareValidatorRequested.Enabled = True
        Else
            DropDownListRequestedBy.Visible = False
            LabelRequestedBy.Visible = False
            CompareValidatorRequested.Enabled = False
            'DropDownListRequestedBy.DataBind()
        End If

        ProvidePoNo()

    End Sub

    Protected Sub ProvidePoNo()
        Dim DDL_ As DropDownList = FormViewPO.FindControl("DropDownListProject")
        Dim DropDownPOIDMaker As DropDownList = FormViewPO.FindControl("DropDownListPOIdMaker")
        Dim TextBoxPONo As TextBox = FormViewPO.FindControl("PO_NoTextBox")
        Dim TextBoxPrjID As TextBox = FormViewPO.FindControl("Project_ID_TextBox")
        Dim DropDownPrjID As DropDownList = FormViewPO.FindControl("DropDownListProject")
        Dim GrdViewPoSumCk As GridView = FormViewPO.FindControl("GridViewPOsumCheck")

        ' temporarily REMOVE LATER
        If DDL_.SelectedValue = 108 AndAlso Page.User.Identity.Name.ToLower <> "savas" Then
            'FormViewPO.Enabled = False
            'LabelMessageToKsenia.Visible = True
            'Exit Sub
        End If
        ' ___________________________


        DropDownPOIDMaker.DataBind()


        If DropDownPOIDMaker.Items.Count = 0 Then
            TextBoxPONo.Text = String.Empty

        Else
            DropDownPOIDMaker.Items(0).Selected = True
            TextBoxPONo.Text = DropDownPOIDMaker.SelectedValue

        End If

        TextBoxPrjID.Text = DropDownPrjID.SelectedValue

        GrdViewPoSumCk.DataBind()
        Dim PanelWarning As Panel = FormViewPO.FindControl("PanelWarning")
        If GrdViewPoSumCk.Rows.Count > 0 Then
            PanelWarning.Visible = True
        Else
            PanelWarning.Visible = False
        End If

        Dim TextBoxCostCodeError As TextBox = FormViewPO.FindControl("TextBoxCostCodeError")
        TextBoxCostCodeError.Text = "Valid"

    End Sub

    'Protected Function GetProjectIDFromSession(ByVal _s As String) As Integer

    '    Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
    '        con.Open()
    '        Dim sqlstring As String = " SELECT     dbo.Table_Contracts.ProjectID " + _
    '            " FROM         dbo.Table_FrameContractTickets INNER JOIN " + _
    '            "                       dbo.Table_Contracts ON dbo.Table_FrameContractTickets.ContractID = dbo.Table_Contracts.ContractID " + _
    '            " WHERE     (dbo.Table_FrameContractTickets.UniqueText = @Session) "

    '        Dim cmd As New SqlCommand(sqlstring, con)
    '        cmd.CommandType =System.Data.CommandType.Text

    '        'syntax for parameter adding
    '        Dim _Session As SqlParameter = cmd.Parameters.Add("@Session",System.Data.SqlDbType.NVarChar, 100)
    '        _Session.Value = _s
    '        Dim dr As SqlDataReader = cmd.ExecuteReader
    '        While dr.Read
    '            Return dr(0)
    '        End While
    '        con.Close()
    '        dr.Close()
    '    End Using

    'End Function

    Protected Function RequestedByRequired(ByVal projectID_ As Integer) As Boolean
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " select RequestedRequired from Table1_Project where ProjectID = @ProjectID "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            'syntax for parameter adding
            Dim PrjID As SqlParameter = cmd.Parameters.Add("@ProjectID", System.Data.SqlDbType.Int)
            PrjID.Value = projectID_
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

    Protected Function ApprovalRequired(ByVal projectID_ As Integer) As Boolean
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " select ApproveRequired from Table1_Project where ProjectID = @ProjectID "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            'syntax for parameter adding
            Dim PrjID As SqlParameter = cmd.Parameters.Add("@ProjectID", System.Data.SqlDbType.Int)
            PrjID.Value = projectID_
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

    Protected Sub InsertButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        ' start to check if PO value and supplier id dublicating
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT COUNT(ProjectID) AS Count FROM " +
                    " ( " +
                    " SELECT 1 As ProjectID " +
                    "  " +
                    " UNION ALL " +
                    "  " +
                    " SELECT     dbo.Table1_Project.ProjectID AS Count " +
                    " FROM         dbo.Table1_Project INNER JOIN " +
                    "                       dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN " +
                    "                       dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID " +
                    " WHERE     (dbo.Table6_Supplier.SupplierID = @SupplierID) AND (dbo.Table2_PONo.TotalPrice = @TotalPrice)  " +
                    "   AND (dbo.Table2_PONo.PO_Currency = @PO_Currency) AND (dbo.Table1_Project.ProjectID = @ProjectID) " +
                    " )  " +
                    " As DataSource1 "


            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            Dim TotalPrice As SqlParameter = cmd.Parameters.Add("@TotalPrice", System.Data.SqlDbType.Decimal)
            Dim TotalPriceTextBox As TextBox = FormViewPO.FindControl("TotalPriceTextBox")
            TotalPrice.Value = Convert.ToDecimal(TotalPriceTextBox.Text)

            Dim PO_Currency As SqlParameter = cmd.Parameters.Add("@PO_Currency", System.Data.SqlDbType.NVarChar)
            Dim DropDownListCurrency As DropDownList = FormViewPO.FindControl("DropDownListCurrency")
            PO_Currency.Value = Convert.ToString(DropDownListCurrency.SelectedValue)

            Dim ProjectID As SqlParameter = cmd.Parameters.Add("@ProjectID", System.Data.SqlDbType.SmallInt)
            Dim DropDownListProject As DropDownList = FormViewPO.FindControl("DropDownListProject")
            ProjectID.Value = Convert.ToInt32(DropDownListProject.SelectedValue)

            Dim SupplierID As SqlParameter = cmd.Parameters.Add("@SupplierID", System.Data.SqlDbType.NVarChar)
            Dim SupplierIDTextBox As TextBox = FormViewPO.FindControl("SupplierIDTextBox")
            SupplierID.Value = Convert.ToString(SupplierIDTextBox.Text)

            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim PO_NoTextBox As TextBox = FormViewPO.FindControl("PO_NoTextBox")
            While dr.Read
                If dr(0) > 1 Then
                    ' PO might be dublicated!
                    Session(PO_NoTextBox.Text) = "PO might be dublicated!"
                    ' feed all parameter for SqlDataSource
                    Dim SqlDataSourcePoPossibleDublication As SqlDataSource = FormViewPO.FindControl("SqlDataSourcePoPossibleDublication")
                    SqlDataSourcePoPossibleDublication.SelectParameters("Project_ID").DefaultValue = Convert.ToInt32(DropDownListProject.SelectedValue)
                    SqlDataSourcePoPossibleDublication.SelectParameters("TotalPrice").DefaultValue = Convert.ToDecimal(TotalPriceTextBox.Text)
                    SqlDataSourcePoPossibleDublication.SelectParameters("SupplierID").DefaultValue = Convert.ToString(SupplierIDTextBox.Text)
                    SqlDataSourcePoPossibleDublication.SelectParameters("PO_Currency").DefaultValue = Convert.ToString(DropDownListCurrency.SelectedValue)
                Else
                    ' Single PO
                    Session(PO_NoTextBox.Text) = "Single PO"
                End If
            End While
            con.Close()
            dr.Close()
            con.Dispose()
        End Using

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Roles.IsUserInRole("InitiateContractAndAddendum") Then
            ' user not to allow
            Response.Redirect("~/webforms/AccessDenied.aspx")
        End If

        Dim _FormViewSupplier As FormView = FormViewPO.FindControl("FormViewSupplier")
        Dim _InsertButtonInPO As LinkButton = FormViewPO.FindControl("InsertButton")
        Dim _InsertButtonInSupplier As LinkButton = _FormViewSupplier.FindControl("InsertButton")

        'PreventMultipleClick.PreventMultipleClicks(_InsertButtonInPO)
        'PreventMultipleClick.PreventMultipleClicks(_InsertButtonInSupplier)

        ' it provides query parameter for DropDownListCostCode
        If IsPostBack Or Not IsPostBack Then
            TextBoxUserName.Text = Page.User.Identity.Name
        End If

        ' Check if it is from FrameContract
        If Not IsPostBack And Not PreviousPage Is Nothing Then
            Dim previousPageURL As String = Request.UrlReferrer.AbsolutePath.ToString
            Dim PageFileInfo As New System.IO.FileInfo(previousPageURL)
            If PageFileInfo.Name.ToString.ToLower = "contractview.aspx" Then
                If Session("FrameTicket") IsNot Nothing Then

                    HiddenField_FrameContractId.Value = GetVariablesFromFrameContract(Session("FrameTicket")).ContractID

                    Dim _commonContract As Integer = 0

                    Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities
                        Dim i As Integer = GetVariablesFromFrameContract(Session("FrameTicket")).ContractID
                        Dim commonContract = Aggregate C In db.Table_Contracts_Common Where C.ContractID = i Into Count()

                        _commonContract = commonContract

                    End Using

                    ' provide this value as default. Dont delete this lines
                    TextBoxFrameContract.Text = "yes"
                    TextBoxFrameContractID.Text = GetVariablesFromFrameContract(Session("FrameTicket")).ContractID.ToString

                    If GetVariablesFromFrameContract(Session("FrameTicket")).ProjectID = 175 Then
                        TextBoxFrameContract.Text = "yes"
                        TextBoxFrameContractID.Text = GetVariablesFromFrameContract(Session("FrameTicket")).ContractID.ToString
                    End If

                    If _commonContract > 0 Then
                        TextBoxFrameContract.Text = "no"
                        TextBoxFrameContractID.Text = 0
                    End If

                    ThisCodeWasHere_UseAsItIs(FormViewPO)

                    ' conditional 

                    Dim DropDownPrj As DropDownList = FormViewPO.FindControl("DropDownListProject")

                    If GetVariablesFromFrameContract(Session("FrameTicket")).ProjectID = 175 Or
                         _commonContract > 0 Then
                        ' This is office project. treat differently.
                        DropDownPrj.Enabled = True
                        Dim lst As New ListItem("Select Project", "0")
                        DropDownPrj.Items.Insert(0, lst)

                    Else
                        DropDownPrj.SelectedValue = GetVariablesFromFrameContract(Session("FrameTicket")).ProjectID
                        DropDownPrj.Enabled = False

                    End If

                    ProvidePoNo()

                    Dim DropDownListCurrency As DropDownList = FormViewPO.FindControl("DropDownListCurrency")
                    DropDownListCurrency.SelectedValue = GetVariablesFromFrameContract(Session("FrameTicket")).Currency
                    DropDownListCurrency.Enabled = False
                    Dim DropDownListRequestedBy As DropDownList = FormViewPO.FindControl("DropDownListRequestedBy")
                    Dim LabelRequestedBy As Label = FormViewPO.FindControl("LabelRequestedBy")
                    Dim CompareValidatorRequested As CompareValidator = FormViewPO.FindControl("CompareValidatorRequested")
                    If RequestedByRequired(GetVariablesFromFrameContract(Session("FrameTicket")).ProjectID) = True Then
                        DropDownListRequestedBy.Visible = True
                        LabelRequestedBy.Visible = True
                        CompareValidatorRequested.Enabled = True
                    Else
                        DropDownListRequestedBy.Visible = False
                        LabelRequestedBy.Visible = False
                        CompareValidatorRequested.Enabled = False
                    End If

                    If GetVariablesFromFrameContract(Session("FrameTicket")).ProjectID = 175 Or
                         _commonContract > 0 Then
                        DropDownListRequestedBy.Enabled = True

                    Else
                        DropDownListRequestedBy.SelectedValue = GetVariablesFromFrameContract(Session("FrameTicket")).RequestedBy
                        DropDownListRequestedBy.Enabled = False

                    End If

                    Dim DropDownListCostCode As DropDownList = FormViewPO.FindControl("DropDownListCostCode")

                    DropDownListCostCode.SelectedValue = PTS_MERCURY.helper.Table_Contracts.GetRowByPrimaryKey(GetVariablesFromFrameContract(Session("FrameTicket")).ContractID).CostCode.Trim()

                    Dim SupplierIDTextBox As TextBox = FormViewPO.FindControl("SupplierIDTextBox")
                    SupplierIDTextBox.Text = GetVariablesFromFrameContract(Session("FrameTicket")).SupplierID
                    SupplierIDTextBox.Enabled = False
                    SupplierIDTextBox_TextChanged(SupplierIDTextBox, Nothing)

                    Dim ButtonEnterSupplier As Button = FormViewPO.FindControl("ButtonEnterSupplier")
                    ButtonEnterSupplier.Enabled = False

                    Session.Remove("FrameTicket")

                    Exit Sub

                End If
            End If

        End If

        If Not IsPostBack Then
            ' check session variable if page coming from POrequest
            Dim userName As String = Page.User.Identity.Name.ToLower.ToString
            If Not SessionsPoRequest.ScenarioSessionExist("Scenario1") Then
                Response.Redirect("~/webforms/PORequest.aspx")
                Exit Sub
            Else
                ' Page is coming from POrequest. Assing all session variables to controls

                ThisCodeWasHere_UseAsItIs(FormViewPO)

                'Assing all session variables to controls
                Dim _Scenario As String = "Scenario1"

                Dim DropDownPrj As DropDownList = FormViewPO.FindControl("DropDownListProject")
                DropDownPrj.SelectedValue = GetVariables(userName + _Scenario).ProjectID
                DropDownPrj.Enabled = False

                ' Provide PO no. Because DropDownListProjectID already assigned
                ProvidePoNo()
                Dim TotalPriceTextBox As TextBox = FormViewPO.FindControl("TotalPriceTextBox")
                TotalPriceTextBox.Text = GetVariables(userName + _Scenario).TotalPrice
                TotalPriceTextBox.Enabled = False
                Dim TextBoxVATpercent As TextBox = FormViewPO.FindControl("TextBoxVATpercent")
                TextBoxVATpercent.Text = GetVariables(userName + _Scenario).VAT
                TextBoxVATpercent.Enabled = False
                Dim DropDownListCurrency As DropDownList = FormViewPO.FindControl("DropDownListCurrency")
                DropDownListCurrency.SelectedValue = GetVariables(userName + _Scenario).Currency
                DropDownListCurrency.Enabled = False
                Dim DropDownListCostCode As DropDownList = FormViewPO.FindControl("DropDownListCostCode")
                DropDownListCostCode.SelectedValue = GetVariables(userName + _Scenario).CostCode.Trim
                DropDownListCostCode.Enabled = False
                Dim DropDownListRequestedBy As DropDownList = FormViewPO.FindControl("DropDownListRequestedBy")
                Dim LabelRequestedBy As Label = FormViewPO.FindControl("LabelRequestedBy")
                Dim CompareValidatorRequested As CompareValidator = FormViewPO.FindControl("CompareValidatorRequested")
                If RequestedByRequired(GetVariables(userName + _Scenario).ProjectID) = True Then
                    DropDownListRequestedBy.Visible = True
                    LabelRequestedBy.Visible = True
                    CompareValidatorRequested.Enabled = True
                Else
                    DropDownListRequestedBy.Visible = False
                    LabelRequestedBy.Visible = False
                    CompareValidatorRequested.Enabled = False
                End If
                DropDownListRequestedBy.SelectedValue = GetVariables(userName + _Scenario).RequestedBy
                DropDownListRequestedBy.Enabled = False

            End If
        End If

        TextOsman.Text = Session("Currency")

        'If Not IsPostBack Then

        '  'Dim lst As New ListItem("Select Project", "0")
        '  'DropDownPrj.Items.Insert(0, lst)
        'End If

        'If Not IsPostBack Then
        '  Dim DrpProjec As DropDownList = FormViewPO.FindControl("DropDownListProject")
        '  DrpProjec.Focus()
        'End If

        'If IsPostBack Then
        '  Dim DropDownPrji As DropDownList = FormViewPO.FindControl("DropDownListProject")
        '  Dim controll1 As New String(Page.Request.Params.Get("__EVENTTARGET"))

        '  If (Not controll1 Is Nothing) Or (controll1 <> "") Then
        '    If controll1 = "ctl00$MainContent$FormViewPO$DropDownListProject" Then
        '      If DropDownPrji.Items(0).ToString = "Select Project" Then
        '        DropDownPrji.Items.RemoveAt(0)
        '      End If
        '      Dim DropDownListCurrency As DropDownList = FormViewPO.FindControl("DropDownListCurrency")
        '      DropDownListCurrency.Enabled = True
        '    End If
        '  End If
        'End If

        Dim _DropDownListProject As DropDownList = DirectCast(FormViewPO.FindControl("DropDownListProject"), DropDownList)
        Dim _DropDownListCostCode As DropDownList = DirectCast(FormViewPO.FindControl("DropDownListCostCode"), DropDownList)
        Dim _TotalPriceTextBox As TextBox = DirectCast(FormViewPO.FindControl("TotalPriceTextBox"), TextBox)
        Dim _TextBoxVATpercent As TextBox = DirectCast(FormViewPO.FindControl("TextBoxVATpercent"), TextBox)
        Dim _InsertButton As LinkButton = DirectCast(FormViewPO.FindControl("InsertButton"), LinkButton)

        Dim _newvalue As Decimal = 0.0

        _newvalue = Math.Round(IIf(IsNumeric(_TotalPriceTextBox.Text), _TotalPriceTextBox.Text, 0) / ((100 + (IIf(IsNumeric(_TextBoxVATpercent.Text), _TextBoxVATpercent.Text, 0))) / 100), 2)


        If PTS_MERCURY.helper.Table_Budget.GetBudgetEmailControlFailed(Page,
                                                               IIf(_DropDownListProject.SelectedIndex > 0, _DropDownListProject.SelectedValue, 0),
                                                               _DropDownListCostCode.SelectedValue.ToString().Trim(),
                                                               0,
                                                               _newvalue) =
                                                           True Then

            _InsertButton.Enabled = False

        Else

            _InsertButton.Enabled = True

        End If

    End Sub

    Protected Sub ThisCodeWasHere_UseAsItIs(ByVal _Formview As FormView)

        ' _____ THIS PART MOVED TO HERE FROM OLD POCREATE
        ' it adds jscript into insert button
        'Dim FrmVPOSupplier As FormView = _Formview.FindControl("FormViewSupplier")
        'Dim Inst As LinkButton = FrmVPOSupplier.FindControl("InsertButton")
        'Inst.Attributes.Add("onclick", "return SupplierInsert();")

        ' Po date to be today as default
        Dim TextBoxToday As TextBox = _Formview.FindControl("PO_DateTextBox")
        TextBoxToday.Text = Mid(TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time")).ToString(), 1, 10).ToString()

        ' Bind dropdownlist for ProjectID
        Dim DropDownPrj As DropDownList = _Formview.FindControl("DropDownListProject")
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, (RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5),dbo.Table1_Project.ProjectID))) as ProjectName, dbo.aspnet_Users.UserName FROM         dbo.Table1_Project INNER JOIN  dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN  dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId WHERE  (Table1_Project.CurrentStatus = 1) AND  (dbo.aspnet_Users.UserName = @UserName) ORDER BY dbo.Table1_Project.ProjectName"
            Dim cmd As New SqlCommand(sqlstring, con)

            cmd.CommandType = System.Data.CommandType.Text

            Dim UserParm As SqlParameter = cmd.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar, 256)
            UserParm.Value = Page.User.Identity.Name

            Dim dr As SqlDataReader = cmd.ExecuteReader

            DropDownPrj.DataSource = dr
            DropDownPrj.DataTextField = "ProjectName"
            DropDownPrj.DataValueField = "ProjectID"
            DropDownPrj.DataBind()
            con.Close()
            dr.Close()
            con.Dispose()
        End Using

        ' END OF /// THIS PART MOVED TO HERE FROM OLD POCREATE

    End Sub

    Protected Sub SqlDataSourceSupplierEnter_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
        Dim command As System.Data.Common.DbCommand = e.Command
        Dim FrmViewSupplier As FormView = FormViewPO.FindControl("FormViewSupplier")
        Dim TextBoxSupplier As TextBox = FormViewPO.FindControl("SupplierIDTextBox")

        Dim DrpSplr As DropDownList = FormViewPO.FindControl("DropLspLr")
        Dim LblShwSplr As Label = FormViewPO.FindControl("LabelShowSupplier")
        Dim LabelSplrError As Label = FormViewPO.FindControl("LabelSplrError")


        FrmViewSupplier.Visible = False
        TextBoxSupplier.Text = command.Parameters("@SupplierID").Value
        TextBoxSupplier.BackColor = System.Drawing.Color.White

        DrpSplr.DataBind()
        LblShwSplr.Text = DrpSplr.SelectedValue
        LabelSplrError.Text = ""

    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        If IsPostBack Then

            Dim FrmViewSupplier As FormView = FormViewPO.FindControl("FormViewSupplier")
            Dim aBtn As Button = FormViewPO.FindControl("ButtonEnterSupplier")

            If FrmViewSupplier.Visible = True Then
                aBtn.Text = "Hide Supplier Window"
            ElseIf FrmViewSupplier.Visible = False Then
                aBtn.Text = "Add New Supplier"
            End If

        End If

        If IsPostBack Then
            Dim controll As New String(Page.Request.Params.Get("__EVENTTARGET"))

            If (Not controll Is Nothing) Or (controll <> "") Then
                If controll = "ctl00$MainContent$FormViewPO$SupplierIDTextBox" Then

                    Dim LblShwSplr As Label = FormViewPO.FindControl("LabelShowSupplier")
                    Dim LabelErr As Label = FormViewPO.FindControl("LabelSplrError")
                    Dim TxtSupplier As TextBox = FormViewPO.FindControl("SupplierIDTextBox")

                    If LblShwSplr.Text = "" Then
                        LabelErr.Text = "INN Number is not 10 digit or Supplier does not exist"
                        TxtSupplier.BackColor = System.Drawing.Color.Yellow
                    ElseIf LblShwSplr.Text <> "" Then
                        LabelErr.Text = ""
                        TxtSupplier.BackColor = System.Drawing.Color.White
                        Dim TxtDescription As TextBox = FormViewPO.FindControl("DescriptionTextBox")
                        TxtDescription.Focus()
                    End If
                End If
            End If
        End If

    End Sub

    Protected Sub SupplierIDTextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim TextBoxSupplierID As TextBox = sender

        TextBoxSupplierID.Text = Left(TextBoxSupplierID.Text.ToString, 12).Trim

        Dim FixedTextBox As TextBox = sender
        FixedTextBox.Text = Mid(FixedTextBox.Text, 1, 12)

        Dim DrpSplr As DropDownList = FormViewPO.FindControl("DropLspLr")
        Dim LblShwSplr1 As Label = FormViewPO.FindControl("LabelShowSupplier")
        Dim GrdViewPoSumCk2 As GridView = FormViewPO.FindControl("GridViewPOsumCheck")
        Dim GridViewContractAndPoSuggestion As GridView = FormViewPO.FindControl("GridViewContractAndPoSuggestion")

        DrpSplr.DataBind()
        LblShwSplr1.Text = DrpSplr.SelectedValue

        GrdViewPoSumCk2.DataBind()
        Dim PanelWarning As Panel = FormViewPO.FindControl("PanelWarning")
        If GrdViewPoSumCk2.Rows.Count > 0 Then
            PanelWarning.Visible = True
        Else
            PanelWarning.Visible = False
        End If

        ' bind Gridview to check if any contract and addendum suggesstion exist for this PO
        GridViewContractAndPoSuggestion.DataBind()

    End Sub

    Protected Sub ButtonEnterSupplier_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FrmViewSupplier As FormView = FormViewPO.FindControl("FormViewSupplier")
        Dim Btn As Button = FormViewPO.FindControl("ButtonEnterSupplier")


        If Btn.Text = "Add New Supplier" Then
            FrmViewSupplier.Visible = True
        End If

        If Btn.Text = "Hide Supplier Window" Then
            FrmViewSupplier.Visible = False
            Btn.Text = "Add New Supplier"
        End If

    End Sub

    Protected Sub FormViewPO_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertedEventArgs) Handles FormViewPO.ItemInserted

        Dim PO_NoTextBox As TextBox = FormViewPO.FindControl("PO_NoTextBox")
        Session.Remove(PO_NoTextBox.Text)

        ' remove all Enter characters in Description of TABLE2_PO
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "Fix_ReturnCharacter_On_Table2_PO"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.StoredProcedure
            Dim dr As SqlDataReader = cmd.ExecuteReader
            con.Close()
            dr.Close()
            con.Dispose()
        End Using

        ' Check if PO created successfully or not
        If POexist(PO_NoTextBox.Text.ToString) Then
            RedirectToInvoiceDefinePage("CheckApprovalToSendEmail")
        Else
            message("PO cannot be created. Something went wrong.")
        End If

    End Sub

    Public Sub message(ByVal Message As String)
        Dim BuildScript As New System.Text.StringBuilder
        Dim cs As ClientScriptManager = Page.ClientScript
        BuildScript.Append("<script>")
        BuildScript.Append(Environment.NewLine)
        BuildScript.Append("alert('" & Message & "');")
        BuildScript.Append(Environment.NewLine)
        BuildScript.Append("</" + "script>")
        cs.RegisterStartupScript(Me.[GetType](), "asd", BuildScript.ToString())
    End Sub

    Protected Function POexist(ByVal _pono As String) As Boolean
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT Count(PO_No) as CountOfPo FROM Table2_PONo WHERE PO_No = @PO_No"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim ponoParameter As SqlParameter = cmd.Parameters.Add("@PO_No", System.Data.SqlDbType.NVarChar, 12)
            ponoParameter.Value = _pono
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim returnvalue As Boolean = False
            While dr.Read
                If dr(0) = 1 Then
                    returnvalue = True
                End If
            End While
            Return returnvalue
            con.Close()
            dr.Close()
            con.Dispose()
        End Using

    End Function

    Protected Sub FormViewPO_ItemInserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertEventArgs) Handles FormViewPO.ItemInserting

        Dim TotalPriceTextBox_ As TextBox = FormViewPO.FindControl("TotalPriceTextBox")
        Dim DropDownListProject_ As DropDownList = FormViewPO.FindControl("DropDownListProject")

        If IsNumeric(HiddenField_FrameContractId.Value) Then
            If HiddenField_FrameContractId.Value > 0 Then

                If PTS_MERCURY.helper.Table1_Project.GetRowByPrimaryKey(DropDownListProject_.SelectedValue).FrameBudgetEmailControl = True Then

                    Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                        Dim bb = db.sp_FrameContractBudgetPoDetails(HiddenField_FrameContractId.Value, PTS_MERCURY.helper.Table_Contracts.BudgetTolerancePercentage.TolerancePercent, 0, CType(TotalPriceTextBox_.Text, Decimal))

                        For Each var In bb

                            If var.BudgetExceeded = True Then
                                ' stop it
                                Literal_MaxAllowedPo.Text = String.Format("{0:#,##0.00}", var.Budget - (var.TotalPO - CType(TotalPriceTextBox_.Text, Decimal)))

                                ' give error notification
                                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalFramePoControl" + "').modal({}) });", True)
                                e.Cancel = True
                                Exit Sub

                            End If

                            If var.BudgetAboutToExceedWarning = True Then
                                ' send email
                                ' cancelled at the moment
                                'PTS_MERCURY.helper.EmailGenerator.FrameBudgetEmailGenerator.Send(Page.User.Identity.Name.ToLower.Trim(), HiddenField_FrameContractId.Value, PTS_MERCURY.helper.EmailGenerator.FrameBudgetEmailGenerator.TypeOfNotification.BudgetexceedWarning)

                            End If

                        Next

                    End Using

                End If

            End If

        End If

        ' check the session variable to proceed or not
        Dim PO_NoTextBox As TextBox = FormViewPO.FindControl("PO_NoTextBox")
        If Session(PO_NoTextBox.Text) IsNot Nothing Then
            If e.CommandArgument = "Go" OrElse Session(PO_NoTextBox.Text) = "Single PO" Then

                Dim zoneId As String = "Russian Standard Time"
                Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
                Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

                SqlDataSourcePO.InsertParameters("CreatedBy").DefaultValue = result.ToString()
                SqlDataSourcePO.InsertParameters("CreatedBy").Type = TypeCode.DateTime

                SqlDataSourcePO.InsertParameters("PersonCreated").DefaultValue = Page.User.Identity.Name.ToString
                SqlDataSourcePO.InsertParameters("PersonCreated").Type = TypeCode.String

                ' proceed inserting
                Dim DropDownListCurrency As DropDownList = FormViewPO.FindControl("DropDownListCurrency")
                Dim DropDownListCostCode As DropDownList = FormViewPO.FindControl("DropDownListCostCode")
                Dim PO_DateTextBox As TextBox = FormViewPO.FindControl("PO_DateTextBox")

                e.Values("PO_Currency") = DropDownListCurrency.SelectedValue.ToString
                e.Values("PO_Date") = Convert.ToDateTime(Mid(PO_DateTextBox.Text.ToString, 1, 2).ToString + "/" + Mid(PO_DateTextBox.Text.ToString, 4, 2).ToString + "/" + Mid(PO_DateTextBox.Text.ToString, 7, 4).ToString)
                e.Values("CostCode") = DropDownListCostCode.SelectedValue.ToString

                ' approval parameters
                Dim DropDownListProject As DropDownList = FormViewPO.FindControl("DropDownListProject")
                Dim DropDownListRequestedBy As DropDownList = FormViewPO.FindControl("DropDownListRequestedBy")

                ' I DEACTIVATED THIS PART, NO NEED IN APPROVAL MATRIX
                'If ApprovalRequired(DropDownListProject.SelectedValue) = True Then
                '  e.Values("Approved") = False
                '  e.Values("PersonApproved") = Nothing
                '  e.Values("RequestedBy") = DropDownListRequestedBy.SelectedValue.ToString
                'Else
                '  e.Values("Approved") = Nothing
                '  e.Values("PersonApproved") = Nothing
                '  e.Values("RequestedBy") = Nothing
                'End If

                If TextBoxFrameContract.Text = "yes" Then
                    e.Values("FrameContractID") = Convert.ToInt32(TextBoxFrameContractID.Text)
                    e.Values("FrameContractPO") = True
                    e.Values("Approved") = False
                    e.Values("PersonApproved") = Nothing
                    e.Values("RequestedBy") = DropDownListRequestedBy.SelectedValue.ToString

                Else
                    e.Values("FrameContractID") = Convert.ToInt32("0")
                    e.Values("FrameContractPO") = False
                    e.Values("Approved") = Nothing
                    e.Values("PersonApproved") = Nothing
                    e.Values("RequestedBy") = DropDownListRequestedBy.SelectedValue.ToString
                End If

                ' update contract or addendum tables if user match a PO against that
                Dim GridViewContractAndPoSuggestion As GridView = FormViewPO.FindControl("GridViewContractAndPoSuggestion")
                For Each row In GridViewContractAndPoSuggestion.Rows
                    Dim CheckBoxSuggestion As CheckBox = DirectCast(row.FindControl("CheckBoxSuggestion"), CheckBox)
                    Dim labelID As Label = DirectCast(row.FindControl("labelID"), Label)
                    If CheckBoxSuggestion IsNot Nothing Then
                        If CheckBoxSuggestion.Checked = True Then
                            If labelID.Text.ToString.IndexOf("A") > 0 Then
                                ' it is Addendum, update addendum table
                                Using cn As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                                    Dim cmd As New System.Data.SqlClient.SqlCommand()
                                    cmd.Connection = cn
                                    cmd.CommandText = "UPDATE Table_Addendums SET PO_No = '" + e.Values("PO_No").ToString + "' WHERE AddendumID = " + Mid(labelID.Text, labelID.Text.ToString.IndexOf("A") + 2, Len(labelID.Text) - labelID.Text.ToString.IndexOf("A") - 2).Replace(" ", "")
                                    cmd.CommandType = System.Data.CommandType.Text
                                    cn.Open()
                                    cmd.ExecuteNonQuery()
                                    cn.Close()
                                    cn.Dispose()
                                End Using
                            ElseIf labelID.Text.ToString.IndexOf("A") = -1 Then
                                ' it is Contract, update contract table
                                Response.Write("." + Mid(labelID.Text, 1, labelID.Text.ToString.IndexOf("_")).Replace(" ", "") + ".")
                                Using cn As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                                    Dim cmd As New System.Data.SqlClient.SqlCommand()
                                    cmd.Connection = cn
                                    cmd.CommandText = "UPDATE Table_Contracts SET PO_No = '" + e.Values("PO_No").ToString + "' WHERE ContractID = " + Mid(labelID.Text, 1, labelID.Text.ToString.IndexOf("_")).Replace(" ", "")
                                    cmd.CommandType = System.Data.CommandType.Text
                                    cn.Open()
                                    cmd.ExecuteNonQuery()
                                    cn.Close()
                                    cn.Dispose()
                                End Using
                            End If
                        End If
                    End If
                Next

                SessionsPoRequest.RemoveScenarioSession("Scenario1")

            ElseIf Session(PO_NoTextBox.Text) = "PO might be dublicated!" Then
                ' stop insert. Show modalPopUp for possible dublicating PO
                e.Cancel = True
                ' bind GridViewPoPossibleDublication to source

                Dim GridViewPoPossibleDublication As GridView = FormViewPO.FindControl("GridViewPoPossibleDublication")
                Dim SqlDataSourcePoPossibleDublication As SqlDataSource = FormViewPO.FindControl("SqlDataSourcePoPossibleDublication")
                Dim ModalPopupExtender1 As ModalPopupExtender = FormViewPO.FindControl("ModalPopupExtender1")
                Dim panEdit As Panel = FormViewPO.FindControl("panEdit")

                GridViewPoPossibleDublication.DataSource = SqlDataSourcePoPossibleDublication
                GridViewPoPossibleDublication.DataBind()
                ModalPopupExtender1.Show()
                panEdit.CssClass = "PanelWarning"

            End If
        End If
    End Sub

    Protected Sub LinkButtonPO_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub GridViewPOsumCheck_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        If (e.CommandName = "OpenPO") Then
            Dim GridViewPOsumCheck As GridView = FormViewPO.FindControl("GridViewPOsumCheck")

            ' Retrieve the row index stored in the CommandArgument property.
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            ' Retrieve the row that contains the button 
            ' from the Rows collection.
            Dim row As GridViewRow = GridViewPOsumCheck.Rows(index)

            ' Add code here to add the item to the shopping cart.
            'Dim LabelPO As Label = DirectCast(row.FindControl("LabelPO"), Label)
            Dim LinkButtonPO As LinkButton = DirectCast(row.FindControl("LinkButtonPO"), LinkButton)

            TextBoxPO_fromLinkButton.Text = LinkButtonPO.Text

            RedirectToInvoiceDefinePage("IgnoreApproval")

        End If
    End Sub

    Protected Sub GridViewPOsumCheck_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            If DirectCast(e.Row.FindControl("LinkButtonPO"), LinkButton) IsNot Nothing Then
                If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "Approved")) AndAlso DataBinder.Eval(e.Row.DataItem, "Approved") = False Then
                    DirectCast(e.Row.FindControl("LinkButtonPO"), LinkButton).Visible = False
                    e.Row.Cells(1).Text = "Not Approved Po"
                    e.Row.Cells(1).BackColor = System.Drawing.Color.Black
                    e.Row.Cells(1).ForeColor = System.Drawing.Color.Lime
                End If
            End If
        End If
    End Sub

    Protected Sub DropDownListCostCode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim TextBoxCostCodeError As TextBox = FormViewPO.FindControl("TextBoxCostCodeError")
        Dim DropDownListProject As DropDownList = FormViewPO.FindControl("DropDownListProject")
        Dim DropDownListCostCode As DropDownList = FormViewPO.FindControl("DropDownListCostCode")

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT     RTRIM(Type) AS Type" +
                                                    " FROM Table1_Project" +
                                                    " WHERE     ProjectID =" + "'" + DropDownListProject.SelectedValue.ToString + "'"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                Dim ProjectType As String
                Dim DropDownListCurrency As DropDownList = FormViewPO.FindControl("DropDownListCurrency")
                ProjectType = dr(0).ToString()
                If ProjectType = "DataCenter" Then
                    If DropDownListCostCode.SelectedValue.ToString = "0" Then
                        TextBoxCostCodeError.Text = "Valid"
                        DropDownListCurrency.Enabled = True
                    ElseIf Len(DropDownListCostCode.SelectedValue.ToString) = 10 Then
                        TextBoxCostCodeError.Text = "Valid"
                        DropDownListCurrency.Enabled = True
                    ElseIf Len(DropDownListCostCode.SelectedValue.ToString) = 1 AndAlso Not IsNumeric(DropDownListCostCode.SelectedValue.ToString) Then
                        TextBoxCostCodeError.Text = "NotValid"
                        Dim CompareValidatorCostCode As CompareValidator = FormViewPO.FindControl("CompareValidatorCostCode")
                        CompareValidatorCostCode.Validate()
                        DropDownListCurrency.Enabled = True
                    ElseIf Len(DropDownListCostCode.SelectedValue.ToString) > 1 AndAlso Len(DropDownListCostCode.SelectedValue.ToString) < 10 Then
                        If Roles.IsUserInRole("Finance") Then
                            If IsNumeric(DropDownListCostCode.SelectedValue.ToString) Then
                                TextBoxCostCodeError.Text = "Valid"
                                DropDownListCurrency.SelectedValue = "Rub"
                                DropDownListCurrency.Enabled = False
                            ElseIf Not IsNumeric(DropDownListCostCode.SelectedValue.ToString) Then
                                TextBoxCostCodeError.Text = "NotValid"
                                Dim CompareValidatorCostCode As CompareValidator = FormViewPO.FindControl("CompareValidatorCostCode")
                                CompareValidatorCostCode.Validate()
                                DropDownListCurrency.Enabled = True
                            End If
                        Else
                            TextBoxCostCodeError.Text = "NotValid"
                            Dim CompareValidatorCostCode As CompareValidator = FormViewPO.FindControl("CompareValidatorCostCode")
                            CompareValidatorCostCode.Validate()
                            DropDownListCurrency.Enabled = True
                        End If
                    End If
                Else
                    TextBoxCostCodeError.Text = "Valid"

                    Dim DropDownListCostCodeType As DropDownList = FormViewPO.FindControl("DropDownListCostCodeType")
                    DropDownListCostCodeType.DataBind()
                    If DropDownListCostCodeType.SelectedValue.ToString = "Finance" Then
                        DropDownListCurrency.SelectedValue = "Rub"
                        DropDownListCurrency.Enabled = False
                    Else
                        DropDownListCurrency.Enabled = True
                    End If

                End If
            End While

            con.Close()
            dr.Close()
            con.Dispose()
        End Using

        ' frame contract Currency should be fixed
        Dim _DropDownListCurrency As DropDownList = FormViewPO.FindControl("DropDownListCurrency")
        If TextBoxFrameContract.Text = "yes" Then
            _DropDownListCurrency.Enabled = False
        End If

        Dim TotalPriceTextBox As TextBox = FormViewPO.FindControl("TotalPriceTextBox")
        TotalPriceTextBox.Focus()
    End Sub

    Protected Sub SqlDataSourceSupplierEnter_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs)

    End Sub

    Protected Sub InsertButton_Click1(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        Dim sqlsupplier As SqlDataSource = FormViewPO.FindControl("SqlDataSourceSupplierEnter")
        sqlsupplier.InsertParameters("CreatedBy").DefaultValue = result.ToString
        sqlsupplier.InsertParameters("CreatedBy").Type = TypeCode.DateTime

        sqlsupplier.InsertParameters("PersonCreated").DefaultValue = Page.User.Identity.Name.ToString
        sqlsupplier.InsertParameters("PersonCreated").Type = TypeCode.String
    End Sub

    Protected Sub GridViewContractAndPoSuggestion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
    End Sub

    Protected Sub CheckBoxSuggestion_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim GridViewContractAndPoSuggestion As GridView = FormViewPO.FindControl("GridViewContractAndPoSuggestion")
        Dim checkBoxMe As CheckBox = sender
        Dim SenderClient As String = checkBoxMe.ClientID.ToString
        If checkBoxMe.Checked = True Then
            For Each row In GridViewContractAndPoSuggestion.Rows
                If DirectCast(row.FindControl("CheckBoxSuggestion"), CheckBox) IsNot Nothing Then
                    If DirectCast(row.FindControl("CheckBoxSuggestion"), CheckBox).Checked = True AndAlso DirectCast(row.FindControl("CheckBoxSuggestion"), CheckBox).ClientID.ToString <> SenderClient Then
                        DirectCast(row.FindControl("CheckBoxSuggestion"), CheckBox).Checked = False
                        row.BackColor = System.Drawing.Color.White
                    ElseIf DirectCast(row.FindControl("CheckBoxSuggestion"), CheckBox).Checked = True AndAlso DirectCast(row.FindControl("CheckBoxSuggestion"), CheckBox).ClientID.ToString = SenderClient Then
                        DirectCast(row.FindControl("CheckBoxSuggestion"), CheckBox).Checked = True
                        row.BackColor = System.Drawing.Color.Lime
                    End If
                End If
            Next
        ElseIf checkBoxMe.Checked = False Then
            For Each row In GridViewContractAndPoSuggestion.Rows
                If DirectCast(row.FindControl("CheckBoxSuggestion"), CheckBox) IsNot Nothing Then
                    row.BackColor = System.Drawing.Color.White
                End If
            Next
        End If
    End Sub

    Protected Sub GridViewContractAndPoSuggestion_DataBound(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim GridViewContractAndPoSuggestion As GridView = FormViewPO.FindControl("GridViewContractAndPoSuggestion")
        Dim LabelSuggestionDescription As Label = FormViewPO.FindControl("LabelSuggestionDescription")
        If GridViewContractAndPoSuggestion.Rows.Count > 0 Then
            LabelSuggestionDescription.Text = "                <div style=" + """" + "border: thin solid #C0C0C0; padding: 2px; margin: 2px; width: 1000px; background-color: #F08080;" + """" + "> " +
      "                    <span style=" + """" + "color: #0000ff; font-size: 8pt" + """" + ">There are some contracts or addendums&nbsp;by Legal Department which would be raised against the&nbsp;PO you are going to create now.</span><br /> " +
      "                    <span style=" + """" + "color: #000000; font-size: 9pt; font-weight: bold" + """" + ">Please select suitable one if it matches to this PO...</span><br /> " +
      "                    <span style=" + """" + "color: #0000ff; font-size: 8pt" + """" + ">Once you select, then you can " + "<span style=" + """" + "color: #000000; font-size: 9pt; font-weight: bold; text-decoration: underline" + """" + ">automatically</span>" + " follow contracts with corresponding PO values on this page <a shape=" + """" + "rect" + """" + " href=" + """" + "http://pts.mercuryeng.ru/webforms/ContractVersusPo.aspx" + """" + "><span style=" + """" + "font-size: 8pt; font-weight: bold; text-decoration: underline" + """" + ">www.mercuryeng.org/ContractVersusPo.aspx</span></a></span> " +
      "                </div> "
        Else
            LabelSuggestionDescription.Text = ""
        End If
    End Sub

    Protected Sub GridViewContractAndPoSuggestion_Load(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub RedirectToInvoiceDefinePage(ByVal CheckApproval As String)

        Dim PoNoNext As TextBox = FormViewPO.FindControl("PO_NoTextBox")
        Dim DDLproject As DropDownList = FormViewPO.FindControl("DropDownListProject")
        Dim PO_link As String = TextBoxPO_fromLinkButton.Text

        If CheckApproval.ToLower = "ignoreapproval" Then

            Response.Redirect("~/webforms/invoicedefine.aspx?PoNo=" + PO_link)

            Exit Sub
        ElseIf CheckApproval.ToLower = "checkapprovaltosendemail" Then

            ' check here if approval required. If so, redirect to MainPage, and send email to the person who approves PO
            Dim DropDownListProject As DropDownList = FormViewPO.FindControl("DropDownListProject")
            Dim LabelShowSupplier As Label = FormViewPO.FindControl("LabelShowSupplier")
            Dim DescriptionTextBox As TextBox = FormViewPO.FindControl("DescriptionTextBox")
            Dim DropDownListCostCode As DropDownList = FormViewPO.FindControl("DropDownListCostCode")
            Dim TotalPriceTextBox As TextBox = FormViewPO.FindControl("TotalPriceTextBox")
            Dim TotalPrice As String = String.Format("{0:#,##0.00}", Convert.ToDecimal(TotalPriceTextBox.Text))
            Dim TextBoxVATpercent As TextBox = FormViewPO.FindControl("TextBoxVATpercent")
            Dim DropDownListCurrency As DropDownList = FormViewPO.FindControl("DropDownListCurrency")
            Dim DropDownListRequestedBy As DropDownList = FormViewPO.FindControl("DropDownListRequestedBy")

            ' If it is Frame Contract, send approval email to receipents
            If TextBoxFrameContract.Text = "yes" Then

                ' This Stored Procedure re organize PO description as per contract No and Contrat Date
                Using adapter As New ApprovalMatrixTableAdapters.QueriesTableAdapter
                    adapter.UpdatePO_Descriptions_asPerContractDetails()
                    adapter.Dispose()
                End Using

                Dim emailBody As String = " <a href=" + """" + "http://pts.mercuryeng.ru/webforms/poApprovalFrame.aspx?PO_No=" + PoNoNext.Text + """" + " target=" + """" + "_blank" + """" + ">Перейти на страницу ОФИЦИАЛЬНОГО УТВЕРЖДЕНИЯ</a> " +
                  "<table class=" + """" + "MsoNormalTable" + """" + " width=" + """" + "500" + """" + " border=" + """" + "0" + """" + " cellspacing=" + """" + "0" + """" + " cellpadding=" + """" + "0" + """" + " style=" + """" + "margin: auto auto auto -1.15pt; width: 375pt; border-collapse: collapse; mso-yfti-tbllook: 1184; mso-padding-alt: 0cm 0cm 0cm 0cm" + """" + ">" +
          "<tbody>" +
          "<tr style=" + """" + "height: 30.75pt; mso-yfti-irow: 0; mso-yfti-firstrow: yes" + """" + ">" +
          "<td width=" + """" + "159" + """" + " style=" + """" + "border-bottom: #4f81bd 1.5pt solid; border-left: #4f81bd 1pt solid; padding-bottom: 0cm; padding-left: 5.4pt; width: 119pt; padding-right: 5.4pt; background: black; height: 30.75pt; border-top: #4f81bd 1pt solid; border-right: #4f81bd 1pt solid; padding-top: 0cm" + """" + ">" +
          "<p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><b><span style=" + """" + "color: white" + """" + "><font size=" + """" + "3" + """" + "><font face=" + """" + "Calibri" + """" + ">New PO" +
          "<o:p></o:p></font></font></span></b></p></td>" +
          "<td width=" + """" + "341" + """" + " style=" + """" + "border-bottom: #4f81bd 1.5pt solid; border-left: #f0f0f0; padding-bottom: 0cm; padding-left: 5.4pt; width: 256pt; padding-right: 5.4pt; background: black; height: 30.75pt; border-top: #4f81bd 1pt solid; border-right: #4f81bd 1pt solid; padding-top: 0cm" + """" + ">" +
          "<p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><b><span style=" + """" + "color: white" + """" + "><font size=" + """" + "3" + """" + "><font face=" + """" + "Calibri" + """" + ">" + PoNoNext.Text + "" +
          "<o:p></o:p></font></font></span></b></p></td></tr>" +
          "<tr style=" + """" + "height: 39pt; mso-yfti-irow: 1" + """" + ">" +
          "<td width=" + """" + "159" + """" + " valign=" + """" + "top" + """" + " style=" + """" + "border-bottom: #4f81bd 1pt solid; border-left: #4f81bd 1pt solid; padding-bottom: 0cm; background-color: transparent; padding-left: 5.4pt; width: 119pt; padding-right: 5.4pt; height: 39pt; border-top: #f0f0f0; border-right: #4f81bd 1pt solid; padding-top: 0cm" + """" + ">" +
          "<p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><b><span style=" + """" + "color: #1f497d" + """" + "><font size=" + """" + "3" + """" + "><font face=" + """" + "Calibri" + """" + ">Кто запросил" +
          "<o:p></o:p></font></font></span></b></p></td>" +
          "<td width=" + """" + "341" + """" + " valign=" + """" + "top" + """" + " style=" + """" + "border-bottom: #4f81bd 1pt solid; border-left: #f0f0f0; padding-bottom: 0cm; background-color: transparent; padding-left: 5.4pt; width: 256pt; padding-right: 5.4pt; height: 39pt; border-top: #f0f0f0; border-right: #4f81bd 1pt solid; padding-top: 0cm" + """" + ">" +
          "<p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span style=" + """" + "color: #1f497d" + """" + "><font size=" + """" + "3" + """" + "><font face=" + """" + "Calibri" + """" + ">" + DropDownListRequestedBy.SelectedItem.ToString + "" +
          "<o:p></o:p></font></font></span></p></td></tr>" +
          "<tr style=" + """" + "height: 39pt; mso-yfti-irow: 2" + """" + ">" +
          "<td width=" + """" + "159" + """" + " valign=" + """" + "top" + """" + " style=" + """" + "border-bottom: #4f81bd 1pt solid; border-left: #4f81bd 1pt solid; padding-bottom: 0cm; padding-left: 5.4pt; width: 119pt; padding-right: 5.4pt; background: #d3dfee; height: 39pt; border-top: #f0f0f0; border-right: #4f81bd 1pt solid; padding-top: 0cm" + """" + ">" +
          "<p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><b><span style=" + """" + "color: #1f497d" + """" + "><font size=" + """" + "3" + """" + "><font face=" + """" + "Calibri" + """" + ">поставщик" +
          "<o:p></o:p></font></font></span></b></p></td>" +
          "<td width=" + """" + "341" + """" + " valign=" + """" + "top" + """" + " style=" + """" + "border-bottom: #4f81bd 1pt solid; border-left: #f0f0f0; padding-bottom: 0cm; padding-left: 5.4pt; width: 256pt; padding-right: 5.4pt; background: #d3dfee; height: 39pt; border-top: #f0f0f0; border-right: #4f81bd 1pt solid; padding-top: 0cm" + """" + ">" +
          "<p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span style=" + """" + "color: #1f497d" + """" + "><font size=" + """" + "3" + """" + "><font face=" + """" + "Calibri" + """" + ">" + LabelShowSupplier.Text + "" +
          "<o:p></o:p></font></font></span></p></td></tr>" +
          "<tr style=" + """" + "height: 39pt; mso-yfti-irow: 3" + """" + ">" +
          "<td width=" + """" + "159" + """" + " valign=" + """" + "top" + """" + " style=" + """" + "border-bottom: #4f81bd 1pt solid; border-left: #4f81bd 1pt solid; padding-bottom: 0cm; background-color: transparent; padding-left: 5.4pt; width: 119pt; padding-right: 5.4pt; height: 39pt; border-top: #f0f0f0; border-right: #4f81bd 1pt solid; padding-top: 0cm" + """" + ">" +
          "<p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><b><span style=" + """" + "color: #1f497d" + """" + "><font size=" + """" + "3" + """" + "><font face=" + """" + "Calibri" + """" + ">Описание" +
          "<o:p></o:p></font></font></span></b></p></td>" +
          "<td width=" + """" + "341" + """" + " valign=" + """" + "top" + """" + " style=" + """" + "border-bottom: #4f81bd 1pt solid; border-left: #f0f0f0; padding-bottom: 0cm; background-color: transparent; padding-left: 5.4pt; width: 256pt; padding-right: 5.4pt; height: 39pt; border-top: #f0f0f0; border-right: #4f81bd 1pt solid; padding-top: 0cm" + """" + ">" +
          "<p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span style=" + """" + "color: #1f497d" + """" + "><font size=" + """" + "3" + """" + "><font face=" + """" + "Calibri" + """" + ">" + PTS_MERCURY.helper.Table2_PONo.GetRowByPrimaryKey(PoNoNext.Text).Description + "" +
          "<o:p></o:p></font></font></span></p></td></tr>" +
          "<tr style=" + """" + "height: 39pt; mso-yfti-irow: 4" + """" + ">" +
          "<td width=" + """" + "159" + """" + " valign=" + """" + "top" + """" + " style=" + """" + "border-bottom: #4f81bd 1pt solid; border-left: #4f81bd 1pt solid; padding-bottom: 0cm; padding-left: 5.4pt; width: 119pt; padding-right: 5.4pt; background: #d3dfee; height: 39pt; border-top: #f0f0f0; border-right: #4f81bd 1pt solid; padding-top: 0cm" + """" + ">" +
          "<p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><b><span style=" + """" + "color: #1f497d" + """" + "><font size=" + """" + "3" + """" + "><font face=" + """" + "Calibri" + """" + ">Код затрат" +
          "<o:p></o:p></font></font></span></b></p></td>" +
          "<td width=" + """" + "341" + """" + " valign=" + """" + "top" + """" + " style=" + """" + "border-bottom: #4f81bd 1pt solid; border-left: #f0f0f0; padding-bottom: 0cm; padding-left: 5.4pt; width: 256pt; padding-right: 5.4pt; background: #d3dfee; height: 39pt; border-top: #f0f0f0; border-right: #4f81bd 1pt solid; padding-top: 0cm" + """" + ">" +
          "<p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span style=" + """" + "color: #1f497d" + """" + "><font size=" + """" + "3" + """" + "><font face=" + """" + "Calibri" + """" + ">" + DropDownListCostCode.SelectedItem.ToString + "" +
          "<o:p></o:p></font></font></span></p></td></tr>" +
          "<tr style=" + """" + "height: 39pt; mso-yfti-irow: 5" + """" + ">" +
          "<td width=" + """" + "159" + """" + " valign=" + """" + "top" + """" + " style=" + """" + "border-bottom: #4f81bd 1pt solid; border-left: #4f81bd 1pt solid; padding-bottom: 0cm; background-color: transparent; padding-left: 5.4pt; width: 119pt; padding-right: 5.4pt; height: 39pt; border-top: #f0f0f0; border-right: #4f81bd 1pt solid; padding-top: 0cm" + """" + ">" +
          "<p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><b><span style=" + """" + "color: #1f497d" + """" + "><font size=" + """" + "3" + """" + "><font face=" + """" + "Calibri" + """" + ">Общая цена с НДС" +
          "<o:p></o:p></font></font></span></b></p></td>" +
          "<td width=" + """" + "341" + """" + " valign=" + """" + "top" + """" + " style=" + """" + "border-bottom: #4f81bd 1pt solid; border-left: #f0f0f0; padding-bottom: 0cm; background-color: transparent; padding-left: 5.4pt; width: 256pt; padding-right: 5.4pt; height: 39pt; border-top: #f0f0f0; border-right: #4f81bd 1pt solid; padding-top: 0cm" + """" + ">" +
          "<p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span style=" + """" + "color: #1f497d" + """" + "><font size=" + """" + "3" + """" + "><font face=" + """" + "Calibri" + """" + ">" + TotalPrice + "" +
          "<o:p></o:p></font></font></span></p></td></tr>" +
          "<tr style=" + """" + "height: 39pt; mso-yfti-irow: 6" + """" + ">" +
          "<td width=" + """" + "159" + """" + " valign=" + """" + "top" + """" + " style=" + """" + "border-bottom: #4f81bd 1pt solid; border-left: #4f81bd 1pt solid; padding-bottom: 0cm; padding-left: 5.4pt; width: 119pt; padding-right: 5.4pt; background: #d3dfee; height: 39pt; border-top: #f0f0f0; border-right: #4f81bd 1pt solid; padding-top: 0cm" + """" + ">" +
          "<p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><b><span style=" + """" + "color: #1f497d" + """" + "><font size=" + """" + "3" + """" + "><font face=" + """" + "Calibri" + """" + ">НДС %" +
          "<o:p></o:p></font></font></span></b></p></td>" +
          "<td width=" + """" + "341" + """" + " valign=" + """" + "top" + """" + " style=" + """" + "border-bottom: #4f81bd 1pt solid; border-left: #f0f0f0; padding-bottom: 0cm; padding-left: 5.4pt; width: 256pt; padding-right: 5.4pt; background: #d3dfee; height: 39pt; border-top: #f0f0f0; border-right: #4f81bd 1pt solid; padding-top: 0cm" + """" + ">" +
          "<p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span style=" + """" + "color: #1f497d" + """" + "><font size=" + """" + "3" + """" + "><font face=" + """" + "Calibri" + """" + ">" + TextBoxVATpercent.Text + "" +
          "<o:p></o:p></font></font></span></p></td></tr>" +
          "<tr style=" + """" + "height: 39pt; mso-yfti-irow: 7; mso-yfti-lastrow: yes" + """" + ">" +
          "<td width=" + """" + "159" + """" + " valign=" + """" + "top" + """" + " style=" + """" + "border-bottom: #4f81bd 1pt solid; border-left: #4f81bd 1pt solid; padding-bottom: 0cm; background-color: transparent; padding-left: 5.4pt; width: 119pt; padding-right: 5.4pt; height: 39pt; border-top: #f0f0f0; border-right: #4f81bd 1pt solid; padding-top: 0cm" + """" + ">" +
          "<p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><b><span style=" + """" + "color: #1f497d" + """" + "><font size=" + """" + "3" + """" + "><font face=" + """" + "Calibri" + """" + ">валюта" +
          "<o:p></o:p></font></font></span></b></p></td>" +
          "<td width=" + """" + "341" + """" + " valign=" + """" + "top" + """" + " style=" + """" + "border-bottom: #4f81bd 1pt solid; border-left: #f0f0f0; padding-bottom: 0cm; background-color: transparent; padding-left: 5.4pt; width: 256pt; padding-right: 5.4pt; height: 39pt; border-top: #f0f0f0; border-right: #4f81bd 1pt solid; padding-top: 0cm" + """" + ">" +
          "<p class=" + """" + "MsoNormal" + """" + " style=" + """" + "margin: 0cm 0cm 0pt" + """" + "><span style=" + """" + "color: #1f497d" + """" + "><font size=" + """" + "3" + """" + "><font face=" + """" + "Calibri" + """" + ">" + DropDownListCurrency.SelectedItem.ToString + "" +
          "<o:p></o:p></font></font></span></p></td></tr></tbody></table>"
                sendEmailForApprovalFrame(DropDownListProject.SelectedValue, "Запрос на одобрение " + PoNoNext.Text, emailBody, PoNoNext.Text)
                Session("GiveApprovalNotification") = "GiveNotificationOnDefaultPage"
                Response.Redirect("~/webforms/default.aspx")
            Else
                Response.Redirect("~/webforms/invoicedefine.aspx?PoNo=" + PoNoNext.Text)
            End If
        End If

    End Sub

    Protected Sub sendEmailForApprovalFrame(ByVal ProjectID As Integer, ByVal Subject As String, ByVal Body As String, ByVal POno As String)
        Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "PO Approval Frame")
        Dim mm1 As New MailMessage()
        mm1.From = MailFrom

        ' ADD AUTHORIZED PERSON
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " Select LoweredEmail from ( " +
        " SELECT     RTRIM(dbo.aspnet_Membership.LoweredEmail) AS LoweredEmail  " +
        " FROM  dbo.aspnet_Membership INNER JOIN  " +
        "   dbo.aspnet_Users ON dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId AND dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId  " +
        " WHERE (dbo.aspnet_Users.ApproveFramePo = 1)  " +
        "  " +
        " UNION ALL " +
        "  " +
        " SELECT     RTRIM(dbo.aspnet_Membership.LoweredEmail) AS LoweredEmail " +
        " FROM         dbo.aspnet_Users INNER JOIN " +
        "					  dbo.aspnet_Membership ON dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId AND dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId INNER JOIN " +
        "					  dbo.Table_Approval_UserPositionPrjJunction ON dbo.aspnet_Users.UserName = dbo.Table_Approval_UserPositionPrjJunction.UserName INNER JOIN " +
        "					  dbo.Table_Approval_PositionEmployee ON dbo.Table_Approval_UserPositionPrjJunction.PositionID = dbo.Table_Approval_PositionEmployee.PositionID " +
        " WHERE     (dbo.Table_Approval_PositionEmployee.PositionName = N'Cost Controller') AND (dbo.Table_Approval_UserPositionPrjJunction.ProjectID = @ProjectID) " +
        "  " +
        " UNION ALL " +
        "  " +
        " SELECT     RTRIM(dbo.aspnet_Membership.LoweredEmail) AS LoweredEmail " +
        " FROM         dbo.aspnet_Users INNER JOIN " +
        "                       dbo.aspnet_Membership ON dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId AND dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId INNER JOIN " +
        "                       dbo.aspnet_UsersInRoles ON dbo.aspnet_Users.UserId = dbo.aspnet_UsersInRoles.UserId AND  " +
        "                       dbo.aspnet_Users.UserId = dbo.aspnet_UsersInRoles.UserId INNER JOIN " +
        "                       dbo.aspnet_Roles ON dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId AND dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId INNER JOIN " +
        "                       dbo.Table_Prj_User_Junction ON dbo.aspnet_Users.UserId = dbo.Table_Prj_User_Junction.UserID " +
        " WHERE     (dbo.Table_Prj_User_Junction.ProjectID = @ProjectID) AND (dbo.aspnet_Roles.LoweredRoleName = N'initiatecontractandaddendum') " +
        " ) as Datasource  " +
        " group by LoweredEmail "

            Dim cmd As New SqlCommand(sqlstring, con)

            cmd.CommandType = System.Data.CommandType.Text

            Dim ProjectID_parameter As SqlParameter = cmd.Parameters.Add("@ProjectID", System.Data.SqlDbType.Int)
            Dim POno_parameter As SqlParameter = cmd.Parameters.Add("@PO_No", System.Data.SqlDbType.NVarChar, 15)

            ProjectID_parameter.Value = ProjectID
            POno_parameter.Value = POno
            Dim dr As SqlDataReader = cmd.ExecuteReader

            While dr.Read
                If dr(0).ToString.Length <> 0 Then
                    mm1.To.Add(dr(0).ToString)
                End If
            End While
            dr.Close()
            con.Close()
            con.Dispose()
        End Using

        ' Remove this after implementing general notification matrix
        If ProjectID = 197 Or ProjectID = 195 Then
            mm1.To.Add("anton.mokrov@mercuryeng.ru")
        End If
        ' Remove this after implementing general notification matrix

        mm1.Bcc.Add("sk@mercuryeng.ru")

        mm1.Subject = Subject
        mm1.Body = Body
        mm1.IsBodyHtml = True

        Dim smtp As New SmtpClient_RussianEncoded
        Try
            smtp.Send(mm1)
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub

    Protected Sub GridViewPoPossibleDublication_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Balance")) = 0 Then
                e.Row.Cells(5).Text = "<span style=" + """" + "background-color: #ffff00; color: #ff0000; font-size: 8pt; font-weight: bold" + """" + ">Closed!</span>"
            End If
        End If
    End Sub

    Protected Sub DropDownListRequestedBy_DataBound(sender As Object, e As System.EventArgs)
        Dim DropDownListRequestedBy As DropDownList = sender
        Dim DropDownPrjID As DropDownList = FormViewPO.FindControl("DropDownListProject")
        Dim lst2 As New ListItem("Select Person", "0")
        DropDownListRequestedBy.Items.Insert(0, lst2)
    End Sub

    Protected Sub CheckBoxPersonSupplier_CheckedChanged(sender As Object, e As EventArgs)

        Dim FormViewSupplier As FormView = FormViewPO.FindControl("FormViewSupplier")
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

    ' I WANTED THIS AS SEPERATE CLASS IN APP_CODE, BUT NOT WORKING

    Protected Function GetVariables(ByVal userName_Scenario As String) As ScenarioVariables

        Dim userName As String = HttpContext.Current.User.Identity.Name.ToLower.ToString
        Dim _return As New ScenarioVariables

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT ProjectID, TotalPrice, VAT, RTRIM(Currency) AS Currency, RTRIM(CostCode) AS CostCode, RTRIM(RequestedBy) AS RequestedBy FROM Table_Scenario WHERE Scenario LIKE '%' + @Scenario + '%' "
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
        End Using

        Return _return

    End Function

    Public Class ScenarioVariables

        Friend ProjectID As Integer
        Friend TotalPrice As Decimal
        Friend VAT As Integer
        Friend Currency As String
        Friend CostCode As String
        Friend RequestedBy As String

    End Class

    Protected Function GetVariablesFromFrameContract(ByVal _UniqueText As String) As FrameContractParameters

        Dim userName As String = HttpContext.Current.User.Identity.Name.ToLower.ToString
        Dim _return As New FrameContractParameters

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT     dbo.Table_FrameContractTickets.ContractID,dbo.Table_Contracts.ProjectID, RTRIM(dbo.Table_Contracts.ContractCurrency) AS ContractCurrency, RTRIM(dbo.Table_Contracts.RequestedBy) AS RequestedBy, dbo.Table_Contracts.SupplierID " +
                                      " FROM         dbo.Table_FrameContractTickets INNER JOIN " +
                                      "              dbo.Table_Contracts ON dbo.Table_FrameContractTickets.ContractID = dbo.Table_Contracts.ContractID " +
                                      " WHERE     (dbo.Table_FrameContractTickets.UniqueText = @UniqueText) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim UniqueText As SqlParameter = cmd.Parameters.Add("@UniqueText", System.Data.SqlDbType.NVarChar)
            UniqueText.Value = _UniqueText
            Dim dr As SqlDataReader = cmd.ExecuteReader

            While dr.Read

                _return.ContractID = dr("ContractID")
                _return.ProjectID = dr("ProjectID")
                _return.SupplierID = dr("SupplierID").ToString
                _return.Currency = dr("ContractCurrency")

                If String.IsNullOrEmpty(dr("RequestedBy").ToString) Then
                    _return.RequestedBy = Nothing
                Else
                    _return.RequestedBy = dr("RequestedBy")
                End If

            End While

            con.Close()
            dr.Close()
            con.Dispose()
        End Using

        Return _return

    End Function

    Public Class FrameContractParameters

        Friend ContractID As Integer
        Friend ProjectID As Integer
        Friend SupplierID As String
        Friend Currency As String
        Friend RequestedBy As String

    End Class

    Protected Sub FormViewSupplier_ItemInserting(sender As Object, e As FormViewInsertEventArgs)

        Dim FormViewSupplier As FormView = FormViewPO.FindControl("FormViewSupplier")
        Dim SupplierIDTextBox As TextBox = FormViewSupplier.FindControl("SupplierIDTextBox")
        Dim LabelSupplierMessage As Label = FormViewSupplier.FindControl("LabelSupplierMessage")
        Dim sqlsupplier As SqlDataSource = FormViewPO.FindControl("SqlDataSourceSupplierEnter")

        Using Adapter As New MercuryTableAdapters.Table6_SupplierTableAdapter
            If Adapter.GetCountBySupplierID(SupplierIDTextBox.Text) > 0 Then
                ' give message that it is dublicating
                LabelSupplierMessage.Visible = True
                e.Cancel = True
            End If

            Adapter.Dispose()

        End Using

    End Sub

End Class

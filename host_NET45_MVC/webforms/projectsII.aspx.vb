Partial Class projectsII
  Inherits System.Web.UI.Page

  Protected Sub GridViewProjects_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewProjects.Load
    If Not IsPostBack Then
      GridViewProjects.Sort("ProjectID", SortDirection.Descending)
    End If
  End Sub

  Protected Sub GridViewProjects_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewProjects.RowCommand

    If (e.CommandName = "Update") Then
      Dim index As Integer = Convert.ToInt32(e.CommandArgument)
      Dim row As GridViewRow = GridViewProjects.Rows(index)
      Dim InsuranceStart As String = ""
      Dim InsuranceFinish As String = ""
      Dim CompletionDate As String = ""
      Dim TextBoxInsuranceStart As TextBox = DirectCast(row.FindControl("TextBoxInsuranceStart"), TextBox)
      Dim TextBoxInsuranceFinish As TextBox = DirectCast(row.FindControl("TextBoxInsuranceFinish"), TextBox)
      Dim TextBoxCompletion As TextBox = DirectCast(row.FindControl("TextBoxCompletion"), TextBox)

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

      If TextBoxCompletion.Text = "" Then
        CompletionDate = " NULL "
      Else
        CompletionDate = "CONVERT(DATETIME, '" + Mid(TextBoxCompletion.Text, 7, 4).ToString + "-" + Mid(TextBoxCompletion.Text, 4, 2).ToString + "-" + Mid(TextBoxCompletion.Text, 1, 2).ToString + " 00:00:00" + "', 102)"
      End If

      SqlDataSourceProjects.UpdateCommand = "UPDATE [Table1_Project] " + _
                 " SET [ProjectID] = " + DirectCast(row.FindControl("TextBoxProjectIDEdit"), TextBox).Text + " " + _
                    " ,[ProjectName] = '" + DirectCast(row.FindControl("TextBoxProjectNameEdit"), TextBox).Text + "' " + _
                    " ,[CurrentStatus] = @CurrentStatus " + _
                    " ,[Type] = @Type " + _
                    " ,[Report] = @Report " + _
                    " ,[BackUpRequired] = @BackUpRequired " + _
                    " ,[POcreate] = @POcreate " + _
                    " ,[CompletionDate] = " + CompletionDate + _
                    " ,[ContractCurrency] = @ContractCurrency " + _
                    " ,[ContractAmount] = @ContractAmount " + _
                    " ,[Margin] = @Margin " + _
                    " ,[InsuranceStart] = " + InsuranceStart + _
                    " ,[InsuranceFinish] = " + InsuranceFinish + _
               " WHERE [ProjectID] = " + LabelKeepProjectID.Text + ""

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
    Dim CheckBoxReportRequiredEdit As CheckBox = DirectCast(e.Row.FindControl("CheckBoxReportRequiredEdit"), CheckBox)
    Dim CheckBoxBackUpRequiredEdit As CheckBox = DirectCast(e.Row.FindControl("CheckBoxBackUpRequiredEdit"), CheckBox)
    Dim CheckBoxPOcreateEdit As CheckBox = DirectCast(e.Row.FindControl("CheckBoxPOcreateEdit"), CheckBox)
    Dim LabelDaySinceLastActionItem As Label = DirectCast(e.Row.FindControl("LabelDaySinceLastActionItem"), Label)
    Dim LabelDaySinceLastActionEdit As Label = DirectCast(e.Row.FindControl("LabelDaySinceLastActionEdit"), Label)
    Dim SqlDataSourceAddendum As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourceAddendum"), SqlDataSource)
    Dim SqlDataSourceForecastEdit As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourceForecastEdit"), SqlDataSource)
    Dim LabelProjectIDItem As Label = DirectCast(e.Row.FindControl("LabelProjectIDItem"), Label)
    Dim TextBoxInsuranceStart As TextBox = DirectCast(e.Row.FindControl("TextBoxInsuranceStart"), TextBox)
    Dim TextBoxInsuranceFinish As TextBox = DirectCast(e.Row.FindControl("TextBoxInsuranceFinish"), TextBox)

    Dim LabelCurrentAmount As Label = DirectCast(e.Row.FindControl("LabelCurrentAmount"), Label)
    Dim DropDownListCurrentAmount As DropDownList = DirectCast(e.Row.FindControl("DropDownListCurrentAmount"), DropDownList)
    Dim SqlDataSourceCurrentAmount As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourceCurrentAmount"), SqlDataSource)

    Dim LabelContractCurrentAmountEdit As Label = DirectCast(e.Row.FindControl("LabelContractCurrentAmountEdit"), Label)
    Dim DropDownListCurrentAmountEdit As DropDownList = DirectCast(e.Row.FindControl("DropDownListCurrentAmountEdit"), DropDownList)
    Dim SqlDataSourceCurrentAmountEdit As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourceCurrentAmountEdit"), SqlDataSource)
    Dim SqlDataSourceTotalCashIn As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourceTotalCashIn"), SqlDataSource)
    Dim SqlDataSourceTotalCashInEdit As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourceTotalCashInEdit"), SqlDataSource)
    Dim DropDownListTotalCashIn As DropDownList = DirectCast(e.Row.FindControl("DropDownListTotalCashIn"), DropDownList)
    Dim DropDownListTotalCashInEdit As DropDownList = DirectCast(e.Row.FindControl("DropDownListTotalCashInEdit"), DropDownList)

    Dim LabelTotalCashIn As Label = DirectCast(e.Row.FindControl("LabelTotalCashIn"), Label)
    Dim LabelTotalCashInEdit As Label = DirectCast(e.Row.FindControl("LabelTotalCashInEdit"), Label)

    Dim SqlDataSourceCashIn As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourceCashIn"), SqlDataSource)

    If LabelDaySinceLastActionItem IsNot Nothing AndAlso Convert.ToInt32(LabelDaySinceLastActionItem.Text) < 0 Then
      LabelDaySinceLastActionItem.Text = "Nothing Yet"
            'e.Row.Cells(8).BackColor = System.Drawing.Color.Silver
        End If

        If LabelDaySinceLastActionEdit IsNot Nothing AndAlso Convert.ToInt32(LabelDaySinceLastActionEdit.Text) < 0 Then
            LabelDaySinceLastActionEdit.Text = "Nothing Yet"
            'e.Row.Cells(8).BackColor = System.Drawing.Color.Silver
        End If

        If Roles.IsUserInRole("CostStaffActive") AndAlso Not Roles.IsUserInRole("admin") Then
            ' update possible, delete impossible
            If LinkButtonDelete IsNot Nothing Then
                LinkButtonDelete.Visible = False
                LinkButtonEdit.Visible = True
            End If
            If TextBoxProjectIDEdit IsNot Nothing Then
                TextBoxProjectIDEdit.Enabled = False
                TextBoxProjectNameEdit.Enabled = False
                CheckBoxCurrentStatusEdit.Enabled = False
                DropDownListProjectType.Enabled = False
                CheckBoxReportRequiredEdit.Enabled = False
                CheckBoxBackUpRequiredEdit.Enabled = False
                CheckBoxPOcreateEdit.Enabled = False
                TextBoxInsuranceStart.Enabled = False
                TextBoxInsuranceFinish.Enabled = False
            End If
        ElseIf Roles.IsUserInRole("CostStaffActive") AndAlso Roles.IsUserInRole("admin") Then
            ' update possible, delete possible
            If LinkButtonDelete IsNot Nothing Then
                LinkButtonDelete.Visible = True
                LinkButtonEdit.Visible = True
            End If
            If TextBoxProjectIDEdit IsNot Nothing Then
                TextBoxProjectIDEdit.Enabled = True
                TextBoxProjectNameEdit.Enabled = True
                CheckBoxCurrentStatusEdit.Enabled = True
                DropDownListProjectType.Enabled = True
                CheckBoxReportRequiredEdit.Enabled = True
                CheckBoxBackUpRequiredEdit.Enabled = True
                CheckBoxPOcreateEdit.Enabled = True
                TextBoxInsuranceStart.Enabled = True
                TextBoxInsuranceFinish.Enabled = True
            End If
        Else
            ' update impossible, delete impossible
            If LinkButtonDelete IsNot Nothing Then
                LinkButtonDelete.Visible = False
                LinkButtonEdit.Visible = False
            End If
        End If

        ' take ProjectID for Update issue
        If TextBoxProjectIDEdit IsNot Nothing Then
            LabelKeepProjectID.Text = TextBoxProjectIDEdit.Text
            SqlDataSourceAddendum.SelectParameters("ProjectID").DefaultValue = Convert.ToInt32(TextBoxProjectIDEdit.Text)
            SqlDataSourceForecastEdit.SelectParameters("ProjectID").DefaultValue = Convert.ToInt32(TextBoxProjectIDEdit.Text)
            SqlDataSourceCurrentAmountEdit.SelectParameters("ProjectID").DefaultValue = Convert.ToInt32(TextBoxProjectIDEdit.Text)
            SqlDataSourceTotalCashInEdit.SelectParameters("ProjectID").DefaultValue = Convert.ToInt32(TextBoxProjectIDEdit.Text)
            SqlDataSourceCashIn.SelectParameters("ProjectID").DefaultValue = Convert.ToInt32(TextBoxProjectIDEdit.Text)
        End If

        ' take ProjectID for ItemTemplate then transfer DDL current amount into label
        If LabelProjectIDItem IsNot Nothing Then
            SqlDataSourceCurrentAmount.SelectParameters("ProjectID").DefaultValue = Convert.ToInt32(LabelProjectIDItem.Text)
            DropDownListCurrentAmount.DataBind()
            If DropDownListCurrentAmount.SelectedItem.Text = "" Then
                LabelCurrentAmount.Text = ""
            Else
                LabelCurrentAmount.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(DropDownListCurrentAmount.SelectedItem.Text))
            End If

            SqlDataSourceTotalCashIn.SelectParameters("ProjectID").DefaultValue = Convert.ToInt32(LabelProjectIDItem.Text)
            DropDownListTotalCashIn.DataBind()
            If DropDownListTotalCashIn.SelectedItem.Text = "" Then
                LabelTotalCashIn.Text = ""
            Else
                LabelTotalCashIn.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(DropDownListTotalCashIn.SelectedItem.Text))
            End If
        End If

        ' In Edit Template it transfers DDL current amount into label
        If LabelContractCurrentAmountEdit IsNot Nothing Then
            DropDownListCurrentAmountEdit.DataBind()
            If DropDownListCurrentAmountEdit.SelectedItem.Text = "" Then
                LabelContractCurrentAmountEdit.Text = ""
            Else
                LabelContractCurrentAmountEdit.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(DropDownListCurrentAmountEdit.SelectedItem.Text))
            End If

            DropDownListTotalCashInEdit.DataBind()
            If DropDownListTotalCashInEdit.SelectedItem.Text = "" Then
                LabelTotalCashInEdit.Text = ""
            Else
                LabelTotalCashInEdit.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(DropDownListTotalCashInEdit.SelectedItem.Text))
            End If

        End If

        ' highlight edit row
        If DropDownListProjectType IsNot Nothing Then
            e.Row.BackColor = System.Drawing.Color.DeepSkyBlue
        End If

    ' put BlueDot if checked
    Dim CheckBoxCurrentStatusItem As CheckBox = DirectCast(e.Row.FindControl("CheckBoxCurrentStatusItem"), CheckBox)
    Dim CheckBoxReportRequiredItem As CheckBox = DirectCast(e.Row.FindControl("CheckBoxReportRequiredItem"), CheckBox)
    Dim CheckBoxBackUpRequiredItem As CheckBox = DirectCast(e.Row.FindControl("CheckBoxBackUpRequiredItem"), CheckBox)
    Dim CheckBoxPOcreateItem As CheckBox = DirectCast(e.Row.FindControl("CheckBoxPOcreateItem"), CheckBox)
    Dim ImageCurrentStatus As Image = DirectCast(e.Row.FindControl("ImageCurrentStatus"), Image)
    Dim ImageReport As Image = DirectCast(e.Row.FindControl("ImageReport"), Image)
    Dim ImageBackUpRequired As Image = DirectCast(e.Row.FindControl("ImageBackUpRequired"), Image)
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

      If CheckBoxPOcreateItem.Checked = True Then
        ImagePOcreate.Visible = True
      Else
        ImagePOcreate.Visible = False
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

  Protected Sub InsertButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    Dim AddendumDate As String = ""
    For Each gvr As GridViewRow In GridViewProjects.Rows
      If gvr.RowType = DataControlRowType.DataRow Then
        Dim SqlDataSourceInsertAddendum As SqlDataSource = DirectCast(gvr.FindControl("SqlDataSourceInsertAddendum"), SqlDataSource)
        Dim FormViewInsertAddendum As FormView = DirectCast(gvr.FindControl("FormViewInsertAddendum"), FormView)

        If FormViewInsertAddendum IsNot Nothing Then
          Dim TextBoxAddendumDate As TextBox = FormViewInsertAddendum.FindControl("TextBoxAddendumDate")
          AddendumDate = "CONVERT(DATETIME, '" + Mid(TextBoxAddendumDate.Text, 7, 4).ToString + "-" + Mid(TextBoxAddendumDate.Text, 4, 2).ToString + "-" + Mid(TextBoxAddendumDate.Text, 1, 2).ToString + " 00:00:00" + "', 102)"
        End If

        If SqlDataSourceInsertAddendum IsNot Nothing Then
          SqlDataSourceInsertAddendum.InsertCommand = "" + _
            " INSERT INTO [Table_ProjectAddendum] " + _
            " ([ProjectID] " + _
            "    , [AddendumDescription] " + _
            "    , [AddendumDate] " + _
            "    , [AddendumAmount]) " + _
            "           VALUES" + _
            " (" + LabelKeepProjectID.Text + " " + _
            "   , @AddendumDescription " + _
            "   , " + AddendumDate + " " + _
            "   , @AddendumAmount)"
        End If
      End If
    Next
  End Sub

  Protected Sub InsertButton_ClickCashIn(ByVal sender As Object, ByVal e As System.EventArgs)
    Dim CashInDate As String = ""
    For Each gvr As GridViewRow In GridViewProjects.Rows
      If gvr.RowType = DataControlRowType.DataRow Then
        Dim SqlDataSourceInsertCashIn As SqlDataSource = DirectCast(gvr.FindControl("SqlDataSourceInsertCashIn"), SqlDataSource)
        Dim FormViewCashIn As FormView = DirectCast(gvr.FindControl("FormViewCashIn"), FormView)

        If FormViewCashIn IsNot Nothing Then
          Dim TextBoxCashInDate As TextBox = FormViewCashIn.FindControl("TextBoxCashInDate")
          CashInDate = "CONVERT(DATETIME, '" + Mid(TextBoxCashInDate.Text, 7, 4).ToString + "-" + Mid(TextBoxCashInDate.Text, 4, 2).ToString + "-" + Mid(TextBoxCashInDate.Text, 1, 2).ToString + " 00:00:00" + "', 102)"
        End If

        If SqlDataSourceInsertCashIn IsNot Nothing Then
          SqlDataSourceInsertCashIn.InsertCommand = "" + _
            " INSERT INTO [Table_ProjectCashIn] " + _
            " ([ProjectID] " + _
            "    , [CashInDescription] " + _
            "    , [CashInDate] " + _
            "    , [CashInAmount]) " + _
            "           VALUES" + _
            " (" + LabelKeepProjectID.Text + " " + _
            "   , @CashInDescription " + _
            "   , " + CashInDate + " " + _
            "   , @CashInAmount)"
        End If
      End If
    Next
  End Sub

    Protected Sub GridViewAddendum_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        If (e.CommandName = "Update") Then
            Dim GridViewAddendum As GridView = CType(sender, GridView)
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridViewAddendum.Rows(index)

            Dim TextBoxAddendumDate As TextBox = DirectCast(row.FindControl("TextBoxAddendumDate"), TextBox)
            LabelKeepAddendumDate.Text = Mid(TextBoxAddendumDate.Text, 7, 4).ToString + "-" + Mid(TextBoxAddendumDate.Text, 4, 2).ToString + "-" + Mid(TextBoxAddendumDate.Text, 1, 2).ToString + " 00:00:00"
        End If
  End Sub

  Protected Sub GridViewCashIn_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
    If (e.CommandName = "Update") Then
      Dim GridViewCashIn As GridView = CType(sender, GridView)
      Dim index As Integer = Convert.ToInt32(e.CommandArgument)
      Dim row As GridViewRow = GridViewCashIn.Rows(index)

      Dim TextBoxCashInDate As TextBox = DirectCast(row.FindControl("TextBoxCashInDate"), TextBox)
      LabelKeepCashInDate.Text = Mid(TextBoxCashInDate.Text, 7, 4).ToString + "-" + Mid(TextBoxCashInDate.Text, 4, 2).ToString + "-" + Mid(TextBoxCashInDate.Text, 1, 2).ToString + " 00:00:00"
    End If
  End Sub

  Protected Sub GridViewAddendum_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)
    e.NewValues("ProjectID") = Convert.ToInt32(LabelKeepProjectID.Text)
    e.NewValues("AddendumDate") = Convert.ToDateTime(LabelKeepAddendumDate.Text)
  End Sub

  Protected Sub GridViewCashIn_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)
    e.NewValues("ProjectID") = Convert.ToInt32(LabelKeepProjectID.Text)
    e.NewValues("CashInDate") = Convert.ToDateTime(LabelKeepCashInDate.Text)
  End Sub


  Protected Sub GridViewForecast_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
    ' doenst required anything
  End Sub

  Protected Sub GridViewForecast_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)
    e.NewValues("ProjectID") = Convert.ToInt32(LabelKeepProjectID.Text)
  End Sub

  Protected Sub InsertButton_Click1(ByVal sender As Object, ByVal e As System.EventArgs)
    For Each gvr As GridViewRow In GridViewProjects.Rows
      If gvr.RowType = DataControlRowType.DataRow Then
        Dim SqlDataSourceInsertForecast As SqlDataSource = DirectCast(gvr.FindControl("SqlDataSourceInsertForecast"), SqlDataSource)

        If SqlDataSourceInsertForecast IsNot Nothing Then
          SqlDataSourceInsertForecast.InsertCommand = "" + _
            " INSERT INTO [Table_ProjectForecast] " + _
            " ([ProjectID] " + _
            "    , [ForecastType] " + _
            "    , [ForecastYearMonth] " + _
            "    , [ForecastAmount]) " + _
            "           VALUES" + _
            " (" + LabelKeepProjectID.Text + " " + _
            "   , @ForecastType " + _
            "   , @ForecastYearMonth " + _
            "   , @ForecastAmount)"
        End If
      End If
    Next
  End Sub

  Protected Sub FormViewInsertAddendum_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertedEventArgs)
    For Each gvr As GridViewRow In GridViewProjects.Rows
      If gvr.RowType = DataControlRowType.DataRow Then
        Dim GridViewAddendum As GridView = DirectCast(gvr.FindControl("GridViewAddendum"), GridView)
        If GridViewAddendum IsNot Nothing Then
          GridViewAddendum.DataBind()
        End If

        ' In Edit Template it rebind and transfers DDL current amount into label
        Dim LabelContractCurrentAmountEdit As Label = DirectCast(gvr.FindControl("LabelContractCurrentAmountEdit"), Label)
        Dim DropDownListCurrentAmountEdit As DropDownList = DirectCast(gvr.FindControl("DropDownListCurrentAmountEdit"), DropDownList)
        If LabelContractCurrentAmountEdit IsNot Nothing Then
          DropDownListCurrentAmountEdit.DataBind()
          If DropDownListCurrentAmountEdit.SelectedItem.Text = "" Then
            LabelContractCurrentAmountEdit.Text = ""
          Else
            LabelContractCurrentAmountEdit.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(DropDownListCurrentAmountEdit.SelectedItem.Text))
          End If
        End If
      End If
    Next
  End Sub

  Protected Sub FormViewInsertCashIn_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertedEventArgs)
    For Each gvr As GridViewRow In GridViewProjects.Rows
      If gvr.RowType = DataControlRowType.DataRow Then
        Dim GridViewCashIn As GridView = DirectCast(gvr.FindControl("GridViewCashIn"), GridView)
        If GridViewCashIn IsNot Nothing Then
          GridViewCashIn.DataBind()
        End If

        ' In Edit Template it rebind and transfers DDL current amount into label
        Dim LabelTotalCashInEdit As Label = DirectCast(gvr.FindControl("LabelTotalCashInEdit"), Label)
        Dim DropDownListTotalCashInEdit As DropDownList = DirectCast(gvr.FindControl("DropDownListTotalCashInEdit"), DropDownList)
        If LabelTotalCashInEdit IsNot Nothing Then
          DropDownListTotalCashInEdit.DataBind()
          If DropDownListTotalCashInEdit.SelectedItem.Text = "" Then
            LabelTotalCashInEdit.Text = ""
          Else
            LabelTotalCashInEdit.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(DropDownListTotalCashInEdit.SelectedItem.Text))
          End If
        End If
      End If
    Next
  End Sub

  Protected Sub FormViewInsertForecast_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertedEventArgs)
    For Each gvr As GridViewRow In GridViewProjects.Rows
      If gvr.RowType = DataControlRowType.DataRow Then
        Dim GridViewForecast As GridView = DirectCast(gvr.FindControl("GridViewForecast"), GridView)
        If GridViewForecast IsNot Nothing Then
          GridViewForecast.DataBind()
        End If
      End If
    Next
  End Sub

  Protected Sub GridViewAddendum_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs)
    For Each gvr As GridViewRow In GridViewProjects.Rows
      If gvr.RowType = DataControlRowType.DataRow Then
        ' In Edit Template it rebind and transfers DDL current amount into label
        Dim LabelContractCurrentAmountEdit As Label = DirectCast(gvr.FindControl("LabelContractCurrentAmountEdit"), Label)
        Dim DropDownListCurrentAmountEdit As DropDownList = DirectCast(gvr.FindControl("DropDownListCurrentAmountEdit"), DropDownList)
        If LabelContractCurrentAmountEdit IsNot Nothing Then
          DropDownListCurrentAmountEdit.DataBind()
          If DropDownListCurrentAmountEdit.SelectedItem.Text = "" Then
            LabelContractCurrentAmountEdit.Text = ""
          Else
            LabelContractCurrentAmountEdit.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(DropDownListCurrentAmountEdit.SelectedItem.Text))
          End If
        End If
      End If
    Next
  End Sub

  Protected Sub GridViewCashIn_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs)
    For Each gvr As GridViewRow In GridViewProjects.Rows
      If gvr.RowType = DataControlRowType.DataRow Then
        ' In Edit Template it rebind and transfers DDL current amount into label
        Dim LabelTotalCashInEdit As Label = DirectCast(gvr.FindControl("LabelTotalCashInEdit"), Label)
        Dim DropDownListTotalCashInEdit As DropDownList = DirectCast(gvr.FindControl("DropDownListTotalCashInEdit"), DropDownList)
        If LabelTotalCashInEdit IsNot Nothing Then
          DropDownListTotalCashInEdit.DataBind()
          If DropDownListTotalCashInEdit.SelectedItem.Text = "" Then
            LabelTotalCashInEdit.Text = ""
          Else
            LabelTotalCashInEdit.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(DropDownListTotalCashInEdit.SelectedItem.Text))
          End If
        End If
      End If
    Next
  End Sub

  Protected Sub GridViewCashIn_RowUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdatedEventArgs)
    For Each gvr As GridViewRow In GridViewProjects.Rows
      If gvr.RowType = DataControlRowType.DataRow Then
        ' In Edit Template it rebind and transfers DDL current amount into label
        Dim LabelTotalCashInEdit As Label = DirectCast(gvr.FindControl("LabelTotalCashInEdit"), Label)
        Dim DropDownListTotalCashInEdit As DropDownList = DirectCast(gvr.FindControl("DropDownListTotalCashInEdit"), DropDownList)
        If LabelTotalCashInEdit IsNot Nothing Then
          DropDownListTotalCashInEdit.DataBind()
          If DropDownListTotalCashInEdit.SelectedItem.Text = "" Then
            LabelTotalCashInEdit.Text = ""
          Else
            LabelTotalCashInEdit.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(DropDownListTotalCashInEdit.SelectedItem.Text))
          End If
        End If
      End If
    Next
  End Sub

  Protected Sub GridViewAddendum_RowUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdatedEventArgs)
    For Each gvr As GridViewRow In GridViewProjects.Rows
      If gvr.RowType = DataControlRowType.DataRow Then
        ' In Edit Template it rebind and transfers DDL current amount into label
        Dim LabelContractCurrentAmountEdit As Label = DirectCast(gvr.FindControl("LabelContractCurrentAmountEdit"), Label)
        Dim DropDownListCurrentAmountEdit As DropDownList = DirectCast(gvr.FindControl("DropDownListCurrentAmountEdit"), DropDownList)
        If LabelContractCurrentAmountEdit IsNot Nothing Then
          DropDownListCurrentAmountEdit.DataBind()
          If DropDownListCurrentAmountEdit.SelectedItem.Text = "" Then
            LabelContractCurrentAmountEdit.Text = ""
          Else
            LabelContractCurrentAmountEdit.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(DropDownListCurrentAmountEdit.SelectedItem.Text))
          End If
        End If
      End If
    Next
  End Sub

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If IsPostBack Or Not IsPostBack Then
      ' it provides query parameter for DropDownListProject
      TextBoxUserName.Text = Page.User.Identity.Name
    End If
  End Sub

  Protected Sub DropDownListProject_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListProject.DataBound
    Dim lst1 As New ListItem("SELECT PROJECT", String.Empty)
    DropDownListProject.Items.Insert(0, lst1)
  End Sub
End Class

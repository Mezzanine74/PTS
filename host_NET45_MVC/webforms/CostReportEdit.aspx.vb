Imports System.Data.SqlClient
Partial Class CostReportEdit264
  Inherits System.Web.UI.Page

  Dim LeadingCostDisipline As String = ""
  Dim ColorShifter As Integer = 0

  Dim Budget As Decimal
  Dim BudgetCO As Decimal
  Dim PlannedToSpend As Decimal = 0
  Dim PlannedToSpendCO As Decimal = 0
  Dim Revenue As Decimal = 0


  Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

  End Sub

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    If IsPostBack Or Not IsPostBack Then
      If Not User.IsInRole("CostStaffActive") Then
                Response.Redirect("~/webforms/AccessDenied.aspx")
        Exit Sub
      End If
    End If


    If Not IsPostBack Then
      If Request.QueryString("ProjectID") IsNot Nothing AndAlso Request.QueryString("Currency") IsNot Nothing Then
        If Convert.ToInt32(Request.QueryString("ProjectID").ToString) = 0 Then
          ' do nothing
        Else
          DropDownListPrj.SelectedValue = Convert.ToInt32(Request.QueryString("ProjectID").ToString)
        End If
        DropDownListCurrency.SelectedValue = Request.QueryString("Currency").ToString
        DropDownListCostCode.DataBind()
      End If
    End If


    ' it provides query parameter for DropDownListSupplier
    If IsPostBack Or Not IsPostBack Then
      TextBoxUserName.Text = Page.User.Identity.Name
    End If

    ' it provides datasource for DDLcostCode depends on project type - datacenter or fitout
    If IsPostBack Or Not IsPostBack Then
            Using conCostCode As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                conCostCode.Open()
                Dim sqlstring As String = " SELECT     RTRIM(Type) AS Type" + _
                                                        " FROM Table1_Project" + _
                                                        " WHERE     ProjectID =" + "'" + DropDownListPrj.SelectedValue.ToString + "'"
                Dim cmdCostCode As New SqlCommand(sqlstring, conCostCode)
                cmdCostCode.CommandType = System.Data.CommandType.Text
                Dim drCostCode As SqlDataReader = cmdCostCode.ExecuteReader
                While drCostCode.Read
                    Dim ProjectType As String
                    ProjectType = drCostCode(0).ToString()
                    If ProjectType = "DataCenter" Then
                        SqlDataSourceCostCode.SelectCommand = " SELECT     TOP (100) PERCENT RTRIM(CostCode) AS CostCode, RTRIM(CostCode) + REPLICATE(CHAR(160), 12 - LEN(CostCode)) + RTRIM(CodeDescription)  " +
                                                    " AS CostCode_Description " +
                                                    " FROM         dbo.Table7_CostCode " +
                                                    " WHERE     (Type = 'DataCenter') " +
                                                    " ORDER BY [CostCode] ASC "
                    ElseIf ProjectType = "FitOut" Then
                        SqlDataSourceCostCode.SelectCommand = " SELECT * " +
                                                    " FROM " +
                                                    " (SELECT * FROM " +
                                                    "  (SELECT TOP 1 RTRIM([CostCode]) AS CostCode " +
                                                    " ,rtrim(CostCode) + replicate(char(160),12-len(CostCode)) + RTRIM(CodeDescription) AS CostCode_Description " +
                                                    " FROM [dbo].[Table7_CostCode] " +
                                                    " ORDER BY [CostCode] ASC) AS A " +
                                                    " UNION ALL " +
                                                    " SELECT * FROM " +
                                                    " (SELECT     TOP (100) PERCENT RTRIM(CostCode) AS CostCode, RTRIM(CostCode) + REPLICATE(CHAR(160), 12 - LEN(CostCode)) + RTRIM(CodeDescription)  " +
                                                    " AS CostCode_Description " +
                                                    " FROM         dbo.Table7_CostCode " +
                                                    " WHERE     (Type = 'FitOut')) AS B) AS C " +
                                                    " ORDER BY [CostCode] ASC "
                    End If
                End While
                conCostCode.Close()
                drCostCode.Close()
                conCostCode.Dispose()

            End Using

            ' it controls if CostCode is valid in case of Datacenter projects
            Using conCostCodeValidation As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                conCostCodeValidation.Open()
                Dim sqlstringCostCodeValidation As String = " SELECT     RTRIM(Type) AS Type" +
                                                        " FROM Table1_Project" +
                                                        " WHERE     ProjectID =" + "'" + DropDownListPrj.SelectedValue.ToString + "'"
                Dim cmdCostCodeValidation As New SqlCommand(sqlstringCostCodeValidation, conCostCodeValidation)
                cmdCostCodeValidation.CommandType = System.Data.CommandType.Text
                Dim drCostCodeValidation As SqlDataReader = cmdCostCodeValidation.ExecuteReader
                While drCostCodeValidation.Read
                    Dim ProjectTypeCostCodeValidation As String
                    ProjectTypeCostCodeValidation = drCostCodeValidation(0).ToString()
                    If ProjectTypeCostCodeValidation = "DataCenter" Then
                        If DropDownListCostCode.SelectedValue.ToString = "0" Then
                            labelCostCodeValidate.Text = ""
                        ElseIf DropDownListCostCode.SelectedValue.ToString <> "-" AndAlso Len(DropDownListCostCode.SelectedValue.ToString) < 10 Then
                            ' it will check if costcode exist to match. If so, then validation will start.
                            Using conCostCodeValidation2 As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                                conCostCodeValidation2.Open()
                                Dim sqlstringCostCodeValidation2 As String
                                sqlstringCostCodeValidation2 = SqlDataSourceCostCode.SelectCommand.ToString

                                Dim cmdCostCodeValidation2 As New SqlCommand(sqlstringCostCodeValidation2, conCostCodeValidation2)
                                cmdCostCodeValidation2.CommandType = System.Data.CommandType.Text
                                Dim drCostCodeValidation2 As SqlDataReader = cmdCostCodeValidation2.ExecuteReader
                                While drCostCodeValidation2.Read
                                    If drCostCodeValidation2(0).ToString = DropDownListCostCode.SelectedValue.ToString Then
                                        labelCostCodeValidate.Text = "Cost Code must be 10 characters long"
                                    End If
                                End While
                                conCostCodeValidation2.Close()
                                conCostCodeValidation2.Dispose()
                                drCostCodeValidation2.Close()

                            End Using
                        ElseIf Len(DropDownListCostCode.SelectedValue.ToString) = 10 Then
                            labelCostCodeValidate.Text = ""
                        End If
                    Else
                        labelCostCodeValidate.Text = ""
                    End If
                End While
                conCostCodeValidation.Close()
                drCostCodeValidation.Close()
                conCostCodeValidation.Dispose()

            End Using

            LabelCostCodeAddSelectItemDecision.Text = DropDownListCostCode.SelectedValue.ToString
            SqlDataSourceCostCode.DataBind()
            DropDownListCostCode.DataBind()
        End If

    End Sub

    Protected Sub GridViewBudgetEuro_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewBudgetEuro.RowCommand
        If (e.CommandName = "ActionUpdate") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridViewBudgetEuro.Rows(index)
            If TextBoxBudget.Text = "" Then TextBoxBudget.Text = 0
            If TextBoxPlanned.Text = "" Then TextBoxPlanned.Text = 0

            If DDLInsertOrUpdate.Items.Count = 0 Then
                If DirectCast(row.FindControl("TextBoxBudget"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxBudget"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxVCO"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxVCO"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxPlannedToSpend"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxPlannedToSpend"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxPlannedToSpendCO"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxPlannedToSpendCO"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text = 0
                SqlDataSourceBudgetInsert.InsertCommand = "INSERT INTO Table_Budget (ProjectID, CostCode, Budget, PlannedToSpend, Currency, UpdatedPlannedRevenue, VCO, PlannedToSpendCO) VALUES (" +
          "'" + DropDownListPrj.SelectedValue + "'" + "," + "'" + DirectCast(row.FindControl("LabelCostCodeEdit"), Label).Text + "'" +
          "," + DirectCast(row.FindControl("TextBoxBudget"), TextBox).Text +
          "," + DirectCast(row.FindControl("TextBoxPlannedToSpend"), TextBox).Text +
          "," + "'" + DropDownListCurrency.SelectedValue.ToString + "'" +
          "," + DirectCast(row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text +
          "," + DirectCast(row.FindControl("TextBoxVCO"), TextBox).Text +
          "," + DirectCast(row.FindControl("TextBoxPlannedToSpendCO"), TextBox).Text + ")"
                SqlDataSourceBudgetInsert.Insert()
                sender.EditIndex = -1
                sender.DataBind()
            Else
                If DirectCast(row.FindControl("TextBoxBudget"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxBudget"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxVCO"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxVCO"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxPlannedToSpend"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxPlannedToSpend"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxPlannedToSpendCO"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxPlannedToSpendCO"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text = 0
                SqlDataSourceBudgetEuro.UpdateCommand = "UPDATE Table_Budget SET ProjectID =" + "'" + DropDownListPrj.SelectedValue + "'" +
          "," + "CostCode =" + "'" + DirectCast(row.FindControl("LabelCostCodeEdit"), Label).Text + "'" +
          "," + " Budget =" + DirectCast(row.FindControl("TextBoxBudget"), TextBox).Text +
          "," + " VCO =" + DirectCast(row.FindControl("TextBoxVCO"), TextBox).Text +
          "," + " PlannedToSpend= " + DirectCast(row.FindControl("TextBoxPlannedToSpend"), TextBox).Text +
          "," + " PlannedToSpendCO= " + DirectCast(row.FindControl("TextBoxPlannedToSpendCO"), TextBox).Text +
          "," + " Currency= " + "'" + DropDownListCurrency.SelectedValue.ToString + "'" +
          "," + " UpdatedPlannedRevenue= " + DirectCast(row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text +
          " WHERE (ProjectID =" + "'" + DropDownListPrj.SelectedValue + "'" +
          ") AND (CostCode=" + "'" + DirectCast(row.FindControl("LabelCostCodeEdit"), Label).Text + "'" + ")" +
          " AND (" + " Currency= " + "'" + DropDownListCurrency.SelectedValue.ToString + "'" + " )"
                SqlDataSourceBudgetEuro.Update()
                sender.EditIndex = -1
                sender.DataBind()
            End If

            Dim ProgressPercent As String = ""
            If DirectCast(row.FindControl("TextBoxCompletionPercent"), TextBox).Text = "" Then
                ProgressPercent = "0"
            Else
                ProgressPercent = DirectCast(row.FindControl("TextBoxCompletionPercent"), TextBox).Text
            End If

            'If LabelProgressExistOrNot.Text = "True" Then
            '  ' use updateCommand
            '          Using con22 As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            '              con22.Open()
            '              Dim sqlstring22 As String = " UPDATE [Table_ProgressPercent] " + _
            '                                          " SET [ProjectID] =  " + "'" + DropDownListPrj.SelectedValue + "'" + _
            '                                          " ,[CostCode] =  " + "'" + DirectCast(row.FindControl("LabelCostCodeEdit"), Label).Text + "'" + _
            '                                          " ,[CompletionPercent] =  " + ProgressPercent + _
            '                                        " where ([ProjectID] = N'" + DropDownListPrj.SelectedValue + "') AND ([CostCode] = N'" + DirectCast(row.FindControl("LabelCostCodeEdit"), Label).Text + "') "
            '              Dim cmd22 As New SqlCommand(sqlstring22, con22)
            '              cmd22.CommandType =System.Data.CommandType.Text
            '              Dim dr22 As SqlDataReader = cmd22.ExecuteReader
            '              con22.Close()
            '              dr22.Close()
            '              con22.Dispose()

            '          End Using
            '      ElseIf LabelProgressExistOrNot.Text = "False" Then
            '          ' use insertCommand
            '          Using con33 As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            '              con33.Open()
            '              Dim sqlstring33 As String = " INSERT INTO [Table_ProgressPercent] " + _
            '                                          " ([ProjectID] " + _
            '                                          " ,[CostCode] " + _
            '                                          " ,[CompletionPercent]) " + _
            '                                          " VALUES " + _
            '                                          " (" + "'" + DropDownListPrj.SelectedValue + "'" + "," + "'" + DirectCast(row.FindControl("LabelCostCodeEdit"), Label).Text + "'" + "," + ProgressPercent + ") "
            '              Dim cmd33 As New SqlCommand(sqlstring33, con33)
            '              cmd33.CommandType =System.Data.CommandType.Text
            '              Dim dr33 As SqlDataReader = cmd33.ExecuteReader
            '              con33.Close()
            '              dr33.Close()
            '              con33.Dispose()

            '          End Using
            '      End If
            GridViewBudgetEuro.DataBind()
        End If

        If (e.CommandName = "ActionDelete") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridViewBudgetEuro.Rows(index)
            SqlDataSourceBudgetEuro.DeleteCommand = "DELETE FROM Table_Budget WHERE (ProjectID =" + "'" + DropDownListPrj.SelectedValue + "'" + ") AND (" + "CostCode =" + "'" + DirectCast(row.FindControl("LabelCostCodeItem"), Label).Text + "'" + ")" + " AND (" + " Currency= " + "'" + DropDownListCurrency.SelectedValue.ToString + "'" + " )"
            SqlDataSourceBudgetEuro.Delete()
            GridViewBudgetEuro.EditIndex = -1

            ' check if this PrjID and CostCodes exist for another curreny or not
            Using con44 As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con44.Open()
                Dim sqlstring44 As String = " SELECT count([BudgetID]) FROM [Table_Budget] " +
                                          " where ([ProjectID] = " + DropDownListPrj.SelectedValue + ") AND " +
                                          " ([CostCode] = N'" + DirectCast(row.FindControl("LabelCostCodeItem"), Label).Text + "') AND " +
                                          " ([Currency] <> " + "N'" + DropDownListCurrency.SelectedValue.ToString + "')"
                Dim cmd44 As New SqlCommand(sqlstring44, con44)
                cmd44.CommandType = System.Data.CommandType.Text
                Dim dr44 As SqlDataReader = cmd44.ExecuteReader
                While dr44.Read
                    If dr44(0).ToString = "0" Then
                        LabelCostCodePrjIDExistOrNotForOtherCurreny.Text = "False"
                    Else
                        LabelCostCodePrjIDExistOrNotForOtherCurreny.Text = "True"
                    End If
                End While
                con44.Close()
                dr44.Close()
                con44.Dispose()

            End Using

            ' delete ProgressPercent if there is no ProgressPercent for other currenies under this 
            If LabelCostCodePrjIDExistOrNotForOtherCurreny.Text = "False" Then
                Using con66 As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                    con66.Open()
                    Dim sqlstring66 As String = " DELETE [Table_ProgressPercent] " +
                    " where ([ProjectID] = N'" + DropDownListPrj.SelectedValue + "') AND ([CostCode] = N'" + DirectCast(row.FindControl("LabelCostCodeItem"), Label).Text + "') "
                    Dim cmd66 As New SqlCommand(sqlstring66, con66)
                    cmd66.CommandType = System.Data.CommandType.Text
                    Dim dr66 As SqlDataReader = cmd66.ExecuteReader
                    con66.Close()
                    dr66.Close()
                    con66.Dispose()

                End Using
            End If
            GridViewBudgetEuro.DataBind()

        End If

    End Sub

    Protected Sub GridViewBudgetEuro_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewBudgetEuro.RowDataBound

        Dim MyTask As New MyCommonTasks
        MyTask.HoverEffectOnGridviewCells(sender, e.Row)

        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowIndex = 0 Then
                ColorShifter = 0
                Budget = 0
                BudgetCO = 0
                PlannedToSpend = 0
                PlannedToSpendCO = 0

                LeadingCostDisipline = DataBinder.Eval(e.Row.DataItem, "CostVidisionID").ToString
                e.Row.BackColor = System.Drawing.Color.White
            Else
                If DataBinder.Eval(e.Row.DataItem, "CostVidisionID").ToString = LeadingCostDisipline Then
                    If ColorShifter Mod 2 = 0 Then
                        e.Row.BackColor = System.Drawing.Color.White
                    Else
                        e.Row.BackColor = System.Drawing.Color.Gainsboro
                    End If
                ElseIf DataBinder.Eval(e.Row.DataItem, "CostVidisionID").ToString <> LeadingCostDisipline Then
                    ColorShifter = ColorShifter + 1
                    If ColorShifter Mod 2 = 0 Then
                        e.Row.BackColor = System.Drawing.Color.White
                    Else
                        e.Row.BackColor = System.Drawing.Color.Gainsboro
                    End If
                    LeadingCostDisipline = DataBinder.Eval(e.Row.DataItem, "CostVidisionID").ToString
                End If
            End If
        End If

        If DirectCast(e.Row.FindControl("TextBoxBudget"), TextBox) IsNot Nothing Then
            ' check ProgressPercent if any progress exist or not.
            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = " SELECT count([Id]) FROM [Table_ProgressPercent] " +
                                          " where ([ProjectID] = " + DropDownListPrj.SelectedValue + ") AND ([CostCode] = N'" + DirectCast(e.Row.FindControl("LabelCostCodeEdit"), Label).Text + "') "
                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.Text
                Dim dr As SqlDataReader = cmd.ExecuteReader
                While dr.Read
                    If dr(0).ToString = "0" Then
                        LabelProgressExistOrNot.Text = "False"
                    Else
                        LabelProgressExistOrNot.Text = "True"
                    End If
                    'Response.Write(ProgressExist.ToString)
                End While
                con.Close()
                dr.Close()
                con.Dispose()

            End Using

        End If

        If DirectCast(e.Row.FindControl("LabelDeletePossibleItem"), Label) IsNot Nothing AndAlso DirectCast(e.Row.FindControl("LabelDeletePossibleItem"), Label).Text = 1 Then
            DirectCast(e.Row.FindControl("LinkButtonDeleteItem"), LinkButton).Visible = True
        ElseIf DirectCast(e.Row.FindControl("LabelDeletePossibleItem"), Label) IsNot Nothing AndAlso DirectCast(e.Row.FindControl("LabelDeletePossibleItem"), Label).Text = 0 Then
            DirectCast(e.Row.FindControl("LinkButtonDeleteItem"), LinkButton).Visible = False
        End If

        '////////////////////////// EDIT MODE CALCULATION
        If DirectCast(e.Row.FindControl("TextBoxBudget"), TextBox) IsNot Nothing Then
            Budget = Budget + Convert.ToDecimal((DirectCast(e.Row.FindControl("TextBoxBudget"), TextBox).Text))
        End If

        If DirectCast(e.Row.FindControl("TextBoxVCO"), TextBox) IsNot Nothing Then
            BudgetCO = BudgetCO + Convert.ToDecimal((DirectCast(e.Row.FindControl("TextBoxVCO"), TextBox).Text))
        End If

        If DirectCast(e.Row.FindControl("TextBoxPlannedToSpend"), TextBox) IsNot Nothing Then
            PlannedToSpend = PlannedToSpend + Convert.ToDecimal((DirectCast(e.Row.FindControl("TextBoxPlannedToSpend"), TextBox).Text))
        End If

        If DirectCast(e.Row.FindControl("TextBoxPlannedToSpendCO"), TextBox) IsNot Nothing Then
            PlannedToSpendCO = PlannedToSpendCO + Convert.ToDecimal((DirectCast(e.Row.FindControl("TextBoxPlannedToSpendCO"), TextBox).Text))
        End If


        If DirectCast(e.Row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox) IsNot Nothing Then
            If Len(DirectCast(e.Row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text) > 0 Then
                Revenue = Revenue + Convert.ToDecimal((DirectCast(e.Row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text))
            End If
        End If

        ' ///////////////////ITEM MODE Calculation

        If DirectCast(e.Row.FindControl("HyperLinkCostCodeComments"), HyperLink) IsNot Nothing Then
            ' provide hyperlink for user comments on costCode
            'DirectCast(e.Row.FindControl("HyperLinkCostCodeComments"), HyperLink).ImageUrl = "~/Images/comment_costcode.png"
            'DirectCast(e.Row.FindControl("HyperLinkCostCodeComments"), HyperLink).Text = "Notes"

            Dim QueryStrings As String = "?ProjectID=" + DropDownListPrj.SelectedValue.ToString + "&CostCode=" + DataBinder.Eval(e.Row.DataItem, "CostCode").ToString + "&Currency=Euro"
            DirectCast(e.Row.FindControl("HyperLinkCostCodeComments"), HyperLink).Attributes.Add("onclick", "javascript:w= window.open('commentsOncostReport.aspx" + QueryStrings + "','CostCodeComments','left=100,top=100,width=600,height=600,toolbar=0,resizable=0,scrollbars=yes');")

        End If

        If DirectCast(e.Row.FindControl("LabelBudget"), Label) IsNot Nothing Then
            Budget = Budget + Convert.ToDecimal(Replace(DirectCast(e.Row.FindControl("LabelBudget"), Label).Text, ",", ""))
        End If

        If DirectCast(e.Row.FindControl("LabelVCO"), Label) IsNot Nothing Then
            BudgetCO = BudgetCO + Convert.ToDecimal(Replace(DirectCast(e.Row.FindControl("LabelVCO"), Label).Text, ",", ""))
        End If

        If DirectCast(e.Row.FindControl("LabelPlannedToSpend"), Label) IsNot Nothing Then
            PlannedToSpend = PlannedToSpend + Convert.ToDecimal(Replace(DirectCast(e.Row.FindControl("LabelPlannedToSpend"), Label).Text, ",", ""))
        End If

        If DirectCast(e.Row.FindControl("LabelPlannedToSpendCO"), Label) IsNot Nothing Then
            PlannedToSpendCO = PlannedToSpendCO + Convert.ToDecimal(Replace(DirectCast(e.Row.FindControl("LabelPlannedToSpendCO"), Label).Text, ",", ""))
        End If

        If DirectCast(e.Row.FindControl("LabelUpdatedPlannedRevenue"), Label) IsNot Nothing Then
            Revenue = Revenue + Convert.ToDecimal(Replace(DirectCast(e.Row.FindControl("LabelUpdatedPlannedRevenue"), Label).Text, ",", ""))
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            If DirectCast(e.Row.FindControl("LabelBudget"), Label) IsNot Nothing Then
                DirectCast(e.Row.FindControl("LabelBudget"), Label).Text = Format(Budget, "Standard")
            End If

            If DirectCast(e.Row.FindControl("LabelVCO"), Label) IsNot Nothing Then
                DirectCast(e.Row.FindControl("LabelVCO"), Label).Text = Format(BudgetCO, "Standard")
            End If

            If DirectCast(e.Row.FindControl("LabelPlannedToSpend"), Label) IsNot Nothing Then
                DirectCast(e.Row.FindControl("LabelPlannedToSpend"), Label).Text = Format(PlannedToSpend, "Standard")
            End If

            If DirectCast(e.Row.FindControl("LabelPlannedToSpendCO"), Label) IsNot Nothing Then
                DirectCast(e.Row.FindControl("LabelPlannedToSpendCO"), Label).Text = Format(PlannedToSpendCO, "Standard")
            End If

            If DirectCast(e.Row.FindControl("LabelUpdatedPlannedRevenue"), Label) IsNot Nothing Then
                DirectCast(e.Row.FindControl("LabelUpdatedPlannedRevenue"), Label).Text = Format(Revenue, "Standard")
            End If

        End If

        'To identify INSERT or UPDATE required
        If DirectCast(e.Row.FindControl("LabelCostCodeEdit"), Label) IsNot Nothing Then
            SqlDataSourceUpdateOrInsert.SelectCommand = "SELECT ProjectID  ,CostCode FROM Table_Budget WHERE (ProjectID=" + "'" + DropDownListPrj.SelectedValue + "') AND (CostCode=" + "'" + DirectCast(e.Row.FindControl("LabelCostCodeEdit"), Label).Text + "') " + " AND (" + " Currency= " + "'" + DropDownListCurrency.SelectedValue.ToString + "'" + " )"
            SqlDataSourceUpdateOrInsert.DataBind()
            DDLInsertOrUpdate.DataBind()
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim TextBoxBudget As TextBox = DirectCast(e.Row.FindControl("TextBoxBudget"), TextBox)

            If TextBoxBudget IsNot Nothing Then
                If Page.User.Identity.Name.ToLower = "savas" Then
                    TextBoxBudget.ReadOnly = False
                Else
                    TextBoxBudget.ReadOnly = True
                End If
            End If

        End If

        Dim cbxBudgetId As CheckBox = DirectCast(e.Row.FindControl("cbxBudgetId"), CheckBox)
        Dim LabelBudgetId As Label = DirectCast(e.Row.FindControl("LabelBudgetId"), Label)

        If cbxBudgetId IsNot Nothing And LabelBudgetId IsNot Nothing Then
            If LabelBudgetId.Text.Trim.Length > 0 Then
                If PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table_Budget_PlannedToSpendConstraints.IfExist(LabelBudgetId.Text) Then
                    cbxBudgetId.Checked = True
                Else
                    cbxBudgetId.Checked = False
                End If
            End If
        End If

    End Sub

    Protected Sub GridViewBudgetEuro_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewBudgetEuro.Load
        If Not User.IsInRole("CostStaffActive") Then
            GridViewBudgetEuro.Columns(2).Visible = False
        End If
    End Sub

    Protected Sub ButtonNewBudget_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonNewBudget.Click
        If TextBoxBudget.Text = "" Then TextBoxBudget.Text = 0
        If TextBoxPlanned.Text = "" Then TextBoxPlanned.Text = 0
        SqlDataSourceBudgetInsert.InsertCommand = "INSERT INTO Table_Budget (ProjectID, CostCode, Budget, PlannedToSpend, Currency) VALUES (" + "'" + DropDownListPrj.SelectedValue + "'" + "," + "'" + DropDownListCostCode.SelectedValue + "'" + "," + TextBoxBudget.Text + "," + TextBoxPlanned.Text + "," + "'" + DropDownListCurrency.SelectedValue.ToString + "'" + ")"
        SqlDataSourceBudgetInsert.Insert()
        ' rebind all currencies
        GridViewBudgetEuro.DataBind()
        GridViewBudgetDollar.DataBind()
        GridViewBudgetRuble.DataBind()
    End Sub

    Protected Sub GridViewBudgetEuro_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewBudgetEuro.RowCreated
        If IsPostBack Or Not IsPostBack Then
            If e.Row.RowType = DataControlRowType.Header Then
                If Not User.IsInRole("CostStaffActive") Then
                    'AddHeaderRow_GridPassive(GridViewBudgetEuro)
                Else
                    'AddHeaderRow_GridActive(GridViewBudgetEuro)
                End If
            End If
        End If
    End Sub

    Protected Sub DropDownListPrj_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.DataBound
        If Not IsPostBack Then
            Dim lst1 As New ListItem("Select Project", "0")
            Me.DropDownListPrj.Items.Insert(0, lst1)
        End If
    End Sub

    Protected Sub DropDownListPrj_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.SelectedIndexChanged
        ' Euro binds
        SqlDataSourceBudgetEuro.DataBind()
        GridViewBudgetEuro.DataBind()
        ' Dollar binds
        SqlDataSourceBudgetDollar.DataBind()
        GridViewBudgetDollar.DataBind()
        ' Ruble binds
        SqlDataSourceBudgetRuble.DataBind()
        GridViewBudgetRuble.DataBind()
        ' it resets Budget and Planned textboxes just in case
        TextBoxBudget.Text = ""
        TextBoxPlanned.Text = ""
    End Sub

    Protected Sub DropDownListCurrency_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListCurrency.SelectedIndexChanged
        ' Euro binds
        SqlDataSourceBudgetEuro.DataBind()
        GridViewBudgetEuro.DataBind()
        ' Dollar binds
        SqlDataSourceBudgetDollar.DataBind()
        GridViewBudgetDollar.DataBind()
        ' Ruble binds
        SqlDataSourceBudgetRuble.DataBind()
        GridViewBudgetRuble.DataBind()
        ' it resets Budget and Planned textboxes just in case
        TextBoxBudget.Text = ""
        TextBoxPlanned.Text = ""
    End Sub
    Protected Sub SqlDataSourceBudgetEuro_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles SqlDataSourceBudgetEuro.Selecting
        If IsPostBack Or Not IsPostBack Then
            If Not DropDownListCurrency.SelectedValue.Equals("Euro") Then
                e.Cancel = True
            Else
                e.Command.CommandTimeout = 120
                e.Cancel = False
            End If
        End If
    End Sub

    Protected Sub SqlDataSourceBudgetDollar_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles SqlDataSourceBudgetDollar.Selecting
        If IsPostBack Or Not IsPostBack Then
            If Not DropDownListCurrency.SelectedValue.Equals("Dollar") Then
                e.Cancel = True
            Else
                e.Command.CommandTimeout = 120
                e.Cancel = False
            End If
        End If
    End Sub

    Protected Sub SqlDataSourceBudgetRuble_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles SqlDataSourceBudgetRuble.Selecting
        If IsPostBack Or Not IsPostBack Then
            If Not DropDownListCurrency.SelectedValue.Equals("Rub") Then
                e.Cancel = True
            Else
                e.Command.CommandTimeout = 120
                e.Cancel = False
            End If
        End If
    End Sub

    Protected Sub GridViewBudgetDollar_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewBudgetDollar.RowCommand
        If (e.CommandName = "ActionUpdate") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridViewBudgetDollar.Rows(index)

            If DDLInsertOrUpdate.Items.Count = 0 Then
                If DirectCast(row.FindControl("TextBoxBudget"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxBudget"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxVCO"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxVCO"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxPlannedToSpend"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxPlannedToSpend"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxPlannedToSpendCO"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxPlannedToSpendCO"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text = 0
                SqlDataSourceBudgetInsert.InsertCommand = "INSERT INTO Table_Budget (ProjectID, CostCode, Budget, PlannedToSpend, Currency, UpdatedPlannedRevenue, VCO, PlannedToSpendCO) VALUES (" +
          "'" + DropDownListPrj.SelectedValue + "'" + "," + "'" + DirectCast(row.FindControl("LabelCostCodeEdit"), Label).Text + "'" +
          "," + DirectCast(row.FindControl("TextBoxBudget"), TextBox).Text +
          "," + DirectCast(row.FindControl("TextBoxPlannedToSpend"), TextBox).Text +
          "," + "'" + DropDownListCurrency.SelectedValue.ToString + "'" +
          "," + DirectCast(row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text +
          "," + DirectCast(row.FindControl("TextBoxVCO"), TextBox).Text +
          "," + DirectCast(row.FindControl("TextBoxPlannedToSpendCO"), TextBox).Text + ")"
                SqlDataSourceBudgetInsert.Insert()
                sender.EditIndex = -1
                sender.DataBind()
            Else
                If DirectCast(row.FindControl("TextBoxBudget"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxBudget"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxVCO"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxVCO"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxPlannedToSpend"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxPlannedToSpend"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxPlannedToSpendCO"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxPlannedToSpendCO"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text = 0
                SqlDataSourceBudgetEuro.UpdateCommand = "UPDATE Table_Budget SET ProjectID =" + "'" + DropDownListPrj.SelectedValue + "'" +
          "," + "CostCode =" + "'" + DirectCast(row.FindControl("LabelCostCodeEdit"), Label).Text + "'" +
          "," + " Budget =" + DirectCast(row.FindControl("TextBoxBudget"), TextBox).Text +
          "," + " VCO =" + DirectCast(row.FindControl("TextBoxVCO"), TextBox).Text +
          "," + " PlannedToSpend= " + DirectCast(row.FindControl("TextBoxPlannedToSpend"), TextBox).Text +
          "," + " PlannedToSpendCO= " + DirectCast(row.FindControl("TextBoxPlannedToSpendCO"), TextBox).Text +
          "," + " Currency= " + "'" + DropDownListCurrency.SelectedValue.ToString + "'" +
          "," + " UpdatedPlannedRevenue= " + DirectCast(row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text +
          " WHERE (ProjectID =" + "'" + DropDownListPrj.SelectedValue + "'" +
          ") AND (CostCode=" + "'" + DirectCast(row.FindControl("LabelCostCodeEdit"), Label).Text + "'" + ")" +
          " AND (" + " Currency= " + "'" + DropDownListCurrency.SelectedValue.ToString + "'" + " )"
                SqlDataSourceBudgetEuro.Update()
                sender.EditIndex = -1
                sender.DataBind()
            End If


            Dim ProgressPercent As String = ""
            If DirectCast(row.FindControl("TextBoxCompletionPercent"), TextBox).Text = "" Then
                ProgressPercent = "0"
            Else
                ProgressPercent = DirectCast(row.FindControl("TextBoxCompletionPercent"), TextBox).Text
            End If

            'If LabelProgressExistOrNot.Text = "True" Then
            '  ' use updateCommand
            '          Using con22 As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            '              con22.Open()
            '              Dim sqlstring22 As String = " UPDATE [Table_ProgressPercent] " + _
            '                                          " SET [ProjectID] =  " + "'" + DropDownListPrj.SelectedValue + "'" + _
            '                                          " ,[CostCode] =  " + "'" + DirectCast(row.FindControl("LabelCostCodeEdit"), Label).Text + "'" + _
            '                                          " ,[CompletionPercent] =  " + ProgressPercent + _
            '                                        " where ([ProjectID] = N'" + DropDownListPrj.SelectedValue + "') AND ([CostCode] = N'" + DirectCast(row.FindControl("LabelCostCodeEdit"), Label).Text + "') "
            '              Dim cmd22 As New SqlCommand(sqlstring22, con22)
            '              cmd22.CommandType =System.Data.CommandType.Text
            '              Dim dr22 As SqlDataReader = cmd22.ExecuteReader
            '              con22.Close()
            '              dr22.Close()
            '              con22.Dispose()

            '          End Using
            '      ElseIf LabelProgressExistOrNot.Text = "False" Then
            '          ' use insertCommand
            '          Using con33 As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            '              con33.Open()
            '              Dim sqlstring33 As String = " INSERT INTO [Table_ProgressPercent] " + _
            '                                          " ([ProjectID] " + _
            '                                          " ,[CostCode] " + _
            '                                          " ,[CompletionPercent]) " + _
            '                                          " VALUES " + _
            '                                          " (" + "'" + DropDownListPrj.SelectedValue + "'" + "," + "'" + DirectCast(row.FindControl("LabelCostCodeEdit"), Label).Text + "'" + "," + ProgressPercent + ") "
            '              Dim cmd33 As New SqlCommand(sqlstring33, con33)
            '              cmd33.CommandType =System.Data.CommandType.Text
            '              Dim dr33 As SqlDataReader = cmd33.ExecuteReader
            '              con33.Close()
            '              dr33.Close()
            '              con33.Dispose()

            '          End Using
            '      End If

            GridViewBudgetDollar.DataBind()

        End If

        If (e.CommandName = "ActionDelete") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridViewBudgetDollar.Rows(index)
            SqlDataSourceBudgetDollar.DeleteCommand = "DELETE FROM Table_Budget WHERE (ProjectID =" + "'" + DropDownListPrj.SelectedValue + "'" + ") AND (" + "CostCode =" + "'" + DirectCast(row.FindControl("LabelCostCodeItem"), Label).Text + "'" + ")" + " AND (" + " Currency= " + "'" + DropDownListCurrency.SelectedValue.ToString + "'" + " )"
            SqlDataSourceBudgetDollar.Delete()
            GridViewBudgetDollar.EditIndex = -1

            ' check if this PrjID and CostCodes exist for another curreny or not
            Using con44 As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con44.Open()
                Dim sqlstring44 As String = " SELECT count([BudgetID]) FROM [Table_Budget] " +
                                          " where ([ProjectID] = " + DropDownListPrj.SelectedValue + ") AND " +
                                          " ([CostCode] = N'" + DirectCast(row.FindControl("LabelCostCodeItem"), Label).Text + "') AND " +
                                          " ([Currency] <> " + "N'" + DropDownListCurrency.SelectedValue.ToString + "')"
                Dim cmd44 As New SqlCommand(sqlstring44, con44)
                cmd44.CommandType = System.Data.CommandType.Text
                Dim dr44 As SqlDataReader = cmd44.ExecuteReader
                While dr44.Read
                    If dr44(0).ToString = "0" Then
                        LabelCostCodePrjIDExistOrNotForOtherCurreny.Text = "False"
                    Else
                        LabelCostCodePrjIDExistOrNotForOtherCurreny.Text = "True"
                    End If

                End While
                con44.Close()
                dr44.Close()
                con44.Dispose()

            End Using

            ' delete ProgressPercent if there is no ProgressPercent for other currenies under this 
            If LabelCostCodePrjIDExistOrNotForOtherCurreny.Text = "False" Then
                Using con66 As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                    con66.Open()
                    Dim sqlstring66 As String = " DELETE [Table_ProgressPercent] " +
                    " where ([ProjectID] = N'" + DropDownListPrj.SelectedValue + "') AND ([CostCode] = N'" + DirectCast(row.FindControl("LabelCostCodeItem"), Label).Text + "') "
                    Dim cmd66 As New SqlCommand(sqlstring66, con66)
                    cmd66.CommandType = System.Data.CommandType.Text
                    Dim dr66 As SqlDataReader = cmd66.ExecuteReader
                    con66.Close()
                    dr66.Close()
                    con66.Dispose()

                End Using
            End If
            GridViewBudgetDollar.DataBind()
        End If
    End Sub

    Protected Sub GridViewBudgetRuble_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewBudgetRuble.RowCommand

        If (e.CommandName = "ActionUpdate") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridViewBudgetRuble.Rows(index)
            If TextBoxBudget.Text = "" Then TextBoxBudget.Text = 0
            If TextBoxPlanned.Text = "" Then TextBoxPlanned.Text = 0

            If DDLInsertOrUpdate.Items.Count = 0 Then
                If DirectCast(row.FindControl("TextBoxBudget"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxBudget"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxVCO"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxVCO"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxPlannedToSpend"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxPlannedToSpend"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxPlannedToSpendCO"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxPlannedToSpendCO"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxRecovery"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxRecovery"), TextBox).Text = 0

                SqlDataSourceBudgetInsert.InsertCommand = "INSERT INTO Table_Budget (ProjectID, CostCode, Budget, PlannedToSpend, Currency, UpdatedPlannedRevenue, VCO, PlannedToSpendCO, Recovery) VALUES (" +
                  "'" + DropDownListPrj.SelectedValue + "'" + "," + "'" + DirectCast(row.FindControl("LabelCostCodeEdit"), Label).Text + "'" +
                  "," + DirectCast(row.FindControl("TextBoxBudget"), TextBox).Text +
                  "," + DirectCast(row.FindControl("TextBoxPlannedToSpend"), TextBox).Text +
                  "," + "'" + DropDownListCurrency.SelectedValue.ToString + "'" +
                  "," + DirectCast(row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text +
                  "," + DirectCast(row.FindControl("TextBoxVCO"), TextBox).Text +
                  "," + DirectCast(row.FindControl("TextBoxPlannedToSpendCO"), TextBox).Text +
                  "," + DirectCast(row.FindControl("TextBoxRecovery"), TextBox).Text + ")"
                SqlDataSourceBudgetInsert.Insert()
                sender.EditIndex = -1
                sender.DataBind()
            Else
                If DirectCast(row.FindControl("TextBoxBudget"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxBudget"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxVCO"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxVCO"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxPlannedToSpend"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxPlannedToSpend"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxPlannedToSpendCO"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxPlannedToSpendCO"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text = 0
                If DirectCast(row.FindControl("TextBoxRecovery"), TextBox).Text = "" Then DirectCast(row.FindControl("TextBoxRecovery"), TextBox).Text = 0

                SqlDataSourceBudgetEuro.UpdateCommand = "UPDATE Table_Budget SET ProjectID =" + "'" + DropDownListPrj.SelectedValue + "'" +
                  "," + "CostCode =" + "'" + DirectCast(row.FindControl("LabelCostCodeEdit"), Label).Text + "'" +
                  "," + " Budget =" + DirectCast(row.FindControl("TextBoxBudget"), TextBox).Text +
                  "," + " VCO =" + DirectCast(row.FindControl("TextBoxVCO"), TextBox).Text +
                  "," + " PlannedToSpend= " + DirectCast(row.FindControl("TextBoxPlannedToSpend"), TextBox).Text +
                  "," + " PlannedToSpendCO= " + DirectCast(row.FindControl("TextBoxPlannedToSpendCO"), TextBox).Text +
                  "," + " Currency= " + "'" + DropDownListCurrency.SelectedValue.ToString + "'" +
                  "," + " UpdatedPlannedRevenue= " + DirectCast(row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text +
                  "," + " Recovery= " + DirectCast(row.FindControl("TextBoxRecovery"), TextBox).Text +
                  " WHERE (ProjectID =" + "'" + DropDownListPrj.SelectedValue + "'" +
                  ") AND (CostCode=" + "'" + DirectCast(row.FindControl("LabelCostCodeEdit"), Label).Text + "'" + ")" +
                  " AND (" + " Currency= " + "'" + DropDownListCurrency.SelectedValue.ToString + "'" + " )"
                SqlDataSourceBudgetEuro.Update()
                sender.EditIndex = -1
                sender.DataBind()
            End If


            Dim ProgressPercent As String = ""
            If DirectCast(row.FindControl("TextBoxCompletionPercent"), TextBox).Text = "" Then
                ProgressPercent = "0"
            Else
                ProgressPercent = DirectCast(row.FindControl("TextBoxCompletionPercent"), TextBox).Text
            End If

            'If LabelProgressExistOrNot.Text = "True" Then
            '  ' use updateCommand
            '          Using con22 As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            '              con22.Open()
            '              Dim sqlstring22 As String = " UPDATE [Table_ProgressPercent] " + _
            '                                          " SET [ProjectID] =  " + "'" + DropDownListPrj.SelectedValue + "'" + _
            '                                          " ,[CostCode] =  " + "'" + DirectCast(row.FindControl("LabelCostCodeEdit"), Label).Text + "'" + _
            '                                          " ,[CompletionPercent] =  " + ProgressPercent + _
            '                                        " where ([ProjectID] = N'" + DropDownListPrj.SelectedValue + "') AND ([CostCode] = N'" + DirectCast(row.FindControl("LabelCostCodeEdit"), Label).Text + "') "
            '              Dim cmd22 As New SqlCommand(sqlstring22, con22)
            '              cmd22.CommandType =System.Data.CommandType.Text
            '              Dim dr22 As SqlDataReader = cmd22.ExecuteReader
            '              con22.Close()
            '              dr22.Close()
            '              con22.Dispose()

            '          End Using
            '      ElseIf LabelProgressExistOrNot.Text = "False" Then
            '          ' use insertCommand
            '          Using con33 As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            '              con33.Open()
            '              Dim sqlstring33 As String = " INSERT INTO [Table_ProgressPercent] " + _
            '                                          " ([ProjectID] " + _
            '                                          " ,[CostCode] " + _
            '                                          " ,[CompletionPercent]) " + _
            '                                          " VALUES " + _
            '                                          " (" + "'" + DropDownListPrj.SelectedValue + "'" + "," + "'" + DirectCast(row.FindControl("LabelCostCodeEdit"), Label).Text + "'" + "," + ProgressPercent + ") "
            '              Dim cmd33 As New SqlCommand(sqlstring33, con33)
            '              cmd33.CommandType =System.Data.CommandType.Text
            '              Dim dr33 As SqlDataReader = cmd33.ExecuteReader
            '              con33.Close()
            '              dr33.Close()
            '              con33.Dispose()

            '          End Using
            '      End If

            GridViewBudgetRuble.DataBind()
        End If

        If (e.CommandName = "ActionDelete") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridViewBudgetRuble.Rows(index)
            SqlDataSourceBudgetRuble.DeleteCommand = "DELETE FROM Table_Budget WHERE (ProjectID =" + "'" + DropDownListPrj.SelectedValue + "'" + ") AND (" + "CostCode =" + "'" + DirectCast(row.FindControl("LabelCostCodeItem"), Label).Text + "'" + ")" + " AND (" + " Currency= " + "'" + DropDownListCurrency.SelectedValue.ToString + "'" + " )"
            SqlDataSourceBudgetRuble.Delete()
            GridViewBudgetRuble.EditIndex = -1

            ' check if this PrjID and CostCodes exist for another curreny or not
            Using con44 As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con44.Open()
                Dim sqlstring44 As String = " SELECT count([BudgetID]) FROM [Table_Budget] " +
                                          " where ([ProjectID] = " + DropDownListPrj.SelectedValue + ") AND " +
                                          " ([CostCode] = N'" + DirectCast(row.FindControl("LabelCostCodeItem"), Label).Text + "') AND " +
                                          " ([Currency] <> " + "N'" + DropDownListCurrency.SelectedValue.ToString + "')"
                Dim cmd44 As New SqlCommand(sqlstring44, con44)
                cmd44.CommandType = System.Data.CommandType.Text
                Dim dr44 As SqlDataReader = cmd44.ExecuteReader
                While dr44.Read
                    If dr44(0).ToString = "0" Then
                        LabelCostCodePrjIDExistOrNotForOtherCurreny.Text = "False"
                    Else
                        LabelCostCodePrjIDExistOrNotForOtherCurreny.Text = "True"
                    End If

                End While
                con44.Close()
                dr44.Close()
                con44.Dispose()

            End Using

            ' delete ProgressPercent if there is no ProgressPercent for other currenies under this 
            If LabelCostCodePrjIDExistOrNotForOtherCurreny.Text = "False" Then
                Using con66 As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                    con66.Open()
                    Dim sqlstring66 As String = " DELETE [Table_ProgressPercent] " +
                    " where ([ProjectID] = N'" + DropDownListPrj.SelectedValue + "') AND ([CostCode] = N'" + DirectCast(row.FindControl("LabelCostCodeItem"), Label).Text + "') "
                    Dim cmd66 As New SqlCommand(sqlstring66, con66)
                    cmd66.CommandType = System.Data.CommandType.Text
                    Dim dr66 As SqlDataReader = cmd66.ExecuteReader
                    con66.Close()
                    dr66.Close()
                    con66.Dispose()

                End Using
            End If
            GridViewBudgetRuble.DataBind()
        End If
    End Sub

    Protected Sub GridViewBudgetDollar_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewBudgetDollar.RowDataBound

        Dim MyTask As New MyCommonTasks
        MyTask.HoverEffectOnGridviewCells(sender, e.Row)

        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowIndex = 0 Then
                ColorShifter = 0
                Budget = 0
                BudgetCO = 0
                PlannedToSpend = 0
                PlannedToSpendCO = 0


                LeadingCostDisipline = DataBinder.Eval(e.Row.DataItem, "CostVidisionID").ToString
                e.Row.BackColor = System.Drawing.Color.White
            Else
                If DataBinder.Eval(e.Row.DataItem, "CostVidisionID").ToString = LeadingCostDisipline Then
                    If ColorShifter Mod 2 = 0 Then
                        e.Row.BackColor = System.Drawing.Color.White
                    Else
                        e.Row.BackColor = System.Drawing.Color.Gainsboro
                    End If
                ElseIf DataBinder.Eval(e.Row.DataItem, "CostVidisionID").ToString <> LeadingCostDisipline Then
                    ColorShifter = ColorShifter + 1
                    If ColorShifter Mod 2 = 0 Then
                        e.Row.BackColor = System.Drawing.Color.White
                    Else
                        e.Row.BackColor = System.Drawing.Color.Gainsboro
                    End If
                    LeadingCostDisipline = DataBinder.Eval(e.Row.DataItem, "CostVidisionID").ToString
                End If
            End If
        End If

        If DirectCast(e.Row.FindControl("TextBoxBudget"), TextBox) IsNot Nothing Then
            ' check ProgressPercent if any progress exist or not.
            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = " SELECT count([Id]) FROM [Table_ProgressPercent] " +
                                          " where ([ProjectID] = " + DropDownListPrj.SelectedValue + ") AND ([CostCode] = N'" + DirectCast(e.Row.FindControl("LabelCostCodeEdit"), Label).Text + "') "
                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.Text
                Dim dr As SqlDataReader = cmd.ExecuteReader
                While dr.Read
                    If dr(0).ToString = "0" Then
                        LabelProgressExistOrNot.Text = "False"
                    Else
                        LabelProgressExistOrNot.Text = "True"
                    End If
                    'Response.Write(ProgressExist.ToString)
                End While
                con.Close()
                dr.Close()
                con.Dispose()

            End Using

        End If


        If DirectCast(e.Row.FindControl("LabelDeletePossibleItem"), Label) IsNot Nothing AndAlso DirectCast(e.Row.FindControl("LabelDeletePossibleItem"), Label).Text = 1 Then
            DirectCast(e.Row.FindControl("LinkButtonDeleteItem"), LinkButton).Visible = True
        ElseIf DirectCast(e.Row.FindControl("LabelDeletePossibleItem"), Label) IsNot Nothing AndAlso DirectCast(e.Row.FindControl("LabelDeletePossibleItem"), Label).Text = 0 Then
            DirectCast(e.Row.FindControl("LinkButtonDeleteItem"), LinkButton).Visible = False
        End If

        '////////////////////////// EDIT MODE CALCULATION
        If DirectCast(e.Row.FindControl("TextBoxBudget"), TextBox) IsNot Nothing Then
            Budget = Budget + Convert.ToDecimal((DirectCast(e.Row.FindControl("TextBoxBudget"), TextBox).Text))
        End If

        If DirectCast(e.Row.FindControl("TextBoxVCO"), TextBox) IsNot Nothing Then
            BudgetCO = BudgetCO + Convert.ToDecimal((DirectCast(e.Row.FindControl("TextBoxVCO"), TextBox).Text))
        End If

        If DirectCast(e.Row.FindControl("TextBoxPlannedToSpend"), TextBox) IsNot Nothing Then
            PlannedToSpend = PlannedToSpend + Convert.ToDecimal((DirectCast(e.Row.FindControl("TextBoxPlannedToSpend"), TextBox).Text))
        End If

        If DirectCast(e.Row.FindControl("TextBoxPlannedToSpendCO"), TextBox) IsNot Nothing Then
            PlannedToSpendCO = PlannedToSpendCO + Convert.ToDecimal((DirectCast(e.Row.FindControl("TextBoxPlannedToSpendCO"), TextBox).Text))
        End If

        If DirectCast(e.Row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox) IsNot Nothing Then
            If Len(DirectCast(e.Row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text) > 0 Then
                Revenue = Revenue + Convert.ToDecimal((DirectCast(e.Row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text))
            End If
        End If

        ' ///////////////////ITEM MODE Calculation

        If DirectCast(e.Row.FindControl("HyperLinkCostCodeComments"), HyperLink) IsNot Nothing Then
            ' provide hyperlink for user comments on costCode
            'DirectCast(e.Row.FindControl("HyperLinkCostCodeComments"), HyperLink).ImageUrl = "~/Images/comment_costcode.png"
            'DirectCast(e.Row.FindControl("HyperLinkCostCodeComments"), HyperLink).NavigateUrl = "podetails.aspx?ProjectID=" + DropDownListPrj.SelectedValue.ToString + _
            '  "&CostCode=" + DataBinder.Eval(e.Row.DataItem, "CostCode").ToString

            Dim QueryStrings As String = "?ProjectID=" + DropDownListPrj.SelectedValue.ToString + "&CostCode=" + DataBinder.Eval(e.Row.DataItem, "CostCode").ToString + "&Currency=Dollar"
            DirectCast(e.Row.FindControl("HyperLinkCostCodeComments"), HyperLink).Attributes.Add("onclick", "javascript:w= window.open('commentsOncostReport.aspx" + QueryStrings + "','CostCodeComments','left=100,top=100,width=600,height=600,toolbar=0,resizable=0,scrollbars=yes');")
        End If


        If DirectCast(e.Row.FindControl("LabelBudget"), Label) IsNot Nothing Then
            Budget = Budget + Convert.ToDecimal(Replace(DirectCast(e.Row.FindControl("LabelBudget"), Label).Text, ",", ""))
        End If

        If DirectCast(e.Row.FindControl("LabelVCO"), Label) IsNot Nothing Then
            BudgetCO = BudgetCO + Convert.ToDecimal(Replace(DirectCast(e.Row.FindControl("LabelVCO"), Label).Text, ",", ""))
        End If

        If DirectCast(e.Row.FindControl("LabelPlannedToSpend"), Label) IsNot Nothing Then
            PlannedToSpend = PlannedToSpend + Convert.ToDecimal(Replace(DirectCast(e.Row.FindControl("LabelPlannedToSpend"), Label).Text, ",", ""))
        End If

        If DirectCast(e.Row.FindControl("LabelPlannedToSpendCO"), Label) IsNot Nothing Then
            PlannedToSpendCO = PlannedToSpendCO + Convert.ToDecimal(Replace(DirectCast(e.Row.FindControl("LabelPlannedToSpendCO"), Label).Text, ",", ""))
        End If

        If DirectCast(e.Row.FindControl("LabelUpdatedPlannedRevenue"), Label) IsNot Nothing Then
            Revenue = Revenue + Convert.ToDecimal(Replace(DirectCast(e.Row.FindControl("LabelUpdatedPlannedRevenue"), Label).Text, ",", ""))
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            If DirectCast(e.Row.FindControl("LabelBudget"), Label) IsNot Nothing Then
                DirectCast(e.Row.FindControl("LabelBudget"), Label).Text = Format(Budget, "Standard")
            End If

            If DirectCast(e.Row.FindControl("LabelVCO"), Label) IsNot Nothing Then
                DirectCast(e.Row.FindControl("LabelVCO"), Label).Text = Format(BudgetCO, "Standard")
            End If

            If DirectCast(e.Row.FindControl("LabelPlannedToSpend"), Label) IsNot Nothing Then
                DirectCast(e.Row.FindControl("LabelPlannedToSpend"), Label).Text = Format(PlannedToSpend, "Standard")
            End If

            If DirectCast(e.Row.FindControl("LabelPlannedToSpendCO"), Label) IsNot Nothing Then
                DirectCast(e.Row.FindControl("LabelPlannedToSpendCO"), Label).Text = Format(PlannedToSpendCO, "Standard")
            End If

            If DirectCast(e.Row.FindControl("LabelUpdatedPlannedRevenue"), Label) IsNot Nothing Then
                DirectCast(e.Row.FindControl("LabelUpdatedPlannedRevenue"), Label).Text = Format(Revenue, "Standard")
            End If

        End If

        'To identify INSERT or UPDATE required
        If DirectCast(e.Row.FindControl("LabelCostCodeEdit"), Label) IsNot Nothing Then
            SqlDataSourceUpdateOrInsert.SelectCommand = "SELECT ProjectID  ,CostCode FROM Table_Budget WHERE (ProjectID=" + "'" + DropDownListPrj.SelectedValue + "') AND (CostCode=" + "'" + DirectCast(e.Row.FindControl("LabelCostCodeEdit"), Label).Text + "') " + " AND (" + " Currency= " + "'" + DropDownListCurrency.SelectedValue.ToString + "'" + " )"
            SqlDataSourceUpdateOrInsert.DataBind()
            DDLInsertOrUpdate.DataBind()
        End If

        Dim cbxBudgetId As CheckBox = DirectCast(e.Row.FindControl("cbxBudgetId"), CheckBox)
        Dim LabelBudgetId As Label = DirectCast(e.Row.FindControl("LabelBudgetId"), Label)

        If cbxBudgetId IsNot Nothing And LabelBudgetId IsNot Nothing Then
            If LabelBudgetId.Text.Trim.Length > 0 Then
                If PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table_Budget_PlannedToSpendConstraints.IfExist(LabelBudgetId.Text) Then
                    cbxBudgetId.Checked = True
                Else
                    cbxBudgetId.Checked = False
                End If
            End If
        End If

    End Sub

    Protected Sub GridViewBudgetRuble_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewBudgetRuble.RowDataBound

        Dim MyTask As New MyCommonTasks
        MyTask.HoverEffectOnGridviewCells(sender, e.Row)

        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowIndex = 0 Then
                ColorShifter = 0
                Budget = 0
                BudgetCO = 0
                PlannedToSpend = 0
                PlannedToSpendCO = 0


                LeadingCostDisipline = DataBinder.Eval(e.Row.DataItem, "CostVidisionID").ToString
                e.Row.BackColor = System.Drawing.Color.White
            Else
                If DataBinder.Eval(e.Row.DataItem, "CostVidisionID").ToString = LeadingCostDisipline Then
                    If ColorShifter Mod 2 = 0 Then
                        e.Row.BackColor = System.Drawing.Color.White
                    Else
                        e.Row.BackColor = System.Drawing.Color.Gainsboro
                    End If
                ElseIf DataBinder.Eval(e.Row.DataItem, "CostVidisionID").ToString <> LeadingCostDisipline Then
                    ColorShifter = ColorShifter + 1
                    If ColorShifter Mod 2 = 0 Then
                        e.Row.BackColor = System.Drawing.Color.White
                    Else
                        e.Row.BackColor = System.Drawing.Color.Gainsboro
                    End If
                    LeadingCostDisipline = DataBinder.Eval(e.Row.DataItem, "CostVidisionID").ToString
                End If
            End If
        End If

        If DirectCast(e.Row.FindControl("TextBoxBudget"), TextBox) IsNot Nothing Then
            ' check ProgressPercent if any progress exist or not.
            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = " SELECT count([Id]) FROM [Table_ProgressPercent] " +
                                          " where ([ProjectID] = " + DropDownListPrj.SelectedValue + ") AND ([CostCode] = N'" + DirectCast(e.Row.FindControl("LabelCostCodeEdit"), Label).Text + "') "
                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.Text
                Dim dr As SqlDataReader = cmd.ExecuteReader
                While dr.Read
                    If dr(0).ToString = "0" Then
                        LabelProgressExistOrNot.Text = "False"
                    Else
                        LabelProgressExistOrNot.Text = "True"
                    End If
                    'Response.Write(ProgressExist.ToString)
                End While
                con.Close()
                dr.Close()
                con.Dispose()

            End Using

        End If


        If DirectCast(e.Row.FindControl("LabelDeletePossibleItem"), Label) IsNot Nothing AndAlso DirectCast(e.Row.FindControl("LabelDeletePossibleItem"), Label).Text = 1 Then
            DirectCast(e.Row.FindControl("LinkButtonDeleteItem"), LinkButton).Visible = True
        ElseIf DirectCast(e.Row.FindControl("LabelDeletePossibleItem"), Label) IsNot Nothing AndAlso DirectCast(e.Row.FindControl("LabelDeletePossibleItem"), Label).Text = 0 Then
            DirectCast(e.Row.FindControl("LinkButtonDeleteItem"), LinkButton).Visible = False
        End If

        '////////////////////////// EDIT MODE CALCULATION
        If DirectCast(e.Row.FindControl("TextBoxBudget"), TextBox) IsNot Nothing Then
            Budget = Budget + Convert.ToDecimal((DirectCast(e.Row.FindControl("TextBoxBudget"), TextBox).Text))
        End If

        If DirectCast(e.Row.FindControl("TextBoxVCO"), TextBox) IsNot Nothing Then
            BudgetCO = BudgetCO + Convert.ToDecimal((DirectCast(e.Row.FindControl("TextBoxVCO"), TextBox).Text))
        End If

        If DirectCast(e.Row.FindControl("TextBoxPlannedToSpend"), TextBox) IsNot Nothing Then
            PlannedToSpend = PlannedToSpend + Convert.ToDecimal((DirectCast(e.Row.FindControl("TextBoxPlannedToSpend"), TextBox).Text))
        End If

        If DirectCast(e.Row.FindControl("TextBoxPlannedToSpendCO"), TextBox) IsNot Nothing Then
            PlannedToSpendCO = PlannedToSpendCO + Convert.ToDecimal((DirectCast(e.Row.FindControl("TextBoxPlannedToSpendCO"), TextBox).Text))
        End If

        If DirectCast(e.Row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox) IsNot Nothing Then
            If Len(DirectCast(e.Row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text) > 0 Then
                Revenue = Revenue + Convert.ToDecimal((DirectCast(e.Row.FindControl("TextBoxUpdatedPlannedRevenue"), TextBox).Text))
            End If
        End If

        ' ///////////////////ITEM MODE Calculation

        If DirectCast(e.Row.FindControl("HyperLinkCostCodeComments"), HyperLink) IsNot Nothing Then
            ' provide hyperlink for user comments on costCode
            'DirectCast(e.Row.FindControl("HyperLinkCostCodeComments"), HyperLink).ImageUrl = "~/Images/comment_costcode.png"
            'DirectCast(e.Row.FindControl("HyperLinkCostCodeComments"), HyperLink).NavigateUrl = "podetails.aspx?ProjectID=" + DropDownListPrj.SelectedValue.ToString + _
            '  "&CostCode=" + DataBinder.Eval(e.Row.DataItem, "CostCode").ToString

            Dim QueryStrings As String = "?ProjectID=" + DropDownListPrj.SelectedValue.ToString + "&CostCode=" + DataBinder.Eval(e.Row.DataItem, "CostCode").ToString + "&Currency=Rub"
            DirectCast(e.Row.FindControl("HyperLinkCostCodeComments"), HyperLink).Attributes.Add("onclick", "javascript:w= window.open('commentsOncostReport.aspx" + QueryStrings + "','CostCodeComments','left=100,top=100,width=600,height=600,toolbar=0,resizable=0,scrollbars=yes');")
        End If

        If DirectCast(e.Row.FindControl("LabelBudget"), Label) IsNot Nothing Then
            Budget = Budget + Convert.ToDecimal(Replace(DirectCast(e.Row.FindControl("LabelBudget"), Label).Text, ",", ""))
        End If

        If DirectCast(e.Row.FindControl("LabelVCO"), Label) IsNot Nothing Then
            BudgetCO = BudgetCO + Convert.ToDecimal(Replace(DirectCast(e.Row.FindControl("LabelVCO"), Label).Text, ",", ""))
        End If

        If DirectCast(e.Row.FindControl("LabelPlannedToSpend"), Label) IsNot Nothing Then
            PlannedToSpend = PlannedToSpend + Convert.ToDecimal(Replace(DirectCast(e.Row.FindControl("LabelPlannedToSpend"), Label).Text, ",", ""))
        End If

        If DirectCast(e.Row.FindControl("LabelPlannedToSpendCO"), Label) IsNot Nothing Then
            PlannedToSpendCO = PlannedToSpendCO + Convert.ToDecimal(Replace(DirectCast(e.Row.FindControl("LabelPlannedToSpendCO"), Label).Text, ",", ""))
        End If

        If DirectCast(e.Row.FindControl("LabelUpdatedPlannedRevenue"), Label) IsNot Nothing Then
            Revenue = Revenue + Convert.ToDecimal(Replace(DirectCast(e.Row.FindControl("LabelUpdatedPlannedRevenue"), Label).Text, ",", ""))
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            If DirectCast(e.Row.FindControl("LabelBudget"), Label) IsNot Nothing Then
                DirectCast(e.Row.FindControl("LabelBudget"), Label).Text = Format(Budget, "Standard")
            End If

            If DirectCast(e.Row.FindControl("LabelVCO"), Label) IsNot Nothing Then
                DirectCast(e.Row.FindControl("LabelVCO"), Label).Text = Format(BudgetCO, "Standard")
            End If

            If DirectCast(e.Row.FindControl("LabelPlannedToSpend"), Label) IsNot Nothing Then
                DirectCast(e.Row.FindControl("LabelPlannedToSpend"), Label).Text = Format(PlannedToSpend, "Standard")
            End If

            If DirectCast(e.Row.FindControl("LabelPlannedToSpendCO"), Label) IsNot Nothing Then
                DirectCast(e.Row.FindControl("LabelPlannedToSpendCO"), Label).Text = Format(PlannedToSpendCO, "Standard")
            End If

            If DirectCast(e.Row.FindControl("LabelUpdatedPlannedRevenue"), Label) IsNot Nothing Then
                DirectCast(e.Row.FindControl("LabelUpdatedPlannedRevenue"), Label).Text = Format(Revenue, "Standard")
            End If

        End If

        'To identify INSERT or UPDATE required
        If DirectCast(e.Row.FindControl("LabelCostCodeEdit"), Label) IsNot Nothing Then
            SqlDataSourceUpdateOrInsert.SelectCommand = "SELECT ProjectID  ,CostCode FROM Table_Budget WHERE (ProjectID=" + "'" + DropDownListPrj.SelectedValue + "') AND (CostCode=" + "'" + DirectCast(e.Row.FindControl("LabelCostCodeEdit"), Label).Text + "') " + " AND (" + " Currency= " + "'" + DropDownListCurrency.SelectedValue.ToString + "'" + " )"
            SqlDataSourceUpdateOrInsert.DataBind()
            DDLInsertOrUpdate.DataBind()
        End If

        Dim cbxBudgetId As CheckBox = DirectCast(e.Row.FindControl("cbxBudgetId"), CheckBox)
        Dim LabelBudgetId As Label = DirectCast(e.Row.FindControl("LabelBudgetId"), Label)

        If cbxBudgetId IsNot Nothing And LabelBudgetId IsNot Nothing Then
            If LabelBudgetId.Text.Trim.Length > 0 Then
                If PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table_Budget_PlannedToSpendConstraints.IfExist(LabelBudgetId.Text) Then
                    cbxBudgetId.Checked = True
                Else
                    cbxBudgetId.Checked = False
                End If
            End If
        End If

    End Sub

    Protected Sub GridViewBudgetDollar_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewBudgetDollar.Load
        If Not User.IsInRole("CostStaffActive") Then
            GridViewBudgetDollar.Columns(2).Visible = False
        End If
    End Sub

    Protected Sub GridViewBudgetRuble_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewBudgetRuble.Load
        If Not User.IsInRole("CostStaffActive") Then
            GridViewBudgetRuble.Columns(2).Visible = False
        End If
    End Sub

    Protected Sub GridViewBudgetDollar_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewBudgetDollar.RowCreated
        If IsPostBack Or Not IsPostBack Then
            If e.Row.RowType = DataControlRowType.Header Then
                If Not User.IsInRole("CostStaffActive") Then
                    'AddHeaderRow_GridPassive(GridViewBudgetDollar)
                Else
                    'AddHeaderRow_GridActive(GridViewBudgetDollar)
                End If
            End If
        End If
    End Sub

    Protected Sub GridViewBudgetRuble_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewBudgetRuble.RowCreated
        If IsPostBack Or Not IsPostBack Then
            If e.Row.RowType = DataControlRowType.Header Then
                If Not User.IsInRole("CostStaffActive") Then
                    'AddHeaderRow_GridPassive(GridViewBudgetRuble)
                Else
                    'AddHeaderRow_GridActive(GridViewBudgetRuble)
                End If
            End If
        End If
    End Sub

    Protected Sub DropDownListCostCode_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListCostCode.DataBound
        If IsPostBack Then
            Dim lst1 As New ListItem("Select CostCode", "-")
            Me.DropDownListCostCode.Items.Insert(0, lst1)

            If Len(SqlDataSourceCostCode.SelectCommand.ToString) = 0 Then
                ' do nothing
            Else
                ' it will check if costcode exist to match. If so, then costcode value transfered to DDL
                Using conCostCodeValidation As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                    conCostCodeValidation.Open()
                    Dim sqlstringCostCodeValidation As String
                    sqlstringCostCodeValidation = SqlDataSourceCostCode.SelectCommand.ToString

                    Dim cmdCostCodeValidation As New SqlCommand(sqlstringCostCodeValidation, conCostCodeValidation)
                    cmdCostCodeValidation.CommandType = System.Data.CommandType.Text
                    Dim drCostCodeValidation As SqlDataReader = cmdCostCodeValidation.ExecuteReader
                    While drCostCodeValidation.Read
                        If drCostCodeValidation(0).ToString = LabelCostCodeAddSelectItemDecision.Text Then
                            'If LabelResetDDLcostCodeAfterBudgetInsert.Text <> "Inserted" Then
                            DropDownListCostCode.SelectedValue = LabelCostCodeAddSelectItemDecision.Text
                            'End If
                        End If
                    End While
                    conCostCodeValidation.Dispose()
                    drCostCodeValidation.Close()
                    conCostCodeValidation.Dispose()

                End Using
            End If
    ElseIf Not IsPostBack Then
      Dim lst1 As New ListItem("Select CostCode", "-")
      Me.DropDownListCostCode.Items.Insert(0, lst1)
    End If
  End Sub

  Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

  End Sub

  Protected Sub DropDownListCostCode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListCostCode.SelectedIndexChanged

  End Sub

  Protected Sub SqlDataSourceBudgetInsert_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourceBudgetInsert.Inserted
    TextBoxBudget.Text = ""
    TextBoxPlanned.Text = ""
    'LabelResetDDLcostCodeAfterBudgetInsert.Text = "Inserted"
  End Sub

  Protected Sub SqlDataSourceBudgetInsert_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles SqlDataSourceBudgetInsert.Inserting
    ' if there is warning about costcode, cancel insert
    If Len(labelCostCodeValidate.Text.ToString) > 0 Then
      e.Cancel = True
    End If

  End Sub

  Protected Sub AddHeaderRow_GridActive(ByVal MyGrid As GridView)
    Dim rowX As New GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal)
    Dim i As Integer
    For i = 1 To 14
      Dim cell2 As New TableCell()
      cell2.BackColor = System.Drawing.Color.Black
      cell2.ForeColor = System.Drawing.Color.White
      cell2.HorizontalAlign = HorizontalAlign.Center
      Select Case i
        Case 1
          cell2.BackColor = System.Drawing.Color.White
        Case 2
          cell2.BackColor = System.Drawing.Color.White
        Case 3
          cell2.BackColor = System.Drawing.Color.White
        Case 4
          cell2.Text = "A"
        Case 5
          cell2.Text = "B"
        Case 6
          cell2.Text = "A-B"
        Case 7
          cell2.Text = "-"
        Case 8
          cell2.Text = "C"
        Case 9
          cell2.Text = "B1"
        Case 10
          cell2.Text = "C-B1"
        Case 11
          cell2.Text = "A-C"
        Case 12
          cell2.Text = "D"
        Case 13
          cell2.Text = "E"
        Case 14
          cell2.Text = "C-D"
      End Select
      rowX.Cells.Add(cell2)
    Next
    MyGrid.Controls(0).Controls.AddAt(MyGrid.Controls(0).Controls.Count - 1, rowX)
  End Sub

  Protected Sub AddHeaderRow_GridPassive(ByVal MyGrid As GridView)
    Dim rowX As New GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal)
    Dim i As Integer
    For i = 1 To 13
      Dim cell2 As New TableCell()
      cell2.BackColor = System.Drawing.Color.Black
      cell2.ForeColor = System.Drawing.Color.White
      cell2.HorizontalAlign = HorizontalAlign.Center
      Select Case i
        Case 1
          cell2.BackColor = System.Drawing.Color.White
        Case 2
          cell2.BackColor = System.Drawing.Color.White
        Case 3
          cell2.Text = "A"
        Case 4
          cell2.Text = "B"
        Case 5
          cell2.Text = "A-B"
        Case 6
          cell2.Text = "-"
        Case 7
          cell2.Text = "C"
        Case 8
          cell2.Text = "B1"
        Case 9
          cell2.Text = "C-B1"
        Case 10
          cell2.Text = "A-C"
        Case 11
          cell2.Text = "D"
        Case 12
          cell2.Text = "E"
        Case 13
          cell2.Text = "C-D"
      End Select
      rowX.Cells.Add(cell2)
    Next
    MyGrid.Controls(0).Controls.AddAt(MyGrid.Controls(0).Controls.Count - 1, rowX)

  End Sub

End Class

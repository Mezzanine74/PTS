Imports System.Data.SqlClient

Partial Class editpoR
    Inherits System.Web.UI.Page

    Dim Notification As New _GiveNotification

    Protected Sub DropDownListPrj_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.DataBound
        If Not IsPostBack Then
            Dim lst1 As New ListItem("Select Project", "0")
            Me.DropDownListPrj.Items.Insert(0, lst1)
        End If

        If Request.QueryString("ProjectID") IsNot Nothing And Not IsPostBack Then
            DropDownListPrj.SelectedValue = Request.QueryString("ProjectID")
        End If

    End Sub

    Protected Sub DropDownListPrj_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.SelectedIndexChanged
        ' it rejects Marina from TNK-BP to Access Denied Page
        If Page.User.Identity.Name.ToString = "marina" OrElse Page.User.Identity.Name.ToString = "n.komleva" AndAlso DropDownListPrj.SelectedValue = 123 Then
            Response.Redirect("~/webforms/AccessDenied.aspx")
        End If

        Dim nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString())

        nameValues.Remove("SupplierID")

        nameValues.Set("ProjectID", DropDownListPrj.SelectedValue.ToString)
        Dim url As String = Request.Url.AbsolutePath
        Dim updatedQueryString As String = "?" + nameValues.ToString()
        Response.Redirect(url + updatedQueryString)



    End Sub

    Protected Sub GridViewEditPO_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewEditPO.DataBound
        ' check if 

    End Sub

    Protected Sub GridViewEditPO_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridViewEditPO.PageIndexChanging
        ' Cancel the paging operation if the user attempts to navigate
        ' to another page while the GridView control is in edit mode. 
        If GridViewEditPO.EditIndex <> -1 Then
            ' Use the Cancel property to cancel the paging operation.
            e.Cancel = True
            ' Display an error message.
            Dim newPageNumber As Integer = e.NewPageIndex + 1
            Message.Text = "Please update the record before moving to page " & _
              newPageNumber.ToString() & "."
        Else
            ' Clear the error message.
            Message.Text = ""
            MessageCostCode.Visible = False
        End If
    End Sub

    Protected Sub GridViewEditPO_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewEditPO.PreRender
        ' it binds datasource to Gridview based on supplier DDL value
        If CheckBoxListCO.Checked Then
            If Not IsPostBack Then
                SqlDataSourceEditPO.SelectCommand = "SELECT     PO_No, SupplierID, RTRIM(Description) AS Description, TotalPrice, RTRIM(PO_Currency) AS PO_Currency, VATpercent, RTRIM(CostCode) AS CostCode, RTRIM(Notes)  AS Notes, PO_Date, CO, Scenario, FrameContractPO, SupplierName " + _
                "  FROM         dbo.View_EditPO " + _
                " WHERE     (ProjectID =  " + "'" + DropDownListPrj.SelectedValue.ToString + "'" + " ) AND (SupplierID =  " + "'" + DropDownListSupplier.SelectedValue.ToString + "'" + "   )" + _
                " AND CO = 1"
            ElseIf DropDownListPrj.SelectedValue.ToString <> "0" AndAlso DropDownListSupplier.SelectedValue.ToString = "" Then
                SqlDataSourceEditPO.SelectCommand = "SELECT     PO_No, SupplierID, RTRIM(Description) AS Description, TotalPrice, RTRIM(PO_Currency) AS PO_Currency, VATpercent, RTRIM(CostCode) AS CostCode, RTRIM(Notes)  AS Notes, PO_Date, CO, Scenario, FrameContractPO, SupplierName " + _
                "  FROM         dbo.View_EditPO " + _
                " WHERE     (ProjectID =  " + "'" + DropDownListPrj.SelectedValue.ToString + "'" + " ) " + _
                " AND CO = 1"
            ElseIf DropDownListPrj.SelectedValue.ToString <> "0" AndAlso DropDownListSupplier.SelectedValue.ToString <> "0" Then
                SqlDataSourceEditPO.SelectCommand = "SELECT     PO_No, SupplierID, RTRIM(Description) AS Description, TotalPrice, RTRIM(PO_Currency) AS PO_Currency, VATpercent, RTRIM(CostCode) AS CostCode, RTRIM(Notes)  AS Notes, PO_Date, CO, Scenario, FrameContractPO, SupplierName " + _
               "  FROM         dbo.View_EditPO " + _
               " WHERE     (ProjectID =  " + "'" + DropDownListPrj.SelectedValue.ToString + "'" + " ) AND (SupplierID =  " + "'" + DropDownListSupplier.SelectedValue.ToString + "'" + "   )" + _
                " AND CO = 1"
            End If
        Else
            If Not IsPostBack Then
                SqlDataSourceEditPO.SelectCommand = "SELECT     PO_No, SupplierID, RTRIM(Description) AS Description, TotalPrice, RTRIM(PO_Currency) AS PO_Currency, VATpercent, RTRIM(CostCode) AS CostCode, RTRIM(Notes)  AS Notes, PO_Date, CO, Scenario, FrameContractPO, SupplierName " + _
                "  FROM         dbo.View_EditPO " + _
                " WHERE     (ProjectID =  " + "'" + DropDownListPrj.SelectedValue.ToString + "'" + " ) AND (SupplierID =  " + "'" + DropDownListSupplier.SelectedValue.ToString + "'" + "   )"
            ElseIf DropDownListPrj.SelectedValue.ToString <> "0" AndAlso DropDownListSupplier.SelectedValue.ToString = "" Then
                SqlDataSourceEditPO.SelectCommand = "SELECT     PO_No, SupplierID, RTRIM(Description) AS Description, TotalPrice, RTRIM(PO_Currency) AS PO_Currency, VATpercent, RTRIM(CostCode) AS CostCode, RTRIM(Notes)  AS Notes, PO_Date, CO, Scenario, FrameContractPO, SupplierName " + _
                "  FROM         dbo.View_EditPO " + _
                " WHERE     (ProjectID =  " + "'" + DropDownListPrj.SelectedValue.ToString + "'" + " ) "
            ElseIf DropDownListPrj.SelectedValue.ToString <> "0" AndAlso DropDownListSupplier.SelectedValue.ToString <> "0" Then
                SqlDataSourceEditPO.SelectCommand = "SELECT     PO_No, SupplierID, RTRIM(Description) AS Description, TotalPrice, RTRIM(PO_Currency) AS PO_Currency, VATpercent, RTRIM(CostCode) AS CostCode, RTRIM(Notes)  AS Notes, PO_Date, CO, Scenario, FrameContractPO, SupplierName " + _
               "  FROM         dbo.View_EditPO " + _
               " WHERE     (ProjectID =  " + "'" + DropDownListPrj.SelectedValue.ToString + "'" + " ) AND (SupplierID =  " + "'" + DropDownListSupplier.SelectedValue.ToString + "'" + "   )"
            End If
        End If


    End Sub

    Protected Sub GridViewEditPO_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GridViewEditPO.RowCancelingEdit
        ' Clear the error message.
        Message.Text = ""
        MessageCostCode.Visible = False
        'clear the PO no text and add parameters
        TextBoxPOValueCheck.Text = ""

    End Sub

    Protected Sub GridViewEditPO_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewEditPO.RowDataBound

        Dim MyTask As New MyCommonTasks
        MyTask.HoverEffectOnGridviewCells(sender, e.Row)

        If e.Row.RowType = DataControlRowType.DataRow Then
            If DirectCast(e.Row.FindControl("LabelCOItem"), Label) IsNot Nothing Then
                If DataBinder.Eval(e.Row.DataItem, "CO") = True Then
                    DirectCast(e.Row.FindControl("LabelCOItem"), Label).Text = "Change Order"
                End If
            End If
        End If

        If (e.Row.RowState = (DataControlRowState.Edit Or DataControlRowState.Alternate)) OrElse (e.Row.RowState = DataControlRowState.Edit) Then

            ' it transfers POno of selected row into textbox and add parameters
            Dim POno As Label = DirectCast(e.Row.FindControl("LabePO"), Label)
            TextBoxPOValueCheck.Text = POno.Text
            SqlDataSourceTotalInvoiced.SelectParameters("PO_No").DefaultValue = TextBoxPOValueCheck.Text
            SqlDataSourceTotalInvoiced.SelectParameters("PO_No").Type = TypeCode.String


            ' it transfers INN of selected row into textbox and add parameters
            Dim TextBoxINNvat As TextBox = DirectCast(e.Row.FindControl("SupplierIDTextBox"), TextBox)
            TextBoxINNvatFREECheck.Text = TextBoxINNvat.Text
            SqlDataSourceINNcheck.SelectParameters("SupplierID").DefaultValue = TextBoxINNvatFREECheck.Text
            SqlDataSourceINNcheck.SelectParameters("SupplierID").Type = TypeCode.String

        End If

        ' it will create a condition to enable/disable buttons depends on the user type
        If DirectCast(e.Row.FindControl("LinkButtonEdit"), LinkButton) IsNot Nothing Then
            SqlDataSourceFinanceCheck.SelectCommand = "SELECT RTRIM(CostCode) AS CostCode, RTRIM(Type) AS Type FROM dbo.Table7_CostCode WHERE (RTRIM(Type) = 'Finance') AND (RTRIM(CostCode) = '" + DirectCast(e.Row.FindControl("LabelItemCostCode"), Label).Text.ToString + "')"
            DropDownListFinanceCheck.DataBind()
            If DropDownListFinanceCheck.Items.Count > 0 Then
                If Roles.IsUserInRole("Finance") Then
                    DirectCast(e.Row.FindControl("LinkButtonEdit"), LinkButton).Enabled = True
                    DirectCast(e.Row.FindControl("LinkButtonDelete"), LinkButton).Enabled = True
                Else
                    DirectCast(e.Row.FindControl("LinkButtonEdit"), LinkButton).Enabled = False
                    DirectCast(e.Row.FindControl("LinkButtonDelete"), LinkButton).Enabled = False
                End If
            End If
        End If

        ' it will feed DDLcostCode depends on user type. This block will never run for non Finance Users, as i have created new conditions for edit and delete button above.
        ' ... but keep it here anyway
        If DirectCast(e.Row.FindControl("DropDownListCostCode"), DropDownList) IsNot Nothing Then
            SqlDataSourceFinanceCheck.SelectCommand = "SELECT RTRIM(CostCode) AS CostCode, RTRIM(Type) AS Type FROM dbo.Table7_CostCode WHERE (RTRIM(Type) = 'Finance') AND (RTRIM(CostCode) = '" + DataBinder.Eval(e.Row.DataItem, "CostCode").ToString + "')"
            DropDownListFinanceCheck.DataBind()

            If DropDownListFinanceCheck.Items.Count > 0 Then
                If Roles.IsUserInRole("Finance") Then
                    DirectCast(e.Row.FindControl("DropDownListCostCode"), DropDownList).Enabled = True
                Else
                    DirectCast(e.Row.FindControl("DropDownListCostCode"), DropDownList).Enabled = False
                End If
            End If

            Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT     RTRIM(Type) AS Type" + _
                                                    " FROM Table1_Project" + _
                                                    " WHERE     ProjectID =" + "'" + DropDownListPrj.SelectedValue.ToString + "'"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                Dim ProjectType As String
                ProjectType = dr(0).ToString()
                If ProjectType = "DataCenter" Then
                    DirectCast(e.Row.FindControl("SqlDataSourceCostCode"), SqlDataSource).SelectCommand = " SELECT * " +
                                                " FROM " +
                                                " (SELECT * FROM " +
                                                "  (SELECT TOP 1 RTRIM([CostCode]) AS CostCode " +
                                                " ,rtrim(CostCode) + replicate(char(160),12-len(CostCode)) + RTRIM(CodeDescription) AS CostCode_Description " +
                                                " FROM [dbo].[Table7_CostCode] " +
                                                " ORDER BY [CostCode] ASC) AS A " +
                                                " UNION ALL " +
                                                " SELECT RTRIM(dbo.Table7_CostCode.CostCode) AS CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 12 - LEN(dbo.Table7_CostCode.CostCode)) " +
                                                " + RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCode_Description " +
                                                " FROM dbo.aspnet_UsersInRoles INNER JOIN " +
                                                " dbo.aspnet_Users ON dbo.aspnet_UsersInRoles.UserId = dbo.aspnet_Users.UserId AND dbo.aspnet_UsersInRoles.UserId = dbo.aspnet_Users.UserId INNER JOIN " +
                                                " dbo.aspnet_Roles ON dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId AND dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId INNER JOIN " +
                                                " dbo.Table7_CostCode ON dbo.aspnet_Roles.RoleName = dbo.Table7_CostCode.Type " +
                                                " WHERE (dbo.aspnet_Users.UserName = '" + Page.User.Identity.Name.ToString + "') AND (dbo.Table7_CostCode.Type = N'Finance') " +
                                                " UNION ALL " +
                                                " SELECT * FROM " +
                                                " (SELECT     TOP (100) PERCENT RTRIM(CostCode) AS CostCode, RTRIM(CostCode) + REPLICATE(CHAR(160), 12 - LEN(CostCode)) + RTRIM(CodeDescription)  " +
                                                " AS CostCode_Description " +
                                                " FROM         dbo.Table7_CostCode " +
                                                " WHERE     (Type = 'DataCenter')) AS B) AS C " +
                                                " ORDER BY [CostCode] ASC "
                    DirectCast(e.Row.FindControl("SqlDataSourceCostCode"), SqlDataSource).DataBind()
                    DirectCast(e.Row.FindControl("DropDownListCostCode"), DropDownList).DataBind()
                ElseIf ProjectType = "FitOut" Then

                    ' Please a code block to decide CostCodeValidator enabled or disabled
                    Using conBudgetExist As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                        conBudgetExist.Open()
                        Dim sqlstringBudgetExist As String = " IF (select count(BudgetID) from Table_Budget " +
                        " inner join Table1_Project on Table1_Project.ProjectID = Table_Budget.ProjectID  " +
                        " where (Table_Budget.ProjectID = " + DropDownListPrj.SelectedValue.ToString + ") AND (Table1_Project.[Type] <> N'DataCenter')) > 0  " +
                        " Select N'BudgetExist' " +
                        " Else " +
                        " Select N'BudgetDoesntExist' "
                        Dim cmdBudgetExist As New SqlCommand(sqlstringBudgetExist, conBudgetExist)
                        cmdBudgetExist.CommandType = System.Data.CommandType.Text
                        Dim drBudgetExist As SqlDataReader = cmdBudgetExist.ExecuteReader
                        While drBudgetExist.Read
                            If drBudgetExist(0).ToString = "BudgetExist" Then
                                DirectCast(e.Row.FindControl("CompareValidatorCostCode"), CompareValidator).Enabled = True
                            ElseIf drBudgetExist(0).ToString = "BudgetDoesntExist" Then
                                DirectCast(e.Row.FindControl("CompareValidatorCostCode"), CompareValidator).Enabled = False
                            End If
                        End While
                        conBudgetExist.Close()
                        conBudgetExist.Dispose()
                        drBudgetExist.Close()

                    End Using

                    DirectCast(e.Row.FindControl("SqlDataSourceCostCode"), SqlDataSource).SelectCommand = " IF (select count(BudgetID) from Table_Budget " +
          " inner join Table1_Project on Table1_Project.ProjectID = Table_Budget.ProjectID  " +
          " where (Table_Budget.ProjectID = " + DropDownListPrj.SelectedValue.ToString + ") AND (Table1_Project.[Type] <> N'DataCenter')) > 0  " +
          "  " +
          " SELECT RTRIM(CostCode) AS CostCode, RTRIM(CostCode_Description) AS CostCode_Description " +
          "                                                         FROM " +
          "                                                         (SELECT * FROM " +
          "                                                         (SELECT TOP 1 RTRIM([CostCode]) AS CostCode " +
          "                                                               ,rtrim(CostCode) + replicate(char(160),20-len(CostCode)) + RTRIM(CodeDescription) AS CostCode_Description " +
          "                                                           FROM [dbo].[Table7_CostCode] " +
          "                                                           ORDER BY [CostCode] ASC) AS A " +
          "  " +
          "                                                         UNION ALL " +
          "  " +
          "                                                         SELECT     RTRIM(dbo.Table7_CostCode.CostCode) AS CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 20 - LEN(dbo.Table7_CostCode.CostCode))  " +
          "                                                                               + RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCode_Description " +
          "                                                         FROM         dbo.aspnet_UsersInRoles INNER JOIN " +
          "                                                                               dbo.aspnet_Users ON dbo.aspnet_UsersInRoles.UserId = dbo.aspnet_Users.UserId AND dbo.aspnet_UsersInRoles.UserId = dbo.aspnet_Users.UserId INNER JOIN " +
          "                                                                               dbo.aspnet_Roles ON dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId AND dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId INNER JOIN " +
          "                                                                               dbo.Table7_CostCode ON dbo.aspnet_Roles.RoleName = dbo.Table7_CostCode.Type " +
          "                                                         WHERE     (dbo.aspnet_Users.UserName = '" + Page.User.Identity.Name.ToString + "') AND (dbo.Table7_CostCode.Type = N'Finance') " +
          "  " +
          "                                                         UNION ALL " +
          "  " +
          "                                                         SELECT * FROM " +
          "                                                         (SELECT CostCode, CostCode_Description FROM " +
          " ( " +
          " SELECT     dbo.Table7_CostCode.CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 20 - LEN(dbo.Table7_CostCode.CostCode))  " +
          " + RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCode_Description " +
          " FROM         dbo.Table_Budget INNER JOIN " +
          " dbo.Table7_CostCode ON dbo.Table_Budget.CostCode = dbo.Table7_CostCode.CostCode " +
          " WHERE     (dbo.Table_Budget.ProjectID = " + DropDownListPrj.SelectedValue.ToString + ") AND (dbo.Table7_CostCode.Type <> N'Finance') " +
          " GROUP BY dbo.Table7_CostCode.CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 20 - LEN(dbo.Table7_CostCode.CostCode))  " +
          " + RTRIM(dbo.Table7_CostCode.CodeDescription) " +
          " HAVING      (dbo.Table7_CostCode.CostCode <> N'0') " +
          "  " +
          " UNION ALL " +
          "  " +
          " SELECT     dbo.Table7_CostCode.CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 20 - LEN(dbo.Table7_CostCode.CostCode))  " +
          "   + RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCode_Description " +
          " FROM         dbo.Table2_PONo INNER JOIN " +
          "   dbo.Table7_CostCode ON dbo.Table7_CostCode.CostCode = dbo.Table2_PONo.CostCode " +
          " WHERE     (dbo.Table7_CostCode.Type <> N'Finance') AND (dbo.Table2_PONo.Project_ID = " + DropDownListPrj.SelectedValue.ToString + ") " +
          " GROUP BY dbo.Table7_CostCode.CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 20 - LEN(dbo.Table7_CostCode.CostCode))  " +
          "   + RTRIM(dbo.Table7_CostCode.CodeDescription) " +
          " HAVING      (dbo.Table7_CostCode.CostCode <> N'0') " +
          " ) AS DataSource1 " +
          " GROUP BY CostCode, CostCode_Description) AS B) AS C " +
          "                                                         ORDER BY [CostCode] ASC " +
          "  " +
          " else " +
          "  " +
                                                 " SELECT *  " +
                                                 " FROM   " +
                                                 " (SELECT * FROM   " +
                                                 "  (SELECT TOP 1 RTRIM([CostCode]) AS CostCode   " +
                                                 " ,rtrim(CostCode) + replicate(char(160),20-len(CostCode)) + RTRIM(CodeDescription) AS CostCode_Description   " +
                                                 " FROM [dbo].[Table7_CostCode]   " +
                                                 " ORDER BY [CostCode] ASC) AS A   " +
                                                 " UNION ALL   " +
                                                 " SELECT RTRIM(dbo.Table7_CostCode.CostCode) AS CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 20 - LEN(dbo.Table7_CostCode.CostCode))   " +
                                                 " + RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCode_Description   " +
                                                 " FROM dbo.aspnet_UsersInRoles INNER JOIN   " +
                                                 " dbo.aspnet_Users ON dbo.aspnet_UsersInRoles.UserId = dbo.aspnet_Users.UserId AND dbo.aspnet_UsersInRoles.UserId = dbo.aspnet_Users.UserId INNER JOIN   " +
                                                 " dbo.aspnet_Roles ON dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId AND dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId INNER JOIN   " +
                                                 " dbo.Table7_CostCode ON dbo.aspnet_Roles.RoleName = dbo.Table7_CostCode.Type   " +
                                                 " WHERE (dbo.aspnet_Users.UserName = '" + Page.User.Identity.Name.ToString + "') AND (dbo.Table7_CostCode.Type = N'Finance')   " +
                                                 " UNION ALL   " +
                                                 " SELECT * FROM   " +
                                                 " (SELECT     TOP (100) PERCENT RTRIM(CostCode) AS CostCode, RTRIM(CostCode) + REPLICATE(CHAR(160), 20 - LEN(CostCode)) + RTRIM(CodeDescription)    " +
                                                 " AS CostCode_Description   " +
                                                 " FROM         dbo.Table7_CostCode   " +
                                                 " WHERE     (Type = 'FitOut') " +
                                                 "	AND CostCode NOT IN " +
                                                 "	( " +
                                                 "	select CostCode from Table7_CostCode " +
                                                 "	where CostCode like N'%[_]%' AND ISNUMERIC(RIGHT(RTRIM(CostCode),3)) = 1 AND RIGHT(RTRIM(CostCode),3) <> CONVERT(nvarChar(4)," + DropDownListPrj.SelectedValue.ToString + ") " +
                                                 "	) " +
                                                 " ) AS B) AS C   " +
                                                 "  ORDER BY [CostCode] ASC  "

                    DirectCast(e.Row.FindControl("SqlDataSourceCostCode"), SqlDataSource).DataBind()
                    DirectCast(e.Row.FindControl("DropDownListCostCode"), DropDownList).DataBind()
                End If
            End While
            DirectCast(e.Row.FindControl("DropDownListCostCode"), DropDownList).SelectedValue = DataBinder.Eval(e.Row.DataItem, "CostCode").ToString
        End If

        ' it shows HyperlinkEditSubPo if required
        ' Check if PO_total and TotalInvoice same
        If e.Row.RowType = DataControlRowType.DataRow Then
            If FullSubPoExist(DataBinder.Eval(e.Row.DataItem, "PO_No").ToString) Then
                Dim HyperlinkEditSubPo As HyperLink = DirectCast(e.Row.FindControl("HyperlinkEditSubPo"), HyperLink)
                Dim TextBoxTotalPrice As TextBox = DirectCast(e.Row.FindControl("TextBoxTotalPrice"), TextBox)
                If HyperlinkEditSubPo IsNot Nothing Then
                    HyperlinkEditSubPo.Visible = True
                    TextBoxTotalPrice.Enabled = False
                    HyperlinkEditSubPo.Attributes.Add("onclick", "javascript:w= window.open('POsubEdit.aspx?PO_No=" _
                                                      + DataBinder.Eval(e.Row.DataItem, "PO_No").ToString _
                                                      + "','SupplierByType','left=100,top=100,width=750,height=600,toolbar=0,resizable=0,scrollbars=yes');")
                End If
            End If
        End If

        ' it disable DDLCurrency if there is already a payment under this PO!
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim DropDownListCurrency As DropDownList = DirectCast(e.Row.FindControl("DropDownListCurrency"), DropDownList)
            Dim ButtonWhy As ImageButton = DirectCast(e.Row.FindControl("ButtonWhy"), ImageButton)
            If DropDownListCurrency IsNot Nothing Then
                If ThereIsPaymentUnderThisPo(DataBinder.Eval(e.Row.DataItem, "PO_No").ToString) Then
                    DropDownListCurrency.Visible = False
                    ButtonWhy.Visible = True
                End If
            End If
        End If

        ' It fixes column width problem
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Label1 As Label = DirectCast(e.Row.FindControl("Label1"), Label)

            If Label1 IsNot Nothing Then
                Label1.Text = Label1.Text.Replace(",", "," + " ")
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            If (DataBinder.Eval(e.Row.DataItem, "Scenario") <> 0 Or DataBinder.Eval(e.Row.DataItem, "FrameContractPO") = True) And
                PTS.CoreTables.CreateDataReader.Create_Table1_Project(DropDownListPrj.SelectedValue).NewGeneration = True Then

                Dim CheckBoxCO As CheckBox = DirectCast(e.Row.FindControl("CheckBoxCO"), CheckBox)
                Dim SupplierIDTextBox As TextBox = DirectCast(e.Row.FindControl("SupplierIDTextBox"), TextBox)
                Dim DescriptionTextBox As TextBox = DirectCast(e.Row.FindControl("DescriptionTextBox"), TextBox)
                Dim TextBoxTotalPrice As TextBox = DirectCast(e.Row.FindControl("TextBoxTotalPrice"), TextBox)
                Dim DropDownListCurrency As DropDownList = DirectCast(e.Row.FindControl("DropDownListCurrency"), DropDownList)
                Dim TextBoxVATPercent As TextBox = DirectCast(e.Row.FindControl("TextBoxVATPercent"), TextBox)
                Dim DropDownListCostCode As DropDownList = DirectCast(e.Row.FindControl("DropDownListCostCode"), DropDownList)
                Dim TextBoxPODateShown As TextBox = DirectCast(e.Row.FindControl("TextBoxPODateShown"), TextBox)
                Dim TextBox6 As TextBox = DirectCast(e.Row.FindControl("TextBox6"), TextBox)
                Dim LabelApprMxViolation As Label = DirectCast(e.Row.FindControl("LabelApprMxViolation"), Label)

                If CheckBoxCO IsNot Nothing Then
                    CheckBoxCO.Enabled = False
                    SupplierIDTextBox.Enabled = False
                    DescriptionTextBox.Enabled = False
                    TextBoxTotalPrice.Enabled = False
                    DropDownListCurrency.Enabled = False
                    TextBoxVATPercent.Enabled = False
                    DropDownListCostCode.Enabled = True
                    TextBoxPODateShown.Enabled = False
                    TextBox6.Enabled = False
                    LabelApprMxViolation.Text = "allow" ' This will allow user to update CostCode on RowUpdating event
                End If
            End If
        End If

        ' Hide DELETE button if it is violating Approval Matrix.
        ' Frame Contract > ALLOW
        ' Scenario >=1 DONT ALLOW
        If e.Row.RowType = DataControlRowType.DataRow Then
            If PTS.CoreTables.CreateDataReader.Create_Table1_Project(DropDownListPrj.SelectedValue).NewGeneration = True Then
                If DataBinder.Eval(e.Row.DataItem, "Scenario") >= 1 Then
                    Dim LinkButtonDelete As LinkButton = DirectCast(e.Row.FindControl("LinkButtonDelete"), LinkButton)
                    If LinkButtonDelete IsNot Nothing Then
                        LinkButtonDelete.Visible = False
                        e.Row.Cells(1).Text = DataBinder.Eval(e.Row.DataItem, "PO_No").ToString + "<br/><span style=" + """" + "color: #ff0000" + """" + ">There is referred contract</span>"
                    End If
                End If
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LinkButtonEdit As LinkButton = DirectCast(e.Row.FindControl("LinkButtonEdit"), LinkButton)
            Dim LinkButtonDelete As LinkButton = DirectCast(e.Row.FindControl("LinkButtonDelete"), LinkButton)
            Dim SupplierIDTextBox As TextBox = DirectCast(e.Row.FindControl("SupplierIDTextBox"), TextBox)
            Dim DescriptionTextBox As TextBox = DirectCast(e.Row.FindControl("DescriptionTextBox"), TextBox)
            Dim TextBoxTotalPrice As TextBox = DirectCast(e.Row.FindControl("TextBoxTotalPrice"), TextBox)
            Dim TextBoxVATPercent As TextBox = DirectCast(e.Row.FindControl("TextBoxVATPercent"), TextBox)
            Dim DropDownListCostCode As DropDownList = DirectCast(e.Row.FindControl("DropDownListCostCode"), DropDownList)
            Dim DropDownListCurrency As DropDownList = DirectCast(e.Row.FindControl("DropDownListCurrency"), DropDownList)
            Dim TextBoxPODateShown As TextBox = DirectCast(e.Row.FindControl("TextBoxPODateShown"), TextBox)
            Dim TextBox6 As TextBox = DirectCast(e.Row.FindControl("TextBox6"), TextBox)

            If Mid(DataBinder.Eval(e.Row.DataItem, "PO_No").ToString(), 4, 3) = "108" Then
                If Page.User.Identity.Name.ToLower = "roman" Then
                    If LinkButtonEdit IsNot Nothing Then
                        LinkButtonEdit.Enabled = True
                        LinkButtonDelete.Enabled = False
                    End If
                    If SupplierIDTextBox IsNot Nothing Then
                        SupplierIDTextBox.Enabled = True
                        DescriptionTextBox.Enabled = True
                        TextBoxTotalPrice.Enabled = False
                        TextBoxVATPercent.Enabled = False
                        DropDownListCostCode.Enabled = True
                        DropDownListCurrency.Enabled = False
                        TextBoxPODateShown.Enabled = True
                        TextBox6.Enabled = True
                    End If
                ElseIf Page.User.Identity.Name.ToLower = "savas" Then
                    If LinkButtonEdit IsNot Nothing Then
                        LinkButtonEdit.Enabled = True
                        LinkButtonDelete.Enabled = True
                    End If
                    If SupplierIDTextBox IsNot Nothing Then
                        SupplierIDTextBox.Enabled = True
                        DescriptionTextBox.Enabled = True
                        TextBoxTotalPrice.Enabled = True
                        TextBoxVATPercent.Enabled = True
                        DropDownListCostCode.Enabled = True
                        DropDownListCurrency.Enabled = True
                        TextBoxPODateShown.Enabled = True
                        TextBox6.Enabled = True
                    End If
                Else
                    If LinkButtonEdit IsNot Nothing Then
                        LinkButtonEdit.Enabled = False
                        LinkButtonDelete.Enabled = False
                    End If
                End If
            End If



        End If

        Dim SupplierIDTextBoxTransfer As TextBox = DirectCast(e.Row.FindControl("SupplierIDTextBox"), TextBox)
        If SupplierIDTextBoxTransfer IsNot Nothing Then
            If Session("CarrySupplierID") IsNot Nothing Then
                SupplierIDTextBoxTransfer.Text = Session("CarrySupplierID")

                Dim _id As String = Session("CarrySupplierID")

                Using context As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                    Dim LabelSupplierNameEdit As Label = DirectCast(e.Row.FindControl("LabelSupplierNameEdit"), Label)
                    Dim _Supplier = (From C In context.Table6_Supplier Where C.SupplierID = _id).ToList()(0)
                    LabelSupplierNameEdit.Text = _Supplier.SupplierName.ToString

                    context.Dispose()

                End Using

                Session.Remove("CarrySupplierID")

            End If
        End If

    End Sub

    Protected Function FullSubPoExist(ByVal PO_No As String) As Boolean
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT     COUNT(Table2_PONo_1.PO_No) AS CountOfPo " +
        " FROM         (SELECT     dbo.Table2_PONo.PO_No, SUM(dbo.Table2_PONo_Sub.TotalPrice) AS Sum_TotalSubPo " +
        "                        FROM          dbo.Table2_PONo_Sub INNER JOIN " +
        "                                               dbo.Table2_PONo ON dbo.Table2_PONo_Sub.PO_No = dbo.Table2_PONo.PO_No " +
        "                        GROUP BY dbo.Table2_PONo.PO_No) AS TotalSubPo INNER JOIN " +
        "                       dbo.Table2_PONo AS Table2_PONo_1 ON TotalSubPo.PO_No = Table2_PONo_1.PO_No AND  " +
        "                       TotalSubPo.Sum_TotalSubPo = Table2_PONo_1.TotalPrice " +
        " WHERE     (Table2_PONo_1.PO_No = N'" + PO_No + "') "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim FullSubPo As Boolean = False
            While dr.Read
                If dr(0) = 0 Then
                    FullSubPo = False
                Else
                    FullSubPo = True
                End If
            End While
            con.Close()
            con.Dispose()
            dr.Close()
            Return FullSubPo

        End Using

    End Function

    Protected Function ThereIsPaymentUnderThisPo(ByVal PO_No As String) As Boolean
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT     COUNT(dbo.Table5_PayLog.PayReqNo) AS CountOfPayment " +
        " FROM         dbo.Table3_Invoice INNER JOIN " +
        "                       dbo.Table4_PaymentRequest ON dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID INNER JOIN " +
        "                       dbo.Table2_PONo ON dbo.Table3_Invoice.PO_No = dbo.Table2_PONo.PO_No LEFT OUTER JOIN " +
        "                       dbo.Table5_PayLog ON dbo.Table4_PaymentRequest.PayReqNo = dbo.Table5_PayLog.PayReqNo " +
        " GROUP BY dbo.Table2_PONo.PO_No " +
        " HAVING      (dbo.Table2_PONo.PO_No = N'" + PO_No + "') "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim PaymentExist As Boolean = False
            While dr.Read
                If dr(0) > 0 Then
                    PaymentExist = True
                End If
            End While
            con.Close()
            con.Dispose()
            dr.Close()
            Return PaymentExist

        End Using
    End Function

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init

        'ScriptManager1.SupportsPartialRendering = False

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' it provides query parameter for DropDownListSupplier
        If IsPostBack Or Not IsPostBack Then
            TextBoxUserName.Text = Page.User.Identity.Name
        End If

        ' it removes SELECT PROJECT after posback
        If IsPostBack Then
            Dim controll1 As New String(Page.Request.Params.Get("__EVENTTARGET"))
            If (Not controll1 Is Nothing) Or (controll1 <> "") Then
                If controll1 = "ctl00$MainContent$DropDownListPrj" Then
                    If Me.DropDownListPrj.Items(0).ToString = "Select Project" Then
                        Me.DropDownListPrj.Items.RemoveAt(0)
                    End If
                End If
            End If
        End If


    End Sub

    Protected Sub GridViewEditPO_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles GridViewEditPO.RowDeleted
        ' it makes linkbutton invisible to prevent error animation

        Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Deleted Successfully!</p>")

    End Sub

    Protected Sub GridViewEditPO_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridViewEditPO.RowDeleting

        Dim index As Integer = Convert.ToInt32(e.RowIndex)
        Dim row As GridViewRow = GridViewEditPO.Rows(index)
        Dim LabelPO_NoInItem As Label = DirectCast(row.FindControl("LabelPO_NoInItem"), Label)

        If Not InvoiceExistAgainstThisPo(LabelPO_NoInItem.Text) AndAlso Not DeliveryDocExistAgainstThisPo(LabelPO_NoInItem.Text) Then

            PnlInvoice.Visible = False
            PnlDelivery.Visible = False

            Dim zoneId As String = "Russian Standard Time"
            Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
            Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

            Dim InstanceOfDeletion As String = "'" + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + "-" + Mid(result.ToString, 1, 2).ToString + " " + Mid(result.ToString, 12, 2).ToString + ":" + Mid(result.ToString, 15, 2).ToString + ":" + Mid(result.ToString, 18, 2).ToString + "'"

            Using cn As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                Dim cmd As New System.Data.SqlClient.SqlCommand()
                cmd.Connection = cn

                cmd.CommandText = "UPDATE Table2_PONo SET DeletedBy = " + InstanceOfDeletion + ", PersonDeleted = " + "'" + Page.User.Identity.Name.ToString + "'" + " WHERE PO_No = " + "'" + LabelPO_NoInItem.Text + "'"
                cmd.CommandType = System.Data.CommandType.Text
                cn.Open()
                cmd.ExecuteNonQuery()
                cn.Close()
                cn.Dispose()

            End Using

            ' Update Table_Contract if this PO assigned to
            Using cnContract As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                Dim cmdContract As New System.Data.SqlClient.SqlCommand()
                cmdContract.Connection = cnContract

                cmdContract.CommandText = "UPDATE Table_Contracts SET PO_No = NULL WHERE PO_No = " + "'" + LabelPO_NoInItem.Text + "'"
                cmdContract.CommandType = System.Data.CommandType.Text
                cnContract.Open()
                cmdContract.ExecuteNonQuery()
                cnContract.Close()
                cnContract.Dispose()

            End Using

            ' Update Table_Contract if this PO assigned to
            Using cnAddendum As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                Dim cmdAddendum As New System.Data.SqlClient.SqlCommand()
                cmdAddendum.Connection = cnAddendum

                cmdAddendum.CommandText = "UPDATE Table_Addendums SET PO_No = NULL WHERE PO_No = " + "'" + LabelPO_NoInItem.Text + "'"
                cmdAddendum.CommandType = System.Data.CommandType.Text
                cnAddendum.Open()
                cmdAddendum.ExecuteNonQuery()
                cnAddendum.Close()
                cnAddendum.Dispose()

            End Using
        Else
            ' Show modalpopup
            If InvoiceExistAgainstThisPo(LabelPO_NoInItem.Text) AndAlso DeliveryDocExistAgainstThisPo(LabelPO_NoInItem.Text) Then
                ModalPopupExtenderDeleteWarning.Show()
                PnlInvoice.Visible = True
                PnlDelivery.Visible = True
            End If

            If Not InvoiceExistAgainstThisPo(LabelPO_NoInItem.Text) AndAlso DeliveryDocExistAgainstThisPo(LabelPO_NoInItem.Text) Then
                ModalPopupExtenderDeleteWarning.Show()
                PnlInvoice.Visible = False
                PnlDelivery.Visible = True
            End If

            If InvoiceExistAgainstThisPo(LabelPO_NoInItem.Text) AndAlso Not DeliveryDocExistAgainstThisPo(LabelPO_NoInItem.Text) Then
                ModalPopupExtenderDeleteWarning.Show()
                PnlInvoice.Visible = True
                PnlDelivery.Visible = False
            End If

            e.Cancel = True

        End If

    End Sub

    Protected Function InvoiceExistAgainstThisPo(ByVal PO_No As String) As Boolean
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT     COUNT(dbo.Table3_Invoice.InvoiceID) AS CountOfInvoice " +
        " FROM         dbo.Table3_Invoice RIGHT OUTER JOIN " +
        "                       dbo.Table2_PONo ON dbo.Table3_Invoice.PO_No = dbo.Table2_PONo.PO_No " +
        " GROUP BY dbo.Table2_PONo.PO_No " +
        " HAVING      (dbo.Table2_PONo.PO_No = N'" + PO_No + "') "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim _Exist As Boolean = False
            While dr.Read
                If dr(0) > 0 Then
                    _Exist = True
                End If
            End While
            con.Close()
            con.Dispose()
            dr.Close()
            Return _Exist

        End Using

    End Function

    Protected Function DeliveryDocExistAgainstThisPo(ByVal _PO_No As String) As Boolean

        Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        Using con
            con.Open()
            Dim sqlstring As String = " SELECT Count(PO_No) As CountOfPo FROM ( " +
                                      " SELECT PO_No FROM Table_PO_Akt WHERE PO_No = @PO_No " +
                                      " UNION ALL " +
                                      " SELECT PO_No FROM Table_PO_Nakladnaya WHERE PO_No = @PO_No " +
                                      " ) AS DataSource "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            Dim PO_No As SqlParameter = cmd.Parameters.Add("@PO_No", System.Data.SqlDbType.NVarChar, 20)
            PO_No.Value = _PO_No
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim _Exist As Boolean = False
            While dr.Read
                If dr(0) > 0 Then
                    _Exist = True
                End If
            End While
            con.Close()
            con.Dispose()
            dr.Close()
            Return _Exist
        End Using

    End Function

    Protected Sub GridViewEditPO_RowUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdatedEventArgs) Handles GridViewEditPO.RowUpdated

        Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Updated Successfully!</p>")

        Message.Text = ""
        MessageCostCode.Visible = False
        DropDownListSupplier.DataBind()

        ' remove all Enter characters in Description of TABLE2_PO
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "Fix_ReturnCharacter_On_Table2_PO"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.StoredProcedure
            Dim dr As SqlDataReader = cmd.ExecuteReader
            con.Close()
            con.Dispose()
            dr.Close()

        End Using

    End Sub

    Protected Sub GridViewEditPO_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridViewEditPO.RowUpdating

        Dim index As Integer = Convert.ToInt32(e.RowIndex)
        Dim row As GridViewRow = GridViewEditPO.Rows(index)
        Dim TextBoxTotalPrice As TextBox = DirectCast(row.FindControl("TextBoxTotalPrice"), TextBox)
        Dim TextBoxVATPercent As TextBox = DirectCast(row.FindControl("TextBoxVATPercent"), TextBox)
        Dim LabelApprMxViolation As Label = DirectCast(row.FindControl("LabelApprMxViolation"), Label)
        Dim NewPoValueWithoutVAT As Double
        Dim POValueMinimumPossibility As Double

        Dim _LabePO As Label = DirectCast(row.FindControl("LabePO"), Label)
        Dim _TextBoxTotalPrice As TextBox = DirectCast(row.FindControl("TextBoxTotalPrice"), TextBox)
        Dim _DDLcurrency As DropDownList = DirectCast(row.FindControl("DropDownListCurrency"), DropDownList)

        Dim TotalPriceTextBox_ As TextBox = TextBoxTotalPrice
        Dim DropDownListProject_ As DropDownList = DropDownListPrj
        Dim FrameContractId As Integer = PTS_MERCURY.helper.Table2_PONo.GetRowByPrimaryKey(_LabePO.Text).FrameContractID

        If FrameContractId > 0 Then
            If PTS_MERCURY.helper.Table1_Project.GetRowByPrimaryKey(DropDownListProject_.SelectedValue).FrameBudgetEmailControl = True Then

                Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                    Dim bb = db.sp_FrameContractBudgetPoDetails(FrameContractId, PTS_MERCURY.helper.Table_Contracts.BudgetTolerancePercentage.TolerancePercent, CType(e.OldValues("TotalPrice"), Decimal), CType(TotalPriceTextBox_.Text, Decimal))

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
                            PTS_MERCURY.helper.EmailGenerator.FrameBudgetEmailGenerator.Send(Page.User.Identity.Name.ToLower.Trim(), FrameContractId, PTS_MERCURY.helper.EmailGenerator.FrameBudgetEmailGenerator.TypeOfNotification.BudgetexceedWarning)

                        End If

                    Next

                End Using

            End If
        End If

        'Check limits for updated PO value if exceeds Scenarios
        If PTS.CoreTables.CreateDataReader.Create_Table1_Project(DropDownListPrj.SelectedValue).NewGeneration = True Then
            If CalculateScenario.Calculate(Convert.ToDecimal(TextBoxTotalPrice.Text), Convert.ToDecimal(TextBoxVATPercent.Text), _DDLcurrency.SelectedValue.ToString) > 1 AndAlso
                PTS_MERCURY.helper.Table2_PONo.GetRowByPrimaryKey(_LabePO.Text).Scenario = 0 Then
                If LabelApprMxViolation.Text <> "allow" Then
                    Message.Text = " This new PO value requires Approval Matrix. Transaction cancelled "
                    e.Cancel = True
                End If
            End If
        End If

        If GetMinimumPoValueAgainstDeliveryDocs(_LabePO.Text) > Math.Round(Convert.ToDecimal(_TextBoxTotalPrice.Text), 2) Then

            Dim a As New MyCommonTasks
            a.SendEmailToAdmin(_LabePO.Text + " > " +
               "Po value cannot be less than this> " + GetMinimumPoValueAgainstDeliveryDocs(_LabePO.Text).ToString + " > " + Page.User.Identity.Name.ToString, "")

            LabelMinPo.Text = String.Format("{0:#,##0.00}", GetMinimumPoValueAgainstDeliveryDocs(_LabePO.Text)) + " " + _DDLcurrency.SelectedItem.Text.ToString
            ModalPopupExtenderDeleteWarning.Show()
            PnlInvoice.Visible = False
            PnlDelivery.Visible = False
            PnlDeliveryCoeff.Visible = True
            e.Cancel = True
            Exit Sub

        End If

        ' There will be two different scenario for updating
        'if thereis fullSubPo, then POValue to be ignored in update command
        If FullSubPoExist(DirectCast(row.FindControl("LabePO"), Label).Text) Then
            SqlDataSourceEditPO.UpdateCommand = "UPDATE View_EditPO SET SupplierID = @SupplierID,  Description = @Description, PO_Currency = @PO_Currency, VATpercent = @VATpercent, CostCode = @CostCode, Notes = @Notes, PO_Date = @PO_Date, UpdatedBy = @UpdatedBy, PersonUpdated = @PersonUpdated, CO = @CO WHERE (PO_No = @PO_No)"
        Else
            ' Evaluating revised PO value without VAT
            If DropDownListINNcheck.SelectedValue = True Then
                NewPoValueWithoutVAT = Math.Round((Convert.ToDouble(TextBoxTotalPrice.Text) / (1 + 0 / 100)), 2)
                POValueMinimumPossibility = Math.Round(Convert.ToDouble(DropDownListTotalInvoicedValue.SelectedValue), 2)
            Else
                NewPoValueWithoutVAT = Math.Round((Convert.ToDouble(TextBoxTotalPrice.Text) / (1 + Convert.ToDouble(TextBoxVATPercent.Text) / 100)), 2)
                POValueMinimumPossibility = Math.Round(Convert.ToDouble(DropDownListTotalInvoicedValue.SelectedValue) * (1 + Convert.ToDouble(TextBoxVATPercent.Text) / 100), 2)
            End If

            If NewPoValueWithoutVAT < DropDownListTotalInvoicedValue.SelectedValue Then
                If DropDownListINNcheck.SelectedValue = True Then
                    Message.Text = "PO value can not be less than " + String.Format("{0:#,##0.00}", POValueMinimumPossibility) + " without VAT as there is " + String.Format("{0:#,##0.00}", Convert.ToDouble(DropDownListTotalInvoicedValue.SelectedValue)) + " total invoiced value defined under VAT Free company"
                Else
                    Message.Text = "PO value can not be less than " + String.Format("{0:#,##0.00}", POValueMinimumPossibility) + " including " + TextBoxVATPercent.Text + "% VAT as there is " + String.Format("{0:#,##0.00}", Convert.ToDouble(DropDownListTotalInvoicedValue.SelectedValue)) + " total invoiced items without VAT"
                End If
                e.Cancel = True
            End If
        End If

        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)
        e.NewValues("UpdatedBy") = result

        e.NewValues("PersonUpdated") = Page.User.Identity.Name.ToString

        e.NewValues("SupplierID") = Mid(DirectCast(row.FindControl("SupplierIDTextBox"), TextBox).Text.ToString, 1, 12)

        Dim TextBoxPODateShown As TextBox = DirectCast(row.FindControl("TextBoxPODateShown"), TextBox)
        e.NewValues("PO_Date") = Convert.ToDateTime(Mid(TextBoxPODateShown.Text.ToString, 1, 2).ToString + "/" + Mid(TextBoxPODateShown.Text.ToString, 4, 2).ToString + "/" + Mid(TextBoxPODateShown.Text.ToString, 7, 4).ToString)

        If Convert.ToDateTime(result) < Convert.ToDateTime(Mid(TextBoxPODateShown.Text.ToString, 1, 2).ToString + "/" + Mid(TextBoxPODateShown.Text.ToString, 4, 2).ToString + "/" + Mid(TextBoxPODateShown.Text.ToString, 7, 4).ToString) Then
            Message.Text = "PO date can not be later than today"
            e.Cancel = True
        End If

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT     RTRIM(Type) AS Type" +
                                                    " FROM Table1_Project" +
                                                    " WHERE     ProjectID =" + "'" + DropDownListPrj.SelectedValue.ToString + "'"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                Dim ProjectType As String
                ProjectType = dr(0).ToString()
                If ProjectType = "DataCenter" Then
                    Dim DropDownListCostCode As DropDownList = DirectCast(row.FindControl("DropDownListCostCode"), DropDownList)
                    If DropDownListCostCode.SelectedValue.ToString = "0" Then
                        e.NewValues("CostCode") = DropDownListCostCode.SelectedValue.ToString
                        MessageCostCode.Visible = False
                    ElseIf Len(DropDownListCostCode.SelectedValue.ToString) = 10 Then
                        e.NewValues("CostCode") = DropDownListCostCode.SelectedValue.ToString
                        MessageCostCode.Visible = False
                    Else
                        If Roles.IsUserInRole("Finance") Then
                            If IsNumeric(DropDownListCostCode.SelectedValue.ToString) AndAlso Len(DropDownListCostCode.SelectedValue.ToString) < 10 Then
                                e.NewValues("CostCode") = DropDownListCostCode.SelectedValue.ToString
                                MessageCostCode.Visible = False
                            ElseIf Not IsNumeric(DropDownListCostCode.SelectedValue.ToString) AndAlso Len(DropDownListCostCode.SelectedValue.ToString) < 10 Then
                                MessageCostCode.Visible = True
                                e.Cancel = True
                            End If
                        Else
                            If Len(DropDownListCostCode.SelectedValue.ToString) < 10 Then
                                MessageCostCode.Visible = True
                                e.Cancel = True
                            End If
                        End If
                    End If
                Else
                    e.NewValues("CostCode") = DirectCast(row.FindControl("DropDownListCostCode"), DropDownList).SelectedValue.ToString
                    MessageCostCode.Visible = False
                End If
            End While

            dr.Close()
            con.Close()
            con.Dispose()

        End Using

        ' cancel row updating if supplierid "1111111111"
        Dim SupplierIDTextBox As TextBox = DirectCast(row.FindControl("SupplierIDTextBox"), TextBox)
        If SupplierIDTextBox IsNot Nothing Then
            If SupplierIDTextBox.Text = "1111111111" Then
                e.Cancel = True
            End If
        End If

        Dim _projectID As Short = DropDownListPrj.SelectedValue.ToString().Trim()
        Dim _costcode As String = DirectCast(row.FindControl("DropDownListCostCode"), DropDownList).SelectedValue.ToString().Trim()
        Dim _oldValue As Decimal = e.OldValues("TotalPrice")
        Dim _newValue As Decimal = e.NewValues("TotalPrice")
        Dim _vat As Decimal = e.NewValues("VATpercent")

        _oldValue = Math.Round(IIf(IsNumeric(_oldValue), _oldValue, 0) / ((100 + (IIf(IsNumeric(_vat), _vat, 0))) / 100), 2)
        _newValue = Math.Round(IIf(IsNumeric(_newValue), _newValue, 0) / ((100 + (IIf(IsNumeric(_vat), _vat, 0))) / 100), 2)

        If PTS_MERCURY.helper.Table_Budget.GetBudgetEmailControlFailed(Page,
                                                               _projectID,
                                                               _costcode,
                                                               _oldValue,
                                                               _newValue) =
                                                           True Then

            e.Cancel = True

        End If

    End Sub

    Protected Function GetMinimumPoValueAgainstDeliveryDocs(ByVal _PO_No As String) As Decimal

        Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        Using con
            con.Open()
            ' This SP returns Delivery Doc Coeff, if it is more than 1, it means PO goes less than 
            Dim sqlstring As String = "GetMinimumPoValueAgainstDeliveryDocs"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim PO_No As SqlParameter = cmd.Parameters.Add("@PO_No", System.Data.SqlDbType.NVarChar, 11)
            PO_No.Value = _PO_No

            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim _return As Decimal = 0
            While dr.Read
                _return = dr(0)
            End While
            con.Close()
            con.Dispose()
            dr.Close()
            Return _return
        End Using

    End Function

    Protected Sub DropDownListSupplier_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListSupplier.DataBound
        Dim lst As New ListItem("ALL SUPPLIER", "")
        DropDownListSupplier.Items.Insert(0, lst)

        If Request.QueryString("SupplierID") IsNot Nothing And Not IsPostBack Then
            DropDownListSupplier.SelectedValue = Request.QueryString("SupplierID")
        End If

    End Sub

    Protected Sub DropDownListSupplier_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListSupplier.SelectedIndexChanged

        Dim nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString())
        nameValues.Set("SupplierID", DropDownListSupplier.SelectedValue.ToString.Trim)

        If DropDownListSupplier.SelectedIndex = 0 Then
            nameValues.Remove("SupplierID")
        End If

        Dim url As String = Request.Url.AbsolutePath
        Dim updatedQueryString As String = "?" + nameValues.ToString()
        Response.Redirect(url + updatedQueryString)

    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

    End Sub

    Protected Sub SqlDataSourceSupplier_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles SqlDataSourceSupplier.DataBinding
    End Sub

    Protected Sub ButtonWhy_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub SupplierIDTextBox_TextChanged(sender As Object, e As EventArgs)

        Dim TextBoxSupplierID As TextBox = sender

        Dim _id As String = Left(TextBoxSupplierID.Text.ToString, 12).Trim

        Using context As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            Dim _Count = Aggregate C In context.Table6_Supplier Where C.SupplierID = _id Into Count()

            If _Count > 0 Then
                Session("CarrySupplierID") = Left(TextBoxSupplierID.Text.ToString, 12).Trim
            End If

            context.Dispose()

        End Using

    End Sub
End Class

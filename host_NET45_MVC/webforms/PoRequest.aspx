<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="PoRequest.aspx.vb" Inherits="PoRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<asp:TextBox ID="TextBoxCostCodeError" runat="server"  Text="Valid" CssClass="hidepanel"></asp:TextBox>

  <%-- MODAL POPUP REDIRECT --%>
  <asp:ModalPopupExtender ID="ModalPopupExtenderRedirect" runat="server"
   TargetControlID="ButtonRedirect"
   PopupControlID="PanelRedirect"
   BackgroundCssClass="modalBackground2"
   CancelControlID="btnCancelRedirect"
   PopupDragHandleControlID="PanelRedirect" >
  </asp:ModalPopupExtender>
  <asp:Panel ID="PanelRedirect" runat="server" Style="display:none;" >
     <div style="text-align: right">
                <asp:Button ID="btnCancelRedirect" runat="server" Text="X" cssClass="hidepanel"
                 Font-Bold="True" Font-Size="16px" ForeColor="White" BackColor="#FF0066" 
                 OnClientClick="changeClass" />
     </div>

      <asp:Literal ID="literalScenarioInfo" runat="server" ></asp:Literal>

      <br />
        <asp:Button id="ButtonContinue"  runat="server" />
  </asp:Panel>
  <asp:Button id="ButtonRedirect"  runat="server" CssClass="hidepanel"/>
  <%-- /MODAL POPUP REDIRECT --%>


<asp:Panel ID="PanelExistingSession" runat="server">

<table class="TableExistingSession">
  <tr>
   <td>
     <asp:ImageButton ID="ImageButtonFrameContract" runat="server" visible="false"
     ImageUrl="~/images/ContinueScnr-1.png" PostBackUrl="~/webforms/contractenterNew.aspx"/>
   </td>
    <td rowspan="8">
      <asp:ImageButton ID="ImageButtonRemoveSession" runat="server" 
        ImageUrl="~/images/RemoveScenarios.png" />
    </td>
  </tr>
  <tr>
   <td>
     <asp:ImageButton ID="ImageButtonContinueScenario1" runat="server" visible="false"
     ImageUrl="~/images/ContinueScnr1.png" />
   </td>
  </tr>
  <tr>
   <td>
     <asp:ImageButton ID="ImageButtonContinueScenario2" runat="server" visible="false"
     ImageUrl="~/images/ContinueScnr2.png" PostBackUrl="~/webforms/contractenterNew.aspx"/>
   </td>
  </tr>
  <tr>
   <td>
     <asp:ImageButton ID="ImageButtonContinueScenario3" runat="server" visible="false"
     ImageUrl="~/images/ContinueScnr3.png" PostBackUrl="~/webforms/contractenterNew.aspx"/>
   </td>
  </tr>
  <tr>
   <td>
     <asp:ImageButton ID="ImageButtonContinueScenario4" runat="server" visible="false"
     ImageUrl="~/images/ContinueScnr4.png" PostBackUrl="~/webforms/contractenterNew.aspx"/>
   </td>
  </tr>
  <tr>
   <td>
     <asp:ImageButton ID="ImageButtonContinueScenario5" runat="server" visible="false"
     ImageUrl="~/images/ContinueScnr5.png" PostBackUrl="~/webforms/contractenterNew.aspx"/>
   </td>
  </tr>
    <tr>
   <td>
     <asp:ImageButton ID="ImageButtonContinueScenario6" runat="server" visible="false"
     ImageUrl="~/images/ContinueScnr6.png" PostBackUrl="~/webforms/contractenterNew.aspx"/>
   </td>
    </tr>
    <tr>
   <td>
     <asp:ImageButton ID="ImageButtonContinueScenario7" runat="server" visible="false"
     ImageUrl="~/images/ContinueScnr7.png" PostBackUrl="~/webforms/contractenterNew.aspx"/>
   </td>
    </tr>
</table>

</asp:Panel>

<asp:Panel ID="PanelPOrequest" runat="server">

    <div class="DivPoRequest" >
        <asp:CheckBox ID="CheckBoxNominated" runat="server" Checked ="false" Visible="false"
        Text="Nominated SubContractor" AutoPostBack="true" />

        <asp:CheckBox ID="CheckBoxFrameContract" runat="server" Checked ="false" 
        Text="Frame Contract" AutoPostBack="true" />
        <br /> <br />
        <asp:CheckBox ID="CheckBoxSmallContract" runat="server" Checked ="false" 
        Text="Small Contract" AutoPostBack="true" visible="true" />
    </div>

    <table class="TablePoRequest">
     <tr>
      <td>
       Project Name
      </td>
      <td>
         <asp:DropDownList ID="DropDownListProject" runat="server" AutoPostBack="True" 
          onselectedindexchanged="DropDownListProject_SelectedIndexChanged" 
           DataSourceID="SqlDataSourceProject" 
              DataTextField="ProjectName" DataValueField="ProjectID">
         </asp:DropDownList>

         <asp:CompareValidator ID="CompareValidatorDropDownListPrj" runat="server" CssClass="LabelGeneral" Operator="NotEqual"
           ErrorMessage="Required" ControlToValidate="DropDownListProject" Display="Dynamic" ValueToCompare="0">
         </asp:CompareValidator>

          <asp:SqlDataSource ID="SqlDataSourceProject" runat="server" 
              ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
              SelectCommand="
			SELECT * FROM (
				SELECT 
                dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID))  
                AS ProjectName
                FROM Table_Approval_UserRolePrjectJunction 
                INNER JOIN dbo.Table1_Project ON dbo.Table_Approval_UserRolePrjectJunction.ProjectID = dbo.Table1_Project.ProjectID 
                WHERE RoleName = N'InitiateContractAndAddendum' AND UserName = @UserName AND (dbo.Table1_Project.CurrentStatus = 1) 

				UNION ALL

				SELECT 
                dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID))  
                AS ProjectName
                FROM Table_Approval_UserRolePrjectJunction 
                INNER JOIN dbo.Table1_Project ON dbo.Table_Approval_UserRolePrjectJunction.ProjectID = dbo.Table1_Project.ProjectID 
                WHERE RoleName = N'LawyerOnSite' AND UserName = @UserName AND (dbo.Table1_Project.CurrentStatus = 1) 
			) AS DataSource GROUP BY ProjectID, ProjectName
			ORDER BY ProjectName ASC ">
              <SelectParameters>
                  <asp:Parameter DefaultValue="_" Name="UserName" />
              </SelectParameters>
          </asp:SqlDataSource>
      </td> 
     </tr>
     <tr>
      <td>
       Po Value With VAT
      </td>
      <td>
                                <asp:TextBox ID="TotalPriceTextBox" runat="server"  
                                    Text='<%# Bind("TotalPrice") %>'  />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                  ControlToValidate="TotalPriceTextBox" CssClass="LabelGeneral" Display="Dynamic" 
                                  ErrorMessage="Required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                  ControlToValidate="TotalPriceTextBox" CssClass="LabelGeneral" Display="Dynamic" 
                                  ErrorMessage="not valid number" 
                                  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                                    </asp:RegularExpressionValidator>
                                <asp:CompareValidator ID="CompareValidatorPoValue" runat="server" ControlToValidate="TotalPriceTextBox"
                                    Display="Dynamic" ValueToCompare="0" Operator="GreaterThan" ErrorMessage="should be greater than 0" >

                                </asp:CompareValidator>
      </td> 
     </tr>
     <tr>
      <td>
       VAT Percent
      </td>
      <td>
                                <asp:TextBox ID="TextBoxVATpercent" runat="server" 
                                    Text='<%# Bind("VATpercent") %>' ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                  ControlToValidate="TextBoxVATpercent" CssClass="LabelGeneral" Display="Dynamic" 
                                  ErrorMessage="Required"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                  ControlToValidate="TextBoxVATpercent" CssClass="LabelGeneral" Display="Dynamic" 
                                  ErrorMessage="range to be 0-20" MaximumValue="20" MinimumValue="0" 
                                  Type="Double"></asp:RangeValidator>
                                <asp:FilteredTextBoxExtender ID="TextBoxVATpercent_FilteredTextBoxExtender" 
                                    runat="server" FilterType="Numbers" TargetControlID="TextBoxVATpercent">
                                </asp:FilteredTextBoxExtender>
      </td> 
     </tr>
     <tr>
      <td>
       Currency
      </td>
      <td>
                                <asp:DropDownList ID="DropDownListCurrency" runat="server" >
                                    <asp:ListItem Value="_" Selected="True">Select Currency</asp:ListItem>
                                    <asp:ListItem Value="Rub">Ruble</asp:ListItem>
                                    <asp:ListItem Value="Dollar">Dollar</asp:ListItem>
                                    <asp:ListItem Value="Euro">Euro</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidatorCurrency" runat="server" 
                                  ControlToValidate="DropDownListCurrency" CssClass="LabelGeneral" 
                                  Display="Dynamic" Enabled="true" ErrorMessage="please select currency" 
                                  Operator="NotEqual" Type="String" ValueToCompare="_">
                            </asp:CompareValidator>
      </td> 
     </tr>
     <tr>
      <td>
       Cost Code
      </td>
      <td>
        <asp:DropDownList ID="DropDownListCostCode" runat="server" 
            DataSourceID="SqlDataSourceCostCode" DataTextField="CostCode_Description" 
            DataValueField="CostCode"  Width="500px" Font-Size="11px" CssClass="ddl_fxfnt"
            onselectedindexchanged="DropDownListCostCode_SelectedIndexChanged" AutoPostBack="True" >
        </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourceCostCode" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 

                            SelectCommand="
IF (select count(BudgetID) from Table_Budget
inner join Table1_Project on Table1_Project.ProjectID = Table_Budget.ProjectID 
where (Table_Budget.ProjectID = @ProjectID) AND (Table1_Project.[Type] <> N'DataCenter')) > 0 

BEGIN

-- if user is Vusala, Show transport
IF (SELECT rtrim(@UserName) ) = N'vusala.gadjieva' AND (SELECT @ProjectID) = 168
	BEGIN
		SELECT * 
																FROM
																(SELECT * FROM
																(SELECT TOP 1 RTRIM([CostCode]) AS CostCode
																	  ,rtrim(CostCode) + replicate(char(160),20-len(CostCode)) + RTRIM(CodeDescription) AS CostCode_Description
																  FROM [dbo].[Table7_CostCode]
																  ORDER BY [CostCode] ASC) AS A

																UNION ALL

																SELECT     RTRIM(dbo.Table7_CostCode.CostCode) AS CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 20 - LEN(dbo.Table7_CostCode.CostCode)) 
																					  + RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCode_Description
																FROM         dbo.Table7_CostCode
																WHERE     (dbo.Table7_CostCode.CostCode = N'005')

																UNION ALL

																SELECT * FROM
																(SELECT     RTRIM(dbo.Table7_CostCode.CostCode) AS CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 20 - LEN(dbo.Table7_CostCode.CostCode)) 
																					  + RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCode_Description
																FROM         dbo.Table_Budget INNER JOIN
																					  dbo.Table7_CostCode ON dbo.Table_Budget.CostCode = dbo.Table7_CostCode.CostCode
																WHERE     (dbo.Table_Budget.ProjectID = @ProjectID) AND (dbo.Table7_CostCode.Type <> N'Finance')
																GROUP BY dbo.Table7_CostCode.CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 20 - LEN(dbo.Table7_CostCode.CostCode)) 
																					  + RTRIM(dbo.Table7_CostCode.CodeDescription)) AS B) AS C
																ORDER BY [CostCode] ASC
	END
ELSE
	BEGIN
-- Select only budget costcodes if there is budget
		SELECT * 
																FROM
																(SELECT * FROM
																(SELECT TOP 1 RTRIM([CostCode]) AS CostCode
																	  ,rtrim(CostCode) + replicate(char(160),20-len(CostCode)) + RTRIM(CodeDescription) AS CostCode_Description
																  FROM [dbo].[Table7_CostCode]
																  ORDER BY [CostCode] ASC) AS A

																UNION ALL

																SELECT     RTRIM(dbo.Table7_CostCode.CostCode) AS CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 20 - LEN(dbo.Table7_CostCode.CostCode)) 
																					  + RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCode_Description
																FROM         dbo.aspnet_UsersInRoles INNER JOIN
																					  dbo.aspnet_Users ON dbo.aspnet_UsersInRoles.UserId = dbo.aspnet_Users.UserId AND dbo.aspnet_UsersInRoles.UserId = dbo.aspnet_Users.UserId INNER JOIN
																					  dbo.aspnet_Roles ON dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId AND dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId INNER JOIN
																					  dbo.Table7_CostCode ON dbo.aspnet_Roles.RoleName = dbo.Table7_CostCode.Type
																WHERE     (dbo.aspnet_Users.UserName = @Username) AND (dbo.Table7_CostCode.Type = N'Finance')

																UNION ALL

																SELECT * FROM
																(SELECT     RTRIM(dbo.Table7_CostCode.CostCode) AS CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 20 - LEN(dbo.Table7_CostCode.CostCode)) 
																					  + RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCode_Description
																FROM         dbo.Table_Budget INNER JOIN
																					  dbo.Table7_CostCode ON dbo.Table_Budget.CostCode = dbo.Table7_CostCode.CostCode
																WHERE     (dbo.Table_Budget.ProjectID = @ProjectID) AND (dbo.Table7_CostCode.Type <> N'Finance')
																GROUP BY dbo.Table7_CostCode.CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 20 - LEN(dbo.Table7_CostCode.CostCode)) 
																					  + RTRIM(dbo.Table7_CostCode.CodeDescription)) AS B) AS C
																ORDER BY [CostCode] ASC
	END
END

ELSE

-- Select if there is no Budget for this project
SELECT *
                                                        FROM
                                                        (SELECT * FROM
                                                        (SELECT TOP 1 RTRIM([CostCode]) AS CostCode
                                                              ,rtrim(CostCode) + replicate(char(160),20-len(CostCode)) + RTRIM(CodeDescription) AS CostCode_Description
                                                          FROM [dbo].[Table7_CostCode]
                                                          ORDER BY [CostCode] ASC) AS A

                                                        UNION ALL

                                                        SELECT     RTRIM(dbo.Table7_CostCode.CostCode) AS CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 20 - LEN(dbo.Table7_CostCode.CostCode)) 
                                                                              + RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCode_Description
                                                        FROM         dbo.aspnet_UsersInRoles INNER JOIN
                                                                              dbo.aspnet_Users ON dbo.aspnet_UsersInRoles.UserId = dbo.aspnet_Users.UserId AND dbo.aspnet_UsersInRoles.UserId = dbo.aspnet_Users.UserId INNER JOIN
                                                                              dbo.aspnet_Roles ON dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId AND dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId INNER JOIN
                                                                              dbo.Table7_CostCode ON dbo.aspnet_Roles.RoleName = dbo.Table7_CostCode.Type
                                                        WHERE     (dbo.aspnet_Users.UserName = @username) AND (dbo.Table7_CostCode.Type = N'Finance')

                                                        UNION ALL

                                                        SELECT * FROM
                                                        (SELECT   RTRIM(dbo.Table7_CostCode.CostCode) AS CostCode , rtrim(CostCode) + replicate(char(160),20-len(CostCode)) + RTRIM(CodeDescription) AS CostCode_Description
                                                        FROM         dbo.Table1_Project INNER JOIN
                                                                              dbo.Table7_CostCode ON dbo.Table1_Project.Type = dbo.Table7_CostCode.Type
                                                        WHERE     (dbo.Table1_Project.ProjectID = @ProjectID )
                            
                                                        AND CostCode NOT IN
                                                        (
                                                        select CostCode from Table7_CostCode
                                                        where CostCode like N'%[_]%' AND ISNUMERIC(RIGHT(RTRIM(CostCode),3)) = 1 AND RIGHT(RTRIM(CostCode),3) <> CONVERT(nvarChar(4),@ProjectID)
                                                        )
                            ) AS B) AS C
                                                        ORDER BY [CostCode] ASC "
                                SelectCommandType="Text">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="DropDownListProject" DefaultValue="" 
                                            Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
                                        <asp:Parameter Name="UserName" Type="String" />
                                    </SelectParameters>
                            </asp:SqlDataSource>
        <br />
        <asp:CompareValidator ID="CompareValidatorCostCodeBudget" runat="server" 
          ControlToValidate="DropDownListCostCode" CssClass="LabelGeneral" 
          Display="Dynamic" Enabled="true" ErrorMessage="please select costcode" 
          Operator="NotEqual" Type="String" ValueToCompare="0">
                            </asp:CompareValidator>
        <asp:CompareValidator ID="CompareValidatorCostCode" runat="server" 
          ControlToValidate="TextBoxCostCodeError" CssClass="LabelGeneral" 
          Display="Dynamic" ErrorMessage="Cost Code must be 10 character" 
          Operator="Equal" Type="String" ValueToCompare="Valid">
                            </asp:CompareValidator>
      </td> 
     </tr>
     <tr>
      <td>
          <asp:Label ID="LabelRequestedBy" runat="server" Text="Who Req? :"  Visible="false"></asp:Label>
      </td>
      <td>
                            <asp:DropDownList ID="DropDownListRequestedBy" runat="server"  Visible="false"
                                ondatabound="DropDownListRequestedBy_DataBound" 
                                DataSourceID = "SqlDataSourceRequestedBy"
                                DataTextField="NameSurname" DataValueField="username">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourceRequestedBy" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                SelectCommand=" select rtrim(username) as username, RTRIM(NameSurname) AS NameSurname  from Table_PersonRequestPo where ProjectID = @ProjectID And Active = 1
                                 order by NameSurname asc ">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListProject" DefaultValue=0 
                                        Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:CompareValidator ID="CompareValidatorRequested" runat="server" 
                              ControlToValidate="DropDownListRequestedBy" CssClass="LabelGeneral" 
                              Display="Dynamic" ErrorMessage="Required" Operator="NotEqual" Type="String" 
                              ValueToCompare="0">
                            </asp:CompareValidator>
      </td>
     </tr>
     <tr>
      <td colspan="2">

       <asp:LinkButton ID="ButtonSendRequest" runat="server" OnClick="ButtonSendRequest_Click" CssClass="btn btn-success"><i class="ace-icon fa fa-check"></i> Send Request</asp:LinkButton>




      </td>
     </tr>
    </table>
</asp:Panel>




</asp:Content>


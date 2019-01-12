<%@ Page Title="" Language="VB"  MasterPageFile="~/site.master" EnableEventValidation ="false"  AutoEventWireup="false" CodeFile="CostReportEdit.aspx.vb" Inherits="CostReportEdit264" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <style type="text/css">
    .style1
    {
      width: 70px;
    }
  </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

                    <asp:Label ID="LabelCostCodeAddSelectItemDecision" runat="server"  Visible="false"></asp:Label>
                    <asp:Label ID="LabelResetDDLcostCodeAfterBudgetInsert" runat="server"  Visible="false"></asp:Label>
                    <asp:Label ID="LabelProgressExistOrNot" runat="server" Visible="false" ></asp:Label>
                    <asp:Label ID="LabelCostCodePrjIDExistOrNotForOtherCurreny" runat="server" Visible="false" ></asp:Label>

    <table >
        <tr>
            <td>
             <div>
              <table style="margin: 1px; border: thin solid #333333; background-color: #F5F5F5" >
                  <tr>
                      <td>
                          <asp:DropDownList ID="DropDownListPrj" runat="server"  AutoPostBack="true"
                              DataSourceID="SqlDataSourcePrj" DataTextField="ProjectName" 
                              DataValueField="ProjectID"  Font-Size="10px" Width="160px">
                          </asp:DropDownList>
                      </td>
                      <td>
                                  <asp:DropDownList ID="DropDownListCurrency" runat="server" 
                                      AutoPostBack="true" Font-Size="10px" Width="100px" >
                                      <asp:ListItem Value="-">Select Currency</asp:ListItem>
                                      <asp:ListItem Value="Euro">Euro</asp:ListItem>
                                      <asp:ListItem Value="Rub">Ruble</asp:ListItem>
                                      <asp:ListItem Value="Dollar">Dollar</asp:ListItem>
                                  </asp:DropDownList>
                                  <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                      ControlToValidate="DropDownListCurrency" Display="Dynamic" 
                                      ErrorMessage="Required" Font-Size="10px" Operator="NotEqual" ValueToCompare="-"></asp:CompareValidator>
                                      </td>
                  </tr>
              </table>
              </div>
                <div style="font-size: 10px; vertical-align: middle;">
   
                </div>
            </td>
            <td>

    <asp:Panel ID="PanelEnclose" runat="server" Visible="false" >
    <table style="margin: 1px; border: thin solid #333333; background-color: #F5F5F5" >
        <tr>
            <td>
                  </td>
            <td>
                   <FONT style="color: #808080; font-size: 10px; font-family: Arial;">Cost Code</FONT>     
                        <asp:CompareValidator ID="CompareValidator2" runat="server" 
                            ControlToValidate="DropDownListCostCode" Display="Dynamic" 
                            ErrorMessage="Required" Font-Size="10px" Operator="NotEqual" ValueToCompare="-"></asp:CompareValidator>
                        <asp:Label ID="labelCostCodeValidate" runat="server" Text="" Font-Size="10px" ForeColor="Red"></asp:Label> 
            </td>
            <td class="style1">
                   <FONT style="color: #808080; font-size: 10px; font-family: Arial;">Validated / Planned Cost</FONT>   
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorBudget" runat="server" 
                            ControlToValidate="TextBoxBudget"  Font-Size="10px"
                            ErrorMessage="not valid number" 
                            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td class="style1">
                  <FONT style="color: #808080; font-size: 10px; font-family: Arial;">Updated / Planned Cost</FONT>    
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorPlanned" runat="server" 
                            ControlToValidate="TextBoxPlanned"  Font-Size="10px"
                            ErrorMessage="not valid number" 
                            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td class="style1">
                  <FONT style="color: #808080; font-size: 10px; font-family: Arial;">Updated Planned Revenue</FONT>    
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ControlToValidate="TextBoxRevenue"  Font-Size="10px"
                            ErrorMessage="not valid number" 
                            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                  <asp:Button ID="ButtonNewBudget" runat="server" Text="Insert New Item" 
                      Font-Size="10px" Font-Bold="False" ForeColor="Blue" />
            </td>
            <td>
                        <asp:DropDownList ID="DropDownListCostCode" runat="server" 
                            DataSourceID="SqlDataSourceCostCode" DataTextField="CostCode_Description" 
                            DataValueField="CostCode" 
                            Font-Size="11px" Font-Names="Consolas" Width="500px" >
                        </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourceCostCode" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" >
                        </asp:SqlDataSource>
            </td>
            <td class="style1">
                      <asp:TextBox ID="TextBoxBudget" runat="server" Font-Size="10px" Width="65px"></asp:TextBox>
            </td>
            <td class="style1">
                      <asp:TextBox ID="TextBoxPlanned" runat="server" Font-Size="10px" Width="65px"></asp:TextBox>
            </td>
            <td class="style1">
                      <asp:TextBox ID="TextBoxRevenue" runat="server" Font-Size="10px" Width="65px"></asp:TextBox>
            </td>
        </tr>
        </table>
    </asp:Panel>
            </td>
        </tr>
    </table>

    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand="SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, (RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5),dbo.Table1_Project.ProjectID))) as ProjectName, dbo.aspnet_Users.UserName FROM         dbo.Table1_Project INNER JOIN  dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN  dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId WHERE     (dbo.aspnet_Users.UserName = @UserName) AND (dbo.Table1_Project.CurrentStatus = 1)  ORDER BY dbo.Table1_Project.ProjectName">

        <SelectParameters>
            <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" 
                PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="SqlDataSourceBudgetInsert" runat="server"
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" >
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="SqlDataSourceUpdateOrInsert" runat="server"
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>">
    </asp:SqlDataSource>
    
     <asp:DropDownList ID="DDLInsertOrUpdate" runat="server" 
        DataSourceID="SqlDataSourceUpdateOrInsert" DataTextField="ProjectID" 
        DataValueField="ProjectID"  Visible="False" >
    </asp:DropDownList>
    
     <asp:TextBox ID="TextBoxUserName" runat="server" Visible="False"></asp:TextBox>

    <asp:GridView ID="GridViewBudgetEuro" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="CostCode" DataSourceID="SqlDataSourceBudgetEuro" 
        CssClass="Grid" ShowFooter="True" >
        <Columns>
            
            <asp:TemplateField HeaderText="Cost Code" SortExpression="CostCodeDesc" HeaderStyle-Width="150" 
            ItemStyle-HorizontalAlign="Left" footerstyle-HorizontalAlign="Right" ControlStyle-Width="150px">
                <HeaderTemplate>
                    <font size="4"><font color="#0000FF"><b>IN EURO</b></font></font>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelDeletePossibleItem" runat="server" Text='<%# Eval("DeletePossible") %>' Visible="false"></asp:Label>
                    <asp:HyperLink ID="HyperLinkCostCodeComments" runat="server" Target="_blank" CssClass="Hlink" 
                    Text='<%# Bind("CostCodeDesc") %>' Font-Underline="False" ForeColor="Black" Font-Bold="false"></asp:HyperLink>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="LabelDeletePossibleEdit" runat="server" Text='<%# Eval("DeletePossible") %>' Visible="false"></asp:Label>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("CostCodeDesc") %>'></asp:Label>
                </EditItemTemplate>

                <FooterStyle HorizontalAlign="Right" />
<ControlStyle Width="150px" />
<HeaderStyle Width="150px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Cost Code" SortExpression="CostCode" HeaderStyle-Width="150"
             ItemStyle-HorizontalAlign="Right" footerstyle-HorizontalAlign="Right"  >
                <ItemTemplate>
                    <asp:Label ID="LabelCostCodeItem" runat="server" Text='<%# Eval("CostCode") %>' ></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="LabelCostCodeEdit" runat="server" Text='<%# Eval("CostCode") %>' ></asp:Label>
                </EditItemTemplate>
                <FooterTemplate>
                Total
                </FooterTemplate>

            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>

                                <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False" 
                                    CommandName="Edit" Text="Edit" Font-Size="10px"></asp:LinkButton>

                                <asp:LinkButton ID="LinkButtonDeleteItem" runat="server" CausesValidation="False"  OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                    CommandName="ActionDelete" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" Text="Delete" Font-Size="10px" ForeColor="Red"></asp:LinkButton>

                </ItemTemplate>
                <EditItemTemplate>

                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" ValidationGroup="Grid" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                        CommandName="ActionUpdate" Text="Update"  Font-Size="10px" ForeColor="#00CC00" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:LinkButton>

                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                        CommandName="Cancel" Text="Cancel" Font-Size="10px"></asp:LinkButton>

                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Validated BOQ Exc. VAT" SortExpression="Budget" ControlStyle-Width="126" HeaderStyle-Width="126" ItemStyle-HorizontalAlign="Right" footerstyle-HorizontalAlign="Right" FooterStyle-BackColor="WhiteSmoke">
                <ItemTemplate>
                    <asp:Label ID="LabelBudget" runat="server" Text='<%# Bind("Budget","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxBudget" runat="server" Text='<%# Eval("Budget") %>' 
                    Font-Size="10px" BackColor="#FF3300" ReadOnly="True"  > </asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorBudget" runat="server" 
                            ControlToValidate="TextBoxBudget" CssClass="LabelGeneral"  ValidationGroup="Grid"
                            ErrorMessage="not valid number" 
                            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" Display="Dynamic"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="LabelBudget" runat="server" ></asp:Label>
                </FooterTemplate>

<ControlStyle Width="126px"></ControlStyle>

<FooterStyle HorizontalAlign="Right" BackColor="#666666" ForeColor="White"></FooterStyle>

<HeaderStyle Width="126px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Validated Chage Order Budget exc.VAT" SortExpression="VCO" ControlStyle-Width="126" HeaderStyle-Width="126" ItemStyle-HorizontalAlign="Right" footerstyle-HorizontalAlign="Right" FooterStyle-BackColor="WhiteSmoke" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="LabelVCO" runat="server" Text='<%# Bind("VCO","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxVCO" runat="server" Text='<%# Eval("VCO") %>' 
                    Font-Size="10px" BackColor="#99FF66" ReadOnly="False" > </asp:TextBox>

                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="LabelVCO" runat="server" ></asp:Label>
                </FooterTemplate>

<ControlStyle Width="126px"></ControlStyle>

<FooterStyle HorizontalAlign="Right" BackColor="#666666" ForeColor="White"></FooterStyle>

<HeaderStyle Width="126px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="LabelBudgetId" runat="server" CssClass="budgetid hide" Text='<%# Eval("BudgetId")%>'></asp:Label>
                    <asp:CheckBox ID="cbxBudgetId" Checked="true" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Updated / planned costTotal Forecast Cost To Complete Project" SortExpression="PlannedToSpend" ControlStyle-Width="126" HeaderStyle-Width="126" ItemStyle-HorizontalAlign="Right" footerstyle-HorizontalAlign="Right" FooterStyle-BackColor="WhiteSmoke">
                <ItemTemplate>
                    <asp:Label ID="LabelPlannedToSpend" runat="server" Text='<%# Bind("PlannedToSpend","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxPlannedToSpend" runat="server" Text='<%# Eval("PlannedToSpend") %>'  BackColor="#99FF66" ReadOnly="False"
                     Font-Size="10px"  > </asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorPlanned" runat="server" 
                            ControlToValidate="TextBoxPlannedToSpend" CssClass="LabelGeneral"  ValidationGroup="Grid"
                            ErrorMessage="not valid number" 
                            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" Display="Dynamic"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="LabelPlannedToSpend" runat="server" ></asp:Label>
                </FooterTemplate>
<ControlStyle Width="126px"></ControlStyle>

<FooterStyle HorizontalAlign="Right" BackColor="#666666" ForeColor="White"></FooterStyle>

<HeaderStyle Width="126px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Updated Change Order Budget Exc.VAT" SortExpression="PlannedToSpendCO" ControlStyle-Width="126" HeaderStyle-Width="126" ItemStyle-HorizontalAlign="Right" footerstyle-HorizontalAlign="Right" FooterStyle-BackColor="WhiteSmoke" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="LabelPlannedToSpendCO" runat="server" Text='<%# Bind("PlannedToSpendCO","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxPlannedToSpendCO" runat="server" Text='<%# Eval("PlannedToSpendCO") %>'  BackColor="#99FF66" ReadOnly="False"
                     Font-Size="10px"  > </asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorPlannedCO" runat="server" 
                            ControlToValidate="TextBoxPlannedToSpendCO" CssClass="LabelGeneral"  ValidationGroup="Grid"
                            ErrorMessage="not valid number" 
                            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" Display="Dynamic"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="LabelPlannedToSpendCO" runat="server" ></asp:Label>
                </FooterTemplate>
<ControlStyle Width="126px"></ControlStyle>

<FooterStyle HorizontalAlign="Right" BackColor="#666666" ForeColor="White"></FooterStyle>

<HeaderStyle Width="126px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Updated Planned Revenue Exc.VAT" SortExpression="UpdatedPlannedRevenue" ControlStyle-Width="126" HeaderStyle-Width="126" ItemStyle-HorizontalAlign="Right" footerstyle-HorizontalAlign="Right" FooterStyle-BackColor="WhiteSmoke">
                <ItemTemplate>
                    <asp:Label ID="LabelUpdatedPlannedRevenue" runat="server" Text='<%# Bind("UpdatedPlannedRevenue","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxUpdatedPlannedRevenue" runat="server" Text='<%# Eval("UpdatedPlannedRevenue") %>'  
                    BackColor="#99FF66" Font-Size="10px"  > </asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorRevenue" runat="server" 
                            ControlToValidate="TextBoxUpdatedPlannedRevenue" CssClass="LabelGeneral"  ValidationGroup="Grid"
                            ErrorMessage="not valid number" 
                            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" Display="Dynamic"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="LabelUpdatedPlannedRevenue" runat="server" ></asp:Label>
                </FooterTemplate>
<ControlStyle Width="126px"></ControlStyle>

<FooterStyle HorizontalAlign="Right" BackColor="#666666" ForeColor="White"></FooterStyle>

<HeaderStyle Width="126px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Total Progress Percent" SortExpression="CompletionPercent" HeaderStyle-Width="50" ItemStyle-HorizontalAlign="Right" footerstyle-HorizontalAlign="Right" FooterStyle-BackColor="WhiteSmoke" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="LabelCompletionPercent" runat="server" Text='<%# Bind("CompletionPercent") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxCompletionPercent" runat="server" Text='<%# Eval("CompletionPercent") %>'  
                    BackColor="#99FF66" Font-Size="10px" Width="30px"></asp:TextBox>
                            <asp:RangeValidator ID="RangeValidator1" runat="server"  Display="Dynamic" ValidationGroup = "Grid"
                                ErrorMessage="out of range!" ControlToValidate="TextBoxCompletionPercent" 
                                CssClass="LabelGeneral" MaximumValue="100" MinimumValue="0" Type="Double" >
                            </asp:RangeValidator>
                </EditItemTemplate>
                <FooterTemplate>
                </FooterTemplate>

                <FooterStyle HorizontalAlign="Right" BackColor="#666666" ForeColor="White"></FooterStyle>

                <HeaderStyle Width="50px"></HeaderStyle>

                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>

          </Columns>
        <pagerstyle  horizontalalign="Center" CssClass="pager" />
        <RowStyle  CssClass="GridItemNakladnaya" Height="30px" />
        <HeaderStyle  CssClass="GridHeader" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceBudgetEuro" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand=" SELECT * FROM (
SELECT     dbo.View_Qry_CostCodeWithPoPerProjectEuro.ProjectID, RTRIM(dbo.View_Qry_CostCodeWithPoPerProjectEuro.CostCode) AS CostCode, 
                      RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCodeDesc, CASE WHEN Budget IS NULL THEN 0 ELSE Budget END AS Budget, 
                      CASE WHEN PlannedToSpend IS NULL THEN 0 ELSE PlannedToSpend END AS PlannedToSpend, 
                      CASE WHEN View_Qry_CostCodeWithPoPerProjectEuro.CostCode IS NULL THEN 1 ELSE 0 END AS DeletePossible, CASE WHEN CompletionPercent IS NULL 
                      THEN 0 ELSE CompletionPercent END AS CompletionPercent, dbo.Table7_CostDivision.CostVidisionID, dbo.Table_Budget.UpdatedPlannedRevenue,
                      CASE WHEN dbo.Table_Budget.VCO IS NULL THEN 0 ELSE dbo.Table_Budget.VCO END AS VCO, CASE WHEN dbo.Table_Budget.PlannedToSpendCO IS NULL THEN 0 ELSE dbo.Table_Budget.PlannedToSpendCO END AS PlannedToSpendCO, dbo.Table_Budget.BudgetId
FROM         dbo.View_Qry_CostCodeWithPoPerProjectEuro INNER JOIN
                      dbo.Table7_CostCode ON dbo.View_Qry_CostCodeWithPoPerProjectEuro.CostCode = dbo.Table7_CostCode.CostCode LEFT OUTER JOIN
                      dbo.Table7_CostDivision ON dbo.Table7_CostCode.CostVidisionID = dbo.Table7_CostDivision.CostVidisionID LEFT OUTER JOIN
                      dbo.Table_Budget ON dbo.View_Qry_CostCodeWithPoPerProjectEuro.Currency = dbo.Table_Budget.Currency AND 
                      dbo.View_Qry_CostCodeWithPoPerProjectEuro.ProjectID = dbo.Table_Budget.ProjectID AND 
                      dbo.View_Qry_CostCodeWithPoPerProjectEuro.CostCode = dbo.Table_Budget.CostCode LEFT OUTER JOIN
                      dbo.Table_ProgressPercent ON dbo.View_Qry_CostCodeWithPoPerProjectEuro.ProjectID = dbo.Table_ProgressPercent.ProjectID AND 
                      dbo.View_Qry_CostCodeWithPoPerProjectEuro.CostCode = dbo.Table_ProgressPercent.CostCode

UNION ALL

SELECT     dbo.Table_Budget.ProjectID, RTRIM(dbo.Table_Budget.CostCode) AS CostCode, RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCodeDesc, 
                      CASE WHEN Budget IS NULL THEN 0 ELSE Budget END AS Budget, CASE WHEN PlannedToSpend IS NULL THEN 0 ELSE PlannedToSpend END AS PlannedToSpend, 
                      CASE WHEN View_Qry_CostCodeWithPoPerProjectEuro.CostCode IS NULL THEN 1 ELSE 0 END AS DeletePossible, CASE WHEN CompletionPercent IS NULL 
                      THEN 0 ELSE CompletionPercent END AS CompletionPercent, dbo.Table7_CostDivision.CostVidisionID, dbo.Table_Budget.UpdatedPlannedRevenue,
                      CASE WHEN dbo.Table_Budget.VCO IS NULL THEN 0 ELSE dbo.Table_Budget.VCO END AS VCO, CASE WHEN dbo.Table_Budget.PlannedToSpendCO IS NULL THEN 0 ELSE dbo.Table_Budget.PlannedToSpendCO END AS PlannedToSpendCO, dbo.Table_Budget.BudgetId
FROM         dbo.Table7_CostCode INNER JOIN
                      dbo.Table_Budget ON dbo.Table7_CostCode.CostCode = dbo.Table_Budget.CostCode LEFT OUTER JOIN
                      dbo.Table7_CostDivision ON dbo.Table7_CostCode.CostVidisionID = dbo.Table7_CostDivision.CostVidisionID LEFT OUTER JOIN
                      dbo.Table_ProgressPercent ON dbo.Table_Budget.ProjectID = dbo.Table_ProgressPercent.ProjectID AND 
                      dbo.Table_Budget.CostCode = dbo.Table_ProgressPercent.CostCode LEFT OUTER JOIN
                      dbo.View_Qry_CostCodeWithPoPerProjectEuro ON dbo.Table_Budget.ProjectID = dbo.View_Qry_CostCodeWithPoPerProjectEuro.ProjectID AND 
                      dbo.Table_Budget.CostCode = dbo.View_Qry_CostCodeWithPoPerProjectEuro.CostCode
WHERE     (dbo.Table_Budget.Currency = N'Euro') AND (dbo.View_Qry_CostCodeWithPoPerProjectEuro.ProjectID IS NULL) AND 
                      (dbo.View_Qry_CostCodeWithPoPerProjectEuro.CostCode IS NULL)
                      ) AS DataSource1
                      WHERE ProjectID = @ProjectID
                      ORDER BY CostVidisionID, CostCode" >
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" Name="ProjectID" 
                PropertyName="SelectedValue" Type="Int16" />
        </SelectParameters>
       </asp:SqlDataSource>
       
        <%-- DOLLAR  --%> <%-- DOLLAR  --%> <%-- DOLLAR  --%> <%-- DOLLAR  --%> <%-- DOLLAR  --%> <%-- DOLLAR  --%> <%-- DOLLAR  --%> <%-- DOLLAR  --%> 
    <asp:GridView ID="GridViewBudgetDollar" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="CostCode" DataSourceID="SqlDataSourceBudgetDollar" 
        CssClass="Grid" ShowFooter="True" >
        <Columns>
            
            <asp:TemplateField HeaderText="Cost Code" SortExpression="CostCodeDesc" ControlStyle-Width="150" HeaderStyle-Width="150" 
            ItemStyle-HorizontalAlign="Left" footerstyle-HorizontalAlign="Right" >
                <HeaderTemplate>
                    <font size="4"><font color="#0000FF"><b>IN DOLLAR</b></font></font>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelDeletePossibleItem" runat="server" Text='<%# Eval("DeletePossible") %>' Visible="false"></asp:Label>
                    <asp:HyperLink ID="HyperLinkCostCodeComments" runat="server" Target="_blank" CssClass="Hlink" 
                    Text='<%# Bind("CostCodeDesc") %>' Font-Underline="False" ForeColor="Black" Font-Bold="false"></asp:HyperLink>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="LabelDeletePossibleEdit" runat="server" Text='<%# Eval("DeletePossible") %>' Visible="false"></asp:Label>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("CostCodeDesc") %>'></asp:Label>
                </EditItemTemplate>

<ControlStyle Width="150px"></ControlStyle>

                <FooterStyle HorizontalAlign="Right" />

<HeaderStyle Width="150px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Cost Code" SortExpression="CostCode" HeaderStyle-Width="150"
             ItemStyle-HorizontalAlign="Right" footerstyle-HorizontalAlign="Right"  >
                <ItemTemplate>
                    <asp:Label ID="LabelCostCodeItem" runat="server" Text='<%# Eval("CostCode") %>' ></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="LabelCostCodeEdit" runat="server" Text='<%# Eval("CostCode") %>' ></asp:Label>
                </EditItemTemplate>
                <FooterTemplate>
                Total
                </FooterTemplate>

                <FooterStyle HorizontalAlign="Right" />

                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>

                                <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False" 
                                    CommandName="Edit" Text="Edit" Font-Size="10px"></asp:LinkButton>

                                <asp:LinkButton ID="LinkButtonDeleteItem" runat="server" CausesValidation="False"  OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                    CommandName="ActionDelete" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" Text="Delete" Font-Size="10px" ForeColor="Red"></asp:LinkButton>

                </ItemTemplate>
                <EditItemTemplate>

                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" ValidationGroup="Grid" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                        CommandName="ActionUpdate" Text="Update"  Font-Size="10px" ForeColor="#00CC00" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:LinkButton>

                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                        CommandName="Cancel" Text="Cancel" Font-Size="10px"></asp:LinkButton>

                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Validated BOQ Exc. VAT" SortExpression="Budget" ControlStyle-Width="126" HeaderStyle-Width="126" ItemStyle-HorizontalAlign="Right" footerstyle-HorizontalAlign="Right" FooterStyle-BackColor="WhiteSmoke">
                <ItemTemplate>
                    <asp:Label ID="LabelBudget" runat="server" Text='<%# Bind("Budget","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxBudget" runat="server" Text='<%# Eval("Budget") %>' 
                    Font-Size="10px" BackColor="#FF3300" ReadOnly="True"> </asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorBudget" runat="server" 
                            ControlToValidate="TextBoxBudget" CssClass="LabelGeneral"  ValidationGroup="Grid"
                            ErrorMessage="not valid number" 
                            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" Display="Dynamic"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="LabelBudget" runat="server" ></asp:Label>
                </FooterTemplate>

<ControlStyle Width="126px"></ControlStyle>

<FooterStyle HorizontalAlign="Right" BackColor="#666666" ForeColor="White"></FooterStyle>

<HeaderStyle Width="126px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Validated Chage Order Budget exc.VAT" SortExpression="VCO" ControlStyle-Width="126" HeaderStyle-Width="126" ItemStyle-HorizontalAlign="Right" footerstyle-HorizontalAlign="Right" FooterStyle-BackColor="WhiteSmoke" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="LabelVCO" runat="server" Text='<%# Bind("VCO","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxVCO" runat="server" Text='<%# Eval("VCO") %>' 
                    Font-Size="10px" BackColor="#99FF66" ReadOnly="False"  > </asp:TextBox>

                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="LabelVCO" runat="server" ></asp:Label>
                </FooterTemplate>

<ControlStyle Width="126px"></ControlStyle>

<FooterStyle HorizontalAlign="Right" BackColor="#666666" ForeColor="White"></FooterStyle>

<HeaderStyle Width="126px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="LabelBudgetId" runat="server" CssClass="budgetid hide" Text='<%# Eval("BudgetId")%>'></asp:Label>
                    <asp:CheckBox ID="cbxBudgetId" Checked="true" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Updated / planned costTotal Forecast Cost To Complete Project" SortExpression="PlannedToSpend" ControlStyle-Width="126" HeaderStyle-Width="126" ItemStyle-HorizontalAlign="Right" footerstyle-HorizontalAlign="Right" FooterStyle-BackColor="WhiteSmoke">
                <ItemTemplate>
                    <asp:Label ID="LabelPlannedToSpend" runat="server" Text='<%# Bind("PlannedToSpend","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxPlannedToSpend" runat="server" Text='<%# Eval("PlannedToSpend") %>'  BackColor="#99FF66" Font-Size="10px"> </asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorPlanned" runat="server" 
                            ControlToValidate="TextBoxPlannedToSpend" CssClass="LabelGeneral"  ValidationGroup="Grid"
                            ErrorMessage="not valid number" 
                            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" Display="Dynamic"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="LabelPlannedToSpend" runat="server" ></asp:Label>
                </FooterTemplate>
<ControlStyle Width="126px"></ControlStyle>

<FooterStyle HorizontalAlign="Right" BackColor="#666666" ForeColor="White"></FooterStyle>

<HeaderStyle Width="126px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Updated Change Order Budget Exc.VAT" SortExpression="PlannedToSpendCO" ControlStyle-Width="126" HeaderStyle-Width="126" ItemStyle-HorizontalAlign="Right" footerstyle-HorizontalAlign="Right" FooterStyle-BackColor="WhiteSmoke" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="LabelPlannedToSpendCO" runat="server" Text='<%# Bind("PlannedToSpendCO","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxPlannedToSpendCO" runat="server" Text='<%# Eval("PlannedToSpendCO") %>'  BackColor="#99FF66" ReadOnly="False"
                     Font-Size="10px"  > </asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorPlannedCO" runat="server" 
                            ControlToValidate="TextBoxPlannedToSpendCO" CssClass="LabelGeneral"  ValidationGroup="Grid"
                            ErrorMessage="not valid number" 
                            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" Display="Dynamic"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="LabelPlannedToSpendCO" runat="server" ></asp:Label>
                </FooterTemplate>
<ControlStyle Width="126px"></ControlStyle>

<FooterStyle HorizontalAlign="Right" BackColor="#666666" ForeColor="White"></FooterStyle>

<HeaderStyle Width="126px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Updated Planned Revenue Exc.VAT" SortExpression="UpdatedPlannedRevenue" ControlStyle-Width="126" HeaderStyle-Width="126" ItemStyle-HorizontalAlign="Right" footerstyle-HorizontalAlign="Right" FooterStyle-BackColor="WhiteSmoke">
                <ItemTemplate>
                    <asp:Label ID="LabelUpdatedPlannedRevenue" runat="server" Text='<%# Bind("UpdatedPlannedRevenue","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxUpdatedPlannedRevenue" runat="server" Text='<%# Eval("UpdatedPlannedRevenue") %>'  BackColor="#99FF66" Font-Size="10px"> </asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorRevenue" runat="server" 
                            ControlToValidate="TextBoxUpdatedPlannedRevenue" CssClass="LabelGeneral"  ValidationGroup="Grid"
                            ErrorMessage="not valid number" 
                            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" Display="Dynamic"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="LabelUpdatedPlannedRevenue" runat="server" ></asp:Label>
                </FooterTemplate>
<ControlStyle Width="126px"></ControlStyle>

<FooterStyle HorizontalAlign="Right" BackColor="#666666" ForeColor="White"></FooterStyle>

<HeaderStyle Width="126px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Total Progress Percent" SortExpression="CompletionPercent" HeaderStyle-Width="50" ItemStyle-HorizontalAlign="Right" footerstyle-HorizontalAlign="Right" FooterStyle-BackColor="WhiteSmoke" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="LabelCompletionPercent" runat="server" Text='<%# Bind("CompletionPercent") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxCompletionPercent" runat="server" Text='<%# Eval("CompletionPercent") %>' 
                     BackColor="#99FF66" Font-Size="10px" Width="30px"> </asp:TextBox>
                            <asp:RangeValidator ID="RangeValidator1" runat="server"  Display="Dynamic" ValidationGroup = "Grid"
                                ErrorMessage="out of range!" ControlToValidate="TextBoxCompletionPercent" 
                                CssClass="LabelGeneral" MaximumValue="100" MinimumValue="0" Type="Double" >
                            </asp:RangeValidator>
                </EditItemTemplate>
                <FooterTemplate>
                </FooterTemplate>

                <FooterStyle HorizontalAlign="Right" BackColor="#666666" ForeColor="White"></FooterStyle>

                <HeaderStyle Width="50px"></HeaderStyle>

                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>
              
          </Columns>
        <pagerstyle  horizontalalign="Center" CssClass="pager" />
        <RowStyle  CssClass="GridItemNakladnaya" Height="30px" />
        <HeaderStyle  CssClass="GridHeader" />
    </asp:GridView>
    

    <asp:SqlDataSource ID="SqlDataSourceBudgetDollar" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand=" SELECT * FROM (
SELECT     dbo.View_Qry_CostCodeWithPoPerProjectDollar.ProjectID, RTRIM(dbo.View_Qry_CostCodeWithPoPerProjectDollar.CostCode) AS CostCode, 
                      RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCodeDesc, CASE WHEN Budget IS NULL THEN 0 ELSE Budget END AS Budget, 
                      CASE WHEN PlannedToSpend IS NULL THEN 0 ELSE PlannedToSpend END AS PlannedToSpend, 
                      CASE WHEN View_Qry_CostCodeWithPoPerProjectDollar.CostCode IS NULL THEN 1 ELSE 0 END AS DeletePossible, CASE WHEN CompletionPercent IS NULL 
                      THEN 0 ELSE CompletionPercent END AS CompletionPercent, dbo.Table7_CostDivision.CostVidisionID, dbo.Table_Budget.UpdatedPlannedRevenue,
                      CASE WHEN dbo.Table_Budget.VCO IS NULL THEN 0 ELSE dbo.Table_Budget.VCO END AS VCO, CASE WHEN dbo.Table_Budget.PlannedToSpendCO IS NULL THEN 0 ELSE dbo.Table_Budget.PlannedToSpendCO END AS PlannedToSpendCO, dbo.Table_Budget.BudgetId
FROM         dbo.View_Qry_CostCodeWithPoPerProjectDollar INNER JOIN
                      dbo.Table7_CostCode ON dbo.View_Qry_CostCodeWithPoPerProjectDollar.CostCode = dbo.Table7_CostCode.CostCode LEFT OUTER JOIN
                      dbo.Table7_CostDivision ON dbo.Table7_CostCode.CostVidisionID = dbo.Table7_CostDivision.CostVidisionID LEFT OUTER JOIN
                      dbo.Table_Budget ON dbo.View_Qry_CostCodeWithPoPerProjectDollar.Currency = dbo.Table_Budget.Currency AND 
                      dbo.View_Qry_CostCodeWithPoPerProjectDollar.ProjectID = dbo.Table_Budget.ProjectID AND 
                      dbo.View_Qry_CostCodeWithPoPerProjectDollar.CostCode = dbo.Table_Budget.CostCode LEFT OUTER JOIN
                      dbo.Table_ProgressPercent ON dbo.View_Qry_CostCodeWithPoPerProjectDollar.ProjectID = dbo.Table_ProgressPercent.ProjectID AND 
                      dbo.View_Qry_CostCodeWithPoPerProjectDollar.CostCode = dbo.Table_ProgressPercent.CostCode

UNION ALL

SELECT     dbo.Table_Budget.ProjectID, RTRIM(dbo.Table_Budget.CostCode) AS CostCode, RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCodeDesc, 
                      CASE WHEN Budget IS NULL THEN 0 ELSE Budget END AS Budget, CASE WHEN PlannedToSpend IS NULL THEN 0 ELSE PlannedToSpend END AS PlannedToSpend, 
                      CASE WHEN View_Qry_CostCodeWithPoPerProjectDollar.CostCode IS NULL THEN 1 ELSE 0 END AS DeletePossible, CASE WHEN CompletionPercent IS NULL 
                      THEN 0 ELSE CompletionPercent END AS CompletionPercent, dbo.Table7_CostDivision.CostVidisionID, dbo.Table_Budget.UpdatedPlannedRevenue,
                      CASE WHEN dbo.Table_Budget.VCO IS NULL THEN 0 ELSE dbo.Table_Budget.VCO END AS VCO, CASE WHEN dbo.Table_Budget.PlannedToSpendCO IS NULL THEN 0 ELSE dbo.Table_Budget.PlannedToSpendCO END AS PlannedToSpendCO, dbo.Table_Budget.BudgetId
FROM         dbo.Table7_CostCode INNER JOIN
                      dbo.Table_Budget ON dbo.Table7_CostCode.CostCode = dbo.Table_Budget.CostCode LEFT OUTER JOIN
                      dbo.Table7_CostDivision ON dbo.Table7_CostCode.CostVidisionID = dbo.Table7_CostDivision.CostVidisionID LEFT OUTER JOIN
                      dbo.Table_ProgressPercent ON dbo.Table_Budget.ProjectID = dbo.Table_ProgressPercent.ProjectID AND 
                      dbo.Table_Budget.CostCode = dbo.Table_ProgressPercent.CostCode LEFT OUTER JOIN
                      dbo.View_Qry_CostCodeWithPoPerProjectDollar ON dbo.Table_Budget.ProjectID = dbo.View_Qry_CostCodeWithPoPerProjectDollar.ProjectID AND 
                      dbo.Table_Budget.CostCode = dbo.View_Qry_CostCodeWithPoPerProjectDollar.CostCode
WHERE     (dbo.Table_Budget.Currency = N'Dollar') AND (dbo.View_Qry_CostCodeWithPoPerProjectDollar.ProjectID IS NULL) AND 
                      (dbo.View_Qry_CostCodeWithPoPerProjectDollar.CostCode IS NULL)
                      ) AS DataSource1
                      WHERE ProjectID = @ProjectID
                      ORDER BY CostVidisionID, CostCode " >
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" Name="ProjectID" 
                PropertyName="SelectedValue" Type="Int16" />
        </SelectParameters>
       </asp:SqlDataSource>
    
    
        <%-- RUBLE  --%>   <%-- RUBLE  --%> <%-- RUBLE  --%> <%-- RUBLE  --%> <%-- RUBLE  --%> <%-- RUBLE  --%> <%-- RUBLE  --%> <%-- RUBLE  --%> 
       
    <asp:GridView ID="GridViewBudgetRuble" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="CostCode" DataSourceID="SqlDataSourceBudgetRuble" 
        CssClass="Grid" ShowFooter="True" >
        <Columns>
            
            <asp:TemplateField HeaderText="Cost Code" SortExpression="CostCodeDesc" ControlStyle-Width="150" HeaderStyle-Width="150" 
            ItemStyle-HorizontalAlign="Left" footerstyle-HorizontalAlign="Right" >
                <HeaderTemplate>
                    <font size="4"><font color="#0000FF"><b>IN RUBLE</b></font></font>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelDeletePossibleItem" runat="server" Text='<%# Eval("DeletePossible") %>' Visible="false"></asp:Label>
                    <asp:HyperLink ID="HyperLinkCostCodeComments" runat="server" Target="_blank" CssClass="Hlink" 
                    Text='<%# Bind("CostCodeDesc") %>' Font-Underline="False" ForeColor="Black" Font-Bold="false"></asp:HyperLink>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="LabelDeletePossibleEdit" runat="server" Text='<%# Eval("DeletePossible") %>' Visible="false"></asp:Label>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("CostCodeDesc") %>'></asp:Label>
                </EditItemTemplate>

<ControlStyle Width="150px"></ControlStyle>

                <FooterStyle HorizontalAlign="Right" />

<HeaderStyle Width="150px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Cost Code" SortExpression="CostCode" HeaderStyle-Width="150"
             ItemStyle-HorizontalAlign="Right" footerstyle-HorizontalAlign="Right"  >
                <ItemTemplate>
                    <asp:Label ID="LabelCostCodeItem" runat="server" Text='<%# Eval("CostCode") %>' ></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="LabelCostCodeEdit" runat="server" Text='<%# Eval("CostCode") %>' ></asp:Label>
                </EditItemTemplate>
                <FooterTemplate>
                Total
                </FooterTemplate>

                <FooterStyle HorizontalAlign="Right" />

                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>

                                <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False" 
                                    CommandName="Edit" Text="Edit" Font-Size="10px"></asp:LinkButton>

                                <asp:LinkButton ID="LinkButtonDeleteItem" runat="server" CausesValidation="False"  OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                    CommandName="ActionDelete" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" Text="Delete" Font-Size="10px" ForeColor="Red"></asp:LinkButton>

                </ItemTemplate>
                <EditItemTemplate>

                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" ValidationGroup="Grid" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                        CommandName="ActionUpdate" Text="Update"  Font-Size="10px" ForeColor="#00CC00" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:LinkButton>

                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                        CommandName="Cancel" Text="Cancel" Font-Size="10px"></asp:LinkButton>

                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Validated BOQ Exc. VAT" SortExpression="Budget" ControlStyle-Width="126" HeaderStyle-Width="126" ItemStyle-HorizontalAlign="Right" footerstyle-HorizontalAlign="Right" FooterStyle-BackColor="WhiteSmoke">
                <ItemTemplate>
                    <asp:Label ID="LabelBudget" runat="server" Text='<%# Bind("Budget","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxBudget" runat="server" Text='<%# Eval("Budget") %>' 
                    Font-Size="10px" BackColor="#FF3300" ReadOnly="False"> </asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorBudget" runat="server" 
                            ControlToValidate="TextBoxBudget" CssClass="LabelGeneral"  ValidationGroup="Grid"
                            ErrorMessage="not valid number" 
                            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" Display="Dynamic"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="LabelBudget" runat="server" ></asp:Label>
                </FooterTemplate>

<ControlStyle Width="126px"></ControlStyle>

<FooterStyle HorizontalAlign="Right" BackColor="#666666" ForeColor="White"></FooterStyle>

<HeaderStyle Width="126px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Validated Chage Order Budget exc.VAT" SortExpression="VCO" ControlStyle-Width="126" HeaderStyle-Width="126" ItemStyle-HorizontalAlign="Right" footerstyle-HorizontalAlign="Right" FooterStyle-BackColor="WhiteSmoke" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="LabelVCO" runat="server" Text='<%# Bind("VCO","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxVCO" runat="server" Text='<%# Eval("VCO") %>' 
                    Font-Size="10px" BackColor="#99FF66" ReadOnly="False"   > </asp:TextBox>

                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="LabelVCO" runat="server" ></asp:Label>
                </FooterTemplate>

<ControlStyle Width="126px"></ControlStyle>

<FooterStyle HorizontalAlign="Right" BackColor="#666666" ForeColor="White"></FooterStyle>

<HeaderStyle Width="126px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="LabelBudgetId" runat="server" CssClass="budgetid hide" Text='<%# Eval("BudgetId")%>'></asp:Label>
                    <asp:CheckBox ID="cbxBudgetId" Checked="true" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Updated / planned costTotal Forecast Cost To Complete Project" SortExpression="PlannedToSpend" ControlStyle-Width="126" HeaderStyle-Width="126" ItemStyle-HorizontalAlign="Right" footerstyle-HorizontalAlign="Right" FooterStyle-BackColor="WhiteSmoke">
                <ItemTemplate>
                   <asp:Label ID="LabelPlannedToSpend" runat="server" Text='<%# Bind("PlannedToSpend","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxPlannedToSpend" runat="server" Text='<%# Eval("PlannedToSpend") %>'  BackColor="#99FF66" Font-Size="10px"> </asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorPlanned" runat="server" 
                            ControlToValidate="TextBoxPlannedToSpend" CssClass="LabelGeneral"  ValidationGroup="Grid"
                            ErrorMessage="not valid number" 
                            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" Display="Dynamic"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="LabelPlannedToSpend" runat="server" ></asp:Label>
                </FooterTemplate>
<ControlStyle Width="126px"></ControlStyle>

<FooterStyle HorizontalAlign="Right" BackColor="#666666" ForeColor="White"></FooterStyle>

<HeaderStyle Width="126px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Updated Change Order Budget Exc.VAT" SortExpression="PlannedToSpendCO" ControlStyle-Width="126" HeaderStyle-Width="126" ItemStyle-HorizontalAlign="Right" footerstyle-HorizontalAlign="Right" FooterStyle-BackColor="WhiteSmoke" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="LabelPlannedToSpendCO" runat="server" Text='<%# Bind("PlannedToSpendCO","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxPlannedToSpendCO" runat="server" Text='<%# Eval("PlannedToSpendCO") %>'  BackColor="#99FF66" ReadOnly="False"
                     Font-Size="10px"  > </asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorPlannedCO" runat="server" 
                            ControlToValidate="TextBoxPlannedToSpendCO" CssClass="LabelGeneral"  ValidationGroup="Grid"
                            ErrorMessage="not valid number" 
                            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" Display="Dynamic"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="LabelPlannedToSpendCO" runat="server" ></asp:Label>
                </FooterTemplate>
<ControlStyle Width="126px"></ControlStyle>

<FooterStyle HorizontalAlign="Right" BackColor="#666666" ForeColor="White"></FooterStyle>

<HeaderStyle Width="126px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Updated Planned Revenue Exc.VAT" SortExpression="UpdatedPlannedRevenue" ControlStyle-Width="126" HeaderStyle-Width="126" ItemStyle-HorizontalAlign="Right" footerstyle-HorizontalAlign="Right" FooterStyle-BackColor="WhiteSmoke">
                <ItemTemplate>
                    <asp:Label ID="LabelUpdatedPlannedRevenue" runat="server" Text='<%# Bind("UpdatedPlannedRevenue","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxUpdatedPlannedRevenue" runat="server" Text='<%# Eval("UpdatedPlannedRevenue") %>'  BackColor="#99FF66" Font-Size="10px"> </asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorRevenue" runat="server" 
                            ControlToValidate="TextBoxUpdatedPlannedRevenue" CssClass="LabelGeneral"  ValidationGroup="Grid"
                            ErrorMessage="not valid number" 
                            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" Display="Dynamic"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="LabelUpdatedPlannedRevenue" runat="server" ></asp:Label>
                </FooterTemplate>
<ControlStyle Width="126px"></ControlStyle>

<FooterStyle HorizontalAlign="Right" BackColor="#666666" ForeColor="White"></FooterStyle>

<HeaderStyle Width="126px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Total Progress Percent" SortExpression="CompletionPercent" HeaderStyle-Width="50" ItemStyle-HorizontalAlign="Right" footerstyle-HorizontalAlign="Right" FooterStyle-BackColor="WhiteSmoke" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="LabelCompletionPercent" runat="server" Text='<%# Bind("CompletionPercent") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxCompletionPercent" runat="server" Text='<%# Eval("CompletionPercent") %>'
                     BackColor="#99FF66" Font-Size="10px" Width="30px"> </asp:TextBox>
                            <asp:RangeValidator ID="RangeValidator1" runat="server"  Display="Dynamic" ValidationGroup = "Grid"
                                ErrorMessage="out of range!" ControlToValidate="TextBoxCompletionPercent" 
                                CssClass="LabelGeneral" MaximumValue="100" MinimumValue="0" Type="Double" >
                            </asp:RangeValidator>
                </EditItemTemplate>
                <FooterTemplate>
                </FooterTemplate>

                <FooterStyle HorizontalAlign="Right" BackColor="#666666" ForeColor="White"></FooterStyle>

                <HeaderStyle Width="50px"></HeaderStyle>

                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Recovery Exc.VAT" SortExpression="Recovery" ControlStyle-Width="126" HeaderStyle-Width="126" ItemStyle-HorizontalAlign="Right" footerstyle-HorizontalAlign="Right" FooterStyle-BackColor="WhiteSmoke">
                <ItemTemplate>
                    <asp:Label ID="LabelRecovery" runat="server" Text='<%# Bind("Recovery", "{0:###,###,###.00}")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxRecovery" runat="server" Text='<%# Eval("Recovery")%>'  BackColor="#99FF66" Font-Size="10px"> </asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionRecovery" runat="server" 
                            ControlToValidate="TextBoxRecovery" CssClass="LabelGeneral"  ValidationGroup="Grid"
                            ErrorMessage="not valid number" 
                            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" Display="Dynamic"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="LabelRecovery" runat="server" ></asp:Label>
                </FooterTemplate>
<ControlStyle Width="126px"></ControlStyle>

<FooterStyle HorizontalAlign="Right" BackColor="#666666" ForeColor="White"></FooterStyle>

<HeaderStyle Width="126px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>

          </Columns>
        <pagerstyle  horizontalalign="Center" CssClass="pager" />
        <RowStyle  CssClass="GridItemNakladnaya" Height="30px" />
        <HeaderStyle  CssClass="GridHeader" />
    </asp:GridView>

     <asp:SqlDataSource ID="SqlDataSourceBudgetRuble" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand=" SELECT * FROM (
SELECT     dbo.View_Qry_CostCodeWithPoPerProjectRuble.ProjectID, RTRIM(dbo.View_Qry_CostCodeWithPoPerProjectRuble.CostCode) AS CostCode, 
                      RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCodeDesc, CASE WHEN Budget IS NULL THEN 0 ELSE Budget END AS Budget, 
                      CASE WHEN PlannedToSpend IS NULL THEN 0 ELSE PlannedToSpend END AS PlannedToSpend, 
                      CASE WHEN View_Qry_CostCodeWithPoPerProjectRuble.CostCode IS NULL THEN 1 ELSE 0 END AS DeletePossible, CASE WHEN CompletionPercent IS NULL 
                      THEN 0 ELSE CompletionPercent END AS CompletionPercent, dbo.Table7_CostDivision.CostVidisionID, dbo.Table_Budget.UpdatedPlannedRevenue,
                      CASE WHEN dbo.Table_Budget.VCO IS NULL THEN 0 ELSE dbo.Table_Budget.VCO END AS VCO, CASE WHEN dbo.Table_Budget.PlannedToSpendCO IS NULL THEN 0 ELSE dbo.Table_Budget.PlannedToSpendCO END AS PlannedToSpendCO, ISNULL(Recovery,0) AS Recovery, dbo.Table_Budget.BudgetId
FROM         dbo.View_Qry_CostCodeWithPoPerProjectRuble INNER JOIN
                      dbo.Table7_CostCode ON dbo.View_Qry_CostCodeWithPoPerProjectRuble.CostCode = dbo.Table7_CostCode.CostCode LEFT OUTER JOIN
                      dbo.Table7_CostDivision ON dbo.Table7_CostCode.CostVidisionID = dbo.Table7_CostDivision.CostVidisionID LEFT OUTER JOIN
                      dbo.Table_Budget ON dbo.View_Qry_CostCodeWithPoPerProjectRuble.Currency = dbo.Table_Budget.Currency AND 
                      dbo.View_Qry_CostCodeWithPoPerProjectRuble.ProjectID = dbo.Table_Budget.ProjectID AND 
                      dbo.View_Qry_CostCodeWithPoPerProjectRuble.CostCode = dbo.Table_Budget.CostCode LEFT OUTER JOIN
                      dbo.Table_ProgressPercent ON dbo.View_Qry_CostCodeWithPoPerProjectRuble.ProjectID = dbo.Table_ProgressPercent.ProjectID AND 
                      dbo.View_Qry_CostCodeWithPoPerProjectRuble.CostCode = dbo.Table_ProgressPercent.CostCode

UNION ALL

SELECT     dbo.Table_Budget.ProjectID, RTRIM(dbo.Table_Budget.CostCode) AS CostCode, RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCodeDesc, 
                      CASE WHEN Budget IS NULL THEN 0 ELSE Budget END AS Budget, CASE WHEN PlannedToSpend IS NULL THEN 0 ELSE PlannedToSpend END AS PlannedToSpend, 
                      CASE WHEN View_Qry_CostCodeWithPoPerProjectRuble.CostCode IS NULL THEN 1 ELSE 0 END AS DeletePossible, CASE WHEN CompletionPercent IS NULL 
                      THEN 0 ELSE CompletionPercent END AS CompletionPercent, dbo.Table7_CostDivision.CostVidisionID, dbo.Table_Budget.UpdatedPlannedRevenue,
                      CASE WHEN dbo.Table_Budget.VCO IS NULL THEN 0 ELSE dbo.Table_Budget.VCO END AS VCO, CASE WHEN dbo.Table_Budget.PlannedToSpendCO IS NULL THEN 0 ELSE dbo.Table_Budget.PlannedToSpendCO END AS PlannedToSpendCO, ISNULL(Recovery,0) AS Recovery, dbo.Table_Budget.BudgetId
FROM         dbo.Table7_CostCode INNER JOIN
                      dbo.Table_Budget ON dbo.Table7_CostCode.CostCode = dbo.Table_Budget.CostCode LEFT OUTER JOIN
                      dbo.Table7_CostDivision ON dbo.Table7_CostCode.CostVidisionID = dbo.Table7_CostDivision.CostVidisionID LEFT OUTER JOIN
                      dbo.Table_ProgressPercent ON dbo.Table_Budget.ProjectID = dbo.Table_ProgressPercent.ProjectID AND 
                      dbo.Table_Budget.CostCode = dbo.Table_ProgressPercent.CostCode LEFT OUTER JOIN
                      dbo.View_Qry_CostCodeWithPoPerProjectRuble ON dbo.Table_Budget.ProjectID = dbo.View_Qry_CostCodeWithPoPerProjectRuble.ProjectID AND 
                      dbo.Table_Budget.CostCode = dbo.View_Qry_CostCodeWithPoPerProjectRuble.CostCode
WHERE     (dbo.Table_Budget.Currency = N'Rub') AND (dbo.View_Qry_CostCodeWithPoPerProjectRuble.ProjectID IS NULL) AND 
                      (dbo.View_Qry_CostCodeWithPoPerProjectRuble.CostCode IS NULL)
                      ) AS DataSource1
                      WHERE ProjectID = @ProjectID
                      ORDER BY CostVidisionID, CostCode " >
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" Name="ProjectID" 
                PropertyName="SelectedValue" Type="Int16" />
        </SelectParameters>
       </asp:SqlDataSource>
   

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" Runat="Server">

    <script type="text/javascript">
        $(function () {

            $("input[name*='cbxBudgetId']").on('change', function (e) {

                var _clickedItem = $(this)
                var BudgetID = _clickedItem.prev().text();

                if (_clickedItem.is(':checked'))
                        $.ajax({
                            type: "POST",
                            url: '/PageMethods/Table_Budget_PlannedToSpendConstraints_Insert?BudgetID=' + BudgetID,
                            success: function (response) {

                            }
                        });
                else
                    $.ajax({
                        type: "POST",
                        url: '/PageMethods/Table_Budget_PlannedToSpendConstraints_Delete?BudgetID=' + BudgetID,
                        success: function (response) {

                        }
                    });
            })
        })
    </script>



</asp:Content>


<%@ Page Title="" Language="VB" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Table_Budget.aspx.vb" Inherits="Pages_PTS_Table_Budget" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="Head" runat="server">

    <link rel="stylesheet" href="/assets/css/styles_PTS_MERCURY.css" />

</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:FormView runat="server" ID="FormviewTable_Budget" DataKeyNames="BudgetID" DefaultMode="Insert"
        ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.db.Table_Budget"
        OnCallingDataMethods="FormviewTable_Budget_CallingDataMethods"
        SelectMethod="FormviewTable_Budget_GetItem"
        InsertMethod="FormviewTable_Budget_InsertItem">
        <InsertItemTemplate>
            <fieldset>
                <legend>
                    <div style="display: inline-block">Table_Budget</div>
                    <div style="display: inline-block; float: right;"><a href="_ListOfTables.aspx">List Of Tables</a></div>
                </legend>
                <ol>
                    <div>

                        <label>Project Name</label>
                        <asp:DropDownList ID="DropDownListProjectName" runat="server" ValidationGroup="Table_BudgetInsert"
                            SelectMethod="GetDropDownListProjectName"
                            AppendDataBoundItems="true" SelectedValue="<%# BindItem.ProjectID%>" ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.db.Table1_Project"
                            DataTextField="ProjectName" DataValueField="ProjectID">
                            <asp:ListItem Value="">Select ProjectName:</asp:ListItem>
                        </asp:DropDownList>

                        <label>Code Description</label>
                        <asp:DropDownList ID="DropDownListCodeDescription" runat="server" ValidationGroup="Table_BudgetInsert"
                            SelectMethod="GetDropDownListCodeDescription"
                            AppendDataBoundItems="true" SelectedValue="<%# BindItem.CostCode%>" ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table7_CostCode+Table7_CostCode_For_DDL"
                            DataTextField="CodeDescription" DataValueField="CostCode">
                            <asp:ListItem Value="">Select CodeDescription:</asp:ListItem>
                        </asp:DropDownList>

                        <label>Budget</label>
                        <asp:DynamicControl runat="server" DataField="Budget" Mode="Insert" ValidationGroup="Table_BudgetInsert" />

                        <label>Planned To Spend</label>
                        <asp:DynamicControl runat="server" DataField="PlannedToSpend" Mode="Insert" ValidationGroup="Table_BudgetInsert" />

                        <label>Planned To Spend C O</label>
                        <asp:DynamicControl runat="server" DataField="PlannedToSpendCO" Mode="Insert" ValidationGroup="Table_BudgetInsert" />

                        <label>Currency</label>
                        <asp:DropDownList ID="DropDownListCurrency" runat="server" ValidationGroup="Table_BudgetInsert"
                            SelectMethod="GetDropDownListDropDownListCurrency"
                            AppendDataBoundItems="true" SelectedValue="<%# BindItem.Currency%>" ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.helper.Currency+Currency_DDL"
                            DataTextField="Currency_Description" DataValueField="Currency">
                            <asp:ListItem Value="">Select Currency:</asp:ListItem>
                        </asp:DropDownList>

                        <label>Updated Planned Revenue</label>
                        <asp:DynamicControl runat="server" DataField="UpdatedPlannedRevenue" Mode="Insert" ValidationGroup="Table_BudgetInsert" />

                        <label>Original B O Q</label>
                        <asp:DynamicControl runat="server" DataField="OriginalBOQ" Mode="Insert" ValidationGroup="Table_BudgetInsert" />

                        <label>V C O</label>
                        <asp:DynamicControl runat="server" DataField="VCO" Mode="Insert" ValidationGroup="Table_BudgetInsert" />

                        <label>Cost To Go B O Q</label>
                        <asp:DynamicControl runat="server" DataField="CostToGoBOQ" Mode="Insert" ValidationGroup="Table_BudgetInsert" />

                        <label>Cost To Go V C O</label>
                        <asp:DynamicControl runat="server" DataField="CostToGoVCO" Mode="Insert" ValidationGroup="Table_BudgetInsert" />


                        <label></label>
                        <asp:Button ID="btnSave" runat="server" Text="Insert" CommandName="Insert" ValidationGroup="Table_BudgetInsert" CssClass="btn btn-mini btn-info" />
                    </div>
                </ol>
            </fieldset>
        </InsertItemTemplate>
    </asp:FormView>


    <asp:ValidationSummary runat="server" ShowModelStateErrors="true" ValidationGroup="Table_BudgetInsert" ForeColor="Red" />

    <asp:HiddenField ID="ProjectID" runat="server" Value="0" />

    <asp:DropDownList ID="DropDownListProjectName" runat="server"
        SelectMethod="GetDropDownListProjectName"
        AppendDataBoundItems="true" ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.db.Table1_Project"
        DataTextField="ProjectName" DataValueField="ProjectID" AutoPostBack="true">
        <asp:ListItem Value="0">Select ProjectName To Filter:</asp:ListItem>
    </asp:DropDownList>

    <asp:GridView runat="server" ID="GridviewTable_Budget" CssClass="table table-nonfluid table-hover" GridLines="None" DataKeyNames="BudgetID" 
        ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table_Budget+Table_Budget_ToGrid"
        OnCallingDataMethods="GridviewTable_Budget_CallingDataMethods"
        SelectMethod="GridviewTable_Budget_GetData"
        UpdateMethod="GridviewTable_Budget_UpdateItem"
        DeleteMethod="GridviewTable_Budget_DeleteItem"
        AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField ShowHeader="False" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
                <EditItemTemplate>
                    <div class="btn-group-vertical" role="group">
                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True" CssClass="btn btn-minier btn-primary"
                            CommandName="Update" Text="Update" ValidationGroup="Table_BudgetUpdate"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CssClass="btn btn-minier btn-success "
                            CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </div>
                </EditItemTemplate>
                <ItemTemplate>
                    <div class="btn-group-vertical" role="group">
                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False" CssClass="btn btn-minier btn-success "
                            CommandName="Edit" Text="Edit"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonDelete" runat="server" CssClass="btn btn-minier btn-danger" CausesValidation="false"
                            OnClientClick="return confirm('Are you sure you want to delete this record?');"
                            CommandName="Delete" Text="Delete"></asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Project Name" HeaderStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label ID="labelProjectName" runat="server" Text="<%# PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table1_Project.GetRowByPrimaryKey(Item.ProjectID).ProjectName%>"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownListProjectName" runat="server"
                        SelectMethod="GetDropDownListProjectName"
                        AppendDataBoundItems="true" SelectedValue="<%# BindItem.ProjectID%>" ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.db.Table1_Project"
                        DataTextField="ProjectName" DataValueField="ProjectID">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Code Description" HeaderStyle-Width="100px">
                <ItemTemplate>

                    <%# Item.CostCode%>-<%# PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table7_CostCode.GetRowByPrimaryKey(Item.CostCode).CodeDescription%>

                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownListCodeDescription" runat="server" ValidationGroup="Table_BudgetUpdate"
                        SelectMethod="GetDropDownListCodeDescription"
                        AppendDataBoundItems="true" SelectedValue="<%# BindItem.CostCode%>" ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table7_CostCode+Table7_CostCode_For_DDL"
                        DataTextField="CodeDescription" DataValueField="CostCode">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:DynamicField DataField="Budget" ValidationGroup="Table_BudgetUpdate" HeaderText="Budget" />

            <asp:DynamicField DataField="PlannedToSpend" ValidationGroup="Table_BudgetUpdate" HeaderText="Planned To Spend" />

            <asp:DynamicField DataField="PlannedToSpendCO" ValidationGroup="Table_BudgetUpdate" HeaderText="Planned To Spend C O" />

            <asp:TemplateField HeaderText="Currency">
                <ItemTemplate>
                    <asp:Label ID="labelCurrency" runat="server" Text="<%# BindItem.Currency%>"></asp:Label>

                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownListCurrency" runat="server" ValidationGroup="Table_BudgetUpdate" Style="width: 200px"
                        SelectMethod="GetDropDownListDropDownListCurrency"
                        AppendDataBoundItems="true" SelectedValue="<%# BindItem.Currency%>" ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.helper.Currency+Currency_DDL"
                        DataTextField="Currency_Description" DataValueField="Currency">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:DynamicField DataField="UpdatedPlannedRevenue" ValidationGroup="Table_BudgetUpdate" HeaderText="Updated Planned Revenue" />

            <asp:DynamicField DataField="OriginalBOQ" ValidationGroup="Table_BudgetUpdate" HeaderText="Original B O Q" />

            <asp:DynamicField DataField="VCO" ValidationGroup="Table_BudgetUpdate" HeaderText="V C O" />

            <asp:DynamicField DataField="CostToGoBOQ" ValidationGroup="Table_BudgetUpdate" HeaderText="Cost To Go B O Q" />

            <asp:DynamicField DataField="CostToGoVCO" ValidationGroup="Table_BudgetUpdate" HeaderText="Cost To Go V C O" />


        </Columns>
        <PagerSettings Position="TopAndBottom" />
        <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
        <HeaderStyle CssClass="headergridnew" />
    </asp:GridView>


    <asp:ValidationSummary runat="server" ShowModelStateErrors="true" ValidationGroup="Table_BudgetUpdate" ForeColor="Red" />

</asp:Content>


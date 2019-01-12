<%@ Page Title="" Language="VB" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Table7_CostDivision2.aspx.vb" Inherits="Pages_PTS_Table7_CostDivision2" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="Head" runat="server">

<link rel="stylesheet" href="/assets/css/styles_PTS_MERCURY.css" />

</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContent" Runat="Server">

<asp:FormView runat="server" ID="FormviewTable7_CostDivision2" DataKeyNames="CostDivision2ID" DefaultMode="Insert"
ItemType = "PTS_App_Code_VB_Project.PTS_MERCURY.db.Table7_CostDivision2"
OnCallingDataMethods = "FormviewTable7_CostDivision2_CallingDataMethods"
SelectMethod = "FormviewTable7_CostDivision2_GetItem"
InsertMethod="FormviewTable7_CostDivision2_InsertItem">
<InsertItemTemplate>
<fieldset>
<legend><div style="display:inline-block">Table7_CostDivision2</div><div style="display:inline-block; float:right;"><a href="_ListOfTables.aspx">List Of Tables</a></div></legend>
<ol>
<div>

<label>Cost Division2 I D</label>
<asp:DynamicControl runat="server" DataField="CostDivision2ID" Mode="Insert" ValidationGroup="Table7_CostDivision2Insert" />

<label>Cost Division2 Description</label>
<asp:TextBox ID="TextBoxCostDivision2Description" runat="server" Text='<%# Bind("CostDivision2Description")%>' TextMode="MultiLine" Rows="5" ValidationGroup="Table7_CostDivision2Insert" ></asp:TextBox>


<label></label>
<asp:Button ID="btnSave" runat="server" Text="Insert" CommandName="Insert" ValidationGroup="Table7_CostDivision2Insert" CssClass="btn btn-mini btn-info" />
</div>
</ol>
</fieldset>
</InsertItemTemplate>
</asp:FormView>


<asp:ValidationSummary runat="server" ShowModelStateErrors="true" ValidationGroup="Table7_CostDivision2Insert" ForeColor="Red" />

<asp:GridView runat="server" ID="GridviewTable7_CostDivision2" CssClass="table table-nonfluid table-hover" GridLines="None" DataKeyNames="CostDivision2ID" AllowPaging="true"
ItemType = "PTS_App_Code_VB_Project.PTS_MERCURY.db.Table7_CostDivision2"
OnCallingDataMethods = "GridviewTable7_CostDivision2_CallingDataMethods"
SelectMethod = "GridviewTable7_CostDivision2_GetData"
UpdateMethod="GridviewTable7_CostDivision2_UpdateItem"
DeleteMethod="GridviewTable7_CostDivision2_DeleteItem"
AutoGenerateColumns="false">
<Columns>
<asp:TemplateField ShowHeader="False" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
<EditItemTemplate>
<div class="btn-group-vertical" role="group">
<asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True" CssClass="btn btn-minier btn-primary"
CommandName="Update" Text="Update" ValidationGroup="Table7_CostDivision2Update"></asp:LinkButton>
<asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CssClass="btn btn-minier btn-success "
CommandName="Cancel" Text="Cancel"></asp:LinkButton>
</div>
</EditItemTemplate>
<ItemTemplate>
<div class="btn-group-vertical" role="group">
<asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False" CssClass="btn btn-minier btn-success "
CommandName="Edit" Text="Edit"></asp:LinkButton>
<asp:LinkButton ID="LinkButtonDelete" runat="server" CssClass="btn btn-minier btn-danger" CausesValidation="false"
OnClientClick = "return confirm('Are you sure you want to delete this record?');"
CommandName="Delete" Text="Delete"></asp:LinkButton>
</div>
</ItemTemplate>
</asp:TemplateField>

<asp:DynamicField DataField="CostDivision2ID" ValidationGroup="Table7_CostDivision2Update" HeaderText="Cost Division2 I D" />

            <asp:TemplateField >
                <ItemTemplate>
                       <span class="span_small"><%# Item.CostDivision2Description%></span>
                </ItemTemplate>
                <EditItemTemplate>
                       <asp:TextBox ID="TextBoxCostDivision2Description" runat="server" Text='<%# Bind("CostDivision2Description")%>' TextMode="MultiLine" Rows="5" ValidationGroup="Table7_CostDivision2Update" ></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>


</Columns>
                <PagerSettings Position="TopAndBottom" />
                <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
                <HeaderStyle CssClass="headergridnew" />
</asp:GridView>


<asp:ValidationSummary runat="server" ShowModelStateErrors="true" ValidationGroup="Table7_CostDivision2Update" ForeColor="Red" />

</asp:Content>


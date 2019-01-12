<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="FeedTamasValidation.aspx.vb" Inherits="FeedTamasValidation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<asp:GridView ID="GridViewValidation" runat="server" AutoGenerateColumns="False" 
DataSourceID="SqlDataSourceValidation">
        <Columns>

            <asp:TemplateField HeaderText="CostCode" ControlStyle-Width="150" HeaderStyle-Width="150">
                <ItemTemplate>
                    <asp:Label ID="LabelCostCode" runat="server" Text='<%# Bind("CostCode") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="CostCodeDesc" ControlStyle-Width="250" HeaderStyle-Width="250">
                <ItemTemplate>
                    <asp:Label ID="LabelCostCodeDesc" runat="server" Text='<%# Bind("CostCodeDesc") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Updated / planned cost" ControlStyle-Width="200" HeaderStyle-Width="200" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="LabelUpdatedPlannedCost" runat="server" Text='<%# Bind("PlannedToSpend") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Committed Cost" ControlStyle-Width="200" HeaderStyle-Width="200" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="LabelCommittedCost" runat="server" Text='<%# Bind("PoTotalEuroExcVAT") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
                <RowStyle  CssClass="GridItemNakladnaya" />
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSourceValidation" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand="SelectTamasValidation" SelectCommandType="StoredProcedure">
    </asp:SqlDataSource>

</asp:Content>


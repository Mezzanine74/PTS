<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="BalanceForeignCurrency.aspx.vb" Inherits="BalanceForeignCurrency" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

  <asp:GridView ID="GridViewBalanceBreakdownForeignCurrency" runat="server" 
    AutoGenerateColumns="False"  CssClass="Grid"
    DataSourceID="ObjectDataSourceFixRateSubReport" ShowFooter="True">
    <Columns>
      <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" 
        HeaderStyle-Width="150px" ItemStyle-Width="150px"
        ReadOnly="True" SortExpression="SupplierName">
<HeaderStyle Width="150px"></HeaderStyle>

<ItemStyle Width="150px"></ItemStyle>
        </asp:BoundField>

      <asp:BoundField DataField="PO_No" HeaderText="PO_No" ReadOnly="True" 
      HeaderStyle-Width="100px" ItemStyle-Width="100px"
        SortExpression="PO_No">
<HeaderStyle Width="100px"></HeaderStyle>

<ItemStyle Width="100px"></ItemStyle>
      </asp:BoundField>

        <asp:TemplateField HeaderText="Description" FooterStyle-BackColor="#CBCFEB">
            <ItemTemplate>
                <asp:Literal ID="LiteralDescription" runat="server" Text='<%# Bind("Description")%>'></asp:Literal>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Literal ID="LiteralDescriptionFooter" runat="server" Text="T O T A L :"></asp:Literal>
            </FooterTemplate>
            <HeaderStyle Width="200px" />
            <ItemStyle Width="200px" />
            <FooterStyle HorizontalAlign="Right" Width="200px" />
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Paid With VAT" FooterStyle-BackColor="#CBCFEB">
            <ItemTemplate>
                <asp:Literal ID="LiteralPaid" runat="server" Text='<%# Bind("PaidWithVAT", "{0:N2}") %>'></asp:Literal>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Literal ID="LiteralPaidFooter" runat="server" Text="Label"></asp:Literal>
            </FooterTemplate>
            <HeaderStyle Width="100px" />
            <ItemStyle HorizontalAlign="Right" Width="100px" />
            <FooterStyle HorizontalAlign="Right" Width="100px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Balance With VAT" FooterStyle-BackColor="#CBCFEB">
            <ItemTemplate>
                <asp:Literal ID="LiteralBalance" runat="server" Text='<%# Bind("BalanceWithVAT", "{0:N2}") %>'></asp:Literal>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Literal ID="LiteralBalanceFooter" runat="server" Text="Label"></asp:Literal>
            </FooterTemplate>
            <HeaderStyle Width="100px" />
            <ItemStyle HorizontalAlign="Right" Width="100px" />
            <FooterStyle HorizontalAlign="Right" Width="100px" />
        </asp:TemplateField>

      <asp:BoundField DataField="Po_Currency" 
        ReadOnly="True" SortExpression="Po_Currency"></asp:BoundField>
    </Columns>
        <RowStyle  CssClass="GridItemNakladnaya" Height="20px" />
        <HeaderStyle  CssClass="GridHeader" />
  </asp:GridView>

    <asp:ObjectDataSource ID="ObjectDataSourceFixRateSubReport" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        TypeName="PTS_App_Code_VB_Project.MercuryTableAdapters.SP_FixRateSubReportTableAdapter">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="ProjectID" QueryStringField="ProjectID" Type="Int32" />
            <asp:QueryStringParameter DefaultValue="-" Name="Po_Currency" QueryStringField="Po_Currency" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>


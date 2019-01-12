<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="VirtualPOs.aspx.vb" Inherits="VirtualPOs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:ImageButton ID="ImageButtonExcel" runat="server" Width="20px" ToolTip="Export To Excel" OnClick="ImageButtonExcel_Click"
    ImageUrl="~/Images/Excel.jpg" />

    <br /><br />
    
    <asp:GridView ID="GridViewVirtualPO" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSourceVirtualPO" 
        ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="ProjectName" HeaderText="ProjectName" ReadOnly="True" SortExpression="ProjectName" />
            <asp:BoundField DataField="PO_No" HeaderText="PO_No" SortExpression="PO_No" />
            <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description" HeaderStyle-Width="250px" ItemStyle-Width="250px" />
            <asp:BoundField DataField="INN" HeaderText="INN" SortExpression="INN" />
            <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" ReadOnly="True" SortExpression="SupplierName" />
            <asp:BoundField DataField="PO_Value_With_Vat" HeaderText="PO Value With Vat" SortExpression="PO_Value_With_Vat" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="150px" />
            <asp:BoundField DataField="PO_Currency" HeaderText="PO Currency" SortExpression="PO_Currency" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" SortExpression="PaymentDate" DataFormatString="{0:dd/MM/yyyy}"/>
            <asp:BoundField DataField="Payment_Value_Ruble_With_Vat" HeaderText="Payment Value Ruble With Vat" SortExpression="Payment_Value_Ruble_With_Vat" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="150px" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceVirtualPO" runat="server" ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand="EXEC [dbo].[SP_GetVirtualPOs]"></asp:SqlDataSource>



</asp:Content>


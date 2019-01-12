<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="SelectPOContractsAddendumsForStas.aspx.vb" Inherits="SelectPOContractsAddendumsForStas" enableEventValidation ="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:DropDownList ID="DropDownListPrj" runat="server" AutoPostBack="true"
        DataSourceID="SqlDataSourcePrj" DataTextField="ProjectName"
        DataValueField="ProjectID">
    </asp:DropDownList>
    
    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server"
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
        SelectCommand="
        SELECT * FROM (
        SELECT 111111111 AS ProjectID, N'_Select' AS ProjectName

        UNION ALL
        
        SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID,
         (RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5),dbo.Table1_Project.ProjectID))) as ProjectName
          FROM         dbo.Table1_Project 
          WHERE (dbo.Table1_Project.NewGeneration = 1) 
        ) AS Source
        ORDER BY ProjectName"></asp:SqlDataSource>

    <asp:Button ID="ButtonExcel" runat="server" Text="To Excel" CssClass="btn btn-purple" OnClick="ButtonExcel_Click" />


    <asp:GridView ID="GridViewReport" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceReport">
        <Columns>
            <asp:BoundField DataField="_item" HeaderText="_item" ReadOnly="True" SortExpression="_item" />
            <asp:BoundField DataField="PO_No" HeaderText="PO_No" ReadOnly="True" SortExpression="PO_No" />
            <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" ReadOnly="True" SortExpression="SupplierName" />
            <asp:BoundField DataField="ContractNo" HeaderText="ContractNo" ReadOnly="True" SortExpression="ContractNo" />
            <asp:BoundField DataField="ContractDate" HeaderText="ContractDate" ReadOnly="True" SortExpression="ContractDate" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="PO_Currency" HeaderText="PO_Currency" ReadOnly="True" SortExpression="PO_Currency" />
            <asp:BoundField DataField="_Type" HeaderText="_Type" ReadOnly="True" SortExpression="_Type" />
            <asp:BoundField DataField="PoTotalExcVAT" HeaderText="PoTotalExcVAT" ReadOnly="True" SortExpression="PoTotalExcVAT" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField DataField="PaidTotalExcVAT" HeaderText="PaidTotalExcVAT" ReadOnly="True" SortExpression="PaidTotalExcVAT" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description" />
            <asp:BoundField DataField="CostCode" HeaderText="CostCode" ReadOnly="True" SortExpression="CostCode" />
            <asp:BoundField DataField="CodeDescription" HeaderText="CodeDescription" ReadOnly="True" SortExpression="CodeDescription" />
            <asp:BoundField DataField="SectionName" HeaderText="SectionName" ReadOnly="True" SortExpression="SectionName" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceReport" runat="server" ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand="EXEC [dbo].[Select_Contracts_POs_ForStas] @ProjectID">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="111111111" Name="ProjectID" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>


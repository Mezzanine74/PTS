<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="SearchContractsInspectErrors.aspx.vb" Inherits="SearchContractsInspectErrors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperlinkInspection" runat="server" Target="_blank"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ContractBreakdownsConnectionString %>" 
        SelectCommand=" SELECT '~/ContractBrkdwn.aspx?ContractID='+rtrim(convert(nVarChar(20),[ContractID])) AS Link  FROM [dbo].[Table_Contract_Brkdwn]  where len(rtrim(Contract_Brkdwn)) > 20 "></asp:SqlDataSource>

</asp:Content>


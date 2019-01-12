<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="FileSizeReport.aspx.vb" Inherits="FileSizeReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
    DataKeyNames="PayReqNo" DataSourceID="SqlDataSource1" 
    EnableModelValidation="True">

    <Columns>

      <asp:BoundField DataField="PayReqNo" HeaderText="PayReqNo" 
        InsertVisible="False" ReadOnly="True" SortExpression="PayReqNo" />
      <asp:BoundField DataField="LinkToInvoice" HeaderText="LinkToInvoice" 
        ReadOnly="True" SortExpression="LinkToInvoice" />
      <asp:TemplateField>
       <ItemTemplate>

       </ItemTemplate>
      </asp:TemplateField>

    </Columns>
  </asp:GridView>
  <asp:Button ID="Button1" runat="server" Text="Button" />
  <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand="SELECT TOP (10) PayReqNo, RTRIM(LinkToInvoice) AS LinkToInvoice FROM Table4_PaymentRequest ORDER BY CreatedBy ASC">
  </asp:SqlDataSource>

</asp:Content>


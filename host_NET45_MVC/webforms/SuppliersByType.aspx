<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="SuppliersByType.aspx.vb" Inherits="SuppliersByType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

  <asp:DataList ID="DataListSupplierByType" runat="server" 
    DataKeyField="SupplierTypeId" DataSourceID="SqlDataSourceSupplierByType" 
    Font-Size="11px">
    <HeaderStyle BackColor="#333333" ForeColor="White" />
    <HeaderTemplate>
    <asp:Label ID="LabelSupplierType" runat="server" ></asp:Label>
    </HeaderTemplate>
    <ItemStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
    <ItemTemplate>
      <asp:Label ID="LabelSupplierName" runat="server" Text='<%# Bind("SupplierName") %>'  ></asp:Label>
    </ItemTemplate>
  </asp:DataList>
  <asp:SqlDataSource ID="SqlDataSourceSupplierByType" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand="SELECT     TOP (100) PERCENT dbo.Table_SupplierType.SupplierTypeId,RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName
FROM         dbo.Table6_Supplier INNER JOIN
                      dbo.Table_SupplierType_Junction ON dbo.Table6_Supplier.SupplierID = dbo.Table_SupplierType_Junction.SupplierID INNER JOIN
                      dbo.Table_SupplierType ON dbo.Table_SupplierType_Junction.SupplierTypeId = dbo.Table_SupplierType.SupplierTypeId
WHERE     (dbo.Table_SupplierType.SupplierTypeId = @SupplierTypeId)
ORDER BY SupplierName">
    <SelectParameters>
      <asp:QueryStringParameter DefaultValue="0" Name="SupplierTypeId" 
        QueryStringField="SupplierTypeId" />
    </SelectParameters>
  </asp:SqlDataSource>

</asp:Content>


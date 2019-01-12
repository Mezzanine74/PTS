<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="FeedTamasFile.aspx.vb" Inherits="FeedTamasFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <asp:Button ID="Button1" runat="server" Text="Feed Tamas File" />
  <asp:GridView ID="GridViewFeedTamasFile" runat="server" AutoGenerateColumns="False" 
    DataSourceID="SqlDataSourceFeedTamasFile" EnableModelValidation="True" CssClass="Grid">
    <Columns>
      <asp:BoundField DataField="CostCode" HeaderText="CostCode" ReadOnly="True" 
        SortExpression="CostCode" HeaderStyle-Width="100px" />
      <asp:BoundField DataField="CodeDescription" HeaderText="CodeDescription" 
        ReadOnly="True" SortExpression="CodeDescription" />
      <asp:BoundField DataField="PoTotalEuroExcVAT" HeaderText="PoTotalEuroExcVAT" 
        ReadOnly="True" SortExpression="PoTotalEuroExcVAT" ItemStyle-HorizontalAlign="Right" />
      <asp:BoundField DataField="EuroPaidExcVAT" HeaderText="EuroPaidExcVAT" 
        ReadOnly="True" SortExpression="EuroPaidExcVAT" ItemStyle-HorizontalAlign="Right" />
    </Columns>
                <HeaderStyle BackColor="#FF3300" ForeColor="White" Height="20px" />
                <RowStyle  CssClass="GridItemNakladnaya" />
  </asp:GridView>
  <asp:SqlDataSource ID="SqlDataSourceFeedTamasFile" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand="SELECT     TOP (100) PERCENT RTRIM(dbo.Table7_CostCode.CostCode) AS CostCode, RTRIM(dbo.Table7_CostCode.CodeDescription) AS CodeDescription, 
                      SUM(case when dbo.View_QryW3.PoTotalEuroExcVAT IS null then 0 else dbo.View_QryW3.PoTotalEuroExcVAT end) AS PoTotalEuroExcVAT, SUM(case when dbo.View_QryW3.EuroPaidExcVAT IS null then 0 else dbo.View_QryW3.EuroPaidExcVAT end) AS EuroPaidExcVAT
FROM         dbo.View_QryW3 INNER JOIN
                      dbo.Table2_PONo ON dbo.View_QryW3.PO_No = dbo.Table2_PONo.PO_No INNER JOIN
                      dbo.Table7_CostCode ON dbo.Table2_PONo.CostCode = dbo.Table7_CostCode.CostCode
WHERE     (dbo.Table2_PONo.Project_ID = 108)
GROUP BY RTRIM(dbo.Table7_CostCode.CodeDescription), RTRIM(dbo.Table7_CostCode.CostCode)
ORDER BY CostCode"></asp:SqlDataSource>
</asp:Content>


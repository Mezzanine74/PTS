<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="FeedOlgaFile.aspx.vb" Inherits="FeedOlgaFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <asp:Button ID="Button1" runat="server" Text="Feed Olga File" />
  <asp:GridView ID="GridViewFeedOlgaFile" runat="server" AutoGenerateColumns="False" 
    DataSourceID="SqlDataSourceFeedOlgaFile" EnableModelValidation="True" CssClass="Grid">
    <Columns>
      <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" ReadOnly="True" 
        SortExpression="ProjectID" HeaderStyle-Width="100px" />
      <asp:BoundField DataField="ProjectName" HeaderText="ProjectName" ReadOnly="True" 
        SortExpression="ProjectName" HeaderStyle-Width="200px" />
      <asp:BoundField DataField="Prj_Currency" HeaderText="Prj_Currency" 
        ReadOnly="True" SortExpression="Prj_Currency" HeaderStyle-Width="100px" />
      <asp:BoundField DataField="Total_Paid_Inc_VAT" HeaderText="Total_Paid_Inc_VAT" 
        ReadOnly="True" SortExpression="Total_Paid_Inc_VAT" ItemStyle-HorizontalAlign="Right" />
      <asp:BoundField DataField="FxRate" HeaderText="FxRate" 
        ReadOnly="True" SortExpression="FxRate" ItemStyle-HorizontalAlign="Right" />
    </Columns>
                <HeaderStyle BackColor="#FF3300" ForeColor="White" Height="20px" />
                <RowStyle  CssClass="GridItemNakladnaya" />
  </asp:GridView>
  <asp:SqlDataSource ID="SqlDataSourceFeedOlgaFile" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand=" SELECT ProjectID, ProjectName, Prj_Currency, Total_Paid_Inc_VAT, FxRate FROM (

SELECT     dbo.Table1_Project.ProjectID, RTRIM(Table1_Project_1.ProjectName) AS ProjectName, CONVERT(nvarChar(5), dbo.Table1_Project.ProjectID) 
                      + N'-' + N'EUR' AS Prj_Currency, SUM(CASE WHEN TotalEuroPaidWithVAT IS NULL THEN 0 ELSE TotalEuroPaidWithVAT END) AS Total_Paid_Inc_VAT, 
                      fxRate.FxRate
FROM         dbo.Table1_Project INNER JOIN
                      dbo.Table1_Project AS Table1_Project_1 ON dbo.Table1_Project.ProjectID = Table1_Project_1.ProjectID LEFT OUTER JOIN
                          (SELECT     ProjectID, CONVERT(numeric(5, 2), CASE WHEN (SUM(TotalPoTotalRubleWithVAT) / SUM(TotalPoTotalEuroWithVAT)) IS NULL 
                                                   THEN 0 ELSE SUM(TotalPoTotalRubleWithVAT) / SUM(TotalPoTotalEuroWithVAT) END) AS FxRate
                            FROM          dbo.View_CostCodeSummary0_WithVAT AS View_CostCodeSummary0_WithVAT_1
                            GROUP BY ProjectID) AS fxRate ON dbo.Table1_Project.ProjectID = fxRate.ProjectID LEFT OUTER JOIN
                      dbo.View_CostCodeSummary0_WithVAT ON dbo.Table1_Project.ProjectID = dbo.View_CostCodeSummary0_WithVAT.ProjectID
GROUP BY dbo.Table1_Project.ProjectID, CONVERT(nvarChar(5), dbo.Table1_Project.ProjectID) + N'-' + N'EUR', RTRIM(Table1_Project_1.ProjectName), 
                      fxRate.FxRate
UNION ALL

SELECT     dbo.Table1_Project.ProjectID, RTRIM(Table1_Project_1.ProjectName) AS ProjectName, CONVERT(nvarChar(5), dbo.Table1_Project.ProjectID) 
                      + N'-' + N'USD' AS Prj_Currency, SUM(CASE WHEN TotalDollarPaidWithVAT IS NULL THEN 0 ELSE TotalDollarPaidWithVAT END) AS Total_Paid_Inc_VAT, 
                      fxRate.FxRate
FROM         dbo.Table1_Project INNER JOIN
                      dbo.Table1_Project AS Table1_Project_1 ON dbo.Table1_Project.ProjectID = Table1_Project_1.ProjectID LEFT OUTER JOIN
                          (SELECT     ProjectID, CONVERT(numeric(5, 2), CASE WHEN (SUM(TotalPoTotalRubleWithVAT) / SUM(TotalPoTotalDollarWithVAT)) IS NULL 
                                                   THEN 0 ELSE SUM(TotalPoTotalRubleWithVAT) / SUM(TotalPoTotalDollarWithVAT) END) AS FxRate
                            FROM          dbo.View_CostCodeSummary0_WithVAT AS View_CostCodeSummary0_WithVAT_1
                            GROUP BY ProjectID) AS fxRate ON dbo.Table1_Project.ProjectID = fxRate.ProjectID LEFT OUTER JOIN
                      dbo.View_CostCodeSummary0_WithVAT ON dbo.Table1_Project.ProjectID = dbo.View_CostCodeSummary0_WithVAT.ProjectID
GROUP BY dbo.Table1_Project.ProjectID, CONVERT(nvarChar(5), dbo.Table1_Project.ProjectID) + N'-' + N'USD', RTRIM(Table1_Project_1.ProjectName), 
                      fxRate.FxRate

UNION ALL

SELECT     dbo.Table1_Project.ProjectID, RTRIM(Table1_Project_1.ProjectName) AS ProjectName, CONVERT(nvarChar(5), dbo.Table1_Project.ProjectID) 
                      + N'-' + N'RUB' AS Prj_Currency, SUM(CASE WHEN TotalRublePaidWithVAT IS NULL THEN 0 ELSE TotalRublePaidWithVAT END) AS Total_Paid_Inc_VAT, 
                      fxRate.FxRate
FROM         dbo.Table1_Project INNER JOIN
                      dbo.Table1_Project AS Table1_Project_1 ON dbo.Table1_Project.ProjectID = Table1_Project_1.ProjectID LEFT OUTER JOIN
                          (SELECT     ProjectID, CONVERT(numeric(5, 2), CASE WHEN (SUM(TotalPoTotalRubleWithVAT) / SUM(TotalPoTotalRubleWithVAT)) IS NULL 
                                                   THEN 0 ELSE SUM(TotalPoTotalRubleWithVAT) / SUM(TotalPoTotalRubleWithVAT) END) AS FxRate
                            FROM          dbo.View_CostCodeSummary0_WithVAT AS View_CostCodeSummary0_WithVAT_1
                            GROUP BY ProjectID) AS fxRate ON dbo.Table1_Project.ProjectID = fxRate.ProjectID LEFT OUTER JOIN
                      dbo.View_CostCodeSummary0_WithVAT ON dbo.Table1_Project.ProjectID = dbo.View_CostCodeSummary0_WithVAT.ProjectID
GROUP BY dbo.Table1_Project.ProjectID, CONVERT(nvarChar(5), dbo.Table1_Project.ProjectID) + N'-' + N'RUB', RTRIM(Table1_Project_1.ProjectName), 
                      fxRate.FxRate
) AS DAtaSource1
ORDER BY Prj_Currency ASC
 "
    ></asp:SqlDataSource>
</asp:Content>


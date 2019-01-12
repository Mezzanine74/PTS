<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MakeHistoryForFollowUpKAtya.aspx.vb" Inherits="MakeHistoryForFollowUpKatya" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link Rel="Stylesheet" type="text/css" href="Styles.css" />
    <title></title>
</head>
<body >
    <form id="form1" runat="server">
    <div>
    
      <asp:Button ID="ButtonRun" runat="server" Text="Run" />
    
      POid><asp:TextBox ID="TextBoxProjectID" runat="server" ></asp:TextBox>
      POkey><asp:TextBox ID="TextBoxPOKey" runat="server" ></asp:TextBox>
      Start><asp:TextBox ID="TextBoxStart" runat="server" Text=" 00:00:00"></asp:TextBox>
      Finish><asp:TextBox ID="TextBoxFinish" runat="server" Text=" 00:00:00"></asp:TextBox>
    
    </div>

    <asp:GridView ID="GridViewSendToAction" runat="server" AutoGenerateColumns="False" 
      DataKeyNames="ProjectID" DataSourceID="SqlDataSourceProjectsToGo" 
      EnableModelValidation="True" CssClass="Grid" AllowPaging="True" AllowSorting="True" 
      PageSize="30" PagerSettings-Position="TopAndBottom">
      <Columns>
        <asp:TemplateField HeaderText="ProjectID" SortExpression="ProjectID">
          <ItemTemplate>
            <asp:Button ID="ButtonSendToAction" runat="server" Text="Send To Action" 
            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="SendToAction" />
          </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="ProjectID" SortExpression="ProjectID">
          <ItemTemplate>
            <asp:Label ID="LabelPrjID" runat="server" Text='<%# Bind("ProjectID") %>'></asp:Label>
          </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="ProjectName" SortExpression="ProjectName">
          <ItemTemplate>
            <asp:Label ID="LabelPrjName" runat="server" Text='<%# Bind("ProjectName") %>'></asp:Label>
          </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="MinPO_Date" SortExpression="MinPO_Date">
          <ItemTemplate>
            <asp:Label ID="LabelMinDate" runat="server" Text='<%# Bind("MinPO_Date") %>'></asp:Label>
          </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="MaxDateForProject" 
          SortExpression="MaxDateForProject">
          <ItemTemplate>
            <asp:Label ID="LabelMaxDate" runat="server" Text='<%# Bind("MaxDateForProject") %>'></asp:Label>
          </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="DaySinceFirstAction" 
          SortExpression="DaySinceFirstAction">
          <ItemTemplate>
            <asp:Label ID="Label5" runat="server" Text='<%# Bind("DaySinceFirstAction") %>'></asp:Label>
          </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="DaySinceLastAction" 
          SortExpression="DaySinceLastAction">
          <ItemTemplate>
            <asp:Label ID="Label6" runat="server" Text='<%# Bind("DaySinceLastAction") %>'></asp:Label>
          </ItemTemplate>
        </asp:TemplateField>
      </Columns>
<PagerSettings Position="TopAndBottom"></PagerSettings>
        <pagerstyle  horizontalalign="Center" CssClass="pager" />
        <RowStyle  CssClass="GridItemNakladnaya" />
        <HeaderStyle  CssClass="GridHeader" />
        <PagerStyle CssClass="pager" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceProjectsToGo" runat="server" 
      ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand="SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, dbo.Table1_Project.ProjectName, View_SIL_MinPOdatePerProject.MinPO_Date, 
                      CASE WHEN maxpo_date &gt; maxpaymentdate THEN maxpo_date WHEN maxpo_date &lt; maxpaymentdate THEN maxpaymentdate WHEN maxpo_date = maxpaymentdate
                       THEN maxpaymentdate END AS MaxDateForProject, DATEDIFF(day, View_SIL_MinPOdatePerProject.MinPO_Date, GETDATE()) AS DaySinceFirstAction, 
                      DATEDIFF(day, 
                      CASE WHEN maxpo_date &gt; maxpaymentdate THEN maxpo_date WHEN maxpo_date &lt; maxpaymentdate THEN maxpaymentdate WHEN maxpo_date = maxpaymentdate
                       THEN maxpaymentdate END, GETDATE()) AS DaySinceLastAction
FROM         dbo.Table1_Project LEFT OUTER JOIN
                      View_SIL_MinPOdatePerProject ON dbo.Table1_Project.ProjectID = View_SIL_MinPOdatePerProject.ProjectID LEFT OUTER JOIN
                      View_SILmAxPaymentDatePerProject ON dbo.Table1_Project.ProjectID = View_SILmAxPaymentDatePerProject.ProjectID LEFT OUTER JOIN
                      View_SILMaxPoDatePerProject ON dbo.Table1_Project.ProjectID = View_SILMaxPoDatePerProject.ProjectID
WHERE     (View_SIL_MinPOdatePerProject.MinPO_Date IS NOT NULL)
ORDER BY DaySinceLastAction ASC"></asp:SqlDataSource>

    </form>
</body>
</html>

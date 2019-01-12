<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="PObreakdown.aspx.vb" Inherits="PObreakdown" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


        <asp:DropDownList ID="DropDownListPrj" runat="server" 
            DataSourceID="SqlDataSourcePrj" DataTextField="ProjectName" 
            DataValueField="ProjectID" >
        </asp:DropDownList>

        <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        
            SelectCommand="SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, dbo.Table1_Project.ProjectName, dbo.aspnet_Users.UserName FROM         dbo.Table1_Project INNER JOIN  dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN  dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId WHERE     (dbo.aspnet_Users.UserName = @UserName) AND (dbo.Table1_Project.CurrentStatus=1 ) ORDER BY dbo.Table1_Project.ProjectName">
            <SelectParameters>
                <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" 
                    PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:TextBox ID="TextBoxUserName" runat="server" Visible="False"></asp:TextBox>

        <asp:DropDownList ID="DropDownListCurrency" runat="server"  AutoPostBack ="true">
            <asp:ListItem Selected="True">ALL</asp:ListItem>
            <asp:ListItem >Rub</asp:ListItem>
            <asp:ListItem>Dollar</asp:ListItem>
            <asp:ListItem>Euro</asp:ListItem>
        </asp:DropDownList>

        <asp:DropDownList ID="DropDownListSupplier" runat="server" 
            DataSourceID="SqlDataSourceSupplier" DataTextField="SupplierName" 
            DataValueField="SupplierID" >
        </asp:DropDownList>

        <asp:SqlDataSource ID="SqlDataSourceSupplier" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
            SelectCommand=" CREATE TABLE #SupplierInfo (
                            SupplierID nVarChar(12),
                            SupplierName nVarChar(100))

                             If @Currency = N'ALL' 
 
					                            INSERT INTO #SupplierInfo (SupplierID,SupplierName) (
                                                SELECT N'ALL' as SupplierID, N'_ALL SUPPLIER' AS SupplierName
                                                UNION ALL
                                                SELECT     TOP (100) PERCENT dbo.Table6_Supplier.SupplierID, RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName
                                                FROM         dbo.Table2_PONo INNER JOIN
                                                                      dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID INNER JOIN
                                                                      dbo.Table1_Project ON dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID
                                                WHERE     (dbo.Table1_Project.ProjectID = @ProjectID)
                                                GROUP BY RTRIM(dbo.Table6_Supplier.SupplierName), dbo.Table6_Supplier.SupplierID)

                                                If @Currency = N'Rub'  OR @Currency = N'Euro'  OR @Currency = N'Dollar'  
					                            INSERT INTO #SupplierInfo (SupplierID,SupplierName) (
                                                SELECT N'ALL' as SupplierID, N'_ALL SUPPLIER' AS SupplierName
                                                UNION ALL
                                                SELECT     TOP (100) PERCENT dbo.Table6_Supplier.SupplierID, RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName
                                                FROM         dbo.Table2_PONo INNER JOIN
                                                                      dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID INNER JOIN
                                                                      dbo.Table1_Project ON dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID
                                                WHERE     (dbo.Table1_Project.ProjectID = @ProjectID) AND (dbo.Table2_PONo.PO_Currency LIKE N'%' + @Currency + N'%')
                                                GROUP BY dbo.Table6_Supplier.SupplierID, RTRIM(dbo.Table6_Supplier.SupplierName))
                    
                            SELECT SupplierID, SupplierName FROM #SupplierInfo 
                            ORDER BY SupplierName ASC

                            DROP table #SupplierInfo ">
            <SelectParameters>
              <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" 
                Name="ProjectID" PropertyName="SelectedValue" />
              <asp:ControlParameter ControlID="DropDownListCurrency" DefaultValue="-" 
                Name="Currency" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:Button ID="ButtonRunReport" runat="server" Text="Run Report" CssClass="btn btn-mini btn-success" />

        <asp:ImageButton ID="ImageButtonExportExcelProcurementReportTotal" runat="server" Width="20px" ToolTip="Export To Excel"
            ImageUrl="~/Images/Excel.jpg" Visible="True" />

          <asp:ImageButton ID="ImageButtonRefresh" runat="server" ToolTip="Refresh"
            ImageUrl="~/Images/refreshNew.png" Visible="True" />

    <rsweb:ReportViewer ID="ReportViewerFollowUpReportBySupplierExcVAT" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="false" ShowZoomControl="true" Visible="false" 
    SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
    </rsweb:ReportViewer>

</asp:Content>


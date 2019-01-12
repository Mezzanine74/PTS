<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="FollowUpReportBySupplierExcVAT.aspx.vb" Inherits="_Nakl_FollowUpReportBySupplierExcVAT" %>

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

        <asp:DropDownList ID="DropDownListCurrency" runat="server" >
            <asp:ListItem Selected="True">Ruble</asp:ListItem>
            <asp:ListItem>Dollar</asp:ListItem>
            <asp:ListItem>Euro</asp:ListItem>
        </asp:DropDownList>

        <asp:Button ID="ButtonRunReport" runat="server" Text="Run Report" CssClass="btn btn-primary btn-mini" />

        <asp:ImageButton ID="ImageButtonExportExcelProcurementReportTotal" runat="server" Width="20px" ToolTip="Export To Excel"
            ImageUrl="~/Images/Excel.jpg" Visible="True" />

          <asp:ImageButton ID="ImageButtonRefresh" runat="server" Width="20px" ToolTip="Refresh"
            ImageUrl="~/Images/refresh.png" Visible="True" />

<hr/>


    <rsweb:ReportViewer ID="ReportViewerFollowUpReportBySupplierExcVAT" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="false" ShowZoomControl="true" Visible="false" 
    SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
    </rsweb:ReportViewer>

</asp:Content>


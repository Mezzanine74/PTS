<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="FollowUpReportBySupplierProjectWithVAT.aspx.vb" Inherits="_Nakl_FollowUpReportBySupplierProjectWithVAT" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


        <asp:DropDownList ID="DropDownListSupplier" runat="server" 
            DataSourceID="SqlDataSourceSupplierName" DataTextField="SupplierName" 
            DataValueField="SupplierID" >
        </asp:DropDownList>

        <asp:SqlDataSource ID="SqlDataSourceSupplierName" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        
            SelectCommand="select supplierID, rtrim(SupplierName) as SupplierName from [dbo].[Table6_Supplier] order by SupplierName asc">
        </asp:SqlDataSource>

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


    <rsweb:ReportViewer ID="ReportViewerFollowUpReportBySupplierExcVAT" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="false" ShowZoomControl="true" Visible="false" 
    SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
    </rsweb:ReportViewer>

</asp:Content>


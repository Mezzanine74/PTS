<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="ContractAddendums.aspx.vb" Inherits="ContractAddendums" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    SELECT Project To Generate Excel File > 

    <asp:DropDownList ID="DropDownListPrj" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListPrj_SelectedIndexChanged"
        DataSourceID="SqlDataSourcePrj" DataTextField="ProjectName"
        DataValueField="ProjectID" Font-Size="10px">
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server"
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
        SelectCommand="SELECT 0 AS ProjectID, N'_ALL PROJECTS' AS ProjectName UNION ALL SELECT [ProjectID], [ProjectName] FROM [Table1_Project] WHERE ([NewGeneration] = 1) ORDER BY [ProjectName]"></asp:SqlDataSource>

    <asp:Button ID="ButtonRun" runat="server" Text="Run Report" CssClass="btn btn-mini" Visible="false" />

    <asp:Button ID="ButtonExportExcel" runat="server" Text="Export To Excel" CssClass="btn btn-mini" Visible="false" />

    <div style="text-align: center; width: 100%">
        <rsweb:ReportViewer ID="ReportViewerContractAddendum" runat="server" ProcessingMode="remote"
            ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
            ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
            ShowToolBar="false" ShowZoomControl="true" Visible="false"
            SizeToReportContent="True" ZoomMode="FullPage" AsyncRendering="False">
        </rsweb:ReportViewer>
    </div>


</asp:Content>


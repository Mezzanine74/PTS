<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="PoBreakdownFor1SReconsoliation.aspx.vb" Inherits="PoBreakdownFor1SReconsoliation" %>

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
                        SelectCommand="SELECT 0 AS ProjectID, N'_ALL PROJECTS' AS ProjectName UNION ALL SELECT [ProjectID], [ProjectName] FROM [Table1_Project] WHERE ([CurrentStatus] = 1) ORDER BY [ProjectName]">
                    </asp:SqlDataSource>

                    <asp:Button ID="ButtonRun" runat="server" Text="Run Report" CssClass="btn btn-mini btn-success"/>

                    <asp:Button ID="ButtonExportExcel" runat="server" Text="Export To Excel" CssClass="btn btn-mini btn-default" />


    <div style="text-align: center; width: 100%">
        <rsweb:ReportViewer ID="ReportViewerContractAddendum" runat="server"  ProcessingMode="remote" 
        ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
        ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
        ShowToolBar="false" ShowZoomControl="true" Visible="false" 
        SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
        </rsweb:ReportViewer>
    </div>


</asp:Content>


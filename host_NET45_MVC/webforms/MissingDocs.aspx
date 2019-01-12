<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="MissingDocs.aspx.vb" Inherits="MissingDocs" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

                          <asp:DropDownList ID="DropDownListPrj" runat="server"  autopostback="true"
                              DataSourceID="SqlDataSourcePrj" DataTextField="ProjectName" 
                              DataValueField="ProjectID" >
                          </asp:DropDownList>

    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand="SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID,
         (RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5),dbo.Table1_Project.ProjectID))) as ProjectName
          FROM   dbo.Table1_Project 
          WHERE  (dbo.Table1_Project.CurrentStatus = 1) 
            ORDER BY dbo.Table1_Project.ProjectName">
    </asp:SqlDataSource>

        &nbsp;&nbsp;&nbsp;

            <asp:LinkButton ID="ImageButton1" runat="server" Text="Export To Excel"
                OnClick="ImageButton1_Click"
                ToolTip="Export To Excel" CssClass="label label-sm label-primary arrowed arrowed-right"/>


<div style="text-align: center; width: 100%">
    <rsweb:ReportViewer ID="ReportViewerCostReport" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="false" ShowZoomControl="true" Visible="false" 
    SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
    <ServerReport
       DisplayName="CostReport" ReportPath="/"
          ReportServerUrl="http://localhost/ReportServer_SQLEXPRESS" />
    </rsweb:ReportViewer>
 </div>

</asp:Content>


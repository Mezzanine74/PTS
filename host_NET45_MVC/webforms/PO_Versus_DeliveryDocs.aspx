<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="PO_Versus_DeliveryDocs.aspx.vb" Inherits="PO_Versus_DeliveryDocs" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:DropDownList ID="ddlProject" runat="server" DataSourceID="SqlDataSourcePrj" AutoPostBack="true" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged"
        DataTextField="ProjectName" DataValueField="ProjectID">
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand="SELECT     TOP (100) PERCENT ProjectID, ProjectName
    FROM         (SELECT     0 AS ProjectID, N'_Select Project' AS ProjectName
                           UNION ALL
                           SELECT     TOP (100) PERCENT ProjectID, RTRIM(ProjectName) + N' - ' + RTRIM(CONVERT(nvarChar(10), ProjectID)) AS Expr1
                           FROM         dbo.Table1_Project
                           WHERE     (CurrentStatus = 1)) AS Source
    ORDER BY ProjectName"></asp:SqlDataSource>

    <rsweb:ReportViewer ID="ReportViewerPO" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="false" ShowZoomControl="true" Visible="false" 
    SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
    </rsweb:ReportViewer>

</asp:Content>


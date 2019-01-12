<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="BalanceOnDifferentCurrencies.aspx.vb" Inherits="BalanceOnDifferentCurrencies" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <div style="text-align: center; width: 100%">
        <rsweb:ReportViewer ID="ReportViewerContractAddendum" runat="server"  ProcessingMode="remote" 
        ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
        ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
        ShowToolBar="false" ShowZoomControl="true" Visible="false" 
        SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
        </rsweb:ReportViewer>
    </div>

</asp:Content>


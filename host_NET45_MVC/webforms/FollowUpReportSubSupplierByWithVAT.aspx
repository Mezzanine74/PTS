<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="FollowUpReportSubSupplierByWithVAT.aspx.vb" Inherits="FollowUpReportSubSupplierByWithVAT" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>FollowUp Report Sub By Supplier</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <rsweb:ReportViewer ID="ReportViewer_" runat="server"  
    ProcessingMode="remote"     ShowCredentialPrompts="False" 
    ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="True" ShowParameterPrompts="False" 
    ShowPromptAreaButton="False"
    ShowToolBar="False" ShowZoomControl="False" Visible="false" 
    SizeToReportContent="True" ZoomMode="PageWidth"  AsyncRendering="False">
    </rsweb:ReportViewer>

</asp:Content>


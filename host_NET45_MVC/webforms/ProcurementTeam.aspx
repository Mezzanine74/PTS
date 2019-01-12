<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="ProcurementTeam.aspx.vb" Inherits="ProcurementTeam" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<div >
    <table>
     <tr>
      <td>
        <asp:ImageButton ID="ImageButtonExportExcelProcurementReportTotal" runat="server" Width="20px" ToolTip="Export To Excel"
            ImageUrl="~/Images/Excel.jpg" Visible="True" />
      </td>
      <td style="font-size: 10px; color: #808080; font-weight: bold; font-style: italic; width: 300px; text-align: right;">Refresh All Reports
        <asp:ImageButton ID="ImageButtonRefresh" runat="server" Width="20px" ToolTip="Refresh"
            ImageUrl="~/Images/refresh.png" Visible="True" />
      </td>
     </tr>
    </table>
    <rsweb:ReportViewer ID="ReportViewerProcurementReportTotal" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="false" ShowZoomControl="true" Visible="false" 
    SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
    </rsweb:ReportViewer>
 </div>
 <br />
<div >
        <asp:ImageButton ID="ImageButtonExportExcelProcurementReportByProjects" runat="server" Width="20px" ToolTip="Export To Excel"
            ImageUrl="~/Images/Excel.jpg" Visible="True" />
    <br />
    <rsweb:ReportViewer ID="ReportViewerProcurementReportByProjects" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="false" ShowZoomControl="true" Visible="false" 
    SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
    </rsweb:ReportViewer>
 </div>


</asp:Content>


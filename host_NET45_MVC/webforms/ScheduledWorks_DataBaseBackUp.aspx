<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="ScheduledWorks_DataBaseBackUp.aspx.vb" Inherits="_Nakl_ScheduledWorks_DataBaseBackUp" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<meta name="viewport" content="width=320;user-scalable=no"/>
<link rel="apple-touch-icon" sizes="57x57" href="/BackUp.png"/>

    <asp:SqlDataSource ID="SqlDataSourceAgeingInsert" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" >
    </asp:SqlDataSource>

        Execution Time :<asp:TextBox ID="TextBoxTime" runat="server">18</asp:TextBox>
    <br />
        Shift Day <asp:TextBox ID="TextBoxShift" runat="server">0</asp:TextBox>

    <br />
        <asp:Button ID="ButtonExecute" runat="server" Text="Button" OnClick="ButtonExecute_Click" />

        <asp:SqlDataSource ID="SqlDataSourceFollowUp" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" >
        </asp:SqlDataSource>
    

    <asp:Label ID="LabelInfo" runat="server" Text=""  ></asp:Label>

<div style="text-align: center; width: 100%">
    <rsweb:ReportViewer ID="ReportViewerDailyReportToPatrick" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="false" ShowZoomControl="true" Visible="false" 
    SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
    <ServerReport
       DisplayName="CostReport" ReportPath="/"
          ReportServerUrl="http://localhost/ReportServer_SQLEXPRESS" />
    </rsweb:ReportViewer>
 </div>

<div style="text-align: center; width: 100%">
    <rsweb:ReportViewer ID="_Nakl_CostReportInEuroWthSubPoWthPaidIKEA" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="false" ShowZoomControl="true" Visible="false" 
    SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
    </rsweb:ReportViewer>
</div>


</asp:Content>


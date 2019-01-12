<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="DeliveryPackingListEmail.aspx.vb" Inherits="DeliveryPackingListEmail" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


  <div style="text-align: center; width: 100%">
    <asp:DropDownList ID="DropDownListUserName" runat="server" 
      DataSourceID="SqlDataSourceUser" DataTextField="UserName" 
      DataValueField="UserName" AutoPostBack="true">
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSourceUser" runat="server" 
      ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand="SELECT     RTRIM(aspnet_Users.UserName) AS UserName
FROM         aspnet_Users INNER JOIN
                      aspnet_UsersInRoles ON aspnet_Users.UserId = aspnet_UsersInRoles.UserId AND aspnet_Users.UserId = aspnet_UsersInRoles.UserId INNER JOIN
                      aspnet_Roles ON aspnet_UsersInRoles.RoleId = aspnet_Roles.RoleId AND aspnet_UsersInRoles.RoleId = aspnet_Roles.RoleId
WHERE     (aspnet_Roles.RoleName = N'Enter_Nakladnaya')"></asp:SqlDataSource>
    <asp:Button ID="Button_Test" runat="server" Text="Test" />
    <rsweb:ReportViewer ID="ReportViewerDeliveryReport" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="true" ShowZoomControl="true" Visible="false" 
    SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
    </rsweb:ReportViewer>
 </div>

</asp:Content>
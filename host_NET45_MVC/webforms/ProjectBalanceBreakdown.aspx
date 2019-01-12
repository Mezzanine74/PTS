<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="ProjectBalanceBreakdown.aspx.vb" Inherits="ProjectBalanceBreakdown" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Styles.css" rel="stylesheet" type="text/css" />
  <style type="text/css">

  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<table>
 <tr>
  <td>
                          <asp:DropDownList ID="DropDownListPrj" runat="server"  
                              DataSourceID="SqlDataSourcePrj" DataTextField="ProjectName" 
                              DataValueField="ProjectName"  >
                          </asp:DropDownList>

    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand="SELECT     RTRIM(dbo.Table1_Project.ProjectName)  as ProjectName,
          dbo.aspnet_Users.UserName 
          FROM         dbo.Table1_Project 
          INNER JOIN  dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID 
          INNER JOIN  dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId 
          WHERE     (dbo.aspnet_Users.UserName = @UserName) AND (dbo.Table1_Project.CurrentStatus = 1) AND (dbo.Table1_Project.ContractCurrency IN (N'Rub',N'Dollar',N'Euro'))
            ORDER BY dbo.Table1_Project.ProjectName">
        <SelectParameters>
            <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" 
                PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>

                            <asp:DropDownList ID="DropDownListCurrency" runat="server" >
                                <asp:ListItem Value="Rub">Ruble</asp:ListItem>
                                <asp:ListItem Value="Dollar">Dollar</asp:ListItem>
                                <asp:ListItem Value="Euro">Euro</asp:ListItem>
                            </asp:DropDownList>                            

                          <asp:Button ID="ButtonRunReport" runat="server" Text="Run Report as HTML" CssClass="btn btn-mini"/>

     <asp:TextBox ID="TextBoxUserName" runat="server" Visible="False"></asp:TextBox>

                          <asp:Button ID="ButtonExportToExcel" runat="server" Text="Run Report as Excel" CssClass="btn btn-mini"  />

  </td>
  <td>
                          <asp:RadioButtonList ID="RadioButtonListReportType" runat="server" CellPadding="0" 
                            CellSpacing="0" CssClass="LabelGeneral">
                            <asp:ListItem Selected="True" Value="1"
                            >Partial Balance Breakdown which ignore any POs which are potentially to be closed as last invoices currently in pending list</asp:ListItem>
                            <asp:ListItem Value="2">Complete Balance Breakdown - Detailed </asp:ListItem>
                            <asp:ListItem Value="3">Complete Balance Breakdown - Per PO number </asp:ListItem>
                          </asp:RadioButtonList>
  </td>
 </tr>
</table>


<div style="text-align: center; width: 100%">
    <rsweb:ReportViewer ID="ReportViewerProjectBalanceBreakdown" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="False" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="false" ShowZoomControl="False" Visible="false" 
    SizeToReportContent="True" ZoomMode="FullPage"  AsyncRendering="False">
    <ServerReport
       DisplayName="CostReport" ReportPath="/"
          ReportServerUrl="http://localhost/ReportServer_SQLEXPRESS" />
    </rsweb:ReportViewer>
 </div>

</asp:Content>


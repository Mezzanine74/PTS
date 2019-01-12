<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="AdvancePayments.aspx.vb" Inherits="AdvancePayments" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Styles.css" rel="stylesheet" type="text/css" />
  <style type="text/css">

  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

                          <asp:DropDownList ID="DropDownListPrj" runat="server"  
                              DataSourceID="SqlDataSourcePrj" DataTextField="ProjectName" 
                              DataValueField="ProjectID"  Font-Size="10px" Width="160px" >
                          </asp:DropDownList>

    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand="SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID,
         (RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5),dbo.Table1_Project.ProjectID))) as ProjectName,
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

                          <asp:DropDownList ID="DropDownListCurrency" runat="server" 
                              DataSourceID="SqlDataSourceCurrency" DataTextField="ContractCurrency" 
                              DataValueField="ContractCurrency" Visible="false">
                          </asp:DropDownList>

                          <asp:SqlDataSource ID="SqlDataSourceCurrency" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                              SelectCommand="SELECT RTRIM([ContractCurrency]) AS ContractCurrency FROM [Table1_Project] WHERE ([ProjectID] = @ProjectID)">
                              <SelectParameters>
                                  <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" 
                                      Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
                              </SelectParameters>
                          </asp:SqlDataSource>

                          <asp:Button ID="ButtonRunReport" runat="server" Text="Run Report" 
                              Font-Size="10px"  />

                          &nbsp;&nbsp;&nbsp;&nbsp;

                          <asp:TextBox ID="TextBoxUserName" runat="server" Visible="False"></asp:TextBox>

<div style="text-align: center; width: 100%">
    <rsweb:ReportViewer ID="ReportViewerCostReport" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="False" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="true" ShowZoomControl="False" Visible="false" 
    SizeToReportContent="True" ZoomMode="FullPage"  AsyncRendering="False">
    <ServerReport
       DisplayName="CostReport" ReportPath="/"
          ReportServerUrl="http://localhost/ReportServer_SQLEXPRESS" />
    </rsweb:ReportViewer>
 </div>

</asp:Content>


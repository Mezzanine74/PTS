<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="FollowUpReport2.aspx.vb" Inherits="FollowUpReport22" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">



        <asp:DropDownList ID="DropDownListPrj" runat="server" 
            DataSourceID="SqlDataSourcePrj" DataTextField="ProjectName" 
            DataValueField="ProjectID" AutoPostBack="True">
        </asp:DropDownList>

        <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        
            SelectCommand="SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, dbo.Table1_Project.ProjectName, dbo.aspnet_Users.UserName FROM         dbo.Table1_Project INNER JOIN  dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN  dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId WHERE     (dbo.aspnet_Users.UserName = @UserName) AND (dbo.Table1_Project.CurrentStatus=1 ) ORDER BY dbo.Table1_Project.ProjectName">
            <SelectParameters>
                <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" 
                    PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>

     <asp:TextBox ID="TextBoxUserName" runat="server" Visible="False"></asp:TextBox>

        <asp:DropDownList ID="DropDownListCurrency" runat="server" >
            <asp:ListItem Selected="True">Ruble</asp:ListItem>
            <asp:ListItem>Dollar</asp:ListItem>
            <asp:ListItem>Euro</asp:ListItem>
        </asp:DropDownList>

        <asp:DropDownList ID="DropDownListBackUpDate" runat="server" 
            DataSourceID="SqlDataSourceBackUpDate" DataTextField="BackupDate" DataValueField="BackupDate">
        </asp:DropDownList>

        <asp:SqlDataSource ID="SqlDataSourceBackUpDate" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString_FOLLOWUPREPORT_BACKUPS %>" 
            SelectCommand="select BackupDate from ( 
                                select N'Today' as BackupDate
                                union all
                                SELECT     CONVERT(nvarChar(4), YEAR(BackUpDate)) + N'/' + CASE WHEN LEN(CONVERT(nvarChar(4), MONTH(BackupDate))) = 1 THEN N'0' + CONVERT(nvarChar(4), 
                            MONTH(BackupDate)) ELSE CONVERT(nvarChar(4), MONTH(BackupDate)) END + N'/' + CASE WHEN LEN(CONVERT(nvarChar(4), day(BackupDate))) 
                            = 1 THEN N'0' + CONVERT(nvarChar(4), day(BackupDate)) ELSE CONVERT(nvarChar(4), day(BackupDate)) END AS Expr1
                            FROM         dbo.Table_AvailableBackupDates
                            GROUP BY BackUpDate, ProjectID
                            HAVING      (ProjectID = @ProjectID)
		            ) As DataSource1236
		            ORDER BY BackupDate DESC">
            <SelectParameters>
               <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="" 
               Name="ProjectID" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:Button ID="ButtonRunReport" runat="server" Text="Run Report" CssClass="btn btn-primary btn-mini"/>


        <asp:HyperLink ID="HyperLinkVideo" runat="server" 
          ImageUrl="~/images/video.jpg"  Target="_blank" 
           ToolTip="Video" 
          NavigateUrl="~/video/followupReport_alternative/Default.aspx" 
          BorderColor="#003399" BorderStyle="Solid" BorderWidth="2px"></asp:HyperLink>

<asp:Label id="LabelVATstatus" runat="server"></asp:Label>

<div style="text-align: center; width: 100%">

     <rsweb:ReportViewer ID="ReportViewerFollowUp" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="False" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="true" ShowZoomControl="False" Visible="false" 
    SizeToReportContent="True" ZoomMode="FullPage"  AsyncRendering="False">
    <ServerReport
       DisplayName="FollowUpReport" ReportPath="/"
          ReportServerUrl="http://localhost/ReportServer_SQLEXPRESS" />
    </rsweb:ReportViewer>

 </div>

</asp:Content>


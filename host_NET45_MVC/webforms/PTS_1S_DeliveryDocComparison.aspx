<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="PTS_1S_DeliveryDocComparison.aspx.vb" Inherits="PTS_1S_DeliveryDocComparison" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:DropDownList ID="ddlProject" runat="server" DataSourceID="SqlDataSourcePrj"
        DataTextField="ProjectName" DataValueField="ProjectID">
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand="SELECT     TOP (100) PERCENT ProjectID, ProjectName
    FROM         (SELECT     0 AS ProjectID, N'_Select Project' AS ProjectName
                           UNION ALL
                           SELECT     TOP (100) PERCENT ProjectID, RTRIM(ProjectName) + N' - ' + RTRIM(CONVERT(nvarChar(10), ProjectID)) AS Expr1
                           FROM         dbo.Table1_Project
                           WHERE     (CurrentStatus = 1)) AS Source
    ORDER BY ProjectName"></asp:SqlDataSource>

    <div class="btn-group">
        <button data-toggle="dropdown" class="btn btn-primary btn-white dropdown-toggle">
            Get Reports
			<i class="ace-icon fa fa-angle-down icon-on-right"></i>
        </button>

        <ul class="dropdown-menu">
            <li>
                <asp:LinkButton ID="LnkBtn0" runat="server" OnClick="LnkBtn0_Click" >Export Delivery Document Excel</asp:LinkButton>
            </li>

            <li>
                <asp:LinkButton ID="LnkBtn1" runat="server" OnClick="LnkBtn1_Click">Export Missing Delivery Document Excel</asp:LinkButton>
            </li>

            <li>
                <asp:LinkButton ID="LnkBtn2" runat="server" OnClick="LnkBtn2_Click">Export Supplier List Documents Balanced but More Payments</asp:LinkButton>
            </li>

        </ul>
    </div>

    <asp:Label ID="LabelTheLAtestUpdate" runat="server" ForeColor="Red" Font-Italic="true" ></asp:Label>


<%--    <asp:Button ID="ButtonUpdateFromPTSManually" runat="server" Text="Update From PTS Manually" />--%>


    &nbsp;


    <asp:HyperLink ID="HyperLinkFeedMatchItems" runat="server" CssClass="btn btn-mini"
        NavigateUrl="~/PTS_1S_DeliveryDocComparisonGrid.aspx" Target="_blank">Got To Match Page</asp:HyperLink>

    &nbsp;&nbsp;

    <asp:HyperLink ID="HyperLinkPTS_1S_ProjectSummaryComparison" runat="server" CssClass="btn btn-mini"
        NavigateUrl="~/PTS_1S_ProjectSummaryComparison.aspx" Target="_blank">Project Summary Comparison</asp:HyperLink>

    <rsweb:ReportViewer ID="ReportViewerPO" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="false" ShowZoomControl="true" Visible="false" 
    SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
    </rsweb:ReportViewer>

</asp:Content>


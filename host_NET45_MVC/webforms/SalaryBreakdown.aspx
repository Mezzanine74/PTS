<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="SalaryBreakdown.aspx.vb" Inherits="SalaryBreakdown" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

           <table>
            <tr>
             <td Class="DrpDwnListGeneral">
                Project
             </td>
             <td Class="DrpDwnListGeneral">
                Year
             </td>
            </tr>
            <tr>
             <td>
                <asp:DropDownList ID="DropDownListProject" runat="server" 
                AutopostBack="True" DataSourceID="SqlDataSourceProjects" DataTextField="ProjectName" DataValueField="ProjectID" >
                </asp:DropDownList>
                 <asp:SqlDataSource ID="SqlDataSourceProjects" runat="server" 
                     ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                     SelectCommand=" 
					SELECT * FROM (
                     SELECT [ProjectID], RTRIM([ProjectName]) AS ProjectName  
                     FROM [Table1_Project] 
                     WHERE CurrentStatus = 1
                     
					UNION ALL

					SELECT 0, N'_ALL'
					) AS DataSource 
                    ORDER BY ProjectName ASC ">
                 </asp:SqlDataSource>
             </td>
             <td>
                <asp:DropDownList ID="DropDownListYear" runat="server" AutopostBack="true" >
                    <asp:ListItem Value="2015" Selected="True" >2015</asp:ListItem>
                    <asp:ListItem Value="2014">2014</asp:ListItem>
                    <asp:ListItem Value="2013">2013</asp:ListItem>
                    <asp:ListItem Value="2012">2012</asp:ListItem>
                    <asp:ListItem Value="2011">2011</asp:ListItem>
                    <asp:ListItem Value="2010">2010</asp:ListItem>
                    <asp:ListItem Value="2009">2009</asp:ListItem>
                    <asp:ListItem Value="2008">2008</asp:ListItem>
                    <asp:ListItem Value="0">ALL</asp:ListItem>
                </asp:DropDownList>
             </td>
             <td>
                <asp:ImageButton ID="ImageButtonExportExcel" runat="server" Width="20px" ToolTip="Export To Excel"
                ImageUrl="~/Images/Excel.jpg" Visible="True" />
             </td>
            </tr>
           </table>

    <rsweb:ReportViewer ID="ReportViewerCostReport" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="false" ShowZoomControl="true" Visible="false" 
    SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
    </rsweb:ReportViewer>

</asp:Content>


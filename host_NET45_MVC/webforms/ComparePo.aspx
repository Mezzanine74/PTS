<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="ComparePo.aspx.vb" Inherits="ComparePo" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

        <table>
         <tr>
          <td>
<img alt="" src="~/images/HintIcon.png" id="ImageHelp"  runat="server" height="20" />        &nbsp;
                <asp:HoverMenuExtender ID="HoverMenuExtenderHelp"  
                        runat="server"  
                        TargetControlID="ImageHelp"  
                        PopupControlID="PanelHelp" PopupPosition="Right" >
              </asp:HoverMenuExtender>

              <asp:Panel    
                        ID="PanelHelp"  
                        runat="server"  
                        BorderColor="#000000" CssClass="hidepanel"
                        BorderWidth="3px"  BackColor="White"  > 
                        <div style="margin: 2px; padding: 2px; color: #FFFFFF; background-color: #FF0000; text-align: center; font-size: large; font-weight: bold;">Hint:</div>
            <img alt="" src="~/images/ComparePo_help.png" id="ImgHelpInPanel"  runat="server"/>
              </asp:Panel>
          </td>
          <td>

      <table>
       <tr>
          <td>
              <table>
               <tr>
                <td>

                </td>
               </tr>
               <tr>
                <td>
                    <asp:DropDownList ID="DropDownListPrj" runat="server" 
                        DataSourceID="SqlDataSourcePrj" DataTextField="ProjectName" 
                        DataValueField="ProjectID" Width="267px" 
                        AutoPostBack="True">
                    </asp:DropDownList>

                    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        
                        SelectCommand="SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, rtrim(dbo.Table1_Project.ProjectName) as ProjectName , dbo.aspnet_Users.UserName FROM         dbo.Table1_Project INNER JOIN  dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN  dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId WHERE     (dbo.aspnet_Users.UserName = @UserName) AND (dbo.Table1_Project.CurrentStatus=1 ) ORDER BY dbo.Table1_Project.ProjectName">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" 
                                PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                 <asp:TextBox ID="TextBoxUserName" runat="server" Visible="False"></asp:TextBox>
                </td>
               </tr>
              </table>
          </td>
          <td>
              <asp:DropDownList ID="DropDownListCurrency" runat="server" 
                  Width="65px">
                  <asp:ListItem Selected="True">Ruble</asp:ListItem>
                  <asp:ListItem>Dollar</asp:ListItem>
                  <asp:ListItem>Euro</asp:ListItem>
              </asp:DropDownList>
          </td>
          <td>
            <asp:DropDownList ID="DropDownListStartDate" runat="server" 
                DataSourceID="SqlDataSourceStartDate" DataTextField="BackupDate" DataValueField="BackupDate">
            
            </asp:DropDownList>

            <asp:SqlDataSource ID="SqlDataSourceStartDate" runat="server" 
                ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString_FOLLOWUPREPORT_BACKUPS %>" 
                SelectCommand="select BackupDate from ( Select N'Select Start Date' as BackupDate
                                union all
                                SELECT     rtrim(BackupDate) as BackupDate
                            FROM         dbo.Table_AvailableBackupDates
                            GROUP BY BackUpDate, ProjectID
                            HAVING      (ProjectID = @ProjectID)
		            ) As DataSource1236
		            ORDER BY BackupDate DESC">
                <SelectParameters>
                   <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="" 
                   Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
                </SelectParameters>
            </asp:SqlDataSource>
          </td>
          <td>
              <asp:DropDownList ID="DropDownListFinishDate" runat="server" 
                  DataSourceID="SqlDataSourceFinishDate" DataTextField="BackupDate" DataValueField="BackupDate">
            
              </asp:DropDownList>

              <asp:SqlDataSource ID="SqlDataSourceFinishDate" runat="server" 
                  ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString_FOLLOWUPREPORT_BACKUPS %>" 
                  SelectCommand="select BackupDate from ( Select N'Select Finish Date' as BackupDate
                                union all
                                SELECT     rtrim(BackupDate) as BackupDate
                            FROM         dbo.Table_AvailableBackupDates
                            GROUP BY BackUpDate, ProjectID
                            HAVING      (ProjectID = @ProjectID)
		            ) As DataSource1236
		            ORDER BY BackupDate DESC">
                  <SelectParameters>
                     <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="" 
                     Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
                  </SelectParameters>
              </asp:SqlDataSource>
          </td>
       </tr>
      </table>

</td>
          <td style="padding-left:5px;">
              <asp:Button ID="ButtonRunReport" runat="server" Text="Run Report as HTML" CssClass="btn btn-mini" />
          </td>
          <td style="padding-left:5px;">
              <asp:Button ID="ButtonExportToExcel" runat="server" Text="Run Report as Excel" CssClass="btn btn-mini"  />
          </td>
          <td style="padding-left:5px;">
              <asp:Label ID="LabelWarning" runat="server" Text="" ></asp:Label>
          </td>
         </tr>
        </table>

<div style="text-align: center; width: 100%">

     <rsweb:ReportViewer ID="ReportViewerComparePo" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="False" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="false" ShowZoomControl="False" Visible="false" 
    SizeToReportContent="True" ZoomMode="FullPage"  AsyncRendering="False">
    <ServerReport
       DisplayName="ComparePo" ReportPath="/"
          ReportServerUrl="http://localhost/ReportServer_SQLEXPRESS" />
    </rsweb:ReportViewer>

 </div>


</asp:Content>
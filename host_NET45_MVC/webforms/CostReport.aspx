<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" EnableViewState="true" AutoEventWireup="false" CodeFile="CostReport.aspx.vb" Inherits="_Nakl_CostReport_RRS__2" %>

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

                          <asp:DropDownList ID="DropDownListPrj" runat="server"  autopostback="true"
                              DataSourceID="SqlDataSourcePrj" DataTextField="ProjectName"
                              DataValueField="ProjectID"   >
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

                          <asp:Button ID="ButtonRunReport" runat="server" Text="Run Report" Visible = "false" CssClass="btn btn-mini btn-default"  />

                          &nbsp;&nbsp;&nbsp;&nbsp;

                          <asp:HyperLink ID="HyperLinkEditData" runat="server" Target="_blank" CssClass="btn btn-mini btn-success" 
                            text="Edit Data" Visible="False"
                          ></asp:HyperLink>

                          &nbsp;&nbsp;&nbsp;&nbsp;

        <asp:ImageButton ID="ImageButtonExportExcel" runat="server" Width="20px" ToolTip="Export To Excel"
            ImageUrl="~/Images/Excel.jpg" Visible="True" />

        &nbsp;

        <asp:ImageButton ID="ImageButtonRefresh" runat="server" Width="20px" ToolTip="Refresh"
            ImageUrl="~/Images/refresh.png" Visible="True" />

        &nbsp;
                          <asp:HyperLink ID="HyperLinkSubPoList" runat="server" Target="_blank"  
                            text="SubPo List" Font-Size="10px" 
                          ></asp:HyperLink>


     <asp:TextBox ID="TextBoxUserName" runat="server" Visible="False"></asp:TextBox>

      </td>
      <td>
      <div style="OVERFLOW:auto; WIDTH:450px; HEIGHT:50px">
                    <asp:Label ID="labelWarning" runat="server" CssClass="LabelMessage" 
                     Text="Euther collected documents or Total PO are not correctly distrubuted." ></asp:Label>
                    <br />
                    <asp:DataList ID="DataListOpenDocuments" runat="server" 
                        DataSourceID="SqlDataSourceOpenDocuments" RepeatDirection="Horizontal"
                        Font-Size="10px">
                      <ItemTemplate>
                          <table >
                              <tr>
                                  <td >
                                          <div style="padding: 2px; width: 100px; background-color: #FF0066; text-align: right; margin-bottom: 1px; color: #FFFFFF;">
                                               <asp:HyperLink ID="HyperLinkSubPo" runat="server" Target="_blank" CssClass="Hlink" 
                                                Text='<%# Bind("PO_No") %>' Font-Underline="False" ForeColor="Black" Font-Bold="false">
                                             </asp:HyperLink>
                                          </div>
                                  </td>
                              </tr>
                          </table>
                      </ItemTemplate>
                    </asp:DataList>
                    <asp:SqlDataSource ID="SqlDataSourceOpenDocuments" runat="server" 
                             ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                             SelectCommand=" 
                                SELECT PO_No FROM (
                                SELECT     DataSourceTotalCollectedOriginalPo.PO_No 
                                FROM         dbo.Table2_PONo INNER JOIN
                                                      dbo.Table7_CostCode ON dbo.Table2_PONo.CostCode = dbo.Table7_CostCode.CostCode INNER JOIN
                                                          (SELECT     PO_No, SUM(CollectedValue) AS TotalCollectedValueSubPo
                                                            FROM          dbo.Table2_PONo_Sub
                                                            GROUP BY PO_No) AS DataSourceTotalCollectedSubPo INNER JOIN


                                                          (SELECT     PO_No, SumCollected AS TotalCollectedOriginalPo
                                                            FROM View_PO_CollectedDocsRubWithVAT ) AS DataSourceTotalCollectedOriginalPo 
                            
                                                            ON 
                                                      DataSourceTotalCollectedSubPo.PO_No = DataSourceTotalCollectedOriginalPo.PO_No ON 
                                                      dbo.Table2_PONo.PO_No = DataSourceTotalCollectedOriginalPo.PO_No
                                WHERE     (DataSourceTotalCollectedOriginalPo.TotalCollectedOriginalPo - DataSourceTotalCollectedSubPo.TotalCollectedValueSubPo > 0) AND 
                                                      (dbo.Table2_PONo.Project_ID = @ProjectID)

                                UNION ALL

                                SELECT     Source_TotalSubPO.PO_No
                                FROM         (SELECT     PO_No, SUM(TotalPrice) AS Sum_TotalPrice
                                                       FROM          dbo.Table2_PONo_Sub
                                                       GROUP BY PO_No) AS Source_TotalSubPO INNER JOIN
                                                      dbo.Table2_PONo ON Source_TotalSubPO.PO_No = dbo.Table2_PONo.PO_No
                                WHERE     (Source_TotalSubPO.Sum_TotalPrice / dbo.Table2_PONo.TotalPrice <> 1) AND (dbo.Table2_PONo.Project_ID = @ProjectID)


                                ) AS Source
                                GROUP BY PO_No
                                                 ">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="" 
                                    Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
                            </SelectParameters>
                    </asp:SqlDataSource>
      </div>

      </td>
      <td>
       <asp:button ID="ButtonRunForTamasDCCRubleHTML" runat="server" Text="DCS in Ruble HTML" Visible="false" CssClass="btn btn-mini btn-purple" />
       <asp:button ID="ButtonRunForTamasDCCRubleExcel" runat="server" Text="DCS in Ruble EXCEL" Visible="false" CssClass="btn btn-mini btn-purple" />
       <asp:TextBox ID="TextBoxExchangeRateTamas" runat="server" width="50px" ></asp:TextBox>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorRate" ControlToValidate="TextBoxExchangeRateTamas" Display="Dynamic"
                    runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorRate" runat="server"  Display="Dynamic"
                    ErrorMessage="Required" ControlToValidate="TextBoxExchangeRateTamas"></asp:RequiredFieldValidator>


      </td>
     </tr>
    </table>

<div style="text-align: center; width: 100%">
    <rsweb:ReportViewer ID="ReportViewerCostReport" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="false" ShowZoomControl="true" Visible="false" 
    SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
    </rsweb:ReportViewer>
</div>

</asp:Content>


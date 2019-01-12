<%@ Page Title="" Language="VB" MasterPageFile="~/site.master"  MaintainScrollPositionOnPostback="true" 
AutoEventWireup="false" EnableEventValidation ="false" CodeFile="Clients.aspx.vb" Inherits="contractView_ApprovalMatrixPTM3REVISED" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="POdetailsForEmail.ascx" tagname="SeperateControl" tagprefix="uc1" %>

<%@ Register src="WebUserControl_ContractEmailBody.ascx" tagname="SeperateControl2" tagprefix="uc1" %>

<%@ Register src="WebUserControl_AddendumEmailBody.ascx" tagname="SeperateControl3" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <title>View Contracts</title>
  <script type="text/javascript">
    function pageLoad() {
    }
    function SetAutoCompleteWidth(source, EventArgs) {
      var target
      target = ((document.getElementBy) ? document.getElementById("AutoCompleteDiv") : document.all.AutoCompleteDiv);
      target.style.width = '441px';
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<asp:panel ID="PanelToAddendums"  runat="server" CssClass="hidepanel">
   <uc1:SeperateControl ID="POdetailsForEmail" runat="server" />
   <uc1:SeperateControl2 ID="WebUserControl_ContractEmailBody" runat="server" />
   <uc1:SeperateControl3 ID="WebUserControl_AddendumEmailBody" runat="server" />
   <asp:Literal ID="LiteralCounter" runat="server" Text="0" ></asp:Literal>
   <asp:Label ID="LabelProjectName" runat="server" ></asp:Label>
   <asp:Label ID="LabelGridViewPagingStatus" runat="server" ></asp:Label>   
   <asp:Label ID="LabelGridViewPageSize" runat="server" ></asp:Label>         
   <asp:Label ID="LabelGridViewPageNumber" runat="server" ></asp:Label>      
   <asp:Label ID="LabelPOno" runat="server" ></asp:Label>
   <asp:Label ID="LabelSupplierNameV" runat="server" ></asp:Label>
   <asp:Label ID="LabelContractNo" runat="server" ></asp:Label>
   <asp:Label ID="LabelContractDate" runat="server" ></asp:Label>
   <asp:Label ID="LabelContractValue" runat="server" ></asp:Label>
   <asp:Label ID="LabelContractValueWithVAT" runat="server" ></asp:Label>
   <asp:Label ID="LabelContractCurrencyV" runat="server" ></asp:Label>
   <asp:Label ID="LabelContractType" runat="server" ></asp:Label>
   <asp:Label ID="LabelContractDescription" runat="server" ></asp:Label>
   <asp:Label ID="LabelContractID" runat="server" ></asp:Label>   
   <asp:DropDownList ID="DDLforSupplierNameCheck" runat="server"
    DataSourceID="SqlDataSourceforSupplierNameCheck" DataTextField="SupplierName" DataValueField="SupplierName"    
   ></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceforSupplierNameCheck" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
                     SelectCommand="SELECT RTRIM(SupplierName) AS SupplierName FROM Table6_Supplier WHERE SupplierID = @SupplierID">
                    <SelectParameters>
                        <asp:Parameter Name="SupplierID" />
                    </SelectParameters>
                </asp:SqlDataSource>
   <asp:Label ID="LabelEditModeIndex" runat="server" ></asp:Label>   
   <asp:SqlDataSource ID="SqlDataSourcePoDublication" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>">
   </asp:SqlDataSource>
   <asp:DropDownList ID="DropDownListPoDublication" runat="server"
    DataTextField="PO_No" DataValueField="PO_No" DataSourceID="SqlDataSourcePoDublication" >
   </asp:DropDownList>
</asp:panel>

    <table>
        <tr>
            <td>

            </td>
            <td style="width:220px;" >
                <asp:DropDownList ID="DropDownListProjectSelected" runat="server" DataSourceID="SqlDataSourceProjectsSelected" 
                    DataTextField="ProjectName" DataValueField="ProjectID" AutoPostBack="True" >
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceProjectsSelected" runat="server" ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand="SELECT * FROM (
SELECT 0 AS ProjectID, N'_Select Project' AS ProjectName

UNION ALL

SELECT     TOP (100) PERCENT dbo.Table_Contract_ProjectIDforClient.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + N' - ' + RTRIM(CONVERT(nVarChar(10), 
                      dbo.Table_Contract_ProjectIDforClient.ProjectID)) AS ProjectName
FROM         dbo.Table_Contract_ProjectIDforClient INNER JOIN
                      dbo.Table1_Project ON dbo.Table_Contract_ProjectIDforClient.ProjectID = dbo.Table1_Project.ProjectID
GROUP BY dbo.Table_Contract_ProjectIDforClient.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + N' - ' + RTRIM(CONVERT(nVarChar(10), 
                      dbo.Table_Contract_ProjectIDforClient.ProjectID))
) AS SourceProjectSelected
ORDER BY ProjectName ASC"></asp:SqlDataSource>
            </td>
            <td style="width:220px;">
                <asp:DropDownList ID="DropDownListPrjID" runat="server" AutoPostBack="True" DataSourceID="SqlDataSourcePrj" DataTextField="ProjectName" DataValueField="ProjectID" ondatabound="DropDownListPrjID_DataBound" Width="205px">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand="
                                SELECT 999 AS ProjectID, N'_CLIENTS' AS ProjectName, NULL AS UserName ">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td style="width:270px;" >
                <asp:DropDownList ID="DropDownListSupplier" runat="server" AutoPostBack="True" 
                    DataSourceID="SqlDataSourceSupplier" 
                    DataTextField="SupplierName" DataValueField="SupplierID" 
                    Width="260px">
                </asp:DropDownList>
                <asp:Panel ID="PanelSupplier" runat="server" CssClass="hidepanel">
                    <asp:SqlDataSource ID="SqlDataSourceSupplier" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand="SELECT     TOP (100) PERCENT dbo.Table_Contracts.ProjectID, dbo.Table_Contracts.SupplierID, RTRIM(dbo.Table6_Supplier.SupplierName) 
                      AS SupplierName
                        FROM         dbo.Table_Contracts INNER JOIN
                      dbo.Table6_Supplier ON dbo.Table_Contracts.SupplierID = dbo.Table6_Supplier.SupplierID INNER JOIN
                      dbo.Table1_Project ON dbo.Table_Contracts.ProjectID = dbo.Table1_Project.ProjectID
                        GROUP BY dbo.Table_Contracts.ProjectID, dbo.Table_Contracts.SupplierID, RTRIM(dbo.Table6_Supplier.SupplierName)
                       HAVING      (dbo.Table_Contracts.ProjectID = @ProjectID)
                        ORDER BY SupplierName">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownListPrjID" Name="ProjectID" 
                                PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </asp:Panel>
            </td>
            <td style="width:32px;">
                <asp:ImageButton ID="ImageButton1" runat="server" BorderColor="#666666" 
                    BorderStyle="Solid" BorderWidth="1px" ImageUrl="~/Images/Excel.jpg" 
                    ToolTip="Export To Excel" Width="20px" />
            </td>
            <td style="font-size: 10px; text-align: right; width: 100px;">
            Page Size
            </td>
            <td>
                 <asp:DropDownList ID="DDLpageSize" runat="server" AutoPostBack="true" >
                                        <asp:ListItem Value="ALL" Selected="True">ALL</asp:ListItem>                            
                                        <asp:ListItem Value="20">20</asp:ListItem>
                                        <asp:ListItem Value="40">40</asp:ListItem>
                                        <asp:ListItem Value="60">60</asp:ListItem>
                 </asp:DropDownList>
            </td>
            <td>
              <div>
                  <asp:Label ID="LabelInsurance" runat="server" BackColor="Yellow" 
                  Font-Bold="True" ForeColor="Red" Text="Insurance alert!"></asp:Label>
              </div>
              <div style="OVERFLOW:auto; WIDTH:500px; HEIGHT:45px">
                      <asp:DataList ID="DataListInsurance" runat="server" 
                        DataSourceID="SqlDataSourceInsurance" Font-Size="10px" ItemStyle-Wrap="False" 
                        RepeatDirection="Horizontal">
                        <ItemTemplate>
                          <table>
                            <td>
                              <div style="padding: 2px; background-color: #CC0000; text-align: right; margin-bottom: 1px; font-weight: bold; color: #FFFFFF;">
                                <asp:Label ID="LabelProjects" runat="server" Text='<%# Bind("ProjectName") %>'></asp:Label>
                              </div>
                            </td>
                            <td>
                              <div style="padding: 2px; background-color: #CC0000; text-align: right; margin-bottom: 1px; font-weight: bold; color: #FFFFFF;">
                                <asp:Label ID="LabelDayToGo" runat="server" Text='<%# Bind("DayToGo") %>'></asp:Label>
                              </div>
                            </td>
                          </table>
                        </ItemTemplate>
                      </asp:DataList>
                      <asp:SqlDataSource ID="SqlDataSourceInsurance" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
                        SelectCommand="SELECT     RTRIM(ProjectName) AS ProjectName, RTRIM(CONVERT(nvarchar(2), CASE WHEN InsuranceFinish IS NOT NULL THEN datediff(day, getdate(), InsuranceFinish) END)) 
                      + ' day left' AS DayToGo FROM         dbo.Table1_Project WHERE     (CASE WHEN InsuranceFinish IS NOT NULL THEN datediff(day, getdate(), InsuranceFinish) END <= 15) AND (InsuranceFinish IS NOT NULL) AND 
                      (CASE WHEN InsuranceFinish IS NOT NULL THEN datediff(day, getdate(), InsuranceFinish) END >= 0)"
                        ></asp:SqlDataSource>
              </div>
            </td>
            <td >
              <asp:ImageButton ID="ImageButtonNotApprovedItems" runat="server"  Width="120px" Visible="true"
                ImageUrl="~/images/NotApprovedItems.png" />
            </td>
            <td style="width: 60px">
              <div style="text-align: center">
                <asp:ImageButton ID="ImageButtonNotes" runat="server" 
                  ImageUrl="~/Images/Notes.png" PostBackUrl="~/webforms/contractnotes.aspx" />
              </div>
              <div style="text-align: center; font-size: 10px; font-weight: bold;">
                List Of Suppliers
              </div>
            </td>
        </tr>
    </table>
      <table>
          <tr>
           <td style="border: 2px solid #333333; color: #FFFFFF; width:40px; background-color: #FFFF00;">
           </td>
           <td style="font-size: 9px; font-weight: bold; color: #333333; font-style: italic;">
           > All Contracts and Addendums are highlighted by yellow if they are not signed by supplier and not signed by Mercury
           </td>
            <td style="width: 400px; text-align: right;">
               <asp:Label ID="LabelTheLatestContractNo" runat="server" ></asp:Label>
            </td>
          </tr>
      </table>

          <asp:TextBox ID="TextBoxUserName" runat="server" visible="false"></asp:TextBox>
        
    <asp:GridView ID="GridViewShowContract" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSourceForContractGrid" EnableModelValidation="True" 
        CssClass="GridContractView" AllowSorting="True" DataKeyNames="ContractID" >
        <Columns>

<asp:TemplateField >
                <HeaderTemplate>
                        <table style="color: #FFFFFF;">
                            <tr>
                                <td style="width:40px;">
                                </td>
                                <td style="width:20px;">
                                </td>
                                <td style="width:80px; text-align: center;">
                                    PO_no
                                </td>
                                <td style="width:80px; text-align: center;">
                                ContractNo
                                
                                    <table width=100% >
                                    <tr>
                                    <td width=100% >
                                        <table align=center>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderContractNoASC" runat="server" 
                                                        ImageUrl="~/Images/ASC.JPG" onclick="ImageButtonHeaderContractNoASC_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderContractNoDESC" runat="server" 
                                                        ImageUrl="~/Images/DESC.JPG" onclick="ImageButtonHeaderContractNoDESC_Click" />
                                                </td>
                                            </tr>
                                        </table> 
                                    </td>
                                    </tr>
                                    </table>                                
                                
                                </td>
                                <td style="width:80px; text-align: center;">
                                    ContractDate
                                    
                                    <table width=100% >
                                    <tr>
                                    <td width=100% >
                                        <table align=center>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderContractDateASC" runat="server" 
                                                        ImageUrl="~/Images/ASC.JPG" onclick="ImageButtonHeaderContractDateASC_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderContractDateDESC" runat="server" 
                                                        ImageUrl="~/Images/DESC.JPG" onclick="ImageButtonHeaderContractDateDESC_Click" />
                                                </td>
                                            </tr>
                                        </table> 
                                    </td>
                                    </tr>
                                    </table>                                        
                                    
                                </td>
                                <td style="width:135px; text-align: center;">
                                    SupplierName
                                    
                                    <table width=100% >
                                    <tr>
                                    <td width=100% >
                                        <table align=center>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderSupplierASC" runat="server" 
                                                        ImageUrl="~/Images/ASC.JPG" onclick="ImageButtonHeaderSupplierASC_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderSupplierDESC" runat="server" 
                                                        ImageUrl="~/Images/DESC.JPG" onclick="ImageButtonHeaderSupplierDESC_Click" />
                                                </td>
                                            </tr>
                                        </table> 
                                    </td>
                                    </tr>
                                    </table>                                        
                                    
                                </td>
                                <td style="width:130px; text-align: center;">
                                    ContractDescription
                                    
                                    <table width=100% >
                                    <tr>
                                    <td width=100% >
                                        <table align=center>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderDescriptionASC" runat="server" 
                                                        ImageUrl="~/Images/ASC.JPG" onclick="ImageButtonHeaderDescriptionASC_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderDescriptionDESC" runat="server" 
                                                        ImageUrl="~/Images/DESC.JPG" onclick="ImageButtonHeaderDescriptionDESC_Click" />
                                                </td>
                                            </tr>
                                        </table> 
                                    </td>
                                    </tr>
                                    </table>                                        
                                    
                                </td>
                                <td style="width:50px; text-align: center;">
                                    Contract Type
                                    
                                    <table width=100% >
                                    <tr>
                                    <td width=100% >
                                        <table align=center>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderContractTypeASC" runat="server" 
                                                        ImageUrl="~/Images/ASC.JPG" onclick="ImageButtonHeaderContractTypeASC_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderContractTypeDESC" runat="server" 
                                                        ImageUrl="~/Images/DESC.JPG" onclick="ImageButtonHeaderContractTypeDESC_Click" />
                                                </td>
                                            </tr>
                                        </table>                                    
                                    </td>
                                    </tr>
                                    </table>                                        
                                    
                                </td>
                                <td style="width:50px; text-align: center;">
                                    Signed By Supplier
                                    
                                    <table width=100% >
                                    <tr>
                                    <td width=100% >
                                        <table align=center>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderSignedBySupplierASC" runat="server" 
                                                        ImageUrl="~/Images/ASC.JPG" 
                                                        onclick="ImageButtonHeaderSignedBySupplierASC_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderSignedBySupplierDESC" runat="server" 
                                                        ImageUrl="~/Images/DESC.JPG" 
                                                        onclick="ImageButtonHeaderSignedBySupplierDESC_Click" />
                                                </td>
                                            </tr>
                                        </table>                                      
                                    </td>
                                    </tr>
                                    </table>                                        
                                    
                                </td>
                                <td style="width:50px; text-align: center;">
                                    Signed By mercury
                                    
                                    <table width=100% >
                                    <tr>
                                    <td width=100% >
                                        <table align=center>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderSignedByMercuryASC" runat="server" 
                                                        ImageUrl="~/Images/ASC.JPG" onclick="ImageButtonHeaderSignedByMercuryASC_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderSignedByMercuryDESC" runat="server" 
                                                        ImageUrl="~/Images/DESC.JPG" 
                                                        onclick="ImageButtonHeaderSignedByMercuryDESC_Click" />
                                                </td>
                                            </tr>
                                        </table>                                      
                                    </td>
                                    </tr>
                                    </table>                                        
                                    
                                </td>
                                <td style="width:50px; text-align: center; display: none;">
                                    Collected By Supplier
                                    
                                    <table width=100% >
                                    <tr>
                                    <td width=100% >
                                        <table align=center>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderCollectedBySupplierASC" runat="server" 
                                                        ImageUrl="~/Images/ASC.JPG" 
                                                        onclick="ImageButtonHeaderCollectedBySupplierASC_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderCollectedBySupplierDESC" runat="server" 
                                                        ImageUrl="~/Images/DESC.JPG" 
                                                        onclick="ImageButtonHeaderCollectedBySupplierDESC_Click" />
                                                </td>
                                            </tr>
                                        </table>                                        
                                    </td>
                                    </tr>
                                    </table>                                        
                                    
                                </td>
                                <td style="width:100px; text-align: center;">
                                    <asp:Label ID="LabelLarisaTakHatela" runat="server" ></asp:Label>
                                </td>
                                <td style="width:85px; text-align: center;">
                                    Contract Value
                                    
                                    <table width=100% >
                                    <tr>
                                    <td width=100% >
                                        <table align=center>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderContractValueASC" runat="server" 
                                                        ImageUrl="~/Images/ASC.JPG" onclick="ImageButtonHeaderContractValueASC_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderContractValueDESC" runat="server" 
                                                        ImageUrl="~/Images/DESC.JPG" onclick="ImageButtonHeaderContractValueDESC_Click" />
                                                </td>
                                            </tr>
                                        </table>                                       
                                    </td>
                                    </tr>
                                    </table>                                        
                                    
                                </td>
                                <td style="width:50px; text-align: center;">
                                </td>
                                <td style="width:50px; text-align: center;">
                                    Retention
                                    
                                    <table width=100% >
                                    <tr>
                                    <td width=100% >
                                        <table align=center>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderRetentionASC" runat="server" 
                                                        ImageUrl="~/Images/ASC.JPG" onclick="ImageButtonHeaderRetentionASC_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderRetentionDESC" runat="server" 
                                                        ImageUrl="~/Images/DESC.JPG" onclick="ImageButtonHeaderRetentionDESC_Click" />
                                                </td>
                                            </tr>
                                        </table>                                     
                                    </td>
                                    </tr>
                                    </table>                                        
                                    
                                </td>
                                <td style="width:50px; text-align: center;">
                                    Archived
                                    
                                    <table width=100% >
                                    <tr>
                                    <td width=100% >
                                        <table align=center>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderArchivedASC" runat="server" 
                                                        ImageUrl="~/Images/ASC.JPG" onclick="ImageButtonHeaderArchivedASC_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderArchivedDESC" runat="server" 
                                                        ImageUrl="~/Images/DESC.JPG" onclick="ImageButtonHeaderArchivedDESC_Click" />
                                                </td>
                                            </tr>
                                        </table>                                      
                                    </td>
                                    </tr>
                                    </table>                                        
                                    
                                </td>
                                <td style="width:100px; text-align: center;">
                                    Note
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td style="width: 50px; text-align: center; display: none;">
                                                DOC
                                            </td>
                                            <td style="width: 50px; text-align: center;">
                                                PDF
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                        </table>
                </HeaderTemplate>

                <ItemTemplate>
                        <asp:Panel ID="panelInItem" runat="server" >
                         <asp:HoverMenuExtender ID="HoverMenuExtenderItem" runat="server" 
                          PopupPosition="Top" PopupControlID="panelInItemX" 
                          TargetControlID="panelInItem" >
                         </asp:HoverMenuExtender>
                             <asp:Label ID="LabelPoStatus" runat="server" ></asp:Label>
                             <asp:Label ID="LabelReadyForPo" runat="server" ForeColor="Red" ></asp:Label>

                        <asp:Button ID="ButtonUpdatePrjClient" runat="server" CssClass="btn btn-mini btn-success" CommandName="UpdatePrjClient" OnClientClick = "return confirm('Are you sure you want to update this record?');"
                            CommandArgument='<%# Container.DataItemIndex %>' Text="Update Project To Client" />

                        <asp:DropDownList ID="DDLprojectToClient" runat="server" DataSourceID="SqlDataSourcePrjToClient" DataTextField="ProjectName" DataValueField="ProjectID" ></asp:DropDownList>

                            <asp:SqlDataSource ID="SqlDataSourcePrjToClient" runat="server" ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand="SELECT     TOP (100) PERCENT ProjectID, ProjectName
FROM         (SELECT     ProjectID, RTRIM(ProjectName) + N' - ' + RTRIM(CONVERT(nVarChar(10), ProjectID)) AS ProjectName
                       FROM          dbo.Table1_Project
                       UNION ALL
                       SELECT     0 AS Expr1, N'_SELECT PROJECT' AS Expr2) AS DataSource
WHERE     (ProjectID &lt;&gt; 999)
ORDER BY ProjectName"></asp:SqlDataSource>

                        <table>		
                            <tr>
                              <td>
                                <asp:Panel ID="panelInItemX" runat="server">
                                  <asp:Label ID="LabelHooverToolTipContract" runat="server" ForeColor="Gray" Font-Italic="True" Font-Size="9px" Font-Names="Consolas" BackColor="#F0F8FF"></asp:Label>
                                </asp:Panel>
                              </td>
                            </tr>	
                            <tr>
                             <td>

                             </td>
                            </tr>
                            <tr>
                                <td style="width:40px;">
                                    <div style="display:none;">
                                        <asp:Button ID="ButtonEdit" runat="server" CausesValidation="False" 
                                            CommandName="Edit" CssClass="ButtonEdit" onmouseover="this.style.cursor='hand'" 
                                            Text="Edit" Width="35px" />
                                    </div>
                                    <div style="display:none;">
                                        <asp:Button ID="ButtonDelete" runat="server" CausesValidation="False" 
                                            CommandName="Delete" CssClass="ButtonDelete" 
                                            OnClientClick="return confirm('Are you sure you want to delete this record?');" 
                                            onmouseover="this.style.cursor='hand'" Text="Delete" Width="35px" />
                                    </div>
                                </td>
                                <td style="width:20px;">
                                    <asp:Label ID="LabelSortNumber" runat="server" Font-Bold="True" 
                                        Font-Size="12px" ForeColor="#CC0000"></asp:Label>
                                </td>
                                <td style="width:80px; text-align: center;">
                                    <table style="width:80px; text-align: center;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="LabelProjectNameItem" runat="server" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LabelPOnoItem" runat="server" Text='<%# Bind("PO_No") %>' 
                                                  BackColor="#FFFF66" Visible="false"></asp:Label>

                                                <asp:HyperLink ID="HyperlinkPoNo" runat="server" 
                                                    NavigateUrl='<%# "~/invoicedefine.aspx?PoNo=" + Eval("PO_No") %>'
                                                    Text='<%# Bind("PO_No") %>' BackColor="#FFFF66" Visible="false"
                                                    Target="_blank" ></asp:HyperLink>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="LinkButtonRaisePO" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="RaisePoForFrameContract" Font-Size="9px" Visible="false"
                                                    Font-Names="Tahoma" PostBackUrl="~/webforms/pocreateNew.aspx">Raise PO</asp:LinkButton>

                                                <br /> <br />

                                                <asp:LinkButton ID="LinkButtonAddAddendum" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="AddAddendum" Font-Size="9px" 
                                                    Font-Names="Tahoma" >Add Addendum</asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="LinkButtonNumberOfAddendum" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="ShowAddendum" Font-Size="9px" ForeColor="Red" 
                                                    Font-Names="Tahoma"></asp:LinkButton>
                                                <asp:Label ID="LabelHideShow" runat="server" Text="0" Visible="false" ></asp:Label> 

                                            </td>
                                        </tr>
                                        <tr>
                                           <td>
                                                 <asp:HyperLink ID="HyperlinkAddClientData" runat="server"  
                                                 Target="_blank"  >
                                                 </asp:HyperLink>
                                           </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width:80px; text-align: center;">
                                    <asp:Label ID="LabelContractNoItem" runat="server" 
                                        Text='<%# Bind("ContractNo") %>'></asp:Label>
                                    <asp:Label ID="LabelContractIDitem" runat="server" 
                                        Text='<%# Bind("ContractID") %>' visible="false" ></asp:Label>
                                    <asp:Label ID="LabelRowIndexOnGridviewContract" runat="server"  Text="test"
                                        visible="false"></asp:Label>
                                    <asp:Label ID="LabelCreatedBy" runat="server"  Text='<%# Bind("CreatedBy") %>' 
                                        visible="false"></asp:Label>
                                    <asp:Label ID="LabelUpdatedBy" runat="server"  Text='<%# Bind("UpdatedBy") %>' 
                                        visible="false"></asp:Label>
                                    <asp:Label ID="LabelPersonCreated" runat="server"  Text='<%# Bind("PersonCreated") %>' 
                                        visible="false"></asp:Label>
                                    <asp:Label ID="LabelPersonUpdated" runat="server"  Text='<%# Bind("PersonUpdated") %>' 
                                        visible="false"></asp:Label>
                                </td>
                                <td style="width:80px; text-align: center;">
                                    <asp:Label ID="LabelContractDateItem" runat="server" 
                                        Text='<%# Bind("ContractDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                </td>
                                <td style="width:135px; text-align: center; font-weight:bolder; text-decoration: underline">
                                    <asp:Label ID="LabelSupplierName" runat="server"
                                        Text='<%# Bind("SupplierName") %>'></asp:Label>
                                    <asp:Image ID="ImagePersonOrNot" runat="server" />
                                </td>
                                <td style="width:130px; text-align: left;">
                                    <asp:Label ID="LabelContractDescItem" runat="server" 
                                        Text='<%# Bind("ContractDescription") %>'></asp:Label>
                                </td>
                                <td style="width:50px; text-align: center;">
                                    <asp:Label ID="LabelContractTypeItem" runat="server" 
                                        Text='<%# Bind("ContractType") %>'></asp:Label>
                                </td>
                                <td style="width:50px; text-align: center;">
                                    <asp:Panel ID="PanelSignBySupplier" runat="server" CssClass="hidepanel">
                                        <asp:CheckBox ID="CheckBoxSignBySupplier" runat="server" 
                                            Checked='<%# Bind("SignBySupplier") %>' Enabled="false" />
                                    </asp:Panel>
                                    <asp:Image ID="ImageSignBySupplier" runat="server" Height="18" />
                                </td>
                                <td style="width:50px; text-align: center;">
                                    <asp:Panel ID="PanelSignByMercury" runat="server" CssClass="hidepanel">
                                        <asp:CheckBox ID="CheckBoxSignByMercury" runat="server" 
                                            Checked='<%# Bind("SignByMercury") %>' Enabled="false" />
                                    </asp:Panel>
                                    <asp:Image ID="ImageSignByMercury" runat="server" Height="18" />
                                </td>
                                <td style="width:50px; text-align: center; display: none;">
                                    <asp:Panel ID="PanelCollectionBySupplier" runat="server" CssClass="hidepanel">
                                        <asp:CheckBox ID="CheckBoxCollectionBySupplier" runat="server" 
                                            Checked='<%# Bind("CollectionBySupplier") %>' Enabled="false" />
                                    </asp:Panel>
                                    <asp:Image ID="ImageCollectionBySupplier" runat="server" Height="18" />
                                </td>
                                <td style="width:100px; text-align: center;">
                                    <asp:Label ID="LabelContractGivenTo" runat="server" Text='<%# Bind("ContractGivenTo") %>'></asp:Label>
                                </td>
                                <td style="width:85px; font-size:10px; text-align:right;">
                                    <span style="font-style:italic; color:Gray;">
                                      <asp:literal ID="LiteralContractValueExcVATItem" Text="Exc. VAT" runat="server" ></asp:literal>
                                    </span>
                                    <asp:Label ID="LabelContractValueItem" runat="server" 
                                        Text='<%# Bind("ContractValue_woVAT","{0:###,###,###.00}") %>'></asp:Label>
                                    <br />

                                    <span style="font-style:italic; color:Gray;">
                                    <asp:literal ID="LiteralContractValueWithVATItem" Text="With VAT"  runat="server" ></asp:literal>
                                    </span>
                                    <asp:literal ID="LiteralContractValueWithVATItemValue" Text='<%# Bind("ContractValue_withVAT","{0:###,###,###.00}") %>'  runat="server" >
                                    </asp:literal>
                                    <br />

                                    <span style="font-style:italic; color:Gray;">
                                    <asp:literal ID="LiteralVATItem" Text="VAT %"  runat="server" ></asp:literal>
                                    </span>
                                    <asp:literal ID="LiteralVATItemValue" Text='<%# Bind("VATpercent","{0:###,###,###.00}") %>' runat="server" >
                                    </asp:literal>
                                    <br />
                                    <span 
                                        style="font-style:italic; color:Gray;">
                                    <asp:literal ID="LiteralBudgetTitle" Text="Budget With VAT"  runat="server" ></asp:literal>
                                    </span>
                                    <asp:literal ID="LiteralBudgetValue" Text='<%# Bind("Budget", "{0:###,###,###.00}")%>'  runat="server" >
                                    </asp:literal>
                                    
                                </td>
                                <td style="width:50px; text-align: center;">
                                    <asp:Panel ID="PanelCurrency" runat="server" CssClass="hidepanel">
                                        <asp:Label ID="LabelContractCurrency" runat="server" 
                                            Text='<%# Bind("ContractCurrency") %>'></asp:Label>
                                    </asp:Panel>
                                    <asp:Image ID="ImageCurrency" runat="server" />
                                </td>
                                <td style="width:50px; text-align: center;">
                                    <asp:Label ID="LabelRetention" runat="server" Text='<%# Bind("Retention") %>'></asp:Label>
                                </td>
                                <td style="width:50px; text-align: center;">
                                    <asp:Panel ID="PanelArchivedByMercury" runat="server" CssClass="hidepanel">
                                        <asp:CheckBox ID="CheckBoxArchivedByMercury" runat="server" 
                                            Checked='<%# Bind("ArchivedByMercury") %>' Enabled="false" />
                                    </asp:Panel>
                                    <asp:Image ID="ImageArchivedByMercury" runat="server" Height="18" />
                                </td>
                                <td style="width:100px; text-align: left;">
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Note") %>'></asp:Label>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td style="width: 50px; text-align: center; ">
                                                <asp:ImageButton ID="ImageButtonLinkToTemplatefile_DOC" runat="server" 
                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LinkToTemplatefile_DOC") %>' 
                                                    CommandName="OpenDOC" />
                                            </td>
                                            <td style="width: 50px; text-align: center;">
                                                <asp:ImageButton ID="ImageButtonLinkToPDFcopy" runat="server" 
                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LinkToPDFcopy") %>' 
                                                    CommandName="OpenPDF" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                        </table>

                        <table >
                          <tr>
                           <td style="width:100px;">
                             <asp:Button ID="ButtonExceptionalApprove" runat="server" CausesValidation="False" CommandName="ExceptionalApprove"  
                              OnClientClick="return confirm('Are you sure you want to approve this contract EXCEPTIONALLY?');" Visible="false"
                              CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                              onmouseover="this.style.cursor='hand'" Text="Approve Exceptionally"  />
                              <br />
                            <asp:Literal id="LiteralRequestedBy" runat="server" Text='<%# "Requested By: " + Eval("RequestedBy") %>'></asp:Literal>
                           </td>
                           <td style="width:150px; vertical-align: top;">
                          <asp:Gridview ID="GridviewMissingItemsForApproval" runat="server" 
                            CssClass="GridviewMissingBeforeApproval" DataSourceID="SqlDataSourceMissingItemsForApproval"
                            AutoGenerateColumns="False" >
                            <Columns>
                              <asp:TemplateField HeaderText="Required Items Before Approval" ItemStyle-Width="120px">
                                <ItemTemplate>
                                 <asp:Literal id="LiteralTitle" runat="server" Text='<%# "• " + Eval("Title") %>'></asp:Literal>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:Gridview>
                          <asp:SqlDataSource ID="SqlDataSourceMissingItemsForApproval" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                            SelectCommand="ContractReadyForApproval" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                              <asp:Parameter DefaultValue="0" Name="ContractID" Type="Int32" />
                              <asp:Parameter DefaultValue="1" Name="Shift" Type="Int32" />
                            </SelectParameters>
                          </asp:SqlDataSource>
                        <%------------------------------------------------------------ MISSING ITEMS FOR APPROVAL --%>
                           </td>
                           <td style="width:250px;vertical-align: top;">
                          <asp:Gridview ID="GridviewApprovalStatus" runat="server" 
                            DataSourceID="SqlDataSourceApprovalStatus" CssClass="GridviewApprovalStatus" 
                            AutoGenerateColumns="False" onrowcommand="GridviewApprovalStatus_RowCommand" 
                               onrowdatabound="GridviewApprovalStatus_RowDataBound">
                            <Columns>
                              <asp:TemplateField >
                                <ItemTemplate>
                                 <asp:Literal id="LiteralUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Literal>
                                </ItemTemplate>
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="Approved" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <%-- LiteralApproved and LiteralContractID should stay here, they are being used in RowCommand --%>
                                <asp:Literal id="LiteralApproved" runat="server" Text='<%# Bind("Approved") %>' Visible="false"></asp:Literal>
                                <asp:Literal id="LiteralContractID" runat="server" Text='<%# Bind("ContractID") %>' Visible="false"></asp:Literal>
                                <asp:Literal id="LiteralRejectContractGirls" runat="server" Text='<%# Bind("Exception") %>' Visible="false"></asp:Literal>
                                <asp:ImageButton ID="ImageButtonApproval" runat="server" CommandName="Approval" 
                                CommandArgument='<%# Container.DataItemIndex %>' 
                                 CausesValidation="False" />
                                <asp:LinkButton ID="LinkButtonRejectContractGirls" runat="server" CommandName="ApprovalByRejecting" 
                                CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="False"  />
                                </ItemTemplate>
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="When Approved">
                                <ItemTemplate>
                                 <asp:Literal id="LiteralWhenApproved" runat="server" Text='<%# Bind("WhenApproved") %>'></asp:Literal>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:Gridview>
                          <asp:SqlDataSource ID="SqlDataSourceApprovalStatus" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                            SelectCommand="ApprovalStatusContract" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                              <asp:Parameter DefaultValue="0" Name="ContractID" Type="Int32" />
                              <asp:Parameter DefaultValue="1" Name="Shift" Type="Int32" />
                            </SelectParameters>
                          </asp:SqlDataSource>
                        <%----------------------------------------------------------------- APPROVAL MATRIX --%>
                           </td>
                           <td style="width:250px;;vertical-align: top;">
                        <asp:GridView ID="GridViewOffers" runat="server" AutoGenerateColumns="False" 
                           DataKeyNames="ContractID" DataSourceID="SqlDataSourceOffers" 
                           EnableModelValidation="True"  
                           CssClass="GridviewApprovalStatus" onrowcommand="GridViewOffers_RowCommand" 
                               onrowdatabound="GridViewOffers_RowDataBound"  >
                           <Columns>
                             <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" 
                             HeaderStyle-Width="150px" ItemStyle-Width="150px"
                               SortExpression="SupplierName" />
                             <asp:BoundField DataField="OfferValueWithVAT" HeaderText="Offer Value With VAT" 
                             HeaderStyle-Width="60px" ItemStyle-Width="60px"
                             DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" 
                               SortExpression="OfferValueWithVAT" />
                             <asp:BoundField DataField="Currency" SortExpression="Currency" />
                             <asp:TemplateField>
                              <ItemTemplate>
                              <asp:ImageButton ID="ImageButtonOfferZip" runat="server" CommandName="OpenZip" 
                              CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Attachment") %>' 
                              imageUrl="~/images/zipicon.png" CausesValidation="False" />
                              </ItemTemplate>
                             </asp:TemplateField>
                           </Columns>
                         </asp:GridView>
                         <asp:SqlDataSource ID="SqlDataSourceOffers" runat="server" 
                           ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                           SelectCommand="SELECT ContractID 
                                              ,[SupplierName]
                                              ,[OfferValueWithVAT]
                                              ,[Currency]
                                              ,[Attachment]
                                          FROM [Table_Contract_Offers] 
                                          INNER JOIN Table6_Supplier 
                                          ON Table6_Supplier.SupplierID = Table_Contract_Offers.SupplierID
                                          WHERE ContractID = @ContractID">
                           <SelectParameters>
                             <asp:Parameter DefaultValue="0" Name="ContractID" Type="Int32" />
                           </SelectParameters>
                         </asp:SqlDataSource>

                        <%----------------------------------------------------------------- COMMERCIAL OFFERS --%>
                           </td>
                           <td style="padding-left:10px;">
                            <asp:Panel ID="PanelNominated" runat="server" >
                            <asp:Label ID="LabelESTM_Approval" runat="server" Font-Size="12px"
                                 Text="" ForeColor="#FF3399" Width="300px">
                            </asp:Label>
                                <br />
                            <asp:DropDownList ID="DDLestm" runat="server" CssClass="DrpDwnListGeneral"
                                SelectedValue='<%# Bind("NominatedApprovedByESTM")%>' AutoPostBack="true" OnSelectedIndexChanged="DDLestm_SelectedIndexChanged">
                                <asp:ListItem Value="0">No Response Yet</asp:ListItem>
                                <asp:ListItem Value="1">Confirmed</asp:ListItem>
                                <asp:ListItem Value="2">Not Confirmed</asp:ListItem>
                            </asp:DropDownList>
                                <hr />
                             <span Class="PanelNominated" >
                             Nominated Subcontractor, <br /> No Commercial Offer Required
                             </span>
                            </asp:Panel>

                            <asp:Panel ID="PanelFrameContract" runat="server" >
                             <span Class="PanelFrameContract" >
                             Frame Contract
                             </span>
                            </asp:Panel>
                           </td>
                          </tr>
                        </table>


      <!---- COMMERCIAL TERMS ---------------------------------------------------------->

      <asp:Panel ID="PanelCommercialTermsContractItem" runat="server" CssClass="PanelCommercialTerms" >
      <table >
       <tr>
        <td colspan="12" class="CommercialTermsHeading">
         Commercial Terms
        </td>
       </tr>
       <tr>
        <td class="CommercialTitles">
         Start Date:
        </td>
        <td style="padding:2px;">
          <asp:literal ID="LiteralStartDate" Text='<%# Bind("StartDate","{0:dd/MM/yyyy}") %>' runat="server" ></asp:literal>
        </td>
        <td class="CommercialTitles">
            Penalty To Mercury:
        </td>
        <td style="padding:2px; width:100px;">
         <asp:literal ID="LiteralPenalties" Text='<%# Bind("Penalties") %>' runat="server" ></asp:literal>
        </td>
        <td rowspan="2" class="CommercialTitles">
            Penalty To Mercury&nbsp; Note
        </td>
        <td class="CommercialColumn" rowspan="2">
            <asp:literal ID="LiteralPenaltyNote" Text='<%# Bind("PenaltiesNote")%>' runat="server" ></asp:literal>
        </td>
           <td class="CommercialTitles" rowspan="2">Penalty To Supplier Note</td>
           <td class="CommercialColumn" rowspan="2">
               <asp:Literal ID="LiteralPenaltyNoteToSupplier" runat="server" Text='<%# Bind("PenaltiesToSupplierNote")%>'></asp:Literal>
           </td>
           <td class="CommercialTitles" rowspan="2">Delivery Terms: </td>
        <td rowspan="2" class="CommercialColumn">
         <asp:literal ID="LiteralDeliveryTerms" Text='<%# Bind("DeliveryTerms") %>' runat="server" ></asp:literal>
        </td>
        <td rowspan="2" class="CommercialTitles">
         Guarantee Period:
        </td>
        <td rowspan="2" class="CommercialColumn">
         <asp:literal ID="LiteralGuaranteePeriod" Text='<%# Bind("GuaranteePeriod") %>' runat="server" ></asp:literal>
        </td>
       </tr>
       <tr>
        <td class="CommercialTitles">
         Finish Date:
        </td>
        <td style="padding:2px;">
         <asp:literal ID="LiteralFinishDate" Text='<%# Bind("FinishDate","{0:dd/MM/yyyy}") %>' runat="server" ></asp:literal>
        </td>
        <td class="CommercialTitles">
            Penalty To Supplier:</td>
        <td style="padding:2px; width:100px;">
            <asp:Literal ID="LiteralPenaltiesToSupplier" runat="server" Text='<%# Bind("PenaltiesToSupplier")%>'></asp:Literal>
        </td>
       </tr>
          <tr>
              <td class="CommercialTermsHeading" colspan="12">Payment Terms</td>
          </tr>
          <tr>
              <td class="CommercialTitles">Advance: </td>
              <td style="padding:2px;">
                  <asp:Literal ID="LiteralAdvance" runat="server" Text='<%# Bind("Advance", "{0:###,###,###.0} %")%>' ></asp:Literal>
              </td>
              <td class="CommercialTitles">Interim:</td>
              <td style="padding:2px;">
                  <asp:Literal ID="LiteralInterim" runat="server" Text='<%# Bind("Interim", "{0:###,###,###.0} %")%>' ></asp:Literal>
              </td>
              <td class="CommercialTitles">Shipment:</td>
              <td style="padding:2px;">
                  <asp:Literal ID="LiteralShipment" runat="server" Text='<%# Bind("Shipment", "{0:###,###,###.0} %")%>' ></asp:Literal>
              </td>
              <td class="CommercialTitles">Delivery:</td>
              <td style="padding:2px;">
                  <asp:Literal ID="LiteralDelivery" runat="server" Text='<%# Bind("Delivery", "{0:###,###,###.0} %")%>' ></asp:Literal>
              </td>
              <td class="CommercialTitles">Retention:</td>
              <td style="padding:2px;">
                  <asp:Literal ID="LiteralRetention" runat="server" Text='<%# Bind("Retention", "{0:###,###,###.0} %")%>' ></asp:Literal>
              </td>
              <td >

              </td>
              <td >

              </td>
          </tr>
      </table>
      </asp:Panel>
      <!---------------------------------------------------------- COMMERCIAL TERMS ---->
                       
    <asp:GridView ID="GridViewShowAddendum" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSourceShowAddendum" EnableModelValidation="True" 
        CssClass="Grid"  DataKeyNames="AddendumID" onrowdatabound="GridViewShowAddendum_RowDataBound" 
                            onrowupdating="GridViewShowAddendum_RowUpdating" 
                            onrowcommand="GridViewShowAddendum_RowCommand" 
                            onrowdeleting="GridViewShowAddendum_RowDeleting" 
                            onrowupdated="GridViewShowAddendum_RowUpdated" 
                            onrowdeleted="GridViewShowAddendum_RowDeleted" 
                            onrowcreated="GridViewShowAddendum_RowCreated" >
        <Columns>
            <asp:templatefield>

                <ItemTemplate>
                        <asp:Panel ID="panelInItemAddendum" runat="server" >
                         <asp:HoverMenuExtender ID="HoverMenuExtenderItem" runat="server" 
                          PopupPosition="Top" PopupControlID="panelInItemXAddendum" 
                          TargetControlID="panelInItemAddendum" >
                         </asp:HoverMenuExtender>
                             <asp:Label ID="LabelPoStatus" runat="server" ></asp:Label>
                        <table >		
                            <tr>
                              <td>
                                <asp:Panel ID="panelInItemXAddendum" runat="server">
                                   <asp:Label ID="LabelHooverToolTipAddendum" runat="server" ForeColor="Gray" Font-Italic="True" Font-Size="9px" Font-Names="Consolas" BackColor="#F0F8FF"></asp:Label>
                                </asp:Panel>
                              </td>
                            </tr>	
                            <tr>
                             <td>

                             </td>
                            </tr>
                            <tr>
                              <td style="width:40px;">
                                <div style="display:none;">
                                  <asp:Button ID="ButtonEdit" runat="server" CausesValidation="False" 
                                    CommandName="Edit" CssClass="ButtonEditAddendum" 
                                    onmouseover="this.style.cursor='hand'" Text="Edit" Width="35px" />
                                </div>
                                <div style="display:none;">
                                  <asp:Button ID="ButtonDelete" runat="server" CausesValidation="False" 
                                    CommandName="Delete" CssClass="ButtonDeleteAddendum" 
                                    OnClientClick="return confirm('Are you sure you want to delete this record?');" 
                                    onmouseover="this.style.cursor='hand'" Text="Delete" Width="35px" />
                                </div>
                              </td>
                              <td >
                                <asp:Label ID="LabelSortNumber" runat="server" class="AddendumColumn"></asp:Label>
                              <br />
                             <asp:Button ID="ButtonExceptionalApprove" runat="server" CausesValidation="False" CommandName="ExceptionalApprove"  
                              OnClientClick="return confirm('Are you sure you want to approve this contract EXCEPTIONALLY?');" Visible="false"
                              CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" Width="50px" Height="40px" CssClass="wrap"
                              onmouseover="this.style.cursor='hand'" Text="Exceptional Approve"  />

                              </td>
                              <td style="width:80px; text-align: center;">
                                <asp:Label ID="LabelPOnoItem" runat="server" BackColor="#FFFF66" 
                                  Text='<%# Bind("PO_No") %>' Visible="false"></asp:Label>

                              <asp:HyperLink ID="HyperlinkPoNo" runat="server" 
                              NavigateUrl='<%# "~/invoicedefine.aspx?PoNo=" + Eval("PO_No") %>'
                              Text='<%# Bind("PO_No") %>' BackColor="#FFFF66" Visible="false"
                              Target="_blank" ></asp:HyperLink>

                                <br />
                                <asp:HyperLink ID="HyperlinkAddAddendumData" runat="server" Target="_blank">
                              </asp:HyperLink>
                              </td>
                              <td style="width:80px; text-align: center;">
                                <asp:Label ID="LabelAddendumNoItem" runat="server" 
                                  Text='<%# Bind("AddendumNo") %>'></asp:Label>
                                <asp:Label ID="LabelAddendumIDitem" runat="server" 
                                  Text='<%# Bind("AddendumID") %>' visible="false"></asp:Label>
                                <asp:Label ID="LabelCreatedBy" runat="server" Text='<%# Bind("CreatedBy") %>' 
                                  visible="false"></asp:Label>
                                <asp:Label ID="LabelUpdatedBy" runat="server" Text='<%# Bind("UpdatedBy") %>' 
                                  visible="false"></asp:Label>
                                <asp:Label ID="LabelPersonCreated" runat="server" 
                                  Text='<%# Bind("PersonCreated") %>' visible="false"></asp:Label>
                                <asp:Label ID="LabelPersonUpdated" runat="server" 
                                  Text='<%# Bind("PersonUpdated") %>' visible="false"></asp:Label>
                              </td>
                              <td style="width:80px; text-align: center;">
                                <asp:Label ID="LabelAddendumDateItem" runat="server" 
                                  Text='<%# Bind("AddendumDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                              </td>
                              <td style="width:135px; text-align: left;">
                              </td>
                              <td style="width:130px; text-align: left;">
                                <asp:Label ID="LabelAddendumDescItem" runat="server" 
                                  Text='<%# Bind("AddendumDescription") %>'></asp:Label>
                              </td>
                              <td style="width:50px; text-align: center;">
                              </td>
                              <td style="width:50px; text-align: center;">
                                <asp:Panel ID="PanelSignBySupplier" runat="server" CssClass="hidepanel">
                                  <asp:CheckBox ID="CheckBoxSignBySupplier" runat="server" 
                                    Checked='<%# Bind("AddendumSignBySupplier") %>' Enabled="false" />
                                </asp:Panel>
                                <asp:Image ID="ImageSignBySupplier" runat="server" Height="18" />
                              </td>
                              <td style="width:50px; text-align: center;">
                                <asp:Panel ID="PanelSignByMercury" runat="server" CssClass="hidepanel">
                                  <asp:CheckBox ID="CheckBoxSignByMercury" runat="server" 
                                    Checked='<%# Bind("AddendumSignByMercury") %>' Enabled="false" />
                                </asp:Panel>
                                <asp:Image ID="ImageSignByMercury" runat="server" Height="18" />
                              </td>
                              <td style="width:50px; text-align: center; display: none;">
                                <asp:Panel ID="PanelCollectionBySupplier" runat="server" CssClass="hidepanel">
                                  <asp:CheckBox ID="CheckBoxCollectionBySupplier" runat="server" 
                                    Checked='<%# Bind("AddendumCollectionBySupplier") %>' Enabled="false" />
                                </asp:Panel>
                                <asp:Image ID="ImageCollectionBySupplier" runat="server" Height="18" />
                              </td>
                              <td style="width:100px; text-align: center;">
                                <asp:Label ID="LabelAddendumGivenTo" runat="server" 
                                  Text='<%# Bind("AddendumGivenTo") %>'></asp:Label>
                              </td>
                              <td style="width:85px; text-align: right;">
                                <span style="font-style:italic; color:Gray;">
                                 <asp:literal ID="LiteralAddendumValueExcVATItem" Text="Exc. VAT"  runat="server" ></asp:literal>
                                </span>
                                <asp:Label ID="LabelContractValueItem" runat="server" 
                                  Text='<%# Bind("AddendumValue_woVAT","{0:###,###,###.00}") %>'></asp:Label>
                                    <br />

                                <span style="font-style:italic; color:Gray;">
                                 <asp:literal ID="LiteralAddendumValueWithVATItem" Text="With VAT"  runat="server" ></asp:literal>
                                </span>
                                <asp:literal ID="LiteralAddendumValueWithVATItemValue" Text='<%# Bind("AddendumValue_withVAT","{0:###,###,###.00}") %>'  runat="server" >
                                </asp:literal>
                                <br />

                                <span style="font-style:italic; color:Gray;">
                                 <asp:literal ID="LiteralVATItem" Text="VAT %"  runat="server" ></asp:literal>
                                </span>
                                <asp:literal ID="LiteralVATItemValue" Text='<%# Bind("VATpercent","{0:###,###,###.00}") %>' runat="server" >
                                </asp:literal>

                                    <br />
                                    <span 
                                        style="font-style:italic; color:Gray;">
                                    <asp:literal ID="LiteralBudgetTitle" Text="Budget With VAT"  runat="server" ></asp:literal>
                                    </span>
                                    <asp:literal ID="LiteralBudgetValue" Text='<%# Bind("Budget", "{0:###,###,###.00}")%>'  runat="server" >
                                    </asp:literal>
                                  
                              </td>
                              <td style="width:50px; text-align: center;">
                              </td>
                              <td style="width:50px; text-align: center;">
                                <asp:Label ID="LabelRetention" runat="server" 
                                  Text='<%# Bind("AddendumRetention") %>'></asp:Label>
                              </td>
                              <td style="width:50px; text-align: center;">
                                <asp:Panel ID="PanelArchivedByMercury" runat="server" CssClass="hidepanel">
                                  <asp:CheckBox ID="CheckBoxArchivedByMercury" runat="server" 
                                    Checked='<%# Bind("AddendumArchivedByMercury") %>' Enabled="false" />
                                </asp:Panel>
                                <asp:Image ID="ImageArchivedByMercury" runat="server" Height="18" />
                              </td>
                              <td style="width:100px; text-align: left;">
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("AddendumNote") %>'></asp:Label>
                              </td>
                              <td>
                                <table>
                                  <tr>
                                    <td style="width: 50px; text-align: center; ">
                                      <asp:Label ID="LabelLinkToTemplatefile_DOC" runat="server" CssClass="hidepanel" 
                                        Text='<%# Bind("AddendumLinkToTemplatefile_DOC") %>'></asp:Label>
                                      <asp:ImageButton ID="ImageButtonLinkToTemplatefile_DOC" runat="server" 
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AddendumLinkToTemplatefile_DOC") %>' 
                                        CommandName="OpenDOC" />
                                    </td>
                                    <td style="width: 50px; text-align: center;">
                                      <asp:ImageButton ID="ImageButtonLinkToPDFcopy" runat="server" 
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AddendumLinkToPDFcopy") %>' 
                                        CommandName="OpenPDF" />
                                    </td>
                                  </tr>
                                </table>
                              </td>
                            </tr>

                        </table>

                        <table>
                         <tr>
                           <td style="padding-left: 150px; vertical-align: top;">
                          <asp:Gridview ID="GridviewMissingItemsForApproval" runat="server" 
                            CssClass="GridviewMissingBeforeApproval" DataSourceID="SqlDataSourceMissingItemsForApproval"
                            AutoGenerateColumns="False" >
                            <Columns>
                              <asp:TemplateField HeaderText="Required Items Before Approval" ItemStyle-Width="120px">
                                <ItemTemplate>
                                 <asp:Literal id="LiteralTitle" runat="server" Text='<%# "• " + Eval("Title") %>'></asp:Literal>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:Gridview>
                          <asp:SqlDataSource ID="SqlDataSourceMissingItemsForApproval" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                            SelectCommand="AddendumReadyForApproval" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                              <asp:Parameter DefaultValue="0" Name="AddendumID" Type="Int32" />
                              <asp:Parameter DefaultValue="1" Name="Shift" Type="Int32" />
                            </SelectParameters>
                          </asp:SqlDataSource>
                        <%------------------------------------------------------------ MISSING ITEMS FOR APPROVAL --%>
                           </td>
                           <td style="padding-left: 50px; vertical-align: top;">
                          <asp:Gridview ID="GridviewApprovalStatusAddendum" runat="server" 
                            DataSourceID="SqlDataSourceApprovalStatus" CssClass="GridviewApprovalStatus" 
                            AutoGenerateColumns="False" onrowcommand="GridviewApprovalStatusAddendum_RowCommand" 
                               onrowdatabound="GridviewApprovalStatusAddendum_RowDataBound">
                            <Columns>
                              <asp:TemplateField >
                                <ItemTemplate>
                                 <asp:Literal id="LiteralUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Literal>
                                </ItemTemplate>
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="Approved" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <%-- LiteralApproved and LiteralContractID should stay here, they are being used in RowCommand --%>
                                <asp:Literal id="LiteralApproved" runat="server" Text='<%# Bind("Approved") %>' Visible="false"></asp:Literal>
                                <asp:Literal id="LiteralAddendumID" runat="server" Text='<%# Bind("AddendumID") %>' Visible="false"></asp:Literal>
                                <asp:Literal id="LiteralRejectContractGirls" runat="server" Text='<%# Bind("Exception") %>' Visible="false"></asp:Literal>
                                <asp:ImageButton ID="ImageButtonApproval" runat="server" CommandName="Approval" 
                                CommandArgument='<%# Container.DataItemIndex %>' 
                                 CausesValidation="False" />
                                <asp:LinkButton ID="LinkButtonRejectContractGirls" runat="server" CommandName="ApprovalByRejecting" 
                                CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="False"  />
                                </ItemTemplate>
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="When Approved">
                                <ItemTemplate>
                                 <asp:Literal id="LiteralWhenApproved" runat="server" Text='<%# Bind("WhenApproved") %>'></asp:Literal>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:Gridview>
                          <asp:SqlDataSource ID="SqlDataSourceApprovalStatus" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                            SelectCommand="ApprovalStatusAddendum" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                              <asp:Parameter DefaultValue="0" Name="AddendumID" Type="Int32" />
                              <asp:Parameter DefaultValue="1" Name="Shift" Type="Int32" />
                            </SelectParameters>
                          </asp:SqlDataSource>
                        <%----------------------------------------------------------------- APPROVAL MATRIX --%>
                           </td>
                           <td style="padding-left:10px;">
                            <asp:Panel ID="PanelAddendumType" runat="server" Class="PanelAddendumType">
                              <asp:literal ID="LiteralAddendumType" runat="server" ></asp:literal>
                            </asp:Panel>
                           </td>
                         </tr>
                        </table>
                    </asp:Panel>                 

      <!---- COMMERCIAL TERMS ---------------------------------------------------------->
      <asp:Panel ID="PanelCommercialTermsAddendumItem" runat="server" CssClass="PanelCommercialTerms" >
      <table >
       <tr>
        <td colspan="12" class="CommercialTermsHeading">
         Commercial Terms
        </td>
       </tr>
       <tr>
        <td class="CommercialTitles">
         Start Date:
        </td>
        <td style="padding:2px;">
          <asp:literal ID="LiteralStartDate" Text='<%# Bind("StartDate","{0:dd/MM/yyyy}") %>' runat="server" ></asp:literal>
        </td>
        <td class="CommercialTitles">
            Penalty To Mercury:
        </td>
        <td style="padding:2px; width:100px;">
         <asp:literal ID="LiteralPenalties" Text='<%# Bind("Penalties") %>' runat="server" ></asp:literal>
        </td>
        <td rowspan="2" class="CommercialTitles">
            Penalty To Mercury&nbsp; Note
        </td>
        <td class="CommercialColumn" rowspan="2">
            <asp:literal ID="LiteralPenaltyNote" Text='<%# Bind("PenaltiesNote")%>' runat="server" ></asp:literal>
        </td>
           <td class="CommercialTitles" rowspan="2">Penalty To Supplier Note</td>
           <td class="CommercialColumn" rowspan="2">
               <asp:Literal ID="LiteralPenaltyNoteToSupplier" runat="server" Text='<%# Bind("PenaltiesToSupplierNote")%>'></asp:Literal>
           </td>
           <td class="CommercialTitles" rowspan="2">Delivery Terms: </td>
        <td rowspan="2" class="CommercialColumn">
         <asp:literal ID="LiteralDeliveryTerms" Text='<%# Bind("DeliveryTerms") %>' runat="server" ></asp:literal>
        </td>
        <td rowspan="2" class="CommercialTitles">
         Guarantee Period:
        </td>
        <td rowspan="2" class="CommercialColumn">
         <asp:literal ID="LiteralGuaranteePeriod" Text='<%# Bind("GuaranteePeriod") %>' runat="server" ></asp:literal>
        </td>
       </tr>
       <tr>
        <td class="CommercialTitles">
         Finish Date:
        </td>
        <td style="padding:2px;">
         <asp:literal ID="LiteralFinishDate" Text='<%# Bind("FinishDate","{0:dd/MM/yyyy}") %>' runat="server" ></asp:literal>
        </td>
        <td class="CommercialTitles">
            Penalty To Supplier:</td>
        <td style="padding:2px; width:100px;">
            <asp:Literal ID="LiteralPenaltiesToSupplier" runat="server" Text='<%# Bind("PenaltiesToSupplier")%>'></asp:Literal>
        </td>
       </tr>
          <tr>
              <td class="CommercialTermsHeading" colspan="12">Payment Terms</td>
          </tr>
          <tr>
              <td class="CommercialTitles">Advance: </td>
              <td style="padding:2px;">
                  <asp:Literal ID="LiteralAdvance" runat="server" Text='<%# Bind("Advance", "{0:###,###,###.0} %")%>'></asp:Literal>
              </td>
              <td class="CommercialTitles">Interim:</td>
              <td style="padding:2px;">
                  <asp:Literal ID="LiteralInterim" runat="server" Text='<%# Bind("Interim", "{0:###,###,###.0} %")%>'></asp:Literal>
              </td>
              <td class="CommercialTitles">Shipment:</td>
              <td style="padding:2px;">
                  <asp:Literal ID="LiteralShipment" runat="server" Text='<%# Bind("Shipment", "{0:###,###,###.0} %")%>'></asp:Literal>
              </td>
              <td class="CommercialTitles">Delivery:</td>
              <td style="padding:2px;">
                  <asp:Literal ID="LiteralDelivery" runat="server" Text='<%# Bind("Delivery", "{0:###,###,###.0} %")%>'></asp:Literal>
              </td>
              <td class="CommercialTitles">Retention:</td>
              <td style="padding:2px;">
                  <asp:Literal ID="LiteralRetention" runat="server" Text='<%# Bind("AddendumRetention", "{0:###,###,###.0} %")%>'></asp:Literal>
              </td>
              <td >

              </td>
              <td >

              </td>
          </tr>

      </table>
      </asp:Panel>

      <!---------------------------------------------------------- COMMERCIAL TERMS ---->

                </ItemTemplate>

                <EditItemTemplate>
                        <div style="text-align: center"><asp:Label ID="LabelScenarioOutOfRange" runat="server" Text=""  ForeColor="#FF0066" Font-Bold="True" Font-Size="12px" /></div>
                            <asp:Label ID="LabelPoNoBeforeEdit" runat="server" CssClass="hidepanel" ></asp:Label>
                            <asp:DropDownList ID="DropDownListPOnoEdit" runat="server" DataSourceID="SqlDataSourcePoNo" DataTextField="Information" 
                              DataValueField="PO_No" Font-Names="Consolas" Font-Size="11px" Width="1120px" BackColor="#FFFF66">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourcePoNo" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
SelectCommand="SELECT N'' As PO_No, 'Select Po No' as Information

UNION ALL

SELECT     dbo.Table2_PONo.PO_No, REPLACE(RTRIM(dbo.Table2_PONo.PO_No) 
                      + N' ' + RIGHT('00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000' + CAST(dbo.Table2_PONo.Description
                       AS varchar(140)), 140) + N' &gt; ' + CASE LEN(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT))) WHEN 7 THEN LEFT(rtrim(CONVERT(nchar(20), 
                      dbo.View_PoSumExcVAT.PoSumExcVAT)), 1) + ',' + RIGHT(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 6) 
                      WHEN 8 THEN LEFT(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 2) + ',' + RIGHT(rtrim(CONVERT(nchar(20), 
                      dbo.View_PoSumExcVAT.PoSumExcVAT)), 6) WHEN 9 THEN LEFT(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 3) 
                      + ',' + RIGHT(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 6) WHEN 10 THEN LEFT(rtrim(CONVERT(nchar(20), 
                      dbo.View_PoSumExcVAT.PoSumExcVAT)), 1) + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 2, 3) 
                      + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 5, 3) + '.' + + RIGHT(rtrim(CONVERT(nchar(20), 
                      dbo.View_PoSumExcVAT.PoSumExcVAT)), 2) WHEN 11 THEN LEFT(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 2) 
                      + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 3, 3) + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), 
                      dbo.View_PoSumExcVAT.PoSumExcVAT)), 6, 3) + '.' + + RIGHT(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 2) 
                      WHEN 12 THEN LEFT(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 3) + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), 
                      dbo.View_PoSumExcVAT.PoSumExcVAT)), 4, 3) + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 7, 3) 
                      + '.' + + RIGHT(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 2) ELSE rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)) 
                      END + N' ' + RTRIM(dbo.View_PoSumExcVAT.PO_Currency) + N' exc.VAT', ' ', '_') AS Information
FROM         dbo.Table2_PONo INNER JOIN
                      dbo.Table1_Project ON dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID INNER JOIN
                      dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID INNER JOIN
                      dbo.View_PoSumExcVAT ON dbo.Table2_PONo.PO_No = dbo.View_PoSumExcVAT.PO_No INNER JOIN
                      dbo.Table_Contracts ON dbo.Table6_Supplier.SupplierID = dbo.Table_Contracts.SupplierID INNER JOIN
                      dbo.Table_Addendums ON dbo.Table_Contracts.ContractID = dbo.Table_Addendums.ContractID
WHERE     (dbo.Table2_PONo.Project_ID = @Project_ID) AND (dbo.Table_Addendums.AddendumID = @AddendumID)">
<SelectParameters>
 <asp:Parameter Name="Project_ID" Type="Int32" />
 <asp:Parameter Name="AddendumID" Type="Int32"/>
</SelectParameters>
</asp:SqlDataSource>
<asp:Label ID="LabelWarningPoDublication" runat="server"></asp:Label>

                        <table style="background-color: #E6E6FA">
                            <tr>
                                <td style="width:40px;">
                                    <div>
                                        <asp:Button ID="ButtonUpdate" runat="server" CausesValidation="True" CommandName="Update" CssClass="ButtonUpdateAddendum" onmouseover="this.style.cursor='hand'" Text="Update" Width="35px" />
                                    </div>
                                    <div>
                                        <asp:Button ID="ButtonCancel" runat="server" CausesValidation="False" CommandName="Cancel" CssClass="ButtonCancelAddendum" onmouseover="this.style.cursor='hand'" Text="Cancel" Width="35px" />
                                    </div>
                                </td>
                                <td style="width:20px;"></td>
                                <td style="width:80px; text-align: center;">
                                    <div>
                                        <asp:DropDownList ID="DDLContractID" runat="server" AppendDataBoundItems="True" CssClass="EditContract" DataSourceID="SqlDataSourceContractID" DataTextField="ContractNo" DataValueField="ContractID" Font-Size="9px" Height="20px" selectedvalue='<%# Bind("ContractID") %>' width="75px">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSourceContractID" runat="server" ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand="SELECT     dbo.Table_Contracts.ContractID, RTRIM(dbo.Table_Contracts.ContractNo) AS ContractNo,
                                                                                     dbo.Table1_Project.ProjectID
                                                                                    FROM         dbo.Table_Contracts INNER JOIN
                                                                                    dbo.Table1_Project ON dbo.Table_Contracts.ProjectID = dbo.Table1_Project.ProjectID
                                                                                    WHERE     (dbo.Table1_Project.ProjectID = @ProjectID)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="DropDownListPrjID" Name="ProjectID" PropertyName="SelectedValue" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </td>
                                <td style="width:80px; text-align: center;">

                                   <asp:Panel ID="HideFromUser" runat="server" CssClass="ContractEditTitles">
                                    <asp:Literal ID="LiteralAddenumID" runat="server" Text='<%# Bind("AddendumID") %>'></asp:Literal>
                                    <asp:Literal ID="LiteralPOexecuted" runat="server" Text='<%# Bind("POexecuted") %>'></asp:Literal>
                                   </asp:Panel>

                                    <asp:Label ID="LabelAddendumNoEdit" runat="server" CssClass="ContractEditTitles"  >No</asp:Label>
                                    <br />

                                    <asp:TextBox ID="TextBoxAddendumNoEdit" runat="server" CssClass="EditContract" Height="40" Text='<%# Bind("AddendumNo") %>' width="75px"></asp:TextBox>
                                    <asp:Label ID="LabelScenario" runat="server" CssClass="hidepanel" Text='<%# Bind("Scenario") %>'></asp:Label>
                                </td>
                                <td style="width:80px; text-align: center;">
                                    <asp:Label ID="LabelDateEdit" runat="server" CssClass="ContractEditTitles"  >Date</asp:Label>
                                    <br />

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorContractDate" runat="server" ControlToValidate="TextBoxAddendumDateEdit" Display="Dynamic" ErrorMessage="dd/mm/yyyy" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="TextBoxAddendumDateEdit" runat="server" CssClass="EditContract" Text='<%# Bind("AddendumDate","{0:dd/MM/yyyy}")%>' width="75px"></asp:TextBox>
                                    <asp:CalendarExtender ID="TextBox7_CalendarExtender" runat="server" CssClass="cal_Theme1" format="dd/MM/yyyy" TargetControlID="TextBoxAddendumDateEdit">
                                    </asp:CalendarExtender>
                                </td>
                                <td style="width:135px; text-align: left;"></td>
                                <td style="width:130px; text-align: center;">
                                    <asp:Label ID="LabelDescriptionEdit" runat="server" CssClass="ContractEditTitles"  >Description</asp:Label>
                                    <br />

                                    <asp:TextBox ID="TextBoxAddendumDescriptionEdit" runat="server" CssClass="EditContract" Height="40" Text='<%# Bind("AddendumDescription") %>' TextMode="MultiLine" width="125px"></asp:TextBox>
                                </td>
                                <td style="width:50px; text-align: center;"></td>
                                <td style="width:50px; text-align: center;">
                                    <asp:Label ID="LabelSignSup" runat="server" CssClass="ContractEditTitles"  >Sign Sup.</asp:Label>
                                    <br />

                                    <asp:CheckBox ID="SignBySupplierCheckBoxEdit" runat="server" Checked='<%# Bind("AddendumSignBySupplier") %>' />
                                </td>
                                <td style="width:50px; text-align: center;">
                                    <asp:Label ID="LabelSignMercEdit" runat="server" CssClass="ContractEditTitles"  >Sign Merc.</asp:Label>
                                    <br />

                                    <asp:CheckBox ID="SignByMercuryCheckBoxEdit" runat="server" Checked='<%# Bind("AddendumSignByMercury") %>' />
                                </td>
                                <td style="width:50px; text-align: center; display: none;">
                                    <asp:CheckBox ID="CollectionBySupplierCheckBoxEdit" runat="server" Checked='<%# Bind("AddendumCollectionBySupplier") %>' />
                                </td>
                                <td style="width:100px; text-align: center;">
                                    <asp:Label ID="LabelGivenToEdit" runat="server" CssClass="ContractEditTitles"  >Given To</asp:Label>
                                    <br />

                                    <asp:TextBox ID="TextBoxAddendumGivenTo" runat="server" CssClass="EditContract" Height="40px" Text='<%# Bind("AddendumGivenTo") %>' TextMode="MultiLine" width="75px"></asp:TextBox>
                                </td>
                                <td style="width:85px; text-align: center;">
                                    <span class="ContractEditTitles"><asp:Literal ID="LiteralLiteralAddendumValueExcVAT" runat="server" Text="Exc. VAT"></asp:Literal></span>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorContractValue" runat="server" ControlToValidate="TextBoxAddendumValueEdit" Display="Dynamic" ErrorMessage="Wrong format" ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="TextBoxAddendumValueEdit" runat="server" CssClass="EditContract" Text='<%# Bind("AddendumValue_woVAT") %>' width="75px"></asp:TextBox>
                                    <br />
                                    <span class="ContractEditTitles"><asp:Literal ID="LiteralAddendumValueWithVAT" runat="server" Text="With VAT"></asp:Literal></span>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorAddendumValueWithVATEdit" runat="server" ControlToValidate="TextBoxAddendumValueWithVATEdit" Display="Dynamic" ErrorMessage="Wrong format" ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddendumValueWithVATEdit" runat="server" ControlToValidate="TextBoxAddendumValueWithVATEdit" Display="Dynamic" ErrorMessage="Required">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="TextBoxAddendumValueWithVATEdit" runat="server" CssClass="EditContract" Text='<%# Bind("AddendumValue_WithVAT") %>' width="75px"></asp:TextBox>
                                    <br />
                                    <span class="ContractEditTitles"><asp:Literal ID="LiteralVAT" runat="server" Text="VAT %"></asp:Literal></span>
                                    <asp:FilteredTextBoxExtender ID="TextBoxVATpercent_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="TextBoxVAT">
                                    </asp:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxVAT" runat="server" ControlToValidate="TextBoxVAT" Display="Dynamic" ErrorMessage="Required">
                                    </asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidatorTextBoxVAT" runat="server" ControlToValidate="TextBoxVAT" Display="Dynamic" ErrorMessage="range to be 0-20" MaximumValue="20" MinimumValue="0" Type="Double">
                                    </asp:RangeValidator>
                                    <asp:TextBox ID="TextBoxVAT" runat="server" CssClass="EditContract" Text='<%# Bind("VATpercent") %>' width="75px">
                                    </asp:TextBox>

                                    <br />
                                    <span class="ContractEditTitles"><asp:literal ID="LiteralBudgetEdit" Text="Budget With VAT" runat="server" ></asp:literal></span>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorBudgetEdit" 
                                        runat="server" ControlToValidate="TextBoxBudget" Display="Dynamic" 
                                        ErrorMessage="Wrong format" 
                                        ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="TextBoxBudget" runat="server" 
                                        CssClass="EditContract" Text='<%# Bind("Budget")%>' width="75px"></asp:TextBox>

                                </td>
                                <td style="width:50px; text-align: center;"></td>
                                <td style="width:20px; text-align: center;">
                                    <asp:Label ID="LabelRetentionEdit" runat="server" CssClass="ContractEditTitles"  >Retention</asp:Label>
                                    <br />
                                  <asp:TextBox ID="TextBoxRetention" runat="server" CssClass="EditContract" Text='<%# Bind("AddendumRetention")%>' width="50px" />
                                  <asp:RangeValidator ID="RangeValidatorRetention" runat="server" ControlToValidate="TextBoxRetention" 
                                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" 
                                      MinimumValue="0" Type="Double">
                                  </asp:RangeValidator>
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorRetention" runat="server" ControlToValidate="TextBoxRetention" 
                                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="not valid number" 
                                      ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                                  </asp:RegularExpressionValidator>


                                </td>
                                <td style="width:50px; text-align: center;">
                                    <asp:Label ID="LabelArchievedEdit" runat="server" CssClass="ContractEditTitles"  >Archived</asp:Label>
                                    <br />

                                    <asp:CheckBox ID="ArchivedByMercuryCheckBoxEdit" runat="server" Checked='<%# Bind("AddendumArchivedByMercury") %>' />
                                </td>
                                <td style="width:100px; text-align: center;">
                                    <asp:Label ID="LabelNoteEdit" runat="server" CssClass="ContractEditTitles"  >Note</asp:Label>
                                    <br />

                                    <asp:TextBox ID="TextBoxNoteEdit" runat="server" CssClass="EditContract" Height="40" Text='<%# Bind("AddendumNote") %>' TextMode="MultiLine" width="115px"></asp:TextBox>
                                </td>
                                <td>
                                    <div>
                                        <asp:Label ID="labelInfoForAttachments" runat="server" CssClass="LabelGeneral"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="LabelDocFileEdit" runat="server" Text="Template DOC File" Width="100px" CssClass="ContractEditTitles" />
                                    </div>
                                    <div>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LabelDeleteDOC" runat="server" ForeColor="#3366FF" Text="Delete DOC?" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="CheckBoxDeleteDOC" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="LinkToTemplatefile_DOCTextBoxEdit" runat="server" CssClass="hidepanel" Text='<%# Bind("AddendumLinkToTemplatefile_DOC") %>' />
                                        <asp:FileUpload ID="FileUploadDOCEdit" runat="server" CssClass="EditContract" Width="80px" />
                                    </div>
                                    <div>
                                        <asp:Button ID="ButtonUploadDOCEdit" runat="server" CausesValidation="False" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="UploadDOCAddendum" CssClass="DDLEditContract" Text="Upload" />
                                    </div>
                                    <div>
                                        <asp:Label ID="LabelPDFContractEdit" runat="server" Text="Contract PDF" CssClass="ContractEditTitles"/>
                                    </div>
                                    <div>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LabelDeletePDF" runat="server" ForeColor="#3366FF" Text="Delete PDF?" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="CheckBoxDeletePDF" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="LinkToPDFcopyTextBoxEdit" runat="server" CssClass="hidepanel" Text='<%# Bind("AddendumLinkToPDFcopy") %>' />
                                        <asp:FileUpload ID="FileUploadPDFEdit" runat="server" CssClass="EditContract" Width="80px" />
                                    </div>
                                    <div>
                                        <asp:Button ID="ButtonUploadPDFEdit" runat="server" CausesValidation="False" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="UploadPDFAddendum" CssClass="DDLEditContract" Text="Upload" />
                                    </div>
                                </td>
                            </tr>

                        </table>

      <!---- COMMERCIAL TERMS EDIT MODE-------------------------------------------------------->
      <asp:Panel ID="PanelCommercialTermsAddendumEdit" runat="server" >
      <table style="background-color: #CCCCCC">
       <tr>
           <th colspan="12" style="text-align:center; color:Red;">
               <asp:Literal ID="LiteralCommercialRoleTitle" runat="server">
         You cannot update Commercial Terms, because you dont have this role
        </asp:Literal>
           </th>
           <tr>
               <td class="CommercialTitles">Start Date: </td>
               <td style="padding:2px;">
                   <asp:TextBox ID="TextBoxStartDate" runat="server" CssClass="EditContract" Text='<%# Bind("StartDate","{0:dd/MM/yyyy}")%>' width="75px"></asp:TextBox>
                   <asp:CalendarExtender ID="CalendarExtenderStartDate" runat="server" CssClass="cal_Theme1" format="dd/MM/yyyy" TargetControlID="TextBoxStartDate">
                   </asp:CalendarExtender>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidatorStartDate" runat="server" ControlToValidate="TextBoxStartDate" Display="Dynamic" ErrorMessage="dd/mm/yyyy" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}">
                                    </asp:RegularExpressionValidator>
               </td>
               <td class="CommercialTitles">Penalty To Mercury: </td>
               <td style="padding:2px;">
                   <asp:DropDownList ID="DropDownListPenalty" runat="server" CssClass="EditContract" >
                       <asp:ListItem Value="">-</asp:ListItem>
                       <asp:ListItem Value="1">Yes</asp:ListItem>
                       <asp:ListItem Value="0">No</asp:ListItem>
                   </asp:DropDownList>
               </td>
               <td class="CommercialTitles" rowspan="2">Penalty To Mercury Note:</td>
               <td rowspan="2">
                   <asp:TextBox ID="TextBoxPenaltyNote" runat="server" CssClass="EditContract" Height="40" Text='<%# Bind("PenaltiesNote")%>' TextMode="MultiLine" width="125px">
                                    </asp:TextBox>
                   <br />
                   <asp:Label ID="LabelValidationPenaltyMercuryNote" runat="server" ForeColor="Red" ></asp:Label>
               </td>
               <td class="CommercialTitles" rowspan="2">Penalty To Supplier Note:</td>
               <td rowspan="2">
                   <asp:TextBox ID="TextBoxPenaltyNoteSupplier" runat="server" CssClass="EditContract" Height="40" 
                       Text='<%# Bind("PenaltiesToSupplierNote")%>' TextMode="MultiLine" width="125px"></asp:TextBox>
                   <br />
                   <asp:Label ID="LabelValidationPenaltySupplierNote" runat="server" ForeColor="Red" ></asp:Label>
               </td>
               <td class="CommercialTitles" rowspan="2">Delivery Terms: </td>
               <td rowspan="2" style="padding:2px; width:150px;">
                   <asp:TextBox ID="TextBoxDeliveryTerms" runat="server" CssClass="EditContract" Height="40" Text='<%# Bind("DeliveryTerms") %>' TextMode="MultiLine" width="125px">
                   </asp:TextBox>
               </td>
               <td class="CommercialTitles" rowspan="2">Guarantee Period: </td>
               <td rowspan="2" style="padding:2px; width:150px;">
                   <asp:TextBox ID="TextBoxGuaranteePeriod" runat="server" CssClass="EditContract" Height="40" Text='<%# Bind("GuaranteePeriod") %>' TextMode="MultiLine" width="125px">
                                    </asp:TextBox>
               </td>
           </tr>
           <tr>
               <td class="CommercialTitles">Finish Date: </td>
               <td style="padding:2px;">
                   <asp:TextBox ID="TextBoxFinishDate" runat="server" CssClass="EditContract" Text='<%# Bind("FinishDate","{0:dd/MM/yyyy}")%>' width="75px"></asp:TextBox>
                   <asp:CalendarExtender ID="CalendarExtenderFinishDate" runat="server" CssClass="cal_Theme1" format="dd/MM/yyyy" TargetControlID="TextBoxFinishDate">
                   </asp:CalendarExtender>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidatorFinishDate" runat="server" ControlToValidate="TextBoxFinishDate" Display="Dynamic" ErrorMessage="dd/mm/yyyy" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}">
                                    </asp:RegularExpressionValidator>
               </td>
               <td class="CommercialTitles">Penalty To Supplier:</td>
               <td style="padding:2px;">
                   <asp:DropDownList ID="DropDownListPenaltyToSupplier" runat="server" CssClass="EditContract" >
                       <asp:ListItem Value="">-</asp:ListItem>
                       <asp:ListItem Value="1">Yes</asp:ListItem>
                       <asp:ListItem Value="0">No</asp:ListItem>
                   </asp:DropDownList>
               </td>
           </tr>
       </tr>
          <tr>
              <td class="CommercialTitles">Advance: </td>
              <td style="padding:2px;">
                  <asp:TextBox ID="TextBoxAdvance" runat="server" CssClass="EditContract" Text='<%# Bind("Advance") %>' width="50px" />
                  <asp:RangeValidator ID="RangeValidatorAdvance" runat="server" ControlToValidate="TextBoxAdvance" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" 
                      MinimumValue="0" Type="Double">
                  </asp:RangeValidator>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorAdvance" runat="server" ControlToValidate="TextBoxAdvance" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="not valid number" 
                      ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                  </asp:RegularExpressionValidator>
              </td>
              <td class="CommercialTitles">Interim:</td>
              <td style="padding:2px;">
                  <asp:TextBox ID="TextBoxInterim" runat="server" CssClass="EditContract" Text='<%# Bind("Interim")%>' width="50px" />
                  <asp:RangeValidator ID="RangeValidatorInterim" runat="server" ControlToValidate="TextBoxInterim" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" 
                      MinimumValue="0" Type="Double">
                  </asp:RangeValidator>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorInterim" runat="server" ControlToValidate="TextBoxInterim" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="not valid number" 
                      ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                  </asp:RegularExpressionValidator>
              </td>
              <td class="CommercialTitles">Shipment:</td>
              <td style="padding:2px;">
                  <asp:TextBox ID="TextBoxShipment" runat="server" CssClass="EditContract" Text='<%# Bind("Shipment")%>' width="50px" />
                  <asp:RangeValidator ID="RangeValidatorShipment" runat="server" ControlToValidate="TextBoxShipment" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" 
                      MinimumValue="0" Type="Double">
                  </asp:RangeValidator>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorShipment" runat="server" ControlToValidate="TextBoxShipment" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="not valid number" 
                      ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                  </asp:RegularExpressionValidator>
              </td>
              <td class="CommercialTitles">Delivery:</td>
              <td style="padding:2px;">
                  <asp:TextBox ID="TextBoxDelivery" runat="server" CssClass="EditContract" Text='<%# Bind("Delivery")%>' width="50px" />
                  <asp:RangeValidator ID="RangeValidatorDelivery" runat="server" ControlToValidate="TextBoxDelivery" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" 
                      MinimumValue="0" Type="Double">
                  </asp:RangeValidator>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorDelivery" runat="server" ControlToValidate="TextBoxDelivery" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="not valid number" 
                      ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                  </asp:RegularExpressionValidator>
              </td>
              <td >
                  <asp:Label ID="LabelPaymentTermsValidationNotification" runat="server" ForeColor="Red" Font-Bold="true" Visible="false">
                      Total of Payment Terms exceeds 100%. Please check it again.
                  </asp:Label>
              </td>
              <td >

              </td>
              <td >

              </td>
              <td >

              </td>
          </tr>
      </table>
      </asp:Panel>
      <!---------------------------------------------------------- COMMERCIAL TERMS EDIT MODE---->
                
                </EditItemTemplate>

            </asp:templatefield>
        </Columns>
        <RowStyle  CssClass="GridItemNakladnaya" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceShowAddendum" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand="SELECT [AddendumID]
                                  ,[ContractID]
                                  ,RTRIM(PO_No) AS PO_No
                                  ,rtrim([AddendumNo]) AS AddendumNo
                                  ,[AddendumDate]
                                  ,[AddendumValue_woVAT]
                                  ,[AddendumValue_WithVAT]
                                  ,[VATpercent]
                                  ,rtrim([AddendumDescription]) AS AddendumDescription
                                  ,rtrim([AddendumLinkToTemplatefile_DOC]) AS AddendumLinkToTemplatefile_DOC
                                  ,[AddendumSignBySupplier]
                                  ,[AddendumSignByMercury]
                                  ,[AddendumCollectionBySupplier]
                                  ,RTRIM([AddendumGivenTo]) AS AddendumGivenTo
                                  ,rtrim([AddendumLinkToPDFcopy]) AS AddendumLinkToPDFcopy
                                  ,[AddendumArchivedByMercury]
                                  ,[UpdatedBy]
                                  ,[AddendumRetention]
                                  ,rtrim([AddendumNote]) AS AddendumNote
                                  ,CreatedBy, RTRIM(PersonCreated) AS PersonCreated, RTRIM(PersonUpdated) AS PersonUpdated
                                  ,AttachmentExist
                                  ,NewGeneration
                                  ,RequestedBy
                                  , Penalties
                                  , PenaltiesNote
                                  , Budget
                                  , Advance
                                  , StartDate
                                  , FinishDate
                                  , DeliveryTerms
                                  , GuaranteePeriod
                                  , AddendumTypes
                                  , Scenario
                                  , POexecuted
                                  , PenaltiesToSupplier
                                  , PenaltiesToSupplierNote
                                  , Interim
                                  , Shipment
                                  , Delivery
                                  FROM [Table_Addendums]
                                  WHERE ContractID = @ContractID"
        UpdateCommand="UPDATE [Table_Addendums]
                                   SET [ContractID] = @ContractID
                                      ,[PO_No] = @PO_No
                                      ,[AddendumNo] = @AddendumNo
                                      ,[AddendumDate] = @AddendumDate
                                      ,[AddendumValue_woVAT] = @AddendumValue_woVAT
                                      ,[AddendumValue_WithVAT] = @AddendumValue_WithVAT
                                      ,[VATpercent] = @VATpercent
                                      ,[AddendumDescription] = @AddendumDescription
                                      ,[AddendumLinkToTemplatefile_DOC] = @AddendumLinkToTemplatefile_DOC
                                      ,[AddendumSignBySupplier] = @AddendumSignBySupplier
                                      ,[AddendumSignByMercury] = @AddendumSignByMercury
                                      ,[AddendumCollectionBySupplier] = @AddendumCollectionBySupplier
                                      ,[AddendumGivenTo] = @AddendumGivenTo
                                      ,[AddendumLinkToPDFcopy] = @AddendumLinkToPDFcopy
                                      ,[AddendumArchivedByMercury] = @AddendumArchivedByMercury
                                      ,[UpdatedBy] = @UpdatedBy
                                      ,[PersonUpdated] = @PersonUpdated
                                      ,[AddendumRetention] = @AddendumRetention
                                      ,[AddendumNote] = @AddendumNote
                                      ,[AttachmentExist] = @AttachmentExist
                                      , Penalties = @Penalties 
                                      , PenaltiesNote = @PenaltiesNote 
                                      , Budget = @Budget
                                      , Advance = @Advance 
                                      , StartDate = @StartDate 
                                      , FinishDate = @FinishDate 
                                      , DeliveryTerms = @DeliveryTerms 
                                      , GuaranteePeriod = @GuaranteePeriod 
                                      , PenaltiesToSupplier = @PenaltiesToSupplier 
                                      , PenaltiesToSupplierNote = @PenaltiesToSupplierNote 
                                      , Interim = @Interim
                                      , Shipment = @Shipment
                                      , Delivery = @Delivery
                                 WHERE [AddendumID] = @AddendumID"
        DeleteCommand="
                                ALTER TABLE Table_Addendum_UsersApprv DISABLE TRIGGER AfterDelete_Table_Addendum_UsersApprv
                                ALTER TABLE Table_Addendum_UserRemarks DISABLE TRIGGER Table_Addendum_UserRemarks_delete

                                DELETE Table_Addendums WHERE (AddendumID= @AddendumID)

                                ALTER TABLE Table_Addendum_UsersApprv ENABLE TRIGGER AfterDelete_Table_Addendum_UsersApprv
                                ALTER TABLE Table_Addendum_UserRemarks ENABLE TRIGGER Table_Addendum_UserRemarks_delete " 
                            ondeleted="SqlDataSourceShowAddendum_Deleted">
                         <SelectParameters>
                                <asp:Parameter Name="ContractID" />
                        </SelectParameters>   
    </asp:SqlDataSource>
                        </asp:Panel>
                </ItemTemplate>

                <EditItemTemplate>
                        <div style="text-align: center"><asp:Label ID="LabelScenarioOutOfRange" runat="server" Text=""  ForeColor="#FF0066" Font-Bold="True" Font-Size="12px" /></div>
                        <div style="text-align: center"><asp:Label ID="LabelProjectSupplierComtability" runat="server" Visible="false" Text="Project and Supplier must be compatible"  ForeColor="#FF0066" Font-Bold="True" Font-Size="12px" /></div>
                        <table>
                            <asp:Label ID="LabelPoNoBeforeEdit" runat="server" Visible="false" ></asp:Label>
                            <asp:DropDownList ID="DropDownListPOnoEdit" runat="server" DataSourceID="SqlDataSourcePoNo" DataTextField="Information" 
                              DataValueField="PO_No" Font-Names="Consolas" Font-Size="11px" Width="1120px"  BackColor="#FFFF66">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourcePoNo" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
SelectCommand="SELECT N'' As PO_No, 'Select Po No' as Information

UNION ALL

SELECT
rtrim(Table2_PONo.PO_No) AS PO_No,REPLACE(rtrim(Table2_PONo.PO_No)+N' '+RIGHT('00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000' + CAST(dbo.Table2_PONo.Description AS varchar(140)), 140)
+N' &gt; '+ case LEN(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)))
when 7 then LEFT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),1)+ ','+ RIGHT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),6) 
when 8 then LEFT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),2)+ ','+ RIGHT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),6) 
when 9 then LEFT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),3)+ ','+ RIGHT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),6) 
when 10 then LEFT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),1)+ ','+
	+ SUBSTRING(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),2,3)+ ','+
	+ SUBSTRING(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),5,3)+ '.'+	
	+ RIGHT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),2) 
when 11 then LEFT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),2)+ ','+
	+ SUBSTRING(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),3,3)+ ','+
	+ SUBSTRING(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),6,3)+ '.'+	
	+ RIGHT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),2)	
when 12 then LEFT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),3)+ ','+
	+ SUBSTRING(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),4,3)+ ','+
	+ SUBSTRING(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),7,3)+ '.'+	
	+ RIGHT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),2)	
else rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT))
end +N' ' +RTRIM(dbo.View_PoSumExcVAT.PO_Currency) + N' exc.VAT', ' ', '_')
                       AS Information
FROM         dbo.Table2_PONo INNER JOIN
                      dbo.Table1_Project ON dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID INNER JOIN
                      dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID INNER JOIN
                      dbo.View_PoSumExcVAT ON dbo.Table2_PONo.PO_No = dbo.View_PoSumExcVAT.PO_No
WHERE Project_ID= @Project_ID AND dbo.Table6_Supplier.SupplierID = @SupplierID ">
<SelectParameters>
 <asp:Parameter Name="Project_ID" Type="Int32" />
 <asp:Parameter Name="SupplierID" Type="String"/>
</SelectParameters>
</asp:SqlDataSource>
<asp:Label ID="LabelWarningPoDublication" runat="server"></asp:Label>
                            <tr>
                                <td style="width:40px;">
                                    <div>
                                        <asp:Button ID="ButtonUpdate" runat="server" CausesValidation="True" 
                                            CommandName="Update" CssClass="ButtonUpdate" 
                                            onmouseover="this.style.cursor='hand'" Text="Update" Width="35px" />
                                    </div>
                                    <div>
                                        <asp:Button ID="ButtonCancel" runat="server" CausesValidation="False" 
                                            CommandName="Cancel" CssClass="ButtonCancel" 
                                            onmouseover="this.style.cursor='hand'" Text="Cancel" Width="35px" />
                                    </div>
                                </td>
                                <td style="width:20px;">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                                        AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="10" DynamicLayout="False">
                                        <ProgressTemplate>
                                            <img alt="" src="~/Images/ajax-loaderFacebook.gif" />
                                            <asp:Label ID="Label12" runat="server" CssClass="LabelLoading" Text=""></asp:Label>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                                <td style="width:80px; text-align: center;">
                                    <div>
                                        <asp:DropDownList ID="DDLProjectEdit" runat="server"  
                                            AppendDataBoundItems="True" CssClass="EditContract" 
                                            DataSourceID="SqlDataSourcePrjEdit" DataTextField="ProjectName" 
                                            DataValueField="ProjectID" Font-Size="9px" Height="20px" 
                                            selectedvalue='<%# Bind("ProjectID") %>' width="75px">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSourcePrjEdit" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" >
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" 
                                                    PropertyName="Text" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                    <div>
                                        <asp:Label ID="LabelPOnoEdit" runat="server" Text='<%# Bind("PO_No") %>'></asp:Label>
                                        <asp:Label  ID="LabelScenario" runat="server" Text='<%# Bind("Scenario") %>' CssClass="hidepanel" ></asp:Label>
                                    </div>
                                </td>
                                <td style="width:80px; text-align: center;">

                                  <asp:Panel ID="PanelHideFromUser" runat="server" CssClass="hidepanel">
                                    <asp:Literal ID="LiteralContractID" runat="server" Text='<%# Bind("ContractID") %>'></asp:Literal>
                                    <asp:Literal ID="LiteralPOexecuted" runat="server" Text='<%# Bind("POexecuted") %>'></asp:Literal>
                                  </asp:Panel>

                                    <asp:Label ID="LabelContractNoEdit" runat="server" CssClass="ContractEditTitles"  >Contract No</asp:Label>
                                    <br />
                                    <asp:TextBox ID="TextBoxContractNoEdit" runat="server" CssClass="EditContract" 
                                        Height="40" Text='<%# Bind("ContractNo") %>' width="75px"></asp:TextBox>
                                </td>
                                <td style="width:80px; text-align: center;">
                                    <asp:Label ID="LabelContractDateEdit" runat="server" CssClass="ContractEditTitles"  >Contract Date</asp:Label>
                                    <br />

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorContractDate" 
                                        runat="server" ControlToValidate="TextBoxContractDateEdit" Display="Dynamic" 
                                        ErrorMessage="dd/mm/yyyy" 
                                        ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="TextBoxContractDateEdit" runat="server" 
                                        CssClass="EditContract" Text='<%# Bind("ContractDate","{0:dd/MM/yyyy}")%>' 
                                        width="75px"></asp:TextBox>
                                    <asp:CalendarExtender ID="TextBox7_CalendarExtender" runat="server" 
                                        CssClass="cal_Theme1" format="dd/MM/yyyy" 
                                        TargetControlID="TextBoxContractDateEdit">
                                    </asp:CalendarExtender>
                                </td>
                                <td style="width:135px; text-align: left;">
                                        <asp:TextBox ID="TextBoxPO_Count" runat="server" Text='<%# Bind("PO_Count") %>' CssClass="hidepanel" ></asp:TextBox>
                                    <div>
                                        <asp:Label ID="LabelHint" runat="server" Text="You can type number or letter to find supplier" Font-Size="9px" Font-Italic="True" ForeColor="#3399FF"></asp:Label>
                                      <asp:CompareValidator ID="CompareValidatorSupplier" runat="server" ErrorMessage=""
                                       ValueToCompare="INN Number is not 10 digit or Supplier does not exist"
                                       ControlToValidate="TextBoxToValidate" Display="Dynamic" Operator="NotEqual"></asp:CompareValidator>
                                    </div>

                                    <asp:Label ID="LabelSupplierIDEdit" runat="server" CssClass="ContractEditTitles"  >Supplier INN</asp:Label>
                                    <br />

                                    <asp:TextBox ID="SupplierIDTextBox" runat="server" AutoPostBack="true"
                                        CssClass="EditContract" ontextchanged="SupplierIDTextBox_TextChanged" 
                                        Text='<%# Bind("SupplierID") %>' width="115px" CausesValidation="True" />
                                    <div ID="AutoCompleteDiv">
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorSupplierIDTextBox" ValidationGroup="PO_count"
                                        runat="server" ControlToValidate="SupplierIDTextBox" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:AutoCompleteExtender ID="TextBox1_AutoCompleteExtender" runat="server" 
                                        CompletionInterval="0" CompletionListElementID="AutoCompleteDiv" 
                                        CompletionSetCount="10" MinimumPrefixLength="0" 
                                        onclientshown="SetAutoCompleteWidth" ServiceMethod="SupplierEdit" 
                                        ServicePath="AutoComplete.asmx" TargetControlID="SupplierIDTextBox">
                                    </asp:AutoCompleteExtender>
                                    <div>
                                        <asp:Label ID="LabelSupplierNameInEdit" runat="server" CssClass="LabelGeneral"></asp:Label>
                                        <asp:TextBox ID="TextBoxToValidate" runat="server" Visible="false" ></asp:TextBox>
                                    </div>
                                </td>
                                <td style="width:130px; text-align: center;">
                                    <asp:Label ID="LabelContractDescriptionEdit" runat="server" CssClass="ContractEditTitles"  >Description</asp:Label>
                                    <br />

                                    <asp:TextBox ID="TextBoxContractDescriptionEdit" runat="server" 
                                        CssClass="EditContract" Height="40" Text='<%# Bind("ContractDescription") %>' 
                                        TextMode="MultiLine" width="125px"></asp:TextBox>
                                </td>
                                <td style="width:50px; text-align: center;">
                                    <asp:Label ID="LabelTypeEdit" runat="server" CssClass="ContractEditTitles"  >Type</asp:Label>
                                    <br />

                                    <asp:DropDownList ID="DropDownListContractTypeEdit" runat="server" 
                                        AppendDataBoundItems="True" CssClass="EditContract" 
                                        selectedvalue='<%# Bind("ContractType") %>' width="45px">
                                        <asp:ListItem Value="Sub">Sub</asp:ListItem>
                                        <asp:ListItem Value="Sup">Sup</asp:ListItem>
                                        <asp:ListItem Value="Ser">Ser</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width:50px; text-align: center;">
                                    <asp:Label ID="LabelSignedSupplierEdit" runat="server" CssClass="ContractEditTitles"  >Sign Sup.</asp:Label>
                                    <br />

                                    <asp:CheckBox ID="SignBySupplierCheckBoxEdit" runat="server" 
                                        Checked='<%# Bind("SignBySupplier") %>' />
                                </td>
                                <td style="width:50px; text-align: center;">
                                    <asp:Label ID="LabelSignMercEdit" runat="server" CssClass="ContractEditTitles"  >Sign Merc.</asp:Label>
                                    <br />

                                    <asp:CheckBox ID="SignByMercuryCheckBoxEdit" runat="server" 
                                        Checked='<%# Bind("SignByMercury") %>' />
                                </td>
                                <td style="width:50px; text-align: center; display: none;">
                                    <asp:CheckBox ID="CollectionBySupplierCheckBoxEdit" runat="server" 
                                        Checked='<%# Bind("CollectionBySupplier") %>' />
                                </td>

                                <td style="width:100px; text-align: center;">
                                    <asp:Label ID="LabelGivenToEdit" runat="server" CssClass="ContractEditTitles"  >Given To</asp:Label>
                                    <br />

                                   <asp:TextBox ID="TextBoxContractGivenTo" width="75px" Height="40" 
                                    TextMode="MultiLine" runat="server" Text='<%# Bind("ContractGivenTo") %>' CssClass="EditContract">
                                   </asp:TextBox>
                                </td>

                                <td style="width:85px; font-size:10px;">

                                    <span class="ContractEditTitles"><asp:literal ID="LiteralContractValueExcVAT" Text="Contract Value Exc. VAT" runat="server" ></asp:literal></span>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorContractValue" 
                                        runat="server" ControlToValidate="TextBoxContractValueEdit" Display="Dynamic" 
                                        ErrorMessage="Wrong format" 
                                        ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="TextBoxContractValueEdit" runat="server" 
                                        CssClass="EditContract" Text='<%# Bind("ContractValue_woVAT") %>' width="75px"></asp:TextBox>
                                    <br />

                                    <span class="ContractEditTitles"><asp:literal ID="LiteralContractValueWithVAT" Text="Contract Value With VAT"  runat="server" ></asp:literal></span>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorContractValueWithVATEdit" 
                                        runat="server" ControlToValidate="TextBoxContractValueWithVATEdit" Display="Dynamic" 
                                        ErrorMessage="Wrong format" 
                                        ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorContractValueWithVATEdit" runat="server" 
                                            ControlToValidate="TextBoxContractValueWithVATEdit" 
                                            Display="Dynamic" ErrorMessage="Required">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="TextBoxContractValueWithVATEdit" runat="server" 
                                        CssClass="EditContract" Text='<%# Bind("ContractValue_withVAT") %>' width="75px"></asp:TextBox>
                                    <br />

                                    <span class="ContractEditTitles"><asp:literal ID="LiteralVAT" Text="VAT %"  runat="server" ></asp:literal></span>
                                    <asp:FilteredTextBoxExtender ID="TextBoxVATpercent_FilteredTextBoxExtender" 
                                        runat="server" FilterType="Numbers" TargetControlID="TextBoxVAT">
                                    </asp:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxVAT" runat="server" 
                                        ControlToValidate="TextBoxVAT" Display="Dynamic"
                                        ErrorMessage="Required">
                                    </asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidatorTextBoxVAT" runat="server"  Display="Dynamic"
                                        ErrorMessage="range to be 0-20" ControlToValidate="TextBoxVAT" 
                                        MaximumValue="20" MinimumValue="0" Type="Double">
                                    </asp:RangeValidator>
                                    <asp:TextBox ID="TextBoxVAT" runat="server" 
                                        CssClass="EditContract" Text='<%# Bind("VATpercent") %>' width="75px">
                                    </asp:TextBox>

                                    <br />
                                    <span class="ContractEditTitles"><asp:literal ID="LiteralBudgetEdit" Text="Budget With VAT" runat="server" ></asp:literal></span>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorBudgetEdit" 
                                        runat="server" ControlToValidate="TextBoxBudget" Display="Dynamic" 
                                        ErrorMessage="Wrong format" 
                                        ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="TextBoxBudget" runat="server" 
                                        CssClass="EditContract" Text='<%# Bind("Budget")%>' width="75px"></asp:TextBox>

                                </td>
                                <td style="width:50px; text-align: center;">
                                    <asp:Label ID="LabelCurrencyEdit" runat="server" CssClass="ContractEditTitles"  >Currency</asp:Label>
                                    <br />

                                    <asp:DropDownList ID="DropDownListCurrencyEdit" runat="server" 
                                        AppendDataBoundItems="True" CssClass="EditContract" 
                                        selectedvalue='<%# Bind("ContractCurrency") %>'>
                                        <asp:ListItem Value="">-</asp:ListItem>
                                        <asp:ListItem Value="Rub">Ruble</asp:ListItem>
                                        <asp:ListItem Value="Dollar">Dollar</asp:ListItem>
                                        <asp:ListItem Value="Euro">Euro</asp:ListItem>
                                        <asp:ListItem Value="GBP">GBP</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width:20px; text-align: center;">
                                    <asp:Label ID="LabelRetentionEdit" runat="server" CssClass="ContractEditTitles"  >Retention</asp:Label>
                                    <br />
                                  <asp:TextBox ID="TextBoxRetention" runat="server" CssClass="EditContract" Text='<%# Bind("Retention")%>' width="50px" />
                                  <asp:RangeValidator ID="RangeValidatorRetention" runat="server" ControlToValidate="TextBoxRetention" 
                                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" 
                                      MinimumValue="0" Type="Double">
                                  </asp:RangeValidator>
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorRetention" runat="server" ControlToValidate="TextBoxRetention" 
                                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="not valid number" 
                                      ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                                  </asp:RegularExpressionValidator>
                                </td>
                                <td style="width:50px; text-align: center;">
                                    <asp:Label ID="LabelArchivedEdit" runat="server" CssClass="ContractEditTitles"  >Archived</asp:Label>
                                    <br />

                                    <asp:CheckBox ID="ArchivedByMercuryCheckBoxEdit" runat="server" 
                                        Checked='<%# Bind("ArchivedByMercury") %>' />
                                </td>
                                <td style="width:100px; text-align: center;">
                                    <asp:Label ID="LabelNote" runat="server" CssClass="ContractEditTitles"  >Note</asp:Label>
                                    <br />

                                    <asp:TextBox ID="TextBoxNoteEdit" runat="server" CssClass="EditContract" 
                                        Height="40" Text='<%# Bind("Note") %>' TextMode="MultiLine" width="115px"></asp:TextBox>
                                </td>
                                <td>
                                    <div>
                                        <asp:Label ID="labelInfoForAttachments" runat="server" CssClass="LabelGeneral"></asp:Label>
                                    </div>
                                    <div >
                                        <asp:Label ID="LabelDocFileEdit" runat="server" Text="Template DOC File" 
                                            Width="100px"  CssClass="ContractEditTitles" />
                                    </div>
                                    <div >
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LabelDeleteDOC" runat="server" ForeColor="#3366FF" 
                                                        Text="Delete DOC?" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="CheckBoxDeleteDOC" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                                <div >
                                                        <asp:TextBox ID="LinkToTemplatefile_DOCTextBoxEdit" runat="server" 
                                                            Text='<%# Bind("LinkToTemplatefile_DOC") %>' CssClass="hidepanel" />
                                                        <asp:FileUpload ID="FileUploadDOCEdit" runat="server" CssClass="EditContract" 
                                                            Width="80px" />
                                               </div>
                                                <div >
                                                        <asp:Button ID="ButtonUploadDOCEdit" runat="server"  CausesValidation="False"  
                                                            Text="Upload"  CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="UploadDOC" CssClass="DDLEditContract" />
                                               </div>

                                                <div>
                                                        <asp:Label ID="LabelPDFContractEdit" runat="server" 
                                                            Text="Contract PDF" CssClass="ContractEditTitles" />
                                                </div>
                                               <div>
                                                 <table>
                                                      <tr>
                                                          <td>
                                                              <asp:Label ID="LabelDeletePDF" runat="server" ForeColor="#3366FF" 
                                                                  Text="Delete PDF?" />
                                                          </td>
                                                          <td>
                                                              <asp:CheckBox ID="CheckBoxDeletePDF" runat="server" />
                                                          </td>
                                                      </tr>
                                                 </table>
                                                </div>
                                                <div>
                                                        <asp:TextBox ID="LinkToPDFcopyTextBoxEdit" runat="server" CssClass="hidepanel" 
                                                            Text='<%# Bind("LinkToPDFcopy") %>' />   
                                                        <asp:FileUpload ID="FileUploadPDFEdit" runat="server" CssClass="EditContract" 
                                                            Width="80px" />
                                                </div>
                                                <div>
                                                        <asp:Button ID="ButtonUploadPDFEdit" runat="server"  CausesValidation="False" 
                                                            Text="Upload"  CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="UploadPDF" CssClass="DDLEditContract" />
                                                </div>
                                </td>
                            </tr>
                        </table>

      <!---- COMMERCIAL TERMS EDIT MODE-------------------------------------------------------->
      <asp:Panel ID="PanelCommercialTermsContractEdit" runat="server" >
      <table style="background-color: #CCCCCC">
       <tr>
           <th colspan="12" style="text-align:center; color:Red;">
               <asp:Literal ID="LiteralCommercialRoleTitle" runat="server">
         You cannot update Commercial Terms, because you dont have this role
        </asp:Literal>
           </th>
           <tr>
               <td class="CommercialTitles">Start Date: </td>
               <td style="padding:2px;">
                   <asp:TextBox ID="TextBoxStartDate" runat="server" CssClass="EditContract" Text='<%# Bind("StartDate","{0:dd/MM/yyyy}")%>' width="75px"></asp:TextBox>
                   <asp:CalendarExtender ID="CalendarExtenderStartDate" runat="server" CssClass="cal_Theme1" format="dd/MM/yyyy" TargetControlID="TextBoxStartDate">
                   </asp:CalendarExtender>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidatorStartDate" runat="server" ControlToValidate="TextBoxStartDate" Display="Dynamic" ErrorMessage="dd/mm/yyyy" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}">
                                    </asp:RegularExpressionValidator>
               </td>
               <td class="CommercialTitles">Penalty To Mercury: </td>
               <td style="padding:2px;">
                   <asp:DropDownList ID="DropDownListPenalty" runat="server" CssClass="EditContract" >
                       <asp:ListItem Value="">-</asp:ListItem>
                       <asp:ListItem Value="1">Yes</asp:ListItem>
                       <asp:ListItem Value="0">No</asp:ListItem>
                   </asp:DropDownList>
               </td>
               <td class="CommercialTitles" rowspan="2">Penalty To Mercury Note:</td>
               <td rowspan="2">
                   <asp:TextBox ID="TextBoxPenaltyNote" runat="server" CssClass="EditContract" Height="40" Text='<%# Bind("PenaltiesNote")%>' TextMode="MultiLine" width="125px">
                                    </asp:TextBox>
                   <br />
                   <asp:Label ID="LabelValidationPenaltyMercuryNote" runat="server" ForeColor="Red" ></asp:Label>
               </td>
               <td class="CommercialTitles" rowspan="2">Penalty To Supplier Note:</td>
               <td rowspan="2">
                   <asp:TextBox ID="TextBoxPenaltyNoteSupplier" runat="server" CssClass="EditContract" Height="40" 
                       Text='<%# Bind("PenaltiesToSupplierNote")%>' TextMode="MultiLine" width="125px"></asp:TextBox>
                   <br />
                   <asp:Label ID="LabelValidationPenaltySupplierNote" runat="server" ForeColor="Red" ></asp:Label>
               </td>
               <td class="CommercialTitles" rowspan="2">Delivery Terms: </td>
               <td rowspan="2" style="padding:2px; width:150px;">
                   <asp:TextBox ID="TextBoxDeliveryTerms" runat="server" CssClass="EditContract" Height="40" Text='<%# Bind("DeliveryTerms") %>' TextMode="MultiLine" width="125px">
                   </asp:TextBox>
               </td>
               <td class="CommercialTitles" rowspan="2">Guarantee Period: </td>
               <td rowspan="2" style="padding:2px; width:150px;">
                   <asp:TextBox ID="TextBoxGuaranteePeriod" runat="server" CssClass="EditContract" Height="40" Text='<%# Bind("GuaranteePeriod") %>' TextMode="MultiLine" width="125px">
                                    </asp:TextBox>
               </td>
           </tr>
           <tr>
               <td class="CommercialTitles">Finish Date: </td>
               <td style="padding:2px;">
                   <asp:TextBox ID="TextBoxFinishDate" runat="server" CssClass="EditContract" Text='<%# Bind("FinishDate","{0:dd/MM/yyyy}")%>' width="75px"></asp:TextBox>
                   <asp:CalendarExtender ID="CalendarExtenderFinishDate" runat="server" CssClass="cal_Theme1" format="dd/MM/yyyy" TargetControlID="TextBoxFinishDate">
                   </asp:CalendarExtender>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidatorFinishDate" runat="server" ControlToValidate="TextBoxFinishDate" Display="Dynamic" ErrorMessage="dd/mm/yyyy" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}">
                                    </asp:RegularExpressionValidator>
               </td>
               <td class="CommercialTitles">Penalty To Supplier:</td>
               <td style="padding:2px;">
                   <asp:DropDownList ID="DropDownListPenaltyToSupplier" runat="server" CssClass="EditContract" >
                       <asp:ListItem Value="">-</asp:ListItem>
                       <asp:ListItem Value="1">Yes</asp:ListItem>
                       <asp:ListItem Value="0">No</asp:ListItem>
                   </asp:DropDownList>
               </td>
           </tr>
       </tr>
          <tr>
              <td class="CommercialTitles">Advance: </td>
              <td style="padding:2px;">
                  <asp:TextBox ID="TextBoxAdvance" runat="server" CssClass="EditContract" Text='<%# Bind("Advance") %>' width="50px" />
                  <asp:RangeValidator ID="RangeValidatorAdvance" runat="server" ControlToValidate="TextBoxAdvance" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" 
                      MinimumValue="0" Type="Double">
                  </asp:RangeValidator>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorAdvance" runat="server" ControlToValidate="TextBoxAdvance" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="not valid number" 
                      ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                  </asp:RegularExpressionValidator>
              </td>
              <td class="CommercialTitles">Interim:</td>
              <td style="padding:2px;">
                  <asp:TextBox ID="TextBoxInterim" runat="server" CssClass="EditContract" Text='<%# Bind("Interim")%>' width="50px" />
                  <asp:RangeValidator ID="RangeValidatorInterim" runat="server" ControlToValidate="TextBoxInterim" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" 
                      MinimumValue="0" Type="Double">
                  </asp:RangeValidator>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorInterim" runat="server" ControlToValidate="TextBoxInterim" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="not valid number" 
                      ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                  </asp:RegularExpressionValidator>
              </td>
              <td class="CommercialTitles">Shipment:</td>
              <td style="padding:2px;">
                  <asp:TextBox ID="TextBoxShipment" runat="server" CssClass="EditContract" Text='<%# Bind("Shipment")%>' width="50px" />
                  <asp:RangeValidator ID="RangeValidatorShipment" runat="server" ControlToValidate="TextBoxShipment" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" 
                      MinimumValue="0" Type="Double">
                  </asp:RangeValidator>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorShipment" runat="server" ControlToValidate="TextBoxShipment" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="not valid number" 
                      ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                  </asp:RegularExpressionValidator>
              </td>
              <td class="CommercialTitles">Delivery:</td>
              <td style="padding:2px;">
                  <asp:TextBox ID="TextBoxDelivery" runat="server" CssClass="EditContract" Text='<%# Bind("Delivery")%>' width="50px" />
                  <asp:RangeValidator ID="RangeValidatorDelivery" runat="server" ControlToValidate="TextBoxDelivery" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" 
                      MinimumValue="0" Type="Double">
                  </asp:RangeValidator>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorDelivery" runat="server" ControlToValidate="TextBoxDelivery" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="not valid number" 
                      ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                  </asp:RegularExpressionValidator>
              </td>
              <td >
                  <asp:Label ID="LabelPaymentTermsValidationNotification" runat="server" ForeColor="Red" Font-Bold="true" Visible="false">
                      Total of Payment Terms exceeds 100%. Please check it again.
                  </asp:Label>
              </td>
              <td >

              </td>
              <td >

              </td>
              <td >

              </td>
          </tr>
      </table>
      </asp:Panel>
      <!---------------------------------------------------------- COMMERCIAL TERMS EDIT MODE---->

                </EditItemTemplate>
</asp:TemplateField>

        </Columns>
        <RowStyle  CssClass="GridItemNakladnaya" />
        <HeaderStyle  CssClass="GridHeaderContract" />
        <PagerStyle CssClass="pager" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceForContractGrid" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand="ContractGrid_Select_ForClientASPX" SelectCommandType="StoredProcedure"

        UpdateCommand="UPDATE Table_Contracts
                              SET  ProjectID= @ProjectID
                                   ,PO_No= @PO_No
                                   ,ContractNo= @ContractNo
                                   ,ContractDate= @ContractDate
                                   ,ContractValue_woVAT= @ContractValue_woVAT
                                   ,ContractCurrency= @ContractCurrency
                                   ,SupplierID= @SupplierID
                                   ,ContractDescription= @ContractDescription
                                   ,ContractType= @ContractType
                                   ,LinkToTemplatefile_DOC= @LinkToTemplatefile_DOC
                                   ,SignBySupplier= @SignBySupplier
                                   ,SignByMercury= @SignByMercury
                                   ,CollectionBySupplier= @CollectionBySupplier
                                   ,ContractGivenTo= @ContractGivenTo
                                   ,LinkToPDFcopy= @LinkToPDFcopy
                                   ,ArchivedByMercury= @ArchivedByMercury
                                   ,Retention= @Retention
                                   ,Note= @Note
                                   ,UpdatedBy= @UpdatedBy
                                   ,PersonUpdated= @PersonUpdated
                                   ,[AttachmentExist] = @AttachmentExist
                                   ,[ContractValue_withVAT] = @ContractValue_withVAT
                                   ,[VATpercent] = @VATpercent
                                   ,Penalties = @Penalties
                                   ,PenaltiesNote = @PenaltiesNote
                                   ,PenaltiesToSupplier = @PenaltiesToSupplier
                                   ,PenaltiesToSupplierNote = @PenaltiesToSupplierNote
                                   ,Budget = @Budget
                                   ,Advance = @Advance
                                   ,StartDate = @StartDate 
                                   ,FinishDate = @FinishDate 
                                   ,DeliveryTerms = @DeliveryTerms 
                                   ,GuaranteePeriod = @GuaranteePeriod 
                                   ,Interim= @Interim
                                   ,Shipment= @Shipment
                                   ,Delivery= @Delivery
                                WHERE (ContractID= @ContractID)"
        DeleteCommand="
                                ALTER TABLE Table_Contract_UsersApprv DISABLE TRIGGER AfterDelete_Table_Contract_UsersApprv
                                ALTER TABLE Table_Contracts_UserRemarks DISABLE TRIGGER Table_Contracts_UserRemarks_delete

                                DELETE Table_Contracts WHERE (ContractID= @ContractID)

                                ALTER TABLE [Table_Contract_UsersApprv] ENABLE TRIGGER AfterDelete_Table_Contract_UsersApprv
                                ALTER TABLE Table_Contracts_UserRemarks ENABLE TRIGGER Table_Contracts_UserRemarks_delete " >

                         <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownListPrjID" Name="ProjectID" PropertyName="Text" />
                            <asp:ControlParameter ControlID="DropDownListProjectSelected" Name="ProjectIDSelected" PropertyName="Text" DefaultValue="0" />
                            <asp:ControlParameter ControlID="DropDownListSupplier" Name="SupplierID" PropertyName="Text" />
                            <asp:Parameter Name="ApprovalMode" Type="Int32" DefaultValue="0"/>
                            <asp:Parameter Name="UserName" Type="String" DefaultValue="_"/>
                        </SelectParameters>   
    </asp:SqlDataSource>

        <asp:Panel runat="server" ID="panelexcel" CssClass="hidepanel">

<asp:GridView ID="GridViewExcel" runat="server" AutoGenerateColumns="False" 
         EnableModelValidation="True" 
        CssClass="Grid" HeaderStyle-BackColor="#3333CC" HeaderStyle-ForeColor="White">
        <Columns>
            <asp:TemplateField >
                <ItemTemplate>
                    <asp:Label ID="LabelSortNumber" runat="server" Font-Bold="True" ForeColor="#CC0000" Font-Size="12px"></asp:Label>
                    <asp:Label ID="LabelID" runat="server" Text='<%# Bind("ID") %>' Visible = "false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ContractNo" SortExpression="ContractNoToExcel" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="LabelContractNoToExcel" runat="server" Text='<%# Bind("ContractNoToExcel") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Add Or Not" SortExpression="AddendumOrNot" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="LabelAddendumOrNot" runat="server" Text='<%# Bind("AddendumOrNot") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ContractNo" SortExpression="ContractNo" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("ContractNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ContractDate" SortExpression="ContractDate" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("ContractDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SupplierName" SortExpression="SupplierName" ControlStyle-Width="120" HeaderStyle-Width="120">
                <ItemTemplate>
                    <asp:Label ID="LabelSupplierName" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ContractDescription"  SortExpression="ContractDescription" ControlStyle-Width="300" HeaderStyle-Width="300">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("ContractDescription") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contract Type" SortExpression="ContractType"  HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("ContractType") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Signed By Supplier" SortExpression="SignBySupplier"  HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBoxSignBySupplier" runat="server" 
                        Checked='<%# Bind("SignBySupplier") %>' Enabled="false"  Visible="False" />
                    <asp:Label ID="LabelSignBySupplier" runat="server" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Signed By Mercury" SortExpression="SignByMercury"  HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBoxSignByMercury" runat="server"  Visible="False"
                        Checked='<%# Bind("SignByMercury") %>' Enabled="false" />
                    <asp:Label ID="LabelSignByMercury" runat="server" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Given To" SortExpression="ContractGivenTo"  HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="LabelContractGivenTo" runat="server" 
                        Text='<%# Bind("ContractGivenTo") %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Contract Value woVAT"  SortExpression="ContractValue_woVAT" ControlStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("ContractValue_woVAT") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText=""  SortExpression="ContractCurrency">
                <ItemTemplate>
                                <asp:Label ID="LabelContractCurrency" runat="server" Text='<%# Bind("ContractCurrency") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Retention" SortExpression="Retention"  ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="LabelRetention" runat="server" Text='<%# Bind("Retention") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Archived"  SortExpression="ArchivedByMercury"  HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBoxArchivedByMercury" runat="server" 
                        Checked='<%# Bind("ArchivedByMercury") %>' Enabled="false"  Visible="False" />
                    <asp:Label ID="LabelArchivedByMercury" runat="server" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Note" SortExpression="Note" ControlStyle-Width="100" HeaderStyle-Width="100">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Note") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle  CssClass="GridItemNakladnaya" />
        <HeaderStyle  CssClass="GridHeader" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceExcel" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand="SELECT * FROM
                      (
                      SELECT RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName, DataSource2.*
                      FROM         dbo.Table6_Supplier INNER JOIN
                      (SELECT * FROM
                      (
                      SELECT      CONVERT(nchar(5), ContractID) + N' _ ' AS ID, ProjectID, RTRIM(ContractNo) AS ContractNo, ContractDate, SupplierID, RTRIM(ContractDescription) 
                                            AS ContractDescription, RTRIM(ContractType) AS ContractType, SignBySupplier, SignByMercury, RTRIM(ContractGivenTo) AS ContractGivenTo, ContractValue_woVAT, 
                                            RTRIM(ContractCurrency) AS ContractCurrency, Retention, ArchivedByMercury, RTRIM(Note) AS Note,
                                            RTRIM(ContractNo) AS ContractNoToExcel,N'' AS AddendumOrNot
                      FROM         dbo.Table_Contracts

                      UNION ALL

                      SELECT     CONVERT(nchar(5), dbo.Table_Contracts.ContractID) + 'A' + CONVERT(nchar(5), dbo.Table_Addendums.AddendumID) AS ID, dbo.Table_Contracts.ProjectID, 
                                            RTRIM(dbo.Table_Addendums.AddendumNo) AS AddendumNo, dbo.Table_Addendums.AddendumDate, dbo.Table_Contracts.SupplierID, 
                                            RTRIM(dbo.Table_Addendums.AddendumDescription) AS AddendumDescription, RTRIM(dbo.Table_Contracts.ContractType) AS ContractType, 
                                            dbo.Table_Addendums.AddendumSignBySupplier, dbo.Table_Addendums.AddendumSignByMercury, RTRIM(dbo.Table_Addendums.AddendumGivenTo) 
                                            AS AddendumGivenTo, dbo.Table_Addendums.AddendumValue_woVAT, RTRIM(dbo.Table_Contracts.ContractCurrency) AS ContractCurrency, 
                                            dbo.Table_Addendums.AddendumRetention, dbo.Table_Addendums.AddendumArchivedByMercury, RTRIM(dbo.Table_Addendums.AddendumNote) 
                                            AS AddendumNote, RTRIM(ContractNo) AS ContractNoToExcel, N'Add' AS AddendumOrNot
                      FROM         dbo.Table_Contracts INNER JOIN
                                            dbo.Table_Addendums ON dbo.Table_Contracts.ContractID = dbo.Table_Addendums.ContractID
                      ) As DataSource1) AS DataSource2 ON dbo.Table6_Supplier.SupplierID = DataSource2.SupplierID
                      ) AS DataSource3
                      WHERE     (ProjectID =@ProjectID) AND (SupplierID LIKE '%' + @SupplierID + '%')
                      ORDER BY ID ASC">
                         <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownListPrjID" Name="ProjectID" PropertyName="Text" />
                            <asp:ControlParameter ControlID="DropDownListSupplier" Name="SupplierID" PropertyName="Text" ConvertEmptyStringToNull="False" />
                        </SelectParameters>   
    </asp:SqlDataSource>
        </asp:Panel>

</asp:Content>
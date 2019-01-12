<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" EnableEventValidation="false" AutoEventWireup="false" CodeFile="ContractenterNew.aspx.vb" Inherits="_contractEnterAprMxPTM" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="WebUserControl_ContractEmailBody.ascx" tagname="SeperateControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>contract</title>

    <script language="javascript"  type="text/javascript">

      function SetAutoCompleteWidth(source, EventArgs) {
        var a
        a = document.getElementById('<%=(((Master.FindControl("MainContent")).FindControl("FormViewContract")).FindControl("AutoCompleteDiv2")).ClientID %>');
        a.style.width = '441px';
      }

<%--      function SetAutoCompleteWidthOfferSupplier(source, EventArgs) {
        var a
        a = document.getElementById('<%=(((Master.FindControl("MainContent")).FindControl("FormViewContract")).FindControl("DivTextBoxOfferSupplier")).ClientID %>');
        a.style.width = '441px';
      }--%>

      function SupplierInsert() {
        var target1
        var target2

        target1 = document.getElementById('<%=((((Master.FindControl("MainContent")).FindControl("FormViewContract")).FindControl("FormViewSupplier")).FindControl("SupplierIDTextBox")).ClientID %>');
        target2 = document.getElementById('<%=((((Master.FindControl("MainContent")).FindControl("FormViewContract")).FindControl("FormViewSupplier")).FindControl("SupplierNameTextBox")).ClientID %>');

        if ((target1.value == '') || (target2.value == '')) {
          alert('Supplier ID and Supplier Name must be provided!')
          return false
        }
        else if (target1.value != '') {
          if (target1.value.length != 10) {
            alert('INN number must be 10 digit!')
            return false
          }
        }
      }

    </script>


    <style type="text/css">
        .style1
        {
            width: 200px;
        }
        .style3
        {
            width: 200px;
        }
        .style4
        {
            width: 200px;
        }
        .style5
        {
            width: 200px;
        }
        .stylePercentWidth
        {
            width: 70px;
        }
        
        .style6
        {
            padding: 0px 0px 0px 15px;
        }
        
      .style7
      {}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <div id="ModalFileManager" class="modal">
        <div class="modal-dialog modal-dialog-center">
            <div class="modal-content modal_inlineBlock">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Manage Files</h4>
                </div>
                <div class="modal-body" style="width:800px;" >
                    <asp:Panel ID="panelContainer" runat="server">
                        <dx:ASPxFileManager ID="ASPxFileManager1" runat="server" Visible="true" Height="300" >
                            <Settings RootFolder="~/CONTRACT\_doNotDeleteThis" ThumbnailFolder="~/Thumb/" EnableMultiSelect="true" />
                            <SettingsFileList View="Details"></SettingsFileList>
                            <SettingsEditing AllowDownload="true"/>
                            <SettingsUpload Enabled="false" AdvancedModeSettings-EnableMultiSelect="true"></SettingsUpload>
                            <SettingsFolders visible="false" />
                            <ClientSideEvents SelectedFileOpened="function(s, e) {
	                            e.file.Download();
	                            e.processOnServer = false;
                            }" />
                        </dx:ASPxFileManager>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>



    <uc1:SeperateControl ID="WebUserControl_ContractEmailBody" runat="server" />

    <div style="text-align: center"><asp:Label ID="LabelEntryMessage" runat="server" Visible="false" Text="Your entries have been recived successfully!"  ForeColor="#33CC33" Font-Bold="True" Font-Size="12px" /></div>
    <div style="text-align: center"><asp:Label ID="LabelProjectSupplierComtability" runat="server" Visible="false" Text="Project and Supplier must be compatible"  ForeColor="#FF0066" Font-Bold="True" Font-Size="12px" /></div>

    <asp:SqlDataSource ID="SqlDataSourceContract" runat="server"
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
        SelectCommand="SELECT [ContractID], 
        [ProjectID], 
        [PO_No], 
        [ContractNo], 
        [ContractDate], 
        [ContractValue_woVAT], 
        [ContractValue_withVAT],
        [SignBySupplier], 
        [SentToSupplier], 
        [LinkToTemplatefile_DOC], 
        [ContractType], 
        [ContractDescription], 
        [SupplierID], 
        [ContractCurrency], 
        [SignByMercury], 
        [CollectionBySupplier], 
        [LinkToPDFcopy], 
        [ArchivedByMercury], 
        [Retention], 
        [Note],
        [CostCode],
        [RequestedBy] 
        FROM [Table_Contracts]"
        InsertCommand="INSERT INTO Table_Contracts
                                   (ProjectID
                                   ,PO_No
                                   ,ContractNo
                                   ,ContractDate
                                   ,ContractValue_woVAT
                                   ,ContractValue_withVAT
                                   ,VATpercent
                                   ,ContractCurrency
                                   ,SupplierID
                                   ,ContractDescription
                                   ,ContractType
                                   ,LinkToTemplatefile_DOC
                                   ,SentToSupplier
                                   ,SignBySupplier
                                   ,SignByMercury
                                   ,CollectionBySupplier
                                   ,LinkToPDFcopy
                                   ,ArchivedByMercury
                                   ,Retention
                                   ,Note
                                   ,CreatedBy
                                   ,PersonCreated
                                   ,AttachmentExist
                                   ,CostCode
                                   ,RequestedBy
                                   ,Scenario
                                   ,Penalties
                                   ,PenaltiesNote
                                   ,PenaltiesToSupplier
                                   ,PenaltiesToSupplierNote
                                   ,Budget
                                   ,BudgetLinkToPDF
                                   ,Advance
                                   ,Interim
                                   ,Shipment
                                   ,Delivery
                                   ,StartDate
                                   ,FinishDate
                                   ,DeliveryTerms
                                   ,GuaranteePeriod
                                   ,Nominated
                                   ,FrameContract)
                        VALUES
                                   (@ProjectID
                                   ,@PO_No
                                   ,@ContractNo
                                   ,@ContractDate
                                   ,@ContractValue_woVAT
                                   ,@ContractValue_withVAT
                                   ,@VATpercent
                                   ,@ContractCurrency
                                   ,@SupplierID
                                   ,@ContractDescription
                                   ,@ContractType
                                   ,@LinkToTemplatefile_DOC
                                   ,@SentToSupplier
                                   ,@SignBySupplier
                                   ,@SignByMercury
                                   ,@CollectionBySupplier
                                   ,@LinkToPDFcopy
                                   ,@ArchivedByMercury
                                   ,@Retention
                                   ,@Note
                                   ,@CreatedBy
                                   ,@PersonCreated
                                   ,@AttachmentExist
                                   ,@CostCode
                                   ,@RequestedBy
                                   ,@Scenario
                                   ,@Penalties
                                   ,@PenaltiesNote
                                   ,@PenaltiesToSupplier
                                   ,@PenaltiesToSupplierNote
                                   ,@Budget
                                   ,@BudgetLinkToPDF
                                   ,@Advance
                                   ,@Interim
                                   ,@Shipment
                                   ,@Delivery
                                   ,@StartDate
                                   ,@FinishDate
                                   ,@DeliveryTerms
                                   ,@GuaranteePeriod
                                   ,@Nominated
                                   ,@FrameContract) 
                                   ;SELECT @ID=SCOPE_IDENTITY()" >

    <InsertParameters>
     <asp:Parameter Name="ContractDate" Type="DateTime" />
     <asp:Parameter Name="Retention" Type="Decimal" />
     <asp:Parameter Name="CreatedBy" Type="DateTime" />
     <asp:Parameter Name="PersonCreated" Type="String" />
     <asp:Parameter Name="CostCode" Type="String" />
     <asp:Parameter Name="RequestedBy" Type="String" />
     <asp:Parameter Name="Scenario" Type="Int32" />
     <asp:Parameter Direction="Output" Name="ID" Type="Int32" />
    </InsertParameters>

    </asp:SqlDataSource>

    <div style="color:Gray; font-size:large;"> <asp:Literal ID="LiteralTitle" runat="server" ></asp:Literal> </div>

    <asp:FormView ID="FormViewContract" runat="server" 
        DataSourceID="SqlDataSourceContract" AllowPaging="True" 
        DataKeyNames="ContractID" EmptyDataText="Empty" DefaultMode="Insert">

        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" 
            LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" />

        <EditItemTemplate>

        </EditItemTemplate>
        <InsertItemTemplate>
            
<!-- ------------THIS IS HIDDEN, NOT IN USE IN NEW DESIGN ----------->
<!-- ------------THIS IS HIDDEN, NOT IN USE IN NEW DESIGN ----------->
<!-- ------------THIS IS HIDDEN, NOT IN USE IN NEW DESIGN ----------->
<!-- ------------THIS IS HIDDEN, NOT IN USE IN NEW DESIGN ----------->
<!-- ------------THIS IS HIDDEN, NOT IN USE IN NEW DESIGN ----------->
<asp:Panel ID="hidePanel" runat="server" CssClass="hidepanel" >
            <table style="border: thin ridge #E0E0E0; background-color: #F5F5F5; margin:2px;" >
                <tr>
                    <td class="style1">
                        <asp:Label ID="LabelInsert11" runat="server" CssClass="LabelContract" 
                            Text="SentToSupplier" Width="100px" visible="false"/>
                        <asp:CheckBox ID="SentToSupplierCheckBox" runat="server" CssClass="LabelContract"
                            Checked='<%# Bind("SentToSupplier") %>'  Visible="false"/>
                    </td>
                    <td class="style3">
                        <asp:Label ID="LabelInsert12" runat="server" CssClass="LabelContract" 
                            Text="SignedBySupplier" Width="100px" />
                        <asp:CheckBox ID="SignBySupplierCheckBox" runat="server" CssClass="LabelContract"
                            Checked='<%# Bind("SignBySupplier") %>' />
                    </td>
                    <td >
                        <asp:Label ID="LabelInsert13" runat="server" CssClass="LabelContract" 
                            Text="SignedByMercury" Width="100px" />
                        <asp:CheckBox ID="SignByMercuryCheckBox" runat="server" CssClass="LabelContract"
                            Checked='<%# Bind("SignByMercury") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        </td>
                    <td class="style3">
                        </td>
                    <td >
                        <asp:Label ID="LabelInfoPDF" runat="server" CssClass="LabelGeneral" 
                            style="font-weight: 700"></asp:Label>   
                    </td>
                </tr>

                <tr>
                    <td class="style1">
                        </td>
                    <td class="style3">
                        </td>
                    <td >
                        <asp:Label ID="LabelInsert14" runat="server" CssClass="LabelContract" 
                            Text="Contract PDF" Width="100px" />
                        <asp:TextBox ID="LinkToPDFcopyTextBox" runat="server" CssClass="hidepanel" 
                            Text='<%# Bind("LinkToPDFcopy") %>' />
                        <asp:FileUpload ID="FileUploadPDF" runat="server" CssClass="TextBoxContract" />
                        <asp:Button ID="ButtonUploadPDF" runat="server" CssClass="btn btn-mini btn-default"  CausesValidation="False" onclick="ButtonUploadPDF_Click"
                            Text="Upload" />
                    </td>
                </tr>
            </table>
        
        <br />
        
            <table style="border: thin ridge #E0E0E0; background-color: #F5F5F5; margin:2px;" >
                <tr>
                    <td class="style4">
                        <asp:Label ID="LabelInsert15" runat="server" CssClass="LabelContract" 
                            Text="Collected By Supplier" Width="150px" />
                        <asp:CheckBox ID="CollectionBySupplierCheckBox" runat="server" CssClass="LabelContract"
                            Checked='<%# Bind("CollectionBySupplier") %>' />
                    </td>
                    <td class="style5">
                        <asp:Label ID="LabelInsert16" runat="server" CssClass="LabelContract" 
                            Text="Archived By Mercury" Width="150px" />
                        <asp:CheckBox ID="ArchivedByMercuryCheckBox" runat="server"  CssClass="LabelContract"
                            Checked='<%# Bind("ArchivedByMercury") %>' />
                    </td>
                    <td >
                        <asp:Label ID="LabelRetention" runat="server" CssClass="LabelContract" 
                            Text="Retention" Width="70px" />
                    </td>
                    <td class="stylePercentWidth">
                        <asp:DropDownList ID="DropDownListRetention" runat="server" >
                            <asp:ListItem Value="99.9">Select</asp:ListItem>
                            <asp:ListItem Value="0.0">N/A</asp:ListItem>
                            <asp:ListItem Value="1.0">1 %</asp:ListItem>
                            <asp:ListItem Value="1.5">1.5 %</asp:ListItem>
                            <asp:ListItem Value="2.0">2 %</asp:ListItem>
                            <asp:ListItem Value="2.5">2.5 %</asp:ListItem>
                            <asp:ListItem Value="3.0">3 %</asp:ListItem>
                            <asp:ListItem Value="4.0">4 %</asp:ListItem>
                            <asp:ListItem Value="5.0">5 %</asp:ListItem>
                            <asp:ListItem Value="6.0">6 %</asp:ListItem>
                            <asp:ListItem Value="7.0">7 %</asp:ListItem>
                            <asp:ListItem Value="8.0">8 %</asp:ListItem>
                            <asp:ListItem Value="9.0">9 %</asp:ListItem>
                            <asp:ListItem Value="10.0">10%</asp:ListItem>
                            <asp:ListItem Value="11.0">11%</asp:ListItem>
                            <asp:ListItem Value="12.0">12%</asp:ListItem>
                            <asp:ListItem Value="13.0">13%</asp:ListItem>
                            <asp:ListItem Value="14.0">14%</asp:ListItem>
                            <asp:ListItem Value="15.0">15%</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td >
                        <asp:Label ID="LabelNote" runat="server" CssClass="LabelContract" 
                            Text="Note" Width="50px" />
                        <br />    
                        <asp:TextBox ID="TextBoxNote" runat="server" 
                            CssClass="TextBoxContract" Height="75px" 
                            Text='<%# Bind("Note") %>' TextMode="MultiLine" 
                            Width="200px" />                            
                    </td>
                </tr>
            </table>
</asp:Panel>
<!-- ------------THIS IS HIDDEN, NOT IN USE IN NEW DESIGN ----------->
<!-- ------------THIS IS HIDDEN, NOT IN USE IN NEW DESIGN ----------->
<!-- ------------THIS IS HIDDEN, NOT IN USE IN NEW DESIGN ----------->
<!-- ------------THIS IS HIDDEN, NOT IN USE IN NEW DESIGN ----------->
<!-- ------------THIS IS HIDDEN, NOT IN USE IN NEW DESIGN ----------->

        <table>
        <tr>
        <td>
        <table style="border: thin ridge #E0E0E0; background-color: #F5F5F5; margin:2px;" >
                <tr>
                    <td>
                        
                        <asp:Label ID="LabelInsert1" runat="server" CssClass="LabelContract" 
                            Text="Project" Width="100px" />
                    </td>
                    <td>
                        
                        <asp:TextBox ID="ProjectIDTextBox" runat="server" CssClass="hidepanel" 
                            Text='<%# Bind("ProjectID") %>' />
                        <asp:DropDownList ID="DropDownListPrj" runat="server" 
                            DataSourceID="SqlDataSourcePrj" DataTextField="ProjectName" 
                            DataValueField="ProjectID" 
                            ondatabound="DropDownListPrj_DataBound" >
                        </asp:DropDownList>

                <asp:Panel ID="PanelPrj" runat="server" CssClass="hidepanel">
                    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                    SelectCommand="SELECT  Table1_Project.ProjectID, (RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5),dbo.Table1_Project.ProjectID))) as ProjectName FROM Table1_Project ORDER BY dbo.Table1_Project.ProjectName">
                    </asp:SqlDataSource>
                </asp:Panel>

                    </td>
                <td>
                        <asp:CompareValidator ID="CompareValidatorDropDownListPrj" runat="server" CssClass="LabelGeneral" Operator="NotEqual"
                            ErrorMessage="Required" ControlToValidate="DropDownListPrj" Display="Dynamic" ValueToCompare="0">
                        </asp:CompareValidator>
                </td>
                </tr>

                <tr id="tr_clientproject" runat="server" visible="false">
                    <td>
                        <asp:Label ID="LabelProjectClient" runat="server" CssClass="LabelContract" Text="Project for Client" Width="100px" />
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownListProjectClient" runat="server"
                            DataSourceID="SqlDataSourcePrjClient" DataTextField="ProjectName" 
                            DataValueField="ProjectID" Width="200px">
                        </asp:DropDownList>

                            <asp:SqlDataSource ID="SqlDataSourcePrjClient" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                            SelectCommand="
											IF EXISTS (SELECT     TOP (100) PERCENT dbo.aspnet_Users.UserName
                                            FROM         dbo.aspnet_Users INNER JOIN
                                                                  dbo.aspnet_UsersInRoles ON dbo.aspnet_Users.UserId = dbo.aspnet_UsersInRoles.UserId AND 
                                                                  dbo.aspnet_Users.UserId = dbo.aspnet_UsersInRoles.UserId INNER JOIN
                                                                  dbo.aspnet_Roles ON dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId AND dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId
                                            WHERE     (dbo.aspnet_Roles.RoleName = N'ContractSupportGirl' OR
                                                                  dbo.aspnet_Roles.RoleName = N'ContractLeadGirls') AND (dbo.aspnet_Users.UserName = @username))

                                            SELECT ProjectID, ProjectName FROM (

                                            SELECT 0 AS ProjectID, N'__Select Project' AS ProjectName

                                            UNION ALL

                                            SELECT -1 AS ProjectID, N'_N/A' AS ProjectName

                                            UNION ALL

                                            SELECT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID)) 
                                                                  AS ProjectName
                                            FROM         dbo.Table1_Project INNER JOIN
                                                                  dbo.Table_Contract_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Contract_User_Junction.ProjectID
                                            WHERE     (dbo.Table1_Project.NewGeneration <> 1) AND (dbo.Table_Contract_User_Junction.UserName = @username)
                                            ) AS Source
                                            ORDER BY Source.ProjectName

                                            ELSE

                                            SELECT ProjectID, ProjectName FROM (

                                            SELECT 0 AS ProjectID, N'_Select Project' AS ProjectName

                                            UNION ALL

                                            SELECT     dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID)) 
                                                                  AS ProjectName
                                            FROM         dbo.Table1_Project INNER JOIN
                                                                  dbo.Table_Contract_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Contract_User_Junction.ProjectID
                                            WHERE     (dbo.Table1_Project.NewGeneration <> 1) AND (dbo.Table1_Project.ProjectID <> 999) AND (dbo.Table_Contract_User_Junction.UserName = @username)
                                            ) AS Source
                                            ORDER BY Source.ProjectName
                                ">
                                <SelectParameters>
                                    <asp:Parameter Name="username" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                    </td>
                    <td>
                        <asp:CompareValidator ID="CompareValidatorPrjClient" runat="server" CssClass="LabelGeneral" Operator="NotEqual" Enabled="true"
                            ErrorMessage="Required" ControlToValidate="DropDownListProjectClient" Display="Dynamic" ValueToCompare="0">
                        </asp:CompareValidator>
                    </td>
                </tr>

                <tr>
                    <td>
                        
                        <asp:Label ID="LabelInsert2" runat="server" CssClass="LabelContract" 
                            Text="Po No" Width="100px" />
                    </td>
                    <td>
                        
                        <asp:TextBox ID="PO_NoTextBox" runat="server" CssClass="TextBoxContract" Enabled="false"
                            Text='<%# Bind("PO_No") %>' />
                    </td>
                <td>
                </td>
                </tr>
                <tr>
                    <td>
                        
                        <asp:Label ID="LabelInsert3" runat="server" CssClass="LabelContract" 
                            Text="Supplier INN" Width="100px" />
                    </td>
                    <td>

                       <asp:TextBox ID="x" runat="server" CssClass="TextBoxContract"  Text='<%# Bind("SupplierID") %>'
                            Autopostback="true"  ontextchanged="x_TextChanged" placeholder="Type Number or Text"/>
                                <div ID="AutoCompleteDiv2" 
                                    runat="server" class="LabelGeneral">
                                </div>
                                
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorx" runat="server" 
                            ControlToValidate="x" CssClass="LabelGeneral" Display="Dynamic"
                            ErrorMessage="Required"></asp:RequiredFieldValidator>

                        <asp:AutoCompleteExtender ID="TextBox1_AutoCompleteExtenderx" runat="server" 
                            CompletionInterval="0" CompletionListElementID="AutoCompleteDiv2" 
                            CompletionSetCount="12" MinimumPrefixLength="0" 
                            onclientshown="SetAutoCompleteWidth" ServiceMethod="SupplierEdit" 
                            ServicePath="AutoComplete.asmx" TargetControlID="x">
                        </asp:AutoCompleteExtender>

                    </td>
                <td>
                        <asp:Button ID="ButtonEnterSupplier" runat="server" CssClass="btn btn-mini btn-default" 
                            onclick="ButtonEnterSupplier_Click" Text="Add New Supplier" 
                            Width="120px" CausesValidation="False" />
                </td>
                </tr>
                <tr>
                <td>
                </td>
                <td>
                                            <asp:Label ID="LabelShowSupplier" runat="server" Font-Bold="True" 
                                CssClass="LabelSupplierPOcreate" BackColor="#003366" ForeColor="White"></asp:Label>
                                            <asp:Label ID="LabelSplrError" runat="server" 
                                CssClass="LabelGeneral" Font-Bold="True" ForeColor="#FF3300"></asp:Label>

                </td>
                <td>
                </td>
                </tr>
            </table>
        </td>
        <td class="style6" style="text-align: right">
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
            CssClass="btn btn-mini btn-success" CommandName="Insert" 
            Text="Send Contract Template"  />
            <asp:LinkButton ID="InsertCancelButton" runat="server" CssClass="btn btn-mini btn-default"
                CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </td>
            <td >

                </td>
        </tr>
        </table>
        
            <br />
                        <asp:FormView ID="FormViewSupplier" runat="server" DataKeyNames="SupplierID" 
                            DataSourceID="SqlDataSourceSupplierEnter" DefaultMode="Insert" 
                            Visible="False" OnItemInserting="FormViewSupplier_ItemInserting">
                            <EditItemTemplate>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <table class="FrmViewSupplierPOcreate">
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="LabelSupplierMessage" runat="server" ForeColor="Red" Font-Bold="true" 
                                                Text="Dublicating supplier. Transaction terminated." Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style11">
                                             <asp:Label ID="Label6" runat="server" CssClass="LabelGeneral" 
                                                Text="Supplier ID :"></asp:Label>
                                        </td>
                                        <td class="style10">
                                            <asp:TextBox ID="SupplierIDTextBox" runat="server" CssClass="TextBoxGeneralRev" 
                                                Text='<%# Bind("SupplierID") %>' />
                                            <asp:FilteredTextBoxExtender ID="SupplierIDTextBox_FilteredTextBoxExtender" 
                                                runat="server" FilterType="Numbers" TargetControlID="SupplierIDTextBox"
                                                ></asp:FilteredTextBoxExtender>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorSupplierEnter" Display="Dynamic"
                                                runat="server" ControlToValidate="SupplierIDTextBox" CssClass="LabelGeneral" 
                                                ErrorMessage="INN number to be 10 digit!" Font-Bold="True" ValidationGroup="SupplierEntry"
                                                SetFocusOnError="True" ValidationExpression="\d{10}"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorSupplierID" runat="server" ControlToValidate="SupplierIDTextBox"
                                                ErrorMessage="Required" Display="Dynamic" ValidationGroup="SupplierEntry" CssClass="LabelGeneral">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style11">
                                            <asp:Label ID="Label7" runat="server" CssClass="LabelGeneral" 
                                                Text="Supplier Name :"></asp:Label>
                                        </td>
                                        <td class="style10">
                                            <asp:TextBox ID="SupplierNameTextBox" runat="server" CssClass="TextBoxGeneralRev" 
                                                Text='<%# Bind("SupplierName") %>' Width="190px" />
                                            <br />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorSupplierName" runat="server"  Display="Dynamic"
                                                ErrorMessage="Only latin letters allowed without any special characters" ControlToValidate="SupplierNameTextBox"
                                                CssClass="LabelGeneral" ValidationExpression="^[0-9a-zA-Z ]+$" ValidationGroup="SupplierEntry"> 
                                            </asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorSupplierName" runat="server" ControlToValidate="SupplierNameTextBox"
                                                ErrorMessage="Required" Display="Dynamic" ValidationGroup="SupplierEntry" CssClass="LabelGeneral">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >

                                        </td>
                                        <td >
                                            <font style="font-size:10px;">IP/&#1048;&#1055; : </font><asp:CheckBox ID="CheckBoxPersonSupplier" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBoxPersonSupplier_CheckedChanged" />
                                            <br /><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style11">
                                            <asp:Label ID="Label15" runat="server" CssClass="LabelGeneral" 
                                                Text="VAT Free :"></asp:Label>
                                        </td>
                                        <td class="style10">
                                            <asp:CheckBox ID="VAT_FreeCheckBox2" runat="server" 
                                                Checked='<%# Bind("VAT_Free") %>' CssClass="LabelGeneral" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style11">
                                            <asp:Label ID="Label8" runat="server" CssClass="LabelGeneral" 
                                                Text="Phone 1 :"></asp:Label>
                                        </td>
                                        <td class="style10">
                                            <asp:TextBox ID="PhoneNumber1TextBox" runat="server" CssClass="TextBoxGeneralRev" 
                                                style="margin-left: 0px" Text='<%# Bind("PhoneNumber1") %>' />
                                            <asp:Label ID="LabelOpt" runat="server" CssClass="LabelGeneral" 
                                                Font-Bold="False" Font-Italic="True" ForeColor="#FF3300">Optional</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style11">
                                            <asp:Label ID="Label9" runat="server" CssClass="LabelGeneral" 
                                                Text="Phone 2 :"></asp:Label>
                                        </td>
                                        <td class="style10">
                                            <asp:TextBox ID="PhoneNumber2TextBox" runat="server" CssClass="TextBoxGeneralRev" 
                                                Text='<%# Bind("PhoneNumber2") %>' />
                                            <asp:Label ID="LabelOpt0" runat="server" CssClass="LabelGeneral" 
                                                Font-Bold="False" Font-Italic="True" ForeColor="#FF3300">Optional</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style11">
                                            <asp:Label ID="Label10" runat="server" CssClass="LabelGeneral" 
                                                Text="Fax Number 1 :"></asp:Label>
                                        </td>
                                        <td class="style10">
                                            <asp:TextBox ID="FaxNumber1TextBox" runat="server" CssClass="TextBoxGeneralRev" 
                                                Text='<%# Bind("FaxNumber1") %>' />
                                            <asp:Label ID="LabelOpt1" runat="server" CssClass="LabelGeneral" 
                                                Font-Bold="False" Font-Italic="True" ForeColor="#FF3300">Optional</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style11">
                                            <asp:Label ID="Label11" runat="server" CssClass="LabelGeneral" 
                                                Text="Fax Number 2 :"></asp:Label>
                                        </td>
                                        <td class="style10">
                                            <asp:TextBox ID="FaxNumber2TextBox" runat="server" CssClass="TextBoxGeneralRev" 
                                                Text='<%# Bind("FaxNumber2") %>' />
                                            <asp:Label ID="LabelOpt2" runat="server" CssClass="LabelGeneral" 
                                                Font-Bold="False" Font-Italic="True" ForeColor="#FF3300">Optional</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style11">
                                            <asp:Label ID="Label12" runat="server" CssClass="LabelGeneral" 
                                                Text="Web Adress :"></asp:Label>
                                        </td>
                                        <td class="style10">
                                            <asp:TextBox ID="WebAdressTextBox" runat="server" CssClass="TextBoxGeneralRev" 
                                                Text='<%# Bind("WebAdress") %>' />
                                            <asp:Label ID="LabelOpt3" runat="server" CssClass="LabelGeneral" 
                                                Font-Bold="False" Font-Italic="True" ForeColor="#FF3300">Optional</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style11">
                                            <asp:Label ID="Label13" runat="server" CssClass="LabelGeneral" Text="Email :"></asp:Label>
                                        </td>
                                        <td class="style10">
                                            <asp:TextBox ID="EmailAddressTextBox" runat="server" CssClass="TextBoxGeneralRev" 
                                                Text='<%# Bind("EmailAddress") %>' />
                                            <asp:Label ID="LabelOpt4" runat="server" CssClass="LabelGeneral" 
                                                Font-Bold="False" Font-Italic="True" ForeColor="#FF3300">Optional</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style11">
                                            <asp:Label ID="Label14" runat="server" CssClass="LabelGeneral" Text="Adress :"></asp:Label>
                                        </td>
                                        <td class="style10">
                                            <asp:TextBox ID="AdressTextBox" runat="server" CssClass="TextBoxGeneralRev" 
                                                Text='<%# Bind("Adress") %>' />
                                            <asp:Label ID="LabelOpt5" runat="server" CssClass="LabelGeneral" 
                                                Font-Bold="False" Font-Italic="True" ForeColor="#FF3300">Optional</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style11">
                                            <asp:Label ID="Label16" runat="server" CssClass="LabelGeneral" Text="Notes :"></asp:Label>
                                        </td>
                                        <td class="style10">
                                            <asp:TextBox ID="NotesTextBox" runat="server" CssClass="TextBoxGeneralRev" 
                                                Text='<%# Bind("Notes") %>' />
                                            <asp:Label ID="LabelOpt6" runat="server" CssClass="LabelGeneral" 
                                                Font-Bold="False" Font-Italic="True" ForeColor="#FF3300">Optional</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style11">
                                            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-success"
                                            Text="Insert" onclick="InsertButton_Click1" 
                                            ValidationGroup="SupplierEntry" CommandName="Insert" />
                                        </td>
                                        <td class="style10">
                                             </td>
                                    </tr>
                                </table>
                            </InsertItemTemplate>
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:FormView>
                        <asp:Panel  ID="hide"  runat="server">
                        <asp:SqlDataSource ID="SqlDataSourceSupplierEnter" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                            InsertCommand="INSERT INTO Table6_Supplier(SupplierID, SupplierName, PhoneNumber1, PhoneNumber2, FaxNumber1, FaxNumber2, WebAdress, EmailAddress, Adress, VAT_Free, Notes, CreatedBy, PersonCreated) VALUES (@SupplierID, @SupplierName, @PhoneNumber1, @PhoneNumber2, @FaxNumber1, @FaxNumber2, @WebAdress, @EmailAddress, @Adress, @VAT_Free, @Notes, @CreatedBy, @PersonCreated);SELECT SupplierID=SCOPE_IDENTITY()" 
                            oninserted="SqlDataSourceSupplierEnter_Inserted" 
                            
                            SelectCommand="SELECT [SupplierID], [SupplierName], [PhoneNumber1], [PhoneNumber2], [FaxNumber1], [FaxNumber2], [WebAdress], [EmailAddress], [Adress], [VAT_Free], [Notes], [UpdatedBy] FROM [Table6_Supplier]" 
                            oninserting="SqlDataSourceSupplierEnter_Inserting">
                            <InsertParameters>
                                <asp:Parameter Name="SupplierID" />
                                <asp:Parameter Name="CreatedBy" />
                                <asp:Parameter Name="PersonCreated" />
                            </InsertParameters>
                        </asp:SqlDataSource>
                         </asp:Panel>

        
                  <asp:Label ID="LabelPaymentTermsValidationNotification" runat="server" ForeColor="Red" Font-Bold="true" Visible="false">
                      Total of Payment Terms exceeds 100%. Please check it again.
                  </asp:Label>

            <table style="border: thin ridge #E0E0E0; background-color: #F5F5F5; margin:2px;" >
                <tr>
                    <td>
                        
                        <asp:Label ID="LabelInsert4" runat="server" CssClass="LabelContract" 
                            Text="ContractNo" Width="100px" />
                        
                    </td>
                    <td>
                        <asp:TextBox ID="ContractNoTextBox" runat="server" CssClass="TextBoxContract" 
                            Text='<%# Bind("ContractNo") %>' />
                    </td>
                    <td>
                        <asp:Label ID="LabelInsert10" runat="server" CssClass="LabelContract" 
                            Text="Template DOC File" Width="100px" />
                        <asp:TextBox ID="LinkToTemplatefile_DOCTextBox" runat="server" 
                            Text='<%# Bind("LinkToTemplatefile_DOC") %>' CssClass="hidepanel" />
                    </td>
                    <td>
                      <asp:FileUpload ID="FileUploadDOC" runat="server" CssClass="TextBoxContract" Visible="false" />
                      <asp:Button ID="ButtonUploadDOC" runat="server" CausesValidation="False" 
                        CssClass="btn btn-mini btn-default" onclick="ButtonUploadDOC_Click" Text="click to select your file" />
                      <asp:RequiredFieldValidator ID="RequiredFieldValidatorDOC" runat="server" 
                        ControlToValidate="LinkToTemplatefile_DOCTextBox" CssClass="LabelGeneral" 
                        Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                        <br />
                      <asp:Label ID="LabelInfoDOC" runat="server" CssClass="LabelGeneral" Visible="false"
                        style="font-weight: 700"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        
                        <asp:Label ID="LabelInsert5" runat="server" CssClass="LabelContract" 
                            Text="ContractDate" Width="100px" />
                        
                    </td>
                    <td>
                        
                        <asp:TextBox ID="ContractDateTextBox" runat="server" CssClass="TextBoxContract add_datepicker" />

                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                            ControlToValidate="ContractDateTextBox" CssClass="LabelGeneral" 
                            ErrorMessage="dd/mm/yyyy" Display="Dynamic" 
                            ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"></asp:RegularExpressionValidator>
                        
                    </td>
                    <td>
                        
                        <asp:Label ID="LabelInsertStartDate" runat="server" CssClass="LabelContract" 
                          Text="Start Date" Width="100px" />
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxStartDate" runat="server" CssClass="TextBoxContract add_datepicker" />

                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorStartDate" runat="server" 
                            ControlToValidate="TextBoxStartDate" CssClass="LabelGeneral" 
                            ErrorMessage="dd/mm/yyyy" Display="Dynamic" 
                            ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}">
                        </asp:RegularExpressionValidator>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorStartDate" runat="server" 
                                ControlToValidate="TextBoxStartDate" 
                                CssClass="LabelGeneral" Display="Dynamic"
                                ErrorMessage="Required">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        
                        <asp:Label ID="LabelInsert6" runat="server" CssClass="LabelContract" 
                            Text="Contract Value With VAT" Width="120px" />
                        
                    </td>
                    <td>
                        
                        <asp:TextBox ID="ContractValue_woVATTextBox" runat="server" CssClass="hidepanel" 
                            Text='<%# Bind("ContractValue_woVAT") %>' />

                        <asp:TextBox ID="TextBoxContractValue_withVAT" runat="server" 
                            CssClass="TextBoxContract" Text='<%# Bind("ContractValue_withVAT") %>' />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                            ControlToValidate="TextBoxContractValue_withVAT" CssClass="LabelGeneral" 
                            ErrorMessage="not valid number"  Display="Dynamic"
                            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="TextBoxContractValue_withVAT" 
                                CssClass="LabelGeneral" Display="Dynamic"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>
                        
                    </td>
                    <td>
                      <asp:Label ID="LabelInsertFinishDate" runat="server" CssClass="LabelContract" 
                        Text="Finish Date" Width="100px" />
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxFinishDate" runat="server" CssClass="TextBoxContract add_datepicker" />

                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorFinishDate" runat="server" 
                            ControlToValidate="TextBoxFinishDate" CssClass="LabelGeneral" 
                            ErrorMessage="dd/mm/yyyy" Display="Dynamic" 
                            ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}">
                        </asp:RegularExpressionValidator>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorFinishDate" runat="server" 
                                ControlToValidate="TextBoxFinishDate" 
                                CssClass="LabelGeneral" Display="Dynamic"
                                ErrorMessage="Required">
                        </asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelVAT" runat="server" CssClass="LabelContract" 
                            Text="VAT %" Width="100px" />
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxVAT" runat="server" 
                            CssClass="TextBoxContract" Text='<%# Bind("VATpercent") %>' />
                            <asp:FilteredTextBoxExtender ID="TextBoxVATpercent_FilteredTextBoxExtender" 
                                runat="server" FilterType="Numbers" TargetControlID="TextBoxVAT">
                            </asp:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="TextBoxVAT" CssClass="LabelGeneral" Display="Dynamic"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator1" runat="server"  Display="Dynamic"
                                ErrorMessage="range to be 0-20" ControlToValidate="TextBoxVAT" 
                                CssClass="LabelGeneral" MaximumValue="20" MinimumValue="0" Type="Double"></asp:RangeValidator>                             
                    </td>
                    <td style="border-top-style: solid; border-left-style: solid; border-top-width: thick; border-left-width: thick; border-top-color: #0000FF; border-left-color: #0000FF">
                        <asp:Label ID="LabelInsertAdvance" runat="server" CssClass="LabelContract" Text="Advance %" Width="100px" />
                    </td>
                    <td style="border-top-style: solid; border-right-style: solid; border-top-width: thick; border-right-width: thick; border-top-color: #0000FF; border-right-color: #0000FF">
                        <asp:TextBox ID="TextBoxAdvance" runat="server" CssClass="TextBoxContract" Text='<%# Bind("Advance") %>' />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorAdvance" runat="server" 
                            ControlToValidate="TextBoxAdvance" CssClass="LabelGeneral" Display="Dynamic" 
                            ErrorMessage="Required">
                        </asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidatorAdvance" runat="server" ControlToValidate="TextBoxAdvance" 
                            CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" 
                            MaximumValue="100" MinimumValue="0" Type="Double">
                        </asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelInsert7" runat="server" CssClass="LabelContract" Text="ContractCurrency" Width="100px" />
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownListCurrency" runat="server" >
                            <asp:ListItem Value="">-</asp:ListItem>
                            <asp:ListItem Value="Rub">Ruble</asp:ListItem>
                            <asp:ListItem Value="Dollar">Dollar</asp:ListItem>
                            <asp:ListItem Value="Euro">Euro</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DropDownListCurrency" CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </td>
                    <td style="border-left-style: solid; border-left-width: thick; border-left-color: #0000FF">
                        <asp:Label ID="LabelInsertInterim" runat="server" CssClass="LabelContract" Text="Interim %" Width="100px" />
                    </td>
                    <td style="border-right-style: solid; border-right-width: thick; border-right-color: #0000FF">
                        <asp:TextBox ID="TextBoxInterim" runat="server" CssClass="TextBoxContract" Text='<%# Bind("Interim")%>' />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorInterim" runat="server" 
                            ControlToValidate="TextBoxInterim" CssClass="LabelGeneral" Display="Dynamic" 
                            ErrorMessage="Required">
                        </asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidatorInterim" runat="server" ControlToValidate="TextBoxInterim" 
                            CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" 
                            MaximumValue="100" MinimumValue="0" Type="Double">
                        </asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelInsertCostCode" runat="server" CssClass="LabelContract" Text="Cost Code" Width="100px" />
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxCostCode" runat="server" Text='<%# Bind("CostCode") %>' />
                    </td>
                    <td style="border-left-style: solid; border-left-width: thick; border-left-color: #0000FF">
                        <asp:Label ID="LabelShipment" runat="server" CssClass="LabelContract" Text="Shipment %" Width="100px" />
                    </td>
                    <td style="border-right-style: solid; border-right-width: thick; border-right-color: #0000FF">
                        <asp:TextBox ID="TextBoxShipment" runat="server" CssClass="TextBoxContract" Text='<%# Bind("Shipment")%>' />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorShipment" runat="server" 
                            ControlToValidate="TextBoxShipment" CssClass="LabelGeneral" Display="Dynamic" 
                            ErrorMessage="Required">
                        </asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidatorShipment" runat="server" ControlToValidate="TextBoxShipment" 
                            CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" 
                            MaximumValue="100" MinimumValue="0" Type="Double">
                        </asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelRequestedBy" runat="server" CssClass="LabelContract" Text="Requested By" Width="100px" />
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxRequestedBy" runat="server" Text='<%# Bind("RequestedBy") %>' />
                    </td>
                    <td style="border-left-style: solid; border-left-width: thick; border-left-color: #0000FF">
                        <asp:Label ID="LabelDelivery" runat="server" CssClass="LabelContract" Text="Delivery %" Width="100px" />
                    </td>
                    <td style="border-right-style: solid; border-right-width: thick; border-right-color: #0000FF">
                        <asp:TextBox ID="TextBoxDelivery" runat="server" CssClass="TextBoxContract" Text='<%# Bind("Delivery")%>' />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDelivery" runat="server" 
                            ControlToValidate="TextBoxDelivery" CssClass="LabelGeneral" Display="Dynamic" 
                            ErrorMessage="Required">
                        </asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidatorDelivery" runat="server" ControlToValidate="TextBoxDelivery" 
                            CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" 
                            MaximumValue="100" MinimumValue="0" Type="Double">
                        </asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelInsert8" runat="server" CssClass="LabelContract" Text="ContractType" Width="100px" />
                    </td>
                    <td>
                        <asp:TextBox ID="ContractCurrencyTextBox" runat="server" 
                            CssClass="hidepanel" Text='<%# Bind("ContractCurrency") %>' />
                        <asp:DropDownList ID="DropDownListContractType" runat="server" >
                            <asp:ListItem Value="">-</asp:ListItem>
                            <asp:ListItem Value="Sub">Sub</asp:ListItem>
                            <asp:ListItem Value="Sup">Sup</asp:ListItem>
                            <asp:ListItem Value="Ser">Ser</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDropDownListContractType" runat="server" ControlToValidate="DropDownListContractType" CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>

                    </td>
                    <td style="border-left-style: solid; border-left-width: thick; border-left-color: #0000FF; border-bottom-style: solid; border-bottom-width: thick; border-bottom-color: #0000FF;">
                        <asp:Label ID="LabelInsertRetention" runat="server" CssClass="LabelContract" Text="Retention %" Width="100px" />
                        </td>
                    <td style="border-right-style: solid; border-right-width: thick; border-right-color: #0000FF; border-bottom-style: solid; border-bottom-width: thick; border-bottom-color: #0000FF;">
                        <asp:TextBox ID="TextBoxRetention" runat="server" CssClass="TextBoxContract" Text='<%# Bind("Retention")%>' />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorRetention" runat="server" 
                            ControlToValidate="TextBoxRetention" CssClass="LabelGeneral" Display="Dynamic" 
                            ErrorMessage="Required">
                        </asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidatorRetention" runat="server" ControlToValidate="TextBoxRetention" 
                            CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" 
                            MaximumValue="100" MinimumValue="0" Type="Double">
                        </asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelInsertBudget" runat="server" CssClass="LabelContract" Text="Budget" Width="100px" Visible="false" />
                    </td>
                    <td style="padding-top:5px;">

                        <%--THIS SECTION IS NOT FUNCTIONING ANYTHING. BUDGET 0.1 FROM CODE BEHIND--%>
                        <div class="hide">
                            <asp:TextBox ID="TextBoxBudget" runat="server" 
                                CssClass="TextBoxContract" Text='<%# Bind("Budget")%>' />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorBudget" runat="server" 
                                ControlToValidate="TextBoxBudget" CssClass="LabelGeneral" 
                                ErrorMessage="not valid number"  Display="Dynamic"
                                ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                            </asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorBudget" runat="server" 
                                    ControlToValidate="TextBoxBudget" 
                                    CssClass="LabelGeneral" Display="Dynamic"
                                    ErrorMessage="Required">
                            </asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidatorBudget" runat="server" ControlToValidate="TextBoxBudget" CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Should be greater than ZERO" ValueToCompare="0" Operator="GreaterThan" Type="Double">
                            </asp:CompareValidator>

                            <br /><br />

                            <asp:TextBox ID="TextBoxBudgetPDF" runat="server" CssClass="hidepanel" 
                                Text='<%# Bind("BudgetLinkToPDF")%>' />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorBudgetPDF" runat="server" Enabled="false"
                                    ControlToValidate="TextBoxBudgetPDF" 
                                    CssClass="LabelGeneral" Display="Dynamic"
                                    ErrorMessage="Budget PDF required">
                            </asp:RequiredFieldValidator>

                            <asp:FileUpload ID="FileUploadBudgetPDF" runat="server" CssClass="TextBoxContract" />
                            <asp:Button ID="ButtonBudgetPDFUpload" runat="server" CssClass="btn btn-mini btn-default"  CausesValidation="False" OnClick="ButtonBudgetPDFUpload_Click"
                                Text="Upload Budget PDF" />
                            <asp:Label ID="LabelInfoBudgetPDF" runat="server" CssClass="LabelGeneral" style="font-weight: 700"></asp:Label>   
                        </div>
                        <%-------------------------THIS SECTION IS NOT FUNCTIONING ANYTHING. BUDGET 0.1 FROM CODE BEHIND--%>

                    </td>
                    <td>

                            <asp:Label ID="LabelInsertPenalty" runat="server" CssClass="LabelContract" Text="Penalty To Mercury" Width="100px" />

                    </td>
                        <td>
                            <asp:DropDownList ID="DropDownListPenalty" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListPenalty_SelectedIndexChanged">
                                <asp:ListItem Value="">-</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Value="0">No</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPenalty" runat="server" ControlToValidate="DropDownListPenalty" CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>

                            <br />

                        </td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="LabelInsert9" runat="server" CssClass="LabelContract" Text="ContractDescription" Width="120px" />
                    </td>
                    <td class="style7">
                        <asp:TextBox ID="ContractTypeTextBox" runat="server" CssClass="hidepanel" Text='<%# Bind("ContractType") %>' />
                        <asp:TextBox ID="ContractDescriptionTextBox" runat="server" CssClass="TextBoxContract" Height="75px" Text='<%# Bind("ContractDescription") %>' TextMode="MultiLine" Width="280px" />
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorContractDescriptionTextBox" runat="server" ControlToValidate="ContractDescriptionTextBox" CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                    <td>

                        <asp:Label ID="LabelInsertPenaltyNote" runat="server" CssClass="LabelContract" Text="Penalty To Mercury Note" Width="100px" />
                        </td>
                        <td class="style7">

                            <asp:TextBox ID="TextBoxPenaltyNote" runat="server" CssClass="TextBoxContract" Height="75px" Text='<%# Bind("PenaltiesNote")%>' TextMode="MultiLine" Width="280px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPenaltyNote" runat="server" ControlToValidate="TextBoxPenaltyNote" CssClass="LabelGeneral" Display="Dynamic" Enabled="False" ErrorMessage="Required"></asp:RequiredFieldValidator>
                        </td>
                </tr>
                <tr style="height:100px;" >
                    <td>
                        <asp:Label ID="LabelInsertDeliveryTerms" runat="server" CssClass="LabelContract" Text="Delivery Terms" Width="100px" />
                    </td>
                    <td>
                            
                        <asp:TextBox ID="TextBoxDeliveryTerms" runat="server" CssClass="TextBoxContract" Height="75px" Text='<%# Bind("DeliveryTerms") %>' TextMode="MultiLine" Width="280px" />
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDeliveryTerms" runat="server" ControlToValidate="TextBoxDeliveryTerms" CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Required">
                        </asp:RequiredFieldValidator>
                            
                    </td>
                    <td>
                        <asp:Label ID="LabelInsertPenaltyToSupplier" runat="server" CssClass="LabelContract" Text="Penalty To Supplier" Width="100px" />
                    </td>
                    <td>
                        <br />
                        <asp:DropDownList ID="DropDownListPenaltyToSupplier" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListPenaltyToSupplier_SelectedIndexChanged">
                            <asp:ListItem Value="">-</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPenaltyToSupplier" runat="server" ControlToValidate="DropDownListPenaltyToSupplier" CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="height:100px;">
                    <td>
                        <asp:Label ID="LabelInsertGuarantePeriod" runat="server" CssClass="LabelContract" Text="Guarantee Period" Width="100px" />
                    </td>
                    <td>

                        <asp:TextBox ID="TextBoxGuaranteePeriod" runat="server" CssClass="TextBoxContract" Height="75px" Text='<%# Bind("GuaranteePeriod") %>' TextMode="MultiLine" Width="280px" />
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorGuaranteePeriod" runat="server" ControlToValidate="TextBoxGuaranteePeriod" CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Required">
                        </asp:RequiredFieldValidator>

                    </td>
                    <td>
                        <asp:Label ID="LabelInsertPenaltyNoteToSupplier" runat="server" CssClass="LabelContract" Text="Penalty To Supplier Note" Width="100px" />
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxPenaltyNoteToSupplier" runat="server" CssClass="TextBoxContract" Height="75px" Text='<%# Bind("PenaltiesNote")%>' TextMode="MultiLine" Width="280px" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPenaltyToSupplierNote" runat="server" ControlToValidate="TextBoxPenaltyNoteToSupplier" CssClass="LabelGeneral" Display="Dynamic" Enabled="False" ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>

            <br />

<%--          <asp:Panel ID="PanelNominated" runat="server" >
           <span style="font-style:italic; color:Green; font-size:large;" >
           Nominated Subcontractor, <br /> No Commercial Offer Required
           </span>
          </asp:Panel>--%>

<%--          <asp:Panel ID="PanelOffer" runat="server" >

          <table>
            <tr>
             <td style="vertical-align:top; ">

            <table style="font-size:11px; border: thin ridge #E0E0E0; background-color: #F5F5F5; margin:2px;" >
               <tr>
                 <th colspan="2">
                   2 Commercial Offers Required</th>
                 <tr>
                   <td >
                     
                       <span class="LabelContract">Supplier (INN):</span>
                   </td>
                   <td>
                     <asp:TextBox ID="TextBoxOfferSupplier" runat="server" Autopostback="true" placeholder="Type Number or Text"
                       CssClass="TextBoxContract" ontextchanged="TextBoxOfferSupplier_TextChanged" />
                     <div ID="DivTextBoxOfferSupplier" runat="server" class="LabelGeneral">
                     </div>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidatorOfferSupplier" 
                       runat="server" ControlToValidate="TextBoxOfferSupplier" CssClass="LabelGeneral" 
                       Display="Dynamic" ErrorMessage="Required" ValidationGroup="Offer"></asp:RequiredFieldValidator>
                     <asp:AutoCompleteExtender ID="AutoCompleteExtenderOfferSupplier" runat="server" 
                       CompletionInterval="0" CompletionListElementID="DivTextBoxOfferSupplier" 
                       CompletionSetCount="12" MinimumPrefixLength="0" 
                       onclientshown="SetAutoCompleteWidthOfferSupplier" ServiceMethod="SupplierEdit" 
                       ServicePath="AutoComplete.asmx" TargetControlID="TextBoxOfferSupplier">
                     </asp:AutoCompleteExtender>
                     <br />
                     <asp:Label ID="LabelSupplierNameOffer" runat="server" BackColor="#003366" 
                       CssClass="LabelSupplierPOcreate" Font-Bold="True" ForeColor="White"></asp:Label>
                     <asp:Label ID="LabelSupplierErrorOffer" runat="server" CssClass="LabelGeneral" 
                       Font-Bold="True" ForeColor="#FF3300"></asp:Label>

                   </td>
                 </tr>
                 <tr>
                   <td >
                     <span class="LabelContract">Offer Value With VAT:</span>
                   </td>
                   <td>
                     <asp:TextBox ID="TextBoxOfferValueWithVAT" runat="server" CssClass="TextBoxContract" ></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidatorOfferValueWithVAT" 
                       runat="server" ControlToValidate="TextBoxOfferValueWithVAT" 
                       CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="not valid number" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="Offer">
                 </asp:RegularExpressionValidator>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidatorOfferValueWithVAT" 
                       runat="server" ControlToValidate="TextBoxOfferValueWithVAT" 
                       CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Required" 
                       ValidationGroup="Offer">
                 </asp:RequiredFieldValidator>

                   </td>
                 </tr>
                 <tr>
                   <td >
                       <span class="LabelContract">Currency:</span>
                   </td>
                   <td>
                     <asp:DropDownList ID="DropDownListCurrencyOffer" runat="server" >
                       <asp:ListItem Value="">-</asp:ListItem>
                       <asp:ListItem Value="Rub">Ruble</asp:ListItem>
                       <asp:ListItem Value="Dollar">Dollar</asp:ListItem>
                       <asp:ListItem Value="Euro">Euro</asp:ListItem>
                     </asp:DropDownList>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidatorCurrencyOffer" 
                       runat="server" ControlToValidate="DropDownListCurrencyOffer" 
                       CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Required" 
                       ValidationGroup="Offer">
                  </asp:RequiredFieldValidator>

                   </td>
                 </tr>
                 <tr>
                   <td >
                       <span class="LabelContract">Attachment:<span style="color:Yellow; font-weight:bold;">ZIP File</span></span>
                   </td>
                   <td>
                     <asp:FileUpload ID="FileUploadOffer" runat="server" 
                       CssClass="TextBoxContract" />
                     <asp:Button ID="ButtonUploadOffer" runat="server" CausesValidation="false" CssClass="btn btn-mini btn-default"
                       Font-Size="10px" onclick="ButtonUploadOffer_Click" Text="Upload" />
                     <br />
                     <asp:Label ID="LabelInfoOfferUpload" runat="server" CssClass="LabelGeneral" 
                       style="font-weight: 700"></asp:Label>
                     <asp:TextBox id="TextBoxOfferFilePath" runat="server" CssClass="hidepanel" ></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxOfferFilePath" 
                       runat="server" ControlToValidate="TextBoxOfferFilePath" 
                       CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Required" 
                       ValidationGroup="Offer">
                     </asp:RequiredFieldValidator>

                   </td>
                 </tr>
                 <tr>
                   <td colspan="2" style="text-align:center;">
                     <asp:Button ID="ButtonInsertOffer" runat="server" BackColor="#66FF99" CssClass="btn btn-mini btn-success"
                       Font-Size="10px" onclick="ButtonInsertOffer_Click" Text="Insert Offer" 
                       ValidationGroup="Offer" />
                   </td>
                 </tr>
               </tr>
             </table>
             </td>
             <td style="vertical-align:top; padding-left: 20px;">

              <asp:TextBox ID="TextBoxOfferCount" runat="server" CssClass="hidepanel"  />
              <asp:CompareValidator ID="CompareValidatorOfferCount" runat="server" CssClass="LabelGeneral" 
              Operator="GreaterThanEqual" ControlToValidate="TextBoxOfferCount"
               ErrorMessage="2 Commercial Offers Required" Display="Dynamic" ValueToCompare="2">
              </asp:CompareValidator>

              <asp:GridView ID="GridViewOffers" runat="server" AutoGenerateColumns="False" 
                 DataKeyNames="OfferId" DataSourceID="SqlDataSourceOffers" 
                 EnableModelValidation="True" HeaderStyle-CssClass="LabelContract" 
                 CssClass="GridViewOffersRow" onrowcommand="GridViewOffers_RowCommand" 
                 onrowdatabound="GridViewOffers_RowDataBound" >
                 <Columns>
                   <asp:TemplateField>
                    <ItemTemplate>
                     <asp:Button id="ButtonDelete" runat="server" CommandName="delete" Text="Delete" CssClass="btn btn-mini btn-danger"
                     OnClientClick="return confirm('Are you sure you want to delete this Offer?');"
                     CauseValidation="False"/>
                    </ItemTemplate>
                   </asp:TemplateField>
                   <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" 
                   HeaderStyle-Width="150px" ItemStyle-Width="150px"
                     SortExpression="SupplierName" />
                   <asp:BoundField DataField="OfferValueWithVAT" HeaderText="Offer Value With VAT" 
                   HeaderStyle-Width="60px" ItemStyle-Width="60px"
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
                 SelectCommand="SELECT [OfferId]
                                    ,[Scenario]
                                    ,[SupplierName]
                                    ,[OfferValueWithVAT]
                                    ,[Currency]
                                    ,[Attachment]
                                FROM [Table_ScenarioOffers]
                                INNER JOIN Table6_Supplier 
                                ON Table6_Supplier.SupplierID = Table_ScenarioOffers.SupplierID
                                WHERE [Scenario] = @Scenario"
                   DeleteCommand="DELETE From Table_ScenarioOffers WHERE OfferID = @OfferID">
                 <SelectParameters>
                   <asp:Parameter DefaultValue="-" Name="Scenario" />
                 </SelectParameters>
               </asp:SqlDataSource>
              </td>
            </tr>
          </table>
          </asp:Panel>
--%>
        </InsertItemTemplate>
        <ItemTemplate>

        </ItemTemplate>
        <PagerStyle BackColor="#CCCCCC" ForeColor="Red" />
    </asp:FormView>

</asp:Content>
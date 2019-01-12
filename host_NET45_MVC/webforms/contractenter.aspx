<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="contractenter.aspx.vb" Inherits="contractenter_2IASDFK" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>contract</title>

    <script language="javascript"  type="text/javascript">

      function SetAutoCompleteWidth(source, EventArgs) {
        var a
        a = document.getElementById('<%=(((Master.FindControl("MainContent")).FindControl("FormViewContract")).FindControl("AutoCompleteDiv2")).ClientID %>');
        a.style.width = '441px';
      }

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
            width: 141px;
        }



    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


    <div style="text-align: center"><asp:Label ID="LabelEntryMessage" runat="server" Visible="false" Text="Your entries have been recived successfully!"  ForeColor="#33CC33" Font-Bold="True" Font-Size="12px" /></div>
    <div style="text-align: center"><asp:Label ID="LabelProjectSupplierComtability" runat="server" Visible="false" Text="Project and Supplier must be compatible"  ForeColor="#FF0066" Font-Bold="True" Font-Size="12px" /></div>
    <asp:FormView ID="FormViewContract" runat="server" 
        DataSourceID="SqlDataSourceContract" AllowPaging="True" 
        DataKeyNames="ContractID" EmptyDataText="Empty" DefaultMode="Insert">

        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" 
            LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" />

        <EditItemTemplate>

        </EditItemTemplate>
        <InsertItemTemplate>
        
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
                        <asp:DropDownList ID="DropDownListPrj" runat="server" Autopostback="true"
                            DataSourceID="SqlDataSourcePrj" DataTextField="ProjectName" 
                            DataValueField="ProjectID"  
                            ondatabound="DropDownListPrj_DataBound" 
                            onselectedindexchanged="DropDownListPrj_SelectedIndexChanged" Width="200px">
                        </asp:DropDownList>

                <asp:Panel ID="PanelPrj" runat="server" CssClass="hidepanel">
                    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                    SelectCommand="IF EXISTS (SELECT     TOP (100) PERCENT dbo.aspnet_Users.UserName
                                    FROM         dbo.aspnet_Users INNER JOIN
                                                          dbo.aspnet_UsersInRoles ON dbo.aspnet_Users.UserId = dbo.aspnet_UsersInRoles.UserId AND 
                                                          dbo.aspnet_Users.UserId = dbo.aspnet_UsersInRoles.UserId INNER JOIN
                                                          dbo.aspnet_Roles ON dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId AND dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId
                                    WHERE     (dbo.aspnet_Roles.RoleName = N'ContractSupportGirl' OR
                                                          dbo.aspnet_Roles.RoleName = N'ContractLeadGirls') AND (dbo.aspnet_Users.UserName = @username))

                                    SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID)) 
                                                          AS ProjectName
                                    FROM         dbo.Table1_Project INNER JOIN
                                                          dbo.Table_Contract_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Contract_User_Junction.ProjectID
                                    WHERE     (dbo.Table1_Project.NewGeneration <> 1) AND (dbo.Table_Contract_User_Junction.UserName = @username)
                                    ORDER BY ProjectName

                                    ELSE

                                    SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID)) 
                                                          AS ProjectName, dbo.Table_Contract_User_Junction.UserName
                                    FROM         dbo.Table1_Project INNER JOIN
                                                          dbo.Table_Contract_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Contract_User_Junction.ProjectID
                                    WHERE     (dbo.Table1_Project.NewGeneration <> 1) AND (dbo.Table1_Project.ProjectID <> 999) AND (dbo.Table_Contract_User_Junction.UserName = @username)
                                    ORDER BY ProjectName">
                        <SelectParameters>
                            <asp:Parameter Name="username" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </asp:Panel>

                    </td>
                <td>
                        <asp:CompareValidator ID="CompareValidatorDropDownListPrj" runat="server" CssClass="LabelGeneral" Operator="NotEqual"
                            ErrorMessage="Required" ControlToValidate="DropDownListPrj" Display="Dynamic" ValueToCompare="0">
                        </asp:CompareValidator>
                </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelProjectOnPTS" runat="server" CssClass="LabelContract" visible="false"
                            Text="Project On PTS" Width="100px" />
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownListProjectOnPTS" runat="server" visible="false"
                            DataSourceID="SqlDataSourceProjectOnPTS" DataTextField="ProjectName" 
                            DataValueField="ProjectID" Width="200px">
                        </asp:DropDownList>

                        <asp:CompareValidator ID="CompareValidatorProjectOnPTS" runat="server" CssClass="LabelGeneral" Operator="NotEqual" Enabled="false"
                            ErrorMessage="Required" ControlToValidate="DropDownListProjectOnPTS" Display="Dynamic" ValueToCompare="0">
                        </asp:CompareValidator>

                    <asp:SqlDataSource ID="SqlDataSourceProjectOnPTS" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                    SelectCommand=" 
                                    SELECT ProjectID, ProjectName FROM (
                                    SELECT 0 AS ProjectID, N'_Select Project On PTS' AS ProjectName

                                    UNION ALL

                                    SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID)) 
                                                          AS ProjectName
                                    FROM         dbo.Table1_Project 
                                    WHERE     (dbo.Table1_Project.CurrentStatus = 1 ) AND (dbo.Table1_Project.ProjectID <> 999) )
                                    AS DataSource
									group by ProjectID , ProjectName 
                                    ORDER BY ProjectName ASC
                        ">
                        <SelectParameters>
                            <asp:Parameter Name="username" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                    </td>
                    <td>

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

                        <asp:DropDownList ID="DropLspLr" runat="server"  
                            DataSourceID="SqlDataSourceSuplr" DataTextField="SupplierName" 
                            DataValueField="SupplierName" Visible="True" CssClass="hidepanel">
                        </asp:DropDownList>

                    <asp:Panel ID="Panel1" runat="server" CssClass="hidepanel" >
                        <asp:SqlDataSource ID="SqlDataSourceSuplr" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                            SelectCommand="SELECT [SupplierID], [SupplierName] FROM [Table6_Supplier] WHERE ([SupplierID] = @SupplierID)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="x" DefaultValue="_" 
                                    Name="SupplierID" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                     </asp:Panel> 
                    </td>
                <td>
                        <asp:Button ID="ButtonEnterSupplier" runat="server" CssClass="btn btn-mini btn-default" 
                            onclick="ButtonEnterSupplier_Click" Text="Add New Supplier" 
                            CausesValidation="False" />
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
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-success"
                CommandName="Insert" Text="Insert" />
            <asp:LinkButton ID="InsertCancelButton" runat="server" CssClass="btn btn-mini btn-default" Visible="false"
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
                                            <font style="font-size:10px;">IP/ИП : </font><asp:CheckBox ID="CheckBoxPersonSupplier" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBoxPersonSupplier_CheckedChanged" />
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
                                            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                                            Text="Insert" onclick="InsertButton_Click1" CssClass="btn btn-mini btn-success"
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
        
            <table style="border: thin ridge #E0E0E0; background-color: #F5F5F5; margin:2px;" >
                <tr>
                    <td>
                        
                        <asp:Label ID="LabelInsert4" runat="server" CssClass="LabelContract" 
                            Text="ContractNo" Width="100px" />
                        
                    </td>
                    <td>
                        
                        <asp:TextBox ID="ContractNoTextBox" runat="server" CssClass="TextBoxContract" 
                            Text='<%# Bind("ContractNo") %>' />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorContractNoTextBox" runat="server" 
                            ControlToValidate="ContractNoTextBox" CssClass="LabelGeneral" Display="Dynamic"
                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        
                        <asp:Label ID="LabelInsert10" runat="server" CssClass="LabelContract" 
                            Text="Template DOC or ZIP File" Width="100px" />
                        <asp:TextBox ID="LinkToTemplatefile_DOCTextBox" runat="server" 
                            Text='<%# Bind("LinkToTemplatefile_DOC") %>' CssClass="hidepanel" />
                        
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
                        
                        <asp:FileUpload ID="FileUploadDOC" runat="server" CssClass="TextBoxContract" />
                        
                        <asp:Button ID="ButtonUploadDOC" runat="server" CssClass="btn btn-mini btn-default"  CausesValidation="False" onclick="ButtonUploadDOC_Click"
                            Text="Upload" />
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        
                        <asp:Label ID="LabelInsert6" runat="server" CssClass="LabelContract" 
                            Text="Contract Value Exc. VAT" Width="120px" />
                        
                    </td>
                    <td>
                        
                        <asp:TextBox ID="ContractValue_woVATTextBox" runat="server" 
                            CssClass="TextBoxContract" Text='<%# Bind("ContractValue_woVAT") %>' />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ControlToValidate="ContractValue_woVATTextBox" CssClass="LabelGeneral" 
                            ErrorMessage="not valid number"  Display="Dynamic"
                            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>
                        
                    </td>
                    <td>
                <asp:Label ID="LabelInfoDOC" runat="server" CssClass="LabelGeneral" 
                    style="font-weight: 700"></asp:Label>                        
                        </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelInsert7" runat="server" CssClass="LabelContract" 
                            Text="ContractCurrency" Width="100px" />
                    </td>
                    <td>
                        <asp:TextBox ID="ContractCurrencyTextBox" runat="server" 
                            CssClass="hidepanel" Text='<%# Bind("ContractCurrency") %>' />
                        <asp:DropDownList ID="DropDownListCurrency" runat="server" >
                            <asp:ListItem Value="">-</asp:ListItem>
                            <asp:ListItem Value="Rub">Ruble</asp:ListItem>
                            <asp:ListItem Value="Dollar">Dollar</asp:ListItem>
                            <asp:ListItem Value="Euro">Euro</asp:ListItem>
                            <asp:ListItem Value="GBP">GBP</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelInsert8" runat="server" CssClass="LabelContract" 
                            Text="ContractType" Width="100px" />
                    </td>
                    <td>
                        <asp:TextBox ID="ContractTypeTextBox" runat="server" CssClass="hidepanel" 
                            Text='<%# Bind("ContractType") %>' />
                        <asp:DropDownList ID="DropDownListContractType" runat="server" >
                            <asp:ListItem Value="">-</asp:ListItem>
                            <asp:ListItem Value="Sub">Sub</asp:ListItem>
                            <asp:ListItem Value="Sup">Sup</asp:ListItem>
                            <asp:ListItem Value="Ser">Ser</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDropDownListContractType" runat="server" 
                            ControlToValidate="DropDownListContractType" CssClass="LabelGeneral"  Display="Dynamic"
                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelInsert9" runat="server" CssClass="LabelContract" 
                            Text="ContractDescription" Width="120px" />
                    </td>
                    <td>
                        <asp:TextBox ID="ContractDescriptionTextBox" runat="server" 
                            CssClass="TextBoxContract" Height="75px" 
                            Text='<%# Bind("ContractDescription") %>' TextMode="MultiLine" 
                            Width="350px" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorContractDescriptionTextBox" runat="server" 
                            ControlToValidate="ContractDescriptionTextBox" CssClass="LabelGeneral"  Display="Dynamic"
                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                 <td>
                        <asp:Label ID="LabelWarrantyPeriod" runat="server" CssClass="LabelContract" 
                            Text="Warranty Period" Width="120px" Visible="false" />
                 </td>
                 <td>
                        <asp:TextBox ID="TextBoxWarrantyPeriod" runat="server" 
                            Text='<%# Bind("ContractGivenTo") %>' Visible="false" />
                 </td>
                 <td>
                 </td>
                </tr>
            </table>

            <br />

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
                        <asp:TextBox ID="TextBoxRetention" runat="server" CssClass="TextBoxContract" Text='<%# Bind("Retention")%>' Width="50px" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorRetention" runat="server" 
                            ControlToValidate="TextBoxRetention" CssClass="LabelGeneral" Display="Dynamic" 
                            ErrorMessage="Required">
                        </asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidatorRetention" runat="server" ControlToValidate="TextBoxRetention" 
                            CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" 
                            MaximumValue="100" MinimumValue="0" Type="Double">
                        </asp:RangeValidator>

<%--                        <asp:DropDownList ID="DropDownListRetention" runat="server" 
                            CssClass="TextBoxContract">
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
                        </asp:DropDownList>--%>
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

        <br />

            <asp:Panel ID="panelClientAddData" runat="server" Visible="false">
                <table style="border: thin ridge #E0E0E0; background-color: #F5F5F5; margin: 2px;">
                    <tr>
                        <td style="width: 250px;">
                            <asp:Label ID="LabelDeliveryTerm" runat="server" CssClass="LabelContract"
                                Text="Delivery Terms" Width="100px" />
                            <br />
                            <asp:TextBox ID="TextBoxDeliveryTerm" runat="server"
                                CssClass="TextBoxContract" Height="75px"
                                Text='<%# Bind("DeliveryTerms")%>' TextMode="MultiLine"
                                Width="200px" />

                        </td>
                        <td style="width: 250px;">
                            <asp:Label ID="LabelPenaltyMercury" runat="server" CssClass="LabelContract"
                                Text="Penalty To Mercury" Width="150px" />
                            <asp:CheckBox ID="CheckBoxPenaltyToMercury" runat="server" CssClass="LabelContract" OnCheckedChanged="CheckBoxPenaltyToMercury_CheckedChanged" AutoPostBack="true"
                                Checked='<%# Bind("Penalties")%>' />
                        </td>
                        <td style="width: 250px;">
                            <asp:Label ID="LabelPenaltyMercuryNote" runat="server" CssClass="LabelContract"
                                Text="Penalty To Mercury Note" Width="150px" />
                            <br />
                            <asp:TextBox ID="TextBoxPenaltyMercuryNote" runat="server"
                                CssClass="TextBoxContract" Height="75px"
                                Text='<%# Bind("PenaltiesNote")%>' TextMode="MultiLine"
                                Width="200px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPenaltyMercuryNote" runat="server" ErrorMessage="Required" Display="Dynamic" Enabled="false"
                            CssClass="LabelGeneral" ControlToValidate="TextBoxPenaltyMercuryNote" ></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                </table>
            </asp:Panel>

        </InsertItemTemplate>
        <PagerStyle BackColor="#CCCCCC" ForeColor="Red" />
    </asp:FormView>
    <asp:SqlDataSource ID="SqlDataSourceContract" runat="server"
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
        SelectCommand="SELECT [ContractID], [ProjectID], [PO_No], [ContractNo], [ContractDate], [ContractValue_woVAT], [SignBySupplier], [SentToSupplier], [LinkToTemplatefile_DOC], [ContractType], [ContractDescription], [SupplierID], [ContractCurrency], [SignByMercury], [CollectionBySupplier], [LinkToPDFcopy], [ArchivedByMercury], [Retention], [Note] FROM [Table_Contracts]"
        InsertCommand="INSERT INTO Table_Contracts
                                   (ProjectID
                                   ,PO_No
                                   ,ContractNo
                                   ,ContractDate
                                   ,ContractValue_woVAT
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
                                   ,AttachmentExist, ContractGivenTo, DeliveryTerms, Penalties, PenaltiesNote)
                        VALUES
                                   (@ProjectID
                                   ,@PO_No
                                   ,@ContractNo
                                   ,@ContractDate
                                   ,@ContractValue_woVAT
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
                                   ,@AttachmentExist, @ContractGivenTo, @DeliveryTerms, @Penalties, @PenaltiesNote);SELECT @ID=SCOPE_IDENTITY()" OnInserted="SqlDataSourceContract_Inserted">
        <InsertParameters>
          <asp:Parameter Direction="Output" Name="ID" Type="Int32" />
        </InsertParameters>
    </asp:SqlDataSource>

</asp:Content>
<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" EnableEventValidation="false"
CodeFile="addendumenterNew.aspx.vb" Inherits="_addendumenter_2REV___" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="POdetailsForEmail.ascx" tagname="SeperateControl" tagprefix="uc1" %>

<%@ Register src="WebUserControl_AddendumEmailBody.ascx" tagname="SeperateControl2" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Addendums</title>

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
        
        .styleToLeft
        {
            text-align: left;
            width: 300px;            
        }
        
        .styleToLeft2
        {
            text-align: left;
            width: 150px;            
        }        
        
        .auto-style1 {
            width: 150px;
            height: 26px;
        }
        .auto-style2 {
            width: 250px;
            height: 26px;
        }
        
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

<asp:panel ID="PanelContainerForNextPage"  runat="server" CssClass="hidepanel">
   <uc1:SeperateControl ID="POdetailsForEmail" runat="server" />
    <uc1:SeperateControl2 ID="WebUserControl_AddendumEmailBody" runat="server" />
   <asp:Label ID="LabelGridViewPagingStatusOnAddendum" runat="server" ></asp:Label>   
   <asp:Label ID="LabelGridViewPageSizeOnAddendum" runat="server" ></asp:Label>         
   <asp:Label ID="LabelGridViewPageNumberOnAddendum" runat="server" ></asp:Label>      
</asp:panel>

   <asp:Label ID="LabelContractIDonAddendum" runat="server" Visible="false" ></asp:Label>   
    <asp:FormView ID="FormViewAddendums" runat="server" 
        DataSourceID="SqlDataSourceAddendums" AllowPaging="True" 
        DataKeyNames="AddendumID" EmptyDataText="Empty" DefaultMode="Insert">

        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" 
            LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" />

        <EditItemTemplate>

        </EditItemTemplate>
        <InsertItemTemplate>
        
            <table style="margin:2px;">
                <tr>
                    <td style="vertical-align: top">
                        <table style="border: thin ridge #E0E0E0; background-color: #F5F5F5; margin:2px;">
                            <tr>
                                <td>
                                    <asp:Label ID="LabelInsert17" runat="server" CssClass="LabelContract" 
                                        Text="Project" Width="100px" />
                                </td>
                                <td class="styleToLeft2">
                                    <asp:Label ID="LabelProjectName" runat="server" CssClass="LabelGeneral" 
                                         />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LabelInsert18" runat="server" CssClass="LabelContract" 
                                        Text="Po No" Width="100px" />
                                </td>
                                <td class="styleToLeft2">
                                    <asp:Label ID="LabelPOno" runat="server" CssClass="LabelGeneral" 
                                         />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LabelInsert19" runat="server" CssClass="LabelContract" 
                                        Text="SupplierName" Width="100px" />
                                </td>
                                <td class="styleToLeft2">
                                    <asp:Label ID="LabelSupplierName" runat="server" CssClass="LabelGeneral" 
                                        />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:DropDownList ID="DropDownListAddendumType" runat="server" autopostback="true"
                          onselectedindexchanged="DropDownListAddendumType_SelectedIndexChanged">
                            <asp:ListItem Value="0" Selected="True" >Select Addendum Type</asp:ListItem>
                            <asp:ListItem Value="1">Regular Addendum</asp:ListItem>
                            <asp:ListItem Value="2">Replace Addendum</asp:ListItem>
                            <asp:ListItem Value="3">Zero Value Addendum</asp:ListItem>
                        </asp:DropDownList>
                          <asp:CompareValidator ID="CompareValidatorAddType" runat="server" CssClass="LabelGeneral" 
                          Operator="NotEqual" ControlToValidate="DropDownListAddendumType"
                           ErrorMessage="Required" Display="Dynamic" ValueToCompare="0">
                          </asp:CompareValidator>
                    </td>
                    <td >
                        <table style="border: thin ridge #E0E0E0; background-color: #F5F5F5; margin:2px;">
                            <tr>
                                <td>
                                    <asp:Label ID="LabelInsert20" runat="server" CssClass="LabelContract" 
                                        Text="ContractNo" Width="120px" />
                                </td>
                                <td class ="styleToLeft">
                                    <asp:Label ID="LabelContractName" runat="server" CssClass="LabelGeneral" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LabelInsert21" runat="server" CssClass="LabelContract" 
                                        Text="ContractDate" Width="120px" />
                                </td>
                                <td class="styleToLeft">
                                    <asp:Label ID="LabelContractDate" runat="server" CssClass="LabelGeneral" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LabelInsert22" runat="server" CssClass="LabelContract" 
                                        Text="Contract Value Inc. VAT" Width="120px" />
                                </td>
                                <td class="styleToLeft">
                                    <asp:Label ID="LabelContractValue" runat="server" CssClass="LabelGeneral" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LabelInsert23" runat="server" CssClass="LabelContract" 
                                        Text="ContractCurrency" Width="120px" />
                                </td>
                                <td class="styleToLeft">
                                    <asp:Label ID="LabelCurrency" runat="server" CssClass="LabelGeneral" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LabelInsert24" runat="server" CssClass="LabelContract" 
                                        Text="ContractType" Width="120px" />
                                </td>
                                <td class="styleToLeft">
                                    <asp:Label ID="LabelContractType" runat="server" CssClass="LabelGeneral" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LabelInsert25" runat="server" CssClass="LabelContract" 
                                        Text="ContractDescription" Width="120px" />
                                </td>
                                <td class="styleToLeft">
                                    <asp:Label ID="LabelContractDescription" runat="server" 
                                        CssClass="LabelGeneral" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True"  
                            CommandName="Insert" CssClass="btn btn-mini btn-success" Text="Insert" />
                        <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" 
                            CommandName="Cancel" CssClass="btn btn-mini btn-danger" Text="Cancel" Visible="true" />
                    </td>
                    <td>

                    </td>
                </tr>
            </table>
            <br />
        
                  <asp:Label ID="LabelPaymentTermsValidationNotification" runat="server" ForeColor="Red" Font-Bold="true" Visible="false">
                      Total of Payment Terms exceeds 100%. Please check it again.
                  </asp:Label>

            <table style="border: thin ridge #E0E0E0; background-color: #F5F5F5" >
                <tr>
                    <td>

    <%--
                        <asp:Label ID="LabelCostCode" runat="server" CssClass="LabelContract" 
                            Text="Cost Code" Width="100px" />
    --%>
                        
                    </td>
                    <td>

                      <asp:DropDownList ID="DropDownListCostCode" runat="server" 
                          DataSourceID="SqlDataSourceCostCode" DataTextField="CostCode_Description" 
                          DataValueField="CostCode"  Width="500px" Font-Size="11px"
                          AutoPostback="True" Visible="false" CssClass="ddl_fxfnt"
                        onselectedindexchanged="DropDownListCostCode_SelectedIndexChanged" >
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSourceCostCode" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                SelectCommand="SelectCostCodeNonFinance" 
                                SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:Parameter Name="ProjectID" DefaultValue="0" Type="Int16" />
                                        <asp:Parameter Name="UserName" Type="String" />
                                    </SelectParameters>
                       </asp:SqlDataSource>

                       <asp:TextBox ID="TextBoxCostCodeError" runat="server"  
                        Text="Valid" CssClass="hidepanel">
                       </asp:TextBox>
                       <br />
                            <asp:CompareValidator ID="CompareValidatorCostCode" runat="server" 
                                    ControlToValidate = "TextBoxCostCodeError" Visible="false"
                                    Type = "String" CssClass="LabelGeneral"
                                    Operator="Equal" Display="Dynamic"
                                ErrorMessage="Cost Code must be 10 character" ValueToCompare="Valid">
                            </asp:CompareValidator>

                            <asp:CompareValidator ID="CompareValidatorCostCodeBudget" runat="server"
                                    ControlToValidate = "DropDownListCostCode" Visible="false"
                                    Type = "String" CssClass="LabelGeneral"
                                    Operator="NotEqual" Enabled="true"
                                ErrorMessage="please select costcode" ValueToCompare="0" Display="Dynamic">
                            </asp:CompareValidator>

                    </td>
                    <td>
                        
                        <asp:Label ID="LabelInsert10" runat="server" CssClass="LabelContract" 
                          Text="Template DOC File" Width="100px" />
                    </td>
                    <td>
                      <asp:FileUpload ID="FileUploadDOC" runat="server" CssClass="TextBoxContract" Visible="false" />
                      <asp:Button ID="ButtonUploadDOC" runat="server" CausesValidation="False" 
                        CssClass="btn btn-mini btn-default" onclick="ButtonUploadDOC_Click" Text="click to select your file" />
                      <asp:RequiredFieldValidator ID="RequiredFieldValidatorDOC" runat="server" 
                        ControlToValidate="LinkToTemplatefile_DOCTextBox" CssClass="LabelGeneral" 
                        Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                        <br />
                      <asp:Label ID="LabelInfoDOC" runat="server" CssClass="LabelGeneral"  Visible="false" 
                        style="font-weight: 700"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td>
                    <asp:Label ID="LabelInsert4" runat="server" CssClass="LabelContract" 
                      Text="AddnNo" Width="100px" />
                  </td>
                  <td>
                    <asp:TextBox ID="ContractNoTextBox" runat="server" CssClass="TextBoxContract" 
                      Text='<%# Bind("AddendumNo") %>' />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorContractNoTextBox" 
                      runat="server" ControlToValidate="ContractNoTextBox" CssClass="LabelGeneral" 
                      Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                  </td>
                  <td>
                    <asp:TextBox ID="LinkToTemplatefile_DOCTextBox" runat="server" 
                      CssClass="hidepanel" Text='<%# Bind("AddendumLinkToTemplatefile_DOC") %>' />
                    <asp:Label ID="LabelInsertStartDate" runat="server" CssClass="LabelContract" 
                      Text="Start Date" Width="100px" />
                  </td>
                  <td>
                    <asp:TextBox ID="TextBoxStartDate" runat="server" CssClass="TextBoxContract add_datepicker" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorStartDate" 
                      runat="server" ControlToValidate="TextBoxStartDate" CssClass="LabelGeneral" 
                      Display="Dynamic" ErrorMessage="dd/mm/yyyy" 
                      ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}">
                        </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorStartDate" runat="server" 
                      ControlToValidate="TextBoxStartDate" CssClass="LabelGeneral" Display="Dynamic" 
                      ErrorMessage="Required">
                        </asp:RequiredFieldValidator>
                  </td>
                </tr>
                <tr>
                    <td>
                        
                        <asp:Label ID="LabelInsert5" runat="server" CssClass="LabelContract" 
                            Text="AddnDate" Width="100px" />
                        
                    </td>
                    <td>
                        
                        <asp:TextBox ID="ContractDateTextBox" runat="server" CssClass="TextBoxContract add_datepicker" />

                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                            ControlToValidate="ContractDateTextBox" CssClass="LabelGeneral" 
                            ErrorMessage="dd/mm/yyyy" Display="Dynamic" 
                            ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"></asp:RegularExpressionValidator>
                        
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorDate" 
                      runat="server" ControlToValidate="ContractDateTextBox" CssClass="LabelGeneral" 
                      Display="Dynamic" ErrorMessage="Required" Enabled="false" ></asp:RequiredFieldValidator>

                    </td>
                    <td>
                        
                        <asp:Label ID="LabelInsertFinishDate" runat="server" CssClass="LabelContract" 
                          Text="Finish Date" Width="100px" />
                    </td>
                    <td>
                      <asp:TextBox ID="TextBoxFinishDate" runat="server" CssClass="TextBoxContract add_datepicker" />
                      <asp:RegularExpressionValidator ID="RegularExpressionValidatorFinishDate" 
                        runat="server" ControlToValidate="TextBoxFinishDate" CssClass="LabelGeneral" 
                        Display="Dynamic" ErrorMessage="dd/mm/yyyy" 
                        ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}">
                        </asp:RegularExpressionValidator>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidatorFinishDate" 
                        runat="server" ControlToValidate="TextBoxFinishDate" CssClass="LabelGeneral" 
                        Display="Dynamic" ErrorMessage="Required">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        
                        <asp:Label ID="LabelAddendumValue_WithVAT" runat="server" CssClass="LabelContract" 
                            Text="Addendum Value With VAT" Width="120px" />
                        
                    </td>
                    <td>
                        <asp:TextBox ID="ContractValue_woVATTextBox" runat="server" CssClass="hidepanel"
                          Text='<%# Bind("AddendumValue_woVAT") %>' />
                        <%-- --------------------------------------------------Addendum Value EXC VAT --%>

                        <asp:TextBox ID="TextBoxAddendumValue_withVAT" runat="server" 
                          CssClass="TextBoxContract" Text='<%# Bind("AddendumValue_WithVAT") %>' />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorContractValue_withVAT" 
                          runat="server" ControlToValidate="TextBoxAddendumValue_withVAT" 
                          CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="not valid number" 
                          ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                          ControlToValidate="TextBoxAddendumValue_withVAT" CssClass="LabelGeneral" 
                          Display="Dynamic" ErrorMessage="Required">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidatorAddValueWithVAT" runat="server" CssClass="LabelGeneral" 
                            ControlToValidate="TextBoxAddendumValue_withVAT" ErrorMessage="Must be grater than zero" 
                            Operator="GreaterThan" ValueToCompare="0" Display="Dynamic"></asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidatorReplaceAddendum" runat="server" CssClass="LabelGeneral" Type="Double" 
                            ControlToValidate="TextBoxAddendumValue_withVAT" ErrorMessage="It cannot be less than Total Invoice value" 
                            Operator="GreaterThanEqual" ValueToCompare="0" Display="Dynamic"></asp:CompareValidator>
                    </td>
                    <td style="border-top-style: solid; border-top-width: thick; border-top-color: #0000FF; border-left-style: solid; border-left-width: thick; border-left-color: #0000FF">
                        <asp:Label ID="LabelInsertAdvance" runat="server" CssClass="LabelContract" Text="Advance %" Width="100px" />
                    </td>
                    <td style="border-top-style: solid; border-top-width: thick; border-top-color: #0000FF; border-right-style: solid; border-right-width: thick; border-right-color: #0000FF">
                        <asp:TextBox ID="TextBoxAdvance" runat="server" CssClass="TextBoxContract" Text='<%# Bind("Advance") %>' />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorAdvance" runat="server" ControlToValidate="TextBoxAdvance" 
                            CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidatorAdvance" runat="server" ControlToValidate="TextBoxAdvance" 
                            CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" MinimumValue="0" Type="Double">
                            </asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                 <td>
                         <asp:Label ID="LabelVATpercent" runat="server" CssClass="LabelContract" Text="VATpercent" Width="120px" />
                    </td>
                 <td>
                        <%-- --------------------------------------------------Addendum Value WITH VAT --%>
                        <asp:TextBox ID="TextBoxVAT" runat="server" CssClass="TextBoxContract" Text='<%# Bind("VATpercent") %>' />
                        <asp:FilteredTextBoxExtender ID="TextBoxVATpercent_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="TextBoxVAT">
                        </asp:FilteredTextBoxExtender>
                        <asp:RangeValidator ID="RangeValidatorTextBoxVAT" runat="server" ControlToValidate="TextBoxVAT" CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-20" MaximumValue="20" MinimumValue="0" Type="Double">
                        </asp:RangeValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxVAT" runat="server" ControlToValidate="TextBoxVAT" CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Required">
                        </asp:RequiredFieldValidator>
                 </td>
                 <td style="border-left-style: solid; border-left-width: thick; border-left-color: #0000FF;">
                     <asp:Label ID="LabelInsertInterim" runat="server" CssClass="LabelContract" Text="Interim %" Width="100px" />
                 </td>
                  <td style="border-right-style: solid; border-right-width: thick; border-right-color: #0000FF">
                        <asp:TextBox ID="TextBoxInterim" runat="server" CssClass="TextBoxContract" Text='<%# Bind("Interim")%>' />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorInterim" runat="server" ControlToValidate="TextBoxInterim" 
                            CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidatorInterim" runat="server" ControlToValidate="TextBoxInterim" 
                            CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" MinimumValue="0" Type="Double">
                            </asp:RangeValidator>
                  </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelRequestedBy" runat="server" CssClass="LabelContract" Text="Requested By" Width="100px" />
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownListRequestedBy" runat="server" DataSourceID="SqlDataSourceRequestedBy" DataTextField="NameSurname" DataValueField="username" ondatabound="DropDownListRequestedBy_DataBound">
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidatorRequested" runat="server" ControlToValidate="DropDownListRequestedBy" CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Required" Operator="NotEqual" Type="String" ValueToCompare="0">
                            </asp:CompareValidator>
                    </td>
                    <td style="border-left-style: solid; border-left-width: thick; border-left-color: #0000FF">
                        <asp:Label ID="LabelInsertShipment" runat="server" CssClass="LabelContract" Text="Shipment %" Width="100px" />
                    </td>
                    <td style="border-right-style: solid; border-right-width: thick; border-right-color: #0000FF">
                        <asp:TextBox ID="TextBoxShipment" runat="server" CssClass="TextBoxContract" Text='<%# Bind("Shipment")%>' />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorShipment" runat="server" ControlToValidate="TextBoxShipment" 
                            CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidatorShipment" runat="server" ControlToValidate="TextBoxShipment" 
                            CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" MinimumValue="0" Type="Double">
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
                    <td style="border-left-style: solid; border-left-width: thick; border-left-color: #0000FF">
                        <asp:Label ID="LabelInsertDelivery" runat="server" CssClass="LabelContract" Text="Delivery %" Width="100px" />
                    </td>
                    <td style="border-right-style: solid; border-right-width: thick; border-right-color: #0000FF">
                        <asp:TextBox ID="TextBoxDelivery" runat="server" CssClass="TextBoxContract" Text='<%# Bind("Delivery")%>' />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDelivery" runat="server" ControlToValidate="TextBoxDelivery" 
                            CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidatorDelivery" runat="server" ControlToValidate="TextBoxDelivery" 
                            CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" MinimumValue="0" Type="Double">
                            </asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td style="border-left-style: solid; border-left-width: thick; border-left-color: #0000FF; border-bottom-style: solid; border-bottom-width: thick; border-bottom-color: #0000FF">
                        <asp:Label ID="LabelInsertRetention" runat="server" CssClass="LabelContract" Text="Retention %" Width="100px" />
                    </td>
                    <td style="border-bottom-style: solid; border-bottom-width: thick; border-right-width: thick; border-right-style: solid; border-bottom-color: #0000FF; border-right-color: #0000FF">
                        <asp:TextBox ID="TextBoxRetention" runat="server" CssClass="TextBoxContract" Text='<%# Bind("AddendumRetention")%>' />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddendumRetention" runat="server" ControlToValidate="TextBoxRetention" 
                            CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidatorAddendumRetention" runat="server" ControlToValidate="TextBoxRetention" 
                            CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" MinimumValue="0" Type="Double"></asp:RangeValidator>
                        </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td><%-- --------------------------------------------------------------Addendum VAT --%>
                        <asp:SqlDataSource ID="SqlDataSourceRequestedBy" runat="server" ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand=" select rtrim(username) as username, RTRIM(NameSurname) AS NameSurname  from Table_PersonRequestPo where ProjectID = @ProjectID
                                 order by NameSurname asc ">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="0" Name="ProjectID" Type="Int16" />
                            </SelectParameters>
                        </asp:SqlDataSource>
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
                    </td>
                </tr>
                <tr>
                 <td>
                        <asp:Label ID="LabelInsert9" runat="server" CssClass="LabelContract" Text="AddnDescription" Width="120px" />
                    </td>
                 <td>
                        <%-- --------------------------------------------------------------Addendum VAT --%>
                        <asp:TextBox ID="ContractDescriptionTextBox" runat="server" CssClass="TextBoxContract" Height="75px" Text='<%# Bind("AddendumDescription") %>' TextMode="MultiLine" Width="350px" />
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorContractDescriptionTextBox" runat="server" ControlToValidate="ContractDescriptionTextBox" CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                 </td>
                 <td>
                     <asp:Label ID="LabelInsertPenaltyNote" runat="server" CssClass="LabelContract" Text="Penalty To Mercury Note" Width="100px" />
                 </td>
                  <td>
                      <asp:TextBox ID="TextBoxPenaltyNote" runat="server" CssClass="TextBoxContract" Height="75px" Text='<%# Bind("PenaltiesNote")%>' TextMode="MultiLine" Width="280px" />
                      <br />
                      <asp:RequiredFieldValidator ID="RequiredFieldValidatorPenaltyMercuryNote" runat="server" ControlToValidate="TextBoxPenaltyNote" CssClass="LabelGeneral" Display="Dynamic" Enabled="False" ErrorMessage="Required"></asp:RequiredFieldValidator>
                  </td>
                </tr>
                <tr>
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
                        <asp:DropDownList ID="DropDownListPenaltyToSupplier" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListPenaltyToSupplier_SelectedIndexChanged">
                            <asp:ListItem Value="">-</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPenaltyToSupplier" runat="server" ControlToValidate="DropDownListPenaltyToSupplier" CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
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
                      <asp:TextBox ID="TextBoxPenaltyNoteToSupplier" runat="server" CssClass="TextBoxContract" Height="75px" Text='<%# Bind("PenaltiesToSupplierNote")%>' TextMode="MultiLine" Width="280px" />
                      <br />
                      <asp:RequiredFieldValidator ID="RequiredFieldValidatorPenaltySupplierNote" runat="server" ControlToValidate="TextBoxPenaltyNoteToSupplier" CssClass="LabelGeneral" Display="Dynamic" Enabled="False" ErrorMessage="Required"></asp:RequiredFieldValidator>
                  </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>

            <br />


<!-- ------------THIS IS HIDDEN, NOT IN USE IN NEW DESIGN ----------->
<!-- ------------THIS IS HIDDEN, NOT IN USE IN NEW DESIGN ----------->
<!-- ------------THIS IS HIDDEN, NOT IN USE IN NEW DESIGN ----------->
<!-- ------------THIS IS HIDDEN, NOT IN USE IN NEW DESIGN ----------->
<!-- ------------THIS IS HIDDEN, NOT IN USE IN NEW DESIGN ----------->
<asp:Panel ID="hidePanel" runat="server" CssClass="hidepanel" >

            <table style="border: thin ridge #E0E0E0; background-color: #F5F5F5" >
                <tr>
                    <td class="style1">
                    </td>
                    <td class="style3">
                        <asp:Label ID="LabelInsert12" runat="server" CssClass="LabelContract" 
                            Text="SignedBySupplier" Width="100px" />
                        <asp:CheckBox ID="SignBySupplierCheckBox" runat="server" CssClass="LabelContract"
                            Checked='<%# Bind("AddendumSignBySupplier") %>' />
                    </td>
                    <td >
                        <asp:Label ID="LabelInsert13" runat="server" CssClass="LabelContract" 
                            Text="SignedByMercury" Width="100px" />
                        <asp:CheckBox ID="SignByMercuryCheckBox" runat="server" CssClass="LabelContract"
                            Checked='<%# Bind("AddendumSignByMercury") %>' />
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
                            Text='<%# Bind("AddendumLinkToPDFcopy") %>' />
                        <asp:FileUpload ID="FileUploadPDF" runat="server" CssClass="TextBoxContract" />
                        <asp:Button ID="ButtonUploadPDF" runat="server" CssClass="btn btn-mini btn-default"  CausesValidation="False" onclick="ButtonUploadPDF_Click"
                            Text="Upload" />
                    </td>
                </tr>
            </table>
        
        <br />
        
            <table style="border: thin ridge #E0E0E0; background-color: #F5F5F5" >
                <tr>
                    <td class="style4">
                        <asp:Label ID="LabelInsert15" runat="server" CssClass="LabelContract" 
                            Text="Collected By Supplier" Width="150px" />
                        <asp:CheckBox ID="CollectionBySupplierCheckBox" runat="server" CssClass="LabelContract"
                            Checked='<%# Bind("AddendumCollectionBySupplier") %>' />
                    </td>
                    <td class="style5">
                        <asp:Label ID="LabelInsert16" runat="server" CssClass="LabelContract" 
                            Text="Archived By Mercury" Width="150px" />
                        <asp:CheckBox ID="ArchivedByMercuryCheckBox" runat="server"  CssClass="LabelContract"
                            Checked='<%# Bind("AddendumArchivedByMercury") %>' />
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
                            Text='<%# Bind("AddendumNote") %>' TextMode="MultiLine" 
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

        </InsertItemTemplate>
        <ItemTemplate>

        </ItemTemplate>
        <PagerStyle BackColor="#CCCCCC" ForeColor="Red" />
    </asp:FormView>
    <asp:SqlDataSource ID="SqlDataSourceAddendums" runat="server"
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
        SelectCommand="SELECT * FROM [Table_Addendums]"
        InsertCommand="INSERT INTO [Table_Addendums]
           ([ContractID]
           ,[AddendumNo]
           ,[AddendumDate]
           ,[AddendumValue_woVAT]
           ,[AddendumValue_WithVAT]
           ,[VATpercent]
           ,[AddendumDescription]
           ,[AddendumLinkToTemplatefile_DOC]
           ,[AddendumSignBySupplier]
           ,[AddendumSignByMercury]
           ,[AddendumCollectionBySupplier]
           ,[AddendumLinkToPDFcopy]
           ,[AddendumArchivedByMercury]
           ,[CreatedBy]
           ,[PersonCreated]
           ,[AddendumRetention]
           ,[AddendumNote]
           ,AttachmentExist
           ,CostCode
           ,Budget
           ,BudgetLinkToPDF
           ,NewGeneration
           ,RequestedBy
           ,Penalties
           ,PenaltiesNote
           ,PenaltiesToSupplier
           ,PenaltiesToSupplierNote
           ,Advance
           ,Interim
           ,Shipment
           ,Delivery
           ,StartDate
           ,FinishDate
           ,DeliveryTerms
           ,GuaranteePeriod
           ,AddendumTypes
           ,Scenario)
     VALUES
           (@ContractID
           ,@AddendumNo
           ,@AddendumDate
           ,@AddendumValue_woVAT
           ,@AddendumValue_WithVAT
           ,@VATpercent
           ,@AddendumDescription
           ,@AddendumLinkToTemplatefile_DOC
           ,@AddendumSignBySupplier
           ,@AddendumSignByMercury
           ,@AddendumCollectionBySupplier
           ,@AddendumLinkToPDFcopy
           ,@AddendumArchivedByMercury
           ,@CreatedBy
           ,@PersonCreated
           ,@AddendumRetention
           ,@AddendumNote
           ,@AttachmentExist
           ,@CostCode
           ,@Budget
           ,@BudgetLinkToPDF
           ,1
           ,@RequestedBy
           ,@Penalties
           ,@PenaltiesNote
           ,@PenaltiesToSupplier
           ,@PenaltiesToSupplierNote
           ,@Advance
           ,@Interim
           ,@Shipment
           ,@Delivery
           ,@StartDate
           ,@FinishDate
           ,@DeliveryTerms
           ,@GuaranteePeriod
           ,@AddendumTypes
           ,@Scenario)
           ;SELECT @ID=SCOPE_IDENTITY() ">

    <InsertParameters>
     <asp:Parameter Name="CostCode" Type="String" />
     <asp:Parameter Direction="Output" Name="ID" Type="Int32" />
    </InsertParameters>

    </asp:SqlDataSource>
        
    </ContentTemplate>
 </asp:UpdatePanel> 
</asp:Content>



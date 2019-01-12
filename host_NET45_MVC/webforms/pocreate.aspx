<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="pocreate.aspx.vb" Inherits="pocreateMOREapr" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Open New Purchase Order</title>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />


    <script language="javascript" type="text/javascript">

        function pageLoad() {
        }

        function SetAutoCompleteWidth(source, EventArgs) {
            var a
            a = document.getElementById('<%=(((Master.FindControl("MainContent")).FindControl("FormViewPO")).FindControl("AutoCompleteDiv")).ClientID %>');
            a.style.width = '441px';
        }

        function SupplierInsert() {
            var target1
            var target2

            target1 = document.getElementById('<%=((((Master.FindControl("MainContent")).FindControl("FormViewPO")).FindControl("FormViewSupplier")).FindControl("SupplierIDTextBox")).ClientID %>');
            target2 = document.getElementById('<%=((((Master.FindControl("MainContent")).FindControl("FormViewPO")).FindControl("FormViewSupplier")).FindControl("SupplierNameTextBox")).ClientID %>');

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
        #AutoCompleteDiv {
            width: 64px;
        }

        #AutoCompleteDiv0 {
            width: 184px;
        }

    </style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

<div >

    <div>
        <asp:Label ID="LabelMessageToKsenia" runat="server" Font-Size="X-Large"
            ForeColor="Red" Text="Temporarily blocked." Visible="False"></asp:Label>
    </div>

            <asp:TextBox ID="TextOsman" runat="server" CssClass="hidepanel"></asp:TextBox>

            <asp:FormView ID="FormViewPO" runat="server" DataKeyNames="PO_No" CssClass="table"
                DataSourceID="SqlDataSourcePO"
                DefaultMode="Insert">
                <InsertItemTemplate>

                    <div class="form-horizontal">
                    <div class="col-xs-6" >
                        <div class="form-group">
                            <asp:Label ID="LabelProjectID" runat="server" AssociatedControlID="DropDownListProject" Text="Project:" CssClass="col-xs-3 control-label"></asp:Label>
                            <div class="col-xs-6 " >
                                <asp:DropDownList ID="DropDownListProject" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="DropDownListProject_SelectedIndexChanged"
                                    CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-1 " >
                                <label class="btn btn-mini btn-info pull-left" id="pick-recent-projects">pick from recent projects</label>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="LabelPOno" runat="server" AssociatedControlID="PO_NoTextBox" CssClass="col-xs-3 control-label" Text="PO_No: "></asp:Label>
                            <div class="col-xs-4">
                                <asp:TextBox ID="PO_NoTextBox" runat="server" CssClass="form-control"
                                    ReadOnly="True" Text='<%# Bind("PO_No") %>' />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                    ControlToValidate="PO_NoTextBox"
                                    ErrorMessage="Required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="LabelSupplier" runat="server" Text="Supplier :" AssociatedControlID="SupplierIDTextBox" CssClass="col-xs-3 control-label"></asp:Label>
                            <div class="col-xs-4">
                                <asp:TextBox ID="SupplierIDTextBox" runat="server" AutoPostBack="True" placeholder="INN Number of Supplier"
                                    OnTextChanged="SupplierIDTextBox_TextChanged"
                                    Text='<%# Bind("SupplierID") %>' CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                    ControlToValidate="SupplierIDTextBox" Display="Dynamic"
                                    ErrorMessage="Required"></asp:RequiredFieldValidator>
                                <div id="AutoCompleteDiv"
                                    class="LabelGeneral" runat="server">
                                </div>
                                <asp:FilteredTextBoxExtender ID="TextBox1_FilteredTextBoxExtender"
                                    runat="server" FilterType="Numbers" TargetControlID="SupplierIDTextBox">
                                </asp:FilteredTextBoxExtender>
                                <asp:AutoCompleteExtender ID="TextBox1_AutoCompleteExtender" runat="server"
                                    CompletionInterval="0" CompletionListElementID="AutoCompleteDiv"
                                    CompletionSetCount="12" MinimumPrefixLength="0"
                                    OnClientShown="SetAutoCompleteWidth" ServiceMethod="haydi"
                                    ServicePath="AutoComplete.asmx" TargetControlID="SupplierIDTextBox">
                                </asp:AutoCompleteExtender>

                                <asp:Label ID="LabelShowSupplier" runat="server" CssClass="label label-success"></asp:Label>
                                <asp:Label ID="LabelSplrError" runat="server" CssClass="label label-danger"></asp:Label>
                            </div>
                            <div class="col-xs-2" >
                                <asp:Button ID="ButtonEnterSupplier" runat="server" CssClass="btn btn-success btn-mini"
                                    OnClick="ButtonEnterSupplier_Click" Text="Add New Supplier"
                                    UseSubmitBehavior="False" CausesValidation="False" />

                            </div>
                            <div class="col-xs-1" >
                                <label class="btn btn-mini btn-info pull-left" id="pick-recent-suppliers">pick from recent suppliers</label>
                            </div>

                        </div>

                        <div class="form-group">
                            <asp:Label ID="LabelDescription" runat="server" Text="Description :" AssociatedControlID="DescriptionTextBox" CssClass="col-xs-3 control-label"></asp:Label>

                            <div class="col-xs-6">
                                <asp:TextBox ID="DescriptionTextBox" runat="server" Rows="4" placeholder="Description of Purchase Order"
                                    Text='<%# Bind("Description") %>' TextMode="MultiLine"
                                    CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                    ControlToValidate="DescriptionTextBox"
                                    ErrorMessage="Required"></asp:RequiredFieldValidator>
                            </div>

                        </div>

                        <div class="form-group">

                            <asp:Label ID="LabelPOdate" runat="server" Text="PO Date :" AssociatedControlID="PO_DateTextBox" CssClass="col-xs-3 control-label"></asp:Label>
                            <div class="col-xs-4" >

                                <div class="input-group">
                                    <asp:TextBox ID="PO_DateTextBox" runat="server" CssClass="form-control add_datepicker" data-date-format="dd/mm/yyyy" placeholder="Date dd/mm/yyyy" />
										<span class="input-group-addon">
										   <i class="fa fa-calendar bigger-110"></i>
										</span>
                                </div>

                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                    ControlToValidate="PO_DateTextBox"
                                    ErrorMessage="dd/mm/yyyy" Display="Dynamic"
                                    ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPODate" runat="server" Display="Dynamic"
                                    ErrorMessage="Required" ControlToValidate="PO_DateTextBox"></asp:RequiredFieldValidator>

                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="LabelCostCode" runat="server" Text="Cost Code :" AssociatedControlID="DropDownListCostCode" CssClass="col-xs-3 control-label"></asp:Label>
                            <div class="col-xs-8" style="font-family:Consolas !important;">

                                <asp:DropDownList ID="DropDownListCostCode" runat="server"
                                    DataSourceID="SqlDataSourceCostCode" DataTextField="CostCode_Description"
                                    DataValueField="CostCode" 
                                    CssClass="form-control"
                                    OnSelectedIndexChanged="DropDownListCostCode_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>

                                <asp:TextBox ID="TextBoxCostCodeError" runat="server" Text="Valid" CssClass="hidepanel"></asp:TextBox>

                                <asp:CompareValidator ID="CompareValidatorCostCode" runat="server"
                                    ControlToValidate="TextBoxCostCodeError"
                                    Type="String" CssClass="LabelGeneral"
                                    Operator="Equal" Display="Dynamic"
                                    ErrorMessage="Cost Code must be 10 character" ValueToCompare="Valid">
                                </asp:CompareValidator>

                                <asp:CompareValidator ID="CompareValidatorCostCodeBudget" runat="server"
                                    ControlToValidate="DropDownListCostCode"
                                    Type="String" CssClass="LabelGeneral"
                                    Operator="NotEqual" Enabled="true"
                                    ErrorMessage="please select costcode" ValueToCompare="0" Display="Dynamic">
                                </asp:CompareValidator>

                            </div>
                       </div>

                        <div class="form-group">
                             <asp:Label ID="LabelPOdate0" runat="server" Text="Total Price Inc. VAT :" AssociatedControlID="TotalPriceTextBox" CssClass="col-xs-3 control-label" ></asp:Label>
                            <div class="col-xs-3">
                            <asp:TextBox ID="TotalPriceTextBox" runat="server" placeholder="With VAT" 
                                Text='<%# Bind("TotalPrice") %>' CssClass="form-control" />         
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="TotalPriceTextBox"  Display="Dynamic"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="TotalPriceTextBox"  Display="Dynamic"
                                ErrorMessage="not valid number" 
                                ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                                </asp:RegularExpressionValidator>                            
                            </div>
                            <div class="col-xs-2" >

                                <asp:DropDownList ID="DropDownListCurrency" runat="server" 
                                    CssClass="form-control">
                                    <asp:ListItem Value="Rub">Rub</asp:ListItem>
                                    <asp:ListItem Value="Dollar">$</asp:ListItem>
                                    <asp:ListItem Value="Euro">€</asp:ListItem>
                                </asp:DropDownList>

                            </div>

                            <div class="col-xs-2">
                                <asp:TextBox ID="TextBoxVATpercent" runat="server" placeholder="VAT %"
                                    Text='<%# Bind("VATpercent") %>' CssClass="form-control"></asp:TextBox>                            
                                <asp:FilteredTextBoxExtender ID="TextBoxVATpercent_FilteredTextBoxExtender" 
                                    runat="server" FilterType="Numbers" TargetControlID="TextBoxVATpercent">
                                </asp:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="TextBoxVATpercent"  Display="Dynamic"
                                    ErrorMessage="Required"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator1" runat="server"  Display="Dynamic"
                                    ErrorMessage="range to be 0-20" ControlToValidate="TextBoxVATpercent" 
                                     MaximumValue="20" MinimumValue="0" Type="Double"></asp:RangeValidator>                             

                            </div>

                            <div class="col-xs-1">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CssClass="btn btn-primary"
                                CommandName="Insert"   onclick="InsertButton_Click"  Text="Insert" />
                            </div>

                        </div>

                        <div class="form-group">
                            <asp:Label ID="LabelRequestedBy" runat="server" Text="Who Req? :" CssClass="col-xs-3 control-label" AssociatedControlID="DropDownListRequestedBy" Visible="false"></asp:Label>

                            <div class="col-xs-6">
                                <asp:DropDownList ID="DropDownListRequestedBy" runat="server"  Visible="false"
                                    CssClass="col-xs-2 form-control"  
                                    ondatabound="DropDownListRequestedBy_DataBound" 
                                    DataSourceID = "SqlDataSourceRequestedBy"
                                    DataTextField="NameSurname" DataValueField="username">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSourceRequestedBy" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                    SelectCommand=" select rtrim(username) as username, RTRIM(NameSurname) AS NameSurname  from Table_PersonRequestPo where ProjectID = @ProjectID and Active = 1
                                     order by NameSurname asc ">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="DropDownListProject" DefaultValue=0 
                                            Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <asp:CompareValidator ID="CompareValidatorRequested" runat="server" 
                                        ControlToValidate = "DropDownListRequestedBy"
                                        Type = "String" 
                                        Operator="NotEqual" Display="Dynamic"
                                    ErrorMessage="Required" ValueToCompare="0">
                                </asp:CompareValidator>
                            </div>

                        </div>
                    
                    </div>

                    <div class="col-xs-6" >

                        <asp:FormView ID="FormViewSupplier" runat="server" DataKeyNames="SupplierID" CssClass="table"
                            DataSourceID="SqlDataSourceSupplierEnter" DefaultMode="Insert" 
                            Visible="False" OnItemInserting="FormViewSupplier_ItemInserting" style="background-color:lightgreen;">

                            <InsertItemTemplate>

                             <div class="form-horizontal" >
                                 <div class="col-xs-12" >
                                     <div class="form-group">
                                         <asp:Label ID="Label6" runat="server" Text="Supplier ID :" AssociatedControlID="SupplierIDTextBox" CssClass="col-xs-3 control-label"></asp:Label>
                                         <div class="col-xs-4">
                                            <asp:TextBox ID="SupplierIDTextBox" runat="server" CssClass="form-control"
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
                                            <asp:Label ID="LabelSupplierMessage" runat="server" CssClass="label label-danger" 
                                                Text="Dublicating supplier. Transaction terminated." Visible="false"></asp:Label>
                                         </div>
                                        <div class ="col-xs-3"> 
                                            <div class="checkbox" > 
                                                <label>
                                                    <asp:CheckBox ID="CheckBoxPersonSupplier" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBoxPersonSupplier_CheckedChanged" />
                                                    IP/ИП : 
                                                </label> 
                                            </div > 
                                        </div>
                                     </div>
                                     <div class="form-group">
                                         <asp:Label ID="Label7" runat="server" CssClass="col-xs-3 control-label" AssociatedControlID="SupplierNameTextBox" Text="Supplier Name :"></asp:Label>
                                         <div class="col-xs-4">
                                            <asp:TextBox ID="SupplierNameTextBox" runat="server" CssClass="form-control"
                                                Text='<%# Bind("SupplierName") %>' />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorSupplierName" runat="server"  Display="Dynamic"
                                                ErrorMessage="Only latin letters allowed without any special characters" ControlToValidate="SupplierNameTextBox"
                                                CssClass="LabelGeneral" ValidationExpression="^[0-9a-zA-Z ]+$" ValidationGroup="SupplierEntry"> 
                                            </asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorSupplierName" runat="server" ControlToValidate="SupplierNameTextBox"
                                                ErrorMessage="Required" Display="Dynamic" ValidationGroup="SupplierEntry" CssClass="LabelGeneral">
                                            </asp:RequiredFieldValidator>
                                         </div>
                                     </div>
                                     <div class="form-group">
                                         <asp:Label ID="Label15" runat="server" CssClass="col-xs-3 control-label" AssociatedControlID="VAT_FreeCheckBox2"  Text="VAT Free :"></asp:Label>
                                         <div class="col-xs-4">
                                            <div class="checkbox" > 
                                                <label>
                                                 <asp:CheckBox ID="VAT_FreeCheckBox2" runat="server" Checked='<%# Bind("VAT_Free") %>'  />
                                                </label> 
                                            </div > 
                                         </div>
                                     </div>

                                     <div class="form-group">
                                            <asp:Label ID="Label8" runat="server" CssClass="col-xs-3 control-label" AssociatedControlID="PhoneNumber1TextBox" Text="Phone 1 :"></asp:Label>
                                         <div class="col-xs-4">
                                            <asp:TextBox ID="PhoneNumber1TextBox" runat="server" CssClass="form-control" style="margin-left: 0px" Text='<%# Bind("PhoneNumber1") %>' />
                                            <asp:Label ID="LabelOpt" runat="server"  Font-Bold="False" Font-Italic="True" ForeColor="#FF3300">Optional</asp:Label>
                                         </div>
                                     </div>

                                     <div class="form-group">
                                            <asp:Label ID="Label9" runat="server" CssClass="col-xs-3 control-label" AssociatedControlID="PhoneNumber2TextBox" Text="Phone 2 :"></asp:Label>

                                         <div class="col-xs-4">
                                            <asp:TextBox ID="PhoneNumber2TextBox" runat="server" CssClass="form-control" Text='<%# Bind("PhoneNumber2") %>' />
                                            <asp:Label ID="LabelOpt0" runat="server"  Font-Bold="False" Font-Italic="True" ForeColor="#FF3300">Optional</asp:Label>
                                         </div>
                                     </div>

                                     <div class="form-group">
                                         <asp:Label ID="Label10" runat="server" CssClass="col-xs-3 control-label" AssociatedControlID="FaxNumber1TextBox" Text="Fax Number 1 :"></asp:Label>
                                         <div class="col-xs-4">
                                            <asp:TextBox ID="FaxNumber1TextBox" runat="server" CssClass="form-control" Text='<%# Bind("FaxNumber1") %>' />
                                            <asp:Label ID="LabelOpt1" runat="server" Font-Bold="False" Font-Italic="True" ForeColor="#FF3300">Optional</asp:Label>
                                         </div>
                                     </div>

                                     <div class="form-group">
                                         <asp:Label ID="Label11" runat="server" CssClass="col-xs-3 control-label" AssociatedControlID="FaxNumber2TextBox" Text="Fax Number 2 :"></asp:Label>
                                         <div class="col-xs-4">
                                            <asp:TextBox ID="FaxNumber2TextBox" runat="server" CssClass="form-control" Text='<%# Bind("FaxNumber2") %>' />
                                            <asp:Label ID="LabelOpt2" runat="server"  Font-Bold="False" Font-Italic="True" ForeColor="#FF3300">Optional</asp:Label>
                                         </div>
                                     </div>

                                     <div class="form-group">
                                         <asp:Label ID="Label12" runat="server" CssClass="col-xs-3 control-label" AssociatedControlID="WebAdressTextBox" Text="Web Adress :"></asp:Label>
                                         <div class="col-xs-4">
                                            <asp:TextBox ID="WebAdressTextBox" runat="server" CssClass="form-control" Text='<%# Bind("WebAdress") %>' />
                                            <asp:Label ID="LabelOpt3" runat="server"  Font-Bold="False" Font-Italic="True" ForeColor="#FF3300">Optional</asp:Label>
                                         </div>
                                     </div>

                                     <div class="form-group">
                                          <asp:Label ID="Label13" runat="server" CssClass="col-xs-3 control-label" Text="Email :" AssociatedControlID="EmailAddressTextBox"></asp:Label> 
                                         <div class="col-xs-4">
                                            <asp:TextBox ID="EmailAddressTextBox" runat="server" CssClass="form-control" Text='<%# Bind("EmailAddress") %>' />
                                            <asp:Label ID="LabelOpt4" runat="server"  Font-Bold="False" Font-Italic="True" ForeColor="#FF3300">Optional</asp:Label>
                                         </div>
                                     </div>

                                     <div class="form-group">
                                         <asp:Label ID="Label14" runat="server" CssClass="col-xs-3 control-label" Text="Adress :" AssociatedControlID="AdressTextBox"></asp:Label>
                                         <div class="col-xs-4">
                                            <asp:TextBox ID="AdressTextBox" runat="server" CssClass="form-control" Text='<%# Bind("Adress") %>' />
                                            <asp:Label ID="LabelOpt5" runat="server"  Font-Bold="False" Font-Italic="True" ForeColor="#FF3300">Optional</asp:Label>
                                         </div>
                                     </div>

                                     <div class="form-group">
                                         <asp:Label ID="Label16" runat="server" CssClass="col-xs-3 control-label" Text="Notes :" AssociatedControlID="NotesTextBox"></asp:Label>
                                         <div class="col-xs-4">
                                            <asp:TextBox ID="NotesTextBox" runat="server" CssClass="form-control" Text='<%# Bind("Notes") %>' />
                                            <asp:Label ID="LabelOpt6" runat="server"  Font-Bold="False" Font-Italic="True" ForeColor="#FF3300">Optional</asp:Label>
                                         </div>
                                     </div>

                                     <div class="form-group">
                                         <div class="col-xs-1 col-md-offset-3">
                                            <asp:LinkButton ID="InsertButtonZORT" runat="server" CausesValidation="True" CssClass="btn btn-primary"
                                                Text="Insert" onclick="InsertButton_Click1" ValidationGroup="SupplierEntry" CommandName="Insert" />
                                         </div>
                                     </div>




                                 </div>
                             </div>

                            </InsertItemTemplate>

                        </asp:FormView>
                    </div>

                    </div>

                    <br />

            <table >
              <tr>
               <td>
                  <asp:Label ID="LabelSuggestionDescription" runat="server" ></asp:Label>
                   <asp:Panel ID="PanelSuggestionDescription" runat="server" Visible="false" class="alert alert-danger" role="alert">
                       There are some contracts or addendums by Legal Department which would be raised against the PO you are going to create now.
                       <br />
                       Please select suitable one if it matches to this PO...
                       <br />
                       Once you select, then you can automatically follow contracts with corresponding PO values  <a href="/webforms/ContractVersusPo.aspx" target="_blank">on this page</a>
                   </asp:Panel>
               </td>
              </tr>
             <tr>
              <td>
                        <asp:GridView ID="GridViewContractAndPoSuggestion" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" CssClass="table"
                          DataSourceID="SqlDataSourceContractAddendumSuggestion" EnableModelValidation="True" 
                           onload="GridViewContractAndPoSuggestion_Load" ondatabound="GridViewContractAndPoSuggestion_DataBound">
                              <Columns>
                                <asp:TemplateField ItemStyle-Width="50px">
                                 <ItemTemplate>
                                  <asp:CheckBox ID="CheckBoxSuggestion" runat="server" AutoPostBack="true"
                                  oncheckedchanged="CheckBoxSuggestion_CheckedChanged"/>
                                  <asp:Label ID="labelID" runat="server" Text='<%# Bind("Id") %>' CssClass="hidepanel"></asp:Label>
                                 </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ContractNo" HeaderText="ContractNo" ReadOnly="True" 
                                  HeaderStyle-Width="60px" ItemStyle-Width="60px"/>
                                <asp:BoundField DataField="ContractDate" HeaderText="ContractDate" HeaderStyle-Width="60px" ItemStyle-Width="60px"
                                  ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="ContractDescription" HeaderStyle-Width="150px" ItemStyle-Width="150px"
                                  HeaderText="ContractDescription" ReadOnly="True" />
                                <asp:BoundField DataField="ContractValue_woVAT" HeaderText="Contract Value Exc.VAT" 
                                HeaderStyle-Width="60px" ItemStyle-Width="60px"
                                  ReadOnly="True" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="ContractCurrency" HeaderText="Contract Currency" ReadOnly="True" 
                                  HeaderStyle-Width="60px" ItemStyle-Width="60px"/>
                              </Columns>
                        </asp:GridView>

                        <asp:SqlDataSource ID="SqlDataSourceContractAddendumSuggestion" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                            SelectCommand=" SELECT * FROM
                      (
                      SELECT DataSource2.*
                      FROM         dbo.Table6_Supplier INNER JOIN
                      (SELECT * FROM
                      (
                      SELECT     TOP (100) PERCENT CONVERT(nchar(5), ContractID) + N' _ ' AS ID, RTRIM(Table_Contracts.PO_No) AS PO_No , ProjectID, RTRIM(ContractNo) AS ContractNo, ContractDate, SupplierID, RTRIM(ContractDescription) 
                                            AS ContractDescription, ContractValue_woVAT, 
                                            RTRIM(ContractCurrency) AS ContractCurrency
                      FROM         dbo.Table_Contracts

                      UNION ALL

                      SELECT     CONVERT(nchar(5), dbo.Table_Contracts.ContractID) + 'A' + CONVERT(nchar(5), dbo.Table_Addendums.AddendumID) AS ID, RTRIM(Table_Addendums.PO_No) AS PO_No, dbo.Table_Contracts.ProjectID, 
                                            RTRIM(dbo.Table_Addendums.AddendumNo) AS AddendumNo, dbo.Table_Addendums.AddendumDate, dbo.Table_Contracts.SupplierID, 
                                            RTRIM(dbo.Table_Addendums.AddendumDescription) AS AddendumDescription, dbo.Table_Addendums.AddendumValue_woVAT,
                                            RTRIM(dbo.Table_Contracts.ContractCurrency) AS ContractCurrency
                      FROM         dbo.Table_Contracts INNER JOIN
                                            dbo.Table_Addendums ON dbo.Table_Contracts.ContractID = dbo.Table_Addendums.ContractID
                      ) As DataSource1) AS DataSource2 ON dbo.Table6_Supplier.SupplierID = DataSource2.SupplierID
                      ) AS DataSource3
                      WHERE     (ProjectID =@ProjectID) AND (ProjectID <> 95) AND (ProjectID <> 110) AND (SupplierID =@SupplierID) AND LEN((CASE WHEN PO_No IS NULL THEN N'' ELSE PO_No END)) = 0
                      ORDER BY ID ASC">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownListProject" DefaultValue="" 
                                    Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
                                <asp:ControlParameter ControlID="SupplierIDTextBox" DefaultValue="" 
                                    Name="SupplierID" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
              </td>
             </tr>
            </table>

            <br />
            <table >
                <tr>
                    <td>
                      <asp:Panel ID="PanelWarning" runat="server" Visible="false">

                        <div class="alert alert-danger" role="alert">

                          <div><span class="glyphicon glyphicon-exclamation-sign" ></span>По данному поставщику существует незакрытая заявка на покупку (PO).</div>
                          <div><span class="glyphicon glyphicon-exclamation-sign" ></span>При необходимости, для внесения информации о счете, ниже выберите соответствующий номер заявки на покупку (РО)</div>
                          <div><span class="glyphicon glyphicon-exclamation-sign" ></span>При нажатии на номер заявки (РО) вы автоматически переходите на следующую страницу, где необходимые параметры уже выбраны</div>
                          <hr />
                          <div><span class="glyphicon glyphicon-exclamation-sign" ></span>There is still open PO under this supplier.</div>
                          <div><span class="glyphicon glyphicon-exclamation-sign" ></span>If necessary, please click related PO number below to define more invoice.</div>
                          <div><span class="glyphicon glyphicon-exclamation-sign" ></span>Once you click, you will be automatically redirected to next page with required parameters.</div>

                        </div>

                      </asp:Panel>

                        <asp:GridView ID="GridViewPOsumCheck" runat="server"  
                            AutoGenerateColumns="False"  
                            DataKeyNames="ProjectID,PO_No,SupplierID" 
                            DataSourceID="SqlDataSourcePOSumCheck" 
                            onrowcommand="GridViewPOsumCheck_RowCommand" 
                            onrowdatabound="GridViewPOsumCheck_RowDataBound"
                            HeaderStyle-BackColor="#FF0066" HeaderStyle-ForeColor="White" CssClass="table" Font-Size="10px"  >
                            <Columns>
                                <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" ReadOnly="True" 
                                    SortExpression="ProjectID" Visible="False">
                                    <HeaderStyle Font-Bold="False" Font-Size="Smaller" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-Width="60px" 
                                    HeaderText="PO No" SortExpression="PO_No">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButtonPO" runat="server"  Text='<%#Eval("PO_No") %>' 
                                            CausesValidation="False" onclick="LinkButtonPO_Click" CommandName="OpenPO"  CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                            ></asp:LinkButton>
                                        <asp:Label ID="LabelPO" visible="false" runat="server" Text='<%# Eval("PO_No") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="70px"  
                                    HeaderText="Description" SortExpression="Description">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelDescription" runat="server" 
                                            Text='<%# Eval("Description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="10px" 
                                    HeaderText="Currency" SortExpression="PO_Currency">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelCurrency" runat="server" Text='<%# Eval("PO_Currency") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="50px" 
                                    HeaderText="Po Sum With VAT" ItemStyle-HorizontalAlign="Right" 
                                    SortExpression="POvalue_WthVAT">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelposumWthVAT" runat="server" 
                                            Text='<%# Bind("POvalue_WthVAT","{0:###,###,###.00}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="50px" 
                                    HeaderText="Po Sum Exc. VAT" ItemStyle-HorizontalAlign="Right" 
                                    SortExpression="PoSumExcVAT">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Labelposum" runat="server" 
                                            Text='<%# Bind("PoSumExcVAT","{0:###,###,###.00}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="50px" 
                                    HeaderText="Invoice Sum Exc. VAT" ItemStyle-HorizontalAlign="Right" 
                                    SortExpression="InvoiceSum">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Labelinvoicesum" runat="server" 
                                            Text='<%# Bind("InvoiceSum","{0:###,###,###.00}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="50px" 
                                    HeaderText="Balance Exc. VAT" ItemStyle-HorizontalAlign="Right" SortExpression="Balance">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Labelbalance" runat="server" 
                                            Text='<%# Bind("Balance","{0:###,###,###.00}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="50px" 
                                    HeaderText="Percentage" ItemStyle-HorizontalAlign="Right" 
                                    SortExpression="Percentage">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Labelpercentage" runat="server" Text='<%# Bind("Percentage") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table >
                <tr>
                    <td class="style8">
                        <asp:DropDownList ID="DropDownListPOIdMaker" runat="server" BackColor="#CCCCCC" 
                            DataSourceID="SqlDataSourcePOIDmaker" DataTextField="PO_No_Next" DataValueField="PO_No_Next" 
                            Height="19px" Visible="False" Width="190px">
                        </asp:DropDownList>
                        <br />
                        <asp:SqlDataSource ID="SqlDataSourcePOIDmaker" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                            SelectCommandType="StoredProcedure"
                            SelectCommand="GetPoNoFromMakerNewGeneration">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownListProject" Name="ProjectID" 
                                    PropertyName="SelectedValue" Type="Int16" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <br />
                        <asp:TextBox ID="Project_ID_TextBox" runat="server" BackColor="#CCCCCC" 
                            Text='<%# Bind("Project_ID") %>' Visible="False"></asp:TextBox>
                        <asp:DropDownList ID="DropLspLr" runat="server" BackColor="#CCCCCC" 
                            DataSourceID="SqlDataSourceSuplr" DataTextField="SupplierName" 
                            DataValueField="SupplierName" Height="17px" Visible="False" Width="168px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSourceSuplr" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                            SelectCommand="SELECT [SupplierID], [SupplierName] FROM [Table6_Supplier] WHERE ([SupplierID] = @SupplierID) and SupplierID <> N'1111111111'">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="SupplierIDTextBox" DefaultValue="_" 
                                    Name="SupplierID" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
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
                        <br />
                        <asp:SqlDataSource ID="SqlDataSourceCostCode" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                            SelectCommand="IF (select count(BudgetID) from Table_Budget
inner join Table1_Project on Table1_Project.ProjectID = Table_Budget.ProjectID 
where (Table_Budget.ProjectID = @ProjectID) AND (Table1_Project.[Type] <> N'DataCenter')) > 0 

-- Select only budget costcodes if there is budget

SELECT *
                                                        FROM
                                                        (SELECT * FROM
                                                        (SELECT TOP 1 RTRIM([CostCode]) AS CostCode
                                                              ,rtrim(CostCode) + replicate(char(160),20-len(CostCode)) + RTRIM(CodeDescription) AS CostCode_Description
                                                          FROM [dbo].[Table7_CostCode]
                                                          ORDER BY [CostCode] ASC) AS A

                                                        UNION ALL

                                                        SELECT     RTRIM(dbo.Table7_CostCode.CostCode) AS CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 20 - LEN(dbo.Table7_CostCode.CostCode)) 
                                                                              + RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCode_Description
                                                        FROM         dbo.aspnet_UsersInRoles INNER JOIN
                                                                              dbo.aspnet_Users ON dbo.aspnet_UsersInRoles.UserId = dbo.aspnet_Users.UserId AND dbo.aspnet_UsersInRoles.UserId = dbo.aspnet_Users.UserId INNER JOIN
                                                                              dbo.aspnet_Roles ON dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId AND dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId INNER JOIN
                                                                              dbo.Table7_CostCode ON dbo.aspnet_Roles.RoleName = dbo.Table7_CostCode.Type
                                                        WHERE     (dbo.aspnet_Users.UserName = @Username) AND (dbo.Table7_CostCode.Type = N'Finance')

                                                        UNION ALL

                                                        SELECT * FROM
                                                        (SELECT     dbo.Table7_CostCode.CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 20 - LEN(dbo.Table7_CostCode.CostCode)) 
																			  + RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCode_Description
														FROM         dbo.Table_Budget INNER JOIN
																			  dbo.Table7_CostCode ON dbo.Table_Budget.CostCode = dbo.Table7_CostCode.CostCode
														WHERE     (dbo.Table_Budget.ProjectID = @ProjectID) AND (dbo.Table7_CostCode.Type <> N'Finance')
														GROUP BY dbo.Table7_CostCode.CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 20 - LEN(dbo.Table7_CostCode.CostCode)) 
																			  + RTRIM(dbo.Table7_CostCode.CodeDescription)) AS B) AS C
                                                        ORDER BY [CostCode] ASC

else

-- Select if there is no Budget for this project
SELECT *
                                                        FROM
                                                        (SELECT * FROM
                                                        (SELECT TOP 1 RTRIM([CostCode]) AS CostCode
                                                              ,rtrim(CostCode) + replicate(char(160),20-len(CostCode)) + RTRIM(CodeDescription) AS CostCode_Description
                                                          FROM [dbo].[Table7_CostCode]
                                                          ORDER BY [CostCode] ASC) AS A

                                                        UNION ALL

                                                        SELECT     RTRIM(dbo.Table7_CostCode.CostCode) AS CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 20 - LEN(dbo.Table7_CostCode.CostCode)) 
                                                                              + RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCode_Description
                                                        FROM         dbo.aspnet_UsersInRoles INNER JOIN
                                                                              dbo.aspnet_Users ON dbo.aspnet_UsersInRoles.UserId = dbo.aspnet_Users.UserId AND dbo.aspnet_UsersInRoles.UserId = dbo.aspnet_Users.UserId INNER JOIN
                                                                              dbo.aspnet_Roles ON dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId AND dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId INNER JOIN
                                                                              dbo.Table7_CostCode ON dbo.aspnet_Roles.RoleName = dbo.Table7_CostCode.Type
                                                        WHERE     (dbo.aspnet_Users.UserName = @username) AND (dbo.Table7_CostCode.Type = N'Finance')

                                                        UNION ALL

                                                        SELECT * FROM
                                                        (SELECT     TOP (100) PERCENT RTRIM(dbo.Table7_CostCode.CostCode) AS CostCode , rtrim(CostCode) + replicate(char(160),20-len(CostCode)) + RTRIM(CodeDescription) AS CostCode_Description
                                                        FROM         dbo.Table1_Project INNER JOIN
                                                                              dbo.Table7_CostCode ON dbo.Table1_Project.Type = dbo.Table7_CostCode.Type
                                                        WHERE     (dbo.Table1_Project.ProjectID = @ProjectID )
                            
                                                        AND CostCode NOT IN
                                                        (
                                                        select CostCode from Table7_CostCode
                                                        where CostCode like N'%[_]%' AND ISNUMERIC(RIGHT(RTRIM(CostCode),3)) = 1 AND RIGHT(RTRIM(CostCode),3) <> CONVERT(nvarChar(4),@ProjectID)
                                                        )
                            
                            ) AS B) AS C
                                                        ORDER BY [CostCode] ASC">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListProject" DefaultValue="" 
                                        Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
                                    <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" 
                                        PropertyName="Text" />
                                </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="SqlDataSourcePOSumCheck" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                            SelectCommand="SELECT * FROM [View_POSumCheck] WHERE (([ProjectID] = @ProjectID) AND ([SupplierID] = @SupplierID))">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownListProject" DefaultValue="" 
                                    Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
                                <asp:ControlParameter ControlID="SupplierIDTextBox" DefaultValue="" 
                                    Name="SupplierID" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" 
                            CommandName="Cancel" Text="Cancel" Visible="False" />
                    </td>
                </tr>
            </table>

                        <asp:DropDownList ID="DropDownListCostCodeType" runat="server" 
                            DataSourceID="SqlDataSourceCostCodeType" DataTextField="Type" 
                            DataValueField="Type" Visible="False"></asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSourceCostCodeType" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                            SelectCommand="SELECT Rtrim([Type]) AS Type FROM [Table7_CostCode] WHERE CostCode = @CostCode">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownListCostCode" Name="CostCode" 
                                    PropertyName="SelectedValue" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>

                        <asp:Label ID="LabelNote" runat="server" Text="Note :" CssClass="LabelGeneral" visible="false"></asp:Label>
                        <asp:TextBox ID="NotesTextBox" runat="server" visible="false"
                            Text='<%# Bind("Notes") %>' CssClass="TextBoxGeneral" />

<asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
 TargetControlID="lnkPopup"
 PopupControlID="panEdit"
 BackgroundCssClass="modalBackground"
 CancelControlID="btnCancel"
 PopupDragHandleControlID="panEdit" >
</asp:ModalPopupExtender>

<asp:Panel ID="panEdit" runat="server"  CssClass="hidepanel"
          BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" 
          BackColor="White"  >
        <h2 style="text-align: center; color: #FF0000;">Warning!</h2>
        <div style="text-align: center; font-size: 10px; color: #333333; font-weight: bold;">There are PO in the system with SAME value as shown below.</div>
        <div style="text-align: center; font-size: 10px; color: #333333; font-weight: bold;">Please do not create this PO to avoid dublication</div>
        <table width="100%">
            <tr>
                <td>
                  <asp:GridView ID="GridViewPoPossibleDublication" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="PO_No"  HeaderStyle-BackColor="Silver" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                    EnableModelValidation="True" Font-Size="10px" onrowdatabound="GridViewPoPossibleDublication_RowDataBound">
                    <Columns>
                      <asp:BoundField DataField="PO_No" HeaderText="PO No" ReadOnly="True" />
                      <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-Width="200px" ItemStyle-Width="200px"
                         />
                      <asp:BoundField DataField="TotalPrice" HeaderText="Total Value With VAT" DataFormatString="{0:N2}"
                         HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right" />
                      <asp:BoundField DataField="PO_Currency" HeaderText="Currency" 
                         />
                      <asp:BoundField DataField="PO_Date" HeaderText="PO_Date" DataFormatString="{0:dd/MM/yyyy}"
                         />
                      <asp:BoundField DataField="Balance" HeaderText="Balance"  DataFormatString="{0:N2}"
                        HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right"/>
                    </Columns>
                  </asp:GridView>
                  <asp:SqlDataSource ID="SqlDataSourcePoPossibleDublication" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                    SelectCommand="SELECT     RTRIM(dbo.Table2_PONo.PO_No) AS PO_No, RTRIM(dbo.Table2_PONo.Description) AS Description, dbo.Table2_PONo.TotalPrice, 
                      RTRIM(dbo.Table2_PONo.PO_Currency) AS PO_Currency, dbo.Table2_PONo.PO_Date, DataSource1.Balance
FROM         dbo.Table2_PONo INNER JOIN
                          (SELECT     PO_No, Balance
                            FROM          dbo.View_OpenPo1) AS DataSource1 ON DataSource1.PO_No = dbo.Table2_PONo.PO_No
WHERE     (dbo.Table2_PONo.Project_ID = @Project_ID) AND (dbo.Table2_PONo.TotalPrice = @TotalPrice) AND (dbo.Table2_PONo.SupplierID = @SupplierID) AND
                       (dbo.Table2_PONo.PO_Currency = @PO_Currency) ">
                     <SelectParameters>
                      <asp:Parameter Name="Project_ID" Type="Int32"  />
                      <asp:Parameter Name="TotalPrice" Type="Decimal"  />
                      <asp:Parameter Name="SupplierID" Type="String" />
                      <asp:Parameter Name="PO_Currency" Type="String" />
                     </SelectParameters>
                  </asp:SqlDataSource>
                </td>
            </tr>           
       </table>
       <br />
        <div style="text-align: center; font-size: 16px; color: #808080; font-weight: bold;">Shall you create this Purchase Order?</div>      
       <table style="width: 100%">
        <tr>
         <td style="width: 50%; text-align: center;">
           <asp:Button ID="Button1" runat="server" Text="YES" BackColor="#00CC00" CommandName="Insert"
           CommandArgument="Go"
                Font-Bold="True" Font-Size="16px" ForeColor="White" Width="50px" />
         </td>
         <td style="width: 50%; text-align: center;">
           <asp:Button ID="btnCancel" runat="server" Text="NO" 
              Font-Bold="True" Font-Size="16px" ForeColor="White" BackColor="#FF0066" 
              Width="50px" OnClientClick="changeClass" />
         </td>
        </tr>
       </table>

</asp:Panel>
<a id="lnkPopup" runat="server"></a>

                </InsertItemTemplate>
            </asp:FormView>
            <asp:TextBox ID="TextBoxPO_fromLinkButton" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="TextBoxUserName" runat="server" Visible="False"></asp:TextBox>

    <asp:SqlDataSource ID="SqlDataSourcePO" runat="server"
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
        SelectCommand="SELECT [PO_No], [Project_ID], [SupplierID], [Description], [PO_Date], [Notes], [TotalPrice], [PO_Currency], [CostCode], [VATpercent] FROM [Table2_PONo]"
        InsertCommand="INSERT INTO Table2_PONo 
        (PO_No, 
        Project_ID, 
        SupplierID, 
        Description, 
        PO_Date, 
        Notes, 
        TotalPrice, 
        VATpercent, 
        PO_Currency, 
        CostCode, 
        CreatedBy, 
        PersonCreated,
        Approved,
        PersonApproved,
        RequestedBy,
        CO) 
        VALUES 
        (@PO_No, 
        @Project_ID, 
        @SupplierID, 
        @Description, 
        @PO_Date, 
        @Notes, 
        @TotalPrice, 
        @VATpercent, 
        @PO_Currency, 
        @CostCode, 
        @CreatedBy, 
        @PersonCreated,
        @Approved,
        @PersonApproved,
        @RequestedBy,
        0)">
        <InsertParameters>
            <asp:Parameter Name="CreatedBy" />
            <asp:Parameter Name="PersonCreated" />
        </InsertParameters>
    </asp:SqlDataSource>

</div>

<div id="ModalRecentProjects" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content modal_inlineBlock">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">You can pick from recent projects</h4>
            </div>
            <div class="modal-body">

                <asp:GridView ID="GridViewRecentProjects" runat="server" AutoGenerateColumns="False" DataKeyNames="ProjectID" DataSourceID="SqlDataSourceRecentProjects"
                    CssClass="table table-nonfluid table-hover" GridLines="None" OnRowDataBound="GridViewRecentProjects_RowDataBound" EmptyDataText="No project selected in last 6 months" >
                    <Columns>
                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" ReadOnly="True" SortExpression="ProjectID" />
                        <asp:TemplateField>
                            <ItemTemplate>

                                <asp:Label ID="LabelProjectId" runat="server" CssClass="label cursor_pointer label-success" >pick me</asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ProjectName" HeaderText="ProjectName" ReadOnly="True" SortExpression="ProjectName" />
                        <asp:BoundField DataField="Count" HeaderText="Count" ReadOnly="True" SortExpression="Count" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceRecentProjects" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                    SelectCommand="SELECT     TOP (10) dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName, COUNT(dbo.Table1_Project.ProjectID) AS Count
                    FROM         dbo.Table2_PONo INNER JOIN
                                          dbo.Table1_Project ON dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID
                    WHERE     (dbo.Table2_PONo.PersonCreated = @personcreated) AND (dbo.Table2_PONo.CreatedBy &gt; GETDATE() - 180)
                    GROUP BY dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName)
                    ORDER BY Count DESC">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-" Name="personcreated" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>


            </div>
        </div>
    </div>
</div>

<div id="ModalRecentSuppliers" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content modal_inlineBlock">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">You can pick from recent suppliers</h4>
            </div>
            <div class="modal-body">

                <asp:GridView ID="GridViewRecentSuppliers" runat="server" AutoGenerateColumns="False" DataKeyNames="SupplierID" DataSourceID="SqlDataSourceRecentSuppliers"
                    CssClass="table table-nonfluid table-hover" GridLines="None" OnRowDataBound="GridViewRecentSuppliers_RowDataBound" EmptyDataText="No supplier selected in last 6 months" >
                    <Columns>
                        <asp:BoundField DataField="SupplierID" HeaderText="SupplierID" ReadOnly="True" SortExpression="SupplierID" />
                        <asp:TemplateField>
                            <ItemTemplate>

                                <asp:Label ID="LabelSupplierId" runat="server" CssClass="label cursor_pointer label-success" >pick me</asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" ReadOnly="True" SortExpression="SupplierName" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceRecentSuppliers" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                    SelectCommand="
                                SELECT SupplierID, SupplierName FROM (
                                SELECT     TOP (30) Table6_Supplier.SupplierID, RTRIM(Table6_Supplier.SupplierName) AS SupplierName
                                FROM         Table2_PONo INNER JOIN
                                                      Table1_Project ON Table2_PONo.Project_ID = Table1_Project.ProjectID INNER JOIN
                                                      Table6_Supplier ON Table2_PONo.SupplierID = Table6_Supplier.SupplierID
                                WHERE     (Table2_PONo.PersonCreated = @personcreated)
                                ORDER BY Table2_PONo.CreatedBy DESC
                                ) AS Source
                                GROUP BY SupplierID, SupplierName ">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-" Name="personcreated" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>

            </div>
        </div>
    </div>
</div>

</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">

<script type="text/javascript">
    $(document).ready(function () {

        $('#pick-recent-projects').click(function () {
            $("#ModalRecentProjects").modal('show');
        });

        $('#pick-recent-suppliers').click(function () {
            $("#ModalRecentSuppliers").modal('show');
        });

        $('[data-projectid]').click(function () {
            $('#MainContent_FormViewPO_DropDownListProject').val($(this).data("projectid"));
            __doPostBack('ctl00$MainContent$FormViewPO$DropDownListProject', '')
        });

        $('[data-supplierid]').click(function () {
            $('#MainContent_FormViewPO_SupplierIDTextBox').val($(this).data("supplierid"));
            __doPostBack('ctl00$MainContent$FormViewPO$DropDownListProject', '')
        });

    });
</script>


</asp:Content>



<%@ Page Title="" Language="VB" MasterPageFile="~/SiteCertificate.master" AutoEventWireup="false" CodeFile="CertificationEnterDocument.aspx.vb" Inherits="CertificationEnterDocument" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <div>
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" />
    </div>


    <asp:FormView ID="FormViewDocumentEnter" runat="server" DataKeyNames="DocumentID" DataSourceID="ObjectDataSourceDocument" DefaultMode="Insert">
        <InsertItemTemplate>

                <asp:ObjectDataSource ID="ObjectDataSourceDocType" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_DocumentTypeTableAdapter"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourcePrj" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_ProjectClientJunctionPrjNameAdapter"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceClientByProject" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByProjectID" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_ProjectClientJunctionClientNameAdapter">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceFxRateType" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_FxRateTypeTableAdapter"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceAccount" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_AccountsTableAdapter"></asp:ObjectDataSource>

              <%-- MODAL POPUP PICK CONTRACT --%>
              <asp:ModalPopupExtender ID="ModalPopupExtenderPickContract" runat="server"
               TargetControlID="ButtonPickContract"
               PopupControlID="PanelPickContract"
               BackgroundCssClass="modalBackground"
               CancelControlID="btnCancelPickContract" >
              </asp:ModalPopupExtender>
              <asp:Panel ID="PanelPickContract" runat="server" Style="display:none;" >
                 <div style="text-align: right">
                            <asp:Button ID="btnCancelPickContract" runat="server" Text="X" 
                             Font-Bold="True" Font-Size="16px" ForeColor="White" BackColor="#FF0066" 
                             OnClientClick="changeClass" />
                 </div>
                  <div style="padding:20px; background-color:white; ">
                    <asp:GridView ID="GridViewPickContract" runat="server" DataSourceID="ObjectDataSourceContractPick" AutoGenerateColumns="False" 
                        EmptyDataText="Project and Client should be selected first" BackColor="White" HeaderStyle-BackColor="#3333FF" Font-Size="10px" HeaderStyle-ForeColor="White" OnRowCommand="GridViewPickContract_RowCommand">
                        <Columns>
                            <asp:TemplateField >
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button id="BtnPickThis" runat="server" CommandName="pick" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"  Text="Pick This" CausesValidation="false"
                                        BackColor="lime" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ContractID" HeaderStyle-CssClass="hidepanel" ItemStyle-CssClass="hidepanel" ></asp:BoundField>
                            <asp:BoundField DataField="ContractNo" HeaderText="Contract No" ReadOnly="True" HeaderStyle-Width="150px" ItemStyle-Width="150px"/>
                            <asp:BoundField DataField="ContractDate" HeaderText="Contract Date" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px" ItemStyle-Width="100px"/>
                            <asp:BoundField DataField="ContractValue_woVAT" HeaderText="Contract Value woVAT" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px" ItemStyle-Width="100px" >
<ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ContractCurrency" HeaderText="" ReadOnly="True" />
                            <asp:BoundField DataField="ContractDescription" HeaderText="Contract Description" ReadOnly="True" HeaderStyle-Width="200px" ItemStyle-Width="200px"/>
                            <asp:BoundField DataField="ContractType" HeaderText="Contract Type" ReadOnly="True" />
                        </Columns>

<HeaderStyle BackColor="#3333FF" ForeColor="White"></HeaderStyle>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSourceContractPick" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_ContractsTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
                            <asp:ControlParameter ControlID="DropDownListClient" DefaultValue="0" Name="ClientId" PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>

                  </div>

              </asp:Panel>
              <asp:Button id="ButtonPickContract"  runat="server" CssClass="hidepanel"/>
                <%-- /MODAL POPUP PICK CONTRACT --%>  

              <%-- MODAL POPUP PICK ADDENDUM --%>
              <asp:ModalPopupExtender ID="ModalPopupExtenderPickAddendum" runat="server"
               TargetControlID="ButtonPickAddendum"
               PopupControlID="PanelPickAddendum"
               BackgroundCssClass="modalBackground"
               CancelControlID="btnCancelPickAddendum" >
              </asp:ModalPopupExtender>
              <asp:Panel ID="PanelPickAddendum" runat="server" style="display:none;" >
                 <div style="text-align: right">
                            <asp:Button ID="btnCancelPickAddendum" runat="server" Text="X" 
                             Font-Bold="True" Font-Size="16px" ForeColor="White" BackColor="#FF0066" 
                             OnClientClick="changeClass" />
                 </div>
                  <div style="padding:20px; background-color:white; ">
                    <asp:GridView ID="GridViewPickAddendum" runat="server" DataSourceID="ObjectDataSourcePickAddendum" AutoGenerateColumns="False" 
                        EmptyDataText="Project and Client should be selected first" BackColor="White" HeaderStyle-BackColor="#3333FF" 
                        Font-Size="10px" HeaderStyle-ForeColor="White" OnRowCommand="GridViewPickAddendum_RowCommand" OnRowDataBound="GridViewPickAddendum_RowDataBound">
                        <Columns>
                            <asp:TemplateField >
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button id="BtnPickThis" runat="server" CommandName="pick" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"  Text="Pick This" CausesValidation="false"
                                        BackColor="lime" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ContractID" HeaderStyle-CssClass="hidepanel" ItemStyle-CssClass="hidepanel" ></asp:BoundField>
                            <asp:BoundField DataField="AddendumID" HeaderStyle-CssClass="hidepanel" ItemStyle-CssClass="hidepanel" ></asp:BoundField>
                            <asp:BoundField DataField="ContractNo" HeaderText="Addendum No" ReadOnly="True" HeaderStyle-Width="150px" ItemStyle-Width="150px"/>
                            <asp:BoundField DataField="ContractDate" HeaderText="Addendum Date" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px" ItemStyle-Width="100px"/>
                            <asp:BoundField DataField="ContractValue_woVAT" HeaderText="Addendum Value woVAT" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px" ItemStyle-Width="100px"/>
                            <asp:BoundField DataField="ContractCurrency" HeaderText="" ReadOnly="True"  />
                            <asp:BoundField DataField="ContractDescription" HeaderText="Addendum Description" ReadOnly="True" HeaderStyle-Width="200px" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="ContractType" HeaderText="Contract Type" ReadOnly="True" />
                        </Columns>

<HeaderStyle BackColor="#3333FF" ForeColor="White"></HeaderStyle>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSourcePickAddendum" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_AddendumsTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
                            <asp:ControlParameter ControlID="DropDownListClient" DefaultValue="0" Name="ClientId" PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>

                  </div>

              </asp:Panel>
              <asp:Button id="ButtonPickAddendum"  runat="server" CssClass="hidepanel"/>
                <%-- /MODAL POPUP PICK ADDENDUM --%> 

            <table class="CertificationDocumentEnterTable">
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>

                        <asp:RadioButtonList ID="RadioButtonListSigned" runat="server" RepeatDirection="Horizontal" SelectedValue='<%# Bind("Signed")%>'>
                            <asp:ListItem Value="true">Signed</asp:ListItem>
                            <asp:ListItem Value="false">Not Signed</asp:ListItem>
                        </asp:RadioButtonList>

                    </td>
                    <td style="width:150px;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorSigned" runat="server" ControlToValidate="RadioButtonListSigned" Display="Dynamic" ErrorMessage="Required" ></asp:RequiredFieldValidator>
                    </td>
                    <td>

                        &nbsp;</td>
                    <td>

                        &nbsp;</td>
                    <td>

                        &nbsp;</td>
                </tr>
                <tr>
                    <td>Document Type </td>
                    <td>
                        <asp:DropDownList ID="DropDownListDocType" runat="server" AutoPostBack="True" DataSourceID="ObjectDataSourceDocType" DataTextField="Document_Type" DataValueField="Document_TypeID" OnDataBound="DropDownListDocType_DataBound" OnSelectedIndexChanged="DropDownListDocType_SelectedIndexChanged" SelectedValue='<%# Bind("Document_TypeID")%>'>
                        </asp:DropDownList>
                        &nbsp; </td>
                    <td style="width:150px;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDocType" runat="server" ControlToValidate="DropDownListDocType" Display="Dynamic" ErrorMessage="Required" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                    <td>Doc No</td>
                    <td>
                        <asp:TextBox ID="TextBoxDocNo" runat="server" Text='<%# Bind("DocNo")%>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxDocNo" runat="server" ControlToValidate="TextBoxDocNo" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Project</td>
                    <td>
                        <asp:DropDownList ID="DropDownListPrj" runat="server" DataSourceID="ObjectDataSourcePrj" 
                            DataTextField="ProjectName" DataValueField="ProjectID" AutoPostBack="True" 
                            OnDataBound="DropDownListPrj_DataBound" SelectedValue='<%# Bind("ProjectID")%>' OnSelectedIndexChanged="DropDownListPrj_SelectedIndexChanged" >
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPrj" runat="server" Display="Dynamic"
                            ErrorMessage="Required" ControlToValidate="DropDownListPrj" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                        <td>
                            ContractID
                        </td>
                        <td>

                            <asp:TextBox ID="TextBoxContractID" runat="server" Text='<%# Bind("ContractID")%>' Enabled="false" >
                            </asp:TextBox>
                            <asp:Button ID="BtnPickContract" runat="server" Text="Pick Contract" OnClick="BtnPickContract_Click" CausesValidation="false" />
                        </td>
                        <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxContractID" runat="server" 
                            ErrorMessage="Required" ControlToValidate="TextBoxContractID" Display="Dynamic" >
                        </asp:RequiredFieldValidator>
                        </td>
                </tr>
                <tr>
                    <td>
                        Client</td>
                    <td>
                        <asp:DropDownList ID="DropDownListClient" runat="server" DataSourceID="ObjectDataSourceClientByProject" 
                            DataTextField="ClientName" DataValueField="ClientID" OnDataBound="DropDownListClient_DataBound" AutoPostBack="True" OnSelectedIndexChanged="DropDownListClient_SelectedIndexChanged" >
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorClient" runat="server" Display="Dynamic"
                            ErrorMessage="Required" ControlToValidate="DropDownListClient" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                        <td>
                            AddendumID
                        </td>
                        <td>

                            <asp:TextBox ID="TextBoxAddendumID" runat="server" Text='<%# Bind("AddendumID")%>' Enabled="false" >
                            </asp:TextBox>

                            <asp:Button ID="BtnPickAddendum" runat="server" CausesValidation="false" OnClick="BtnPickAddendum_Click" Text="Pick Addendum" />

                        </td>
                        <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxAddendumID" runat="server" 
                            ErrorMessage="Required" ControlToValidate="TextBoxAddendumID" Display="Dynamic" >
                        </asp:RequiredFieldValidator>
                        </td>
                </tr>
                <tr>
                    <td>
                        Document Date
                    </td>
                    <td>

                        <asp:TextBox ID="TextBoxDocDate" runat="server" Text='<%# Bind("DocumentDate")%>' >
                        </asp:TextBox>

                        <asp:CalendarExtender ID="CalendarExtenderDocDate" runat="server" CssClass="cal_Theme1" Animated="False" 
                        TargetControlID="TextBoxDocDate" format="dd/MM/yyyy" > 
                        </asp:CalendarExtender>

                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDocDate" runat="server" 
                            ErrorMessage="Required" ControlToValidate="TextBoxDocDate" Display="Dynamic" >
                        </asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorDocdate" ControlToValidate="TextBoxDocDate" Display="Dynamic"
                        runat="server" ErrorMessage="dd/mm/yyyy"  ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" >
                        </asp:RegularExpressionValidator>

                    </td>
                    <td>
                        FxRate Type</td>
                    <td>
                        <asp:DropDownList ID="DropDownListFxRateType" runat="server" DataSourceID="ObjectDataSourceFxRateType" 
                            DataTextField="FxRateType" DataValueField="FxRateTypeID" OnDataBound="DropDownListFxRateType_DataBound" SelectedValue='<%# Bind("FxRateTypeID")%>' AutoPostBack="True" OnSelectedIndexChanged="DropDownListFxRateType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorFxRateType" runat="server" Display="Dynamic"
                            ErrorMessage="Required" ControlToValidate="DropDownListFxRateType" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        External Value Exc. VAT</td>
                    <td>
                        <asp:TextBox ID="TextBoxExcValue" runat="server" Text='<%# Bind("ExternalValueExcVAT")%>' >
                        </asp:TextBox>

                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorExtValue" runat="server" 
                            ErrorMessage="Required" ControlToValidate="TextBoxExcValue" Display="Dynamic">
                        </asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorExcValue" ControlToValidate="TextBoxExcValue" Display="Dynamic"
                        runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>

                        <asp:CompareValidator ID="CompareValidatorTextBoxExcValue" runat="server" ControlToValidate="TextBoxExcValue" ErrorMessage="Cannot be zero" ValueToCompare="0" Operator="GreaterThan" Type="Double"  Display="Dynamic" ></asp:CompareValidator>

                    </td>
                    <td>

                        FxRate

                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxFxRate" runat="server" Text='<%# Bind("FxRate")%>' >
                        </asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorFxRate" runat="server" 
                            ErrorMessage="Required" ControlToValidate="TextBoxFxRate" Display="Dynamic">
                        </asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorFxRate" ControlToValidate="TextBoxFxRate" Display="Dynamic"
                        runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>

                    </td>
                </tr>
                <tr>
                    <td>
                        Advance Exc. VAT</td>
                    <td>
                        <asp:TextBox ID="TextBoxAdvance" runat="server" Text='<%# Bind("AdvanceExcVAT")%>' >
                        </asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorAdvance" runat="server" 
                            ErrorMessage="Required" ControlToValidate="TextBoxAdvance" Display="Dynamic">
                        </asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorAdvance" ControlToValidate="TextBoxAdvance" Display="Dynamic"
                        runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>

                        <asp:CompareValidator ID="CompareValidatorTextBoxAdvance" runat="server" ControlToValidate="TextBoxAdvance" ErrorMessage="Cannot be zero" ValueToCompare="0" Operator="GreaterThan" Type="Double"  Display="Dynamic" ></asp:CompareValidator>


                    </td>
                    <td>

                        Account</td>
                    <td>
                        <asp:DropDownList ID="DropDownListAccount" runat="server" DataSourceID="ObjectDataSourceAccount" 
                            DataTextField="AccountName" DataValueField="AccountID" OnDataBound="DropDownListAccount_DataBound" SelectedValue='<%# Bind("AccountID")%>'>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorAccount" runat="server" Display="Dynamic"
                            ErrorMessage="Required" ControlToValidate="DropDownListAccount" InitialValue="0">
                        </asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td>
                        Retention Exc. VAT</td>
                    <td>
                        <asp:TextBox ID="TextBoxRetention" runat="server" Text='<%# Bind("RetentionExcVAT")%>' >
                        </asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorRetention" runat="server" 
                            ErrorMessage="Required" ControlToValidate="TextBoxRetention" Display="Dynamic">
                        </asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorRetention" ControlToValidate="TextBoxRetention" Display="Dynamic"
                        runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>

                        <asp:CompareValidator ID="CompareValidatorTextBoxRetention" runat="server" ControlToValidate="TextBoxRetention" ErrorMessage="Cannot be zero" ValueToCompare="0" Operator="GreaterThan" Type="Double" Display="Dynamic" ></asp:CompareValidator>

                    </td>
                    <td>

                        Scan File</td>
                    <td>
                        <asp:FileUpload ID="FileUploadScanFile" runat="server" />
                        &nbsp;<asp:Button ID="ButtonUpload" runat="server" Text="Upload" CausesValidation="false" OnClick="ButtonUpload_Click" />
                        <asp:TextBox ID="TextBoxStoreLink" runat="server" CssClass="hidepanel" Text='<%# Bind("ScanLink")%>' >
                        </asp:TextBox>
                        <br />
                        <asp:Label ID="LabelInfo" runat="server" ></asp:Label>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorScanFile" runat="server" 
                            ErrorMessage="Required" ControlToValidate="TextBoxStoreLink" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align:center;">
                        <asp:LinkButton ID="LinkButtonInsert" runat="server" CausesValidation="True" 
                            CommandName="Insert" Text="Insert"  Font-Size="X-Large" ForeColor="Red" />
                    </td>
                </tr>
            </table>

        </InsertItemTemplate>

    </asp:FormView>
    <asp:ObjectDataSource ID="ObjectDataSourceDocument" runat="server" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_DocumentsTableAdapter" SelectMethod="GetData" >
        <InsertParameters>
            <asp:Parameter Name="DocumentID" Type="Int32" />
            <asp:Parameter Name="ContractID" Type="Int32" />
            <asp:Parameter Name="AddendumID" Type="Int32" />
            <asp:Parameter Name="Document_TypeID" Type="Int16" />
            <asp:Parameter Name="ProjectID" Type="Int16" />
            <asp:Parameter Name="ClientID" Type="String" />
            <asp:Parameter Name="DocumentDate" Type="DateTime" />
            <asp:Parameter Name="ExternalValueExcVAT" Type="Decimal" />
            <asp:Parameter Name="AdvanceExcVAT" Type="Decimal" />
            <asp:Parameter Name="RetentionExcVAT" Type="Decimal" />
            <asp:Parameter Name="FxRate" Type="Decimal" />
            <asp:Parameter Name="FxRateTypeID" Type="Int16" />
            <asp:Parameter Name="AccountID" Type="Int16" />
            <asp:Parameter Name="ScanLink" Type="String" />
            <asp:Parameter Name="Comment" Type="String" />
            <asp:Parameter Name="Signed" Type="Boolean" />
        </InsertParameters>
    </asp:ObjectDataSource>
    
    <hr />

    <asp:Label ID="LabelSummary" runat="server" Visible="false" Text="Total Sums" Font-Size="Large" ></asp:Label>
    <asp:GridView ID="GridViewSummary" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceSummary" Font-Size="10px">
        <Columns>
            <asp:BoundField DataField="ProjectName" HeaderText="Project Name" ReadOnly="True" SortExpression="ProjectName" HeaderStyle-Width="80px" >
            </asp:BoundField>
            <asp:BoundField DataField="ClientName" HeaderText="Client Name" ReadOnly="True" SortExpression="ClientName" HeaderStyle-Width="80px" >
            </asp:BoundField>
            <asp:BoundField DataField="Sum_ContractAddendumValue_woVAT" HeaderText="Sum of Contract Addendum Value woVAT" SortExpression="Sum_ContractAddendumValue_woVAT" HeaderStyle-Width="100px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >
            </asp:BoundField>
            <asp:BoundField DataField="ContractCurrency" HeaderText="Contract Currency" SortExpression="ContractCurrency" HeaderStyle-Width="80px" >
            </asp:BoundField>
            <asp:BoundField DataField="sumExtrValue" HeaderText="Sum of External Value" SortExpression="sumExtrValue" HeaderStyle-Width="100px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >
            </asp:BoundField>
            <asp:BoundField DataField="Sum_InvoiceValue" HeaderText="Sum of Invoice Value" SortExpression="Sum_InvoiceValue" HeaderStyle-Width="100px"  DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right">
            </asp:BoundField>
            <asp:BoundField DataField="sum_PaymentAmount" HeaderText="Sum of Payment Amount" SortExpression="sum_PaymentAmount" HeaderStyle-Width="100px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >
            </asp:BoundField>
        </Columns>
        <HeaderStyle BackColor="#99CCFF" />
        <RowStyle Height="30px" />
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSourceSummary" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand="
                        IF @ProjectID <> 0 AND @ClientID = N'0'
                        BEGIN
		                        SELECT 
                                ProjectName
                                , ClientName
                                , Sum_ContractAddendumValue_woVAT
                                , ContractCurrency
                                , sumExtrValue
                                , Sum_InvoiceValue
                                , sum_PaymentAmount 
                                FROM Certification.View_Summary1 
                                WHERE ProjectID = @ProjectID 
                                ORDER BY ProjectName, ClientName
                        END

                        IF @ProjectID <> 0 AND @ClientID <> N'0'
                        BEGIN
		                        SELECT 
                                ProjectName
                                , ClientName
                                , Sum_ContractAddendumValue_woVAT
                                , ContractCurrency
                                , sumExtrValue
                                , Sum_InvoiceValue
                                , sum_PaymentAmount 
                                FROM Certification.View_Summary1 
                                WHERE ProjectID = @ProjectID AND ClientID = @ClientID
                                ORDER BY ProjectName, ClientName
                        END

                        IF @ProjectID = 0 AND @ClientID <> N'0'
                        BEGIN
		                        SELECT 
                                ProjectName
                                , ClientName
                                , Sum_ContractAddendumValue_woVAT
                                , ContractCurrency
                                , sumExtrValue
                                , Sum_InvoiceValue
                                , sum_PaymentAmount 
                                FROM Certification.View_Summary1 
                                WHERE ClientID = @ClientID
                                ORDER BY ProjectName, ClientName
                        END ">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="ProjectID" Type="Int16" />
            <asp:Parameter DefaultValue="0" Name="ClientID" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <br />

    <asp:Label ID="LabelDocHistory" runat="server" Visible="false" Text="Documents History" Font-Size="Large" ></asp:Label>
    <asp:GridView ID="GridviewDocumentsHistory" runat="server" AutoGenerateColumns="False" DataKeyNames="DocumentID" DataSourceID="SqlDataSourceDocumentsHistory" Font-Size="10px">
        <Columns>
            <asp:BoundField DataField="ProjectName" HeaderText="Project Name" SortExpression="ProjectName" HeaderStyle-Width="80px" />
            <asp:BoundField DataField="ClientName" HeaderText="Client Name" SortExpression="ClientName" HeaderStyle-Width="80px" />
            <asp:BoundField DataField="Document_Type" HeaderText="Document Type" SortExpression="Document_Type" HeaderStyle-Width="80px" />
            <asp:BoundField DataField="DocNo" HeaderText="Document No" SortExpression="DocNo" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="DocumentDate" HeaderText="Document Date" SortExpression="DocumentDate" HeaderStyle-Width="80px" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="ExternalValueExcVAT" HeaderText="External Value Exc VAT" SortExpression="ExternalValueExcVAT" HeaderStyle-Width="100px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"  />
            <asp:BoundField DataField="AdvanceExcVAT" HeaderText="Advance Exc VAT" SortExpression="AdvanceExcVAT" HeaderStyle-Width="100px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"  />
            <asp:BoundField DataField="RetentionExcVAT" HeaderText="Retention Exc VAT" SortExpression="RetentionExcVAT" HeaderStyle-Width="100px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"  />
            <asp:BoundField DataField="DocumentID" HeaderText="Document ID" ReadOnly="True" SortExpression="DocumentID" HeaderStyle-Width="80px" />
            <asp:BoundField DataField="ContractID" HeaderText="Contract ID" SortExpression="ContractID" HeaderStyle-Width="80px" />
            <asp:TemplateField HeaderText="Contract ID" HeaderStyle-Width="80px" >
                <ItemTemplate>
                    <asp:Literal ID="LiteralContractID" runat="server" Text='<%# Eval("ContractID")%>' ></asp:Literal>
                    <br />
                    <asp:HyperLink ID="HyperlinkFileContract" runat="server" Target="_blank" ForeColor="blue" >See Details</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Addendum ID" HeaderStyle-Width="80px" >
                <ItemTemplate>
                    <asp:Literal ID="LiteralAddendumID" runat="server" Text='<%# Eval("AddendumID")%>' ></asp:Literal>
                    <br />
                    <asp:HyperLink ID="HyperlinkFileAddendum" runat="server" Target="_blank" ForeColor="blue" >See Details</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="AccountName" HeaderText="Account Name" SortExpression="AccountName" HeaderStyle-Width="80px" />
            <asp:TemplateField HeaderText="Files">
                <ItemTemplate>
                            <asp:ImageButton ID="ImageButtonItem" runat="server" CommandName="OpenDocument" CausesValidation="false"
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ScanLink")%>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="Signed" HeaderText="Signed" ReadOnly="true" SortExpression="Signed" />
        </Columns>
        <HeaderStyle BackColor="#99CCFF" />
        <RowStyle Height="30px" />
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSourceDocumentsHistory" runat="server" ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand=" 
            IF @ProjectID <> 0 AND @ClientID = N'0'
            BEGIN
             SELECT [ProjectName]
                  ,[ClientName]
                  ,[Document_Type]
                  ,[DocumentDate]
                  ,[ExternalValueExcVAT]
                  ,[AdvanceExcVAT]
                  ,[RetentionExcVAT]
                  ,[DocumentID]
                  ,[ContractID]
                  ,[AddendumID]
                  ,[AccountName]
                  ,[ScanLink]
                  ,[ProjectID]
                  ,[ClientID]
                  ,[DocNo]
                  ,Signed
              FROM [Certification].[View_DocumentHistory]
              WHERE ProjectID = @ProjectID 
            END

            IF @ProjectID <> 0 AND @ClientID <> N'0'
            BEGIN
             SELECT [ProjectName]
                  ,[ClientName]
                  ,[Document_Type]
                  ,[DocumentDate]
                  ,[ExternalValueExcVAT]
                  ,[AdvanceExcVAT]
                  ,[RetentionExcVAT]
                  ,[DocumentID]
                  ,[ContractID]
                  ,[AddendumID]
                  ,[AccountName]
                  ,[ScanLink]
                  ,[ProjectID]
                  ,[ClientID]
                  ,[DocNo]
                  ,Signed
              FROM [Certification].[View_DocumentHistory]
            WHERE ProjectID = @ProjectID AND ClientID = @ClientID
            END

            IF @ProjectID = 0 AND @ClientID <> N'0'
            BEGIN
             SELECT [ProjectName]
                  ,[ClientName]
                  ,[Document_Type]
                  ,[DocumentDate]
                  ,[ExternalValueExcVAT]
                  ,[AdvanceExcVAT]
                  ,[RetentionExcVAT]
                  ,[DocumentID]
                  ,[ContractID]
                  ,[AddendumID]
                  ,[AccountName]
                  ,[ScanLink]
                  ,[ProjectID]
                  ,[ClientID]
                  ,[DocNo]
                  ,Signed
              FROM [Certification].[View_DocumentHistory]
            WHERE ClientID = @ClientID
            END ">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="ProjectID" />
            <asp:Parameter DefaultValue="0" Name="ClientID" />
        </SelectParameters>
    </asp:SqlDataSource>
    

   

    
</asp:Content>


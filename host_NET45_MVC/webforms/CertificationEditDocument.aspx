<%@ Page Title="" Language="VB" MasterPageFile="~/SiteCertificate.master" AutoEventWireup="false" CodeFile="CertificationEditDocument.aspx.vb" Inherits="CertificationEditDocument" MaintainScrollPositionOnPostback="True" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <div>
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" />
    </div>

    <asp:ObjectDataSource ID="ObjectDataSourceDocType" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_DocumentTypeTableAdapter"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourcePrj" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByDocumentedProjects" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_ProjectClientJunctionPrjNameAdapter"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceClientByProject" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByProjectID" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_ProjectClientJunctionClientNameAdapter">
        <SelectParameters>
         <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceFxRateType" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_FxRateTypeTableAdapter"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceAccount" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_AccountsTableAdapter"></asp:ObjectDataSource>

    <asp:ObjectDataSource ID="ObjectDataSourceDocumentByProjectIDandClientID" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByProjectIDandClientID" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_DocumentsTableAdapter" DeleteMethod="Delete" UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="Original_DocumentID" Type="Int32" />
        </DeleteParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
            <asp:ControlParameter ControlID="DropDownListClient" DefaultValue="0" Name="ClientID" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
        <UpdateParameters>
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
            <asp:Parameter Name="Original_DocumentID" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>

    <asp:DropDownList ID="DropDownListPrj" runat="server" DataSourceID="ObjectDataSourcePrj" 
        DataTextField="ProjectName" DataValueField="ProjectID" AutoPostBack="True" 
        OnDataBound="DropDownListPrj_DataBound" >
    </asp:DropDownList>

    <asp:DropDownList ID="DropDownListClient" runat="server" DataSourceID="ObjectDataSourceClientByProject" AutoPostBack="True"
        DataTextField="ClientName" DataValueField="ClientID" OnDataBound="DropDownListClient_DataBound" >
    </asp:DropDownList>

    <br /><br /><br />
    <asp:GridView ID="GridViewDocumentByProjectIDandClientID" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="DocumentID" DataSourceID="ObjectDataSourceDocumentByProjectIDandClientID">
        <Columns>

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                        CommandName="Edit" Text="Edit"></asp:LinkButton>

                    <asp:LinkButton ID="LinkButtonDelete" Runat="server" 
                    OnClientClick="return confirm('Are you sure you want to delete this record?');"
                    CommandName="Delete" Text="Delete" CssClass="LabelGeneral"></asp:LinkButton>

                </ItemTemplate>

                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True"  CssClass="LabelGeneral"
                        CommandName="Update" Text="Update"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                        CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
            </asp:TemplateField>

         <asp:TemplateField>
             <ItemTemplate>

                <asp:ObjectDataSource ID="ObjectDataSourceClientByProjectItem" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByProjectID" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_ProjectClientJunctionClientNameAdapter">
                    <SelectParameters>
                     <asp:Parameter Name="ProjectID" Type="Int16" DefaultValue="0" />
                    </SelectParameters>
                </asp:ObjectDataSource>

                <table class="CertificationDocumentEnterTable">
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                        <asp:RadioButtonList ID="RadioButtonListSigned" runat="server" RepeatDirection="Horizontal" SelectedValue='<%# Bind("Signed")%>'>
                            <asp:ListItem Value="True">Signed</asp:ListItem>
                            <asp:ListItem Value="False">Not Signed</asp:ListItem>
                        </asp:RadioButtonList>

                        </td>
                        <td style="width:150px;">

                            &nbsp;</td>
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
                            <asp:DropDownList ID="DropDownListDocType" runat="server" AutoPostBack="True" DataSourceID="ObjectDataSourceDocType" DataTextField="Document_Type" DataValueField="Document_TypeID" SelectedValue='<%# Bind("Document_TypeID")%>'>
                            </asp:DropDownList>
                            <asp:Label ID="LblDocType" runat="server"></asp:Label>
                        </td>
                        <td style="width:150px;"></td>
                        <td>Document ID </td>
                        <td>
                            <asp:TextBox ID="TextBoxDocID" runat="server" ReadOnly="true" Text='<%# Bind("DocumentID")%>'>
                            </asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            Project</td>
                        <td>
                            <asp:DropDownList ID="DropDownListPrjItem" runat="server" DataSourceID="ObjectDataSourcePrj" 
                                DataTextField="ProjectName" DataValueField="ProjectID" AutoPostBack="True" 
                                SelectedValue='<%# Bind("ProjectID")%>' >
                            </asp:DropDownList>
                        </td>
                        <td>

                        </td>
                        <td>
                            ContractID
                        </td>
                        <td>

                            <asp:TextBox ID="TextBoxContractID" runat="server" Text='<%# Bind("ContractID")%>' ReadOnly="true" >
                            </asp:TextBox>

                        </td>
                        <td>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            Client</td>
                        <td>
                            <asp:DropDownList ID="DropDownListClientItem" runat="server" DataSourceID="ObjectDataSourceClientByProject" 
                                DataTextField="ClientName" DataValueField="ClientID" SelectedValue='<%# Bind("ClientID")%>' >
                            </asp:DropDownList>
                        </td>
                        <td>

                        </td>
                        <td>
                            AddendumID
                        </td>
                        <td>

                            <asp:TextBox ID="TextBoxAddendumID" runat="server" Text='<%# Bind("AddendumID")%>' ReadOnly="true" >
                            </asp:TextBox>

                        </td>
                        <td>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            Document Date
                        </td>
                        <td>

                            <asp:TextBox ID="TextBoxDocDate" runat="server" Text='<%# Bind("DocumentDate")%>' ReadOnly="true"  >
                            </asp:TextBox>

                        </td>
                        <td>

                        </td>
                        <td>

                            FxRate</td>
                        <td>
                            <asp:TextBox ID="TextBoxFxRate" runat="server" Text='<%# Bind("FxRate")%>' ReadOnly="true" >
                            </asp:TextBox>
                        </td>
                        <td>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            External Value Exc. VAT</td>
                        <td>
                            <asp:TextBox ID="TextBoxExcValue" runat="server" Text='<%# Bind("ExternalValueExcVAT")%>' ReadOnly="true" >
                            </asp:TextBox>

                        </td>
                        <td>

                        </td>
                        <td>
                            FxRate Type</td>
                        <td>
                            <asp:DropDownList ID="DropDownListFxRateType" runat="server" DataSourceID="ObjectDataSourceFxRateType" 
                                DataTextField="FxRateType" DataValueField="FxRateTypeID" SelectedValue='<%# Bind("FxRateTypeID")%>'>
                            </asp:DropDownList>
                        </td>
                        <td>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            Advance Exc. VAT</td>
                        <td>
                            <asp:TextBox ID="TextBoxAdvance" runat="server" Text='<%# Bind("AdvanceExcVAT")%>' ReadOnly="true" >
                            </asp:TextBox>
                        </td>
                        <td>

                        </td>
                        <td>

                            Account</td>
                        <td>
                            <asp:DropDownList ID="DropDownListAccount" runat="server" DataSourceID="ObjectDataSourceAccount" 
                                DataTextField="AccountName" DataValueField="AccountID" SelectedValue='<%# Bind("AccountID")%>'>
                            </asp:DropDownList>
                        </td>
                        <td>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            Retention Exc. VAT</td>
                        <td>
                            <asp:TextBox ID="TextBoxRetention" runat="server" Text='<%# Bind("RetentionExcVAT")%>' ReadOnly="true" >
                            </asp:TextBox>
                        </td>
                        <td>

                        </td>
                        <td>

                            Scan File</td>
                        <td>

                            <asp:ImageButton ID="ImageButtonItem" runat="server" CommandName="OpenDocument"
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ScanLink")%>'/>

                        </td>
                        <td>

                        </td>
                    </tr>
                    <tr>
                        <td>Doc No</td>
                        <td>
                            <asp:TextBox ID="TextBoxDocNo" runat="server" Text='<%# Bind("DocNo")%>' ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>

                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>

             </ItemTemplate>

             <EditItemTemplate>

                <asp:ObjectDataSource ID="ObjectDataSourceClientByProjectEdit" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByProjectID" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_ProjectClientJunctionClientNameAdapter">
                    <SelectParameters>
                     <asp:Parameter Name="ProjectID" Type="Int16" DefaultValue="0" />
                    </SelectParameters>
                </asp:ObjectDataSource>

                <asp:Label ID="LabelError" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Large" ></asp:Label>

                <table class="CertificationDocumentEnterTable" style="background-color:#F0FFFF; ">
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                        <asp:RadioButtonList ID="RadioButtonListSigned" runat="server" RepeatDirection="Horizontal" SelectedValue='<%# Bind("Signed")%>'>
                            <asp:ListItem Value="True">Signed</asp:ListItem>
                            <asp:ListItem Value="False">Not Signed</asp:ListItem>
                        </asp:RadioButtonList>

                        </td>
                        <td style="width:150px;">
                            &nbsp;</td>
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
                            <asp:DropDownList ID="DropDownListDocType" runat="server" AutoPostBack="True" DataSourceID="ObjectDataSourceDocType" DataTextField="Document_Type" DataValueField="Document_TypeID" OnSelectedIndexChanged="DropDownListDocType_SelectedIndexChanged" SelectedValue='<%# Bind("Document_TypeID")%>'>
                            </asp:DropDownList>
                        </td>
                        <td style="width:150px;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDocType" runat="server" ControlToValidate="DropDownListDocType" Display="Dynamic" ErrorMessage="Required" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                        <td>Document ID </td>
                        <td>
                            <asp:TextBox ID="TextBoxDocID" runat="server" ReadOnly="true" Text='<%# Bind("DocumentID")%>'>
                            </asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            Project</td>
                        <td>
                            <asp:DropDownList ID="DropDownListPrjEdit" runat="server" DataSourceID="ObjectDataSourcePrj" 
                                DataTextField="ProjectName" DataValueField="ProjectID" AutoPostBack="True" 
                                SelectedValue='<%# Bind("ProjectID")%>' >
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPrj" runat="server" Display="Dynamic"
                                ErrorMessage="Required" ControlToValidate="DropDownListPrjEdit" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            ContractID
                        </td>
                        <td>

                            <asp:TextBox ID="TextBoxContractID" runat="server" Text='<%# Bind("ContractID")%>' ReadOnly="true" >
                            </asp:TextBox>

                        </td>
                        <td>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            Client</td>
                        <td>
                            <asp:DropDownList ID="DropDownListClient" runat="server" DataSourceID="ObjectDataSourceClientByProject" 
                                DataTextField="ClientName" DataValueField="ClientID" SelectedValue='<%# Bind("ClientID")%>' >
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

                            <asp:TextBox ID="TextBoxAddendumID" runat="server" Text='<%# Bind("AddendumID")%>' ReadOnly="true" >
                            </asp:TextBox>

                        </td>
                        <td>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            Document Date
                        </td>
                        <td>

                            <asp:TextBox ID="TextBoxDocDate" runat="server" Text='<%# Bind("DocumentDate")%>'  >
                            </asp:TextBox>

                            <asp:CalendarExtender ID="CalendarExtenderDocDate" runat="server" CssClass="cal_Theme1"
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

                            FxRate</td>
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
                            FxRate Type</td>
                        <td>
                            <asp:DropDownList ID="DropDownListFxRateType" runat="server" DataSourceID="ObjectDataSourceFxRateType" 
                                DataTextField="FxRateType" DataValueField="FxRateTypeID" SelectedValue='<%# Bind("FxRateTypeID")%>'>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorFxRateType" runat="server" Display="Dynamic"
                                ErrorMessage="Required" ControlToValidate="DropDownListFxRateType" InitialValue="0"></asp:RequiredFieldValidator>
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
                                DataTextField="AccountName" DataValueField="AccountID" SelectedValue='<%# Bind("AccountID")%>'>
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
                            &nbsp;
                            <asp:Button ID="ButtonUploadEdit" runat="server" Text="Upload" CausesValidation="false"  
                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="UploadDocument" />
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
                        <td>Doc No</td>
                        <td>
                            <asp:TextBox ID="TextBoxDocNo" runat="server" Text='<%# Bind("DocNo")%>'></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDocNo" runat="server" ControlToValidate="TextBoxDocNo" Display="Dynamic" ErrorMessage="Required" ></asp:RequiredFieldValidator>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>

             </EditItemTemplate>

         </asp:TemplateField>

        </Columns>
    </asp:GridView>
    <br />

</asp:Content>


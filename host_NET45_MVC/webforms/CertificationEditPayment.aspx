<%@ Page Title="" Language="VB" MasterPageFile="~/SiteCertificate.master" AutoEventWireup="false" CodeFile="CertificationEditPayment.aspx.vb" Inherits="CertificationEditPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <div>
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" />
    </div>

        <asp:ObjectDataSource ID="ObjectDataSourcePayment" runat="server" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByPrjClntDocInv" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_PaymentsTableAdapter" UpdateMethod="Update">
            <DeleteParameters>
                <asp:Parameter Name="Original_PaymentID" Type="Int32" />
            </DeleteParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
                <asp:ControlParameter ControlID="DropDownListClient" DefaultValue="0" Name="ClientID" PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownListDocument" DefaultValue="0" Name="DocumentID" PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="DropDownListInvoice" DefaultValue="0" Name="InvoiceID" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="InvoiceID" Type="Int32" />
                <asp:Parameter Name="PaymentDate" Type="DateTime" />
                <asp:Parameter Name="PaymentAmount" Type="Decimal" />
                <asp:Parameter Name="Original_PaymentID" Type="Int32" />
            </UpdateParameters>
        </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="ObjectDataSourceProject" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByPaidProject" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_ProjectClientJunctionPrjNameAdapter"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceClient" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByPaidClient" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_ProjectClientJunctionClientNameAdapter">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceDocument" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByPaidDocsByProjectandClient" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.DataTableDocsIDForDDLTableAdapter">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
            <asp:ControlParameter ControlID="DropDownListClient" DefaultValue="0" Name="ClientID" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceInvoice" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByPaidInvoiceByProjectClientDoc" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.DataTableInvoiceIDForDDLTableAdapter">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
            <asp:ControlParameter ControlID="DropDownListClient" DefaultValue="0" Name="ClientID" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="DropDownListDocument" DefaultValue="0" Name="DocumentID" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>

<table class="CertfDDLTable">

    <tr class="osman" >
        <td>
            Project
        </td>
        <td>
            Client
        </td>
        <td>
            Document
        </td>
        <td>
            Invoice
        </td>
    </tr>

    <tr>
        <td>
            <asp:DropDownList ID="DropDownListPrj" runat="server" AutoPostBack="True" 
                OnDataBound="DropDownListPrj_DataBound" DataSourceID="ObjectDataSourceProject" DataTextField="ProjectName" DataValueField="ProjectID" >
            </asp:DropDownList>
        </td>
        <td>
            <asp:DropDownList ID="DropDownListClient" runat="server" AutoPostBack="True"
                OnDataBound="DropDownListClient_DataBound" DataSourceID="ObjectDataSourceClient" DataTextField="ClientName" DataValueField="ClientID" >
            </asp:DropDownList>
        </td>
        <td>
            <asp:DropDownList ID="DropDownListDocument" runat="server" AutoPostBack="True" DataSourceID="ObjectDataSourceDocument" DataTextField="DocNo" DataValueField="DocumentID" >
            </asp:DropDownList>
        </td>
        <td>
            <asp:DropDownList ID="DropDownListInvoice" runat="server" AutoPostBack="True" DataSourceID="ObjectDataSourceInvoice" DataTextField="InvoiceID" DataValueField="InvoiceID" >
            </asp:DropDownList>
        </td>
    </tr>

</table>

    <br /><br />

    <asp:GridView ID="GridViewPayment" runat="server" AutoGenerateColumns="False" DataKeyNames="PaymentID" DataSourceID="ObjectDataSourcePayment">
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
                    <table class="CertificationDocumentEnterTable">
                        <tr>
                            <td>
                                Invoice ID
                            </td>
                            <td>

                                <asp:TextBox ID="TextBoxInvoiceID" runat="server" Text='<%# Bind("InvoiceID")%>' ReadOnly="true" >
                                </asp:TextBox>

                            </td>
                            <td style="width:150px;">

                            </td>
                            <td>

                            </td>
                            <td>

                            </td>
                            <td>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                Payment Date
                            </td>
                            <td>

                                <asp:TextBox ID="PaymentDateTextBox" runat="server" Text='<%# Bind("PaymentDate") %>' ReadOnly="true" />

                            </td>
                            <td>

                            </td>
                            <td>

                            </td>
                            <td>

                            </td>
                            <td>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                Payment Amount Exc. VAT
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxPaymentValue" runat="server" Text='<%# Bind("PaymentAmount")%>' ReadOnly="true" >
                                </asp:TextBox>
                            </td>
                            <td>

                            </td>
                            <td>
                        
                            </td>
                            <td>

                            </td>
                            <td>

                            </td>
                        </tr>
                    </table>

                </ItemTemplate>

                <EditItemTemplate>

                    <table class="CertificationDocumentEnterTable" style="background-color:#F0FFFF; ">
                        <tr>
                            <td>
                                Invoice ID
                            </td>
                            <td>

                                <asp:TextBox ID="TextBoxInvoiceID" runat="server" Text='<%# Bind("InvoiceID")%>' ReadOnly="true" >
                                </asp:TextBox>

                            </td>
                            <td style="width:150px;">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorInvoiceID" runat="server" Display="Dynamic"
                                    ErrorMessage="Required" ControlToValidate="TextBoxInvoiceID" ></asp:RequiredFieldValidator>
                            </td>
                            <td>

                            </td>
                            <td>

                            </td>
                            <td>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                Payment Date
                            </td>
                            <td>

                                <asp:TextBox ID="PaymentDateTextBox" runat="server" Text='<%# Bind("PaymentDate") %>' />

                                <asp:CalendarExtender ID="CalendarExtenderPaymentDate" runat="server" CssClass="cal_Theme1"
                                TargetControlID="PaymentDateTextBox" format="dd/MM/yyyy" >
                                </asp:CalendarExtender>

                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPaymentDate" runat="server" 
                                    ErrorMessage="Required" ControlToValidate="PaymentDateTextBox" Display="Dynamic" >
                                </asp:RequiredFieldValidator>

                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorPaymentDate" ControlToValidate="PaymentDateTextBox" Display="Dynamic"
                                runat="server" ErrorMessage="dd/mm/yyyy"  ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" >
                                </asp:RegularExpressionValidator>

                            </td>
                            <td>

                            </td>
                            <td>

                            </td>
                            <td>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                Payment Amount Exc. VAT
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxPaymentValue" runat="server" Text='<%# Bind("PaymentAmount")%>' >
                                </asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPaymentValue" runat="server" 
                                    ErrorMessage="Required" ControlToValidate="TextBoxPaymentValue" Display="Dynamic">
                                </asp:RequiredFieldValidator>

                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorPaymentValue" ControlToValidate="TextBoxPaymentValue" Display="Dynamic"
                                runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>

                                <asp:CompareValidator ID="CompareValidatorInvTotal" runat="server" ControlToValidate="TextBoxPaymentValue" ErrorMessage="To be provided" ValueToCompare="0" Operator="LessThanEqual" Type="Double" Display="Dynamic" ></asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidatorNotZero" runat="server" ControlToValidate="TextBoxPaymentValue" ErrorMessage="Cannot be zero" ValueToCompare="0" Operator="GreaterThan" Type="Double"  Display="Dynamic" ></asp:CompareValidator>

                            </td>
                            <td>
                        
                            </td>
                            <td>

                            </td>
                            <td>

                            </td>
                        </tr>
                    </table>

                </EditItemTemplate>

            </asp:TemplateField>

        </Columns>
    </asp:GridView>

</asp:Content>


<%@ Page Title="" Language="VB" MasterPageFile="~/SiteCertificate.master" AutoEventWireup="false" CodeFile="CertificationEditInvoice.aspx.vb" Inherits="CertificationEditInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <div>
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" />
    </div>

        <asp:ObjectDataSource ID="ObjectDataSourceProject" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByInvoicedProjects" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_ProjectClientJunctionPrjNameAdapter"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceClient" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByInvoicedClient" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_ProjectClientJunctionClientNameAdapter">
            <SelectParameters>
             <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
            </SelectParameters>
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="ObjectDataSourceInvoice" runat="server" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByPrjClientDocument" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_InvoiceTableAdapter" UpdateMethod="Update">
            <DeleteParameters>
                <asp:Parameter Name="Original_InvoiceID" Type="Int32" />
            </DeleteParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
                <asp:ControlParameter ControlID="DropDownListClient" DefaultValue="0" Name="ClientID" PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownListDocument" DefaultValue="0" Name="DocumentID" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="DocumentID" Type="Int32" />
                <asp:Parameter Name="InvoiceNumber" Type="String" />
                <asp:Parameter Name="InvoiceDate" Type="DateTime" />
                <asp:Parameter Name="InvoiceValue" Type="Decimal" />
                <asp:Parameter Name="Actual" Type="Boolean" />
                <asp:Parameter Name="Original_InvoiceID" Type="Int32" />
            </UpdateParameters>
        </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="ObjectDataSourceDocument" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataInvoicedDocsByProjectandClient" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.DataTableDocsIDForDDLTableAdapter">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
            <asp:ControlParameter ControlID="DropDownListClient" DefaultValue="0" Name="ClientID" PropertyName="SelectedValue" Type="String" />
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
            <asp:DropDownList ID="DropDownListDocument" runat="server" AutoPostBack="True" DataSourceID="ObjectDataSourceDocument" DataTextField="DocumentID" DataValueField="DocumentID" >
            </asp:DropDownList>
        </td>
    </tr>

</table>

    <br /> <br />
        <asp:GridView ID="GridViewInvoice" runat="server" AutoGenerateColumns="False" DataKeyNames="InvoiceID" DataSourceID="ObjectDataSourceInvoice">
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
                                    Document ID
                                </td>
                                <td>

                                    <asp:TextBox ID="TextBoxDocumentID" runat="server" Text='<%# Bind("DocumentID")%>' ReadOnly="true"  >
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
                                    Invoice Number
                                </td>
                                <td>

                                    <asp:TextBox ID="TextBoxInvoiceNumber" runat="server" Text='<%# Bind("InvoiceNumber")%>' ReadOnly="true"  >
                                    </asp:TextBox>

                                </td>
                                <td >

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
                                    Invoice Date
                                </td>
                                <td>

                                    <asp:TextBox ID="TextBoxInvoiceDate" runat="server" Text='<%# Bind("InvoiceDate")%>' ReadOnly="true"  >
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
                            <tr>
                                <td>
                                    Invoice Value Exc. VAT
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxInvoiceValue" runat="server" Text='<%# Bind("InvoiceValue")%>' ReadOnly="true"  >
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
                            <tr>
                                <td>
                                    Actual
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownListActual" runat="server" >
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorActual" runat="server" Display="Dynamic"
                                        ErrorMessage="Required" ControlToValidate="DropDownListActual" InitialValue="0"></asp:RequiredFieldValidator>
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
                                    Document ID
                                </td>
                                <td>

                                    <asp:TextBox ID="TextBoxDocumentID" runat="server" Text='<%# Bind("DocumentID")%>' ReadOnly="True" >
                                    </asp:TextBox>

                                </td>
                                <td style="width:150px;">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorDocID" runat="server" Display="Dynamic"
                                        ErrorMessage="Required" ControlToValidate="TextBoxDocumentID" ></asp:RequiredFieldValidator>
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
                                    Invoice Number
                                </td>
                                <td>

                                    <asp:TextBox ID="TextBoxInvoiceNumber" runat="server" Text='<%# Bind("InvoiceNumber")%>' >
                                    </asp:TextBox>

                                </td>
                                <td >
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorInvoiceNumber" runat="server" Display="Dynamic"
                                        ErrorMessage="Required" ControlToValidate="TextBoxInvoiceNumber" ></asp:RequiredFieldValidator>
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
                                    Invoice Date
                                </td>
                                <td>

                                    <asp:TextBox ID="TextBoxInvoiceDate" runat="server" Text='<%# Bind("InvoiceDate")%>' >
                                    </asp:TextBox>

                                    <asp:CalendarExtender ID="CalendarExtenderInvoiceDate" runat="server" CssClass="cal_Theme1"
                                    TargetControlID="TextBoxInvoiceDate" format="dd/MM/yyyy" >
                                    </asp:CalendarExtender>

                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorInvoiceDate" runat="server" 
                                        ErrorMessage="Required" ControlToValidate="TextBoxInvoiceDate" Display="Dynamic" >
                                    </asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorInvoiceDate" ControlToValidate="TextBoxInvoiceDate" Display="Dynamic"
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
                                    Invoice Value Exc. VAT
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxInvoiceValue" runat="server" Text='<%# Bind("InvoiceValue")%>' >
                                    </asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorInvoiceValue" runat="server" 
                                        ErrorMessage="Required" ControlToValidate="TextBoxInvoiceValue" Display="Dynamic">
                                    </asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorInvoiceValue" ControlToValidate="TextBoxInvoiceValue" Display="Dynamic"
                                    runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>

                                    <asp:CompareValidator ID="CompareValidatorInvTotal" runat="server" ControlToValidate="TextBoxInvoiceValue" ErrorMessage="To be provided" ValueToCompare="0" Operator="LessThanEqual" Type="Double" Display="Dynamic" ></asp:CompareValidator>
                                    <asp:CompareValidator ID="CompareValidatorNotZero" runat="server" ControlToValidate="TextBoxInvoiceValue" ErrorMessage="Cannot be zero" ValueToCompare="0" Operator="GreaterThan" Type="Double"  Display="Dynamic" ></asp:CompareValidator>

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
                                    Actual
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownListActual" runat="server" SelectedValue='<%# Bind("Actual")%>' >
                                        <asp:ListItem Value="True">YES</asp:ListItem>
                                        <asp:ListItem Value="False" >NO</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorActual" runat="server" Display="Dynamic"
                                        ErrorMessage="Required" ControlToValidate="DropDownListActual" InitialValue="0"></asp:RequiredFieldValidator>
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


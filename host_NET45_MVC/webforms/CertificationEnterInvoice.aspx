<%@ Page Title="" Language="VB" MasterPageFile="~/SiteCertificate.master" AutoEventWireup="false" CodeFile="CertificationEnterInvoice.aspx.vb" Inherits="CertificationEnterInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <div>
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" />
    </div>

    <asp:ObjectDataSource ID="ObjectDataSourceInvoice" runat="server" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_InvoiceTableAdapter">
        <InsertParameters>
            <asp:Parameter Name="DocumentID" Type="Int32" />
            <asp:Parameter Name="InvoiceID" Type="Int32" />
            <asp:Parameter Name="InvoiceNumber" Type="String" />
            <asp:Parameter Name="InvoiceDate" Type="DateTime" />
            <asp:Parameter Name="InvoiceValue" Type="Decimal" />
            <asp:Parameter Name="Actual" Type="Boolean" />
        </InsertParameters>
    </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="ObjectDataSourceProject" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByDocumentedProjects" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_ProjectClientJunctionPrjNameAdapter" ></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceClient" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByDocumentedClient" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.Table_ProjectClientJunctionClientNameAdapter">
            <SelectParameters>
             <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
            </SelectParameters>
        </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="ObjectDataSourceDocument" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataDocsByProjandClient" TypeName="PTS_App_Code_VB_Project.CertificationTableAdapters.DataTableDocsIDForDDLTableAdapter">
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
            Document No
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

            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDocument" runat="server" Display="Dynamic"
             ErrorMessage="Required" ControlToValidate="DropDownListDocument" InitialValue="0"></asp:RequiredFieldValidator>

            <asp:Label ID="LabelError" runat="server" Font-Size="Large" ForeColor="Red" Visible="false">All documents invoiced</asp:Label>

        </td>
    </tr>

</table>

    <br /> 

    <asp:FormView ID="FormViewEnterInvoice" runat="server" DataKeyNames="InvoiceID" DataSourceID="ObjectDataSourceInvoice" DefaultMode="Insert">
        <InsertItemTemplate>

            <table class="CertificationDocumentEnterTable">
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

                        <asp:CompareValidator ID="CompareValidatorDocTotal" runat="server" ControlToValidate="TextBoxInvoiceValue" ErrorMessage="To be provided" ValueToCompare="0" Operator="LessThanEqual" Type="Double"></asp:CompareValidator>

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
                            <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                            <asp:ListItem>YES</asp:ListItem>
                            <asp:ListItem>NO</asp:ListItem>
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
                <tr>
                    <td colspan="6" style="text-align:center;">
                        <asp:LinkButton ID="LinkButtonInsert" runat="server" CausesValidation="True" 
                            CommandName="Insert" Text="Insert"  Font-Size="X-Large" ForeColor="Red" />
                    </td>
                </tr>
            </table>

        </InsertItemTemplate>
    </asp:FormView>

    <hr />

    <asp:Label ID="LabelDocHistory" runat="server" Visible="false" Text="Documents History" Font-Size="Large" ></asp:Label>
    <asp:GridView ID="GridviewDocumentsHistory" runat="server" AutoGenerateColumns="False" DataKeyNames="DocumentID" DataSourceID="SqlDataSourceDocumentsHistory" Font-Size="10px" ShowHeader="false">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <table>
                        <tr style="background-color:#99CCFF; font-weight:bold;">
                            <td style="width:120px; text-align:center;">
                                Project Name:
                            </td>
                            <td style="width:120px; text-align:center;">
                                Client Name:
                            </td>
                            <td style="width:120px; text-align:center;">
                                Document Type:
                            </td>
                            <td style="width:80px; text-align:center;">
                                Document No:
                            </td>
                            <td style="width:80px; text-align:center;">
                                Document Date:
                            </td>
                            <td style="width:100px; text-align:center;">
                                External Value Exc. VAT:
                            </td>
                            <td style="width:100px; text-align:center;">
                                Advance Exc. VAT:
                            </td>
                            <td style="width:100px; text-align:center;">
                                Retention Exc. VAT:
                            </td>
                            <td style="width:80px; text-align:center;">
                                Document ID:
                            </td>
                            <td style="width:80px; text-align:center;">
                                Contract ID:
                            </td>
                            <td style="width:80px; text-align:center;">
                                Addendum ID:
                            </td>
                            <td style="width:80px; text-align:center;">
                                Account Name:
                            </td>
                            <td style="width:80px; text-align:center;">
                                Files:
                            </td>
                            <td>
                                Signed:
                            </td>
                        </tr>

                        <tr>
                            <td style="width:120px; text-align:center;">
                                <%--Project Name:--%>
                                <asp:Literal ID="LiteralProjectName" runat="server" Text='<%# Eval("ProjectName")%>' ></asp:Literal>
                            </td>
                            <td style="width:120px; text-align:center;">
                                <%--Client Name:--%>
                                <asp:Literal ID="LiteralClientName" runat="server" Text='<%# Eval("ClientName")%>' ></asp:Literal>
                            </td>
                            <td style="width:120px; text-align:center;">
                                <%--Document Type:--%>
                                <asp:Literal ID="LiteralDocument_Type" runat="server" Text='<%# Eval("Document_Type")%>' ></asp:Literal>
                            </td>
                            <td style="width:80px; text-align:center;">
                                <%--Document No:--%>
                                <asp:Literal ID="LiteralDocumentNo" runat="server" Text='<%# Eval("DocNo")%>' ></asp:Literal>
                            </td>
                            <td style="width:80px; text-align:center;">
                                <%--Document Date:--%>
                                <asp:Literal ID="LiteralDocumentDate" runat="server" Text='<%# Eval("DocumentDate", "{0:dd/MM/yyyy}")%>' ></asp:Literal>
                            </td>
                            <td style="width:100px; text-align:center;">
                                <%--External Value Exc. VAT:--%>
                                <asp:Literal ID="LiteralExternalValueExcVAT" runat="server" Text='<%# Eval("ExternalValueExcVAT", "{0:N2}")%>' ></asp:Literal>
                            </td>
                            <td style="width:100px; text-align:center;">
                                <%--Advance Exc. VAT:--%>
                                <asp:Literal ID="LiteralAdvanceExcVAT" runat="server" Text='<%# Eval("AdvanceExcVAT", "{0:N2}")%>' ></asp:Literal>
                            </td>
                            <td style="width:100px; text-align:center;">
                                <%--Retention Exc. VAT:--%>
                                <asp:Literal ID="LiteralRetentionExcVAT" runat="server" Text='<%# Eval("RetentionExcVAT", "{0:N2}")%>' ></asp:Literal>
                            </td>
                            <td style="width:80px; text-align:center;">
                                <%--Document ID:--%>
                                <asp:Literal ID="LiteralDocumentID" runat="server" Text='<%# Eval("DocumentID")%>' ></asp:Literal>
                            </td>
                            <td style="width:80px; text-align:center;">
                                <%--Contract ID:--%>
                                <asp:Literal ID="LiteralContractID" runat="server" Text='<%# Eval("ContractID")%>' ></asp:Literal>
                                <br />
                                <asp:HyperLink ID="HyperlinkFileContract" runat="server" Target="_blank" ForeColor="blue" >See Details</asp:HyperLink>
                            </td>
                            <td style="width:80px; text-align:center;">
                                <%--Addendum ID:--%>
                                <asp:Literal ID="LiteralAddendumID" runat="server" Text='<%# Eval("AddendumID")%>' ></asp:Literal>
                                <br />
                                <asp:HyperLink ID="HyperlinkFileAddendum" runat="server" Target="_blank" ForeColor="blue" >See Details</asp:HyperLink>
                            </td>
                            <td style="width:80px; text-align:center;">
                                <%--Account Name:--%>
                                <asp:Literal ID="LiteralAccountName" runat="server" Text='<%# Eval("AccountName")%>' ></asp:Literal>
                            </td>
                            <td style="width:80px; text-align:center;">
                                <%--Files:--%>
                                <asp:ImageButton ID="ImageButtonItem" runat="server" CommandName="OpenDocument" CausesValidation="false"
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ScanLink")%>'/>
                            </td>
                            <td >
                                <%--Signed:--%>
                                <asp:CheckBox ID="CheckBoxSigned" runat="server" Checked='<%# Eval("Signed")%>' Enabled="False" />
                            </td>

                        </tr>
                    </table>

                   <asp:Label ID="LabelInvoiceHistory" runat="server" Visible="false" Text="Invoice History" Font-Size="Large" ></asp:Label>
                   <asp:GridView ID="GridviewInvoiceHistory" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSourceInvoiceHistory" Font-Size="10px" EmptyDataText="No Invoice Yet" OnDataBound="GridviewInvoiceHistory_DataBound">
                       <Columns>
                           <asp:TemplateField>
                               <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <td style="width:80px; text-align:center;">
                                                Document ID:
                                            </td>
                                            <td style="width:80px; text-align:center;">
                                                Invoice ID:
                                            </td>
                                            <td style="width:80px; text-align:center;">
                                                Invoice Number:
                                            </td>
                                            <td style="width:80px; text-align:center;">
                                                Invoice Date:
                                            </td>
                                            <td style="width:100px; text-align:center;">
                                                Invoice Value:
                                            </td>
                                            <td style="width:80px; text-align:center;">
                                                Actual:
                                            </td>
                                        </tr>
                                    </table>
                               </HeaderTemplate>
                               <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td style="width:80px; text-align:center;">
                                                <%--Document ID:--%>
                                                <asp:Literal ID="LiteralProjectName" runat="server" Text='<%# Eval("DocumentID")%>' ></asp:Literal>
                                            </td>
                                            <td style="width:80px; text-align:center;">
                                                <%--Invoice ID:--%>
                                                <asp:Literal ID="LiteralInvoiceID" runat="server" Text='<%# Eval("InvoiceID")%>' ></asp:Literal>
                                            </td>
                                            <td style="width:80px; text-align:center;">
                                                <%--Invoice Number:--%>
                                                <asp:Literal ID="LiteralInvoiceNumber" runat="server" Text='<%# Eval("InvoiceNumber")%>' ></asp:Literal>
                                            </td>
                                            <td style="width:80px; text-align:center;">
                                                <%--Invoice Date:--%>
                                                <asp:Literal ID="LiteralInvoiceDate" runat="server" Text='<%# Eval("InvoiceDate", "{0:dd/MM/yyyy}")%>' ></asp:Literal>
                                            </td>
                                            <td style="width:100px; text-align:center;">
                                                <%--Invoice Value:--%>
                                                <asp:Literal ID="LiteralInvoiceValue" runat="server" Text='<%# Eval("InvoiceValue", "{0:N2}")%>' ></asp:Literal>
                                            </td>
                                            <td style="width:80px; text-align:center;">
                                                <%--Actual:--%>
                                                <asp:Literal ID="LiteralActual" runat="server" Text='<%# Eval("Actual")%>' ></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                               </ItemTemplate>
                           </asp:TemplateField>
                       </Columns> 
                       <HeaderStyle BackColor="#99CCFF" />
                       <RowStyle Height="30px" />
                   </asp:GridView>
                    <br /><br />
                    <asp:SqlDataSource ID="SqlDataSourceInvoiceHistory" runat="server" ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                        SelectCommand=" SELECT ID, DocumentID, InvoiceID, InvoiceNumber, InvoiceDate, InvoiceValue, CASE WHEN Actual = 0 THEN N'NO' ELSE N'YES' END AS Actual FROM Certification.Table_Invoice WHERE DocumentID = @DocumentID ">
                        <SelectParameters>
                    <asp:Parameter DefaultValue="0" Name="DocumentID" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        <RowStyle Height="30px" />
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSourceDocumentsHistory" runat="server" ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand=" 
            IF @ProjectID <> 0 AND @ClientID = N'0' AND @DocumentID = 0
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

            IF @ProjectID <> 0 AND @ClientID <> N'0' AND @DocumentID = 0
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

            IF @ProjectID = 0 AND @ClientID <> N'0' AND @DocumentID = 0
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
            END

            IF @DocumentID <> 0
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
            WHERE DocumentID = @DocumentID
            END ">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="ProjectID" />
            <asp:Parameter DefaultValue="0" Name="ClientID" />
            <asp:Parameter DefaultValue="0" Name="DocumentID" />
        </SelectParameters>
    </asp:SqlDataSource>
    


    
</asp:Content>


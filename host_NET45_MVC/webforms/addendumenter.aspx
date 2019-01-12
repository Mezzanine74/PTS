<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="addendumenter.aspx.vb" Inherits="addendumenter_2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Addendums</title>

    <style type="text/css">
        .style1 {
            width: 200px !important;
        }

        .style3 {
            width: 200px !important;
        }

        .style4 {
            width: 200px !important;
        }

        .style5 {
            width: 200px !important;
        }

        .stylePercentWidth {
            width: 70px !important;
        }

        .styleToLeft {
            text-align: left !important;
            width: 300px !important;
        }

        .styleToLeft2 {
            text-align: left !important;
            width: 150px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:Panel ID="PanelContainerForNextPage" runat="server" CssClass="hidepanel">
        <asp:Label ID="LabelGridViewPagingStatusOnAddendum" runat="server"></asp:Label>
        <asp:Label ID="LabelGridViewPageSizeOnAddendum" runat="server"></asp:Label>
        <asp:Label ID="LabelGridViewPageNumberOnAddendum" runat="server"></asp:Label>
    </asp:Panel>

    <asp:Label ID="LabelContractIDonAddendum" runat="server" Visible="false"></asp:Label>
    <asp:FormView ID="FormViewAddendums" runat="server"
        DataSourceID="SqlDataSourceAddendums" AllowPaging="True"
        DataKeyNames="AddendumID" EmptyDataText="Empty" DefaultMode="Insert">

        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
            LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" />

        <EditItemTemplate>
        </EditItemTemplate>
        <InsertItemTemplate>

            <table style="margin: 2px;">
                <tr>
                    <td style="vertical-align: top">
                        <table style="border: thin ridge #E0E0E0; background-color: #F5F5F5; margin: 2px;">
                            <tr>
                                <td>
                                    <asp:Label ID="LabelInsert17" runat="server" CssClass="LabelContract"
                                        Text="Project" Width="100px" />
                                </td>
                                <td class="styleToLeft2">
                                    <asp:Label ID="LabelProjectName" runat="server" CssClass="LabelGeneral" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LabelInsert18" runat="server" CssClass="LabelContract"
                                        Text="Po No" Width="100px" />
                                </td>
                                <td class="styleToLeft2">
                                    <asp:Label ID="LabelPOno" runat="server" CssClass="LabelGeneral" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LabelInsert19" runat="server" CssClass="LabelContract"
                                        Text="SupplierName" Width="100px" />
                                </td>
                                <td class="styleToLeft2">
                                    <asp:Label ID="LabelSupplierName" runat="server" CssClass="LabelGeneral"
                                        E />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table style="border: thin ridge #E0E0E0; background-color: #F5F5F5; margin: 2px;">
                            <tr>
                                <td>
                                    <asp:Label ID="LabelInsert20" runat="server" CssClass="LabelContract"
                                        Text="ContractNo" Width="120px" />
                                </td>
                                <td class="styleToLeft">
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
                                        Text="Contract Value Exc. VAT" Width="120px" />
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
                            CommandName="Insert" CssClass="btn btn-mini btn-success" Text="Insert"
                            PostBackUrl="~/webforms/contractview.aspx" />
                        <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False"
                            CommandName="Cancel" CssClass="btn btn-mini btn-danger" Text="Cancel" />
                    </td>
                    <td></td>
                </tr>
            </table>
            <br />

            <table style="border: thin ridge #E0E0E0; background-color: #F5F5F5; margin: 2px;">
                <tr>
                    <td>

                        <asp:Label ID="LabelInsert4" runat="server" CssClass="LabelContract"
                            Text="AddnNo" Width="100px" />

                    </td>
                    <td>
                        <asp:TextBox ID="ContractNoTextBox" runat="server" CssClass="TextBoxContract"
                            Text='<%# Bind("AddendumNo") %>' />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorContractNoTextBox" runat="server"
                            ControlToValidate="ContractNoTextBox" CssClass="LabelGeneral" Display="Dynamic"
                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </td>
                    <td>

                        <asp:Label ID="LabelInsert10" runat="server" CssClass="LabelContract"
                            Text="Template DOC File" Width="100px" />
                        <asp:TextBox ID="LinkToTemplatefile_DOCTextBox" runat="server"
                            Text='<%# Bind("AddendumLinkToTemplatefile_DOC") %>' CssClass="hidepanel" />

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

                    </td>
                    <td>

                        <asp:FileUpload ID="FileUploadDOC" runat="server" CssClass="TextBoxContract" />

                        <asp:Button ID="ButtonUploadDOC" runat="server" CssClass="btn btn-mini btn-default" CausesValidation="False" OnClick="ButtonUploadDOC_Click"
                            Text="Upload" />

                    </td>
                </tr>
                <tr>
                    <td>

                        <asp:Label ID="LabelInsert6" runat="server" CssClass="LabelContract"
                            Text="Addn Value Exc. VAT" Width="120px" />

                    </td>
                    <td>

                        <asp:TextBox ID="ContractValue_woVATTextBox" runat="server"
                            CssClass="TextBoxContract" Text='<%# Bind("AddendumValue_woVAT") %>' />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                            ControlToValidate="ContractValue_woVATTextBox" CssClass="LabelGeneral"
                            ErrorMessage="not valid number" Display="Dynamic"
                            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>

                    </td>
                    <td>
                        <asp:Label ID="LabelInfoDOC" runat="server" CssClass="LabelGeneral"
                            Style="font-weight: 700"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelInsert9" runat="server" CssClass="LabelContract"
                            Text="AddnDescription" Width="120px" />
                    </td>
                    <td>
                        <asp:TextBox ID="ContractDescriptionTextBox" runat="server"
                            CssClass="TextBoxContract" Height="75px"
                            Text='<%# Bind("AddendumDescription") %>' TextMode="MultiLine"
                            Width="350px" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorContractDescriptionTextBox" runat="server"
                            ControlToValidate="ContractDescriptionTextBox" CssClass="LabelGeneral" Display="Dynamic"
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
                            Text='<%# Bind("AddendumGivenTo") %>' Visible="false" />
                    </td>
                    <td></td>
                </tr>
            </table>

            <br />

            <table style="border: thin ridge #E0E0E0; background-color: #F5F5F5; margin: 2px;">
                <tr>
                    <td class="style1"></td>
                    <td class="style3">
                        <asp:Label ID="LabelInsert12" runat="server" CssClass="LabelContract"
                            Text="SignedBySupplier" Width="100px" />
                        <asp:CheckBox ID="SignBySupplierCheckBox" runat="server" CssClass="LabelContract"
                            Checked='<%# Bind("AddendumSignBySupplier") %>' />
                    </td>
                    <td>
                        <asp:Label ID="LabelInsert13" runat="server" CssClass="LabelContract"
                            Text="SignedByMercury" Width="100px" />
                        <asp:CheckBox ID="SignByMercuryCheckBox" runat="server" CssClass="LabelContract"
                            Checked='<%# Bind("AddendumSignByMercury") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="style1"></td>
                    <td class="style3"></td>
                    <td>
                        <asp:Label ID="LabelInfoPDF" runat="server" CssClass="LabelGeneral"
                            Style="font-weight: 700"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td class="style1"></td>
                    <td class="style3"></td>
                    <td>
                        <asp:Label ID="LabelInsert14" runat="server" CssClass="LabelContract"
                            Text="Contract PDF" Width="100px" />
                        <asp:TextBox ID="LinkToPDFcopyTextBox" runat="server" CssClass="hidepanel"
                            Text='<%# Bind("AddendumLinkToPDFcopy") %>' />
                        <asp:FileUpload ID="FileUploadPDF" runat="server" CssClass="TextBoxContract" />
                        <asp:Button ID="ButtonUploadPDF" runat="server" CssClass="btn btn-mini btn-default" CausesValidation="False" OnClick="ButtonUploadPDF_Click"
                            Text="Upload" />
                    </td>
                </tr>
            </table>

            <br />

            <table style="border: thin ridge #E0E0E0; background-color: #F5F5F5; margin: 2px;">
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
                        <asp:CheckBox ID="ArchivedByMercuryCheckBox" runat="server" CssClass="LabelContract"
                            Checked='<%# Bind("AddendumArchivedByMercury") %>' />
                    </td>
                    <td>
                        <asp:Label ID="LabelRetention" runat="server" CssClass="LabelContract"
                            Text="Retention" Width="70px" />
                    </td>
                    <td class="stylePercentWidth">
                        <asp:DropDownList ID="DropDownListRetention" runat="server">
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
                    <td>
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
                            <asp:CheckBox ID="CheckBoxPenaltyToMercury" runat="server" CssClass="LabelContract" AutoPostBack="true" OnCheckedChanged="CheckBoxPenaltyToMercury_CheckedChanged"
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
           ,AttachmentExist, AddendumGivenTo, DeliveryTerms, Penalties, PenaltiesNote)
     VALUES
           (@ContractID
           ,@AddendumNo
           ,@AddendumDate
           ,@AddendumValue_woVAT
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
           ,@AttachmentExist, @AddendumGivenTo, @DeliveryTerms, @Penalties, @PenaltiesNote)"></asp:SqlDataSource>

</asp:Content>



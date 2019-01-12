<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="editpo.aspx.vb" Inherits="editpoR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <title>Edit Purchase Order</title>

    <script type="text/javascript">
        function pageLoad() {
        }
        function SetAutoCompleteWidth(source, EventArgs) {
            var target
            target = ((document.getElementBy) ? document.getElementById("AutoCompleteDiv") : document.all.AutoCompleteDiv);
            target.style.width = '441px';
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div id="ModalFramePoControl" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content modal_inlineBlock">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">!</h4>
                </div>
                <div class="modal-body">
                    You are exceeding budget.
                    <br />
                    PO Value cannot be greater than <asp:Literal ID="Literal_MaxAllowedPo" runat="server" Text="0"/> With VAT
                    <br />
                    Transaction terminated!
                </div>
            </div>
        </div>
    </div>

    <asp:ModalPopupExtender ID="ModalPopupExtenderDeleteWarning" runat="server"
        TargetControlID="ButtonDeleteWarning"
        PopupControlID="PanelDeleteWarning"
        BackgroundCssClass="modalBackground"
        CancelControlID="btnCancel"
        PopupDragHandleControlID="PanelDeleteWarning">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelDeleteWarning" runat="server" Style="display: none;">
        <div style="text-align: right">
            <asp:Button ID="Button1" runat="server" Text="X"
                Font-Bold="True" Font-Size="16px" ForeColor="White" BackColor="#FF0066"
                OnClientClick="changeClass" />
        </div>
        <asp:Panel ID="PnlInvoice" Visible="false" runat="server">
            <table style="background-color: #FFFFFF; padding: 10px; font-size: large; color: #333333; width: 750px;">
                <tr>
                    <td>
                        <img alt="" src="~/images/alert.png" />
                    </td>
                    <td>There is at least one <b style="color: #FF0000">invoice</b> under this PO.
                                                <br />
                        In order to delete this PO, you need to delete related invoices on <a href="/webforms/editinvoice.aspx" target="_blank">edit invoice page</a>
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <asp:Panel ID="PnlDelivery" Visible="false" runat="server">
            <table style="background-color: #FFFFFF; padding: 10px; font-size: large; color: #333333; width: 750px;">
                <tr>
                    <td>
                        <img alt="" src="~/images/alert.png" />
                    </td>
                    <td>There is <b style="color: #FF0000">delivery documents</b> against this PO.
                                                <br />
                        In order to delete this PO, you need to delete related delivery documents on <a href="/webforms/nakladnaya.aspx" target="_blank">Delivery Docs Page</a>
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <asp:Panel ID="PnlDeliveryCoeff" Visible="false" runat="server">
            <table style="background-color: #FFFFFF; padding: 10px; font-size: large; color: #333333; width: 750px;">
                <tr>
                    <td>
                        <img alt="" src="~/images/alert.png" />
                    </td>
                    <td>You cannot reduce PO value less than
                        <asp:Label ID="LabelMinPo" runat="server" CssClass="LabelContract"></asp:Label>. Otherwise it will be less than Collected Delivery Documents.
                                                <br />
                        In order to do this PO, you need to update related delivery documents on <a href="/webforms/nakladnaya.aspx" target="_blank">Delivery Docs Page</a>
                    </td>
                </tr>
            </table>
        </asp:Panel>

    </asp:Panel>
    <asp:Button ID="ButtonDeleteWarning" runat="server" CssClass="hidepanel" />

    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
        TargetControlID="ButtonClientSide"
        PopupControlID="panEdit"
        BackgroundCssClass="modalBackground"
        CancelControlID="btnCancel"
        PopupDragHandleControlID="panEdit">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panEdit" runat="server" Style="display: none;">
        <div style="text-align: right">
            <asp:Button ID="btnCancel" runat="server" Text="X"
                Font-Bold="True" Font-Size="16px" ForeColor="White" BackColor="#FF0066"
                OnClientClick="changeClass" />
        </div>
        <table style="background-color: #FFFFFF; padding: 10px; font-size: large; color: #333333">
            <tr>
                <td>
                    <img alt="" src="~/images/QuestionMark.png" />
                </td>
                <td>There is at least one <b style="color: #FF0000">payment</b> under this PO.
                                                <br />
                    You cannot change PO currency
                                                <br />
                    In order to change currency, existing payments needs to be deleted from database
                                                <br />
                    Please contact to Finance (<a href="mailto:julia.serik@mercuryeng.ru?Subject=PO currency needs to be changed">send email</a>).
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Button ID="ButtonClientSide" runat="server" CssClass="hidepanel" />



    <div class="btn-group">

        <asp:DropDownList ID="DropDownListPrj" runat="server" AutoPostBack="True"
            DataSourceID="SqlDataSourcePrj"
            DataTextField="ProjectName" DataValueField="ProjectID">
        </asp:DropDownList>

        <asp:DropDownList ID="DropDownListSupplier" runat="server" AutoPostBack="True"
            DataSourceID="SqlDataSourceSupplier"
            DataTextField="SupplierName" DataValueField="SupplierID">
        </asp:DropDownList>

        <asp:CheckBox ID="CheckBoxListCO" runat="server"
            Text="Change Order" AutoPostBack="true"></asp:CheckBox>

    </div>

    <hr />

    <asp:SqlDataSource ID="SqlDataSourceSupplier" runat="server"
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
        SelectCommand="SELECT RTRIM(Table6_Supplier.SupplierID) AS SupplierID, RTRIM(Table6_Supplier.SupplierName) AS SupplierName, Table1_Project.ProjectID FROM Table6_Supplier INNER JOIN Table2_PONo ON Table6_Supplier.SupplierID = Table2_PONo.SupplierID INNER JOIN Table1_Project ON Table2_PONo.Project_ID = Table1_Project.ProjectID GROUP BY Table6_Supplier.SupplierID, RTRIM(Table6_Supplier.SupplierName), Table1_Project.ProjectID HAVING (Table1_Project.ProjectID = @ProjectID) ORDER BY SupplierName">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" Name="ProjectID"
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server"
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
        SelectCommand="     SELECT * FROM (
							-- ONLY NEW GENERATION PROJECTS WHERE USER HAS ACCESS
                            SELECT 
                            dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID))  
                            AS ProjectName, dbo.Table_Approval_UserRolePrjectJunction.UserName 
                            FROM Table_Approval_UserRolePrjectJunction 
                            INNER JOIN dbo.Table1_Project ON dbo.Table_Approval_UserRolePrjectJunction.ProjectID = dbo.Table1_Project.ProjectID  
                            WHERE RoleName = N'InitiateContractAndAddendum' AND UserName = @UserName AND (dbo.Table1_Project.CurrentStatus = 1) 

							UNION ALL

							-- ONLY NOT NEW GENERATION PROJECTS WHERE USER HAS ACCESS
							SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID)) 
												  AS ProjectName, dbo.aspnet_Users.UserName
							FROM         dbo.Table1_Project INNER JOIN
												  dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN
												  dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId
							WHERE     (dbo.Table1_Project.CurrentStatus = 1) AND (dbo.aspnet_Users.UserName = @UserName) AND (dbo.Table1_Project.NewGeneration = 0)

							UNION ALL

							-- THIS IS SPECIAFALLY DESIGNED FOR ROMAn HOWEVER IT MAY BE USED FOR ALL OTHERS
							-- IT GIVE ACCESS TO ORDINARY USERS FOR APPROVAL MATRIX MANIPULATION PAGES
							SELECT     dbo.Table_ApprUserExcpAccessToActiveUsers.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID)) 
												  AS ProjectName, dbo.Table_ApprUserExcpAccessToActiveUsers.UserName
							FROM         dbo.Table_ApprUserExcpAccessToActiveUsers INNER JOIN
												  dbo.Table1_Project ON dbo.Table_ApprUserExcpAccessToActiveUsers.ProjectID = dbo.Table1_Project.ProjectID
							WHERE     (dbo.Table_ApprUserExcpAccessToActiveUsers.UserName = @UserName)

							) AS Source
							ORDER BY ProjectName ">
        <SelectParameters>
            <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName"
                PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:Label ID="Message" runat="server" CssClass="LabelMessage"></asp:Label>
    <asp:Label ID="MessageCostCode" runat="server" CssClass="LabelGeneral"
        Visible="False" ForeColor="Red" BackColor="#FFFF66"> Cost Code must be 10 character </asp:Label>

    <asp:DropDownList ID="DropDownListFinanceCheck" runat="server" Visible="false"
        DataSourceID="SqlDataSourceFinanceCheck" DataTextField="CostCode" DataValueField="CostCode">
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSourceFinanceCheck" runat="server"
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"></asp:SqlDataSource>

    <asp:Label ID="LabelMessageToKsenia" runat="server" Font-Size="X-Large"
        ForeColor="Red" Text="Temporarily blocked. Please apply to admin" Visible="False"></asp:Label>


    <asp:GridView ID="GridViewEditPO" runat="server" AutoGenerateColumns="False"
        DataKeyNames="PO_No" DataSourceID="SqlDataSourceEditPO" CssClass="table table-nonfluid table-hover" GridLines="None"
        AllowPaging="True" AllowSorting="True" PageSize="20" PagerSettings-Position="TopAndBottom">
        <Columns>
            <asp:TemplateField ShowHeader="False" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
                <EditItemTemplate>
                    <div class="btn-group-vertical" role="group">
                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True" CssClass="btn btn-minier btn-primary"
                            CommandName="Update" Text="Update"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CssClass="btn btn-minier btn-success "
                            CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </div>
                </EditItemTemplate>
                <ItemTemplate>
                    <div class="btn-group-vertical" role="group">
                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False" CssClass="btn btn-minier btn-success "
                            CommandName="Edit" Text="Edit"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonDelete" runat="server" CssClass="btn btn-minier btn-danger"
                            OnClientClick="return confirm('Are you sure you want to delete this record?');"
                            CommandName="Delete" Text="Delete"></asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="PO_No" SortExpression="PO_No" ItemStyle-Width="100" HeaderStyle-Width="100">
                <EditItemTemplate>
                    <asp:Label ID="LabePO" runat="server" Text='<%# Bind("PO_No") %>'></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="LabelCO" runat="server"
                        CssClass="LabelGeneral" Text="Change Order: "></asp:Label>
                    <asp:CheckBox ID="CheckBoxCO" runat="server"
                        Checked='<%# Bind("CO") %>' CssClass="LabelGeneral" />
                    <asp:Label ID="LabelApprMxViolation" runat="server" CssClass="hidepanel"></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelPO_NoInItem" runat="server" Text='<%# Bind("PO_No") %>'></asp:Label>
                    <br />
                    <asp:Label ID="LabelCOItem" runat="server" CssClass="LabelMessage"></asp:Label>
                </ItemTemplate>

            </asp:TemplateField>
            <asp:TemplateField HeaderText="SupplierID" SortExpression="SupplierID" ItemStyle-Width="100" HeaderStyle-Width="100" ControlStyle-Width="100" >
                <EditItemTemplate>
                    <asp:TextBox ID="SupplierIDTextBox" runat="server" OnTextChanged="SupplierIDTextBox_TextChanged" AutoPostBack="true"
                        Text='<%# Bind("SupplierID") %>' CssClass="TextBoxGeneralRev" />
                    <div id="AutoCompleteDiv">
                    </div>
                    <asp:FilteredTextBoxExtender ID="TextBox1_FilteredTextBoxExtender" Enabled="false"
                        runat="server" FilterType="Numbers" TargetControlID="SupplierIDTextBox">
                    </asp:FilteredTextBoxExtender>
                    <asp:AutoCompleteExtender ID="TextBox1_AutoCompleteExtender" runat="server"
                        CompletionInterval="0" CompletionListElementID="AutoCompleteDiv"
                        CompletionSetCount="12" MinimumPrefixLength="0"
                        ServiceMethod="SupplierIDEdit" OnClientShown="SetAutoCompleteWidth"
                        ServicePath="AutoComplete.asmx" TargetControlID="SupplierIDTextBox">
                    </asp:AutoCompleteExtender>

                    <br />
                    <asp:Label ID="LabelSupplierNameEdit" runat="server" Text='<%# Bind("SupplierName")%>' CssClass="alert alert-info" Font-Size="Small"></asp:Label>

                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("SupplierID") %>'></asp:Label>
                    <br />
                    <asp:Label ID="LabelSupplierName" runat="server" Text='<%# Bind("SupplierName")%>'></asp:Label>
                </ItemTemplate>

            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description" SortExpression="Description" ControlStyle-Width="130" HeaderStyle-Width="130" ItemStyle-Width="130">
                <EditItemTemplate>
                    <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>'
                        Height="72px" TextMode="MultiLine" CssClass="TextBoxGeneralRevMultiline"></asp:TextBox>
                    <asp:Panel ID="PanelLiteralDescriptionEdit" runat="server" CssClass="hidepanel">
                        <asp:Literal ID="LiteralDescriptionTextBox" runat="server"></asp:Literal>
                    </asp:Panel>
                </EditItemTemplate>
                <ItemTemplate>

                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Description") %>'></asp:Label>

                </ItemTemplate>

            </asp:TemplateField>
            <asp:TemplateField HeaderText="PO Value With VAT" SortExpression="TotalPrice" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100" ControlStyle-Width="100" ItemStyle-Width="100">
                <EditItemTemplate>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorTotalPrice" ControlToValidate="TextBoxTotalPrice" Display="Dynamic"
                        runat="server" ErrorMessage="Wrong format" ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>

                    <asp:TextBox ID="TextBoxTotalPrice" runat="server" Text='<%# Bind("TotalPrice") %>' CssClass="TextBoxGeneralRev"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTotalPrice" runat="server" Display="Dynamic"
                        ErrorMessage="Required" ControlToValidate="TextBoxTotalPrice"></asp:RequiredFieldValidator>

                    <div style="text-align: center">
                        <asp:HyperLink ID="HyperlinkEditSubPo" runat="server" Target="blank" Visible="false"
                            onmouseover="this.style.cursor='hand'" ForeColor="#0033CC" Font-Bold="True">edit supPo</asp:HyperLink>
                    </div>


                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("TotalPrice","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>

            </asp:TemplateField>
            <asp:TemplateField HeaderText="Currency" SortExpression="PO_Currency" ControlStyle-Width="51" HeaderStyle-Width="51" ItemStyle-Width="51">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownListCurrency" runat="server"
                        SelectedValue='<%# Bind("PO_Currency") %>' AppendDataBoundItems="True" CssClass="FontSizeBGcolorForEditControls">
                        <asp:ListItem Value="Rub">Ruble</asp:ListItem>
                        <asp:ListItem Value="Dollar">Dollar</asp:ListItem>
                        <asp:ListItem Value="Euro">Euro</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:ImageButton ID="ButtonWhy" runat="server" Visible="false"
                        ImageUrl="~/images/QuestionMark.png" OnClick="ButtonWhy_Click" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("PO_Currency") %>'></asp:Label>
                </ItemTemplate>

            </asp:TemplateField>
            <asp:TemplateField HeaderText="%VAT" SortExpression="VATpercent" ControlStyle-Width="60" HeaderStyle-Width="60">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxVATPercent" runat="server" Text='<%# Bind("VATpercent") %>' CssClass="TextBoxGeneralRev FontSizeBGcolorForEditControls"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="TextBoxVATPercent_FilteredTextBoxExtender"
                        runat="server" FilterType="Numbers" TargetControlID="TextBoxVATPercent">
                    </asp:FilteredTextBoxExtender>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="!" Display="Dynamic"
                        MaximumValue="18" MinimumValue="0" ControlToValidate="TextBoxVATPercent"></asp:RangeValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("VATpercent") %>'></asp:Label>
                </ItemTemplate>

                <ControlStyle Width="60px"></ControlStyle>

                <HeaderStyle Width="60px"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Code" SortExpression="CostCode">
                <EditItemTemplate>
                    <div>
                        <asp:CompareValidator ID="CompareValidatorCostCode" runat="server" Enabled="false"
                            ControlToValidate="DropDownListCostCode" ErrorMessage="please select costcode"
                            ValueToCompare="0" Operator="NotEqual" Display="Dynamic"></asp:CompareValidator>
                    </div>
                    <div>
                        <asp:DropDownList ID="DropDownListCostCode" runat="server"
                            DataSourceID="SqlDataSourceCostCode" DataTextField="CostCode_Description"
                            DataValueField="CostCode" Width="400px" Font-Size="10px"
                            Font-Names="Consolas" AutoPostBack="False" BackColor="#CCFFFF">
                        </asp:DropDownList>
                    </div>
                    <asp:SqlDataSource ID="SqlDataSourceCostCode" runat="server"
                        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"></asp:SqlDataSource>

                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelItemCostCode" runat="server" Text='<%# Bind("CostCode") %>'></asp:Label>
                </ItemTemplate>

            </asp:TemplateField>

            <asp:TemplateField HeaderText="PO Date" SortExpression="PO_Date" HeaderStyle-Width="100" ControlStyle-Width="100">
                <EditItemTemplate>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorPOdate" ControlToValidate="TextBoxPODateShown" Display="Dynamic"
                        runat="server" ErrorMessage="dd/mm/yyyy" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"></asp:RegularExpressionValidator>

                    <asp:TextBox ID="TextBoxPODateShown" runat="server" CssClass="TextBoxGeneralRev add_datepicker" Text='<%# Bind("PO_Date","{0:dd/MM/yyyy}")%>'></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPODate" runat="server" Display="Dynamic"
                        ErrorMessage="Required" ControlToValidate="TextBoxPODateShown"></asp:RequiredFieldValidator>

                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("PO_Date","{0:dd/MM/yyyy}")%>'></asp:Label>
                </ItemTemplate>

            </asp:TemplateField>

            <asp:TemplateField HeaderText="Notes" SortExpression="Notes" ControlStyle-Width="50" HeaderStyle-Width="50">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("Notes") %>' CssClass="FontSizeBGcolorForEditControls"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                </ItemTemplate>

                <ControlStyle Width="50px"></ControlStyle>

                <HeaderStyle Width="50px"></HeaderStyle>
            </asp:TemplateField>

        </Columns>
                <PagerSettings Position="TopAndBottom" />
                <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
                <HeaderStyle CssClass="headergridnew" />
    </asp:GridView>


    <asp:Panel ID="Panel2" runat="server" CssClass="hidepanel">

        <asp:SqlDataSource ID="SqlDataSourceEditPO" runat="server"
            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
            UpdateCommand="UPDATE View_EditPO SET SupplierID = @SupplierID,  Description = @Description, TotalPrice = @TotalPrice, PO_Currency = @PO_Currency, VATpercent = @VATpercent, CostCode = @CostCode, Notes = @Notes, PO_Date = @PO_Date, UpdatedBy = @UpdatedBy, PersonUpdated = @PersonUpdated, CO = @CO WHERE (PO_No = @PO_No)"
            DeleteCommand="DELETE FROM Table2_PONo WHERE PO_No = @PO_No">

            <DeleteParameters>
                <asp:Parameter Name="PO_No" Type="String" />
            </DeleteParameters>

        </asp:SqlDataSource>
        <asp:TextBox ID="TextBoxUserName" runat="server"></asp:TextBox>

        <asp:TextBox ID="TextBoxPOValueCheck" runat="server">0</asp:TextBox>
        <asp:DropDownList ID="DropDownListTotalInvoicedValue" runat="server"
            DataSourceID="SqlDataSourceTotalInvoiced" DataTextField="InvoiceSum"
            DataValueField="InvoiceSum" Height="16px" Width="163px">
        </asp:DropDownList>

        <asp:SqlDataSource ID="SqlDataSourceTotalInvoiced" runat="server"
            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
            SelectCommand="SELECT PO_No, InvoiceSum FROM dbo.View_POsumCommon2 WHERE (PO_No = @PO_No)">
            <SelectParameters>
                <asp:Parameter Name="PO_No" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:TextBox ID="TextBoxINNvatFREECheck" runat="server"></asp:TextBox>

        <asp:DropDownList ID="DropDownListINNcheck" runat="server"
            DataSourceID="SqlDataSourceINNcheck" DataTextField="VAT_Free"
            DataValueField="VAT_Free" Height="16px" Width="163px">
        </asp:DropDownList>

        <asp:SqlDataSource ID="SqlDataSourceINNcheck" runat="server"
            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
            SelectCommand="SELECT SupplierID, VAT_Free FROM dbo.Table6_Supplier WHERE (SupplierID = @SupplierID)">
            <SelectParameters>
                <asp:Parameter Name="SupplierID" />
            </SelectParameters>
        </asp:SqlDataSource>

    </asp:Panel>

</asp:Content>


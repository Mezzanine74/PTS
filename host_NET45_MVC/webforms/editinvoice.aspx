<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="editinvoice.aspx.vb" Inherits="editinvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Edit Invoice</title>    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
        TargetControlID="Button3"
        PopupControlID="Panel1"
        BackgroundCssClass="modalBackground"
        CancelControlID="Button2"
        PopupDragHandleControlID="Panel1">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" Style="display: none;">
        <div style="text-align: right">
            <asp:Button ID="Button2" runat="server" Text="X"
                Font-Bold="True" Font-Size="16px" ForeColor="White" BackColor="#FF0066"
                OnClientClick="changeClass" />
        </div>
        <table style="background-color: #FFFFFF; padding: 10px; font-size: large; color: #333333">
            <tr>
                <td>
                    <img alt="" src="~/images/alert.png" />
                </td>
                <td>There is a <b style="color: #FF0000">payment request</b> referring to this invoice.
                                                <br />
                    In order to delete this invoice, 
                                                <br />
                    ...you need to delete related payment request on <a href="/webforms/editpaymentreq.aspx" target="_blank">edit payment request page</a>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Button ID="Button3" runat="server" CssClass="hidepanel" />

                <div class="btn-group">
                    <asp:DropDownList ID="DropDownListPrj" runat="server" 
                        DataSourceID="SqlDataSourcePrj" DataTextField="ProjectName" 
                        DataValueField="ProjectID" Width="267px" AutoPostBack="True" >
                    </asp:DropDownList>

                   <asp:DropDownList ID="DropDownListSupplier" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListSupplier_SelectedIndexChanged1"
                        DataSourceID="SqlDataSourceSupplier" 
                        DataTextField="SupplierName" DataValueField="SupplierID" Width="267px">
                    </asp:DropDownList>
                </div>


                    <asp:SqlDataSource ID="SqlDataSourceSupplier" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                    SelectCommand="SELECT     TOP (100) PERCENT RTRIM(dbo.Table6_Supplier.SupplierID) AS SupplierID, RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName, dbo.Table1_Project.ProjectID
FROM         dbo.Table6_Supplier INNER JOIN
                      dbo.Table2_PONo ON dbo.Table6_Supplier.SupplierID = dbo.Table2_PONo.SupplierID INNER JOIN
                      dbo.Table1_Project ON dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID INNER JOIN
                      dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No
GROUP BY dbo.Table6_Supplier.SupplierID, RTRIM(dbo.Table6_Supplier.SupplierName), dbo.Table1_Project.ProjectID
HAVING      (dbo.Table1_Project.ProjectID = @ProjectID)
ORDER BY SupplierName">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" Name="ProjectID" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
    
    
    <br />
    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        
        SelectCommand = " IF @UserName = N'dzera'
						BEGIN
							-- GIVE ACCESS TO DZERA FOR EACH PROJECT
							SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID)) 
												  AS ProjectName, dbo.aspnet_Users.UserName
							FROM         dbo.Table1_Project INNER JOIN
												  dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN
												  dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId
							WHERE     (dbo.Table1_Project.CurrentStatus = 1) AND (dbo.aspnet_Users.UserName = @UserName)
							ORDER BY ProjectName 
						END

						IF @UserName <> N'dzera'
						BEGIN
							SELECT * FROM (
							-- ONLY NEW GENERATION PROJECTS WHERE USER HAS ACCESS
                            SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID)) 
                                                  AS ProjectName, dbo.Table_Approval_UserRolePrjectJunction.UserName
                            FROM         dbo.Table1_Project INNER JOIN
                                                  dbo.Table_Approval_UserRolePrjectJunction ON dbo.Table1_Project.ProjectID = dbo.Table_Approval_UserRolePrjectJunction.ProjectID
                            WHERE     (dbo.Table1_Project.CurrentStatus = 1) AND (dbo.Table_Approval_UserRolePrjectJunction.UserName = @UserName) AND 
                                                  (dbo.Table_Approval_UserRolePrjectJunction.RoleName = N'InitiateContractAndAddendum')

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
							ORDER BY ProjectName 
						END ">
        <SelectParameters>
            <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" 
                PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:Label ID="Message" runat="server" CssClass="LabelMessage"></asp:Label>

            <asp:GridView ID="GridViewEditInvoice" runat="server" 
                DataSourceID="SqlDataSourceEditInvoice" AutoGenerateColumns="False" GridLines="None" 
                CssClass="table table-nonfluid table-hover" AllowPaging="True" AllowSorting="True" DataKeyNames="InvoiceID"
                PageSize="20" PagerSettings-Position="TopAndBottom">
                <Columns>

                    <asp:TemplateField ShowHeader="False" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center" >
                        <EditItemTemplate>
                            <div class="btn-group-vertical" role="group" >
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

                    <asp:BoundField DataField="PO_No" HeaderText="PO_No" ReadOnly="True"
                        SortExpression="PO_No" HeaderStyle-Width="100px" ItemStyle-Width="100px" />

                    <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True"
                        SortExpression="Description" HeaderStyle-Width="130px" ItemStyle-Width="130px" />

                    <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" ReadOnly="True"
                        SortExpression="SupplierName" HeaderStyle-Width="100px" ItemStyle-Width="100px" />

                    <asp:TemplateField HeaderText="Invoice_No" SortExpression="Invoice_No" ItemStyle-Width="80" ControlStyle-Width="80" HeaderStyle-Width="80">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxInvoiceNo" runat="server" Text='<%# Bind("Invoice_No") %>' CssClass="TextBoxGeneralRev"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorInvoiceNo" runat="server"
                                ErrorMessage="Required" ControlToValidate="TextBoxInvoiceNo" Display="Dynamic"></asp:RequiredFieldValidator>

                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Invoice_No") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice_Date" SortExpression="Invoice_Date" HeaderStyle-Width="100" ItemStyle-Width="100" ControlStyle-Width="100">
                        <EditItemTemplate>

                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorInvoicedate" ControlToValidate="TextBoxInvoiceDateShown" Display="Dynamic"
                                runat="server" ErrorMessage="dd/mm/yyyy" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"></asp:RegularExpressionValidator>

                            <asp:TextBox ID="TextBoxInvoiceDateShown" runat="server" Text='<%# Bind("Invoice_Date","{0:dd/MM/yyyy}") %>' CssClass="TextBoxGeneralRev add_datepicker"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorInvoiceDate" runat="server" Display="Dynamic"
                                ErrorMessage="Required" ControlToValidate="TextBoxInvoiceDateShown"></asp:RequiredFieldValidator>

                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Invoice_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>


                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="InvoiceValue Exc VAT" SortExpression="InvoiceValue" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100" ItemStyle-Width="100" ControlStyle-Width="100">
                        <EditItemTemplate>

                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorInvoiceValue" ControlToValidate="TextBoxInvoiceValue" Display="Dynamic"
                                runat="server" ErrorMessage="Wrong format" ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>


                            <asp:TextBox ID="TextBoxInvoiceValue" runat="server" Text='<%# Bind("InvoiceValue") %>' CssClass="TextBoxGeneralRev"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorInvoiceValue" runat="server" Display="Dynamic"
                                ErrorMessage="Required" ControlToValidate="TextBoxInvoiceValue"></asp:RequiredFieldValidator>


                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("InvoiceValue","{0:###,###,###.00}") %>'></asp:Label>
                        </ItemTemplate>

                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:TemplateField>

                    <asp:BoundField DataField="PO_Currency" HeaderText="Currency" ReadOnly="True"
                        SortExpression="PO_Currency" HeaderStyle-Width="51px" ItemStyle-Width="51px" />

                    <asp:TemplateField HeaderText="Inv.Description" SortExpression="Notes" ItemStyle-Width="100" HeaderStyle-Width="100" ControlStyle-Width="100">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxNoteEdit" runat="server" Text='<%# Bind("Notes") %>' TextMode="MultiLine" CssClass="TextBoxGeneralRevMultiline"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Notes") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Invoice Type" ItemStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownListInvoiceType" runat="server" 
                                SelectMethod="GetDropDownListInvoiceType"
                                AppendDataBoundItems="true"
                                ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.db.Table3_Invoice_Type"
                                DataTextField="Type_name" DataValueField="Type_id">
                                <asp:ListItem Value="0">-</asp:ListItem>
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorInvoiceType" runat="server" InitialValue="0"
                                ErrorMessage="Required" ControlToValidate="DropDownListInvoiceType" Display="Dynamic">
                            </asp:RequiredFieldValidator>

                            <asp:HiddenField ID="HiddenInvoiceId" runat="server" Value='<%# Bind("InvoiceId")%>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Literal ID="LiteralInvoiceType" runat="server"></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <PagerSettings Position="TopAndBottom" />
                <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
                <HeaderStyle CssClass="headergridnew" />

            </asp:GridView>


    <asp:SqlDataSource ID="SqlDataSourceEditInvoice" runat="server"
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
        UpdateCommand="UPDATE Table3_Invoice
                        SET 
                        Invoice_No = @Invoice_No, 
                        Invoice_Date = @Invoice_Date, 
                        InvoiceValue = @InvoiceValue, 
                        Notes = @Notes 
                        WHERE (InvoiceID = @InvoiceID)"
        DeleteCommand=" DELETE FROM Table3_Invoice WHERE InvoiceID = @InvoiceID"></asp:SqlDataSource>

    <asp:TextBox ID="TextBoxUserName" runat="server" Visible="False"></asp:TextBox>

    <asp:Panel ID="Panel2" runat="server" CssClass="hidepanel">
        <asp:TextBox ID="TextBoxPOFromEditGridRow" runat="server">0</asp:TextBox>
        <asp:TextBox ID="TextBoxInvoiceValueBeforeEdit" runat="server">0</asp:TextBox>

        <asp:DropDownList ID="DropDownListTotalPoValue" runat="server"
            DataSourceID="SqlDataSourceTotalPOvalue" DataTextField="PO_No"
            DataValueField="PoSumExcVAT" Height="25px" Width="163px">
        </asp:DropDownList>

        <asp:SqlDataSource ID="SqlDataSourceTotalPOvalue" runat="server"
            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
            SelectCommand="SELECT PO_No, PoSumExcVAT FROM dbo.View_POsumCommon3 WHERE (PO_No = @PO_No)">
            <SelectParameters>
                <asp:Parameter Name="PO_No" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:DropDownList ID="DropDownListTotalInvoiced" runat="server"
            DataSourceID="SqlDataSourceTotalInvoiced" DataTextField="PO_No"
            DataValueField="InvoiceSum" Height="25px" Width="163px">
        </asp:DropDownList>

        <asp:SqlDataSource ID="SqlDataSourceTotalInvoiced" runat="server"
            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
            SelectCommand="SELECT PO_No, InvoiceSum FROM dbo.View_POsumCommon3 WHERE (PO_No = @PO_No)">
            <SelectParameters>
                <asp:Parameter Name="PO_No" />
            </SelectParameters>
        </asp:SqlDataSource>
    </asp:Panel>

    <asp:DropDownList ID="DropDownListFinanceCheck" runat="server" Visible="false"
        DataSourceID="SqlDataSourceFinanceCheck" DataTextField="InvoiceID" DataValueField="InvoiceID">
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSourceFinanceCheck" runat="server"
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"></asp:SqlDataSource>

</asp:Content>


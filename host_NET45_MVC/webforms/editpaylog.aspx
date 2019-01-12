<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="editpaylog.aspx.vb" Inherits="editpaylog" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <title>Edit Payments</title>
    <script type="text/javascript">

        
    </script>

    <style type="text/css">
        .style1
        {
            width: 295px;
        }
        .style2
        {
            width: 500px;
        }
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

                    <asp:DropDownList ID="DropDownListPrj" runat="server" 
                        DataSourceID="SqlDataSourcePrj" DataTextField="ProjectName" 
                        DataValueField="ProjectID" AutoPostBack="True" >
                    </asp:DropDownList>

                   <asp:DropDownList ID="DropDownListSupplier" runat="server" AutoPostBack="True" 
                        DataSourceID="SqlDataSourceSupplier" 
                        DataTextField="SupplierName" DataValueField="SupplierID" >
                    </asp:DropDownList>

                    <asp:SqlDataSource ID="SqlDataSourceSupplier" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                    SelectCommand="SELECT Table6_Supplier.SupplierID, RTRIM(Table6_Supplier.SupplierName) AS SupplierName, Table1_Project.ProjectID FROM Table6_Supplier INNER JOIN Table2_PONo ON Table6_Supplier.SupplierID = Table2_PONo.SupplierID INNER JOIN Table1_Project ON Table2_PONo.Project_ID = Table1_Project.ProjectID GROUP BY Table6_Supplier.SupplierID, RTRIM(Table6_Supplier.SupplierName), Table1_Project.ProjectID HAVING (Table1_Project.ProjectID = @ProjectID) ORDER BY SupplierName">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" Name="ProjectID" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
    
    
    <br />
    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        
        SelectCommand="SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, (RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5),dbo.Table1_Project.ProjectID))) as ProjectName, dbo.aspnet_Users.UserName FROM         dbo.Table1_Project INNER JOIN  dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN  dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId WHERE  (Table1_Project.CurrentStatus = 1) AND   (dbo.aspnet_Users.UserName = @UserName) ORDER BY dbo.Table1_Project.ProjectName">
        <SelectParameters>
            <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" 
                PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:Label ID="Message" runat="server" CssClass="LabelMessage"></asp:Label>

    <asp:GridView ID="GridViewEditPayLog" runat="server" GridLines="None" 
        AutoGenerateColumns="False" DataSourceID="SqlDataSourceEditPayLog" 
        CssClass="table table-nonfluid table-hover" AllowPaging="True" DataKeyNames="PayReqNo"  PageSize="20" PagerSettings-Position="TopAndBottom">
        <Columns>

                    <asp:TemplateField ShowHeader="False" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
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
            
            <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" 
                SortExpression="ProjectID" ItemStyle-CssClass="hidepanel"  
                HeaderStyle-CssClass="hidepanel" >
            </asp:BoundField>

            <asp:BoundField DataField="PayReqNo" HeaderText="PayReqNo" 
                SortExpression="PayReqNo" ItemStyle-CssClass="hidepanel"  
                HeaderStyle-CssClass="hidepanel" >                
            </asp:BoundField>
            
            <asp:TemplateField HeaderText="PO_No" SortExpression="PO_No" ItemStyle-Width="100" HeaderStyle-Width="100" >
                <EditItemTemplate>
                    <asp:Label ID="Labelpono1" runat="server" Text='<%# Bind("PO_No") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Labelpono" runat="server" Text='<%# Bind("PO_No") %>'></asp:Label>
                    <asp:Label ID="LabelPayReqNoInEdit" runat="server" visible="false" Text='<%# Bind("PayReqNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="SupplierName" SortExpression="SupplierName" ItemStyle-Width="100" HeaderStyle-Width="100" >
                <EditItemTemplate>
                    <asp:Label ID="Labelsuppliername1" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Labelsuppliername" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Description" SortExpression="Description" ItemStyle-Width="130" HeaderStyle-Width="130" >
                <EditItemTemplate>
                    <asp:Label ID="Labeldescription1" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Labeldescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Invoice_No" SortExpression="Invoice_No" ItemStyle-Width="80" HeaderStyle-Width="80" >
                <EditItemTemplate>
                    <asp:Label ID="LabelinvNo1" runat="server" Text='<%# Bind("Invoice_No") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelinvNo" runat="server" Text='<%# Bind("Invoice_No") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Inv. Date" SortExpression="Invoice_Date" ItemStyle-Width="80" HeaderStyle-Width="80">
                <EditItemTemplate>
                    <asp:Label ID="LabelDate1" runat="server" Text='<%# Bind("Invoice_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelDate" runat="server" Text='<%# Bind("Invoice_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="FinanceNo" SortExpression="FinanceNo" ControlStyle-Width="50" HeaderStyle-Width="50" ItemStyle-Width="50" >
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxfinNo" runat="server" Text='<%# Bind("FinanceNo") %>' CssClass="TextBoxGeneralRev" ></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorFinNo" runat="server" 
                    ErrorMessage="Reqrd" ControlToValidate="TextBoxfinNo" Display="Dynamic"></asp:RequiredFieldValidator>

                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("FinanceNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Paym. Date" SortExpression="PaymentDate" ControlStyle-Width="80" HeaderStyle-Width="80" ItemStyle-Width="80" >
                <EditItemTemplate>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorPaymentDate" ControlToValidate="TextBoxpaymentdateShown"  Display="Dynamic"
                    runat="server" ErrorMessage="dd/mm/yyyy"  ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"></asp:RegularExpressionValidator>

                    <asp:TextBox ID="TextBoxpaymentdateShown" runat="server" CssClass="TextBoxGeneralRev add_datepicker" 
                        Text='<%# Bind("PaymentDate","{0:dd/MM/yyyy}") %>' AutoPostBack="true" OnTextChanged="TextBoxpaymentdateShown_TextChanged" ></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPaymentDate" runat="server"   Display="Dynamic"
                    ErrorMessage="Required" ControlToValidate="TextBoxpaymentdateShown"></asp:RequiredFieldValidator>

                    <div style="display:none;"><asp:Label ID="LabelPayReqNo" runat="server" Text='<%# Bind("PayReqNo")%>' ></asp:Label></div>

                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("PaymentDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Amount" SortExpression="Amount" ItemStyle-HorizontalAlign="Right" ControlStyle-Width="80" HeaderStyle-Width="80" ItemStyle-Width="80" >
                <EditItemTemplate>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorAmount" ControlToValidate="TextBoxAmount"  Display="Dynamic"
                    runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>

                    <asp:TextBox ID="TextBoxAmount" runat="server" Text='<%# Bind("Amount") %>' CssClass="TextBoxGeneralRev" ></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorAmount" runat="server"   Display="Dynamic"
                    ErrorMessage="Required" ControlToValidate="TextBoxAmount"></asp:RequiredFieldValidator>

                    <asp:CompareValidator ID="CompareValidatorPaymentMax" runat="server" ControlToValidate="TextBoxAmount" Display="Dynamic" Type="Double" Operator="LessThan"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidatorPaymentMin" runat="server" ControlToValidate="TextBoxAmount" Display="Dynamic" Type="Double" Operator="GreaterThan"></asp:CompareValidator>

                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("Amount","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Currency" SortExpression="Currency" ControlStyle-Width="60" HeaderStyle-Width="60" ItemStyle-Width="60" >
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownListCurrency" runat="server"  Enabled ="false"
                            selectedvalue='<%# Bind("Currency") %>' AppendDataBoundItems="True" >
                            <asp:ListItem Value="Rub">Ruble</asp:ListItem>
                            <asp:ListItem Value="Dollar">Dollar</asp:ListItem>
                            <asp:ListItem Value="Euro">Euro</asp:ListItem>
                    </asp:DropDownList>                      
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelCurrency" runat="server" Text='<%# Bind("Currency") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Note" SortExpression="Note" ControlStyle-Width="50" HeaderStyle-Width="50" ItemStyle-Width="50" >
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxNote" runat="server" Text='<%# Bind("Note") %>' TextMode="MultiLine" CssClass="TextBoxGeneralRevMultiline" ></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelNote" runat="server" Text='<%# Bind("Note") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

        <asp:TemplateField HeaderText="Dollar" ControlStyle-Width="60" HeaderStyle-Width="60" ItemStyle-Width="60" >
         <ItemTemplate>
           <asp:Label ID="LabelDollar" runat="server" Text='<%# Bind("RubbleDollar")%>' CssClass ="LabelGeneral"  ></asp:Label>
         </ItemTemplate>
         <EditItemTemplate>
           <asp:TextBox ID="TextBoxDollar" runat="server" Text='<%# Bind("RubbleDollar")%>' EnableViewState="True" CssClass="TextBoxGeneralRev" Width="60px"
               AutoPostBack="True" CausesValidation="True" OnTextChanged="TextBoxDollar_TextChanged" ></asp:TextBox>
           <asp:RegularExpressionValidator ID="RegularExpressionValidatorDollar" ControlToValidate="TextBoxDollar" Display="Dynamic"
            runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
           </asp:RegularExpressionValidator>
         </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Euro" ControlStyle-Width="60" HeaderStyle-Width="60" ItemStyle-Width="60" >
         <ItemTemplate>
           <asp:Label ID="LabelEuro" runat="server" Text='<%# Bind("RubbleEuro")%>' CssClass ="LabelGeneral"  ></asp:Label>
         </ItemTemplate>
         <EditItemTemplate>
           <asp:TextBox ID="TextBoxEuro" runat="server" Text='<%# Bind("RubbleEuro")%>' EnableViewState="True" CssClass="TextBoxGeneralRev" Width="60px"
               AutoPostBack="True" CausesValidation="True" Font-Size="11px" OnTextChanged="TextBoxEuro_TextChanged" ></asp:TextBox>
           <asp:RegularExpressionValidator ID="RegularExpressionValidatorEuro" ControlToValidate="TextBoxEuro" Display="Dynamic"
            runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
           </asp:RegularExpressionValidator>
         </EditItemTemplate>
        </asp:TemplateField>

        </Columns>
                <PagerSettings Position="TopAndBottom" />
                <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
                <HeaderStyle CssClass="headergridnew" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceEditPayLog" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                
        SelectCommand="SELECT PO_No, RTRIM(SupplierName) AS SupplierName, SupplierID,  Description, RTRIM(Invoice_No) AS Invoice_No, 
                        Invoice_Date, RTRIM(FinanceNo) AS FinanceNo, PaymentDate, Amount, RTRIM(Currency) AS Currency, RTRIM(Note) AS Note, 
                        ProjectID, PayReqNo, RubbleDollar, RubbleEuro 
                        FROM dbo.View_EditPaylog WHERE (ProjectID = @ProjectID)    AND (SupplierID LIKE +'%'+@SupplierID+'%'  )" 
        UpdateCommand="UPDATE View_editpaylog SET FinanceNo = @FinanceNo, PaymentDate = @PaymentDate, Amount = @Amount, Currency = @Currency, Note = @Note, UpdatedBy= @UpdatedBy, PersonUpdated=@PersonUpdated, RubbleDollar=@RubbleDollar, RubbleEuro=@RubbleEuro WHERE (PayReqNo = @PayReqNo)"
        DeleteCommand="DELETE FROM Table5_PayLog WHERE PayReqNo = @PayReqNo">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" 
                Name="ProjectID" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="DropDownListSupplier" DefaultValue="0" 
                Name="SupplierID" PropertyName="SelectedValue" Type="String" />                
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="PayReqNo"  Type="Int32" />
        </DeleteParameters>
        
    </asp:SqlDataSource>
    <asp:TextBox ID="TextBoxUserName" runat="server" Visible="False"></asp:TextBox>

</asp:Content>

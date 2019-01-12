<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WebUserControl_PTS1Scomparison.ascx.vb" Inherits="WebUserControl_PTS1Scomparison" %>


    <asp:GridView ID="GridViewComparison" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSourceComparison" Font-Size="8px" HeaderStyle-BackColor="#99CCFF" ShowFooter="True">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonDelete" Runat="server" 
                    OnClientClick="return confirm('Are you sure you want to delete this record?');"
                    CommandName="DeleteFrom1S" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' Text="Delete" CssClass="LabelGeneral"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FinanceNo1S" HeaderText="Finance No 1S" ReadOnly="True" SortExpression="FinanceNo1S" />
            <asp:BoundField DataField="Supplier_1S" HeaderText="Supplier 1S" ReadOnly="True" SortExpression="Supplier_1S"  HeaderStyle-Width="100px" >
                <HeaderStyle Width="100px"></HeaderStyle>
            </asp:BoundField>
            <asp:BoundField DataField="PaymentDate_1S" HeaderText="Payment Date 1S" ReadOnly="True" SortExpression="PaymentDate_1S" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="60px" >
                <HeaderStyle Width="60px"></HeaderStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="Payment Amount 1S" SortExpression="PaymentAmount_1S"  FooterStyle-HorizontalAlign="Right" FooterStyle-BackColor="#FFCC99" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Literal ID="Literal1" runat="server" Text='<%# Bind("PaymentAmount_1S", "{0:N2}") %>'></asp:Literal>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Literal ID="LiteralPaymentAmount_1SFooter" runat="server" ></asp:Literal>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FinanceNoPTS" HeaderText="Finance No PTS" ReadOnly="True" SortExpression="FinanceNoPTS" ItemStyle-CssClass="PTS_1S" >
                <ItemStyle CssClass="PTS_1S"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="ProjectName" HeaderText="Project Name" ReadOnly="True" SortExpression="ProjectName" />
            <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description" HeaderStyle-Width="200px" >
                <HeaderStyle Width="200px"></HeaderStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Invoice_No" HeaderText="Invoice No" ReadOnly="True" SortExpression="Invoice_No" />
            <asp:BoundField DataField="Invoice_Date" HeaderText="Invoice Date" ReadOnly="True" SortExpression="Invoice_Date"  DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="60px" >
                <HeaderStyle Width="60px"></HeaderStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Supplier_PTS" HeaderText="Supplier PTS" ReadOnly="True" SortExpression="Supplier_PTS"  HeaderStyle-Width="100px" >
                <HeaderStyle Width="100px"></HeaderStyle>
            </asp:BoundField>
            <asp:BoundField DataField="PaymentDate_PTS" HeaderText="Payment Date PTS" ReadOnly="True" SortExpression="PaymentDate_PTS"  DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="60px" >
                <HeaderStyle Width="60px"></HeaderStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="Payment Amount PTS" SortExpression="PaymentAmount_PTS"  FooterStyle-HorizontalAlign="Right" FooterStyle-BackColor="#FFCC99" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Literal ID="Literal2" runat="server" Text='<%# Bind("PaymentAmount_PTS", "{0:N2}") %>'></asp:Literal>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Literal ID="LiteralPaymentAmount_PTSFooter" runat="server" ></asp:Literal>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Supposed To Be Paid" SortExpression="SupposedToBePaid"  FooterStyle-HorizontalAlign="Right" FooterStyle-BackColor="#FFCC99" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Literal ID="Literal3" runat="server" Text='<%# Bind("SupposedToBePaid", "{0:N2}") %>'></asp:Literal>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Literal ID="LiteralSupposedToBePaidFooter" runat="server" ></asp:Literal>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Diff In Real Payments" SortExpression="DifferenceInRealPayments" FooterStyle-HorizontalAlign="Right" FooterStyle-BackColor="#FFCC99" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Literal ID="Literal4" runat="server" Text='<%# Bind("DifferenceInRealPayments", "{0:N2}") %>'></asp:Literal>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Literal ID="LiteralDifferenceInRealPaymentsFooter" runat="server" ></asp:Literal>
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>

<HeaderStyle BackColor="#99CCFF"></HeaderStyle>
    </asp:GridView>
    
    <asp:ObjectDataSource ID="ObjectDataSourceComparison" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="PTS_App_Code_VB_Project.MercuryTableAdapters.PaymentsPTS_1S_Comparison_TableAdapter">
        <SelectParameters>
            <asp:Parameter  Name="CreatedBy" Type="DateTime" />
        </SelectParameters>
    </asp:ObjectDataSource>


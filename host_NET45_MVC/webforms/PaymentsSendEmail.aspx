<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="PaymentsSendEmail.aspx.vb" Inherits="PaymentsSendEmail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<asp:GridView ID="GridViewDSP3HIDDEN" runat="server" AutoGenerateColumns="False" 
DataSourceID="SqlDataSourceDSP3HIDDEN" ShowFooter="True">
        <Columns>
            <asp:TemplateField HeaderText="Project" SortExpression="ProjectName" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="LabelProjectNameHIDDEN" runat="server" Text='<%# Bind("ProjectName") %>'></asp:Label>
                </ItemTemplate>

<ControlStyle Width="80px"></ControlStyle>

<HeaderStyle Width="80px"></HeaderStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="PO" SortExpression="PO_No" ControlStyle-Width="100" HeaderStyle-Width="100">
                <ItemTemplate>
                    <asp:Label ID="Label1HIDDEN" runat="server" Text='<%# Bind("PO_No") %>'></asp:Label>
                </ItemTemplate>

<ControlStyle Width="100px"></ControlStyle>

<HeaderStyle Width="100px"></HeaderStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Supplier" ControlStyle-Width="200" HeaderStyle-Width="200">
                <ItemTemplate>
                    <asp:Label ID="LabelSupplierHIDDEN" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                </ItemTemplate>

<ControlStyle Width="200px"></ControlStyle>

<HeaderStyle Width="200px"></HeaderStyle>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Description" SortExpression="Description" ControlStyle-Width="200" HeaderStyle-Width="200">
                <ItemTemplate>
                    <asp:Label ID="Label2HIDDEN" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>

<ControlStyle Width="200px"></ControlStyle>

<HeaderStyle Width="200px"></HeaderStyle>
            </asp:TemplateField>
            
                   <asp:TemplateField HeaderText="Total PO Value With VAT" ControlStyle-Width="80" HeaderStyle-Width="80">
                       <ItemTemplate>
                           <asp:Label ID="Label3HIDDEN" runat="server"    Text='<%# Bind("POtotalprice") %>'></asp:Label>
                       </ItemTemplate>

<ControlStyle Width="80px"></ControlStyle>

<HeaderStyle Width="80px"></HeaderStyle>
                   </asp:TemplateField>
            

                   <asp:TemplateField HeaderText="Invoice Value Without VAT" ControlStyle-Width="80" HeaderStyle-Width="80">
                       <ItemTemplate>
                            <asp:Label ID="Label11HIDDEN" runat="server" Text='<%# Bind("Invoice_value") %>'></asp:Label>
                       </ItemTemplate>

<ControlStyle Width="80px"></ControlStyle>

<HeaderStyle Width="80px"></HeaderStyle>
                   </asp:TemplateField>

                   <asp:TemplateField HeaderText="Cur." ControlStyle-Width="40" HeaderStyle-Width="40">
                       <ItemTemplate>
                           <div><asp:Label ID="Label5" runat="server" Text='<%# Bind("PO_Currency") %>'></asp:Label></div>
                       </ItemTemplate>

<ControlStyle Width="40px"></ControlStyle>

<HeaderStyle Width="40px"></HeaderStyle>
                   </asp:TemplateField>
            
            
            <asp:TemplateField HeaderText="VAT" SortExpression="VATpercent" ItemStyle-HorizontalAlign="Right" ControlStyle-Width="30" HeaderStyle-Width="30">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("VATpercent") %>'></asp:Label>
                </ItemTemplate>

<ControlStyle Width="30px"></ControlStyle>

<HeaderStyle Width="30px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="CostCode" SortExpression="CostCode" ControlStyle-Width="120" HeaderStyle-Width="120">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("CostCode") %>'></asp:Label>
                </ItemTemplate>

<ControlStyle Width="120px"></ControlStyle>

<HeaderStyle Width="120px"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PO Date" SortExpression="PO_Date" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("PO_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>

<ControlStyle Width="80px"></ControlStyle>

<HeaderStyle Width="80px"></HeaderStyle>
            </asp:TemplateField>
            
                   <asp:TemplateField HeaderText="Invoice No" ControlStyle-Width="100" HeaderStyle-Width="100">
                       <ItemTemplate>
                           <asp:Label ID="Label11invnoo" runat="server" Text='<%# Bind("Invoice_No") %>'></asp:Label>
                       </ItemTemplate>

<ControlStyle Width="100px"></ControlStyle>

<HeaderStyle Width="100px"></HeaderStyle>
                   </asp:TemplateField>

                   <asp:TemplateField HeaderText="Invoice Date " ControlStyle-Width="80" HeaderStyle-Width="80">
                       <ItemTemplate>
                           <div><asp:Label ID="Label12invdate" runat="server" Text='<%# Bind("Invoice_Date","{0:dd/MM/yyyy}") %>'></asp:Label></div>
                       </ItemTemplate>

<ControlStyle Width="80px"></ControlStyle>

<HeaderStyle Width="80px"></HeaderStyle>
                   </asp:TemplateField>


            <asp:TemplateField HeaderText="Site Rec" SortExpression="SiteRecordNo" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="Label12" runat="server" Text='<%# Bind("SiteRecordNo") %>'></asp:Label>
                </ItemTemplate>

<ControlStyle Width="80px"></ControlStyle>

<HeaderStyle Width="80px"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PayReqDate" SortExpression="PayReqDate" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("PayReqDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>

<ControlStyle Width="80px"></ControlStyle>

<HeaderStyle Width="80px"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Finc No" SortExpression="FinanceNo" ControlStyle-Width="40" HeaderStyle-Width="40">
                <ItemTemplate>
                    <asp:Label ID="Label17" runat="server" Text='<%# Bind("FinanceNo") %>'></asp:Label>
                </ItemTemplate>

<ControlStyle Width="40px"></ControlStyle>

<HeaderStyle Width="40px"></HeaderStyle>
            </asp:TemplateField>
            
                   <asp:TemplateField HeaderText="Payment Date"  ControlStyle-Width="80" HeaderStyle-Width="80">
                       <FooterTemplate>
                           <span style="font-size:11px; font-weight:bold;">Total Payment:</span>
                       </FooterTemplate>
                       <ItemTemplate>
                           <asp:Label ID="Label18" runat="server" Text='<%# Bind("PaymentDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                       </ItemTemplate>

<ControlStyle Width="80px"></ControlStyle>

<HeaderStyle Width="80px"></HeaderStyle>
                   </asp:TemplateField>

                   <asp:TemplateField HeaderText="Payment Value With VAT" ControlStyle-Width="80" HeaderStyle-Width="80">
                       <FooterTemplate>
                           <span style="font-size:11px; font-weight:bold;">
                           <asp:Literal ID="LiteralTotalPaymentWithVAT" runat="server"></asp:Literal>
                           </span>
                       </FooterTemplate>
                       <ItemTemplate>
                            <asp:Label ID="Label19" runat="server" Text='<%# Bind("Payment_amount") %>'></asp:Label>
                       </ItemTemplate>

<ControlStyle Width="80px"></ControlStyle>

<HeaderStyle Width="80px"></HeaderStyle>
                   </asp:TemplateField>

                   <asp:TemplateField HeaderText="Cur." ControlStyle-Width="40" HeaderStyle-Width="40">
                       <ItemTemplate>
                           <div><asp:Label ID="Label20" runat="server" Text='<%# Bind("Payment_currency") %>'></asp:Label></div>
                       </ItemTemplate>

<ControlStyle Width="40px"></ControlStyle>

<HeaderStyle Width="40px"></HeaderStyle>
                   </asp:TemplateField>
            
                   <asp:TemplateField HeaderText="Entered To PTS by" ControlStyle-Width="120" HeaderStyle-Width="120">
                       <ItemTemplate>
                           <div><asp:Label ID="LabelTimeStamp" runat="server" Text='<%# Bind("CreatedBy") %>'></asp:Label></div>
                       </ItemTemplate>

<ControlStyle Width="120px"></ControlStyle>

<HeaderStyle Width="120px"></HeaderStyle>
                   </asp:TemplateField>

                   <asp:TemplateField HeaderText="Payment Value Supposed To Be Paid In Ruble With VAT" ControlStyle-Width="80" HeaderStyle-Width="80">
                       <ItemTemplate>
                            <asp:Label ID="LabelSupposedToBePaid" runat="server" Text='<%# Bind("SupposedToBePaid") %>'></asp:Label>
                       </ItemTemplate>

<ControlStyle Width="80px"></ControlStyle>

<HeaderStyle Width="80px"></HeaderStyle>
                   </asp:TemplateField>

                   <asp:TemplateField HeaderText="Difference In Ruble With VAT" ControlStyle-Width="80" HeaderStyle-Width="80">
                       <ItemTemplate>
                            <asp:Label ID="LabelDifference" runat="server" Text='<%# Bind("Difference") %>'></asp:Label>
                       </ItemTemplate>

<ControlStyle Width="80px"></ControlStyle>

<HeaderStyle Width="80px"></HeaderStyle>
                   </asp:TemplateField>

        </Columns>
                <RowStyle  CssClass="GridItemNakladnaya" />
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSourceDSP3HIDDEN" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand="PaymentsToday" SelectCommandType="StoredProcedure">
    </asp:SqlDataSource>

</asp:Content>


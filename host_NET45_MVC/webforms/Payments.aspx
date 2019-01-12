<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="Payments.aspx.vb" Inherits="PaymentsASDFASDDF2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:DropDownList ID="DropDownListPrj" runat="server" AutoPostBack="True" 
        DataSourceID="SqlDataSourcePrj" 
        DataTextField="ProjectName" DataValueField="ProjectID" Width="267px">
    </asp:DropDownList>

    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                SelectCommand="SELECT ProjectID, ProjectName FROM (
		                        SELECT N'0' AS ProjectID, N'_All Projects' AS ProjectName

		                        UNION ALL

		                        SELECT ProjectID, ProjectName 
		                        FROM         dbo.View_Payment
		                        WHERE     (PaymentDate >= @Start) AND (PaymentDate <= @Finish)
		                        GROUP BY ProjectID, ProjectName 
	                        ) AS Source
	                        ORDER BY ProjectName ASC ">
            <SelectParameters>
                <asp:ControlParameter ControlID="lblStart" DefaultValue="0"  Name="Start" PropertyName="Text" Type="DateTime" />
                <asp:ControlParameter ControlID="lblFinish" DefaultValue="0"  Name="Finish" PropertyName="Text" Type="DateTime" />
            </SelectParameters>
    </asp:SqlDataSource>

    <asp:DropDownList ID="DropDownListSupplier" runat="server" AutoPostBack="True" 
        DataSourceID="SqlDataSourceSupplier" 
        DataTextField="SupplierName" DataValueField="SupplierID" >
    </asp:DropDownList>

    <asp:SqlDataSource ID="SqlDataSourceSupplier" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                SelectCommand=" IF @ProjectID = 0
                                BEGIN
	                                SELECT RTRIM(SupplierID) AS SupplierID, SupplierName FROM (
		                                SELECT N'0' AS SupplierID, N'_All Supplier' AS SupplierName

		                                UNION ALL

		                                SELECT SupplierID, SupplierName
		                                FROM         dbo.View_Payment
		                                WHERE     (PaymentDate >= @Start) AND (PaymentDate <= @Finish)
		                                GROUP BY SupplierID, SupplierName
	                                ) AS Source
	                                ORDER BY SupplierName ASC
                                END

                                IF @ProjectID <> 0
                                BEGIN
	                                SELECT RTRIM(SupplierID) AS SupplierID, SupplierName FROM (
		                                SELECT N'0' AS SupplierID, N'_All Supplier' AS SupplierName

		                                UNION ALL

		                                SELECT SupplierID, SupplierName
		                                FROM         dbo.View_Payment
		                                WHERE     (PaymentDate >= @Start) AND (PaymentDate <= @Finish) and ProjectID = @ProjectID
		                                GROUP BY SupplierID, SupplierName
	                                ) AS Source
	                                ORDER BY SupplierName ASC

                                END ">
            <SelectParameters>
                <asp:ControlParameter ControlID="lblStart" DefaultValue="0"  Name="Start" PropertyName="Text" Type="DateTime" />
                <asp:ControlParameter ControlID="lblFinish" DefaultValue="0"  Name="Finish" PropertyName="Text" Type="DateTime" />
                <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0"  Name="ProjectID" PropertyName="SelectedValue" />
            </SelectParameters>
    </asp:SqlDataSource>

    <asp:TextBox ID="TextBoxDateRange" runat="server" CssClass="add_daterangepicker" AutoPostBack="true" placeholder="Date Range" Width="200px"></asp:TextBox>

    <asp:Label ID="lblStart" runat="server" CssClass="hide"></asp:Label>
    <asp:Label ID="lblFinish" runat="server" CssClass="hide"></asp:Label>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.add_daterangepicker').on('apply.daterangepicker', function (ev, picker) {
                __doPostBack('ctl00$MainContent$ShowReport', '')
            });
        });
    </script>

    <asp:LinkButton ID="ButtonExcel" runat="server" CssClass="badge badge-purple" OnClick="ButtonExcel_Click" >
      <i class="ace-icon fa fa-file-excel-o "></i>
      Export To Excel
    </asp:LinkButton>

    <hr />

<asp:GridView ID="GridViewDSP3" runat="server" AutoGenerateColumns="False" PagerSettings-Position="TopAndBottom" GridLines="None" PageSize="100"
        DataSourceID="SqlDataSourceDSP3" CssClass="table table-nonfluid table-hover" AllowPaging="true">
        <Columns>
            <asp:TemplateField HeaderText="Project" SortExpression="ProjectName" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="LabelProjectName" runat="server" Text='<%# Bind("ProjectName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="PO" SortExpression="PO_No" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("PO_No") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Supplier" ControlStyle-Width="100" HeaderStyle-Width="100">
                <ItemTemplate>
                    <asp:Label ID="LabelSupplier" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Description" SortExpression="Description" ControlStyle-Width="180" HeaderStyle-Width="180">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
                   <asp:TemplateField SortExpression="POtotalprice" ControlStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
                       <HeaderTemplate>
                           <div class="PHeaderSepr">Po Total</div>
                           <div class="PHeaderSepr">Invoice Value Exc.VAT</div>                           
                           <div>Currency</div>                       
                       </HeaderTemplate>
                       <ItemTemplate>
                           <div class="PHeaderSepr"><asp:Label ID="Label3" runat="server"    Text='<%# Bind("POtotalprice","{0:###,###,###.00}") %>'></asp:Label></div>
                           <div class="PHeaderSepr"><asp:Label ID="Label11" runat="server" Text='<%# Bind("Invoice_value","{0:###,###,###.00}") %>'></asp:Label></div>                           
                           <div><asp:Label ID="Label5" runat="server" Text='<%# Bind("PO_Currency") %>'></asp:Label></div>
                       </ItemTemplate>


<ControlStyle Width="80px"></ControlStyle>

<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                   </asp:TemplateField>
            
            
            
            <asp:TemplateField HeaderText="VAT" SortExpression="VATpercent" ItemStyle-HorizontalAlign="Right" ControlStyle-Width="30" HeaderStyle-Width="30">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("VATpercent") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="CostCode" SortExpression="CostCode" ControlStyle-Width="100" HeaderStyle-Width="100">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("CostCode") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PO Date" SortExpression="PO_Date" ControlStyle-Width="80" HeaderStyle-Width="80" HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide">
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("PO_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
                   <asp:TemplateField SortExpression="Invoice_No" ControlStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
                       <HeaderTemplate>
                           <div class="PHeaderSepr">Invoice No</div>
                           <div>Invoice Date</div>                       
                       </HeaderTemplate>
                       <ItemTemplate>
                           <div class="PHeaderSepr"><asp:Label ID="Label11invnoo" runat="server" Text='<%# Bind("Invoice_No") %>'></asp:Label></div>
                           <div><asp:Label ID="Label12invdate" runat="server" Text='<%# Bind("Invoice_Date","{0:dd/MM/yyyy}") %>'></asp:Label></div>
                       </ItemTemplate>

<ControlStyle Width="80px"></ControlStyle>

<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                   </asp:TemplateField>

            
            

            <asp:TemplateField HeaderText="Site Rec" SortExpression="SiteRecordNo" ControlStyle-Width="60" HeaderStyle-Width="60">
                <ItemTemplate>
                    <asp:Label ID="Label12" runat="server" Text='<%# Bind("SiteRecordNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PayReqDate" SortExpression="PayReqDate" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("PayReqDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PDF" SortExpression="LinkToInvoice" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButtonPdf" runat="server" CommandName="OpenPdf" 
                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LinkToInvoice") %>'
                     />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Finc No" SortExpression="FinanceNo" ControlStyle-Width="40" HeaderStyle-Width="40">
                <ItemTemplate>
                    <asp:Label ID="Label17" runat="server" Text='<%# Bind("FinanceNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
                   <asp:TemplateField SortExpression="POtotalprice" ControlStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
                       <HeaderTemplate>
                           <div class="PHeaderSepr">PaymentDate</div>
                           <div class="PHeaderSepr">PaymentValue With VAT</div>                           
                           <div>Payment Curn.</div>                       
                       </HeaderTemplate>
                       <ItemTemplate>
                           <div class="PHeaderSepr"><asp:Label ID="Label18" runat="server" Text='<%# Bind("PaymentDate","{0:dd/MM/yyyy}") %>'></asp:Label></div>
                           <div class="PHeaderSepr"><asp:Label ID="Label19" runat="server" Text='<%# Bind("Payment_amount","{0:###,###,###.00}") %>'></asp:Label></div>                           
                           <div><asp:Label ID="Label20" runat="server" Text='<%# Bind("Payment_currency") %>'></asp:Label></div>
                       </ItemTemplate>


<ControlStyle Width="80px"></ControlStyle>

<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                   </asp:TemplateField>
            
            
        </Columns>
                <PagerSettings Position="TopAndBottom" />
                <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
                <HeaderStyle CssClass="headergridnew" />
    </asp:GridView>
    
    <asp:Panel ID="PanelMonitor" runat="server" CssClass="hidepanel" >                    
    <asp:SqlDataSource ID="SqlDataSourceDSP3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand=" IF @ProjectID = 0 AND @SupplierID = N'0'
                        BEGIN
	                        SELECT * FROM [View_Payment] 
	                        WHERE (PaymentDate >= @Start) AND (PaymentDate <= @Finish) 
                        END

                        IF @ProjectID <> 0 AND @SupplierID = N'0'
                        BEGIN
	                        SELECT * FROM [View_Payment] 
	                        WHERE (PaymentDate >= @Start) AND (PaymentDate <= @Finish) AND ProjectID = @ProjectID
                        END

                        IF @ProjectID = 0 AND @SupplierID <> N'0'
                        BEGIN
	                        SELECT * FROM [View_Payment] 
	                        WHERE (PaymentDate >= @Start) AND (PaymentDate <= @Finish) AND SupplierID = @SupplierID
                        END

                        IF @ProjectID <> 0 AND @SupplierID <> N'0'
                        BEGIN
	                        SELECT * FROM [View_Payment] 
	                        WHERE (PaymentDate >= @Start) AND (PaymentDate <= @Finish) AND ProjectID = @ProjectID AND SupplierID = @SupplierID
                        END ">
            <SelectParameters>
                <asp:ControlParameter ControlID="lblStart" DefaultValue="0"  Name="Start" PropertyName="Text" Type="DateTime" />
                <asp:ControlParameter ControlID="lblFinish" DefaultValue="0"  Name="Finish" PropertyName="Text" Type="DateTime" />
                <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0"  Name="ProjectID" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="DropDownListSupplier" DefaultValue="0"  Name="SupplierID" PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
    </asp:SqlDataSource>

<asp:GridView ID="GridViewDSP3HIDDEN" runat="server" AutoGenerateColumns="False" 
        CssClass="Grid" >
        <Columns>
            <asp:TemplateField HeaderText="Project" SortExpression="ProjectName" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="LabelProjectNameHIDDEN" runat="server" Text='<%# Bind("ProjectName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="PO" SortExpression="PO_No" ControlStyle-Width="100" HeaderStyle-Width="100">
                <ItemTemplate>
                    <asp:Label ID="Label1HIDDEN" runat="server" Text='<%# Bind("PO_No") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Supplier" ControlStyle-Width="200" HeaderStyle-Width="200">
                <ItemTemplate>
                    <asp:Label ID="LabelSupplierHIDDEN" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Description" SortExpression="Description" ControlStyle-Width="200" HeaderStyle-Width="200">
                <ItemTemplate>
                    <asp:Label ID="Label2HIDDEN" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
                   <asp:TemplateField HeaderText="Total PO Value With VAT" ControlStyle-Width="80" HeaderStyle-Width="80">
                       <ItemTemplate>
                           <div style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0"><asp:Label ID="Label3HIDDEN" runat="server"    Text='<%# Bind("POtotalprice") %>'></asp:Label></div>
                       </ItemTemplate>
                   </asp:TemplateField>
            

                   <asp:TemplateField HeaderText="Invoice Value Without VAT" ControlStyle-Width="80" HeaderStyle-Width="80">
                       <ItemTemplate>
                            <div style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0"><asp:Label ID="Label11HIDDEN" runat="server" Text='<%# Bind("Invoice_value") %>'></asp:Label></div>                           
                       </ItemTemplate>
                   </asp:TemplateField>

                   <asp:TemplateField HeaderText="Cur." ControlStyle-Width="40" HeaderStyle-Width="40">
                       <ItemTemplate>
                           <div><asp:Label ID="Label5" runat="server" Text='<%# Bind("PO_Currency") %>'></asp:Label></div>
                       </ItemTemplate>
                   </asp:TemplateField>
            
            
            <asp:TemplateField HeaderText="VAT" SortExpression="VATpercent" ItemStyle-HorizontalAlign="Right" ControlStyle-Width="30" HeaderStyle-Width="30">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("VATpercent") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="CostCode" SortExpression="CostCode" ControlStyle-Width="120" HeaderStyle-Width="120">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("CostCode") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PO Date" SortExpression="PO_Date" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("PO_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
                   <asp:TemplateField HeaderText="Invoice No" ControlStyle-Width="100" HeaderStyle-Width="100">
                       <ItemTemplate>
                           <div style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0; "><asp:Label ID="Label11invnoo" runat="server" Text='<%# Bind("Invoice_No") %>'></asp:Label></div>
                       </ItemTemplate>
                   </asp:TemplateField>

                   <asp:TemplateField HeaderText="Invoice Date " ControlStyle-Width="80" HeaderStyle-Width="80">
                       <ItemTemplate>
                           <div><asp:Label ID="Label12invdate" runat="server" Text='<%# Bind("Invoice_Date","{0:dd/MM/yyyy}") %>'></asp:Label></div>
                       </ItemTemplate>
                   </asp:TemplateField>


            <asp:TemplateField HeaderText="Site Rec" SortExpression="SiteRecordNo" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="Label12" runat="server" Text='<%# Bind("SiteRecordNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PayReqDate" SortExpression="PayReqDate" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("PayReqDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Finc No" SortExpression="FinanceNo" ControlStyle-Width="40" HeaderStyle-Width="40">
                <ItemTemplate>
                    <asp:Label ID="Label17" runat="server" Text='<%# Bind("FinanceNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
                   <asp:TemplateField HeaderText="Payment Date"  ControlStyle-Width="80" HeaderStyle-Width="80">
                       <ItemTemplate>
                           <div style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0"><asp:Label ID="Label18" runat="server" Text='<%# Bind("PaymentDate","{0:dd/MM/yyyy}") %>'></asp:Label></div>
                       </ItemTemplate>
                   </asp:TemplateField>

                   <asp:TemplateField HeaderText="Payment Value With VAT" ControlStyle-Width="80" HeaderStyle-Width="80">
                       <ItemTemplate>
                            <div style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0"><asp:Label ID="Label19" runat="server" Text='<%# Bind("Payment_amount") %>'></asp:Label></div>                           
                       </ItemTemplate>
                   </asp:TemplateField>

                   <asp:TemplateField HeaderText="Cur." ControlStyle-Width="40" HeaderStyle-Width="40">
                       <ItemTemplate>
                           <div><asp:Label ID="Label20" runat="server" Text='<%# Bind("Payment_currency") %>'></asp:Label></div>
                       </ItemTemplate>
                   </asp:TemplateField>

            
            
        </Columns>
                <RowStyle  CssClass="GridItemNakladnaya" />
                <HeaderStyle  CssClass="GridHeader" />     
            <PagerStyle CssClass="pager2" />
    </asp:GridView>



    </asp:Panel>        


</asp:Content>


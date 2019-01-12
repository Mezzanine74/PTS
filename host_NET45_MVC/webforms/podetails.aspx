<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" EnableViewState="false" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeFile="podetails.aspx.vb" Inherits="podetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>podetails</title>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

              <asp:label ID="labeltest" runat="server" Text=""></asp:label>
              <asp:label ID="labelSupplierIDTransfer" runat="server" Visible="false" ></asp:label>
              <asp:label ID="labelAllSuppliersMode" runat="server" Text="False" Visible="false"></asp:label>

    <asp:GridView ID="GridViewMonitor" runat="server" AutoGenerateColumns="False"  PagerSettings-Position="TopAndBottom"   
        DataSourceID="SqlDataSourceMonitor" CssClass="Grid" AllowPaging="True" PageSize="30">
        <Columns>
            <asp:TemplateField HeaderText="PO" SortExpression="PO_No" ControlStyle-Width="120" HeaderStyle-Width="120">
                <ItemTemplate>
                    <div><asp:Label ID="LabelProjectName" runat="server" Text='<%# Bind("ProjectName") %>' ForeColor="#6666FF"></asp:Label></div>
                   <div> <asp:HyperLink ID="HyperLinkSubPo" runat="server" Target="_blank" CssClass="Hlink" 
                    Text='<%# Bind("PO_No") %>' Font-Underline="False" ForeColor="Black" Font-Bold="false"></asp:HyperLink></div> 
                </ItemTemplate>
                <ControlStyle Width="120px" />
                <HeaderStyle Width="120px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Supplier" SortExpression="SupplierName" ControlStyle-Width="100" HeaderStyle-Width="100">
                <ItemTemplate>
                    <asp:Label ID="LabelSupplierName" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                    <br />
                    <asp:Label ID="LabelSupplierID" runat="server" ></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="100px" />
                <HeaderStyle Width="100px" />
            </asp:TemplateField>            
            
            <asp:TemplateField HeaderText="Description" SortExpression="Description" ControlStyle-Width="180" HeaderStyle-Width="180">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="180px" />
                <HeaderStyle Width="180px" />
            </asp:TemplateField>
            
                   <asp:TemplateField SortExpression="POtotalprice" ControlStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
                       <HeaderTemplate>
                           <div style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #FFFFFF">Po Total</div>
                           <div style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #FFFFFF">Invoice Value Exc.VAT</div>                           
                           <div>Currency</div>                       
                       </HeaderTemplate>
                       <ItemTemplate>
                           <div style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0"><asp:Label ID="Label3" runat="server"    Text='<%# Bind("POtotalprice","{0:###,###,###.00}") %>'></asp:Label></div>
                            <div style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0"><asp:Label ID="Label11" runat="server" Text='<%# Bind("Invoice_value","{0:###,###,###.00}") %>'></asp:Label></div>                           
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
                <ControlStyle Width="30px" />
                <HeaderStyle Width="30px" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="CostCode" SortExpression="CostCode" ControlStyle-Width="100" HeaderStyle-Width="100">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("CostCode") %>'></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="100px" />
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PO Date" SortExpression="PO_Date" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("PO_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="80px" />
                <HeaderStyle Width="80px" />
            </asp:TemplateField>

            
            
                   <asp:TemplateField SortExpression="Invoice_No" ControlStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
                       <HeaderTemplate>
                           <div style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #FFFFFF">Invoice No</div>
                           <div>Invoice Date</div>                       
                       </HeaderTemplate>
                       <ItemTemplate>
                           <div style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0; color: #0000FF;"><asp:Label ID="Label11invnoo" runat="server" Text='<%# Bind("Invoice_No") %>'></asp:Label></div>
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
                <ControlStyle Width="60px" />
                <HeaderStyle Width="60px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Requsition Date" SortExpression="PayReqDate" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("PayReqDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="80px" />
                <HeaderStyle Width="80px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PDF" SortExpression="LinkToInvoice" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:HyperLink ID="ImageButtonPdf" runat="server" Target="_blank" ></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Urgency" SortExpression="Urgency" ControlStyle-Width="26" HeaderStyle-Width="26">
                <ItemTemplate>
                    <asp:Label ID="Label15" runat="server" Text='<%# Bind("Urgency") %>'></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="26px" />
                <HeaderStyle Width="26px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Approve" SortExpression="PersonApprove" ControlStyle-Width="26" HeaderStyle-Width="26">
                <ItemTemplate>
                    <asp:Label ID="Label16" runat="server" Text='<%# Bind("PersonApprove") %>'></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="26px" />
                <HeaderStyle Width="26px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Finc No" SortExpression="FinanceNo" ControlStyle-Width="40" HeaderStyle-Width="40">
                <ItemTemplate>
                    <asp:Label ID="Label17" runat="server" Text='<%# Bind("FinanceNo") %>'></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="40px" />
                <HeaderStyle Width="40px" />
            </asp:TemplateField>
            
                   <asp:TemplateField SortExpression="POtotalprice" ControlStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
                       <HeaderTemplate>
                           <div style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #FFFFFF">PaymentDate</div>
                           <div style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #FFFFFF">PaymentValue With VAT</div>                           
                           <div>Payment Curn.</div>                       
                       </HeaderTemplate>
                       <ItemTemplate>
                           <div style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0"><asp:Label ID="Label18" runat="server" Text='<%# Bind("PaymentDate","{0:dd/MM/yyyy}") %>'></asp:Label></div>
                            <div style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0"><asp:Label ID="Label19" runat="server" Text='<%# Bind("Payment_amount","{0:###,###,###.00}") %>'></asp:Label></div>                           
                           <div><asp:Label ID="Label20" runat="server" Text='<%# Bind("Payment_currency") %>'></asp:Label></div>
                       </ItemTemplate>


<ControlStyle Width="80px"></ControlStyle>

<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                   </asp:TemplateField>
            
            
        </Columns>
	        <pagerstyle  horizontalalign="Center" CssClass="pager2" />
                <RowStyle  CssClass="GridItemNakladnaya" />
                <HeaderStyle  CssClass="GridHeader" />     
    </asp:GridView>
    
    <asp:Panel ID="PanelMonitor" runat="server" CssClass="hidepanel">                    
    <asp:SqlDataSource ID="SqlDataSourceMonitor" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" >
    </asp:SqlDataSource>
    </asp:Panel>    

</asp:Content>


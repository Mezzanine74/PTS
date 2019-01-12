<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WebUserControl_MonitoringExcelOutput.ascx.vb" Inherits="WebUserControl_MonitoringExcelOutput" %>

        <asp:GridView ID="GridViewExcel" runat="server" AutoGenerateColumns="False" Font-Size="10px" >
        <Columns>
        <asp:BoundField DataField="ProjectName" HeaderText="Project Name" ReadOnly="True" HeaderStyle-Width="120px" ItemStyle-Width="120px"/>
        <asp:BoundField DataField="PO_No" HeaderText="PO No" ReadOnly="True" HeaderStyle-Width="120px" ItemStyle-Width="120px"/>
        <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" ReadOnly="True" HeaderStyle-Width="100px" ItemStyle-Width="100px"/>
        <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" HeaderStyle-Width="180px" ItemStyle-Width="180px"/>
        <asp:BoundField DataField="POtotalprice" HeaderText="Po Value With VAT" ReadOnly="True" HeaderStyle-Width="80px" ItemStyle-Width="80px" 
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
        <asp:BoundField DataField="VATpercent" HeaderText="VAT %" ReadOnly="True" HeaderStyle-Width="80px" ItemStyle-Width="80px" 
            ItemStyle-HorizontalAlign="Right" />
        <asp:BoundField DataField="Invoice_value" HeaderText="Invoice Value With VAT" ReadOnly="True" HeaderStyle-Width="80px" ItemStyle-Width="80px" 
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
        <asp:BoundField DataField="PO_Currency" HeaderText="Currency" ReadOnly="True" />
        <asp:BoundField DataField="CostCode" HeaderText="Cost Code" ReadOnly="True" HeaderStyle-Width="100px" ItemStyle-Width="100px"/>
        <asp:BoundField DataField="Invoice_No" HeaderText="Invoice No" ReadOnly="True" HeaderStyle-Width="80px" ItemStyle-Width="80px"/>
        <asp:BoundField DataField="Invoice_Date" HeaderText="Invoice Date" ReadOnly="True" HeaderStyle-Width="80px" ItemStyle-Width="80px"
            DataFormatString="{0:dd/MM/yyyy}"/>
        <asp:BoundField DataField="SiteRecordNo" HeaderText="Site Record No" ReadOnly="True" HeaderStyle-Width="60px" ItemStyle-Width="60px"/>
        <asp:BoundField DataField="PayReqDate" HeaderText="Payment Requsition Date" ReadOnly="True" HeaderStyle-Width="80px" ItemStyle-Width="80px"
            DataFormatString="{0:dd/MM/yyyy}"/>
        <asp:BoundField DataField="FinanceNo" HeaderText="Finance No" ReadOnly="True" HeaderStyle-Width="40px" ItemStyle-Width="40px"/>
        <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" ReadOnly="True" HeaderStyle-Width="80px" ItemStyle-Width="80px" DataFormatString="{0:dd/MM/yyyy}"/>
        <asp:BoundField DataField="Payment_amount" HeaderText="Payment Amount With VAT" ReadOnly="True" HeaderStyle-Width="100px" ItemStyle-Width="100px"
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
        <asp:BoundField DataField="Payment_currency" HeaderText="Payment Currency" ReadOnly="True" />
        </Columns>
        </asp:GridView>

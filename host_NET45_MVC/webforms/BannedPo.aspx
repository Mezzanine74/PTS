<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="BannedPo.aspx.vb" Inherits="BannedPo" EnableViewState="False" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<div style="margin: 3px; padding: 3px; text-align: center; border: thin solid #808080; background-color: #FF0000; color: #FFFFFF; font-weight: bold; height: 20px; font-size: 12px;">
 There are still missing delivery documents for this PO. Payment is not advised unless we collect those documents.
</div>

  <asp:GridView ID="GridViewBannedPo" runat="server" AutoGenerateColumns="False" 
    DataKeyNames="PO_No" DataSourceID="SqlDataSourceBannedPo" 
    EnableModelValidation="True"  CssClass="Grid">
    <Columns>
      <asp:BoundField DataField="PO_No" HeaderText="PO_No" ReadOnly="True" 
        SortExpression="PO_No" ControlStyle-Width="80" HeaderStyle-Width="80"></asp:BoundField>
      <asp:BoundField DataField="Description" HeaderText="Description"  ControlStyle-Width="120" HeaderStyle-Width="120"
        SortExpression="Description"></asp:BoundField>
      <asp:BoundField DataField="PayReqDate" HeaderText="PayReqDate"  ControlStyle-Width="80" HeaderStyle-Width="80" DataFormatString="{0:dd/MM/yyyy}" 
        SortExpression="PayReqDate"></asp:BoundField>
      <asp:BoundField DataField="SupplierName" HeaderText="SupplierName"  ControlStyle-Width="100" HeaderStyle-Width="100"
        SortExpression="SupplierName"></asp:BoundField>
      <asp:BoundField DataField="Invoice_No" HeaderText="Invoice_No"  ControlStyle-Width="80" HeaderStyle-Width="80"
        SortExpression="Invoice_No"></asp:BoundField>
      <asp:BoundField DataField="Invoice_Date" HeaderText="Invoice_Date"  ControlStyle-Width="80" HeaderStyle-Width="80" DataFormatString="{0:dd/MM/yyyy}"
        SortExpression="Invoice_Date"></asp:BoundField>
      <asp:BoundField DataField="InvoiceValue" HeaderText="InvoiceValue"   ControlStyle-Width="80" HeaderStyle-Width="80" DataFormatString="{0:N2}" 
        ItemStyle-HorizontalAlign="Right"
        SortExpression="InvoiceValue"></asp:BoundField>
      <asp:BoundField DataField="PO_Currency" HeaderText="Currency" 
        SortExpression="PO_Currency"></asp:BoundField>
      <asp:BoundField DataField="PaymentDate" HeaderText="PaymentDate" ControlStyle-Width="80" HeaderStyle-Width="80" DataFormatString="{0:dd/MM/yyyy}" 
        SortExpression="PaymentDate"></asp:BoundField>
      <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount"  ControlStyle-Width="80" HeaderStyle-Width="80" DataFormatString="{0:N2}" 
        ItemStyle-HorizontalAlign="Right">
      </asp:BoundField>
      <asp:BoundField DataField="CollectedValue" HeaderText="CollectedValue"   ControlStyle-Width="80" HeaderStyle-Width="80" DataFormatString="{0:N2}" 
        ItemStyle-HorizontalAlign="Right"
        SortExpression="CollectedValue"></asp:BoundField>
      <asp:BoundField DataField="Diff" HeaderText="Diff" SortExpression="Diff"  ControlStyle-Width="80" HeaderStyle-Width="80" DataFormatString="{0:N2}" 
        ItemStyle-HorizontalAlign="Right">
      </asp:BoundField>
    </Columns>
           <RowStyle  CssClass="GridItemNakladnaya" />
       <HeaderStyle  CssClass="GridHeader" />
  </asp:GridView>

  <asp:SqlDataSource ID="SqlDataSourceBannedPo" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand=" SELECT     ProjectName, PO_No, Description, PayReqDate, SupplierName, Invoice_No, Invoice_Date, InvoiceValue, PO_Currency, PaymentDate, Amount, 
                      CollectedValue, Diff
FROM         dbo.View_MissingDocTracker
WHERE     (PO_No = @PO_No) ">
    <SelectParameters>
      <asp:QueryStringParameter DefaultValue="-" Name="PO_No" 
        QueryStringField="PO_No" />
    </SelectParameters>
  </asp:SqlDataSource>


</asp:Content>


<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="PackingListToday.aspx.vb" Inherits="PackingListToday" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


<table>
 <tr>
  <td>
                    <asp:ImageButton ID="ImageButtonDeliveryReport" runat="server" 
                    ImageUrl="~/Images/Nak_Shot_Akt_.bmp" PostBackUrl="~/webforms/DeliveryFollowUp.aspx" 
                    ToolTip="Delivery FollowUp"  />
  </td>
  <td>
      <div style="width: 800px;">
      <div style="text-align: center; font-size: 15px; font-weight: bold; ">Daily Packing List</div>
                          <div >
                              <table style="text-align: center; width: 100%;" >
                                  <tr>
                                      <td>
                                          <asp:Label ID="LabelDatedBy" runat="server" Text="Dated By" CssClass="LabelGeneral"></asp:Label>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          <asp:TextBox ID="PaymentDateTextBoxShown" runat="server" 
                                              CssClass="add_datepicker" AutoPostBack="True" Width="100px" />

                                      </td>
                                  </tr>
                                  </table>
                          </div>
      </div>   
  </td>
 </tr>
</table>

 <asp:Label ID="LabelRowIndex" runat="server" ></asp:Label>
 <asp:Label ID="LabelRowIndex2" runat="server" ></asp:Label>

    <hr />

    <asp:GridView ID="GridViewPAckingListToday" runat="server" AllowPaging="False" GridLines="None" 
        AutoGenerateColumns="False" DataSourceID="SqlDataSourcePackingListToday" 
            CssClass="table table-nonfluid table-hover" DataKeyNames="PayReqNo" RowStyle-Height="50px">
        <Columns>
            <asp:TemplateField HeaderText="ProjectName" SortExpression="ProjectName" ControlStyle-Width="120" HeaderStyle-Width="120">
                <EditItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("ProjectName") %>'></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="120px" />
                <HeaderStyle Width="120px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PO No" SortExpression="PO_No" ControlStyle-Width="80" HeaderStyle-Width="80">
                <EditItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("PO_No") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("PO_No") %>'></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="80px" />
                <HeaderStyle Width="80px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description" SortExpression="Description" ControlStyle-Width="130" HeaderStyle-Width="130">
                <EditItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="130px" />
                <HeaderStyle Width="130px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SupplierName" SortExpression="SupplierName" ControlStyle-Width="100" HeaderStyle-Width="100">
                <EditItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("SupplierName") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="100px" />
                <HeaderStyle Width="100px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Invoice No" SortExpression="Invoice_No" ControlStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
                <EditItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("Invoice_No") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("Invoice_No") %>'></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="80px" />
                <HeaderStyle Width="80px" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Invoice Date" SortExpression="Invoice_Date" ControlStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Invoice_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="80px" />
                <HeaderStyle Width="80px" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Inv.WithVAT" 
                SortExpression="InvoiceValueWithVAT" ItemStyle-HorizontalAlign="Right" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("InvoiceValueWithVAT","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="80px" />
                <HeaderStyle Width="80px" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="" SortExpression="PO_Currency" >
                <ItemTemplate>
		    <asp:Panel ID="PanelCurrency" runat="server" CssClass="hidepanel">
                      <asp:Label ID="LabelCurrency" runat="server" Text='<%# Bind("PO_Currency") %>'></asp:Label>
		    </asp:Panel>
                    <asp:Image ID="ImageCurrency" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SiteRecNo" SortExpression="SiteRecordNo" ControlStyle-Width="60" HeaderStyle-Width="60" ItemStyle-HorizontalAlign="Right">
                <EditItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("SiteRecordNo") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("SiteRecordNo") %>'></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="60px" />
                <HeaderStyle Width="60px" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField >
                <EditItemTemplate>
                </EditItemTemplate>
                <ItemTemplate>
                  <asp:ImageButton ID="ImageButton1" runat="server" CommandName="OpenPdf" 
                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LinkToInvoice") %>' 
                ImageUrl="~/Images/Person.png" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
                <PagerSettings Position="TopAndBottom" />
                <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
                <HeaderStyle CssClass="headergridnew" />
    </asp:GridView>
    
    <asp:SqlDataSource ID="SqlDataSourcePackingListToday" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand="
                    SELECT PayReqNo,RTRIM(ProjectName) AS ProjectName, 
                    RTRIM(PO_No) AS PO_No, Description, RTRIM(SupplierName) AS SupplierName, 
                    dbo.FunctionInvoiceNoReplaceLongText(RTRIM(Invoice_No)) AS Invoice_No, Invoice_Date, InvoiceValueWithVAT, 
                    RTRIM(PO_Currency) AS PO_Currency, PayReqDate, 
                    RTRIM(SiteRecordNo) AS SiteRecordNo, 
                    rtrim(LinkToInvoice) as LinkToInvoice, 
                    AttachmentExist , Transport, PersonResponsible
                    FROM dbo.View_PackingListToday 
                    WHERE (PayReqDate=@PayReqDate)
                    ORDER BY PayReqNo DESC ">
            <SelectParameters>
                <asp:Parameter Name="PayReqDate" />
            </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>


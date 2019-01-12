<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeFile="PaymentTermsPDF.aspx.vb" Inherits="PaymentTermsPDF" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

  <asp:Label ID="LabelHeaderColumn1" runat="server" CssClass="hidepanel"></asp:Label>
  <asp:Label ID="LabelHeaderColumn2" runat="server" CssClass="hidepanel"></asp:Label>
  <asp:Label ID="LabelHeaderColumn3" runat="server" CssClass="hidepanel"></asp:Label>
  <asp:Label ID="LabelHeaderColumn4" runat="server" CssClass="hidepanel"></asp:Label>


<table>
 <tr>
    <td style="width: 300px">
    <asp:DropDownList ID="DropDownListPrj" runat="server" AutoPostBack="True" 
      DataSourceID="SqlDataSourcePrj" 
      DataTextField="ProjectNameVisible" DataValueField="ProjectName" >
    </asp:DropDownList>

    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand="SELECT     TOP (100) PERCENT RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID)) + N' - ' + RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName
, RTRIM(RTRIM(dbo.Table1_Project.ProjectName + N' - ' + CONVERT(nchar(5), dbo.Table1_Project.ProjectID))) AS ProjectNameVisible
FROM         dbo.Table1_Project INNER JOIN
                      dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN
                      dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId INNER JOIN
                      dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN
                      dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN
                      dbo.Table4_PaymentRequest ON dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID LEFT OUTER JOIN
                      dbo.Table5_PayLog ON dbo.Table4_PaymentRequest.PayReqNo = dbo.Table5_PayLog.PayReqNo
WHERE     (dbo.aspnet_Users.UserName = @UserName) AND (dbo.Table5_PayLog.PayReqNo IS NULL)
GROUP BY RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID)) + N' - ' + RTRIM(dbo.Table1_Project.ProjectName),
         RTRIM(RTRIM(dbo.Table1_Project.ProjectName + N' - ' + CONVERT(nchar(5), dbo.Table1_Project.ProjectID))),
         dbo.Table1_Project.ProjectName
ORDER BY dbo.Table1_Project.ProjectName">
        <SelectParameters>
            <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" 
                PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
    </td>
    <td style="width: 400px;">
     <asp:LinkButton ID="LinkButtonNumberViEw" runat="server" PostBackUrl="~/webforms/PaymentTerms.aspx" CssClass="btn btn-mini">Switch To Payment Terms</asp:LinkButton>
    </td>
    <td style="width: 400px;">
                 <asp:DropDownList ID="DDLsort" runat="server" AutoPostBack="true" >
                                        <asp:ListItem Value="Not Sorted">Not Sorted</asp:ListItem>                            
                                        <asp:ListItem Value="Sort By Payment Term">Sort By Payment Term</asp:ListItem>
                 </asp:DropDownList>
    </td>
 </tr>
</table>

    <asp:TextBox ID="TextBoxUserName" runat="server" CssClass="hidepanel"></asp:TextBox>

  <asp:GridView ID="GridViewPaymentTerms" runat="server" AutoGenerateColumns="False" 
    DataKeyNames="PayReqNo" DataSourceID="SqlDataSourcePaymentTerm" 
    EnableModelValidation="True" CssClass="GridPaymentTerm" >
    <Columns>
      <asp:BoundField DataField="PayReqNo" HeaderText="PayReqNo" ReadOnly="True" 
        ItemStyle-CssClass="hidepanel" HeaderStyle-CssClass="hidepanel" FooterStyle-CssClass="hidepanel"
        SortExpression="PayReqNo" />
      <asp:BoundField DataField="ProjectName" HeaderText="ProjectName" 
        ReadOnly="True" SortExpression="ProjectName" />
      <asp:BoundField DataField="PO_No" HeaderText="PO_No" ReadOnly="True" 
        SortExpression="PO_No" />
      <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" 
        ReadOnly="True" SortExpression="SupplierName" />
      <asp:BoundField DataField="InvoiceNo" HeaderText="InvoiceNo" ReadOnly="True" 
        SortExpression="InvoiceNo" />
      <asp:BoundField DataField="PO_Currency" HeaderText=""
        ReadOnly="True" SortExpression="PO_Currency" />
      <asp:BoundField DataField="PoSumWithVAT" HeaderText="Po Sum With VAT" 
        HeaderStyle-Width="82px" ItemStyle-Width="82px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"
        ReadOnly="True" SortExpression="PoSumWithVAT" />
      <asp:BoundField DataField="PoPaidWithVAT" HeaderText="Po Paid With VAT" 
        HeaderStyle-Width="82px" ItemStyle-Width="82px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"
        ReadOnly="True" SortExpression="PoPaidWithVAT" />
      <asp:BoundField DataField="POBalanceWithVAT" HeaderText="PO Balance With VAT" 
        HeaderStyle-Width="82px" ItemStyle-Width="82px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"
        ReadOnly="True" SortExpression="POBalanceWithVAT" />
      <asp:BoundField DataField="InvoiceValueWithVAT" 
        HeaderStyle-Width="82px" ItemStyle-Width="82px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"
        HeaderText="Invoice Value With VAT" ReadOnly="True" 
        SortExpression="InvoiceValueWithVAT" />
      
      <asp:TemplateField HeaderStyle-Width="10px" ItemStyle-Width="10px">
       <ItemTemplate>
       </ItemTemplate>
      </asp:TemplateField>

      <asp:TemplateField HeaderStyle-Width="82px" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center">
       <ItemTemplate>
        <asp:ImageButton ID="ImageButtonPDF1" runat="server" Visible="false"
        CommandName="ColumnPDF1" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LinkToInvoice") %>'/>
       </ItemTemplate>
      </asp:TemplateField>

      <asp:TemplateField HeaderStyle-Width="10px" ItemStyle-Width="10px">
       <ItemTemplate>
       </ItemTemplate>
      </asp:TemplateField>

      <asp:TemplateField HeaderStyle-Width="82px" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center">
       <ItemTemplate>
        <asp:ImageButton ID="ImageButtonPDF2" runat="server" Visible="false"
        CommandName="ColumnPDF2" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LinkToInvoice") %>'/>
       </ItemTemplate>
      </asp:TemplateField>

      <asp:TemplateField  HeaderStyle-Width="10px" ItemStyle-Width="10px">
       <ItemTemplate>
       </ItemTemplate>
      </asp:TemplateField>

      <asp:TemplateField HeaderStyle-Width="82px" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center">
       <ItemTemplate>
        <asp:ImageButton ID="ImageButtonPDF3" runat="server" Visible="false"
        CommandName="ColumnPDF3" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LinkToInvoice") %>'/>
       </ItemTemplate>
      </asp:TemplateField>

      <asp:TemplateField  HeaderStyle-Width="10px" ItemStyle-Width="10px">
       <ItemTemplate>
       </ItemTemplate>
      </asp:TemplateField>

      <asp:TemplateField HeaderStyle-Width="82px" ItemStyle-Width="82px" ItemStyle-HorizontalAlign="Center">
       <ItemTemplate>
        <asp:ImageButton ID="ImageButtonPDF4" runat="server" Visible="false"
        CommandName="ColumnPDF4" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LinkToInvoice") %>'/>
       </ItemTemplate>
      </asp:TemplateField>

      <asp:BoundField DataField="PaymentTerm" HeaderText="PaymentTerm" 
        SortExpression="PaymentTerm" DataFormatString="{0:dd/MM/yyyy}" 
        ItemStyle-CssClass="hidepanel" HeaderStyle-CssClass="hidepanel" FooterStyle-CssClass="hidepanel" />

    </Columns>
        <RowStyle  CssClass="GridPaymentTermRow" />
        <HeaderStyle  CssClass="GridPaymentTermHeader" />
  </asp:GridView>
  <asp:SqlDataSource ID="SqlDataSourcePaymentTerm" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand="SELECT     dbo.View_PaymentTermsPDF.PayReqNo, dbo.View_PaymentTermsPDF.ProjectName, dbo.View_PaymentTermsPDF.PO_No, dbo.View_PaymentTermsPDF.SupplierName, 
                      dbo.View_PaymentTermsPDF.InvoiceNo, dbo.View_PaymentTermsPDF.PO_Currency, dbo.View_PaymentTermsPDF.PoSumWithVAT, dbo.View_PaymentTermsPDF.PoPaidWithVAT, 
                      dbo.View_PaymentTermsPDF.POBalanceWithVAT, dbo.View_PaymentTermsPDF.InvoiceValueWithVAT, dbo.View_PaymentTermsPDF.PaymentTerm, dbo.View_PaymentTermsPDF.LinkToInvoice,
                      dbo.View_PaymentTermsPDF.AttachmentChange, dbo.View_PaymentTermsPDF.CreatedBy
FROM         dbo.View_PaymentTermsPDF INNER JOIN
                      dbo.Table2_PONo ON dbo.View_PaymentTermsPDF.PO_No = dbo.Table2_PONo.PO_No INNER JOIN
                      dbo.Table1_Project ON dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID INNER JOIN
                      dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN
                      dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId
WHERE     (dbo.View_PaymentTermsPDF.ProjectName LIKE '%' + @ProjectName + '%') AND (dbo.aspnet_Users.UserName = @UserName) 
ORDER BY dbo.View_PaymentTermsPDF.ProjectName, dbo.View_PaymentTermsPDF.PayReqNo ASC">
  <SelectParameters>
   <asp:ControlParameter ControlID="DropDownListPrj" Name="ProjectName" 
     PropertyName="SelectedValue" ConvertEmptyStringToNull="False" />
   <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" 
     PropertyName="Text" />
  </SelectParameters>  
  </asp:SqlDataSource>
</asp:Content>


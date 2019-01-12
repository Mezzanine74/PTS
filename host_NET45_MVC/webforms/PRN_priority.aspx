<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeFile="PRN_priority.aspx.vb" Inherits="PaymentTermsPriority" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<table>
 <tr>
    <td style="width: 300px">
    <asp:DropDownList ID="DropDownListPrj" runat="server" AutoPostBack="True" 
      DataSourceID="SqlDataSourcePrj" 
      DataTextField="ProjectName" DataValueField="ProjectID" Width="267px">
    </asp:DropDownList>

    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand=" SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, dbo.Table1_Project.ProjectName
        FROM         dbo.Table1_Project INNER JOIN
                              dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN
                              dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId INNER JOIN
                              dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN
                              dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN
                              dbo.Table4_PaymentRequest ON dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID LEFT OUTER JOIN
                              dbo.Table5_PayLog ON dbo.Table4_PaymentRequest.PayReqNo = dbo.Table5_PayLog.PayReqNo
        WHERE     (dbo.aspnet_Users.UserName = @UserName) AND (dbo.Table5_PayLog.PayReqNo IS NULL)
        GROUP BY dbo.Table1_Project.ProjectID, dbo.Table1_Project.ProjectName
        ORDER BY dbo.Table1_Project.ProjectName ">
        <SelectParameters>
            <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" 
                PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
  </td>
  <td style="width: 400px;">


   <table>
    <tr>
     <td style="width: 150px;">

       &nbsp;</td>
     <td>
     </td>
    </tr>
   </table>

  </td>
  <td style="border: 1px solid #C0C0C0; padding: 1px; margin: 1px; width: 400px; background-color: #F8F8FF;">
   <asp:Label ID="LabelRatesToShow" runat="server" ></asp:Label>
  </td>
  <td>
  </td>
 </tr>
</table>

    <asp:TextBox ID="TextBoxUserName" runat="server" CssClass="hidepanel"></asp:TextBox>

    <asp:DropDownList ID="DropDownTodayRates" runat="server" 
    DataSourceID="SqlDataSourceTodayRates" DataTextField="Rates" 
    DataValueField="Rates" CssClass="hidepanel" >
  </asp:DropDownList>
  <asp:SqlDataSource ID="SqlDataSourceTodayRates" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" >
  </asp:SqlDataSource>

    <asp:DropDownList ID="DropDownListRatesLatest" runat="server" 
    DataSourceID="SqlDataSourceRatesLatest" DataTextField="Rates" 
    DataValueField="Rates" CssClass="hidepanel" >
  </asp:DropDownList>
  <asp:SqlDataSource ID="SqlDataSourceRatesLatest" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
    SelectCommand="SELECT TOP 1 rtrim(convert(NvarChar(7),[RubbleDollar]))+ 
     N'/'+rtrim(convert(NvarChar(7),[RubbleEuro])) AS Rates FROM [Table8_ExchangeRates] 
     ORDER BY Date DESC" >
  </asp:SqlDataSource>

  <asp:GridView ID="GridViewPaymentTerms" runat="server" AutoGenerateColumns="False" 
    DataKeyNames="PayReqNo" DataSourceID="SqlDataSourcePaymentTerm" 
    EnableModelValidation="True" CssClass="GridPaymentTerm" >
    <Columns>

      <asp:BoundField DataField="ProjectName" HeaderText="ProjectName" 
        ReadOnly="True" SortExpression="ProjectName" />

      <asp:TemplateField>
        <ItemTemplate>
          <asp:DropDownList ID="DropDownListPriority" runat="server" 
            DataTextField="Priority" DataValueField="Priority" Width="40px" 
            selectedvalue='<%# Bind("Priority") %>' AppendDataBoundItems="True" 
            AutoPostBack="True" 
            onselectedindexchanged="DropDownListPriority_SelectedIndexChanged" >
          </asp:DropDownList>
        </ItemTemplate>
      </asp:TemplateField>

      <asp:TemplateField HeaderText="PayReqNo" SortExpression="PayReqNo">
       <ItemTemplate>
        <asp:Label ID="LabelPayReqNo" runat="server" Text='<%# Bind("PayReqNo") %>'  ></asp:Label>
       </ItemTemplate>
      </asp:TemplateField>

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
        ReadOnly="True" SortExpression="PoSumWithVAT" >
<HeaderStyle Width="82px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="82px"></ItemStyle>
      </asp:BoundField>
      <asp:BoundField DataField="PoPaidWithVAT" HeaderText="Po Paid With VAT" 
        HeaderStyle-Width="82px" ItemStyle-Width="82px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"
        ReadOnly="True" SortExpression="PoPaidWithVAT" >
<HeaderStyle Width="82px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="82px"></ItemStyle>
      </asp:BoundField>
      <asp:BoundField DataField="POBalanceWithVAT" HeaderText="PO Balance With VAT" 
        HeaderStyle-Width="82px" ItemStyle-Width="82px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"
        ReadOnly="True" SortExpression="POBalanceWithVAT" >
<HeaderStyle Width="82px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="82px"></ItemStyle>
      </asp:BoundField>
      <asp:BoundField DataField="InvoiceValueWithVAT" 
        HeaderStyle-Width="82px" ItemStyle-Width="82px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"
        HeaderText="Invoice Value With VAT" ReadOnly="True" 
        SortExpression="InvoiceValueWithVAT" >
<HeaderStyle Width="82px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="82px"></ItemStyle>
      </asp:BoundField>
    </Columns>
        <RowStyle  CssClass="GridPaymentTermRow" />
        <HeaderStyle  CssClass="GridPaymentTermHeader" />
  </asp:GridView>
  <asp:SqlDataSource ID="SqlDataSourcePaymentTerm" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand="SELECT     dbo.View_PaymentTerm.PayReqNo, dbo.View_PaymentTerm.ProjectName, dbo.View_PaymentTerm.PO_No, dbo.View_PaymentTerm.SupplierName, 
                      dbo.View_PaymentTerm.InvoiceNo, dbo.View_PaymentTerm.PO_Currency, dbo.View_PaymentTerm.PoSumWithVAT, dbo.View_PaymentTerm.PoPaidWithVAT, 
                      dbo.View_PaymentTerm.POBalanceWithVAT, dbo.View_PaymentTerm.InvoiceValueWithVAT, dbo.View_PaymentTerm.PaymentTerm, dbo.View_PaymentTerm.CreatedBy, (CASE WHEN dbo.View_PaymentTerm.Priority IS NULL THEN 0 ELSE dbo.View_PaymentTerm.Priority END) AS Priority
FROM         dbo.View_PaymentTerm INNER JOIN
                      dbo.Table2_PONo ON dbo.View_PaymentTerm.PO_No = dbo.Table2_PONo.PO_No INNER JOIN
                      dbo.Table1_Project ON dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID INNER JOIN
                      dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN
                      dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId
WHERE     (dbo.View_PaymentTerm.ProjectID = @ProjectID) AND (dbo.aspnet_Users.UserName = @UserName) 
ORDER BY dbo.View_PaymentTerm.ProjectName, dbo.View_PaymentTerm.PayReqNo ASC">
  <SelectParameters>
   <asp:ControlParameter ControlID="DropDownListPrj" Name="ProjectID" 
     PropertyName="SelectedValue" />
   <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" 
     PropertyName="Text" />
  </SelectParameters>  
  </asp:SqlDataSource>
</asp:Content>


<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeFile="PaymentTerms.aspx.vb" Inherits="PaymentTerms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<table>
 <tr>
    <td style="width: 300px">
    <asp:DropDownList ID="DropDownListPrj" runat="server" AutoPostBack="True" 
      DataSourceID="SqlDataSourcePrj" 
      DataTextField="ProjectNameVisible" DataValueField="ProjectName" Width="267px">
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


   <table>
    <tr>
     <td style="width: 150px;">
        <asp:LinkButton ID="LinkButtonPDFviEw" runat="server" PostBackUrl="~/webforms/PaymentTermsPDF.aspx" CssClass="btn btn-mini">Switch To PDF View</asp:LinkButton>
     </td>
     <td>
        <asp:HyperLink ID="HyperLinkManual" runat="server" CssClass="btn-mini"
        NavigateUrl="~/paymenttermsinfo.htm" >HOW IT WORKS!</asp:HyperLink>
     </td>
    </tr>
   </table>


  </td>
  <td style="border: 1px solid #C0C0C0; padding: 1px; margin: 1px; width: 400px; background-color: #F8F8FF;">
   <asp:Label ID="LabelRatesToShow" runat="server" ></asp:Label>
  </td>
  <td>
                 <asp:DropDownList ID="DDLsort" runat="server" AutoPostBack="true" >
                                        <asp:ListItem Value="Not Sorted">Not Sorted</asp:ListItem>                            
                                        <asp:ListItem Value="Sort By Payment Term">Sort By Payment Term</asp:ListItem>
                 </asp:DropDownList>
  </td>
 </tr>
</table>

    <asp:TextBox ID="TextBoxUserName" runat="server" CssClass="hidepanel"></asp:TextBox>

  <asp:Label ID="LabelHeaderColumn1" runat="server" CssClass="hidepanel"></asp:Label>
  <asp:Label ID="LabelHeaderColumn2" runat="server" CssClass="hidepanel"></asp:Label>
  <asp:Label ID="LabelHeaderColumn3" runat="server" CssClass="hidepanel"></asp:Label>
  <asp:Label ID="LabelHeaderColumn4" runat="server" CssClass="hidepanel"></asp:Label>

    <asp:DropDownList ID="DropDownTodayRates" runat="server" CssClass="hide"
    DataSourceID="SqlDataSourceTodayRates" DataTextField="Rates" 
    DataValueField="Rates"  >
  </asp:DropDownList>
  <asp:SqlDataSource ID="SqlDataSourceTodayRates" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" >
  </asp:SqlDataSource>

    <asp:DropDownList ID="DropDownListRatesLatest" runat="server" CssClass="hide"
    DataSourceID="SqlDataSourceRatesLatest" DataTextField="Rates" 
    DataValueField="Rates"  >
  </asp:DropDownList>
  <asp:SqlDataSource ID="SqlDataSourceRatesLatest" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
    SelectCommand="SELECT TOP 1 rtrim(convert(NvarChar(7),[RubbleDollar]))+ 
     N'/'+rtrim(convert(NvarChar(7),[RubbleEuro])) AS Rates FROM [Table8_ExchangeRates] 
     ORDER BY Date DESC" >
  </asp:SqlDataSource>

  <asp:GridView ID="GridViewPaymentTerms" runat="server" AutoGenerateColumns="False" 
    DataKeyNames="PayReqNo" DataSourceID="SqlDataSourcePaymentTerm" 
    EnableModelValidation="True" CssClass="GridPaymentTerm" ShowFooter="True">
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
      
      <asp:TemplateField  HeaderStyle-Width="10px" ItemStyle-Width="10px">
       <ItemTemplate>
        <asp:ImageButton ID="ImageButton1" runat="server" Visible="false"
        CommandName="Column1" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"/>
       </ItemTemplate>
      </asp:TemplateField>

      <asp:BoundField HeaderText="Tuesday" 
        HeaderStyle-Width="82px" ItemStyle-Width="82px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"
        ReadOnly="True" />

      <asp:TemplateField HeaderStyle-Width="10px" ItemStyle-Width="10px">
       <ItemTemplate>
        <asp:ImageButton ID="ImageButton2" runat="server" Visible="false"
        CommandName="Column2" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"/>
       </ItemTemplate>
      </asp:TemplateField>

      <asp:BoundField HeaderText="Thursday" 
        HeaderStyle-Width="82px" ItemStyle-Width="82px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"
        ReadOnly="True" />

      <asp:TemplateField  HeaderStyle-Width="10px" ItemStyle-Width="10px">
       <ItemTemplate>
        <asp:ImageButton ID="ImageButton3" runat="server" Visible="false"
        CommandName="Column3" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"/>
       </ItemTemplate>
      </asp:TemplateField>

      <asp:BoundField HeaderText="Next Tuesday" 
        HeaderStyle-Width="82px" ItemStyle-Width="82px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"
        ReadOnly="True" />

      <asp:TemplateField  HeaderStyle-Width="10px" ItemStyle-Width="10px">
       <ItemTemplate>
        <asp:ImageButton ID="ImageButton4" runat="server" Visible="false"
        CommandName="Column4" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"/>
       </ItemTemplate>
      </asp:TemplateField>

      <asp:BoundField HeaderText="Next Thursday" 
        HeaderStyle-Width="82px" ItemStyle-Width="82px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"
        ReadOnly="True" />


      <asp:BoundField DataField="PaymentTerm" HeaderText="PaymentTerm" 
        SortExpression="PaymentTerm" DataFormatString="{0:dd/MM/yyyy}" 
        ItemStyle-CssClass="hidepanel" HeaderStyle-CssClass="hidepanel" FooterStyle-CssClass="hidepanel" />
    </Columns>
        <RowStyle  CssClass="GridPaymentTermRow" />
        <HeaderStyle  CssClass="GridPaymentTermHeader" />
  </asp:GridView>
  <asp:SqlDataSource ID="SqlDataSourcePaymentTerm" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand="SELECT     dbo.View_PaymentTerm.PayReqNo, dbo.View_PaymentTerm.ProjectName, dbo.View_PaymentTerm.PO_No, dbo.View_PaymentTerm.SupplierName, 
                      dbo.View_PaymentTerm.InvoiceNo, dbo.View_PaymentTerm.PO_Currency, dbo.View_PaymentTerm.PoSumWithVAT, dbo.View_PaymentTerm.PoPaidWithVAT, 
                      dbo.View_PaymentTerm.POBalanceWithVAT, dbo.View_PaymentTerm.InvoiceValueWithVAT, dbo.View_PaymentTerm.PaymentTerm, dbo.View_PaymentTerm.CreatedBy
FROM         dbo.View_PaymentTerm INNER JOIN
                      dbo.Table2_PONo ON dbo.View_PaymentTerm.PO_No = dbo.Table2_PONo.PO_No INNER JOIN
                      dbo.Table1_Project ON dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID INNER JOIN
                      dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN
                      dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId
WHERE     (dbo.View_PaymentTerm.ProjectName LIKE '%' + @ProjectName + '%') AND (dbo.aspnet_Users.UserName = @UserName) 
ORDER BY dbo.View_PaymentTerm.ProjectName, dbo.View_PaymentTerm.PayReqNo ASC">
  <SelectParameters>
   <asp:ControlParameter ControlID="DropDownListPrj" Name="ProjectName" 
     PropertyName="SelectedValue" ConvertEmptyStringToNull="False" />
   <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" 
     PropertyName="Text" />
  </SelectParameters>  
  </asp:SqlDataSource>
</asp:Content>


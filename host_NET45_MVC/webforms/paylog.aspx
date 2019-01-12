<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeFile="paylog.aspx.vb" Inherits="Paylog2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

  <asp:TextBox ID="TextBoxFinanceNoValidation" runat="server" CssClass="hidepanel" ></asp:TextBox>

    <div class="btn-grp">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorPaymentdate" ControlToValidate="PaymentDateTextBoxShown"
                      runat="server" ErrorMessage="dd/mm/yyyy"  
                      ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                      CssClass="LabelGeneral" 
                      ValidationGroup="ValidateDateFormatOnly" Display="Dynamic">
                    </asp:RegularExpressionValidator>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPaymentDate" runat="server" 
                      ErrorMessage="Required" ControlToValidate="PaymentDateTextBoxShown" 
                      CssClass="LabelGeneral" 
                      ValidationGroup="ValidateDateFormatOnly" Display="Dynamic">
                    </asp:RequiredFieldValidator>

      <asp:Button ID="ButtonPayAll" runat="server" Text="PAY ALL" CssClass="btn btn-mini btn-default"
        ValidationGroup="FinanceNoValidation"  />

        <asp:HyperLink ID="HyperLinkVirtual1SEntry" runat="server" CssClass="btn btn-mini btn-default"
            NavigateUrl="~/webforms/PTS_1S_DataEntry.aspx" Target="_blank" Text="Enter 1S Payment File"></asp:HyperLink>

        <asp:HyperLink ID="HyperLinkVirtualInvoice" runat="server" CssClass="btn btn-mini btn-default"
            NavigateUrl="~/webforms/CreateVirtualPo.aspx" Target="_blank" Text="Create Virtual Invoice"></asp:HyperLink>

        <a href="/webforms/paymentsNotes.aspx" target="_blank" class="btn btn-mini btn-default" >Update Notes For Auto Email</a>

        <asp:Button ID="ButtonExchangeRate" runat="server" Text="Update Rates" CausesValidation="False" CssClass="btn btn-mini btn-default" />

    </div>

        <asp:DropDownList ID="DropDownListProjectsInPending" runat="server" 
          DataSourceID="SqlDataSourceProjectsInPending" DataTextField="ProjectName" 
          DataValueField="ProjectID" AutoPostBack="True" >
        </asp:DropDownList>

        <asp:Label ID="LabelTotalPaidItem" runat="server" CssClass="label label-sm label-danger arrowed arrowed-right" Text="Label"></asp:Label>

        <asp:SqlDataSource ID="SqlDataSourceProjectsInPending" runat="server" 
          ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand="SELECT * FROM (
          SELECT 0 As ProjectID, N'_Select Project' AS ProjectName

          UNION ALL

          SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + N' - ' + RTRIM(CONVERT(nvarchar(50), dbo.Table1_Project.ProjectID)) 
                                AS ProjectName
          FROM         dbo.Table1_Project INNER JOIN
                                dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN
                                dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN
                                dbo.Table4_PaymentRequest ON dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID LEFT OUTER JOIN
                                dbo.Table5_PayLog ON dbo.Table4_PaymentRequest.PayReqNo = dbo.Table5_PayLog.PayReqNo
          WHERE     (dbo.Table5_PayLog.PayReqNo IS NULL)
          GROUP BY dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + N' - ' + RTRIM(CONVERT(nvarchar(50), dbo.Table1_Project.ProjectID))
          ) AS DataSource1
          ORDER BY ProjectName ASC">
        </asp:SqlDataSource>

                    <asp:TextBox ID="PaymentDateTextBoxShown" runat="server" 
                      CssClass="add_datepicker" AutoPostBack="True"  CausesValidation="True"  Width="100px"
                      ValidationGroup="ValidateDateFormatOnly" />

      <asp:CompareValidator ID="CompareValidatorFinanceCheck" runat="server" 
        ControlToValidate="TextBoxFinanceNoValidation" CssClass="LabelGeneral" Display="Dynamic"
        ErrorMessage="Please provide &quot;Finance No&quot; for missing items highlighted by RED" 
        ValidationGroup="FinanceNoValidation" ValueToCompare="0" Font-Bold="True"></asp:CompareValidator>

    <hr />

    <asp:GridView ID="GridViewItemsToPay" runat="server" AutoGenerateColumns="False" DataKeyNames="PayReqNo"
    DataSourceID="SqlDataSourceItemsToPay" EnableModelValidation="True" CssClass="table table-nonfluid table-hover" GridLines="None"  >
      <Columns>

        <asp:BoundField DataField="PayReqNo" ReadOnly="True"
          ItemStyle-CssClass="hidepanel" HeaderStyle-CssClass="hidepanel" >
        </asp:BoundField>

        <asp:BoundField DataField="PO_No" HeaderText="PO_No" ReadOnly="True" 
          SortExpression="PO_No" HeaderStyle-Width="100px" ItemStyle-Width="100px">
        </asp:BoundField>

        <asp:BoundField DataField="Description" HeaderText="Description" 
          ReadOnly="True" SortExpression="Description" HeaderStyle-Width="200px" 
          ItemStyle-Width="200px">

<HeaderStyle Width="200px"></HeaderStyle>

<ItemStyle Width="200px"></ItemStyle>
        </asp:BoundField>

        <asp:BoundField DataField="Invoice_No" HeaderText="Invoice No" ReadOnly="True" 
          SortExpression="Invoice_No" HeaderStyle-Width="80px" ItemStyle-Width="80px">

<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle Width="80px"></ItemStyle>
        </asp:BoundField>

        <asp:BoundField DataField="Invoice_Date" HeaderText="Invoice Date" DataFormatString="{0:dd/MM/yyyy}"
          SortExpression="Invoice_Date" HeaderStyle-Width="80px" 
          ItemStyle-Width="80px">

<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle Width="80px"></ItemStyle>
        </asp:BoundField>

        <asp:BoundField DataField="Wo_VAT" HeaderText="Wo_VAT" ReadOnly="True" 
          HeaderStyle-Width="100px" ItemStyle-Width="100px"
          SortExpression="Wo_VAT" DataFormatString="{0:N2}" 
          ItemStyle-HorizontalAlign="Right">

<HeaderStyle Width="100px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
        </asp:BoundField>

        <asp:BoundField DataField="PO_Currency" HeaderText="Original Currency" 
          ReadOnly="True" SortExpression="PO_Currency" HeaderStyle-Width="80px" 
          ItemStyle-Width="80px">

<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle Width="80px"></ItemStyle>
        </asp:BoundField>

        <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" ItemStyle-CssClass="ValueToPay"
          ReadOnly="True" SortExpression="SupplierName" HeaderStyle-Width="100px" 
          ItemStyle-Width="100px" >
        </asp:BoundField>

        <asp:BoundField DataField="WithVAT" HeaderText="In Ruble With VAT" 
          ReadOnly="True" HeaderStyle-Width="100px" ItemStyle-Width="100px"
          SortExpression="WithVAT" DataFormatString="{0:N2}" 
          ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="ValueToPay" >

<HeaderStyle Width="100px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" CssClass="ValueToPay" Width="100px"></ItemStyle>
        </asp:BoundField>

        <asp:TemplateField>
         <ItemTemplate>
          <asp:Button Id="ButtonMove" runat="server"  Text=">" Width="30px"
          CommandName="Move" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"/>
         </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Payment Value" HeaderStyle-Width="100px" ItemStyle-Width="100px">
         <ItemTemplate>
          <div>
           <asp:TextBox ID="TextBoxPaidValue" runat="server" Width="90px" 
            ForeColor="#660066" Font-Bold="True" Font-Size="13px" BackColor="Lavender"
            EnableViewState="True" AutoPostBack="True" 
              ontextchanged="TextBoxPaidValue_TextChanged" CausesValidation="True"></asp:TextBox>
           <asp:RegularExpressionValidator ID="RegularExpressionValidatorPaidValue" ControlToValidate="TextBoxPaidValue" Display="Dynamic"
            runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
           </asp:RegularExpressionValidator>
          </div>
          <div>
           <asp:Label ID="LabelWarningPaidValue" runat="server"></asp:Label>
          </div>
         </ItemTemplate>

<HeaderStyle Width="100px"></HeaderStyle>

<ItemStyle Width="100px"></ItemStyle>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Finance No" HeaderStyle-Width="50px" ItemStyle-Width="50px">
         <ItemTemplate>
           <asp:TextBox ID="TextBoxFinanceNo" runat="server" Width="45px" 
            ForeColor="#660066" Font-Bold="True" Font-Size="13px" BackColor="Lavender" 
             ontextchanged="TextBoxFinanceNo_TextChanged" AutoPostBack="true">
           </asp:TextBox>

          <div>
           <asp:Label ID="LabelWarningFinanceNo" runat="server"></asp:Label>
          </div>
             <asp:Label ID="LabelPayReqNo" runat="server" Text='<%# Eval("PayReqNo")%>' CssClass="hidepanel"></asp:Label>
             
         </ItemTemplate>

<HeaderStyle Width="50px"></HeaderStyle>

<ItemStyle Width="50px"></ItemStyle>
        </asp:TemplateField>
        
        <asp:BoundField DataField="LinkToInvoice" HeaderText="LinkToInvoice" 
          ReadOnly="True" SortExpression="LinkToInvoice" 
          ItemStyle-CssClass="hidepanel" HeaderStyle-CssClass="hidepanel" >

<HeaderStyle CssClass="hidepanel"></HeaderStyle>

<ItemStyle CssClass="hidepanel"></ItemStyle>
        </asp:BoundField>

        <asp:BoundField DataField="Approved" HeaderText="Approved" ReadOnly="True" HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide"
          SortExpression="Approved" ItemStyle-HorizontalAlign="Center">
        </asp:BoundField>

        <asp:TemplateField HeaderText="Dollar" HeaderStyle-Width="50px" ItemStyle-Width="50px" ControlStyle-Width="50px">
         <ItemTemplate>
           <asp:TextBox ID="TextBoxDollar" runat="server" EnableViewState="True" AutoPostBack="True" CausesValidation="True" OnTextChanged="TextBoxDollar_TextChanged" CssClass="TextBoxGeneralRev" ></asp:TextBox>
           <asp:RegularExpressionValidator ID="RegularExpressionValidatorDollar" ControlToValidate="TextBoxDollar" Display="Dynamic"
            runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
           </asp:RegularExpressionValidator>
         </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Euro" HeaderStyle-Width="50px" ItemStyle-Width="50px" ControlStyle-Width="50px">
         <ItemTemplate>
           <asp:TextBox ID="TextBoxEuro" runat="server" EnableViewState="True" AutoPostBack="True" CausesValidation="True" OnTextChanged="TextBoxEuro_TextChanged" CssClass="TextBoxGeneralRev" ></asp:TextBox>
           <asp:RegularExpressionValidator ID="RegularExpressionValidatorEuro" ControlToValidate="TextBoxEuro" Display="Dynamic"
            runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
           </asp:RegularExpressionValidator>
         </ItemTemplate>
        </asp:TemplateField>

      </Columns>
      <HeaderStyle CssClass="headergridnew" />
  </asp:GridView>

  <asp:SqlDataSource ID="SqlDataSourceItemsToPay" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand="SELECT * FROM [View_WhatToBePaidRevised] WHERE (ProjectID = @ProjectID) AND (Date = @Date)">
        <SelectParameters>
          <asp:ControlParameter ControlID="DropDownListProjectsInPending" DefaultValue="0" 
           Name="ProjectID" PropertyName="SelectedValue" Type="Int32" />
          <asp:Parameter Name="Date" />
        </SelectParameters>
  </asp:SqlDataSource>


</asp:Content>


<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="EnterAllInOne.aspx.vb" Inherits="EnterAllInOne" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<h2 style="display:inline;">Get the latest entries from the report <bold style="font:red;">RecentEntriesFrom1S.rdl</bold> </h2>

        <asp:ImageButton ID="ImageButtonExportExcel" runat="server" Width="20px" ToolTip="Export To Excel" OnClick="ImageButtonExportExcel_Click"
            ImageUrl="~/Images/Excel.jpg" Visible="True" />

<div style="text-align: center; width: 100%">
    <rsweb:ReportViewer ID="ReportViewerCostReport" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="false" ShowZoomControl="true" Visible="false" 
    SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
    </rsweb:ReportViewer>
</div>


  Enter<asp:Button ID="ButtonEnter" runat="server" Text="Enter" />

    <asp:GridView ID="GridViewFinance" runat="server" CssClass="Grid" 
         AllowSorting="True" AutoGenerateColumns="False" 
    DataSourceID="SqlDataSourceFrom1S_to_PTS" EnableModelValidation="True" >
        <pagerstyle  horizontalalign="Center" CssClass="pager" />
        <RowStyle  CssClass="GridItemNakladnaya" />
        <Columns>
          <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" 
            SortExpression="ProjectID" />
          <asp:BoundField DataField="CostCode" HeaderText="CostCode" 
            SortExpression="CostCode" />
          <asp:BoundField DataField="Description" HeaderText="Description" 
            SortExpression="Description" />
          <asp:BoundField DataField="PaymentDate" HeaderText="PaymentDate" 
            SortExpression="PaymentDate" />
          <asp:BoundField DataField="Amount" HeaderText="Amount" 
            SortExpression="Amount" />
          <asp:BoundField DataField="VAT_AsItIs" HeaderText="VAT_AsItIs" 
            SortExpression="VAT_AsItIs" />
          <asp:BoundField DataField="InvoiceValue" HeaderText="InvoiceValue" 
            SortExpression="InvoiceValue" />
        </Columns>
        <HeaderStyle  CssClass="GridHeader" />
    </asp:GridView>


  <asp:SqlDataSource ID="SqlDataSourceFrom1S_to_PTS" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand="SELECT [ProjectID]
      ,[CostCode]
      ,[Description]
      ,[PaymentDate]
      ,[Amount]
      ,[VAT_AsItIs]
      ,[InvoiceValue]
  FROM [Table_From1S_to_PTS]"></asp:SqlDataSource>

  <asp:SqlDataSource ID="SqlDataSourceInsertPO" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    InsertCommand="INSERT INTO [Table2_PONo]
           ([PO_No]
           ,[Project_ID]
           ,[SupplierID]
           ,[Description]
           ,[TotalPrice]
           ,[PO_Currency]
           ,[VATpercent]
           ,[CostCode]
           ,[PO_Date]
           ,[CreatedBy]
           ,[PersonCreated])
     VALUES
           (@PO_No
           ,@Project_ID
           ,@SupplierID
           ,@Description
           ,@TotalPrice
           ,@PO_Currency
           ,@VATpercent
           ,@CostCode
           ,@PO_Date
           ,@CreatedBy
           ,@PersonCreated)">
                            <InsertParameters>
                                <asp:Parameter Name="PO_No" />
                                <asp:Parameter Name="Project_ID" />
                                <asp:Parameter Name="SupplierID" />
                                <asp:Parameter Name="Description" />
                                <asp:Parameter Name="TotalPrice" />
                                <asp:Parameter Name="PO_Currency" />
                                <asp:Parameter Name="VATpercent" />
                                <asp:Parameter Name="CostCode" />
                                <asp:Parameter Name="PO_Date" />
                                <asp:Parameter Name="CreatedBy" />
                                <asp:Parameter Name="PersonCreated" />
                            </InsertParameters>
  </asp:SqlDataSource>

  <asp:SqlDataSource ID="SqlDataSourceInsertInvoice" runat="server"
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    InsertCommand="INSERT INTO [Table3_Invoice]
           ([Invoice_No]
           ,[Invoice_Date]
           ,[PO_No]
           ,[InvoiceValue]
           ,[CreatedBy]
           ,[PersonCreated])
     VALUES
           (@Invoice_No
           ,@Invoice_Date
           ,@PO_No
           ,@InvoiceValue
           ,@CreatedBy
           ,@PersonCreated)">
                            <InsertParameters>
                                <asp:Parameter Name="Invoice_No" />
                                <asp:Parameter Name="Invoice_Date" />
                                <asp:Parameter Name="PO_No" />
                                <asp:Parameter Name="InvoiceValue" />
                                <asp:Parameter Name="CreatedBy" />
                                <asp:Parameter Name="PersonCreated" />
                            </InsertParameters>
  </asp:SqlDataSource>


  <asp:DropDownList ID="DropDownListInvoiceID" runat="server" 
    DataSourceID="SqlDataSourceInvoiceID" DataTextField="InvoiceID" 
    DataValueField="InvoiceID">
  </asp:DropDownList>
  <asp:SqlDataSource ID="SqlDataSourceInvoiceID" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand="SELECT     MAX(dbo.Table3_Invoice.InvoiceID) AS InvoiceID, dbo.Table2_PONo.PO_No
FROM         dbo.Table2_PONo INNER JOIN
                      dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No
GROUP BY dbo.Table2_PONo.PO_No
HAVING      (dbo.Table2_PONo.PO_No = @PO_No)">
                            <SelectParameters>
                                <asp:Parameter Name="PO_No" />
                            </SelectParameters>
  </asp:SqlDataSource>

  <asp:SqlDataSource ID="SqlDataSourceInsertPaymentRequest" runat="server"
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    InsertCommand="INSERT INTO [Table4_PaymentRequest]
           ([SiteRecordNo]
           ,[InvoiceID]
           ,[PayReqDate]
           ,[Approved]
           ,[PersonApprove]
           ,[LastAction]
           ,[CreatedBy]
           ,[PersonCreated]
           ,[AttachmentExist])
     VALUES
           (@SiteRecordNo
           ,@InvoiceID
           ,@PayReqDate
           ,@Approved
           ,@PersonApprove
           ,@LastAction
           ,@CreatedBy
           ,@PersonCreated
	   ,@AttachmentExist)">
                            <InsertParameters>
                              <asp:Parameter Name="SiteRecordNo" />
                              <asp:Parameter Name="InvoiceID" />
                              <asp:Parameter Name="PayReqDate" />
                              <asp:Parameter Name="Approved" />
                              <asp:Parameter Name="PersonApprove" />
                              <asp:Parameter Name="LastAction" />
                              <asp:Parameter Name="CreatedBy" />
                              <asp:Parameter Name="PersonCreated" />
                              <asp:Parameter Name="AttachmentExist" />
                            </InsertParameters>
  </asp:SqlDataSource>

  <asp:DropDownList ID="DropDownListPayReqNo" runat="server" 
    DataSourceID="SqlDataSourcePayReqNo" DataTextField="PayReqNo" 
    DataValueField="PayReqNo">
  </asp:DropDownList>
  <asp:SqlDataSource ID="SqlDataSourcePayReqNo" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand="SELECT     MAX(Table4_PaymentRequest.PayReqNo) AS PayReqNo
FROM         Table2_PONo INNER JOIN
                      Table3_Invoice ON Table2_PONo.PO_No = Table3_Invoice.PO_No INNER JOIN
                      Table4_PaymentRequest ON Table3_Invoice.InvoiceID = Table4_PaymentRequest.InvoiceID
GROUP BY Table3_Invoice.InvoiceID
HAVING      (Table3_Invoice.InvoiceID = @InvoiceID)">
                            <SelectParameters>
                                <asp:Parameter Name="InvoiceID" />
                            </SelectParameters>
  </asp:SqlDataSource>

  <asp:SqlDataSource ID="SqlDataSourcePayLog" runat="server"
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    InsertCommand="INSERT INTO [Table5_PayLog]
           ([PayReqNo]
           ,[FinanceNo]
           ,[PaymentDate]
           ,[Amount]
           ,[Currency]
           ,[CreatedBy]
           ,[PersonCreated]
           ,[RubbleDollar]
           ,[RubbleEuro])
     VALUES
           (@PayReqNo
           ,@FinanceNo
           ,@PaymentDate
           ,@Amount
           ,@Currency
           ,@CreatedBy
           ,@PersonCreated
           ,@RubbleDollar
           ,@RubbleEuro )">
                            <InsertParameters>
                              <asp:Parameter Name="PayReqNo" />
                              <asp:Parameter Name="FinanceNo" />
                              <asp:Parameter Name="PaymentDate" />
                              <asp:Parameter Name="Amount" />
                              <asp:Parameter Name="Currency" />
                              <asp:Parameter Name="CreatedBy" />
                              <asp:Parameter Name="PersonCreated" />
                              <asp:Parameter Name="RubbleDollar" />
                              <asp:Parameter Name="RubbleEuro" />
                            </InsertParameters>
  </asp:SqlDataSource>




</asp:Content>


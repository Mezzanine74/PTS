<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" EnableEventValidation="false" AutoEventWireup="false" CodeFile="Pendinglist.aspx.vb" Inherits="PendinglistRev" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Pending List</title>

<script type="text/javascript">
    $(document).ready(function () {

        $('#<%=Page.Master.FindControl("MainContent").FindControl("SpanProjectDetails").ClientID%>').click(function () {
            $("#myModalPDFNote").modal('show');
        });


        $('#MainContent_SpanProjectDetails').click(function () {
            $("#ModalProjectSummary").modal('show');
        });

    });
</script>

 </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

       
<asp:Panel ID="panelHidden" runat="server" CssClass="hidepanel">
    <rsweb:ReportViewer ID="ReportViewerPendingToExcel" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="false" ShowZoomControl="true" Visible="false" 
    SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
    </rsweb:ReportViewer>
</asp:Panel>

    <asp:DropDownList ID="DropDownListPrj" runat="server" 
        DataSourceID="SqlDataSourcePrj" DataTextField="ProjectName" 
        DataValueField="ProjectID" AutoPostBack="True" >
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand="SELECT     TOP (100) PERCENT ProjectID, ProjectName
                        FROM         dbo.View_PendingListRev1
                        GROUP BY ProjectID, ProjectName
                        ORDER BY ProjectName">
    </asp:SqlDataSource>

        <asp:DropDownList ID="DropDownListSupplier" runat="server" 
        DataSourceID="SqlDataSourceSupplier" DataTextField="SupplierName" 
        DataValueField="SupplierID" AutoPostBack="True" >
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSourceSupplier" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand=" IF @ProjectID = 0 
	                        BEGIN 
		                        SELECT RTRIM(SupplierID) AS SupplierID, RTRIM(SupplierName) AS SupplierName FROM(	
		                        SELECT N'0' AS SupplierID, N'_ALL SUPPLIERS' AS SupplierName
		                        UNION ALL
		                        SELECT  SupplierID, RTRIM(SupplierName) AS SupplierName
		                        FROM         dbo.View_PendingListRev1
		                        GROUP BY SupplierID, RTRIM(SupplierName)
		                        ) AS DataSource
		                        ORDER BY SupplierName 
	                        END
                        ELSE
	                        BEGIN
		                        SELECT RTRIM(SupplierID) AS SupplierID, RTRIM(SupplierName) AS SupplierName FROM(	
		                        SELECT N'0' AS SupplierID, N'_ALL SUPPLIERS' AS SupplierName
		                        UNION ALL
		                        SELECT  SupplierID, RTRIM(SupplierName) AS SupplierName
		                        FROM         dbo.View_PendingListRev1
		                        WHERE     (ProjectID = @ProjectID)
		                        GROUP BY SupplierID, RTRIM(SupplierName)
		                        ) AS DataSource
		                        ORDER BY SupplierName 
	                        END ">
      <SelectParameters>
        <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="1" 
          Name="ProjectID" PropertyName="SelectedValue" />
      </SelectParameters>
    </asp:SqlDataSource>

    <div id="ModalProjectSummary" class="modal">
        <div class="modal-dialog modal-dialog-center">
            <div class="modal-content modal_inlineBlock" style="padding:20px!important;">
                <div class="modal-body" style="width: 750px;">
                    <div class="modal-header>">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">details</h4>
                    </div>

                    <asp:DataList ID="DataListDiffCurrencyTotal" runat="server" CssClass="pull-left" 
                        DataSourceID="SqlDataSourceDiffCurrencyTotal" RepeatDirection="Horizontal"
                        Font-Size="10px">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td colspan="3" class="PsummaryHdr">Pending Invoice totals as per original currency
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 90px; color: #0000FF; font-weight: bold;">
                                        <asp:Literal ID="Literal1" runat="server" Text="Euro With VAT"></asp:Literal>
                                        <br />
                                        <asp:Literal ID="LiteralEuro" runat="server" Text='<%# Bind("EuroWithVAT","{0:###,###,###.00}") %>'></asp:Literal>
                                    </td>
                                    <td style="width: 90px; color: #009933; font-weight: bold;">
                                        <asp:Literal ID="Literal2" runat="server" Text="Dollar With VAT"></asp:Literal>
                                        <br />
                                        <asp:Literal ID="LiteralDollar" runat="server" Text='<%# Bind("DollarWithVAT","{0:###,###,###.00}") %>'></asp:Literal>
                                    </td>
                                    <td style="width: 90px; color: #FF0000; font-weight: bold;">
                                        <asp:Literal ID="Literal3" runat="server" Text="Ruble With VAT"></asp:Literal>
                                        <br />
                                        <asp:Literal ID="LiteralRuble" runat="server" Text='<%# Bind("RubleWithVAT","{0:###,###,###.00}") %>'></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>


                    <table class="pull-right">
                        <tr>
                            <td colspan="4" class="PsummaryHdr">Total Pending value at daily exchage rate
                            </td>
                        </tr>

                        <tr>
                            <td class="PsummaryRw"></td>
                            <td class="PsummaryRw">EuroWithVAT
                            </td>
                            <td class="PsummaryRw">DollarWithVAT
                            </td>
                            <td class="PsummaryRw">RubleWithVAT
                            </td>
                        </tr>

                        <tr>
                            <td class="PsummaryRw">All
                            </td>
                            <td class="PHeaderTitleTot">
                                <asp:Label ID="LabelTotalEuroWithVAT" runat="server" Text="-"></asp:Label>
                            </td>
                            <td class="PHeaderTitleTot">
                                <asp:Label ID="LabelTotalDollarWithVAT" runat="server" Text="-"></asp:Label>
                            </td>
                            <td class="PHeaderTitleTot">
                                <asp:Label ID="LabelTotalRubleWithVAT" runat="server" Text="-"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td class="PsummaryRw">Approved
                            </td>
                            <td class="PHeaderTitleAppr">
                                <asp:Label ID="LabelTotalEuroWithVATApproved" runat="server" Text="-"></asp:Label>
                            </td>
                            <td class="PHeaderTitleAppr">
                                <asp:Label ID="LabelTotalDollarWithVATApproved" runat="server" Text="-"></asp:Label>
                            </td>
                            <td class="PHeaderTitleAppr">
                                <asp:Label ID="LabelTotalRubleWithVATApproved" runat="server" Text="-"></asp:Label>
                            </td>
                        </tr>

                    </table>

                </div>
            </div>
        </div>
    </div>

    <span id="SpanProjectDetails" runat="server" class="badge badge-yellow cursor_pointer ">
	    See Summary
    </span>

    <asp:LinkButton ID="ButtonExcel" runat="server" CssClass="badge badge-yellow" OnClick="ButtonExcel_Click1" >
      <i class="ace-icon fa fa-file-excel-o "></i>
      Export To Excel   
    </asp:LinkButton>

   <asp:HyperLink ID="HyperlinkPrjBy" runat="server" NavigateUrl="PendingSummaryByProject.aspx" Target="_blank" 
   CssClass="badge badge-yellow " >Project Breakdown</asp:HyperLink>

    <span id="SpanNumberOfChangedAttachment" runat="server" class="badge badge-danger">
      <i class="ace-icon fa fa-exclamation-triangle "></i>
	  <asp:Literal ID="LiteralAttachmentText" runat="server" ></asp:Literal>
    </span>

    <asp:DropDownList ID="DropDownListNumberOfChangedAttachment" runat="server" 
      DataSourceID="SqlDataSourceNumberOfChangedDataSource" 
      DataTextField="CountOfChangedAttachment" 
      DataValueField="CountOfChangedAttachment" Visible="False">
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSourceNumberOfChangedDataSource" runat="server" 
      ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand="SELECT     COUNT(AttachmentChange) AS CountOfChangedAttachment
FROM         dbo.View_PendingList
WHERE     (AttachmentChange = 1)"></asp:SqlDataSource>


    <hr />
         
    <asp:GridView ID="GridViewPendingList" runat="server" AutoGenerateColumns="False" GridLines="None" 
    DataKeyNames="ProjectID" DataSourceID="SqlDataSourcePendingList" 
        CssClass="table table-nonfluid table-hover" AllowSorting="True" AllowPaging="True" PageSize="40" PagerSettings-Position="TopAndBottom" >
    <Columns>
    
      <asp:BoundField DataField="Approved" HeaderText="Apr." ReadOnly="True" 
        SortExpression="Approved" ItemStyle-Width="80" HeaderStyle-Width="80" ItemStyle-CssClass="hide" HeaderStyle-CssClass="hide"/>

      <asp:BoundField DataField="ProjectName" HeaderText="ProjectName" ReadOnly="True" 
        SortExpression="ProjectName" ItemStyle-Width="80" HeaderStyle-Width="80"/>

      <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" ReadOnly="True" 
         SortExpression="SupplierName" ItemStyle-Width="100" HeaderStyle-Width="100"/>

      <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" 
        SortExpression="Description" ItemStyle-Width="130" HeaderStyle-Width="130"/>

        <asp:TemplateField ItemStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Center" >
                       <HeaderTemplate>
                           From
                       </HeaderTemplate>
                       <ItemTemplate>
<%--                           <div class="DivPersonPRN"><asp:Literal ID="LabelPersonCreated" runat="server" Text='<%# Bind("PersonCreated") %>' EnableViewState="False"></asp:Literal></div>--%>
<%--                           <div class="DivPersonPRN"><asp:Literal ID="LiteralUrgency" runat="server" Text='<%# Bind("Urgency") %>' EnableViewState="False"></asp:Literal></div>
                           <div class="DivPersonPRN"><asp:Literal ID="LiteralApproveBy" runat="server" Text='<%# Bind("PersonApprove") %>' EnableViewState="False"></asp:Literal></div>--%>
                           <usercontrol:ImageUserPhoto ID="userphoto" runat="server" UserName='<%# Bind("PersonCreated") %>' />
                            <asp:Literal ID="LabePayreqDate" runat="server" Text='<%# Bind("PayReqDate","{0:dd/MM/yyyy}") %>' EnableViewState="False" ></asp:Literal>
                           <br />
                            <asp:Literal ID="LiteralPO_No" runat="server" Text='<%# Eval("PO_No")%>' EnableViewState="False" ></asp:Literal>
                       </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField ItemStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
             <asp:HyperLink ID="HyperlinkBannedPo" Target="_blank" runat="server" CssClass="Hlink" onmouseover="this.style.cursor='hand'"  EnableViewState="false" ForeColor="Red" Font-Italic="True" Font-Size="9px">
             </asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>

      <asp:BoundField DataField="PoTotalWithVAT" HeaderText="Po Total With VAT" HeaderStyle-Width="60px" 
        ItemStyle-Width="60px" ReadOnly="True" SortExpression="PoTotalWithVAT" DataFormatString="{0:N2}" 
        ItemStyle-HorizontalAlign="Right" />

      <asp:BoundField DataField="TotalPaidWithVAT" HeaderText="Total Paid With VAT" HeaderStyle-Width="60px" 
        ItemStyle-Width="60px" ReadOnly="True" SortExpression="TotalPaidWithVAT" DataFormatString="{0:N2}" 
        ItemStyle-HorizontalAlign="Right" />

      <asp:BoundField DataField="TotalBalanceWithVAT" HeaderText="Total Balance With VAT" HeaderStyle-Width="60px" 
        ItemStyle-Width="60px" ReadOnly="True" SortExpression="TotalBalanceWithVAT" DataFormatString="{0:N2}" 
        ItemStyle-HorizontalAlign="Right" />

 <asp:TemplateField  ItemStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
                       <HeaderTemplate>
                           <div class="PHeaderSepr" >Invoice No</div>
                           <div class="PHeaderSepr" >Invoice Date</div>                       
                           <div >Site Record No</div>                       
                       </HeaderTemplate>
                       <ItemTemplate>
                           <div Class="DivInvoiceNoPending" ><asp:Literal ID="Label3" runat="server" Text='<%# Bind("Invoice_No") %>'></asp:Literal></div>
                           <div><asp:Literal ID="Label2" runat="server" Text='<%# Bind("Invoice_Date","{0:dd/MM/yyyy}") %>' EnableViewState="False"></asp:Literal></div>
                           <div style="color:Red;"><asp:Literal ID="LabelSiteRecordNo" runat="server" Text='<%# Bind("SiteRecordNo") %>'></asp:Literal></div>
                       </ItemTemplate>
                       <ControlStyle Width="80px" />
                       <HeaderStyle Width="80px" />
                       <ItemStyle HorizontalAlign="Right" />
</asp:TemplateField>

        <asp:TemplateField HeaderText="" SortExpression="PO_Currency" ItemStyle-Width="50" HeaderStyle-Width="50" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Image ID="ImageCurrency" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Total Value Pending With VAT"  ItemStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right" >

            <ItemTemplate>
                <asp:Literal ID="LabelPendingWithVAT" runat="server" Text='<%# Eval("PendingWithVAT","{0:###,###,###.00}") %>' EnableViewState="false" ></asp:Literal>
            </ItemTemplate>

        </asp:TemplateField>
                
        <asp:TemplateField HeaderText="PDF" SortExpression="AttachmentChange" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20" ItemStyle-Width="20">
            <ItemTemplate>
                <div><asp:ImageButton ID="ImageButton1" runat="server" CommandName="OpenPdf" 
                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LinkToInvoice") %>' /></div>
                <div style="max-width:100px!important;"><asp:Label ID="LabelAttachmentChanged" runat="server" Font-Size="10px"></asp:Label></div>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Total Balance o/s After Paying Pending With VAT"  ItemStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right" >

            <ItemTemplate>
                <asp:Literal ID="LabelOSafterPayment" runat="server" EnableViewState="false" Text='<%# Eval("BalanceAfterPending","{0:###,###,###.00}") %>' ></asp:Literal>
            </ItemTemplate>

        </asp:TemplateField>

        <asp:TemplateField>
            <ItemTemplate>
                <a id="split" class="btn btn-mini btn-danger" href="/webforms/PaymentrequestSplitInvoices.aspx?InvoiceId=<%# Eval("InvoiceId")%>" target="_blank">Split Invoice</a>
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>
                <PagerSettings Position="TopAndBottom" />
                <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
                <HeaderStyle CssClass="headergridnew" />
</asp:GridView>

    <asp:SqlDataSource ID="SqlDataSourcePendingList" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
    SelectCommand=" 
        
        DECLARE @ProjectID_ smallint
        DECLARE @SupplierID_ NvarChar(12)

        SET @ProjectID_ = @ProjectID
        SET @SupplierID_ = @SupplierID
                
        if @ProjectID_ = 0  and  @SupplierID_ = N'0'
      SELECT * FROM [View_PendingListRev1] ORDER BY OrderCurrency, PendingWithVAT DESC 

      if @ProjectID_ <> 0 and  @SupplierID_ = N'0'
      SELECT * FROM [View_PendingListRev1] WHERE (ProjectID=@ProjectID_) ORDER BY OrderCurrency, PendingWithVAT DESC

      if @ProjectID_ <> 0 and  @SupplierID_ <> N'0'
      SELECT * FROM [View_PendingListRev1] WHERE (ProjectID=@ProjectID_) AND (SupplierID=@SupplierID_) ORDER BY OrderCurrency, PendingWithVAT DESC

      if @ProjectID_ = 0 and  @SupplierID_ <> N'0'
      SELECT * FROM [View_PendingListRev1] WHERE (SupplierID=@SupplierID_) ORDER BY OrderCurrency, PendingWithVAT DESC " >
            <SelectParameters>
              <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="" 
                Name="ProjectID" PropertyName="SelectedValue" />
              <asp:ControlParameter ControlID="DropDownListSupplier" DefaultValue="" 
                Name="SupplierID" PropertyName="SelectedValue" />
            </SelectParameters>
    </asp:SqlDataSource>
    
    

                    <asp:SqlDataSource ID="SqlDataSourceDiffCurrencyTotal" runat="server" 
                             ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                             SelectCommand=" If @ProjectID = 0 and @SupplierID = N'0'
SELECT     SUM(CASE WHEN dbo.Table2_PONo.PO_Currency = N'Euro' THEN CONVERT(decimal(12, 2), 
                      CASE WHEN dbo.Table2_PONo.PO_Currency = N'Rub' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE
                       dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      / dbo.View_MaxExchangeRate.RubbleEuro ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Dollar' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free
                       = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      * dbo.View_MaxExchangeRate.RubbleDollar / dbo.View_MaxExchangeRate.RubbleEuro ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Euro' THEN
                       (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent)
                       / 100) END) END) END) END) ELSE 0 END) AS EuroWithVAT, SUM(CASE WHEN dbo.Table2_PONo.PO_Currency = N'Dollar' THEN CONVERT(decimal(12,
                       2), 
                      CASE WHEN dbo.Table2_PONo.PO_Currency = N'Rub' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE
                       dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      / dbo.View_MaxExchangeRate.RubbleDollar ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Dollar' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free
                       = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Euro' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue
                       ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      * dbo.View_MaxExchangeRate.RubbleEuro / dbo.View_MaxExchangeRate.RubbleDollar END) END) END) ELSE 0 END) AS DollarWithVAT, 
                      SUM(CASE WHEN dbo.Table2_PONo.PO_Currency = N'Rub' THEN CONVERT(decimal(12, 2), 
                      CASE WHEN dbo.Table2_PONo.PO_Currency = N'Rub' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE
                       dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Dollar' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue
                       ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      * dbo.View_MaxExchangeRate.RubbleDollar ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Euro' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free
                       = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      * dbo.View_MaxExchangeRate.RubbleEuro END) END) END) ELSE 0 END) AS RubleWithVAT
FROM         dbo.Table1_Project INNER JOIN
                      dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN
                      dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN
                      dbo.Table4_PaymentRequest ON dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID INNER JOIN
                      dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID INNER JOIN
                      dbo.Table7_CostCode ON dbo.Table2_PONo.CostCode = dbo.Table7_CostCode.CostCode LEFT OUTER JOIN
                      dbo.View_POsBannedInPending ON dbo.Table2_PONo.PO_No = dbo.View_POsBannedInPending.PO_No LEFT OUTER JOIN
                      dbo.Table5_PayLog ON dbo.Table4_PaymentRequest.PayReqNo = dbo.Table5_PayLog.PayReqNo CROSS JOIN
                      dbo.View_MaxExchangeRate
WHERE     (dbo.Table5_PayLog.PayReqNo IS NULL)

If @ProjectID <> 0 and @SupplierID = N'0'
SELECT     SUM(CASE WHEN dbo.Table2_PONo.PO_Currency = N'Euro' THEN CONVERT(decimal(12, 2), 
                      CASE WHEN dbo.Table2_PONo.PO_Currency = N'Rub' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE
                       dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      / dbo.View_MaxExchangeRate.RubbleEuro ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Dollar' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free
                       = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      * dbo.View_MaxExchangeRate.RubbleDollar / dbo.View_MaxExchangeRate.RubbleEuro ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Euro' THEN
                       (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent)
                       / 100) END) END) END) END) ELSE 0 END) AS EuroWithVAT, SUM(CASE WHEN dbo.Table2_PONo.PO_Currency = N'Dollar' THEN CONVERT(decimal(12,
                       2), 
                      CASE WHEN dbo.Table2_PONo.PO_Currency = N'Rub' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE
                       dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      / dbo.View_MaxExchangeRate.RubbleDollar ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Dollar' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free
                       = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Euro' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue
                       ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      * dbo.View_MaxExchangeRate.RubbleEuro / dbo.View_MaxExchangeRate.RubbleDollar END) END) END) ELSE 0 END) AS DollarWithVAT, 
                      SUM(CASE WHEN dbo.Table2_PONo.PO_Currency = N'Rub' THEN CONVERT(decimal(12, 2), 
                      CASE WHEN dbo.Table2_PONo.PO_Currency = N'Rub' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE
                       dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Dollar' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue
                       ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      * dbo.View_MaxExchangeRate.RubbleDollar ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Euro' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free
                       = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      * dbo.View_MaxExchangeRate.RubbleEuro END) END) END) ELSE 0 END) AS RubleWithVAT
FROM         dbo.Table1_Project INNER JOIN
                      dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN
                      dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN
                      dbo.Table4_PaymentRequest ON dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID INNER JOIN
                      dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID INNER JOIN
                      dbo.Table7_CostCode ON dbo.Table2_PONo.CostCode = dbo.Table7_CostCode.CostCode LEFT OUTER JOIN
                      dbo.View_POsBannedInPending ON dbo.Table2_PONo.PO_No = dbo.View_POsBannedInPending.PO_No LEFT OUTER JOIN
                      dbo.Table5_PayLog ON dbo.Table4_PaymentRequest.PayReqNo = dbo.Table5_PayLog.PayReqNo CROSS JOIN
                      dbo.View_MaxExchangeRate
WHERE     (dbo.Table5_PayLog.PayReqNo IS NULL) AND (dbo.Table1_Project.ProjectID = @ProjectID)

If @ProjectID = 0 and @SupplierID <> N'0'
SELECT     SUM(CASE WHEN dbo.Table2_PONo.PO_Currency = N'Euro' THEN CONVERT(decimal(12, 2), 
                      CASE WHEN dbo.Table2_PONo.PO_Currency = N'Rub' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE
                       dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      / dbo.View_MaxExchangeRate.RubbleEuro ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Dollar' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free
                       = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      * dbo.View_MaxExchangeRate.RubbleDollar / dbo.View_MaxExchangeRate.RubbleEuro ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Euro' THEN
                       (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent)
                       / 100) END) END) END) END) ELSE 0 END) AS EuroWithVAT, SUM(CASE WHEN dbo.Table2_PONo.PO_Currency = N'Dollar' THEN CONVERT(decimal(12,
                       2), 
                      CASE WHEN dbo.Table2_PONo.PO_Currency = N'Rub' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE
                       dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      / dbo.View_MaxExchangeRate.RubbleDollar ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Dollar' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free
                       = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Euro' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue
                       ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      * dbo.View_MaxExchangeRate.RubbleEuro / dbo.View_MaxExchangeRate.RubbleDollar END) END) END) ELSE 0 END) AS DollarWithVAT, 
                      SUM(CASE WHEN dbo.Table2_PONo.PO_Currency = N'Rub' THEN CONVERT(decimal(12, 2), 
                      CASE WHEN dbo.Table2_PONo.PO_Currency = N'Rub' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE
                       dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Dollar' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue
                       ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      * dbo.View_MaxExchangeRate.RubbleDollar ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Euro' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free
                       = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      * dbo.View_MaxExchangeRate.RubbleEuro END) END) END) ELSE 0 END) AS RubleWithVAT
FROM         dbo.Table1_Project INNER JOIN
                      dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN
                      dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN
                      dbo.Table4_PaymentRequest ON dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID INNER JOIN
                      dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID INNER JOIN
                      dbo.Table7_CostCode ON dbo.Table2_PONo.CostCode = dbo.Table7_CostCode.CostCode LEFT OUTER JOIN
                      dbo.View_POsBannedInPending ON dbo.Table2_PONo.PO_No = dbo.View_POsBannedInPending.PO_No LEFT OUTER JOIN
                      dbo.Table5_PayLog ON dbo.Table4_PaymentRequest.PayReqNo = dbo.Table5_PayLog.PayReqNo CROSS JOIN
                      dbo.View_MaxExchangeRate
WHERE     (dbo.Table5_PayLog.PayReqNo IS NULL) AND (dbo.Table6_Supplier.SupplierID = @SupplierID)





If @ProjectID <> 0 and @SupplierID <> N'0'
SELECT     SUM(CASE WHEN dbo.Table2_PONo.PO_Currency = N'Euro' THEN CONVERT(decimal(12, 2), 
                      CASE WHEN dbo.Table2_PONo.PO_Currency = N'Rub' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE
                       dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      / dbo.View_MaxExchangeRate.RubbleEuro ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Dollar' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free
                       = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      * dbo.View_MaxExchangeRate.RubbleDollar / dbo.View_MaxExchangeRate.RubbleEuro ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Euro' THEN
                       (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent)
                       / 100) END) END) END) END) ELSE 0 END) AS EuroWithVAT, SUM(CASE WHEN dbo.Table2_PONo.PO_Currency = N'Dollar' THEN CONVERT(decimal(12,
                       2), 
                      CASE WHEN dbo.Table2_PONo.PO_Currency = N'Rub' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE
                       dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      / dbo.View_MaxExchangeRate.RubbleDollar ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Dollar' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free
                       = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Euro' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue
                       ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      * dbo.View_MaxExchangeRate.RubbleEuro / dbo.View_MaxExchangeRate.RubbleDollar END) END) END) ELSE 0 END) AS DollarWithVAT, 
                      SUM(CASE WHEN dbo.Table2_PONo.PO_Currency = N'Rub' THEN CONVERT(decimal(12, 2), 
                      CASE WHEN dbo.Table2_PONo.PO_Currency = N'Rub' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE
                       dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Dollar' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue
                       ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      * dbo.View_MaxExchangeRate.RubbleDollar ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Euro' THEN (CASE WHEN dbo.Table6_Supplier.VAT_Free
                       = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) / 100) END) 
                      * dbo.View_MaxExchangeRate.RubbleEuro END) END) END) ELSE 0 END) AS RubleWithVAT
FROM         dbo.Table1_Project INNER JOIN
                      dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN
                      dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN
                      dbo.Table4_PaymentRequest ON dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID INNER JOIN
                      dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID INNER JOIN
                      dbo.Table7_CostCode ON dbo.Table2_PONo.CostCode = dbo.Table7_CostCode.CostCode LEFT OUTER JOIN
                      dbo.View_POsBannedInPending ON dbo.Table2_PONo.PO_No = dbo.View_POsBannedInPending.PO_No LEFT OUTER JOIN
                      dbo.Table5_PayLog ON dbo.Table4_PaymentRequest.PayReqNo = dbo.Table5_PayLog.PayReqNo CROSS JOIN
                      dbo.View_MaxExchangeRate
WHERE     (dbo.Table5_PayLog.PayReqNo IS NULL) AND (dbo.Table1_Project.ProjectID = @ProjectID) AND (dbo.Table6_Supplier.SupplierID = @SupplierID) ">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="" 
                                    Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
                                <asp:ControlParameter ControlID="DropDownListSupplier" DefaultValue="" 
                                    Name="SupplierID" PropertyName="SelectedValue" Type="String" />
                            </SelectParameters>
                    </asp:SqlDataSource>

</asp:Content>


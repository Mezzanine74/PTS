<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" EnableViewState="false" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeFile="POs_To_Check.aspx.vb" Inherits="POs_To_Check" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>podetails</title>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

              <asp:label ID="labeltest" runat="server" Text=""></asp:label>
              <asp:label ID="labelSupplierIDTransfer" runat="server" Visible="false" ></asp:label>
              <asp:label ID="labelAllSuppliersMode" runat="server" Text="False" Visible="false"></asp:label>

<div id="ModalMove" class="modal"> JUST USE MODAL CLASS
    <div class="modal-dialog modal-dialog-center">
        <div class="modal-content modal_inlineBlock">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title"></h4>
            </div>
            <div class="modal-body" style="width:750px;">

                <asp:Label ID="LabelPoOnModal" runat="server" CssClass="label label-success"></asp:Label>
                <asp:Label ID="LabelPayReqNoModal" runat="server" CssClass="hide"></asp:Label>

                 > move into >

              <asp:DropDownList ID="DropDownListProject" runat="server" DataSourceID="SqlDataSourceProject" DataTextField="ProjectName" DataValueField="ProjectID">
              </asp:DropDownList>
              <asp:SqlDataSource ID="SqlDataSourceProject" runat="server" 
                  ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                  SelectCommand="SELECT ProjectID, rtrim(Convert(nvarChar(10),ProjectID)) + N' ' + rtrim(ProjectName) AS ProjectName FROM Table1_Project where projectid = 202 order by projectname asc"></asp:SqlDataSource>

                <asp:Button ID="ButtonMoveAction" runat="server" Text="M O V E" CssClass="btn btn-lg btn-success" OnClick="ButtonMoveAction_Click" />


            </div>
        </div>
    </div>
</div>




    <asp:GridView ID="GridViewMonitor" runat="server" AutoGenerateColumns="False"  PagerSettings-Position="TopAndBottom"   
        DataSourceID="SqlDataSourceMonitor" CssClass="Grid" >
        <Columns>

            <asp:TemplateField HeaderText="" >
                <ItemTemplate>

                    <div class="btn-group">
                    <asp:LinkButton ID="LinkButtonOk" runat="server" OnClientClick="return confirm('Are you sure you want to mark as Good?');" CssClass="btn btn-info" CommandName="good" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PayReqNo")%>'>
                      <i class="ace-icon fa fa-check "></i>
                      Good
                    </asp:LinkButton>

                    <asp:LinkButton ID="LinkButtonMove" runat="server" CssClass="btn btn-danger" CommandName="move" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PayReqNo")%>'>
                      <i class="ace-icon fa fa-space-shuttle "></i>
                      Move
                    </asp:LinkButton>
                    </div>


                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="PO" SortExpression="PO_No" HeaderStyle-Width="120">
                <ItemTemplate>
                    <div><asp:Label ID="LabelProjectName" runat="server" Text='<%# Bind("ProjectName") %>' ForeColor="#6666FF"></asp:Label></div>
                    <div><asp:Label ID="LabelPONo" runat="server" Text='<%# Bind("PO_No")%>' ></asp:Label></div>
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
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommandType="Text" 
        SelectCommand=" SELECT     dbo.Table2_PONo.PO_No, CASE WHEN LEN(RTRIM(dbo.Table3_Invoice.Notes)) = 0 OR
                      dbo.Table3_Invoice.Notes IS NULL THEN RTRIM(dbo.Table2_PONo.Description) ELSE RTRIM(dbo.Table2_PONo.Description) + ' / ' + RTRIM(dbo.Table3_Invoice.Notes)
                       END AS Description, CONVERT(decimal(12, 2), dbo.Table2_PONo.TotalPrice) AS POtotalprice, dbo.Table2_PONo.VATpercent, dbo.Table2_PONo.PO_Currency, 
                      dbo.Table2_PONo.PO_Date, dbo.Table3_Invoice.Invoice_No, dbo.Table3_Invoice.Invoice_Date, CONVERT(decimal(12, 2), dbo.Table3_Invoice.InvoiceValue) 
                      AS Invoice_value, RTRIM(dbo.Table4_PaymentRequest.SiteRecordNo) AS SiteRecordNo, dbo.Table4_PaymentRequest.PayReqDate, 
                      RTRIM(dbo.Table4_PaymentRequest.LinkToInvoice) AS LinkToInvoice, RTRIM(dbo.Table4_PaymentRequest.Urgency) AS Urgency, 
                      RTRIM(dbo.Table4_PaymentRequest.PersonApprove) AS PersonApprove, RTRIM(dbo.Table5_PayLog.FinanceNo) AS FinanceNo, dbo.Table5_PayLog.PaymentDate, 
                      CONVERT(decimal(12, 2), dbo.Table5_PayLog.Amount) AS Payment_amount, RTRIM(dbo.Table5_PayLog.Currency) AS Payment_currency, 
                      RTRIM(CONVERT(nvarchar(10), dbo.Table7_CostCode.CostCode)) + N' - ' + RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCode, 
                      RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName, RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName, CASE WHEN AttachmentExist IS NULL 
                      THEN N'False' ELSE AttachmentExist END AS AttachmentExist, dbo.Table4_PaymentRequest.PayReqNo
                      FROM         dbo.Table5_PayLog RIGHT OUTER JOIN
                      dbo.Table_PaymentRequest_OverHeadCheck RIGHT OUTER JOIN
                      dbo.Table4_PaymentRequest ON dbo.Table_PaymentRequest_OverHeadCheck.PayReqNo = dbo.Table4_PaymentRequest.PayReqNo ON 
                      dbo.Table5_PayLog.PayReqNo = dbo.Table4_PaymentRequest.PayReqNo RIGHT OUTER JOIN
                      dbo.Table1_Project LEFT OUTER JOIN
                      dbo.Table6_Supplier INNER JOIN
                      dbo.Table2_PONo ON dbo.Table6_Supplier.SupplierID = dbo.Table2_PONo.SupplierID INNER JOIN
                      dbo.Table7_CostCode ON dbo.Table2_PONo.CostCode = dbo.Table7_CostCode.CostCode LEFT OUTER JOIN
                      dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID ON 
                      dbo.Table4_PaymentRequest.InvoiceID = dbo.Table3_Invoice.InvoiceID
                      WHERE     (dbo.Table1_Project.ProjectManager = N'patrick') AND (dbo.Table4_PaymentRequest.CreatedBy > GETDATE() - 45) AND 
                      (dbo.Table_PaymentRequest_OverHeadCheck.PayReqNo IS NULL) " >
    </asp:SqlDataSource>
    </asp:Panel>    

</asp:Content>


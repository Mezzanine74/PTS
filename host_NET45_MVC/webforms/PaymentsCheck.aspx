<%@ Page Title="" Language="VB" enableEventValidation ="false"  MasterPageFile="~/site.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeFile="PaymentsCheck.aspx.vb" Inherits="PaymentsCheck" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register src="WebUserControl_MonitoringExcelOutput.ascx" tagname="SeperateControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Monitoring</title>

<script type="text/javascript">
  window.onload = function () {
    var strCook = document.cookie;
    if (strCook.indexOf("!~") != 0) {
      var intS = strCook.indexOf("!~");
      var intE = strCook.indexOf("~!");
      var strPos = strCook.substring(intS + 2, intE);
      document.getElementById("'+divTest4+'").scrollTop = strPos;
    }
  }
  function SetDivPosition() {
    var intY = document.getElementById("'+divTest4+'").scrollTop;
    document.cookie = "yPos=!~" + intY + "~!";
  } 
</script> 


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<input type="hidden" id="scrollPos" name="scrollPos" value="0" runat="server"/>
              
    <asp:Panel ID="PanelITpayment" runat="server" CssClass="hidepanel">
      <rsweb:ReportViewer ID="ReportViewer_" runat="server"  
      ProcessingMode="remote"     ShowCredentialPrompts="False" 
      ShowDocumentMapButton="False" ShowFindControls="False"
      ShowPageNavigationControls="True" ShowParameterPrompts="False" 
      ShowPromptAreaButton="False"
      ShowToolBar="False" ShowZoomControl="False" Visible="false" 
      SizeToReportContent="True" ZoomMode="PageWidth"  AsyncRendering="False">
      </rsweb:ReportViewer>
    </asp:Panel>
              <asp:label ID="labeltest" runat="server" Text=""></asp:label>
              <asp:label ID="labelSupplierIDTransfer" runat="server" Visible="false" ></asp:label>
              <asp:label ID="labelAllSuppliersMode" runat="server" Text="False" Visible="false"></asp:label>
                
    <table >
        <tr>
            <td style="vertical-align: top; width: 300px; ">
                <asp:Label ID="LabelPrj" runat="server" Text="Project Name" CssClass="LabelGeneral"></asp:Label>
                <br />
                <asp:ListBox ID="ListBoxPrj" runat="server" DataSourceID="SqlDataSourcePrj"
                DataTextField="ProjectName" 
                DataValueField="ProjectID" Height="180px" Width="242px" AutoPostBack="True" >
                </asp:ListBox>

                <br />

                <asp:Panel ID="PanelPrj" runat="server" CssClass="hidepanel">
                    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                    SelectCommand="SELECT ProjectID, ProjectName FROM
                    (
                    SELECT 0 AS ProjectID, N'_ALL PROJECTS' AS ProjectName
                    UNION ALL
                    SELECT TOP (100) PERCENT 
                    Table1_Project.ProjectID,
                     Table1_Project.ProjectName
                     FROM Table1_Project INNER JOIN Table_Prj_User_Junction ON Table1_Project.ProjectID = Table_Prj_User_Junction.ProjectID INNER JOIN aspnet_Users ON Table_Prj_User_Junction.UserID = aspnet_Users.UserId 
                     WHERE (aspnet_Users.UserName = @UserName)
                    ) AS DataSource1
                    ORDER BY DataSource1.ProjectName ASC">
                    <SelectParameters>
                    <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" 
                    PropertyName="Text" />
                    </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:TextBox ID="TextBoxUserName" runat="server" Visible="False"></asp:TextBox>
                </asp:Panel>
                <asp:ImageButton ID="ImageButton1" runat="server" Width="20px" ToolTip="Export To Excel" Visible="False"
                    ImageUrl="~/Images/Excel.jpg" /> <span style="color:blue; font-size:10px; display:none;">< Export To Excel</span>
            </td>
        </tr>
    </table>

    <asp:GridView ID="GridViewMonitor" runat="server" AutoGenerateColumns="False"  PagerSettings-Position="TopAndBottom" 
        DataSourceID="SqlDataSourceMonitor" CssClass="Grid" >
        <Columns>
            <asp:TemplateField HeaderText="PO" SortExpression="PO_No" ControlStyle-Width="120" HeaderStyle-Width="120">
                <ItemTemplate>
                    <div><asp:Label ID="LabelProjectName" runat="server" Text='<%# Bind("ProjectName") %>' ForeColor="#6666FF"></asp:Label></div>
                   <div><asp:Label ID="Label1" runat="server" Text='<%# Bind("PO_No") %>'></asp:Label></div> 
                </ItemTemplate>
                <ControlStyle Width="120px" />
                <HeaderStyle Width="120px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Supplier" SortExpression="SupplierName" ControlStyle-Width="100" HeaderStyle-Width="100">
                <ItemTemplate>
                    <asp:Label ID="LabelSupplierName" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
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
                    <asp:ImageButton ID="ImageButtonPdf" runat="server" CommandName="OpenPdf" 
                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LinkToInvoice") %>'
                     />
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

                   <asp:TemplateField SortExpression="Difference" ControlStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
                       <HeaderTemplate>
                           <div>Difference in Ruble Inc. VAT</div>                       
                       </HeaderTemplate>
                       <ItemTemplate>
                           <div><asp:Label ID="LabelDifference" runat="server" DataFormatString="{0:N2}" Text='<%# Bind("Difference", "{0:###,###,###.00}")%>'></asp:Label></div>
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

    <asp:Panel ID="PanelMonitor" runat="server" >
    <asp:SqlDataSource ID="SqlDataSourceMonitor" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand="SELECT     TOP (100) PERCENT dbo.Table2_PONo.PO_No, CASE WHEN LEN(RTRIM(dbo.Table3_Invoice.Notes)) = 0 OR
                      dbo.Table3_Invoice.Notes IS NULL THEN RTRIM(dbo.Table2_PONo.Description) ELSE RTRIM(dbo.Table2_PONo.Description) + ' / ' + RTRIM(dbo.Table3_Invoice.Notes)
                       END AS Description, CONVERT(decimal(12, 2), dbo.Table2_PONo.TotalPrice) AS POtotalprice, dbo.Table2_PONo.VATpercent, dbo.Table2_PONo.PO_Currency, 
                      dbo.Table2_PONo.PO_Date, dbo.Table3_Invoice.Invoice_No, dbo.Table3_Invoice.Invoice_Date, CONVERT(decimal(12, 2), dbo.Table3_Invoice.InvoiceValue) 
                      AS Invoice_value, RTRIM(dbo.Table4_PaymentRequest.SiteRecordNo) AS SiteRecordNo, dbo.Table4_PaymentRequest.PayReqDate, 
                      RTRIM(dbo.Table4_PaymentRequest.LinkToInvoice) AS LinkToInvoice, RTRIM(dbo.Table4_PaymentRequest.Urgency) AS Urgency, 
                      RTRIM(dbo.Table4_PaymentRequest.PersonApprove) AS PersonApprove, RTRIM(dbo.Table5_PayLog.FinanceNo) AS FinanceNo, dbo.Table5_PayLog.PaymentDate, 
                      CONVERT(decimal(12, 2), dbo.Table5_PayLog.Amount) AS Payment_amount, RTRIM(dbo.Table5_PayLog.Currency) AS Payment_currency, 
                      RTRIM(CONVERT(nvarchar(10), dbo.Table7_CostCode.CostCode)) + N' - ' + RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCode, 
                      RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName, RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName, 
                      CASE WHEN Table4_PaymentRequest.AttachmentExist IS NULL THEN N'False' ELSE Table4_PaymentRequest.AttachmentExist END AS AttachmentExist, 
                      dbo.View_Payment.Difference
FROM         dbo.View_Payment INNER JOIN
                      dbo.Table5_PayLog INNER JOIN
                      dbo.Table4_PaymentRequest ON dbo.Table5_PayLog.PayReqNo = dbo.Table4_PaymentRequest.PayReqNo ON 
                      dbo.View_Payment.PayReqNo = dbo.Table5_PayLog.PayReqNo RIGHT OUTER JOIN
                      dbo.Table1_Project INNER JOIN
                      dbo.Table6_Supplier INNER JOIN
                      dbo.Table2_PONo ON dbo.Table6_Supplier.SupplierID = dbo.Table2_PONo.SupplierID INNER JOIN
                      dbo.Table7_CostCode ON dbo.Table2_PONo.CostCode = dbo.Table7_CostCode.CostCode LEFT OUTER JOIN
                      dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID ON 
                      dbo.Table4_PaymentRequest.InvoiceID = dbo.Table3_Invoice.InvoiceID
WHERE     (ABS(dbo.View_Payment.Difference) &gt; 0) AND (dbo.Table1_Project.ProjectID = @ProjectID)
ORDER BY ABS(dbo.View_Payment.Difference) DESC" >
        <SelectParameters>
            <asp:ControlParameter ControlID="ListBoxPrj" DefaultValue="0" Name="ProjectID" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>

   </asp:Panel>    

</asp:Content>


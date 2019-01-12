<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="SearchInvoice.aspx.vb" Inherits="SearchInvoice"  EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Search Invoice</title>

<script type="text/javascript" src="Scripts/jquery-1.4.1.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            initiateCheckAllBinding();
        });

        function initiateCheckAllBinding() {
            var headerCheckBox = $("input[id$='CheckBoxALL']");
            var rowCheckBox = $("#StudentTable input[id*='CheckBoxSupplierType']");

            headerCheckBox.click(function (e) {
                rowCheckBox.attr('checked', headerCheckBox.is(':checked'));
            });

            // To select CheckAll when all Item CheckBox is selected and  
            // to deselect CheckAll when an Item CheckBox is deselected.      
            rowCheckBox.click(function (e) {
                var rowCheckBoxSelected = $("#StudentTable input[id*='CheckBoxSupplierType']:checked");

                if (rowCheckBox.length == rowCheckBoxSelected.length) {
                    headerCheckBox.attr("checked", true);
                }
                else {
                    headerCheckBox.attr("checked", false);
                }
            });
            $("table[id$='StudentDataList'] input[id*='CheckBoxSupplierType']")
        }

    </script> 

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


                  <asp:TextBox ID="TextBoxSearch" runat="server" Width="415px" placeholder="PO Description"></asp:TextBox>

                <asp:TextBox ID="TextBoxInvoiceNo" runat="server" placeholder="Invoice No" Width="100px"  />

                <asp:TextBox ID="TextBoxSiteRecordNo" runat="server" placeholder="PayReqNo" Width="100px"  />

                <asp:TextBox ID="TextBoxFinanceNo" runat="server" placeholder="Finance No" Width="100px"  />

                <asp:Button ID="ButtonSelectCategory" runat="server" Text="Categories" CssClass="btn btn-mini btn-default"/>

                <asp:Button ID="ButtonSelectCostCode" runat="server" Text="CostCode" CssClass="btn btn-mini btn-default" />

                <asp:Button ID="ButtonSearch" runat="server" Text="Search" CssClass="btn btn-mini btn-danger"/>

                <asp:Button ID="ButtonExportExcel" runat="server" Text="Excel" CssClass="btn btn-mini btn-success" OnClick="ButtonExportExcel_Click"/>


<asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
 TargetControlID="ButtonSelectCategory"
 PopupControlID="panEdit"
 BackgroundCssClass="modalBackground"
 CancelControlID="btnCancel"
 PopupDragHandleControlID="panEdit" >
</asp:ModalPopupExtender>

<asp:Panel ID="panEdit" runat="server" Style="display:none; padding:10px;" >

                    <div style="text-align: right">
                                     <asp:Button ID="btnCancel" runat="server" Text="X" 
                                        Font-Bold="True" Font-Size="16px" ForeColor="White" BackColor="#FF0066" 
                                        OnClientClick="changeClass" />
                    </div>
                    <div style="padding:10px; border-style:solid; border-color:black; border-width:4px; background-color:white;">
                        <asp:DataList ID="DataListSupplierType" runat="server" CssClass="LabelGeneral"  BackColor="White" 
                          RepeatDirection="Vertical"  DataSourceID="SqlDataSourceSupplierType"
                          RepeatColumns="3" DataKeyField="SupplierTypeId" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px">
                          <ItemTemplate>
                            <asp:Label ID="LabelSubTittle" runat="server" ></asp:Label>
                            <div>
                            <asp:CheckBox ID="CheckBoxSupplierType" runat="server" />
                            <asp:Label ID="LabelSupplierType" runat="server" Text='<%# Bind("SupplierType") %>'  ></asp:Label>
                            <asp:HyperLink ID="HyperLinkSupplierByType" runat="server" Target="_blank" 
                            onmouseover="this.style.cursor='hand'" ForeColor="#6666FF" Font-Underline="True" Font-Size="10px"></asp:HyperLink>
                            </div>
                          </ItemTemplate>
                          <SeparatorStyle BorderColor="#003366" BorderStyle="Solid" BorderWidth="2px" />
                        </asp:DataList>
                        <asp:SqlDataSource ID="SqlDataSourceSupplierType" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                          SelectCommand="SELECT  dbo.Table_SupplierType.SupplierTypeId, dbo.Table_SupplierType.SupplierType, dbo.Table_SupplierType.SupplierDiscipline, 
                          COUNT(dbo.Table_SupplierType_Junction.SupplierTypeId) AS CountOfSupplier
    FROM         dbo.Table_SupplierType LEFT OUTER JOIN
                          dbo.Table_SupplierType_Junction ON dbo.Table_SupplierType.SupplierTypeId = dbo.Table_SupplierType_Junction.SupplierTypeId
    GROUP BY dbo.Table_SupplierType.SupplierTypeId, dbo.Table_SupplierType.SupplierType, dbo.Table_SupplierType.SupplierDiscipline
    ORDER BY dbo.Table_SupplierType.SupplierDiscipline">
                        </asp:SqlDataSource>
                    </div>

</asp:Panel>

<asp:ModalPopupExtender ID="ModalPopupExtenderCostCode" runat="server"
 TargetControlID="ButtonSelectCostCode"
 PopupControlID="panEditCostCode"
 BackgroundCssClass="modalBackground"
 CancelControlID="btnCancelCostCode"
 PopupDragHandleControlID="panEditCostCode" >
</asp:ModalPopupExtender>

<asp:Panel ID="panEditCostCode" runat="server" style="display:none;">

                    <div style="text-align: right">
                                     <asp:Button ID="btnCancelCostCode" runat="server" Text="X" 
                                        Font-Bold="True" Font-Size="16px" ForeColor="White" BackColor="#FF0066" 
                                        OnClientClick="changeClass" />
                    </div>

                    <div style="height:600px;overflow:auto; padding:10px; border-style:solid; border-color:black; border-width:4px; background-color:white;">
                        <asp:DataList ID="DataListCostCode" runat="server" BackColor="White"  CssClass="LabelGeneral"
                          RepeatDirection="Vertical"  DataSourceID="SqlDataSourceCostCode"
                          RepeatColumns="4" DataKeyField="CostCode" BorderColor="#CCCCCC" 
                          BorderStyle="Solid" BorderWidth="1px" >
                          <ItemTemplate >
                          <asp:Label ID="LabelCostDivisionDescription" runat="server" ></asp:Label>
                          <div style="width: 250px; text-align: left;">
                          <table >
                           <tr>
                            <td>
                              <asp:CheckBox ID="CheckBoxCostCode" runat="server" />
                            </td>
                            <td>
                            <asp:Label ID="LabelCostCode" runat="server" Text='<%# Bind("CodeDescription") %>'  ></asp:Label>
                            </td>
                           </tr>
                          </table>
                          </div>
                          </ItemTemplate>
                          <SeparatorStyle BorderColor="#003366" BorderStyle="Solid" BorderWidth="2px" />
                        </asp:DataList>
                        <asp:SqlDataSource ID="SqlDataSourceCostCode" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                          SelectCommand="SELECT CostCode, CodeDescription, CostDivisionDescription, CostVidisionID FROM(
SELECT     TOP (100) PERCENT RTRIM(dbo.Table7_CostCode.CostCode) AS CostCode, RTRIM(dbo.Table7_CostCode.CostCode)+N' '+RTRIM(dbo.Table7_CostCode.CodeDescription) AS CodeDescription, 
                      CASE WHEN RTRIM(dbo.Table7_CostDivision.CostDivisionDescription) IS NULL 
                      THEN N'-' ELSE RTRIM(dbo.Table7_CostDivision.CostDivisionDescription) END AS CostDivisionDescription, 
                      dbo.Table7_CostDivision.CostVidisionID
FROM         dbo.Table7_CostCode INNER JOIN
                      dbo.Table7_CostDivision ON dbo.Table7_CostCode.CostVidisionID = dbo.Table7_CostDivision.CostVidisionID
                      WHERE     (dbo.Table7_CostCode.Type <> N'DataCenter')
                      
UNION ALL

SELECT     TOP (100) PERCENT RTRIM(dbo.Table7_CostCode.CostCode) AS CostCode, RTRIM(dbo.Table7_CostCode.CostCode)+N' '+RTRIM(dbo.Table7_CostCode.CodeDescription) AS CodeDescription, 
                      CASE WHEN RTRIM(dbo.Table7_CostDivision.CostDivisionDescription) IS NULL 
                      THEN N'-' ELSE RTRIM(dbo.Table7_CostDivision.CostDivisionDescription) END AS CostDivisionDescription, N'9999' AS CostVidisionID
FROM         dbo.Table7_CostCode LEFT OUTER JOIN
                      dbo.Table7_CostDivision ON dbo.Table7_CostCode.CostVidisionID = dbo.Table7_CostDivision.CostVidisionID
WHERE     (dbo.Table7_CostDivision.CostVidisionID IS NULL) AND (dbo.Table7_CostCode.Type <> N'DataCenter')
) AS DataSource1
ORDER BY CostVidisionID, CodeDescription">
                        </asp:SqlDataSource>
                    </div>

</asp:Panel>

      <asp:GridView ID="GridViewMonitor" runat="server" AllowPaging="True" 
      PageSize="100" PagerSettings-Position="TopAndBottom"
            AutoGenerateColumns="False" CssClass="Grid" 
      DataSourceID="SqlDataSourceMonitor" EnableModelValidation="True" 
        AllowSorting="True" >
            <EmptyDataTemplate>
                                          <div style="color: #FF0000; font-style: italic; font-size: large; font-weight: normal; width: 800px; text-align: center; ">No Result Found...</div>
            </EmptyDataTemplate>

            <Columns>
                <asp:TemplateField ControlStyle-Width="100" HeaderStyle-Width="100" 
                    HeaderText="ProjectName" SortExpression="ProjectName">
                    <ItemTemplate>
                        <asp:Label ID="LabelProjectName" runat="server" 
                            Text='<%# Bind("ProjectName") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="100px" />
                    <HeaderStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField 
                    HeaderText="PO_No" SortExpression="PO_No">
                    <ItemTemplate>
                        <asp:Label ID="LabelPO_No" runat="server" 
                            Text='<%# Bind("PO_No")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="100" HeaderStyle-Width="100" 
                    HeaderText="SupplierName" SortExpression="SupplierName">
                    <ItemTemplate>
                        <asp:Label ID="LabelSupplierName" runat="server" 
                            Text='<%# Bind("SupplierName") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="100px" />
                    <HeaderStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="180" HeaderStyle-Width="180" 
                    HeaderText="Description" SortExpression="Description">
                    <ItemTemplate>
                        <asp:Label ID="LabelDescription" runat="server" 
                            Text='<%# Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="180px" />
                    <HeaderStyle Width="180px" />
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="80" HeaderStyle-Width="80" 
                    HeaderText="Inv.Value Exc.VAT" SortExpression="Invoice_value">
                    <ItemTemplate>
                        <asp:Label ID="LabelInvoiceValue" runat="server" 
                            Text='<%# Bind("Invoice_value","{0:###,###,###.00}") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <HeaderStyle Width="80px" />
                </asp:TemplateField>

                <asp:TemplateField >
                    <ItemTemplate>
		    <asp:Panel ID="PanelCurrency" runat="server" CssClass ="hidepanel" >
                      <asp:Label ID="LabelCurrency" runat="server" Text='<%# Bind("PO_Currency") %>'></asp:Label>
		    </asp:Panel>
                    <asp:Image ID="ImageCurrency" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="100" HeaderStyle-Width="100" 
                    HeaderText="CostCode" SortExpression="CostCode">
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("CostCode") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="100px" />
                    <HeaderStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="80" HeaderStyle-Width="80" 
                    ItemStyle-HorizontalAlign="Right" SortExpression="Invoice_No">
                    <HeaderTemplate>
                        <div style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #FFFFFF">
                            Invoice No</div>
                        <div>
                            Invoice Date</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0; color: #0000FF;">
                            <asp:Label ID="Label11invnoo" runat="server" Text='<%# Bind("Invoice_No") %>'></asp:Label>
                        </div>
                        <div>
                            <asp:Label ID="Label12invdate" runat="server" 
                                Text='<%# Bind("Invoice_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
            <asp:TemplateField HeaderText="Site Rec" SortExpression="SiteRecordNo" ControlStyle-Width="60" HeaderStyle-Width="60">
                <ItemTemplate>
                    <asp:Label ID="LabelSiteRecordNo" runat="server" Text='<%# Bind("SiteRecordNo") %>'></asp:Label>
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
                <asp:TemplateField HeaderText="PDF" ItemStyle-HorizontalAlign="Center" 
                    SortExpression="LinkToInvoice">
                    <ItemTemplate>
                        <asp:Label ID="LabelLinkInvoice" runat="server" CssClass="hidepanel" 
                            Text='<%# Bind("LinkToInvoice") %>'></asp:Label>
                        <asp:ImageButton ID="ImageButtonPdf" runat="server" 
                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                            CommandName="OpenPdf" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            <asp:TemplateField HeaderText="Finc No" SortExpression="FinanceNo" ControlStyle-Width="40" HeaderStyle-Width="40">
                <ItemTemplate>
                    <asp:Label ID="LabelFinanceNo" runat="server" Text='<%# Bind("FinanceNo") %>'></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="40px" />
                <HeaderStyle Width="40px" />
            </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="80" HeaderStyle-Width="80" 
                    ItemStyle-HorizontalAlign="Right" SortExpression="POtotalprice">
                    <HeaderTemplate>
                        <div style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #FFFFFF">
                            PaymentDate</div>
                        <div style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #FFFFFF">
                            PaymentValue Inc.VAT</div>
                        <div>
                            Payment Curn.</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0">
                            <asp:Label ID="Label18" runat="server" 
                                Text='<%# Bind("PaymentDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                        </div>
                        <div style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0">
                            <asp:Label ID="Label19" runat="server" 
                                Text='<%# Bind("Payment_amount","{0:###,###,###.00}") %>'></asp:Label>
                        </div>
                        <div>
                            <asp:Label ID="Label20" runat="server" Text='<%# Bind("Payment_currency") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
            </Columns>
            <PagerSettings Position="TopAndBottom" />
            <RowStyle CssClass="GridItemNakladnaya" />
            <HeaderStyle CssClass="GridHeader" />
            <pagerstyle  horizontalalign="Center" CssClass="pager2" />
        </asp:GridView>

    <asp:Panel ID="PanelMonitor" runat="server" >
    <asp:SqlDataSource ID="SqlDataSourceMonitor" runat="server"  
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" >
    </asp:SqlDataSource>
    </asp:Panel>    

    <div class="hide">
    <asp:GridView ID="gvFiles" runat="server">
    </asp:GridView>
    </div>

</asp:Content>



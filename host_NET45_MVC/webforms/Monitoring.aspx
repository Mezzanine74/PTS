<%@ Page Title="" Language="VB" EnableEventValidation="false" MasterPageFile="~/site.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeFile="Monitoring.aspx.vb" Inherits="Monitoring" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Src="WebUserControl_MonitoringExcelOutput.ascx" TagName="SeperateControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Monitoring</title>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <!-- Section Gridview modal for Result -->

    <div id="ModalGridResult" class="modal">
        <div class="modal-dialog modal-dialog-center">
            <div class="modal-content modal_inlineBlock">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <%--Gridview for Modal Supplier--%>

                    <asp:GridView ID="GridViewSupplierModal" runat="server" AutoGenerateColumns="False" ShowHeader="True"
                        DataKeyNames="SupplierID" GridLines="None"
                        DataSourceID="SqlDataSourceSupplierModal" CssClass="table table-nonfluid">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="300" HeaderStyle-Width="300" HeaderStyle-HorizontalAlign="Left" HeaderText="Supplier">

                                <ItemTemplate>
                                    <asp:Label ID="LabelSupplier" runat="server" Text='<%# Bind("SupplierName") %>' CssClass="badge btn-info TxtAlgLeft"></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="300px" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="RublePendingWithVAT" ReadOnly="True" DataFormatString="{0:N0}" HeaderText="Ruble Pending With VAT"
                                ItemStyle-HorizontalAlign="Right" ControlStyle-Width="80" HeaderStyle-Width="80" />

                            <asp:BoundField DataField="RublePaidWithVAT" ReadOnly="True" DataFormatString="{0:N0}" HeaderText="Ruble Paid With VAT"
                                ItemStyle-HorizontalAlign="Right" ControlStyle-Width="80" HeaderStyle-Width="80" />

                            <asp:BoundField DataField="PoTotalRubleWithVAT" ReadOnly="True" DataFormatString="{0:N0}" HeaderText="PO Total Ruble With VAT"
                                ItemStyle-HorizontalAlign="Right" ControlStyle-Width="80" HeaderStyle-Width="80" />

                            <asp:BoundField DataField="BalanceRubleWithVAT" ReadOnly="True" DataFormatString="{0:N0}" HeaderText="Balance Ruble With VAT"
                                ItemStyle-HorizontalAlign="Right" ControlStyle-Width="80" HeaderStyle-Width="80" />

                            <asp:BoundField DataField="DocValuePTSRubleWithVAT" ReadOnly="True" DataFormatString="{0:N0}" HeaderText="Collected Doc PTS With VAT"
                                ItemStyle-HorizontalAlign="Right" ControlStyle-Width="80" HeaderStyle-Width="80" />

                            <asp:BoundField DataField="DocValue1SRubleWithVAT" ReadOnly="True" DataFormatString="{0:N0}" HeaderText="Collected Doc 1S With VAT"
                                ItemStyle-HorizontalAlign="Right" ControlStyle-Width="80" HeaderStyle-Width="80" />

                            <asp:TemplateField HeaderText="Collected Doc PTS With VAT" ControlStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperlinkDocPTSValue" runat="server" Target="_blank"><%# Eval("DocValuePTSRubleWithVAT", "{0:N2}")%></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Collected Doc 1S With VAT" ControlStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperlinkDoc1SValue" runat="server" Target="_blank"><%# Eval("DocValue1SRubleWithVAT", "{0:N2}")%></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <HeaderStyle CssClass="headergridnew" />
                    </asp:GridView>

                    <asp:SqlDataSource ID="SqlDataSourceSupplierModal" runat="server"
                        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
                        SelectCommand="SP_GetMonitoringSupplierForModal" SelectCommandType="StoredProcedure" OnInit="SqlDataSourceSupplierModal_Init">
                        <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownListProject" DefaultValue="0" Type="Int16" Name="ProjectID" />
                                <asp:Parameter Name="UserName" Type="String" />
                                <asp:ControlParameter Name="SupplierID" Type="String" ControlID="labelSupplierIDTransfer" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                    <%--END OF Gridview for Modal Supplier--%>

                    <asp:LinkButton ID="LinkButtonExcelOnModal" runat="server" Text="Export To Excel"
                      OnClick="LinkButtonExcelOnModal_Click"
                    ToolTip="Export To Excel" CssClass="label label-sm label-primary arrowed arrowed-right" />

                </div>

                <div class="modal-body" style="width: 100%; margin: 10px;">

                    <div class="modal-adjust-width-height-for-mobile">

                        <asp:GridView ID="GridViewMonitor" runat="server" AutoGenerateColumns="False" PagerSettings-Position="TopAndBottom"
                            DataSourceID="SqlDataSourceMonitor" CssClass="Grid" GridLines="None" AllowPaging="True" PageSize="30">
                            <Columns>
                                <asp:TemplateField HeaderText="PO" SortExpression="PO_No" ControlStyle-Width="120" HeaderStyle-Width="120">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelProjectName" runat="server" Text='<%# Bind("ProjectName") %>' ForeColor="#6666FF"></asp:Label>
                                        <br />
                                        <asp:HyperLink ID="HypPo" runat="server" Target="_blank" NavigateUrl='<%# "~/po.aspx?po=" & Eval("PO_No")%>' Text='<%# Eval("PO_No")%>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ControlStyle Width="120px" />
                                    <HeaderStyle Width="120px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Supplier" SortExpression="SupplierName" ControlStyle-Width="100" HeaderStyle-Width="100">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelSupplierName" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="LabelSupplierID" runat="server" Text='<%# Bind("SupplierID")%>'></asp:Label>
                                        <br />
                                        <asp:HyperLink ID="HypSupplierDetails" runat="server" Target="_blank" NavigateUrl='<%# "~/SupplierDetails.aspx?SupplierID=" & Eval("SupplierID")%>' Text="Details"></asp:HyperLink>
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
                                        <div class="PHeaderSepr">Po Total Value With VAT</div>
                                        <div class="PHeaderSepr">Invoice Value With VAT</div>
                                        <div>Currency</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="PHeaderSepr">
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("POtotalprice","{0:###,###,###.00}") %>'></asp:Label>
                                        </div>
                                        <div class="PHeaderSepr">
                                            <asp:Label ID="Label11" runat="server" Text='<%# Bind("Invoice_value","{0:###,###,###.00}") %>'></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("PO_Currency") %>'></asp:Label>
                                        </div>
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
                                        <div class="PHeaderSepr">Invoice No</div>
                                        <div class="PHeaderSepr">Invoice Date</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="PHeaderSepr">
                                            <asp:Label ID="Label11invnoo" runat="server" Text='<%# Bind("Invoice_No") %>'></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="Label12invdate" runat="server" Text='<%# Bind("Invoice_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </div>
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
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LinkToInvoice") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Urgency" SortExpression="Urgency" ControlStyle-Width="26" HeaderStyle-Width="26" HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide">
                                    <ItemTemplate>
                                        <asp:Label ID="Label15" runat="server" Text='<%# Bind("Urgency") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="26px" />
                                    <HeaderStyle Width="26px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approve" SortExpression="PersonApprove" ControlStyle-Width="26" HeaderStyle-Width="26" HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide">
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
                                        <div class="PHeaderSepr">PaymentDate</div>
                                        <div class="PHeaderSepr">PaymentValue With VAT</div>
                                        <div>Payment Curn.</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0">
                                            <asp:Label ID="Label18" runat="server" Text='<%# Bind("PaymentDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </div>
                                        <div style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0">
                                            <asp:Label ID="Label19" runat="server" Text='<%# Bind("Payment_amount","{0:###,###,###.00}") %>'></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="Label20" runat="server" Text='<%# Bind("Payment_currency") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>


                                    <ControlStyle Width="80px"></ControlStyle>

                                    <HeaderStyle Width="80px"></HeaderStyle>

                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <PagerSettings Position="TopAndBottom" />
                            <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
                            <RowStyle CssClass="GridItemNakladnaya" />
                            <HeaderStyle BackColor="LightBlue" />

                        </asp:GridView>

                        <asp:SqlDataSource ID="SqlDataSourceMonitor" runat="server"
                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
                            SelectCommand="SP_GetMonitoring" SelectCommandType="StoredProcedure" OnInit="SqlDataSourceMonitor_Init">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownListProject" DefaultValue="0" Type="Int16" Name="ProjectID" />
                                <asp:Parameter Name="UserName" Type="String" />
                                <asp:ControlParameter Name="SupplierID" Type="String" ControlID="labelSupplierIDTransfer" />
                            </SelectParameters>
                        </asp:SqlDataSource>

                    </div>

                </div>
            </div>
        </div>
    </div>

    <!-- Project Section -->
    <asp:DropDownList ID="DropDownListProject" runat="server" DataSourceID="SqlDataSourcePrj" OnSelectedIndexChanged="DropDownListProject_SelectedIndexChanged" OnDataBound="DropDownListProject_DataBound"
        DataTextField="ProjectName" DataValueField="ProjectID" AutoPostBack="True">
    </asp:DropDownList>

    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server"
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
        SelectCommand="SELECT ProjectID, ProjectName FROM
                    (
                    SELECT -1 AS ProjectID, N'__SELECT PROJECT' AS ProjectName
                    UNION ALL
                    SELECT 0 AS ProjectID, N'_ALL PROJECTS' AS ProjectName
                    UNION ALL
                    SELECT TOP (100) PERCENT 
                    Table1_Project.ProjectID,
                     Table1_Project.ProjectName
                     FROM Table1_Project INNER JOIN Table_Prj_User_Junction ON Table1_Project.ProjectID = Table_Prj_User_Junction.ProjectID INNER JOIN aspnet_Users ON Table_Prj_User_Junction.UserID = aspnet_Users.UserId 
                     WHERE (aspnet_Users.UserName = @UserName)  and Table1_Project.CurrentStatus = 1
                    ) AS DataSource1
                    ORDER BY DataSource1.ProjectName ASC">
        <SelectParameters>
            <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName"
                PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:TextBox ID="TextBoxUserName" runat="server" Visible="False"></asp:TextBox>

    <asp:LinkButton ID="LinkButtonItPayments" runat="server" CssClass="btn btn-mini btn-default">IT payments</asp:LinkButton>

    <asp:LinkButton ID="ImageButtonExcel" runat="server" Text="Export To Excel"
        OnClick="ImageButtonExcel_Click"
        ToolTip="Export To Excel" CssClass="label label-sm label-primary arrowed arrowed-right" />

    <!-- Gridview Section -->
    <hr />

    <asp:GridView ID="GridViewSupplier" runat="server" AutoGenerateColumns="False" ShowHeader="True"
        DataKeyNames="SupplierID" PagerSettings-Position="TopAndBottom" GridLines="None"
        DataSourceID="SqlDataSourceMonitoringSupplier" CssClass="table table-nonfluid table-hover"
        AllowPaging="True" PageSize="200">
        <Columns>
            <asp:TemplateField ItemStyle-Width="300" HeaderStyle-Width="300" HeaderStyle-HorizontalAlign="Left" HeaderText="Supplier">
                <HeaderTemplate>
                    <asp:TextBox ID="TextBoxFindSupplier" runat="server" placeholder="Search Supplier Name"  AutoPostBack="true" OnTextChanged="TextBoxFindSupplier_TextChanged"></asp:TextBox>

                    <div id="AutoCompleteSupplier" runat="server" class="zindexMax">
                    </div>

                    <asp:AutoCompleteExtender ID="AutoCompleteExtenderSupplier" runat="server" 
                        CompletionInterval="0" CompletionListElementID="AutoCompleteSupplier"
                        CompletionSetCount="12" MinimumPrefixLength="0" UseContextKey="true"
                        OnClientShown="SetAutoCompleteWidthSupplier" ServiceMethod="SearchSupplierOnMonitoring"
                        ServicePath="AutoComplete.asmx" TargetControlID="TextBoxFindSupplier">
                    </asp:AutoCompleteExtender>

                </HeaderTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonSupplier" runat="server" Text='<%# Bind("SupplierName") %>' CssClass="badge btn-info TxtAlgLeft"
                        CommandName="FindSupplierName" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SupplierID") %>'></asp:LinkButton>
                </ItemTemplate>
                <ControlStyle Width="300px" />
            </asp:TemplateField>

            <asp:BoundField DataField="RublePendingWithVAT" ReadOnly="True" DataFormatString="{0:N0}" HeaderText="Ruble Pending With VAT"
                ItemStyle-HorizontalAlign="Right" ControlStyle-Width="80" HeaderStyle-Width="80" />

            <asp:BoundField DataField="RublePaidWithVAT" ReadOnly="True" DataFormatString="{0:N0}" HeaderText="Ruble Paid With VAT"
                ItemStyle-HorizontalAlign="Right" ControlStyle-Width="80" HeaderStyle-Width="80" />

            <asp:BoundField DataField="PoTotalRubleWithVAT" ReadOnly="True" DataFormatString="{0:N0}" HeaderText="PO Total Ruble With VAT"
                ItemStyle-HorizontalAlign="Right" ControlStyle-Width="80" HeaderStyle-Width="80" />

            <asp:BoundField DataField="BalanceRubleWithVAT" ReadOnly="True" DataFormatString="{0:N0}" HeaderText="Balance Ruble With VAT"
                ItemStyle-HorizontalAlign="Right" ControlStyle-Width="80" HeaderStyle-Width="80" />
           
            <asp:TemplateField HeaderText="Collected Doc PTS With VAT" ControlStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperlinkDocPTSValue" runat="server" Target="_blank"><%# Eval("DocValuePTSRubleWithVAT", "{0:N2}")%></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Collected Doc 1S With VAT" ControlStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperlinkDoc1SValue" runat="server" Target="_blank"><%# Eval("DocValue1SRubleWithVAT", "{0:N2}")%></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        <PagerSettings Position="TopAndBottom" />
        <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
        <HeaderStyle CssClass="headergridnew" />
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSourceMonitoringSupplier" runat="server"
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
        SelectCommand="SP_GetMonitoringSupplier" SelectCommandType="StoredProcedure" OnInit="SqlDataSourceMonitoringSupplier_Init">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownListProject" DefaultValue="0" Type="Int16" Name="ProjectID" />
            <asp:Parameter Name="UserName" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:Panel ID="PanelITpayment" runat="server" CssClass="hidepanel">
        <rsweb:ReportViewer ID="ReportViewer_" runat="server"
            ProcessingMode="remote" ShowCredentialPrompts="False"
            ShowDocumentMapButton="False" ShowFindControls="False"
            ShowPageNavigationControls="True" ShowParameterPrompts="False"
            ShowPromptAreaButton="False"
            ShowToolBar="False" ShowZoomControl="False" Visible="false"
            SizeToReportContent="True" ZoomMode="PageWidth" AsyncRendering="False">
        </rsweb:ReportViewer>
    </asp:Panel>
    <asp:Label ID="labeltest" runat="server" Text=""></asp:Label>
    <asp:Label ID="labelSupplierIDTransfer" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="labelAllSuppliersMode" runat="server" Text="False" Visible="false"></asp:Label>


    <uc1:SeperateControl ID="WebUserControl_MonitoringExcelOutput" runat="server" />

</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server" ID="ContentScripts">

<%--    <script type="text/javascript">
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
        
    </script>--%>

    <script type="text/javascript">

        function SetAutoCompleteWidthSupplier() {

            $get("MainContent_GridViewSupplier_AutoCompleteSupplier").style.minWidth = '400px';
        }

    </script>

</asp:Content>

<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="FollowUpReport.aspx.vb" Inherits="_Nakl_FollowUpReportDeliveryFinanceFasterTREV" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>FollowUp Report</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:Label ID="LabelCriteriaCostCodeForExcel" runat="server" Visible="false" ></asp:Label>
    <asp:Label ID="LabelProjectIDforExcel" runat="server" Visible="false" ></asp:Label>


            <asp:LinkButton ID="ImageButtonOld" runat="server" Text="Old Format XLS" Visible="false"
                Onclick="ImageButtonOld_Click"
                ToolTip="Export To Excel" CssClass="label label-sm label-primary arrowed arrowed-right"/>

            <asp:LinkButton ID="ImageButton1" runat="server" 
                OnClick="ImageButton1_Click" Text="New Format XLS"
                ToolTip="Export To Excel" CssClass="label label-sm label-success arrowed arrowed-right" />
            
            <asp:Label ID="labelWhichCurrency" runat="server" Visible="false" ></asp:Label>
            
        <asp:DropDownList ID="DropDownListPrj" runat="server" 
            DataSourceID="SqlDataSourcePrj" DataTextField="ProjectName" 
            DataValueField="ProjectID" >
        </asp:DropDownList>

        <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        
            SelectCommand="SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, dbo.Table1_Project.ProjectName, dbo.aspnet_Users.UserName FROM         dbo.Table1_Project INNER JOIN  dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN  dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId WHERE     (dbo.aspnet_Users.UserName = @UserName) AND (dbo.Table1_Project.CurrentStatus=1 ) ORDER BY dbo.Table1_Project.ProjectName">
            <SelectParameters>
                <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" 
                    PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>
    
    
        <asp:DropDownList ID="DropDownListCurrency" runat="server" >
            <asp:ListItem Selected="True">Rub</asp:ListItem>
            <asp:ListItem>Dollar</asp:ListItem>
            <asp:ListItem>Euro</asp:ListItem>
        </asp:DropDownList>
    
        <asp:TextBox ID="TextBoxUserName" runat="server" Visible="False"></asp:TextBox>
    
        <asp:Button ID="ButtonRunReport" runat="server" Text="Run Report" CssClass="btn btn-mini"/>

      <asp:Label ID="LabelProjectName" runat="server" CssClass="label label-default arrowed arrowed-in-right" ></asp:Label>

        <asp:LinkButton ID="ImageButtonExcelSummary" runat="server" Text="Summary Page In XLS"
                OnClick="ImageButtonExcelSummary_Click"
              ToolTip="Export To Excel" CssClass="label label-sm label-purple arrowed arrowed-right"/>
            

      <asp:LinkButton ID="LinkButton1" runat="server" CssClass="label label-grey"
        PostBackUrl="~/webforms/FollowUpReport2.aspx">Switch To Alternative Follow Up Report</asp:LinkButton>

    <asp:Label id="LabelVATstatus" runat="server"></asp:Label>

                 <asp:DropDownList ID="DropDownListFinanceCheck" runat="server" Visible="false"
                  DataSourceID="SqlDataSourceFinanceCheck" DataTextField="CostCode" DataValueField="CostCode"    
                 ></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceFinanceCheck" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>">
                </asp:SqlDataSource>

            <asp:SqlDataSource ID="SqlDataSourceRubleToExcel" runat="server" 
               ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
               SelectCommand="select * from View_FollowupreportTOexcelruble where (ProjectID = @ProjectID) ORDER BY PO_No">
            <SelectParameters>
                <asp:ControlParameter ControlID="LabelProjectIDforExcel" DefaultValue="0" 
                Name="ProjectID" Type="Int32"  />
            </SelectParameters>
           </asp:SqlDataSource>         
      
            <asp:SqlDataSource ID="SqlDataSourceDollarToExcel" runat="server" 
               ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
               SelectCommand="select * from View_FollowupreportTOexceldollar where (ProjectID = @ProjectID)  ORDER BY PO_No">
            <SelectParameters>
                <asp:ControlParameter ControlID="LabelProjectIDforExcel" DefaultValue="0" 
                Name="ProjectID" Type="Int32"  />
            </SelectParameters>
           </asp:SqlDataSource>   
           
            <asp:SqlDataSource ID="SqlDataSourceEuroToExcel" runat="server" 
               ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
               SelectCommand="select * from View_FollowupreportTOexceleuro where (ProjectID = @ProjectID) ORDER BY PO_No">
            <SelectParameters>
                <asp:ControlParameter ControlID="LabelProjectIDforExcel" DefaultValue="0" 
                Name="ProjectID" Type="Int32"  />
            </SelectParameters>
           </asp:SqlDataSource>   
     

<div style="text-align: center;>
    <rsweb:ReportViewer ID="ReportViewerDeliveryReport" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="false" ShowZoomControl="true" Visible="false" 
    SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
    </rsweb:ReportViewer>
 </div>



      <asp:Panel ID="PanelGridHidden" runat="server" CssClass="hidepanel"  >
<%-- GridView for Ruble To Excel --%>
 
        <asp:GridView ID="GridViewRubleToExcel" runat="server" AutoGenerateColumns="False" 
            >
            <Columns>
                <asp:TemplateField HeaderText="ProjectID" SortExpression="ProjectID" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ProjectID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CostCode" SortExpression="CostCode" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("CostCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PO_No" SortExpression="PO_No" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("PO_No") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Invoice_No" SortExpression="Invoice_No" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelInvoice_No" runat="server" Text='<%# Bind("Invoice_No") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Invoice_Date" SortExpression="Invoice_Date" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelInvoice_Date" runat="server" Text='<%# Bind("Invoice_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="InvoiceValue Exc VAT" SortExpression="InvoiceValue" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelInvoiceValue" runat="server" Text='<%# Bind("InvoiceValue") %>'></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                <asp:TemplateField HeaderText="PO_Currency" SortExpression="PO_Currency" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("PO_Currency") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SiteRecordNo" SortExpression="SiteRecordNo" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelSiteRecordNo" runat="server" Text='<%# Bind("SiteRecordNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Activity Code" SortExpression="ActivityCode" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelActivityCode" runat="server" Text='<%# Bind("ActivityCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PaymentDate" SortExpression="PaymentDate" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelPaymentDate" runat="server" Text='<%# Bind("PaymentDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                <asp:TemplateField HeaderText="RubbleDollar" SortExpression="RubbleDollar" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("RubbleDollar") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RubbleEuro" SortExpression="RubbleEuro" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("RubbleEuro") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SupplierName" SortExpression="SupplierName" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" SortExpression="Description" ControlStyle-Width="130" HeaderStyle-Width="130" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RublePendingExcVAT"  SortExpression="RublePendingExcVAT" 
                ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelRublePendingExcVAT" runat="server" Text='<%# Bind("RublePendingExcVAT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RublePaidExcVAT" 
                    SortExpression="RublePaidExcVAT" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelRublePaidExcVAT" runat="server" Text='<%# Bind("RublePaidExcVAT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OrderValueRuble Exc VAT" 
                    SortExpression="OrderValueRuble" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelOrderValueRuble" runat="server" Text='<%# Bind("OrderValueRuble") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="VATpaidRuble" SortExpression="VATpaidRuble" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelVATpaidRuble" runat="server" Text='<%# Bind("VATpaidRuble") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        
<%-- GridView for Dollar To Excel --%>
 
        <asp:GridView ID="GridViewDollarToExcel" runat="server" AutoGenerateColumns="False" 
            >
            <Columns>
                <asp:TemplateField HeaderText="ProjectID" SortExpression="ProjectID" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ProjectID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CostCode" SortExpression="CostCode" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("CostCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PO_No" SortExpression="PO_No" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("PO_No") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Invoice_No" SortExpression="Invoice_No" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelInvoice_No" runat="server" Text='<%# Bind("Invoice_No") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Invoice_Date" SortExpression="Invoice_Date" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelInvoice_Date" runat="server" Text='<%# Bind("Invoice_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="InvoiceValue Exc VAT" SortExpression="InvoiceValue" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelInvoiceValue" runat="server" Text='<%# Bind("InvoiceValue") %>'></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                <asp:TemplateField HeaderText="PO_Currency" SortExpression="PO_Currency" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("PO_Currency") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SiteRecordNo" SortExpression="SiteRecordNo" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelSiteRecordNo" runat="server" Text='<%# Bind("SiteRecordNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Activity Code" SortExpression="ActivityCode" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelActivityCode" runat="server" Text='<%# Bind("ActivityCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PaymentDate" SortExpression="PaymentDate" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelPaymentDate" runat="server" Text='<%# Bind("PaymentDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                <asp:TemplateField HeaderText="RubbleDollar" SortExpression="RubbleDollar" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("RubbleDollar") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RubbleEuro" SortExpression="RubbleEuro" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("RubbleEuro") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SupplierName" SortExpression="SupplierName" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" SortExpression="Description" ControlStyle-Width="130" HeaderStyle-Width="130" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="dollarPendingExcVAT"  SortExpression="dollarPendingExcVAT" 
                ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabeldollarPendingExcVAT" runat="server" Text='<%# Bind("dollarPendingExcVAT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="dollarPaidExcVAT" 
                    SortExpression="dollarPaidExcVAT" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabeldollarPaidExcVAT" runat="server" Text='<%# Bind("dollarPaidExcVAT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OrderValueDollar Exc VAT" 
                    SortExpression="OrderValueDollar" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelOrderValueDollar" runat="server" Text='<%# Bind("OrderValueDollar") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VATpaiddollar" SortExpression="VATpaiddollar" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelVATpaiddollar" runat="server" Text='<%# Bind("VATpaiddollar") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        
<%-- GridView for Dollar To Excel --%>
 
        <asp:GridView ID="GridViewEuroToExcel" runat="server" AutoGenerateColumns="False" 
            >
            <Columns>
                <asp:TemplateField HeaderText="ProjectID" SortExpression="ProjectID" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ProjectID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CostCode" SortExpression="CostCode" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("CostCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PO_No" SortExpression="PO_No" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("PO_No") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Invoice_No" SortExpression="Invoice_No" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelInvoice_No" runat="server" Text='<%# Bind("Invoice_No") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Invoice_Date" SortExpression="Invoice_Date" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelInvoice_Date" runat="server" Text='<%# Bind("Invoice_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="InvoiceValue Exc VAT" SortExpression="InvoiceValue" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelInvoiceValue" runat="server" Text='<%# Bind("InvoiceValue") %>'></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                <asp:TemplateField HeaderText="PO_Currency" SortExpression="PO_Currency" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("PO_Currency") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SiteRecordNo" SortExpression="SiteRecordNo" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelSiteRecordNo" runat="server" Text='<%# Bind("SiteRecordNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Activity Code" SortExpression="ActivityCode" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelActivityCode" runat="server" Text='<%# Bind("ActivityCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PaymentDate" SortExpression="PaymentDate" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelPaymentDate" runat="server" Text='<%# Bind("PaymentDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                <asp:TemplateField HeaderText="Rubbleeuro" SortExpression="RubbleDollar" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("RubbleDollar") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RubbleEuro" SortExpression="RubbleEuro" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("RubbleEuro") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SupplierName" SortExpression="SupplierName" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" SortExpression="Description" ControlStyle-Width="130" HeaderStyle-Width="130" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="euroPendingExcVAT"  SortExpression="euroPendingExcVAT" 
                ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabeleuroPendingExcVAT" runat="server" Text='<%# Bind("euroPendingExcVAT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="euroPaidExcVAT" 
                    SortExpression="euroPaidExcVAT" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabeleuroPaidExcVAT" runat="server" Text='<%# Bind("euroPaidExcVAT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OrderValueEuro Exc VAT" 
                    SortExpression="OrderValueEuro" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelOrderValueEuro" runat="server" Text='<%# Bind("OrderValueEuro") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VATpaideuro" SortExpression="VATpaideuro" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelVATpaideuro" runat="server" Text='<%# Bind("VATpaideuro") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
 </asp:Panel>     
 </asp:Content>


<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="FollowUpReportBackup2.aspx.vb" Inherits="FollowUpReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>FollowUp Report</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

   
    <asp:Label ID="Labelfc" runat="server" ></asp:Label>
    <asp:Label ID="LabelProjectIDforExcel" runat="server" Visible="false" ></asp:Label>

        <asp:ImageButton ID="ImageButton1" runat="server" 
            ImageUrl="~/Images/Excel.jpg"  OnClick="ImageButton1_Click"
            ToolTip="Export To Excel" Width="20px" BorderStyle="Solid" 
            BorderWidth="1px" />
            
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
    
    <asp:TextBox ID="TextBoxUserName" runat="server" Visible="False"></asp:TextBox>
    
    <br />
    
            <asp:SqlDataSource ID="SqlDataSourceRubleToExcel" runat="server" 
               ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
               SelectCommand="select * from View_FollowupreportTOexcelruble where (ProjectID = @ProjectID) ORDER BY CostCode, PO_No">
            <SelectParameters>
                <asp:ControlParameter ControlID="LabelProjectIDforExcel" DefaultValue="0" 
                Name="ProjectID" Type="Int32"  />
            </SelectParameters>
           </asp:SqlDataSource>         
      
            <asp:SqlDataSource ID="SqlDataSourceDollarToExcel" runat="server" 
               ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
               SelectCommand="select * from View_FollowupreportTOexceldollar where (ProjectID = @ProjectID)  ORDER BY CostCode, PO_No">
            <SelectParameters>
                <asp:ControlParameter ControlID="LabelProjectIDforExcel" DefaultValue="0" 
                Name="ProjectID" Type="Int32"  />
            </SelectParameters>
           </asp:SqlDataSource>   
           
            <asp:SqlDataSource ID="SqlDataSourceEuroToExcel" runat="server" 
               ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
               SelectCommand="select * from View_FollowupreportTOexceleuro where (ProjectID = @ProjectID)  ORDER BY CostCode, PO_No">
            <SelectParameters>
                <asp:ControlParameter ControlID="LabelProjectIDforExcel" DefaultValue="0" 
                Name="ProjectID" Type="Int32"  />
            </SelectParameters>
           </asp:SqlDataSource>   
     
            <asp:SqlDataSource ID="SqlDataSourceRegisterToDatabase" runat="server" 
               ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" >
           </asp:SqlDataSource>   

            <asp:DropDownList ID="DropDownListBackUpControl" runat="server"
                DataSourceID="SqlDataSourceBackUpControl" DataTextField="BackUPID" 
                DataValueField="BackUPID" Visible="false"  >
            </asp:DropDownList>

            <asp:SqlDataSource ID="SqlDataSourceBackUpControl" runat="server" 
               ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" >
           </asp:SqlDataSource>

            <asp:DropDownList ID="DropDownListBackUpControlPendingList" runat="server"
                DataSourceID="SqlDataSourceBackUpControlPendingList" DataTextField="BackUPID" 
                DataValueField="BackUPID" Visible="false"  >
            </asp:DropDownList>

            <asp:SqlDataSource ID="SqlDataSourceBackUpControlPendingList" runat="server" 
               ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" >
           </asp:SqlDataSource>


          <asp:SqlDataSource ID="SqlDataSourcePendingExcel" runat="server" 
              ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
              SelectCommand = "SELECT * FROM [View_PendingList] ORDER BY ProjectName ASC" >
                 </asp:SqlDataSource>    


      <asp:Panel ID="PanelGridHidden" runat="server"  CssClass="hidepanel"> 
<%-- GridView for Ruble To Excel --%>
 
        <asp:GridView ID="GridViewRubleToExcel" runat="server" AutoGenerateColumns="False" ShowFooter="True">
            <Columns>
                <asp:TemplateField HeaderText="ProjectID" SortExpression="ProjectID" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ProjectID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CostCode" SortExpression="CostCode" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelCostCode" runat="server" Text='<%# Bind("CostCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PO_No" SortExpression="PO_No" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelPO_No" runat="server" Text='<%# Bind("PO_No") %>'></asp:Label>
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
                <asp:TemplateField HeaderText="InvoiceValue" SortExpression="InvoiceValue" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
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
                    <FooterTemplate>
                        <asp:Label ID="LabelFooterTitle" runat="server" Text="T O T A L" Font-Bold="True"></asp:Label>                    
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RublePendingExcVAT"  SortExpression="RublePendingExcVAT" 
                ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelRublePendingExcVAT" runat="server" Text='<%# Bind("RublePendingExcVAT") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelRublePendingExcVATtotal" runat="server" ></asp:Label>                    
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RublePaidExcVAT" 
                    SortExpression="RublePaidExcVAT" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelRublePaidExcVAT" runat="server" Text='<%# Bind("RublePaidExcVAT") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelRublePaidExcVATtotal" runat="server" ></asp:Label>                    
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OrderValueRuble" 
                    SortExpression="OrderValueRuble" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelOrderValueRuble" runat="server" Text='<%# Bind("OrderValueRuble") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelOrderValueRubletotal" runat="server" ></asp:Label>                    
                    </FooterTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="VATpaidRuble" SortExpression="VATpaidRuble" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelVATpaidRuble" runat="server" Text='<%# Bind("VATpaidRuble") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelVATpaidRubletotal" runat="server" ></asp:Label>                    
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OutstandingVATRuble" 
                    SortExpression="OutstandingVATRuble" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelOutstandingVATRuble" runat="server" 
                            Text='<%# Bind("OutstandingVATRuble") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelOutstandingVATRubletotal" runat="server" ></asp:Label>                    
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="StatusNote" SortExpression="StatusNote" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelStatusNote" runat="server" Text='<%# Bind("StatusNote") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        
<%-- GridView for Dollar To Excel --%>
 
        <asp:GridView ID="GridViewDollarToExcel" runat="server" AutoGenerateColumns="False" ShowFooter="True">
            <Columns>
                <asp:TemplateField HeaderText="ProjectID" SortExpression="ProjectID" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ProjectID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CostCode" SortExpression="CostCode" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelCostCode" runat="server" Text='<%# Bind("CostCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PO_No" SortExpression="PO_No" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelPO_No" runat="server" Text='<%# Bind("PO_No") %>'></asp:Label>
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
                <asp:TemplateField HeaderText="InvoiceValue" SortExpression="InvoiceValue" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
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
                    <FooterTemplate>
                        <asp:Label ID="LabelFooterTitle" runat="server" Text="T O T A L" Font-Bold="True"></asp:Label>                    
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="dollarPendingExcVAT"  SortExpression="dollarPendingExcVAT" 
                ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabeldollarPendingExcVAT" runat="server" Text='<%# Bind("dollarPendingExcVAT") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabeldollarPendingExcVATtotal" runat="server" ></asp:Label>                    
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="dollarPaidExcVAT" 
                    SortExpression="dollarPaidExcVAT" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabeldollarPaidExcVAT" runat="server" Text='<%# Bind("dollarPaidExcVAT") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabeldollarPaidExcVATtotal" runat="server" ></asp:Label>                    
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OrderValueDollar" 
                    SortExpression="OrderValueDollar" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelOrderValueDollar" runat="server" Text='<%# Bind("OrderValueDollar") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelOrderValueDollartotal" runat="server" ></asp:Label>                    
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VATpaiddollar" SortExpression="VATpaiddollar" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelVATpaiddollar" runat="server" Text='<%# Bind("VATpaiddollar") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelVATpaiddollartotal" runat="server" ></asp:Label>                    
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OutstandingVATdollar" 
                    SortExpression="OutstandingVATdollar" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelOutstandingVATdollar" runat="server" 
                            Text='<%# Bind("OutstandingVATdollar") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelOutstandingVATdollartotal" runat="server" ></asp:Label>                    
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="StatusNote" SortExpression="StatusNote" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelStatusNote" runat="server" Text='<%# Bind("StatusNote") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        
<%-- GridView for Dollar To Excel --%>
 
        <asp:GridView ID="GridViewEuroToExcel" runat="server" AutoGenerateColumns="False" ShowFooter="True">
            <Columns>
                <asp:TemplateField HeaderText="ProjectID" SortExpression="ProjectID" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ProjectID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CostCode" SortExpression="CostCode" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelCostCode" runat="server" Text='<%# Bind("CostCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PO_No" SortExpression="PO_No" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelPO_No" runat="server" Text='<%# Bind("PO_No") %>'></asp:Label>
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
                <asp:TemplateField HeaderText="InvoiceValue" SortExpression="InvoiceValue" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
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
                    <FooterTemplate>
                        <asp:Label ID="LabelFooterTitle" runat="server" Text="T O T A L" Font-Bold="True"></asp:Label>                    
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="euroPendingExcVAT"  SortExpression="euroPendingExcVAT" 
                ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabeleuroPendingExcVAT" runat="server" Text='<%# Bind("euroPendingExcVAT") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabeleuroPendingExcVATtotal" runat="server" ></asp:Label>                    
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="euroPaidExcVAT" 
                    SortExpression="euroPaidExcVAT" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabeleuroPaidExcVAT" runat="server" Text='<%# Bind("euroPaidExcVAT") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabeleuroPaidExcVATtotal" runat="server" ></asp:Label>                    
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OrderValueEuro" 
                    SortExpression="OrderValueEuro" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelOrderValueEuro" runat="server" Text='<%# Bind("OrderValueEuro") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelOrderValueEurototal" runat="server" ></asp:Label>                    
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VATpaideuro" SortExpression="VATpaideuro" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelVATpaideuro" runat="server" Text='<%# Bind("VATpaideuro") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelVATpaideurototal" runat="server" ></asp:Label>                    
                    </FooterTemplate>
                </asp:TemplateField>      
                <asp:TemplateField HeaderText="OutstandingVATeuro" 
                    SortExpression="OutstandingVATeuro" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelOutstandingVATeuro" runat="server" 
                            Text='<%# Bind("OutstandingVATeuro") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelOutstandingVATeurototal" runat="server" ></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="StatusNote" SortExpression="StatusNote" ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LabelStatusNote" runat="server" Text='<%# Bind("StatusNote") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <%-- Excel Pending List --%>

   <asp:GridView ID="GridViewPendingListToExcel" runat="server" AutoGenerateColumns="False"  
    DataKeyNames="ProjectID"  
        CssClass="Grid"  AllowSorting="True" ShowFooter="True">
    <Columns>
    
        <asp:TemplateField HeaderText="Approval" ControlStyle-Width="120" HeaderStyle-Width="120">
            <ItemTemplate>
                <asp:Label ID="LabelApproved_" runat="server" Text='<%# Bind("Approved") %>'></asp:Label>
            </ItemTemplate>
            <ControlStyle Width="120px" />
            <HeaderStyle Width="120px" />
        </asp:TemplateField>       
         
        <asp:TemplateField HeaderText="Requested By" ControlStyle-Width="120" HeaderStyle-Width="120">
            <ItemTemplate>
                <asp:Label ID="LabelPersonCreated" runat="server" Text='<%# Bind("PersonCreated") %>'></asp:Label>
            </ItemTemplate>
            <ControlStyle Width="120px" />
            <HeaderStyle Width="120px" />
        </asp:TemplateField>       


        <asp:TemplateField HeaderText="Urg" ControlStyle-Width="80" HeaderStyle-Width="80">
            <ItemTemplate>
                <asp:Label ID="LabelStatus" runat="server" Text='<%# Bind("Urgency") %>'></asp:Label>
            </ItemTemplate>
            <ControlStyle Width="80px" />
            <HeaderStyle Width="80px" />
        </asp:TemplateField>        
        
        <asp:TemplateField HeaderText="By"  ControlStyle-Width="80" HeaderStyle-Width="80">
            <ItemTemplate>
                <asp:Label ID="LabelPersonApproved_" runat="server" Text='<%# Bind("PersonApprove") %>'></asp:Label>
            </ItemTemplate>
            <ControlStyle Width="80px" />
            <HeaderStyle Width="80px" />
        </asp:TemplateField>     

        <asp:TemplateField HeaderText="ProjectName"  ControlStyle-Width="200" HeaderStyle-Width="200">
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Bind("ProjectName") %>'></asp:Label>
            </ItemTemplate>

<ControlStyle Width="200px"></ControlStyle>

<HeaderStyle Width="200px"></HeaderStyle>
        </asp:TemplateField>


        <asp:TemplateField HeaderText="Requsition"  ControlStyle-Width="80" HeaderStyle-Width="80">
            <ItemTemplate>
                <asp:Label ID="LabePayreqDate" runat="server" Text='<%# Bind("PayReqDate","{0:dd/MM/yyyy}") %>'></asp:Label>
            </ItemTemplate>

<ControlStyle Width="80px"></ControlStyle>

<HeaderStyle Width="80px"></HeaderStyle>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Description"  ControlStyle-Width="250" HeaderStyle-Width="250">
            <ItemTemplate>
                <asp:Label ID="Label11" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
            </ItemTemplate>

<ControlStyle Width="250px"></ControlStyle>

<HeaderStyle Width="250px"></HeaderStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="SupplierName"  ControlStyle-Width="200" HeaderStyle-Width="200">
            <ItemTemplate>
                <asp:Label ID="Label10" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
            </ItemTemplate>

<ControlStyle Width="200px"></ControlStyle>

<HeaderStyle Width="200px"></HeaderStyle>
        </asp:TemplateField>
        
<asp:TemplateField  HeaderText="Invoice No"  ControlStyle-Width="150" HeaderStyle-Width="150" ItemStyle-HorizontalAlign="Right">
                       <ItemTemplate>
                           <asp:Label ID="Label3" runat="server" Text='<%# Bind("Invoice_No") %>'></asp:Label>
                       </ItemTemplate>
                       <ControlStyle Width="150px" />
                       <HeaderStyle Width="150px" />
                       <ItemStyle HorizontalAlign="Right" />
</asp:TemplateField>

<asp:TemplateField  HeaderText="Invoice Date"  ControlStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
                       <ItemTemplate>
                           <asp:Label ID="Label2" runat="server" Text='<%# Bind("Invoice_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                       </ItemTemplate>
                       <ControlStyle Width="80px" />
                       <HeaderStyle Width="80px" />
                       <ItemStyle HorizontalAlign="Right" />
</asp:TemplateField>
        
        
            <asp:TemplateField HeaderText="Invoice Value exc.VAT"  ItemStyle-HorizontalAlign="Right" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="Labelinvoicevalue" runat="server" Text='<%# Bind("InvoiceValue") %>'></asp:Label>
                </ItemTemplate>

<ControlStyle Width="80px"></ControlStyle>

<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>        
        
        <asp:TemplateField HeaderText=""  >
            <ItemTemplate>
    <asp:Panel ID="PanelCurrency" runat="server" CssClass="hidepanel">
                <asp:Label ID="LabelPO_Currency" runat="server" Text='<%# Bind("PO_Currency") %>' ></asp:Label>
    </asp:Panel>                 
            </ItemTemplate>
        </asp:TemplateField>
        
        
        <asp:TemplateField HeaderText="SiteRecordNo"  ControlStyle-Width="120" HeaderStyle-Width="120" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <asp:Label ID="Label4" runat="server" Text='<%# Bind("SiteRecordNo") %>'></asp:Label>
            </ItemTemplate>
            <FooterTemplate>
            T O T A L
            </FooterTemplate>

<ControlStyle Width="120px"></ControlStyle>

<HeaderStyle Width="120px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="EuroWithVAT" ControlStyle-Width="120" HeaderStyle-Width="120" ItemStyle-HorizontalAlign="Right" >

            <ItemTemplate>
                <asp:Label ID="LabelEuroWithVAT" runat="server" Text='<%# Bind("EuroWithVAT") %>'></asp:Label>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Label ID="LabelEuroWithVATFooter" runat="server" ></asp:Label>            
            </FooterTemplate>

<ControlStyle Width="120px"></ControlStyle>

<HeaderStyle Width="120px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField  HeaderText="DollarWithVAT" ControlStyle-Width="120" HeaderStyle-Width="120" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <asp:Label ID="LabelDollarWithVAT" runat="server" Text='<%# Bind("DollarWithVAT") %>'></asp:Label>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Label ID="LabelDollarWithVATFooter" runat="server" ></asp:Label>
            </FooterTemplate>

<ControlStyle Width="120px"></ControlStyle>

<HeaderStyle Width="120px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField  HeaderText="RubleWithVAT" ControlStyle-Width="120" HeaderStyle-Width="120" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <asp:Label ID="LabelRubleWithVAT" runat="server" Text='<%# Bind("RubleWithVAT") %>'></asp:Label>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Label ID="LabelRubleWithVATFooter" runat="server" ></asp:Label>
            </FooterTemplate>
<ControlStyle Width="120px"></ControlStyle>

<HeaderStyle Width="120px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField  HeaderText="RubleExcVAT" ControlStyle-Width="120" HeaderStyle-Width="120" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <asp:Label ID="LabelRubleExcVAT" runat="server" Text='<%# Bind("RubleExcVAT") %>'></asp:Label>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Label ID="LabelRubleExcVATFooter" runat="server" ></asp:Label>
            </FooterTemplate>

<ControlStyle Width="120px"></ControlStyle>

<HeaderStyle Width="120px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
        </asp:TemplateField>
                
    </Columns>
       <PagerSettings Position="TopAndBottom" />
       <RowStyle  CssClass="GridItemNakladnaya" />
       <HeaderStyle  CssClass="GridHeader" />
            <PagerStyle CssClass="pager" />
</asp:GridView>

 </asp:Panel>     
 

</asp:Content>


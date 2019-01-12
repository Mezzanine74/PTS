<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="POforeignCurrency.aspx.vb" Inherits="POForeignCurrency" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <table >
        <tr>
            <td style="text-align: center" >
        <asp:ImageButton ID="ImageButton1" runat="server" Width="20px" ToolTip="Export To Excel"
            ImageUrl="~/Images/Excel.jpg" />
                    <asp:DropDownList ID="DropDownListPrj" runat="server" AutoPostBack="True" 
                        CssClass="DrpDwnListGeneral" DataSourceID="SqlDataSourcePrj" 
                        DataTextField="ProjectName" DataValueField="ProjectID" Width="267px">
                    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                SelectCommand="SELECT     dbo.Table1_Project.ProjectID, (RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5),dbo.Table1_Project.ProjectID))) as ProjectName FROM dbo.Table1_Project WHERE dbo.Table1_Project.CurrentStatus=1 ORDER BY (RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5),dbo.Table1_Project.ProjectID))) ASC ">
    </asp:SqlDataSource>
               </td>
        </tr>
        <tr>
            <td>
    <asp:SqlDataSource ID="SqlDataSourcePOweek" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" >
    </asp:SqlDataSource>
    <asp:GridView ID="GridViewPOweek" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="ProjectID" DataSourceID="SqlDataSourcePOweek" CssClass="Grid" 
                    EmptyDataText="No new PO yet" AllowPaging="True" PageSize="100" PagerSettings-Position="TopAndBottom">
        <Columns>
            <asp:TemplateField HeaderText="ProjectName" SortExpression="ProjectName" itemstyle-Width="150" HeaderStyle-Width="150">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ProjectName") %>'></asp:Label>
                </ItemTemplate>


            </asp:TemplateField>
            <asp:TemplateField HeaderText="PO_No" SortExpression="PO_No" itemstyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("PO_No") %>'></asp:Label>
                </ItemTemplate>

<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle Width="80px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SupplierName" SortExpression="SupplierName" itemstyle-Width="150" HeaderStyle-Width="150">
                <ItemTemplate>
                    <asp:Label ID="LabelSupplierName" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                </ItemTemplate>

            </asp:TemplateField>

            <asp:TemplateField HeaderText="PO_Date" SortExpression="PO_Date" ItemStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("PO_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>

<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="TotalPrice With VAT" SortExpression="TotalPriceWithVAT" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("TotalPriceWithVAT","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>

<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField >
                <ItemTemplate>
		    <asp:Panel ID="PanelCurrency" runat="server" CssClass="hidepanel">
                      <asp:Label ID="LabelCurrency" runat="server" Text='<%# Bind("PO_Currency") %>'></asp:Label>
                    <asp:Label ID="LabelIfNull" visible="false" runat="server" Text='<%# Bind("IfNull") %>'></asp:Label>                      
		    </asp:Panel>
                    <asp:Image ID="ImageCurrency" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="TotalPrice Exc VAT" SortExpression="TotalPriceExcVAT" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="LabelTotalPriceExcVAT" runat="server" Text='<%# Bind("TotalPriceExcVAT","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>

<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PaidWthVAT" SortExpression="PaidWthVAT" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="LabelPaidWthVAT" runat="server" Text='<%# Bind("PaidWthVAT","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>

<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PendingWthVAT" SortExpression="PendingWthVAT" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="LabelPendingWthVAT" runat="server" Text='<%# Bind("PendingWthVAT","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>

<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="BalanceWthVAT" SortExpression="BalanceWthVAT" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="LabelBalanceWthVAT" runat="server" Text='<%# Bind("BalanceWthVAT","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>

<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
            </asp:TemplateField>
        </Columns>
           <PagerSettings Position="TopAndBottom" />
           <RowStyle  CssClass="GridItemNakladnaya" />
           <HeaderStyle  CssClass="GridHeader" />
           <pagerstyle  horizontalalign="Center" CssClass="pager2" />
    </asp:GridView>
    
        <asp:SqlDataSource ID="SqlDataSourceTotal" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" >
    </asp:SqlDataSource>
    
        <asp:GridView ID="GridViewTotalSum" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="ProjectID"  CssClass="Grid" ShowHeader="False" DataSourceID="SqlDataSourceTotal">
        <Columns>
            <asp:TemplateField  itemstyle-Width="150" HeaderStyle-Width="150">
 
            </asp:TemplateField>
            <asp:TemplateField itemstyle-Width="80" HeaderStyle-Width="80">

            </asp:TemplateField>
            <asp:TemplateField  itemstyle-Width="150" HeaderStyle-Width="150">

            </asp:TemplateField>

            <asp:TemplateField  ItemStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">

            </asp:TemplateField>
            <asp:TemplateField  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80" HeaderStyle-Width="80" ItemStyle-BackColor="#333333" ItemStyle-ForeColor="White">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("TotalPriceWithVAT","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField >
                <ItemTemplate>
		    <asp:Panel ID="PanelCurrency" runat="server" CssClass="hidepanel">
                      <asp:Label ID="LabelCurrency" runat="server" Text='<%# Bind("PO_Currency") %>'></asp:Label>
		    </asp:Panel>
                    <asp:Image ID="ImageCurrency" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80" HeaderStyle-Width="80" ItemStyle-BackColor="#333333" ItemStyle-ForeColor="White">
                <ItemTemplate>
                    <asp:Label ID="LabelTotalPriceExcVAT" runat="server" Text='<%# Bind("TotalPriceExcVAT","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>

            </asp:TemplateField>
            <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80" HeaderStyle-Width="80" ItemStyle-BackColor="#333333" ItemStyle-ForeColor="White">
                <ItemTemplate>
                    <asp:Label ID="LabelPaidWthVAT" runat="server" Text='<%# Bind("PaidWthVAT","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>

            </asp:TemplateField>
            <asp:TemplateField  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="89" HeaderStyle-Width="89" ItemStyle-BackColor="#333333" ItemStyle-ForeColor="White">
                <ItemTemplate>
                    <asp:Label ID="LabelPendingWthVAT" runat="server" Text='<%# Bind("PendingWthVAT","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>

            </asp:TemplateField>
            <asp:TemplateField  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="89" HeaderStyle-Width="89" ItemStyle-BackColor="#333333" ItemStyle-ForeColor="White">
                <ItemTemplate>
                    <asp:Label ID="LabelBalanceWthVAT" runat="server" Text='<%# Bind("BalanceWthVAT","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>

            </asp:TemplateField>
        </Columns>
    </asp:GridView>
               </td>
        </tr>
    </table>

    <asp:Panel ID="PAnelHiddden" runat="server" CssClass="hidepanel">
        <asp:GridView ID="GridViewToExcel" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="ProjectID" CssClass="Grid" 
                    EmptyDataText="No new PO yet" AllowPaging="True" PageSize="100" PagerSettings-Position="TopAndBottom">
        <Columns>
            <asp:TemplateField HeaderText="ProjectName" SortExpression="ProjectName" itemstyle-Width="150" HeaderStyle-Width="150">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ProjectName") %>'></asp:Label>
                </ItemTemplate>


            </asp:TemplateField>
            <asp:TemplateField HeaderText="PO_No" SortExpression="PO_No" itemstyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("PO_No") %>'></asp:Label>
                </ItemTemplate>

<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle Width="80px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SupplierName" SortExpression="SupplierName" itemstyle-Width="150" HeaderStyle-Width="150">
                <ItemTemplate>
                    <asp:Label ID="LabelSupplierName" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                </ItemTemplate>

            </asp:TemplateField>

            <asp:TemplateField HeaderText="PO_Date" SortExpression="PO_Date" ItemStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("PO_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>

<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="TotalPrice With VAT" SortExpression="TotalPriceWithVAT" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("TotalPriceWithVAT","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>

<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField >
                <ItemTemplate>
                    <asp:Label ID="LabelCurrency" runat="server" Text='<%# Bind("PO_Currency") %>'></asp:Label>
                    <asp:Label ID="LabelIfNull" visible="false" runat="server" Text='<%# Bind("IfNull") %>'></asp:Label>                      
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="TotalPrice Exc VAT" SortExpression="TotalPriceExcVAT" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="LabelTotalPriceExcVAT" runat="server" Text='<%# Bind("TotalPriceExcVAT","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>

<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PaidWthVAT" SortExpression="PaidWthVAT" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="LabelPaidWthVAT" runat="server" Text='<%# Bind("PaidWthVAT","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>

<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PendingWthVAT" SortExpression="PendingWthVAT" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="LabelPendingWthVAT" runat="server" Text='<%# Bind("PendingWthVAT","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>

<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="BalanceWthVAT" SortExpression="BalanceWthVAT" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="LabelBalanceWthVAT" runat="server" Text='<%# Bind("BalanceWthVAT","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>

<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
            </asp:TemplateField>
        </Columns>
           <PagerSettings Position="TopAndBottom" />
           <RowStyle  CssClass="GridItemNakladnaya" />
           <HeaderStyle  CssClass="GridHeader" />
           <pagerstyle  horizontalalign="Center" CssClass="pager2" />
    </asp:GridView>

        <asp:GridView ID="GridViewSumToExcel" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="ProjectID"  CssClass="Grid" ShowHeader="False">
        <Columns>
            <asp:TemplateField  itemstyle-Width="150" HeaderStyle-Width="150">
 
            </asp:TemplateField>
            <asp:TemplateField itemstyle-Width="80" HeaderStyle-Width="80">

            </asp:TemplateField>
            <asp:TemplateField  itemstyle-Width="150" HeaderStyle-Width="150">

            </asp:TemplateField>

            <asp:TemplateField  ItemStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">

            </asp:TemplateField>
            <asp:TemplateField  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80" HeaderStyle-Width="80" ItemStyle-BackColor="#333333" ItemStyle-ForeColor="White">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("TotalPriceWithVAT","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField >
                <ItemTemplate>
                      <asp:Label ID="LabelCurrency" runat="server" Text='<%# Bind("PO_Currency") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80" HeaderStyle-Width="80" ItemStyle-BackColor="#333333" ItemStyle-ForeColor="White">
                <ItemTemplate>
                    <asp:Label ID="LabelTotalPriceExcVAT" runat="server" Text='<%# Bind("TotalPriceExcVAT","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>

            </asp:TemplateField>
            <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80" HeaderStyle-Width="80" ItemStyle-BackColor="#333333" ItemStyle-ForeColor="White">
                <ItemTemplate>
                    <asp:Label ID="LabelPaidWthVAT" runat="server" Text='<%# Bind("PaidWthVAT","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>

            </asp:TemplateField>
            <asp:TemplateField  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="89" HeaderStyle-Width="89" ItemStyle-BackColor="#333333" ItemStyle-ForeColor="White">
                <ItemTemplate>
                    <asp:Label ID="LabelPendingWthVAT" runat="server" Text='<%# Bind("PendingWthVAT","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>

            </asp:TemplateField>
            <asp:TemplateField  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="89" HeaderStyle-Width="89" ItemStyle-BackColor="#333333" ItemStyle-ForeColor="White">
                <ItemTemplate>
                    <asp:Label ID="LabelBalanceWthVAT" runat="server" Text='<%# Bind("BalanceWthVAT","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>

            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    
    </asp:Panel>





</asp:Content>


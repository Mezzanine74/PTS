<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="open_po.aspx.vb" Inherits="open_po" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <asp:DropDownList ID="DropDownListPrj" runat="server" 
        DataSourceID="SqlDataSourcePrj" DataTextField="ProjectName" 
        DataValueField="ProjectID" AutoPostBack="True">
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        
        SelectCommand="SELECT TOP (100) PERCENT Table1_Project.ProjectID, Table1_Project.ProjectName, aspnet_Users.UserName FROM Table1_Project INNER JOIN Table_Prj_User_Junction ON Table1_Project.ProjectID = Table_Prj_User_Junction.ProjectID INNER JOIN aspnet_Users ON Table_Prj_User_Junction.UserID = aspnet_Users.UserId WHERE (aspnet_Users.UserName = @UserName) ORDER BY Table1_Project.ProjectName">
        <SelectParameters>
            <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" 
                PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>

    <hr />

    <asp:GridView ID="GridViewOpenPO" runat="server" AutoGenerateColumns="False" GridLines="None" 
        DataSourceID="SqlDataSourceOpenPO" DataKeyNames="PO_No" CssClass="table table-nonfluid table-hover" AllowPaging="False" AllowSorting="True" PagerSettings-Position="TopAndBottom">
        <Columns>
            <asp:TemplateField HeaderText="PO No" SortExpression="PO_No" ItemStyle-Width="100" HeaderStyle-Width="100">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("PO_No") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Supplier" SortExpression="SupplierName" ItemStyle-Width="100" HeaderStyle-Width="100">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description" SortExpression="Description" ItemStyle-Width="130" HeaderStyle-Width="130">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PoSumExcVAT" SortExpression="PoSumExcVAT" ItemStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("PoSumExcVAT","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  ItemStyle-Width="50" HeaderStyle-Width="50">
                <ItemTemplate>
		    <asp:Panel ID="PanelCurrency" runat="server" CssClass="hidepanel">
                      <asp:Label ID="LabelCurrency" runat="server" Text='<%# Bind("PO_Currency") %>'></asp:Label>
		    </asp:Panel>
                    <asp:Image ID="ImageCurrency" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="To Go" SortExpression="Balance" ItemStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("Balance","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="% to go" SortExpression="Percentage" ItemStyle-Width="70" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("Percentage","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
                <PagerSettings Position="TopAndBottom" />
                <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
                <HeaderStyle CssClass="headergridnew" />

    </asp:GridView>

    <asp:TextBox ID="TextBoxUserName" runat="server" Visible="False"></asp:TextBox>
    
    <asp:SqlDataSource ID="SqlDataSourceOpenPO" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand=" SELECT     TOP (100) PERCENT dbo.Table2_PONo.PO_No, RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName, RTRIM(dbo.Table2_PONo.Description) 
                      AS Description, CONVERT(decimal(12, 2), 
                      (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table2_POno.TotalPrice ELSE dbo.Table2_POno.TotalPrice / ((100 + dbo.Table2_POno.VATpercent)
                       / 100) END)) AS PoSumExcVAT, RTRIM(dbo.Table2_PONo.PO_Currency) AS PO_Currency, CONVERT(decimal(12, 2), 
                      (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table2_POno.TotalPrice ELSE dbo.Table2_POno.TotalPrice / ((100 + dbo.Table2_POno.VATpercent)
                       / 100) END)) - SUM(CONVERT(decimal(12, 2), (CASE WHEN dbo.Table3_Invoice.InvoiceValue IS NULL 
                      THEN 0 ELSE dbo.Table3_Invoice.InvoiceValue END))) AS Balance, ABS(CONVERT(decimal(12, 2), 
                      (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table2_POno.TotalPrice ELSE dbo.Table2_POno.TotalPrice / ((100 + dbo.Table2_POno.VATpercent)
                       / 100) END)) - SUM(CONVERT(decimal(12, 2), (CASE WHEN dbo.Table3_Invoice.InvoiceValue IS NULL 
                      THEN 0 ELSE dbo.Table3_Invoice.InvoiceValue END)))) / CONVERT(decimal(12, 2), 
                      (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table2_POno.TotalPrice ELSE dbo.Table2_POno.TotalPrice / ((100 + dbo.Table2_POno.VATpercent)
                       / 100) END)) * 100 AS Percentage
FROM         dbo.Table2_PONo INNER JOIN
                      dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID LEFT OUTER JOIN
                      dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No
WHERE     (dbo.Table2_PONo.Project_ID = @ProjectID)
GROUP BY CONVERT(decimal(12, 2), 
                      (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table2_POno.TotalPrice ELSE dbo.Table2_POno.TotalPrice / ((100 + dbo.Table2_POno.VATpercent)
                       / 100) END)), dbo.Table2_PONo.PO_No, RTRIM(dbo.Table6_Supplier.SupplierName), RTRIM(dbo.Table2_PONo.Description), 
                      RTRIM(dbo.Table2_PONo.PO_Currency)
HAVING      (CONVERT(decimal(12, 2), 
                      (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table2_POno.TotalPrice ELSE dbo.Table2_POno.TotalPrice / ((100 + dbo.Table2_POno.VATpercent)
                       / 100) END)) - SUM(CONVERT(decimal(12, 2), (CASE WHEN dbo.Table3_Invoice.InvoiceValue IS NULL 
                      THEN 0 ELSE dbo.Table3_Invoice.InvoiceValue END))) > 0)
ORDER BY dbo.Table2_PONo.PO_No DESC " 
        >

        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" 
                Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
        </SelectParameters>
        
    </asp:SqlDataSource>
    
</asp:Content>


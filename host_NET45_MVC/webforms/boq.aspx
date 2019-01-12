<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="boq.aspx.vb" Inherits="boq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Search Invoice</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <table >
        <tr>
            <td>
    <asp:TextBox ID="TextBoxSearch" runat="server" Width="415px" CssClass="TextBoxGeneralRev"></asp:TextBox>
            </td>
            <td>
    <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-default btn-mini" />
            </td>
        </tr>
        </table>
       
      <asp:GridView ID="GridViewMonitor" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CssClass="Grid" 
        DataSourceID="SqlDataSourceMonitor" AllowSorting="True" PageSize="40">
            <Columns>
                <asp:TemplateField ControlStyle-Width="100" HeaderStyle-Width="100" 
                    HeaderText="Tender" SortExpression="Tender">
                    <ItemTemplate>
                        <asp:Label ID="LabelTender" runat="server" 
                            Text='<%# Bind("Tender") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="100px" />

<ControlStyle Width="100px"></ControlStyle>

                    <HeaderStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="100" HeaderStyle-Width="100" 
                    HeaderText="Category" SortExpression="Category">
                    <ItemTemplate>
                        <asp:Label ID="Category" runat="server" 
                            Text='<%# Bind("Category") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="100px" />

<ControlStyle Width="100px"></ControlStyle>

                    <HeaderStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="180" HeaderStyle-Width="180" 
                    HeaderText="Description" SortExpression="Description">
                    <ItemTemplate>
                        <asp:Label ID="LabelDescription" runat="server" 
                            Text='<%# Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="180px" />

<ControlStyle Width="180px"></ControlStyle>

                    <HeaderStyle Width="180px" />
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right"
                    HeaderText="Quantity" SortExpression="Quantity">
                    <ItemTemplate>
                        <asp:Label ID="LabelQuantity" runat="server" 
                            Text='<%# Bind("Quantity","{0:###,###,###.00}") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="100px" />

<ControlStyle Width="100px"></ControlStyle>

                    <HeaderStyle Width="100px" />

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderStyle-Width="50" ItemStyle-HorizontalAlign="Center"
                    HeaderText="Unit" SortExpression="Unit">
                    <ItemTemplate>
                        <asp:Label ID="LabelUnit" runat="server" 
                            Text='<%# Bind("Unit") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="50px" />

<ControlStyle Width="50px"></ControlStyle>

                    <HeaderStyle Width="50px" />

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right"
                    HeaderText="Material Rate Exc.VAT" SortExpression="MaterialUnitRates">
                    <ItemTemplate>
                        <asp:Label ID="LabelMaterialUnitRates" runat="server" 
                            Text='<%# Bind("MaterialUnitRates","{0:###,###,###.00}") %>' 
                            Font-Bold="True"></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="100px" />

<ControlStyle Width="100px"></ControlStyle>

                    <HeaderStyle Width="100px" />

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField >
                    <ItemTemplate>
		                <asp:Panel ID="PanelCurrency1" runat="server" CssClass="hidepanel">
                                  <asp:Label ID="LabelCurrency1" runat="server" Text='<%# Bind("Currency") %>'></asp:Label>
		                </asp:Panel>
                                <asp:Image ID="ImageCurrency1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right"
                    HeaderText="Labor Rate Exc.VAT" SortExpression="LaborUnitRates">
                    <ItemTemplate>
                        <asp:Label ID="LabelLaborUnitRates" runat="server" Font-Bold="True"
                            Text='<%# Bind("LaborUnitRates","{0:###,###,###.00}") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="100px" />

<ControlStyle Width="100px"></ControlStyle>

                    <HeaderStyle Width="100px" />

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField >
                    <ItemTemplate>
		                <asp:Panel ID="PanelCurrency2" runat="server" CssClass="hidepanel">
                                  <asp:Label ID="LabelCurrency2" runat="server" Text='<%# Bind("Currency") %>'></asp:Label>
		                </asp:Panel>
                                <asp:Image ID="ImageCurrency2" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderStyle-Width="50" ItemStyle-HorizontalAlign="Right"
                    HeaderText="WASTE" SortExpression="WASTE">
                    <ItemTemplate>
                        <asp:Label ID="LabelWASTE" runat="server" 
                            Text='<%# Bind("WASTE","{0:###,###,###.00}") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="50px" />

<ControlStyle Width="50px"></ControlStyle>

                    <HeaderStyle Width="50px" />

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right"
                    HeaderText="Total Rate" SortExpression="Rate">
                    <ItemTemplate>
                        <asp:Label ID="LabelRate" runat="server" 
                            Text='<%# Bind("Rate","{0:###,###,###.00}") %>' ForeColor="#999999"></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="100px" />

<ControlStyle Width="100px"></ControlStyle>

                    <HeaderStyle Width="100px" />

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="100" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Right"
                    HeaderText="TotalExcVAT" SortExpression="TotalExcVAT" >
                    <ItemTemplate>
                        <asp:Label ID="LabelTotalExcVAT" runat="server" ForeColor="#999999"
                            Text='<%# Bind("TotalExcVAT","{0:###,###,###.00}") %>' ></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="100px" />

<ControlStyle Width="100px"></ControlStyle>

                    <HeaderStyle Width="100px" />

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:TemplateField>
            </Columns>
            <PagerSettings Position="TopAndBottom" />
            <RowStyle CssClass="GridItemNakladnaya" />
            <HeaderStyle CssClass="GridHeader" />
            <PagerStyle CssClass="pager2" />
        </asp:GridView>

    <asp:Panel ID="PanelMonitor" runat="server" CssClass="HidePanel" >
    <asp:SqlDataSource ID="SqlDataSourceMonitor" runat="server"  
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" >
            
    </asp:SqlDataSource>
    </asp:Panel>    




</asp:Content>



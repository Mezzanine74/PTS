<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="FollowUsers.aspx.vb" Inherits="FollowUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:SqlDataSource ID="SqlDataSourceFollowUsersx" runat="server"
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionStringVisitorsLog %>" 
    SelectCommand=" SELECT TOP 1000 * FROM [Table_VisitorLogs]  WHERE (UserName LIKE '%'+ @UserName + '%') AND (PageName LIKE '%'+ @PageName + '%')   ORDER BY VisitTime DESC" >
                        <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownListUserName" DefaultValue="" Name="UserName" 
                            PropertyName="SelectedValue" ConvertEmptyStringToNull="False" />
                        <asp:ControlParameter ControlID="DropDownListPageName" DefaultValue="" Name="PageName" 
                            PropertyName="SelectedValue" ConvertEmptyStringToNull="False" />
                    </SelectParameters>
        </asp:SqlDataSource>
    <table >
        <tr>
            <td style="width: 100px">
                <asp:DropDownList ID="DropDownListUserName" runat="server" 
                    DataSourceID="SqlDataSourceUserName" DataTextField="UserName" 
                    DataValueField="UserName" AutoPostBack="True" >
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceUserName" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionStringVisitorsLog %>" 
                    SelectCommand="SELECT [UserName] FROM [Table_VisitorLogs] GROUP BY [UserName] ORDER BY [UserName] ASC">
                </asp:SqlDataSource>
            </td>
            <td style="width: 200px">
                <asp:DropDownList ID="DropDownListPageName" runat="server" 
                    DataSourceID="SqlDataSourcePageName" DataTextField="PageName" 
                    DataValueField="PageName" AutoPostBack="True" >
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourcePageName" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionStringVisitorsLog %>" 
                    SelectCommand="SELECT [PageName] FROM [Table_VisitorLogs] GROUP BY [PageName] ORDER BY [PageName] ASC">
                </asp:SqlDataSource>
             </td>
        </tr>
    </table>

    <asp:GridView ID="GridView1" runat="server"  CssClass="Grid" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSourceFollowUsersx" DataKeyNames="Id" PageSize="200"
        AllowPaging="True" AllowSorting="True" PagerSettings-Position="TopAndBottom">
        <Columns>
        <asp:TemplateField HeaderText="User Name" SortExpression="UserName" ControlStyle-Width="100" HeaderStyle-Width="100">
        <ItemTemplate>
          <asp:Label ID="LabelUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
        </ItemTemplate>

<ControlStyle Width="100px"></ControlStyle>

<HeaderStyle Width="100px"></HeaderStyle>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Page Name" SortExpression="PageName" ControlStyle-Width="200" HeaderStyle-Width="200">
        <ItemTemplate>
          <asp:Label ID="LabelPageName" runat="server" Text='<%# Bind("PageName") %>'></asp:Label>
        </ItemTemplate>

<ControlStyle Width="200px"></ControlStyle>

<HeaderStyle Width="200px"></HeaderStyle>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Visit Time" SortExpression="VisitTime" ControlStyle-Width="200" HeaderStyle-Width="200">
        <ItemTemplate>
          <asp:Label ID="LabelVisitTime" runat="server" Text='<%# Bind("VisitTime") %>'></asp:Label>
        </ItemTemplate>

<ControlStyle Width="200px"></ControlStyle>

<HeaderStyle Width="200px"></HeaderStyle>
        </asp:TemplateField>

        <asp:TemplateField  HeaderText="Ip Adress" SortExpression="IpAdress" ControlStyle-Width="100" HeaderStyle-Width="100">
        <ItemTemplate>
          <asp:Label ID="LabelIPAdress" runat="server" Text='<%# Bind("IpAdress") %>'></asp:Label>
        </ItemTemplate>

<ControlStyle Width="100px"></ControlStyle>

<HeaderStyle Width="100px"></HeaderStyle>
        </asp:TemplateField>

        <asp:TemplateField   HeaderText="Country" SortExpression="Country" ControlStyle-Width="200" HeaderStyle-Width="200">
        <ItemTemplate>
          <asp:Label ID="LabelCountry" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
        </ItemTemplate>

<ControlStyle Width="200px"></ControlStyle>

<HeaderStyle Width="200px"></HeaderStyle>
        </asp:TemplateField>
        </Columns>
        <pagerstyle  horizontalalign="Center" CssClass="pager2" />
        <PagerSettings Position="TopAndBottom" />
        <RowStyle  CssClass="GridItemNakladnaya" />
        <HeaderStyle  CssClass="GridHeader" />
    </asp:GridView>
   
</asp:Content>


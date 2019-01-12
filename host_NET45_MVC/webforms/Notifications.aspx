<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="Notifications.aspx.vb" Inherits="NotificationsZOO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:Timer ID="TimerNotificationsRead" interval="5000" runat="server" Enabled="False"></asp:Timer>

<div style="text-align: center">
                              
                              <asp:DropDownList ID="DropDownListNotificationType" runat="server" 
                                AutoPostBack="True"  >
                                <asp:ListItem Value="0" Selected="True">ALL</asp:ListItem>
                                <asp:ListItem Value="1">Payment Request</asp:ListItem>
                                <asp:ListItem Value="2">Approval</asp:ListItem>
                                <asp:ListItem Value="3">Contract</asp:ListItem>
                            </asp:DropDownList>                            
</div>

  <asp:GridView ID="GridViewNotifications" runat="server" AutoGenerateColumns="False" 
    DataKeyNames="NotificationID" DataSourceID="SqlDataSourceNotifications" 
    CssClass="Grid" AllowPaging="True" PageSize="40" PagerSettings-Position="TopAndBottom"
    EnableModelValidation="True">
    <Columns>

      <asp:TemplateField HeaderText="Time Stamp" ItemStyle-Width="160px">
       <ItemTemplate>
        <asp:label ID="LabelTimeStamp" runat="server" Text='<%# Bind("TimeStamp","{0:f}") %>'></asp:label>
       </ItemTemplate>
      </asp:TemplateField>

      <asp:TemplateField HeaderText="Notification" SortExpression="Notification" ItemStyle-Width="800px">
       <ItemTemplate>
        <asp:label ID="LabelNotification" runat="server" Text='<%# Bind("Notification") %>'></asp:label>
       </ItemTemplate>
      </asp:TemplateField>
    </Columns>
        <RowStyle  CssClass="GridItemNakladnaya" Height="30px" />
        <HeaderStyle  CssClass="GridHeader" Height="30px" />
            <PagerStyle CssClass="pager2" />
  </asp:GridView>
  <asp:SqlDataSource ID="SqlDataSourceNotifications" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" >
</asp:SqlDataSource>


</asp:Content>


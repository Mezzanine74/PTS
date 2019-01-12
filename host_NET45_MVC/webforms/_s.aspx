<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="~/webforms/_s.aspx.vb" Inherits="_s" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
<dx:ASPxPopupControl runat="server" ID="Popup">
    <Windows>
        <dx:PopupWindow Name="AndrewFuller" HeaderText="Andrew Fuller" FooterText="Tacoma, USA"></dx:PopupWindow>
        <dx:PopupWindow Name="JanetLeverling" HeaderText="Janet Leverling" FooterText="Kirkland, USA"></dx:PopupWindow>
        <dx:PopupWindow Name="MargaretPeacock" HeaderText="Margaret Peacock" FooterText="Seattle, USA"></dx:PopupWindow>
        <dx:PopupWindow Name="NancyDavolio" HeaderText="Nancy Davolio" FooterText="Redmond, USA"></dx:PopupWindow>
    </Windows>
</dx:ASPxPopupControl>

</asp:Content>
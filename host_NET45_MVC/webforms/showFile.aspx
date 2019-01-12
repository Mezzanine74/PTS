<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="showFile.aspx.vb" Inherits="showFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    
    <div style="text-align: center;">
        <iframe runat="server" id="iframepdf" height="1000" width="1000"></iframe>
    </div>

</asp:Content>


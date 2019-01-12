<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="ClosePo.aspx.vb" Inherits="ClosePo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

  <br /><br />

  <div style="width: 100%; text-align: center">
      <asp:Button ID="Button1" runat="server" CssClass="btn btn-mini btn-default"
        Text="PRESS BUTTON TO CLOSE THIS PO" />
  </div>

    <br />

    <div style="width: 100%; text-align: center">
      <asp:Label ID="labelInfo" runat="server" Text=""></asp:Label>
    </div>

</asp:Content>


<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="PaymentsNotes.aspx.vb" Inherits="PaymentsNotes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
  TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

  <h2 style=" text-align:center; ">Update Your Notes. This will be attached to email</h2>

        <asp:Button Id="ButtonUpdate" runat="server" Text="Update" Visible="true" 
          CssClass="btn btn-mini"/>

        <cc1:Editor ID="EditorNotes" runat="server"
          Width="800px" Height="600px" Visible="true"/>

</asp:Content>


<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" EnableViewState="false" AutoEventWireup="false" CodeFile="AccessDenied.aspx.vb" Inherits="AccessDenied2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


    <div style="text-align: center">
    <img src="~/images/access-denied.png" alt="" />
     <h1>Access Denied</h1>
     <h4>You are not authorized to visit this page </h4>
    <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/webforms/Default.aspx" CssClass="btn btn-default">Main Page</asp:LinkButton>     
    </div>

</asp:Content>


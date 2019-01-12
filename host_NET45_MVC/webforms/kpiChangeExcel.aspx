<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="kpiChangeExcel.aspx.vb" Inherits="kpiChangeExcel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <asp:FileUpload ID="FileUploadChangeKPiExcel" runat="server" />
  <asp:Button ID="ButtonUpdateExcelFile" runat="server" Text="Button" />
</asp:Content>


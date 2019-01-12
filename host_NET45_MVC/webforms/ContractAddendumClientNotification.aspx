<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="ContractAddendumClientNotification.aspx.vb" Inherits="ContractAddendumClientNotification" %>

<%@ Register src="WebUserControl_ContractEmailBody.ascx" tagname="ContractEmailBody" tagprefix="uc1" %>

<%@ Register src="WebUserControl_AddendumEmailBody.ascx" tagname="AddendumEmailBody" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<uc1:ContractEmailBody ID="WebUserControl_ContractEmailBody" runat="server" />

<uc1:AddendumEmailBody ID="WebUserControl_AddendumEmailBody" runat="server" />

</asp:Content>


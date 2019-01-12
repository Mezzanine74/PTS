<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" EnableEventValidation="false" AutoEventWireup="false" CodeFile="PTS_1S.aspx.vb" Inherits="PTS_1S" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="WebUserControl_PTS1Scomparison.ascx" tagname="SeperateControl1" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    Select Date >
    <asp:TextBox ID="PaymentDateTextBox" runat="server" CssClass="TextBoxGeneralRev add_datepicker" AutoPostBack="True" Width="80px" />

    <asp:ImageButton ID="ImageButtonExcel" runat="server" Width="20px" ToolTip="Export To Excel"
    ImageUrl="~/Images/Excel.jpg" OnClick="ImageButtonExcel_Click" />

    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
    ControlToValidate="PaymentDateTextBox" CssClass="LabelGeneral" 
    ErrorMessage="dd/mm/yyyy" Display="Dynamic"
    ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"></asp:RegularExpressionValidator>

    <uc1:SeperateControl1 ID="WebUserControl_PTS1Scomparison" runat="server" />

</asp:Content>


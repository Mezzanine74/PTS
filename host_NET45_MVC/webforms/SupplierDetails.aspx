<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="SupplierDetails.aspx.vb" Inherits="SupplierDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


  <asp:Panel ID="PanelSupplierDetails" runat="server"  >

    <table style="background-color:white; padding:15px; border-color:black; border-style:solid; border-width:5px;">
        <tr>
            <td colspan="2" style="text-align:center; background-color:#FFD700;">
            <span style="font-weight:bold"><asp:Literal ID="LiteralHeadSupplierDetails" runat="server" /></span>
            </td>
        </tr>
        <tr>
            <td style="width:400px; padding:15px;">
                <span style="font-weight:bold">Supplier Adress Details In English</span>
                <br />
                <asp:Literal ID="LiteralHTMLSupplierAdressDetailsENG" runat="server" />
            </td>
            <td style="width:400px; padding:15px;">
                <span style="font-weight:bold">Supplier Adress Details In Russian</span>
                <br />
                <asp:Literal ID="LiteralHTMLSupplierAdressDetailsRUS" runat="server"  />
            </td>
        </tr>
        <tr>
            <td style="width:400px; padding:15px;">
                <span style="font-weight:bold">Supplier Banking Details In English</span>
                <br />
                <asp:Literal ID="LiteralHTMLSupplierBankingDetailsENG" runat="server" />
            </td>
            <td style="width:400px; padding:15px;">
                <span style="font-weight:bold">Supplier Banking Details In Russian</span>
                <br />
                <asp:Literal ID="LiteralHTMLSupplierBankingDetailsRUS" runat="server"  />
            </td>
        </tr>
    </table>

  </asp:Panel>

</asp:Content>


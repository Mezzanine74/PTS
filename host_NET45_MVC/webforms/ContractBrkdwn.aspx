<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="ContractBrkdwn.aspx.vb" Inherits="ContractBrkdwn" ViewStateMode="Disabled" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


  <%-- MODAL POPUP PAYREQNO History --%>
  <asp:ModalPopupExtender ID="ModalPopupExtenderSupplierDetails" runat="server"
   TargetControlID="ButtonSupplierDetails"
   PopupControlID="PanelSupplierDetails"
   BackgroundCssClass="modalBackground"
   CancelControlID="btnCancelSupplierDetails"
   >
  </asp:ModalPopupExtender>
  <asp:Panel ID="PanelSupplierDetails" runat="server" Style="display:none;" >
     <div style="text-align: right">
                <asp:Button ID="btnCancelSupplierDetails" runat="server" Text="X" 
                 Font-Bold="True" Font-Size="16px" ForeColor="White" BackColor="#FF0066" 
                 OnClientClick="changeClass" />
     </div>

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
  <asp:Button id="ButtonSupplierDetails"  runat="server" CssClass="hidepanel"/>
    <%-- /MODAL POPUP PAYREQNO History --%>  

    <div style="font-size:small">
        <font style="color:gray;">Project Name:</font> <asp:Literal ID="LtrlProjectName" runat="server"></asp:Literal>
        <br />
        <font style="color:gray;">Supplier Name:</font> <asp:Literal ID="LiteralSupplierName" runat="server"></asp:Literal>
        <asp:LinkButton ID="LinkButtonSupplierDetails" runat="server" ForeColor="red"> See Supplier Details</asp:LinkButton>
        <br />
        <font style="color:gray;">Contract Description:</font> <asp:Literal ID="LiteralDescription" runat="server"></asp:Literal>
        <br />
        <font style="color:gray;">Contract Value:</font> <asp:Literal ID="LiteralContractValue" runat="server"></asp:Literal>
        <br />
        <font style="color:gray;">Contract Currency:</font> <asp:Literal ID="LiteralCurrency" runat="server"></asp:Literal>
        <br />
        <asp:HyperLink ID="HypLkContractDetails" runat="server" Target="_blank" ForeColor="Red">
            Click To See Contract Details With Links To Attchments
        </asp:HyperLink>
    </div>

    <br />

    <font style="font-style:italic; font-size:12px;">You searched for:</font>
    <font style="font-weight:bold; font-size:12px;"><asp:Literal ID="LiteralSearchCriteria" runat="server" ></asp:Literal></font>

    <br />

    <span style="color:darkgrey; font-weight:bold; font-size:14px;">
        THIS BREAKDOWN MANUALLY EXTRACTED FROM CONTRACT ATTACHMENT.
        (Export to Excel<asp:ImageButton ID="ImageButtonExcel" runat="server" ImageUrl="~/images/excel.png" />)
    </span>

    <br />


    <asp:Literal ID="LiteralHTML" runat="server" ></asp:Literal>

</asp:Content>


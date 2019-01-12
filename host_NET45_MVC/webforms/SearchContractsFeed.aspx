<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" culture="ru-RU" uiCulture="ru-RU" 
    AutoEventWireup="false" CodeFile="SearchContractsFeed.aspx.vb" Inherits="SearchContractsFeed" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
  TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <table>
        <tr>
            <td>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True">Contract</asp:ListItem>
                    <asp:ListItem>Addendum</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                ID: <asp:TextBox ID="TxContractID" runat="server" Font-Size="Large" ForeColor="Blue" Height="30px"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTxContractID" runat="server" ErrorMessage="Required" ControlToValidate="TxContractID"></asp:RequiredFieldValidator>

                <asp:Button ID="BtnGetData" runat="server" Text="Save Data" Font-Size="Large" Height="100px" Width="300px" CssClass="btn" />
            </td>
        </tr>
    </table>

    <br />

    <table>
        <tr>
            <td >
                <span style="font-weight:bold">Breakdown</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorHTMLbrkDwn" runat="server" ErrorMessage="Required" ControlToValidate="HTMLbrkDwn"></asp:RequiredFieldValidator>
                <cc1:Editor ID="HTMLbrkDwn" runat="server" Width="500px" Height="350px" />
            </td>
            <td>
                <asp:Literal ID="LiteralbrkDwn" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td >
                <span style="font-weight:bold">Clear Text</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorEditorHTMLcleanText" runat="server" ErrorMessage="Required" ControlToValidate="EditorHTMLcleanText"></asp:RequiredFieldValidator>
                 <br />
                <asp:textBox ID="EditorHTMLcleanText" runat="server" Width="500px" Height="350px" TextMode="MultiLine" />
            </td>
            <td >
                <asp:Literal ID="LiteralEditorHTMLcleanText" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>

    <hr />

    <asp:Panel ID="PanelSupplierDetails" runat="server" >

    <table>
        <tr>
            <td>
                Supplier ID: <asp:TextBox ID="TxSupplierID" runat="server" Font-Size="Large" ForeColor="Blue" Height="30px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTxSupplierID" runat="server" ErrorMessage="Required" ControlToValidate="TxSupplierID"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>

    <table>
        <tr>
            <td style="width:400px;">
                <span style="font-weight:bold">Supplier Adress Details In English</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorHTMLSupplierAdressDetailsENG" runat="server" ErrorMessage="Required" ControlToValidate="HTMLSupplierAdressDetailsENG"></asp:RequiredFieldValidator>
                <asp:TextBox ID="HTMLSupplierAdressDetailsENG" runat="server" Width="400px" Height="250px" TextMode="MultiLine" />
            </td>
            <td style="width:400px;">
                <span style="font-weight:bold">Supplier Adress Details In Russian</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorHTMLSupplierAdressDetailsRUS" runat="server" ErrorMessage="Required" ControlToValidate="HTMLSupplierAdressDetailsRUS"></asp:RequiredFieldValidator>
                <asp:TextBox ID="HTMLSupplierAdressDetailsRUS" runat="server" Width="400px" Height="250px" TextMode="MultiLine" />
            </td>
        </tr>
        <tr>
            <td style="width:400px;">
                <span style="font-weight:bold">Supplier Banking Details In English</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorHTMLSupplierBankingDetailsENG" runat="server" ErrorMessage="Required" ControlToValidate="HTMLSupplierBankingDetailsENG"></asp:RequiredFieldValidator>
                <asp:TextBox ID="HTMLSupplierBankingDetailsENG" runat="server" Width="400px" Height="250px" TextMode="MultiLine" />
            </td>
            <td style="width:400px;">
                <span style="font-weight:bold">Supplier Banking Details In Russian</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorHTMLSupplierBankingDetailsRUS" runat="server" ErrorMessage="Required" ControlToValidate="HTMLSupplierBankingDetailsRUS"></asp:RequiredFieldValidator>
                <asp:TextBox ID="HTMLSupplierBankingDetailsRUS" runat="server" Width="400px" Height="250px" TextMode="MultiLine" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Literal ID="LiteralSupplierID" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr style="font-size:10px; background-color:ghostwhite;">
            <td style="width:400px; padding:3px;">
                <asp:Literal ID="LiteralSupplierAdressDetailsENG" runat="server"></asp:Literal>
            </td>
            <td style="width:400px; padding:3px;">
                <asp:Literal ID="LiteralSupplierAdressDetailsRUS" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr style="font-size:10px; background-color:ghostwhite;">
            <td style="width:400px; padding:3px;">
                <asp:Literal ID="LiteralSupplierBankingDetailsENG" runat="server"></asp:Literal>
            </td>
            <td style="width:400px; padding:3px;">
                <asp:Literal ID="LiteralSupplierBankingDetailsRUS" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>

    </asp:Panel>

</asp:Content>


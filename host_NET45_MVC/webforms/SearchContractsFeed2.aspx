<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" culture="ru-RU" uiCulture="ru-RU" 
    AutoEventWireup="false" CodeFile="SearchContractsFeed2.aspx.vb" Inherits="SearchContractsFeed2" %>

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

                &nbsp;&nbsp;

                <asp:Button ID="BtnGetData" runat="server" Text="Save Data" Font-Size="Large" Height="35px" Width="250px" />
            </td>
        </tr>
    </table>

    <br />

    <table style="display:none;">
        <tr>
            <td colspan="2">
                <span style="font-weight:bold">Breakdown</span>
                <cc1:Editor ID="HTMLbrkDwn" runat="server" Width="800px" Height="350px" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Literal ID="LiteralbrkDwn" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    
                <asp:textBox ID="EditorHTMLcleanText" runat="server" Width="800px" Height="350px" TextMode="MultiLine" />

</asp:Content>


<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WebUserControlCheckBoxBoostrap.ascx.vb" Inherits="WebUserControlCheckBoxBoostrap" %>


<label>
    <asp:CheckBox ID="CheckBox" runat="server" 
        OnCheckedChanged="CheckBox_CheckedChanged" 
        OnPreRender="CheckBox_PreRender"/>
    <span class="lbl">
        <asp:Literal ID="LiteralCheckBox" runat="server"></asp:Literal>
    </span>
</label>


